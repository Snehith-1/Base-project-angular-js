using ems.brs.Models;
using ems.storage.Functions;
using ems.utilities.Functions;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;
using OfficeOpenXml.Style;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace ems.brs.Dataacess
{
    public class DaMyUnreconciliationManagement
    {
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        string msSQL, msGetGid, msGetGid1, MSGETGID, lsbrs_gid, lsbanktransac_name, msGetGid2, lsbanktrans_gid;
        OdbcDataReader objODBCDatareader;      
        dbconn objdbconn = new dbconn();
        HttpPostedFile httpPostedFile;
        string lspath, endRange, excelRange, lscustref_no, lsbank_name;
        string lsUnreconpath, lsUnreconname, lsUnreconcloudpath;
        int mnresult;
        int rowCount, columnCount;
        string lstaggedmember_name, lstaggedmember_gid, lstagemployee_gid;
        //ExcelPackage xlPackage = new ExcelPackage();
        ExcelPackage xlPackage;

        public void DaGetBank(MdlMyUnreconciliationManagement values)
        {
            msSQL = " SELECT brsbank_gid,bank_name FROM brs_mst_tbank " +
                    "  group by brsbank_gid ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbankdtl_List = new List<bankdtllist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getbankdtl_List.Add(new bankdtllist
                    {
                        bankname_gid = dt["brsbank_gid"].ToString(),
                        bankname_name = dt["bank_name"].ToString(),

                    });
                    values.bankdtllist = getbankdtl_List;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaBankNameList(MdlMyUnreconciliationManagement values, string bank_gid)
        {
            msSQL = " SELECT brsbank_gid,bank_name FROM brs_mst_tbank where brsbank_gid ='" + bank_gid + "'" +
                        "  group by brsbank_gid ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_bankdtl = new List<bankdtllist>();
            if (dt_datatable.Rows.Count != 0)
            {
                values.bankdtllist = dt_datatable.AsEnumerable().Select(row => new bankdtllist
                {
                    bankdtl_gid = row["brsbank_gid"].ToString(),
                    bankdtl_name = row["bank_name"].ToString()
                }
                ).ToList();
            }
            dt_datatable.Dispose();
        }
        public void DaGetMyunreConciliationSummarySearch(MdlMyUnreconciliationManagement values)
        {
            try
            {

                string bank_gid, cr_dr, knockoff_status, amount_greater, amount_lesser, trns_date;

                bank_gid = values.bank_gid;
                cr_dr = values.cr_dr;
                knockoff_status = values.knockoff_status;
                amount_greater = values.amount_greater;
                amount_lesser = values.amount_lesser;
                trns_date = values.trns_date;
                if (amount_greater == null || amount_greater == "")
                {
                    amount_greater = "0";
                }
                else
                {

                    amount_greater = (values.amount_greater).Replace(",", "");

                }
                if (amount_lesser == null || amount_lesser == "")
                {
                    amount_lesser = "0";
                }
                else
                {

                    amount_lesser = (values.amount_lesser).Replace(",", "");
                }
                msSQL = "call brs_rpt_spmyunreconpendingsummary(" + "'" + values.bank_gid + "'" + "," + "'" + values.cr_dr + "'" + "," + "'" + values.knockoff_status + "'" + "," + "" + amount_greater + "" + "," + "" + amount_lesser + "" + "," + "'" + values.trns_date + "'" + ")";



                dt_datatable = objdbconn.GetDataTable(msSQL);
                var GetMyUnreconciliation_list = new List<MyUnreconciliation_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        GetMyUnreconciliation_list.Add(new MyUnreconciliation_list
                        {
                            tagemployee_gid = (dr_datarow["tagemployee_gid"].ToString()),
                            banktransc_gid = (dr_datarow["banktransc_gid"].ToString()),
                            transact_particulars = (dr_datarow["transact_particulars"].ToString()),
                            tagged_remarks = (dr_datarow["tagged_remarks"].ToString()),
                            transact_val = (dr_datarow["transact_val"].ToString()),
                            trn_date = (dr_datarow["trn_date"].ToString()),
                            cr_dr = (dr_datarow["cr_dr"].ToString()),
                            custref_no = (dr_datarow["custref_no"].ToString()),
                            bank_name = (dr_datarow["bank_name"].ToString()),
                            tagged_status = (dr_datarow["tagged_status"].ToString()),
                            taggedmember_name = (dr_datarow["taggedmember_name"].ToString()),
                            knockoff_status = (dr_datarow["knockoff_status"].ToString()),

                        });
                    }
                    values.MyUnreconciliation_list = GetMyUnreconciliation_list;
                }
                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }
        public void DaGetMyunreConciliationSummary(MdlMyUnreconciliationManagement values)
        {
            try
            {

                string bank_gid, cr_dr, knockoff_status, amount_greater, amount_lesser, trns_date;

                bank_gid = values.bank_gid;
                cr_dr = values.cr_dr;
                knockoff_status = values.knockoff_status;
                amount_greater = values.amount_greater;
                amount_lesser = values.amount_lesser;
                trns_date = values.trns_date;
                if (amount_greater == null || amount_greater == "")
                {
                    amount_greater = "0";
                }
                else
                {

                    amount_greater = (values.amount_greater).Replace(",", "");

                }
                if (amount_lesser == null || amount_lesser == "")
                {
                    amount_lesser = "0";
                }
                else
                {

                    amount_lesser = (values.amount_lesser).Replace(",", "");
                }

                msSQL = "call brs_rpt_spmyunreconpendingsummary(" + "'" + values.bank_gid + "'" + "," + "'" + values.cr_dr + "'" + "," + "'" + values.knockoff_status + "'" + "," + "" + amount_greater + "" + "," + "" + amount_lesser + "" + "," + "'" + values.trns_date + "'" + ")";

                dt_datatable = objdbconn.GetDataTable(msSQL);

                var GetMyUnreconciliation_list = new List<MyUnreconciliation_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        GetMyUnreconciliation_list.Add(new MyUnreconciliation_list
                        {
                            tagemployee_gid = (dr_datarow["tagemployee_gid"].ToString()),
                            banktransc_gid = (dr_datarow["banktransc_gid"].ToString()),
                            transact_particulars = (dr_datarow["transact_particulars"].ToString()),
                            tagged_remarks = (dr_datarow["tagged_remarks"].ToString()),
                            transact_val = (dr_datarow["transact_val"].ToString()),
                            trn_date = (dr_datarow["trn_date"].ToString()),
                            cr_dr = (dr_datarow["cr_dr"].ToString()),
                            custref_no = (dr_datarow["custref_no"].ToString()),
                            bank_name = (dr_datarow["bank_name"].ToString()),
                            tagged_status = (dr_datarow["tagged_status"].ToString()),
                            taggedmember_name = (dr_datarow["taggedmember_name"].ToString()),
                            knockoff_status = (dr_datarow["knockoff_status"].ToString()),
                        });
                    }
                    values.MyUnreconciliation_list = GetMyUnreconciliation_list;
                }
                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }
        public void DaGetMyunreConciliationSummaryCount(MdlMyUnreconciliationManagement values)
        {
            try
            {
                msSQL = " SELECT  distinct count(*) as pendingrpt_count  FROM brs_trn_tbanktransactiondetails "  + 
                       " WHERE ((knockoff_status = 'Pending' and (tagged_status = 'Assigned' or tagged_status = 'Pending'))) ";
                values.pendingrpt_count = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " SELECT distinct count(*) as closedrpt_count  FROM brs_trn_tbanktransactiondetails" +
                    " where (( knockoff_status='Matched') or (knockoff_status='AssignMatched') or (knockoff_status='ManuallyMatched') or (knockoff_status='PartiallyMatched')) ";
                values.closedrpt_count = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " SELECT distinct count(*) as allocatependingrpt_count FROM osd_trn_tbankalert2allocated " +
                     " Where brs_flag='Y' " ;
                values.allocatependingrpt_count = objdbconn.GetExecuteScalar(msSQL);

            }
            catch
            {
                values.status = false;
            }

        }

        public void DaUnreconPendingExport(MyUnreconciliation_list values, string employee_gid)
        {

            string bank_gid, cr_dr, knockoff_status, amount_greater, amount_lesser, trns_date;

            bank_gid = values.bank_gid;
            cr_dr = values.cr_dr;
            knockoff_status = values.knockoff_status;
            amount_greater = values.amount_greater;
            amount_lesser = values.amount_lesser;
            trns_date = values.trns_date;
            if (amount_greater == null || amount_greater == "")
            {
                amount_greater = "0";
            }
            else
            {

                amount_greater = (values.amount_greater).Replace(",", "");

            }
            if (amount_lesser == null || amount_lesser == "")
            {
                amount_lesser = "0";
            }
            else
            {

                amount_lesser = (values.amount_lesser).Replace(",", "");
            }
            msSQL = "call brs_rpt_spmyunreconpendingreport(" + "'" + values.bank_gid + "'" + "," + "'" + values.cr_dr + "'" + "," + "'" + values.knockoff_status + "'" + "," + "" + amount_greater + "" + "," + "" + amount_lesser + "" + "," + "'" + values.trns_date + "'" + ")";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Unreconciliation Pending Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                //values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "OSD/AllTicket/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "BRS/UnreconciliationReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

                {
                    if ((!System.IO.Directory.Exists(values.lspath)))
                        System.IO.Directory.CreateDirectory(values.lspath);
                }

                values.lsname = "Unreconciliation_Pending_Report" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".xlsx";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "BRS/UnreconciliationReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                values.lscloudpath = lscompany_code + "/" + "BRS/UnreconciliationReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                // values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "OSD/AllTicket/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                //bool status;
                //status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "OSD/AllTicket/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname,ms);
                //ms.Close();
                //values.lspath = "erpdocument" + "/" + lscompany_code + "/" + "OSD/AllTicket/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 44])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", values.lscloudpath, ms);
                values.lspath = objcmnstorage.EncryptData(ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "BRS/UnreconciliationReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname);
                values.lscloudpath = objcmnstorage.EncryptData(lscompany_code + "/" + "BRS/UnreconciliationReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname);
                ms.Close();
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
        }
        public void DaGetMyunreConciliationClosedSummarySearch(MdlMyUnreconciliationManagement values)
        {
            try
            {

                string bank_gid, cr_dr, knockoff_status, amount_greater, amount_lesser, trns_date;

                bank_gid = values.bank_gid;
                cr_dr = values.cr_dr;
                knockoff_status = values.knockoff_status;
                amount_greater = values.amount_greater;
                amount_lesser = values.amount_lesser;
                trns_date = values.trns_date;
                if (amount_greater == null || amount_greater == "")
                {
                    amount_greater = "0";
                }
                else
                {

                    amount_greater = (values.amount_greater).Replace(",", "");

                }
                if (amount_lesser == null || amount_lesser == "")
                {
                    amount_lesser = "0";
                }
                else
                {

                    amount_lesser = (values.amount_lesser).Replace(",", "");
                }

                msSQL = "call brs_rpt_spmyunreconclosedsummary(" + "'" + values.bank_gid + "'" + "," + "'" + values.cr_dr + "'" + "," + "'" + values.knockoff_status + "'" + "," + "" + amount_greater + "" + "," + "" + amount_lesser + "" + "," + "'" + values.trns_date + "'" + ")";



                dt_datatable = objdbconn.GetDataTable(msSQL);
                var GetMyUnreconciliation_list = new List<MyUnreconciliationClose_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        GetMyUnreconciliation_list.Add(new MyUnreconciliationClose_list
                        {
                            tagemployee_gid = (dr_datarow["tagemployee_gid"].ToString()),
                            banktransc_gid = (dr_datarow["banktransc_gid"].ToString()),
                            transact_particulars = (dr_datarow["transact_particulars"].ToString()),
                            tagged_remarks = (dr_datarow["tagged_remarks"].ToString()),
                            transact_val = (dr_datarow["transact_val"].ToString()),
                            trn_date = (dr_datarow["trn_date"].ToString()),
                            cr_dr = (dr_datarow["cr_dr"].ToString()),
                            custref_no = (dr_datarow["custref_no"].ToString()),
                            bank_name = (dr_datarow["bank_name"].ToString()),
                            tagged_status = (dr_datarow["tagged_status"].ToString()),
                            taggedmember_name = (dr_datarow["taggedmember_name"].ToString()),
                            knockoff_status = (dr_datarow["knockoff_status"].ToString()),
                            manualknockoff_remarks = (dr_datarow["manualknockoff_remarks"].ToString()),
                            //repayment_amount = (dr_datarow["repayment_amount"].ToString()),
                            //remarks = (dr_datarow["remarks"].ToString()),
                            //repay_knockoff_status = (dr_datarow["repay_knockoff_status"].ToString()),
                            //account_number = (dr_datarow["account_number"].ToString()),
                        });
                    }
                    values.MyUnreconciliationClose_list = GetMyUnreconciliation_list;
                }
                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }
        public void DaGetMyunreConciliationClosedSummary(MdlMyUnreconciliationManagement values)
        {
            try
            {

                string bank_gid, cr_dr, knockoff_status, amount_greater, amount_lesser, trns_date;

                bank_gid = values.bank_gid;
                cr_dr = values.cr_dr;
                knockoff_status = values.knockoff_status;
                amount_greater = values.amount_greater;
                amount_lesser = values.amount_lesser;
                trns_date = values.trns_date;
                if (amount_greater == null || amount_greater == "")
                {
                    amount_greater = "0";
                }
                else
                {

                    amount_greater = (values.amount_greater).Replace(",", "");

                }
                if (amount_lesser == null || amount_lesser == "")
                {
                    amount_lesser = "0";
                }
                else
                {

                    amount_lesser = (values.amount_lesser).Replace(",", "");
                }
                msSQL = "call brs_rpt_spmyunreconclosedsummary(" + "'" + values.bank_gid + "'" + "," + "'" + values.cr_dr + "'" + "," + "'" + values.knockoff_status + "'" + "," + "" + amount_greater + "" + "," + "" + amount_lesser + "" + "," + "'" + values.trns_date + "'" + ")";



                dt_datatable = objdbconn.GetDataTable(msSQL);
                var GetMyUnreconciliation_list = new List<MyUnreconciliationClose_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        GetMyUnreconciliation_list.Add(new MyUnreconciliationClose_list
                        {
                            tagemployee_gid = (dr_datarow["tagemployee_gid"].ToString()),
                            banktransc_gid = (dr_datarow["banktransc_gid"].ToString()),
                            transact_particulars = (dr_datarow["transact_particulars"].ToString()),
                            tagged_remarks = (dr_datarow["tagged_remarks"].ToString()),
                            transact_val = (dr_datarow["transact_val"].ToString()),
                            trn_date = (dr_datarow["trn_date"].ToString()),
                            cr_dr = (dr_datarow["cr_dr"].ToString()),
                            custref_no = (dr_datarow["custref_no"].ToString()),
                            bank_name = (dr_datarow["bank_name"].ToString()),
                            tagged_status = (dr_datarow["tagged_status"].ToString()),
                            taggedmember_name = (dr_datarow["taggedmember_name"].ToString()),
                            knockoff_date = (dr_datarow["knockoff_date"].ToString()),                           
                            knockoff_status = (dr_datarow["knockoff_status"].ToString()),
                            manualknockoff_remarks = (dr_datarow["manualknockoff_remarks"].ToString()),
                        });
                    }
                    values.MyUnreconciliationClose_list = GetMyUnreconciliation_list;
                }
                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }

        public void DaUnreconClosedExport(MyUnreconciliationClose_list values, string employee_gid)
        {

            string bank_gid, cr_dr, knockoff_status, amount_greater, amount_lesser, trns_date;

            bank_gid = values.bank_gid;
            cr_dr = values.cr_dr;
            knockoff_status = values.knockoff_status;
            amount_greater = values.amount_greater;
            amount_lesser = values.amount_lesser;
            trns_date = values.trns_date;
            if (amount_greater == null || amount_greater == "")
            {
                amount_greater = "0";
            }
            else
            {

                amount_greater = (values.amount_greater).Replace(",", "");

            }
            if (amount_lesser == null || amount_lesser == "")
            {
                amount_lesser = "0";
            }
            else
            {

                amount_lesser = (values.amount_lesser).Replace(",", "");
            }
            msSQL = "call brs_rpt_spmyunreconclosedreport(" + "'" + values.bank_gid + "'" + "," + "'" + values.cr_dr + "'" + "," + "'" + values.knockoff_status + "'" + "," + "" + amount_greater + "" + "," + "" + amount_lesser + "" + "," + "'" + values.trns_date + "'" + ")";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Unreconciliation Closed Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                //values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "OSD/AllTicket/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "BRS/UnreconciliationReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

                {
                    if ((!System.IO.Directory.Exists(values.lspath)))
                        System.IO.Directory.CreateDirectory(values.lspath);
                }

                values.lsname = "Unreconciliation_Closed_Report" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".xlsx";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "BRS/UnreconciliationReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                values.lscloudpath = lscompany_code + "/" + "BRS/UnreconciliationReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                // values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "OSD/AllTicket/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                //bool status;
                //status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "OSD/AllTicket/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname,ms);
                //ms.Close();
                //values.lspath = "erpdocument" + "/" + lscompany_code + "/" + "OSD/AllTicket/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 46])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "BRS/UnreconciliationReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname, ms);
                values.lspath = objcmnstorage.EncryptData(ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "BRS/UnreconciliationReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname);
                values.lscloudpath = objcmnstorage.EncryptData(lscompany_code + "/" + "BRS/UnreconciliationReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname);

                ms.Close();
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
        }
        public void DaGetMyUnreconReportView(string banktransc_gid, transaction_list values)
        {

            try
            {
                msSQL = "call brs_rpt_spmyunreconpendingsummaryview(" + "'" + banktransc_gid + "'" + ")";

                //msSQL = " SELECT a.banktransc_gid,a.banktransc_refno,c.allocated_status,c.rm_status,c.rm_remarks, a.bankconfig_gid,a.bank_gid,b.bank_name,b.branch_name,b.acc_no,b.custref_no,a.reconcildoc_gid,DATE_FORMAT(a.trn_date, '%d-%b-%Y %h:%i %p') as trn_date, DATE_FORMAT(a.value_date, '%d-%b-%Y %h:%i %p') as value_date , " +
                //        " DATE_FORMAT(a.payment_date, '%d-%b-%Y %h:%i %p') as payment_date , a.transact_particulars, a.debit_amt, a.credit_amt,FORMAT(a.transact_val,2,'en_IN') as transact_val, a.remarks, a.cr_dr, a.chq_no, a.created_by,  DATE_FORMAT(a.created_date, '%d-%b-%Y %h:%i %p') as created_date ,  " +
                //        " d.bankrepaytransc_gid, d.repayreconcildoc_gid, d.account_number, d.transaction_date as repay_transaction_date, d.repayment_date, d.transaction_id,  " +
                //        " d.repayment_amount, d.principal, d.normal_interest, d.forfeiture_waiver, d.remarks, d.repayment_type, d.penal_interest,  " +
                //        " d.instrument, d.old_dpd, d.dpd, d.account_code, d.interest_tds, d.penal_interest_tds, d.knockoff_flag, d.knockoff_status as repay_knockoff_status  " +
                //        " ,a.transc_balance,baselocation_name FROM brs_trn_tbanktransactiondetails a  " +
                //        " left join brs_mst_tbank b  on a.bank_gid = b.bank_gid  " +
                //        " left join osd_trn_tbankalert2allocated c on a.banktransc_gid = c.ticketref_no  " +
                //        " left join adm_mst_tuser u on a.created_by = u.user_gid  " +
                //        " left join hrm_mst_temployee e on u.user_gid = e.user_gid  " +
                //        " LEFT JOIN brs_mst_trepaymenttransaction d  ON a.bankrepaytransc_gid = d.bankrepaytransc_gid " +
                //        " left join sys_mst_tbaselocation f on f.baselocation_gid=e.baselocation_gid  " +
                //        " where  a.banktransc_gid='" + banktransc_gid + "'  order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
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
                    values.bankrepaytransc_gid = objODBCDatareader["bankrepaytransc_gid"].ToString();
                    values.account_number = objODBCDatareader["account_number"].ToString();
                    values.repay_transaction_date = objODBCDatareader["repay_transaction_date"].ToString();
                    values.repayment_date = objODBCDatareader["repayment_date"].ToString();
                    values.repayment_type = objODBCDatareader["repayment_type"].ToString();
                    values.penal_interest = objODBCDatareader["penal_interest"].ToString();
                    values.repayment_amount = objODBCDatareader["repayment_amount"].ToString();
                    values.principal = objODBCDatareader["principal"].ToString();
                    values.normal_interest = objODBCDatareader["normal_interest"].ToString();
                    values.forfeiture_waiver = objODBCDatareader["forfeiture_waiver"].ToString();
                    values.repay_remarks = objODBCDatareader["remarks"].ToString();
                    values.instrument = objODBCDatareader["instrument"].ToString();                   
                    values.old_dpd = objODBCDatareader["old_dpd"].ToString();
                    values.dpd = objODBCDatareader["dpd"].ToString();
                    values.account_code = objODBCDatareader["account_code"].ToString();
                    values.interest_tds = objODBCDatareader["interest_tds"].ToString();
                    values.instrument = objODBCDatareader["instrument"].ToString();                  
                    values.penal_interest_tds = objODBCDatareader["penal_interest_tds"].ToString();
                    values.knockoff_flag = objODBCDatareader["knockoff_flag"].ToString();
                    values.repay_knockoff_status = objODBCDatareader["repay_knockoff_status"].ToString();
                    values.allocated_status = objODBCDatareader["allocated_status"].ToString();
                    values.rm_status = objODBCDatareader["rm_status"].ToString();
                    values.rm_remarks = objODBCDatareader["rm_remarks"].ToString();
                    values.baselocation_name = objODBCDatareader["baselocation_name"].ToString();
                    values.manualknockoff_remarks = objODBCDatareader["manualknockoff_remarks"].ToString();
                    values.knockoff_status = objODBCDatareader["knockoff_status"].ToString();

                }
                objODBCDatareader.Close();
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }

        }
        public void DaGetAssignedHistoryView(MdlMyUnreconciliationManagement values, string employee_gid, string banktransc_gid)
        {
            try
            {

                msSQL = " select tagemployee_gid,banktransc_gid,taggedmember_gid,taggedmember_name,tagged_remarks, DATE_FORMAT(a.tagged_date, '%d-%b-%Y %h:%i %p') as tagged_date, " +
                        " DATE_FORMAT(a.created_date, '%d-%b-%Y %h:%i %p') as created_date, concat(g.user_firstname,' ',g.user_lastname,' / ',g.user_code) as assigned_by from brs_trn_ttagemployee a " +
                        " left join adm_mst_tuser g on a.created_by= g.user_gid " +
                        " where a.banktransc_gid='" + banktransc_gid + "' order by a.created_date desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gettransactionlist = new List<assignedview_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        gettransactionlist.Add(new assignedview_list
                        {
                            banktransc_gid = (dr_datarow["banktransc_gid"].ToString()),
                            taggedmember_name = (dr_datarow["taggedmember_name"].ToString()),
                            tagged_remarks = (dr_datarow["tagged_remarks"].ToString()),
                            tagged_date = (dr_datarow["tagged_date"].ToString()),
                            taggedmember_gid = (dr_datarow["taggedmember_gid"].ToString()),
                            assigned_by = (dr_datarow["assigned_by"].ToString())

                        });
                    }
                    values.assignedview_list = gettransactionlist;
                }
                dt_datatable.Dispose();

                values.status = true;

            }
            catch
            {
                values.status = false;
            }

        }
        public void DaGetTransferredHistoryView(MdlMyUnreconciliationManagement values, string employee_gid, string banktransc_gid)
        {
            try
            {


                msSQL = " select tagemployee_gid,banktransc_gid,transfermember_gid,transfermember_name,transfer_remarks, DATE_FORMAT(a.transfer_date, '%d-%b-%Y %h:%i %p') as transfer_date, " +
                     " DATE_FORMAT(a.created_date, '%d-%b-%Y %h:%i %p') as created_date, concat(g.user_firstname,' ',g.user_lastname,' / ',g.user_code) as transfer_by from brs_trn_ttransferemployee a " +
                     " left join adm_mst_tuser g on a.created_by= g.user_gid " +
                         " where a.banktransc_gid='" + banktransc_gid + "' order by a.created_date desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gettransactionlist = new List<transferview_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        gettransactionlist.Add(new transferview_list
                        {
                            banktransc_gid = (dr_datarow["banktransc_gid"].ToString()),
                            transfer_toname = (dr_datarow["transfermember_name"].ToString()),
                            transfer_reason = (dr_datarow["transfer_remarks"].ToString()),
                            transfered_date = (dr_datarow["transfer_date"].ToString()),
                            transfer_to = (dr_datarow["transfermember_gid"].ToString()),
                            transfer_by = (dr_datarow["transfer_by"].ToString()),

                        });
                    }
                    values.transferview_list = gettransactionlist;
                }
                dt_datatable.Dispose();

                values.status = true;

            }
            catch
            {
                values.status = false;
            }

        }
        public void DaExportAllocatedPendingReport(unreconallocatependingrpt_list values)
        {
            msSQL = "call brs_rpt_spallocatedpendingreport";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            dt_datatable.Columns.Remove("bankalert2allocated_gid");

            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);

            var workSheet = excel.Workbook.Worksheets.Add("AllocatedPending_Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                //values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "OSD/AllTicket/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "BRS/AllocatedPendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

                {
                    if ((!System.IO.Directory.Exists(values.lspath)))
                        System.IO.Directory.CreateDirectory(values.lspath);
                }

                values.lsname = "AllocatedPending_Report" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".xlsx";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "BRS/AllocatedPendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                values.lscloudpath = lscompany_code + "/" + "BRS/AllocatedPendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                //lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                //values.lsname = "AllocatedPending_Report.xlsx";
                ////var path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "Osd/BankAlertReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                ////objExportBankReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "Osd/BankAlertReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objExportBankReport.lsname;
                //var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "BRS/AllocatedPendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                //values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "BRS/AllocatedPendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                //values.lscloudpath = lscompany_code + "/" + "BRS/AllocatedPendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                bool exists = System.IO.Directory.Exists(values.lspath);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(values.lspath);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 19])  //Address "A1:A19"

                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);

                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "BRS/AllocatedPendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname, ms);
                values.lspath = objcmnstorage.EncryptData(ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "BRS/AllocatedPendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname);
                values.lscloudpath = objcmnstorage.EncryptData(lscompany_code + "/" + "BRS/AllocatedPendingReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname);

                ms.Close();
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
            values.status = true;
            values.message = "Success";
        }
        public void DaGetAllocatedPendingReportSummary(MdlMyUnreconciliationManagement values,string user_gid)
        {

            msSQL = "call brs_rpt_spalloactedpendingreportsummary";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var objunreconallocaterpt_list = new List<unreconallocatependingrpt_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    objunreconallocaterpt_list.Add(new unreconallocatependingrpt_list
                    {
                        department_name = dr_datarow["department_name"].ToString(),
                        ticketref_no = dr_datarow["ticketref_no"].ToString(),
                        created_date = dr_datarow["created_date"].ToString(),
                        allocated_status = dr_datarow["allocated_status"].ToString(),
                        operation_status = dr_datarow["operation_status"].ToString(),
                        customer_name = dr_datarow["customer_name"].ToString(),
                        assigned_toname = dr_datarow["assigned_toname"].ToString(),
                        assigned_date = dr_datarow["assigned_date"].ToString(),
                        operationstatus_updated_date = dr_datarow["operationstatus_updated_date"].ToString(),
                        //source = dr_datarow["source"].ToString(),

                    });
                }
                values.unreconallocaterpt_list = objunreconallocaterpt_list;
            }
            dt_datatable.Dispose();
            values.status = true;
            values.message = "Success";
        }

    }
}
