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
    public class DaMstHRLoanPurpose
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid;
        int mnResult;

        public void DaGetHRLoanPurpose(MdlMstHRLoanPurpose objhrloanpurpose)

        {
            try
            {
                msSQL = " SELECT hrloanpurpose_gid,hrloanpurpose_name,lms_code, bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status,mandatory " +
                        " FROM hrl_mst_thrloanpurpose a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.hrloanpurpose_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gethrloanpurpose_list = new List<hrloanpurpose_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        gethrloanpurpose_list.Add(new hrloanpurpose_list
                        {
                            hrloanpurpose_gid = (dr_datarow["hrloanpurpose_gid"].ToString()),
                            hrloanpurpose_name = (dr_datarow["hrloanpurpose_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            mandatory = (dr_datarow["mandatory"].ToString()),
                        });
                    }
                    objhrloanpurpose.hrloanpurpose_list = gethrloanpurpose_list;
                }
                dt_datatable.Dispose();
                objhrloanpurpose.status = true;
            }
            catch
            {
                objhrloanpurpose.status = false;
            }
        }

        public void DaCreateHRLoanPurpose(hrloanpurpose values, string employee_gid)
        {
            msSQL = "select hrloanpurpose_name from hrl_mst_thrloanpurpose where hrloanpurpose_name = '" + values.hrloanpurpose_name.Replace("'", @"\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Purpose already exist";
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("HRPP");
                msSQL = " insert into hrl_mst_thrloanpurpose(" +
                        " hrloanpurpose_gid ," +
                        " lms_code," +
                        " bureau_code," +
                        " hrloanpurpose_name ," +
                        " purpose_note ," +
                        " mandatory ," +
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

                msSQL += "'" + values.hrloanpurpose_name.Replace("'", @"\'") + "'," +
                        "'" + values.purpose_note.Replace("'", @"\'") + "'," +
                        "'" + values.mandatory.Replace("'", @"\'") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Purpose added successfully";
                }
                else
                {
                    values.message = "Error occured while adding";
                    values.status = false;
                }
            }
        }
        // Edit 

        public void DaEditHRLoanPurpose(string hrloanpurpose_gid, hrloanpurpose values)
        {
            try
            {
                msSQL = " SELECT hrloanpurpose_gid,hrloanpurpose_name,purpose_note,mandatory,lms_code, bureau_code, status as status FROM hrl_mst_thrloanpurpose " +
                        " where hrloanpurpose_gid='" + hrloanpurpose_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.hrloanpurpose_gid = objODBCDatareader["hrloanpurpose_gid"].ToString();
                    values.hrloanpurpose_name = objODBCDatareader["hrloanpurpose_name"].ToString();
                    values.purpose_note = objODBCDatareader["purpose_note"].ToString();
                    values.mandatory = objODBCDatareader["mandatory"].ToString();
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

        public void DaUpdateHRLoanPurpose(string employee_gid, hrloanpurpose values)
        {
            msSQL = "select updated_by,date_format(updated_date,'%Y-%m-%d %h:%i:%s') as updated_date,hrloanpurpose_name from hrl_mst_thrloanpurpose where hrloanpurpose_gid ='" + values.hrloanpurpose_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("HRPL");
                    msSQL = " insert into hrl_mst_thrloanpurposelog(" +
                              " hrloanpurposelog_gid  ," +
                              " hrloanpurpose_gid," +
                              " hrloanpurpose_name, " +                              
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.hrloanpurpose_gid + "'," +
                              "'" + objODBCDatareader["hrloanpurpose_name"].ToString().Replace("'", @"\'") + "'," +                              
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            msSQL = " update hrl_mst_thrloanpurpose set ";
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

            msSQL += " hrloanpurpose_name='" + values.hrloanpurpose_name.Replace("'", @"\'") + "'," +
                    " purpose_note='" + values.purpose_note.Replace("'", @"\'") + "'," +
                    " mandatory='" + values.mandatory.Replace("'", @"\'") + "'," +
                    " updated_by='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where hrloanpurpose_gid='" + values.hrloanpurpose_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Purpose updated successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error occured while updating";
            }
        }

        //Status 

        public void DaInactiveHRLoanPurpose(hrloanpurpose values, string employee_gid)
        {
            msSQL = " update hrl_mst_thrloanpurpose set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", @"\'") + "'" +
                    " where hrloanpurpose_gid='" + values.hrloanpurpose_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("HRPI");

                msSQL = " insert into hrl_mst_thrloanpurposeinactivelog (" +
                      " hrloanpurposeinactivelog_gid, " +
                      " hrloanpurpose_gid," +
                      " hrloanpurpose_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.hrloanpurpose_gid + "'," +
                      " '" + values.hrloanpurpose_name.Replace("'", @"\'") + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", @"\'") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Purpose inactivated successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Purpose activated successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error occurred";
            }
        }

        public void DaInactiveHRLoanPurposeHistory(HRLoanPurposeInactiveHistory objhrloanpurposehistory, string hrloanpurpose_gid)
        {
            try
            {
                msSQL = " select a.remarks, date_format(updated_date,'%Y-%m-%d %h:%i:%s') as updated_date, " +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                " from hrl_mst_thrloanpurposeinactivelog a " +
                " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                " where a.hrloanpurpose_gid='" + hrloanpurpose_gid + "' order by a.hrloanpurposeinactivelog_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getpurposeinactivehistory_list = new List<purposeinactivehistory_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getpurposeinactivehistory_list.Add(new purposeinactivehistory_list
                        {
                            status = (dr_datarow["status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString())
                        });
                    }
                    objhrloanpurposehistory.purposeinactivehistory_list = getpurposeinactivehistory_list;
                }
                dt_datatable.Dispose();
                objhrloanpurposehistory.status = true;
            }
            catch
            {
                objhrloanpurposehistory.status = false;
            }
        }

        // Delete

        public void DaDeleteHRLoanPurpose(string hrloanpurpose_gid, string employee_gid, result values)
        {
            msSQL = " select purpose_gid from hrl_trn_trequest where purpose_gid ='" + hrloanpurpose_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.message = "This Purpose is mapped, so..you can't delete it..!";
                values.status = false;
                objODBCDatareader.Close();
            }
            else
            {
                msSQL = " delete from hrl_mst_thrloanpurpose where hrloanpurpose_gid ='" + hrloanpurpose_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Purpose deleted successfully..!";
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