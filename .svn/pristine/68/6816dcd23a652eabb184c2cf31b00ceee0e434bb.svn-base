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
    public class DaMstMarketingCallReceivedNumber
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader, objODBCDatareader1, objODBCDatareader2;
        string msSQL, msGetGid, msGetGidREF, msGetcreditops2maker_gid, msGetcreditops2checker_gid, lsopsmappinggid, lsmaster_value;
        int mnResult;

        public void DaGetMarketingCallReceivedNumber(MdlMstMarketingCallReceivedNumber objapplication360)
        {
            try
            {
                msSQL = " SELECT marketingcallreceivednumber_gid,marketingcallreceivednumber_name,lms_code, bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM mar_mst_tmarketingcallreceivednumber a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.marketingcallreceivednumber_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmarketingcallreceivednumber_list = new List<marketingcallreceivednumber_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmarketingcallreceivednumber_list.Add(new marketingcallreceivednumber_list
                        {
                            marketingcallreceivednumber_gid = (dr_datarow["marketingcallreceivednumber_gid"].ToString()),
                            marketingcallreceivednumber_name = (dr_datarow["marketingcallreceivednumber_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    objapplication360.marketingcallreceivednumber_list = getmarketingcallreceivednumber_list;
                }
                dt_datatable.Dispose();
                objapplication360.status = true;
            }
            catch
            {
                objapplication360.status = false;
            }
        }

        public void DaCreateMarketingCallReceivedNumber(marketingcallreceivednumber values, string employee_gid)
        {
            msSQL = "select marketingcallreceivednumber_name from mar_mst_tmarketingcallreceivednumber where marketingcallreceivednumber_name = '" + values.marketingcallreceivednumber_name.Replace("'", "\\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "call receivednumber name Already Exist";
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("BDRN");
                msSQL = " insert into mar_mst_tmarketingcallreceivednumber(" +
                        " marketingcallreceivednumber_gid ," +
                        " lms_code," +
                        " bureau_code," +
                        " marketingcallreceivednumber_name," +
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

                msSQL += "'" + values.marketingcallreceivednumber_name.Replace("'", "") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Call Received Number Added Successfully";
                }
                else
                {
                    values.message = "Error Occured While Adding";
                    values.status = false;
                }
            }
        }
        // Edit 

        public void DaEditMarketingCallReceivedNumber(string marketingcallreceivednumber_gid, marketingcallreceivednumber values)
        {
            try
            {
                msSQL = " SELECT marketingcallreceivednumber_gid,marketingcallreceivednumber_name,lms_code, bureau_code, status as status FROM mar_mst_tmarketingcallreceivednumber " +
                        " where marketingcallreceivednumber_gid='" + marketingcallreceivednumber_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.marketingcallreceivednumber_gid = objODBCDatareader["marketingcallreceivednumber_gid"].ToString();
                    values.marketingcallreceivednumber_name = objODBCDatareader["marketingcallreceivednumber_name"].ToString();
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

        public void DaUpdateMarketingCallReceivedNumber(string employee_gid, marketingcallreceivednumber values)
        {
            msSQL = "select updated_by, updated_date,marketingcallreceivednumber_name from mar_mst_tmarketingcallreceivednumber where marketingcallreceivednumber_gid ='" + values.marketingcallreceivednumber_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("BDCL");
                    msSQL = " insert into mar_mst_tmarketingcallreceivednumberlog(" +
                              " callreceivednumberlog_gid  ," +
                              " marketingcallreceivednumber_gid," +
                              " marketingcallreceivednumber_name, " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.marketingcallreceivednumber_gid + "'," +
                              "'" + objODBCDatareader["marketingcallreceivednumber_name"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            msSQL = " update mar_mst_tmarketingcallreceivednumber set ";
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

            msSQL += " marketingcallreceivednumber_name='" + values.marketingcallreceivednumber_name.Replace("'", "") + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where marketingcallreceivednumber_gid='" + values.marketingcallreceivednumber_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Call Received Number Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Updating";
            }
        }

        // Status

        public void DaInactiveMarketingCallReceivedNumber(marketingcallreceivednumber values, string employee_gid)
        {
            msSQL = " update mar_mst_tmarketingcallreceivednumber set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where marketingcallreceivednumber_gid='" + values.marketingcallreceivednumber_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("BDRI");

                msSQL = " insert into mar_mst_tmarketingcallreceivednumberinactivelog (" +
                      " marketingcallreceivednumberinactivelog_gid, " +
                      " marketingcallreceivednumber_gid," +
                      " marketingcallreceivednumber_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.marketingcallreceivednumber_gid + "'," +
                      " '" + values.marketingcallreceivednumber_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Call Received Number Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Call Received Number Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaInactiveMarketingCallReceivedNumberHistory(MarketingCallReceivedNumberInactiveHistory objapplicationhistory, string marketingcallreceivednumber_gid)
        {
            try
            {
                msSQL = " select a.remarks, date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                " from mar_mst_tmarketingcallreceivednumberinactivelog a " +
                " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                " where a.marketingcallreceivednumber_gid='" + marketingcallreceivednumber_gid + "' order by a.marketingcallreceivednumberinactivelog_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmarketingcallreceivednumberinactivehistory_list = new List<marketingcallreceivednumberinactivehistory_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmarketingcallreceivednumberinactivehistory_list.Add(new marketingcallreceivednumberinactivehistory_list
                        {
                            status = (dr_datarow["status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString())
                        });
                    }
                    objapplicationhistory.marketingcallreceivednumberinactivehistory_list = getmarketingcallreceivednumberinactivehistory_list;
                }
                dt_datatable.Dispose();
                objapplicationhistory.status = true;
            }
            catch
            {
                objapplicationhistory.status = false;
            }
        }

        //Delete

        public void DaDeleteMarketingCallReceivedNumber(string marketingcallreceivednumber_gid, string employee_gid, result values)
        {
            msSQL = " select marketingcallreceivednumber_gid from mar_trn_tmarketingcall where marketingcallreceivednumber_gid='" + marketingcallreceivednumber_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.message = "Can't able to delete Call received Number because it is mapped to add marketing call";
                values.status = false;
                objODBCDatareader.Close();
            }
            else
            {
                msSQL = " select marketingcallreceivednumber_name from mar_mst_tmarketingcallreceivednumber where marketingcallreceivednumber_gid='" + marketingcallreceivednumber_gid + "'";
                lsmaster_value = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " delete from mar_mst_tmarketingcallreceivednumber where marketingcallreceivednumber_gid='" + marketingcallreceivednumber_gid + "'";
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
                             "'Call Received Number'," +
                             "'" + lsmaster_value + "'," +
                             "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Call Received Number Deleted Successfully..!";
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