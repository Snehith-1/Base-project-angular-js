using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.osd.Models;

namespace ems.osd.DataAccess
{
    public class DaOsdTrnDashboard
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_levelthree;
        string msSQL;

        public void DaOsdPrevilege(string user_gid, osdprivilege values)
        {

            msSQL = " SELECT a.module_gid FROM adm_mst_tprivilege a" +
                     " LEFT JOIN adm_mst_tmodule b ON a.module_gid = b.module_gid WHERE user_gid = '" + user_gid + "' AND menu_level = 4" +
                     " and b.module_gid like 'OSD%' order by b.display_order asc";
            dt_levelthree = objdbconn.GetDataTable(msSQL);
            if (dt_levelthree.Rows.Count != 0)
            {
                values.osdprivilege_list = dt_levelthree.AsEnumerable().Select(row => new osdprivilege_list
                {
                    osdprivilege = row["module_gid"].ToString()
                }).ToList();
            }
            dt_levelthree.Dispose();

        }
    }
}