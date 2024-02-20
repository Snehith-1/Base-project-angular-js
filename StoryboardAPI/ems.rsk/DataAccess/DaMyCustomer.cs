using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.rsk.Models;
using System.Configuration;

namespace ems.rsk.DataAccess
{
    public class DaMyCustomer
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable, dr_datatable;
        string msSQL;
        int mnResult;

        public void DaGetZonalCustomerRMDetail(customerRMdtl objCustomer,string employee_gid)
        {
            try
            {

                msSQL = " select a.customer_gid,a.customername,a.vertical_code,a.customer_urn,f.zonal_name,state,ppa_name, " +
                       " district_name,concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as zonalriskmanager, " +
                       " concat(e.user_firstname, ' ', e.user_lastname, '/', e.user_code) as riskmanager from ocs_mst_tcustomer a " +
                       " left join hrm_mst_temployee b on a.zonal_riskmanager = b.employee_gid " +
                       " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                       " left join hrm_mst_temployee d on a.assigned_RM = d.employee_gid " +
                       " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                       " left join rsk_mst_tzonalmapping f on a.zonal_gid = f.zonalmapping_gid " +
                       " where a.zonal_riskmanager = '" + employee_gid + "' " +
                       " order by a.customer_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    objCustomer.customer_list = dt_datatable.AsEnumerable().Select(row =>
                    new customer_list
                    {
                        customer_gid = (row["customer_gid"].ToString()),
                        customername = (row["customername"].ToString()),
                        vertical_code = (row["vertical_code"].ToString()),
                        customer_urn = (row["customer_urn"].ToString()),
                        state_name = (row["state"].ToString()),
                        district_name = (row["district_name"].ToString()),
                        zonal_name = (row["zonal_name"].ToString()),
                        zonal_riskmanager = (row["zonalriskmanager"].ToString()),
                        riskmanager_name = (row["riskmanager"].ToString()),
                        ppa_name=(row["ppa_name"].ToString()),
                    }).ToList();
                    dt_datatable.Dispose();

                }
                objCustomer.status = true;
            }
            catch (Exception ex)
            {
                objCustomer.message = ex.Message.ToString();
                objCustomer.status = false;
            }

        }
    }
}