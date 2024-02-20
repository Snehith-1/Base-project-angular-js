using ems.mastersamagro.Models;
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

namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess provide access for various Single and Mutliple events (Add, Edit, View, Delete, 
    /// Status Update and summary) in Amendment
    /// </summary>
    /// <remarks>Written by Premchander.K </remarks>
    public class DaAgrMstAmendment
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid, msGetAPICode;
        int mnResult;

        public void DaGetAmendment(MdlAgrMstAmendment objamendment)

        {
            try
            {
                msSQL = " SELECT amendment_gid,amendment,lms_code, bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,api_code, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM agr_mst_tamendment a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.amendment_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getamendment_list = new List<amendment_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getamendment_list.Add(new amendment_list
                        {
                            amendment_gid = (dr_datarow["amendment_gid"].ToString()),
                            amendment = (dr_datarow["amendment"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                        });
                    }
                    objamendment.amendment_list = getamendment_list;
                }
                dt_datatable.Dispose();
                objamendment.status = true;
            }
            catch
            {
                objamendment.status = false;
            }
        }

        public void DaCreateAmendment(amendmentlist values, string employee_gid)
        {
            msSQL = "select amendment from agr_mst_tamendment where amendment = '" + values.amendment.Replace("'", @"\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Amendment already exist";
            }
            else
            {
                msGetAPICode = objcmnfunctions.GetApiMasterGID("ATAC");
                msGetGid = objcmnfunctions.GetMasterGID("AMDT");
                msSQL = " insert into agr_mst_tamendment(" +
                        " amendment_gid ," +
                        " api_code ," +
                        " lms_code," +
                        " bureau_code," +
                        " amendment ," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," + 
                        "'" + msGetAPICode + "',";
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

                msSQL += "'" + values.amendment.Replace("'", @"\'") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Amendment added successfully";
                }
                else
                {
                    values.message = "Error occured while adding";
                    values.status = false;
                }
            }
        }
        // Edit 

        public void DaEditAmendment(string amendment_gid, amendmentlist values)
        {
            try
            {
                msSQL = " SELECT amendment_gid,amendment,lms_code, bureau_code, status as status FROM agr_mst_tamendment " +
                        " where amendment_gid='" + amendment_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.amendment_gid = objODBCDatareader["amendment_gid"].ToString();
                    values.amendment = objODBCDatareader["amendment"].ToString();
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

        public void DaUpdateAmendment(string employee_gid, amendmentlist values)
        {
            msSQL = "select updated_by,date_format(updated_date,'%Y-%m-%d %h:%i:%s') as updated_date,amendment from agr_mst_tamendment where amendment_gid ='" + values.amendment_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("AMTL");
                    msSQL = " insert into agr_mst_tamendmentlog(" +
                              " amendmentlog_gid  ," +
                              " amendment_gid," +
                              " amendment, " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.amendment_gid + "'," +
                              "'" + objODBCDatareader["amendment"].ToString().Replace("'", @"\'") + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            msSQL = " update agr_mst_tamendment set ";
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

            msSQL += " amendment='" + values.amendment.Replace("'", @"\'") + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where amendment_gid='" + values.amendment_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Amendment updated successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error occured while updating";
            }
        }

        //Status 

        public void DaInactiveAmendment(amendmentlist values, string employee_gid)
        {
            msSQL = " update agr_mst_tamendment set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", @"\'") + "'" +
                    " where amendment_gid='" + values.amendment_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("AMTI");

                msSQL = " insert into agr_mst_tamendmentinactivelog (" +
                      " amendmentinactivelog_gid, " +
                      " amendment_gid," +
                      " amendment," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.amendment_gid + "'," +
                      " '" + values.amendment.Replace("'", @"\'") + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", @"\'") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Amendment inactivated successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Amendment activated successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error occurred";
            }
        }

        public void DaInactiveAmendmentHistory(AmendmentInactiveHistory objamendmenthistory, string amendment_gid)
        {
            try
            {
                msSQL = " select a.remarks, date_format(a.updated_date,'%Y-%m-%d %h:%i:%s') as updated_date, " +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                " from agr_mst_tamendmentinactivelog a " +
                " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                " where a.amendment_gid='" + amendment_gid + "' order by a.amendmentinactivelog_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getamendmentinactivehistory_list = new List<amendmentinactivehistory_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getamendmentinactivehistory_list.Add(new amendmentinactivehistory_list
                        {
                            status = (dr_datarow["status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString())
                        });
                    }
                    objamendmenthistory.amendmentinactivehistory_list = getamendmentinactivehistory_list;
                }
                dt_datatable.Dispose();
                objamendmenthistory.status = true;
            }
            catch
            {
                objamendmenthistory.status = false;
            }
        }

        // Delete

        public void DaDeleteAmendment(string amendment_gid, string employee_gid, result values)
        {
            msSQL = " select amendment_gid from agr_mst_tamendmentreason where amendment_gid ='" + amendment_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.message = "This amendment is mapped, so..you can't delete it..!";
                values.status = false;
                objODBCDatareader.Close();
            }
            else
            {
                msSQL = " delete from agr_mst_tamendment where amendment_gid ='" + amendment_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Amendment deleted successfully..!";
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