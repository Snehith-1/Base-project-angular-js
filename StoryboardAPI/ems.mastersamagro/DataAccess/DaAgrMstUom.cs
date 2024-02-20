using ems.mastersamagro.Models;
using ems.utilities.Functions;
using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.Odbc;
using System.Configuration;
using ems.storage.Functions;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System.Globalization;
using OfficeOpenXml;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;
using System.Linq;

namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess will provide access to various functionalities for uom master 
    /// </summary>
    /// <remarks>Written by Sherin Augusta</remarks>
    public class DaAgrMstUom
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader;
        HttpPostedFile httpPostedFile;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGetAPICode;
        int mnResult;

        public void DaGetuom(UomList objvalues)
        {
            try
            {
                msSQL = " SELECT uom_gid,uom_name,lms_code, bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, api_code, " +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM agr_mst_tuom a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.uom_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getUom_list = new List<Uomdtl>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getUom_list.Add(new Uomdtl
                        {
                            uom_gid = (dr_datarow["uom_gid"].ToString()),
                            uom_name = (dr_datarow["uom_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            uom_status = (dr_datarow["status"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                        });
                    }
                    objvalues.Uomdtl = getUom_list;
                }
                dt_datatable.Dispose();
                objvalues.status = true;
            }
            catch
            {
                objvalues.status = false;
            }
        }

        public void DaCreateuom(Uomdtl values, string employee_gid)
        {
            if (values.lms_code == null || values.lms_code == "")
                values.lms_code = "";
            else
                values.lms_code = values.lms_code.Replace("'", "");
            if (values.bureau_code == null || values.bureau_code == "")
                values.bureau_code = "";
            else
                values.bureau_code = values.bureau_code.Replace("'", "");

            msGetAPICode = objcmnfunctions.GetApiMasterGID("UOMC");
            msGetGid = objcmnfunctions.GetMasterGID("MUOM");
            msSQL = " insert into agr_mst_tuom(" +
                    " uom_gid," +
                    " api_code," +
                    " uom_name," +
                    " lms_code," +
                    " bureau_code," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + msGetAPICode + "'," +
                    "'" + values.uom_name.Replace("'", "") + "'," +
                    "'" + values.lms_code + "'," +
                    "'" + values.bureau_code + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "UOM Details are Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Adding";
            }
        }

        public void DaEdituom(string uom_gid, Uomdtl values)
        {
            try
            {
                msSQL = " SELECT uom_gid,uom_name,lms_code, bureau_code, status as Status FROM agr_mst_tuom where uom_gid='" + uom_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.uom_gid = objODBCDatareader["uom_gid"].ToString();
                    values.uom_name = objODBCDatareader["uom_name"].ToString();
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    values.uom_status = objODBCDatareader["Status"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdateuom(string employee_gid, Uomdtl values)
        {
            if (values.lms_code == null || values.lms_code == "")
                values.lms_code = "";
            else
                values.lms_code = values.lms_code.Replace("'", "");
            if (values.bureau_code == null || values.bureau_code == "")
                values.bureau_code = "";
            else
                values.bureau_code = values.bureau_code.Replace("'", "");

            msSQL = " update agr_mst_tuom set " +
                 " uom_name='" + values.uom_name.Replace("'", "") + "'," +
                 " lms_code='" + values.lms_code + "'," +
                 " bureau_code='" + values.bureau_code + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where uom_gid='" + values.uom_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("UOML");

                msSQL = " insert into agr_mst_tuomlog (" +
                       " uomlog_gid, " +
                       " uom_gid, " +
                       " uom_name," +
                       " updated_by," +
                       " updated_date) " +
                       " values (" +
                       " '" + msGetGid + "'," +
                       " '" + values.uom_gid + "'," +
                       " '" + values.uom_name.Replace("'", "") + "'," +
                       " '" + employee_gid + "'," +
                       " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "UOM Details are Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }

        public void DaInactiveuom(Uomdtl values, string employee_gid)
        {
            msSQL = " update agr_mst_tuom set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where uom_gid='" + values.uom_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("IUOM");

                msSQL = " insert into agr_mst_tuomstatuslog (" +
                      " uomstatuslog_gid, " +
                      " uom_gid," +
                      " status," +
                      " uom_name, " +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.uom_gid + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.uom_name + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == "N")
                {
                    values.status = true;
                    values.message = "UOM Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "UOM Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaDeleteuom(string uom_gid, string employee_gid, result values)
        {
            
                msSQL = " delete from agr_mst_tuom where uom_gid='" + uom_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "UOM Deleted Successfully..!"; 
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }
            //}
        }

        public void DauomInactiveLogview(string uom_gid, UomList values)
        {
            try
            {
                msSQL = " SELECT uom_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM agr_mst_tuomstatuslog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where uom_gid ='" + uom_gid + "' order by a.uomstatuslog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getUomdtl_list = new List<Uomdtl>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getUomdtl_list.Add(new Uomdtl
                        {
                            uom_gid = (dr_datarow["uom_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            uom_status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.Uomdtl = getUomdtl_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetuomList(UomList objvalues)
        {
            try
            {
                msSQL = " select uom_gid,uom_name from agr_mst_tuom where status='Y' order by uom_name asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getUom_list = new List<Uomdtl>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getUom_list.Add(new Uomdtl
                        {
                            uom_gid = (dr_datarow["uom_gid"].ToString()),
                            uom_name = (dr_datarow["uom_name"].ToString()), 
                        });
                    }
                    objvalues.Uomdtl = getUom_list;
                }
                dt_datatable.Dispose();
                objvalues.status = true;
            }
            catch
            {
                objvalues.status = false;
            }
        }
    }
}