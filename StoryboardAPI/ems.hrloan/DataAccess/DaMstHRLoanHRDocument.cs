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
    public class DaMstHRLoanHRDocument
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid;
        int mnResult;        
        string lshrdocumentchecklist_name, lshrdocumentgid;
        
        //HR Document
        public void DaGetHRDocument(MdlMstHRLoanHRDocument hrloanhrdocument)
        {
            try
            {
                msSQL = " SELECT hrdocument_gid,hrdocument_name,hrloantypeoffinancialassistance_gid,hrloantypeoffinancialassistance_name,hrloanseverity_gid,hrloanseverity_name,lms_code, bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM hrl_mst_thrdocument a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.hrdocument_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gethrloanhrdocument_list = new List<hrloanhrdocument_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        gethrloanhrdocument_list.Add(new hrloanhrdocument_list
                        {
                            hrdocument_gid = (dr_datarow["hrdocument_gid"].ToString()),
                            hrdocument_name = (dr_datarow["hrdocument_name"].ToString()),
                            hrloantypeoffinancialassistance_gid = (dr_datarow["hrloantypeoffinancialassistance_gid"].ToString()),
                            hrloantypeoffinancialassistance_name = (dr_datarow["hrloantypeoffinancialassistance_name"].ToString()),
                            hrloanseverity_gid = (dr_datarow["hrloanseverity_gid"].ToString()),
                            hrloanseverity_name = (dr_datarow["hrloanseverity_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    hrloanhrdocument.hrloanhrdocument_list = gethrloanhrdocument_list;
                }
                dt_datatable.Dispose();
                hrloanhrdocument.status = true;
            }
            catch
            {
                hrloanhrdocument.status = false;
            }
        }

        public void DaCreateHRDocument(hrdocument values, string employee_gid)
        {
            msSQL = "select hrdocument_name from hrl_mst_thrdocument where hrdocument_name = '" + values.hrdocument_name.Replace("'", @"\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "HR Document already exist";
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("HRDC");

                msSQL = " insert into hrl_mst_thrdocument(" +
                        " hrdocument_gid," +
                        " lms_code," +
                        " bureau_code," +
                        " hrdocument_name," +
                        " created_by," +
                        " hrloantypeoffinancialassistance_gid," +
                        " hrloantypeoffinancialassistance_name," +
                         " hrloanseverity_name," +
                        " hrloanseverity_gid," +
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

                msSQL += "'" + values.hrdocument_name.Replace("'", @"\'") + "'," +
                         "'" + employee_gid + "'," +
                        "'" + values.hrloantypeoffinancialassistance_gid.Replace("'", "") + "'," +
                         "'" + values.hrloantypeoffinancialassistance_name.Replace("'", @"\'") + "'," +
                          "'" + values.hrloanseverity_name.Replace("'", @"\'") + "'," +
                         "'" + values.hrloanseverity_gid.Replace("'", "") + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "HR Document added successfully";

                    msSQL = "update hrl_mst_thrdocumentchecklist set hrdocument_gid ='" + msGetGid + "' where hrdocument_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    values.status = false;
                    values.message = "Error occurred while adding";
                }
            }
        }


        public void DaGetHRDocumentDropDown(string employee_gid, MdlMstHRLoanHRDocument values)
        {

            msSQL = " SELECT hrloantypeoffinancialassistance_gid, hrloantypeoffinancialassistance_name " +
                    " FROM hrl_mst_thrloantypeoffinancialassistance  where status='Y' order by hrloantypeoffinancialassistance_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocument = new List<document_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getdocument.Add(new document_list
                    {
                        hrloantypeoffinancialassistance_gid = (dr_datarow["hrloantypeoffinancialassistance_gid"].ToString()),
                        hrloantypeoffinancialassistance_name = (dr_datarow["hrloantypeoffinancialassistance_name"].ToString()),

                    });
                }
                values.document_list = getdocument;
            }
            dt_datatable.Dispose();

            msSQL = " SELECT hrloanseverity_gid, hrloanseverity_name " +
                  " FROM hrl_mst_thrloanseverity  where status='Y' order by hrloanseverity_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gethrdocumentseverity = new List<hrdocumentseverity_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    gethrdocumentseverity.Add(new hrdocumentseverity_list
                    {
                        hrloanseverity_gid = (dr_datarow["hrloanseverity_gid"].ToString()),
                        hrloanseverity_name = (dr_datarow["hrloanseverity_name"].ToString()),

                    });
                }
                values.hrdocumentseverity_list = gethrdocumentseverity;
            }
            dt_datatable.Dispose();
        }

        public void DaEditHRDocument(string hrdocument_gid, hrdocument values)
        {
            try
            {
                msSQL = " SELECT hrdocument_gid,hrdocument_name,hrloantypeoffinancialassistance_gid,hrloantypeoffinancialassistance_name, " +
                        " hrloanseverity_gid,hrloanseverity_name,lms_code, bureau_code, status as Status " +
                        " FROM hrl_mst_thrdocument where hrdocument_gid='" + hrdocument_gid + "' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.hrdocument_gid = objODBCDatareader["hrdocument_gid"].ToString();
                    values.hrdocument_name = objODBCDatareader["hrdocument_name"].ToString();
                    values.hrloantypeoffinancialassistance_gid = objODBCDatareader["hrloantypeoffinancialassistance_gid"].ToString();
                    values.hrloantypeoffinancialassistance_name = objODBCDatareader["hrloantypeoffinancialassistance_name"].ToString();
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

        public bool DaUpdateHRDocument(string employee_gid, hrdocument values)
        {
            msSQL = "select hrdocument_gid from hrl_mst_thrdocument  where hrdocument_name = '" + values.hrdocument_name.Replace("'", "\\'") + "'";
            lshrdocumentgid = objdbconn.GetExecuteScalar(msSQL);
            if (lshrdocumentgid != "")
            {
                if (lshrdocumentgid != values.hrdocument_gid)
                {
                    values.message = "HR Document already exist";
                    values.status = false;
                    return false;
                }
            }

            msSQL = "select updated_by, date_format(updated_date,'%Y-%m-%d %h:%i:%s') as updated_date,hrdocument_name,hrloantypeoffinancialassistance_gid,hrloantypeoffinancialassistance_name,lms_code, bureau_code, status as Status from hrl_mst_thrdocument where hrdocument_gid ='" + values.hrdocument_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("HRDL");
                    msSQL = " insert into hrl_mst_thrdocumentlog (" +
                              " hrdocumentlog_gid," +
                              " hrdocument_gid," +
                              " hrdocument_name," +
                              " hrloantypeoffinancialassistance_gid," +
                              " hrloantypeoffinancialassistance_name," +
                              " hrloanseverity_gid, " +
                              " hrloanseverity_name, " +
                              " lms_code," +
                              " bureau_code," +
                              " Status," +
                              " updated_by," +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.hrdocument_gid + "'," +
                              "'" + objODBCDatareader["hrdocument_name"].ToString() + "'," +
                              "'" + values.hrloantypeoffinancialassistance_gid + "'," +
                              "'" + values.hrloantypeoffinancialassistance_name.Replace("'", @"\'") + "'," +
                              "'" + values.hrloanseverity_gid + "'," +
                              "'" + values.hrloanseverity_name.Replace("'", @"\'") + "'," +
                              "'" + values.lms_code + "'," +
                              "'" + values.bureau_code + "'," +
                              "'" + values.Status + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();

            msSQL = " update hrl_mst_thrdocument set " +
                     " hrdocument_gid='" + values.hrdocument_gid.Replace("'", "") + "'," +
                     " hrdocument_name='" + values.hrdocument_name.Replace("'", @"\'") + "'," +
                     " hrloantypeoffinancialassistance_gid='" + values.hrloantypeoffinancialassistance_gid + "'," +
                     " hrloantypeoffinancialassistance_name='" + values.hrloantypeoffinancialassistance_name.Replace("'", @"\'") + "'," +
                      " hrloanseverity_gid='" + values.hrloanseverity_gid + "'," +
                     " hrloanseverity_name='" + values.hrloanseverity_name.Replace("'", @"\'") + "'," +
                     " lms_code='" + values.lms_code.Replace("'", @"\'") + "'," +
                     " bureau_code='" + values.bureau_code.Replace("'", @"\'") + "'," +
                     " Status='" + values.Status.Replace("'", @"\'") + "'," +
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where hrdocument_gid='" + values.hrdocument_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                msSQL = "update hrl_mst_thrdocumentchecklist set hrdocument_gid ='" + values.hrdocument_gid + "' where hrdocument_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "HR Document updated successfully";
                return true;


            }
            else
            {
                values.status = false;
                values.message = "Error occurred while updating";
                return false;
            }
        }

        public void DaInactiveHRDocument(hrloanhrdocument values, string employee_gid)
        {
            msSQL = " update hrl_mst_thrdocument set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", @"\'") + "'" +
                    " where hrdocument_gid='" + values.hrdocument_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("HRDI");

                msSQL = " insert into hrl_mst_thrdocumentinactivelog (" +
                      " hrdocumentinactivelog_gid, " +
                      " hrdocument_gid," +
                      " hrdocument_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.hrdocument_gid + "'," +
                      " '" + values.hrdocument_name.Replace("'", @"\'") + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", @"\'") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "HR Document inactivated successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "HR Document activated successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error occurred";
            }
        }

        public void DaInactiveHRDocumentHistory(HRLoanHRDocumentInactiveHistory objhrdocumenthistory, string hrdocument_gid)
        {
            try
            {
                msSQL = " select a.remarks, date_format(updated_date,'%Y-%m-%d %h:%i:%s') as updated_date, " +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                " from hrl_mst_thrdocumentinactivelog a " +
                " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                " where a.hrdocument_gid='" + hrdocument_gid + "' order by a.hrdocumentinactivelog_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gethrdocumentinactivehistory_list = new List<hrdocumentinactivehistory_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        gethrdocumentinactivehistory_list.Add(new hrdocumentinactivehistory_list
                        {
                            status = (dr_datarow["status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString())
                        });
                    }
                    objhrdocumenthistory.hrdocumentinactivehistory_list = gethrdocumentinactivehistory_list;
                }
                dt_datatable.Dispose();
                objhrdocumenthistory.status = true;
            }
            catch
            {
                objhrdocumenthistory.status = false;
            }

        }

        public void DaDeleteHRDocument(string hrdocument_gid, string employee_gid, result values)
        {
            msSQL = " select hrdocument_gid from hrl_trn_thrspecialdocument where hrdocument_gid ='" + hrdocument_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.message = "This HR Document is mapped, so..you can't delete it..!";
                values.status = false;
                objODBCDatareader.Close();
            }
            else
            {
                msSQL = " delete from hrl_mst_thrdocument where hrdocument_gid='" + hrdocument_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "HR Document deleted successfully..!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error occured..!";
                }
            }
        }

        // Add Check List

        public void DaGetHRDocumentCheckList(string employee_gid, MdlMstHRLoanHRDocument objhrloanhrdocument)
        {
            try
            {
                
                msSQL = "select hrdocumentchecklist_gid,hrdocumentchecklist_name from hrl_mst_thrdocumentchecklist where " +
                  " hrdocument_gid ='" + employee_gid + "'" + "order by hrdocument_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gethrloanhrdocument_list = new List<hrloanhrdocument_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        gethrloanhrdocument_list.Add(new hrloanhrdocument_list
                        {
                            hrdocumentchecklist_gid = (dr_datarow["hrdocumentchecklist_gid"].ToString()),
                            hrdocumentchecklist_name = (dr_datarow["hrdocumentchecklist_name"].ToString()),
                           
                        });
                    }
                    objhrloanhrdocument.hrloanhrdocument_list = gethrloanhrdocument_list;
                }
                dt_datatable.Dispose();
                objhrloanhrdocument.status = true;
            }
            catch
            {
                objhrloanhrdocument.status = false;
            }
        }

        public void DaCreateHRDocumentCheckList(checklist values, string employee_gid)
        {
            if (values.hrdocumentchecklist_name == null || values.hrdocumentchecklist_name == "")
            {
                lshrdocumentchecklist_name = "";
            }
            else
            {
                lshrdocumentchecklist_name = values.hrdocumentchecklist_name.Replace("'", @"\'");
            }

            msGetGid = objcmnfunctions.GetMasterGID("HDCL");
            msSQL = " insert into hrl_mst_thrdocumentchecklist(" +
                    " hrdocumentchecklist_gid," +
                    " hrdocument_gid," +
                    " hrdocumentchecklist_name," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + lshrdocumentchecklist_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Check list added successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error occurred while adding";
            }
        }

        // EditCheckList

        public void DaGetHRDocumentCheckListEditList(string hrdocument_gid, MdlMstHRLoanHRDocument values)
        {
            msSQL = " select hrdocumentchecklist_gid ,hrdocumentchecklist_name,hrdocument_gid from  hrl_mst_thrdocumentchecklist " +
                    " where hrdocument_gid ='" + hrdocument_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getchecklist_list = new List<checklist_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getchecklist_list.Add(new checklist_list
                    {
                        hrdocumentchecklist_gid = dt["hrdocumentchecklist_gid"].ToString(),
                        hrdocument_gid = dt["hrdocument_gid"].ToString(),
                        hrdocumentchecklist_name = dt["hrdocumentchecklist_name"].ToString(),
                    });
                    values.checklist_list = getchecklist_list;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetHRDocumentCheckListTempEditList(string hrdocument_gid, string employee_gid, MdlMstHRLoanHRDocument values)
        {
            msSQL = " select hrdocumentchecklist_gid ,hrdocumentchecklist_name,hrdocument_gid from hrl_mst_thrdocumentchecklist " +
                    " where hrdocument_gid = '" + employee_gid + "' or hrdocument_gid ='" + hrdocument_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gethrloanhrdocument_list = new List<hrloanhrdocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    gethrloanhrdocument_list.Add(new hrloanhrdocument_list
                    {
                        hrdocumentchecklist_gid = dt["hrdocumentchecklist_gid"].ToString(),
                        hrdocument_gid = dt["hrdocument_gid"].ToString(),
                        hrdocumentchecklist_name = dt["hrdocumentchecklist_name"].ToString(),

                    });
                    values.hrloanhrdocument_list = gethrloanhrdocument_list;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaHRDocumentCheckListTempClear(string employee_gid, result values)
        {
            msSQL = "delete from hrl_mst_thrdocumentchecklist where hrdocument_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
            }
            else
            {
                values.status = false;
            }
        }

        public void DaDeleteHRDocumentCheckList(string hrdocumentchecklist_gid, variety values)
        {
            msSQL = "delete from hrl_mst_thrdocumentchecklist where hrdocumentchecklist_gid='" + hrdocumentchecklist_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Check list deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error occured";
                values.status = false;

            }

        }

    }
}