using ems.hrloan.Models;
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


namespace ems.hrloan.DataAccess
{
    public class DaMstHRLoanTypeofFinancialAssistance
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid;
        int mnResult;

        public void DaGetHRLoanTypeofFinancialAssistance(MdlMstHRLoanTypeofFinancialAssistance objtypeoffinancialassistance)

        {
            try
            {
                msSQL = " SELECT a.hrloantypeoffinancialassistance_gid,a.hrloantypeoffinancialassistance_name,a.lms_code, a.bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, "+ 
                        " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by,d.hrloantenure_name, "+
                        " case when a.status = 'N' then 'Inactive' else 'Active' end as status, "+
                        " case when d.hrloantenure_name is not null then 'Yes' else 'No' end as tenure "+
                        " FROM hrl_mst_thrloantypeoffinancialassistance a "+
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid "+
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid "+
                        " left join hrl_mst_thrloantenure d on d.hrloantypeoffinancialassistance_gid = a.hrloantypeoffinancialassistance_gid "+
                        " order by a.hrloantypeoffinancialassistance_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gettypeoffinancialassistance_list = new List<typeoffinancialassistance_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        gettypeoffinancialassistance_list.Add(new typeoffinancialassistance_list
                        {
                            hrloantypeoffinancialassistance_gid = (dr_datarow["hrloantypeoffinancialassistance_gid"].ToString()),
                            hrloantypeoffinancialassistance_name = (dr_datarow["hrloantypeoffinancialassistance_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            tenure = (dr_datarow["tenure"].ToString()),
                        });
                    }
                    objtypeoffinancialassistance.typeoffinancialassistance_list = gettypeoffinancialassistance_list;
                }
                dt_datatable.Dispose();
                objtypeoffinancialassistance.status = true;
            }
            catch
            {
                objtypeoffinancialassistance.status = false;
            }
        }

        public void DaCreateHRLoanTypeofFinancialAssistance(typeoffinancialassistance values, string employee_gid)
        {
            msSQL = "select hrloantypeoffinancialassistance_name from hrl_mst_thrloantypeoffinancialassistance where hrloantypeoffinancialassistance_name = '" + values.hrloantypeoffinancialassistance_name.Replace("'", @"\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Type of financial assistance already exist";
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("HRFA");
                msSQL = " insert into hrl_mst_thrloantypeoffinancialassistance(" +
                        " hrloantypeoffinancialassistance_gid ," +
                        " lms_code," +
                        " bureau_code," +
                        " hrloantypeoffinancialassistance_name ," +
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
                    msSQL += "'" + values.lms_code.Replace("'", @"\'") + "',";
                }
                if (values.bureau_code == "" || values.bureau_code == null)
                {
                    msSQL += "'',";
                }
                else
                {
                    msSQL += "'" + values.bureau_code.Replace("'", @"\'") + "',";
                }

                msSQL += "'" + values.hrloantypeoffinancialassistance_name.Replace("'", @"\'") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Type of financial assistance added successfully";
                }
                else
                {
                    values.message = "Error occured while adding";
                    values.status = false;
                }
            }
        }
        // Edit 

        public void DaEditHRLoanTypeofFinancialAssistance(string hrloantypeoffinancialassistance_gid, typeoffinancialassistance values)
        {
            try
            {
                msSQL = " SELECT hrloantypeoffinancialassistance_gid,hrloantypeoffinancialassistance_name,lms_code, bureau_code, status as status FROM hrl_mst_thrloantypeoffinancialassistance " +
                        " where hrloantypeoffinancialassistance_gid='" + hrloantypeoffinancialassistance_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.hrloantypeoffinancialassistance_gid = objODBCDatareader["hrloantypeoffinancialassistance_gid"].ToString();
                    values.hrloantypeoffinancialassistance_name = objODBCDatareader["hrloantypeoffinancialassistance_name"].ToString();
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

        public void DaUpdateHRLoanTypeofFinancialAssistance(string employee_gid, typeoffinancialassistance values)
        {
            msSQL = "select updated_by, date_format(updated_date,'%Y-%m-%d %h:%i:%s') as updated_date,hrloantypeoffinancialassistance_name from hrl_mst_thrloantypeoffinancialassistance where hrloantypeoffinancialassistance_gid ='" + values.hrloantypeoffinancialassistance_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("HRFL");
                    msSQL = " insert into hrl_mst_thrloantypeoffinancialassistancelog(" +
                              " hrloantypeoffinancialassistancelog_gid  ," +
                              " hrloantypeoffinancialassistance_gid," +
                              " hrloantypeoffinancialassistance_name, " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.hrloantypeoffinancialassistance_gid + "'," +
                              "'" + objODBCDatareader["hrloantypeoffinancialassistance_name"].ToString().Replace("'", @"\'") + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            msSQL = " update hrl_mst_thrloantypeoffinancialassistance set ";
            if (values.lms_code == "" || values.lms_code == null)
            {
                msSQL += " lms_code='',";
            }
            else
            {
                msSQL += " lms_code='" + values.lms_code.Replace("'", @"\'") + "',";
            }
            if (values.bureau_code == "" || values.bureau_code == null)
            {
                msSQL += " bureau_code='',";
            }
            else
            {
                msSQL += " bureau_code='" + values.bureau_code.Replace("'", @"\'") + "',";
            }

            msSQL += " hrloantypeoffinancialassistance_name='" + values.hrloantypeoffinancialassistance_name.Replace("'", @"\'") + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where hrloantypeoffinancialassistance_gid='" + values.hrloantypeoffinancialassistance_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Type of financial assistance updated successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error occured while updating";
            }
        }

        //Status 

        public void DaInactiveHRLoanTypeofFinancialAssistance(typeoffinancialassistance values, string employee_gid)
        {
            msSQL = " update hrl_mst_thrloantypeoffinancialassistance set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", @"\'") + "'" +
                    " where hrloantypeoffinancialassistance_gid='" + values.hrloantypeoffinancialassistance_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("HRFI");

                msSQL = " insert into hrl_mst_thrloantypeoffinancialassistanceinactivelog (" +
                      " hrloantypeoffinancialassistanceinactivelog_gid, " +
                      " hrloantypeoffinancialassistance_gid," +
                      " hrloantypeoffinancialassistance_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.hrloantypeoffinancialassistance_gid + "'," +
                      " '" + values.hrloantypeoffinancialassistance_name.Replace("'", @"\'") + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", @"\'") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Type of financial assistance inactivated successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Type of financial assistance activated successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error occurred";
            }
        }

        public void DaInactiveHRLoanTypeofFinancialAssistanceHistory(TypeofFinancialAssistanceInactiveHistory objapplicationhistory, string hrloantypeoffinancialassistance_gid)
        {
            try
            {
                msSQL = " select a.remarks, date_format(a.updated_date,'%Y-%m-%d %h:%i:%s') as updated_date, " +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                " from hrl_mst_thrloantypeoffinancialassistanceinactivelog a " +
                " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                " where a.hrloantypeoffinancialassistance_gid='" + hrloantypeoffinancialassistance_gid + "' order by a.hrloantypeoffinancialassistanceinactivelog_gid desc ";

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

        public void DaDeleteHRLoanTypeofFinancialAssistance(string hrloantypeoffinancialassistance_gid, string employee_gid, result values)
        {
            msSQL = " select fintype_gid from hrl_trn_trequest where fintype_gid ='" + hrloantypeoffinancialassistance_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.message = "This Financial Assistance is mapped, so..you can't delete it..!";
                values.status = false;
                objODBCDatareader.Close();
            }
            else
            {               
            msSQL = " delete from hrl_mst_thrloantypeoffinancialassistance where hrloantypeoffinancialassistance_gid ='" + hrloantypeoffinancialassistance_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {               
                values.status = true;
                values.message = "Type of financial assistance deleted successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error occured..!";
            }
            }
        }  
    }
}