using ems.master.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;

/// <summary>
/// (It's used for Designation Master ) Designation DataAccess Class accessed by API methods from related Controller class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash</remarks>

namespace ems.master.DataAccess
{
    public class DaDesignation
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid, msGetAPICode;
        int mnResult, GetApiMasterGID;
        string lsmaster_value, lslms_code, lsbureau_code, lsdocumentgid;

        public void DaGetDesignation(MdlDesignation objMdlDesignation)
        {
            try
            {
                msSQL = " SELECT a.designation_gid,a.api_code,a.designation_type,lms_code,bureau_code,status_log, " +
                    " date_format(a.created_date,'%d-%m-%Y || %h:%i %p') as created_date,concat(c.user_firstname,' ' ,c.user_lastname,'||',c.user_code) as created_by " +
                    " from ocs_mst_tdesignation a" +
                    " left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid   order by a.designation_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdesignation = new List<designation_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getdesignation.Add(new designation_list
                        {
                            designation_gid = (dr_datarow["designation_gid"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                            designation_type = (dr_datarow["designation_type"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            status_log = (dr_datarow["status_log"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                        });
                    }
                    objMdlDesignation.designation_list = getdesignation;
                }
                dt_datatable.Dispose();
                objMdlDesignation.status = true;
            }
            catch
            {
                objMdlDesignation.status = false;
            }
        }

        public void DaCreateDesignation(designation values, string employee_gid)
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

            msSQL = "select designation_type from ocs_mst_tdesignation where designation_type = '" + values.designation_type.Replace("'", "\\'") + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Designation Already Exist";
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("MSDS");
                msGetAPICode = objcmnfunctions.GetApiMasterGID("DESI");
                msSQL = " insert into ocs_mst_tdesignation(" +
                        " designation_gid," +
                        " api_code," +
                        " lms_code," +
                        " bureau_code," +
                        " designation_type," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                       "'" + msGetAPICode + "'," +
                       "'" + lslms_code + "'," +
                      "'" + lsbureau_code + "'," +
                      "'" + values.designation_type.Replace("'", "") + "'," +             
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Designation Added Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred While Adding";
                }
            }
        }
       
