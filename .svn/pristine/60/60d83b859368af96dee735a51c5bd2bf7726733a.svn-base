using ems.foundation.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;


namespace ems.foundation.DataAccess
{
    public class DaFndMstCategoryTypeMaster
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader, objODBCDatareader1, objODBCDatareader2;
        string msSQL, msGetGid, lscategorytype_value, lslms_code, lsbureau_code, lsremarks, lscategorytype_code;
        int mnResult;
        public void DaGetCategoryType(MdlFndMstCategoryTypeMaster values)
        {
            try
            {
                msSQL = " SELECT a.categorytype_gid,a.categorytype_name, a.categorytype_code, a.lms_code, a.bureau_code,a.remarks, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM fnd_mst_tcategorytype a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.categorytype_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcategorytype_list = new List<categorytype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcategorytype_list.Add(new categorytype_list
                        {
                            categorytype_gid = (dr_datarow["categorytype_gid"].ToString()),
                            categorytype_name = (dr_datarow["categorytype_name"].ToString()),

                            categorytype_code = (dr_datarow["categorytype_code"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    values.categorytype_list = getcategorytype_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaCreateCategoryType(MdlFndMstCategoryTypeMaster values, string employee_gid)
        {
            msSQL = "select categorytype_name from fnd_mst_tcategorytype where categorytype_name = '" + values.categorytype_name.Replace("'", "\\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "category Type Already Exist";
            }
            else
            {

                if (values.lms_code == null || values.lms_code == "")
                {
                    lslms_code = "";
                }
                else
                {
                    lslms_code = values.lms_code.Replace("'", "");
                }
                if (values.bureau_code == null || values.bureau_code == "")
                {
                    lsbureau_code = "";
                }
                else
                {
                    lsbureau_code = values.bureau_code.Replace("'", "");
                }
                if (values.remarks == null || values.remarks == "")
                {
                    lsremarks = "";
                }
                else
                {
                    lsremarks = values.remarks.Replace("'", "");
                }



                msGetGid = objcmnfunctions.GetMasterGID("CATY");
                lscategorytype_code = objcmnfunctions.GetMasterGID("CAGY");

                msSQL = " insert into fnd_mst_tcategorytype(" +
                        " categorytype_gid," +
                        " categorytype_name," +
                        " categorytype_code," +
                        " lms_code," +
                        " bureau_code," +
                        " remarks," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.categorytype_name.Replace("'", "") + "'," +
                        "'" + lscategorytype_code + "'," +
                        "'" + lslms_code + "'," +
                        "'" + lsbureau_code + "'," +
                        "'" + lsremarks + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Category Type Added Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred While Adding Category Type";
                }
            }
        }

        public void DaEditCategoryType(string categorytype_gid, MdlFndMstCategoryTypeMaster values)
        {
            try
            {
                msSQL = " SELECT categorytype_gid,categorytype_name,categorytype_code,lms_code, bureau_code,remarks, status as Status FROM fnd_mst_tcategorytype where categorytype_gid='" + categorytype_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.categorytype_gid = objODBCDatareader["categorytype_gid"].ToString();
                    values.categorytype_name = objODBCDatareader["categorytype_name"].ToString();

                    values.categorytype_code = objODBCDatareader["categorytype_code"].ToString();
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    values.remarks = objODBCDatareader["remarks"].ToString();
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

        public void DaUpdateCategoryType(string employee_gid, MdlFndMstCategoryTypeMaster values)
        {


            msSQL = " update fnd_mst_tcategorytype set " +
                 " categorytype_name='" + values.categorytype_name.Replace("'", "") + "'," +

                 " categorytype_code='" + values.categorytype_code + "'," +
                 " lms_code='" + values.lms_code + "'," +
                 " bureau_code='" + values.bureau_code + "'," +
                 " remarks='" + values.remarks + "'," +

                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where categorytype_gid='" + values.categorytype_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("FDLO");

                msSQL = " insert into fnd_mst_tcategorytypelog (" +
                       " categorytypelog_gid, " +
                       " categorytype_gid, " +
                       " categorytype_name," +
                       " updated_by," +
                       " updated_date) " +
                       " values (" +
                       " '" + msGetGid + "'," +
                       " '" + values.categorytype_gid + "'," +
                       " '" + values.categorytype_name.Replace("'", "") + "'," +
                       " '" + employee_gid + "'," +
                       " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Category Type Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }

        public void DaInactiveCategoryType(MdlFndMstCategoryTypeMaster values, string employee_gid)
        {
            msSQL = " update fnd_mst_tcategorytype set status='" + values.rbo_status + "'" +
                    " where categorytype_gid='" + values.categorytype_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("FDIL");

                msSQL = " insert into fnd_mst_tcategorytypeinactivelog (" +
                      " categorytypeinactivelog_gid, " +
                      " categorytype_gid," +
                      " categorytype_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.categorytype_gid + "'," +
                      " '" + values.categorytype_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Category Type Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Category Type Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaDeleteCategoryType(string categorytype_gid, string employee_gid, MdlFndMstCategoryTypeMaster values)
        {


            msSQL = " select categorytype_name from fnd_mst_tcategorytype where categorytype_gid='" + categorytype_gid + "'";
            lscategorytype_value = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " delete from fnd_mst_tcategorytype where categorytype_gid='" + categorytype_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Category Type Deleted Successfully..!";
                msGetGid = objcmnfunctions.GetMasterGID("FDDL");
                msSQL = " insert into fnd_mst_tcategorytypedeletelog(" +
                         "categorytypedeletelog_gid, " +
                         "categorytype_gid, " +
                         "master_name, " +
                         "master_value, " +
                         "deleted_by, " +
                         "deleted_date) " +
                         " values(" +
                         "'" + msGetGid + "'," +
                         "'" + categorytype_gid + "', " +
                         "'Category Type'," +
                         "'" + lscategorytype_value + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }


        public void DaCategoryTypeInactiveLogview(string categorytype_gid, MdlFndMstCategoryTypeMaster values)
        {
            try
            {
                msSQL = " SELECT a.categorytype_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM fnd_mst_tcategorytypeinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.categorytype_gid ='" + categorytype_gid + "' order by a.categorytypeinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcategorytype_list = new List<categorytype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcategorytype_list.Add(new categorytype_list
                        {
                            categorytype_gid = (dr_datarow["categorytype_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.categorytype_list = getcategorytype_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }



    }
}