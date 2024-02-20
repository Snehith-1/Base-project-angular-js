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
    public class DaMstHRLoanSeverity
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid;
        int mnResult;

        public void DaGetHRLoanSeverity(MdlMstHRLoanSeverity objhrloanseverity)

        {
            try
            {
                msSQL = " SELECT hrloanseverity_gid,hrloanseverity_name,lms_code, bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM hrl_mst_thrloanseverity a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.hrloanseverity_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gethrloanseverity_list = new List<hrloanseverity_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        gethrloanseverity_list.Add(new hrloanseverity_list
                        {
                            hrloanseverity_gid = (dr_datarow["hrloanseverity_gid"].ToString()),
                            hrloanseverity_name = (dr_datarow["hrloanseverity_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    objhrloanseverity.hrloanseverity_list = gethrloanseverity_list;
                }
                dt_datatable.Dispose();
                objhrloanseverity.status = true;
            }
            catch
            {
                objhrloanseverity.status = false;
            }
        }

        public void DaCreateHRLoanSeverity(hrloanseverity values, string employee_gid)
        {
            msSQL = "select hrloanseverity_name from hrl_mst_thrloanseverity where hrloanseverity_name = '" + values.hrloanseverity_name.Replace("'", @"\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Severity name Already Exist";
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("HRSV");
                msSQL = " insert into hrl_mst_thrloanseverity(" +
                        " hrloanseverity_gid ," +
                        " lms_code," +
                        " bureau_code," +
                        " hrloanseverity_name ," +
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

                msSQL += "'" + values.hrloanseverity_name.Replace("'", @"\'") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Severity added successfully";
                }
                else
                {
                    values.message = "Error occured while adding";
                    values.status = false;
                }
            }
        }
        // Edit 

        public void DaEditHRLoanSeverity(string hrloanseverity_gid, hrloanseverity values)
        {
            try
            {
                msSQL = " SELECT hrloanseverity_gid,hrloanseverity_name,lms_code, bureau_code, status as status FROM hrl_mst_thrloanseverity " +
                        " where hrloanseverity_gid='" + hrloanseverity_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.hrloanseverity_gid = objODBCDatareader["hrloanseverity_gid"].ToString();
                    values.hrloanseverity_name = objODBCDatareader["hrloanseverity_name"].ToString();
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

        public void DaUpdateHRLoanSeverity(string employee_gid, hrloanseverity values)
        {
            msSQL = "select updated_by, date_format(updated_date,'%Y-%m-%d %h:%i:%s') as updated_date,hrloanseverity_name from hrl_mst_thrloanseverity where hrloanseverity_gid ='" + values.hrloanseverity_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("HRSL");
                    msSQL = " insert into hrl_mst_thrloanseveritylog(" +
                              " hrloanseveritylog_gid  ," +
                              " hrloanseverity_gid," +
                              " hrloanseverity_name, " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.hrloanseverity_gid + "'," +
                              "'" + objODBCDatareader["hrloanseverity_name"].ToString().Replace("'", @"\'") + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            msSQL = " update hrl_mst_thrloanseverity set ";
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

            msSQL += " hrloanseverity_name='" + values.hrloanseverity_name.Replace("'", @"\'") + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where hrloanseverity_gid='" + values.hrloanseverity_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Severity updated successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error occured while updating";
            }
        }

        //Status 

        public void DaInactiveHRLoanSeverity(hrloanseverity values, string employee_gid)
        {
            msSQL = " update hrl_mst_thrloanseverity set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", @"\'") + "'" +
                    " where hrloanseverity_gid='" + values.hrloanseverity_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("HRSI");

                msSQL = " insert into hrl_mst_thrloanseverityinactivelog (" +
                      " hrloanseverityinactivelog_gid, " +
                      " hrloanseverity_gid," +
                      " hrloanseverity_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.hrloanseverity_gid + "'," +
                      " '" + values.hrloanseverity_name.Replace("'", @"\'") + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", @"\'") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Severity inactivated successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Severity activated successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error occurred";
            }
        }

        public void DaInactiveHRLoanSeverityHistory(HRLoanSeverityInactiveHistory objhrloanseverityhistory, string hrloanseverity_gid)
        {
            try
            {
                msSQL = " select a.remarks,date_format(updated_date,'%Y-%m-%d %h:%i:%s') as updated_date, " +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                " from hrl_mst_thrloanseverityinactivelog a " +
                " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                " where a.hrloanseverity_gid='" + hrloanseverity_gid + "' order by a.hrloanseverityinactivelog_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getseverityinactivehistory_list = new List<severityinactivehistory_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getseverityinactivehistory_list.Add(new severityinactivehistory_list
                        {
                            status = (dr_datarow["status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString())
                        });
                    }
                    objhrloanseverityhistory.severityinactivehistory_list = getseverityinactivehistory_list;
                }
                dt_datatable.Dispose();
                objhrloanseverityhistory.status = true;
            }
            catch
            {
                objhrloanseverityhistory.status = false;
            }
        }

        // Delete

        public void DaDeleteHRLoanSeverity(string hrloanseverity_gid, string employee_gid, result values)
        {
            msSQL = " select severity_gid from hrl_trn_trequest where severity_gid ='" + hrloanseverity_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.message = "This Severity is mapped, so..you can't delete it..!";
                values.status = false;
                objODBCDatareader.Close();
            }
            else
            {
                msSQL = " delete from hrl_mst_thrloanseverity where hrloanseverity_gid ='" + hrloanseverity_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Severity deleted successfully..!";
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