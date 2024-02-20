using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.system.Models;
using ems.system.DataAccess;
using System.Configuration;
using System.IO;
using System.Text;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using ems.storage.Functions;
using System.Threading;
using System.Reflection;
using static OfficeOpenXml.ExcelErrorValue;
using System.Security.Cryptography;
using Microsoft.AspNet.Identity;

namespace ems.system.DataAccess
{

    public class DaSystemMaster
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader, objODBCDatareader1;
        string msSQL, msGetGid, msGet_LocationGid, clusterGID, msGet_clusterGid, regionGID, msGet_regionGid, msGetTaskCode, msGetUserCode,
            msGetTask2AssignedToGid, msGetTask2EscalationMailToGid, msGetHRCode, msGetHR2NotifyToGid, msGetAPICode, msGetsystem_ownername_gid;
        int mnResult, mnResultSub1, mnResultSub2;
        string lslevelonemodule_gid, module_gid, lsleveltwomodule_gid, module_gid_parent, lsleveltwomodulestatus_gid, lslevelonemodulestatus_gid;
        string lsmaster_value, lslms_code, lsbureau_code, lsbase_value, lssalutation_value, lsproject_value, lsbloodgroup_value, lsdocumentgid;
        string lsleveloneparent_gid, lsleveltwoparent_gid, lslevelthreeparent_gid, lsleveltwo_name, lslevelone_name,
           lslevelthree_name1, lsleveltwo_name1, lslevelone_name1;
        string lscreated_date, lscreated_by, lsleveltwomodule1_gid, lslevelthreeparent1_gid, lsleveltwoparent1_gid, lsleveloneparent1_gid,
             lsleveltwomodulemenu_gid, lslevelonemodulemenu_gid, lsuser_gid;
        string lsemployee_gid, lsemployee_name, lsemployeegroup_gid, lsemployeegroup_name, lslevelfourparent_gid, lslevelthree_name, lslevelthreemodule_gid;
        string lsuser_code, lsexternalsystem_name;
        //Blood Group

