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
    public class DaMstHRLoanTermsandConditions
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid;
        int mnResult;

        public void DaGetHRLoanTermsandConditions(MdlMstHRLoanTermsandConditions objhrloantermsandconditions)

        {
            try
            {
                msSQL = " SELECT hrloantermsandconditions_gid,hrloantermsandconditions_name,lms_code, bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM hrl_mst_thrloantermsandconditions a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.hrloantermsandconditions_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gethrloantermsandconditions_list = new List<hrloantermsandconditions_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        gethrloantermsandconditions_list.Add(new hrloantermsandconditions_list
                        {
                            hrloantermsandconditions_gid = (dr_datarow["hrloantermsandconditions_gid"].ToString()),
                            hrloantermsandconditions_name = (dr_datarow["hrloantermsandconditions_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    objhrloantermsandconditions.hrloantermsandconditions_list = gethrloantermsandconditions_list;
                }
                dt_datatable.Dispose();
                objhrloantermsandconditions.status = true;
            }
            catch
            {
                objhrloantermsandconditions.status = false;
            }
        }

        public void DaCreateHRLoanTermsandConditions(hrloantermsandconditions values, string employee_gid)
        {
            msSQL = "select hrloantermsandconditions_name from hrl_mst_thrloantermsandconditions where hrloantermsandconditions_name = '" + values.hrloantermsandconditions_name.Replace("'", @"\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Terms and Conditions already exist";
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("HRTC");
                msSQL = " insert into hrl_mst_thrloantermsandconditions(" +
                        " hrloantermsandconditions_gid ," +
                        " lms_code," +
                        " bureau_code," +
                        " hrloantermsandconditions_name ," +
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

                msSQL += "'" + values.hrloantermsandconditions_name.Replace("'", @"\'") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Terms and Conditions added successfully";
                }
                else
                {
                    values.message = "Error occured while adding";
                    values.status = false;
                }
            }
        }
        // Edit 

        public void DaEditHRLoanTermsandConditions(string hrloantermsandconditions_gid, hrloantermsandconditions values)
        {
            try
            {
                msSQL = " SELECT hrloantermsandconditions_gid,hrloantermsandconditions_name,lms_code, bureau_code, status as status FROM hrl_mst_thrloantermsandconditions " +
                        " where hrloantermsandconditions_gid='" + hrloantermsandconditions_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.hrloantermsandconditions_gid = objODBCDatareader["hrloantermsandconditions_gid"].ToString();
                    values.hrloantermsandconditions_name = objODBCDatareader["hrloantermsandconditions_name"].ToString();
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

        public void DaUpdateHRLoanTermsandConditions(string employee_gid, hrloantermsandconditions values)
        {
            msSQL = "select updated_by, date_format(updated_date,'%Y-%m-%d %h:%i:%s') as updated_date,hrloantermsandconditions_name from hrl_mst_thrloantermsandconditions where hrloantermsandconditions_gid ='" + values.hrloantermsandconditions_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("HRCL");
                    msSQL = " insert into hrl_mst_thrloantermsandconditionslog(" +
                              " hrloantermsandconditionslog_gid  ," +
                              " hrloantermsandconditions_gid," +
                              " hrloantermsandconditions_name, " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.hrloantermsandconditions_gid + "'," +
                              "'" + objODBCDatareader["hrloantermsandconditions_name"].ToString().Replace("'", @"\'") + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            msSQL = " update hrl_mst_thrloantermsandconditions set ";
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

            msSQL += " hrloantermsandconditions_name='" + values.hrloantermsandconditions_name.Replace("'", @"\'") + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where hrloantermsandconditions_gid='" + values.hrloantermsandconditions_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Terms and Conditions updated successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error occured while updating";
            }
        }

        //Status 

        public void DaInactiveHRLoanTermsandConditions(hrloantermsandconditions values, string employee_gid)
        {
            msSQL = " update hrl_mst_thrloantermsandconditions set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", @"\'") + "'" +
                    " where hrloantermsandconditions_gid='" + values.hrloantermsandconditions_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("HRCI");

                msSQL = " insert into hrl_mst_thrloantermsandconditionsinactivelog (" +
                      " hrloantermsandconditionsinactivelog_gid, " +
                      " hrloantermsandconditions_gid," +
                      " hrloantermsandconditions_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.hrloantermsandconditions_gid + "'," +
                      " '" + values.hrloantermsandconditions_name.Replace("'", @"\'") + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", @"\'") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Terms and Conditions inactivated successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Terms and Conditions activated successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error occurred";
            }
        }

        public void DaInactiveHRLoanTermsandConditionsHistory(HRLoanTermsandConditionsInactiveHistory objhrloantermsandconditionshistory, string hrloantermsandconditions_gid)
        {
            try
            {
                msSQL = " select a.remarks,date_format(updated_date,'%Y-%m-%d %h:%i:%s') as updated_date, " +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                " from hrl_mst_thrloantermsandconditionsinactivelog a " +
                " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                " where a.hrloantermsandconditions_gid='" + hrloantermsandconditions_gid + "' order by a.hrloantermsandconditionsinactivelog_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gettermsandconditionsinactivehistory_list = new List<termsandconditionsinactivehistory_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        gettermsandconditionsinactivehistory_list.Add(new termsandconditionsinactivehistory_list
                        {
                            status = (dr_datarow["status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString())
                        });
                    }
                    objhrloantermsandconditionshistory.termsandconditionsinactivehistory_list = gettermsandconditionsinactivehistory_list;
                }
                dt_datatable.Dispose();
                objhrloantermsandconditionshistory.status = true;
            }
            catch
            {
                objhrloantermsandconditionshistory.status = false;
            }
        }

        // Delete

        public void DaDeleteHRLoanTermsandConditions(string hrloantermsandconditions_gid, string employee_gid, result values)
        {
            msSQL = " select hrloantermsandconditions_gid from hrl_trn_thrtermsandconditions2 where hrloantermsandconditions_gid ='" + hrloantermsandconditions_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.message = "This Terms and Conditions is mapped, so..you can't delete it..!";
                values.status = false;
                objODBCDatareader.Close();
            }
            else
            {
                msSQL = " delete from hrl_mst_thrloantermsandconditions where hrloantermsandconditions_gid ='" + hrloantermsandconditions_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Terms and Conditions deleted successfully..!";
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