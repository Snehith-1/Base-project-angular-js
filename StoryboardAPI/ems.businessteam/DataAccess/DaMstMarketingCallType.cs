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
    public class DaMstMarketingCallType
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader, objODBCDatareader1, objODBCDatareader2;
        string msSQL, msGetGid, msGetGidREF, msGetcreditops2maker_gid, msGetcreditops2checker_gid, lsopsmappinggid, lsmaster_value;
        int mnResult;

        public void DaGetCreateMarketingCallType(MdlMstMarketingCallType objapplication360)

        {
            try
            {
                msSQL = " SELECT marketingcalltype_gid,marketingcalltype_name,lms_code, bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM mar_mst_tmarketingcalltype a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.marketingcalltype_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmarketingcalltype_list = new List<marketingcalltype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmarketingcalltype_list.Add(new marketingcalltype_list
                        {
                            marketingcalltype_gid = (dr_datarow["marketingcalltype_gid"].ToString()),
                            marketingcalltype_name = (dr_datarow["marketingcalltype_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    objapplication360.marketingcalltype_list = getmarketingcalltype_list;
                }
                dt_datatable.Dispose();
                objapplication360.status = true;
            }
            catch
            {
                objapplication360.status = false;
            }
        }

        public void DaCreateMarketingCallType(marketingcalltype values, string employee_gid)
        {
            msSQL = "select marketingcalltype_name from mar_mst_tmarketingcalltype where marketingcalltype_name = '" + values.marketingcalltype_name.Replace("'", "\\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Call Type name Already Exist";
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("BDCT");
                msSQL = " insert into mar_mst_tmarketingcalltype(" +
                        " marketingcalltype_gid ," +
                        " lms_code," +
                        " bureau_code," +
                        " marketingcalltype_name ," +
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

                msSQL += "'" + values.marketingcalltype_name.Replace("'", "") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Call Type Added Successfully";
                }
                else
                {
                    values.message = "Error Occured While Adding";
                    values.status = false;
                }
            }
        }
        // Edit 

        public void DaEditMarketingCallType(string marketingcalltype_gid, marketingcalltype values)
        {
            try
            {
                msSQL = " SELECT marketingcalltype_gid,marketingcalltype_name,lms_code, bureau_code, status as status FROM mar_mst_tmarketingcalltype " +
                        " where marketingcalltype_gid='" + marketingcalltype_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.marketingcalltype_gid = objODBCDatareader["marketingcalltype_gid"].ToString();
                    values.marketingcalltype_name = objODBCDatareader["marketingcalltype_name"].ToString();
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

        public void DaUpdateMarketingCallType(string employee_gid, marketingcalltype values)
        {
            msSQL = "select updated_by, updated_date,marketingcalltype_name from mar_mst_tmarketingcalltype where marketingcalltype_gid ='" + values.marketingcalltype_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("BCTL");
                    msSQL = " insert into mar_mst_tmarketingcalltypelog(" +
                              " calltypelog_gid  ," +
                              " marketingcalltype_gid," +
                              " marketingcalltype_name, " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.marketingcalltype_gid + "'," +
                              "'" + objODBCDatareader["marketingcalltype_name"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            msSQL = " update mar_mst_tmarketingcalltype set ";
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

            msSQL += " marketingcalltype_name='" + values.marketingcalltype_name.Replace("'", "") + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where marketingcalltype_gid='" + values.marketingcalltype_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Call Type Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Updating";
            }
        }

        // Status

        public void DaInactiveMarketingCallType(marketingcalltype values, string employee_gid)
        {
            msSQL = " update mar_mst_tmarketingcalltype set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where marketingcalltype_gid='" + values.marketingcalltype_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("BCTI");

                msSQL = " insert into mar_mst_tmarketingcalltypeinactivelog (" +
                      " marketingcalltypeinactivelog_gid, " +
                      " marketingcalltype_gid," +
                      " marketingcalltype_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.marketingcalltype_gid + "'," +
                      " '" + values.marketingcalltype_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Call Type Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Call Type Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaInactiveMarketingCallTypeHistory(MarketingCallTypeInactiveHistory objapplicationhistory, string marketingcalltype_gid)
        {
            try
            {
                msSQL = " select a.remarks, date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                " from mar_mst_tmarketingcalltypeinactivelog a " +
                " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                " where a.marketingcalltype_gid='" + marketingcalltype_gid + "' order by a.marketingcalltypeinactivelog_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmarketinginactivehistory_list = new List<marketinginactivehistory_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmarketinginactivehistory_list.Add(new marketinginactivehistory_list
                        {
                            status = (dr_datarow["status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString())
                        });
                    }
                    objapplicationhistory.marketinginactivehistory_list = getmarketinginactivehistory_list;
                }
                dt_datatable.Dispose();
                objapplicationhistory.status = true;
            }
            catch
            {
                objapplicationhistory.status = false;
            }
        }

        // Delete

        public void DaDeleteMarketingCallType(string marketingcalltype_gid, string employee_gid, result values)
        {
            msSQL = " select marketingcalltype_gid from mar_trn_tmarketingcall where marketingcalltype_gid='" + marketingcalltype_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.message = "Can't able to delete Call Type because it is mapped to add Business Development call";
                values.status = false;
                objODBCDatareader.Close();
            }
            else
            {

                msSQL = " select marketingcalltype_name from mar_mst_tmarketingcalltype where marketingcalltype_gid='" + marketingcalltype_gid + "'";
                lsmaster_value = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " delete from mar_mst_tmarketingcalltype where marketingcalltype_gid ='" + marketingcalltype_gid + "'";
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
                    values.message = "Call Type Deleted Successfully..!";
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