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
    public class DaMstMarketingTelecallingFunction
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader, objODBCDatareader1, objODBCDatareader2;
        string msSQL, msGetGid, msGetGidREF, msGetcreditops2maker_gid, msGetcreditops2checker_gid, lsopsmappinggid, lsmaster_value;
        int mnResult;

        public void DaGetMarketingTelecallingFunction(MdlMstMarketingTelecallingFunction objapplication360)
        {
            try
            {
                msSQL = " SELECT marketingtelecallingfunction_gid,marketingtelecallingfunction_name,lms_code, bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM mar_mst_tmarketingtelecallingfunction a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.marketingtelecallingfunction_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmarketingtelecallingfunction_list = new List<marketingtelecallingfunction_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmarketingtelecallingfunction_list.Add(new marketingtelecallingfunction_list
                        {
                            marketingtelecallingfunction_gid = (dr_datarow["marketingtelecallingfunction_gid"].ToString()),
                            marketingtelecallingfunction_name = (dr_datarow["marketingtelecallingfunction_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    objapplication360.marketingtelecallingfunction_list = getmarketingtelecallingfunction_list;
                }
                dt_datatable.Dispose();
                objapplication360.status = true;
            }
            catch
            {
                objapplication360.status = false;
            }
        }
        public void DaCreateMarketingTelecallingFunction(marketingtelecallingfunction values, string employee_gid)
        {
            msSQL = "select marketingtelecallingfunction_name from mar_mst_tmarketingtelecallingfunction where marketingtelecallingfunction_name = '" + values.marketingtelecallingfunction_name.Replace("'", "\\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Business Development Function name Already Exist";
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("BDFG");
                msSQL = " insert into mar_mst_tmarketingtelecallingfunction(" +
                        " marketingtelecallingfunction_gid ," +
                        " lms_code," +
                        " bureau_code," +
                        " marketingtelecallingfunction_name," +
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

                msSQL += "'" + values.marketingtelecallingfunction_name.Replace("'", "") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Business Development Function Added Successfully";
                }
                else
                {
                    values.message = "Error Occured While Adding";
                    values.status = false;
                }
            }
        }
        // Edit 

        public void DaEditMarketingTelecallingFunction(string marketingtelecallingfunction_gid, marketingtelecallingfunction values)
        {
            try
            {
                msSQL = " SELECT marketingtelecallingfunction_gid,marketingtelecallingfunction_name,lms_code, bureau_code, status as status FROM mar_mst_tmarketingtelecallingfunction " +
                        " where marketingtelecallingfunction_gid='" + marketingtelecallingfunction_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.marketingtelecallingfunction_gid = objODBCDatareader["marketingtelecallingfunction_gid"].ToString();
                    values.marketingtelecallingfunction_name = objODBCDatareader["marketingtelecallingfunction_name"].ToString();
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

        public void DaUpdateMarketingTelecallingFunction(string employee_gid, marketingtelecallingfunction values)
        {
            msSQL = "select updated_by, updated_date,marketingtelecallingfunction_name from mar_mst_tmarketingtelecallingfunction where marketingtelecallingfunction_gid ='" + values.marketingtelecallingfunction_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("BDFL");
                    msSQL = " insert into mar_mst_tmarketingtelecallingfunctionlog(" +
                              " telecallingfunctionlog_gid  ," +
                              " marketingtelecallingfunction_gid," +
                              " marketingtelecallingfunction_name, " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.marketingtelecallingfunction_gid + "'," +
                              "'" + objODBCDatareader["marketingtelecallingfunction_name"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            msSQL = " update mar_mst_tmarketingtelecallingfunction set ";
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

            msSQL += " marketingtelecallingfunction_name='" + values.marketingtelecallingfunction_name.Replace("'", "") + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where marketingtelecallingfunction_gid='" + values.marketingtelecallingfunction_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Business Development Function Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Updating";
            }
        }
        // Status

        public void DaInactiveMarketingTelecallingFunction(marketingtelecallingfunction values, string employee_gid)
        {
            msSQL = " update mar_mst_tmarketingtelecallingfunction set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where marketingtelecallingfunction_gid='" + values.marketingtelecallingfunction_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("BDFI");

                msSQL = " insert into mar_mst_tmarketingtelecallingfunctioninactivelog (" +
                      " marketingtelecallingfunctioninactivelog_gid, " +
                      " marketingtelecallingfunction_gid," +
                      " marketingtelecallingfunction_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.marketingtelecallingfunction_gid + "'," +
                      " '" + values.marketingtelecallingfunction_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Business Development Function Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Business Development Function Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaInactiveMarketingTelecallingFunctionHistory(MarketingTelecallingFunctionInactiveHistory objapplicationhistory, string marketingtelecallingfunction_gid)
        {
            try
            {
                msSQL = " select a.remarks, date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                " from mar_mst_tmarketingtelecallingfunctioninactivelog a " +
                " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                " where a.marketingtelecallingfunction_gid='" + marketingtelecallingfunction_gid + "' order by a.marketingtelecallingfunctioninactivelog_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmarketingtelecallinginactivehistory_list = new List<marketingtelecallinginactivehistory_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmarketingtelecallinginactivehistory_list.Add(new marketingtelecallinginactivehistory_list
                        {
                            status = (dr_datarow["status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString())
                        });
                    }
                    objapplicationhistory.marketingtelecallinginactivehistory_list = getmarketingtelecallinginactivehistory_list;
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

        public void DaDeleteMarketingTelecallingFunction(string marketingtelecallingfunction_gid, string employee_gid, result values)
        {

            msSQL = " select marketingtelecallingfunction_gid from mar_trn_tmarketingcall where marketingtelecallingfunction_gid='" + marketingtelecallingfunction_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.message = "Can't able to delete Business Development Function because it is mapped to add Business Development call";
                values.status = false;
                objODBCDatareader.Close();
            }
            else
            {

                msSQL = " select marketingtelecallingfunction_name from mar_mst_tmarketingtelecallingfunction where marketingtelecallingfunction_gid='" + marketingtelecallingfunction_gid + "'";
                lsmaster_value = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " delete from mar_mst_tmarketingtelecallingfunction where marketingtelecallingfunction_gid='" + marketingtelecallingfunction_gid + "'";
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
                             "'Business Function'," +
                             "'" + lsmaster_value + "'," +
                             "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Business Development Function Deleted Successfully..!";
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