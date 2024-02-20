using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.rsk.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Configuration;
using System.Drawing;
using ems.storage.Functions;

namespace ems.rsk.DataAccess
{
    public class DaExclusionList
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable, dr_datatable;
        HttpPostedFile httpPostedFile;
        string msSQL, msGetGid;
        int mnResult;

        public bool DaGetExclusionAllocation(string customer_urn, string allocationdtl_gid, string exclusion_reason, string user_gid, result values)
        {

            msSQL = " select customerdisb_gid,customer_name from rsk_trn_tcustomerdisbursement where customer_urn='" + customer_urn + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                string lscustomerdisb_gid = objODBCDatareader["customerdisb_gid"].ToString();
                string lscustomer_name = objODBCDatareader["customer_name"].ToString();
                objODBCDatareader.Close();
                string msGet_Gid = objcmnfunctions.GetMasterGID("EXCH");

                msSQL = "insert into rsk_trn_texclusionhistory(" +
                       " exclusion_historygid ," +
                       " customerdisb_gid," +
                       " customer_urn," +
                       " customer_name," +
                       " excluded_status," +
                       " excluded_stage ," +
                       " exclusion_reason," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGet_Gid + "'," +
                       "'" + lscustomerdisb_gid + "'," +
                       "'" + customer_urn + "'," +
                       "'" + lscustomer_name + "'," +
                       "'Excluded'," +
                       "'Allocation'," +
                       "'" + exclusion_reason.Replace("'", "\\'") + "'," +
                       "'" + user_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update rsk_trn_tcustomerdisbursement" +
                        " set exclusion_flag ='Y', " +
                        " allocate_flag='N'," +
                        " exclusion_updatedby='" + user_gid + "'," +
                        " exclusion_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                        " where customerdisb_gid = '" + lscustomerdisb_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update rsk_trn_tallocationdtl" +
                       " set allocation_excludedflag ='Y', " +
                       " allocation_flag ='N'," +
                       " allocation_status='Excluded'," +
                       " allocation_excludedby='" + user_gid + "'," +
                       " allocation_excludeddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                       " where allocationdtl_gid = '" + allocationdtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }
            else
            {
                objODBCDatareader.Close();
                values.message = "No Records Found..!";
                values.status = false;
                return false;
            }

            if (mnResult != 0)
            {
                values.message = "Customer Excluded Successfully..!";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured..!";
                values.status = false;
                return false;
            }

        }

