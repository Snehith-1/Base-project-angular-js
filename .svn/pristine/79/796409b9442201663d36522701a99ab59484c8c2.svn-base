using ems.lgl.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ems.lgl.DataAccess
{
    public class DaOptDashboard
    {
        ems.utilities.Functions.dbconn objdbconn = new ems.utilities.Functions.dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        string msSQL;
        public bool DaGetGID(mdloptDashboard values, string user_gid)
        {
          
            msSQL = "select a.module_gid from adm_mst_tmodule a" +
            " left join adm_mst_tprivilege b on b.module_gid = a.module_gid where" +
            " a.module_gid_parent = 'OPTMST' and" +
            " b.user_gid = '" + user_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getGID = new List<master_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getGID.Add(new master_list
                    {
                        Gid = dr_datarow["module_gid"].ToString()
                    });
                }
                values.master_list = getGID;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }
    }
}