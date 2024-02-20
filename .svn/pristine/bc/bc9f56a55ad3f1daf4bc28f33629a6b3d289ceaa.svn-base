using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.ecms.Models;

namespace ems.ecms.DataAccess
{
    /// <summary>
    /// ecmsdashboard Controller Class containing API methods for accessing the  DataAccess class DaDashboardecms
    /// Checking the preivilege for deferral and dashboard
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    public class DaDashboardecms
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_levelthree;
        DataTable dt_datatable;
        string msSQL;
        public void Dacheckpreivilegedeferral(string user_gid,MdlDashboardecms values)
        {
          
            msSQL = " select * from adm_mst_tprivilege where module_gid='ECMSTSNDFM'" +
                    " and user_gid='"+ user_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
               
                values.privilege_deferral = "showdeferral";
                values.status = true;
                values.message = "success";
            }   
            else
            {
                objODBCDatareader.Close();
                
                values.privilege_deferral = "hidedeferral";
                values.status = false;
                values.message = "failure";
            }
           

        }
        public void Dacheckpreivilegedeferral1(string user_gid, MdlDashboardecms values)
        {
           
            msSQL = " select * from adm_mst_tprivilege where module_gid='ECMS'" +
                    " and user_gid='" + user_gid + "'";
            objODBCDatareader = objdbconn .GetDataReader(msSQL);
            if (objODBCDatareader.HasRows==true)
            {
                objODBCDatareader.Close();
               
                values.privilege_deferral = "show";
                values.status = true;
                values.message = "success";
            }
            else
            {
                objODBCDatareader.Close();
              
                values.privilege_deferral = "hide";
                values.status = false;
                values.message = "failure";
            }


        }
        public void DaecmsPrevilege(string user_gid, ecmsprivilege values)
        {

            msSQL = " SELECT a.module_gid FROM adm_mst_tprivilege a" +
                     " LEFT JOIN adm_mst_tmodule b ON a.module_gid = b.module_gid WHERE user_gid = '"+ user_gid+"' AND menu_level = 4" +
                     " and b.module_gid like 'ECM%' order by b.display_order asc";
            dt_levelthree = objdbconn.GetDataTable(msSQL);
            if (dt_levelthree.Rows.Count != 0)
            {
                values.ecmsprivilege_list = dt_levelthree.AsEnumerable().Select(row => new ecmsprivilege_list
                {
                    ecmsprivilege = row["module_gid"].ToString()
                }).ToList();
            }
            dt_levelthree.Dispose();
        }
    }
}