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
    /// Branch Controller Class containing API methods for accessing the  DataAccess class DaBranch - to get the branch details
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    public class DaBranch
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
       
        DataTable dt_datatable;
        string msSQL;
       
   
            public void DaGetBranch(MdlBranch objBranch)
        {
            msSQL = " SELECT branch_gid,concat(branch_code,'/',branch_name) as branch_name FROM hrm_mst_tbranch order by branch_code asc ";
          
            dt_datatable = objdbconn.GetDataTable (msSQL);
            var getBranch = new List<branch_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getBranch.Add(new branch_list
                    {
                        branch_gid = (dr_datarow["branch_gid"].ToString()),
                        branch_name = (dr_datarow["branch_name"].ToString()),
                    });
                }
                objBranch.branch_list = getBranch;
            }
            dt_datatable.Dispose();
           
        }
    }
    }
