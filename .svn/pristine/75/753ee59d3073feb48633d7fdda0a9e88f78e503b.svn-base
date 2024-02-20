using ems.storage.Functions;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using ems.brs.Models;
using System.Data;
using System.Web.Http;
using System.Net.NetworkInformation;

namespace ems.brs.Dataacess
{
    /// <summary>
    /// The remianing is below 2 or 3  rupees the ticket will closed automatically condition based master
    /// </summary>
    ///<remarks>Written by Komathi</remarks>
    public class DaCloseRemaindingAmount
    {
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        string msSQL, msGetGid, lsstart_date, lsend_date, lsstartdate, lsenddate, lsremaining_date, lsremaining_datetime;
        OdbcDataReader objODBCDatareader;
        dbconn objdbconn = new dbconn();
        int mnResult;

        public void DaGetRemaindingAmount(MdlCloseRemaindingAmount objCloseRemaindingAmount)
        {
            try
            {
                msSQL = "select remaindingamount_gid,remaindingamount_name,remaindingamount_amount," +
                        "date_format(a.created_date,'%d-%b-%Y  %h:%i %p') as created_date," +
                        " a.remarks,if(status='Y','Active','Inactive') as status," +
                        " concat(user_code,'  ',user_firstname, '  ', user_lastname) as created_by from brs_mst_tremaindingamount a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid=b.user_gid";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getremaindingamount_list = new List<remaindingamount_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getremaindingamount_list.Add(new remaindingamount_list
                        {
                            remaindingamount_gid = dr_datarow["remaindingamount_gid"].ToString(),
                            remaindingamount_name = dr_datarow["remaindingamount_name"].ToString(),
                            remaindingamount_amount = dr_datarow["remaindingamount_amount"].ToString(),
                            created_by = dr_datarow["created_by"].ToString(),
                            created_date = dr_datarow["created_date"].ToString(),
                            remarks = dr_datarow["remarks"].ToString(),
                            remainding_status = dr_datarow["status"].ToString()
                        });
                    }
                    objCloseRemaindingAmount.remaindingamount_list = getremaindingamount_list;
                    objCloseRemaindingAmount.status = true;
                }
            }
            catch
            {
                objCloseRemaindingAmount.status=false;
            }
        }
        public void DaCreateRemaindingAmount( remaindingamount_list values,string employee_gid )
        {
            msGetGid = objcmnfunctions.GetMasterGID("CRAM");
            msSQL= "insert into brs_mst_tremaindingamount(" +
                    "remaindingamount_gid," +
                    "remaindingamount_name," +
                    "remaindingamount_amount,"+
                    "status," +
                    " remarks," +
                    " created_by," +
                    " created_date ) values (" +
                    "'"+ msGetGid +"',"+
                     "null," +
                    "'"+ values.remaindingamount_amount +"'," +
                    "'Y'," +
                    "'"+ values.remarks +"'," +
                    "'"+employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult=objdbconn.ExecuteNonQuerySQL(msSQL);
            if(mnResult==1)
            {
                values.status = true;
                values.message = "Remaiding Amount Added Successfully";
               
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding...";
            }

        }

       
        public void DaRemainingAmountClosed(MdlUnreconciliationManagement values, string user_gid)
        {

            msSQL = " select status  from brs_mst_tremaindingamount where remaindingamount_gid ='" + values.remaindingamount_gid + "'";
            string lsstatus = objdbconn.GetExecuteScalar(msSQL);

            if(lsstatus =="N")
            {
                values.message = "Remaining amount inactivated,so you can't able to further proceed";
                values.status = false;
                return;
            }

            try
            {
                lsstart_date = values.start_date;
                lsstartdate = GetDateFormat(lsstart_date);
                lsend_date = values.end_date;
                lsenddate = GetDateFormat(lsend_date);

                msSQL = " select banktransc_gid,transact_particulars,remaining_amount from brs_trn_tbanktransactiondetails where (created_date between '" + lsstartdate + "' and '" + lsenddate + "') and (remaining_amount < '" + values.remainding_amount + "') and (knockoff_status ='Pending')";

                dt_datatable = objdbconn.GetDataTable(msSQL);

                var getremainingamountlist = new List<remainingamountlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getremainingamountlist.Add(new remainingamountlist
                        {
                            banktransc_gid = (dr_datarow["banktransc_gid"].ToString()),
                            transact_particulars = (dr_datarow["transact_particulars"].ToString()),

                        });
                        msSQL = " update brs_mst_trepaymenttransaction set knockoff_status='Matched', " +
                                                    " knockoff_flag='Y', " +
                                                    " knockoff_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                                                    " banktransc_gid = '" + dr_datarow["banktransc_gid"].ToString() + "'" +
                                                    " where remarks = '" + dr_datarow["transact_particulars"].ToString() + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        msSQL = " update brs_trn_tbanktransactiondetails set knockoff_status='Matched' , " +
                                               " knockoff_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                                               " knockoff_flag='Y', " +
                                               " closed_by= '" + user_gid + "' " +
                                               " where banktransc_gid = '" + dr_datarow["banktransc_gid"].ToString() + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    values.remainingamountlist = getremainingamountlist;

                    values.status = true;
                    values.message = "Ticket Matched Sucessfully..!";
                }
                if (dt_datatable.Rows.Count == 0)
                {
                    values.status = false;
                    values.message = "Didn't show the remaining amount in selected date..!";
                }
                dt_datatable.Dispose();

            }
            catch (Exception ex)
            {
                values.status = false;
            }

        }
        public string GetDateFormat(string lsdate)
        {
            DateTime Date;
            string[] formats = { "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'" };
            DateTime.TryParseExact(lsdate, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out Date);

            lsremaining_date = Convert.ToDateTime(Date).AddDays(1).ToString("yyyy-MM-dd");
            lsremaining_datetime = DateTime.Now.ToString("HH:mm:ss");

            string wefdate = lsremaining_date + " " + lsremaining_datetime;
            return wefdate;
        }
        public void DaGetRemainingAmount(remaindingamount_list values, string remaindingamount_gid)
        {
            msSQL = "select remaindingamount_amount,remaindingamount_gid,status from brs_mst_tremaindingamount " +
                        "where remaindingamount_gid ='" + remaindingamount_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.remaindingamount_gid = objODBCDatareader["remaindingamount_gid"].ToString();
                values.remaindingamount_amount = objODBCDatareader["remaindingamount_amount"].ToString();
                values.Status = objODBCDatareader["status"].ToString();              
            }
            objODBCDatareader.Close();
            values.status = true;
        }

        public void DaGetRemainingAmountStatus(remaindingamount_list values)
        {
            msSQL = " select count(*)Active from brs_mst_tremaindingamount where status='Y' ";                       
            string active_count=objdbconn.GetExecuteScalar(msSQL);
            values.activeamount_count = active_count;


        }
        public void DaInactiveRemaindingAmount(remaindingamount_list values, string employee_gid)
        {
            msSQL = " update brs_mst_tremaindingamount set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where remaindingamount_gid='" + values.remaindingamount_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("RAIL");

                msSQL = " insert into  brs_mst_tremainingamountinactivelog (" +
                      " remainingamountinactivelog_gid , " +
                      " remaindingamount_gid," +
                      " remaindingamount_amount," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.remaindingamount_gid + "'," +
                      " '" + values.remaindingamount_amount + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Remaining Amount Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Remaining Amount Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }
        public void DaRemaindingAmountInactiveLogview(string remaindingamount_gid, MdlCloseRemaindingAmount values)
        {
            try
            {
                msSQL = " SELECT a.remainingamountinactivelog_gid,remaindingamount_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM brs_mst_tremainingamountinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.remaindingamount_gid ='" + remaindingamount_gid + "' order by a.remainingamountinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getinactiveremaindingamount_list = new List<inactiveremaindingamount_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getinactiveremaindingamount_list.Add(new inactiveremaindingamount_list
                        {
                            remaindingamount_gid = (dr_datarow["remaindingamount_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.inactiveremaindingamount_list = getinactiveremaindingamount_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
    }
}