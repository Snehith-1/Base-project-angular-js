using ems.brs.Models;
using ems.storage.Functions;
using ems.utilities.Functions;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Util;

namespace ems.brs.Dataacess
{
    /// <summary>
    ///To upload ICICI Statement it digitize the LMS trnascation to get matched/Nonmatched  cases
    /// </summary>
    /// <remarks>Written by Motches,Poornima</remarks>
    public class DaIciciReconcillation
    {
        string bankrepaytransc_gid;
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        string msSQL, msGetGid, msGetGid1, lsrepayment_amount, lsaccount_number, lsurn_no, lstransactionid, lstransaction_date, lsrepayment_date, lsbrs_gid, lsbanktransac_name, msGetGid2, lsbanktrans_gid, lsbankremaining_amount, lsknockoff_status;
        OdbcDataReader objODBCDatareader;
        dbconn objdbconn = new dbconn();
        HttpPostedFile httpPostedFile;
        string lspath, endRange, excelRange, lsremarks;
        int mnresult;
        int rowCount, columnCount;
        string lsbankconfig_gidrange, lstransc_idrange, lstrn_daterange,
          lsacc_norange, lsvalue_daterange, lspayment_daterange, lstransact_particularsrange,
         lsdebit_amtrange, lscredit_amtrange, lstransact_valrange, lsremarksrange,
         lscustref_norange, lsbranch_namerange, lsbalance_amtrange, lschq_norange, lscr_drrange, lsknockoffby_financerange;
        string lstransact_particulars, lscheqref, lstransact_val, lsdrcr, lsbalance, lsvalue_date, lscredit_amt, lsdebit_amt;
        string lstrn_date, lstrndate, lspayment_date, lscustref_no, lsacc_no, lsbranch, lstransc_id, lsknockofffin;
        int lstransact_valcolumn, lscr_drcolumn, lsvalue_datecolumn, lstransc_idcolumn;
        //ExcelPackage xlPackage = new ExcelPackage();
        ExcelPackage xlPackage;
        int lstrndatastart_rowrange;
        int lstrn_datecolumn;
        int lstrn_daterow;
        int lspayment_datecolumn;
        int lstransact_particularscolumn;
        int lscheqref_column;
        int lsdebit_amtcolumn;
        int lscredit_amtcolumn;
        int lsbalance_column;
        DateTime dt;
        decimal remaining_amount;
        string lsconverted_date;
        FileStream filexlsx;
        FileStream filexlsx1;
        ExcelWorksheet worksheet;
        string FileExtension;
        string lsfilepath, lsfile_gid;
        int lscustref_norow, lscustref_nocolumn, lsacc_norow, lsacc_nocolumn, lsbranch_namerow, lsbranch_namecolumn, lsknockofffin_column;
        public bool DaPostBRSExcelSample(HttpRequest httpRequest, MdlIciciReconcillation objfilename, string employee_gid, string user_gid)
        {
            Icici_upload_list objdocumentmodel = new Icici_upload_list();
            HttpFileCollection httpFileCollection;
            DataTable dt = null;
           lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;          
            MemoryStream msxlsx = new MemoryStream();
            MemoryStream mscsvxlsx = new MemoryStream();
            String path = lspath;


            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "BRS/Icicireconcildoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                        string lsbank_gid = httpRequest.Form["brsbank_gid"].ToString();
                        string lsbank_name = httpRequest.Form["bank_name"].ToString();
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
                            objfilename.message = "File Format Not Supported..!'";
                            return false;
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
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "BRS/Icicireconcildoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);

                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "BRS/Icicireconcildoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        file.Close();
                        ms.Close();
                        msxlsx.Close();
                        mscsvxlsx.Close();

                        msGetGid = objcmnfunctions.GetMasterGID("BRSD");

                        msSQL = " insert into brs_trn_treconcillationdocument(" +
                                 " reconcildoc_gid," +
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
                        if (mnresult == 1)
                        {
                            objfilename.status = true;
                            objfilename.message = "Document uploaded successfully..!";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error occured..!";
                        }



                        msSQL = "select bankconfig_gid, transc_id, datastart_row, acc_no,  trn_date, value_date, payment_date," +
                            " transact_particulars, debit_amt, credit_amt, transact_val, remarks, custref_no, branch_name, balance_amt," +
                            " chq_no, created_by, created_date, cr_dr,knockoffby_finance " +
            "from brs_mst_tbankconfiguration where brsbank_gid = '" + lsbank_gid + "' ";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {

                            lsbankconfig_gidrange = objODBCDatareader["bankconfig_gid"].ToString();
                            lstransc_idrange = objODBCDatareader["transc_id"].ToString();
                            lstrndatastart_rowrange = Convert.ToInt32(objODBCDatareader["datastart_row"].ToString());
                            lstrn_daterange = objODBCDatareader["trn_date"].ToString();
                            lsacc_norange = objODBCDatareader["acc_no"].ToString();
                            lsvalue_daterange = objODBCDatareader["value_date"].ToString();
                            lspayment_daterange = objODBCDatareader["payment_date"].ToString();
                            lstransact_particularsrange = objODBCDatareader["transact_particulars"].ToString();
                            lsdebit_amtrange = objODBCDatareader["debit_amt"].ToString();
                            lscredit_amtrange = objODBCDatareader["credit_amt"].ToString();
                            lstransact_valrange = objODBCDatareader["transact_val"].ToString();
                            lsremarksrange = objODBCDatareader["remarks"].ToString();
                            lscustref_norange = objODBCDatareader["custref_no"].ToString();
                            lsbranch_namerange = objODBCDatareader["branch_name"].ToString();
                            lsbalance_amtrange = objODBCDatareader["balance_amt"].ToString();
                            lschq_norange = objODBCDatareader["chq_no"].ToString();
                            lscr_drrange = objODBCDatareader["cr_dr"].ToString();
                            lsknockoffby_financerange = objODBCDatareader["knockoffby_finance"].ToString();

                        }
                        string[] splits = lstrn_daterange.Split(',');
                        string[] splits1 = lstransact_particularsrange.Split(',');
                        string[] splits2 = lschq_norange.Split(',');
                        string[] splits3 = lsacc_norange.Split(',');
                        string[] splits4 = lsbranch_namerange.Split(',');
                        string[] splits5 = lstransact_valrange.Split(',');
                        string[] splits6 = lscr_drrange.Split(',');
                        string[] splits7 = lsbalance_amtrange.Split(',');
                        string[] splits8 = lsvalue_daterange.Split(',');
                        string[] splits9 = lstransc_idrange.Split(',');
                        string[] splits10 = lscustref_norange.Split(',');
                        string[] splits12 = lsknockoffby_financerange.Split(',');


                        lscustref_norow = (Convert.ToInt32(splits10[0]) == 0) ? 1 : Convert.ToInt32(splits10[0]);
                        lscustref_nocolumn = (Convert.ToInt32(splits10[1]) == 0) ? 1 : Convert.ToInt32(splits10[1]);
                        lsacc_norow = (Convert.ToInt32(splits3[0]) == 0) ? 1 : Convert.ToInt32(splits3[0]);
                        lsacc_nocolumn = (Convert.ToInt32(splits3[1]) == 0) ? 1 : Convert.ToInt32(splits3[1]);
                        lsbranch_namerow = (Convert.ToInt32(splits4[0]) == 0) ? 1 : Convert.ToInt32(splits4[0]);
                        lsbranch_namecolumn = (Convert.ToInt32(splits4[1]) == 0) ? 1 : Convert.ToInt32(splits4[1]);






                        if (worksheet.Cells[lsacc_norow, lsacc_nocolumn].Value != null)
                        {
                            if ((!string.IsNullOrWhiteSpace(worksheet.Cells[lsacc_norow, lsacc_nocolumn].Value.ToString()) || (worksheet.Cells[lsacc_norow, lsacc_nocolumn].Value.ToString() != "")))
                            {
                                lsacc_no = worksheet.Cells[lsacc_norow, lsacc_nocolumn].Value.ToString();

                            }
                        }


                        int trancount_total = 0;
                        for (int row = lstrndatastart_rowrange; row <= rowCount; row++)

                        {
                            trancount_total++;
                        }

                        int decsc_row = lstrndatastart_rowrange;
                        lstrn_datecolumn = Convert.ToInt32(splits[1]);
                        lstransact_particularscolumn = Convert.ToInt32(splits1[1]);
                        lscheqref_column = Convert.ToInt32(splits2[1]);
                        lsbalance_column = Convert.ToInt32(splits7[1]);
                        lstransc_idcolumn = Convert.ToInt32(splits9[1]);
                        lscr_drcolumn = Convert.ToInt32(splits6[1]);
                        lsvalue_datecolumn = Convert.ToInt32(splits8[1]);
                        lstransact_valcolumn = Convert.ToInt32(splits5[1]);
                        lscustref_nocolumn = Convert.ToInt32(splits10[1]);
                        lsknockofffin_column = Convert.ToInt32(splits12[1]);

                        worksheet.Cells.DefaultIfEmpty();



                        for (i = 1; i <= trancount_total; i++)

                        {
                            if (worksheet.Cells[decsc_row, lstrn_datecolumn].Value != null)
                            {



                                if (worksheet.Cells[decsc_row, lstrn_datecolumn].Value != null)
                                {
                                    if ((!string.IsNullOrWhiteSpace(worksheet.Cells[decsc_row, lstrn_datecolumn].Value.ToString()) || (worksheet.Cells[decsc_row, lstrn_datecolumn].Value.ToString() != "")))
                                    {

                                        string lstrn_date1 = worksheet.Cells[decsc_row, lstrn_datecolumn].Value.ToString().TrimEnd();
                                        try
                                        {
                                            lstrn_date = objcmnfunctions.GetDateFormat(lstrn_date1);
                                        }
                                        catch
                                        {
                                            lstrn_date = objcmnfunctions.GetDateFormat(lstrn_date1);

                                        }





                                    }
                                }
                                else
                                {
                                    lstrn_date = "";
                                }
                                if (worksheet.Cells[decsc_row, lstransact_particularscolumn].Value != null)
                                {
                                    if ((!string.IsNullOrWhiteSpace(worksheet.Cells[decsc_row, lstransact_particularscolumn].Value.ToString()) || (worksheet.Cells[decsc_row, lstransact_particularscolumn].Value.ToString() != "")))
                                    {
                                        lstransact_particulars = worksheet.Cells[decsc_row, lstransact_particularscolumn].Value.ToString();
                                    }
                                }
                                else
                                {
                                    lstransact_particulars = "";
                                }
                                if (worksheet.Cells[decsc_row, lscheqref_column].Value != null)
                                {
                                    if ((!string.IsNullOrWhiteSpace(worksheet.Cells[decsc_row, lscheqref_column].Value.ToString()) || (worksheet.Cells[decsc_row, lscheqref_column].Value.ToString() != "")))
                                    {

                                        lscheqref = worksheet.Cells[decsc_row, lscheqref_column].Value.ToString();
                                    }
                                }
                                else
                                {
                                    lscheqref = "";
                                }
                                if (worksheet.Cells[decsc_row, lstransc_idcolumn].Value != null)
                                {
                                    if ((!string.IsNullOrWhiteSpace(worksheet.Cells[decsc_row, lstransc_idcolumn].Value.ToString()) || (worksheet.Cells[decsc_row, lstransc_idcolumn].Value.ToString() != "")))
                                    {
                                        lstransc_id = worksheet.Cells[decsc_row, lstransc_idcolumn].Value.ToString();
                                    }
                                }
                                else
                                {
                                    lstransc_id = "";
                                }
                                if (worksheet.Cells[decsc_row, lscr_drcolumn].Value != null)
                                {
                                    if ((!string.IsNullOrWhiteSpace(worksheet.Cells[decsc_row, lscr_drcolumn].Value.ToString()) || (worksheet.Cells[decsc_row, lscr_drcolumn].Value.ToString() != "")))
                                    {

                                        lsdrcr = worksheet.Cells[decsc_row, lscr_drcolumn].Value.ToString();
                                    }
                                }
                                else
                                {
                                    lsdrcr = "";
                                }
                                if (worksheet.Cells[decsc_row, lsbalance_column].Value != null)
                                {
                                    if ((!string.IsNullOrWhiteSpace(worksheet.Cells[decsc_row, lsbalance_column].Value.ToString()) || (worksheet.Cells[decsc_row, lsbalance_column].Value.ToString() != "")))
                                    {

                                        lsbalance = worksheet.Cells[decsc_row, lsbalance_column].Value.ToString();
                                    }
                                }
                                else
                                {
                                    lsbalance = "";
                                }
                                if (worksheet.Cells[decsc_row, lsvalue_datecolumn].Value != null)
                                {
                                    if ((!string.IsNullOrWhiteSpace(worksheet.Cells[decsc_row, lsvalue_datecolumn].Value.ToString()) || (worksheet.Cells[decsc_row, lsvalue_datecolumn].Value.ToString() != "")))
                                    {
                                        string lsvalue_date1 = worksheet.Cells[decsc_row, lsvalue_datecolumn].Value.ToString();
                                        try
                                        {
                                            lsvalue_date = objcmnfunctions.GetDateFormat(lsvalue_date1);

                                        }
                                        catch
                                        {
                                            lsvalue_date = objcmnfunctions.GetDateFormat(lsvalue_date1);

                                        }

                                    }
                                }
                                else
                                {
                                    lsvalue_date = "";
                                }

                                if (worksheet.Cells[decsc_row, lstransact_valcolumn].Value != null)
                                {
                                    if ((!string.IsNullOrWhiteSpace(worksheet.Cells[decsc_row, lstransact_valcolumn].Value.ToString()) || (worksheet.Cells[decsc_row, lstransact_valcolumn].Value.ToString() != "")))
                                    {
                                        lstransact_val = worksheet.Cells[decsc_row, lstransact_valcolumn].Value.ToString();
                                    }
                                }
                                else
                                {
                                    lstransact_val = "";
                                }
                                if (worksheet.Cells[decsc_row, lsknockofffin_column].Value != null)
                                {
                                    if ((!string.IsNullOrWhiteSpace(worksheet.Cells[decsc_row, lsknockofffin_column].Value.ToString()) || (worksheet.Cells[decsc_row, lsknockofffin_column].Value.ToString() != "")))
                                    {
                                        lsknockofffin = worksheet.Cells[decsc_row, lsknockofffin_column].Value.ToString();
                                    }
                                }
                                else
                                {
                                    lsknockofffin = "null";
                                }
                                //int spaceIndex = lstrn_date.IndexOf(' ');
                                //string dateStr = lstrn_date.Substring(0, spaceIndex);
                                //string timeStr = lstrn_date.Substring(spaceIndex + 1);
                                //string dateStr1 = lsvalue_date.Substring(0, spaceIndex);
                                //string timeStr1 = lsvalue_date.Substring(spaceIndex + 1);
                                //if (dateStr != dateStr1)
                                //{
                                //    lstrn_date = dateStr1 + " " + timeStr;
                                //}
                                if (lsdrcr == "DR")
                                {
                                    lsdebit_amt = lstransact_val;
                                    lscredit_amt = "";
                                }
                                else
                                {
                                    lscredit_amt = lstransact_val;
                                    lsdebit_amt = "";
                                }


                                msSQL = " select count(*) as brsbank_flag from brs_mst_tbank " +
                                                         " where acc_no = '" + lsdocument_title.Replace("'", @"\'") + "' and " +
                                                       "  brsbank_gid = '" + lsbank_gid + "'";
                                int brsbank_flag = Convert.ToInt16(objdbconn.GetExecuteScalar(msSQL));

                                if (brsbank_flag == 0)
                                {
                                    lsbrs_gid = objcmnfunctions.GetMasterGID("BRSB");

                                }
                                else
                                {

                                    msSQL = " select bank_gid  from brs_mst_tbank " +
                                            " where acc_no = '" + lsdocument_title.Replace("'", @"\'") + "' and " +
                                            "  brsbank_gid = '" + lsbank_gid + "'";
                                    lsbrs_gid = objdbconn.GetExecuteScalar(msSQL);

                                }
                                if (brsbank_flag == 0)
                                {

                                    msSQL = " insert into brs_mst_tbank(" +
                                              "bank_gid," +
                                              "brsbank_gid," +
                                              "bank_name," +
                                              "acc_no," +
                                              "custref_no," +
                                              "branch_name," +
                                              "created_by," +
                                              "created_date)" +
                                               " values(" +
                                               "'" + lsbrs_gid + "'," +
                                                "'" + lsbank_gid + "'," +
                                               "'" + lsbank_name + "'," +
                                               "'" + lsdocument_title.Replace("'", @"\'") + "'," +
                                               "'" + lscustref_no + "'," +
                                               "'" + lsbranch + "'," +
                                               "'" + user_gid + "'," +
                                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                }
                                if (lsbank_name != "")
                                {
                                    lsbanktransac_name = lsbank_name.Substring(0, 4);
                                    msGetGid2 = objcmnfunctions.GetMasterGID("BNKT");
                                    lsbanktrans_gid = lsbanktransac_name + msGetGid2;
                                }
                                msSQL = " select count(*) from brs_trn_tbanktransactiondetails " +
                                                      " where transact_particulars = '" + lstransact_particulars.Trim().Replace("'", "") + "'" +
                                                      " and trn_date = '" + lstrn_date + "' " +
                                                      "and transact_val = '" + lstransact_val.Trim().Replace(",", "") + "' " +
                                                      " and transc_balance = '" + lsbalance.Trim() + "' ";

                                int lstransact_flag = Convert.ToInt16(objdbconn.GetExecuteScalar(msSQL));

                                if (lstransact_flag != 0)
                                {
                                    objfilename.status = false;
                                    objfilename.message = "Transaction Is Already Existed ...!";
                                }
                                else if (lsknockofffin.Trim() == "Yes" || lsknockofffin.Trim() == "yes")
                                {
                                    string lstrndate1 = null;

                                    msGetGid1 = objcmnfunctions.GetMasterGID("BRST");
                                    msSQL = " insert into brs_trn_tbanktransactiondetails(" +
                                        " banktransc_gid," +
                                        " banktransc_refno," +
                                        " bankconfig_gid," +
                                        " bank_gid ," +
                                        " brsbank_gid ," +
                                        " reconcildoc_gid ," +
                                        " trn_date ," +
                                        " value_date ," +
                                        " payment_date ," +
                                        "transact_particulars," +
                                        "debit_amt," +
                                        "credit_amt," +
                                        "remaining_amount," +
                                        "transact_val," +
                                        "remarks," +
                                        "cr_dr," +
                                        "chq_no," +
                                        "transc_balance," +
                                        " created_by," +
                                        " knockoffby_finance," +
                                        " knockoff_status," +
                                         " rule_number," +
                                        " created_date" +
                                        " )values(" +

                                    "'" + msGetGid1 + "'," +
                                    "'" + lsbanktrans_gid + "'," +
                                    "'" + lsbankconfig_gidrange + "'," +
                                    "'" + lsbrs_gid + "'," +
                                    "'" + lsbank_gid + "'," +
                                    "'" + msGetGid + "'," +
                                    "'" + lstrn_date + "'," +
                                    "'" + lsvalue_date + "'," +
                                    "null," +
                                    "'" + lstransact_particulars.Trim().Replace("'", "") + "'," +
                                    "'" + lsdebit_amt.Trim() + "'," +
                                      "'" + lscredit_amt.Trim() + "',";
                                    //"'" + lstransact_val.Trim().Replace(",", "") + "'," +
                                    if ((lstransact_val.Trim() == null) || (lstransact_val.Trim() == ""))
                                    {
                                        msSQL += "0.00,";
                                    }
                                    else
                                    {
                                        msSQL += "'" + lstransact_val.Trim().Replace(",", "") + "',";
                                    }
                                    msSQL += "'" + lstransact_val.Trim().Replace(",", "") + "'," +
                                    "null," +
                                    "'" + lsdrcr + "'," +
                                    "'" + lscheqref + "'," +
                                    "'" + lsbalance.Trim() + "'," +
                                    "'" + user_gid + "'," +
                                    "'" + lsknockofffin.Trim() + "'," +
                                    "'FinancePending'," +
                                     "'R0'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    if (mnresult == 1)
                                    {
                                        objfilename.status = true;
                                        objfilename.message = "Transaction data inserted successfully..!";
                                    }
                                }
                                else
                                {


                                    msGetGid1 = objcmnfunctions.GetMasterGID("BRST");
                                    msSQL = " insert into brs_trn_tbanktransactiondetails(" +
                                        " banktransc_gid," +
                                        " banktransc_refno," +
                                        " bankconfig_gid," +
                                        " bank_gid ," +
                                        " brsbank_gid ," +
                                        " reconcildoc_gid ," +
                                        " trn_date ," +
                                        " value_date ," +
                                         " payment_date ," +
                                         "transact_particulars," +
                                         "debit_amt," +
                                         "credit_amt," +
                                          "remaining_amount," +
                                          "transact_val," +
                                           "remarks," +
                                            "cr_dr," +
                                            "chq_no," +
                                           "transc_balance," +
                                        " created_by," +
                                        " knockoffby_finance," +
                                        " created_date" +
                                        " )values(" +

                                    "'" + msGetGid1 + "'," +
                                    "'" + lsbanktrans_gid + "'," +
                                    "'" + lsbankconfig_gidrange + "'," +
                                    "'" + lsbrs_gid + "'," +
                                     "'" + lsbank_gid + "'," +
                                    "'" + msGetGid + "'," +
                                    "'" + lstrn_date + "'," +
                                     "'" + lsvalue_date + "'," +
                                    "null," +
                                    "'" + lstransact_particulars.Trim().Replace("'", "") + "'," +
                                    "'" + lsdebit_amt.Trim() + "'," +
                                      "'" + lscredit_amt.Trim() + "',";
                                    //"'" + lstransact_val.Trim().Replace(",", "") + "'," +
                                    if ((lstransact_val.Trim() == null) || (lstransact_val.Trim() == ""))
                                    {
                                        msSQL += "0.00,";
                                    }
                                    else
                                    {
                                        msSQL += "'" + lstransact_val.Trim().Replace(",", "") + "',";
                                    }
                                    msSQL += "'" + lstransact_val.Trim().Replace(",", "") + "'," +
                                             "null," +
                                             "'" + lsdrcr + "'," +
                                             "'" + lscheqref + "'," +
                                               "'" + lsbalance.Trim() + "'," +
                                             "'" + user_gid + "'," +
                                             "'" + lsknockofffin.Trim() + "'," +
                                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    if (mnresult == 1)
                                    {
                                        msSQL = " update brs_trn_tbanktransactiondetails set transact_val= replace(transact_val,',','')  where transact_val like '%,%'  " +
                                                      " AND banktransc_gid = '" + msGetGid1 + "'";
                                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        msSQL = " update brs_trn_tbanktransactiondetails set transact_val= '0.00'  where transact_val ='' " +
                                                           " AND banktransc_gid = '" + msGetGid1 + "'";
                                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        try
                                        {
                                            Decimal count;
                                            count = 0;
                                            Decimal blank_count;
                                            blank_count = 0;

                                            //List<string> b_list = new List<string>();
                                            msSQL = "select DATE_FORMAT(trn_date, '%Y-%m-%d') as trn_date from brs_trn_tbanktransactiondetails where banktransc_gid = '" + msGetGid1 + "'";
                                            objODBCDatareader = objdbconn.GetDataReader(msSQL);

                                            if (objODBCDatareader.HasRows)
                                            {
                                                lstrndate = objODBCDatareader["trn_date"].ToString();
                                            }
                                            objODBCDatareader.Close();
                                            //select
                                            msSQL = "SELECT remarks,repayment_amount,account_number,transaction_id,urn_no, DATE_FORMAT(transaction_date, '%Y-%m-%d') as transaction_date, DATE_FORMAT(repayment_date, '%Y-%m-%d') as repayment_date " +
                                                 " FROM brs_mst_trepaymenttransaction  " +
                                                 " where  remarks = '" + lstransact_particulars.Trim().Replace("'", "") + "' and repayment_date='" + lstrndate + "'";
                                            dt_datatable = objdbconn.GetDataTable(msSQL);

                                            //b_list = dt_datatable.AsEnumerable().Select(p => p.Field<string>("remarks")).ToList();
                                            //string[] a_split = lstransact_particulars.Split(new Char[] { ' ', '-', '/' });
                                            if (dt_datatable.Rows.Count != 0)
                                            {
                                                FullyMatchedCase(user_gid);
                                                                                              
                                            }

                                            msSQL = "SELECT remarks " +
                                                     "FROM brs_mst_trepaymenttransaction where fullymatched_flag='N' ";

                                            dt_datatable = objdbconn.GetDataTable(msSQL);

                                            if (dt_datatable.Rows.Count != 0)
                                                {
                                                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                                                    {
                                                        lsremarks = (dr_datarow["remarks"].ToString());
                                                        bool lspartialstatus = lstransact_particulars.Trim().Replace("'", "").Contains(lsremarks);

                                                        if (lspartialstatus == true)
                                                        {
                                                            PartialMatchedCase(lsremarks, user_gid);
                                                        }

                                                    }
                                                }
                                            

                                        }
                                        catch (Exception ex)
                                        {

                                        }
                                        objfilename.status = true;
                                        objfilename.message = "Transaction data inserted successfully..!";
                                    }
                                    else
                                    {
                                        objfilename.status = false;
                                        objfilename.message = "Error occured..!";
                                    }
                                    dt_datatable.Dispose();
                                    //bool hasCount = false;
                                    //bool hasCount1 = false;
                                    //msSQL = "select banktransc_gid,banktransc_refno from brs_trn_tbanktransactiondetails where bankrepaytransc_gid <>'' and transact_particulars = '" + lstransact_particulars + "'  group by bankrepaytransc_gid HAVING COUNT(bankrepaytransc_gid)> 1 ";
                                    //objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    //if (objODBCDatareader.HasRows == true)
                                    //{
                                    //    hasCount = true;
                                    //}
                                    //objODBCDatareader.Close();
                                    //if (hasCount == true)
                                    //{
                                    //    msSQL = " SELECT bankrepaytransc_gid  FROM brs_mst_trepaymenttransaction  where remarks = '" + lstransact_particulars + "' ";

                                    //    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    //    if (objODBCDatareader.HasRows == true)
                                    //    {
                                    //        bankrepaytransc_gid = objODBCDatareader["bankrepaytransc_gid"].ToString();
                                    //    }
                                    //    objODBCDatareader.Close();
                                    //    msSQL = " update brs_trn_tbanktransactiondetails set knockoff_status='PartiallyMatched',knockoff_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', knockoff_flag='Y'  " +
                                    //                       " where bankrepaytransc_gid = '" + bankrepaytransc_gid + "'";
                                    //    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    //    msSQL = " update brs_mst_trepaymenttransaction set knockoff_status='PartiallyMatched' ,knockoff_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', knockoff_flag='Y' " +
                                    //                       " where bankrepaytransc_gid = '" + bankrepaytransc_gid + "'";
                                    //    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    //}
                                    //else
                                    //{
                                    //    msSQL = "select transact_particulars from brs_trn_tbanktransactiondetails where transact_particulars <>'' and transact_particulars = '" + lstransact_particulars + "'  group by transact_particulars HAVING COUNT(transact_particulars)> 1 ";
                                    //    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    //    if (objODBCDatareader.HasRows == true)
                                    //    {
                                    //        hasCount1 = true;
                                    //    }
                                    //    objODBCDatareader.Close();
                                    //    if (hasCount1 == true)
                                    //    {
                                    //        msSQL = " SELECT bankrepaytransc_gid  FROM brs_mst_trepaymenttransaction  where remarks = '" + lstransact_particulars + "' ";
                                    //        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    //        if (objODBCDatareader.HasRows == true)
                                    //        {
                                    //            bankrepaytransc_gid = objODBCDatareader["bankrepaytransc_gid"].ToString();
                                    //        }
                                    //        objODBCDatareader.Close();

                                    //        msSQL = " update brs_trn_tbanktransactiondetails set knockoff_status='PartiallyMatched',knockoff_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' , knockoff_flag='Y'  " +
                                    //                           " where bankrepaytransc_gid = '" + bankrepaytransc_gid + "'";
                                    //        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    //        msSQL = " update brs_mst_trepaymenttransaction set knockoff_status='PartiallyMatched' ,knockoff_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' , knockoff_flag='Y' " +
                                    //                           " where bankrepaytransc_gid = '" + bankrepaytransc_gid + "'";
                                    //        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    //    }


                                    //}
                                    //objODBCDatareader.Close();
                                }
                                lstransact_particulars = string.Empty;
                                lscheqref = string.Empty;
                                lstransact_val = string.Empty;
                                lsdrcr = string.Empty;
                                lsbalance = string.Empty;
                                lstrn_date = string.Empty;
                                lsvalue_date = string.Empty;
                                lscredit_amt = string.Empty;
                                lsdebit_amt = string.Empty;
                                lsknockofffin = string.Empty;
                                decsc_row++;
                            }
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
                objfilename.message = "Error occured..!";

            }
            return true;
        }
        public string FullyMatchedCase(string user_gid)
        {
            try
            {
                Decimal count;
                count = 0;
                Decimal blank_count;
                blank_count = 0;

                List<string> b_list = new List<string>();
                msSQL = "select DATE_FORMAT(trn_date, '%Y-%m-%d') as trn_date from brs_trn_tbanktransactiondetails where banktransc_gid = '" + msGetGid1 + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows)
                {
                    lstrndate = objODBCDatareader["trn_date"].ToString();
                }
                objODBCDatareader.Close();

                //select
                msSQL = "SELECT remarks,repayment_amount,account_number,transaction_id,urn_no, DATE_FORMAT(transaction_date, '%Y-%m-%d') as transaction_date, DATE_FORMAT(repayment_date, '%Y-%m-%d') as repayment_date " +
                     " FROM brs_mst_trepaymenttransaction  " +
                     " where  remarks = '" + lstransact_particulars.Trim().Replace("'", "") + "' and repayment_date='" + lstrndate + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                //msSQL = "select transact_particulars from brs_trn_tbanktransactiondetails  " +
                //    " where created_by= '" + user_gid + "'";
                //dt_datatable = objdbconn.GetDataTable(msSQL);

                //b_list = dt_datatable.AsEnumerable().Select(p => p.Field<string>("remarks")).ToList();
                //string[] a_split = lstransact_particulars.Split(new Char[] { ' ', '-', '/' });
                if (dt_datatable.Rows.Count != 0)
                {
                   
                    foreach (DataRow datarow in dt_datatable.Rows)
                    {
                        string b = datarow["remarks"].ToString();


                        //foreach (var a in a_split)
                        //{
                        //    if (string.IsNullOrWhiteSpace(a))
                        //    {


                        //        blank_count = blank_count + 1;
                        //    }
                        //    else
                        //    {
                        //        int verify = b.IndexOf(a);

                        //        if (verify != -1)
                        //        {
                        //            count = count + 1;
                        //        }
                        //    }

                        //}
                        lsrepayment_amount = datarow["repayment_amount"].ToString();
                        lsaccount_number = datarow["account_number"].ToString();
                        lstransactionid = datarow["transaction_id"].ToString();
                        lsurn_no = datarow["urn_no"].ToString();
                        lstransaction_date = datarow["transaction_date"].ToString();
                        lsrepayment_date = datarow["repayment_date"].ToString();

                        //Decimal totalcount = a_split.Count() - blank_count;
                        //double percentage = Math.Round((double)((count / totalcount) * 100), 2);
                        msSQL = " SELECT  remaining_amount,knockoff_status FROM brs_trn_tbanktransactiondetails  " +
                                        " where banktransc_gid = '" + msGetGid1 + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsbankremaining_amount = objODBCDatareader["remaining_amount"].ToString();
                            lsknockoff_status = objODBCDatareader["knockoff_status"].ToString();

                        }
                        var convertDecimalrepaymentamount = Convert.ToDecimal(lsrepayment_amount.Trim());
                        var convertDecimalbankremainingamount = Convert.ToDecimal(lsbankremaining_amount.Trim());


                        if ((convertDecimalrepaymentamount == convertDecimalbankremainingamount) && ((lsdrcr == "CR") || (lsdrcr == "Cr")) && (lsknockoff_status == "Pending"))

                        {

                            msSQL = " SELECT  transaction_id FROM brs_mst_trepaymenttransaction  " +
                                        " where remarks = '" + b + "'";
                            string lstransaction_id = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = " update brs_mst_trepaymenttransaction set knockoff_status='Matched' ,fullymatched_flag='Y' , knockoff_flag='Y' ,knockoff_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', banktransc_gid = '" + msGetGid1 + "' " +
                                      " where remarks = '" + b + "'";
                            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            msSQL = " SELECT  bankrepaytransc_gid FROM brs_mst_trepaymenttransaction  " +
                                      " where remarks = '" + b + "'";
                            string lsrepayment_id = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = " update brs_trn_tbanktransactiondetails set knockoff_status='Matched' ,fullymatched_flag='Y' ,rule_number ='R1',knockoff_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', knockoff_flag='Y',remaining_amount='0.00', bankrepaytransc_gid = '" + lsrepayment_id + "',closed_by= '" + user_gid + "' " +
                              " where banktransc_gid = '" + msGetGid1 + "' ";
                            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

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
                                     "'" + msGetGid1 + "'," +
                                     "'Technology'," +
                                     "'SERM2022062277'," +
                                     "'autoapproval.samfin@samunnati.com'," +
                                     "'BRAA0002'," +
                                     "'collection'," +
                                     "'" + convertDecimalrepaymentamount + "'," +
                                     "'Booked in LMS / FA'," +
                                     "'AutoClosed'," +
                                      "'" + lsaccount_number + "'," +
                                     "'" + lstransactionid + "'," +
                                     "'" + lstransaction_date + "'," +
                                     "'" + lsrepayment_date + "'," +
                                     "'" + lsurn_no + "'," +
                                     "'" + user_gid + "'," +
                                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                        //var convertDecimalrepaymentamount = Convert.ToDecimal(lsrepayment_amount);
                        //var convertDecimalcreditamount = Convert.ToDecimal(lstransact_val.Trim());


                        if ((convertDecimalrepaymentamount < convertDecimalbankremainingamount) && (lsknockoff_status == "Pending"))
                        {
                            if (lsrepayment_amount == null || lsrepayment_amount == "")
                            {
                                lsrepayment_amount = "0";
                            }
                            var convertDecimalrepaymentamountremaining = Convert.ToDecimal(lsrepayment_amount);


                            msSQL = " SELECT  sum(amount) FROM brs_trn_tunrecontransactiondetails" +
                                                                               " where action_name ='Booked in LMS / FA' and banktransc_gid = '" + msGetGid1 + "'";
                            string lsamount = objdbconn.GetExecuteScalar(msSQL);

                            lsamount = (lsamount == null || lsamount == "") ? "0" : lsamount;

                            decimal convertDecimallsamount = Convert.ToDecimal(lsamount) + convertDecimalrepaymentamountremaining;
                            remaining_amount = convertDecimalbankremainingamount - convertDecimalrepaymentamountremaining;


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
                                         "'" + msGetGid1 + "'," +
                                         "'Technology'," +
                                         "'SERM2022062277'," +
                                         "'autoapproval.samfin@samunnati.com'," +
                                         "'BRAA0002'," +
                                         "'collection'," +
                                         "'" + convertDecimalrepaymentamount + "'," +
                                         "'" + remaining_amount + "'," +
                                         "'Booked in LMS / FA'," +
                                         "'AutoClosed'," +                                      
                                         "'" + lsaccount_number + "'," +
                                         "'" + lstransactionid + "'," +
                                         "'" + lstransaction_date + "'," +
                                         "'" + lsrepayment_date + "'," +
                                         "'" + lsurn_no + "'," +
                                         "'" + user_gid + "'," +
                                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " update brs_trn_tbanktransactiondetails set adjust_amount = '" + convertDecimallsamount + "',fullymatched_flag='Y' ,rule_number ='R1',brstransactiondetailsadvice_flag = 'Y' where banktransc_gid='" + msGetGid1 + "'";
                            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            msSQL = " update brs_mst_trepaymenttransaction set fullymatched_flag='Y' " +
                                    " where remarks = '" + b + "'";
                            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " update brs_trn_tbanktransactiondetails set remaining_amount='" + remaining_amount + "'" +
                                    " where banktransc_gid = '" + msGetGid1 + "'";
                            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        if ((convertDecimalrepaymentamount > convertDecimalbankremainingamount) && (lsknockoff_status == "Pending"))
                        {
                            if (lsrepayment_amount == null || lsrepayment_amount == "")
                            {
                                lsrepayment_amount = "0";
                            }
                            var convertDecimalrepaymentamountremaining = Convert.ToDecimal(lsrepayment_amount);


                            msSQL = " SELECT  sum(amount) FROM brs_trn_tunrecontransactiondetails" +
                                                                               " where action_name ='Booked in LMS / FA' and banktransc_gid = '" + msGetGid1 + "'";
                            string lsamount = objdbconn.GetExecuteScalar(msSQL);

                            lsamount = (lsamount == null || lsamount == "") ? "0" : lsamount;

                            decimal convertDecimallsamount = Convert.ToDecimal(lsamount) + convertDecimalrepaymentamountremaining;
                            remaining_amount = convertDecimalbankremainingamount - convertDecimalrepaymentamountremaining;


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
                                         "'" + msGetGid1 + "'," +
                                         "'Technology'," +
                                         "'SERM2022062277'," +
                                         "'autoapproval.samfin@samunnati.com'," +
                                         "'BRAA0002'," +
                                         "'collection'," +
                                         "'" + convertDecimalrepaymentamount + "'," +
                                         "'" + remaining_amount + "'," +
                                         "'Booked in LMS / FA'," +
                                         "'AutoClosed'," +
                                         "'" + lsaccount_number + "'," +
                                         "'" + lstransactionid + "'," +
                                         "'" + lstransaction_date + "'," +
                                         "'" + lsrepayment_date + "'," +
                                         "'" + lsurn_no + "'," +
                                         "'" + user_gid + "'," +
                                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            string negativeremainingamount = "-10.00";

                            decimal negativeremainingamountcheck = Convert.ToDecimal(negativeremainingamount);

                            if (remaining_amount >= negativeremainingamountcheck)
                            {
                                msSQL = " SELECT  transaction_id FROM brs_mst_trepaymenttransaction  " +
                                        " where remarks = '" + b + "'";
                                string lstransaction_id = objdbconn.GetExecuteScalar(msSQL);
                                msSQL = " update brs_mst_trepaymenttransaction set knockoff_status='Matched' ,fullymatched_flag='Y' , knockoff_flag='Y' ,knockoff_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', banktransc_gid = '" + msGetGid1 + "' " +
                                          " where remarks = '" + b + "'";
                                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                msSQL = " SELECT  bankrepaytransc_gid FROM brs_mst_trepaymenttransaction  " +
                                          " where remarks = '" + b + "'";
                                string lsrepayment_id = objdbconn.GetExecuteScalar(msSQL);
                                msSQL = " update brs_trn_tbanktransactiondetails set knockoff_status='Matched' ,fullymatched_flag='Y' ,rule_number ='R1',knockoff_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', knockoff_flag='Y', bankrepaytransc_gid = '" + lsrepayment_id + "',closed_by= '" + user_gid + "' " +
                                  " where banktransc_gid = '" + msGetGid1 + "' ";
                                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                msSQL = " update brs_trn_tbanktransactiondetails set adjust_amount = '" + convertDecimallsamount + "',remaining_amount='" + remaining_amount + "',brstransactiondetailsadvice_flag = 'Y' where banktransc_gid='" + msGetGid1 + "'";
                                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            }
                            else
                            {
                                msSQL = " update brs_trn_tbanktransactiondetails set adjust_amount = '" + convertDecimallsamount + "',fullymatched_flag='Y' ,rule_number ='R1',brstransactiondetailsadvice_flag = 'Y' where banktransc_gid='" + msGetGid1 + "'";
                                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                msSQL = " update brs_mst_trepaymenttransaction set fullymatched_flag='Y' " +
                                        " where remarks = '" + b + "'";
                                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                msSQL = " update brs_trn_tbanktransactiondetails set remaining_amount='" + remaining_amount + "'" +
                                        " where banktransc_gid = '" + msGetGid1 + "'";
                                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            }
                        }

                        count = 0;
                        blank_count = 0;
                    }

                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {

            }
            return user_gid;
        }

        public string PartialMatchedCase(string lsremarks,string user_gid)
        {
            try
            {
                Decimal count;
                count = 0;
                Decimal blank_count;
                blank_count = 0;

                List<string> b_list = new List<string>();
                msSQL = "select DATE_FORMAT(trn_date, '%Y-%m-%d') as trn_date from brs_trn_tbanktransactiondetails where banktransc_gid = '" + msGetGid1 + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows)
                {
                    lstrndate = objODBCDatareader["trn_date"].ToString();
                }
                objODBCDatareader.Close();

                //select
                msSQL = "SELECT remarks,repayment_amount,account_number,transaction_id,urn_no, DATE_FORMAT(transaction_date, '%Y-%m-%d') as transaction_date, DATE_FORMAT(repayment_date, '%Y-%m-%d') as repayment_date " +
                     " FROM brs_mst_trepaymenttransaction  " +
                     " where  remarks = '" + lsremarks.Trim().Replace("'", "") + "' and repayment_date='" + lstrndate + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

               
                if (dt_datatable.Rows.Count != 0)
                {

                    foreach (DataRow datarow in dt_datatable.Rows)
                    {
                        string b = datarow["remarks"].ToString();
                                               
                        lsrepayment_amount = datarow["repayment_amount"].ToString();
                        lsaccount_number = datarow["account_number"].ToString();
                        lstransactionid = datarow["transaction_id"].ToString();
                        lsurn_no = datarow["urn_no"].ToString();
                        lstransaction_date = datarow["transaction_date"].ToString();
                        lsrepayment_date = datarow["repayment_date"].ToString();

                      
                        msSQL = " SELECT  remaining_amount,knockoff_status FROM brs_trn_tbanktransactiondetails  " +
                                        " where banktransc_gid = '" + msGetGid1 + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsbankremaining_amount = objODBCDatareader["remaining_amount"].ToString();
                            lsknockoff_status = objODBCDatareader["knockoff_status"].ToString();

                        }
                        var convertDecimalrepaymentamount = Convert.ToDecimal(lsrepayment_amount.Trim());
                        var convertDecimalbankremainingamount = Convert.ToDecimal(lsbankremaining_amount.Trim());


                        if ((convertDecimalrepaymentamount == convertDecimalbankremainingamount) && ((lsdrcr == "CR") || (lsdrcr == "Cr")) && (lsknockoff_status == "Pending"))

                        {

                            msSQL = " SELECT  transaction_id FROM brs_mst_trepaymenttransaction  " +
                                        " where remarks = '" + b + "'";
                            string lstransaction_id = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = " update brs_mst_trepaymenttransaction set knockoff_status='Matched' , knockoff_flag='Y' ,knockoff_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', banktransc_gid = '" + msGetGid1 + "' " +
                                      " where remarks = '" + b + "'";
                            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            msSQL = " SELECT  bankrepaytransc_gid FROM brs_mst_trepaymenttransaction  " +
                                      " where remarks = '" + b + "'";
                            string lsrepayment_id = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = " update brs_trn_tbanktransactiondetails set knockoff_status='Matched' ,rule_number ='R2',knockoff_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', knockoff_flag='Y',remaining_amount='0.00', bankrepaytransc_gid = '" + lsrepayment_id + "',closed_by= '" + user_gid + "' " +
                              " where banktransc_gid = '" + msGetGid1 + "' ";
                            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

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
                                     "'" + msGetGid1 + "'," +
                                     "'Technology'," +
                                     "'SERM2022062277'," +
                                     "'autoapproval.samfin@samunnati.com'," +
                                     "'BRAA0002'," +
                                     "'collection'," +
                                     "'" + convertDecimalrepaymentamount + "'," +
                                     "'Booked in LMS / FA'," +
                                     "'AutoClosed'," +
                                      "'" + lsaccount_number + "'," +
                                     "'" + lstransactionid + "'," +
                                     "'" + lstransaction_date + "'," +
                                     "'" + lsrepayment_date + "'," +
                                     "'" + lsurn_no + "'," +
                                     "'" + user_gid + "'," +
                                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                        //var convertDecimalrepaymentamount = Convert.ToDecimal(lsrepayment_amount);
                        //var convertDecimalcreditamount = Convert.ToDecimal(lstransact_val.Trim());


                        if ((convertDecimalrepaymentamount < convertDecimalbankremainingamount) && (lsknockoff_status == "Pending"))
                        {
                            if (lsrepayment_amount == null || lsrepayment_amount == "")
                            {
                                lsrepayment_amount = "0";
                            }
                            var convertDecimalrepaymentamountremaining = Convert.ToDecimal(lsrepayment_amount);


                            msSQL = " SELECT  sum(amount) FROM brs_trn_tunrecontransactiondetails" +
                                                                               " where action_name ='Booked in LMS / FA' and banktransc_gid = '" + msGetGid1 + "'";
                            string lsamount = objdbconn.GetExecuteScalar(msSQL);

                            lsamount = (lsamount == null || lsamount == "") ? "0" : lsamount;

                            decimal convertDecimallsamount = Convert.ToDecimal(lsamount) + convertDecimalrepaymentamountremaining;
                            remaining_amount = convertDecimalbankremainingamount - convertDecimalrepaymentamountremaining;


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
                                         "'" + msGetGid1 + "'," +
                                         "'Technology'," +
                                         "'SERM2022062277'," +
                                         "'autoapproval.samfin@samunnati.com'," +
                                         "'BRAA0002'," +
                                         "'collection'," +
                                         "'" + convertDecimalrepaymentamount + "'," +
                                         "'" + remaining_amount + "'," +
                                         "'Booked in LMS / FA'," +
                                         "'AutoClosed'," +
                                         "'" + lsaccount_number + "'," +
                                         "'" + lstransactionid + "'," +
                                         "'" + lstransaction_date + "'," +
                                         "'" + lsrepayment_date + "'," +
                                         "'" + lsurn_no + "'," +
                                         "'" + user_gid + "'," +
                                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " update brs_trn_tbanktransactiondetails set adjust_amount = '" + convertDecimallsamount + "',rule_number ='R2',brstransactiondetailsadvice_flag = 'Y' where banktransc_gid='" + msGetGid1 + "'";
                            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " update brs_trn_tbanktransactiondetails set remaining_amount='" + remaining_amount + "'" +
                                    " where banktransc_gid = '" + msGetGid1 + "'";
                            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        if ((convertDecimalrepaymentamount > convertDecimalbankremainingamount) && (lsknockoff_status == "Pending"))
                        {
                            if (lsrepayment_amount == null || lsrepayment_amount == "")
                            {
                                lsrepayment_amount = "0";
                            }
                            var convertDecimalrepaymentamountremaining = Convert.ToDecimal(lsrepayment_amount);


                            msSQL = " SELECT  sum(amount) FROM brs_trn_tunrecontransactiondetails" +
                                                                               " where action_name ='Booked in LMS / FA' and banktransc_gid = '" + msGetGid1 + "'";
                            string lsamount = objdbconn.GetExecuteScalar(msSQL);

                            lsamount = (lsamount == null || lsamount == "") ? "0" : lsamount;

                            decimal convertDecimallsamount = Convert.ToDecimal(lsamount) + convertDecimalrepaymentamountremaining;
                            remaining_amount = convertDecimalbankremainingamount - convertDecimalrepaymentamountremaining;


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
                                         "'" + msGetGid1 + "'," +
                                         "'Technology'," +
                                         "'SERM2022062277'," +
                                         "'autoapproval.samfin@samunnati.com'," +
                                         "'BRAA0002'," +
                                         "'collection'," +
                                         "'" + convertDecimalrepaymentamount + "'," +
                                         "'" + remaining_amount + "'," +
                                         "'Booked in LMS / FA'," +
                                         "'AutoClosed'," +
                                         "'" + lsaccount_number + "'," +
                                         "'" + lstransactionid + "'," +
                                         "'" + lstransaction_date + "'," +
                                         "'" + lsrepayment_date + "'," +
                                         "'" + lsurn_no + "'," +
                                         "'" + user_gid + "'," +
                                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            string negativeremainingamount = "-10.00";

                            decimal negativeremainingamountcheck = Convert.ToDecimal(negativeremainingamount);

                            if (remaining_amount >= negativeremainingamountcheck)
                            {
                                msSQL = " SELECT  transaction_id FROM brs_mst_trepaymenttransaction  " +
                                        " where remarks = '" + b + "'";
                                string lstransaction_id = objdbconn.GetExecuteScalar(msSQL);
                                msSQL = " update brs_mst_trepaymenttransaction set knockoff_status='Matched' ,knockoff_flag='Y' ,knockoff_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', banktransc_gid = '" + msGetGid1 + "' " +
                                          " where remarks = '" + b + "'";
                                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                msSQL = " SELECT  bankrepaytransc_gid FROM brs_mst_trepaymenttransaction  " +
                                          " where remarks = '" + b + "'";
                                string lsrepayment_id = objdbconn.GetExecuteScalar(msSQL);
                                msSQL = " update brs_trn_tbanktransactiondetails set knockoff_status='Matched' ,rule_number ='R2',knockoff_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', knockoff_flag='Y', bankrepaytransc_gid = '" + lsrepayment_id + "',closed_by= '" + user_gid + "' " +
                                  " where banktransc_gid = '" + msGetGid1 + "' ";
                                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                msSQL = " update brs_trn_tbanktransactiondetails set adjust_amount = '" + convertDecimallsamount + "',remaining_amount='" + remaining_amount + "',brstransactiondetailsadvice_flag = 'Y' where banktransc_gid='" + msGetGid1 + "'";
                                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            }
                            else
                            {
                                msSQL = " update brs_trn_tbanktransactiondetails set adjust_amount = '" + convertDecimallsamount + "',rule_number ='R2',brstransactiondetailsadvice_flag = 'Y' where banktransc_gid='" + msGetGid1 + "'";
                                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                               
                                msSQL = " update brs_trn_tbanktransactiondetails set remaining_amount='" + remaining_amount + "'" +
                                        " where banktransc_gid = '" + msGetGid1 + "'";
                                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            }
                        }
                        count = 0;
                        blank_count = 0;
                    }

                }
                dt_datatable.Dispose();
               
            }
            catch (Exception ex)
            {

            }
            return user_gid;
        }
    }
}