        public bool DaGetExclusionZonalExport(string employee_gid, exportexclusion values)
        {
            msSQL = " select date_format(exclusion_updateddate, '%d-%m-%Y') as ExcludedDate,a.customer_urn as CustomerURN, " +
                      " a.customername as CustomerName, format(a.total_sanction, 2) as SanctionAmount," +
                      " case when(DATEDIFF(CURDATE(), lastvisit_date) > 180) then date_format(c.last_disb_date, '%d-%m-%Y')" +
                      " else date_format(c.first_disb_date, '%d-%m-%Y') end as LoanDisbursementDate," +
                       " case when(b.lastvisit_date is null) then DATEDIFF(CURDATE(), c.first_disb_date) else" +
                      " DATEDIFF(CURDATE(), c.last_disb_date) end as DayPassedDisbursement," +
                        " DATEDIFF(CURDATE(), b.lastvisit_date) as DayPassedVisit," +
                      " date_format(lastvisit_date, '%d-%m-%Y') as LastVisitDate, " +
                      " case when(DATEDIFF(CURDATE(), lastvisit_date) > 180 and c.last_disb_date > lastvisit_date) then 'Re-Visit' " +
                      " when(DATEDIFF(CURDATE(), lastvisit_date) > 180 and c.last_disb_date < lastvisit_date) then 'Re-Visit'" +
                      " else 'Fresh' end as QualifiedStatus ," +
                      " concat(d.user_firstname,' ',d.user_lastname,'/',d.user_code) as ExcludedBy " +
                      " from ocs_mst_tcustomer a" +
                      " left join rsk_trn_tcustomervisit b on a.customer_urn = b.customer_urn" +
                      " left join rsk_trn_tcustomerdisbursement c on a.customer_urn = c.customer_urn" +
                      " left join adm_mst_tuser d on c.exclusion_updatedby=d.user_gid " +
                      " where zonal_gid in (select zonalmapping_gid from rsk_mst_tzonalmapping where zonalrisk_managerGid = '" + employee_gid + "')" +
                      " and c.exclusion_flag='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                string lscompany_code = string.Empty;
                MemoryStream ms = new MemoryStream();
                ExcelPackage excel = new ExcelPackage(ms);
                var workSheet = excel.Workbook.Worksheets.Add("ExclusionList");
                try
                {
                    msSQL = " select company_code from adm_mst_tcompany";

                    lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                    values.excel_path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "RSK/ExclusionListDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                    {
                        if ((!System.IO.Directory.Exists(values.excel_path)))
                            System.IO.Directory.CreateDirectory(values.excel_path);
                    }

                    values.excel_name = "ExclusionList" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".xlsx";
                    values.excel_path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "RSK/ExclusionListDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.excel_name;

                    workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                    FileInfo file = new FileInfo(values.excel_path);
                    using (var range = workSheet.Cells[1, 1, 1, 10])  //Address "A1:A18"
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.Green);
                        range.Style.Font.Color.SetColor(Color.White);
                    }
                    excel.SaveAs(ms);
                    bool status;
                    values.excel_path = lscompany_code + "/" + "RSK/ExclusionListDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.excel_name;
                    status = objcmnstorage.UploadStream("erpdocument", values.excel_path, ms);
                    values.status = status;
                   
                    ms.Close();


                }
                catch (Exception ex)
                {
                    dt_datatable.Dispose();
                    values.message = ex.ToString();
                    values.status = false;
                }
            }
            else
            {
                dt_datatable.Dispose();
                values.message = "No Records Found..!";
                values.status = false;
            }
            values.excel_path = objcmnstorage.EncryptData((values.excel_path));
            return true;
        }

        public bool DaGetExclusionSummary(exclusioncustomerlist values)
        {
            msSQL = " select a.customer_urn,case when(DATEDIFF(CURDATE(), lastvisit_date) > 180 and c.last_disb_date > lastvisit_date) then 'Re-Visit' " +
                      " when(DATEDIFF(CURDATE(), lastvisit_date) > 180 and c.last_disb_date < lastvisit_date) then 'Re-Visit'" +
                      " else 'Fresh' end as qualified_status ,a.customername,format(a.total_sanction, 2) as total_sanction," +
                      " case when(b.lastvisit_date is null) then DATEDIFF(CURDATE(), c.first_disb_date) else" +
                      " DATEDIFF(CURDATE(), c.last_disb_date) end as daypassed_disbursement," +
                      " DATEDIFF(CURDATE(), b.lastvisit_date) as daypassed_visit," +
                      " case when(DATEDIFF(CURDATE(), lastvisit_date) > 180) then date_format(c.last_disb_date, '%d-%m-%Y')" +
                      " else date_format(c.first_disb_date, '%d-%m-%Y') end as disbursement_date," +
                      " date_format(lastvisit_date, '%d-%m-%Y') as lastvisit_date, " +
                      " date_format(exclusion_updateddate, '%d-%m-%Y') as exclusion_updateddate, " +
                      " concat(d.user_firstname,' ',d.user_lastname,'/',d.user_code) as excludedby " +
                      " from ocs_mst_tcustomer a" +
                      " left join rsk_trn_tcustomervisit b on a.customer_urn = b.customer_urn" +
                      " left join rsk_trn_tcustomerdisbursement c on a.customer_urn = c.customer_urn" +
                      " left join adm_mst_tuser d on c.exclusion_updatedby=d.user_gid " +
                      " where c.exclusion_flag='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<exclusioncustomer>();
            if (dt_datatable.Rows.Count != 0)
            {
                values.exclusioncustomer = dt_datatable.AsEnumerable().Select(row => new exclusioncustomer
                {
                    customer_urn = row["customer_urn"].ToString(),
                    customer_name = row["customername"].ToString(),
                    excluded_date = row["exclusion_updateddate"].ToString(),
                    excluded_by = row["excludedby"].ToString(),
                    qualified_status = row["qualified_status"].ToString(),
                    total_sanction = row["total_sanction"].ToString(),
                    disbursement_date = row["disbursement_date"].ToString(),
                    daypassed_disbursement = row["daypassed_disbursement"].ToString(),
                    lastvisit_date = row["lastvisit_date"].ToString(),
                    daypassed_visit = row["daypassed_visit"].ToString(),
                }).ToList();
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaGetExclusionExport(exportexclusion values)
        {
            msSQL = " select date_format(exclusion_updateddate, '%d-%m-%Y') as ExcludedDate,a.customer_urn as CustomerURN, " +
                      " a.customername as CustomerName, format(a.total_sanction, 2) as SanctionAmount," +
                      " case when(DATEDIFF(CURDATE(), lastvisit_date) > 180) then date_format(c.last_disb_date, '%d-%m-%Y')" +
                      " else date_format(c.first_disb_date, '%d-%m-%Y') end as LoanDisbursementDate," +
                       " case when(b.lastvisit_date is null) then DATEDIFF(CURDATE(), c.first_disb_date) else" +
                      " DATEDIFF(CURDATE(), c.last_disb_date) end as DayPassedDisbursement," +
                        " DATEDIFF(CURDATE(), b.lastvisit_date) as DayPassedVisit," +
                      " date_format(lastvisit_date, '%d-%m-%Y') as LastVisitDate, " +
                      " case when(DATEDIFF(CURDATE(), lastvisit_date) > 180 and c.last_disb_date > lastvisit_date) then 'Re-Visit' " +
                      " when(DATEDIFF(CURDATE(), lastvisit_date) > 180 and c.last_disb_date < lastvisit_date) then 'Re-Visit'" +
                      " else 'Fresh' end as QualifiedStatus ," +
                      " concat(d.user_firstname,' ',d.user_lastname,'/',d.user_code) as ExcludedBy " +
                      " from ocs_mst_tcustomer a" +
                      " left join rsk_trn_tcustomervisit b on a.customer_urn = b.customer_urn" +
                      " left join rsk_trn_tcustomerdisbursement c on a.customer_urn = c.customer_urn" +
                      " left join adm_mst_tuser d on c.exclusion_updatedby=d.user_gid " +
                      " where c.exclusion_flag='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                string lscompany_code = string.Empty;
                MemoryStream ms = new MemoryStream();
                ExcelPackage excel = new ExcelPackage(ms);
                var workSheet = excel.Workbook.Worksheets.Add("ExclusionList");
                try
                {
                    msSQL = " select company_code from adm_mst_tcompany";

                    lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                    values.excel_path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "RSK/ExclusionListDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                    {
                        if ((!System.IO.Directory.Exists(values.excel_path)))
                            System.IO.Directory.CreateDirectory(values.excel_path);
                    }

                    values.excel_name = "ExclusionList" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".xlsx";
                    values.excel_path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "RSK/ExclusionListDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.excel_name;

                    workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                    //FileInfo file = new FileInfo(values.excel_path);
                    using (var range = workSheet.Cells[1, 1, 1, 10])  //Address "A1:A18"
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.Green);
                        range.Style.Font.Color.SetColor(Color.White);
                    }
                    excel.SaveAs(ms);
                    bool status;
                    values.excel_path = lscompany_code + "/" + "RSK/ExclusionListDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.excel_name;
                    status = objcmnstorage.UploadStream("erpdocument", values.excel_path, ms);
                    ms.Close();
                    values.status = status;
                    values.excel_path = objcmnstorage.EncryptData((values.excel_path));

                }
                catch (Exception ex)
                {
                    dt_datatable.Dispose();
                    values.message = ex.ToString();
                    values.status = false;
                }
            }
            else
            {
                dt_datatable.Dispose();
                values.message = "No Records Found..!";
                values.status = false;
            }

            return true;
        }
    }
}