        public void DaGetBloodGroup(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT a.bloodgroup_gid ,a.api_code,a.bloodgroup_name,a.lms_code, a.bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tbloodgroup a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid where a.delete_flag='N' order by a.bloodgroup_gid  desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            bloodgroup_gid = (dr_datarow["bloodgroup_gid"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                            bloodgroup_name = (dr_datarow["bloodgroup_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    objmaster.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }


        public void DaGetBloodGroupActive(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT a.bloodgroup_gid ,a.bloodgroup_name FROM sys_mst_tbloodgroup a where a.delete_flag='N' and a.status='Y' and delete_flag='N' order by a.bloodgroup_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            bloodgroup_gid = (dr_datarow["bloodgroup_gid"].ToString()),
                            bloodgroup_name = (dr_datarow["bloodgroup_name"].ToString()),
                        });
                    }
                    objmaster.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaCreateBloodGroup(master values, string employee_gid)
        {
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
            msSQL = "select bloodgroup_name from sys_mst_tbloodgroup where bloodgroup_name = '" + values.bloodgroup_name.Replace("'", "\\'") + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Blood Group Already Exist";
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("SBGT");
                msGetAPICode = objcmnfunctions.GetApiMasterGID("BLOD");
                msSQL = " insert into sys_mst_tbloodgroup(" +
                        " bloodgroup_gid ," +
                        " api_code," +
                        " lms_code," +
                        " bureau_code," +
                        " bloodgroup_name ," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + msGetAPICode + "'," +
                        "'" + lslms_code + "'," +
                        "'" + lsbureau_code + "'," +
                        "'" + values.bloodgroup_name.Replace("'", "") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Blood Group Added successfully";
                }
                else
                {
                    values.message = "Error Occured while Adding";
                    values.status = false;
                }
            }
        }
        public void DaEditBloodGroup(string bloodgroup_gid, master values)
        {
            try
            {
                msSQL = " SELECT bloodgroup_gid,bloodgroup_name,lms_code, bureau_code, status as Status FROM sys_mst_tbloodgroup " +
                        " where bloodgroup_gid='" + bloodgroup_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.bloodgroup_gid = objODBCDatareader["bloodgroup_gid"].ToString();
                    values.bloodgroup_name = objODBCDatareader["bloodgroup_name"].ToString();
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    values.status_bloodgroup = objODBCDatareader["status_bloodgroup"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }


        public void DaUpdateBloodGroup(string employee_gid, master values)
        {
            msSQL = "select bloodgroup_gid from sys_mst_tbloodgroup where delete_flag='N' and bloodgroup_name = '" + values.bloodgroup_name.Replace("'", "\\'") + "'";
            lsdocumentgid = objdbconn.GetExecuteScalar(msSQL);
            if (lsdocumentgid != "")
            {
                if (lsdocumentgid != values.bloodgroup_gid)
                {
                    values.message = "Blood group Name Already Exist";
                    values.status = false;
                    return;
                }
            }
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

            msSQL = " update sys_mst_tbloodgroup set " +
            " bloodgroup_name='" + values.bloodgroup_name.Replace("'", "") + "'," +
            " lms_code='" + lslms_code + "'," +
            " bureau_code='" + lsbureau_code + "'," +
            " updated_by='" + employee_gid + "'," +
            " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
            " where bloodgroup_gid='" + values.bloodgroup_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                //msGetGid = objcmnfunctions.GetMasterGID("MELG");
                msGetGid = objcmnfunctions.GetMasterGID("SBGL");
                msSQL = " insert into sys_mst_tbloodgrouplog(" +
                          " bloodgroup_loggid   ," +
                          " bloodgroup_gid," +
                          " bloodgroup_name , " +
                          " created_by, " +
                          " created_date) " +
                          " values(" +
                          "'" + msGetGid + "'," +
                          "'" + values.bloodgroup_gid + "'," +
                          "'" + lsbloodgroup_value + "'," +
                          "'" + employee_gid + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Blood Group Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }


        public void DaInactiveBloodGroup(master values, string employee_gid)
        {
            msSQL = " update sys_mst_tbloodgroup set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where bloodgroup_gid='" + values.bloodgroup_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("SBGI");

                msSQL = " insert into sys_mst_tbloodgroupinactivelog (" +
                      " bloodgroupinactivelog_gid   , " +
                      " bloodgroup_gid," +
                      " bloodgroup_name ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.bloodgroup_gid + "'," +
                      " '" + values.bloodgroup_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Blood Group Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Blood Group Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaDeleteBloodGroup(string bloodgroup_gid, string employee_gid, master values)
        {
            msSQL = " update sys_mst_tbloodgroup   set delete_flag='Y'," +
                    " deleted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   " deleted_by='" + employee_gid + "'" +
                   " where bloodgroup_gid='" + bloodgroup_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Blood Group Deleted Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }

        }

        public void DaBloodGroupInactiveLogview(string bloodgroup_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT bloodgroup_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tbloodgroupinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where bloodgroup_gid ='" + bloodgroup_gid + "' order by a.bloodgroupinactivelog_gid    desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            bloodgroup_gid = (dr_datarow["bloodgroup_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        //Base Location

        public void DaGetBaseLocation(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT a.baselocation_gid ,a.api_code,a.baselocation_name,a.lms_code, a.bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tbaselocation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid where a.delete_flag='N' order by a.baselocation_gid  desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            baselocation_gid = (dr_datarow["baselocation_gid"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                            baselocation_name = (dr_datarow["baselocation_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    objmaster.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaGetBaseLocationlist(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT a.baselocation_gid ,a.baselocation_name " +
                        " FROM sys_mst_tbaselocation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid where a.delete_flag='N' order by a.baselocation_gid  desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getlocation_list = new List<location_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getlocation_list.Add(new location_list
                        {
                            baselocation_gid = (dr_datarow["baselocation_gid"].ToString()),
                            baselocation_name = (dr_datarow["baselocation_name"].ToString()),

                        });
                    }
                    objmaster.location_list = getlocation_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaGetBaseLocationlistActive(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT a.baselocation_gid ,a.baselocation_name " +
                        " FROM sys_mst_tbaselocation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid where a.delete_flag='N' and status='Y' order by a.baselocation_gid  desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getlocation_list = new List<location_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getlocation_list.Add(new location_list
                        {
                            baselocation_gid = (dr_datarow["baselocation_gid"].ToString()),
                            baselocation_name = (dr_datarow["baselocation_name"].ToString()),

                        });
                    }
                    objmaster.location_list = getlocation_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaCreateBaseLocation(master values, string employee_gid)
        {
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
            msSQL = "select baselocation_name from sys_mst_tbaselocation where baselocation_name = '" + values.baselocation_name.Replace("'", "\\'") + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Base Location Already Exist";
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("SBLT");
                msGetAPICode = objcmnfunctions.GetApiMasterGID("BSLN");
                msSQL = " insert into sys_mst_tbaselocation(" +
                        " baselocation_gid ," +
                        " api_code," +
                        " lms_code," +
                        " bureau_code," +
                        " baselocation_name ," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                       "'" + msGetAPICode + "'," +
                        "'" + lslms_code + "'," +
                        "'" + lsbureau_code + "'," +

                      "'" + values.baselocation_name.Replace("'", "") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Base Location Added successfully";
                }
                else
                {
                    values.message = "Error Occured while Adding";
                    values.status = false;
                }
            }
        }
        public void DaEditBaseLocation(string baselocation_gid, master values)
        {
            try
            {
                msSQL = " SELECT baselocation_gid,baselocation_name,lms_code, bureau_code, status as Status FROM sys_mst_tbaselocation " +
                        " where baselocation_gid='" + baselocation_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.baselocation_gid = objODBCDatareader["baselocation_gid"].ToString();
                    values.baselocation_name = objODBCDatareader["baselocation_name"].ToString();
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    //values.status_baselocation = objODBCDatareader["status_baselocation"].ToString();
                    values.Status = objODBCDatareader["status"].ToString();

                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdateBaseLocation(string employee_gid, master values)
        {
            msSQL = "select baselocation_gid from sys_mst_tbaselocation where delete_flag='N' and baselocation_name = '" + values.baselocation_name.Replace("'", "\\'") + "'";
            lsdocumentgid = objdbconn.GetExecuteScalar(msSQL);
            if (lsdocumentgid != "")
            {
                if (lsdocumentgid != values.baselocation_gid)
                {
                    values.message = "Base Location Already Exist";
                    values.status = false;
                    return;
                }
            }
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

            msSQL = " update sys_mst_tbaselocation set " +
            " baselocation_name='" + values.baselocation_name.Replace("'", "") + "'," +
            " lms_code='" + lslms_code + "'," +
            " bureau_code='" + lsbureau_code + "'," +
            " updated_by='" + employee_gid + "'," +
            " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
            " where baselocation_gid='" + values.baselocation_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                //msGetGid = objcmnfunctions.GetMasterGID("MELG");
                msGetGid = objcmnfunctions.GetMasterGID("SBLL");
                msSQL = " insert into sys_mst_tbaselocationlog(" +
                          " baselocation_loggid," +
                          " baselocation_gid," +
                          " baselocation_name , " +
                          " created_by, " +
                          " created_date) " +
                          " values(" +
                          "'" + msGetGid + "'," +
                          "'" + values.baselocation_gid + "'," +
                          "'" + lsbase_value + "'," +
                          "'" + employee_gid + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Base Location Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }


        public void DaInactiveBaseLocation(master values, string employee_gid)
        {
            msSQL = " update sys_mst_tbaselocation set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where baselocation_gid='" + values.baselocation_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("SBLI");

                msSQL = " insert into sys_mst_tbaselocationinactivelog (" +
                      " baselocationinactivelog_gid   , " +
                      " baselocation_gid," +
                      " baselocation_name ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.baselocation_gid + "'," +
                      " '" + values.baselocation_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Base Location Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Base Location  Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaDeleteBaseLocation(string baselocation_gid, string employee_gid, master values)
        {
            msSQL = " update sys_mst_tbaselocation  set delete_flag='Y'," +
                    " deleted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   " deleted_by='" + employee_gid + "'" +
                   " where baselocation_gid='" + baselocation_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Base Location Deleted Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }

        }

        public void DaBaseLocationInactiveLogview(string baselocation_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT a.baselocation_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tbaselocationinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.baselocation_gid ='" + baselocation_gid + "' order by a.baselocationinactivelog_gid   desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            baselocation_gid = (dr_datarow["baselocation_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        //physical status

        public void DaGetPhysicalStatus(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT a.physicalstatus_gid,a.api_code,a.physicalstatus_name,a.lms_code, a.bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tphysicalstatus a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid where a.delete_flag='N' order by a.physicalstatus_gid  desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            physicalstatus_gid = (dr_datarow["physicalstatus_gid"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                            physicalstatus_name = (dr_datarow["physicalstatus_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    objmaster.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaCreatePhysicalStatus(master values, string employee_gid)
        {
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
            msSQL = "select physicalstatus_name from sys_mst_tphysicalstatus where physicalstatus_name = '" + values.physicalstatus_name.Replace("'", "\\'") + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Physical Status Already Exist";
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("SPST");
                msGetAPICode = objcmnfunctions.GetApiMasterGID("PHCS");
                msSQL = " insert into sys_mst_tphysicalstatus(" +
                        " physicalstatus_gid," +
                        " api_code," +
                        " lms_code," +
                        " bureau_code," +
                        " physicalstatus_name," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + msGetAPICode + "'," +
                        "'" + lslms_code + "'," +
                        "'" + lsbureau_code + "'," +
                       "'" + values.physicalstatus_name.Replace("'", "") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Physical Status Added successfully";
                }
                else
                {
                    values.message = "Error Occured while Adding";
                    values.status = false;
                }
            }
        }

        public void DaEditPhysicalStatus(string physicalstatus_gid, master values)
        {
            try
            {
                msSQL = " SELECT physicalstatus_gid,physicalstatus_name,lms_code, bureau_code, status as Status FROM sys_mst_tphysicalstatus " +
                        " where physicalstatus_gid='" + physicalstatus_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.physicalstatus_gid = objODBCDatareader["physicalstatus_gid"].ToString();
                    values.physicalstatus_name = objODBCDatareader["physicalstatus_name"].ToString();
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    //values.status_physicalstatus = objODBCDatareader["status_physicalstatus"].ToString();
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

        public void DaUpdatePhysicalStatus(string employee_gid, master values)
        {
            msSQL = "select physicalstatus_gid from sys_mst_tphysicalstatus where physicalstatus_name = '" + values.physicalstatus_name.Replace("'", "\\'") + "'";
            lsdocumentgid = objdbconn.GetExecuteScalar(msSQL);
            if (lsdocumentgid != "")
            {
                if (lsdocumentgid != values.physicalstatus_gid)
                {
                    values.message = "Physical Status Already Exist";
                    values.status = false;
                    return;
                }
            }

            msSQL = "select updated_by, updated_date,physicalstatus_name from sys_mst_tphysicalstatus where physicalstatus_gid ='" + values.physicalstatus_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("SPSL");
                    msSQL = " insert into sys_mst_tphysicalstatuslog(" +
                              " physicalstatus_loggid," +
                              " physicalstatus_gid," +
                              " physicalstatus_name," +
                              " created_by," +
                              " created_date)" +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.physicalstatus_gid + "'," +
                              "'" + objODBCDatareader["physicalstatus_name"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            msSQL = " update sys_mst_tphysicalstatus set ";
            if (values.lms_code == "" || values.lms_code == null)
            {
                msSQL += " lms_code='',";
            }
            else
            {
                msSQL += " lms_code='" + values.lms_code.Replace("'", "") + "',";
            }
            if (values.bureau_code == "" || values.bureau_code == null)
            {
                msSQL += " bureau_code='',";
            }
            else
            {
                msSQL += " bureau_code='" + values.bureau_code.Replace("'", "") + "',";
            }

            msSQL += " physicalstatus_name='" + values.physicalstatus_name.Replace("'", "") + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where physicalstatus_gid='" + values.physicalstatus_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Physical Status updated successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating";
            }
        }

        public void DaInactivePhysicalStatus(master values, string employee_gid)
        {
            msSQL = " update sys_mst_tphysicalstatus set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where physicalstatus_gid='" + values.physicalstatus_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("SPSI");

                msSQL = " insert into sys_mst_tphysicalstatusinactivelog (" +
                      " physicalstatusinactivelog_gid   , " +
                      " physicalstatus_gid," +
                      " physicalstatus_name ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.physicalstatus_gid + "'," +
                      " '" + values.physicalstatus_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Physical Status Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Physical Status Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaDeletePhysicalStatus(string physicalstatus_gid, string employee_gid, master values)
        {
            msSQL = " update sys_mst_tphysicalstatus  set delete_flag='Y'," +
                    " deleted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   " deleted_by='" + employee_gid + "'" +
                   " where physicalstatus_gid='" + physicalstatus_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Physical Status Deleted Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }

        }

        public void DaPhysicalStatusInactiveLogview(string physicalstatus_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT physicalstatus_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tphysicalstatusinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where physicalstatus_gid ='" + physicalstatus_gid + "' order by a.physicalstatusinactivelog_gid    desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            physicalstatus_gid = (dr_datarow["physicalstatus_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        //Calendar Group

        public void DaGetCalendarGroup(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT a.calendargroup_gid ,a.calendargroup_name,a.lms_code, a.bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,api_code," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tcalendargroup a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid where a.delete_flag='N' order by a.calendargroup_gid  desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            calendargroup_gid = (dr_datarow["calendargroup_gid"].ToString()),
                            calendargroup_name = (dr_datarow["calendargroup_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                        });
                    }
                    objmaster.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaCreateCalendarGroup(master values, string employee_gid)
        {

            string lslms_code, lsbureau_code = "";

            if (values.lms_code == null || values.lms_code == "")
            {
                lslms_code = "";
            }
            else
            {
                lslms_code = values.lms_code.Replace("'", "\\'");
            }
            if (values.bureau_code == null || values.bureau_code == "")
            {
                lsbureau_code = "";
            }
            else
            {
                lsbureau_code = values.bureau_code.Replace("'", "\\'");
            }

            string bureau_code, calendargroup_name, lms_code;
            //bureau_code = calendargroup_name = lms_code = "";

            msSQL = " SELECT calendargroup_gid, lms_code,bureau_code,calendargroup_name FROM sys_mst_tcalendargroup ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            //var getSegment = new List<CalendarGroupComparison_List>();

            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    //getSegment.Add(new EncoreProductComparison_List
                    //{

                    lms_code = (dr_datarow["lms_code"].ToString());
                    bureau_code = (dr_datarow["bureau_code"].ToString());
                    calendargroup_name = (dr_datarow["calendargroup_name"].ToString());

                    if (lms_code == lslms_code && bureau_code == lsbureau_code && calendargroup_name == values.calendargroup_name)
                    {
                        values.message = "This Calendar Group Already Exists";
                        values.status = false;
                        return;
                    }
                }

                dt_datatable.Dispose();
            }

            msGetAPICode = objcmnfunctions.GetApiMasterGID("CGCA");
            msGetGid = objcmnfunctions.GetMasterGID("SCGT");
            msSQL = " insert into sys_mst_tcalendargroup(" +
                    " calendargroup_gid ," +
                    " api_code," +
                    " lms_code," +
                    " bureau_code," +
                    " calendargroup_name ," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + msGetAPICode + "'," +
                    "'" + lslms_code + "'," +
                    "'" + lsbureau_code + "'," +
                    "'" + values.calendargroup_name.Replace("'", "\\'") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Calendar Group Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }
        }

        public void DaEditCalendarGroup(string calendargroup_gid, master values)
        {
            try
            {
                msSQL = " SELECT calendargroup_gid,calendargroup_name,lms_code, bureau_code, status as status_calendargroup FROM sys_mst_tcalendargroup " +
                        " where calendargroup_gid='" + calendargroup_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.calendargroup_gid = objODBCDatareader["calendargroup_gid"].ToString();
                    values.calendargroup_name = objODBCDatareader["calendargroup_name"].ToString();
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    values.status_calendargroup = objODBCDatareader["status_calendargroup"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdateCalendarGroup(string employee_gid, master values)
        {
            string lslms_code, lsbureau_code = "";

            if (values.lms_code == null || values.lms_code == "")
            {
                lslms_code = "";
            }
            else
            {
                lslms_code = values.lms_code.Replace("'", "\\'");
            }
            if (values.bureau_code == null || values.bureau_code == "")
            {
                lsbureau_code = "";
            }
            else
            {
                lsbureau_code = values.bureau_code.Replace("'", "\\'");
            }

            string bureau_code, calendargroup_name, lms_code;
            //bureau_code = calendargroup_name = lms_code = "";

            msSQL = " SELECT calendargroup_gid, lms_code,bureau_code,calendargroup_name FROM sys_mst_tcalendargroup ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            //var getSegment = new List<CalendarGroupComparison_List>();

            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    //getSegment.Add(new EncoreProductComparison_List
                    //{

                    lms_code = (dr_datarow["lms_code"].ToString());
                    bureau_code = (dr_datarow["bureau_code"].ToString());
                    calendargroup_name = (dr_datarow["calendargroup_name"].ToString());

                    if (lms_code == lslms_code && bureau_code == lsbureau_code && calendargroup_name == values.calendargroup_name)
                    {
                        values.message = "This Calendar Group Already Exists";
                        values.status = false;
                        return;
                    }
                }

                dt_datatable.Dispose();
            }

            msSQL = "select updated_by, updated_date,calendargroup_name from sys_mst_tcalendargroup where calendargroup_gid ='" + values.calendargroup_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("SCGL");
                    msSQL = " insert into sys_mst_tcalendargrouplog(" +
                              " calendargroup_loggid   ," +
                              " calendargroup_gid," +
                              " calendargroup_name , " +
                              " created_by, " +
                              " created_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.calendargroup_gid + "'," +
                              "'" + values.calendargroup_name.Replace("'", "\\'") + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            msSQL = " update sys_mst_tcalendargroup set " +
                " calendargroup_name='" + values.calendargroup_name.Replace("'", "\\'") + "'," +
                 " lms_code='" + lslms_code + "'," +
                 " bureau_code='" + lsbureau_code + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where calendargroup_gid='" + values.calendargroup_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Calendar Group updated successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating";
            }
        }

        public void DaInactiveCalendarGroup(master values, string employee_gid)
        {
            msSQL = " update sys_mst_tcalendargroup set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "\\'") + "'" +
                    " where calendargroup_gid='" + values.calendargroup_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("SCGI");

                msSQL = " insert into sys_mst_tcalendargroupinactivelog (" +
                      " calendargroupinactivelog_gid   , " +
                      " calendargroup_gid," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.calendargroup_gid + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "\\'") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Calendar Group Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Calendar Group Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaDeleteCalendarGroup(string calendargroup_gid, string employee_gid, master values)
        {
            msSQL = " update sys_mst_tcalendargroup  set delete_flag='Y'," +
                    " deleted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   " deleted_by='" + employee_gid + "'" +
                   " where calendargroup_gid='" + calendargroup_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Calendar Group Deleted Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }

        }

        public void DaCalendarGroupInactiveLogview(string calendargroup_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT calendargroup_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tcalendargroupinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where calendargroup_gid ='" + calendargroup_gid + "' order by a.calendargroupinactivelog_gid    desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            calendargroup_gid = (dr_datarow["calendargroup_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        //Client Role

        public void DaGetSubFunction(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT a.subfunction_gid  ,a.api_code,a.subfunction_name ,a.lms_code, a.bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tsubfunction a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid where a.delete_flag='N' order by a.subfunction_gid   desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            subfunction_gid = (dr_datarow["subfunction_gid"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                            subfunction_name = (dr_datarow["subfunction_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    objmaster.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }

            catch (Exception ex)
            {
                objmaster.status = false;
            }
        }

        public void DaCreateSubFunction(master values, string employee_gid)
        {

            string lslms_code, lsbureau_code = "";

            if (values.lms_code == null || values.lms_code == "")
            {
                lslms_code = "";
            }
            else
            {
                lslms_code = values.lms_code.Replace("'", "\\'");
            }
            if (values.bureau_code == null || values.bureau_code == "")
            {
                lsbureau_code = "";
            }
            else
            {
                lsbureau_code = values.bureau_code.Replace("'", "\\'");
            }

            string bureau_code, subfunction_name, lms_code;

            msSQL = " SELECT lms_code,bureau_code,subfunction_name FROM sys_mst_tsubfunction ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            //var getSegment = new List<CalendarGroupComparison_List>();

            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    //getSegment.Add(new EncoreProductComparison_List
                    //{

                    lms_code = (dr_datarow["lms_code"].ToString());
                    bureau_code = (dr_datarow["bureau_code"].ToString());
                    subfunction_name = (dr_datarow["subfunction_name"].ToString());

                    if (lms_code == lslms_code && bureau_code == lsbureau_code && subfunction_name == values.subfunction_name)
                    {
                        values.message = "This Sub Function Already Exists";
                        values.status = false;
                        return;
                    }
                }

                dt_datatable.Dispose();
            }
            msGetGid = objcmnfunctions.GetMasterGID("SCRT");
            msGetAPICode = objcmnfunctions.GetApiMasterGID("SUBF");
            msSQL = " insert into sys_mst_tsubfunction(" +
                    " subfunction_gid  ," +
                     " api_code," +
                    " lms_code," +
                    " bureau_code," +
                    " subfunction_name  ," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + msGetAPICode + "'," +
                    "'" + lslms_code + "'," +
                    "'" + lsbureau_code + "'," +
                    "'" + values.subfunction_name.Replace("'", "\\'") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Subfunction Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }
        }

        public void DaEditSubFunction(string subfunction_gid, master values)
        {
            try
            {
                msSQL = " SELECT subfunction_gid ,subfunction_name ,lms_code, bureau_code, status as Status FROM sys_mst_tsubfunction " +
                        " where subfunction_gid ='" + subfunction_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.subfunction_gid = objODBCDatareader["subfunction_gid"].ToString();
                    values.subfunction_name = objODBCDatareader["subfunction_name"].ToString();
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

        public void DaUpdateSubFunction(string employee_gid, master values)
        {
            string lslms_code, lsbureau_code = "";

            if (values.lms_code == null || values.lms_code == "")
            {
                lslms_code = "";
            }
            else
            {
                lslms_code = values.lms_code.Replace("'", "\\'");
            }
            if (values.bureau_code == null || values.bureau_code == "")
            {
                lsbureau_code = "";
            }
            else
            {
                lsbureau_code = values.bureau_code.Replace("'", "\\'");
            }

            string bureau_code, subfunction_name, lms_code;

            msSQL = " SELECT lms_code,bureau_code,subfunction_name FROM sys_mst_tsubfunction ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            //var getSegment = new List<CalendarGroupComparison_List>();

            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    //getSegment.Add(new EncoreProductComparison_List
                    //{

                    lms_code = (dr_datarow["lms_code"].ToString());
                    bureau_code = (dr_datarow["bureau_code"].ToString());
                    subfunction_name = (dr_datarow["subfunction_name"].ToString());

                    if (lms_code == lslms_code && bureau_code == lsbureau_code && subfunction_name == values.subfunction_name)
                    {
                        values.message = "This Sub Function Already Exists";
                        values.status = false;
                        return;
                    }
                }

                dt_datatable.Dispose();
            }

            msSQL = "select updated_by, updated_date,subfunction_gid from sys_mst_tsubfunction where subfunction_gid  ='" + values.subfunction_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("SCRL");
                    msSQL = " insert into sys_mst_tsubfunctionlog(" +
                              " subfunction_loggid   ," +
                              " subfunction_gid ," +
                              " subfunction_name , " +
                              " created_by, " +
                              " created_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.subfunction_gid + "'," +
                              "'" + values.subfunction_name.Replace("'", "\\'") + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            msSQL = " update sys_mst_tsubfunction set " +
                    " subfunction_name ='" + values.subfunction_name.Replace("'", "\\'") + "'," +
                     " lms_code='" + lslms_code + "'," +
                     " bureau_code='" + lsbureau_code + "'," +
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where subfunction_gid ='" + values.subfunction_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "SubFunction updated successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating";
            }
        }

        public void DaInactiveSubFunction(master values, string employee_gid)
        {
            msSQL = " update sys_mst_tsubfunction set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "\\'") + "'" +
                    " where subfunction_gid ='" + values.subfunction_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("SCRI");

                msSQL = " insert into sys_mst_tsubfunctioninactivelog (" +
                      " subfunctioninactivelog_gid   , " +
                      " subfunction_gid ," +
                      " subfunction_name  ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.subfunction_gid + "'," +
                      " '" + values.subfunction_name.Replace("'", "\\'") + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "\\'") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Subfunction Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Subfunction Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaDeleteSubFunction(string subfunction_gid, string employee_gid, master values)
        {
            msSQL = " update sys_mst_tsubfunction  set delete_flag='Y'," +
                    " deleted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   " deleted_by='" + employee_gid + "'" +
                   " where subfunction_gid='" + subfunction_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "SubFunction Deleted Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }

        }

        public void DaSubFunctionInactiveLogview(string subfunction_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT a.subfunction_gid ,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tsubfunctioninactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.subfunction_gid  ='" + subfunction_gid + "' order by a.subfunctioninactivelog_gid    desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            subfunction_gid = (dr_datarow["subfunction_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        //Salutation

        public void DaGetSalutation(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT a.salutation_gid ,a.api_code,a.salutation_name,a.lms_code, a.bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tsalutation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid where a.delete_flag='N' order by a.salutation_gid  desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            salutation_gid = (dr_datarow["salutation_gid"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                            salutation_name = (dr_datarow["salutation_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    objmaster.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaCreateSalutation(master values, string employee_gid)
        {
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
            msSQL = "select salutation_name from sys_mst_tsalutation where salutation_name = '" + values.salutation_name.Replace("'", "\\'") + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Salutation Name Already Exist";
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("SSMT");
                msGetAPICode = objcmnfunctions.GetApiMasterGID("SALN");
                msSQL = " insert into sys_mst_tsalutation(" +
                        " salutation_gid," +
                          " api_code," +
                        " lms_code," +
                        " bureau_code," +
                        " salutation_name," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                     "'" + msGetAPICode + "'," +
                      "'" + lslms_code + "'," +
                     "'" + lsbureau_code + "'," +

                 "'" + values.salutation_name.Replace("'", "") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Salutation Added successfully";
                }
                else
                {
                    values.message = "Error Occured while Adding";
                    values.status = false;
                }
            }
        }
        public void DaEditSalutation(string salutation_gid, master values)
        {
            try
            {
                msSQL = " SELECT salutation_gid,salutation_name ,lms_code, bureau_code, status as Status FROM sys_mst_tsalutation " +
                        " where salutation_gid='" + salutation_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.salutation_gid = objODBCDatareader["salutation_gid"].ToString();
                    values.salutation_name = objODBCDatareader["salutation_name"].ToString();
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    //values.status_salutation = objODBCDatareader["status_salutation"].ToString();
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


        public void DaUpdateSalutation(string employee_gid, master values)
        {
            msSQL = "select salutation_gid from sys_mst_tsalutation where delete_flag='N' and salutation_name = '" + values.salutation_name.Replace("'", "\\'") + "'";
            lsdocumentgid = objdbconn.GetExecuteScalar(msSQL);
            if (lsdocumentgid != "")
            {
                if (lsdocumentgid != values.salutation_gid)
                {
                    values.message = "Salutation Name Already Exist";
                    values.status = false;
                    return;
                }
            }
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

            msSQL = " update sys_mst_tsalutation set " +
            " salutation_name='" + values.salutation_name.Replace("'", "") + "'," +
            " lms_code='" + lslms_code + "'," +
            " bureau_code='" + lsbureau_code + "'," +
            " updated_by='" + employee_gid + "'," +
            " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
            " where salutation_gid='" + values.salutation_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " select salutation_name from sys_mst_tsalutation where salutation_gid='" + values.salutation_gid + "'";
            lssalutation_value = objdbconn.GetExecuteScalar(msSQL);
            if (mnResult != 0)
            {
                //msGetGid = objcmnfunctions.GetMasterGID("MELG");
                msGetGid = objcmnfunctions.GetMasterGID("SSML");
                msSQL = " insert into sys_mst_tsalutationlog(" +
                          " salutation_loggid    ," +
                          " salutation_gid," +
                          " salutation_name, " +
                          " created_by, " +
                          " created_date) " +
                          " values(" +
                          "'" + msGetGid + "'," +
                          "'" + values.salutation_gid + "'," +
                          "'" + lssalutation_value + "'," +
                          "'" + employee_gid + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Salutation Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }


        public void DaInactiveSalutation(master values, string employee_gid)
        {
            msSQL = " update sys_mst_tsalutation set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where salutation_gid ='" + values.salutation_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("SSMI");

                msSQL = " insert into sys_mst_tsalutationinactivelog (" +
                      " salutationinactivelog_gid, " +
                      " salutation_gid," +
                      " salutation_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.salutation_gid + "'," +
                      " '" + values.salutation_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Salutation Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Salutation  Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaDeleteSalutation(string salutation_gid, string employee_gid, master values)
        {
            msSQL = " update sys_mst_tsalutation  set delete_flag='Y'," +
                    " deleted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   " deleted_by='" + employee_gid + "'" +
                   " where salutation_gid='" + salutation_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Salutation Deleted Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }

        }

        public void DaSalutationInactiveLogview(string salutation_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT salutation_gid ,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tsalutationinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where salutation_gid  ='" + salutation_gid + "' order by a.salutationinactivelog_gid   desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            salutation_gid = (dr_datarow["salutation_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        //PROJECT

        // Add Project
        public void DaGetProject(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT a.project_gid ,a.project,a.lms_code, a.bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,api_code," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tproject a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.project_gid  desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            project_gid = (dr_datarow["project_gid"].ToString()),
                            project_name = (dr_datarow["project"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                        });
                    }
                    objmaster.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaCreateProject(master values, string employee_gid)
        {
            if (values.lms_code == "" || values.lms_code == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.lms_code.Replace("'", "\\'") + "',";
            }
            if (values.bureau_code == "" || values.bureau_code == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.bureau_code.Replace("'", "\\'") + "',";
            }
            msSQL = "select project from sys_mst_tproject where project = '" + values.project_name.Replace("'", "\\'") + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Project Name Already Exist";
            }
            else
            {
                msGetAPICode = objcmnfunctions.GetApiMasterGID("PRCA");
                msGetGid = objcmnfunctions.GetMasterGID("SMPR");
                msSQL = " insert into sys_mst_tproject(" +
                        " project_gid ," +
                        " api_code ," +
                        " lms_code," +
                        " bureau_code," +
                        " project," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + msGetAPICode + "'," +
                        "'" + lslms_code + "'," +
                        "'" + lsbureau_code + "'," +
                         "'" + values.project_name.Replace("'", "\\'") + "'," +
                        "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                //msSQL += "'" + values.project_name.Replace("'", "") + "'," +
                //        "'" + employee_gid + "'," +
                //        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Project Added successfully";
                }
                else
                {
                    values.message = "Error Occured while Adding";
                    values.status = false;
                }
            }
        }
        //  Edit Project 

        public void DaEditProject(string project_gid, master values)
        {
            try
            {
                msSQL = " SELECT project_gid,project,lms_code, bureau_code, status as status FROM sys_mst_tproject " +
                        " where project_gid='" + project_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.project_gid = objODBCDatareader["project_gid"].ToString();
                    values.project_name = objODBCDatareader["project"].ToString();
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    values.status_project = objODBCDatareader["status"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }


        public void DaUpdateProject(string employee_gid, master values)
        {
            msSQL = "select project_gid from sys_mst_tproject where project = '" + values.project_name.Replace("'", "\\'") + "'";
            lsdocumentgid = objdbconn.GetExecuteScalar(msSQL);
            if (lsdocumentgid != "")
            {
                if (lsdocumentgid != values.project_gid)
                {
                    values.message = "project name Already Exist";
                    values.status = false;
                    return;
                }
            }
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

            msSQL = " update sys_mst_tproject set " +
            " project='" + values.project_name.Replace("'", "") + "'," +
            " lms_code='" + lslms_code + "'," +
            " bureau_code='" + lsbureau_code + "'," +
            " updated_by='" + employee_gid + "'," +
            " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
            " where project_gid='" + values.project_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " select project from sys_mst_tproject where project_gid='" + values.project_gid + "'";
            lsproject_value = objdbconn.GetExecuteScalar(msSQL);
            if (mnResult != 0)
            {
                //msGetGid = objcmnfunctions.GetMasterGID("MELG");
                msGetGid = objcmnfunctions.GetMasterGID("PRLG");
                msSQL = " insert into sys_mst_tprojectlogid(" +
                          " projectlog_gid," +
                          " project_gid," +
                          " project , " +
                          " created_by," +
                          " created_date)" +
                          " values(" +
                          "'" + msGetGid + "'," +
                          "'" + values.project_gid + "'," +
                          "'" + lsproject_value + "'," +
                          "'" + employee_gid + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";


                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "project Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }


        //Project Status

        public void DaInactiveProject(master values, string employee_gid)
        {
            msSQL = " update sys_mst_tproject set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where project_gid='" + values.project_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("SMPI");

                msSQL = " insert into sys_mst_tprojectinactivelog (" +
                      " projectinactivelog_gid   , " +
                      " project_gid," +
                      " project ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date)" +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.project_gid + "'," +
                      " '" + values.project_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Project Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Project  Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaProjectInactiveLogview(string project_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT project_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status, a.remarks" +
                        " FROM sys_mst_tprojectinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where project_gid ='" + project_gid + "' order by a.projectinactivelog_gid   desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            project_gid = (dr_datarow["project_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        // Delete Project 

        public void DaDeleteProject(string project_gid, string employee_gid, result values)
        {
            msSQL = " select project  from sys_mst_tproject where project_gid='" + project_gid + "'";
            lsmaster_value = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " delete from sys_mst_tproject where project_gid ='" + project_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Project Deleted Successfully..!";
                msGetGid = objcmnfunctions.GetMasterGID("MSTD");
                msSQL = " insert into ocs_mst_tmasterdeletelog(" +
                         "master_gid, " +
                         "master_name, " +
                         "master_value, " +
                         "deleted_by, " +
                         "deleted_date) " +
                         " values(" +
                         "'" + msGetGid + "'," +
                         "'project '," +
                         "'" + lsmaster_value + "'," +
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

        public void DaPostClusterAdd(cluster values, string employee_gid)
        {
            msSQL = "select cluster_name from sys_mst_tclustermapping where cluster_name = '" + values.cluster_name.Replace("'", "\\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Cluster Name Already Exist";
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("CLST");
                msGetAPICode = objcmnfunctions.GetApiMasterGID("CLUM");
                msSQL = " insert into sys_mst_tclustermapping(" +
                        " cluster_gid ," +
                        " api_code," +
                        " cluster_name," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                         "'" + msGetAPICode + "'," +
                        "'" + values.cluster_name.Replace("'", "") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                for (var i = 0; i < values.locationlist.Count; i++)
                {
                    msGet_LocationGid = objcmnfunctions.GetMasterGID("CL2L");

                    msSQL = "Insert into sys_mst_tcluster2baselocation( " +
                           " cluster2baselocation_gid, " +
                           " cluster_gid," +
                           " baselocation_gid," +
                           " baselocation_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGet_LocationGid + "'," +
                           "'" + msGetGid + "'," +
                           "'" + values.locationlist[i].baselocation_gid + "'," +
                           "'" + values.locationlist[i].baselocation_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Cluster Added successfully";
                }
                else
                {
                    values.message = "Error Occured while Adding";
                    values.status = false;
                }
            }
        }

        public void DaGetClusterSummary(cluster objmaster)
        {
            try
            {
                msSQL = " SELECT a.cluster_gid ,a.api_code,a.cluster_name, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tclustermapping a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcluster_list = new List<cluster_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcluster_list.Add(new cluster_list
                        {
                            cluster_gid = (dr_datarow["cluster_gid"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                            cluster_name = (dr_datarow["cluster_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            status = (dr_datarow["status"].ToString())
                        });
                    }
                    objmaster.cluster_list = getcluster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaGetClusterEdit(string cluster_gid, cluster objmaster)
        {
            msSQL = " select cluster_gid,cluster_name, status as status from sys_mst_tclustermapping " +
                    " where cluster_gid='" + cluster_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objmaster.cluster_gid = objODBCDatareader["cluster_gid"].ToString();
                objmaster.cluster_name = objODBCDatareader["cluster_name"].ToString();
                objmaster.cluster_status = objODBCDatareader["status"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select cluster2baselocation_gid,cluster_gid,baselocation_gid, baselocation_name from sys_mst_tcluster2baselocation " +
                  " where cluster_gid='" + cluster_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlocationList = new List<locationlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getlocationList.Add(new locationlist
                    {
                        baselocation_gid = dt["baselocation_gid"].ToString(),
                        baselocation_name = dt["baselocation_name"].ToString(),
                        cluster2baselocation_gid = dt["cluster2baselocation_gid"].ToString(),
                    });
                    objmaster.locationlist = getlocationList;
                }
            }
            dt_datatable.Dispose();

        }

        public bool DaPostClusterUpdate(string employee_gid, cluster values)
        {
            msSQL = "select cluster_gid from sys_mst_tclustermapping where cluster_name='" + values.cluster_name.Replace("'", "\\'") + "'";
            clusterGID = objdbconn.GetExecuteScalar(msSQL);
            if (clusterGID != "")
            {
                if (clusterGID != values.cluster_gid)
                {
                    values.status = false;
                    values.message = "Cluster Name Already Exist";
                    return false;
                }
            }

            msSQL = "select updated_by, updated_date,cluster_name from sys_mst_tclustermapping where cluster_gid ='" + values.cluster_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("CLSL");
                    msSQL = " insert into sys_mst_tclustermappinglog(" +
                              " clusterlog_gid," +
                              " cluster_gid," +
                              " cluster_name , " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.cluster_gid + "'," +
                              "'" + objODBCDatareader["cluster_name"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();

            msSQL = "update sys_mst_tclustermapping set cluster_name='" + values.cluster_name.Replace("'", "") + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where cluster_gid='" + values.cluster_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " select cluster2baselocation_gid,cluster_gid,baselocation_gid,baselocation_name,created_by,created_date " +
                    " from sys_mst_tcluster2baselocation where cluster_gid ='" + values.cluster_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            foreach (DataRow dt in dt_datatable.Rows)
            {
                msSQL = "Insert into sys_mst_tcluster2baselocationlog( " +
                          " cluster2baselocation_gid, " +
                          " cluster_gid," +
                          " baselocation_gid," +
                          " baselocation_name," +
                          " created_by," +
                          " created_date," +
                          " deleted_by," +
                          " deleted_date)" +
                          " values(" +
                          "'" + dt["cluster2baselocation_gid"].ToString() + "'," +
                          "'" + dt["cluster_gid"].ToString() + "'," +
                          "'" + dt["baselocation_gid"].ToString() + "'," +
                          "'" + dt["baselocation_name"].ToString() + "'," +
                          "'" + dt["created_by"].ToString() + "'," +
                          "'" + Convert.ToDateTime(dt["created_date"]).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                          "'" + employee_gid + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            msSQL = " delete from sys_mst_tcluster2baselocation where cluster_gid ='" + values.cluster_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                for (var i = 0; i < values.locationlist.Count; i++)
                {
                    msGet_LocationGid = objcmnfunctions.GetMasterGID("CL2L");

                    msSQL = "Insert into sys_mst_tcluster2baselocation( " +
                           " cluster2baselocation_gid, " +
                           " cluster_gid," +
                           " baselocation_gid," +
                           " baselocation_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGet_LocationGid + "'," +
                           "'" + values.cluster_gid + "'," +
                           "'" + values.locationlist[i].baselocation_gid + "'," +
                           "'" + values.locationlist[i].baselocation_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Cluster updated successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Cluster";
                return false;
            }
        }
        public void DaGetCluster2BaseLocation(string cluster_gid, cluster objmaster)
        {
            msSQL = " select cluster2baselocation_gid,cluster_gid,baselocation_gid, baselocation_name from sys_mst_tcluster2baselocation " +
                  " where cluster_gid='" + cluster_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlocationList = new List<locationlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getlocationList.Add(new locationlist
                    {
                        baselocation_gid = dt["baselocation_gid"].ToString(),
                        baselocation_name = dt["baselocation_name"].ToString(),
                        cluster2baselocation_gid = dt["cluster2baselocation_gid"].ToString(),
                    });
                    objmaster.locationlist = getlocationList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetUnTaggedLocations(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT baselocation_gid ,baselocation_name FROM sys_mst_tbaselocation" +
                        " where delete_flag='N' and baselocation_gid not in (select baselocation_gid from sys_mst_tcluster2baselocation) " +
                        " order by baselocation_gid  desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            baselocation_gid = (dr_datarow["baselocation_gid"].ToString()),
                            baselocation_name = (dr_datarow["baselocation_name"].ToString()),
                        });
                    }
                    objmaster.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaGetUnTaggedLocationsEdit(MdlSystemMaster objmaster, string cluster_gid)
        {
            try
            {
                msSQL = " SELECT baselocation_gid ,baselocation_name FROM sys_mst_tbaselocation" +
                        " where delete_flag='N' and baselocation_gid not in (select baselocation_gid from sys_mst_tcluster2baselocation where cluster_gid<>'" + cluster_gid + "') " +
                        " order by baselocation_gid  desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            baselocation_gid = (dr_datarow["baselocation_gid"].ToString()),
                            baselocation_name = (dr_datarow["baselocation_name"].ToString()),
                        });
                    }
                    objmaster.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public bool DaInactiveCluster(cluster values, string employee_gid)
        {
            if (values.rbo_status.ToString() == "N")
            {
                msSQL = " select (select count(application_gid)  from ocs_mst_tapplication where approval_status = 'Submitted to Approval' and baselocation_gid in (select baselocation_gid from sys_mst_tcluster2baselocation where cluster_gid = '" + values.cluster_gid + "')) as total_ocs_pendingcount , " +
                        " (select count(application_gid)  from agr_mst_tapplication where approval_status = 'Submitted to Approval' and  baselocation_gid in (select baselocation_gid from sys_mst_tcluster2baselocation where cluster_gid = '" + values.cluster_gid + "')) as total_agrbyr_pendingcount, " +
                        " (select count(application_gid)  from agr_mst_tsuprapplication where approval_status = 'Submitted to Approval' and  baselocation_gid in (select baselocation_gid from sys_mst_tcluster2baselocation where cluster_gid = '" + values.cluster_gid + "')) as total_agrsupr_pendingcount ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.ocs_pendingcount = objODBCDatareader["total_ocs_pendingcount"].ToString();
                    values.agrbyr_pendingcount = objODBCDatareader["total_agrbyr_pendingcount"].ToString();
                    values.agrsupr_pendingcount = objODBCDatareader["total_agrsupr_pendingcount"].ToString();

                }
                objODBCDatareader.Close();


                if (values.ocs_pendingcount != "0" || values.agrbyr_pendingcount != "0" || values.agrsupr_pendingcount != "0")
                {
                    values.message = "N";
                    values.status = false;
                    return false;
                }

                //msSQL = " select count(application_gid) as total_pendingcount from ocs_mst_tapplication where approval_status = 'Submitted to Approval' and " +
                //        " baselocation_gid in (select baselocation_gid from sys_mst_tcluster2baselocation where cluster_gid = '" + values.cluster_gid + "')";
                //values.pending_count = objdbconn.GetExecuteScalar(msSQL);
                //if (values.pending_count != "0")
                //{
                //    values.message = "N";
                //    values.status = false;
                //    return false;
                //}

            }



            msSQL = " update sys_mst_tclustermapping set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where cluster_gid='" + values.cluster_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                if (values.rbo_status.ToString() == "N")
                {
                    msSQL = " select cluster2baselocation_gid,cluster_gid,baselocation_gid,baselocation_name,created_by,created_date " +
                            " from sys_mst_tcluster2baselocation where cluster_gid ='" + values.cluster_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msSQL = "Insert into sys_mst_tcluster2baselocationlog( " +
                                  " cluster2baselocation_gid, " +
                                  " cluster_gid," +
                                  " baselocation_gid," +
                                  " baselocation_name," +
                                  " created_by," +
                                  " created_date," +
                                  " deleted_by," +
                                  " deleted_date)" +
                                  " values(" +
                                  "'" + dt["cluster2baselocation_gid"].ToString() + "'," +
                                  "'" + dt["cluster_gid"].ToString() + "'," +
                                  "'" + dt["baselocation_gid"].ToString() + "'," +
                                  "'" + dt["baselocation_name"].ToString() + "'," +
                                  "'" + dt["created_by"].ToString() + "'," +
                                  "'" + Convert.ToDateTime(dt["created_date"]).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                  "'" + employee_gid + "'," +
                                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                    msSQL = " delete from sys_mst_tcluster2baselocation where cluster_gid ='" + values.cluster_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msGetGid = objcmnfunctions.GetMasterGID("CLSI");

                msSQL = " insert into sys_mst_tclusterinactivelog (" +
                      " clusterinactivelog_gid , " +
                      " cluster_gid," +
                      " cluster_name ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.cluster_gid + "'," +
                      " '" + values.cluster_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Cluster Inactivated Successfully";
                }
                else
                {

                    values.status = true;
                    values.message = "Cluster  Activated Successfully";
                }
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
                return false;
            }
        }

        public void DaClusterInactiveLogview(string cluster_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT cluster_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tclusterinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where cluster_gid ='" + cluster_gid + "' order by a.clusterinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            cluster_gid = (dr_datarow["cluster_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaPostRegionAdd(region values, string employee_gid)
        {
            msSQL = "select region_name from sys_mst_tregionmapping where region_name = '" + values.region_name.Replace("'", "\\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Region Name Already Exist";
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("RGNM");
                msGetAPICode = objcmnfunctions.GetApiMasterGID("REGM");
                msSQL = " insert into sys_mst_tregionmapping(" +
                        " region_gid ," +
                        " api_code," +
                        " region_name," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                         "'" + msGetAPICode + "'," +
                        "'" + values.region_name.Replace("'", "") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                for (var i = 0; i < values.cluster_list.Count; i++)
                {
                    msGet_clusterGid = objcmnfunctions.GetMasterGID("R2CL");

                    msSQL = "Insert into sys_mst_tregion2cluster( " +
                           " region2cluster_gid, " +
                           " region_gid," +
                           " cluster_gid," +
                           " cluster_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGet_clusterGid + "'," +
                           "'" + msGetGid + "'," +
                           "'" + values.cluster_list[i].cluster_gid + "'," +
                           "'" + values.cluster_list[i].cluster_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Region Added successfully";
                }
                else
                {
                    values.message = "Error Occured while Adding";
                    values.status = false;
                }
            }
        }

        public void DaGetRegionSummary(region objmaster)
        {
            try
            {
                msSQL = " SELECT a.region_gid ,a.api_code,a.region_name, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tregionmapping a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getregion_list = new List<region_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getregion_list.Add(new region_list
                        {
                            region_gid = (dr_datarow["region_gid"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                            region_name = (dr_datarow["region_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            status = (dr_datarow["status"].ToString())
                        });
                    }
                    objmaster.region_list = getregion_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaGetRegionEdit(string region_gid, region objmaster)
        {
            msSQL = " select region_gid,region_name, status as status from sys_mst_tregionmapping " +
                    " where region_gid='" + region_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objmaster.region_gid = objODBCDatareader["region_gid"].ToString();
                objmaster.region_name = objODBCDatareader["region_name"].ToString();
                objmaster.region_status = objODBCDatareader["status"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select region2cluster_gid,region_gid,cluster_gid, cluster_name from sys_mst_tregion2cluster " +
                  " where region_gid='" + region_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getclusterList = new List<cluster_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getclusterList.Add(new cluster_list
                    {
                        cluster_gid = dt["cluster_gid"].ToString(),
                        cluster_name = dt["cluster_name"].ToString(),
                        region2cluster_gid = dt["region2cluster_gid"].ToString(),
                    });
                    objmaster.cluster_list = getclusterList;
                }
            }
            dt_datatable.Dispose();

        }

        public bool DaPostRegionUpdate(string employee_gid, region values)
        {
            msSQL = "select region_gid from sys_mst_tregionmapping where region_name='" + values.region_name.Replace("'", "\\'") + "'";
            regionGID = objdbconn.GetExecuteScalar(msSQL);
            if (regionGID != "")
            {
                if (regionGID != values.region_gid)
                {
                    values.status = false;
                    values.message = "Region Name Already Exist";
                    return false;
                }
            }

            msSQL = "select updated_by, updated_date,region_name from sys_mst_tregionmapping where region_gid ='" + values.region_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("RGNL");
                    msSQL = " insert into sys_mst_tregionmappinglog(" +
                              " regionlog_gid," +
                              " region_gid," +
                              " region_name , " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.region_gid + "'," +
                              "'" + objODBCDatareader["region_name"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();

            msSQL = "update sys_mst_tregionmapping set region_name='" + values.region_name.Replace("'", "") + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where region_gid='" + values.region_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from sys_mst_tregion2cluster where region_gid ='" + values.region_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                for (var i = 0; i < values.cluster_list.Count; i++)
                {
                    msGet_regionGid = objcmnfunctions.GetMasterGID("R2CL");

                    msSQL = "Insert into sys_mst_tregion2cluster( " +
                           " region2cluster_gid, " +
                           " region_gid," +
                           " cluster_gid," +
                           " cluster_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGet_regionGid + "'," +
                           "'" + values.region_gid + "'," +
                           "'" + values.cluster_list[i].cluster_gid + "'," +
                           "'" + values.cluster_list[i].cluster_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Region updated successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Region";
                return false;
            }
        }
        public void DaGetRegion2Cluster(string region_gid, cluster objmaster)
        {
            //msSQL = " select region2cluster_gid,region_gid,cluster_gid, cluster_name from sys_mst_tregion2cluster " +
            //      " where region_gid='" + region_gid + "'";
            msSQL = " select a.region_gid,a.region_name,b.region2cluster_gid,b.cluster_gid,b.cluster_name,group_concat(c.baselocation_name) as baselocation_name " +
                    " from sys_mst_tregionmapping a " +
                    " left join sys_mst_tregion2cluster b on a.region_gid = b.region_gid " +
                    " left join sys_mst_tcluster2baselocation c on c.cluster_gid = b.cluster_gid " +
                    " where a.region_gid = '" + region_gid + "' " +
                    " group by b.cluster_gid ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getclusterList = new List<cluster_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getclusterList.Add(new cluster_list
                    {
                        cluster_gid = dt["cluster_gid"].ToString(),
                        cluster_name = dt["cluster_name"].ToString(),
                        baselocation_name = dt["baselocation_name"].ToString(),
                        region2cluster_gid = dt["region2cluster_gid"].ToString(),
                    });
                    objmaster.cluster_list = getclusterList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetUnTaggedClusters(region objmaster)
        {
            try
            {
                msSQL = " SELECT cluster_gid ,cluster_name FROM sys_mst_tclustermapping" +
                        " where cluster_gid not in (select cluster_gid from sys_mst_tregion2cluster) " +
                        " order by cluster_name asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcluster_list = new List<cluster_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcluster_list.Add(new cluster_list
                        {
                            cluster_gid = (dr_datarow["cluster_gid"].ToString()),
                            cluster_name = (dr_datarow["cluster_name"].ToString()),
                        });
                    }
                    objmaster.cluster_list = getcluster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaGetUnTaggedClustersEdit(region objmaster, string region_gid)
        {
            try
            {
                msSQL = " SELECT cluster_gid ,cluster_name FROM sys_mst_tclustermapping" +
                        " where cluster_gid not in (select cluster_gid from sys_mst_tregion2cluster where region_gid<>'" + region_gid + "') " +
                        " order by cluster_name asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcluster_list = new List<cluster_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcluster_list.Add(new cluster_list
                        {
                            cluster_gid = (dr_datarow["cluster_gid"].ToString()),
                            cluster_name = (dr_datarow["cluster_name"].ToString()),
                        });
                    }
                    objmaster.cluster_list = getcluster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public bool DaPostRegionInactive(region values, string employee_gid)
        {

            if (values.rbo_status.ToString() == "N")
            {
                msSQL = " select (select count(application_gid) as total_ocs_pendingcount from ocs_mst_tapplication where approval_status = 'Submitted to Approval' and cluster_gid in (select cluster_gid from sys_mst_tregion2cluster where region_gid = '" + values.region_gid + "')) as total_ocs_pendingcount, " +
                        " (select count(application_gid)  from agr_mst_tapplication where approval_status = 'Submitted to Approval' and  cluster_gid in (select cluster_gid from sys_mst_tregion2cluster where region_gid = '" + values.region_gid + "')) as total_agrbyr_pendingcount, " +
                        " (select count(application_gid) from agr_mst_tsuprapplication where approval_status = 'Submitted to Approval' and  cluster_gid in (select cluster_gid from sys_mst_tregion2cluster where region_gid = '" + values.region_gid + "'))  as total_agrsupr_pendingcount ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.ocs_pendingcount = objODBCDatareader["total_ocs_pendingcount"].ToString();
                    values.agrbyr_pendingcount = objODBCDatareader["total_agrbyr_pendingcount"].ToString();
                    values.agrsupr_pendingcount = objODBCDatareader["total_agrsupr_pendingcount"].ToString();

                }
                objODBCDatareader.Close();


                if (values.ocs_pendingcount != "0" || values.agrbyr_pendingcount != "0" || values.agrsupr_pendingcount != "0")
                {
                    values.message = "N";
                    values.status = false;
                    return false;
                }

            }

            msSQL = " update sys_mst_tregionmapping set status ='" + values.rbo_status + "'," +
                " remarks='" + values.remarks.Replace("'", "") + "'" +
                " where region_gid='" + values.region_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("RGIL");

                msSQL = " insert into sys_mst_tregioninactivelog (" +
                      " regioninactivelog_gid , " +
                      " region_gid," +
                      " region_name ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.region_gid + "'," +
                      " '" + values.region_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Region Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Region Activated Successfully";
                }
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
                return false;
            }

        }

        public void DaGetRegionInactiveLogview(string region_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT region_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tregioninactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where region_gid ='" + region_gid + "' order by a.regioninactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            region_gid = (dr_datarow["region_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaPostZoneAdd(zone values, string employee_gid)
        {
            msSQL = "select zone_name from sys_mst_tzonemapping where zone_name = '" + values.zone_name.Replace("'", "\\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Zone Name Already Exist";
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("ZONG");
                msGetAPICode = objcmnfunctions.GetApiMasterGID("ZONM");
                msSQL = " insert into sys_mst_tzonemapping(" +
                        " zone_gid ," +
                        " api_code," +
                        " zone_name," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                         "'" + msGetAPICode + "'," +
                        "'" + values.zone_name.Replace("'", "") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                for (var i = 0; i < values.region_list.Count; i++)
                {
                    msGet_regionGid = objcmnfunctions.GetMasterGID("Z2RG");

                    msSQL = "Insert into sys_mst_tzone2region( " +
                           " zone2region_gid, " +
                           " zone_gid," +
                           " region_gid," +
                           " region_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGet_regionGid + "'," +
                           "'" + msGetGid + "'," +
                           "'" + values.region_list[i].region_gid + "'," +
                           "'" + values.region_list[i].region_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Zone Added successfully";
                }
                else
                {
                    values.message = "Error Occured while Adding";
                    values.status = false;
                }
            }
        }

        public void DaGetZoneSummary(zone objmaster)
        {
            try
            {
                msSQL = " SELECT a.zone_gid ,a.api_code,a.zone_name, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tzonemapping a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getzone_list = new List<zone_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getzone_list.Add(new zone_list
                        {
                            zone_gid = (dr_datarow["zone_gid"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                            zone_name = (dr_datarow["zone_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            status = (dr_datarow["status"].ToString())
                        });
                    }
                    objmaster.zone_list = getzone_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaGetZoneEdit(string zone_gid, zone objmaster)
        {
            msSQL = " select zone_gid,zone_name, status as status from sys_mst_tzonemapping " +
                    " where zone_gid='" + zone_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objmaster.zone_gid = objODBCDatareader["zone_gid"].ToString();
                objmaster.zone_name = objODBCDatareader["zone_name"].ToString();
                objmaster.zone_status = objODBCDatareader["status"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select zone2region_gid,zone_gid,region_gid, region_name from sys_mst_tzone2region " +
                  " where zone_gid='" + zone_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getregionList = new List<region_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getregionList.Add(new region_list
                    {
                        region_gid = dt["region_gid"].ToString(),
                        region_name = dt["region_name"].ToString(),
                        zone2region_gid = dt["zone2region_gid"].ToString(),
                    });
                    objmaster.region_list = getregionList;
                }
            }
            dt_datatable.Dispose();

        }

        public bool DaPostZoneUpdate(string employee_gid, zone values)
        {
            string zoneGID;
            msSQL = "select zone_gid from sys_mst_tzonemapping where zone_name='" + values.zone_name.Replace("'", "\\'") + "'";
            zoneGID = objdbconn.GetExecuteScalar(msSQL);
            if (zoneGID != "")
            {
                if (zoneGID != values.zone_gid)
                {
                    values.status = false;
                    values.message = "Zone Name Already Exist";
                    return false;
                }
            }

            msSQL = "select updated_by, updated_date,zone_name from sys_mst_tzonemapping where zone_gid ='" + values.zone_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("ZONL");
                    msSQL = " insert into sys_mst_tzonemappinglog(" +
                              " zonelog_gid," +
                              " zone_gid," +
                              " zone_name , " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.zone_gid + "'," +
                              "'" + objODBCDatareader["zone_name"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();

            msSQL = "update sys_mst_tzonemapping set zone_name='" + values.zone_name.Replace("'", "") + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where zone_gid='" + values.zone_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from sys_mst_tzone2region where zone_gid ='" + values.zone_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                for (var i = 0; i < values.region_list.Count; i++)
                {
                    msGet_regionGid = objcmnfunctions.GetMasterGID("Z2RG");

                    msSQL = "Insert into sys_mst_tzone2region( " +
                           " zone2region_gid, " +
                           " zone_gid," +
                           " region_gid," +
                           " region_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGet_regionGid + "'," +
                           "'" + values.zone_gid + "'," +
                           "'" + values.region_list[i].region_gid + "'," +
                           "'" + values.region_list[i].region_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Zone updated successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Zone";
                return false;
            }
        }
        public void DaGetZone2Region(string zone_gid, region objmaster)
        {
            msSQL = " select zone2region_gid,zone_gid,region_gid, region_name from sys_mst_tzone2region " +
                  " where zone_gid='" + zone_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getregionList = new List<region_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getregionList.Add(new region_list
                    {
                        region_gid = dt["region_gid"].ToString(),
                        region_name = dt["region_name"].ToString(),
                        zone2region_gid = dt["zone2region_gid"].ToString(),
                    });
                    objmaster.region_list = getregionList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetUnTaggedRegions(zone objmaster)
        {
            try
            {
                msSQL = " SELECT region_gid ,region_name FROM sys_mst_tregionmapping" +
                        " where region_gid not in (select region_gid from sys_mst_tzone2region) " +
                        " order by region_name asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getregion_list = new List<region_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getregion_list.Add(new region_list
                        {
                            region_gid = (dr_datarow["region_gid"].ToString()),
                            region_name = (dr_datarow["region_name"].ToString()),
                        });
                    }
                    objmaster.region_list = getregion_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaGetUnTaggedRegionsEdit(zone objmaster, string zone_gid)
        {
            try
            {
                msSQL = " SELECT region_gid ,region_name FROM sys_mst_tregionmapping" +
                        " where region_gid not in (select region_gid from sys_mst_tzone2region where zone_gid<>'" + zone_gid + "') " +
                        " order by region_name asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getregion_list = new List<region_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getregion_list.Add(new region_list
                        {
                            region_gid = (dr_datarow["region_gid"].ToString()),
                            region_name = (dr_datarow["region_name"].ToString()),
                        });
                    }
                    objmaster.region_list = getregion_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public bool DaPostZoneInactive(zone values, string employee_gid)
        {


            if (values.rbo_status.ToString() == "N")
            {
                msSQL = " select (select count(application_gid)  from ocs_mst_tapplication where approval_status = 'Submitted to Approval' and zone_gid in (select zone_gid from sys_mst_tzone2region where zone_gid = '" + values.zone_gid + "')) as total_ocs_pendingcount, " +
                        " (select count(application_gid)  from agr_mst_tapplication where approval_status = 'Submitted to Approval' and  zone_gid in (select zone_gid from sys_mst_tzone2region where zone_gid = '" + values.zone_gid + "')) as total_agrbyr_pendingcount, " +
                        " (select count(application_gid) from agr_mst_tsuprapplication where approval_status = 'Submitted to Approval' and  zone_gid in (select zone_gid from sys_mst_tzone2region where zone_gid = '" + values.zone_gid + "')) as total_agrsupr_pendingcount  ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.ocs_pendingcount = objODBCDatareader["total_ocs_pendingcount"].ToString();
                    values.agrbyr_pendingcount = objODBCDatareader["total_agrbyr_pendingcount"].ToString();
                    values.agrsupr_pendingcount = objODBCDatareader["total_agrsupr_pendingcount"].ToString();

                }
                objODBCDatareader.Close();


                if (values.ocs_pendingcount != "0" || values.agrbyr_pendingcount != "0" || values.agrsupr_pendingcount != "0")
                {
                    values.message = "N";
                    values.status = false;
                    return false;
                }

            }


            msSQL = " update sys_mst_tzonemapping set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where zone_gid='" + values.zone_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("ZNIL");

                msSQL = " insert into sys_mst_tzoneinactivelog (" +
                      " zoneinactivelog_gid , " +
                      " zone_gid," +
                      " zone_name ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.zone_gid + "'," +
                      " '" + values.zone_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Zone Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Zone Activated Successfully";
                }
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
                return false;
            }
        }

        public void DaGetZoneInactiveLogview(string zone_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT zone_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tzoneinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where zone_gid ='" + zone_gid + "' order by a.zoneinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            zone_gid = (dr_datarow["zone_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetRegionList(region objmaster)
        {
            try
            {
                msSQL = " SELECT region_gid ,region_name " +
                        " FROM sys_mst_tregionmapping" +
                        " order by region_name asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getregion_list = new List<region_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getregion_list.Add(new region_list
                        {
                            region_gid = (dr_datarow["region_gid"].ToString()),
                            region_name = (dr_datarow["region_name"].ToString())
                        });
                    }
                    objmaster.region_list = getregion_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }
        public void DaGetVerticallist(mdlvertical objmaster)
        {
            try
            {
                msSQL = " SELECT vertical_gid ,vertical_name FROM ocs_mst_tvertical where status_log='Y' " +
                        " order by vertical_name asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getvertical_list = new List<vertical_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getvertical_list.Add(new vertical_list
                        {
                            vertical_gid = (dr_datarow["vertical_gid"].ToString()),
                            vertical_name = (dr_datarow["vertical_name"].ToString()),
                        });
                    }
                    objmaster.vertical_list = getvertical_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }
        public void DaGetEmployeelist(mdlemployee objmaster)
        {
            try
            {
                msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' || ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                   " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                   " where user_status<>'N' order by a.user_firstname asc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employee = new List<employee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    objmaster.employeelist = dt_datatable.AsEnumerable().Select(row =>
                      new employeelist
                      {
                          employee_gid = row["employee_gid"].ToString(),
                          employee_name = row["employee_name"].ToString()
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch (Exception ex)
            {
                objmaster.status = false;
            }
        }
        // Region Head Add
        public void DaPostRegionHeadAdd(mdlregionhead values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("RGHD");
            msGetAPICode = objcmnfunctions.GetApiMasterGID("REGH");
            msSQL = " insert into sys_mst_tregionhead(" +
                    " regionhead_gid," +
                    " api_code," +
                    " region_gid ," +
                    " region_name," +
                    " vertical_gid," +
                    " vertical_name," +
                    " employee_gid, " +
                    " employee_name, " +
                    " program_gid, " +
                    " program_name, " +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + msGetAPICode + "'," +
                    "'" + values.region_gid + "'," +
                    "'" + values.region_name + "'," +
                    "'" + values.vertical_gid + "'," +
                    "'" + values.vertical_name + "'," +
                    "'" + values.employee_gid + "'," +
                    "'" + values.employee_name + "'," +
                    "'" + values.program_gid + "'," +
                    "'" + values.program_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Region Head Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }

        }

        public void DaGetRegionHeadSummary(region objmaster)
        {
            try
            {
                msSQL = " SELECT a.regionhead_gid ,a.api_code,a.employee_name,a.employee_gid, a.region_name, a.vertical_name, " +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.program_name, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tregionhead a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getclusterhead_list = new List<regionhead_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getclusterhead_list.Add(new regionhead_list
                        {
                            regionhead_gid = (dr_datarow["regionhead_gid"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                            employee_gid = (dr_datarow["employee_gid"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            region_name = (dr_datarow["region_name"].ToString()),
                            program_name = (dr_datarow["program_name"].ToString()),
                            vertical_name = (dr_datarow["vertical_name"].ToString())
                        });
                    }
                    objmaster.regionhead_list = getclusterhead_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaPostRegionHeadInactive(mdlregionhead values, string employee_gid)
        {
            msSQL = " update sys_mst_tregionhead set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where regionhead_gid='" + values.regionhead_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("RHIL");

                msSQL = " insert into sys_mst_tregionheadinactivelog (" +
                      " regionheadinactivelog_gid , " +
                      " regionhead_gid," +
                      " employee_gid ," +
                      " employee_name ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.regionhead_gid + "'," +
                      " '" + values.employee_gid + "'," +
                      " '" + values.employee_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Region Head Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Region Head Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaGetRegionHeadInactiveLogview(string regionhead_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT regionhead_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tregionheadinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where regionhead_gid ='" + regionhead_gid + "' order by a.regionheadinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            regionhead_gid = (dr_datarow["regionhead_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetRegionHeadEdit(string regionhead_gid, mdlregionhead objmaster)
        {
            msSQL = " select regionhead_gid,region_gid, region_name, vertical_gid, vertical_name,program_gid,program_name, " +
                    " employee_gid, employee_name, status as status from sys_mst_tregionhead " +
                    " where regionhead_gid='" + regionhead_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objmaster.regionhead_gid = objODBCDatareader["regionhead_gid"].ToString();
                objmaster.region_gid = objODBCDatareader["region_gid"].ToString();
                objmaster.region_name = objODBCDatareader["region_name"].ToString();
                objmaster.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                objmaster.vertical_name = objODBCDatareader["vertical_name"].ToString();
                objmaster.employee_gid = objODBCDatareader["employee_gid"].ToString();
                objmaster.employee_name = objODBCDatareader["employee_name"].ToString();
                objmaster.regionhead_status = objODBCDatareader["status"].ToString();
                objmaster.program_gid = objODBCDatareader["program_gid"].ToString();
                objmaster.program_name = objODBCDatareader["program_name"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " SELECT region_gid ,region_name " +
                        " FROM sys_mst_tregionmapping" +
                        " order by region_name asc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getregion_list = new List<region_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getregion_list.Add(new region_list
                    {
                        region_gid = (dr_datarow["region_gid"].ToString()),
                        region_name = (dr_datarow["region_name"].ToString())
                    });
                }
                objmaster.region_list = getregion_list;
            }
            dt_datatable.Dispose();
            objmaster.status = true;

            msSQL = " SELECT vertical_gid ,vertical_name FROM ocs_mst_tvertical where status_log='Y' " +
                       " order by vertical_name asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getvertical_list = new List<vertical_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getvertical_list.Add(new vertical_list
                    {
                        vertical_gid = (dr_datarow["vertical_gid"].ToString()),
                        vertical_name = (dr_datarow["vertical_name"].ToString()),
                    });
                }
                objmaster.vertical_list = getvertical_list;
            }
            dt_datatable.Dispose();
            objmaster.status = true;

            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                   " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                   " where user_status<>'N' order by a.user_firstname asc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_employee = new List<employee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                objmaster.employeelist = dt_datatable.AsEnumerable().Select(row =>
                  new employeelist
                  {
                      employee_gid = row["employee_gid"].ToString(),
                      employee_name = row["employee_name"].ToString()
                  }
                ).ToList();
            }
            dt_datatable.Dispose();
            objmaster.status = true;

        }

        public bool DaPostRegionHeadUpdate(string employee_gid, mdlregionhead values)
        {

            msSQL = "select updated_by, updated_date,employee_gid, employee_name,program_gid,program_name, " +
                    "region_gid, region_name, vertical_gid, vertical_name from sys_mst_tregionhead where regionhead_gid ='" + values.regionhead_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("RGHL");
                    msSQL = " insert into sys_mst_tregionheadlog(" +
                              " regionheadlog_gid," +
                              " regionhead_gid," +
                              " region_gid , " +
                              " region_name , " +
                              " vertical_gid , " +
                              " vertical_name , " +
                              " program_gid, " +
                              " program_name, " +
                              " employee_gid , " +
                              " employee_name , " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.regionhead_gid + "'," +
                              "'" + objODBCDatareader["region_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["region_name"].ToString() + "'," +
                              "'" + objODBCDatareader["vertical_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["vertical_name"].ToString() + "'," +
                              "'" + objODBCDatareader["program_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["program_name"].ToString() + "'," +
                              "'" + objODBCDatareader["employee_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["employee_name"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = " select a.application_gid, application_no, customer_name, customer_urn, b.approval_status, vertical_gid, vertical_name, " +
                           " baselocation_gid, baselocation_name, cluster_gid, cluster_name, region_gid, region_name, zone_gid, zone_name, " +
                           " relationshipmanager_gid, relationshipmanager_name, drm_gid, drm_name, program_gid,program_name, " +
                           " clustermanager_gid, clustermanager_name, regionalhead_gid, regionalhead_name, zonalhead_gid, zonalhead_name, " +
                           " businesshead_gid, businesshead_name from ocs_mst_tapplication a " +
                           " left join ocs_trn_tapplicationapproval b on a.application_gid = b.application_gid " +
                           " where regionalhead_gid = '" + objODBCDatareader["employee_gid"].ToString() + "' and b.approval_status = 'Pending' and b.hierary_level='3' and " +
                           " a.vertical_gid='" + values.vertical_gid + "' and a.region_gid='" + values.region_gid + "' and a.program_gid ='" + values.program_gid + "'  group by b.application_gid ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        //msGetDocumentGid = objcmnfunctions.GetMasterGID("CRDO");

                        msSQL = " insert into ocs_mst_tapplicationlog(" +
                         " application_gid," +
                         " application_no, " +
                         " customer_urn," +
                         " customer_name," +
                         " program_gid, " +
                         " program_name, " +
                         " vertical_gid," +
                         " vertical_name, " +
                         " baselocation_gid," +
                         " baselocation_name, " +
                         " cluster_gid," +
                         " cluster_name," +
                         " region_gid," +
                         " region_name, " +
                         " zone_gid," +
                         " zone_name, " +
                         " relationshipmanager_gid," +
                         " relationshipmanager_name," +
                         " drm_gid," +
                         " drm_name, " +
                         " clustermanager_gid," +
                         " clustermanager_name, " +
                         " regionalhead_gid," +
                         " regionalhead_name," +
                         " zonalhead_gid," +
                         " zonalhead_name, " +
                         " businesshead_gid," +
                         " businesshead_name," +
                         " head_change," +
                         " created_by," +
                         " created_date)" +
                         " values(" +
                         "'" + dt["application_gid"].ToString() + "'," +
                         "'" + dt["application_no"].ToString() + "'," +
                         "'" + dt["customer_urn"].ToString() + "'," +
                         "'" + dt["customer_name"].ToString() + "'," +
                         "'" + dt["program_gid"].ToString() + "'," +
                         "'" + dt["program_name"].ToString() + "'," +
                         "'" + dt["vertical_gid"].ToString() + "'," +
                         "'" + dt["vertical_name"].ToString() + "'," +
                         "'" + dt["baselocation_gid"].ToString() + "'," +
                         "'" + dt["baselocation_name"].ToString() + "'," +
                         "'" + dt["cluster_gid"].ToString() + "'," +
                         "'" + dt["cluster_name"].ToString() + "'," +
                         "'" + dt["region_gid"].ToString() + "'," +
                         "'" + dt["region_name"].ToString() + "'," +
                         "'" + dt["zone_gid"].ToString() + "'," +
                         "'" + dt["zone_name"].ToString() + "'," +
                         "'" + dt["relationshipmanager_gid"].ToString() + "'," +
                         "'" + dt["relationshipmanager_name"].ToString() + "'," +
                         "'" + dt["drm_gid"].ToString() + "'," +
                         "'" + dt["drm_name"].ToString() + "'," +
                         "'" + dt["clustermanager_gid"].ToString() + "'," +
                         "'" + dt["clustermanager_name"].ToString() + "'," +
                         "'" + dt["regionalhead_gid"].ToString() + "'," +
                         "'" + dt["regionalhead_name"].ToString() + "'," +
                         "'" + dt["zonalhead_gid"].ToString() + "'," +
                         "'" + dt["zonalhead_name"].ToString() + "'," +
                         "'" + dt["businesshead_gid"].ToString() + "'," +
                         "'" + dt["businesshead_name"].ToString() + "'," +
                         "'RH'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update ocs_mst_tapplication set " +
                                " regionalhead_gid='" + values.employee_gid + "'," +
                                " regionalhead_name='" + values.employee_name + "'" +
                                " where application_gid='" + dt["application_gid"].ToString() + "' and vertical_gid='" + values.vertical_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update ocs_trn_tapplicationapproval set " +
                                " approval_gid='" + values.employee_gid + "'," +
                                " approval_name='" + values.employee_name + "'" +
                                " where application_gid='" + dt["application_gid"].ToString() + "' and hierary_level='3' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();

                msSQL = " select a.application_gid, application_no, customer_name, customer_urn, b.approval_status, vertical_gid, vertical_name, " +
                          " baselocation_gid, baselocation_name, cluster_gid, cluster_name, region_gid, region_name, zone_gid, zone_name, " +
                          " relationshipmanager_gid, relationshipmanager_name, drm_gid, drm_name, program_gid,program_name, " +
                          " clustermanager_gid, clustermanager_name, regionalhead_gid, regionalhead_name, zonalhead_gid, zonalhead_name, " +
                          " businesshead_gid, businesshead_name from agr_mst_tapplication a " +
                          " left join agr_trn_tapplicationapproval b on a.application_gid = b.application_gid " +
                          " where regionalhead_gid = '" + objODBCDatareader["employee_gid"].ToString() + "' and b.approval_status = 'Pending' and b.hierary_level='3' and " +
                          " a.vertical_gid='" + values.vertical_gid + "' and a.region_gid='" + values.region_gid + "' and a.program_gid ='" + values.program_gid + "'  group by b.application_gid ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        //msGetDocumentGid = objcmnfunctions.GetMasterGID("CRDO");

                        msSQL = " insert into agr_mst_tapplicationlog(" +
                         " application_gid," +
                         " application_no, " +
                         " customer_urn," +
                         " customer_name," +
                         " program_gid, " +
                         " program_name, " +
                         " vertical_gid," +
                         " vertical_name, " +
                         " baselocation_gid," +
                         " baselocation_name, " +
                         " cluster_gid," +
                         " cluster_name," +
                         " region_gid," +
                         " region_name, " +
                         " zone_gid," +
                         " zone_name, " +
                         " relationshipmanager_gid," +
                         " relationshipmanager_name," +
                         " drm_gid," +
                         " drm_name, " +
                         " clustermanager_gid," +
                         " clustermanager_name, " +
                         " regionalhead_gid," +
                         " regionalhead_name," +
                         " zonalhead_gid," +
                         " zonalhead_name, " +
                         " businesshead_gid," +
                         " businesshead_name," +
                         " head_change," +
                         " created_by," +
                         " created_date)" +
                         " values(" +
                         "'" + dt["application_gid"].ToString() + "'," +
                         "'" + dt["application_no"].ToString() + "'," +
                         "'" + dt["customer_urn"].ToString() + "'," +
                         "'" + dt["customer_name"].ToString() + "'," +
                         "'" + dt["program_gid"].ToString() + "'," +
                         "'" + dt["program_name"].ToString() + "'," +
                         "'" + dt["vertical_gid"].ToString() + "'," +
                         "'" + dt["vertical_name"].ToString() + "'," +
                         "'" + dt["baselocation_gid"].ToString() + "'," +
                         "'" + dt["baselocation_name"].ToString() + "'," +
                         "'" + dt["cluster_gid"].ToString() + "'," +
                         "'" + dt["cluster_name"].ToString() + "'," +
                         "'" + dt["region_gid"].ToString() + "'," +
                         "'" + dt["region_name"].ToString() + "'," +
                         "'" + dt["zone_gid"].ToString() + "'," +
                         "'" + dt["zone_name"].ToString() + "'," +
                         "'" + dt["relationshipmanager_gid"].ToString() + "'," +
                         "'" + dt["relationshipmanager_name"].ToString() + "'," +
                         "'" + dt["drm_gid"].ToString() + "'," +
                         "'" + dt["drm_name"].ToString() + "'," +
                         "'" + dt["clustermanager_gid"].ToString() + "'," +
                         "'" + dt["clustermanager_name"].ToString() + "'," +
                         "'" + dt["regionalhead_gid"].ToString() + "'," +
                         "'" + dt["regionalhead_name"].ToString() + "'," +
                         "'" + dt["zonalhead_gid"].ToString() + "'," +
                         "'" + dt["zonalhead_name"].ToString() + "'," +
                         "'" + dt["businesshead_gid"].ToString() + "'," +
                         "'" + dt["businesshead_name"].ToString() + "'," +
                         "'RH'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_mst_tapplication set " +
                                " regionalhead_gid='" + values.employee_gid + "'," +
                                " regionalhead_name='" + values.employee_name + "'" +
                                " where application_gid='" + dt["application_gid"].ToString() + "' and vertical_gid='" + values.vertical_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_trn_tapplicationapproval set " +
                                " approval_gid='" + values.employee_gid + "'," +
                                " approval_name='" + values.employee_name + "'" +
                                " where application_gid='" + dt["application_gid"].ToString() + "' and hierary_level='3' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();


                msSQL = " select a.application_gid, application_no, customer_name, customer_urn, b.approval_status, vertical_gid, vertical_name, " +
                          " baselocation_gid, baselocation_name, cluster_gid, cluster_name, region_gid, region_name, zone_gid, zone_name, " +
                          " relationshipmanager_gid, relationshipmanager_name, drm_gid, drm_name, program_gid,program_name, " +
                          " clustermanager_gid, clustermanager_name, regionalhead_gid, regionalhead_name, zonalhead_gid, zonalhead_name, " +
                          " businesshead_gid, businesshead_name from agr_mst_tsuprapplication a " +
                          " left join agr_trn_tsuprapplicationapproval b on a.application_gid = b.application_gid " +
                          " where regionalhead_gid = '" + objODBCDatareader["employee_gid"].ToString() + "' and b.approval_status = 'Pending' and b.hierary_level='3' and " +
                          " a.vertical_gid='" + values.vertical_gid + "' and a.region_gid='" + values.region_gid + "' and a.program_gid ='" + values.program_gid + "'  group by b.application_gid ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        //msGetDocumentGid = objcmnfunctions.GetMasterGID("CRDO");

                        msSQL = " insert into agr_mst_tsuprapplicationlog(" +
                         " application_gid," +
                         " application_no, " +
                         " customer_urn," +
                         " customer_name," +
                         " program_gid, " +
                         " program_name, " +
                         " vertical_gid," +
                         " vertical_name, " +
                         " baselocation_gid," +
                         " baselocation_name, " +
                         " cluster_gid," +
                         " cluster_name," +
                         " region_gid," +
                         " region_name, " +
                         " zone_gid," +
                         " zone_name, " +
                         " relationshipmanager_gid," +
                         " relationshipmanager_name," +
                         " drm_gid," +
                         " drm_name, " +
                         " clustermanager_gid," +
                         " clustermanager_name, " +
                         " regionalhead_gid," +
                         " regionalhead_name," +
                         " zonalhead_gid," +
                         " zonalhead_name, " +
                         " businesshead_gid," +
                         " businesshead_name," +
                         " head_change," +
                         " created_by," +
                         " created_date)" +
                         " values(" +
                         "'" + dt["application_gid"].ToString() + "'," +
                         "'" + dt["application_no"].ToString() + "'," +
                         "'" + dt["customer_urn"].ToString() + "'," +
                         "'" + dt["customer_name"].ToString() + "'," +
                         "'" + dt["program_gid"].ToString() + "'," +
                         "'" + dt["program_name"].ToString() + "'," +
                         "'" + dt["vertical_gid"].ToString() + "'," +
                         "'" + dt["vertical_name"].ToString() + "'," +
                         "'" + dt["baselocation_gid"].ToString() + "'," +
                         "'" + dt["baselocation_name"].ToString() + "'," +
                         "'" + dt["cluster_gid"].ToString() + "'," +
                         "'" + dt["cluster_name"].ToString() + "'," +
                         "'" + dt["region_gid"].ToString() + "'," +
                         "'" + dt["region_name"].ToString() + "'," +
                         "'" + dt["zone_gid"].ToString() + "'," +
                         "'" + dt["zone_name"].ToString() + "'," +
                         "'" + dt["relationshipmanager_gid"].ToString() + "'," +
                         "'" + dt["relationshipmanager_name"].ToString() + "'," +
                         "'" + dt["drm_gid"].ToString() + "'," +
                         "'" + dt["drm_name"].ToString() + "'," +
                         "'" + dt["clustermanager_gid"].ToString() + "'," +
                         "'" + dt["clustermanager_name"].ToString() + "'," +
                         "'" + dt["regionalhead_gid"].ToString() + "'," +
                         "'" + dt["regionalhead_name"].ToString() + "'," +
                         "'" + dt["zonalhead_gid"].ToString() + "'," +
                         "'" + dt["zonalhead_name"].ToString() + "'," +
                         "'" + dt["businesshead_gid"].ToString() + "'," +
                         "'" + dt["businesshead_name"].ToString() + "'," +
                         "'RH'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_mst_tsuprapplication set " +
                                " regionalhead_gid='" + values.employee_gid + "'," +
                                " regionalhead_name='" + values.employee_name + "'" +
                                " where application_gid='" + dt["application_gid"].ToString() + "' and vertical_gid='" + values.vertical_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_trn_tsuprapplicationapproval set " +
                                " approval_gid='" + values.employee_gid + "'," +
                                " approval_name='" + values.employee_name + "'" +
                                " where application_gid='" + dt["application_gid"].ToString() + "' and hierary_level='3' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();

            }
            objODBCDatareader.Close();

            msSQL = "update sys_mst_tregionhead set region_gid='" + values.region_gid + "'," +
                 " region_name='" + values.region_name + "'," +
                 " vertical_gid='" + values.vertical_gid + "'," +
                 " vertical_name='" + values.vertical_name + "'," +
                 " program_gid='" + values.program_gid + "'," +
                 " program_name='" + values.program_name + "'," +
                 " employee_gid='" + values.employee_gid + "'," +
                 " employee_name='" + values.employee_name + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where regionhead_gid='" + values.regionhead_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Region Head updated successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Region Head";
                return false;
            }
        }

        public void DaGetZoneList(zone objmaster)
        {
            try
            {
                msSQL = " SELECT zone_gid ,zone_name " +
                        " FROM sys_mst_tzonemapping" +
                        " order by zone_name asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getzone_list = new List<zone_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getzone_list.Add(new zone_list
                        {
                            zone_gid = (dr_datarow["zone_gid"].ToString()),
                            zone_name = (dr_datarow["zone_name"].ToString())
                        });
                    }
                    objmaster.zone_list = getzone_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }
        // Business Head Add
        public void DaPostBusinessHeadAdd(mdlbusinesshead values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("BSHD");
            msGetAPICode = objcmnfunctions.GetApiMasterGID("BUSH");
            msSQL = " insert into sys_mst_tbusinesshead(" +
                    " businesshead_gid," +
                    " api_code," +
                    " zone_gid ," +
                    " zone_name," +
                    " vertical_gid," +
                    " vertical_name," +
                    " program_gid, " +
                    " program_name, " +
                    " employee_gid, " +
                    " employee_name, " +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                     "'" + msGetAPICode + "'," +
                    "'" + values.zone_gid + "'," +
                    "'" + values.zone_name + "'," +
                    "'" + values.vertical_gid + "'," +
                    "'" + values.vertical_name + "'," +
                    "'" + values.program_gid + "'," +
                    "'" + values.program_name + "'," +
                    "'" + values.employee_gid + "'," +
                    "'" + values.employee_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Business Head Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }

        }

        public void DaGetBusinessHeadSummary(mdlbusinesshead objmaster)
        {
            try
            {
                msSQL = " SELECT a.businesshead_gid ,a.api_code,a.employee_name,a.employee_gid, a.zone_name, a.vertical_name,a.program_name, " +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tbusinesshead a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getbusinesshead_list = new List<businesshead_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getbusinesshead_list.Add(new businesshead_list
                        {
                            businesshead_gid = (dr_datarow["businesshead_gid"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                            employee_gid = (dr_datarow["employee_gid"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            zone_name = (dr_datarow["zone_name"].ToString()),
                            vertical_name = (dr_datarow["vertical_name"].ToString()),
                            program_name = (dr_datarow["program_name"].ToString())
                        });
                    }
                    objmaster.businesshead_list = getbusinesshead_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaPostBusinessHeadInactive(mdlbusinesshead values, string employee_gid)
        {
            msSQL = " update sys_mst_tbusinesshead set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where businesshead_gid='" + values.businesshead_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("BHIL");

                msSQL = " insert into sys_mst_tbusinessheadinactivelog (" +
                      " businessheadinactivelog_gid , " +
                      " businesshead_gid," +
                      " employee_gid ," +
                      " employee_name ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.businesshead_gid + "'," +
                      " '" + values.employee_gid + "'," +
                      " '" + values.employee_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Business Head Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Business Head Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaGetBusinessHeadInactiveLogview(string businesshead_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT businesshead_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tbusinessheadinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where businesshead_gid ='" + businesshead_gid + "' order by a.businessheadinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            businesshead_gid = (dr_datarow["businesshead_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetBusinessHeadEdit(string businesshead_gid, mdlbusinesshead objmaster)
        {
            msSQL = " select businesshead_gid,zone_gid, zone_name, vertical_gid, vertical_name, " +
                    " employee_gid, employee_name, status as status,program_gid, program_name from sys_mst_tbusinesshead " +
                    " where businesshead_gid='" + businesshead_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objmaster.businesshead_gid = objODBCDatareader["businesshead_gid"].ToString();
                objmaster.zone_gid = objODBCDatareader["zone_gid"].ToString();
                objmaster.zone_name = objODBCDatareader["zone_name"].ToString();
                objmaster.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                objmaster.vertical_name = objODBCDatareader["vertical_name"].ToString();
                objmaster.employee_gid = objODBCDatareader["employee_gid"].ToString();
                objmaster.employee_name = objODBCDatareader["employee_name"].ToString();
                objmaster.businesshead_status = objODBCDatareader["status"].ToString();
                objmaster.program_gid = objODBCDatareader["program_gid"].ToString();
                objmaster.program_name = objODBCDatareader["program_name"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " SELECT zone_gid ,zone_name FROM sys_mst_tzonemapping" +
                    " order by zone_name asc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getzone_list = new List<zone_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getzone_list.Add(new zone_list
                    {
                        zone_gid = (dr_datarow["zone_gid"].ToString()),
                        zone_name = (dr_datarow["zone_name"].ToString())
                    });
                }
                objmaster.zone_list = getzone_list;
            }
            dt_datatable.Dispose();
            objmaster.status = true;

            msSQL = " SELECT vertical_gid ,vertical_name FROM ocs_mst_tvertical  where status_log='Y' " +
                       " order by vertical_name asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getvertical_list = new List<vertical_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getvertical_list.Add(new vertical_list
                    {
                        vertical_gid = (dr_datarow["vertical_gid"].ToString()),
                        vertical_name = (dr_datarow["vertical_name"].ToString()),
                    });
                }
                objmaster.vertical_list = getvertical_list;
            }
            dt_datatable.Dispose();
            objmaster.status = true;

            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                   " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                   " where user_status<>'N' order by a.user_firstname asc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_employee = new List<employee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                objmaster.employeelist = dt_datatable.AsEnumerable().Select(row =>
                  new employeelist
                  {
                      employee_gid = row["employee_gid"].ToString(),
                      employee_name = row["employee_name"].ToString()
                  }
                ).ToList();
            }
            dt_datatable.Dispose();
            objmaster.status = true;

        }

        public bool DaPostBusinessHeadUpdate(string employee_gid, mdlbusinesshead values)
        {

            msSQL = "select updated_by, updated_date,employee_gid, employee_name,program_gid, program_name, " +
                    "zone_gid, zone_name, vertical_gid, vertical_name from sys_mst_tbusinesshead where businesshead_gid ='" + values.businesshead_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("BSHL");
                    msSQL = " insert into sys_mst_tbusinessheadlog(" +
                              " businessheadlog_gid," +
                              " businesshead_gid," +
                              " zone_gid , " +
                              " zone_name , " +
                              " vertical_gid , " +
                              " vertical_name , " +
                              " program_gid , " +
                              " program_name , " +
                              " employee_gid , " +
                              " employee_name , " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.businesshead_gid + "'," +
                              "'" + objODBCDatareader["zone_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["zone_name"].ToString() + "'," +
                              "'" + objODBCDatareader["vertical_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["vertical_name"].ToString() + "'," +
                              "'" + objODBCDatareader["program_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["program_name"].ToString() + "'," +
                              "'" + objODBCDatareader["employee_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["employee_name"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = " select a.application_gid, application_no, customer_name, customer_urn, b.approval_status, vertical_gid, vertical_name, " +
                           " baselocation_gid, baselocation_name, cluster_gid, cluster_name, region_gid, region_name, zone_gid, zone_name, " +
                           " relationshipmanager_gid, relationshipmanager_name, drm_gid, drm_name,program_gid, program_name, " +
                           " clustermanager_gid, clustermanager_name, regionalhead_gid, regionalhead_name, zonalhead_gid, zonalhead_name, " +
                           " businesshead_gid, businesshead_name from ocs_mst_tapplication a " +
                           " left join ocs_trn_tapplicationapproval b on a.application_gid = b.application_gid " +
                           " where businesshead_gid = '" + objODBCDatareader["employee_gid"].ToString() + "' and b.approval_status = 'Pending' and b.hierary_level='5' and " +
                           " a.vertical_gid='" + values.vertical_gid + "' and a.zone_gid='" + values.zone_gid + "' and a.program_gid ='" + values.program_gid + "'  group by b.application_gid ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        //msGetDocumentGid = objcmnfunctions.GetMasterGID("CRDO");

                        msSQL = " insert into ocs_mst_tapplicationlog(" +
                         " application_gid," +
                         " application_no, " +
                         " customer_urn," +
                         " customer_name," +
                         " vertical_gid," +
                         " vertical_name, " +
                         " program_gid," +
                         " program_name, " +
                         " baselocation_gid," +
                         " baselocation_name, " +
                         " cluster_gid," +
                         " cluster_name," +
                         " region_gid," +
                         " region_name, " +
                         " zone_gid," +
                         " zone_name, " +
                         " relationshipmanager_gid," +
                         " relationshipmanager_name," +
                         " drm_gid," +
                         " drm_name, " +
                         " clustermanager_gid," +
                         " clustermanager_name, " +
                         " regionalhead_gid," +
                         " regionalhead_name," +
                         " zonalhead_gid," +
                         " zonalhead_name, " +
                         " businesshead_gid," +
                         " businesshead_name," +
                         " head_change," +
                         " created_by," +
                         " created_date)" +
                         " values(" +
                         "'" + dt["application_gid"].ToString() + "'," +
                         "'" + dt["application_no"].ToString() + "'," +
                         "'" + dt["customer_urn"].ToString() + "'," +
                         "'" + dt["customer_name"].ToString() + "'," +
                         "'" + dt["vertical_gid"].ToString() + "'," +
                         "'" + dt["vertical_name"].ToString() + "'," +
                         "'" + dt["program_gid"].ToString() + "'," +
                         "'" + dt["program_name"].ToString() + "'," +
                         "'" + dt["baselocation_gid"].ToString() + "'," +
                         "'" + dt["baselocation_name"].ToString() + "'," +
                         "'" + dt["cluster_gid"].ToString() + "'," +
                         "'" + dt["cluster_name"].ToString() + "'," +
                         "'" + dt["region_gid"].ToString() + "'," +
                         "'" + dt["region_name"].ToString() + "'," +
                         "'" + dt["zone_gid"].ToString() + "'," +
                         "'" + dt["zone_name"].ToString() + "'," +
                         "'" + dt["relationshipmanager_gid"].ToString() + "'," +
                         "'" + dt["relationshipmanager_name"].ToString() + "'," +
                         "'" + dt["drm_gid"].ToString() + "'," +
                         "'" + dt["drm_name"].ToString() + "'," +
                         "'" + dt["clustermanager_gid"].ToString() + "'," +
                         "'" + dt["clustermanager_name"].ToString() + "'," +
                         "'" + dt["regionalhead_gid"].ToString() + "'," +
                         "'" + dt["regionalhead_name"].ToString() + "'," +
                         "'" + dt["zonalhead_gid"].ToString() + "'," +
                         "'" + dt["zonalhead_name"].ToString() + "'," +
                         "'" + dt["businesshead_gid"].ToString() + "'," +
                         "'" + dt["businesshead_name"].ToString() + "'," +
                         "'BVH'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update ocs_mst_tapplication set " +
                                " businesshead_gid='" + values.employee_gid + "'," +
                                " businesshead_name='" + values.employee_name + "'" +
                                " where application_gid='" + dt["application_gid"].ToString() + "' and vertical_gid='" + values.vertical_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update ocs_trn_tapplicationapproval set " +
                                " approval_gid='" + values.employee_gid + "'," +
                                " approval_name='" + values.employee_name + "'" +
                                " where application_gid='" + dt["application_gid"].ToString() + "' and hierary_level='5' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();

                msSQL = " select a.application_gid, application_no, customer_name, customer_urn, b.approval_status, vertical_gid, vertical_name, " +
                          " baselocation_gid, baselocation_name, cluster_gid, cluster_name, region_gid, region_name, zone_gid, zone_name, " +
                          " relationshipmanager_gid, relationshipmanager_name, drm_gid, drm_name,program_gid, program_name, " +
                          " clustermanager_gid, clustermanager_name, regionalhead_gid, regionalhead_name, zonalhead_gid, zonalhead_name, " +
                          " businesshead_gid, businesshead_name from agr_mst_tapplication a " +
                          " left join agr_trn_tapplicationapproval b on a.application_gid = b.application_gid " +
                          " where businesshead_gid = '" + objODBCDatareader["employee_gid"].ToString() + "' and b.approval_status = 'Pending' and b.hierary_level='5' and " +
                          " a.vertical_gid='" + values.vertical_gid + "' and a.zone_gid='" + values.zone_gid + "' and a.program_gid ='" + values.program_gid + "'  group by b.application_gid ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        //msGetDocumentGid = objcmnfunctions.GetMasterGID("CRDO");

                        msSQL = " insert into agr_mst_tapplicationlog(" +
                         " application_gid," +
                         " application_no, " +
                         " customer_urn," +
                         " customer_name," +
                         " vertical_gid," +
                         " vertical_name, " +
                         " program_gid," +
                         " program_name, " +
                         " baselocation_gid," +
                         " baselocation_name, " +
                         " cluster_gid," +
                         " cluster_name," +
                         " region_gid," +
                         " region_name, " +
                         " zone_gid," +
                         " zone_name, " +
                         " relationshipmanager_gid," +
                         " relationshipmanager_name," +
                         " drm_gid," +
                         " drm_name, " +
                         " clustermanager_gid," +
                         " clustermanager_name, " +
                         " regionalhead_gid," +
                         " regionalhead_name," +
                         " zonalhead_gid," +
                         " zonalhead_name, " +
                         " businesshead_gid," +
                         " businesshead_name," +
                         " head_change," +
                         " created_by," +
                         " created_date)" +
                         " values(" +
                         "'" + dt["application_gid"].ToString() + "'," +
                         "'" + dt["application_no"].ToString() + "'," +
                         "'" + dt["customer_urn"].ToString() + "'," +
                         "'" + dt["customer_name"].ToString() + "'," +
                         "'" + dt["vertical_gid"].ToString() + "'," +
                         "'" + dt["vertical_name"].ToString() + "'," +
                         "'" + dt["program_gid"].ToString() + "'," +
                         "'" + dt["program_name"].ToString() + "'," +
                         "'" + dt["baselocation_gid"].ToString() + "'," +
                         "'" + dt["baselocation_name"].ToString() + "'," +
                         "'" + dt["cluster_gid"].ToString() + "'," +
                         "'" + dt["cluster_name"].ToString() + "'," +
                         "'" + dt["region_gid"].ToString() + "'," +
                         "'" + dt["region_name"].ToString() + "'," +
                         "'" + dt["zone_gid"].ToString() + "'," +
                         "'" + dt["zone_name"].ToString() + "'," +
                         "'" + dt["relationshipmanager_gid"].ToString() + "'," +
                         "'" + dt["relationshipmanager_name"].ToString() + "'," +
                         "'" + dt["drm_gid"].ToString() + "'," +
                         "'" + dt["drm_name"].ToString() + "'," +
                         "'" + dt["clustermanager_gid"].ToString() + "'," +
                         "'" + dt["clustermanager_name"].ToString() + "'," +
                         "'" + dt["regionalhead_gid"].ToString() + "'," +
                         "'" + dt["regionalhead_name"].ToString() + "'," +
                         "'" + dt["zonalhead_gid"].ToString() + "'," +
                         "'" + dt["zonalhead_name"].ToString() + "'," +
                         "'" + dt["businesshead_gid"].ToString() + "'," +
                         "'" + dt["businesshead_name"].ToString() + "'," +
                         "'BVH'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_mst_tapplication set " +
                                " businesshead_gid='" + values.employee_gid + "'," +
                                " businesshead_name='" + values.employee_name + "'" +
                                " where application_gid='" + dt["application_gid"].ToString() + "' and vertical_gid='" + values.vertical_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_trn_tapplicationapproval set " +
                                " approval_gid='" + values.employee_gid + "'," +
                                " approval_name='" + values.employee_name + "'" +
                                " where application_gid='" + dt["application_gid"].ToString() + "' and hierary_level='5' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();

                msSQL = " select a.application_gid, application_no, customer_name, customer_urn, b.approval_status, vertical_gid, vertical_name, " +
                          " baselocation_gid, baselocation_name, cluster_gid, cluster_name, region_gid, region_name, zone_gid, zone_name, " +
                          " relationshipmanager_gid, relationshipmanager_name, drm_gid, drm_name,program_gid, program_name, " +
                          " clustermanager_gid, clustermanager_name, regionalhead_gid, regionalhead_name, zonalhead_gid, zonalhead_name, " +
                          " businesshead_gid, businesshead_name from agr_mst_tsuprapplication a " +
                          " left join agr_trn_tsuprapplicationapproval b on a.application_gid = b.application_gid " +
                          " where businesshead_gid = '" + objODBCDatareader["employee_gid"].ToString() + "' and b.approval_status = 'Pending' and b.hierary_level='5' and " +
                          " a.vertical_gid='" + values.vertical_gid + "' and a.zone_gid='" + values.zone_gid + "' and a.program_gid ='" + values.program_gid + "'  group by b.application_gid ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        //msGetDocumentGid = objcmnfunctions.GetMasterGID("CRDO");

                        msSQL = " insert into agr_mst_tsuprapplicationlog(" +
                         " application_gid," +
                         " application_no, " +
                         " customer_urn," +
                         " customer_name," +
                         " vertical_gid," +
                         " vertical_name, " +
                         " program_gid," +
                         " program_name, " +
                         " baselocation_gid," +
                         " baselocation_name, " +
                         " cluster_gid," +
                         " cluster_name," +
                         " region_gid," +
                         " region_name, " +
                         " zone_gid," +
                         " zone_name, " +
                         " relationshipmanager_gid," +
                         " relationshipmanager_name," +
                         " drm_gid," +
                         " drm_name, " +
                         " clustermanager_gid," +
                         " clustermanager_name, " +
                         " regionalhead_gid," +
                         " regionalhead_name," +
                         " zonalhead_gid," +
                         " zonalhead_name, " +
                         " businesshead_gid," +
                         " businesshead_name," +
                         " head_change," +
                         " created_by," +
                         " created_date)" +
                         " values(" +
                         "'" + dt["application_gid"].ToString() + "'," +
                         "'" + dt["application_no"].ToString() + "'," +
                         "'" + dt["customer_urn"].ToString() + "'," +
                         "'" + dt["customer_name"].ToString() + "'," +
                         "'" + dt["vertical_gid"].ToString() + "'," +
                         "'" + dt["vertical_name"].ToString() + "'," +
                         "'" + dt["program_gid"].ToString() + "'," +
                         "'" + dt["program_name"].ToString() + "'," +
                         "'" + dt["baselocation_gid"].ToString() + "'," +
                         "'" + dt["baselocation_name"].ToString() + "'," +
                         "'" + dt["cluster_gid"].ToString() + "'," +
                         "'" + dt["cluster_name"].ToString() + "'," +
                         "'" + dt["region_gid"].ToString() + "'," +
                         "'" + dt["region_name"].ToString() + "'," +
                         "'" + dt["zone_gid"].ToString() + "'," +
                         "'" + dt["zone_name"].ToString() + "'," +
                         "'" + dt["relationshipmanager_gid"].ToString() + "'," +
                         "'" + dt["relationshipmanager_name"].ToString() + "'," +
                         "'" + dt["drm_gid"].ToString() + "'," +
                         "'" + dt["drm_name"].ToString() + "'," +
                         "'" + dt["clustermanager_gid"].ToString() + "'," +
                         "'" + dt["clustermanager_name"].ToString() + "'," +
                         "'" + dt["regionalhead_gid"].ToString() + "'," +
                         "'" + dt["regionalhead_name"].ToString() + "'," +
                         "'" + dt["zonalhead_gid"].ToString() + "'," +
                         "'" + dt["zonalhead_name"].ToString() + "'," +
                         "'" + dt["businesshead_gid"].ToString() + "'," +
                         "'" + dt["businesshead_name"].ToString() + "'," +
                         "'BVH'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_mst_tsuprapplication set " +
                                " businesshead_gid='" + values.employee_gid + "'," +
                                " businesshead_name='" + values.employee_name + "'" +
                                " where application_gid='" + dt["application_gid"].ToString() + "' and vertical_gid='" + values.vertical_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_trn_tsuprapplicationapproval set " +
                                " approval_gid='" + values.employee_gid + "'," +
                                " approval_name='" + values.employee_name + "'" +
                                " where application_gid='" + dt["application_gid"].ToString() + "' and hierary_level='5' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();

            }
            objODBCDatareader.Close();

            msSQL = "update sys_mst_tbusinesshead set zone_gid='" + values.zone_gid + "'," +
                 " zone_name='" + values.zone_name + "'," +
                 " vertical_gid='" + values.vertical_gid + "'," +
                 " vertical_name='" + values.vertical_name + "'," +
                 " program_gid='" + values.program_gid + "'," +
                 " program_name='" + values.program_name + "'," +
                 " employee_gid='" + values.employee_gid + "'," +
                 " employee_name='" + values.employee_name + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where businesshead_gid='" + values.businesshead_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Business Head updated successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Business Head";
                return false;
            }
        }

        // Group Business Head Add
        public void DaPostGroupBusinessHeadAdd(mdlbusinesshead values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("GBHD");
            msGetAPICode = objcmnfunctions.GetApiMasterGID("GBUH");
            msSQL = " insert into sys_mst_tgroupbusinesshead(" +
                    " groupbusinesshead_gid," +
                    " api_code," +
                    " zone_gid ," +
                    " zone_name," +
                    " vertical_gid," +
                    " vertical_name," +
                    " employee_gid, " +
                    " employee_name, " +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                     "'" + msGetAPICode + "'," +
                    "'" + values.zone_gid + "'," +
                    "'" + values.zone_name + "'," +
                    "'" + values.vertical_gid + "'," +
                    "'" + values.vertical_name + "'," +
                    "'" + values.employee_gid + "'," +
                    "'" + values.employee_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Group Business Head Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }

        }

        public void DaGetGroupBusinessHeadSummary(mdlbusinesshead objmaster)
        {
            try
            {
                msSQL = " SELECT a.groupbusinesshead_gid ,a.api_code,a.employee_name,a.employee_gid, a.zone_name, a.vertical_name, " +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tgroupbusinesshead a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getbusinesshead_list = new List<businesshead_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getbusinesshead_list.Add(new businesshead_list
                        {
                            groupbusinesshead_gid = (dr_datarow["groupbusinesshead_gid"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                            employee_gid = (dr_datarow["employee_gid"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            zone_name = (dr_datarow["zone_name"].ToString()),
                            vertical_name = (dr_datarow["vertical_name"].ToString())
                        });
                    }
                    objmaster.businesshead_list = getbusinesshead_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaPostGroupBusinessHeadInactive(mdlbusinesshead values, string employee_gid)
        {
            msSQL = " update sys_mst_tgroupbusinesshead set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where groupbusinesshead_gid='" + values.groupbusinesshead_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("GBIL");

                msSQL = " insert into sys_mst_tgroupbusinessheadinactivelog (" +
                      " groupbusinessheadinactivelog_gid , " +
                      " groupbusinesshead_gid," +
                      " employee_gid ," +
                      " employee_name ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.groupbusinesshead_gid + "'," +
                      " '" + values.employee_gid + "'," +
                      " '" + values.employee_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Group Business Head Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Group Business Head Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaGetGroupBusinessHeadInactiveLogview(string groupbusinesshead_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT groupbusinesshead_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tgroupbusinessheadinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where groupbusinesshead_gid ='" + groupbusinesshead_gid + "' order by a.groupbusinessheadinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            groupbusinesshead_gid = (dr_datarow["groupbusinesshead_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetGroupBusinessHeadEdit(string groupbusinesshead_gid, mdlbusinesshead objmaster)
        {
            msSQL = " select groupbusinesshead_gid,zone_gid, zone_name, vertical_gid, vertical_name, " +
                    " employee_gid, employee_name, status as status from sys_mst_tgroupbusinesshead " +
                    " where groupbusinesshead_gid='" + groupbusinesshead_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objmaster.groupbusinesshead_gid = objODBCDatareader["groupbusinesshead_gid"].ToString();
                objmaster.zone_gid = objODBCDatareader["zone_gid"].ToString();
                objmaster.zone_name = objODBCDatareader["zone_name"].ToString();
                objmaster.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                objmaster.vertical_name = objODBCDatareader["vertical_name"].ToString();
                objmaster.employee_gid = objODBCDatareader["employee_gid"].ToString();
                objmaster.employee_name = objODBCDatareader["employee_name"].ToString();
                objmaster.businesshead_status = objODBCDatareader["status"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " SELECT zone_gid ,zone_name FROM sys_mst_tzonemapping" +
                    " order by zone_name asc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getzone_list = new List<zone_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getzone_list.Add(new zone_list
                    {
                        zone_gid = (dr_datarow["zone_gid"].ToString()),
                        zone_name = (dr_datarow["zone_name"].ToString())
                    });
                }
                objmaster.zone_list = getzone_list;
            }
            dt_datatable.Dispose();
            objmaster.status = true;

            msSQL = " SELECT vertical_gid ,vertical_name FROM ocs_mst_tvertical  where status_log='Y' " +
                       " order by vertical_name asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getvertical_list = new List<vertical_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getvertical_list.Add(new vertical_list
                    {
                        vertical_gid = (dr_datarow["vertical_gid"].ToString()),
                        vertical_name = (dr_datarow["vertical_name"].ToString()),
                    });
                }
                objmaster.vertical_list = getvertical_list;
            }
            dt_datatable.Dispose();
            objmaster.status = true;

            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                   " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                   " where user_status<>'N' order by a.user_firstname asc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_employee = new List<employee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                objmaster.employeelist = dt_datatable.AsEnumerable().Select(row =>
                  new employeelist
                  {
                      employee_gid = row["employee_gid"].ToString(),
                      employee_name = row["employee_name"].ToString()
                  }
                ).ToList();
            }
            dt_datatable.Dispose();
            objmaster.status = true;
        }

        public bool DaPostGroupBusinessHeadUpdate(string employee_gid, mdlbusinesshead values)
        {

            msSQL = "select updated_by, updated_date,employee_gid, employee_name, " +
                    "zone_gid, zone_name, vertical_gid, vertical_name from sys_mst_tgroupbusinesshead where groupbusinesshead_gid ='" + values.groupbusinesshead_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("GBHL");
                    msSQL = " insert into sys_mst_tgroupbusinessheadlog(" +
                              " groupbusinessheadlog_gid," +
                              " groupbusinesshead_gid," +
                              " zone_gid , " +
                              " zone_name , " +
                              " vertical_gid , " +
                              " vertical_name , " +
                              " employee_gid , " +
                              " employee_name , " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.groupbusinesshead_gid + "'," +
                              "'" + objODBCDatareader["zone_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["zone_name"].ToString() + "'," +
                              "'" + objODBCDatareader["vertical_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["vertical_name"].ToString() + "'," +
                              "'" + objODBCDatareader["employee_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["employee_name"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();

            msSQL = "update sys_mst_tgroupbusinesshead set zone_gid='" + values.zone_gid + "'," +
                 " zone_name='" + values.zone_name + "'," +
                 " vertical_gid='" + values.vertical_gid + "'," +
                 " vertical_name='" + values.vertical_name + "'," +
                 " employee_gid='" + values.employee_gid + "'," +
                 " employee_name='" + values.employee_name + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where groupbusinesshead_gid='" + values.groupbusinesshead_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Group Business Head updated successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Group Business Head";
                return false;
            }
        }

        //Cluster Head codes

        public void DaGetClusterslist(region objmaster)
        {
            try
            {
                msSQL = " SELECT cluster_gid ,cluster_name FROM sys_mst_tclustermapping" +
                        " order by cluster_name asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcluster_list = new List<cluster_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcluster_list.Add(new cluster_list
                        {
                            cluster_gid = (dr_datarow["cluster_gid"].ToString()),
                            cluster_name = (dr_datarow["cluster_name"].ToString()),
                        });
                    }
                    objmaster.cluster_list = getcluster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaPostClusterheadAdd(mdlclusterhead values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("CLHD");
            msGetAPICode = objcmnfunctions.GetApiMasterGID("CLUH");
            msSQL = " insert into sys_mst_tclusterhead(" +
                    " clusterhead_gid," +
                    " api_code," +
                    " cluster_gid ," +
                    " cluster_name," +
                    " vertical_gid," +
                    " vertical_name," +
                    " employee_gid, " +
                    " employee_name, " +
                    " program_gid, " +
                    " program_name, " +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                     "'" + msGetAPICode + "'," +
                    "'" + values.cluster_gid + "'," +
                    "'" + values.cluster_name + "'," +
                    "'" + values.vertical_gid + "'," +
                    "'" + values.vertical_name + "'," +
                    "'" + values.employee_gid + "'," +
                    "'" + values.employee_name + "'," +
                    "'" + values.program_gid + "'," +
                    "'" + values.program_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Cluster Head Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }

        }

        public void DaInactiveClusterhead(mdlclusterhead values, string employee_gid)
        {
            msSQL = " update sys_mst_tclusterhead set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where clusterhead_gid='" + values.clusterhead_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            string lsemployeegid = objdbconn.GetExecuteScalar("select employee_gid from sys_mst_tclusterhead where clusterhead_gid = '" + values.clusterhead_gid + "' ");
            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("CHDA");

                msSQL = " insert into sys_mst_tclusterheadinactivelog (" +
                      " clusterheadinactivelog_gid , " +
                      " clusterhead_gid," +
                      " employee_gid, " +
                      " employee_name ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.clusterhead_gid + "'," +
                      " '" + lsemployeegid + "'," +
                      " '" + values.employee_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Cluster Head Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Cluster Head  Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaClusterheadInactiveLogview(string clusterhead_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT clusterhead_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tclusterheadinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where clusterhead_gid ='" + clusterhead_gid + "' order by a.clusterheadinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            clusterhead_gid = (dr_datarow["clusterhead_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetClusterHeadSummary(cluster objmaster)
        {
            try
            {
                msSQL = " SELECT a.clusterhead_gid ,a.api_code,a.employee_name,a.employee_gid, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,vertical_name,cluster_name,program_name," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tclusterhead a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getclusterhead_list = new List<clusterhead_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getclusterhead_list.Add(new clusterhead_list
                        {
                            clusterhead_gid = (dr_datarow["clusterhead_gid"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                            cluster_name = (dr_datarow["cluster_name"].ToString()),
                            employee_gid = (dr_datarow["employee_gid"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            vertical_name = (dr_datarow["vertical_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            program_name = (dr_datarow["program_name"].ToString()),
                            status = (dr_datarow["status"].ToString())
                        });
                    }
                    objmaster.clusterhead_list = getclusterhead_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaGetClusterHeadEdit(string clusterhead_gid, mdlclusterhead objmaster)
        {
            msSQL = " select clusterhead_gid,cluster_gid,cluster_name,vertical_gid,vertical_name,employee_gid, " +
                    " employee_name, status as status,program_gid,program_name from sys_mst_tclusterhead " +
                    " where clusterhead_gid='" + clusterhead_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objmaster.clusterhead_gid = objODBCDatareader["clusterhead_gid"].ToString();
                objmaster.cluster_gid = objODBCDatareader["cluster_gid"].ToString();
                objmaster.cluster_name = objODBCDatareader["cluster_name"].ToString();
                objmaster.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                objmaster.vertical_name = objODBCDatareader["vertical_name"].ToString();
                objmaster.employee_gid = objODBCDatareader["employee_gid"].ToString();
                objmaster.employee_name = objODBCDatareader["employee_name"].ToString();
                objmaster.clusterhead_status = objODBCDatareader["status"].ToString();
                objmaster.program_gid = objODBCDatareader["program_gid"].ToString();
                objmaster.program_name = objODBCDatareader["program_name"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " SELECT cluster_gid ,cluster_name FROM sys_mst_tclustermapping" +
                           " order by cluster_name asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcluster_list = new List<cluster_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcluster_list.Add(new cluster_list
                    {
                        cluster_gid = (dr_datarow["cluster_gid"].ToString()),
                        cluster_name = (dr_datarow["cluster_name"].ToString()),
                    });
                }
                objmaster.cluster_list = getcluster_list;
            }
            dt_datatable.Dispose();

            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
           " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
           " where user_status<>'N' order by a.user_firstname asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getemployee_list = new List<employeelist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getemployee_list.Add(new employeelist
                    {
                        employee_gid = (dr_datarow["employee_gid"].ToString()),
                        employee_name = (dr_datarow["employee_name"].ToString()),
                    });
                }
                objmaster.employeelist = getemployee_list;
            }
            dt_datatable.Dispose();

            msSQL = " SELECT vertical_gid ,vertical_name FROM ocs_mst_tvertical  where status_log='Y'" +
                       " order by vertical_name asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getvertical_list = new List<vertical_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getvertical_list.Add(new vertical_list
                    {
                        vertical_gid = (dr_datarow["vertical_gid"].ToString()),
                        vertical_name = (dr_datarow["vertical_name"].ToString()),
                    });
                }
                objmaster.vertical_list = getvertical_list;
            }
            dt_datatable.Dispose();

        }

        public bool DaPostClusterHeadUpdate(string employee_gid, mdlclusterhead values)
        {


            msSQL = " select updated_by, updated_date,clusterhead_gid,employee_name,employee_gid,cluster_gid,cluster_name, " +
                    " vertical_gid,vertical_name, program_gid, program_name  from sys_mst_tclusterhead where clusterhead_gid ='" + values.clusterhead_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("CHDL");
                    msSQL = " insert into sys_mst_tclusterheadlog(" +
                              " clusterheadlog_gid," +
                              " clusterhead_gid," +
                              " employee_gid, " +
                              " employee_name , " +
                              " vertical_gid," +
                              " vertical_name , " +
                              " cluster_gid," +
                              " cluster_name , " +
                              " program_gid, " +
                              " program_name, " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.clusterhead_gid + "'," +
                              "'" + objODBCDatareader["employee_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["employee_name"].ToString() + "'," +
                              "'" + objODBCDatareader["vertical_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["vertical_name"].ToString() + "'," +
                              "'" + objODBCDatareader["cluster_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["cluster_name"].ToString() + "'," +
                              "'" + objODBCDatareader["program_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["program_name"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = " select a.application_gid, application_no, customer_name, customer_urn, b.approval_status, vertical_gid, vertical_name, " +
                           " baselocation_gid, baselocation_name, cluster_gid, cluster_name, region_gid, region_name, zone_gid, zone_name, " +
                           " relationshipmanager_gid, relationshipmanager_name, drm_gid, drm_name, program_gid, program_name, " +
                           " clustermanager_gid, clustermanager_name, regionalhead_gid, regionalhead_name, zonalhead_gid, zonalhead_name, " +
                           " businesshead_gid, businesshead_name from ocs_mst_tapplication a " +
                           " left join ocs_trn_tapplicationapproval b on a.application_gid = b.application_gid " +
                           " where clustermanager_gid = '" + objODBCDatareader["employee_gid"].ToString() + "' and b.approval_status = 'Pending' and b.hierary_level='2' and " +
                           " a.vertical_gid='" + values.vertical_gid + "' and a.cluster_gid='" + values.cluster_gid + "'" +
                           " and a.program_gid ='" + values.program_gid + "'  group by b.application_gid ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        //msGetDocumentGid = objcmnfunctions.GetMasterGID("CRDO");

                        msSQL = " insert into ocs_mst_tapplicationlog(" +
                         " application_gid," +
                         " application_no, " +
                         " customer_urn," +
                         " customer_name," +
                         " program_gid, " +
                         " program_name, " +
                         " vertical_gid," +
                         " vertical_name, " +
                         " baselocation_gid," +
                         " baselocation_name, " +
                         " cluster_gid," +
                         " cluster_name," +
                         " region_gid," +
                         " region_name, " +
                         " zone_gid," +
                         " zone_name, " +
                         " relationshipmanager_gid," +
                         " relationshipmanager_name," +
                         " drm_gid," +
                         " drm_name, " +
                         " clustermanager_gid," +
                         " clustermanager_name, " +
                         " regionalhead_gid," +
                         " regionalhead_name," +
                         " zonalhead_gid," +
                         " zonalhead_name, " +
                         " businesshead_gid," +
                         " businesshead_name," +
                         " head_change," +
                         " created_by," +
                         " created_date)" +
                         " values(" +
                         "'" + dt["application_gid"].ToString() + "'," +
                         "'" + dt["application_no"].ToString() + "'," +
                         "'" + dt["customer_urn"].ToString() + "'," +
                         "'" + dt["customer_name"].ToString() + "'," +
                         "'" + dt["program_gid"].ToString() + "'," +
                         "'" + dt["program_name"].ToString() + "'," +
                         "'" + dt["vertical_gid"].ToString() + "'," +
                         "'" + dt["vertical_name"].ToString() + "'," +
                         "'" + dt["baselocation_gid"].ToString() + "'," +
                         "'" + dt["baselocation_name"].ToString() + "'," +
                         "'" + dt["cluster_gid"].ToString() + "'," +
                         "'" + dt["cluster_name"].ToString() + "'," +
                         "'" + dt["region_gid"].ToString() + "'," +
                         "'" + dt["region_name"].ToString() + "'," +
                         "'" + dt["zone_gid"].ToString() + "'," +
                         "'" + dt["zone_name"].ToString() + "'," +
                         "'" + dt["relationshipmanager_gid"].ToString() + "'," +
                         "'" + dt["relationshipmanager_name"].ToString() + "'," +
                         "'" + dt["drm_gid"].ToString() + "'," +
                         "'" + dt["drm_name"].ToString() + "'," +
                         "'" + dt["clustermanager_gid"].ToString() + "'," +
                         "'" + dt["clustermanager_name"].ToString() + "'," +
                         "'" + dt["regionalhead_gid"].ToString() + "'," +
                         "'" + dt["regionalhead_name"].ToString() + "'," +
                         "'" + dt["zonalhead_gid"].ToString() + "'," +
                         "'" + dt["zonalhead_name"].ToString() + "'," +
                         "'" + dt["businesshead_gid"].ToString() + "'," +
                         "'" + dt["businesshead_name"].ToString() + "'," +
                         "'CH'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update ocs_mst_tapplication set " +
                                " clustermanager_gid='" + values.employee_gid + "'," +
                                " clustermanager_name='" + values.employee_name + "'" +
                                " where application_gid='" + dt["application_gid"].ToString() + "' and vertical_gid='" + values.vertical_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update ocs_trn_tapplicationapproval set " +
                                " approval_gid='" + values.employee_gid + "'," +
                                " approval_name='" + values.employee_name + "'" +
                                " where application_gid='" + dt["application_gid"].ToString() + "' and hierary_level='2' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();

                msSQL = " select a.application_gid, application_no, customer_name, customer_urn, b.approval_status, vertical_gid, vertical_name, " +
                          " baselocation_gid, baselocation_name, cluster_gid, cluster_name, region_gid, region_name, zone_gid, zone_name, " +
                          " relationshipmanager_gid, relationshipmanager_name, drm_gid, drm_name, program_gid, program_name, " +
                          " clustermanager_gid, clustermanager_name, regionalhead_gid, regionalhead_name, zonalhead_gid, zonalhead_name, " +
                          " businesshead_gid, businesshead_name from agr_mst_tapplication a " +
                          " left join agr_trn_tapplicationapproval b on a.application_gid = b.application_gid " +
                          " where clustermanager_gid = '" + objODBCDatareader["employee_gid"].ToString() + "' and b.approval_status = 'Pending' and b.hierary_level='2' and " +
                          " a.vertical_gid='" + values.vertical_gid + "' and a.cluster_gid='" + values.cluster_gid + "'" +
                          " and a.program_gid ='" + values.program_gid + "'  group by b.application_gid ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        //msGetDocumentGid = objcmnfunctions.GetMasterGID("CRDO");

                        msSQL = " insert into agr_mst_tapplicationlog(" +
                         " application_gid," +
                         " application_no, " +
                         " customer_urn," +
                         " customer_name," +
                         " program_gid, " +
                         " program_name, " +
                         " vertical_gid," +
                         " vertical_name, " +
                         " baselocation_gid," +
                         " baselocation_name, " +
                         " cluster_gid," +
                         " cluster_name," +
                         " region_gid," +
                         " region_name, " +
                         " zone_gid," +
                         " zone_name, " +
                         " relationshipmanager_gid," +
                         " relationshipmanager_name," +
                         " drm_gid," +
                         " drm_name, " +
                         " clustermanager_gid," +
                         " clustermanager_name, " +
                         " regionalhead_gid," +
                         " regionalhead_name," +
                         " zonalhead_gid," +
                         " zonalhead_name, " +
                         " businesshead_gid," +
                         " businesshead_name," +
                         " head_change," +
                         " created_by," +
                         " created_date)" +
                         " values(" +
                         "'" + dt["application_gid"].ToString() + "'," +
                         "'" + dt["application_no"].ToString() + "'," +
                         "'" + dt["customer_urn"].ToString() + "'," +
                         "'" + dt["customer_name"].ToString() + "'," +
                         "'" + dt["program_gid"].ToString() + "'," +
                         "'" + dt["program_name"].ToString() + "'," +
                         "'" + dt["vertical_gid"].ToString() + "'," +
                         "'" + dt["vertical_name"].ToString() + "'," +
                         "'" + dt["baselocation_gid"].ToString() + "'," +
                         "'" + dt["baselocation_name"].ToString() + "'," +
                         "'" + dt["cluster_gid"].ToString() + "'," +
                         "'" + dt["cluster_name"].ToString() + "'," +
                         "'" + dt["region_gid"].ToString() + "'," +
                         "'" + dt["region_name"].ToString() + "'," +
                         "'" + dt["zone_gid"].ToString() + "'," +
                         "'" + dt["zone_name"].ToString() + "'," +
                         "'" + dt["relationshipmanager_gid"].ToString() + "'," +
                         "'" + dt["relationshipmanager_name"].ToString() + "'," +
                         "'" + dt["drm_gid"].ToString() + "'," +
                         "'" + dt["drm_name"].ToString() + "'," +
                         "'" + dt["clustermanager_gid"].ToString() + "'," +
                         "'" + dt["clustermanager_name"].ToString() + "'," +
                         "'" + dt["regionalhead_gid"].ToString() + "'," +
                         "'" + dt["regionalhead_name"].ToString() + "'," +
                         "'" + dt["zonalhead_gid"].ToString() + "'," +
                         "'" + dt["zonalhead_name"].ToString() + "'," +
                         "'" + dt["businesshead_gid"].ToString() + "'," +
                         "'" + dt["businesshead_name"].ToString() + "'," +
                         "'CH'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_mst_tapplication set " +
                                " clustermanager_gid='" + values.employee_gid + "'," +
                                " clustermanager_name='" + values.employee_name + "'" +
                                " where application_gid='" + dt["application_gid"].ToString() + "' and vertical_gid='" + values.vertical_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_trn_tapplicationapproval set " +
                                " approval_gid='" + values.employee_gid + "'," +
                                " approval_name='" + values.employee_name + "'" +
                                " where application_gid='" + dt["application_gid"].ToString() + "' and hierary_level='2' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();

                msSQL = " select a.application_gid, application_no, customer_name, customer_urn, b.approval_status, vertical_gid, vertical_name, " +
                         " baselocation_gid, baselocation_name, cluster_gid, cluster_name, region_gid, region_name, zone_gid, zone_name, " +
                         " relationshipmanager_gid, relationshipmanager_name, drm_gid, drm_name, program_gid, program_name, " +
                         " clustermanager_gid, clustermanager_name, regionalhead_gid, regionalhead_name, zonalhead_gid, zonalhead_name, " +
                         " businesshead_gid, businesshead_name from agr_mst_tsuprapplication a " +
                         " left join agr_trn_tsuprapplicationapproval b on a.application_gid = b.application_gid " +
                         " where clustermanager_gid = '" + objODBCDatareader["employee_gid"].ToString() + "' and b.approval_status = 'Pending' and b.hierary_level='2' and " +
                         " a.vertical_gid='" + values.vertical_gid + "' and a.cluster_gid='" + values.cluster_gid + "'" +
                         " and a.program_gid ='" + values.program_gid + "'  group by b.application_gid ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        //msGetDocumentGid = objcmnfunctions.GetMasterGID("CRDO");

                        msSQL = " insert into agr_mst_tsuprapplicationlog(" +
                         " application_gid," +
                         " application_no, " +
                         " customer_urn," +
                         " customer_name," +
                         " program_gid, " +
                         " program_name, " +
                         " vertical_gid," +
                         " vertical_name, " +
                         " baselocation_gid," +
                         " baselocation_name, " +
                         " cluster_gid," +
                         " cluster_name," +
                         " region_gid," +
                         " region_name, " +
                         " zone_gid," +
                         " zone_name, " +
                         " relationshipmanager_gid," +
                         " relationshipmanager_name," +
                         " drm_gid," +
                         " drm_name, " +
                         " clustermanager_gid," +
                         " clustermanager_name, " +
                         " regionalhead_gid," +
                         " regionalhead_name," +
                         " zonalhead_gid," +
                         " zonalhead_name, " +
                         " businesshead_gid," +
                         " businesshead_name," +
                         " head_change," +
                         " created_by," +
                         " created_date)" +
                         " values(" +
                         "'" + dt["application_gid"].ToString() + "'," +
                         "'" + dt["application_no"].ToString() + "'," +
                         "'" + dt["customer_urn"].ToString() + "'," +
                         "'" + dt["customer_name"].ToString() + "'," +
                         "'" + dt["program_gid"].ToString() + "'," +
                         "'" + dt["program_name"].ToString() + "'," +
                         "'" + dt["vertical_gid"].ToString() + "'," +
                         "'" + dt["vertical_name"].ToString() + "'," +
                         "'" + dt["baselocation_gid"].ToString() + "'," +
                         "'" + dt["baselocation_name"].ToString() + "'," +
                         "'" + dt["cluster_gid"].ToString() + "'," +
                         "'" + dt["cluster_name"].ToString() + "'," +
                         "'" + dt["region_gid"].ToString() + "'," +
                         "'" + dt["region_name"].ToString() + "'," +
                         "'" + dt["zone_gid"].ToString() + "'," +
                         "'" + dt["zone_name"].ToString() + "'," +
                         "'" + dt["relationshipmanager_gid"].ToString() + "'," +
                         "'" + dt["relationshipmanager_name"].ToString() + "'," +
                         "'" + dt["drm_gid"].ToString() + "'," +
                         "'" + dt["drm_name"].ToString() + "'," +
                         "'" + dt["clustermanager_gid"].ToString() + "'," +
                         "'" + dt["clustermanager_name"].ToString() + "'," +
                         "'" + dt["regionalhead_gid"].ToString() + "'," +
                         "'" + dt["regionalhead_name"].ToString() + "'," +
                         "'" + dt["zonalhead_gid"].ToString() + "'," +
                         "'" + dt["zonalhead_name"].ToString() + "'," +
                         "'" + dt["businesshead_gid"].ToString() + "'," +
                         "'" + dt["businesshead_name"].ToString() + "'," +
                         "'CH'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_mst_tsuprapplication set " +
                                " clustermanager_gid='" + values.employee_gid + "'," +
                                " clustermanager_name='" + values.employee_name + "'" +
                                " where application_gid='" + dt["application_gid"].ToString() + "' and vertical_gid='" + values.vertical_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_trn_tsuprapplicationapproval set " +
                                " approval_gid='" + values.employee_gid + "'," +
                                " approval_name='" + values.employee_name + "'" +
                                " where application_gid='" + dt["application_gid"].ToString() + "' and hierary_level='2' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();


            }
            objODBCDatareader.Close();

            msSQL = "update sys_mst_tclusterhead set " +
                " employee_gid='" + values.employee_gid + "'," +
                " employee_name='" + values.employee_name + "'," +
                " vertical_gid='" + values.vertical_gid + "'," +
                " vertical_name='" + values.vertical_name + "'," +
                " cluster_gid='" + values.cluster_gid + "'," +
                " cluster_name='" + values.cluster_name + "'," +
                " program_gid ='" + values.program_gid + "'," +
                " program_name ='" + values.program_name + "'," +
                " updated_by='" + employee_gid + "'," +
                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " where clusterhead_gid='" + values.clusterhead_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Cluster Head updated successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Cluster Head";
                return false;
            }
        }


        //Zonal Head codes

        public void DaGetZonalslist(zone objmaster)
        {
            try
            {
                msSQL = " SELECT zone_gid ,zone_name FROM sys_mst_tzonemapping" +
                        " order by zone_name asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getzone_list = new List<zone_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getzone_list.Add(new zone_list
                        {
                            zone_gid = (dr_datarow["zone_gid"].ToString()),
                            zone_name = (dr_datarow["zone_name"].ToString()),
                        });
                    }
                    objmaster.zone_list = getzone_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaPostZonalheadAdd(mdlzonalhead values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("ZNHD");
            msGetAPICode = objcmnfunctions.GetApiMasterGID("ZONH");
            msSQL = " insert into sys_mst_tZonalhead(" +
                    " zonalhead_gid," +
                    " api_code," +
                    " zonal_gid ," +
                    " zonal_name," +
                    " vertical_gid," +
                    " vertical_name," +
                    " program_gid, " +
                    " program_name, " +
                    " employee_gid, " +
                    " employee_name, " +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                     "'" + msGetAPICode + "'," +
                    "'" + values.zonal_gid + "'," +
                    "'" + values.zonal_name + "'," +
                    "'" + values.vertical_gid + "'," +
                    "'" + values.vertical_name + "'," +
                    "'" + values.program_gid + "'," +
                    "'" + values.program_name + "'," +
                    "'" + values.employee_gid + "'," +
                    "'" + values.employee_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Zonal Head Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }

        }

        public void DaInactiveZonalhead(mdlzonalhead values, string employee_gid)
        {
            msSQL = " update sys_mst_tzonalhead set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where zonalhead_gid='" + values.zonalhead_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            string lsemployeegid = objdbconn.GetExecuteScalar("select employee_gid from sys_mst_tzonalhead where zonalhead_gid = '" + values.zonalhead_gid + "' ");
            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("ZNDA");

                msSQL = " insert into sys_mst_tzonalheadinactivelog (" +
                      " zonalheadinactivelog_gid , " +
                      " zonalhead_gid," +
                      " employee_gid, " +
                      " employee_name ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.zonalhead_gid + "'," +
                      " '" + lsemployeegid + "'," +
                      " '" + values.employee_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Zonal Head Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Zonal Head  Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaZonalheadInactiveLogview(string zonalhead_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT zonalhead_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tzonalheadinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where zonalhead_gid ='" + zonalhead_gid + "' order by a.zonalheadinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            zonalhead_gid = (dr_datarow["zonalhead_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetZonalHeadSummary(zone objmaster)
        {
            try
            {
                msSQL = " SELECT a.zonalhead_gid ,a.api_code,a.employee_name,a.employee_gid, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,vertical_name,zonal_name,program_name, " +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tzonalhead a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getzonalhead_list = new List<zonalhead_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getzonalhead_list.Add(new zonalhead_list
                        {
                            zonalhead_gid = (dr_datarow["zonalhead_gid"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                            zonal_name = (dr_datarow["zonal_name"].ToString()),
                            employee_gid = (dr_datarow["employee_gid"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            vertical_name = (dr_datarow["vertical_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            program_name = (dr_datarow["program_name"].ToString())
                        });
                    }
                    objmaster.zonalhead_list = getzonalhead_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaGetZonalHeadEdit(string zonalhead_gid, mdlzonalhead objmaster)
        {
            msSQL = " select zonalhead_gid,zonal_gid,zonal_name,vertical_gid,vertical_name,employee_gid,employee_name, " +
                    " status as status,program_gid, program_name from sys_mst_tzonalhead " +
                    " where zonalhead_gid='" + zonalhead_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objmaster.zonalhead_gid = objODBCDatareader["zonalhead_gid"].ToString();
                objmaster.zonal_gid = objODBCDatareader["zonal_gid"].ToString();
                objmaster.zonal_name = objODBCDatareader["zonal_name"].ToString();
                objmaster.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                objmaster.vertical_name = objODBCDatareader["vertical_name"].ToString();
                objmaster.employee_gid = objODBCDatareader["employee_gid"].ToString();
                objmaster.employee_name = objODBCDatareader["employee_name"].ToString();
                objmaster.zonalhead_status = objODBCDatareader["status"].ToString();
                objmaster.program_gid = objODBCDatareader["program_gid"].ToString();
                objmaster.program_name = objODBCDatareader["program_name"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " SELECT zone_gid ,zone_name FROM sys_mst_tzonemapping" +
                       " order by zone_name asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getzone_list = new List<zone_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getzone_list.Add(new zone_list
                    {
                        zone_gid = (dr_datarow["zone_gid"].ToString()),
                        zone_name = (dr_datarow["zone_name"].ToString()),
                    });
                }
                objmaster.zone_list = getzone_list;
            }
            dt_datatable.Dispose();

            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
           " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
           " where user_status<>'N' order by a.user_firstname asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getemployee_list = new List<employeelist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getemployee_list.Add(new employeelist
                    {
                        employee_gid = (dr_datarow["employee_gid"].ToString()),
                        employee_name = (dr_datarow["employee_name"].ToString()),
                    });
                }
                objmaster.employeelist = getemployee_list;
            }
            dt_datatable.Dispose();

            msSQL = " SELECT vertical_gid ,vertical_name FROM ocs_mst_tvertical  where status_log='Y' " +
                       " order by vertical_name asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getvertical_list = new List<vertical_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getvertical_list.Add(new vertical_list
                    {
                        vertical_gid = (dr_datarow["vertical_gid"].ToString()),
                        vertical_name = (dr_datarow["vertical_name"].ToString()),
                    });
                }
                objmaster.vertical_list = getvertical_list;
            }
            dt_datatable.Dispose();

        }

        public bool DaPostZonalHeadUpdate(string employee_gid, mdlzonalhead values)
        {


            msSQL = "select updated_by, updated_date,zonal_name,employee_name,employee_gid,zonal_gid,vertical_gid,vertical_name, " +
                    " program_gid, program_name from sys_mst_tzonalhead where zonalhead_gid ='" + values.zonalhead_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("ZNDL");
                    msSQL = " insert into sys_mst_tzonalheadlog(" +
                              " zonalheadlog_gid," +
                              " zonalhead_gid," +
                              " employee_gid, " +
                              " employee_name , " +
                              " vertical_gid," +
                              " vertical_name , " +
                              " program_gid, " +
                              " program_name, " +
                              " zonal_gid," +
                              " zonal_name , " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.zonalhead_gid + "'," +
                              "'" + objODBCDatareader["employee_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["employee_name"].ToString() + "'," +
                              "'" + objODBCDatareader["vertical_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["vertical_name"].ToString() + "'," +
                              "'" + objODBCDatareader["program_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["program_name"].ToString() + "'," +
                              "'" + objODBCDatareader["zonal_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["zonal_name"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = " select a.application_gid, application_no, customer_name, customer_urn, b.approval_status, vertical_gid, vertical_name, " +
                           " baselocation_gid, baselocation_name, cluster_gid, cluster_name, region_gid, region_name, zone_gid, zone_name, " +
                           " relationshipmanager_gid, relationshipmanager_name, drm_gid, drm_name, program_gid, program_name, " +
                           " clustermanager_gid, clustermanager_name, regionalhead_gid, regionalhead_name, zonalhead_gid, zonalhead_name, " +
                           " businesshead_gid, businesshead_name from ocs_mst_tapplication a " +
                           " left join ocs_trn_tapplicationapproval b on a.application_gid = b.application_gid " +
                           " where zonalhead_gid = '" + objODBCDatareader["employee_gid"].ToString() + "' and b.approval_status = 'Pending' and b.hierary_level='4' and " +
                           " a.vertical_gid='" + values.vertical_gid + "' and a.zone_gid='" + values.zonal_gid + "' and a.program_gid = '" + values.program_gid + "'  group by b.application_gid ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        //msGetDocumentGid = objcmnfunctions.GetMasterGID("CRDO");

                        msSQL = " insert into ocs_mst_tapplicationlog(" +
                         " application_gid," +
                         " application_no, " +
                         " customer_urn," +
                         " customer_name," +
                         " vertical_gid," +
                         " vertical_name, " +
                         " program_gid, " +
                         " program_name, " +
                         " baselocation_gid," +
                         " baselocation_name, " +
                         " cluster_gid," +
                         " cluster_name," +
                         " region_gid," +
                         " region_name, " +
                         " zone_gid," +
                         " zone_name, " +
                         " relationshipmanager_gid," +
                         " relationshipmanager_name," +
                         " drm_gid," +
                         " drm_name, " +
                         " clustermanager_gid," +
                         " clustermanager_name, " +
                         " regionalhead_gid," +
                         " regionalhead_name," +
                         " zonalhead_gid," +
                         " zonalhead_name, " +
                         " businesshead_gid," +
                         " businesshead_name," +
                         " head_change," +
                         " created_by," +
                         " created_date)" +
                         " values(" +
                         "'" + dt["application_gid"].ToString() + "'," +
                         "'" + dt["application_no"].ToString() + "'," +
                         "'" + dt["customer_urn"].ToString() + "'," +
                         "'" + dt["customer_name"].ToString() + "'," +
                         "'" + dt["vertical_gid"].ToString() + "'," +
                         "'" + dt["vertical_name"].ToString() + "'," +
                         "'" + dt["program_gid"].ToString() + "'," +
                         "'" + dt["program_name"].ToString() + "'," +
                         "'" + dt["baselocation_gid"].ToString() + "'," +
                         "'" + dt["baselocation_name"].ToString() + "'," +
                         "'" + dt["cluster_gid"].ToString() + "'," +
                         "'" + dt["cluster_name"].ToString() + "'," +
                         "'" + dt["region_gid"].ToString() + "'," +
                         "'" + dt["region_name"].ToString() + "'," +
                         "'" + dt["zone_gid"].ToString() + "'," +
                         "'" + dt["zone_name"].ToString() + "'," +
                         "'" + dt["relationshipmanager_gid"].ToString() + "'," +
                         "'" + dt["relationshipmanager_name"].ToString() + "'," +
                         "'" + dt["drm_gid"].ToString() + "'," +
                         "'" + dt["drm_name"].ToString() + "'," +
                         "'" + dt["clustermanager_gid"].ToString() + "'," +
                         "'" + dt["clustermanager_name"].ToString() + "'," +
                         "'" + dt["regionalhead_gid"].ToString() + "'," +
                         "'" + dt["regionalhead_name"].ToString() + "'," +
                         "'" + dt["zonalhead_gid"].ToString() + "'," +
                         "'" + dt["zonalhead_name"].ToString() + "'," +
                         "'" + dt["businesshead_gid"].ToString() + "'," +
                         "'" + dt["businesshead_name"].ToString() + "'," +
                         "'ZH'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update ocs_mst_tapplication set " +
                                " zonalhead_gid='" + values.employee_gid + "'," +
                                " zonalhead_name='" + values.employee_name + "'" +
                                " where application_gid='" + dt["application_gid"].ToString() + "' and vertical_gid='" + values.vertical_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update ocs_trn_tapplicationapproval set " +
                                " approval_gid='" + values.employee_gid + "'," +
                                " approval_name='" + values.employee_name + "'" +
                                " where application_gid='" + dt["application_gid"].ToString() + "' and hierary_level='4' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();

                msSQL = " select a.application_gid, application_no, customer_name, customer_urn, b.approval_status, vertical_gid, vertical_name, " +
                                           " baselocation_gid, baselocation_name, cluster_gid, cluster_name, region_gid, region_name, zone_gid, zone_name, " +
                                           " relationshipmanager_gid, relationshipmanager_name, drm_gid, drm_name, program_gid, program_name, " +
                                           " clustermanager_gid, clustermanager_name, regionalhead_gid, regionalhead_name, zonalhead_gid, zonalhead_name, " +
                                           " businesshead_gid, businesshead_name from agr_mst_tapplication a " +
                                           " left join agr_trn_tapplicationapproval b on a.application_gid = b.application_gid " +
                                           " where zonalhead_gid = '" + objODBCDatareader["employee_gid"].ToString() + "' and b.approval_status = 'Pending' and b.hierary_level='4' and " +
                                           " a.vertical_gid='" + values.vertical_gid + "' and a.zone_gid='" + values.zonal_gid + "' and a.program_gid = '" + values.program_gid + "'  group by b.application_gid ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        //msGetDocumentGid = objcmnfunctions.GetMasterGID("CRDO");

                        msSQL = " insert into agr_mst_tapplicationlog(" +
                         " application_gid," +
                         " application_no, " +
                         " customer_urn," +
                         " customer_name," +
                         " vertical_gid," +
                         " vertical_name, " +
                         " program_gid, " +
                         " program_name, " +
                         " baselocation_gid," +
                         " baselocation_name, " +
                         " cluster_gid," +
                         " cluster_name," +
                         " region_gid," +
                         " region_name, " +
                         " zone_gid," +
                         " zone_name, " +
                         " relationshipmanager_gid," +
                         " relationshipmanager_name," +
                         " drm_gid," +
                         " drm_name, " +
                         " clustermanager_gid," +
                         " clustermanager_name, " +
                         " regionalhead_gid," +
                         " regionalhead_name," +
                         " zonalhead_gid," +
                         " zonalhead_name, " +
                         " businesshead_gid," +
                         " businesshead_name," +
                         " head_change," +
                         " created_by," +
                         " created_date)" +
                         " values(" +
                         "'" + dt["application_gid"].ToString() + "'," +
                         "'" + dt["application_no"].ToString() + "'," +
                         "'" + dt["customer_urn"].ToString() + "'," +
                         "'" + dt["customer_name"].ToString() + "'," +
                         "'" + dt["vertical_gid"].ToString() + "'," +
                         "'" + dt["vertical_name"].ToString() + "'," +
                         "'" + dt["program_gid"].ToString() + "'," +
                         "'" + dt["program_name"].ToString() + "'," +
                         "'" + dt["baselocation_gid"].ToString() + "'," +
                         "'" + dt["baselocation_name"].ToString() + "'," +
                         "'" + dt["cluster_gid"].ToString() + "'," +
                         "'" + dt["cluster_name"].ToString() + "'," +
                         "'" + dt["region_gid"].ToString() + "'," +
                         "'" + dt["region_name"].ToString() + "'," +
                         "'" + dt["zone_gid"].ToString() + "'," +
                         "'" + dt["zone_name"].ToString() + "'," +
                         "'" + dt["relationshipmanager_gid"].ToString() + "'," +
                         "'" + dt["relationshipmanager_name"].ToString() + "'," +
                         "'" + dt["drm_gid"].ToString() + "'," +
                         "'" + dt["drm_name"].ToString() + "'," +
                         "'" + dt["clustermanager_gid"].ToString() + "'," +
                         "'" + dt["clustermanager_name"].ToString() + "'," +
                         "'" + dt["regionalhead_gid"].ToString() + "'," +
                         "'" + dt["regionalhead_name"].ToString() + "'," +
                         "'" + dt["zonalhead_gid"].ToString() + "'," +
                         "'" + dt["zonalhead_name"].ToString() + "'," +
                         "'" + dt["businesshead_gid"].ToString() + "'," +
                         "'" + dt["businesshead_name"].ToString() + "'," +
                         "'ZH'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_mst_tapplication set " +
                                " zonalhead_gid='" + values.employee_gid + "'," +
                                " zonalhead_name='" + values.employee_name + "'" +
                                " where application_gid='" + dt["application_gid"].ToString() + "' and vertical_gid='" + values.vertical_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_trn_tapplicationapproval set " +
                                " approval_gid='" + values.employee_gid + "'," +
                                " approval_name='" + values.employee_name + "'" +
                                " where application_gid='" + dt["application_gid"].ToString() + "' and hierary_level='4' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();



                msSQL = " select a.application_gid, application_no, customer_name, customer_urn, b.approval_status, vertical_gid, vertical_name, " +
                                           " baselocation_gid, baselocation_name, cluster_gid, cluster_name, region_gid, region_name, zone_gid, zone_name, " +
                                           " relationshipmanager_gid, relationshipmanager_name, drm_gid, drm_name, program_gid, program_name, " +
                                           " clustermanager_gid, clustermanager_name, regionalhead_gid, regionalhead_name, zonalhead_gid, zonalhead_name, " +
                                           " businesshead_gid, businesshead_name from agr_mst_tsuprapplication a " +
                                           " left join agr_trn_tsuprapplicationapproval b on a.application_gid = b.application_gid " +
                                           " where zonalhead_gid = '" + objODBCDatareader["employee_gid"].ToString() + "' and b.approval_status = 'Pending' and b.hierary_level='4' and " +
                                           " a.vertical_gid='" + values.vertical_gid + "' and a.zone_gid='" + values.zonal_gid + "' and a.program_gid = '" + values.program_gid + "'  group by b.application_gid ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        //msGetDocumentGid = objcmnfunctions.GetMasterGID("CRDO");

                        msSQL = " insert into agr_mst_tsuprapplicationlog(" +
                         " application_gid," +
                         " application_no, " +
                         " customer_urn," +
                         " customer_name," +
                         " vertical_gid," +
                         " vertical_name, " +
                         " program_gid, " +
                         " program_name, " +
                         " baselocation_gid," +
                         " baselocation_name, " +
                         " cluster_gid," +
                         " cluster_name," +
                         " region_gid," +
                         " region_name, " +
                         " zone_gid," +
                         " zone_name, " +
                         " relationshipmanager_gid," +
                         " relationshipmanager_name," +
                         " drm_gid," +
                         " drm_name, " +
                         " clustermanager_gid," +
                         " clustermanager_name, " +
                         " regionalhead_gid," +
                         " regionalhead_name," +
                         " zonalhead_gid," +
                         " zonalhead_name, " +
                         " businesshead_gid," +
                         " businesshead_name," +
                         " head_change," +
                         " created_by," +
                         " created_date)" +
                         " values(" +
                         "'" + dt["application_gid"].ToString() + "'," +
                         "'" + dt["application_no"].ToString() + "'," +
                         "'" + dt["customer_urn"].ToString() + "'," +
                         "'" + dt["customer_name"].ToString() + "'," +
                         "'" + dt["vertical_gid"].ToString() + "'," +
                         "'" + dt["vertical_name"].ToString() + "'," +
                         "'" + dt["program_gid"].ToString() + "'," +
                         "'" + dt["program_name"].ToString() + "'," +
                         "'" + dt["baselocation_gid"].ToString() + "'," +
                         "'" + dt["baselocation_name"].ToString() + "'," +
                         "'" + dt["cluster_gid"].ToString() + "'," +
                         "'" + dt["cluster_name"].ToString() + "'," +
                         "'" + dt["region_gid"].ToString() + "'," +
                         "'" + dt["region_name"].ToString() + "'," +
                         "'" + dt["zone_gid"].ToString() + "'," +
                         "'" + dt["zone_name"].ToString() + "'," +
                         "'" + dt["relationshipmanager_gid"].ToString() + "'," +
                         "'" + dt["relationshipmanager_name"].ToString() + "'," +
                         "'" + dt["drm_gid"].ToString() + "'," +
                         "'" + dt["drm_name"].ToString() + "'," +
                         "'" + dt["clustermanager_gid"].ToString() + "'," +
                         "'" + dt["clustermanager_name"].ToString() + "'," +
                         "'" + dt["regionalhead_gid"].ToString() + "'," +
                         "'" + dt["regionalhead_name"].ToString() + "'," +
                         "'" + dt["zonalhead_gid"].ToString() + "'," +
                         "'" + dt["zonalhead_name"].ToString() + "'," +
                         "'" + dt["businesshead_gid"].ToString() + "'," +
                         "'" + dt["businesshead_name"].ToString() + "'," +
                         "'ZH'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_mst_tsuprapplication set " +
                                " zonalhead_gid='" + values.employee_gid + "'," +
                                " zonalhead_name='" + values.employee_name + "'" +
                                " where application_gid='" + dt["application_gid"].ToString() + "' and vertical_gid='" + values.vertical_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_trn_tsuprapplicationapproval set " +
                                " approval_gid='" + values.employee_gid + "'," +
                                " approval_name='" + values.employee_name + "'" +
                                " where application_gid='" + dt["application_gid"].ToString() + "' and hierary_level='4' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();

            }
            objODBCDatareader.Close();

            msSQL = "update sys_mst_tzonalhead set " +
                " employee_gid='" + values.employee_gid + "'," +
                " employee_name='" + values.employee_name + "'," +
                " vertical_gid='" + values.vertical_gid + "'," +
                " vertical_name='" + values.vertical_name + "'," +
                " program_gid='" + values.program_gid + "'," +
                " program_name='" + values.program_name + "'," +
                " zonal_gid='" + values.zonal_gid + "'," +
                " zonal_name='" + values.zonal_name + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where zonalhead_gid='" + values.zonalhead_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Zonal Head updated successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Zonal Head";
                return false;
            }
        }

        public void DaGetVerticalProgramList(string vertical_gid, string lstype, string lstypegid, verticalprogram_list values)
        {

            msSQL = " select a.program_gid,program from ocs_mst_tprogram2vertical a " +
                    " left join ocs_mst_tprogram b on a.program_gid = b.program_gid " +
                    " where a.vertical_gid = '" + vertical_gid + "' and b.status = 'Y' and a.status = 'Y'";
            if (lstype == "cluster")
            {
                msSQL += " and a.program_gid not in (select program_gid from sys_mst_tclusterhead " +
                         " where vertical_gid = '" + vertical_gid + "' and cluster_gid='" + lstypegid + "' and status = 'Y' and program_gid is not null )";
            }
            else if (lstype == "region")
            {
                msSQL += " and a.program_gid not in (select program_gid from sys_mst_tregionhead " +
                        " where vertical_gid = '" + vertical_gid + "' and region_gid ='" + lstypegid + "' and status = 'Y' and program_gid is not null )";
            }
            else if (lstype == "business")
            {
                msSQL += " and a.program_gid not in (select program_gid from sys_mst_tbusinesshead " +
                        " where vertical_gid = '" + vertical_gid + "' and zone_gid ='" + lstypegid + "' and status = 'Y' and program_gid is not null )";
            }
            else if (lstype == "zonal")
            {
                msSQL += " and a.program_gid not in (select program_gid from sys_mst_tzonalhead " +
                        " where vertical_gid = '" + vertical_gid + "' and zonal_gid ='" + lstypegid + "' and status = 'Y' and program_gid is not null )";
            }
            else
            {
            }
            msSQL += " order by program asc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getprogram_list = new List<program_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getprogram_list.Add(new program_list
                    {
                        program_gid = (dr_datarow["program_gid"].ToString()),
                        program_name = (dr_datarow["program"].ToString()),
                    });
                }
                values.program_list = getprogram_list;
            }
            dt_datatable.Dispose();
        }


        public void DaGetEditVerticalProgramList(string vertical_gid, string lstype, string lstypegid, string lsmaster_gid, verticalprogram_list values)
        {

            msSQL = " select a.program_gid,program from ocs_mst_tprogram2vertical a " +
                    " left join ocs_mst_tprogram b on a.program_gid = b.program_gid " +
                    " where a.vertical_gid = '" + vertical_gid + "' and b.status = 'Y' and a.status = 'Y'";
            if (lstype == "cluster")
            {
                msSQL += " and a.program_gid not in (select program_gid from sys_mst_tclusterhead " +
                         " where vertical_gid = '" + vertical_gid + "' and cluster_gid='" + lstypegid + "' " +
                         " and clusterhead_gid != '" + lsmaster_gid + "' and status = 'Y' and program_gid is not null )";
            }
            else if (lstype == "region")
            {
                msSQL += " and a.program_gid not in (select program_gid from sys_mst_tregionhead " +
                        " where vertical_gid = '" + vertical_gid + "' and region_gid ='" + lstypegid + "' " +
                        " and regionhead_gid != '" + lsmaster_gid + "' and status = 'Y' and program_gid is not null )";
            }
            else if (lstype == "business")
            {
                msSQL += " and a.program_gid not in (select program_gid from sys_mst_tbusinesshead " +
                         " where vertical_gid = '" + vertical_gid + "' and zone_gid ='" + lstypegid + "' " +
                         " and businesshead_gid!= '" + lsmaster_gid + "'  and status = 'Y' and program_gid is not null )";
            }
            else if (lstype == "zonal")
            {
                msSQL += " and a.program_gid not in (select program_gid from sys_mst_tzonalhead " +
                        " where vertical_gid = '" + vertical_gid + "' and zonal_gid ='" + lstypegid + "' " +
                        " and zonalhead_gid!= '" + lsmaster_gid + "' and status = 'Y' and program_gid is not null )";
            }
            else
            {
            }
            msSQL += " order by program asc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getprogram_list = new List<program_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getprogram_list.Add(new program_list
                    {
                        program_gid = (dr_datarow["program_gid"].ToString()),
                        program_name = (dr_datarow["program"].ToString()),
                    });
                }
                values.program_list = getprogram_list;
            }
            dt_datatable.Dispose();
        }

        //Product Head codes



        public void DaPostProductheadAdd(mdlproducthead values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("PRHD");
            msGetAPICode = objcmnfunctions.GetApiMasterGID("PROH");
            msSQL = " insert into sys_mst_tProducthead(" +
                    " producthead_gid," +
                    " api_code," +
                    " product_gid ," +
                    " product_name," +
                    " employee_gid, " +
                    " employee_name, " +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                     "'" + msGetAPICode + "'," +
                    "'" + values.product_gid + "'," +
                    "'" + values.product_name + "'," +
                    "'" + values.employee_gid + "'," +
                    "'" + values.employee_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Product Head Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }

        }

        public void DaInactiveProducthead(mdlproducthead values, string employee_gid)
        {
            msSQL = " update sys_mst_tproducthead set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where producthead_gid='" + values.producthead_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            string lsemployeegid = objdbconn.GetExecuteScalar("select employee_gid from sys_mst_tproducthead where producthead_gid = '" + values.producthead_gid + "' ");
            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("PRDA");

                msSQL = " insert into sys_mst_tproductheadinactivelog (" +
                      " productheadinactivelog_gid , " +
                      " producthead_gid," +
                      " employee_gid, " +
                      " employee_name ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.producthead_gid + "'," +
                      " '" + lsemployeegid + "'," +
                      " '" + values.employee_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Product Head Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Product Head  Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaProductheadInactiveLogview(string producthead_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT producthead_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tproductheadinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where producthead_gid ='" + producthead_gid + "' order by a.productheadinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            producthead_gid = (dr_datarow["producthead_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetProductHeadSummary(zone objmaster)
        {
            try
            {
                msSQL = " SELECT a.producthead_gid ,a.api_code,a.employee_name,a.employee_gid, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,product_name," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tproducthead a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getproducthead_list = new List<producthead_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getproducthead_list.Add(new producthead_list
                        {
                            producthead_gid = (dr_datarow["producthead_gid"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                            product_name = (dr_datarow["product_name"].ToString()),
                            employee_gid = (dr_datarow["employee_gid"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            status = (dr_datarow["status"].ToString())
                        });
                    }
                    objmaster.producthead_list = getproducthead_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaGetProductHeadEdit(string producthead_gid, mdlproducthead objmaster)
        {
            msSQL = " select producthead_gid,product_name,product_gid,employee_gid,employee_name, status as status from sys_mst_tproducthead " +
                    " where producthead_gid='" + producthead_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objmaster.producthead_gid = objODBCDatareader["producthead_gid"].ToString();
                objmaster.product_gid = objODBCDatareader["product_gid"].ToString();
                objmaster.product_name = objODBCDatareader["product_name"].ToString();
                objmaster.employee_gid = objODBCDatareader["employee_gid"].ToString();
                objmaster.employee_name = objODBCDatareader["employee_name"].ToString();
                objmaster.producthead_status = objODBCDatareader["status"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " SELECT zone_gid ,zone_name FROM sys_mst_tzonemapping" +
                       " order by zone_name asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getzone_list = new List<zone_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getzone_list.Add(new zone_list
                    {
                        zone_gid = (dr_datarow["zone_gid"].ToString()),
                        zone_name = (dr_datarow["zone_name"].ToString()),
                    });
                }
                objmaster.zone_list = getzone_list;
            }
            dt_datatable.Dispose();

            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
           " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
           " where user_status<>'N' order by a.user_firstname asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getemployee_list = new List<employeelist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getemployee_list.Add(new employeelist
                    {
                        employee_gid = (dr_datarow["employee_gid"].ToString()),
                        employee_name = (dr_datarow["employee_name"].ToString()),
                    });
                }
                objmaster.employeelist = getemployee_list;
            }
            dt_datatable.Dispose();



        }

        public bool DaPostProductHeadUpdate(string employee_gid, mdlproducthead values)
        {


            msSQL = "select updated_by, updated_date,product_name,employee_name,employee_gid,product_gid from sys_mst_tproducthead where producthead_gid ='" + values.producthead_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("PRDL");
                    msSQL = " insert into sys_mst_tproductheadlog(" +
                              " productheadlog_gid," +
                              " producthead_gid," +
                              " employee_gid, " +
                              " employee_name , " +
                              " product_gid," +
                              " product_name , " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.producthead_gid + "'," +
                              "'" + objODBCDatareader["employee_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["employee_name"].ToString() + "'," +
                              "'" + objODBCDatareader["product_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["product_name"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();

            msSQL = "update sys_mst_tproducthead set " +
                " employee_gid='" + values.employee_gid + "'," +
                " employee_name='" + values.employee_name + "'," +
                " product_gid='" + values.product_gid + "'," +
                " product_name='" + values.product_name + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where producthead_gid='" + values.producthead_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Product Head updated successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Product Head";
                return false;
            }
        }

        //Task

        public void DaPostTaskAdd(MdlTask values, string employee_gid)
        {
            string lslms_code, lsbureau_code, lstat, lstask_description, lstask_name = "";

            if (values.lms_code == null || values.lms_code == "")
            {
                lslms_code = "";
            }
            else
            {
                lslms_code = values.lms_code.Replace("'", "\\'");
            }
            if (values.bureau_code == null || values.bureau_code == "")
            {
                lsbureau_code = "";
            }
            else
            {
                lsbureau_code = values.bureau_code.Replace("'", "\\'");
            }
            if (values.tat == null || values.tat == "")
            {
                lstat = "";
            }
            else
            {
                lstat = values.tat.Replace("'", "\\'");
            }
            if (values.task_description == null || values.task_description == "")
            {
                lstask_description = "";
            }
            else
            {
                lstask_description = values.task_description.Replace("'", "\\'");
            }
            if (values.task_name == null || values.task_name == "")
            {
                lstask_name = "";
            }
            else
            {
                lstask_name = values.task_name.Replace("'", "\\'");
            }

            string bureau_code, lms_code, task_name, task_description, tat;
            //bureau_code = calendargroup_name = lms_code = "";

            msSQL = " SELECT task_name, lms_code,bureau_code,tat, task_description FROM sys_mst_ttask ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            //var getSegment = new List<CalendarGroupComparison_List>();

            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    //getSegment.Add(new EncoreProductComparison_List
                    //{

                    lms_code = (dr_datarow["lms_code"].ToString());
                    bureau_code = (dr_datarow["bureau_code"].ToString());
                    task_name = (dr_datarow["task_name"].ToString());
                    tat = (dr_datarow["tat"].ToString());
                    task_description = (dr_datarow["task_description"].ToString());



                    if ((lms_code == lslms_code && bureau_code == lsbureau_code && task_name == lstask_name && tat == lstat && task_description == lstask_description) || (task_name == lstask_name))
                    {
                        values.message = "This Task Already Exists";
                        values.status = false;
                        return;
                    }

                }

                dt_datatable.Dispose();
            }
            //msSQL = "select task_name from sys_mst_ttask where task_name = '" + values.task_name.Replace("'", "\\'") + "'";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);
            //if (objODBCDatareader.HasRows == true)
            //{
            //    values.status = false;
            //    values.message = "Task Name Already Exists";
            //}
            //else
            //{

            msGetAPICode = objcmnfunctions.GetApiMasterGID("TAAC");
            msGetGid = objcmnfunctions.GetMasterGID("STSK");
            msGetTaskCode = objcmnfunctions.GetMasterGID("TSKC");
            msSQL = " insert into sys_mst_ttask(" +
                    " task_gid ," +
                    " api_code ," +
                    " task_code ," +
                    " task_name," +
                    " lms_code ," +
                    " bureau_code ," +
                    " task_description," +
                    " tat," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + msGetAPICode + "'," +
                    "'" + msGetTaskCode + "'," +
                    "'" + lstask_name + "'," +
                    "'" + lslms_code + "'," +
                    "'" + lsbureau_code + "'," +
                    "'" + lstask_description + "'," +
                    "'" + lstat + "'," +
                    "'" + employee_gid + "'," +
                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            for (var i = 0; i < values.assigned_to.Count; i++)
            {
                msGetTask2AssignedToGid = objcmnfunctions.GetMasterGID("TAST");
                msSQL = "Insert into sys_mst_ttask2assignedto( " +
                       " task2assignedto_gid, " +
                       " task_gid," +
                       " assignedto_gid," +
                       " assignedto_name," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetTask2AssignedToGid + "'," +
                       "'" + msGetGid + "'," +
                       "'" + values.assigned_to[i].employee_gid + "'," +
                       "'" + values.assigned_to[i].employee_name + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResultSub1 = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            for (var i = 0; i < values.escalationmail_to.Count; i++)
            {
                msGetTask2EscalationMailToGid = objcmnfunctions.GetMasterGID("TEMT");
                msSQL = "Insert into sys_mst_ttask2escalationmailto( " +
                       " task2escalationmailto_gid, " +
                       " task_gid," +
                       " escalationmailto_gid," +
                       " escalationmailto_name," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetTask2EscalationMailToGid + "'," +
                       "'" + msGetGid + "'," +
                       "'" + values.escalationmail_to[i].employee_gid + "'," +
                       "'" + values.escalationmail_to[i].employee_name + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResultSub2 = objdbconn.ExecuteNonQuerySQL(msSQL);
            }


            if (mnResult != 0 && mnResultSub1 != 0 && mnResultSub2 != 0)
            {
                values.status = true;
                values.message = "Task Added Successfully";
            }
            else
            {
                values.message = "Error Occured While Adding Task";
                values.status = false;
            }

            //objODBCDatareader.Close();

        }

        public void DaGetTaskSummary(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT a.task_gid ,a.task_name,a.lms_code, a.bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,api_code," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_ttask a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid where a.delete_flag='N' order by a.task_gid  desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            task_gid = (dr_datarow["task_gid"].ToString()),
                            task_name = (dr_datarow["task_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                        });
                    }
                    objmaster.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaEditTask(string task_gid, MdlTask objmaster)
        {

            msSQL = " SELECT task_gid,task_code,task_name,lms_code, bureau_code,task_description,tat, status as Status FROM sys_mst_ttask " +
                    " where task_gid='" + task_gid + "' ";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objmaster.task_gid = objODBCDatareader["task_gid"].ToString();
                objmaster.task_code = objODBCDatareader["task_code"].ToString();
                objmaster.task_name = objODBCDatareader["task_name"].ToString();
                objmaster.lms_code = objODBCDatareader["lms_code"].ToString();
                objmaster.bureau_code = objODBCDatareader["bureau_code"].ToString();
                objmaster.task_description = objODBCDatareader["task_description"].ToString();
                objmaster.tat = objODBCDatareader["tat"].ToString();
                objmaster.Status = objODBCDatareader["Status"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select assignedto_gid,assignedto_name from sys_mst_ttask2assignedto " +
            " where task_gid='" + task_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getassignedtoList = new List<assignedto_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getassignedtoList.Add(new assignedto_list
                    {
                        employee_gid = dt["assignedto_gid"].ToString(),
                        employee_name = dt["assignedto_name"].ToString(),
                    });
                    objmaster.assigned_to = getassignedtoList;
                }
            }

            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
              " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
              " where user_status<>'N' order by a.user_firstname asc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                objmaster.assignedto_general = dt_datatable.AsEnumerable().Select(row =>
                  new assignedto_list
                  {
                      employee_gid = row["employee_gid"].ToString(),
                      employee_name = row["employee_name"].ToString()
                  }
                ).ToList();
            }
            dt_datatable.Dispose();

            msSQL = " select escalationmailto_gid,escalationmailto_name from sys_mst_ttask2escalationmailto " +
            " where task_gid='" + task_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getescalationmailtoList = new List<escalationmailto_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getescalationmailtoList.Add(new escalationmailto_list
                    {
                        employee_gid = dt["escalationmailto_gid"].ToString(),
                        employee_name = dt["escalationmailto_name"].ToString(),
                    });
                    objmaster.escalationmail_to = getescalationmailtoList;
                }
            }

            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
              " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
              " where user_status<>'N' order by a.user_firstname asc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                objmaster.escalationmailto_general = dt_datatable.AsEnumerable().Select(row =>
                  new escalationmailto_list
                  {
                      employee_gid = row["employee_gid"].ToString(),
                      employee_name = row["employee_name"].ToString()
                  }
                ).ToList();
            }
            dt_datatable.Dispose();

        }

        public bool DaUpdateTask(string employee_gid, MdlTask values)
        {
            string lslms_code, lsbureau_code, lstat, lstask_description, lstask_name = "";

            if (values.lms_code == null || values.lms_code == "")
            {
                lslms_code = "";
            }
            else
            {
                lslms_code = values.lms_code.Replace("'", "\\'");
            }
            if (values.bureau_code == null || values.bureau_code == "")
            {
                lsbureau_code = "";
            }
            else
            {
                lsbureau_code = values.bureau_code.Replace("'", "\\'");
            }
            if (values.tat == null || values.tat == "")
            {
                lstat = "";
            }
            else
            {
                lstat = values.tat.Replace("'", "\\'");
            }
            if (values.task_description == null || values.task_description == "")
            {
                lstask_description = "";
            }
            else
            {
                lstask_description = values.task_description.Replace("'", "\\'");
            }
            if (values.task_name == null || values.task_name == "")
            {
                lstask_name = "";
            }
            else
            {
                lstask_name = values.task_name.Replace("'", "\\'");
            }

            string bureau_code, lms_code, task_name, task_description, tat;
            //bureau_code = calendargroup_name = lms_code = "";

            msSQL = " SELECT task_name, lms_code,bureau_code,tat, task_description FROM sys_mst_ttask ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            //var getSegment = new List<CalendarGroupComparison_List>();

            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    //getSegment.Add(new EncoreProductComparison_List
                    //{

                    lms_code = (dr_datarow["lms_code"].ToString());
                    bureau_code = (dr_datarow["bureau_code"].ToString());
                    task_name = (dr_datarow["task_name"].ToString());
                    tat = (dr_datarow["tat"].ToString());
                    task_description = (dr_datarow["task_description"].ToString());



                    if ((lms_code == lslms_code && bureau_code == lsbureau_code && task_name == lstask_name && tat == lstat && task_description == lstask_description) || (task_name == lstask_name))
                    {
                        values.message = "This Task Already Exists";
                        values.status = false;
                        return false;
                    }

                }

                dt_datatable.Dispose();
            }

            msSQL = "select task_gid, task_name, updated_by, updated_date from sys_mst_ttask where task_gid='" + values.task_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("PMSL");
                    msSQL = " insert into sys_mst_ttasklog(" +
                              " tasklog_gid  ," +
                              " task_gid," +
                              " task_name, " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.task_gid + "'," +
                              "'" + lstask_name + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();


            msSQL = " update sys_mst_ttask set " +
                    " lms_code='" + lslms_code + "'," +
                    " bureau_code='" + lsbureau_code + "'," +
                    " task_name='" + lstask_name + "'," +
                    " tat='" + lstat + "'," +
                    " task_description='" + lstask_description + "'," +
                    " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where task_gid='" + values.task_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from sys_mst_ttask2assignedto where task_gid = '" + values.task_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                for (var i = 0; i < values.assigned_to.Count; i++)
                {
                    msGetTask2AssignedToGid = objcmnfunctions.GetMasterGID("TAST");
                    msSQL = "Insert into sys_mst_ttask2assignedto( " +
                           " task2assignedto_gid, " +
                           " task_gid," +
                           " assignedto_gid," +
                           " assignedto_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetTask2AssignedToGid + "'," +
                           "'" + values.task_gid + "'," +
                           "'" + values.assigned_to[i].employee_gid + "'," +
                           "'" + values.assigned_to[i].employee_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResultSub1 = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }

            msSQL = " delete from sys_mst_ttask2escalationmailto where task_gid = '" + values.task_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                for (var i = 0; i < values.escalationmail_to.Count; i++)
                {
                    msGetTask2EscalationMailToGid = objcmnfunctions.GetMasterGID("TEMT");
                    msSQL = "Insert into sys_mst_ttask2escalationmailto( " +
                           " task2escalationmailto_gid, " +
                           " task_gid," +
                           " escalationmailto_gid," +
                           " escalationmailto_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetTask2EscalationMailToGid + "'," +
                           "'" + values.task_gid + "'," +
                           "'" + values.escalationmail_to[i].employee_gid + "'," +
                           "'" + values.escalationmail_to[i].employee_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResultSub2 = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }

            if (mnResult != 0 && mnResultSub1 != 0 && mnResultSub2 != 0)
            {

                values.status = true;
                values.message = "Task Updated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Updating Task";
                return false;
            }
        }

        public void DaInactiveTask(master values, string employee_gid)
        {
            msSQL = " select taskinitiate_gid from sys_mst_ttaskinitiate where task_gid='" + values.task_gid + "' and (task_status= 'null' or task_status = 'Initiated')";
            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader1.HasRows == true)
            {
                objODBCDatareader1.Close();
                values.message = "Can't able to inactive Task, Because it is tagged to Employee Onboarding";
                values.status = false;
            }
            else
            {
                msSQL = " update sys_mst_ttask set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where task_gid='" + values.task_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("STKI");

                    msSQL = " insert into sys_mst_ttaskinactivelog (" +
                          " taskinactivelog_gid  , " +
                          " task_gid," +
                          " task_name," +
                          " status," +
                          " remarks," +
                          " updated_by," +
                          " updated_date) " +
                          " values (" +
                          " '" + msGetGid + "'," +
                          " '" + values.task_gid + "'," +
                          " '" + values.task_name + "'," +
                          " '" + values.rbo_status + "'," +
                          " '" + values.remarks.Replace("'", "") + "'," +
                          " '" + employee_gid + "'," +
                          " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (values.rbo_status == 'N')
                    {
                        values.status = true;
                        values.message = "Task Inactivated Successfully";
                    }
                    else
                    {
                        values.status = true;
                        values.message = "Task Activated Successfully";
                    }
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred";
                }

            }
        }

        public void DaTaskInactiveLogview(string task_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT task_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_ttaskinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where task_gid ='" + task_gid + "' order by a.taskinactivelog_gid    desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            task_gid = (dr_datarow["task_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaDeleteTask(string task_gid, string employee_gid, master values)
        {
            //msSQL = " update sys_mst_ttask   set delete_flag='Y'," +
            //        " deleted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
            //       "  deleted_by='" + employee_gid + "'" +
            //       " where task_gid='" + task_gid + "' ";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //if (mnResult != 0)
            //{

            //    values.status = true;
            //    values.message = "Task Deleted Successfully";

            //}
            //else
            //{
            //    values.status = false;
            //    values.message = "Error Occurred";
            //}
            msSQL = " select taskinitiate_gid from sys_mst_ttaskinitiate where task_gid='" + task_gid + "' and (task_status= 'null' or task_status = 'Initiated')";
            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader1.HasRows == true)
            {
                objODBCDatareader1.Close();
                values.message = "Can't able to delete Task, Because it is tagged to Employee Onboarding";
                values.status = false;
            }
            else
            {
                objODBCDatareader1.Close();
                msSQL = " select task_name from sys_mst_ttask where task_gid='" + task_gid + "'";
                lsmaster_value = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " delete from sys_mst_ttask where task_gid='" + task_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Task Deleted Successfully..!";
                    msGetGid = objcmnfunctions.GetMasterGID("MSTD");
                    msSQL = " insert into ocs_mst_tmasterdeletelog(" +
                             "master_gid, " +
                             "master_name, " +
                             "master_value, " +
                             "deleted_by, " +
                             "deleted_date) " +
                             " values(" +
                             "'" + msGetGid + "'," +
                             "'Task'," +
                             "'" + lsmaster_value + "'," +
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

        public void DaGetTaskMultiselectList(string task_gid, MdlTask objmaster)
        {

            msSQL = " SELECT GROUP_CONCAT(distinct(b.assignedto_name) SEPARATOR ', ') as assignedto_name, " +
                    " GROUP_CONCAT(distinct(c.escalationmailto_name) SEPARATOR ', ') as escalationmailto_name FROM sys_mst_ttask a " +
                    " left join sys_mst_ttask2assignedto b on a.task_gid = b.task_gid" +
                    " left join sys_mst_ttask2escalationmailto c on a.task_gid = c.task_gid" +
                    " where a.task_gid='" + task_gid + "' ";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objmaster.assignedto_name = objODBCDatareader["assignedto_name"].ToString();
                objmaster.escalationmailto_name = objODBCDatareader["escalationmailto_name"].ToString();

            }
            objODBCDatareader.Close();
        }

        public void DaGetFirstLevelMenu(menu objmaster)
        {
            try
            {
                msSQL = " SELECT module_gid,module_name FROM adm_mst_tmodule where module_gid_parent='$'" +
                        " order by display_order asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmenu_list = new List<menu_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmenu_list.Add(new menu_list
                        {
                            module_gid = (dr_datarow["module_gid"].ToString()),
                            module_name = (dr_datarow["module_name"].ToString()),
                        });
                    }
                    objmaster.menu_list = getmenu_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch (Exception ex)
            {
                objmaster.status = false;
            }
        }
        public void DaGetSecondLevelMenu(menu objmaster, string module_gid_parent)
        {
            try
            {
                msSQL = " SELECT module_gid,module_name FROM adm_mst_tmodule where module_gid_parent='" + module_gid_parent + "'" +
                        " order by display_order asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmenu_list = new List<menu_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmenu_list.Add(new menu_list
                        {
                            module_gid = (dr_datarow["module_gid"].ToString()),
                            module_name = (dr_datarow["module_name"].ToString()),
                        });
                    }
                    objmaster.menu_list = getmenu_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }
        public void DaGetThirdLevelMenu(menu objmaster, string module_gid_parent)
        {
            try
            {
                msSQL = " SELECT module_gid,module_name FROM adm_mst_tmodule where module_gid_parent='" + module_gid_parent + "'" +
                        "  order by display_order asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmenu_list = new List<menu_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmenu_list.Add(new menu_list
                        {
                            module_gid = (dr_datarow["module_gid"].ToString()),
                            module_name = (dr_datarow["module_name"].ToString()),
                        });
                    }
                    objmaster.menu_list = getmenu_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }


        public void DaPostMenudAdd(menu values, string employee_gid)
        {
            msSQL = " SELECT module_gid_parent FROM adm_mst_tmodule where module_gid='" + values.module_gid + "'";
            lslevelfourparent_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " SELECT module_gid_parent FROM adm_mst_tmodule where module_gid='" + lslevelfourparent_gid + "'";
            lslevelthreeparent_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " SELECT module_gid_parent FROM adm_mst_tmodule where module_gid='" + lslevelthreeparent_gid + "'";
            lsleveltwoparent_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " SELECT module_gid_parent FROM adm_mst_tmodule where module_gid='" + lsleveltwoparent_gid + "'";
            lsleveloneparent_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " SELECT module_name FROM adm_mst_tmodule where module_gid='" + lslevelfourparent_gid + "'";
            lslevelthree_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " SELECT module_name FROM adm_mst_tmodule where module_gid='" + lslevelthreeparent_gid + "'";
            lsleveltwo_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " SELECT module_name FROM adm_mst_tmodule where module_gid='" + lsleveltwoparent_gid + "'";
            lslevelone_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " SELECT module_gid FROM sys_mst_tmenumapping where module_gid='" + lsleveltwoparent_gid + "'";
            lslevelonemodule_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " SELECT module_gid FROM sys_mst_tmenumapping where module_gid='" + lslevelthreeparent_gid + "'";
            lsleveltwomodule_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " SELECT module_gid FROM sys_mst_tmenumapping where module_gid='" + lslevelfourparent_gid + "'";
            lslevelthreemodule_gid = objdbconn.GetExecuteScalar(msSQL);

            if (String.IsNullOrEmpty(lslevelonemodule_gid))
            {
                msGetGid = objcmnfunctions.GetMasterGID("MENU");
                msSQL = " insert into sys_mst_tmenumapping(" +
                        " menu_gid," +
                        " module_gid_parent ," +
                        " module_gid ," +
                        " module_name," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + lsleveloneparent_gid + "'," +
                        "'" + lsleveltwoparent_gid + "'," +
                        "'" + lslevelone_name + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = " SELECT module_gid FROM sys_mst_tmenumapping where module_gid='" + lsleveltwoparent_gid + "' and status ='Y'";
                lslevelonemodulestatus_gid = objdbconn.GetExecuteScalar(msSQL);

                if (String.IsNullOrEmpty(lslevelonemodulestatus_gid))
                {

                    msSQL = " Update sys_mst_tmenumapping set status ='Y' where module_gid='" + lsleveltwoparent_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }

            if (String.IsNullOrEmpty(lsleveltwomodule_gid))
            {
                msGetGid = objcmnfunctions.GetMasterGID("MENU");
                msSQL = " insert into sys_mst_tmenumapping(" +
                        " menu_gid," +
                        " module_gid_parent ," +
                        " module_gid ," +
                        " module_name," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + lsleveltwoparent_gid + "'," +
                        "'" + lslevelthreeparent_gid + "'," +
                        "'" + lsleveltwo_name + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = " SELECT module_gid FROM sys_mst_tmenumapping where module_gid='" + lslevelthreeparent_gid + "'and status ='Y'";
                lsleveltwomodulestatus_gid = objdbconn.GetExecuteScalar(msSQL);

                if (String.IsNullOrEmpty(lsleveltwomodulestatus_gid))
                {

                    msSQL = " Update sys_mst_tmenumapping set status ='Y' where module_gid='" + lslevelthreeparent_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            if (String.IsNullOrEmpty(lslevelthreemodule_gid))
            {
                msGetGid = objcmnfunctions.GetMasterGID("MENU");
                msSQL = " insert into sys_mst_tmenumapping(" +
                        " menu_gid," +
                        " module_gid_parent ," +
                        " module_gid ," +
                        " module_name," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + lslevelthreeparent_gid + "'," +
                        "'" + lslevelfourparent_gid + "'," +
                        "'" + lslevelthree_name + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else 
            {
                msSQL = " SELECT module_gid FROM sys_mst_tmenumapping where module_gid='" + lslevelfourparent_gid + "'and status ='Y'";
                lsleveltwomodulestatus_gid = objdbconn.GetExecuteScalar(msSQL);

                if (String.IsNullOrEmpty(lsleveltwomodulestatus_gid))
                {

                    msSQL = " Update sys_mst_tmenumapping set status ='Y' where module_gid='" + lslevelfourparent_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            msGetAPICode = objcmnfunctions.GetApiMasterGID("MMAC");
            msGetGid = objcmnfunctions.GetMasterGID("MENU");
            msSQL = " insert into sys_mst_tmenumapping(" +
                    " menu_gid," +
                    " api_code," +
                    " module_gid_parent," +
                    " module_gid ," +
                    " module_name," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + msGetAPICode + "'," +
                    "'" + lslevelfourparent_gid + "'," +
                    "'" + values.module_gid + "'," +
                    "'" + values.module_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Menu Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }
        }

        public void DaGetMenuMappingSummary(menu objmaster)
        {
            try
            {
                msSQL = " SELECT a.menu_gid,a.module_gid ,a.module_name, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,api_code," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tmenumapping a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid where module_gid like '%_________%' order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmenusummary_list = new List<menusummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmenusummary_list.Add(new menusummary_list
                        {
                            menu_gid = (dr_datarow["menu_gid"].ToString()),
                            module_gid = (dr_datarow["module_gid"].ToString()),
                            module_name = (dr_datarow["module_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString())
                        });
                    }
                    objmaster.menusummary_list = getmenusummary_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }
        public void DaGetMenuMappingEdit(string menu_gid, menu values)
        {
            try
            {
                msSQL = " select menu_gid, module_gid_parent, module_gid, module_name, status as Status from sys_mst_tmenumapping " +
                        " where menu_gid='" + menu_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.menu_gid = objODBCDatareader["menu_gid"].ToString();
                    values.module_gid_parent = objODBCDatareader["module_gid_parent"].ToString();
                    values.module_gid = objODBCDatareader["module_gid"].ToString();
                    values.module_name = objODBCDatareader["module_name"].ToString();
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
        public void DaGetMenuMappingUpdate(string menu_gid, string employee_gid, menu values)
        {
            msSQL = " SELECT module_gid_parent FROM sys_mst_tmenumapping where menu_gid='" + menu_gid + "'";
            lsleveloneparent_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " SELECT module_gid FROM sys_mst_tmenumapping where menu_gid='" + menu_gid + "'";
            lsleveltwoparent_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " SELECT module_name FROM sys_mst_tmenumapping where menu_gid='" + menu_gid + "'";
            lslevelone_name = objdbconn.GetExecuteScalar(msSQL);

            msGetGid = objcmnfunctions.GetMasterGID("UMNU");
            msSQL = " insert into sys_mst_tmenumappingupdatelog(" +
                        " menudupdate_gid, " +
                        " menu_gid," +
                        " module_gid_parent," +
                        " module_gid ," +
                        " module_gid," +
                        " updated_by, " +
                        " updated_date) " +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + menu_gid + "'," +
                        "'" + lsleveloneparent_gid + "'," +
                        "'" + lsleveltwoparent_gid + "'," +
                        "'" + lslevelone_name + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update sys_mst_tmenumapping set module_gid_parent, module_gid, module_gid where menu_gid='" + menu_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Menu Deleted Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Deleting..!";
            }
        }
        public void DaGetMenuMappingDelete(string menu_gid, string employee_gid, menu values)
        {
            msSQL = " SELECT module_gid_parent FROM sys_mst_tmenumapping where menu_gid='" + menu_gid + "'";
            lsleveloneparent_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " SELECT module_gid FROM sys_mst_tmenumapping where menu_gid='" + menu_gid + "'";
            lsleveltwoparent_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " SELECT module_name FROM sys_mst_tmenumapping where menu_gid='" + menu_gid + "'";
            lslevelone_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " SELECT created_by FROM sys_mst_tmenumapping where menu_gid='" + menu_gid + "'";
            lscreated_by = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " SELECT date_format(created_date,'%Y-%m-%d %H:%i:%s') as created_date FROM sys_mst_tmenumapping where menu_gid='" + menu_gid + "'";
            lscreated_date = objdbconn.GetExecuteScalar(msSQL);
            msGetGid = objcmnfunctions.GetMasterGID("DMNU");
            msSQL = " insert into sys_mst_tmenumappingdeletelog(" +
                     " menudelete_gid, " +
                     " menu_gid," +
                     " module_gid_parent," +
                     " module_gid ," +
                     " module_name," +
                     " created_by," +
                     " created_date, " +
                     " deleted_by, " +
                     " deleted_date) " +
                     " values(" +
                     "'" + msGetGid + "'," +
                     "'" + menu_gid + "'," +
                     "'" + lsleveloneparent_gid + "'," +
                     "'" + lsleveltwoparent_gid + "'," +
                     "'" + lslevelone_name + "'," +
                     "'" + lscreated_by + "'," +
                     "'" + lscreated_date + "'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = " delete from sys_mst_tmenumapping where menu_gid='" + menu_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = " SELECT module_gid FROM sys_mst_tmenumapping where module_gid_parent='" + lsleveloneparent_gid + "'";
                module_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " SELECT module_gid_parent FROM sys_mst_tmenumapping where module_gid='" + lsleveloneparent_gid + "'";
                module_gid_parent = objdbconn.GetExecuteScalar(msSQL);
                if (String.IsNullOrEmpty(module_gid))
                {
                    msSQL = " delete from sys_mst_tmenumapping where module_gid='" + lsleveloneparent_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (String.IsNullOrEmpty(module_gid))
                {
                    msSQL = " delete from sys_mst_tmenumapping where module_gid='" + module_gid_parent + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                values.status = true;
                values.message = "Menu Deleted Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Deleting..!";
            }
        }
        public void DaGetMenuMappingInactivate(menu values, string employee_gid)
        {
            msSQL = " update sys_mst_tmenumapping set status='" + values.rbo_status + "'" +
                    " where menu_gid ='" + values.menu_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                msSQL = " SELECT module_gid_parent FROM sys_mst_tmenumapping where menu_gid='" + values.menu_gid + "'";
                module_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " SELECT module_gid_parent FROM sys_mst_tmenumapping where module_gid='" + module_gid + "'";
                module_gid_parent = objdbconn.GetExecuteScalar(msSQL);
                if (values.rbo_status == 'N')
                {
                    msSQL = " SELECT module_gid FROM sys_mst_tmenumapping where module_gid_parent='" + module_gid + "' and status='Y'";
                    lsleveltwoparent_gid = objdbconn.GetExecuteScalar(msSQL);

                    if (String.IsNullOrEmpty(lsleveltwoparent_gid))
                    {
                        msSQL = " update sys_mst_tmenumapping set status='" + values.rbo_status + "'" +
                       " where module_gid ='" + module_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " SELECT module_gid_parent FROM sys_mst_tmenumapping where module_gid_parent='" + module_gid_parent + "' and status='Y'";
                        lsleveloneparent_gid = objdbconn.GetExecuteScalar(msSQL);

                        if (String.IsNullOrEmpty(lsleveloneparent_gid))
                        {
                            msSQL = " update sys_mst_tmenumapping set status='" + values.rbo_status + "'" +
                           " where module_gid ='" + module_gid_parent + "' ";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                }
                else
                {
                    msSQL = " SELECT module_gid FROM sys_mst_tmenumapping where module_gid='" + module_gid + "' and status='Y'";
                    lsleveltwoparent_gid = objdbconn.GetExecuteScalar(msSQL);

                    if (String.IsNullOrEmpty(lsleveltwoparent_gid))
                    {
                        msSQL = " update sys_mst_tmenumapping set status='" + values.rbo_status + "'" +
                       " where module_gid ='" + module_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " SELECT module_gid_parent FROM sys_mst_tmenumapping where module_gid='" + module_gid_parent + "' and status='Y'";
                        lsleveloneparent_gid = objdbconn.GetExecuteScalar(msSQL);

                        if (String.IsNullOrEmpty(lsleveloneparent_gid))
                        {
                            msSQL = " update sys_mst_tmenumapping set status='" + values.rbo_status + "'" +
                           " where module_gid ='" + module_gid_parent + "' ";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                }



                msGetGid = objcmnfunctions.GetMasterGID("UMNU");

                msSQL = " insert into sys_mst_tmenumappinginactivelog (" +
                      " menuinactive_gid, " +
                      " menu_gid," +
                      " module_name," +
                      " status," +
                      " remarks," +
                      " created_by," +
                      " created_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.menu_gid + "'," +
                      " '" + values.module_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Menu Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Menu Type Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaGetMenuMappingInactivateview(string menu_gid, menu values)
        {
            try
            {
                msSQL = " SELECT menu_gid,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tmenumappinginactivelog a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where menu_gid ='" + menu_gid + "' order by a.menuinactive_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplication_list = new List<menusummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getapplication_list.Add(new menusummary_list
                        {
                            menu_gid = (dr_datarow["menu_gid"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.menusummary_list = getapplication_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }


        //HR Notification

        public void DaPostHRNotification(MdlHRNotification values, string employee_gid)
        {
            msSQL = "select application_name from sys_mst_thrnotification where application_name = '" + values.application_name.Replace("'", "\\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Notification Already Exists";
            }
            else
            {

                msGetAPICode = objcmnfunctions.GetApiMasterGID("HRNA");
                msGetGid = objcmnfunctions.GetMasterGID("HRNO");
                msGetHRCode = objcmnfunctions.GetMasterGID("HRNC");
                msSQL = " insert into sys_mst_thrnotification(" +
                        " hrnotification_gid ," +
                        " api_code ," +
                        " hrnotification_code ," +
                        " application_name," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + msGetAPICode + "'," +
                        "'" + msGetHRCode + "',";
                if (values.application_name == "" || values.application_name == null)
                {
                    msSQL += "'',";
                }
                else
                {
                    msSQL += "'" + values.application_name.Replace("'", "") + "',";
                }


                msSQL += "'" + employee_gid + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                for (var i = 0; i < values.notify_to.Count; i++)
                {
                    msGetHR2NotifyToGid = objcmnfunctions.GetMasterGID("HRNT");
                    msSQL = "Insert into sys_mst_thrnotification2notifyto( " +
                           " hr2notifyto_gid, " +
                           " hrnotification_gid," +
                           " notifyto_gid," +
                           " notifyto_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetHR2NotifyToGid + "'," +
                           "'" + msGetGid + "'," +
                           "'" + values.notify_to[i].employee_gid + "'," +
                           "'" + values.notify_to[i].employee_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Notification Added Successfully";
                }
                else
                {
                    values.message = "Error Occured While Adding Notification";
                    values.status = false;
                }

                objODBCDatareader.Close();
            }
        }

        public void DaGetHRNotificationSummary(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT a.hrnotification_gid,a.application_name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,api_code," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_thrnotification a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid  order by a.hrnotification_gid  desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            hrnotification_gid = (dr_datarow["hrnotification_gid"].ToString()),
                            application_name = (dr_datarow["application_name"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                        });
                    }
                    objmaster.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaEditHRNotification(string hrnotification_gid, MdlHRNotification objmaster)
        {

            msSQL = " SELECT hrnotification_gid,hrnotification_code,application_name, status as Status FROM  sys_mst_thrnotification" +
                    " where hrnotification_gid='" + hrnotification_gid + "' ";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objmaster.hrnotification_gid = objODBCDatareader["hrnotification_gid"].ToString();
                objmaster.hrnotification_code = objODBCDatareader["hrnotification_code"].ToString();
                objmaster.application_name = objODBCDatareader["application_name"].ToString();
                objmaster.Status = objODBCDatareader["Status"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select notifyto_gid,notifyto_name from sys_mst_thrnotification2notifyto " +
            " where hrnotification_gid='" + hrnotification_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getnotifytoList = new List<notifyto_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getnotifytoList.Add(new notifyto_list
                    {
                        employee_gid = dt["notifyto_gid"].ToString(),
                        employee_name = dt["notifyto_name"].ToString(),
                    });
                    objmaster.notify_to = getnotifytoList;
                }
            }

            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
              " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
              " where user_status<>'N' order by a.user_firstname asc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                objmaster.notifyto_general = dt_datatable.AsEnumerable().Select(row =>
                  new notifyto_list
                  {
                      employee_gid = row["employee_gid"].ToString(),
                      employee_name = row["employee_name"].ToString()
                  }
                ).ToList();
            }
            dt_datatable.Dispose();


            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
              " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
              " where user_status<>'N' order by a.user_firstname asc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            dt_datatable.Dispose();

        }

        public bool DaUpdateHRNotification(string employee_gid, MdlHRNotification values)
        {
            msSQL = "select hrnotification_gid,application_name, updated_by, updated_date from sys_mst_thrnotification where hrnotification_gid='" + values.hrnotification_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("HROG");
                    msSQL = " insert into sys_mst_thrnotificationlog(" +
                               " hrnotificationlog_gid  ," +
                               " hrnotification_gid," +
                               " application_name, " +
                               " updated_by, " +
                               " updated_date) " +
                               " values(" +
                               "'" + msGetGid + "'," +
                               "'" + objODBCDatareader["hrnotification_gid"].ToString() + "'," +
                               "'" + objODBCDatareader["application_name"].ToString() + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";


                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();


            msSQL = " update sys_mst_thrnotification set ";
            if (values.application_name == "" || values.application_name == null)
            {
                msSQL += " application_name='',";
            }
            else
            {
                msSQL += " application_name='" + values.application_name + "',";
            }
            msSQL += " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where hrnotification_gid='" + values.hrnotification_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from sys_mst_thrnotification2notifyto where hrnotification_gid = '" + values.hrnotification_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                for (var i = 0; i < values.notify_to.Count; i++)
                {
                    msGetHR2NotifyToGid = objcmnfunctions.GetMasterGID("HRNT");
                    msSQL = "Insert into sys_mst_thrnotification2notifyto( " +
                           " hr2notifyto_gid, " +
                           " hrnotification_gid," +
                           " notifyto_gid," +
                           " notifyto_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetHR2NotifyToGid + "'," +
                           "'" + values.hrnotification_gid + "'," +
                           "'" + values.notify_to[i].employee_gid + "'," +
                           "'" + values.notify_to[i].employee_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }


            //msSQL = " delete from sys_mst_ttask2escalationmailto where hrnotification_gid = '" + values.hrnotification_gid + "'";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //if (mnResult != 0)
            //{
            //    for (var i = 0; i < values.escalationmail_to.Count; i++)
            //    {
            //        msGetTask2EscalationMailToGid = objcmnfunctions.GetMasterGID("TEMT");
            //        msSQL = "Insert into sys_mst_ttask2escalationmailto( " +
            //               " task2escalationmailto_gid, " +
            //               " task_gid," +
            //               " escalationmailto_gid," +
            //               " escalationmailto_name," +
            //               " created_by," +
            //               " created_date)" +
            //               " values(" +
            //               "'" + msGetTask2EscalationMailToGid + "'," +
            //               "'" + values.task_gid + "'," +
            //               "'" + values.escalationmail_to[i].employee_gid + "'," +
            //               "'" + values.escalationmail_to[i].employee_name + "'," +
            //               "'" + employee_gid + "'," +
            //               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //    }
            //}

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Notification Updated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Updating Notificaton";
                return false;
            }
        }
        public void DaGetHRNotificationNotifyToList(string hrnotification_gid, MdlHRNotification objmaster)
        {

            msSQL = " SELECT GROUP_CONCAT(distinct(b.notifyto_name) SEPARATOR ', ') as notifyto_name FROM sys_mst_thrnotification a " +
                   " left join sys_mst_thrnotification2notifyto b on a.hrnotification_gid = b.hrnotification_gid" +
                   " where a.hrnotification_gid='" + hrnotification_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objmaster.notifyto_name = objODBCDatareader["notifyto_name"].ToString();
                //objmaster.application_name = objODBCDatareader["application_name"].ToString();

            }
            objODBCDatareader.Close();
        }

        public void DaInactiveHRNotification(master values, string employee_gid)
        {

            msSQL = " update sys_mst_thrnotification set status ='" + values.rbo_status + "'," +
                " remarks='" + values.remarks.Replace("'", "") + "'" +
                " where hrnotification_gid='" + values.hrnotification_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("HRIN");

                msSQL = " insert into sys_mst_thrnotificationinactivelog (" +
                      " hrnotificationinactivelog_gid  , " +
                      " hrnotification_gid," +
                      " application_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.hrnotification_gid + "'," +
                      " '" + values.application_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "HRNotification Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "HRNotification Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }


        }
        public void DaHRNotificationInactiveLogview(string hrnotification_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT hrnotification_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_thrnotificationinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where hrnotification_gid ='" + hrnotification_gid + "' order by a.hrnotificationinactivelog_gid    desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            hrnotification_gid = (dr_datarow["hrnotification_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaDeleteHRNotification(string hrnotification_gid, string employee_gid, result values)
        {

            //msSQL = " select equipment_gid from ocs_mst_tequipment where equipment_gid='" + equipment_gid + "'";
            //objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
            //if (objODBCDatareader1.HasRows == true)
            //{
            //    objODBCDatareader1.Close();
            //    values.message = "Can't able to delete Equipment, Because it is tagged to Equipment";
            //    values.status = false;
            //}
            //else
            //{
            //    objODBCDatareader1.Close();
            msSQL = " select application_name from sys_mst_thrnotification where hrnotification_gid='" + hrnotification_gid + "'";
            lsmaster_value = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " delete from sys_mst_thrnotification where hrnotification_gid='" + hrnotification_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "HR Notification Deleted Successfully..!";
                msGetGid = objcmnfunctions.GetMasterGID("MSTD");
                msSQL = " insert into ocs_mst_tmasterdeletelog(" +
                         "master_gid, " +
                         "master_name, " +
                         "master_value, " +
                         "deleted_by, " +
                         "deleted_date) " +
                         " values(" +
                         "'" + msGetGid + "'," +
                         "'HR Notification'," +
                         "'" + lsmaster_value + "'," +
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

        public void DaBaselocationReportExcel(exportexcel values)
        {
            msSQL = "  SELECT a.baselocation_name as 'Base Location',a.lms_code as 'LMS Code', a.bureau_code as 'Bureau Code', " +
                         " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as 'Created Date', " +
                         " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as 'Created By', " +
                         " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as 'Updated By', " +
                         " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as 'Updated Date'," +
                         " case when a.status = 'N' then 'Inactive' else 'Active' end as 'Status'  " +
                         " FROM sys_mst_tbaselocation a  " +
                         " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                         " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                         " left join hrm_mst_temployee e on a.updated_by = e.employee_gid " +
                         " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                         " where a.delete_flag = 'N' order by a.baselocation_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Base Location");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "System/Baselocation_Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                {
                    if ((!System.IO.Directory.Exists(values.lspath)))
                        System.IO.Directory.CreateDirectory(values.lspath);
                }

                values.lsname = "Baselocation" + ".xlsx";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "System/Baselocation_Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 8])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                dt_datatable.Dispose();
                values.lspath = lscompany_code + "/" + "System/Baselocation_Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", values.lspath, ms);
                ms.Close();
                values.lspath = objcmnstorage.EncryptData(values.lspath);
                values.status = true;
                values.message = "Success";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
        }
        public void DaClusterReportExcel(exportexcel values)
        {
            msSQL = " SELECT a.cluster_name as 'Cluster Name', date_format(a.created_date, '%d-%m-%Y %h:%i %p') as 'Created Date', " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as 'Created By',  " +
                    " (select group_concat(baselocation_name) from sys_mst_tcluster2baselocation where cluster_gid = a.cluster_gid) as 'Baselocation Name', " +
                    " case when a.status = 'N' then 'Inactive' else 'Active' end as 'Status' " +
                    " FROM sys_mst_tclustermapping a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " order by a.created_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Cluster Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "System/Cluster_Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                {
                    if ((!System.IO.Directory.Exists(values.lspath)))
                        System.IO.Directory.CreateDirectory(values.lspath);
                }

                values.lsname = "Cluster_Report" + ".xlsx";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "System/Cluster_Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 5])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                values.lspath = lscompany_code + "/" + "System/Cluster_Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                status = objcmnstorage.UploadStream("erpdocument", values.lspath, ms);
                ms.Close();
                dt_datatable.Dispose();


                values.lspath = objcmnstorage.EncryptData(values.lspath);
                values.status = true;
                values.message = "Success";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
        }
        public void DaRegionalReportExcel(exportexcel values)
        {
            msSQL = " SELECT a.region_name as 'Region Name', date_format(a.created_date,'%d-%m-%Y %h:%i %p') as 'Created Date', " +
                      " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as 'Created By',  " +
                      " (select group_concat(cluster_name) from sys_mst_tregion2cluster where region_gid = a.region_gid) as 'Cluster Name', " +
                      " case when a.status = 'N' then 'Inactive' else 'Active' end as 'Status' " +
                      " FROM sys_mst_tregionmapping a " +
                      " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                      " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                      " order by a.created_date desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Regional Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "System/Regional_Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                {
                    if ((!System.IO.Directory.Exists(values.lspath)))
                        System.IO.Directory.CreateDirectory(values.lspath);
                }

                values.lsname = "Regional_Report" + ".xlsx";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "System/Regional_Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 5])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                values.lspath = lscompany_code + "/" + "System/Regional_Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                status = objcmnstorage.UploadStream("erpdocument", values.lspath, ms);
                ms.Close();
                dt_datatable.Dispose();
                values.lspath = objcmnstorage.EncryptData(values.lspath);
                values.status = true;
                values.message = "Success";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
        }
        public void DaZonalReportExcel(exportexcel values)
        {
            //msSQL =  " SELECT a.zone_name as 'Zone Name', date_format(a.created_date, '%d-%m-%Y %h:%i %p') as 'Created Date', " +
            //         " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as 'Created By', " +
            //         " (select group_concat(region_name) from sys_mst_tzone2region where zone_gid = a.zone_gid) as 'Region Name', " +
            //         " case when a.status = 'N' then 'Inactive' else 'Active' end as 'Status' , " +
            //         " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as 'Updated Date',  " +
            //         " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as 'Updated By'," +
            //         " a.remarks as 'Remarks' " +
            //         " FROM sys_mst_tzonemapping a " +
            //         " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
            //         " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
            //         " left join hrm_mst_temployee d on a.created_by = d.employee_gid " +
            //         " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
            //         " order by a.created_date desc ";


            msSQL = " SELECT a.zone_name as 'Zone Name', (select group_concat(region_name) from sys_mst_tzone2region where zone_gid = a.zone_gid) as 'Region Name',  " +
                     " group_concat(distinct (concat(h.region_name, ':', (select group_concat(distinct p.cluster_name) from sys_mst_tregion2cluster p where p.region_gid = h.region_gid), ';' ) )) as `Cluster Name`," +
                     " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as 'Created Date',  " +
                     " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as 'Created By',    " +
                     " case when a.status = 'N' then 'Inactive' else 'Active' end as 'Status'   " +
                     " FROM sys_mst_tzonemapping a  " +
                     " left join sys_mst_tzone2region f on a.zone_gid = f.zone_gid " +
                     " left join sys_mst_tregionmapping h on f.region_gid = h.region_gid " +
                     " left join hrm_mst_temployee b on a.created_by = b.employee_gid  " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                     " left join sys_mst_tregion2cluster g on f.region_gid = g.region_gid " +
                     " group by f.zone_gid " +
                     " order by a.created_date desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Zonal Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "System/Zonal_Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                {
                    if ((!System.IO.Directory.Exists(values.lspath)))
                        System.IO.Directory.CreateDirectory(values.lspath);
                }

                values.lsname = "Zonal_Report" + ".xlsx";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "System/Zonal_Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 6])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                values.lspath = lscompany_code + "/" + "System/Zonal_Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                status = objcmnstorage.UploadStream("erpdocument", values.lspath, ms);
                ms.Close();
                dt_datatable.Dispose();
                values.lspath = objcmnstorage.EncryptData(values.lspath);
                values.status = true;
                values.message = "Success";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
        }

        //public void LogForAudit(string strVal)
        //{
        //    try
        //    {
        //        string lspath = ConfigurationManager.AppSettings["file_path"].ToString() + "/erp_documents/MenuMappingLog/" + DateTime.Now.Year + @"\" + DateTime.Now.Month;
        //        if ((!System.IO.Directory.Exists(lspath)))
        //            System.IO.Directory.CreateDirectory(lspath);

        //        lspath = lspath + @"\" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt";
        //        System.IO.StreamWriter sw = new System.IO.StreamWriter(lspath, true);
        //        sw.WriteLine(strVal);
        //        sw.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        //Branch Summary

        public void DaGetBranchSummary(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT a.branch_gid,api_code,branch_code,branch_name, branch_prefix, " +
                        " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as branchmanager_gid," +
                        " branch_location FROM hrm_mst_tbranch a " +
                        " left join hrm_mst_temployee b on a.branchmanager_gid = b.employee_gid " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.branch_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            branch_gid = (dr_datarow["branch_gid"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                            branch_code = (dr_datarow["branch_code"].ToString()),
                            branch_name = (dr_datarow["branch_name"].ToString()),
                            branch_prefix = (dr_datarow["branch_prefix"].ToString()),
                            branchmanager_gid = (dr_datarow["branchmanager_gid"].ToString()),
                            branch_location = (dr_datarow["branch_location"].ToString()),
                            //status = (dr_datarow["status"].ToString()),
                        });
                    }
                    objmaster.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        //Department Summary

        public void DaGetDepartmentSummary(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT a.department_gid,api_code,department_code,department_prefix, department_name, " +
                       " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as department_manager " +
                       " FROM hrm_mst_tdepartment a " +
                       " left join hrm_mst_temployee b on a.department_manager = b.employee_gid " +
                       " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.department_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            department_gid = (dr_datarow["department_gid"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                            department_code = (dr_datarow["department_code"].ToString()),
                            department_prefix = (dr_datarow["department_prefix"].ToString()),
                            department_name = (dr_datarow["department_name"].ToString()),
                            department_manager = (dr_datarow["department_manager"].ToString()),

                        });
                    }
                    objmaster.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }
       
        //One API user Registration
        public void DaGetExternalUser(MdlSystemMaster objoneapiuser)
        {

            try
            {
                msSQL = "select  a.user2oneapi_gid,a.user_code,a.externalsystem_password,a.externalsystem_name," +
                        " if(a.user_status='Y','Active','Inactive') as user_status ," +
                        " if(a.web_active='Y','Active','Inactive') as web_active ," +
                        " a.externalsystem_ownername,date_format(a.created_date,'%d-%m-%Y') as created_date," +
                        " concat(b.user_firstname,' ',b.user_lastname,' || ',b.user_code ) as created_by,email_id " +
                        " from adm_mst_tuser2oneapi a " +
                        " left join adm_mst_tuser b on a.created_by=b.user_gid where 1=1 order by a.created_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getoneapiuser_list = new List<externaluser_lists>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getoneapiuser_list.Add(new externaluser_lists
                        {
                            user2oneapi_gid = (dr_datarow["user2oneapi_gid"].ToString()),
                            externaluser_code = (dr_datarow["user_code"].ToString()),
                            externaluser_password = (dr_datarow["externalsystem_password"].ToString()),
                            externaluser_status = (dr_datarow["user_status"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            externalsystem_name = (dr_datarow["externalsystem_name"].ToString()),
                            externalsystem_ownername = (dr_datarow["externalsystem_ownername"].ToString()),
                            web_active=(dr_datarow["web_active"].ToString()),
                            email_id = (dr_datarow["email_id"].ToString()),
                        });
                    }
                    objoneapiuser.externaluser_list = getoneapiuser_list;
                }
                dt_datatable.Dispose();
                objoneapiuser.status = true;

            }
            catch (Exception ex)
            {
                objoneapiuser.status = false;
            }
        }
        public void DaPostExternalUser(externaluser_lists values, string user_gid)
        {
            try
            {
                msSQL = "select externalsystem_name from adm_mst_tuser2oneapi where externalsystem_name = '" + values.externalsystem_name.Replace("'", "\\'") + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "System Name Already Exists";
                }
                else
                {
                    string salt = SecurityHelper.GenerateSalt(70);
                    string passwordHashed = SecurityHelper.HashPassword(values.externaluser_password, salt, 10101, 70);


                    msGetGid = objcmnfunctions.GetMasterGID("OAUS");
                    //for (var i = 0; i < values.externalsystem_ownernameList.Count; i++)
                    //{

                    msSQL = "insert into adm_mst_tuser2oneapi(" +
                        "user2oneapi_gid ," +
                        "user_code ," +
                        "user_password ," +
                        "user_status ," +
                        "web_active ," +
                        "externalsystem_name," +
                        "externalsystem_ownername ," +
                        "created_by ," +
                        "created_date ) Values (" +
                        " '" + msGetGid + "'," +
                        " '" + values.externaluser_code + "'," +
                        "'" + passwordHashed + "'," +
                        "'Y'," +
                        "'Y'," +
                        "'" + values.externalsystem_name.Replace("'", "\\'") + "'," +
                        "'" + values.externalsystem_ownername + "'," +
                        "'" + user_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    //}
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = "User Added successfully";
                    }
                    else
                    {
                        values.message = "Error Occured while Adding";
                        values.status = false;
                    }
                }
            }

            catch (Exception ex)
            {

            }

        }

        public void DaPopSystemOwner(MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT distinct a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name, " +
                   " b.employee_gid from adm_mst_tuser a " +
                   " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                   " where user_status<>'N' and a.employee_status = 'A' " +
                   " order by a.user_firstname asc";

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
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }


        }
        public void DaCreateUserReg(userpurpose values, string user_gid)
        {
            msSQL = "select externalsystem_name from adm_mst_tuser2oneapi where externalsystem_name = '" + values.externalsystem_name.Replace("'", "\\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "System Name already exist";
            }
            else
            {
                string salt = SecurityHelper.GenerateSalt(70);
                string passwordHashed = SecurityHelper.HashPassword(values.externaluser_password, salt, 10101, 70);

                msGetGid = objcmnfunctions.GetMasterGID("OAUS");
                msGetUserCode = objcmnfunctions.GetMasterGID("OAUC");
                if (values.externalsystem_ownernameList != null)
                {
                    for (var i = 0; i < values.externalsystem_ownernameList.Count; i++)
                    {
                        lsemployee_gid += values.externalsystem_ownernameList[i].employee_gid + ",";
                        lsemployee_name += values.externalsystem_ownernameList[i].employee_name + ",";
                    }
                    lsemployeegroup_gid = lsemployee_gid.TrimEnd(',');
                    lsemployeegroup_name = lsemployee_name.TrimEnd(',');
                }
                for (var i = 0; i < values.externalsystem_ownernameList.Count; i++)
                {
                    msGetsystem_ownername_gid = objcmnfunctions.GetMasterGID("UASO");
                    msSQL = "Insert into adm_mst_tuserapi2systemowner( " +
                           " userapi2systemowner_gid, " +
                           " user2oneapi_gid," +
                           " systemowner_gid," +
                           " systemowner_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetsystem_ownername_gid + "'," +
                           "'" + msGetGid + "'," +
                           "'" + values.externalsystem_ownernameList[i].employee_gid + "'," +
                           "'" + values.externalsystem_ownernameList[i].employee_name + "'," +
                           "'" + user_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = "insert into adm_mst_tuser2oneapi(" +
                    "user2oneapi_gid ," +
                    "user_code ," +        
                    "user_password ," +
                    "password_salt ," +
                    "user_status ," +
                    "web_active ," +
                    "externalsystem_name," +
                    "externalsystem_ownername ," +
                    "externalsystem_ownergid ," +
                    "email_id ," +
                    "created_by ," +
                    "created_date ) Values (" +
                    " '" + msGetGid + "'," +
                    " '" + msGetUserCode + "'," +
                    "'" + passwordHashed + "'," +
                    "'" + salt + "'," +
                    "'Y'," +
                    "'Y'," +
                    "'" + values.externalsystem_name.Replace("'", "\\'") + "'," +
                    "'" + lsemployeegroup_name + "'," +
                    "'" + lsemployeegroup_gid + "'," +
                    "'" + values.email_id + "'," +
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "User added successfully";
                }
                else
                {
                    values.message = "Error occured while registering user!";
                    values.status = false;
                }
            }
        }
        public void DaAssignedSystemOwner(string user2oneapi_gid, userpurpose values)
        {
            msSQL = " select user2oneapi_gid,user_code,externalsystem_name,externalsystem_ownername a  from adm_mst_tuser2oneapi " +
                      " where user2oneapi_gid = '" + user2oneapi_gid + "' order by user2oneapi_gid asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getsystemasssigned_list = new List<systemasssigned_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getsystemasssigned_list.Add(new systemasssigned_list
                    {
                        employee_gid = dt["member_gid"].ToString(),
                        employee_name = dt["member_name"].ToString(),
                        user2oneapi_gid = dt["user2oneapi_gid"].ToString(),
                        externalsystem_name = dt["externalsystem_name"].ToString(),
                    });
                    values.systemasssigned_list = getsystemasssigned_list;
                }
            }
            dt_datatable.Dispose();
        }

      
        public void DaGetUserEdit(string user2oneapi_gid, userpurpose values)
        {
            msSQL = " select user2oneapi_gid,user_code,externalsystem_name,externalsystem_ownername,user_status,externalsystem_password,email_id from adm_mst_tuser2oneapi" +
                       " where user2oneapi_gid = '" + user2oneapi_gid + "' order by user2oneapi_gid asc ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.user2oneapi_gid = objODBCDatareader["user2oneapi_gid"].ToString();
                values.externalsystem_name = objODBCDatareader["externalsystem_name"].ToString();
                values.externaluser_status = objODBCDatareader["user_status"].ToString();
                values.externaluser_code = objODBCDatareader["user_code"].ToString(); 
                    values.externaluser_password = objODBCDatareader["externalsystem_password"].ToString();
                values.email_id = objODBCDatareader["email_id"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select userapi2systemowner_gid,systemowner_name,systemowner_gid from adm_mst_tuserapi2systemowner " +
                     " where user2oneapi_gid = '" + user2oneapi_gid + "' order by user2oneapi_gid asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getsystemasssigned_list = new List<systemasssigned_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getsystemasssigned_list.Add(new systemasssigned_list
                    {
                        employee_gid = dt["systemowner_gid"].ToString(),
                        employee_name = dt["systemowner_name"].ToString(),
                        user2oneapi_gid = dt["userapi2systemowner_gid"].ToString(),
                      
                    });
                    values.systemasssigned_list = getsystemasssigned_list;
                }
            }
            dt_datatable.Dispose();
           
            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' || ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                           " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                           " where user_status<>'N' order by a.user_firstname asc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_manageremployee = new List<employeeem_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                values.employeeem_list = dt_datatable.AsEnumerable().Select(row =>
                  new employeeem_list
                  {
                      employee_gid = row["employee_gid"].ToString(),
                      employee_name = row["employee_name"].ToString()
                  }
                ).ToList();
            }
            dt_datatable.Dispose();

            values.status = true;


        }
        public bool DaPostUserUpdate(string user_gid, userpurpose values)
        {
            msSQL = "select user2oneapi_gid from adm_mst_tuser2oneapi where externalsystem_name='" + values.externalsystem_name.Replace("'", "\\'") + "'";
            clusterGID = objdbconn.GetExecuteScalar(msSQL);
            if (clusterGID != "")
            {
                if (clusterGID != values.user2oneapi_gid)
                {
                    values.status = false;
                    values.message = "System Name Already Exist";
                    return false;
                }
            }

            msSQL = "select updated_by, updated_date,externalsystem_name,email_id from adm_mst_tuser2oneapi where user2oneapi_gid ='" + values.user2oneapi_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();
                string lsemail_id = objODBCDatareader["email_id"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("UAML");
                    msSQL = " insert into sys_mst_tuser2apimappinglog(" +
                              " user2apilog_gid," +
                              " user2oneapi_gid," +
                              " externalsystem_name , " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.user2oneapi_gid + "'," +
                              "'" + objODBCDatareader["externalsystem_name"].ToString() + "'," +
                              "'" + user_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            if (values.systemasssigned_list != null)
            {
                for (var i = 0; i < values.systemasssigned_list.Count; i++)
                {
                    lsemployee_gid += values.systemasssigned_list[i].employee_gid + ",";
                    lsemployee_name += values.systemasssigned_list[i].employee_name + ",";
                }
                lsemployeegroup_gid = lsemployee_gid.TrimEnd(',');
                lsemployeegroup_name = lsemployee_name.TrimEnd(',');
            }
            msSQL = "update adm_mst_tuser2oneapi set externalsystem_name='" + values.externalsystem_name.Replace("'", "") + "'," +
                 " externalsystem_ownergid='" + lsemployeegroup_gid + "'," +
                 " externalsystem_ownername='" + lsemployeegroup_name + "'," +
                 " email_id='" + values.email_id + "'," +
                 " updated_by='" + user_gid + "'," +                 
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where user2oneapi_gid='" + values.user2oneapi_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from adm_mst_tuserapi2systemowner where user2oneapi_gid ='" + values.user2oneapi_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                for (var i = 0; i < values.systemasssigned_list.Count; i++)
                {
                    msGetsystem_ownername_gid = objcmnfunctions.GetMasterGID("UASO");
                    msSQL = "Insert into adm_mst_tuserapi2systemowner( " +
                           " userapi2systemowner_gid, " +
                           " user2oneapi_gid," +
                           " systemowner_gid," +
                           " systemowner_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetsystem_ownername_gid + "'," +
                           "'" + values.user2oneapi_gid + "'," +
                           "'" + values.systemasssigned_list[i].employee_gid + "'," +
                           "'" + values.systemasssigned_list[i].employee_name + "'," +
                           "'" + user_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }


            if (mnResult != 0)
            {
                values.status = true;
                values.message = "User Details updated successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating User";
                return false;
            }
        }
        public void DaInactiveUserReg(userpurpose values, string employee_gid)
        {
            msSQL = " update adm_mst_tuser2oneapi set user_status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where user2oneapi_gid='" + values.user2oneapi_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("UAIL");

                msSQL = " insert into adm_mst_tuser2oneapiinactivelog (" +
                      " user2oneapiinactivelog_gid, " +
                      " user2oneapi_gid," +
                      " externalsystem_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.user2oneapi_gid + "'," +
                      " '" + values.externalsystem_name.Replace("'", "") + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "User Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "User Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred while changing user status!";
            }
        }
        public void DaUserRegInactiveLogview(string user2oneapi_gid, userpurpose values)
        {
            try
            {
                msSQL = " SELECT a.user2oneapi_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM adm_mst_tuser2oneapiinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.user2oneapi_gid ='" + user2oneapi_gid + "' order by a.user2oneapiinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getuserinactivelog_list = new List<userinactivelog_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getuserinactivelog_list.Add(new userinactivelog_list
                        {
                            user2oneapi_gid = (dr_datarow["user2oneapi_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.userinactivelog_list = getuserinactivelog_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public bool DaGetOneApiCode(userpurpose values, string user2oneapi_gid)
        {
            bool status = false;
            try
            {
                msSQL = "select user_code,web_active,user_active,user_status from adm_mst_tuser2oneapi where user2oneapi_gid='" + user2oneapi_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.user_code = objODBCDatareader["user_code"].ToString();
                    values.web_active = objODBCDatareader["web_active"].ToString();
                    values.user_active = objODBCDatareader["user_active"].ToString();
                    values.user_status = objODBCDatareader["user_status"].ToString();

                }
                else
                {
                    values.message = "Error occurred while getting one api code..!!";
                    values.status = false;
                    status = false;

                }

            }

            catch (Exception ex)
            {
              
                values.status = false;
                values.message = "Error Occured while getting one api code..";

            }
            return status;
        }
        public bool DaPasswordUpdate(userpurpose values, string employee_gid)
        {
            bool status = false;
            try
            {
                string salt = SecurityHelper.GenerateSalt(70);
                string passwordHashed = SecurityHelper.HashPassword(values.user_password, salt, 10101, 70);

                msSQL = "update adm_mst_tuser2oneapi set user_password='" + passwordHashed + "',password_salt = '" + salt + "'  where user2oneapi_gid='" + values.user2oneapi_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("UARP");

                    msSQL = " insert into sys_mst_tuser2apiresetpasswordlog (" +
                          " user2apiresetpasswordlog_gid, " +
                          " user2oneapi_gid," +
                          " previous_password," +
                          " current_password," + 
                          " updated_by," +
                          " updated_date) " +
                          " values (" +
                          " '" + msGetGid + "'," +
                          " '" + values.user2oneapi_gid + "'," +
                          " '" + values.externaluser_password + "'," +
                          " '" + passwordHashed + "'," +
                          " '" + employee_gid + "'," +
                          " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult == 1)
                    {
                        values.message = "Password updated successfully..!!";
                        values.status = true;
                        status = true;


                    }
                    else
                    {
                        values.message = "Error occurred while updating password..!!";
                        values.status = false;
                        status = false;

                    }
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred while updating password";
                }
                

            }

            catch (Exception ex)
            {
                string lspath = ConfigurationManager.AppSettings["file_path"].ToString() + "/oneapidocuments/ErrorLog/UserRegistraion/" + DateTime.Now.Year + @"\" + DateTime.Now.Month;
                if ((!System.IO.Directory.Exists(lspath)))
                    System.IO.Directory.CreateDirectory(lspath);



                lspath = lspath + @"\" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt";
                System.IO.StreamWriter sw = new System.IO.StreamWriter(lspath, true);
                sw.WriteLine("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********Exception-" + ex.Message.ToString() + "*********Query-" + msSQL);
                sw.Close(); 



                values.status = false;
                values.message = "Error Occured..";

            }
            return status;
        }
        public void DaUserRegResetPwdLogview(string user2oneapi_gid, userpurpose values)
        {
            try
            {
                msSQL = " SELECT a.user2oneapi_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        "  a.previous_password,a.current_password " +
                        " FROM sys_mst_tuser2apiresetpasswordlog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.user2oneapi_gid ='" + user2oneapi_gid + "' order by a.user2apiresetpasswordlog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getuserinactivelog_list = new List<userinactivelog_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getuserinactivelog_list.Add(new userinactivelog_list
                        {
                            user2oneapi_gid = (dr_datarow["user2oneapi_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            previous_password = (dr_datarow["previous_password"].ToString()),
                            current_password = (dr_datarow["current_password"].ToString()),
                        });
                    }
                    values.userinactivelog_list = getuserinactivelog_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public bool DaPostWebDeActivate(string employee_gid, userpurpose values)
        {
            bool status = false;
            
            try
            {
                msSQL = "select externalsystem_name,user_code from adm_mst_tuser2oneapi where user2oneapi_gid='" + values.user2oneapi_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    lsuser_code = objODBCDatareader["user_code"].ToString();
                    lsexternalsystem_name = objODBCDatareader["externalsystem_name"].ToString();
                }

                msSQL = "update adm_mst_tuser2oneapi set web_active='N' where user2oneapi_gid='" + values.user2oneapi_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("UAWA");
                    msSQL = " insert into adm_mst_tuser2oneapiwebaccesslog(" +
                              " user2oneapiwebaccesslog_gid," +
                              " user2oneapi_gid," +
                              " externalsystem_name, " +
                              " user_code, " +
                              " status, " +
                              " remarks, " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.user2oneapi_gid + "'," +
                              "'" + lsexternalsystem_name + "'," +
                              "'" + lsuser_code + "'," +
                              "'N'," +
                              "'" + values.remarks.Replace("'", "") + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult == 1)
                    {
                        values.message = "Deactivated successfully..!!";
                        values.status = true;
                        status = true;


                    }
                    else
                    {
                        values.message = "Error occurred in log..!!";
                        values.status = false;
                        status = false;

                    }


                }
                else
                {
                    values.message = "Error occurred while Deactivating..!!";
                    values.status = false;
                    status = false;

                }

            }

            catch (Exception ex)
            {
                
                values.status = false;
                values.message = "Error Occured while changing web access status..";

            }
            return status;
        }
        public bool DaPostWebActivate(string employee_gid, userpurpose values)
        {
            bool status = false;

            try
            {
                msSQL = "select externalsystem_name,user_code from adm_mst_tuser2oneapi where user2oneapi_gid='" + values.user2oneapi_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    lsuser_code = objODBCDatareader["user_code"].ToString();
                    lsexternalsystem_name = objODBCDatareader["externalsystem_name"].ToString();
                }

                msSQL = "update adm_mst_tuser2oneapi set web_active='Y' where user2oneapi_gid='" + values.user2oneapi_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("UAWA");
                    msSQL = " insert into adm_mst_tuser2oneapiwebaccesslog(" +
                              " user2oneapiwebaccesslog_gid," +
                              " user2oneapi_gid," +
                              " externalsystem_name, " +
                              " user_code, " +
                              " status, " +
                              " remarks, " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.user2oneapi_gid + "'," +
                              "'" + lsexternalsystem_name + "'," +
                              "'" + lsuser_code + "'," +
                              "'Y'," +
                              "'" + values.remarks.Replace("'", "") + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult == 1)
                    {
                        values.message = "Activated successfully..!!";
                        values.status = true;
                        status = true;


                    }
                    else
                    {
                        values.message = "Error occurred while Activating..!!";
                        values.status = false;
                        status = false;

                    }


                }
                else
                {
                    values.message = "Error occurred while Deactivating..!!";
                    values.status = false;
                    status = false;

                }

            }

            catch (Exception ex)
            {
              

                values.status = false;
                values.message = "Error Occured While Updating Web Access! ..";

            }
            return status;
        }
        public void DaGetWebAccessActiveLog(string user2oneapi_gid, userpurpose values)
        {
            try
            {

                msSQL = " SELECT a.user2oneapi_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date,  " +
                        " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as updated_by, " +
                        " a.user_code,a.status,a.remarks " +
                        " FROM adm_mst_tuser2oneapiwebaccesslog a " +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.user2oneapi_gid = '" + user2oneapi_gid + "' order by a.user2oneapiwebaccesslog_gid desc  ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getSegment = new List<userinactivelog_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getSegment.Add(new userinactivelog_list
                        {
                            user_code = (dr_datarow["user_code"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                        });
                    }
                    values.userinactivelog_list = getSegment;
                }
                dt_datatable.Dispose();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetFourthLevelMenu(menu objmaster, string module_gid_parent)
        {
            try
            {
                msSQL = " SELECT module_gid,module_name FROM adm_mst_tmodule where module_gid_parent='" + module_gid_parent + "'" +
                        " and module_gid not in (select module_gid from sys_mst_tmenumapping) order by display_order asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmenu_list = new List<menu_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmenu_list.Add(new menu_list
                        {
                            module_gid = (dr_datarow["module_gid"].ToString()),
                            module_name = (dr_datarow["module_name"].ToString()),
                        });
                    }
                    objmaster.menu_list = getmenu_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        } 

        //State Summary
        public void DaGetGstStateSummary(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT state_gid, state_code, state_name, diaplay_order, api_code FROM ocs_mst_tstate a order by a.state_gid desc  ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            state_gid = (dr_datarow["state_gid"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                            state_code = (dr_datarow["state_code"].ToString()),
                            diaplay_order = (dr_datarow["diaplay_order"].ToString()),
                            state_name = (dr_datarow["state_name"].ToString()),                            

                        });
                    }
                    objmaster.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        //get city name
        public void DaGetCityList(MdlFPOCity objvalues)
        {
            try
            {
                msSQL = " SELECT city_gid,city_name,api_code FROM ocs_mst_tcity";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcity_list = new List<getcity_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    getcity_list = dt_datatable.AsEnumerable().Select(row => new getcity_list
                    {
                        City_Gid = row["city_gid"].ToString(),
                        City_Name = row["city_name"].ToString(),
                        api_code = row["api_code"].ToString(),
                    }).ToList();
                }
                dt_datatable.Dispose();
                objvalues.getcity_list = getcity_list;

            }
            catch (Exception ex)
            {
                objvalues.getcity_list = null;
            }
        }



    }

}