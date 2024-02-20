using ems.brs.Models;
using ems.storage.Functions;
using ems.utilities.Functions;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace ems.brs.Dataacess
{
    /// <summary>
    ///The uploaded Pending ticket is showing Unrecon-Management Pending Summary. Assigned ticket showing assigned summary and Re assign ticket showing reassign summary.
    ///Matched and Closed cases showing Closed Summary
    ///In Pending Summary we can adjust/advice the transaction amount or assign any one person.
    ///Assigned person is closed the ticket or send back to the customer.
    /// If the user select Adjust against the Repayment/ Refund  there is no changes in remaining amount.
    ///  If the user select Booked in LMS / FA the remaining amount is reduce based on  user amount.
    ///  remaining is 0.00 the ticket is acknowledge or closed.
    /// </summary>
    /// <remarks>Written by Santhana Kumar </remarks>
    public class DaUnreconciliationManagement
    {
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        string msSQL, msGetGid,lsstart_date,lsend_date,lsstartdate,lsenddate, lsremaining_date, lsremaining_datetime, msGetGid1, MSGETGID, lsbrs_gid,lsaction_name, lsremaining_amount, lsadvice_amount, lsadjust_amount, lsbanktransac_name, employeename, msGetGid2, lsbanktrans_gid;
        OdbcDataReader objODBCDatareader;
        dbconn objdbconn = new dbconn();
        HttpPostedFile httpPostedFile;
        string lspath, lscreated_by, endRange, excelRange, lscustref_no, lsemployee_name, lsbank_name, lsadjusted_amount, lspendingremainingamount;
        int mnresult;
        decimal adjusted_amount, advice_amount, remaining_amount ,lsadjustedamount, lsaddremainingamount;
        int rowCount, columnCount;
        string lstaggedmember_name, lssamfincustomer_name, lstaggedmember_gid, lstagemployee_gid;
        string unreconallocation_gid, assignedrm_date, assigned_rm, assignedrm_name;
        //ExcelPackage xlPackage = new ExcelPackage();
        ExcelPackage xlPackage;
        public void DaGetunreConcillationSummary(unrecocillationlist values, string user_gid)
        {
            try
            {

                msSQL = " SELECT a.knockoff_status,a.banktransc_refno,a.banktransc_gid,a.transact_particulars,"+
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " FORMAT(a.transact_val,2,'en_IN') as transact_val,FORMAT(a.remaining_amount,2,'en_IN') as remaining_amount," +
                        " a.knockoff_flag,a.cr_dr,date_format(a.trn_date,'%d-%b-%Y') as trn_date,b.bank_name,a.tagged_status,a.knockoffby_finance" +
                        " FROM brs_trn_tbanktransactiondetails  a  " +
                        " left join brs_mst_tbank b  on a.bank_gid =b.bank_gid  " +
                        " left join adm_mst_tuser c on a.created_by= c.user_gid " +
                        " where ((a.cr_dr ='CR' and  a.knockoff_status='Pending') and " +
                        " (a.cr_dr ='CR' and  a.tagged_status='Pending')) order by a.created_date asc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getunrecocillationcreditlist = new List<unrecocillationlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getunrecocillationcreditlist.Add(new unrecocillationlist
                        {
                            banktransc_gid = (dr_datarow["banktransc_gid"].ToString()),
                            banktransc_refno = (dr_datarow["banktransc_refno"].ToString()),
                            knockoff_status = (dr_datarow["knockoff_status"].ToString()),
                            transact_particulars = (dr_datarow["transact_particulars"].ToString()),
                            transact_val = (dr_datarow["transact_val"].ToString()),
                            trn_date = (dr_datarow["trn_date"].ToString()),
                            cr_dr = (dr_datarow["cr_dr"].ToString()),
                            knockoff_flag = (dr_datarow["knockoff_flag"].ToString()),
                            bank_name = (dr_datarow["bank_name"].ToString()),
                            tagged_status = (dr_datarow["tagged_status"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            remaining_amount = (dr_datarow["remaining_amount"].ToString()),
                            knockoffby_finance = (dr_datarow["knockoffby_finance"].ToString()),


                        });
                    }
                    values.unrecocillationpendingcredit_list = getunrecocillationcreditlist;
                }
                msSQL = " SELECT a.knockoff_status,a.banktransc_refno,a.banktransc_gid,a.transact_particulars,FORMAT(a.transact_val,2,'en_IN') as transact_val," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " FORMAT(a.remaining_amount,2,'en_IN') as remaining_amount,a.knockoff_flag,a.cr_dr,date_format(a.trn_date,'%d-%b-%Y') as trn_date," +
                        " b.bank_name,a.tagged_status,a.knockoffby_finance FROM brs_trn_tbanktransactiondetails  a  " +
                        " left join brs_mst_tbank b  on a.bank_gid =b.bank_gid  " +
                        " left join adm_mst_tuser c on a.created_by= c.user_gid " +
                        " where ((a.cr_dr ='DR' and  a.knockoff_status='Pending') and (a.cr_dr ='DR' and  a.tagged_status='Pending')) order by a.created_date asc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getunrecocillationdebitlist = new List<unrecocillationlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getunrecocillationdebitlist.Add(new unrecocillationlist
                        {
                            banktransc_gid = (dr_datarow["banktransc_gid"].ToString()),
                            banktransc_refno = (dr_datarow["banktransc_refno"].ToString()),
                            knockoff_status = (dr_datarow["knockoff_status"].ToString()),
                            transact_particulars = (dr_datarow["transact_particulars"].ToString()),
                            transact_val = (dr_datarow["transact_val"].ToString()),
                            trn_date = (dr_datarow["trn_date"].ToString()),
                            cr_dr = (dr_datarow["cr_dr"].ToString()),
                            knockoff_flag = (dr_datarow["knockoff_flag"].ToString()),
                            bank_name = (dr_datarow["bank_name"].ToString()),
                            tagged_status = (dr_datarow["tagged_status"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            remaining_amount = (dr_datarow["remaining_amount"].ToString()),
                            knockoffby_finance = (dr_datarow["knockoffby_finance"].ToString()),


                        });
                    }
                    values.unrecocillationpendingdebit_list = getunrecocillationdebitlist;
                }
                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }

        public void DaGetunreConcillationFinanceSummary(unrecocillationlist values, string user_gid)
        {
            try
            {

                msSQL = " SELECT a.knockoff_status,a.banktransc_refno,a.banktransc_gid,a.transact_particulars," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " FORMAT(a.transact_val,2,'en_IN') as transact_val,FORMAT(a.remaining_amount,2,'en_IN') as remaining_amount," +
                        " a.knockoff_flag,a.cr_dr,date_format(a.trn_date,'%d-%b-%Y') as trn_date,b.bank_name,a.tagged_status,a.knockoffby_finance" +
                        " FROM brs_trn_tbanktransactiondetails  a  " +
                        " left join brs_mst_tbank b  on a.bank_gid =b.bank_gid  " +
                        " left join adm_mst_tuser c on a.created_by= c.user_gid " +
                        " where ((a.cr_dr ='CR' and  a.knockoff_status='FinancePending') and " +
                        " (a.cr_dr ='CR' and  a.tagged_status='Pending')) order by a.created_date asc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getunrecocillationcreditfinlist = new List<unrecocillationfinlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getunrecocillationcreditfinlist.Add(new unrecocillationfinlist
                        {
                            banktransc_gid = (dr_datarow["banktransc_gid"].ToString()),
                            banktransc_refno = (dr_datarow["banktransc_refno"].ToString()),
                            knockoff_status = (dr_datarow["knockoff_status"].ToString()),
                            transact_particulars = (dr_datarow["transact_particulars"].ToString()),
                            transact_val = (dr_datarow["transact_val"].ToString()),
                            trn_date = (dr_datarow["trn_date"].ToString()),
                            cr_dr = (dr_datarow["cr_dr"].ToString()),
                            knockoff_flag = (dr_datarow["knockoff_flag"].ToString()),
                            bank_name = (dr_datarow["bank_name"].ToString()),
                            tagged_status = (dr_datarow["tagged_status"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            remaining_amount = (dr_datarow["remaining_amount"].ToString()),
                            knockoffby_finance = (dr_datarow["knockoffby_finance"].ToString()),


                        });
                    }
                    values.unrecocillationfinpendingcredit_list = getunrecocillationcreditfinlist;
                }
                msSQL = " SELECT a.knockoff_status,a.banktransc_refno,a.banktransc_gid,a.transact_particulars,FORMAT(a.transact_val,2,'en_IN') as transact_val," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " FORMAT(a.remaining_amount,2,'en_IN') as remaining_amount,a.knockoff_flag,a.cr_dr,date_format(a.trn_date,'%d-%b-%Y') as trn_date," +
                        " b.bank_name,a.tagged_status,a.knockoffby_finance FROM brs_trn_tbanktransactiondetails  a  " +
                        " left join brs_mst_tbank b  on a.bank_gid =b.bank_gid  " +
                        " left join adm_mst_tuser c on a.created_by= c.user_gid " +
                        " where ((a.cr_dr ='DR' and  a.knockoff_status='FinancePending') and (a.cr_dr ='DR' and  a.tagged_status='Pending')) order by a.created_date asc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getunrecocillationfindebitlist = new List<unrecocillationfinlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getunrecocillationfindebitlist.Add(new unrecocillationfinlist
                        {
                            banktransc_gid = (dr_datarow["banktransc_gid"].ToString()),
                            banktransc_refno = (dr_datarow["banktransc_refno"].ToString()),
                            knockoff_status = (dr_datarow["knockoff_status"].ToString()),
                            transact_particulars = (dr_datarow["transact_particulars"].ToString()),
                            transact_val = (dr_datarow["transact_val"].ToString()),
                            trn_date = (dr_datarow["trn_date"].ToString()),
                            cr_dr = (dr_datarow["cr_dr"].ToString()),
                            knockoff_flag = (dr_datarow["knockoff_flag"].ToString()),
                            bank_name = (dr_datarow["bank_name"].ToString()),
                            tagged_status = (dr_datarow["tagged_status"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            remaining_amount = (dr_datarow["remaining_amount"].ToString()),
                            knockoffby_finance = (dr_datarow["knockoffby_finance"].ToString()),


                        });
                    }
                    values.unrecocillationfinpendingdebit_list = getunrecocillationfindebitlist;
                }
                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }

        public void DaGetunreConreassignpendingSummary(unrecocillationlist values, string user_gid)
        {
            try
            {

                msSQL = " SELECT a.knockoff_status,a.banktransc_refno,a.banktransc_gid,a.transact_particulars,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,FORMAT(a.transact_val,2,'en_IN') as transact_val,FORMAT(a.remaining_amount,2,'en_IN') as remaining_amount,a.knockoff_flag,a.cr_dr,date_format(a.trn_date,'%d-%b-%Y') as trn_date,b.bank_name,a.tagged_status, CONCAT(FLOOR(timestampdiff(day, a.rmsendback_on, now())), ' days ') as aging,CONCAT(FLOOR(timestampdiff(day, a.trn_date, now())), ' days ') as transactionaging " +
                    " FROM brs_trn_tbanktransactiondetails  a  " +
                     " left join brs_mst_tbank b  on a.bank_gid =b.bank_gid  " +
                      " left join adm_mst_tuser c on a.created_by= c.user_gid " +
                        " where ((a.cr_dr ='CR' and  a.knockoff_status='Pending') and (a.cr_dr ='CR' and  a.tagged_status='Reassign')) order by a.created_date asc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getunrecocillationcreditlist = new List<unrecocillationlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getunrecocillationcreditlist.Add(new unrecocillationlist
                        {
                            banktransc_gid = (dr_datarow["banktransc_gid"].ToString()),
                            banktransc_refno = (dr_datarow["banktransc_refno"].ToString()),
                            knockoff_status = (dr_datarow["knockoff_status"].ToString()),
                            transact_particulars = (dr_datarow["transact_particulars"].ToString()),
                            transact_val = (dr_datarow["transact_val"].ToString()),
                            trn_date = (dr_datarow["trn_date"].ToString()),
                            cr_dr = (dr_datarow["cr_dr"].ToString()),
                            knockoff_flag = (dr_datarow["knockoff_flag"].ToString()),
                            bank_name = (dr_datarow["bank_name"].ToString()),
                            tagged_status = (dr_datarow["tagged_status"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            remaining_amount = (dr_datarow["remaining_amount"].ToString()),
                            aging = (dr_datarow["aging"].ToString()),
                            transaction_aging = (dr_datarow["transactionaging"].ToString())

                        });
                    }
                    values.unrecocillationpendingcredit_list = getunrecocillationcreditlist;
                }
                msSQL = " SELECT a.knockoff_status,a.banktransc_refno,a.banktransc_gid,a.transact_particulars,FORMAT(a.transact_val,2,'en_IN') as transact_val,FORMAT(a.remaining_amount,2,'en_IN') as remaining_amount,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.knockoff_flag,a.cr_dr,date_format(a.trn_date,'%d-%b-%Y') as trn_date,b.bank_name,a.tagged_status,CONCAT(FLOOR(timestampdiff(day, a.rmsendback_on, now())), ' days ') as aging,CONCAT(FLOOR(timestampdiff(day, a.trn_date, now())), ' days ') as transactionaging FROM brs_trn_tbanktransactiondetails  a  " +
                    " left join brs_mst_tbank b  on a.bank_gid =b.bank_gid  " +
                     " left join adm_mst_tuser c on a.created_by= c.user_gid " +
                       " where ((a.cr_dr ='DR' and  a.knockoff_status='Pending') and (a.cr_dr ='DR' and  a.tagged_status='Reassign')) order by a.created_date asc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getunrecocillationdebitlist = new List<unrecocillationlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getunrecocillationdebitlist.Add(new unrecocillationlist
                        {
                            banktransc_gid = (dr_datarow["banktransc_gid"].ToString()),
                            banktransc_refno = (dr_datarow["banktransc_refno"].ToString()),
                            knockoff_status = (dr_datarow["knockoff_status"].ToString()),
                            transact_particulars = (dr_datarow["transact_particulars"].ToString()),
                            transact_val = (dr_datarow["transact_val"].ToString()),
                            trn_date = (dr_datarow["trn_date"].ToString()),
                            cr_dr = (dr_datarow["cr_dr"].ToString()),
                            knockoff_flag = (dr_datarow["knockoff_flag"].ToString()),
                            bank_name = (dr_datarow["bank_name"].ToString()),
                            tagged_status = (dr_datarow["tagged_status"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            remaining_amount = (dr_datarow["remaining_amount"].ToString()),
                            aging = (dr_datarow["aging"].ToString()),

                            transaction_aging = (dr_datarow["transactionaging"].ToString())


                        });
                    }
                    values.unrecocillationpendingdebit_list = getunrecocillationdebitlist;
                }
                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }
        //public void DaGetunreConcillationTagCompleted(unrecocillationTaglist values, string user_gid)
        //{
        //    try
        //    {

        //        msSQL = " SELECT a.knockoff_status,a.banktransc_refno,a.banktransc_gid,a.transact_particulars,a.transact_val,a.knockoff_flag,a.cr_dr,a.trn_date,b.bank_name,a.tagged_status FROM brs_trn_tbanktransactiondetails  a  " +
        //             " left join brs_mst_tbank b  on a.bank_gid =b.bank_gid  " +
        //                " where  a.tagged_status='Assigned' order by a.created_date desc ";

        //        dt_datatable = objdbconn.GetDataTable(msSQL);
        //        var getunrecocillationtaglist = new List<unrecocillationTaglist>();
        //        if (dt_datatable.Rows.Count != 0)
        //        {
        //            foreach (DataRow dr_datarow in dt_datatable.Rows)
        //            {
        //                getunrecocillationtaglist.Add(new unrecocillationTaglist
        //                {
        //                    banktransc_gid = (dr_datarow["banktransc_gid"].ToString()),
        //                    banktransc_refno = (dr_datarow["banktransc_refno"].ToString()),
        //                    knockoff_status = (dr_datarow["knockoff_status"].ToString()),
        //                    transact_particulars = (dr_datarow["transact_particulars"].ToString()),
        //                    transact_val = (dr_datarow["transact_val"].ToString()),
        //                    trn_date = (dr_datarow["trn_date"].ToString()),
        //                    cr_dr = (dr_datarow["cr_dr"].ToString()),
        //                    knockoff_flag = (dr_datarow["knockoff_flag"].ToString()),
        //                    bank_name = (dr_datarow["bank_name"].ToString()),
        //                    tagged_status = (dr_datarow["tagged_status"].ToString())


        //                });
        //            }
        //            values.unrecocillationtag_list = getunrecocillationtaglist;
        //        }
        //        dt_datatable.Dispose();

        //        values.status = true;
        //    }
        //    catch
        //    {
        //        values.status = false;
        //    }

        //}
        public void DaPost2ReAssign(MdlUnreconciliationManagement values, string employee_gid, string user_gid)
        {
            int mnresult1;

            msSQL = " select created_date as assignedrm_date,taggedmember_gid as assigned_rm, " +
                       "  taggedmember_name as assignedrm_name " +
                       " from brs_trn_ttagemployee  " +
                       " where banktransc_gid='" + values.banktransc_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                assignedrm_date = objODBCDatareader["assignedrm_date"].ToString();
                assigned_rm = objODBCDatareader["assigned_rm"].ToString();
                assignedrm_name = objODBCDatareader["assignedrm_name"].ToString();

            }
            objODBCDatareader.Close();

            msSQL = " delete from brs_trn_ttagemployee where banktransc_gid= '" + values.banktransc_gid + "'";

            mnresult1 = objdbconn.ExecuteNonQuerySQL(msSQL);

            msGetGid = objcmnfunctions.GetMasterGID("BRSA");
            msSQL = " insert into brs_trn_ttagemployee(" +
                    " tagemployee_gid," +
                    " banktransc_gid," +
                    " taggedmember_gid," +
                    " taggedmember_name," +
                    " tagged_remarks," +
                    " tagged_date," +
                    " created_by," +
                    " created_date )" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.banktransc_gid + "'," +
                    "'" + values.assigned_to + "'," +
                    "'" + values.assigned_toname + "'," +
                    "'" + values.assigned_remarks.Replace("'", "\\'") + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult == 0)
            {
                values.status = false;
                values.message = "Error occurred while assigning..!";
                return;
            }
            else
            {

                msSQL = " update brs_trn_tbanktransactiondetails set taggedmember_gid='" + values.assigned_to + "'," +
                         " taggedmember_name= '" + values.assigned_toname + "'," +
                         " tagged_status='Assigned' " +
                         " where banktransc_gid='" + values.banktransc_gid + "'";

                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);



                unreconallocation_gid = objdbconn.GetExecuteScalar("select unreconallocation_gid from brs_trn_tunreconallocation where ticketref_no='" + values.banktransc_gid + "'");


                msSQL = " update brs_trn_tunreconallocation set created_by = '" + user_gid + "', " +
                        " created_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        " assigned_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                         " assignedto_gid ='" + values.assigned_to + "', " +
                          " assigned_toname='" + values.assigned_toname + "', " +
                          //" assigned_rm='" + assigned_rm + "', " +
                          // " assignedrm_date='" + assignedrm_date + "', " +
                          //  " assignedrm_name='" + assignedrm_name + "' ," +
                          " allocated_status = 'Pending' " +
                          " where unreconallocation_gid='" + unreconallocation_gid + "'";                 
                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnresult == 0)
                {
                    values.status = false;
                    values.message = "Error occurred while assigning..!";
                    return;
                }
                else
                {
                    msGetGid1 = objcmnfunctions.GetMasterGID("REAE");

                    msSQL = " insert into brs_trn_treassignemployeelog (" +
                          " reassignemployeelog_gid , " +
                          " banktransc_gid," +
                          " taggedmember_gid," +
                          " taggedmember_name," +
                          " tagged_remarks," +
                          " tagged_date," +
                          " created_by," +
                          " created_date) " +
                          " values (" +
                          " '" + msGetGid1 + "'," +
                            "'" + values.banktransc_gid + "'," +
                           "'" + values.assigned_to + "'," +
                           "'" + values.assigned_toname + "'," +
                           "'" + values.assigned_remarks.Replace("'", "\\'") + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                          " '" + employee_gid + "'," +
                          " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Assigned Successfully..!";

                }
            }
        }
        public void DaPost2Assign(MdlUnreconciliationManagement values, string employee_gid, string user_gid)
        {

            msGetGid = objcmnfunctions.GetMasterGID("BRSA");
            msSQL = " insert into brs_trn_ttagemployee(" +
                    " tagemployee_gid," +
                    " banktransc_gid," +
                    " taggedmember_gid," +
                    " taggedmember_name," +
                    " tagged_remarks," +
                    " tagged_date," +
                    " created_by," +
                    " created_date )" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.banktransc_gid + "'," +
                    "'" + values.assigned_to + "'," +
                    "'" + values.assigned_toname + "'," +
                    "'" + values.assigned_remarks.Replace("'", "\\'") + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult == 0)
            {
                values.status = false;
                values.message = "Error occurred while assigning..!";
                return;
            }
            else
            {

                msSQL = "update brs_trn_tbanktransactiondetails set taggedmember_gid='" + values.assigned_to + "'," +
                 " taggedmember_name= '" + values.assigned_toname + "'," +
                 " tagged_status='Assigned' where banktransc_gid='" + values.banktransc_gid + "'";
                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = " SELECT a.banktransc_gid,a.banktransc_refno,a.bankconfig_gid,a.bank_gid,b.bank_name,b.branch_name,b.acc_no,b.custref_no,a.reconcildoc_gid,DATE_FORMAT(a.trn_date, '%d-%m-%Y %h:%i %p') as trn_date, DATE_FORMAT(a.value_date, '%d-%m-%Y %h:%i %p') as value_date," +
              "DATE_FORMAT(a.payment_date, '%d-%m-%Y %h:%i %p') as payment_date , a.transact_particulars, a.debit_amt, a.credit_amt,FORMAT(a.transact_val,2,'en_IN') as transact_val, a.remarks, a.cr_dr, a.chq_no, a.created_by,  DATE_FORMAT(a.created_date, '%d-%m-%Y %h:%i %p') as created_date ," +
              "  a.transc_balance FROM brs_trn_tbanktransactiondetails a  " +
              " left join brs_mst_tbank b  on a.bank_gid =b.bank_gid  " +
                  " where a.banktransc_gid='" + values.banktransc_gid + "' order by a.created_date desc ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lscustref_no = objODBCDatareader["custref_no"].ToString();
                    lsbank_name = objODBCDatareader["bank_name"].ToString();

                }
                string lsdepartmentgid = objdbconn.GetExecuteScalar("select businessunit_gid from osd_mst_tbusinessunit where businessunit_name='Business Process'");
              
                msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                         "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                         "where b.employee_gid ='" + employee_gid + "'";
                employeename = objdbconn.GetExecuteScalar(msSQL);

                MSGETGID = objcmnfunctions.GetMasterGID("UNRE");
                msSQL = "Insert into brs_trn_tunreconallocation( " +
              " unreconallocation_gid," +
              " banktransc_gid," +
              " ticketref_no," +           
              " bank_name," +
              " customerref_no," +
               " assignedto_gid," +
              " assigned_toname," +
              " assignedby_gid," +
              " assigned_byname," +
               "assigned_date," +
               "assigned_remarks," +            
               "created_by," +
              " created_date)" +
              " values(" +
             "'" + MSGETGID + "'," +
              "'" + values.banktransc_gid + "'," +
                "'" + values.banktransc_gid + "'," +
                "'" + lsbank_name + "'," +
                "'" + lscustref_no + "'," +
                "'" + values.assigned_to + "'," +
                "'" + values.assigned_toname + "'," +
                 "'" + employee_gid + "'," +
               "'" + employeename + "'," +
               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
              "'" + values.assigned_remarks.Replace("'", "\\'") + "'," +
               "'" + user_gid + "'," +
              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnresult == 0)
                {
                    values.status = false;
                    values.message = "Error occurred while assigning..!";
                    return;
                }
                else
                {
                    msGetGid1 = objcmnfunctions.GetMasterGID("REAE");

                    msSQL = " insert into brs_trn_treassignemployeelog (" +
                          " reassignemployeelog_gid , " +
                          " banktransc_gid," +
                          " taggedmember_gid," +
                          " taggedmember_name," +
                          " tagged_remarks," +
                           " tagged_date," +
                          " created_by," +
                          " created_date) " +
                          " values (" +
                          " '" + msGetGid1 + "'," +
                            "'" + values.banktransc_gid + "'," +
                           "'" + values.assigned_to + "'," +
                           "'" + values.assigned_toname + "'," +
                           "'" + values.assigned_remarks.Replace("'", "\\'") + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                          " '" + employee_gid + "'," +
                          " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);



                    values.status = true;
                    values.message = "Assigned Successfully..!";

                }
            }
        }
        public void DaPost2Transfer(MdlUnreconciliationManagement values, string employee_gid, string user_gid)
        {
            try
            {
                msSQL = "SELECT a.banktransc_gid, " +
             " c.tagemployee_gid,c.taggedmember_gid, c.taggedmember_name, c.tagged_remarks, c.tagged_date,a.tagged_status " +
           " FROM brs_trn_tbanktransactiondetails a left join brs_trn_ttagemployee c on a.banktransc_gid = c.banktransc_gid " +
           " where a.banktransc_gid='" + values.banktransc_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    lstagemployee_gid = objODBCDatareader["tagemployee_gid"].ToString();
                    lstaggedmember_gid = objODBCDatareader["taggedmember_gid"].ToString();
                    lstaggedmember_name = objODBCDatareader["taggedmember_name"].ToString();
                }

                msGetGid = objcmnfunctions.GetMasterGID("BRTT");
                msSQL = " insert into brs_trn_ttransferemployee(" +
                        " transferemployee_gid," +
                        " banktransc_gid," +
                        " transfermember_gid," +
                        " transfermember_name," +
                        " transfer_remarks," +
                        " transfer_date," +
                        " tagemployee_gid," +
                        " created_by," +
                        " created_date )" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.banktransc_gid + "'," +
                        "'" + values.transfer_to + "'," +
                        "'" + values.transfer_toname + "'," +
                        "'" + values.transfer_reason.Replace("'", "\\'") + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                         "'" + lstagemployee_gid + "'," +
                        "'" + user_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnresult == 0)
                {
                    values.status = false;
                    values.message = "Error occurred while transferring..!";
                    return;
                }
                else
                {

                    msSQL = "update brs_trn_ttagemployee set transfer_status='Transferred', " +
                   " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',tagged_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', updated_by='" + user_gid + "' " +
                       " where banktransc_gid='" + values.banktransc_gid + "' ";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnresult == 0)
                    {
                        values.status = false;
                        values.message = "Error occurred while transferring..!";
                        return;
                    }

                    else
                    {

                        msSQL = "update brs_trn_tbanktransactiondetails set taggedmember_gid='" + values.transfer_to + "'," +
                     " taggedmember_name= '" + values.transfer_toname + "' " +
                     " where banktransc_gid='" + values.banktransc_gid + "'";
                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        unreconallocation_gid = objdbconn.GetExecuteScalar("select unreconallocation_gid from brs_trn_tunreconallocation where ticketref_no='" + values.banktransc_gid + "'");


                        msSQL = " update brs_trn_tunreconallocation set created_by = '" + user_gid + "', " +
                                " created_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                " assigned_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                 " assignedto_gid ='" + values.transfer_to + "', " +
                                  " assigned_toname='" + values.transfer_toname + "', " +
                                  
                                  //" assigned_rm='" + assigned_rm + "', " +
                                  // " assignedrm_date='" + assignedrm_date + "', " +
                                  //  " assignedrm_name='" + assignedrm_name + "' ," +
                                  " allocated_status = 'Pending' " +
                                  " where unreconallocation_gid='" + unreconallocation_gid + "'";
                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnresult == 0)
                        {
                            values.status = false;
                            values.message = "Error occurred while transferring..!";
                            return;
                        }
                        else
                        {
                            values.status = true;
                            values.message = "Transferred Successfully..!";

                        }
                    }
                }
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaGetAllocatedDetail(string banktransc_gid, transactionlist values)
        {

            try
            {

                msSQL = "SELECT a.banktransc_gid,a.banktransc_refno,c.allocated_status,c.rm_status,c.rm_remarks, a.bankconfig_gid,a.bank_gid,b.bank_name,b.branch_name,b.acc_no,b.custref_no,a.reconcildoc_gid,DATE_FORMAT(a.trn_date, '%d-%b-%Y %h:%i %p') as trn_date, DATE_FORMAT(a.value_date, '%d-%b-%Y %h:%i %p') as value_date ," +
                    " DATE_FORMAT(a.payment_date, '%d-%b-%Y %h:%i %p') as payment_date , a.transact_particulars, a.debit_amt,a.brstransactiondetails_flag,a.brstransactiondetailsadvice_flag, a.credit_amt,FORMAT(a.transact_val,2,'en_IN') as transact_val,FORMAT(a.remaining_amount,2,'en_IN') as remaining_amount, a.remarks, a.cr_dr, a.chq_no, a.created_by,  DATE_FORMAT(a.created_date, '%d-%b-%Y %h:%i %p') as created_date , " +
                     " a.transc_balance,baselocation_name, a.manualknockoff_remarks ," +
                     " a.assigned_rm,DATE_FORMAT(a.rmsendback_on, '%d-%b-%Y %h:%i %p')  as rmsendback_on,a.sendback_reason ,a.taggedmember_gid as assignedrm_gid " +
                     " FROM brs_trn_tbanktransactiondetails a " +
                     " left join brs_mst_tbank b  on a.bank_gid = b.bank_gid " +
                     " left join osd_trn_tbankalert2allocated c on a.banktransc_gid = c.ticketref_no " +
                       " left join adm_mst_tuser d on a.created_by = d.user_gid " +
                     " left join hrm_mst_temployee e on d.user_gid = e.user_gid " +
                    "left join sys_mst_tbaselocation f on f.baselocation_gid=e.baselocation_gid " +
                        " where  a.banktransc_gid='" + banktransc_gid + "'  order by a.created_date desc ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    banktransc_gid = objODBCDatareader["banktransc_gid"].ToString();
                    values.banktransc_refno = objODBCDatareader["banktransc_refno"].ToString();
                    values.bank_name = objODBCDatareader["bank_name"].ToString();
                    values.custref_no = objODBCDatareader["custref_no"].ToString();
                    values.branch_name = objODBCDatareader["branch_name"].ToString();
                    values.cr_dr = objODBCDatareader["cr_dr"].ToString();
                    values.transact_val = objODBCDatareader["transact_val"].ToString();
                    values.remarks = objODBCDatareader["remarks"].ToString();
                    values.transc_balance = objODBCDatareader["transc_balance"].ToString();
                    values.acc_no = objODBCDatareader["acc_no"].ToString();
                    values.trn_date = objODBCDatareader["trn_date"].ToString();
                    values.bankconfig_gid = objODBCDatareader["bankconfig_gid"].ToString();
                    values.bank_gid = objODBCDatareader["bank_gid"].ToString();
                    values.reconcildoc_gid = objODBCDatareader["reconcildoc_gid"].ToString();
                    values.trn_date = objODBCDatareader["trn_date"].ToString();
                    values.value_date = objODBCDatareader["value_date"].ToString();
                    values.payment_date = objODBCDatareader["payment_date"].ToString();
                    values.transact_particulars = objODBCDatareader["transact_particulars"].ToString();
                    values.debit_amt = objODBCDatareader["debit_amt"].ToString();
                    values.credit_amt = objODBCDatareader["credit_amt"].ToString();
                    values.chq_no = objODBCDatareader["chq_no"].ToString();
                    values.created_by = objODBCDatareader["created_by"].ToString();
                    values.allocated_status = objODBCDatareader["allocated_status"].ToString();
                    values.rm_status = objODBCDatareader["rm_status"].ToString();
                    values.assigned_rm = objODBCDatareader["assigned_rm"].ToString();
                    values.rmsendback_on = objODBCDatareader["rmsendback_on"].ToString();
                    values.sendback_reason = objODBCDatareader["sendback_reason"].ToString();
                    values.assignedrm_gid = objODBCDatareader["assignedrm_gid"].ToString();
                    values.manualknockoff_remarks = objODBCDatareader["manualknockoff_remarks"].ToString();
                    values.remaining_amount = objODBCDatareader["remaining_amount"].ToString();
                    values.brstransactiondetails_flag = objODBCDatareader["brstransactiondetails_flag"].ToString();
                    values.brstransactiondetailsadvice_flag = objODBCDatareader["brstransactiondetailsadvice_flag"].ToString();

                }
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
            }


        }
        public void DaGetUnReconciliationAssignedSummary(transactionlist values, string employee_gid)
        {
            msSQL = "SELECT a.banktransc_gid,a.bankconfig_gid,a.bank_gid,b.bank_name,b.branch_name,b.acc_no,b.custref_no,a.reconcildoc_gid, " +
                " DATE_FORMAT(a.trn_date, '%d-%b-%Y %h:%i %p') as trn_date, DATE_FORMAT(a.value_date, '%d-%b-%Y %h:%i %p') as value_date, " +
                " DATE_FORMAT(a.payment_date, '%d-%b-%Y %h:%i %p') as payment_date , a.transact_particulars, a.debit_amt, a.credit_amt, " +
                " Format(a.transact_val,2,'en_IN') as transact_val,Format(a.remaining_amount,2,'en_IN') as remaining_amount, a.remarks, a.cr_dr, a.chq_no ,concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as created_by, " +
                "DATE_FORMAT(a.created_date, '%d-%b-%Y %h:%i %p') as created_date,a.transc_balance ,p.brs_status, " +
                " CONCAT(FLOOR(timestampdiff(day, c.created_date, now())), ' days ') as aging, " +
                " CONCAT(FLOOR(timestampdiff(day, a.trn_date, now())), ' days ') as transactionaging, " +
                " c.tagemployee_gid,c.taggedmember_gid, c.taggedmember_name, c.tagged_remarks, c.tagged_date,a.tagged_status,a.knockoff_status, " +
                " case when DATE_FORMAT(f.transfer_date, '%d-%b-%Y %h:%i %p') is null then DATE_FORMAT(c.tagged_date, '%d-%b-%Y %h:%i %p') else (select DATE_FORMAT(max(te.transfer_date), '%d-%b-%Y %h:%i %p') from brs_trn_ttransferemployee te " +
                " where te.banktransc_gid = a.banktransc_gid) end as 'assigned_date', " +
                " case when f.created_by is null then(select  concat(g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) from brs_trn_ttagemployee te " +
                "left join adm_mst_tuser g on te.created_by = g.user_gid " +
                " where te.banktransc_gid = a.banktransc_gid group by te.banktransc_gid) else " +
               " (select  concat(g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) from brs_trn_ttransferemployee te " +
                " left join adm_mst_tuser g on te.created_by = g.user_gid " +
                " where te.banktransc_gid = a.banktransc_gid and te.transfer_date = (select max(transfer_date) " +
                " from brs_trn_ttransferemployee f   where f.banktransc_gid = a.banktransc_gid))end as  'assigned_by'," +
                " case when f.transfermember_name is null then c.taggedmember_name else " +
                "  (select transfermember_name from brs_trn_ttransferemployee te " +
                "   where te.banktransc_gid = a.banktransc_gid and te.transfer_date = (select max(transfer_date) from brs_trn_ttransferemployee f " +
                "  where f.banktransc_gid = a.banktransc_gid) )end as  'assigned_to' " +                
                " FROM brs_trn_tbanktransactiondetails a " +
                " left join brs_mst_tbank b  on a.bank_gid = b.bank_gid " +
                " left join adm_mst_tuser e on a.created_by= e.user_gid " + 
                " left join brs_trn_ttagemployee c on a.banktransc_gid = c.banktransc_gid " +
                " left join adm_mst_tuser g on c.created_by= g.user_gid " +
                " left join brs_trn_ttransferemployee f on a.banktransc_gid = f.banktransc_gid " +
                " left join brs_trn_tbrsunreconciliation2allocated p on p.banktransc_gid = a.banktransc_gid " +
                " where ((a.cr_dr ='CR' and  a.tagged_status='Assigned') and (a.cr_dr ='CR' and  a.knockoff_status='Pending') )  group by a.banktransc_gid  " +
                "  order by a.created_date asc";
            //" where tagged_status='Assigned' and c.taggedmember_gid ='" + employee_gid + "' order by a.created_date desc "; // case when f.transfermember_name is not null then f.transfermember_name else a.transfermember_name end as assigned_by
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.BankAlertUnreconciliationcredit_list = dt_datatable.AsEnumerable().Select(row => new BankAlertUnreconciliation_list
                {
                    tagemployee_gid = row["tagemployee_gid"].ToString(),
                    banktransc_gid = row["banktransc_gid"].ToString(),
                    taggedmember_gid = row["taggedmember_gid"].ToString(),
                    transact_particulars = row["transact_particulars"].ToString(),
                    taggedmember_name = row["taggedmember_name"].ToString(),
                    tagged_remarks = row["tagged_remarks"].ToString(),
                    tagged_date = row["tagged_date"].ToString(),
                    created_by = row["created_by"].ToString(),
                    created_date = row["created_date"].ToString(),
                    trn_date = row["trn_date"].ToString(),
                    custref_no = row["custref_no"].ToString(),
                    bank_name = row["bank_name"].ToString(),
                    branch_name = row["branch_name"].ToString(),
                    transact_val = row["transact_val"].ToString(),
                    acc_no = row["acc_no"].ToString(),
                    cr_dr = row["cr_dr"].ToString(),
                    transc_balance = row["transc_balance"].ToString(),
                    tagged_status = row["tagged_status"].ToString(),
                    brs_status = row["brs_status"].ToString(),
                    knockoff_status = row["knockoff_status"].ToString(),
                    assigned_date = row["assigned_date"].ToString(),
                    assigned_by = row["assigned_by"].ToString(),
                    assigned_to = row["assigned_to"].ToString(),
                    aging = row["aging"].ToString(),
                    remaining_amount = row["remaining_amount"].ToString(),
                    transaction_aging = row["transactionaging"].ToString(),

                }).ToList();
            }
            msSQL = "SELECT a.banktransc_gid,a.bankconfig_gid,a.bank_gid,b.bank_name,b.branch_name,b.acc_no,b.custref_no,a.reconcildoc_gid, " +
            " DATE_FORMAT(a.trn_date, '%d-%b-%Y %h:%i %p') as trn_date, DATE_FORMAT(a.value_date, '%d-%b-%Y %h:%i %p') as value_date, " +
            " DATE_FORMAT(a.payment_date, '%d-%b-%Y %h:%i %p') as payment_date , a.transact_particulars, a.debit_amt, a.credit_amt, " +
            " FORMAT(a.transact_val,2,'en_IN') as transact_val,Format(a.remaining_amount,2,'en_IN') as remaining_amount, a.remarks, a.cr_dr, a.chq_no,concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as created_by, " +
            "DATE_FORMAT(a.created_date, '%d-%b-%Y %h:%i %p') as created_date,a.transc_balance  ,p.brs_status,  " +
           " CONCAT(FLOOR(timestampdiff(day, c.created_date, now())), ' days ',MOD(timestampdiff(hour, c.created_date, now()), '24'),' Hrs ',MOD(timestampdiff(minute, c.created_date, now()), '60'), 'Mins') as aging, " +
            " CONCAT(FLOOR(timestampdiff(day, a.trn_date, now())), ' days ') as transactionaging, " +
             " c.tagemployee_gid,c.taggedmember_gid, c.taggedmember_name, c.tagged_remarks, c.tagged_date,a.tagged_status,a.knockoff_status, " +
            " case when DATE_FORMAT(f.transfer_date, '%d-%b-%Y %h:%i %p') is null then DATE_FORMAT(c.tagged_date, '%d-%b-%Y %h:%i %p') else (select DATE_FORMAT(max(te.transfer_date), '%d-%b-%Y %h:%i %p') from brs_trn_ttransferemployee te " +
                " where te.banktransc_gid = a.banktransc_gid) end as 'assigned_date', " +
              " case when f.created_by is null then(select  concat(g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) from brs_trn_ttagemployee te " +
                "left join adm_mst_tuser g on te.created_by = g.user_gid " +
            "where te.banktransc_gid = a.banktransc_gid group by te.banktransc_gid) else " +
               " (select  concat(g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) from brs_trn_ttransferemployee te " +
                " left join adm_mst_tuser g on te.created_by = g.user_gid " +  
                " where te.banktransc_gid = a.banktransc_gid and te.transfer_date = (select max(transfer_date) " +
                " from brs_trn_ttransferemployee f   where f.banktransc_gid = a.banktransc_gid))end as  'assigned_by'," +
                " case when f.transfermember_name is null then c.taggedmember_name else " +
                "  (select transfermember_name from brs_trn_ttransferemployee te " +
                "   where te.banktransc_gid = a.banktransc_gid and te.transfer_date = (select max(transfer_date) from brs_trn_ttransferemployee f " +
                "  where f.banktransc_gid = a.banktransc_gid) )end as  'assigned_to' " + 
                " FROM brs_trn_tbanktransactiondetails a " +
                    "left join brs_mst_tbank b  on a.bank_gid = b.bank_gid " +
                    " left join adm_mst_tuser e on a.created_by= e.user_gid " +
                     " left join brs_trn_tbrsunreconciliation2allocated p on p.banktransc_gid = a.banktransc_gid " +
                    " left join brs_trn_ttagemployee c on a.banktransc_gid = c.banktransc_gid " +
                    " left join adm_mst_tuser g on c.created_by= g.user_gid " +
                    " left join brs_trn_ttransferemployee f on a.banktransc_gid = f.banktransc_gid " +
                     "where ((a.cr_dr ='DR' and  a.tagged_status='Assigned') and (a.cr_dr ='DR' and  a.knockoff_status='Pending') )  group by a.banktransc_gid  " +
            "  order by a.created_date asc ";
            // " MAX(f.transfer_date) as assigned_date, case when f.transfermember_name is not null then f.transfermember_name else a.transfermember_name end as assigned_by, c.taggedmember_name as assigned_to " +
                        //" where tagged_status='Assigned' and c.taggedmember_gid ='" + employee_gid + "' order by a.created_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.BankAlertUnreconciliationdebit_list = dt_datatable.AsEnumerable().Select(row => new BankAlertUnreconciliation_list
                {
                    tagemployee_gid = row["tagemployee_gid"].ToString(),
                    banktransc_gid = row["banktransc_gid"].ToString(),
                    taggedmember_gid = row["taggedmember_gid"].ToString(),
                    transact_particulars = row["transact_particulars"].ToString(),
                    taggedmember_name = row["taggedmember_name"].ToString(),
                    tagged_remarks = row["tagged_remarks"].ToString(),
                    tagged_date = row["tagged_date"].ToString(),
                    created_by = row["created_by"].ToString(),
                    created_date = row["created_date"].ToString(),
                    trn_date = row["trn_date"].ToString(),
                    custref_no = row["custref_no"].ToString(),
                    bank_name = row["bank_name"].ToString(),
                    branch_name = row["branch_name"].ToString(),
                    transact_val = row["transact_val"].ToString(),
                    acc_no = row["acc_no"].ToString(),
                    cr_dr = row["cr_dr"].ToString(),
                    transc_balance = row["transc_balance"].ToString(),
                    tagged_status = row["tagged_status"].ToString(),
                    brs_status = row["brs_status"].ToString(),
                    knockoff_status = row["knockoff_status"].ToString(),
                    assigned_date = row["assigned_date"].ToString(),
                    assigned_by = row["assigned_by"].ToString(),
                    assigned_to = row["assigned_to"].ToString(),
                    aging = row["aging"].ToString(),
                    remaining_amount = row["remaining_amount"].ToString(),
                    transaction_aging = row["transactionaging"].ToString(),

                }).ToList();
            }
            dt_datatable.Dispose();
        }
        public void DaGetUnReconciliationClosedSummary(transactionlist values, string employee_gid)
        {
            msSQL = "SELECT a.banktransc_gid,a.bankconfig_gid,a.bank_gid,b.bank_name,b.branch_name,b.acc_no,b.custref_no,a.reconcildoc_gid, " +
             " DATE_FORMAT(a.trn_date, '%d-%b-%Y %h:%i %p') as trn_date, DATE_FORMAT(a.value_date, '%d-%b-%Y %h:%i %p') as value_date, " +
             " DATE_FORMAT(a.payment_date, '%d-%b-%Y %h:%i %p') as payment_date , a.transact_particulars, a.debit_amt, a.credit_amt, " +
             " Format(a.transact_val,2,'en_IN') as transact_val,Format(a.remaining_amount,2,'en_IN') as remaining_amount, a.remarks, a.cr_dr, a.chq_no ,concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as created_by, " +
             " DATE_FORMAT(a.created_date, '%d-%b-%Y %h:%i %p') as created_date,a.transc_balance , DATE_FORMAT(a.knockoff_date, '%d-%b-%Y %h:%i %p') as Closed_on_Date, " +
             " case when ((a.knockoff_status='AssignMatched') or (a.knockoff_status='Closed') or (a.knockoff_status='Matched')  and a.closed_by is not null ) then (concat(f.user_firstname,' ',f.user_lastname,' / ',f.user_code)) " +
             " when (a.knockoff_status = 'ManuallyMatched' and  a.closed_by is not null) then (concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code)) end as Closed_By, " +
             " c.tagemployee_gid,c.taggedmember_gid, c.taggedmember_name, c.tagged_remarks, c.tagged_date,a.tagged_status ,a.knockoff_status,a.manualknockoff_remarks ," +
             " CONCAT(FLOOR(timestampdiff(day, (ifnull(c.created_date,a.created_date)),a.knockoff_date)), ' days ') as aging, " +
             " CONCAT(FLOOR(timestampdiff(day, a.trn_date, a.knockoff_date)), ' days ') as transactionaging " +
            " FROM brs_trn_tbanktransactiondetails a " +
                     "left join brs_mst_tbank b  on a.bank_gid = b.bank_gid " +
                     " left join adm_mst_tuser e on a.created_by= e.user_gid " +
                     " left join adm_mst_tuser f on a.closed_by = f.user_gid " +
                     " left join brs_trn_ttagemployee c on a.banktransc_gid = c.banktransc_gid " +
                      " where  ((a.cr_dr ='CR' and a.knockoff_status='AssignMatched') or (a.cr_dr ='CR' and a.knockoff_status='ManuallyMatched') or (a.cr_dr ='CR' and a.knockoff_status='Matched') or (a.cr_dr ='CR' and a.knockoff_status='Closed')) " +
              " group by a.banktransc_gid order by a.created_date asc  ";
            //" where tagged_status='Assigned' and c.taggedmember_gid ='" + employee_gid + "' order by a.created_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.BankAlertUnreconciliationcredit_list = dt_datatable.AsEnumerable().Select(row => new BankAlertUnreconciliation_list
                {
                    tagemployee_gid = row["tagemployee_gid"].ToString(),
                    banktransc_gid = row["banktransc_gid"].ToString(),
                    taggedmember_gid = row["taggedmember_gid"].ToString(),
                    transact_particulars = row["transact_particulars"].ToString(),
                    taggedmember_name = row["taggedmember_name"].ToString(),
                    tagged_remarks = row["tagged_remarks"].ToString(),
                    tagged_date = row["tagged_date"].ToString(),
                    created_by = row["created_by"].ToString(),
                    created_date = row["created_date"].ToString(),
                    trn_date = row["trn_date"].ToString(),
                    custref_no = row["custref_no"].ToString(),
                    bank_name = row["bank_name"].ToString(),
                    branch_name = row["branch_name"].ToString(),
                    transact_val = row["transact_val"].ToString(),
                    acc_no = row["acc_no"].ToString(),
                    cr_dr = row["cr_dr"].ToString(),
                    transc_balance = row["transc_balance"].ToString(),
                    tagged_status = row["tagged_status"].ToString(),
                    knockoff_status = row["knockoff_status"].ToString(),
                    manualknockoff_remarks = row["manualknockoff_remarks"].ToString(),
                    closed_date = row["Closed_on_Date"].ToString(),
                    closed_by = row["Closed_By"].ToString(),
                    remaining_amount = row["remaining_amount"].ToString(),
                    aging = row["aging"].ToString(),
                    transaction_aging = row["transactionaging"].ToString(),

                }).ToList();
            }
            msSQL = "SELECT a.banktransc_gid,a.bankconfig_gid,a.bank_gid,b.bank_name,b.branch_name,b.acc_no,b.custref_no,a.reconcildoc_gid, " +
            " DATE_FORMAT(a.trn_date, '%d-%b-%Y %h:%i %p') as trn_date, DATE_FORMAT(a.value_date, '%d-%b-%Y %h:%i %p') as value_date, " +
            " DATE_FORMAT(a.payment_date, '%d-%b-%Y %h:%i %p') as payment_date , a.transact_particulars, a.debit_amt, a.credit_amt, " +
            " FORMAT(a.transact_val,2,'en_IN') as transact_val,Format(a.remaining_amount,2,'en_IN') as remaining_amount, a.remarks, a.cr_dr, a.chq_no,concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as created_by, " +
            " DATE_FORMAT(a.created_date, '%d-%b-%Y %h:%i %p') as created_date,a.transc_balance , DATE_FORMAT(a.knockoff_date, '%d-%b-%Y %h:%i %p') as Closed_on_Date," +
            //" case when f.updated_by is not null then (concat(f.user_firstname,' ',f.user_lastname,' / ',f.user_code)) when a.updated_by is not null then (concat(f.user_firstname,' ',f.user_lastname,' / ',f.user_code)) else (concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code)) end as Closed_By," +
            //" case when a.knockoff_date is not null then () else (concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code)) end as Closed_By," +
            " concat(f.user_firstname,' ',f.user_lastname,' / ',f.user_code) as Closed_By," +
            " c.tagemployee_gid,c.taggedmember_gid, c.taggedmember_name, c.tagged_remarks, c.tagged_date,a.tagged_status,a.knockoff_status,a.manualknockoff_remarks ," +
            " CONCAT(FLOOR(timestampdiff(day, (ifnull(c.created_date,a.created_date)),a.knockoff_date)), ' days ') as aging, " +
             " CONCAT(FLOOR(timestampdiff(day, a.trn_date, a.knockoff_date)), ' days ') as transactionaging " +
            " FROM brs_trn_tbanktransactiondetails a " +
                    "left join brs_mst_tbank b  on a.bank_gid = b.bank_gid " +
                    " left join adm_mst_tuser e on a.created_by= e.user_gid " +
                    " left join adm_mst_tuser f on a.closed_by = f.user_gid " +
                    " left join brs_trn_ttagemployee c on a.banktransc_gid = c.banktransc_gid " +
                     " where ((a.cr_dr ='DR' and  a.knockoff_status='AssignMatched') or (a.cr_dr ='DR' and  a.knockoff_status='ManuallyMatched') or (a.cr_dr ='DR' and a.knockoff_status='Matched') or (a.cr_dr ='DR' and a.knockoff_status='Closed') )  " +
                    " group by a.banktransc_gid  order by a.created_date asc";
            //" where tagged_status='Assigned' and c.taggedmember_gid ='" + employee_gid + "' order by a.created_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.BankAlertUnreconciliationdebit_list = dt_datatable.AsEnumerable().Select(row => new BankAlertUnreconciliation_list
                {
                    tagemployee_gid = row["tagemployee_gid"].ToString(),
                    banktransc_gid = row["banktransc_gid"].ToString(),
                    taggedmember_gid = row["taggedmember_gid"].ToString(),
                    transact_particulars = row["transact_particulars"].ToString(),
                    taggedmember_name = row["taggedmember_name"].ToString(),
                    tagged_remarks = row["tagged_remarks"].ToString(),
                    tagged_date = row["tagged_date"].ToString(),
                    created_by = row["created_by"].ToString(),
                    created_date = row["created_date"].ToString(),
                    trn_date = row["trn_date"].ToString(),
                    custref_no = row["custref_no"].ToString(),
                    bank_name = row["bank_name"].ToString(),
                    branch_name = row["branch_name"].ToString(),
                    transact_val = row["transact_val"].ToString(),
                    acc_no = row["acc_no"].ToString(),
                    cr_dr = row["cr_dr"].ToString(),
                    transc_balance = row["transc_balance"].ToString(),
                    tagged_status = row["tagged_status"].ToString(),
                    knockoff_status = row["knockoff_status"].ToString(),
                    manualknockoff_remarks = row["manualknockoff_remarks"].ToString(),
                    closed_date = row["Closed_on_Date"].ToString(),
                    closed_by = row["Closed_By"].ToString(),
                    remaining_amount = row["remaining_amount"].ToString(),
                    aging = row["aging"].ToString(),
                    transaction_aging = row["transactionaging"].ToString(),

                }).ToList();
            }
            dt_datatable.Dispose();
        }
        public void DaGetAssignedHistory(MdlUnreconciliationManagement values, string employee_gid, string banktransc_gid)
        {
            try
            {

                msSQL = " select tagemployee_gid,banktransc_gid,taggedmember_gid,taggedmember_name,tagged_remarks, DATE_FORMAT(a.tagged_date, '%d-%b-%Y %h:%i %p') as tagged_date, " +
                        " DATE_FORMAT(a.created_date, '%d-%b-%Y %h:%i %p') as created_date,concat(g.user_firstname,' ',g.user_lastname,' / ',g.user_code) as assigned_by  from brs_trn_ttagemployee a " +
                        " left join adm_mst_tuser g on a.created_by= g.user_gid " +
                        " where a.banktransc_gid='" + banktransc_gid + "' order by a.created_date desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gettransactionlist = new List<assignedlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        gettransactionlist.Add(new assignedlist
                        {
                            banktransc_gid = (dr_datarow["banktransc_gid"].ToString()),
                            taggedmember_name = (dr_datarow["taggedmember_name"].ToString()),
                            tagged_remarks = (dr_datarow["tagged_remarks"].ToString()),
                            tagged_date = (dr_datarow["tagged_date"].ToString()),
                            taggedmember_gid = (dr_datarow["taggedmember_gid"].ToString()),
                            assigned_by = (dr_datarow["assigned_by"].ToString()),

                        });
                    }
                    values.assignedlist = gettransactionlist;
                }
                dt_datatable.Dispose();

                values.status = true;

            }
            catch
            {
                values.status = false;
            }

        }
        public void DaGetSendBackHistory(MdlUnreconciliationManagement values, string employee_gid,string banktransc_gid)
        {
            try
            {
                msSQL = " SELECT a.banktransc_gid,a.reason,date_format(a.created_date,'%d-%b-%Y %h:%i %p') as created_date," +
                "concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by " +
                "FROM brs_trn_tunreconnlog a " +
                "left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                "left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                "where a.banktransc_gid ='" + banktransc_gid + "' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getsendbacklist = new List<sendbacklist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getsendbacklist.Add(new sendbacklist
                        {
                            banktransc_gid = (dr_datarow["banktransc_gid"].ToString()),
                            sendback_by = (dr_datarow["created_by"].ToString()),
                            sendback_date = (dr_datarow["created_date"].ToString()),
                            sendback_reason = (dr_datarow["reason"].ToString()),

                        });
                    }
                    values.sendbacklist = getsendbacklist;
                }
                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaGetunreConcillationCount(cocillationlist values, string user_gid)
        {
            msSQL = "SELECT  distinct COUNT(*) AS creditsum_count " +
                    " FROM brs_trn_tbanktransactiondetails " +
                    " WHERE " +
                    " (" +
                    " (cr_dr = 'CR' AND knockoff_status = 'Pending' AND tagged_status = 'Pending') " +
                    " OR "+
                    " (cr_dr = 'CR' AND knockoff_status = 'Pending' AND tagged_status = 'Reassign')  " +
                    " OR " +
                    " (cr_dr = 'CR' AND tagged_status = 'Assigned' AND knockoff_status = 'Pending') " +
                    " OR " +
                    " (cr_dr = 'CR' AND(knockoff_status = 'AssignMatched' OR knockoff_status = 'FinancePending' or knockoff_status = 'Closed')) " +
                    " ) ";
            values.creditsum_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "SELECT  distinct COUNT(*) AS debitsum_count " +
                   " FROM brs_trn_tbanktransactiondetails " +
                   " WHERE " +
                   " (" +
                   " (cr_dr = 'DR' AND knockoff_status = 'Pending' AND tagged_status = 'Pending') " +
                   " OR " +
                   " (cr_dr = 'DR' AND knockoff_status = 'Pending' AND tagged_status = 'Reassign')  " +
                   " OR " +
                   " (cr_dr = 'DR' AND tagged_status = 'Assigned' AND knockoff_status = 'Pending') " +
                   " OR " +
                   " (cr_dr = 'DR' AND(knockoff_status = 'AssignMatched' OR knockoff_status = 'FinancePending' or knockoff_status = 'Closed')) " +
                   " ) ";
            values.debitsum_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select  distinct count(*) as unreconpendingcredit_count  FROM brs_trn_tbanktransactiondetails   " +
                       " where ((cr_dr ='CR' and  knockoff_status='Pending') and (cr_dr ='CR' and  tagged_status='Pending')) ";
            values.unreconpendingcredit_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select distinct count(*) as unreconpendingdebit_count  FROM brs_trn_tbanktransactiondetails  " +
                       "  where ((cr_dr ='DR' and  knockoff_status='Pending') and (cr_dr ='DR' and  tagged_status='Pending')) ";
            values.unreconpendingdebit_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select  distinct count(*) as unreconreassignpendingcredit_count  FROM brs_trn_tbanktransactiondetails   " +
                      " where ((cr_dr ='CR' and  knockoff_status='Pending') and (cr_dr ='CR' and  tagged_status='Reassign')) ";
            values.unreconreassignpendingcredit_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select distinct count(*) as unreconreassignpendingdebit_count  FROM brs_trn_tbanktransactiondetails  " +
                       "  where ((cr_dr ='DR' and  knockoff_status='Pending') and (cr_dr ='DR' and  tagged_status='Reassign')) ";
            values.unreconreassignpendingdebit_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select  distinct count(*) as unreconassigncredit_count  FROM brs_trn_tbanktransactiondetails   " +
                        " where ((cr_dr ='CR' and  tagged_status='Assigned') and (cr_dr ='CR' and  knockoff_status='Pending') ) ";
            values.unreconassigncredit_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select  distinct count(*) as unreconassigndebit_count  FROM brs_trn_tbanktransactiondetails  " +
                       "  where ((cr_dr ='DR' and  tagged_status='Assigned') and (cr_dr ='DR' and  knockoff_status='Pending') ) ";
            values.unreconassigndebit_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(*) as unreconclosecredit_count  FROM brs_trn_tbanktransactiondetails a  " +
                        " where  ((cr_dr ='CR' and knockoff_status='AssignMatched') or (cr_dr ='CR' and knockoff_status='ManuallyMatched') or (cr_dr ='CR' and knockoff_status='Matched') or (cr_dr ='CR' and a.knockoff_status='Closed') )  ";
            values.unreconclosecredit_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select distinct count(*) as unreconclosedebit_count  FROM brs_trn_tbanktransactiondetails a " +
                       "  where ((cr_dr ='DR' and  knockoff_status='AssignMatched') or (cr_dr ='DR' and  knockoff_status='ManuallyMatched') or (cr_dr ='DR' and knockoff_status='Matched') or (cr_dr ='DR' and a.knockoff_status='Closed') ) ";
            values.unreconclosedebit_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select distinct count(*) as unreconfin_count  FROM brs_trn_tbanktransactiondetails a " +
                      " where ((a.cr_dr ='CR' and  a.knockoff_status='FinancePending') and " +
                        " (a.cr_dr ='CR' and  a.tagged_status='Pending')) ";
            values.unreconfin_count = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select distinct count(*) as unreconfin_count  FROM brs_trn_tbanktransactiondetails a " +
                     " where ((a.cr_dr ='DR' and  a.knockoff_status='FinancePending') and " +
                       " (a.cr_dr ='DR' and  a.tagged_status='Pending')) ";
            values.unrecondebitfin_count = objdbconn.GetExecuteScalar(msSQL);

        }
        public void DaPostManualMatch(cocillationlist values, string user_gid)
        {

            try
            {
                msSQL = " update brs_trn_tbanktransactiondetails set knockoff_status='ManuallyMatched',knockoff_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', knockoff_flag='Y', " +
                        " updated_by='" + user_gid  + "', updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        " manualknockoff_remarks='" + values.manualknockoff_remarks + "' where banktransc_gid = '" + values.banktransc_gid + "'";
                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);


                if (mnresult == 1)
                {
                    values.status = true;
                    values.message = " The Ticket is matched Successfully..!";

                }
                else
                {
                    values.status = false;
                    values.message = " The Ticket is mismatched ..!";

                }
            }

            catch (Exception ex)
            {

            }

        }
        public void DaGetTransferredHistory(MdlUnreconciliationManagement values, string employee_gid, string banktransc_gid)
        {
            try
            {


                msSQL = " select tagemployee_gid,banktransc_gid,transfermember_gid,transfermember_name,transfer_remarks, DATE_FORMAT(a.transfer_date, '%d-%b-%Y %h:%i %p') as transfer_date, " +
                     " DATE_FORMAT(a.created_date, '%d-%b-%Y %h:%i %p') as created_date, concat(g.user_firstname,' ',g.user_lastname,' / ',g.user_code) as transfer_by from brs_trn_ttransferemployee a " +
                     " left join adm_mst_tuser g on a.created_by= g.user_gid " +
                         " where a.banktransc_gid='" + banktransc_gid + "' order by a.created_date desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gettransactionlist = new List<transferlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        gettransactionlist.Add(new transferlist
                        {
                            banktransc_gid = (dr_datarow["banktransc_gid"].ToString()),
                            transfer_toname = (dr_datarow["transfermember_name"].ToString()),
                            transfer_reason = (dr_datarow["transfer_remarks"].ToString()),
                            transfered_date = (dr_datarow["transfer_date"].ToString()),
                            transfer_to = (dr_datarow["transfermember_gid"].ToString()),
                            transfer_by = (dr_datarow["transfer_by"].ToString()),

                        });
                    }
                    values.transferlist = gettransactionlist;
                }
                dt_datatable.Dispose();

                values.status = true;

            }
            catch
            {
                values.status = false;
            }

        }
        //public void DaGetTransferreddisablestatus(cocillationlist values)
        //{
        //    try
        //    {

        //        msSQL = " select a.brs_status,b.banktransc_gid from osd_trn_tbrsunreconciliation2allocated a " +
        //            " left join brs_trn_tbanktransactiondetails b on b.banktransc_gid = a.banktransc_gid ";

        //        objODBCDatareader = objdbconn.GetDataReader(msSQL);
        //        if (objODBCDatareader.HasRows == true)
        //        {
        //            values.brs_status = objODBCDatareader["brs_status"].ToString();
        //            values.banktransc_gid = objODBCDatareader["banktransc_gid"].ToString();

        //        }
        //    }
        //    catch
        //    {

        //    }
        //}
        public void DaGetReassignEmployee(MdlEmployeeExpCurid objemployee, string employee_gid,string tagemployee_gid)
        {
            try
            {
               string tagemployeegid = objdbconn.GetExecuteScalar(" select taggedmember_gid from brs_trn_tbanktransactiondetails where banktransc_gid = '" + tagemployee_gid + "' and tagged_status = 'Reassign'");

               msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                       " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                       " where user_status<>'N' and employee_gid not in ('" + tagemployeegid + "') order by a.user_firstname asc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employee = new List<employee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    objemployee.employee_list = dt_datatable.AsEnumerable().Select(row =>
                      new employee_list
                      {
                          employee_gid = row["employee_gid"].ToString(),
                          employee_name = row["employee_name"].ToString()
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();
                objemployee.status = true;
            }
            catch (Exception ex)
            {
                objemployee.status = false;
            }


        }
        public void DaGetEmployee(MdlEmployeeExpCurid objemployee, string employee_gid)
        {
            try
            {
                msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                       " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                       " where user_status<>'N' and employee_gid not in ('" + employee_gid + "') order by a.user_firstname asc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employee = new List<employee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    objemployee.employee_list = dt_datatable.AsEnumerable().Select(row =>
                      new employee_list
                      {
                          employee_gid = row["employee_gid"].ToString(),
                          employee_name = row["employee_name"].ToString()
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();
                objemployee.status = true;
            }
            catch (Exception ex)
            {
                objemployee.status = false;
            }


        }
        public void DaGetReassignemployeeLog(unrecocillationTaglist values,string employee_gid,string banktransc_gid)
        {
            try
            {
                msSQL = " SELECT a.banktransc_gid,a.tagged_remarks,a.reassignemployeelog_gid,a.taggedmember_gid,a.taggedmember_name,date_format(a.tagged_date,'%d-%b-%Y %h:%i %p') as tagged_date,date_format(a.created_date,'%d-%b-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                        " FROM brs_trn_treassignemployeelog a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.banktransc_gid ='" + banktransc_gid + "' order by a.reassignemployeelog_gid asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getreassignemployee_list = new List<reassignemployee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getreassignemployee_list.Add(new reassignemployee_list
                        {
                            banktransc_gid = (dr_datarow["banktransc_gid"].ToString()),
                            assigned_toname = (dr_datarow["taggedmember_name"].ToString()),
                            tagged_date = (dr_datarow["tagged_date"].ToString()),
                            assigned_remarks = (dr_datarow["tagged_remarks"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),

                        });
                    }
                    values.reassignemployee_list = getreassignemployee_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }
        public void DaPostUnreconTransactionDetails(MdlUnreconciliationManagement values, string employee_gid, string user_gid)
        {

            msSQL = " select sum(remaining_amount)  from brs_trn_tbanktransactiondetails where banktransc_gid='" + values.banktransc_gid + "'";
            lspendingremainingamount = objdbconn.GetExecuteScalar(msSQL);

            var convertDecimalremainingamount = Convert.ToDecimal(lspendingremainingamount);
            var convertDecimalamount = Convert.ToDecimal(values.amount.Replace(",", ""));

            if (convertDecimalamount > convertDecimalremainingamount)
            {
                values.message = "Amount Exceeded the Credit Amount";
                values.status = false;
                return;
            }

            msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                 "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                 "where b.user_gid ='" + user_gid + "'";
            lsemployee_name = objdbconn.GetExecuteScalar(msSQL);

            if (values.samfincustomer_name == null || values.samfincustomer_name == "")
            {
                lssamfincustomer_name = "--";
            }
            else
            {
                lssamfincustomer_name = values.samfincustomer_name.Replace("'", "");
            }


            msGetGid = objcmnfunctions.GetMasterGID("URTD");
            msSQL = " insert into brs_trn_tunrecontransactiondetails(" +
                    " unrecontransactiondetails_gid," +
                    " banktransc_gid," +
                    //" department_gid," +
                    " department_name," +
                    //" assignby_gid," +
                    " assignby_name," +
                     " activity_gid," +
                    " activity_name," +
                     " samfincustomer_gid," +
                    " samfincustomer_name," +
                     " amount," +
                     " action_name," +
                     " transaction_remarks," +
                     " brstransactiondetails_flag," +
                    " created_by," +
                    " created_date )" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.banktransc_gid + "'," +
                    "'" + values.department_name + "'," +
                     //"'" + values.assignby_gid + "'," +
                    "'" + lsemployee_name + "'," +
                      "'" + values.activity_gid + "'," +
                    "'" + values.activity_name.Replace("'", "") + "'," +
                     "'" + values.samfincustomer_gid + "'," +
                    "'" + lssamfincustomer_name + "'," +
                       "'" + values.amount.Replace(",", "") + "'," +
                    "'" + values.action_name + "'," +
                    "'" + values.transaction_remarks.Replace("'", "") + "'," +
                    "'Y'," +
                     "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " select sum(adjust_amount)  from brs_trn_tbanktransactiondetails where banktransc_gid='" + values.banktransc_gid + "'";

            lsadjust_amount = objdbconn.GetExecuteScalar(msSQL);
            if (lsadjust_amount == null || lsadjust_amount == "")
            {
                lsadjust_amount = "0";
            }
            var convertDecimal = Convert.ToDecimal(lsadjust_amount);

            //Decimal val = Decimal.Truncate(convertDecimal);


            msSQL = " select sum(advice_amount)  from brs_trn_tbanktransactiondetails where banktransc_gid='" + values.banktransc_gid + "'";

            lsadvice_amount = objdbconn.GetExecuteScalar(msSQL);

            if (lsadvice_amount == null || lsadvice_amount == "")
            {
                lsadvice_amount = "0";
            }
            var convertDecimal2 = Convert.ToDecimal(lsadvice_amount);

            //Decimal val2 = Decimal.Truncate(convertDecimal2);

            msSQL = " select (transact_val) as transact_val  from brs_trn_tbanktransactiondetails where banktransc_gid='" + values.banktransc_gid + "'";

            lsremaining_amount = objdbconn.GetExecuteScalar(msSQL);
            if (lsremaining_amount == null || lsremaining_amount == "")
            {
                lsremaining_amount = "0";
            }
            var convertDecimalremaining = Convert.ToDecimal(lsremaining_amount);

            //Decimal lsremainingamount = Decimal.Truncate(convertDecimalremaining);
            if (mnresult != 0)
            {
                if(values.action_name == "Booked in LMS / FA")
                {
                    var convertDecimal1 = Convert.ToDecimal(values.amount);

                    //Decimal val1 = Decimal.Truncate(convertDecimal1);

                    adjusted_amount = convertDecimal + convertDecimal1;

                    remaining_amount = convertDecimalremaining - adjusted_amount;

                    msSQL = " update brs_trn_tbanktransactiondetails set brstransactiondetailsadvice_flag = 'Y' where banktransc_gid='" + values.banktransc_gid + "'";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update brs_trn_tbanktransactiondetails set adjust_amount = '" + adjusted_amount + "' where banktransc_gid='" + values.banktransc_gid + "'";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update brs_trn_tbanktransactiondetails set remaining_amount = '" + remaining_amount + "' where banktransc_gid='" + values.banktransc_gid + "'";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update brs_trn_tunrecontransactiondetails set remaining_amount = '" + remaining_amount + "' where unrecontransactiondetails_gid='" + msGetGid + "'";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }

                if (values.action_name == "Adjust against the Repayment/ Refund")
                {
                    var convertDecimal3 = Convert.ToDecimal(values.amount);

                    //Decimal val3 = Decimal.Truncate(convertDecimal3);

                    advice_amount = convertDecimal2 + convertDecimal3;

                    remaining_amount = convertDecimalremaining - convertDecimal;

                    msSQL = " update brs_trn_tbanktransactiondetails set brstransactiondetailsadvice_flag = 'N' where banktransc_gid='" + values.banktransc_gid + "'";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update brs_trn_tbanktransactiondetails set advice_amount = '" + advice_amount + "' where banktransc_gid='" + values.banktransc_gid + "'";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update brs_trn_tunrecontransactiondetails set remaining_amount = '" + remaining_amount + "' where unrecontransactiondetails_gid='" + msGetGid + "'";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                values.status = true;
                values.message = "Transaction Details Added Sucessfully..!";
                return;
            }
            else
            {

                    values.status = false;
                    values.message = "Error occurred while transaction details..!";
                return;
            }
        }
        public void DaGetUnreconTransactionList(string banktransc_gid, MdlUnreconciliationManagement values)
        {
            msSQL = "select unrecontransactiondetails_gid,banktransc_gid,department_name,assignby_name,activity_name, amount, " +
                "action_name,remaining_amount,if (samfincustomer_name is null,'--',samfincustomer_name) as samfincustomer_name,  transaction_remarks," +
               " if (account_number is null,'--',account_number) as account_number ,if (transaction_id is null,'--',transaction_id) as transaction_id ,if (urn_no is null,'--',urn_no) as urn_no ," +
               " if (transaction_date is null,'--',DATE_FORMAT(transaction_date, '%d-%m-%Y')) as transaction_date ,if (repayment_date is null,'--',DATE_FORMAT(repayment_date, '%d-%m-%Y')) as repayment_date " +
                " from brs_trn_tunrecontransactiondetails where " +
              " banktransc_gid='" + banktransc_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getunrecontransactionlist = new List<unrecontransactionlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getunrecontransactionlist.Add(new unrecontransactionlist
                    {
                        unrecontransactiondetails_gid = (dr_datarow["unrecontransactiondetails_gid"].ToString()),
                        department_name = (dr_datarow["department_name"].ToString()),
                        assignby_name = (dr_datarow["assignby_name"].ToString()),
                        activity_name = (dr_datarow["activity_name"].ToString()),
                        amount = (dr_datarow["amount"].ToString()),
                        action_name = (dr_datarow["action_name"].ToString()),
                        banktransc_gid = (dr_datarow["banktransc_gid"].ToString()),
                        transaction_remarks = (dr_datarow["transaction_remarks"].ToString()),
                        remaining_amount = (dr_datarow["remaining_amount"].ToString()),
                        samfincustomer_name = (dr_datarow["samfincustomer_name"].ToString()),
                        account_number = (dr_datarow["account_number"].ToString()),
                        transaction_id = (dr_datarow["transaction_id"].ToString()),
                        urn_no = (dr_datarow["urn_no"].ToString()),
                        transaction_date = (dr_datarow["transaction_date"].ToString()),
                        repayment_date = (dr_datarow["repayment_date"].ToString()),

                    });
                }

            }
            values.unrecontransactionlist = getunrecontransactionlist;
            dt_datatable.Dispose();
        }
        public void DaGetUnreconBankTransactionList(string banktransc_gid, MdlUnreconciliationManagement values)
        {
            msSQL = "select unrecontransactiondetails_gid,banktransc_gid,department_name,assignby_name,activity_name, amount, " +
               "action_name,remaining_amount,if (samfincustomer_name is null,'--',samfincustomer_name) as samfincustomer_name,  transaction_remarks," +
              " if (account_number is null,'--',account_number) as account_number ,if (transaction_id is null,'--',transaction_id) as transaction_id ,if (urn_no is null,'--',urn_no) as urn_no ," +
              " if (transaction_date is null,'--',DATE_FORMAT(transaction_date, '%d-%m-%Y')) as transaction_date ,if (repayment_date is null,'--',DATE_FORMAT(repayment_date, '%d-%m-%Y')) as repayment_date " +
               " from brs_trn_tunrecontransactiondetails where " +
             " banktransc_gid='" + banktransc_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getunrecontransactionlist = new List<unrecontransactionlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getunrecontransactionlist.Add(new unrecontransactionlist
                    {
                        unrecontransactiondetails_gid = (dr_datarow["unrecontransactiondetails_gid"].ToString()),
                        department_name = (dr_datarow["department_name"].ToString()),
                        assignby_name = (dr_datarow["assignby_name"].ToString()),
                        activity_name = (dr_datarow["activity_name"].ToString()),
                        amount = (dr_datarow["amount"].ToString()),
                        action_name = (dr_datarow["action_name"].ToString()),
                        banktransc_gid = (dr_datarow["banktransc_gid"].ToString()),
                        transaction_remarks = (dr_datarow["transaction_remarks"].ToString()),
                        remaining_amount = (dr_datarow["remaining_amount"].ToString()),
                        samfincustomer_name = (dr_datarow["samfincustomer_name"].ToString()),
                        account_number = (dr_datarow["account_number"].ToString()),
                        transaction_id = (dr_datarow["transaction_id"].ToString()),
                        urn_no = (dr_datarow["urn_no"].ToString()),
                        transaction_date = (dr_datarow["transaction_date"].ToString()),
                        repayment_date = (dr_datarow["repayment_date"].ToString()),
                    });
                }

            }
            values.unrecontransactionlist = getunrecontransactionlist;
            dt_datatable.Dispose();
        }
        public void DaGetBrsUnReconciliationSummary(MdlUnreconciliationManagement values, string employee_gid)
        {
            //msSQL = " SELECT a.bankalert2allocated_gid,a.brs_flag,a.ticketref_no,date_format(a.email_date, '%d-%b-%Y || %h:%i %p') as email_date, " +
            //        " date_format(a.created_date, '%d-%b-%Y %h:%i %p') as created_date, a.allocated_status, " +
            //         " if (a.operation_status = 'Completed',CONCAT(FLOOR((DATEDIFF(a.operationstatus_updated_date, a.created_date))), ' days ', MOD(HOUR(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ', MOD(HOUR(TIMEDIFF(now(), a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.created_date)), 'Mins')) as aging, " +
            //         " a.seen_flag,a.customer_urn,a.created_by, b.trn_date,b.transc_balance,b.banktransc_gid,b.taggedmember_gid,b.taggedmember_name,b.bank_gid,c.bank_name,c.branch_name, " +
            //         " a.customer_name,a.customer_gid,a.operation_status,if (a.brs_flag = 'Y','BRS','Email') as source, " +
            //         " DATE_FORMAT(f.created_date, '%d-%b-%Y %h:%i %p') as assigned_date, concat(g.user_firstname,' ',g.user_lastname,' / ',g.user_code) as assigned_by , f.taggedmember_name as assigned_to " +
            //         " FROM osd_trn_tbankalert2allocated a " +
            //         " left join brs_trn_tbanktransactiondetails  b on a.ticketref_no = b.banktransc_gid " +
            //         " left join brs_mst_tbank c on b.bank_gid = c.bank_gid " +
            //         " left join brs_trn_ttagemployee f on a.ticketref_no = f.banktransc_gid " +
            //         " left join adm_mst_tuser g on f.created_by= g.user_gid " +
            //        " WHERE a.allocated_status in ('Pending', 'Completed') and b.taggedmember_gid = '" + employee_gid + "' order by a.created_date desc ";
            //" where tagged_status='Assigned' and c.taggedmember_gid ='" + employee_gid + "' order by a.created_date desc ";

            msSQL = " SELECT a.unreconallocation_gid,a.ticketref_no," +
                  " date_format(a.assigned_date, '%d-%b-%Y %h:%i %p') as assigned_date, " +
                  " date_format(a.created_date, '%d-%b-%Y %h:%i %p') as created_date," +
                  " a.created_by, date_format(b.trn_date,'%d-%b-%Y') as trn_date,b.transc_balance,FORMAT(b.remaining_amount,2,'en_IN') as remaining_amount,FORMAT(b.transact_val,2,'en_IN') as transact_val,b.banktransc_gid," +
                  " b.taggedmember_gid,b.bank_gid,c.bank_name,c.branch_name,b.transact_particulars," +
                  " concat(g.user_firstname,' ',g.user_lastname,' / ',g.user_code) as assigned_by , " +
                  " b.taggedmember_name as assigned_to,CONCAT(FLOOR(timestampdiff(day, e.tagged_date, now())), ' days ') as aging,CONCAT(FLOOR(timestampdiff(day,  b.trn_date, now())), ' days ') as transactionaging," +
                  " case when DATE_FORMAT(f.transfer_date, '%d-%b-%Y %h:%i %p') is null then  " +
                  " date_format(a.assigned_date, '%d-%b-%Y  %h:%i %p') else  " +
                  " (select DATE_FORMAT(max(te.transfer_date), '%d-%b-%Y %h:%i %p') from brs_trn_ttransferemployee te " +
                  " where te.banktransc_gid = b.banktransc_gid) end as 'assigned_date', " +
                   " case when f.created_by is null then (select  concat(g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) from brs_trn_ttagemployee te " +
                "left join adm_mst_tuser g on te.created_by = g.user_gid " +
                " where te.banktransc_gid = b.banktransc_gid group by te.banktransc_gid) else " +
               " (select  concat(g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) from brs_trn_ttransferemployee te " +
                " left join adm_mst_tuser g on te.created_by = g.user_gid " +
                " where te.banktransc_gid = b.banktransc_gid and te.transfer_date = (select max(transfer_date) " +
                " from brs_trn_ttransferemployee f   where f.banktransc_gid = b.banktransc_gid))end as  'taggedmember_name' " +
                  " FROM brs_trn_tunreconallocation a   left join brs_trn_tbanktransactiondetails  b on" +
                  " a.ticketref_no = b.banktransc_gid " +
                  " left join brs_mst_tbank c on b.bank_gid = c.bank_gid  " +
                  " left join brs_trn_ttagemployee e on b.banktransc_gid = e.banktransc_gid  " +
                  " left join adm_mst_tuser g on c.created_by= g.user_gid  " +
                  " left join brs_trn_ttransferemployee f on b.banktransc_gid = f.banktransc_gid " +
                  " where (( b.taggedmember_gid = '" + employee_gid + "') and (b.knockoff_status not in ('Closed','Matched')) and (b.tagged_status not in ('Reassign'))) group by a.ticketref_no " +
                  "  order by a.created_date asc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.BrsUnreconciliation_list = dt_datatable.AsEnumerable().Select(row => new BrsUnreconciliation_list
                {
                    unreconallocation_gid = row["unreconallocation_gid"].ToString(),
                    ticketref_no = row["ticketref_no"].ToString(),
                    created_date = row["created_date"].ToString(),
                    bank_name = row["bank_name"].ToString(),
                    taggedmember_gid = row["taggedmember_gid"].ToString(),
                    taggedmember_name = row["taggedmember_name"].ToString(),
                    created_by = row["created_by"].ToString(),
                    branch_name = row["branch_name"].ToString(),
                    banktransc_gid = row["banktransc_gid"].ToString(),
                    trn_date = row["trn_date"].ToString(),
                    transc_balance = row["transc_balance"].ToString(),
                    assigned_date = row["assigned_date"].ToString(),
                    assigned_by = row["assigned_by"].ToString(),
                    assigned_to = row["assigned_to"].ToString(),
                    transact_val = row["transact_val"].ToString(),
                    remaining_amount = row["remaining_amount"].ToString(),
                    aging = row["aging"].ToString(),
                    transact_particulars = row["transact_particulars"].ToString(),
                    transactionaging = row["transactionaging"].ToString(),

                    //trn_date = row["trn_date"].ToString(),
                    //custref_no = row["custref_no"].ToString(),
                    //bank_name = row["bank_name"].ToString(),
                    //branch_name = row["branch_name"].ToString(),
                    //acc_no = row["acc_no"].ToString(),
                    //transc_balance = row["transc_balance"].ToString(),
                    //tagged_status = row["tagged_status"].ToString(),
                }).ToList();
            }
            dt_datatable.Dispose();
        }
        public void DaPostUnconciliationStatusUpdation(MdlUnreconciliationStatusUpdate values, string user_gid, string employee_gid)
        {
            msSQL = "SELECT a.banktransc_gid,FORMAT(a.transact_val,2,'en_IN') as transact_val," +
                   " FORMAT(a.adjust_amount,2,'en_IN') as adjust_amount, a.credit_amt " +
                    " FROM brs_trn_tbanktransactiondetails a " +                 
                       " where  a.banktransc_gid='" + values.banktransc_gid + "'  order by a.created_date desc ";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {             

                values.transact_val = objODBCDatareader["transact_val"].ToString();
                values.lsadjusted_amount = objODBCDatareader["adjust_amount"].ToString();
                
            }
            objODBCDatareader.Close();
            var transact_val = Convert.ToDecimal(values.transact_val);
            var lsadjusted_amount = Convert.ToDecimal(values.lsadjusted_amount);

            if (lsadjusted_amount < transact_val)
            {
                values.message = "Credit amount is not tally so cannot be acknowledged & closed";
                values.status = false;
                return;
            }
            
            try
            {
                msGetGid = objcmnfunctions.GetMasterGID("BUTA");
                msSQL = "Insert into brs_trn_tbrsunreconciliation2allocated( " +
                " brs2allocated_gid," +
                " banktransc_gid," +
                //" unreconciliation_status," +
                //" updation_remarks," +
                " brs_status," +
                " created_by," +
                " created_date)" +
                " values(" +
                "'" + msGetGid + "'," +
                "'" + values.banktransc_gid + "'," +
                //"'" + values.cbounreconciliation_status.Replace("'", "") + "'," +
                //"'" + values.updation_remarks.Replace("'", "") + "'," +
                "'Completed'," +
                "'" + user_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnresult != 0)
                {
                    //msSQL = " update brs_trn_tbrsunreconciliation2allocated set fileupload_gid='" + values.fileupload_gid + "'  " +
                    //                  " where banktransc_gid = '" + values.banktransc_gid + "'";
                    //mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    //msSQL = " update brs_trn_tunreconallocation set allocated_status ='Closed'" +
                    //                " where banktransc_gid = '" + values.banktransc_gid + "'";
                    //mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    unreconallocation_gid = objdbconn.GetExecuteScalar("select unreconallocation_gid from brs_trn_tunreconallocation where ticketref_no='" + values.banktransc_gid + "'");


                    msSQL = " update brs_trn_tunreconallocation set updated_by = '" + user_gid + "', " +
                            " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                              " allocated_status = 'Closed' " +
                              " where unreconallocation_gid='" + unreconallocation_gid + "'";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update brs_trn_tbanktransactiondetails set " +
                     " knockoff_status='Closed'," +                 
                    " closed_by='" + user_gid + "'," +
                    " knockoff_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "closed_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where banktransc_gid='" + values.banktransc_gid + "'";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    //   msSQL = " SELECT a.banktransc_gid,a.banktransc_refno,a.bankconfig_gid,a.bank_gid,b.bank_name,b.branch_name,b.acc_no,b.custref_no,a.reconcildoc_gid,DATE_FORMAT(a.trn_date, '%d-%b-%Y %h:%i %p') as trn_date, DATE_FORMAT(a.value_date, '%d-%b-%Y %h:%i %p') as value_date," +
                    // "DATE_FORMAT(a.payment_date, '%d-%b-%Y %h:%i %p') as payment_date , a.transact_particulars, a.debit_amt, a.credit_amt,a.transact_val, a.remarks, a.cr_dr, a.chq_no, a.created_by,  DATE_FORMAT(a.created_date, '%d-%b-%Y %h:%i %p') as created_date ," +
                    // "  a.transc_balance FROM brs_trn_tbanktransactiondetails a  " +
                    // " left join brs_mst_tbank b  on a.bank_gid =b.bank_gid  " +
                    //     " where a.banktransc_gid='" + values.banktransc_gid + "' order by a.created_date desc ";

                    //   objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    //   if (objODBCDatareader.HasRows == true)
                    //   {
                    //        lscustref_no = objODBCDatareader["custref_no"].ToString();
                    //      lsbank_name = objODBCDatareader["bank_name"].ToString();

                    //   }
                    //   string lsdepartmentgid = objdbconn.GetExecuteScalar("select businessunit_gid from osd_mst_tbusinessunit where businessunit_name='Business Process'");
                    //   MSGETGID = objcmnfunctions.GetMasterGID("ALDB");
                    //   msSQL = "Insert into osd_trn_tbankalert2allocated( " +
                    // " bankalert2allocated_gid," +
                    // " customer_name," +
                    // " customer_urn," +
                    // " customer_gid," +
                    // " ticketref_no," +
                    // " allocated_status," +
                    // " rm_remarks," +
                    //  "rm_status,"+
                    // " updated_by," +
                    //  " updated_date," +
                    //  " created_by," +
                    //  " mapping_to,"+
                    //  "department_gid,"+
                    //  "department_name," +
                    //  "email_date," +
                    // " created_date)" +
                    // " values(" +
                    //"'" + MSGETGID + "'," +
                    // "'" + lsbank_name + "'," +
                    //  "'" + lscustref_no + "'," +
                    //   "'" + lsbank_name + "'," +
                    //   "'" + values.banktransc_gid + "'," +
                    //    "'Completed'," +
                    //"'" + values.updation_remarks + "'," +
                    // "'" + values.cbounreconciliation_status + "'," +                 
                    // "'" + employee_gid + "'," +
                    // "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    //   "'" + user_gid + "'," +
                    //  "'BRS'," +
                    //  "'" + lsdepartmentgid + "'," +
                    // "'Business Process'," +                  
                    //  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    // "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    //   mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
            values.status = true;
            values.message = "Acknowledged & Closed Successfully";
        }


        public void DaUnconPendingStatusUpdation(MdlUnreconciliationStatusUpdate values, string user_gid, string employee_gid)
        {
            msSQL = "SELECT a.banktransc_gid,FORMAT(a.transact_val,2,'en_IN') as transact_val," +
                   " FORMAT(a.adjust_amount,2,'en_IN') as adjust_amount, a.credit_amt " +
                    " FROM brs_trn_tbanktransactiondetails a " +
                       " where  a.banktransc_gid='" + values.banktransc_gid + "'  order by a.created_date desc ";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {

                values.transact_val = objODBCDatareader["transact_val"].ToString();
                values.lsadjusted_amount = objODBCDatareader["adjust_amount"].ToString();

            }
            objODBCDatareader.Close();
            var transact_val = Convert.ToDecimal(values.transact_val);
            var lsadjusted_amount = Convert.ToDecimal(values.lsadjusted_amount);

            if (lsadjusted_amount < transact_val)
            {
                values.message = "Credit amount is not tally so cannot be acknowledged & closed";
                values.status = false;
                return;
            }

            try
            {
                MSGETGID = objcmnfunctions.GetMasterGID("UNRE");
                msSQL = "Insert into brs_trn_tunreconallocation( " +
              " unreconallocation_gid," +
              " banktransc_gid," +
              " ticketref_no," +
              " allocated_status," +
              " updated_date," +
              " updated_by ," +
               "created_by," +
              " created_date)" +
              " values(" +
              "'" + MSGETGID + "'," +
              "'" + values.banktransc_gid + "'," +
              "'" + values.banktransc_gid + "'," +
              "'Closed' ," +
              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
              "'" + user_gid + "'," +
              "'" + user_gid + "'," +
              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnresult != 0)
                {
                    //msSQL = " update brs_trn_tbrsunreconciliation2allocated set fileupload_gid='" + values.fileupload_gid + "'  " +
                    //                  " where banktransc_gid = '" + values.banktransc_gid + "'";
                    //mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    //msSQL = " update brs_trn_tunreconallocation set allocated_status ='Closed'" +
                    //                " where banktransc_gid = '" + values.banktransc_gid + "'";
                    //mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    //unreconallocation_gid = objdbconn.GetExecuteScalar("select unreconallocation_gid from brs_trn_tunreconallocation where ticketref_no='" + values.banktransc_gid + "'");


                    msSQL = " update brs_trn_tunreconallocation set updated_by = '" + user_gid + "', " +
                            " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                              " allocated_status = 'Closed' " +
                              " where unreconallocation_gid='" + unreconallocation_gid + "'";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update brs_trn_tbanktransactiondetails set " +
                     " knockoff_status='Closed'," +
                      " tagged_status='Not Assigned'," +
                    " closed_by='" + user_gid + "'," +
                    " knockoff_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "closed_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where banktransc_gid='" + values.banktransc_gid + "'";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    //   msSQL = " SELECT a.banktransc_gid,a.banktransc_refno,a.bankconfig_gid,a.bank_gid,b.bank_name,b.branch_name,b.acc_no,b.custref_no,a.reconcildoc_gid,DATE_FORMAT(a.trn_date, '%d-%b-%Y %h:%i %p') as trn_date, DATE_FORMAT(a.value_date, '%d-%b-%Y %h:%i %p') as value_date," +
                    // "DATE_FORMAT(a.payment_date, '%d-%b-%Y %h:%i %p') as payment_date , a.transact_particulars, a.debit_amt, a.credit_amt,a.transact_val, a.remarks, a.cr_dr, a.chq_no, a.created_by,  DATE_FORMAT(a.created_date, '%d-%b-%Y %h:%i %p') as created_date ," +
                    // "  a.transc_balance FROM brs_trn_tbanktransactiondetails a  " +
                    // " left join brs_mst_tbank b  on a.bank_gid =b.bank_gid  " +
                    //     " where a.banktransc_gid='" + values.banktransc_gid + "' order by a.created_date desc ";

                    //   objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    //   if (objODBCDatareader.HasRows == true)
                    //   {
                    //        lscustref_no = objODBCDatareader["custref_no"].ToString();
                    //      lsbank_name = objODBCDatareader["bank_name"].ToString();

                    //   }
                    //   string lsdepartmentgid = objdbconn.GetExecuteScalar("select businessunit_gid from osd_mst_tbusinessunit where businessunit_name='Business Process'");
                    //   MSGETGID = objcmnfunctions.GetMasterGID("ALDB");
                    //   msSQL = "Insert into osd_trn_tbankalert2allocated( " +
                    // " bankalert2allocated_gid," +
                    // " customer_name," +
                    // " customer_urn," +
                    // " customer_gid," +
                    // " ticketref_no," +
                    // " allocated_status," +
                    // " rm_remarks," +
                    //  "rm_status,"+
                    // " updated_by," +
                    //  " updated_date," +
                    //  " created_by," +
                    //  " mapping_to,"+
                    //  "department_gid,"+
                    //  "department_name," +
                    //  "email_date," +
                    // " created_date)" +
                    // " values(" +
                    //"'" + MSGETGID + "'," +
                    // "'" + lsbank_name + "'," +
                    //  "'" + lscustref_no + "'," +
                    //   "'" + lsbank_name + "'," +
                    //   "'" + values.banktransc_gid + "'," +
                    //    "'Completed'," +
                    //"'" + values.updation_remarks + "'," +
                    // "'" + values.cbounreconciliation_status + "'," +                 
                    // "'" + employee_gid + "'," +
                    // "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    //   "'" + user_gid + "'," +
                    //  "'BRS'," +
                    //  "'" + lsdepartmentgid + "'," +
                    // "'Business Process'," +                  
                    //  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    // "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    //   mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
            values.status = true;
            values.message = "Acknowledged & Closed Successfully";
        }

        public void DaPostRMSendback(MdlUnreconciliationStatusUpdate values, string employee_gid)
        {

            msSQL = "select concat(b.user_firstname ,' ',b.user_lastname,' / ',b.user_code) as rm_name from hrm_mst_temployee a" +
                        " left join adm_mst_tuser b on a.user_gid=b.user_gid where employee_gid='" + employee_gid + "'";
            string lsrm_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " update brs_trn_tbanktransactiondetails set " +
                    " assigned_rm = '" + lsrm_name + "'," +
                    " rmsendback_on = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                    " sendback_reason ='" + values.sendback_reason.Replace("'", "") + "', " +
                    " tagged_status = 'Reassign' where " +
                    " banktransc_gid = '" + values.banktransc_gid + "'";

            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //msSQL = " update brs_trn_tunreconallocation set " +
            //        "  allocated_status='Reassign' " +
            //        " where " +
            //        " unreconallocation_gid = '" + values.banktransc_gid + "'";

            //mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            unreconallocation_gid = objdbconn.GetExecuteScalar("select unreconallocation_gid from brs_trn_tunreconallocation where ticketref_no='" + values.banktransc_gid + "'");


            msSQL = " update brs_trn_tunreconallocation set " +
                      " allocated_status = 'Reassign' " +
                      " where unreconallocation_gid='" + unreconallocation_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msGetGid2 = objcmnfunctions.GetMasterGID("BURL");
            msSQL = " insert into brs_trn_tunreconnlog(" +
                    " unreconnlog_gid," +
                    " banktransc_gid," +
                    " reason," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid2 + "'," +
                    "'" + values.banktransc_gid + "'," +
                    "'" + values.sendback_reason.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
            values.message = "UnReconciliation Details Sent Back to BRS Successfully";
        }
        
        public void GetAllocatedCount(MdlUnreconciliationStatusUpdate values, string employee_gid)
        {
           
            msSQL = " select count(distinct b.banktransc_gid) from brs_trn_tunreconallocation a " +
                " left join brs_trn_tbanktransactiondetails b on a.ticketref_no = b.banktransc_gid " +
              " where (( b.taggedmember_gid = '" + employee_gid + "') and (b.knockoff_status not in ('Closed','Matched')) and (b.tagged_status not in ('Reassign'))) ";
            values.unreconciliation_count = objdbconn.GetExecuteScalar(msSQL);
            values.status = true;
        }
        public void DaGetDepartmentName(MdlUnreconciliationStatusUpdate values,string employee_gid)
        {
            msSQL = " SELECT a.department_name  FROM hrm_mst_tdepartment a " +
                   "left join hrm_mst_temployee b on  b.department_gid = a.department_gid " +
                    " where b.employee_gid = '" + employee_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.department_name = objODBCDatareader["department_name"].ToString();
            }
            objODBCDatareader.Close();
            msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                  "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                  "where b.employee_gid ='" + employee_gid + "'";
            values.employee_name = objdbconn.GetExecuteScalar(msSQL);
        }
        public void DaGetUnreconTransactionDelete(string unrecontransactiondetails_gid, string banktransc_gid, string user_gid, MdlUnreconciliationManagement values)
        {

            msSQL = "select created_by from brs_trn_tunrecontransactiondetails where unrecontransactiondetails_gid='" + unrecontransactiondetails_gid + "'";
            lscreated_by = objdbconn.GetExecuteScalar(msSQL);

            if(lscreated_by != user_gid)
            {
                values.message = "Updated by user can only delete the entry";
                values.status = false;
                return;
            }
            msSQL = " select action_name  from brs_trn_tunrecontransactiondetails where unrecontransactiondetails_gid='" + unrecontransactiondetails_gid + "'";
            lsaction_name = objdbconn.GetExecuteScalar(msSQL);

            if (lsaction_name == "Booked in LMS / FA")
            {

                msSQL = "select FORMAT(amount,2,'en_IN') as amount from brs_trn_tunrecontransactiondetails where unrecontransactiondetails_gid='" + unrecontransactiondetails_gid + "'";
                lsadjust_amount = objdbconn.GetExecuteScalar(msSQL);


                if (lsadjust_amount == null || lsadjust_amount == "")
                {
                    lsadjust_amount = "0";
                }
                var convertDecimal2 = Convert.ToDecimal(lsadjust_amount);

                msSQL = " select FORMAT(remaining_amount,2,'en_IN') as transact_val  from brs_trn_tbanktransactiondetails where banktransc_gid='" + banktransc_gid + "'";

                lsremaining_amount = objdbconn.GetExecuteScalar(msSQL);
                if (lsremaining_amount == null || lsremaining_amount == "")
                {
                    lsremaining_amount = "0";
                }
                var convertDecimalremaining = Convert.ToDecimal(lsremaining_amount);


                remaining_amount = convertDecimalremaining + convertDecimal2;

                msSQL = " select FORMAT(adjust_amount,2,'en_IN') as adjust_amount  from brs_trn_tbanktransactiondetails where banktransc_gid='" + banktransc_gid + "'";

                lsadjusted_amount = objdbconn.GetExecuteScalar(msSQL);
                if (lsadjusted_amount == null || lsadjusted_amount == "")
                {
                    lsadjusted_amount = "0";
                }
                var convertDecimaladjusted = Convert.ToDecimal(lsadjusted_amount);

                lsadjustedamount = convertDecimaladjusted - convertDecimal2;


                //lsaddremainingamount = remaining_amount + convertDecimal2;

                msSQL = " update brs_trn_tbanktransactiondetails set remaining_amount = '" + remaining_amount + "' where banktransc_gid='" + banktransc_gid + "'";
                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update brs_trn_tbanktransactiondetails set adjust_amount = '" + lsadjustedamount + "' where banktransc_gid='" + banktransc_gid + "'";
                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //msSQL = " update brs_trn_tbanktransactiondetails set remaining_amount = '" + lsaddremainingamount + "' where banktransc_gid='" + banktransc_gid + "'";
                //mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update brs_trn_tunrecontransactiondetails set remaining_amount = '" + remaining_amount + "' where banktransc_gid='" + banktransc_gid + "'";
                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                
            }

            if (lsadjust_amount == null || lsadjust_amount == "")
            {
                lsadjust_amount = "0";
            }
            msGetGid = objcmnfunctions.GetMasterGID("TDDL");
            msSQL = " insert into  brs_trn_ttransactiondetailsdeletelog(" +
                     "transactiondetailsdeletelog, " +
                     "banktransc_gid, " +
                     "adjust_amount, " +
                     "deleted_by, " +
                     "deleted_date) " +
                     " values(" +
                     "'" + msGetGid + "'," +
                     "'" + banktransc_gid + "', " +
                       "'" + lsadjust_amount.Replace(",", "") + "'," +
                     "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from brs_trn_tunrecontransactiondetails where unrecontransactiondetails_gid='" + unrecontransactiondetails_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {

                values.message = "Transaction Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }
        public void DaGetSamfinCustomerSummary(MdlUnreconciliationManagement values)
        {
            try
            {
                msSQL = " SELECT application_gid,concat(customer_name,' / ',customer_urn) as customer_name from ocs_trn_tcadapplication group by application_gid";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getassignedlist = new List<assignedlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getassignedlist.Add(new assignedlist
                        {
                            samfincustomer_name = (dr_datarow["customer_name"].ToString()),
                            samfincustomer_gid = (dr_datarow["application_gid"].ToString()),


                        });
                    }
                    values.assignedlist = getassignedlist;

                }
                dt_datatable.Dispose();
                values.status = true;

            }
            catch (Exception ex)
            {
                values.status = false;
            }

        }
        
        public void DaGetAdjustAdviceEmployeeWiseShow(MdlUnreconciliationManagement values,string employee_gid)
        {
            try
            {
                msSQL = " SELECT employee_gid,employee_name from brs_mst_tadjustadviceemployeeshow where employee_gid = '" + employee_gid + "' ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    msSQL = " SELECT adjustadvicedetails_gid,adjustadvice_name from brs_mst_tadjustadvicedetails ";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getadjustadvicelist = new List<adjustadvicelist>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getadjustadvicelist.Add(new adjustadvicelist
                            {
                                adjustadvice_name = (dr_datarow["adjustadvice_name"].ToString()),
                                adjustadvicedetails_gid = (dr_datarow["adjustadvicedetails_gid"].ToString()),


                            });
                        }
                        values.adjustadvicelist = getadjustadvicelist;

                    }
                    dt_datatable.Dispose();
                    values.status = true;

                }
                else
                {
                    msSQL = " SELECT adjustadvicedetails_gid,adjustadvice_name from brs_mst_tadjustadvicedetails limit 1 ";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getadjustadvicelist = new List<adjustadvicelist>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getadjustadvicelist.Add(new adjustadvicelist
                            {
                                adjustadvice_name = (dr_datarow["adjustadvice_name"].ToString()),
                                adjustadvicedetails_gid = (dr_datarow["adjustadvicedetails_gid"].ToString()),


                            });
                        }
                        values.adjustadvicelist = getadjustadvicelist;

                    }
                    dt_datatable.Dispose();
                    values.status = true;
                }
            }
            catch (Exception ex)
            {
                values.status = false;
            }

        }

        public void DaGetUnreconCreditSummaryManagementExcelExport(MdlUnreconciliationManagement values)
        {

            msSQL = "CALL brs_trn_spunreconcreditsummarymanagement ()";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            //ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Unrecon Credit Pending Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lsname = "Unrecon Credit Pending Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Brs/Unrecon Credit Pending Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Brs/Unrecon Credit Pending Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                values.lscloudpath = lscompany_code + "/" + "Brs/Unrecon Credit Pending Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 50])  //Address "A1:A50"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Brs/Unrecon Credit Pending Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
            values.lscloudpath = objcmnstorage.EncryptData(values.lscloudpath);
            values.lspath = objcmnstorage.EncryptData(values.lspath);
            values.status = true;
            values.message = "Success";
        }
        public void DaGetUnreconCreditFinancePendingManagementExcelExport(MdlUnreconciliationManagement values)
        {

            msSQL = "CALL brs_trn_spunreconcreditfinancependingmanagement ()";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            //ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Unrecon Credit Finance Pending Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lsname = "Unrecon Credit Finance Pending Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Brs/Unrecon Credit Finance Pending Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Brs/Unrecon Credit Finance Pending Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                values.lscloudpath = lscompany_code + "/" + "Brs/Unrecon Credit Finance Pending Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 50])  //Address "A1:A50"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Brs/Unrecon Credit Finance Pending Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
            values.lscloudpath = objcmnstorage.EncryptData(values.lscloudpath);
            values.lspath = objcmnstorage.EncryptData(values.lspath);
            values.status = true;
            values.message = "Success";
        }
        public void DaGetUnreconCreditReassignPendingSummaryManagementExcelExport(MdlUnreconciliationManagement values)
        {

            msSQL = "CALL brs_trn_spunreconcreditreassignpendingmanagement ()";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            //ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Unrecon Credit Reassign Pending Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lsname = "Unrecon Credit Reassign Pending Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Brs/Unrecon Credit Reassign Pending Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Brs/Unrecon Credit Reassign Pending Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                values.lscloudpath = lscompany_code + "/" + "Brs/Unrecon Credit Reassign Pending Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 50])  //Address "A1:A50"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Brs/Unrecon Credit Reassign Pending Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
            values.lscloudpath = objcmnstorage.EncryptData(values.lscloudpath);
            values.lspath = objcmnstorage.EncryptData(values.lspath);
            values.status = true;
            values.message = "Success";
        }
        public void DaGetUnreconCreditAssignedManagementExcelExport(MdlUnreconciliationManagement values)
        {

            msSQL = "CALL brs_trn_spunreconcreditassignedmanagement ()";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            //ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Unrecon Credit Assign Pending Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lsname = "Unrecon Credit Assign Pending Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Brs/Unrecon Credit Assign Pending Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Brs/Unrecon Credit Assign Pending Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                values.lscloudpath = lscompany_code + "/" + "Brs/Unrecon Credit Assign Pending Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 50])  //Address "A1:A50"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Brs/Unrecon Credit Assign Pending Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
            values.lscloudpath = objcmnstorage.EncryptData(values.lscloudpath);
            values.lspath = objcmnstorage.EncryptData(values.lspath);
            values.status = true;
            values.message = "Success";
        }
        public void DaGetUnreconCreditClosedManagementExcelExport(MdlUnreconciliationManagement values)
        {

            msSQL = "CALL brs_trn_spunreconcreditclosedmanagement ()";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            //ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Unrecon Credit Closed Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lsname = "Unrecon Credit Closed Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Brs/Unrecon Credit Closed Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Brs/Unrecon Credit Closed Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                values.lscloudpath = lscompany_code + "/" + "Brs/Unrecon Credit Closed Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 50])  //Address "A1:A50"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Brs/Unrecon Credit Closed Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
            values.lscloudpath = objcmnstorage.EncryptData(values.lscloudpath);
            values.lspath = objcmnstorage.EncryptData(values.lspath);
            values.status = true;
            values.message = "Success";
        }
        // debit

        public void DaGetUnreconDebitPendingManagementExcelExport(MdlUnreconciliationManagement values)
        {

            msSQL = "CALL brs_trn_spunrecondebitpendingsummarymanagement ()";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            //ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Unrecon Debit Pending Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lsname = "Unrecon Debit Pending Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Brs/Unrecon Debit Pending Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Brs/Unrecon Debit Pending Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                values.lscloudpath = lscompany_code + "/" + "Brs/Unrecon Debit Pending Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 50])  //Address "A1:A50"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Brs/Unrecon Debit Pending Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
            values.lscloudpath = objcmnstorage.EncryptData(values.lscloudpath);
            values.lspath = objcmnstorage.EncryptData(values.lspath);
            values.status = true;
            values.message = "Success";
        }
        public void DaGetUnreconDebitFinancePendingManagementExcelExport(MdlUnreconciliationManagement values)
        {

            msSQL = "CALL brs_trn_spunrecondebitfinancependingmanagement ()";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            //ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Unrecon Debit Finance Pending Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lsname = "Unrecon Debit Finance Pending Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Brs/Unrecon Debit Finance Pending Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Brs/Unrecon Debit Finance Pending Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                values.lscloudpath = lscompany_code + "/" + "Brs/Unrecon Debit Finance Pending Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 50])  //Address "A1:A50"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Brs/Unrecon Debit Finance Pending Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
            values.lscloudpath = objcmnstorage.EncryptData(values.lscloudpath);
            values.lspath = objcmnstorage.EncryptData(values.lspath);
            values.status = true;
            values.message = "Success";
        }
        public void DaGetUnreconDebitReassignPendingSummaryManagementExcelExport(MdlUnreconciliationManagement values)
        {

            msSQL = "CALL brs_trn_spunrecondebitreassignpendingmanagement ()";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            //ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Unrecon Debit Reassign Pending Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lsname = "Unrecon Debit Reassign Pending Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Brs/Unrecon Debit Reassign Pending Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Brs/Unrecon Debit Reassign Pending Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                values.lscloudpath = lscompany_code + "/" + "Brs/Unrecon Debit Reassign Pending Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 50])  //Address "A1:A50"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Brs/Unrecon Debit Reassign Pending Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
            values.lscloudpath = objcmnstorage.EncryptData(values.lscloudpath);
            values.lspath = objcmnstorage.EncryptData(values.lspath);
            values.status = true;
            values.message = "Success";
        }
        public void DaGetUnreconDebitAssignedManagementExcelExport(MdlUnreconciliationManagement values)
        {

            msSQL = "CALL brs_trn_spunrecondebitassignedmanagement ()";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            //ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Unrecon Debit Assign Pending Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lsname = "Unrecon Debit Assign Pending Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Brs/Unrecon Debit Assign Pending Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Brs/Unrecon Debit Assign Pending Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                values.lscloudpath = lscompany_code + "/" + "Brs/Unrecon Debit Assign Pending Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 50])  //Address "A1:A50"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Brs/Unrecon Debit Assign Pending Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
            values.lscloudpath = objcmnstorage.EncryptData(values.lscloudpath);
            values.lspath = objcmnstorage.EncryptData(values.lspath);
            values.status = true;
            values.message = "Success";
        }
        public void DaGetUnreconDebitClosedManagementExcelExport(MdlUnreconciliationManagement values)
        {

            msSQL = "CALL brs_trn_spunrecondebitclosedmanagement ()";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            //ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Unrecon Debit Closed Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lsname = "Unrecon Debit Closed Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Brs/Unrecon Debit Closed Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Brs/Unrecon Debit Closed Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                values.lscloudpath = lscompany_code + "/" + "Brs/Unrecon Debit Closed Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 50])  //Address "A1:A50"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Brs/Unrecon Debit Closed Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
            values.lscloudpath = objcmnstorage.EncryptData(values.lscloudpath);
            values.lspath = objcmnstorage.EncryptData(values.lspath);
            values.status = true;
            values.message = "Success";
        }
        public void DaGetUnreconciliationAssignedSummaryExcelExport(MdlUnreconciliationManagement values,string employee_gid)
        {

            msSQL = "CALL brs_trn_spunreconciliationassignedsummary ( '" + employee_gid + "')";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            //ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Unreconciliation Assigned Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lsname = "Unreconciliation Assigned Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Brs/Unreconciliation Assigned Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Brs/Unreconciliation Assigned Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                values.lscloudpath = lscompany_code + "/" + "Brs/Unreconciliation Assigned Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 50])  //Address "A1:A50"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Brs/Unreconciliation Assigned Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
            values.lscloudpath = objcmnstorage.EncryptData(values.lscloudpath);
            values.lspath = objcmnstorage.EncryptData(values.lspath);
            values.status = true;
            values.message = "Success";
        }
    }

}