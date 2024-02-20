using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.master.Models;
using System.Configuration;

namespace ems.master.DataAccess
{
    public class DaMstCourierAck
    {
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDataReader;
        dbconn objdbconn = new dbconn();
        string msSQL;
        int mnResult;
        string lscompany_name = string.Empty;

        public void DaGetAcknowledgeForm(string courierMgmt_gid, MdlCourierAckDtl values)
        {
            try
            {

                lscompany_name = ConfigurationManager.AppSettings[HttpContext.Current.Request.Url.Host].ToString();

                msSQL = " SELECT courierref_no, couriermgmt_gid,date_format(date_of_courier,'%d-%m-%Y') as date_of_courier," +
                        " address,sanction_gid,sanctionref_no,customer_gid," +
                        " customer_name,document_type,sender_name,pod_no,couriercompany_name," +
                        " courierhandover_to,courier_type,remarks,date_format(ack_date,'%d-%m-%Y') as ack_date," +
                        " address,ack_status,ackby_name" +
                        " FROM " + lscompany_name + ".ocs_trn_tcouriermgnt" +
                        " WHERE couriermgmt_gid='" + courierMgmt_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    objODBCDataReader.Read();

                    values.courierref_no = objODBCDataReader["courierref_no"].ToString();
                    values.courierMgmt_gid = objODBCDataReader["couriermgmt_gid"].ToString();
                    values.date_of_courier = objODBCDataReader["date_of_courier"].ToString();
                    values.sanction_gid = objODBCDataReader["sanction_gid"].ToString();
                    values.sanctionref_no = objODBCDataReader["sanctionref_no"].ToString();
                    values.customer_gid = objODBCDataReader["customer_gid"].ToString();
                    values.customer_name = objODBCDataReader["customer_name"].ToString();
                    values.document_type = objODBCDataReader["document_type"].ToString();
                    values.sender_name = objODBCDataReader["sender_name"].ToString();
                    values.pod_no = objODBCDataReader["pod_no"].ToString();
                    values.couriercompany_name = objODBCDataReader["couriercompany_name"].ToString();
                    values.courierhandover_to = objODBCDataReader["courierhandover_to"].ToString();
                    values.courier_type = objODBCDataReader["courier_type"].ToString();
                    values.address = objODBCDataReader["address"].ToString();
                    values.ack_status = objODBCDataReader["ack_status"].ToString();
                    values.remarks = objODBCDataReader["remarks"].ToString();
                    values.ack_date = objODBCDataReader["ack_date"].ToString();
                    values.ackby_name = objODBCDataReader["ackby_name"].ToString();
                }
                objODBCDataReader.Close();
                values.status = true;
                values.message = "Data Fetched";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.Message;
            }
        }
        public result DaPostAckStatus(MdlMstCourierAck values)
        {
            lscompany_name = ConfigurationManager.AppSettings[HttpContext.Current.Request.Url.Host].ToString();
            result objResult = new Models.result();

            msSQL = " UPDATE " + lscompany_name + ".ocs_trn_tcouriermgnt SET" +
                    " ack_status='Acknowledged'," +
                    " ack_date=current_timestamp," +
                    " ackby_gid='" + values.employee_gid + "'," +
                    " ackby_name=(select concat(a.user_firstname,' ',a.user_lastname,'/',a.user_code) from " + lscompany_name + ".adm_mst_tuser a left join " + lscompany_name + ".hrm_mst_temployee b ON a.user_gid=b.user_gid where b.employee_gid='" + values.employee_gid + "')" +
                    " WHERE couriermgmt_gid='" + values.CourierMgmt_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                objResult.status = true;
                objResult.message = "Acknowledged Successfully";
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error";
            }
            return objResult;
        }
    }
}