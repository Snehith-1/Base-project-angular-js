using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using System.Net;
using System.IO;
using ems.utilities.Functions;
using ems.lgl.Models;
using System.Configuration;
using System.Drawing;

namespace ems.lgl.DataAccess
{
    public class DaLglMstServiceType
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, msGetGid;
        int mnResult;

        public bool DaPostservicetype(string employee_gid, MdlLglMstServiceType values)
        {
            if ((values.service_type == "Others") || (values.service_type == "Other") || (values.service_type == "other") || (values.service_type == "others"))
            {
                values.status = false;
                values.message = "Can't able to add this Service Type";
                return false;
            }
            msSQL = "select service_type from lgl_mst_tservicetype where service_type='" + values.service_type + "'or service_code='" + values.service_code + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                msGetGid = objcmnfunctions.GetMasterGID("LRQT");

            msSQL = "insert into lgl_mst_tservicetype(" +
                      " servicetype_gid ," +
                      " service_code ," +
                      " service_type ," +
                      " created_by," +
                      " created_date)" +
                      " values(" +
                      "'" + msGetGid + "'," +
                      "'" + values.service_code.Replace("'", "") + "'," +
                      "'" + values.service_type.Replace("'", "") + "'," +
                      "'" + employee_gid + "'," +
                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            objODBCDatareader.Close();

                if (mnResult != 0)
                {


                values.status = true;
                values.message = "Service Type Added Sucessfully";
                return true;
                }
                else
               {
                values.status = false;
                values.message = "Error Occured while adding";
                return false;
               }

                
            }
            else
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Service Type / Code Already Exists";
                return false;
            }

        }
        public bool Dagetservicetype(MdlLglMstServiceType values, string employee_gid)
        {

            msSQL = "select a.servicetype_gid,a.service_code,a.service_type,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,"+
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by from lgl_mst_tservicetype a " +
                " left join hrm_mst_temployee b on a.created_by=b.employee_gid"+
                " left join adm_mst_tuser c on c.user_gid=b.user_gid order by a.servicetype_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getservicetype_list = new List<servicetype_list>();
            if (dt_datatable.Rows.Count != 0)
            { 
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    getservicetype_list.Add(new servicetype_list
                    {
                        servicetype_gid = (dr_datarow["servicetype_gid"].ToString()),
                        service_code = (dr_datarow["service_code"].ToString()),
                        service_type = (dr_datarow["service_type"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                    });
                }
            values.servicetype_list = getservicetype_list;

            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }
        public bool Daupdateservicetype(string employee_gid, MdlLglMstServiceType values)
        {
            if ((values.service_type == "Others") || (values.service_type == "Other") || (values.service_type == "other") || (values.service_type == "others"))
            {
                values.status = false;
                values.message = "Can't able to add this Service Type";
                return false;
            }
            msSQL = "update lgl_mst_tservicetype set" +
                     " service_code='" + values.service_code + "'," +
                     " service_type='" + values.service_type + "'," +
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where servicetype_gid='" + values.servicetype_gid + "'";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {


                values.status = true;
                values.message = "Service Type updated Sucessfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating ";
                return false;
            }
          
           
        }
        public bool Daeditservicetype(string employee_gid, string servicetype_gid, MdlLglMstServiceType values)
        {

            msSQL = "select servicetype_gid,service_code,service_type from lgl_mst_tservicetype where servicetype_gid='" + servicetype_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.servicetype_gid = objODBCDatareader["servicetype_gid"].ToString();
                values.service_code = objODBCDatareader["service_code"].ToString();
                values.service_type = objODBCDatareader["service_type"].ToString();
            }
            objODBCDatareader.Close();
            values.status = true;
            return true;
        }
        public bool Dadeleteservicetype(string servicetype_gid, MdlLglMstServiceType values)
        {

            msSQL = "delete from lgl_mst_tservicetype where servicetype_gid='" + servicetype_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Request Type deleted Sucessfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while deleting ";
                return false;
            }
        }

        public bool Dagetservicetype2invoice(MdlLglMstServiceType values, string employee_gid)
        {

            msSQL = "select servicetype_gid,service_code,service_type from lgl_mst_tservicetype";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getservicetype_list = new List<servicetype_list>();
            if (dt_datatable.Rows.Count != 0)
            { 
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    getservicetype_list.Add(new servicetype_list
                    {
                        servicetype_gid = (dr_datarow["servicetype_gid"].ToString()),
                        service_code = (dr_datarow["service_code"].ToString()),
                        service_type = (dr_datarow["service_type"].ToString()),

                    });
                   
                }
            getservicetype_list.Add(new servicetype_list
            {
                service_type = "Others",
                servicetype_gid = "Others",
            });
            values.servicetype_list = getservicetype_list;
            }

            dt_datatable.Dispose();
            values.status = true;
            return true;
        }
    }
}