using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.rsk.Models;

namespace ems.rsk.DataAccess
{
    public class DaRskDashboard
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_levelthree, dt_datatable;
        string msSQL;

        public void DaGetRskPrivilege(string user_gid, rskprivilege values)
        {

            msSQL = " SELECT a.module_gid FROM adm_mst_tprivilege a" +
                     " LEFT JOIN adm_mst_tmodule b ON a.module_gid = b.module_gid WHERE user_gid = '" + user_gid + "' AND menu_level = 4" +
                     " and b.module_gid like 'RSK%' order by b.display_order asc";
            dt_levelthree = objdbconn.GetDataTable(msSQL);
            if (dt_levelthree.Rows.Count != 0)
            {
                values.rskprivilege_list = dt_levelthree.AsEnumerable().Select(row => new rskprivilege_list
                {
                    rskprivilege = row["module_gid"].ToString()
                }).ToList();
            }
            dt_levelthree.Dispose();

        }

        public void GetRMAllocateCountdtl(string assigned_RM, string qualified_status, customerstatusList values)
        {

            msSQL = " select concat(b.customer_name,'/',b.customer_urn) as customer_name, " +
                    " date_format(c.lastvisit_date,'%d-%m-%Y') as lastvisit_date " +
                    " from rsk_trn_tallocationdtl a " +
                    " left join rsk_trn_tallocatecustomerdtl b on a.allocationdtl_gid = b.allocationdtl_gid " +
                    " left join rsk_trn_tcustomervisit c on a.customer_gid = c.customer_gid " +
                    " where a.allocation_assignedRM = '" + assigned_RM + "'" +
                    " and a.qualified_status='" + qualified_status + "' and allocation_status = 'Allocated'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomerstatusdtl = new List<customerstatusdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcustomerstatusdtl.Add(new customerstatusdtl
                    {
                        customer_name = dr_datarow["customer_name"].ToString(),
                        lastvisit_date = dr_datarow["lastvisit_date"].ToString(),
                    });
                }
                values.customerstatusdtl = getcustomerstatusdtl;
                values.status = true;
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();

        }

        public void DaGetAllocationDtl(allocationvisitgraphlist values)
        {
            msSQL = " select concat(CAST(monthname(visit_allocated_date) AS char), ' ', year(visit_allocated_date)) as monthname, " +
                    " (select count(allocationdtl_gid) from rsk_trn_tallocationdtl b " +
                    " where month(a.visit_allocated_date) = month(b.visit_allocated_date) " +
                    " and year(a.visit_allocated_date) = year(b.visit_allocated_date) " +
                    " group by monthname(visit_allocated_date)) AS allocatedmonthcount, " +
                    " (select count(allocationdtl_gid) from rsk_trn_tvisitreportgenerate c " +
                    " where monthname(a.visit_allocated_date) = monthname(c.completed_date) " +
                    " and year(a.visit_allocated_date) = year(c.completed_date) " +
                    " group by monthname(completed_date)) AS completedcount " +
                    " from rsk_trn_tallocationdtl a where year(visit_allocated_date) = '" + DateTime.Now.ToString("yyyy") + "' " +
                    " group by monthname(visit_allocated_date) ORDER BY MONTH(visit_allocated_date) desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdata = new List<allocationvisitgraphdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                if (dt_datatable.Rows.Count < 5)
                {
                    int count = 5 - dt_datatable.Rows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        getdata.Add(new allocationvisitgraphdtl
                        {
                            monthname = "",
                            countAllocated = "",
                            countCompleted = ""
                        });
                    }
                }
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getdata.Add(new allocationvisitgraphdtl
                    {
                        monthname = dr_datarow["monthname"].ToString(),
                        countAllocated = dr_datarow["allocatedmonthcount"].ToString(),
                        countCompleted = dr_datarow["completedcount"].ToString()
                    });
                }
                values.allocationvisitgraphdtl = getdata;
            }
            dt_datatable.Dispose();
        }
    }
}