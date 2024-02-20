using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.ep.Models;
using System.Configuration;

namespace ems.ep.DataAccess
{
    public class DaCaseAllocation
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL;

        public bool DaGetExternalAllocatedDtl(string user_gid, allocationdtlList values)
        {
            msSQL = " select a.allocationdtl_gid,a.customer_gid,b.customername,b.customer_urn,a.state_name, a.completed_flag," +
                    " a.district_name,concat(f.user_firstname,' ',f.user_lastname) as assigedRMname, a.allocation_status," +
                    " concat(h.user_firstname,' ',h.user_lastname) as requested_by,date_format(a.target_date,'%d-%m-%Y') as target_date, " +
                    " date_format(a.requested_date,'%d-%m-%Y %h:%i %p') as requested_date " +
                    " from rsk_trn_tallocationdtl a " +
                    " left join ocs_mst_tcustomer b on a.customer_gid = b.customer_gid " +
                    " left join hrm_mst_temployee e on e.employee_gid=a.allocation_assignedRM" +
                    " left join adm_mst_tuser f on f.user_gid=e.user_gid " +
                    " left join hrm_mst_temployee g on g.employee_gid = a.requested_by" +
                    " left join adm_mst_tuser h on h.user_gid=g.user_gid " +
                    " where a.allocate_externalGid='" + user_gid + "' order by allocationdtl_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_allocationdtl = new List<allocationdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_allocationdtl.Add(new allocationdtl
                    {
                        allocationdtl_gid = dt["allocationdtl_gid"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        customername = dt["customername"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        state_name = dt["state_name"].ToString(),
                        district_name = dt["district_name"].ToString(),
                        assigned_RM = dt["assigedRMname"].ToString(),
                        requested_by = dt["requested_by"].ToString(),
                        requested_date = dt["requested_date"].ToString(),
                        completed_flag = dt["completed_flag"].ToString(),
                        allocation_status = dt["allocation_status"].ToString(),
                        target_date=dt["target_date"].ToString(),
                    });
                }
                values.allocationdtl = get_allocationdtl;
            }
            dt_datatable.Dispose();
            return true;
        }
        public bool DaGetExternalVisitCancelLog(visistreportcancelList values, string allocationdtl_gid)
        {
            msSQL = "select cancel_remarks,date_format(a.created_date, '%d-%m-%Y %h:%m %p') as created_date, " +
                    " concat(b.external_username, ' / ',b.external_usercode) as created_by from rsk_trn_tcancelvisitreport a " +
                    "  left join rsk_mst_texternaluser b on a.created_by = b.external_usergid " +
                    " where allocationdtl_gid='" + allocationdtl_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_visistreportcancel = new List<visistreportcancel>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_visistreportcancel.Add(new visistreportcancel
                    {
                        cancel_remarks = dt["cancel_remarks"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                    });
                }
                values.visistreportcancel = get_visistreportcancel;
            }
            dt_datatable.Dispose();

            return true;
        }
    }
}