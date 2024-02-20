﻿using ems.businessteam.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;


namespace ems.businessteam.DataAccess
{
    public class DaMstEnquiryRequire
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader, objODBCDatareader1, objODBCDatareader2;
        string msSQL, msGetGid, lsaudittype_value, lslms_code, lsbureau_code, lsaudittype_code;
        int mnResult;
        public void DaGetEnquiryRequire(MdlMstEnquiryRequire values)
        {
            try
            {
                msSQL = " SELECT a.enquiryrequire_gid,a.enquiryrequire_name, a.lms_code, a.bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM ldm_mst_tenquiryrequire a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.enquiryrequire_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getenquiryrequire_list = new List<enquiryrequire_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getenquiryrequire_list.Add(new enquiryrequire_list
                        {
                            enquiryrequire_gid = (dr_datarow["enquiryrequire_gid"].ToString()),
                            enquiryrequire_name = (dr_datarow["enquiryrequire_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    values.enquiryrequire_list = getenquiryrequire_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaCreateEnquiryRequire(MdlMstEnquiryRequire values, string employee_gid)
        {
            msSQL = "select enquiryrequire_name from ldm_mst_tenquiryrequire where enquiryrequire_name = '" + values.enquiryrequire_name.Replace("'", "\\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Require Already Exist";
            }
            else
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



                msGetGid = objcmnfunctions.GetMasterGID("ENQR");

                msSQL = " insert into ldm_mst_tenquiryrequire(" +
                        " enquiryrequire_gid," +
                        " enquiryrequire_name," +
                        " lms_code," +
                        " bureau_code," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.enquiryrequire_name.Replace("'", "") + "'," +
                        "'" + lslms_code + "'," +
                        "'" + lsbureau_code + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Enquiry Require Added Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred While Adding";
                }
            }
        }

        public void DaEditEnquiryRequire(string enquiryrequire_gid, MdlMstEnquiryRequire values)
        {
            try
            {
                msSQL = " SELECT enquiryrequire_gid,enquiryrequire_name,lms_code, bureau_code, status as Status FROM ldm_mst_tenquiryrequire where enquiryrequire_gid='" + enquiryrequire_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.enquiryrequire_gid = objODBCDatareader["enquiryrequire_gid"].ToString();
                    values.enquiryrequire_name = objODBCDatareader["enquiryrequire_name"].ToString();
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

        public void DaUpdateEnquiryRequire(string employee_gid, MdlMstEnquiryRequire values)
        {


            msSQL = " update ldm_mst_tenquiryrequire set " +
                 " enquiryrequire_name='" + values.enquiryrequire_name.Replace("'", "") + "'," +
                 " lms_code='" + values.lms_code + "'," +
                 " bureau_code='" + values.bureau_code + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where enquiryrequire_gid='" + values.enquiryrequire_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("ENQL");

                msSQL = " insert into ldm_mst_tenquiryrequirelog (" +
                       " enquiryrequirelog_gid, " +
                       " enquiryrequire_gid, " +
                       " enquiryrequire_name," +
                       " updated_by," +
                       " updated_date) " +
                       " values (" +
                       " '" + msGetGid + "'," +
                       " '" + values.enquiryrequire_gid + "'," +
                       " '" + values.enquiryrequire_name.Replace("'", "") + "'," +
                       " '" + employee_gid + "'," +
                       " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Enquiry Require Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }

        public void DaInactiveEnquiryRequire(MdlMstEnquiryRequire values, string employee_gid)
        {
            msSQL = " update ldm_mst_tenquiryrequire set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where enquiryrequire_gid='" + values.enquiryrequire_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("ENIL");

                msSQL = " insert into ldm_mst_tenquiryrequireinactivelog (" +
                      " enquiryrequireinactivelog_gid, " +
                      " enquiryrequire_gid," +
                      " enquiryrequire_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.enquiryrequire_gid + "'," +
                      " '" + values.enquiryrequire_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Require Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Require Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaDeleteEnquiryRequire(string enquiryrequire_gid, string employee_gid, MdlMstEnquiryRequire values)
        {
            msSQL = " select enquiryrequire_gid from mar_trn_tmarketingcall where enquiryrequire_gid='" + enquiryrequire_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.message = "Can't able to delete Require because it is mapped to Enquiry Call";
                values.status = false;
                objODBCDatareader.Close();
            }
            else
            {

                msSQL = " select enquiryrequire_name from ldm_mst_tenquiryrequire where enquiryrequire_gid='" + enquiryrequire_gid + "'";
                lsaudittype_value = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " delete from ldm_mst_tenquiryrequire where enquiryrequire_gid='" + enquiryrequire_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Require Deleted Successfully..!";

                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }
            }
        }


        public void DaEnquiryRequireInactiveLogview(string enquiryrequire_gid, MdlMstEnquiryRequire values)
        {
            try
            {
                msSQL = " SELECT a.enquiryrequire_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM ldm_mst_tenquiryrequireinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.enquiryrequire_gid ='" + enquiryrequire_gid + "' order by a.enquiryrequireinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getenquiryrequire_list = new List<enquiryrequire_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getenquiryrequire_list.Add(new enquiryrequire_list
                        {
                            enquiryrequire_gid = (dr_datarow["enquiryrequire_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.enquiryrequire_list = getenquiryrequire_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetEnquiryRequireActive(MdlMstEnquiryRequire values)
        {
            try
            {
                msSQL = "select enquiryrequire_gid,enquiryrequire_name from ldm_mst_tenquiryrequire where status ='Y' order by created_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getenquiryrequire_list = new List<enquiryrequire_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getenquiryrequire_list.Add(new enquiryrequire_list
                        {
                            enquiryrequire_gid = (dr_datarow["enquiryrequire_gid"].ToString()),
                            enquiryrequire_name = (dr_datarow["enquiryrequire_name"].ToString()),
                        });
                    }
                    values.enquiryrequire_list = getenquiryrequire_list;
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