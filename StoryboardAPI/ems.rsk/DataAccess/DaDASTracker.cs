using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.rsk.Models;
using ems.utilities.Functions;
using System.Net;

namespace ems.rsk.DataAccess
{
    public class DaDASTracker
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDataReader;
        DataTable dt_datatable, dt_table;
        string msSQL, msGetGid, msRefGid;
        int mnResult;

        public bool DaPostAcknowledgedBuyers(string employee_gid, acknowledgedbuyers values)
        {

            msGetGid = objcmnfunctions.GetMasterGID("ACKB");

            msSQL = " insert into rsk_mst_tacknowledgedbuyers (" +
                   " acknowledgedbuyers_gid ," +
                   " customer_gid," +
                   " acknowledged_buyers," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + values.customer_gid + "'," +
                   "'" + values.acknowledged_buyers + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Acknowledged buyers are Added Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public bool DaGetAcknowledgedBuyers(string customer_gid, listacknowledgedbuyers values)
        {
            msSQL = "select acknowledgedbuyers_gid,acknowledged_buyers from rsk_mst_tacknowledgedbuyers where customer_gid='" + customer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getacknowledgedbuyers = new List<acknowledgedbuyers>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getacknowledgedbuyers.Add(new acknowledgedbuyers
                    {
                        acknowledgedbuyers_gid = dt["acknowledgedbuyers_gid"].ToString(),
                        acknowledged_buyers = dt["acknowledged_buyers"].ToString(),
                    });
                    values.acknowledgedbuyers = getacknowledgedbuyers;
                }
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaDeleteAcknowledgedBuyers(string acknowledgedbuyers_gid, acknowledgedbuyers values)
        {

            msSQL = "delete from rsk_mst_tacknowledgedbuyers where acknowledgedbuyers_gid ='" + acknowledgedbuyers_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Acknowledged buyer Details are Deleted Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public bool getacknowledgedbuyersdtl_da(string acknowledgedbuyers_gid, acknowledgedbuyers values)
        {

            msSQL = " select acknowledgedbuyers_gid,acknowledged_buyers from rsk_mst_tacknowledgedbuyers " +
                    " where acknowledgedbuyers_gid ='" + acknowledgedbuyers_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.acknowledgedbuyers_gid = objODBCDataReader["acknowledgedbuyers_gid"].ToString();
                values.acknowledged_buyers = objODBCDataReader["acknowledged_buyers"].ToString();
            }
            objODBCDataReader.Close();
            if (mnResult != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DaPostUpdateAcknowledgedBuyers(string employee_gid, acknowledgedbuyers values)
        {

            msSQL = " update rsk_mst_tacknowledgedbuyers set " +
                   " acknowledged_buyers= '" + values.acknowledged_buyers + "'," +
                   " updated_by= '" + employee_gid + "'," +
                   " updated_date= '" + DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss") + "'," +
                   " where acknowledgedbuyers_gid ='" + values.acknowledgedbuyers_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Acknowledged buyer details are Updated Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public bool DaPostRemitterBuyers(string employee_gid, remitterbuyers values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("UACK");

            msSQL = " insert into  rsk_mst_tremitterbuyers (" +
                   " remitterbuyers_gid ," +
                   " customer_gid," +
                   " remitter_status," +
                   " remitter_self," +
                   " remitter_ackbuyersgid," +
                   " remitter_ackbuyers," +
                   " remitter_unackbuyers," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + values.customer_gid + "'," +
                   "'" + values.remitter_status + "'," +
                   "'" + values.remitter_self + "'," +
                   "'" + values.remitter_ackbuyersgid + "'," +
                   "'" + values.remitter_ackbuyers + "'," +
                   "'" + values.remitter_unackbuyers + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Remitter buyers are Added Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public bool DaGetRemitterBuyers(string customer_gid, listremitterbuyers values)
        {
            msSQL = " Select a.remitterbuyers_gid,a.remitter_status,(case when a.remitter_status='Self' then b.customername " +
                    " when a.remitter_status = 'Acknowledged buyer' then a.remitter_ackbuyers " +
                    " else a.remitter_unackbuyers End ) as remitter_buyer from rsk_mst_tremitterbuyers a " +
                    " left join ocs_mst_tcustomer b on a.customer_gid = b.customer_gid " +
                    " where a.customer_gid = '" + customer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getremitterbuyers = new List<remitterbuyers>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getremitterbuyers.Add(new remitterbuyers
                    {
                        remitterbuyers_gid = dt["remitterbuyers_gid"].ToString(),
                        remitter_status = dt["remitter_status"].ToString(),
                        remitter_buyer = dt["remitter_buyer"].ToString()
                    });
                    values.remitterbuyers = getremitterbuyers;
                }
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaDeleteRemitterBuyers(string remitterbuyers_gid, remitterbuyers values)
        {
            msSQL = "delete from rsk_mst_tremitterbuyers where remitterbuyers_gid='" + remitterbuyers_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult!=0)
            {
                values.status = true;
                values.message = "Remitter buyer Details are Deleted Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }
    }
}