using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ems.master.Models;
using ems.utilities.Functions;
using System.Data.Odbc;
using System.Data;

namespace ems.master.DataAccess
{
    public class DaMstCreditmappingmaster
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;

        public bool Daemployeelist(Mdlemployee values)
        {
            try
            {
                msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,'/',a.user_code) as employee_name," +
                       " b.employee_gid from adm_mst_tuser a " +
                       " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                       " WHERE user_status<>'N' ORDER BY a.user_firstname asc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employee = new List<employeelist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    values.employeelist = dt_datatable.AsEnumerable().Select(row =>
                    new employeelist
                    {
                        employee_gid = row["employee_gid"].ToString(),
                        employee_name = row["employee_name"].ToString()
                    }
                    ).ToList();
                }
                dt_datatable.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }
    }
}
    