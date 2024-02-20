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
    public class DaMstHRLoanRequestWithdrawn
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;        
        string msSQL;
       

       
        public void DaGetHRloanDetailsWithdrawn(MdlMstHRLoanRequestWithdrawn values, string user_gid, string employee_gid)
        {
            msSQL = " select request_gid, request_refno, request_status, raisequery_status, fintype_name, " +
                     " employee_gid, employee_name, employee_role, department_name, " +
                     " user_gid, reporting_mgr,  functional_head, created_by, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                     " raised_department,  amount,  purpose_name,  severity_name, tenure from hrl_trn_trequest a " +
                     " where a.created_by='" + user_gid + "' and a.request_status = 'Withdrawn' order by request_gid desc";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            var gethrequestWithdrawnList = new List<hrloanrequestWithdrawn>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    gethrequestWithdrawnList.Add(new hrloanrequestWithdrawn
                    {
                        request_gid = dt["request_gid"].ToString(),
                        request_refno = dt["request_refno"].ToString(),
                        request_status = dt["request_status"].ToString(),
                        raisequery_status = dt["raisequery_status"].ToString(),
                        fintype_name = dt["fintype_name"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                        employee_role = dt["employee_role"].ToString(),
                        department_name = dt["department_name"].ToString(),
                        user_gid = dt["user_gid"].ToString(),
                        reporting_mgr = dt["reporting_mgr"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        amount = dt["amount"].ToString(),

                    });
                    values.hrloanrequestWithdrawn = gethrequestWithdrawnList;
                }
            }
            dt_datatable.Dispose();
        }   

    }
}