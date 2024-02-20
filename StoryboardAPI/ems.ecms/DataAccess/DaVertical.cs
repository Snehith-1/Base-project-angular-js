using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using System.Net;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.VisualBasic;
using System.Security.Cryptography;
using ems.utilities.Functions;
using ems.ecms.Models;

namespace ems.ecms.DataAccess
{
    /// <summary>
    /// vertical Controller Class containing API methods for accessing the  DataAccess class DaVertical - 
    ///  Vertical - summary, add, update, edit, update log, delete
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    public class DaVertical
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
       
        DataTable dt_datatable;
        string msSQL, msGetGid, msGetGidREF, msGetAPICode;
        int mnResult, GetApiMasterGID;
        string lsmaster_value;


        public void DaPostCreatVvertical(mdlcreateVertical values, string employee_gid)
        {
            msSQL = "select vertical_gid from ocs_mst_tvertical where vertical_name = '" + values.vertical_name.Replace("'", "\\'") + "'";
            string lsdocumentgid = objdbconn.GetExecuteScalar(msSQL);
            if (lsdocumentgid != "")
            {
                //if (lsdocumentgid != values.vertical_gid)
                //{
                    values.message = " This Vertical Already Exists";
                    values.status = false;
                    return;
                //}
            }

            msGetAPICode = objcmnfunctions .GetApiMasterGID("VEAC");
            msGetGid =objcmnfunctions .GetMasterGID("SEGM");
           // msGetGidREF = objcmnfunctions.GetMasterGID("VC");
            msSQL = " insert into ocs_mst_tvertical(" +
                    " vertical_gid," +
                    " vertical_name," +
                    " api_code," +
                    " sequence_curval," +
                    " vertical_code," +
                    " vertical_refno," +
                    " lms_code," +
                    " bureau_code," +
                    " entity_gid," +
                    " entity_name," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.vertical_name.Replace("'", "") + "'," +
                    "'" + msGetAPICode + "'," +
            "'0',";

            if (values.vertical_code == "" || values.vertical_code == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.vertical_code.Replace("'", "") + "',";
            }
            if (values.vertical_refno == "" || values.vertical_refno == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.vertical_refno.Replace("'", "") + "',";
            }
            if (values.lms_code=="" || values.lms_code==null)
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

           msSQL+=  "'" + values.entity_gid.Replace("'", "") + "'," +
                    "'" + values.entity_name.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn .ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Vertical Added Successfully..!!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Adding Vertical.!";
            }
        
        }
        //getVertical
        public void DaGetVertical(MdlVertical objgetsegment)
        {
            try
            {
                msSQL = " SELECT a.vertical_gid,a.vertical_name,a.vertical_code,a.lms_code,a.bureau_code,a.entity_name,a.entity_gid,status_log, a.vertical_refno,a.api_code," +
                    " date_format(a.created_date,'%d-%m-%Y || %h:%i %p') as created_date,concat(c.user_firstname,' ' ,c.user_lastname,'||',c.user_code) as created_by"+
                    " FROM ocs_mst_tvertical a" +
                    " left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid order by a.vertical_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getSegment = new List<vertical_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getSegment.Add(new vertical_list
                        {
                            vertical_gid = (dr_datarow["vertical_gid"].ToString()),
                            vertical_name = (dr_datarow["vertical_name"].ToString()),
                            vertical_code = (dr_datarow["vertical_code"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            entity_name = (dr_datarow["entity_name"].ToString()),
                            entity_gid = (dr_datarow["entity_gid"].ToString()),
                            status_log = (dr_datarow["status_log"].ToString()),
                            vertical_refno = (dr_datarow["vertical_refno"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                        });
                    }
                    objgetsegment.vertical_list = getSegment;
                }
                dt_datatable.Dispose();
                objgetsegment.status = true;

            }
            catch
            {
                objgetsegment.status = false;
            }
        }
        public void DaGetEditVertical(string vertical_gid, verticaledit values)
        {
            try
            {
                msSQL = " select vertical_gid,vertical_name,vertical_code,lms_code,bureau_code,entity_gid,entity_name,status_log, vertical_refno " +
                    " from ocs_mst_tvertical where vertical_gid='" + vertical_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                    values.verticalNameedit = objODBCDatareader["vertical_name"].ToString();
                    values.verticalCodeedit = objODBCDatareader["vertical_code"].ToString();
                    values.bureau_codeedit = objODBCDatareader["bureau_code"].ToString();
                    values.lms_codeedit = objODBCDatareader["lms_code"].ToString();
                    values.entity_name = objODBCDatareader["entity_name"].ToString();
                    values.entity_gid = objODBCDatareader["entity_gid"].ToString();
                    values.status_log = objODBCDatareader["status_log"].ToString();
                    values.vertical_gid = vertical_gid;
                    values.vertical_refno = objODBCDatareader["vertical_refno"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaPostUpdateVertical(string employee_gid, verticaledit values)
        {
            
            msSQL = "select updated_by, updated_date ,vertical_name, vertical_refno from ocs_mst_tvertical where vertical_gid = '" + values.vertical_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            
            msSQL = "select vertical_gid from ocs_mst_tvertical where vertical_name = '" + objODBCDatareader["vertical_name"].ToString() + "'";
            string lsdocumentgid = objdbconn.GetExecuteScalar(msSQL);
            if (lsdocumentgid != "")
            {
                if (lsdocumentgid != values.vertical_gid)
                {
                    values.message = " This Vertical Already Exists";
                    values.status = false;
                    return;
                }
            }

            if (objODBCDatareader.HasRows == true)
            {
               string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_by"].ToString();
                
                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("VLOG");
                    msSQL = " insert into ocs_trn_tauditverticallog(" +
                              " auditverticallog_gid," +
                              " vertical_gid," +
                              " vertical_name, " +
                              " vertical_refno, " +
                              " created_by, " +
                              " created_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.vertical_gid + "'," +
                              "'" + objODBCDatareader["vertical_name"].ToString() + "'," +
                              "'" + objODBCDatareader["vertical_refno"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            msSQL = " update ocs_mst_tvertical set " +
                 " vertical_name='" + values.verticalNameedit + "',";
            if (values.verticalCodeedit == "" || values.verticalCodeedit == null)
            {
                msSQL += " vertical_code='',";
            }
            else
            {
                msSQL += " vertical_code='" + values.verticalCodeedit + "',";
            }
            if (values.vertical_refno == "" || values.vertical_refno == null)
            {
                msSQL += " vertical_refno='',";
            }
            else
            {
                msSQL += " vertical_refno='" + values.vertical_refno + "',";
            }
            if (values.lms_codeedit==""|| values.lms_codeedit==null)
            {
                msSQL += " lms_code='',";
            }
            else
            {
                msSQL += " lms_code='" + values.lms_codeedit + "',";
            }
            if (values.bureau_codeedit == "" || values.bureau_codeedit == null)
            {
                msSQL += " bureau_code='',";
            }
            else
            {
                msSQL += " bureau_code='" + values.bureau_codeedit + "',";
            }
           
                msSQL+= " entity_gid='" + values.entity_gid + "'," +
                 " entity_name='" + values.entity_name + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where vertical_gid='" + values.vertical_gid + "' ";
            mnResult = objdbconn .ExecuteNonQuerySQL(msSQL);
          
            if (mnResult != 0)
            {
              
                values.status = true;
                values.message = "Vertical Updated Successfully..!!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating Vertical.!";
            }
        }

        public void DaPostDeleteVertical(string vertical_gid,string employee_gid, verticaledit values)
        {
            
            msSQL = " select * from ocs_mst_tapplication where vertical_gid='" + vertical_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                values.message = "Can't able to delete Vertical, Because it is tagged to Application Creation";
                values.status = false;
                return;
            }
            else
            {
                msSQL = " select vertical_name from ocs_mst_tvertical where vertical_gid='" + vertical_gid + "'";
                lsmaster_value = objdbconn.GetExecuteScalar(msSQL);
               
                msSQL = " delete from ocs_mst_tvertical where vertical_gid='" + vertical_gid + "'";
            mnResult = objdbconn .ExecuteNonQuerySQL(msSQL);
            
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
                             "'Vertical'," +
                             "'" + lsmaster_value + "'," +
                             "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            else
            {
                values.status = false;
            }
            objODBCDatareader.Close();
            }
        }
        public void DaVerticalStatusUpdate(string employee_gid, verticaledit values)
        {

            msSQL = " update ocs_mst_tvertical set status_log='"+ values.status_log +"',"+
                " remarks='"+ values.remarks.Replace("'", " ") + "',"+
                " updated_by='" + employee_gid + "'," +
                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " where vertical_gid='" + values.vertical_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
           
            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("VLOG");
                msSQL = " insert into ocs_trn_tverticalstatuslog(" +
                          " verticalstatuslog_gid," +
                          " vertical_gid," +
                          " status_log, " +
                          " remarks, " +
                          " created_by, " +
                          " created_date) " +
                          " values(" +
                          "'" + msGetGid + "'," +
                          "'" + values.vertical_gid + "'," +
                          "'" + values.status_log + "'," +
                          "'" + values.remarks.Replace("'", " ") + "'," +
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
        //Get Active Status Log
        public void DaGetActiveLog(string vertical_gid, MdlVertical objgetsegment)
        {
            try
            {
                msSQL = " SELECT a.verticalstatuslog_gid,d.vertical_name,a.status_log,a.remarks, " +
                    " date_format(a.created_date,'%d-%m-%Y || %h:%i %p') as created_date,concat(c.user_firstname,' ' ,c.user_lastname,'||',c.user_code) as created_by" +
                    " FROM ocs_trn_tverticalstatuslog a" +
                    " left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid "+
                    "  left join ocs_mst_tvertical d on a.vertical_gid=d.vertical_gid where a.vertical_gid='" + vertical_gid + "' order by a.verticalstatuslog_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getSegment = new List<vertical_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getSegment.Add(new vertical_list
                        {
                            vertical_name = (dr_datarow["vertical_name"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            status_log = (dr_datarow["status_log"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                        });
                    }
                    objgetsegment.vertical_list = getSegment;
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