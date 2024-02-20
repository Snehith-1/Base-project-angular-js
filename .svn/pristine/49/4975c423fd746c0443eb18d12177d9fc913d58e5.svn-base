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
using ems.utilities.Functions;
using ems.hrm.Models;
namespace ems.hrm.DataAccess
{

    public class daApplyPermission
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcConnection objODBCconnection;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        DataTable objTblRQ;
        DataTable table;
        string msSQL;
        string msGetGID;
        int mnResult;
        DateTime onduty_date;
        DateTime onduty_from;
        DateTime onduty_to;
        DateTime onduty_duration;
        string onduty_reason;
        string ondutytracker_status;
        string approved_by;
        DateTime approved_date;
        public bool  postapplypermission_da(string employee_gid,string user_gid, permission_details values)
        {
            msSQL = " select permission_gid from  hrm_trn_tpermission where  employee_gid='" + employee_gid + "'" +
                    " and permission_date='" + values.permission_date +"'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows ==true )
            {
                return false ;
                
            }

            msSQL = " Select a.holiday_name,b.employee_gid from hrm_mst_tholiday a " +
                    " inner join hrm_mst_tholiday2employee b on a.holiday_gid=b.holiday_gid " +
                    " where a.holiday_date='" + values.permission_date + "' and b.employee_gid='" + employee_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows ==true )
            {
                return false;
            }

            return true ;
        }
    }
}
