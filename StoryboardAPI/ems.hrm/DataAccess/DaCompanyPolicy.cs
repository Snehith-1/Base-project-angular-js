using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.VisualBasic;
using System.Security.Cryptography;
using System.Data.Odbc;
using ems.hrm.Models;
using ems.utilities.Functions;
namespace ems.hrm.DataAccess
{
    public class DaCompanyPolicy 
    {
        DataTable dt_datatable;
        string msSQL;
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();

        public bool companypolicy_da(company_policy objcompanypolicy)
        {
            try
            {
                msSQL = "select policy_name,policy_desc from hrm_trn_tpolicymanagement ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_companypolicy = new List<policy_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        get_companypolicy.Add(new policy_list
                        {
                            company_policies = dt["policy_name"].ToString(),
                            policies_description = dt["policy_desc"].ToString()
                        });
                        objcompanypolicy.policy_list = get_companypolicy;
                    }
                }
                dt_datatable.Dispose();
                objcompanypolicy.status = true;
                return true;
            }
            catch
            {
                objcompanypolicy.status = false;
                return false;
            }
        }

    }
       
}