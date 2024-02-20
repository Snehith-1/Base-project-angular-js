using ems.idas.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Web;
using System.Linq;

namespace ems.idas.DataAccess
{
    public class DaIdasMstCourierCompany
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        string msSQL, msGetGID, msGetGIDRef;
        OdbcDataReader objODBCDataReader;
        int mnResult;

        public result DaPostCourierCompany(MdlCourierCompany values,string user_gid)
        {
            result objResult = new Models.result();

            msSQL = " select couriercompany_gid from ids_mst_tcouriercompany where couriercompany_name='" + values.couriercompany_name.Replace("'", "") + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader .HasRows==true )
            {
                objODBCDataReader.Close();
                objResult.status = false;
                objResult.message = "Courier Company Name Already Exists.";
                return objResult;
            }
            objODBCDataReader.Close();

            msGetGID = objcmnfunctions.GetMasterGID("CMPY");
            msGetGIDRef = objcmnfunctions.GetMasterGID("COUR");

            msSQL = " INSERT INTO ids_mst_tcouriercompany(" +
                    " couriercompany_gid," +
                    " couriercompany_code," +
                    " couriercompany_name," +
                    " description," +
                    " created_by)" +
                    " VALUES(" +
                    "'" + msGetGID + "'," +
                    "'" + msGetGIDRef + "'," +
                    "'" + values.couriercompany_name.Replace("'", "") + "'," +
                    "'" + values.description.Replace("'", "") + "'," +
                    "'" + user_gid + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult==1)
            {
                objResult.status = true;
                objResult.message = "Courier Company Name Added Successfully";
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";
            }

            return objResult; 

        }
        public result DaPostUpdateCourierCompany(MdlCourierCompany values, string user_gid)
        {
            result objResult = new Models.result();
            msSQL = " select couriercompany_gid from ids_mst_tcouriercompany where couriercompany_name='" + values.couriercompany_name.Replace("'", "") + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                if (objODBCDataReader ["couriercompany_gid"].ToString ()!= values.couriercompany_gid)
                {
                    objODBCDataReader.Close();
                    objResult.status = false;
                    objResult.message = "Courier Company Name Already Exists.";
                    return objResult;
                }
              
            }
            objODBCDataReader.Close();


            msSQL = " Update ids_mst_tcouriercompany set" +
                    " couriercompany_name='" + values.couriercompany_name.Replace("'", "") + "'," +
                    " description='" + values.description.Replace("'", "") + "'," +
                    " updated_date=current_timestamp," +
                    " updated_by='" + user_gid + "'" +
                    " where couriercompany_gid='" + values.couriercompany_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                objResult.status = true;
                objResult.message = "Courier Company Name Updated Successfully";
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";
            }

            return objResult;

        }
        public result DaPostDeleteCourierCompany(string couriercompany_gid)
        {
            result objResult = new Models.result();
            msSQL = " DELETE FROM ids_mst_tcouriercompany WHERE couriercompany_gid = '" + couriercompany_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult ==1)
            {
                objResult.status = true;
                objResult.message = "Courier Company Deleted Successfully";
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";
            }

            return objResult;
        }
        public void DaGetCourierCompanySummary(MdlCourierCompanySummary values)
        {
            msSQL = " SELECT couriercompany_gid, couriercompany_code,couriercompany_name,description" +
                    " FROM ids_mst_tcouriercompany " +
                    " WHERE 1=1 ORDER BY created_date DESC";
            dt_datatable = objdbconn.GetDataTable(msSQL );
            if (dt_datatable .Rows .Count !=0)
            {
                values.MdlCourierCompany =dt_datatable .AsEnumerable ().Select(row=>new MdlCourierCompany
                {
                    couriercompany_gid =row["couriercompany_gid"].ToString (),
                    couriercompany_name =row["couriercompany_name"].ToString (),
                    couriercompany_code =row["couriercompany_code"].ToString (),
                    description =row["description"].ToString ()
                }).ToList();
                values.status = true;
                values.message = "Data Fetched";
            }
            else
            {
                values.status = false;
                values.message = "Record Not Found";
            }
            dt_datatable.Dispose();
        }
    }
}