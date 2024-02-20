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
    public class DaMstMarketingSourceOfContact
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader, objODBCDatareader1, objODBCDatareader2;
        string msSQL, msGetGid, msGetGidREF, msGetcreditops2maker_gid, msGetcreditops2checker_gid, lsopsmappinggid, lsmaster_value;
        int mnResult;

        public void DaGetMarketingSourceofContact(MdlMstMarketingSourceOfContact objapplication360)

        {
            try
            {
                msSQL = " SELECT marketingsourceofcontact_gid,marketingsourceofcontact_name,lms_code, bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM mar_mst_tmarketingsourceofcontact a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.marketingsourceofcontact_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplication_list = new List<application_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getapplication_list.Add(new application_list
                        {
                            marketingsourceofcontact_gid = (dr_datarow["marketingsourceofcontact_gid"].ToString()),
                            marketingsourceofcontact_name = (dr_datarow["marketingsourceofcontact_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    objapplication360.application_list = getapplication_list;
                }
                dt_datatable.Dispose();
                objapplication360.status = true;
            }
            catch
            {
                objapplication360.status = false;
            }
        }

        public void DaCreateMarketingSourceofContact(application360 values, string employee_gid)
        {
            msSQL = "select marketingsourceofcontact_name from mar_mst_tmarketingsourceofcontact where marketingsourceofcontact_name = '" + values.marketingsourceofcontact_name.Replace("'", "\\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Source Of Contact name Already Exist";
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("BSOC");
                msSQL = " insert into mar_mst_tmarketingsourceofcontact(" +
                        " marketingsourceofcontact_gid ," +
                        " lms_code," +
                        " bureau_code," +
                        " marketingsourceofcontact_name ," +
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

                msSQL += "'" + values.marketingsourceofcontact_name.Replace("'", "") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Source of Contact Added Successfully";
                }
                else
                {
                    values.message = "Error Occured While Adding";
                    values.status = false;
                }
            }
        }
        // Edit 

        public void DaEditMarketingSourceofContact(string marketingsourceofcontact_gid, application360 values)
        {
            try
            {
                msSQL = " SELECT marketingsourceofcontact_gid,marketingsourceofcontact_name,lms_code, bureau_code, status as status FROM mar_mst_tmarketingsourceofcontact " +
                        " where marketingsourceofcontact_gid='" + marketingsourceofcontact_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.marketingsourceofcontact_gid = objODBCDatareader["marketingsourceofcontact_gid"].ToString();
                    values.marketingsourceofcontact_name = objODBCDatareader["marketingsourceofcontact_name"].ToString();
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

        public void DaUpdateMarketingSourceofContact(string employee_gid, application360 values)
        {
            msSQL = "select updated_by, updated_date,marketingsourceofcontact_name from mar_mst_tmarketingsourceofcontact where marketingsourceofcontact_gid ='" + values.marketingsourceofcontact_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("BSOL");
                    msSQL = " insert into mar_mst_tmarketingsourceofcontactlog(" +
                              " sourceofcontactlog_gid  ," +
                              " marketingsourceofcontact_gid," +
                              " marketingsourceofcontact_name, " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.marketingsourceofcontact_gid + "'," +
                              "'" + objODBCDatareader["marketingsourceofcontact_name"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            msSQL = " update mar_mst_tmarketingsourceofcontact set ";
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

            msSQL += " marketingsourceofcontact_name='" + values.marketingsourceofcontact_name.Replace("'", "") + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where marketingsourceofcontact_gid='" + values.marketingsourceofcontact_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Source of Contact Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Updating";
            }
        }

        //Status 

        public void DaInactiveMarketingSourceofContact(application360 values, string employee_gid)
        {
            msSQL = " update mar_mst_tmarketingsourceofcontact set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where marketingsourceofcontact_gid='" + values.marketingsourceofcontact_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("BDOI");

                msSQL = " insert into mar_mst_tmarketingsourceofcontactinactivelog (" +
                      " marketingsourceofcontactinactivelog_gid, " +
                      " marketingsourceofcontact_gid," +
                      " marketingsourceofcontact_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.marketingsourceofcontact_gid + "'," +
                      " '" + values.marketingsourceofcontact_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Source of Contact Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Source of Contact Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaInactiveMarketingSourceofContactHistory(ApplicationInactiveHistory objapplicationhistory, string marketingsourceofcontact_gid)
        {
            try
            {
                msSQL = " select a.remarks, date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                " from mar_mst_tmarketingsourceofcontactinactivelog a " +
                " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                " where a.marketingsourceofcontact_gid='" + marketingsourceofcontact_gid + "' order by a.marketingsourceofcontactinactivelog_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getinactivehistory_list = new List<inactivehistory_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getinactivehistory_list.Add(new inactivehistory_list
                        {
                            status = (dr_datarow["status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString())
                        });
                    }
                    objapplicationhistory.inactivehistory_list = getinactivehistory_list;
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

        public void DaDeleteMarketingSourceofContact(string marketingsourceofcontact_gid, string employee_gid, result values)
        {
            msSQL = " select marketingsourceofcontact_gid from mar_trn_tmarketingcall where marketingsourceofcontact_gid='" + marketingsourceofcontact_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.message = "Can't able to delete Source Of Contact because it is mapped to add Business Development call";
                values.status = false;
                objODBCDatareader.Close();
            }
            else
            {

                msSQL = " select marketingsourceofcontact_name from mar_mst_tmarketingsourceofcontact where marketingsourceofcontact_gid='" + marketingsourceofcontact_gid + "'";
                lsmaster_value = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " delete from mar_mst_tmarketingsourceofcontact where marketingsourceofcontact_gid ='" + marketingsourceofcontact_gid + "'";
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
                             "'Source of Contact'," +
                             "'" + lsmaster_value + "'," +
                             "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Source of Contact Deleted Successfully..!";
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