        public void DaEditDesignation(string designation_gid, designation values)
        {
            try
            {
                msSQL = " select designation_gid,lms_code,bureau_code,status_log ,designation_type from ocs_mst_tdesignation where designation_gid='" + designation_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.designation_type = objODBCDatareader["designation_type"].ToString();
                    values.designation_gid = objODBCDatareader["designation_gid"].ToString();
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    values.status_log = objODBCDatareader["status_log"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdateDesignation(string employee_gid, designation values)
        {
            msSQL = "select designation_gid from ocs_mst_tdesignation where designation_type = '" + values.designation_type.Replace("'", "\\'") + "'";
            lsdocumentgid = objdbconn.GetExecuteScalar(msSQL);
            if (lsdocumentgid != "")
            {
                if (lsdocumentgid != values.designation_gid)
                {
                    values.message = "Designation Type Already Exist";
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
   
                    msSQL = " update ocs_mst_tdesignation set " +
                " designation_type='" + values.designation_type.Replace("'", "") + "'," +
                " lms_code='" + lslms_code + "'," +
                " bureau_code='" + lsbureau_code + "'," +
                " updated_by='" + employee_gid + "'," +
                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " where designation_gid='" + values.designation_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select designation_type from ocs_mst_tdesignation where designation_gid='" + values.designation_gid + "'";
                lsmaster_value = objdbconn.GetExecuteScalar(msSQL);
                if (mnResult != 0)
                {
                    //msGetGid = objcmnfunctions.GetMasterGID("MELG");
                    msGetGid = objcmnfunctions.GetMasterGID("DLOG");
                    msSQL = " insert into ocs_trn_tauditdesignationlog(" +
                              " auditdesignationlog_gid," +
                              " designation_gid," +
                              " designation_type, " +
                              " created_by, " +
                              " created_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.designation_gid + "'," +
                              "'" + lsmaster_value + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Designation Type Updated Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred While Updating";
                }
            }
           
       
        public void DaDeleteDesignation(string designation_gid, string employee_gid,designation values)
        {
           
                msSQL = "select application_gid from ocs_mst_tapplication where designation_gid = '" + designation_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.message = "Can't able to delete Designation, Because it is tagged to Application Creation";
                    values.status = false;
                    return;
                }
                else
                {
                   
                        msSQL = " select institution_gid from ocs_mst_tinstitution where designation_gid='" + designation_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            objODBCDatareader.Close();
                            values.message = "Can't able to delete Designation, Because it is tagged to Application Creation";
                            values.status = false;
                            return;
                        }
                        else
                        {
                            objODBCDatareader.Close();
                            msSQL = " select designation_type from ocs_mst_tdesignation where designation_gid='" + designation_gid + "'";
                            lsmaster_value = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = " delete from ocs_mst_tdesignation where designation_gid='" + designation_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (mnResult != 0)
                            {
                                values.status = true;
                                msGetGid = objcmnfunctions.GetMasterGID("MSTD");
                                msSQL = " insert into ocs_mst_tmasterdeletelog(" +
                                         "master_gid, " +
                                         "master_name, " +
                                         "master_value, " +
                                         "deleted_by, " +
                                         "deleted_date) " +
                                         " values(" +
                                         "'" + msGetGid + "'," +
                                         "'Designation'," +
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
          
                }
        }
        public void DaGetDesignationASC(MdlDesignation objMdlDesignation)
        {
            try
            {
                msSQL = " SELECT designation_gid,designation_type FROM ocs_mst_tdesignation order by designation_gid asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdesignation = new List<designation_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getdesignation.Add(new designation_list
                        {
                            designation_gid = (dr_datarow["designation_gid"].ToString()),
                             designation_type = (dr_datarow["designation_type"].ToString()),
                        });
                    }
                    objMdlDesignation.designation_list = getdesignation;
                }
                dt_datatable.Dispose();
                objMdlDesignation.status = true;
            }
            catch
            {
                objMdlDesignation.status = false;
            }
        }
        public void DaDesignationStatusUpdate(string employee_gid, designation values)
        {

            msSQL = " update ocs_mst_tdesignation set status_log='" + values.status_log + "'," +
                " remarks='" + values.remarks.Replace("'", " ") + "'," +
                " updated_by='" + employee_gid + "'," +
                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " where designation_gid='" + values.designation_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("DLOG");
                msSQL = " insert into ocs_trn_tdesignationtatuslog(" +
                          " designationstatuslog_gid," +
                          " designation_gid," +
                          " status_log, " +
                          " remarks, " +
                          " created_by, " +
                          " created_date) " +
                          " values(" +
                          "'" + msGetGid + "'," +
                          "'" + values.designation_gid + "'," +
                          "'" + values.status_log + "'," +
                          "'" + values.remarks.Replace("'", " ") + "'," +
                          "'" + employee_gid + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Designation Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Designation Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }


            
        }

        //Get Active Status Log
        public void DaGetActiveLog(string designation_gid, MdlDesignation objgetsegment)
        {
            try
            {
                msSQL = " SELECT d.designation_type,a.status_log,a.remarks, " +
                    " date_format(a.created_date,'%d-%m-%Y || %h:%i %p') as created_date,concat(c.user_firstname,' ' ,c.user_lastname,'||',c.user_code) as created_by" +
                    " FROM ocs_trn_tdesignationtatuslog a" +
                    " left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    "  left join ocs_mst_tdesignation d on a.designation_gid=d.designation_gid where a.designation_gid='" + designation_gid + "' order by a.designationstatuslog_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getSegment = new List<designation_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getSegment.Add(new designation_list
                        {
                            designation_type = (dr_datarow["designation_type"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            status_log = (dr_datarow["status_log"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                        });
                    }
                    objgetsegment.designation_list = getSegment;
                }
                dt_datatable.Dispose();
                objgetsegment.status = true;

            }
            catch
            {
                objgetsegment.status = false;
            }
        }
    }
}