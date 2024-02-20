using ems.brs.Models;
using ems.storage.Functions;
using ems.utilities.Functions;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Http.Results;
using System.Web.Util;
using static OfficeOpenXml.ExcelErrorValue;

namespace ems.brs.Dataacess
{
    public class DaRepaymentReconcillation
    {
        string banktransc_gid;
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable, dt_datatable1;
        string msSQL, msGetGid, msGetGid1, msGetGidUR, lsbrs_gid, lsbanktransac_name, msGetGid2, lsbanktrans_gid, lscredit_amt, lscr_dr, lsbanktransc_gid, lsbanktransc_refno, lsknockoff_status, lsremainingamount_status;
        OdbcDataReader objODBCDatareader;
        dbconn objdbconn = new dbconn();
        HttpPostedFile httpPostedFile;
        decimal remaining_amount, lstotalamount, lsremainingamount;
        string lspath, endRange, excelRange, lstransact_particulars,lsremarks;
        double sumof_remainingamount;
        int mnresult;
        int lscount,null_count;
        int lsdata;
        int AccountNumbercolumn,UrnNocolumn, RepaymentDatecolumn, TransactionDatecolumn, TransactionIDcolumn,
            RepaymentAmountcolumn, Principalcolumn, NormalInterestcolumn, PenalInterestcolumn,
            ForfeitureWaivercolumn, UserIDcolumn, Instrumentcolumn, RepaymentTypecolumn,
            OldDPDcolumn, DPDcolumn, AccountCodecolumn, REMARKScolumn, PenalInterestTDScolumn, InterestTDScolumn;
        int rowCount, columnCount;
        //ExcelPackage xlPackage = new ExcelPackage();
        ExcelPackage xlPackage;
        string lsconverted_date;
        int lscredcount = 0;
        ExcelWorksheet worksheet;
        string FileExtension;
        string lsfilepath, lsfile_gid;
        public void DaPostBRSRepaymentExcelSample(HttpRequest httpRequest, MdlRepaymentReconcillation objfilename, string employee_gid, string user_gid)
        {

            upload_list2 objdocumentmodel = new upload_list2();
            HttpFileCollection httpFileCollection;
            DataTable dt = null;
             lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;           
            MemoryStream msxlsx = new MemoryStream();
            MemoryStream mscsvxlsx = new MemoryStream();
            FileStream filexlsx;
            FileStream filexlsx1;
            string AccountNumber, RepaymentDate,UrnNo, TransactionDate,
                TransactionID, RepaymentAmount, Principal, NormalInterest,
                PenalInterest, ForfeitureWaiver, UserID, Instrument, RepaymentType, OldDPD,
                DPD, AccountCode, InterestTDS, REMARKS, PenalInterestTDS;
            String path = lspath;


            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = HttpContext.Current.Server.MapPath("/erpdocument" + "/" + lscompany_code + "/BRS/Repaymentreconcildoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;
                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {
                        string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                        httpPostedFile = httpFileCollection[i];
                         FileExtension = httpPostedFile.FileName;
                         lsfile_gid = msdocument_gid;
                        string lsdocument_title = httpRequest.Form["file_name"].ToString();
                        string project_flag = httpRequest.Form["project_flag"].ToString();
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        Stream ls_readStream;
                        ls_readStream = httpPostedFile.InputStream;
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);
                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File Format Not Supported..!";
                            return;
                        }

                        //path creation        
                        lsfilepath = path + "/";
                        FileStream file = new FileStream(lsfilepath + lsfile_gid, FileMode.Create, FileAccess.Write);
                        ms.WriteTo(file);

                        if (FileExtension == ".xls")
                        {
                            Workbook workbook = new Workbook();

                            //workbook.LoadFromFile(lsfilepath + lsfile_gid);
                            workbook.LoadFromFile(Path.Combine(lsfilepath, lsfile_gid));
                            workbook.SaveToFile(Path.Combine(lsfilepath, lsfile_gid + ".xlsx"), ExcelVersion.Version2007);
                            string filePath = Path.Combine(lsfilepath, lsfile_gid + ".xlsx");
                            string sheetNameToDelete = "Evaluation Warning"; // Replace with the actual sheet name to delete

                            using (var package = new ExcelPackage(new FileInfo(filePath)))
                            {
                                // Get the sheet by name
                                var sheetToDelete = package.Workbook.Worksheets[sheetNameToDelete];

                                if (sheetToDelete != null)
                                {
                                    package.Workbook.Worksheets.Delete(sheetToDelete); // Delete the sheet
                                }

                                package.Save(); // Save the modified workbook
                            }
                            // workbook.SaveToStream(outputStream);

                            using (filexlsx = new FileStream(Path.Combine(lsfilepath, lsfile_gid + ".xlsx"), FileMode.Open, FileAccess.Read))
                            {
                                byte[] bytesxlsx = new byte[filexlsx.Length];
                                filexlsx.Read(bytes, 0, (int)filexlsx.Length);
                                msxlsx.Write(bytes, 0, (int)filexlsx.Length);
                            }

                            ExcelPackage xlPackageXlsx = new ExcelPackage(msxlsx);
                            worksheet = xlPackageXlsx.Workbook.Worksheets[1];
                            rowCount = worksheet.Dimension.End.Row;
                            columnCount = worksheet.Dimension.End.Column;
                            endRange = worksheet.Dimension.End.Address;
                            filexlsx.Close();
                            string newpath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "BRS/Extensionfiles/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
                            {
                                if ((!System.IO.Directory.Exists(path)))
                                    System.IO.Directory.CreateDirectory(path);
                            }
                            workbook.SaveToFile(Path.Combine(newpath, lsfile_gid + ".xlsx"), ExcelVersion.Version2007);
                        }
                        else if (FileExtension == ".xlsx")
                        {
                            xlPackage = new ExcelPackage(ms);
                            //using (ExcelPackage xlPackage = new ExcelPackage(ms))
                            //{
                            worksheet = xlPackage.Workbook.Worksheets[1];
                            rowCount = worksheet.Dimension.End.Row;
                            columnCount = worksheet.Dimension.End.Column;
                            endRange = worksheet.Dimension.End.Address;
                        }
                        else if (FileExtension == ".csv")
                        {
                            file.Close();
                            ms.Close();

                            Workbook workbook = new Workbook();
                            workbook.LoadFromFile(Path.Combine(lsfilepath, lsfile_gid), ",", 1, 1);
                            workbook.SaveToFile(Path.Combine(lsfilepath, lsfile_gid + ".xlsx"), ExcelVersion.Version2016);
                            using (filexlsx1 = new FileStream(Path.Combine(lsfilepath, lsfile_gid + ".xlsx"), FileMode.Open, FileAccess.Read))
                            {
                                //byte[] bytesxlsx = new byte[filexlsx1.Length];
                                //filexlsx1.Read(bytes, 0, (int)filexlsx1.Length);
                                //mscsvxlsx.Write(bytes, 0, (int)filexlsx1.Length);

                                byte[] buffer = new byte[1024]; // You can adjust the buffer size

                                int bytesRead;
                                while ((bytesRead = filexlsx1.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    mscsvxlsx.Write(buffer, 0, bytesRead);
                                }
                            }
                            string filePath = Path.Combine(lsfilepath, lsfile_gid + ".xlsx");
                            string sheetNameToDelete = "Evaluation Warning"; // Replace with the actual sheet name to delete

                            using (var package = new ExcelPackage(new FileInfo(filePath)))
                            {
                                // Get the sheet by name
                                var sheetToDelete = package.Workbook.Worksheets[sheetNameToDelete];

                                if (sheetToDelete != null)
                                {
                                    package.Workbook.Worksheets.Delete(sheetToDelete); // Delete the sheet
                                }

                                package.Save(); // Save the modified workbook
                            }
                            ExcelPackage xlPackageXlsx = new ExcelPackage(mscsvxlsx);
                            worksheet = xlPackageXlsx.Workbook.Worksheets[1];
                            rowCount = worksheet.Dimension.End.Row;
                            columnCount = worksheet.Dimension.End.Column;
                            endRange = worksheet.Dimension.End.Address;

                            filexlsx1.Close();
                            string newpath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "BRS/Extensionfiles/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
                            {
                                if ((!System.IO.Directory.Exists(path)))
                                    System.IO.Directory.CreateDirectory(path);
                            }
                            workbook.SaveToFile(Path.Combine(newpath, lsfile_gid + ".xlsx"), ExcelVersion.Version2016);
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "BRS/Repaymentreconcildoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);

                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "BRS/Repaymentreconcildoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        file.Close();
                        ms.Close();
                        msxlsx.Close();
                        mscsvxlsx.Close();
                        objcmnfunctions.uploadFile(lsfilepath, lsfile_gid);
                        msGetGid = objcmnfunctions.GetMasterGID("BRRD");
                        msSQL = " insert into brs_trn_trepayreconcillationdocument(" +
                                 " repayreconcildoc_gid," +
                                 " file_name," +
                                 " file_path ," +
                                 " created_by," +
                                 " created_date" +
                                 " )values(" +
                                 "'" + msGetGid + "'," +
                                 "'" + lsdocument_title.Replace("'", @"\'") + "'," +
                                 "'" + lspath + msdocument_gid + FileExtension + "'," +
                                 "'" + user_gid + "'," +
                                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnresult != 0)
                        {
                            try
                            {
                                if (FileExtension == ".xlsx")
                                {
                                    path = @"" + path.Replace("/", "\\") + "\\" + lsfile_gid + "";
                                    excelRange = "A1:" + endRange.ToString();
                                    dt = objcmnfunctions.ExcelToDataTable(path, excelRange);
                                    dt = dt.Rows.Cast<DataRow>().Where(r => string.Join("", r.ItemArray) != string.Empty).CopyToDataTable();
                                }
                                else if (FileExtension == ".xls")
                                {
                                    string filePathxls = Path.Combine(lsfilepath, lsfile_gid + ".xlsx");
                                    path = @"" + filePathxls.Replace("/", "\\");                                   
                                    dt = objcmnfunctions.ExcelToDataTable(path, excelRange);
                                    dt = dt.Rows.Cast<DataRow>().Where(r => string.Join("", r.ItemArray) != string.Empty).CopyToDataTable();
                                }
                                else if (FileExtension == ".csv")
                                {
                                    string filePathcsv = Path.Combine(lsfilepath, lsfile_gid + ".xlsx");
                                    path = @"" + filePathcsv.Replace("/", "\\");
                                    excelRange = "A1:" + endRange.ToString();
                                    dt = objcmnfunctions.ExcelToDataTable(path, excelRange);
                                    dt = dt.Rows.Cast<DataRow>().Where(r => string.Join("", r.ItemArray) != string.Empty).CopyToDataTable();
                                }
                            }
                            catch (Exception ex)
                            {
                                objfilename.status = false;
                                objfilename.message = ex.ToString();

                            }
                            char[] charsToReplace = { '*', ' ', '/', '@', '$', '#', '!', '^', '%', '(', ')', '\'' };

                            // Get the header names from the CSV file
                            List<string> headers = dt.Columns.Cast<DataColumn>().Select(column =>
                                string.Join("", column.ColumnName.Split(charsToReplace, StringSplitOptions.RemoveEmptyEntries))
                                    .ToLower()).ToList();
                            if (dt.Rows.Count == 0)
                            {
                                objfilename.message = "No data found ";
                                objfilename.status = false;
                                return;
                            }
                            // Identify the positions of the desired columns
                            AccountNumbercolumn = headers.IndexOf("accountnumber");
                            UrnNocolumn = headers.IndexOf("urnno");
                            RepaymentDatecolumn = headers.IndexOf("repaymentdate");
                            TransactionDatecolumn = headers.IndexOf("transactiondate");
                            TransactionIDcolumn = headers.IndexOf("transactionid");
                            RepaymentAmountcolumn = headers.IndexOf("repaymentamount");
                            Principalcolumn = headers.IndexOf("principal");
                            NormalInterestcolumn = headers.IndexOf("normalinterest");
                            PenalInterestcolumn = headers.IndexOf("penalinterest");
                            ForfeitureWaivercolumn = headers.IndexOf("forfeiturewaiver");
                            if (headers.IndexOf("userid") != -1)
                                UserIDcolumn = headers.IndexOf("userid");
                            if (headers.IndexOf("user") != -1)
                                UserIDcolumn = headers.IndexOf("user");
                            Instrumentcolumn = headers.IndexOf("instrument");
                            RepaymentTypecolumn = headers.IndexOf("repaymenttype");
                            OldDPDcolumn = headers.IndexOf("olddpd");
                            AccountCodecolumn = headers.IndexOf("accountcode");
                            InterestTDScolumn = headers.IndexOf("interesttds");
                            REMARKScolumn = headers.IndexOf("remarks");
                            PenalInterestTDScolumn = headers.IndexOf("penalinteresttds");


                            // Create the SQL query dynamically based on the identified column positions
                            msSQL = $@" INSERT INTO brs_tmp_trepayment (account_number,urn_no, transaction_date, repayment_date, transaction_id,repayment_amount, " +
                                    " principal,normal_interest,forfeiture_waiver,user_id,repayment_type,penal_interest,old_dpd, " +
                                    " instrument,dpd,account_code,interest_tds,remarks,penal_interest_tds,repayreconcildoc_gid, " +
                                    " created_by,created_date) VALUES ";
                            List<string> valueRows = new List<string>();
                            foreach (DataRow row in dt.Rows)
                            {
                                // Retrieve values from the desired columns

                                AccountNumber = row[AccountNumbercolumn].ToString();
                                UrnNo = row[UrnNocolumn].ToString();
                                RepaymentDate = row[RepaymentDatecolumn].ToString();
                                TransactionDate = row[TransactionDatecolumn].ToString();
                                TransactionID = row[TransactionIDcolumn].ToString().Replace("'", "");
                                RepaymentAmount = row[RepaymentAmountcolumn].ToString();
                                Principal = row[Principalcolumn].ToString();
                                NormalInterest = row[NormalInterestcolumn].ToString();
                                PenalInterest = row[PenalInterestcolumn].ToString();
                                ForfeitureWaiver = row[ForfeitureWaivercolumn].ToString();
                                UserID = row[UserIDcolumn].ToString();
                                Instrument = row[Instrumentcolumn].ToString();
                                RepaymentType = row[RepaymentTypecolumn].ToString();
                                OldDPD = row[OldDPDcolumn].ToString();
                                DPD = row[DPDcolumn].ToString();
                                AccountCode = row[AccountCodecolumn].ToString();
                                InterestTDS = row[InterestTDScolumn].ToString();
                                REMARKS = row[REMARKScolumn].ToString().Trim().Replace("'", "");
                                PenalInterestTDS = row[PenalInterestTDScolumn].ToString();

                                // Add values to the list of rows for the SQL query
                                if (!string.IsNullOrWhiteSpace(REMARKS))
                                {
                                    string lstrn_date = objcmnfunctions.GetDateFormat(TransactionDate);
                                    string lsrepayment_date = objcmnfunctions.GetDateFormat(RepaymentDate);
                                    valueRows.Add($"('{AccountNumber}',{UrnNo}, '{lstrn_date}', '{lsrepayment_date}','{TransactionID}','{RepaymentAmount}','{Principal}','{NormalInterest}','{ForfeitureWaiver}','{UserID}','{RepaymentType}','{PenalInterest}','{OldDPD}','{Instrument}','{DPD}', '{AccountCode}','{InterestTDS}','{REMARKS}','{PenalInterestTDS}','{msGetGid}','{user_gid}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");

                                }
                                AccountNumber = string.Empty;
                                UrnNo = string.Empty;
                                TransactionDate = string.Empty;
                                RepaymentDate = string.Empty;
                                TransactionID = string.Empty;
                                RepaymentAmount = string.Empty;
                                Principal = string.Empty;
                                NormalInterest = string.Empty;
                                ForfeitureWaiver = string.Empty;
                                UserID = string.Empty;
                                RepaymentType = string.Empty;
                                PenalInterest = string.Empty;
                                OldDPD = string.Empty;
                                Instrument = string.Empty;
                                DPD = string.Empty;
                                AccountCode = string.Empty;
                                InterestTDS = string.Empty;
                                REMARKS = string.Empty;
                                PenalInterestTDS = string.Empty;
                            }

                            // Combine the rows into a single SQL query
                            msSQL += string.Join(", ", valueRows);
                            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            if (mnresult == 1)
                            {
                                objfilename.status = true;
                                objfilename.message = "LMS Repayment imported successfully..!";
                            }
                            else
                            {
                                objfilename.status = false;
                                objfilename.message = "Error occured..!";
                            }
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error occured..!";
                        }
                    }
                    if (FileExtension == ".xls" || FileExtension == ".csv")
                    {
                        string filePathToDelete = Path.Combine(lsfilepath, lsfile_gid + ".xlsx");
                        File.Delete(filePathToDelete);
                    }

                }
            }
            catch (Exception ex)
            {
                objfilename.status = false;
                objfilename.message = ex.ToString();
            }
        }
        public void DaGetRepaymentPendingSummary(repaymentlist values, string user_gid)
        {
            try
            {


                msSQL = "call brs_trn_spbrsrepaymentpendingsummary";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    string json = DataTableToJson(dt_datatable);
                    values.JSONdata = json;
                }
                dt_datatable.Dispose();

                values.status = true;
                //values.offsetlimit = limitoffset_from;
            }
            catch
            {
                values.status = false;
            }

        }
        public void DaGetRepaymentMatchedSummary(repaymentlist values, string user_gid)
        {
            try
            {

                //msSQL = " select a.bankrepaytransc_gid, a.repayreconcildoc_gid, a.account_number,DATE_FORMAT(a.transaction_date, '%d-%m-%Y %h:%i %p') as transaction_date,DATE_FORMAT(a.repayment_date, '%d-%m-%Y %h:%i %p') as repayment_date,a.transaction_id,FORMAT(a.repayment_amount,2,'en_IN') as repayment_amount ,FORMAT(a.principal,2,'en_IN') as principal,FORMAT(a.normal_interest,2,'en_IN') as normal_interest,a.forfeiture_waiver, " +
                //" a.remarks,a.user_id,a.repayment_type,a.penal_interest,a.instrument,a.old_dpd,dpd,a.account_code,a.interest_tds,a.penal_interest_tds,DATE_FORMAT(a.created_date, '%d-%m-%Y %h:%i %p') as created_date,a.updated_by,DATE_FORMAT(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date, " +
                //" a.knockoff_flag,a.tagged_status, a.knockoff_status,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by from brs_mst_trepaymenttransaction a " +
                //" left join adm_mst_tuser c on a.created_by= c.user_gid " +
                //" order by a.created_date desc ";
                msSQL = "call brs_trn_spbrsrepaymentmatchedsummary";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    string json = DataTableToJson(dt_datatable);
                    values.JSONdata = json;
                }
                dt_datatable.Dispose();

                values.status = true;
                //values.offsetlimit = limitoffset_from;
                

            }
            catch
            {
                values.status = false;
            }

        }

        public void DaGetRepaymentSummaryCounts(string employee_gid, repaymentlist values)
        {
            msSQL = "select (select count(a.bankrepaytransc_gid) from brs_mst_trepaymenttransaction a where a.knockoff_status = 'Pending'" +
                    " order by a.created_date desc)  AS repaymentpending_count, " +
            " (select count(a.bankrepaytransc_gid) from brs_mst_trepaymenttransaction a where a.knockoff_status = 'Matched'" +
            " order by a.created_date desc) As repaymentmatched_count ";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.repaymentpending_count = objODBCDatareader["repaymentpending_count"].ToString();
                values.repaymentmatched_count = objODBCDatareader["repaymentmatched_count"].ToString();

            }
            objODBCDatareader.Close();
        }

        public static string DataTableToJson(DataTable dt)
        { // Convert DataTable to JSON using JSON.NET
            string json = JsonConvert.SerializeObject(dt);
            return json;
        }
        public void DaGetRepaymentTemplateSummary(uploadlist values, string user_gid)
        {
            try
            {

                msSQL = "call brs_mst_splmstemplatesummary";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getuploadlist = new List<uploadlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getuploadlist.Add(new uploadlist
                        {
                            repayreconcildoc_gid = (dr_datarow["repayreconcildoc_gid"].ToString()),
                            file_name = (dr_datarow["file_name"].ToString()),
                            file_path = objcmnstorage.EncryptData((dr_datarow["file_path"].ToString())),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            Status = (dr_datarow["status"].ToString()),
                            norepayment_flag = (dr_datarow["norepayment_flag"].ToString()),
                            pending_count = (dr_datarow["pending_count"].ToString()),
                            closed_count = (dr_datarow["closed_count"].ToString()),
                            total_count = (dr_datarow["total_count"].ToString()),

                        });
                    }
                    values.uploadtemplatelist = getuploadlist;
                }
                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }

        //public void DaGetreConcillationSummary(cocillationlist values, string user_gid)
        //{
        //    try
        //    {

        //        msSQL = " SELECT a.knockoff_status,a.banktransc_refno,a.banktransc_gid,a.transact_particulars," +
        //            "a.transact_val,a.knockoff_flag,a.cr_dr,a.trn_date,b.bank_name,DATE_FORMAT(a.knockoff_date, '%d-%m-%Y %h:%i %p') as knockoff_date,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by FROM brs_trn_tbanktransactiondetails  a  " +
        //             " left join brs_mst_tbank b  on a.bank_gid =b.bank_gid  " +
        //             " left join adm_mst_tuser c on a.created_by= c.user_gid " +
        //                " where  ((a.cr_dr ='CR' and  knockoff_status='Matched') or  (a.cr_dr ='CR' and knockoff_status='AssignMatched')  or (a.cr_dr ='CR' and  knockoff_status='ManuallyMatched')) order by a.created_date desc ";

        //        dt_datatable = objdbconn.GetDataTable(msSQL);
        //        var getcocillationcreditlist = new List<cocillationlist>();
        //        if (dt_datatable.Rows.Count != 0)
        //        {
        //            foreach (DataRow dr_datarow in dt_datatable.Rows)
        //            {
        //                getcocillationcreditlist.Add(new cocillationlist
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
        //                    created_by = (dr_datarow["created_by"].ToString()),
        //                    knockoff_date = (dr_datarow["knockoff_date"].ToString()),

        //                });
        //            }
        //            values.cocillationcredit_list = getcocillationcreditlist;
        //        }
        //        msSQL = " SELECT a.knockoff_status,a.banktransc_refno,a.banktransc_gid,a.transact_particulars,a.transact_val,a.knockoff_flag," +
        //            "a.cr_dr,a.trn_date,b.bank_name,DATE_FORMAT(a.knockoff_date, '%d-%m-%Y %h:%i %p') as knockoff_date,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by FROM brs_trn_tbanktransactiondetails  a  " +
        //              " left join brs_mst_tbank b  on a.bank_gid =b.bank_gid  " +
        //               " left join adm_mst_tuser c on a.created_by= c.user_gid " +
        //                 " where  ((a.cr_dr ='DR' and  knockoff_status='Matched') or  (a.cr_dr ='DR' and knockoff_status='AssignMatched')  or (a.cr_dr ='DR' and  knockoff_status='ManuallyMatched'))  order by a.created_date desc ";

        //        dt_datatable = objdbconn.GetDataTable(msSQL);
        //        var getcocillationdebitlist = new List<cocillationlist>();
        //        if (dt_datatable.Rows.Count != 0)
        //        {
        //            foreach (DataRow dr_datarow in dt_datatable.Rows)
        //            {
        //                getcocillationdebitlist.Add(new cocillationlist
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
        //                    created_by = (dr_datarow["created_by"].ToString()),
        //                    knockoff_date = (dr_datarow["knockoff_date"].ToString()),




        //                });
        //            }
        //            values.cocillationdebit_list = getcocillationdebitlist;
        //        }
        //        dt_datatable.Dispose();

        //        values.status = true;
        //    }
        //    catch
        //    {
        //        values.status = false;
        //    }

        //}
        public void DaGetCreditMatchedSummary(cocillationlist values, string user_gid)
        {
            try
            {

                msSQL = " SELECT a.knockoff_status,a.banktransc_refno,a.banktransc_gid,a.transact_particulars,a.bankrepaytransc_gid, " +
                    "a.transact_val,a.knockoff_flag,a.cr_dr,a.trn_date,b.bank_name,DATE_FORMAT(a.knockoff_date, '%d-%m-%Y %h:%i %p') as knockoff_date,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by FROM brs_trn_tbanktransactiondetails  a  " +
                     " left join brs_mst_tbank b  on a.bank_gid =b.bank_gid  " +
                     " left join adm_mst_tuser c on a.created_by= c.user_gid " +
                        " where a.cr_dr ='CR' and  a.knockoff_status='Matched'  group by a.banktransc_gid order by a.created_date desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcreditmatched_list = new List<cocillationlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcreditmatched_list.Add(new cocillationlist
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
                            created_by = (dr_datarow["created_by"].ToString()),
                            knockoff_date = (dr_datarow["knockoff_date"].ToString()),
                            bankrepaytransc_gid = (dr_datarow["bankrepaytransc_gid"].ToString()),
                        });
                    }
                    values.creditmatched_list = getcreditmatched_list;
                }

                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }
        public void DaGetDebitMatchedSummary(cocillationlist values, string user_gid)
        {
            try
            {

                msSQL = " SELECT a.knockoff_status,a.banktransc_refno,a.banktransc_gid,a.transact_particulars,a.bankrepaytransc_gid,  " +
                    "a.transact_val,a.knockoff_flag,a.cr_dr,a.trn_date,b.bank_name,DATE_FORMAT(a.knockoff_date, '%d-%m-%Y %h:%i %p') as knockoff_date,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by FROM brs_trn_tbanktransactiondetails  a  " +
                     " left join brs_mst_tbank b  on a.bank_gid =b.bank_gid  " +
                     " left join adm_mst_tuser c on a.created_by= c.user_gid " +
                        " where a.cr_dr ='DR' and  a.knockoff_status='Matched'  group by a.banktransc_gid  order by a.created_date desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdebitmatched_list = new List<cocillationlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getdebitmatched_list.Add(new cocillationlist
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
                            created_by = (dr_datarow["created_by"].ToString()),
                            knockoff_date = (dr_datarow["knockoff_date"].ToString()),
                            bankrepaytransc_gid = (dr_datarow["bankrepaytransc_gid"].ToString()),


                        });
                    }
                    values.Debitmatched_list = getdebitmatched_list;
                }

                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }
        public void DaGetCreditPartialMatchedSummary(cocillationlist values, string user_gid)
        {
            try
            {

                msSQL = " SELECT a.knockoff_status,a.banktransc_refno,a.banktransc_gid,a.transact_particulars, a.bankrepaytransc_gid, " +
                    "a.transact_val,a.knockoff_flag,a.cr_dr,a.trn_date,b.bank_name,DATE_FORMAT(a.knockoff_date, '%d-%m-%Y %h:%i %p') as knockoff_date,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by FROM brs_trn_tbanktransactiondetails  a  " +
                     " left join brs_mst_tbank b  on a.bank_gid =b.bank_gid  " +
                     " left join adm_mst_tuser c on a.created_by= c.user_gid " +
                        " where a.cr_dr ='CR' and  a.knockoff_status='PartiallyMatched'  group by a.banktransc_gid  order by a.created_date desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcreditpartialmatched_list = new List<cocillationlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcreditpartialmatched_list.Add(new cocillationlist
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
                            created_by = (dr_datarow["created_by"].ToString()),
                            knockoff_date = (dr_datarow["knockoff_date"].ToString()),
                            bankrepaytransc_gid = (dr_datarow["bankrepaytransc_gid"].ToString()),

                        });
                    }
                    values.creditpartialmatched_list = getcreditpartialmatched_list;
                }

                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }
        public void DaGetDebitPartialMatchedSummary(cocillationlist values, string user_gid)
        {
            try
            {

                msSQL = " SELECT a.knockoff_status,a.banktransc_refno,a.banktransc_gid,a.transact_particulars, a.bankrepaytransc_gid,  " +
                    "a.transact_val,a.knockoff_flag,a.cr_dr,a.trn_date,b.bank_name,DATE_FORMAT(a.knockoff_date, '%d-%m-%Y %h:%i %p') as knockoff_date,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by FROM brs_trn_tbanktransactiondetails  a  " +
                     " left join brs_mst_tbank b  on a.bank_gid =b.bank_gid  " +
                     " left join adm_mst_tuser c on a.created_by= c.user_gid " +
                        " where a.cr_dr ='DR' and  a.knockoff_status='PartiallyMatched'   group by a.banktransc_gid order by a.created_date desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdebitpartialmatched_list = new List<cocillationlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getdebitpartialmatched_list.Add(new cocillationlist
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
                            created_by = (dr_datarow["created_by"].ToString()),
                            knockoff_date = (dr_datarow["knockoff_date"].ToString()),
                            bankrepaytransc_gid = (dr_datarow["bankrepaytransc_gid"].ToString()),

                        });
                    }
                    values.debitpartialmatched_list = getdebitpartialmatched_list;
                }

                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }
        public void DaGetCreditUnmatchedUnassignedSummary(cocillationlist values, string user_gid)
        {
            try
            {

                msSQL = " SELECT a.knockoff_status,a.banktransc_refno,a.banktransc_gid,a.transact_particulars, a.tagged_status," +
                    "a.transact_val,a.knockoff_flag,a.cr_dr,a.trn_date,b.bank_name,DATE_FORMAT(a.knockoff_date, '%d-%m-%Y %h:%i %p') as knockoff_date,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by FROM brs_trn_tbanktransactiondetails  a  " +
                     " left join brs_mst_tbank b  on a.bank_gid =b.bank_gid  " +
                     " left join adm_mst_tuser c on a.created_by= c.user_gid " +
                        " where ((a.cr_dr ='CR' and  a.knockoff_status='Pending') and (a.cr_dr ='CR' and  a.tagged_status='Pending'))  group by a.banktransc_gid order by a.created_date desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcreditUnmatchedUnassigned_list = new List<cocillationlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcreditUnmatchedUnassigned_list.Add(new cocillationlist
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
                            created_by = (dr_datarow["created_by"].ToString()),
                            knockoff_date = (dr_datarow["knockoff_date"].ToString()),
                            tagged_status = (dr_datarow["tagged_status"].ToString())

                        });
                    }
                    values.creditUnmatchedUnassigned_list = getcreditUnmatchedUnassigned_list;
                }

                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }
        public void DaGetDebitUnmatchedUnassignedSummary(cocillationlist values, string user_gid)
        {
            try
            {

                msSQL = " SELECT a.knockoff_status,a.banktransc_refno,a.banktransc_gid,a.transact_particulars,a.tagged_status," +
                    "a.transact_val,a.knockoff_flag,a.cr_dr,a.trn_date,b.bank_name,DATE_FORMAT(a.knockoff_date, '%d-%m-%Y %h:%i %p') as knockoff_date,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by FROM brs_trn_tbanktransactiondetails  a  " +
                     " left join brs_mst_tbank b  on a.bank_gid =b.bank_gid  " +
                     " left join adm_mst_tuser c on a.created_by= c.user_gid " +
                        " where ((a.cr_dr ='DR' and  a.knockoff_status='Pending') and (a.cr_dr ='DR' and  a.tagged_status='Pending'))  group by a.banktransc_gid  order by a.created_date desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdebitUnmatchedUnassigned_list = new List<cocillationlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getdebitUnmatchedUnassigned_list.Add(new cocillationlist
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
                            created_by = (dr_datarow["created_by"].ToString()),
                            knockoff_date = (dr_datarow["knockoff_date"].ToString()),
                            tagged_status = (dr_datarow["tagged_status"].ToString())

                        });
                    }
                    values.debitUnmatchedUnassigned_list = getdebitUnmatchedUnassigned_list;
                }

                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }
        public void DaGetCreditUnmatchedassignedSummary(cocillationlist values, string user_gid)
        {
            try
            {

                msSQL = " SELECT a.knockoff_status,a.banktransc_refno,a.banktransc_gid,a.transact_particulars,a.tagged_status, " +
                    "a.transact_val,a.knockoff_flag,a.cr_dr,a.trn_date,b.bank_name,DATE_FORMAT(a.knockoff_date, '%d-%m-%Y %h:%i %p') as knockoff_date,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by FROM brs_trn_tbanktransactiondetails  a  " +
                     " left join brs_mst_tbank b  on a.bank_gid =b.bank_gid  " +
                     " left join adm_mst_tuser c on a.created_by= c.user_gid " +
                        " where ((a.cr_dr ='CR' and  a.tagged_status='Assigned') and (a.cr_dr ='CR' and  a.knockoff_status='Pending'))  group by a.banktransc_gid order by a.created_date desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcreditUnmatchedassigned_list = new List<cocillationlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcreditUnmatchedassigned_list.Add(new cocillationlist
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
                            created_by = (dr_datarow["created_by"].ToString()),
                            knockoff_date = (dr_datarow["knockoff_date"].ToString()),
                            tagged_status = (dr_datarow["tagged_status"].ToString())

                        });
                    }
                    values.creditunmatchedassigned_list = getcreditUnmatchedassigned_list;
                }

                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }
        public void DaGetDebitUnmatchedassignedSummary(cocillationlist values, string user_gid)
        {
            try
            {

                msSQL = " SELECT a.knockoff_status,a.banktransc_refno,a.banktransc_gid,a.transact_particulars,a.tagged_status," +
                    "a.transact_val,a.knockoff_flag,a.cr_dr,a.trn_date,b.bank_name,DATE_FORMAT(a.knockoff_date, '%d-%m-%Y %h:%i %p') as knockoff_date,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by FROM brs_trn_tbanktransactiondetails  a  " +
                     " left join brs_mst_tbank b  on a.bank_gid =b.bank_gid  " +
                     " left join adm_mst_tuser c on a.created_by= c.user_gid " +
                        " where ((a.cr_dr ='DR' and  a.tagged_status='Assigned') and (a.cr_dr ='DR' and  a.knockoff_status='Pending'))  group by a.banktransc_gid order by a.created_date desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdebitmatchedassigned_list = new List<cocillationlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getdebitmatchedassigned_list.Add(new cocillationlist
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
                            created_by = (dr_datarow["created_by"].ToString()),
                            knockoff_date = (dr_datarow["knockoff_date"].ToString()),
                            tagged_status = (dr_datarow["tagged_status"].ToString())
                        });
                    }
                    values.debitmatchedassigned_list = getdebitmatchedassigned_list;
                }

                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }
        public void DaGetCreditClosedSummary(cocillationlist values, string user_gid)
        {
            try
            {

                msSQL = " SELECT a.knockoff_status,a.banktransc_refno,a.banktransc_gid,a.transact_particulars,a.bankrepaytransc_gid , a.manualknockoff_remarks ,  " +
                    "a.transact_val,a.knockoff_flag,a.cr_dr,a.trn_date,b.bank_name,DATE_FORMAT(a.knockoff_date, '%d-%m-%Y %h:%i %p') as knockoff_date,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by FROM brs_trn_tbanktransactiondetails  a  " +
                     " left join brs_mst_tbank b  on a.bank_gid =b.bank_gid  " +
                     " left join adm_mst_tuser c on a.created_by= c.user_gid " +
                        " where ((a.cr_dr ='CR' and a.knockoff_status='AssignMatched') or (a.cr_dr ='CR' and a.knockoff_status='ManuallyMatched') ) group by a.banktransc_gid  order by a.created_date desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getCreditClosed_list = new List<cocillationlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getCreditClosed_list.Add(new cocillationlist
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
                            created_by = (dr_datarow["created_by"].ToString()),
                            knockoff_date = (dr_datarow["knockoff_date"].ToString()),
                            bankrepaytransc_gid = (dr_datarow["bankrepaytransc_gid"].ToString()),
                            manualknockoff_remarks = (dr_datarow["manualknockoff_remarks"].ToString()),

                        });
                    }
                    values.CreditClosed_list = getCreditClosed_list;
                }

                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }
        public void DaGetDebitClosedSummary(cocillationlist values, string user_gid)
        {
            try
            {

                msSQL = " SELECT a.knockoff_status,a.banktransc_refno,a.banktransc_gid,a.transact_particulars,a.bankrepaytransc_gid , a.manualknockoff_remarks , " +
                    "a.transact_val,a.knockoff_flag,a.cr_dr,a.trn_date,b.bank_name,DATE_FORMAT(a.knockoff_date, '%d-%m-%Y %h:%i %p') as knockoff_date,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by FROM brs_trn_tbanktransactiondetails  a  " +
                     " left join brs_mst_tbank b  on a.bank_gid =b.bank_gid  " +
                     " left join adm_mst_tuser c on a.created_by= c.user_gid " +
                        " where ((a.cr_dr ='DR' and  a.knockoff_status='AssignMatched') or (a.cr_dr ='DR' and  a.knockoff_status='ManuallyMatched'))  group by a.banktransc_gid order by a.created_date desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdebitClosed_list = new List<cocillationlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getdebitClosed_list.Add(new cocillationlist
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
                            created_by = (dr_datarow["created_by"].ToString()),
                            knockoff_date = (dr_datarow["knockoff_date"].ToString()),
                            bankrepaytransc_gid = (dr_datarow["bankrepaytransc_gid"].ToString()),
                            manualknockoff_remarks = (dr_datarow["manualknockoff_remarks"].ToString()),

                        });
                    }
                    values.debitClosed_list = getdebitClosed_list;
                }

                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }

        //public void DaGetunreConcillationSummary(cocillationlist values, string user_gid)
        //{
        //    try
        //    {

        //        msSQL = " SELECT a.knockoff_status,a.banktransc_refno,a.banktransc_gid,a.transact_particulars," +
        //            "a.transact_val,a.knockoff_flag,a.cr_dr,a.trn_date,b.bank_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by FROM brs_trn_tbanktransactiondetails  a  " +
        //             " left join brs_mst_tbank b  on a.bank_gid =b.bank_gid  " +
        //             " left join adm_mst_tuser c on a.created_by= c.user_gid " +
        //                " where  cr_dr ='CR' and a.knockoff_status='Pending' order by a.created_date desc ";

        //        dt_datatable = objdbconn.GetDataTable(msSQL);
        //        var getuncocillationcreditlist = new List<cocillationlist>();
        //        if (dt_datatable.Rows.Count != 0)
        //        {
        //            foreach (DataRow dr_datarow in dt_datatable.Rows)
        //            {
        //                getuncocillationcreditlist.Add(new cocillationlist
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
        //                    created_by = (dr_datarow["created_by"].ToString()),


        //                });
        //            }
        //            values.uncocillationcredit_list = getuncocillationcreditlist;
        //        }
        //        msSQL = " SELECT a.knockoff_status,a.banktransc_refno,a.banktransc_gid,a.transact_particulars," +
        //            "a.transact_val,a.knockoff_flag,a.cr_dr,a.trn_date,b.bank_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by FROM brs_trn_tbanktransactiondetails  a  " +
        //            " left join brs_mst_tbank b  on a.bank_gid =b.bank_gid  " +
        //            " left join adm_mst_tuser c on a.created_by= c.user_gid " +
        //               " where cr_dr ='DR' and a.knockoff_status='Pending' order by a.created_date desc ";

        //        dt_datatable = objdbconn.GetDataTable(msSQL);
        //        var getundebitcocillationlist = new List<cocillationlist>();
        //        if (dt_datatable.Rows.Count != 0)
        //        {
        //            foreach (DataRow dr_datarow in dt_datatable.Rows)
        //            {
        //                getundebitcocillationlist.Add(new cocillationlist
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
        //                    created_by = (dr_datarow["created_by"].ToString()),


        //                });
        //            }
        //            values.uncocillationdebit_list = getundebitcocillationlist;
        //        }
        //        dt_datatable.Dispose();

        //        values.status = true;
        //    }
        //    catch
        //    {
        //        values.status = false;
        //    }

        //}
        public void DaDeleteRepaymentdata(string bankrepaytransc_gid, repaymentlist values)
        {
            msSQL = "select count(*) as lsstatus from brs_mst_trepaymenttransaction " +
                      " where knockoff_status='Matched' and bankrepaytransc_gid='" + bankrepaytransc_gid + "'";
            int lsstatus = Convert.ToInt16(objdbconn.GetExecuteScalar(msSQL));
            if (lsstatus != 0)
            {
                values.message = "Repayment data is matched So..you can't delete it..!";
                values.status = true;
            }
            else
            {
                msSQL = "delete from brs_mst_trepaymenttransaction where bankrepaytransc_gid ='" + bankrepaytransc_gid + "'";
                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnresult != 0)
                {

                    values.message = "Transaction data deleted successfully";
                    values.status = true;
                }
                else
                {
                    values.message = "Error Occured While Deleting the transaction data";
                    values.status = false;

                }
            }
        }
        public void DaDeleteRepaymentTemplatedata(string repayreconcildoc_gid, transactionlist values)
        {
            msSQL = "select count(*) as lsstatus from brs_mst_trepaymenttransaction" +
                     " where knockoff_status='Matched' and repayreconcildoc_gid='" + repayreconcildoc_gid + "'";
            int lsstatus = Convert.ToInt16(objdbconn.GetExecuteScalar(msSQL));
            if (lsstatus != 0)
            {
                values.message = "Repayment data is matched So..you can't delete it..!";
                values.status = true;
            }
            else
            {
                msSQL = "delete from brs_trn_trepayreconcillationdocument where repayreconcildoc_gid='" + repayreconcildoc_gid + "'";
                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = " select count(*) as brstemplate_flag from brs_mst_trepaymenttransaction " +
                         "where repayreconcildoc_gid = '" + repayreconcildoc_gid + "'";

                int brstemplate_flag = Convert.ToInt16(objdbconn.GetExecuteScalar(msSQL));

                if (brstemplate_flag != 0)
                {
                    msSQL = "delete from brs_mst_trepaymenttransaction where repayreconcildoc_gid='" + repayreconcildoc_gid + "'";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }

                if (mnresult != 0)
                {

                    values.message = " Template and Repayment transaction data deleted successfully";
                    values.status = true;
                }
                else
                {
                    values.message = "Error Occured While Deleting the template";
                    values.status = false;

                }
            }

        }
        public void DaGetCreditCount(cocillationlist values, string user_gid)
        {
            msSQL = "select distinct count(*) as credit_count  FROM brs_trn_tbanktransactiondetails  " +
                        " where  cr_dr ='CR' ";
            values.credit_count = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select distinct count(*) as creditmatch_count  FROM brs_trn_tbanktransactiondetails   " +
                       " where cr_dr ='CR' and  knockoff_status='Matched' ";
            values.creditmatch_count = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select distinct count(*) as partialcredit_count  FROM brs_trn_tbanktransactiondetails  " +
                       " where cr_dr ='CR' and  knockoff_status='PartiallyMatched'  ";
            values.partialcredit_count = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select distinct count(*) as unmatchunassign_count from brs_trn_tbanktransactiondetails " +
                " where  ((cr_dr ='CR' and  knockoff_status='Pending') and (cr_dr ='CR' and  tagged_status='Pending')) ";
            values.unmatchunassign_count = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select  distinct count(*) as unmatchassign_count from brs_trn_tbanktransactiondetails  " +
               " where  ((cr_dr ='CR' and  tagged_status='Assigned') and (cr_dr ='CR' and  knockoff_status='Pending') ) ";
            values.unmatchassign_count = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select  distinct count(*) as creditclose_count from brs_trn_tbanktransactiondetails  " +
               " where ((cr_dr ='CR' and knockoff_status='AssignMatched') or (cr_dr ='CR' and knockoff_status='ManuallyMatched') ) ";
            values.creditclose_count = objdbconn.GetExecuteScalar(msSQL);
        }
        public void DaGetDebitCount(cocillationlist values, string user_gid)
        {
            msSQL = "select distinct count(*) as credit_count  FROM brs_trn_tbanktransactiondetails  " +
                        " where  cr_dr ='DR' ";
            values.debit_count = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select  distinct count(*) as creditmatch_count  FROM brs_trn_tbanktransactiondetails   " +
                       " where cr_dr ='DR' and  knockoff_status='Matched' ";
            values.debitmatch_count = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select distinct count(*) as partialcredit_count  FROM brs_trn_tbanktransactiondetails  " +
                       " where cr_dr ='DR' and  knockoff_status='PartiallyMatched'  ";
            values.partialdebit_count = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select  distinct count(*) as unmatchunassign_count from brs_trn_tbanktransactiondetails " +
                " where  ((cr_dr ='DR' and  knockoff_status='Pending') and (cr_dr ='DR' and  tagged_status='Pending')) ";
            values.unmatchunassign_count = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select distinct count(*) as unmatchassign_count from brs_trn_tbanktransactiondetails  " +
               " where  ((cr_dr ='DR' and  tagged_status='Assigned') and (cr_dr ='DR' and  knockoff_status='Pending') ) ";
            values.unmatchassign_count = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select distinct count(*) as debitclose_count from brs_trn_tbanktransactiondetails  " +
               " where ((cr_dr ='DR' and knockoff_status='AssignMatched') or (cr_dr ='DR' and knockoff_status='ManuallyMatched'))";
            values.debitclose_count = objdbconn.GetExecuteScalar(msSQL);
        }
        public void DaGetRepaymentTemplateCount(uploadlist values, string user_gid)
        {
            try
            {
                msSQL = " SELECT count(*) as repayment_count FROM brs_trn_trepayreconcillationdocument" +
            " order by created_date desc ";
                values.repayment_count = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " SELECT count(*) as transac_count FROM brs_trn_treconcillationdocument" +
                " order by created_date desc ";
                values.transac_count = objdbconn.GetExecuteScalar(msSQL);
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaGetRepaymentUnReconcillationSummary(repayment_unrecocillationlist values, string user_gid)
        {
            try
            {

                msSQL = " select a.bankrepaytransc_gid, a.repayreconcildoc_gid, a.account_number,DATE_FORMAT(a.transaction_date, '%d-%m-%Y %h:%i %p') as transaction_date,DATE_FORMAT(a.repayment_date, '%d-%m-%Y %h:%i %p') as repayment_date,a.transaction_id,a.repayment_amount,a.principal,a.normal_interest,a.forfeiture_waiver, " +
              "  a.remarks,a.user_id,a.repayment_type,a.penal_interest,a.instrument,a.old_dpd,a.dpd,a.account_code,a.interest_tds,a.penal_interest_tds,DATE_FORMAT(a.created_date, '%d-%m-%Y %h:%i %p') as created_date,DATE_FORMAT(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date, " +
              "  a.knockoff_flag,a.tagged_status,b.knockoff_date,a.knockoff_status,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by from brs_mst_trepaymenttransaction a  " +
              "  left join brs_trn_tbanktransactiondetails b on b.bankrepaytransc_gid = a.bankrepaytransc_gid   " +
              "  left join adm_mst_tuser c on a.created_by= c.user_gid  " +
                " where a.knockoff_status='Pending' order by a.created_date desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getrepayment_unrecocillationlist = new List<repayment_unrecocillationlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getrepayment_unrecocillationlist.Add(new repayment_unrecocillationlist
                        {
                            bankrepaytransc_gid = (dr_datarow["bankrepaytransc_gid"].ToString()),
                            repayreconcildoc_gid = (dr_datarow["repayreconcildoc_gid"].ToString()),
                            account_number = (dr_datarow["account_number"].ToString()),
                            account_code = (dr_datarow["account_code"].ToString()),
                            instrument = (dr_datarow["instrument"].ToString()),
                            transaction_date = (dr_datarow["transaction_date"].ToString()),
                            repayment_date = (dr_datarow["repayment_date"].ToString()),
                            transaction_id = (dr_datarow["transaction_id"].ToString()),
                            repayment_amount = (dr_datarow["repayment_amount"].ToString()),
                            principal = (dr_datarow["principal"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            repayment_type = (dr_datarow["repayment_type"].ToString()),
                            penal_interest = (dr_datarow["penal_interest"].ToString()),
                            tagged_status = (dr_datarow["tagged_status"].ToString()),
                            knockoff_status = (dr_datarow["knockoff_status"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),


                        });
                    }
                    values.repayment_unrecocillation_list = getrepayment_unrecocillationlist;
                }
                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }
        public void DaGetRepaymentReconcillationSummary(repayment_cocillationlist values, string user_gid)
        {
            try
            {
                msSQL = "call brs_trn_sprepayreconsummary()";

                //  msSQL = " select a.bankrepaytransc_gid, a.repayreconcildoc_gid, a.account_number,DATE_FORMAT(a.transaction_date, '%d-%m-%Y %h:%i %p') as transaction_date,DATE_FORMAT(a.repayment_date, '%d-%m-%Y %h:%i %p') as repayment_date,a.transaction_id,a.repayment_amount,a.principal,a.normal_interest,a.forfeiture_waiver, " +  
                //"  a.remarks,a.user_id,a.repayment_type,a.penal_interest,a.instrument,a.old_dpd,a.dpd,a.account_code,a.interest_tds,a.penal_interest_tds,DATE_FORMAT(a.created_date, '%d-%m-%Y %h:%i %p') as created_date,DATE_FORMAT(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date, " +
                //"  a.knockoff_flag,a.tagged_status,DATE_FORMAT(b.knockoff_date, '%d-%m-%Y %h:%i %p') as knockoff_date,b.knockoff_status,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by from brs_mst_trepaymenttransaction a  " + 
                //"  left join brs_trn_tbanktransactiondetails b on b.bankrepaytransc_gid = a.bankrepaytransc_gid   " +
                //"  left join adm_mst_tuser c on a.created_by= c.user_gid  " +
                //"  where a.knockoff_status='Matched'  or a.knockoff_status='ManuallyMatched' order by a.created_date desc";


                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getrepayment_cocillationlist = new List<repayment_cocillationlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getrepayment_cocillationlist.Add(new repayment_cocillationlist
                        {
                            bankrepaytransc_gid = (dr_datarow["bankrepaytransc_gid"].ToString()),
                            repayreconcildoc_gid = (dr_datarow["repayreconcildoc_gid"].ToString()),
                            account_number = (dr_datarow["account_number"].ToString()),
                            account_code = (dr_datarow["account_code"].ToString()),
                            instrument = (dr_datarow["instrument"].ToString()),
                            transaction_date = (dr_datarow["transaction_date"].ToString()),
                            repayment_date = (dr_datarow["repayment_date"].ToString()),
                            transaction_id = (dr_datarow["transaction_id"].ToString()),
                            repayment_amount = (dr_datarow["repayment_amount"].ToString()),
                            principal = (dr_datarow["principal"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            repayment_type = (dr_datarow["repayment_type"].ToString()),
                            penal_interest = (dr_datarow["penal_interest"].ToString()),
                            tagged_status = (dr_datarow["tagged_status"].ToString()),
                            knockoff_status = (dr_datarow["knockoff_status"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            knockoff_date = (dr_datarow["knockoff_date"].ToString()),
                            banktransc_gid = (dr_datarow["banktransc_gid"].ToString()),




                        });
                    }
                    values.repayment_cocillation_list = getrepayment_cocillationlist;
                }
                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }
        public void DaGetRepaymentReconcillationCount(repayment_cocillationlist values, string user_gid)
        {
            try
            {
                msSQL = " select count(*) as repay_reconc_count from brs_mst_trepaymenttransaction a " +
                " where a.knockoff_status='Matched' or  a.knockoff_status='PartiallyMatched' order by a.created_date desc ";
                values.repay_reconc_count = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " select count(*) as repay_unreconc_count from brs_mst_trepaymenttransaction a " +
                    " where a.knockoff_status='Pending' order by a.created_date desc ";
                values.repay_unreconc_count = objdbconn.GetExecuteScalar(msSQL);
            }
            catch
            {
                values.status = false;
            }

        }
        public void DaGetRepaymentPartialmatch(repayment_cocillationlist values, string user_gid)
        {
            try
            {
                msSQL = " select count(*) as repay_reconc_count from brs_mst_trepaymenttransaction a " +
                " where a.knockoff_status='Matched' or  a.knockoff_status='PartiallyMatched' order by a.created_date desc ";
                values.repay_reconc_count = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " select count(*) as repay_unreconc_count from brs_mst_trepaymenttransaction a " +
                    " where a.knockoff_status='Pending' order by a.created_date desc ";
                values.repay_unreconc_count = objdbconn.GetExecuteScalar(msSQL);
            }
            catch
            {
                values.status = false;
            }

        }
        public string GetDateFormat(string lsdate)
        {
            DateTime Date;
            string[] formats = { "dd/MM/yyyy","dd-MM-yyyy", "dd/M/yyyy","dd-M-yyyy", "d/M/yyyy", "d-M-yyyy", "d/MM/yyyy","d-MM-yyyy",
                                                           "dd/MM/yy", "dd-MM-yy", "dd/M/yy","dd-M-yy", "d/M/yy", "d-M-yy", "d/MM/yy", "d-MM-yy", "MM/dd/yyyy","MM/dd/yyyy h:mm:ss tt","M/dd/yyyy","M/dd/yyyy h:mm:ss tt","MM-dd-yyyy","d/M/yyyy h:mm:ss tt","d-M-yyyy h:mm:ss tt",
                                                "dd/MM/yyyy h:mm:ss tt","dd-MM-yyyy h:mm:ss tt","dd-M-yyyy h:mm:ss tt" ,"d-MM-yyyy h:mm:ss tt","dd/M/yyyy h:mm:ss tt","dd-MM-yy h:mm:ss tt","dd/MM/yy h:mm:ss tt","d/M/yyyy h:mm:ss","d-M-yyyy h:mm:ss",
                                                "dd/MM/yyyy h:mm:ss","dd-MM-yyyy h:mm:ss","dd-M-yyyy h:mm:ss" ,"d-MM-yyyy h:mm:ss","dd/M/yyyy h:mm:ss","dd-MM-yy h:mm:ss","dd/MM/yy h:mm:ss"};
            DateTime.TryParseExact(lsdate, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out Date);
            lsconverted_date = Convert.ToDateTime(Date).ToString("yyyy-MM-dd HH:mm:ss");

            return lsconverted_date;
        }

        public void DaGetLMSPartialhistory(repayment_cocillationlist values, string banktransc_gid)
        {
            try
            {

                msSQL = " select a.bankrepaytransc_gid,a.banktransc_gid, a.repayreconcildoc_gid, a.account_number, " +
                " DATE_FORMAT(a.transaction_date, '%d-%m-%Y %h:%i %p') as transaction_date,DATE_FORMAT(a.repayment_date, '%d-%m-%Y %h:%i %p') as repayment_date, " +
                " a.transaction_id,FORMAT(a.repayment_amount,2,'en_IN') as repayment_amount ,FORMAT(a.principal,2,'en_IN') as principal,FORMAT(a.normal_interest,2,'en_IN') as normal_interest, " +
                "a.forfeiture_waiver,  a.remarks,a.user_id,a.repayment_type,a.penal_interest,a.instrument,a.old_dpd,dpd,a.account_code,a.interest_tds,a.penal_interest_tds, " +
                " DATE_FORMAT(a.created_date, '%d-%m-%Y %h:%i %p') as created_date,a.updated_by,DATE_FORMAT(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date,a.knockoff_flag,a.tagged_status,a.knockoff_date, " +
                " a.knockoff_status,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by from brs_mst_trepaymenttransaction a " +
                " left join adm_mst_tuser c on a.created_by= c.user_gid " +
                " where a.knockoff_status='PartiallyMatched' and a.banktransc_gid='" + banktransc_gid + "'  order by a.created_date desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getrepaymentlist = new List<repayment_cocillationlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getrepaymentlist.Add(new repayment_cocillationlist
                        {
                            bankrepaytransc_gid = (dr_datarow["bankrepaytransc_gid"].ToString()),
                            repayreconcildoc_gid = (dr_datarow["repayreconcildoc_gid"].ToString()),
                            account_number = (dr_datarow["account_number"].ToString()),
                            account_code = (dr_datarow["account_code"].ToString()),
                            instrument = (dr_datarow["instrument"].ToString()),
                            transaction_date = (dr_datarow["transaction_date"].ToString()),
                            repayment_date = (dr_datarow["repayment_date"].ToString()),
                            transaction_id = (dr_datarow["transaction_id"].ToString()),
                            repayment_amount = (dr_datarow["repayment_amount"].ToString()),
                            principal = (dr_datarow["principal"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            repayment_type = (dr_datarow["repayment_type"].ToString()),
                            penal_interest = (dr_datarow["penal_interest"].ToString()),
                            tagged_status = (dr_datarow["tagged_status"].ToString()),
                            knockoff_status = (dr_datarow["knockoff_status"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            knockoff_date = (dr_datarow["knockoff_date"].ToString()),
                            banktransc_gid = (dr_datarow["banktransc_gid"].ToString()),


                        });
                    }
                    values.repayment_lmshistory = getrepaymentlist;
                }
                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }

        public void DaBRSLmsprocess(string user_gid, MdllmsTmprepay values)
        {
            msSQL = " insert into brs_mst_trepaymenttransaction(bankrepaytransc_gid, account_number,urn_no, transaction_date, repayment_date, " +
                   " transaction_id ,repayment_amount,principal,normal_interest,forfeiture_waiver,user_id,repayment_type,penal_interest, " +
                   " old_dpd,instrument,dpd,account_code,interest_tds,remarks, " +
                   " penal_interest_tds,repayreconcildoc_gid,created_by,created_date) " +
                   " select concat('MISD'," + DateTime.Now.ToString("yyyyMMdd") + " ,tmprepayment_gid) as bankrepaytransc_gid,account_number,urn_no, " +
                   " transaction_date  as transaction_date, " +
                   " repayment_date  as repayment_date, " +
                   " transaction_id ,repayment_amount,principal,normal_interest,forfeiture_waiver,user_id,repayment_type,penal_interest, " +
                   " old_dpd,instrument,dpd,account_code,interest_tds,remarks, " +
                   " penal_interest_tds,repayreconcildoc_gid,'" + user_gid + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                   " from brs_tmp_trepayment where repayreconcildoc_gid='" + values.repayreconcildoc_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult == 1)
            {
                List<MdlImportbanktransactiondtl> MdlImportbanktransactiondtl = new List<MdlImportbanktransactiondtl>();
                List<Mdllmsrepayprocess> Mdllmsrepayprocess = new List<Mdllmsrepayprocess>();

                // Repayment table
                msSQL = " select repayment_amount,account_number,transaction_id,urn_no, DATE_FORMAT(transaction_date, '%Y-%m-%d') as transaction_date, DATE_FORMAT(repayment_date, '%Y-%m-%d') as repayment_date,bankrepaytransc_gid,remarks from brs_mst_trepaymenttransaction " +
                        " where repayreconcildoc_gid='" + values.repayreconcildoc_gid + "' " +
                        " and remarks in (select  transact_particulars from  brs_trn_tbanktransactiondetails group by transact_particulars) ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                Mdllmsrepayprocess = cmnfunctions.ConvertDataTable<Mdllmsrepayprocess>(dt_datatable);
                dt_datatable.Dispose();

                msSQL = " select remarks from brs_mst_trepaymenttransaction " +
                      " where repayreconcildoc_gid='" + values.repayreconcildoc_gid + "' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsremarks = (dr_datarow["remarks"].ToString());

                        msSQL = "SELECT transact_particulars " +
                       "FROM brs_trn_tbanktransactiondetails where knockoff_status='Pending'  ";
                        dt_datatable1 = objdbconn.GetDataTable(msSQL);

                        foreach (DataRow dr_datarow1 in dt_datatable1.Rows)
                        {

                            lstransact_particulars = (dr_datarow1["transact_particulars"].ToString());
                            bool lspartialstatus = lstransact_particulars.Contains(lsremarks);

                            if (lspartialstatus == true)
                            {
                                null_count = null_count + 1;
                            }
                        }
                    }
                }

                if (Mdllmsrepayprocess.Count == 0 && null_count == 0)
                {
                    msSQL = " update brs_trn_trepayreconcillationdocument set norepayment_flag='Y' " +
                            " where repayreconcildoc_gid = '" + values.repayreconcildoc_gid + "' ";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnresult != 0) {
                        msSQL = " delete from brs_tmp_trepayment " +
                              " where repayreconcildoc_gid = '" + values.repayreconcildoc_gid + "' ";
                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    values.status = false;
                    values.message = "No Repayment Progress Data";
                    return;
                }

                // Sub bank Table 
                msSQL = " SELECT  banktransc_gid,banktransc_refno,date_format(trn_date,'%Y-%m-%d') as trn_date, FORMAT(remaining_amount,2) as remaining_amount,transact_val, " +
                        " cr_dr,transact_particulars,knockoff_status,bankrepaytransc_gid FROM brs_trn_tbanktransactiondetails " +
                        " where  knockoff_status ='Pending' and transact_particulars in ( select remarks from brs_tmp_trepayment " +
                        " where repayreconcildoc_gid='" + values.repayreconcildoc_gid + "')";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                MdlImportbanktransactiondtl = cmnfunctions.ConvertDataTable<MdlImportbanktransactiondtl>(dt_datatable);
                dt_datatable.Dispose();

                
                foreach (var dr in Mdllmsrepayprocess)
                {
                    try
                    {
                        List<MdlImportbanktransactiondtl> checktransactParticular = MdlImportbanktransactiondtl.Where(a => a.transact_particulars == dr.remarks && a.trn_date == dr.repayment_date).ToList();
                        if (checktransactParticular.Count != 0)
                        {
                              
                            foreach (var j in checktransactParticular)
                            {
                                List<MdlImportbanktransactiondtl> checkbanktranscrepay_gid = MdlImportbanktransactiondtl.Where(a => a.bankrepaytransc_gid == dr.bankrepaytransc_gid).ToList();
                                if (checkbanktranscrepay_gid.Count == 0)
                                {
                                    lscredit_amt = j.transact_val;
                                    lscr_dr = j.cr_dr;
                                    lsbanktransc_gid = j.banktransc_gid;
                                    lsbanktransc_refno = j.banktransc_refno;
                                    lsknockoff_status = j.knockoff_status;
                                    double lsremain_amount = Convert.ToDouble(j.remaining_amount);
                                    double lsrepayment_amount = Convert.ToDouble(dr.repayment_amount);

                                    if (lsremain_amount == lsrepayment_amount && ((lscr_dr == "CR") || (lscr_dr == "Cr")) && (lsknockoff_status == "Pending"))
                                    {
                                        string knocoffstatus = "Matched";
                                        msSQL = " update brs_trn_tbanktransactiondetails set knockoff_status='" + knocoffstatus + "' , " +
                                                " knockoff_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                                                " knockoff_flag='Y', " +
                                                " fullymatched_flag='Y', " +
                                                " rule_number ='R1', " +
                                                " bankrepaytransc_gid = '" + dr.bankrepaytransc_gid + "' , " +
                                                " closed_by= '" + user_gid + "' " +
                                                " where banktransc_refno = '" + lsbanktransc_refno + "' ";
                                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                        msSQL = " update brs_trn_tbanktransactiondetails set remaining_amount='0.0'" +
                                                " where banktransc_gid = '" + lsbanktransc_gid + "'";
                                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                        msSQL = " update brs_mst_trepaymenttransaction set knockoff_status='Matched', " +
                                                    " knockoff_flag='Y', " +
                                                    " fullymatched_flag='Y', " +
                                                    " knockoff_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                                                    " banktransc_gid = '" + lsbanktransc_gid + "'" +
                                                    " where remarks = '" + dr.remarks + "' ";
                                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                        string lsbankremainingamount = "0.00";

                                        msGetGidUR = objcmnfunctions.GetMasterGID("URTD");
                                        msSQL = " insert into brs_trn_tunrecontransactiondetails(" +
                                                     " unrecontransactiondetails_gid," +
                                                     " banktransc_gid," +
                                                     " department_name," +
                                                     " assignby_gid," +
                                                     " assignby_name," +
                                                     " activity_gid," +
                                                     " activity_name," +
                                                     " amount," +
                                                     " action_name," +
                                                     " transaction_remarks," +
                                                     " account_number," +
                                                     " transaction_id," +
                                                     " transaction_date," +
                                                     " repayment_date," +
                                                     " urn_no," +
                                                     " created_by," +
                                                     " created_date )" +
                                                     " values(" +
                                                     "'" + msGetGidUR + "'," +
                                                     "'" + lsbanktransc_gid + "'," +
                                                     "'Technology'," +
                                                     "'SERM2022062277'," +
                                                     "'autoapproval.samfin@samunnati.com'," +
                                                     "'BRAA0002'," +
                                                     "'collection'," +
                                                     "'" + dr.repayment_amount + "'," +
                                                     "'Booked in LMS / FA'," +
                                                     "'AutoClosed'," +
                                                     "'" + dr.account_number + "'," +
                                                     "'" + dr.transaction_id + "'," +
                                                     "'" + dr.transaction_date + "'," +
                                                     "'" + dr.repayment_date + "'," +
                                                     "'" + dr.urn_no + "'," +
                                                     "'" + user_gid + "'," +
                                                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        int index = MdlImportbanktransactiondtl.FindIndex(a => a.banktransc_gid == lsbanktransc_gid);
                                        if (index != -1)
                                        {
                                            MdlImportbanktransactiondtl[index].remaining_amount = lsbankremainingamount.ToString();
                                            MdlImportbanktransactiondtl[index].bankrepaytransc_gid = dr.bankrepaytransc_gid.ToString();

                                        }

                                    }

                                    else if ((lsrepayment_amount < lsremain_amount) && (lsknockoff_status == "Pending"))
                                    {
                                        // Amount unmatched 

                                        msSQL = " SELECT  sum(amount) FROM brs_trn_tunrecontransactiondetails" +
                                                " where action_name ='Booked in LMS / FA' and banktransc_gid = '" + lsbanktransc_gid + "'";
                                        string lsamount = objdbconn.GetExecuteScalar(msSQL);
                                        lsamount = (lsamount == null || lsamount == "") ? "0" : lsamount;

                                        decimal convertDecimallsamount = Convert.ToDecimal(lsamount) + Convert.ToDecimal(dr.repayment_amount);
                                        remaining_amount = Convert.ToDecimal(j.remaining_amount) - Convert.ToDecimal(dr.repayment_amount);

                                        msGetGid = objcmnfunctions.GetMasterGID("URTD");

                                        msSQL = " insert into brs_trn_tunrecontransactiondetails(" +
                                                " unrecontransactiondetails_gid," +
                                                " banktransc_gid," +
                                                " department_name," +
                                                " assignby_gid," +
                                                " assignby_name," +
                                                " activity_gid," +
                                                " activity_name," +
                                                " amount," +
                                                " remaining_amount," +
                                                " action_name," +
                                                " transaction_remarks," +
                                                " account_number," +
                                                " transaction_id," +
                                                " transaction_date," +
                                                " repayment_date," +
                                                " urn_no," +
                                                " created_by," +
                                                " created_date )" +
                                                " values(" +
                                               "'" + msGetGid + "'," +
                                               "'" + lsbanktransc_gid + "'," +
                                               "'Technology'," +
                                               "'SERM2022062277'," +
                                               "'autoapproval.samfin@samunnati.com'," +
                                               "'BRAA0002'," +
                                               "'collection'," +
                                               "'" + dr.repayment_amount + "'," +
                                               "'" + remaining_amount + "'," +
                                               "'Booked in LMS / FA'," +
                                               "'AutoClosed'," +
                                               "'" + dr.account_number + "'," +
                                               "'" + dr.transaction_id + "'," +
                                               "'" + dr.transaction_date + "'," +
                                               "'" + dr.repayment_date + "'," +
                                               "'" + dr.urn_no + "'," +
                                               "'" + user_gid + "'," +
                                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);


                                        msSQL = " update brs_trn_tbanktransactiondetails set " +
                                                " bankrepaytransc_gid = '" + dr.bankrepaytransc_gid + "' , " +
                                                " remaining_amount= '" + remaining_amount + "'," +
                                                " adjust_amount = '" + convertDecimallsamount + "', " +
                                                " rule_number ='R1'," +
                                                " fullymatched_flag='Y', " +
                                                " brstransactiondetailsadvice_flag = 'Y' " +
                                                " where banktransc_gid = '" + lsbanktransc_gid + "' ";
                                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                        msSQL = " update brs_mst_trepaymenttransaction set knockoff_status='Matched'," +
                                                " knockoff_flag='Y', " +
                                                " fullymatched_flag='Y', " +
                                                " knockoff_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                                                " banktransc_gid = '" + lsbanktransc_gid + "' " +
                                                " where remarks = '" + dr.remarks + "' ";
                                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        int index = MdlImportbanktransactiondtl.FindIndex(a => a.banktransc_gid == lsbanktransc_gid);
                                        if (index != -1)
                                        {
                                            MdlImportbanktransactiondtl[index].remaining_amount = remaining_amount.ToString();
                                            MdlImportbanktransactiondtl[index].bankrepaytransc_gid = dr.bankrepaytransc_gid.ToString();

                                        }


                                    }
                                    else if ((lsrepayment_amount > lsremain_amount) && (lsknockoff_status == "Pending"))
                                    {
                                        // Amount unmatched Negative Remaining Amount

                                        msSQL = " SELECT  sum(amount) FROM brs_trn_tunrecontransactiondetails" +
                                                " where action_name ='Booked in LMS / FA' and banktransc_gid = '" + lsbanktransc_gid + "'";
                                        string lsamount = objdbconn.GetExecuteScalar(msSQL);
                                        lsamount = (lsamount == null || lsamount == "") ? "0" : lsamount;

                                        decimal convertDecimallsamount = Convert.ToDecimal(lsamount) + Convert.ToDecimal(dr.repayment_amount);
                                        remaining_amount = Convert.ToDecimal(j.remaining_amount) - Convert.ToDecimal(dr.repayment_amount);

                                        msGetGid = objcmnfunctions.GetMasterGID("URTD");

                                        msSQL = " insert into brs_trn_tunrecontransactiondetails(" +
                                                " unrecontransactiondetails_gid," +
                                                " banktransc_gid," +
                                                " department_name," +
                                                " assignby_gid," +
                                                " assignby_name," +
                                                " activity_gid," +
                                                " activity_name," +
                                                " amount," +
                                                " remaining_amount," +
                                                " action_name," +
                                                " transaction_remarks," +
                                                " account_number," +
                                                " transaction_id," +
                                                " transaction_date," +
                                                " repayment_date," +
                                                " urn_no," +
                                                " created_by," +
                                                " created_date )" +
                                                " values(" +
                                               "'" + msGetGid + "'," +
                                               "'" + lsbanktransc_gid + "'," +
                                               "'Technology'," +
                                               "'SERM2022062277'," +
                                               "'autoapproval.samfin@samunnati.com'," +
                                               "'BRAA0002'," +
                                               "'collection'," +
                                               "'" + dr.repayment_amount + "'," +
                                               "'" + remaining_amount + "'," +
                                               "'Booked in LMS / FA'," +
                                               "'AutoClosed'," +
                                               "'" + dr.account_number + "'," +
                                               "'" + dr.transaction_id + "'," +
                                               "'" + dr.transaction_date + "'," +
                                               "'" + dr.repayment_date + "'," +
                                               "'" + dr.urn_no + "'," +
                                               "'" + user_gid + "'," +
                                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                        string negativeremainingamount = "-10.00";

                                        decimal negativeremainingamountcheck = Convert.ToDecimal(negativeremainingamount);

                                        if (remaining_amount >= negativeremainingamountcheck)
                                        {
                                            string knocoffstatus = "Matched";
                                            msSQL = " update brs_trn_tbanktransactiondetails set knockoff_status='" + knocoffstatus + "' , " +
                                                    " knockoff_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                                                    " knockoff_flag='Y', " +
                                                    " fullymatched_flag='Y', " +
                                                    " rule_number ='R1', " +
                                                    " bankrepaytransc_gid = '" + dr.bankrepaytransc_gid + "' , " +
                                                    " closed_by= '" + user_gid + "' " +
                                                    " where banktransc_refno = '" + lsbanktransc_refno + "' ";
                                            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                            msSQL = " update brs_trn_tbanktransactiondetails set adjust_amount = '" + convertDecimallsamount + "',remaining_amount='" + remaining_amount + "',brstransactiondetailsadvice_flag = 'Y' where banktransc_gid='" + lsbanktransc_gid + "'";
                                            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                            msSQL = " update brs_mst_trepaymenttransaction set knockoff_status='Matched', " +
                                                        " knockoff_flag='Y', " +
                                                        " fullymatched_flag='Y', " +
                                                        " knockoff_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                                                        " banktransc_gid = '" + lsbanktransc_gid + "'" +
                                                        " where remarks = '" + dr.remarks + "' ";
                                            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        }

                                        else
                                        {
                                            msSQL = " update brs_trn_tbanktransactiondetails set " +
                                               " bankrepaytransc_gid = '" + dr.bankrepaytransc_gid + "' , " +
                                               " remaining_amount= '" + remaining_amount + "'," +
                                               " adjust_amount = '" + convertDecimallsamount + "', " +
                                               " rule_number ='R1'," +
                                               " fullymatched_flag='Y', " +
                                               " brstransactiondetailsadvice_flag = 'Y' " +
                                               " where banktransc_gid = '" + lsbanktransc_gid + "' ";
                                            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                            msSQL = " update brs_mst_trepaymenttransaction set knockoff_status='Matched'," +
                                                    " knockoff_flag='Y', " +
                                                    " fullymatched_flag='Y', " +
                                                    " knockoff_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                                                    " banktransc_gid = '" + lsbanktransc_gid + "' " +
                                                    " where remarks = '" + dr.remarks + "' ";
                                            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        }
                                           

                                        int index = MdlImportbanktransactiondtl.FindIndex(a => a.banktransc_gid == lsbanktransc_gid);
                                        if (index != -1)
                                        {
                                            MdlImportbanktransactiondtl[index].remaining_amount = remaining_amount.ToString();
                                            MdlImportbanktransactiondtl[index].bankrepaytransc_gid = dr.bankrepaytransc_gid.ToString();

                                        }


                                    }

                                }



                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        msSQL = " delete from brs_mst_trepaymenttransaction " +
                             " where repayreconcildoc_gid = '" + values.repayreconcildoc_gid + "' ";
                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        values.status = false;
                        values.message = ex.ToString();
                    }
                }


                msSQL = " select repayment_amount,account_number,transaction_id,urn_no, DATE_FORMAT(transaction_date, '%Y-%m-%d') as transaction_date, DATE_FORMAT(repayment_date, '%Y-%m-%d') as repayment_date,bankrepaytransc_gid,remarks from brs_mst_trepaymenttransaction " +

                 " where repayreconcildoc_gid='" + values.repayreconcildoc_gid + "' and fullymatched_flag='N' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                Mdllmsrepayprocess = cmnfunctions.ConvertDataTable<Mdllmsrepayprocess>(dt_datatable);
                dt_datatable.Dispose();

                if (Mdllmsrepayprocess.Count != 0)
                {
                    foreach (var lmspartial in Mdllmsrepayprocess)
                    {
                        lmspartial.remarks.ToList();

                        msSQL = " SELECT  banktransc_gid,banktransc_refno,date_format(trn_date,'%Y-%m-%d') as trn_date, FORMAT(remaining_amount,2) as remaining_amount,transact_val, " +
                          " cr_dr,transact_particulars,knockoff_status,bankrepaytransc_gid FROM brs_trn_tbanktransactiondetails " +
                          " where knockoff_status='Pending' and fullymatched_flag='N'";

                          dt_datatable1 = objdbconn.GetDataTable(msSQL);
                        MdlImportbanktransactiondtl = cmnfunctions.ConvertDataTable<MdlImportbanktransactiondtl>(dt_datatable1);
                        dt_datatable.Dispose();

                        foreach (var bankpartial in MdlImportbanktransactiondtl)
                        {
                            bankpartial.transact_particulars.ToList();
                            
                            bool lspartialstatus = bankpartial.transact_particulars.Contains(lmspartial.remarks);

                            if (lspartialstatus == true)
                            {
                          
                                try
                                {
                                    List<MdlImportbanktransactiondtl> checktransactParticularPartial = MdlImportbanktransactiondtl.Where(a => a.transact_particulars.Contains(lmspartial.remarks) && a.trn_date == lmspartial.repayment_date).ToList();
                                    if (checktransactParticularPartial.Count != 0)
                                    {
                                           
                                                List<MdlImportbanktransactiondtl> checkbanktranscrepay_gid = MdlImportbanktransactiondtl.Where(a => a.bankrepaytransc_gid == lmspartial.bankrepaytransc_gid).ToList();
                                                if (checkbanktranscrepay_gid.Count == 0)
                                                {
                                                    lscredit_amt = bankpartial.transact_val;
                                                    lscr_dr = bankpartial.cr_dr;
                                                    lsbanktransc_gid = bankpartial.banktransc_gid;
                                                    lsbanktransc_refno = bankpartial.banktransc_refno;
                                                    lsknockoff_status = bankpartial.knockoff_status;
                                                    double lsremain_amount = Convert.ToDouble(bankpartial.remaining_amount);
                                                    double lsrepayment_amount = Convert.ToDouble(lmspartial.repayment_amount);

                                                    if (lsremain_amount == lsrepayment_amount && ((lscr_dr == "CR") || (lscr_dr == "Cr")) && (lsknockoff_status == "Pending"))
                                                    {
                                                        string knocoffstatus = "Matched";
                                                        msSQL = " update brs_trn_tbanktransactiondetails set knockoff_status='" + knocoffstatus + "' , " +
                                                                " knockoff_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                                                                " knockoff_flag='Y', " +
                                                                " rule_number ='R2', " +
                                                                " bankrepaytransc_gid = '" + lmspartial.bankrepaytransc_gid + "' , " +
                                                                " closed_by= '" + user_gid + "' " +
                                                                " where banktransc_refno = '" + lsbanktransc_refno + "' ";
                                                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                                        msSQL = " update brs_trn_tbanktransactiondetails set remaining_amount='0.0'" +
                                                                " where banktransc_gid = '" + lsbanktransc_gid + "'";
                                                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                                        msSQL = " update brs_mst_trepaymenttransaction set knockoff_status='Matched', " +
                                                                    " knockoff_flag='Y', " +
                                                                    " knockoff_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                                                                    " banktransc_gid = '" + lsbanktransc_gid + "'" +
                                                                    " where remarks = '" + lmspartial.remarks + "' ";
                                                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                                        string lsbankremainingamount = "0.00";

                                                        msGetGidUR = objcmnfunctions.GetMasterGID("URTD");
                                                        msSQL = " insert into brs_trn_tunrecontransactiondetails(" +
                                                                     " unrecontransactiondetails_gid," +
                                                                     " banktransc_gid," +
                                                                     " department_name," +
                                                                     " assignby_gid," +
                                                                     " assignby_name," +
                                                                     " activity_gid," +
                                                                     " activity_name," +
                                                                     " amount," +
                                                                     " action_name," +
                                                                     " transaction_remarks," +
                                                                     " account_number," +
                                                                     " transaction_id," +
                                                                     " transaction_date," +
                                                                     " repayment_date," +
                                                                     " urn_no," +
                                                                     " created_by," +
                                                                     " created_date )" +
                                                                     " values(" +
                                                                     "'" + msGetGidUR + "'," +
                                                                     "'" + lsbanktransc_gid + "'," +
                                                                     "'Technology'," +
                                                                     "'SERM2022062277'," +
                                                                     "'autoapproval.samfin@samunnati.com'," +
                                                                     "'BRAA0002'," +
                                                                     "'collection'," +
                                                                     "'" + lmspartial.repayment_amount + "'," +
                                                                     "'Booked in LMS / FA'," +
                                                                     "'AutoClosed'," +
                                                                     "'" + lmspartial.account_number + "'," +
                                                                     "'" + lmspartial.transaction_id + "'," +
                                                                     "'" + lmspartial.transaction_date + "'," +
                                                                     "'" + lmspartial.repayment_date + "'," +
                                                                     "'" + lmspartial.urn_no + "'," +
                                                                     "'" + user_gid + "'," +
                                                                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                                        int index = MdlImportbanktransactiondtl.FindIndex(a => a.banktransc_gid == lsbanktransc_gid);
                                                        if (index != -1)
                                                        {
                                                            MdlImportbanktransactiondtl[index].remaining_amount = lsbankremainingamount.ToString();
                                                            MdlImportbanktransactiondtl[index].bankrepaytransc_gid = lmspartial.bankrepaytransc_gid.ToString();

                                                        }

                                                    }

                                                    else if ((lsrepayment_amount < lsremain_amount) && (lsknockoff_status == "Pending"))
                                                    {
                                                        // Amount unmatched 

                                                        msSQL = " SELECT  sum(amount) FROM brs_trn_tunrecontransactiondetails" +
                                                                " where action_name ='Booked in LMS / FA' and banktransc_gid = '" + lsbanktransc_gid + "'";
                                                        string lsamount = objdbconn.GetExecuteScalar(msSQL);
                                                        lsamount = (lsamount == null || lsamount == "") ? "0" : lsamount;

                                                        decimal convertDecimallsamount = Convert.ToDecimal(lsamount) + Convert.ToDecimal(lmspartial.repayment_amount);
                                                        remaining_amount = Convert.ToDecimal(bankpartial.remaining_amount) - Convert.ToDecimal(lmspartial.repayment_amount);

                                                        msGetGid = objcmnfunctions.GetMasterGID("URTD");

                                                        msSQL = " insert into brs_trn_tunrecontransactiondetails(" +
                                                                " unrecontransactiondetails_gid," +
                                                                " banktransc_gid," +
                                                                " department_name," +
                                                                " assignby_gid," +
                                                                " assignby_name," +
                                                                " activity_gid," +
                                                                " activity_name," +
                                                                " amount," +
                                                                " remaining_amount," +
                                                                " action_name," +
                                                                " transaction_remarks," +
                                                                " account_number," +
                                                                " transaction_id," +
                                                                " transaction_date," +
                                                                " repayment_date," +
                                                                " urn_no," +
                                                                " created_by," +
                                                                " created_date )" +
                                                                " values(" +
                                                               "'" + msGetGid + "'," +
                                                               "'" + lsbanktransc_gid + "'," +
                                                               "'Technology'," +
                                                               "'SERM2022062277'," +
                                                               "'autoapproval.samfin@samunnati.com'," +
                                                               "'BRAA0002'," +
                                                               "'collection'," +
                                                               "'" + lmspartial.repayment_amount + "'," +
                                                               "'" + remaining_amount + "'," +
                                                               "'Booked in LMS / FA'," +
                                                               "'AutoClosed'," +
                                                               "'" + lmspartial.account_number + "'," +
                                                               "'" + lmspartial.transaction_id + "'," +
                                                               "'" + lmspartial.transaction_date + "'," +
                                                               "'" + lmspartial.repayment_date + "'," +
                                                               "'" + lmspartial.urn_no + "'," +
                                                               "'" + user_gid + "'," +
                                                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);


                                                        msSQL = " update brs_trn_tbanktransactiondetails set " +
                                                                " bankrepaytransc_gid = '" + lmspartial.bankrepaytransc_gid + "' , " +
                                                                " remaining_amount= '" + remaining_amount + "'," +
                                                                " adjust_amount = '" + convertDecimallsamount + "', " +
                                                                " rule_number ='R2'," +
                                                                " brstransactiondetailsadvice_flag = 'Y' " +
                                                                " where banktransc_gid = '" + lsbanktransc_gid + "' ";
                                                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                                        msSQL = " update brs_mst_trepaymenttransaction set knockoff_status='Matched'," +
                                                                " knockoff_flag='Y', " +
                                                                " knockoff_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                                                                " banktransc_gid = '" + lsbanktransc_gid + "' " +
                                                                " where remarks = '" + lmspartial.remarks + "' ";
                                                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                                        int index = MdlImportbanktransactiondtl.FindIndex(a => a.banktransc_gid == lsbanktransc_gid);
                                                        if (index != -1)
                                                        {
                                                            MdlImportbanktransactiondtl[index].remaining_amount = remaining_amount.ToString();
                                                            MdlImportbanktransactiondtl[index].bankrepaytransc_gid = lmspartial.bankrepaytransc_gid.ToString();

                                                        }


                                                    }
                                            else if ((lsrepayment_amount > lsremain_amount) && (lsknockoff_status == "Pending"))
                                            {
                                                // Amount unmatched Negative Remaining Amount

                                                msSQL = " SELECT  sum(amount) FROM brs_trn_tunrecontransactiondetails" +
                                                        " where action_name ='Booked in LMS / FA' and banktransc_gid = '" + lsbanktransc_gid + "'";
                                                string lsamount = objdbconn.GetExecuteScalar(msSQL);
                                                lsamount = (lsamount == null || lsamount == "") ? "0" : lsamount;

                                                decimal convertDecimallsamount = Convert.ToDecimal(lsamount) + Convert.ToDecimal(lmspartial.repayment_amount);
                                                remaining_amount = Convert.ToDecimal(bankpartial.remaining_amount) - Convert.ToDecimal(lmspartial.repayment_amount);

                                                msGetGid = objcmnfunctions.GetMasterGID("URTD");

                                                msSQL = " insert into brs_trn_tunrecontransactiondetails(" +
                                                        " unrecontransactiondetails_gid," +
                                                        " banktransc_gid," +
                                                        " department_name," +
                                                        " assignby_gid," +
                                                        " assignby_name," +
                                                        " activity_gid," +
                                                        " activity_name," +
                                                        " amount," +
                                                        " remaining_amount," +
                                                        " action_name," +
                                                        " transaction_remarks," +
                                                        " account_number," +
                                                        " transaction_id," +
                                                        " transaction_date," +
                                                        " repayment_date," +
                                                        " urn_no," +
                                                        " created_by," +
                                                        " created_date )" +
                                                        " values(" +
                                                       "'" + msGetGid + "'," +
                                                       "'" + lsbanktransc_gid + "'," +
                                                       "'Technology'," +
                                                       "'SERM2022062277'," +
                                                       "'autoapproval.samfin@samunnati.com'," +
                                                       "'BRAA0002'," +
                                                       "'collection'," +
                                                       "'" + lmspartial.repayment_amount + "'," +
                                                       "'" + remaining_amount + "'," +
                                                       "'Booked in LMS / FA'," +
                                                       "'AutoClosed'," +
                                                       "'" + lmspartial.account_number + "'," +
                                                       "'" + lmspartial.transaction_id + "'," +
                                                       "'" + lmspartial.transaction_date + "'," +
                                                       "'" + lmspartial.repayment_date + "'," +
                                                       "'" + lmspartial.urn_no + "'," +
                                                       "'" + user_gid + "'," +
                                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);


                                                string negativeremainingamount = "-10.00";

                                                decimal negativeremainingamountcheck = Convert.ToDecimal(negativeremainingamount);

                                                if (remaining_amount >= negativeremainingamountcheck)
                                                {
                                                
                                                    string knocoffstatus = "Matched";
                                                    msSQL = " update brs_trn_tbanktransactiondetails set knockoff_status='" + knocoffstatus + "' , " +
                                                            " knockoff_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                                                            " knockoff_flag='Y', " +
                                                            " rule_number ='R2', " +
                                                            " bankrepaytransc_gid = '" + lmspartial.bankrepaytransc_gid + "' , " +
                                                            " closed_by= '" + user_gid + "' " +
                                                            " where banktransc_refno = '" + lsbanktransc_refno + "' ";
                                                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                                    msSQL = " update brs_trn_tbanktransactiondetails set remaining_amount='0.0'" +
                                                            " where banktransc_gid = '" + lsbanktransc_gid + "'";
                                                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                                    msSQL = " update brs_mst_trepaymenttransaction set knockoff_status='Matched', " +
                                                                " knockoff_flag='Y', " +
                                                                " knockoff_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                                                                " banktransc_gid = '" + lsbanktransc_gid + "'" +
                                                                " where remarks = '" + lmspartial.remarks + "' ";
                                                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                                }

                                                else
                                                {
                                                    msSQL = " update brs_trn_tbanktransactiondetails set " +
                                                            " bankrepaytransc_gid = '" + lmspartial.bankrepaytransc_gid + "' , " +
                                                            " remaining_amount= '" + remaining_amount + "'," +
                                                            " adjust_amount = '" + convertDecimallsamount + "', " +
                                                            " rule_number ='R2'," +
                                                            " brstransactiondetailsadvice_flag = 'Y' " +
                                                            " where banktransc_gid = '" + lsbanktransc_gid + "' ";
                                                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                                    msSQL = " update brs_mst_trepaymenttransaction set knockoff_status='Matched'," +
                                                            " knockoff_flag='Y', " +
                                                            " knockoff_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                                                            " banktransc_gid = '" + lsbanktransc_gid + "' " +
                                                            " where remarks = '" + lmspartial.remarks + "' ";
                                                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                                }

                                                
                                                int index = MdlImportbanktransactiondtl.FindIndex(a => a.banktransc_gid == lsbanktransc_gid);
                                                if (index != -1)
                                                {
                                                    MdlImportbanktransactiondtl[index].remaining_amount = remaining_amount.ToString();
                                                    MdlImportbanktransactiondtl[index].bankrepaytransc_gid = lmspartial.bankrepaytransc_gid.ToString();

                                                }


                                            }

                                        }



                                            
                                        }


                                    }
                                    catch (Exception ex)
                                    {
                                        msSQL = " delete from brs_mst_trepaymenttransaction " +
                                             " where repayreconcildoc_gid = '" + values.repayreconcildoc_gid + "' ";
                                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                        values.status = false;
                                        values.message = ex.ToString();
                                    }
                                

                            }
                        }
                    }
                }



                msSQL = " update brs_trn_trepayreconcillationdocument set status='Y' " +
                              " where repayreconcildoc_gid = '" + values.repayreconcildoc_gid + "' ";
                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnresult == 1)
                {
                    msSQL = " delete from brs_tmp_trepayment " +
                           " where repayreconcildoc_gid = '" + values.repayreconcildoc_gid + "' ";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                values.status = true;
                values.message = "Repayment data inserted successfully..!";
                dt_datatable.Dispose();
            }
            else
            {
                values.status = false;
                values.message = "Error occured..!";
            }
        }
        //public DataTable ExcelToDataTable(string FileName, string sheet_name, string range)
        //{
        //    DataTable datatable = new DataTable();
        //    string lsConnectionString = string.Empty;
        //    string fileExtension = Path.GetExtension(FileName);

        //    if (fileExtension == ".xls")
        //    {
        //        lsConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileName + ";" + "Extended Properties='Excel 8.0;HDR=YES;'";
        //    }
        //    else if (fileExtension == ".xlsx")
        //    {
        //        lsConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;MAXSCANROWS=0';";
        //    }

        //    using (OleDbConnection objConn = new OleDbConnection(lsConnectionString))
        //    {
        //        objConn.Open();
        //        OleDbCommand cmd = new OleDbCommand();
        //        OleDbDataAdapter oleda = new OleDbDataAdapter();
        //        DataSet ds = new DataSet();

        //        cmd.Connection = objConn;
        //        cmd.CommandType = CommandType.Text;

        //        // Modify the query to include the specified sheet_name and range
        //        cmd.CommandText = "SELECT * FROM [" + sheet_name + range + "]";

        //        oleda = new OleDbDataAdapter(cmd);
        //        oleda.Fill(ds, "excelData");
        //        datatable = ds.Tables["excelData"];

        //        objConn.Close();
        //    }

        //    return datatable;
        //}

        public DataTable ExcelToDataTable(string FileName, string sheet_name, string range)
        {
            DataTable datatable = new DataTable();
            int totalSheet = 1;
            string lsConnectionString = string.Empty;
            string fileExtension = Path.GetExtension(FileName);
            if (fileExtension == ".xls")
            {
                lsConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileName + ";" + "Extended Properties='Excel 8.0;HDR=YES;'";
            }
            else if (fileExtension == ".xlsx")
            {
                lsConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;MAXSCANROWS=0';";
            }

            using (OleDbConnection objConn = new OleDbConnection(lsConnectionString))
            {
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand();
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                DataSet ds = new DataSet();
                DataTable dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetName = string.Empty;
                if (dt != null)
                {
                    var tempDataTable = (from dataRow in dt.AsEnumerable()
                                         where !dataRow["TABLE_NAME"].ToString().Contains("FilterDatabase")
                                         select dataRow).CopyToDataTable();
                    dt = tempDataTable;
                    totalSheet = dt.Rows.Count;
                    sheetName = dt.Rows[0]["TABLE_NAME"].ToString();
                }
                sheetName = sheet_name.Replace("'", "").Trim() + range;
                cmd.Connection = objConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [" + sheetName + "$" + range + "]"; // Separate sheet name and range

                oleda = new OleDbDataAdapter(cmd);
                oleda.Fill(ds, "excelData");

                 datatable = ds.Tables["excelData"];
                objConn.Close();
            }
            return datatable;
        }
    }

}