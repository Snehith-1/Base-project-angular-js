using ems.businessteam.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Web;
using ems.storage.Functions;

namespace ems.businessteam.DataAccess
{
    public class DaMstBDLeadRequestType
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader, objODBCDatareader1, objODBCDatareader2;
        string msSQL, msGetGid, msGetGidREF, msGetcreditops2maker_gid, msGetcreditops2checker_gid, lsopsmappinggid, lsmaster_value;
        int mnResult;

        public void DaGetLeadRequestType(MdlBDLeadRequestType values)

        {
            try
            {
                msSQL = " SELECT leadrequesttype_gid,leadrequesttype_name,lms_code, bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM mar_mst_tleadrequesttype a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.leadrequesttype_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getleadrequesttype_list = new List<leadrequesttype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getleadrequesttype_list.Add(new leadrequesttype_list
                        {
                            leadrequesttype_gid = (dr_datarow["leadrequesttype_gid"].ToString()),
                            leadrequesttype_name = (dr_datarow["leadrequesttype_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    values.leadrequesttype_list = getleadrequesttype_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaCreateLeadRequestType(leadrequesttype values, string employee_gid)
        {
            msSQL = "select leadrequesttype_name from mar_mst_tleadrequesttype where leadrequesttype_name = '" + values.leadrequesttype_name.Replace("'", "\\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Lead request type name Already Exist";
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("LRTY");
                msSQL = " insert into mar_mst_tleadrequesttype(" +
                        " leadrequesttype_gid ," +
                        " lms_code," +
                        " bureau_code," +
                        " leadrequesttype_name ," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "',";
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

                msSQL += "'" + values.leadrequesttype_name.Replace("'", "") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Lead request Type Added Successfully";
                }
                else
                {
                    values.message = "Error Occured While Adding";
                    values.status = false;
                }
            }
        }
        // Edit 

        public void DaEditLeadRequestType(string leadrequesttype_gid, leadrequesttype values)
        {
            try
            {
                msSQL = " SELECT leadrequesttype_gid,leadrequesttype_name,lms_code, bureau_code, status as status FROM mar_mst_tleadrequesttype " +
                        " where leadrequesttype_gid='" + leadrequesttype_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.leadrequesttype_gid = objODBCDatareader["leadrequesttype_gid"].ToString();
                    values.leadrequesttype_name = objODBCDatareader["leadrequesttype_name"].ToString();
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

        public void DaUpdateLeadRequestType(string employee_gid, leadrequesttype values)
        {
            msSQL = "select updated_by, updated_date,leadrequesttype_name from mar_mst_tleadrequesttype where leadrequesttype_gid ='" + values.leadrequesttype_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("LRTL");
                    msSQL = " insert into mar_mst_tleadrequesttypelog(" +
                              " leadrequesttypelog_gid  ," +
                              " leadrequesttype_gid," +
                              " leadrequesttype_name, " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.leadrequesttype_gid + "'," +
                              "'" + objODBCDatareader["leadrequesttype_name"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            msSQL = " update mar_mst_tleadrequesttype set ";
            if (values.lms_code == "" || values.lms_code == null)
            {
                msSQL += " lms_code='',";
            }
            else
            {
                msSQL += " lms_code='" + values.lms_code + "',";
            }
            if (values.bureau_code == "" || values.bureau_code == null)
            {
                msSQL += " bureau_code='',";
            }
            else
            {
                msSQL += " bureau_code='" + values.bureau_code + "',";
            }

            msSQL += " leadrequesttype_name='" + values.leadrequesttype_name.Replace("'", "") + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where leadrequesttype_gid='" + values.leadrequesttype_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Lead request type Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Updating";
            }
        }

        // Status

        public void DaInactiveLeadRequestType(leadrequesttype values, string employee_gid)
        {
            msSQL = " update mar_mst_tleadrequesttype set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where leadrequesttype_gid='" + values.leadrequesttype_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("LTIL");

                msSQL = " insert into mar_mst_tleadrequesttypeinactivelog (" +
                      " leadrequesttypeinactivelog_gid, " +
                      " leadrequesttype_gid," +
                      " leadrequesttype_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.leadrequesttype_gid + "'," +
                      " '" + values.leadrequesttype_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Lead request type Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Lead request type Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaInactiveLeadRequestTypeHistory(leadrequesttypeInactiveHistory values, string leadrequesttype_gid)
        {
            try
            {
                msSQL = " select a.remarks, date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                " from mar_mst_tleadrequesttypeinactivelog a " +
                " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                " where a.leadrequesttype_gid='" + leadrequesttype_gid + "' order by a.leadrequesttypeinactivelog_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getleadrequesttypeinactivehistory_list = new List<leadrequesttypeinactivehistory_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getleadrequesttypeinactivehistory_list.Add(new leadrequesttypeinactivehistory_list
                        {
                            status = (dr_datarow["status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString())
                        });
                    }
                    values.leadrequesttypeinactivehistory_list = getleadrequesttypeinactivehistory_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        // Delete

        public void DaDeleteLeadRequestType(string leadrequesttype_gid, string employee_gid, result values)
        {
            msSQL = " select leadrequesttype_gid from mar_trn_tmarketingcall where leadrequesttype_gid='" + leadrequesttype_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.message = "Can't able to delete Call Type because it is mapped to add Business Development call";
                values.status = false;
                objODBCDatareader.Close();
            }
            else
            {

                msSQL = " select leadrequesttype_name from mar_mst_tleadrequesttype where leadrequesttype_gid='" + leadrequesttype_gid + "'";
                lsmaster_value = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " delete from mar_mst_tleadrequesttype where leadrequesttype_gid ='" + leadrequesttype_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {

                    msGetGid = objcmnfunctions.GetMasterGID("MSTD");
                    msSQL = " insert into mar_mst_tmasterdeletelog(" +
                             "master_gid, " +
                             "master_name, " +
                             "master_value, " +
                             "deleted_by, " +
                             "deleted_date) " +
                             " values(" +
                             "'" + msGetGid + "'," +
                             "'Call Type'," +
                             "'" + lsmaster_value + "'," +
                             "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Lead request type Deleted Successfully..!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }
            }
        }
    }
}