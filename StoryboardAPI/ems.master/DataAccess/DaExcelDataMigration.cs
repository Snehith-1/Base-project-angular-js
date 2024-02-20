﻿using ems.master.Models;
using ems.utilities.Functions;
using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Data.OleDb;
using System.Configuration;
//using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System.Globalization;
using OfficeOpenXml;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Web.Script.Serialization;
using System.Net;
using System.Reflection;
using static OfficeOpenXml.ExcelErrorValue;

/// <summary>
/// (It's used for Samfin IPL Migration ) Samfin IPL Migration DataAccess Class accessed by API methods from related Controller class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sherin,Logapriya and Abilash</remarks>


namespace ems.master.DataAccess
{
    public class DaExcelDataMigration
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid, lscompany_code;
        int mnResult;
        HttpPostedFile httpPostedFile;

        public void DaImportApplicationCreationData(HttpRequest httpRequest, string employee_gid, result objResult)
        {
            DataTable dt = null;
            DataTable Generaltable = new DataTable();
            DataTable CompanyDetailtable = new DataTable();
            DataTable IndividualDetailtable = new DataTable();
            DataTable EconomicCapitalDetailtable = new DataTable();
            DataTable OverallLimitDetailtable = new DataTable();
            DataTable ProductDetailtable = new DataTable();
            DataTable ServiceChargesDetailtable = new DataTable();
            DataTable CompanyBureauDetailtable = new DataTable();
            DataTable IndividualBureauDetailtable = new DataTable();
            DataTable DocumentMappingtable = new DataTable();         
            List<ApplicationList> ApplicationInfoList = new List<ApplicationList>();
            List<CompanyDetailsList> CompanyInfoList = new List<CompanyDetailsList>();
            List<IndividualDetailsList> IndividualInfoList = new List<IndividualDetailsList>();
            List<EconomicCapitalDetails> EconomicCapitalInfoList = new List<EconomicCapitalDetails>();
            List<OverallLimitDetails> OverallLimitInfoList = new List<OverallLimitDetails>();
            List<ProductDetails> ProductDetailInfoList = new List<ProductDetails>();
            List<ServiceChargesDetails> ServiceChargesInfoList = new List<ServiceChargesDetails>();
            List<BureauDetails> IndividualBureauDetailInfoList = new List<BureauDetails>();
            List<BureauDetails> CompanyBureauDetailInfoList = new List<BureauDetails>();
            List<DocumentMappingDetails> DocumentMappingInfoList = new List<DocumentMappingDetails>();           

            try
            {
                //int insertCount = 0;
                HttpFileCollection httpFileCollection;

                string lspath, lsfilePath;

                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                // Create Directory
                lsfilePath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/Master/ExcelDataMigration/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

                if ((!System.IO.Directory.Exists(lsfilePath)))
                    System.IO.Directory.CreateDirectory(lsfilePath);

                httpFileCollection = httpRequest.Files;
                for (int i = 0; i < httpFileCollection.Count; i++)
                {
                    httpPostedFile = httpFileCollection[i];
                }
                string FileExtension = httpPostedFile.FileName;

                string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                string lsfile_gid = msdocument_gid;
                FileExtension = Path.GetExtension(FileExtension).ToLower();
                lsfile_gid = lsfile_gid + FileExtension;

                Stream ls_readStream;
                ls_readStream = httpPostedFile.InputStream;
                MemoryStream ms = new MemoryStream();
                ls_readStream.CopyTo(ms);

                //path creation        
                lspath = lsfilePath + "/";
                FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);
                var obj = new List<MdlExcelSheetInfo_list>();
                //MdlExcelSheetInfo obj = new MdlExcelSheetInfo();
                try
                {
                    using (ExcelPackage xlPackage = new ExcelPackage(ms))
                    {
                        int[] arr = new int[xlPackage.Workbook.Worksheets.Count];
                        int totalsheet = xlPackage.Workbook.Worksheets.Count;
                        for (int i = 0; i < totalsheet; i++)
                        {
                            ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[i + 1];
                            obj.Add(new MdlExcelSheetInfo_list
                            {
                                sheetName = "'" + worksheet.Name + "$'",
                                rowCount = worksheet.Dimension.End.Row,
                                columnCount = worksheet.Dimension.End.Column,
                                endRange = worksheet.Dimension.End.Address,
                            });
                        }
                    }
                    file.Close();
                    ms.Close();

                    objcmnfunctions.uploadFile(lspath, lsfile_gid);

                    try
                    {
                        lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";

                        string lsConnectionString = string.Empty;
                        string fileExtension = Path.GetExtension(lsfilePath);
                        if (fileExtension == ".xls")
                        {
                            lsConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + lsfilePath + ";" + "Extended Properties='Excel 8.0;HDR=YES;'";
                        }
                        else if (fileExtension == ".xlsx")
                        {
                            lsConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + lsfilePath + ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;MAXSCANROWS=0';";
                        }
                        //int totalSheet = 1;

                        string excelRange;

                        using (OleDbConnection objConn = new OleDbConnection(lsConnectionString))
                        {
                            objConn.Open();
                            OleDbCommand cmd = new OleDbCommand();
                            OleDbDataAdapter oleda = new OleDbDataAdapter();
                            DataSet ds = new DataSet();
                            DataTable dt1 = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sheetName = string.Empty;
                            if (dt1 != null)
                            {
                                var tempDataTable = (from dataRow in dt1.AsEnumerable()
                                                     where !dataRow["TABLE_NAME"].ToString().Contains("FilterDatabase")
                                                     select dataRow).CopyToDataTable();
                                dt1 = tempDataTable;
                                for (int i = 0; i < dt1.Rows.Count; i++)
                                {

                                    //totalSheet = dt1.Rows.Count;
                                    sheetName = dt1.Rows[i]["TABLE_NAME"].ToString();
                                    var getrange = obj.Where(x => x.sheetName == sheetName).FirstOrDefault();
                                    excelRange = "A1:" + getrange.endRange + getrange.rowCount.ToString();
                                    sheetName = sheetName.Replace("'", "").Trim() + excelRange;
                                    cmd.Connection = objConn;
                                    cmd.CommandType = CommandType.Text;
                                    cmd.CommandText = "SELECT * FROM [" + sheetName + "]";
                                    oleda = new OleDbDataAdapter(cmd);
                                    string DataTableName = dt1.Rows[i]["TABLE_NAME"].ToString().Replace("'", "").Trim();
                                    DataTableName = DataTableName.Replace("$", "");
                                    DataTableName = DataTableName.Replace(" ", "");
                                    oleda.Fill(ds, DataTableName);
                                }
                            }
                            ProductDetailtable.Constraints.Clear();
                            ProductDetailtable.Clear();
                            ProductDetailtable.Dispose();
                            ProductDetailtable = null; 

                            Generaltable = ds.Tables["GeneralDetails"];
                            CompanyDetailtable = ds.Tables["CompanyDetails"];
                            IndividualDetailtable = ds.Tables["IndividualDetails"];
                            EconomicCapitalDetailtable = ds.Tables["EconomicCapitalDetails"];
                            OverallLimitDetailtable = ds.Tables["OverallLimitDetails"];
                            ProductDetailtable = ds.Tables["ProductDetails"];
                            ServiceChargesDetailtable = ds.Tables["ServiceChargesDetails"];
                            CompanyBureauDetailtable = ds.Tables["CompanyBureauDetails"];
                            IndividualBureauDetailtable = ds.Tables["IndividualBureauDetails"];
                            DocumentMappingtable = ds.Tables["DocumentMappingDetails"];
                                                 
                            objConn.Close();

                            if (Generaltable != null)
                            {
                                Generaltable = Generaltable.Rows.Cast<DataRow>().Where(r => string.Join("", r.ItemArray).Trim() != string.Empty).CopyToDataTable();
                                ApplicationInfoList = ConvertDataTable<ApplicationList>(Generaltable); 
                                Generaltable.Dispose();
                                Generaltable = null; 
                            }

                            // Company Details List
                            if (CompanyDetailtable != null)
                            {
                                CompanyDetailtable = CompanyDetailtable.Rows.Cast<DataRow>().Where(r => string.Join("", r.ItemArray).Trim() != string.Empty).CopyToDataTable();
                                CompanyInfoList = ConvertDataTable<CompanyDetailsList>(CompanyDetailtable);  
                                CompanyDetailtable.Dispose();
                                CompanyDetailtable = null;
                            }
                            // Individual Details List
                            if (IndividualDetailtable != null)
                            {
                                IndividualDetailtable = IndividualDetailtable.Rows.Cast<DataRow>().Where(r => string.Join("", r.ItemArray).Trim() != string.Empty).CopyToDataTable();
                                IndividualInfoList = ConvertDataTable<IndividualDetailsList>(IndividualDetailtable);  
                                IndividualDetailtable.Dispose();
                                IndividualDetailtable = null;
                            }
                            // Economic Capital Details List 
                            if (EconomicCapitalInfoList != null)
                            {
                                EconomicCapitalDetailtable = EconomicCapitalDetailtable.Rows.Cast<DataRow>().Where(r => string.Join("", r.ItemArray).Trim() != string.Empty).CopyToDataTable();
                                EconomicCapitalInfoList = ConvertDataTable<EconomicCapitalDetails>(EconomicCapitalDetailtable);  
                                EconomicCapitalDetailtable.Dispose();
                                EconomicCapitalDetailtable = null;
                            }
                            // Overall Limit List 
                            if (OverallLimitDetailtable != null)
                            {
                                OverallLimitDetailtable = OverallLimitDetailtable.Rows.Cast<DataRow>().Where(r => string.Join("", r.ItemArray).Trim() != string.Empty).CopyToDataTable();
                                OverallLimitInfoList = ConvertDataTable<OverallLimitDetails>(OverallLimitDetailtable); 
                                OverallLimitDetailtable.Dispose();
                                OverallLimitDetailtable = null; 
                            }
                            // Product Detail  List 
                            if (ProductDetailtable != null)
                            {
                                ProductDetailtable = ProductDetailtable.Rows.Cast<DataRow>().Where(r => string.Join("", r.ItemArray).Trim() != string.Empty).CopyToDataTable();
                                ProductDetailInfoList = ConvertDataTable<ProductDetails>(ProductDetailtable); 
                                ProductDetailtable.Dispose();
                                ProductDetailtable = null;
                            }
                            // Service Charges List 
                            if (ServiceChargesDetailtable != null)
                            {
                                ServiceChargesDetailtable = ServiceChargesDetailtable.Rows.Cast<DataRow>().Where(r => string.Join("", r.ItemArray).Trim() != string.Empty).CopyToDataTable();
                                ServiceChargesInfoList = ConvertDataTable<ServiceChargesDetails>(ServiceChargesDetailtable);  
                                ServiceChargesDetailtable.Dispose();
                                ServiceChargesDetailtable = null;
                            }
                            // Company Bureau Detail List 
                            if (CompanyBureauDetailtable != null)
                            {
                                CompanyBureauDetailtable = CompanyBureauDetailtable.Rows.Cast<DataRow>().Where(r => string.Join("", r.ItemArray).Trim() != string.Empty).CopyToDataTable();
                                CompanyBureauDetailInfoList = ConvertDataTable<BureauDetails>(CompanyBureauDetailtable);
                                CompanyBureauDetailtable.Dispose();
                                CompanyBureauDetailtable = null; 
                            }
                            // Individual Bureau Detail List 
                            if (IndividualBureauDetailtable != null)
                            {
                                IndividualBureauDetailtable = IndividualBureauDetailtable.Rows.Cast<DataRow>().Where(r => string.Join("", r.ItemArray).Trim() != string.Empty).CopyToDataTable();
                                IndividualBureauDetailInfoList = ConvertDataTable<BureauDetails>(IndividualBureauDetailtable);
                                IndividualBureauDetailtable.Dispose();
                                IndividualBureauDetailtable = null;
                            }
                            // Document Mapping Details List 
                            if (DocumentMappingInfoList != null)
                            {
                                DocumentMappingtable = DocumentMappingtable.Rows.Cast<DataRow>().Where(r => string.Join("", r.ItemArray).Trim() != string.Empty).CopyToDataTable();
                                DocumentMappingInfoList = ConvertDataTable<DocumentMappingDetails>(DocumentMappingtable);
                                DocumentMappingtable.Dispose();
                                DocumentMappingtable = null;
                            }                           
                        }

                        // General Info Submit 
                        List<ApplicationList> applicationdtl = DaSubmitGeneralDtl(ApplicationInfoList);

                        // Company info Submit 
                        List<CompanyDetailsList> companydtl =  DaSubmitCompanyInfo(CompanyInfoList, applicationdtl);

                        // Individual Info  
                        List<IndividualDetailsList> individualdtl = DaSubmitIndividualInfo(IndividualInfoList, applicationdtl);

                        // Economic Capital Submit
                        DaSubmitEconomicCapitalDtl(EconomicCapitalInfoList, applicationdtl);

                        // Overall Limit Submit 
                        DaSubmitOverallLimitDtl(OverallLimitInfoList, applicationdtl);

                        // Product Details Submit 
                        DaSubmitProductLoanDtl(ProductDetailInfoList, applicationdtl);

                        // Service Charge Details Submit 
                        DaSubmitServiceChargesDtl(ServiceChargesInfoList, applicationdtl);

                        // Company Bureau Details Submit 
                        DaSubmitBureauDtl(CompanyBureauDetailInfoList, companydtl);

                        // Individual Bureau Details Submit 
                        DaSubmitIndividualBureauDtl(IndividualBureauDetailInfoList, individualdtl);

                        // Document Mapping Submit 
                        DaSubmitDocumentMappingDtl(DocumentMappingInfoList, applicationdtl, companydtl, individualdtl);
                    }
                    catch (Exception ex)
                    {
                        LogForAudit(ex.ToString());
                        objResult.status = false;
                        objResult.message = ex.ToString();

                        return;
                    }
                }
                catch (Exception ex)
                {

                    LogForAudit(ex.ToString());
                    objResult.status = false;
                    objResult.message = ex.ToString();
                    return;
                }

                objResult.status = true;
                objResult.message = "Excel uploaded successfully";

            }

            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = "Error occured in uploading Excel";
            }
            try
            {

            }
            catch (Exception ex)
            {
            }

        }

        // General Info 
        public List<ApplicationList> DaSubmitGeneralDtl(List<ApplicationList> datalist)
        {
            LogForAudit("----General Info - Started -----------");
            // From Vertical Master
            msSQL = " SELECT a.vertical_gid,a.vertical_name,vertical_code " +
                    " FROM ocs_mst_tvertical a  where status_log='Y' order by a.vertical_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<MstverticalList> MstverticalList = new List<MstverticalList>();
            MstverticalList = ConvertDataTable<MstverticalList>(dt_datatable);
            //dt_datatable.Dispose();

            // Credit Group Master
            msSQL = " SELECT creditmapping_gid as creditgroup_gid ,creditgroup_name FROM ocs_mst_tcreditmapping a" +
                      " where status='Y' order by a.creditmapping_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<MstCreditgroupList> MstCreditgroupList = new List<MstCreditgroupList>();
            MstCreditgroupList = ConvertDataTable<MstCreditgroupList>(dt_datatable);

            // Constitution 
            msSQL = " SELECT constitution_gid,constitution_name FROM ocs_mst_tconstitution a" +
                   " where status_log='Y' order by constitution_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<MstconstitutionList> MstconstitutionList = new List<MstconstitutionList>();
            MstconstitutionList = ConvertDataTable<MstconstitutionList>(dt_datatable);

            // Product Master
            msSQL = " SELECT product_gid,product_name,businessunit_gid,businessunit_name,valuechain_gid,valuechain_name FROM ocs_mst_tproducts a" +
                     " where status='Y' order by a.product_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<MstproductnameList> MstproductnameList = new List<MstproductnameList>();
            MstproductnameList = ConvertDataTable<MstproductnameList>(dt_datatable);

            // Variety Master

            msSQL = " SELECT product_gid,variety_gid,variety_name,botanical_name,alternative_name FROM ocs_mst_tvariety a " +
                    " order by a.product_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<MstVarietynameList> MstVarietynameList = new List<MstVarietynameList>();
            MstVarietynameList = ConvertDataTable<MstVarietynameList>(dt_datatable);

            // Program Master
            msSQL = " SELECT program_gid,program FROM ocs_mst_tprogram a" +
                       " where status='Y' order by a.program_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<MstprogramList> MstprogramList = new List<MstprogramList>();
            MstprogramList = ConvertDataTable<MstprogramList>(dt_datatable);

            // Vernacular Master
            msSQL = " SELECT vernacularlanguage_gid,vernacular_language FROM ocs_mst_tvernacularlanguage a" +
                     " where status='Y' order by a.vernacularlanguage_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<Mstvernacularlang_list> Mstvernacularlang_list = new List<Mstvernacularlang_list>();
            Mstvernacularlang_list = ConvertDataTable<Mstvernacularlang_list>(dt_datatable);

            // Designation Master
            msSQL = " SELECT a.designation_gid,a.designation_type from ocs_mst_tdesignation a" +
                 "  where status_log='Y' order by a.designation_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<MstdesignationList> MstdesignationList = new List<MstdesignationList>();
            MstdesignationList = ConvertDataTable<MstdesignationList>(dt_datatable);

            msSQL = " SELECT geneticcode_gid,geneticcode_name FROM ocs_mst_tgeneticcode a" +
                  " where status='Y' order by a.geneticcode_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<MstGeneticList> MstGeneticList = new List<MstGeneticList>();
            MstGeneticList = ConvertDataTable<MstGeneticList>(dt_datatable);
            List<ApplicationList> DaApplicationList = new List<ApplicationList>();

            foreach (var values in datalist)
            {
                msSQL = " SELECT  application_gid FROM ocs_mst_tapplication a" +
                                   " where  application_no ='" + values.ARN + "' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    LogForAudit("Application Number '" + values.ApplicationNumber + "' Already Exist");

                }
                else
                {
                    if (string.IsNullOrEmpty(values.Migrated_ARN_number))
                {
                    try
                    {


                        string lsemployee_name = "", lsemployee_gid = "";
                        string lsdrm_gid = "", lsdrm_name = "";
                        //string lsapplication_gid = string.Empty;
                        string lsvertical_gid = "", lsConstitution_gid = "", lsvernacularlan_gid = "", lsdesignation_gid = "";
                        string lscredit_groupgid = "", lsprogram_gid = "", lsproduct_gid = "", lsvariety_gid = "";
                        string lsclusterhead, lsregionalhead, lszonalhead, lsbusinesshead;
                        string lsclusterheadgid, lsregionalheadgid, lszonalheadgid, lsbusinessheadgid;
                        string lsbaselocationgid, lsclustergid, lsregiongid, lszonalgid;
                        string lsbaselocationname, lsclustername, lsregionname, lszonalname;


                        msSQL = " select a.employee_gid, concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as employee_name " +
                                " from hrm_mst_temployee a" +
                                " left join adm_mst_tuser b on a.user_gid=b.user_gid" +
                                " where b.user_code='" + values.RM_Employee_Code + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
                            lsemployee_name = objODBCDatareader["employee_name"].ToString();
                        }
                        objODBCDatareader.Close();


                        msSQL = " select concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as level_one ,a.employeereporting_to from adm_mst_tmodule2employee a " +
                               " left join hrm_mst_temployee f on f.employee_gid = a.employeereporting_to " +
                               " left join adm_mst_tuser g on g.user_gid = f.user_gid " +
                               " where  a.module_gid in  (select module_gid_parent from adm_mst_tmodule where module_gid in  " +
                               " (select modulereportingto_gid from adm_mst_tcompany)) and g.user_status = 'Y' and a.employee_gid ='" + lsemployee_gid + "' " +
                               "  group by a.employee_gid ";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsdrm_gid = objODBCDatareader["employeereporting_to"].ToString();
                            lsdrm_name = objODBCDatareader["level_one"].ToString();
                        }
                        objODBCDatareader.Close();
                        values.RM_EmployeeGid = lsemployee_gid;
                        if (values.ApplicationNumber != "" || values.ApplicationNumber != null)
                        {
                            var lsvertical = MstverticalList.Where(x => x.vertical_name.ToLower().Trim() == values.Vertical.ToLower().Trim()).FirstOrDefault();
                            if (lsvertical != null)
                                lsvertical_gid = lsvertical.vertical_gid;
                            var lsConstitution = MstconstitutionList.Where(x => x.constitution_name.ToLower().Trim() == values.Constitution.ToLower().Trim()).FirstOrDefault();
                            if (lsConstitution != null)
                                lsConstitution_gid = lsConstitution.constitution_gid;
                            var lsvernacularlan = Mstvernacularlang_list.Where(x => x.vernacular_language.ToLower().Trim() == values.Vernacular_Language.ToLower().Trim()).FirstOrDefault();
                            if (lsvernacularlan != null)
                                lsvernacularlan_gid = lsvernacularlan.vernacularlanguage_gid;
                            var lsdesignation = MstdesignationList.Where(x => x.designation_type.ToLower().Trim() == values.Designation.ToLower().Trim()).FirstOrDefault();
                            if (lsdesignation != null)
                                lsdesignation_gid = lsdesignation.designation_gid;
                            var lscredit_group = MstCreditgroupList.Where(x => x.creditgroup_name.ToLower().Trim() == values.Credit_Group.ToLower().Trim()).FirstOrDefault();
                            if (lscredit_group != null)
                                lscredit_groupgid = lscredit_group.creditgroup_gid;
                            var lsprogram = MstprogramList.Where(x => x.program.ToLower().Trim() == values.Program.ToLower().Trim()).FirstOrDefault();
                            if (lsprogram != null)
                                lsprogram_gid = lsprogram.program_gid;
                            var lsproduct_dtl = MstproductnameList.Where(x => x.product_name.ToLower().Trim() == values.Product.ToLower().Trim()).FirstOrDefault();
                            if (lsproduct_dtl != null)
                            {
                                lsproduct_gid = lsproduct_dtl.product_gid;
                                values.Sector_Strategic_BusinessUnit = lsproduct_dtl.businessunit_name;
                                values.Category = lsproduct_dtl.valuechain_name;

                                try
                                {
                                    var lsvarietydtl = MstVarietynameList.Where(x => x.variety_name.ToLower() == values.Variety.ToLower() && x.product_gid == lsproduct_gid).FirstOrDefault();
                                    if (lsvarietydtl != null)
                                    {
                                        lsvariety_gid = lsvarietydtl.variety_gid;
                                        values.Botanical_Name = lsvarietydtl.botanical_name;
                                        values.Alternative_Names = lsvarietydtl.alternative_name;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    lsvariety_gid = "";
                                    values.Botanical_Name = "";
                                    values.Alternative_Names = "";
                                }
                            }

                            msSQL = " select distinct b.baselocation_gid,b.baselocation_name,c.cluster_gid,c.cluster_name,c.employee_name as clusterhead,c.vertical_name as clustervertical," +
                                  " e.region_gid,e.region_name,e.employee_name as regionhead,g.zonal_gid,g.zonal_name,g.employee_name as zonalhead ," +
                                  " c.employee_gid as clusterhead_gid,e.employee_gid as regionhead_gid, " +
                                  " g.employee_gid as zonalhead_gid,h.employee_gid as businesshead_gid, " +
                                  " h.employee_name as businesshead from hrm_mst_temployee a" +
                                  " left join sys_mst_tcluster2baselocation b on b.baselocation_gid = a.baselocation_gid" +
                                  " left  join sys_mst_tclusterhead c on b.cluster_gid = c.cluster_gid" +
                                  " left join sys_mst_tregion2cluster d on c.cluster_gid = d.cluster_gid" +
                                  " left join sys_mst_tregionhead e on d.region_gid = e.region_gid" +
                                  " left join sys_mst_tzone2region f on f.region_gid = d.region_gid" +
                                  " left join sys_mst_tzonalhead g on f.zone_gid = g.zonal_gid" +
                                  " left join sys_mst_tbusinesshead h on h.zone_gid = g.zonal_gid where a.employee_gid = '" + lsemployee_gid + "' and" +
                                  " c.vertical_gid = '" + lsvertical_gid + "'" +
                                  " and e.vertical_gid = '" + lsvertical_gid + "' and " +
                                  " g.vertical_gid = '" + lsvertical_gid + "' and h.vertical_gid = '" + lsvertical_gid + "'" +
                                  " and c.program_gid = '" + lsprogram_gid + "' and e.program_gid = '" + lsprogram_gid + "' and " +
                                  " g.program_gid = '" + lsprogram_gid + "' and h.program_gid = '" + lsprogram_gid + "' " +
                                  " and e.status='Y' and g.status='Y' and h.status='Y' and c.status='Y'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsclusterhead = objODBCDatareader["clusterhead"].ToString();
                                lsregionalhead = objODBCDatareader["regionhead"].ToString();
                                lszonalhead = objODBCDatareader["zonalhead"].ToString();
                                lsbusinesshead = objODBCDatareader["businesshead"].ToString();
                                lsclusterheadgid = objODBCDatareader["clusterhead_gid"].ToString();
                                lsregionalheadgid = objODBCDatareader["regionhead_gid"].ToString();
                                lszonalheadgid = objODBCDatareader["zonalhead_gid"].ToString();
                                lsbusinessheadgid = objODBCDatareader["businesshead_gid"].ToString();
                                lsbaselocationgid = objODBCDatareader["baselocation_gid"].ToString();
                                lsbaselocationname = objODBCDatareader["baselocation_name"].ToString();
                                lsclustergid = objODBCDatareader["cluster_gid"].ToString();
                                lsclustername = objODBCDatareader["cluster_name"].ToString();
                                lsregiongid = objODBCDatareader["region_gid"].ToString();
                                lsregionname = objODBCDatareader["region_name"].ToString();
                                lszonalgid = objODBCDatareader["zonal_gid"].ToString();
                                lszonalname = objODBCDatareader["zonal_name"].ToString();
                                objODBCDatareader.Close();

                                values.application_gid = objcmnfunctions.GetMasterGID("APPC");
                                msSQL = " insert into ocs_mst_tapplication(" +
                                        " application_gid," +
                                        " customerref_name, " +
                                        " application_no," +
                                        " migration_applicationno, " +
                                        " vertical_gid," +
                                        " vertical_name," +
                                        " constitution_gid," +
                                        " constitution_name," +
                                        " sa_status," +
                                        " saname_gid," +
                                        " sa_name," +
                                        " relationshipmanager_name," +
                                        " relationshipmanager_gid," +
                                        " drm_gid," +
                                        " drm_name," +
                                        " vernacular_language," +
                                        " vernacularlanguage_gid," +
                                        " contactpersonfirst_name," +
                                        " contactpersonmiddle_name," +
                                        " contactpersonlast_name," +
                                        " designation_gid," +
                                        " designation_type," +
                                        " baselocation_gid," +
                                        " baselocation_name," +
                                        " cluster_gid," +
                                        " cluster_name," +
                                        " region_gid," +
                                        " region_name," +
                                        " zone_gid," +
                                        " zone_name," +
                                        " clustermanager_gid," +
                                        " clustermanager_name," +
                                        " zonalhead_name," +
                                        " zonalhead_gid," +
                                        " regionalhead_name," +
                                        " regionalhead_gid," +
                                        " businesshead_name," +
                                        " businesshead_gid," +
                                        " creditgroup_gid," +
                                        " creditgroup_name," +
                                        " program_gid," +
                                        " program_name," +
                                        " product_gid," +
                                        " product_name," +
                                        " variety_gid," +
                                        " variety_name," +
                                        " sector_name," +
                                        " category_name," +
                                        " botanical_name," +
                                        " alternative_name," +
                                        " status," +
                                        " created_by," +
                                        " created_date) values(" +
                                        "'" + values.application_gid + "'," +
                                        "'" + values.Applicant_Name + "'," +
                                        "'" + values.ARN + "'," +
                                        "'" + values.ApplicationNumber + "'," +
                                        "'" + lsvertical_gid + "'," +
                                        "'" + values.Vertical + "'," +
                                        "'" + lsConstitution_gid + "'," +
                                        "'" + values.Constitution + "'," +
                                        "'" + values.Application_FromSA_Yes_No + "'," +
                                        "'" + values.SAMAssociate_IDName + "'," +
                                        "'" + values.SAMAssociate_IDName + "'," +
                                        "'" + lsemployee_name + "'," +
                                        "'" + lsemployee_gid + "'," +
                                        "'" + lsdrm_gid + "'," +
                                        "'" + lsdrm_name + "'," +
                                        "'" + values.Vernacular_Language + "'," +
                                        "'" + lsvernacularlan_gid + "'," +
                                        "'" + values.First_Name + "'," +
                                        "'" + values.Middle_Name + "'," +
                                        "'" + values.Last_Name + "'," +
                                        "'" + lsdesignation_gid + "'," +
                                        "'" + values.Designation + "'," +
                                        "'" + lsbaselocationgid + "'," +
                                        "'" + lsbaselocationname + "'," +
                                        "'" + lsclustergid + "'," +
                                        "'" + lsclustername + "'," +
                                        "'" + lsregiongid + "'," +
                                        "'" + lsregionname + "'," +
                                        "'" + lszonalgid + "'," +
                                        "'" + lszonalname + "'," +
                                        "'" + lsclusterheadgid + "'," +
                                        "'" + lsclusterhead + "'," +
                                        "'" + lszonalhead + "'," +
                                        "'" + lszonalheadgid + "'," +
                                        "'" + lsregionalhead + "'," +
                                        "'" + lsregionalheadgid + "'," +
                                        "'" + lsbusinesshead + "'," +
                                        "'" + lsbusinessheadgid + "'," +
                                        "'" + lscredit_groupgid + "'," +
                                        "'" + values.Credit_Group + "'," +
                                        "'" + lsprogram_gid + "'," +
                                        "'" + values.Program + "'," +
                                        "'" + lsproduct_gid + "'," +
                                        "'" + values.Product + "'," +
                                        "'" + lsvariety_gid + "'," +
                                        "'" + values.Variety + "'," +
                                        "'" + values.Sector_Strategic_BusinessUnit + "'," +
                                        "'" + values.Category + "'," +
                                        "'" + values.Botanical_Name + "'," +
                                        "'" + values.Alternative_Names + "'," +
                                        "'Completed'," +
                                        "'" + lsemployee_gid + "'," +
                                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                // Genetic Code by Business Team 
                                foreach (var i in MstGeneticList)
                                {
                                    msGetGid = objcmnfunctions.GetMasterGID("A2GC");
                                    msSQL = " insert into ocs_mst_tapplication2geneticcode(" +
                                           " application2geneticcode_gid," +
                                           " application_gid," +
                                           " geneticcode_gid," +
                                           " geneticcode_name," +
                                           " genetic_status," +
                                           " genetic_remarks," +
                                           " created_by," +
                                           " created_date)" +
                                           " values(" +
                                           "'" + msGetGid + "'," +
                                           "'" + values.application_gid + "'," +
                                           "'" + i.geneticcode_gid + "'," +
                                           "'" + i.geneticcode_name.Replace("'", " ") + "'," +
                                           "'" + values.Genetic_Status + "'," +
                                           "'" + values.Observations.Replace("'", " ") + "'," +
                                           "'" + lsemployee_gid + "'," +
                                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }

                                //    Mobile Number(s)  
                                msGetGid = objcmnfunctions.GetMasterGID("A2CN");
                                msSQL = " insert into ocs_mst_tapplication2contactno(" +
                                        " application2contact_gid," +
                                        " application_gid," +
                                        " mobile_no," +
                                        " primary_mobileno," +
                                        " whatsapp_mobileno," +
                                        " created_by," +
                                        " created_date)" +
                                        " values(" +
                                        "'" + msGetGid + "'," +
                                        "'" + values.application_gid + "'," +
                                        "'" + values.Mobile_Number + "'," +
                                        "'" + values.Mobile_Primary_Status + "'," +
                                        "'" + values.Whatapp_Number + "'," +
                                        "'" + lsemployee_gid + "'," +
                                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                // Mail ID(s) 
                                msGetGid = objcmnfunctions.GetMasterGID("A2EA");
                                msSQL = " insert into ocs_mst_tapplication2email(" +
                                        " application2email_gid," +
                                        " application_gid," +
                                        " email_address," +
                                        " primary_emailaddress," +
                                        " created_by," +
                                        " created_date)" +
                                        " values(" +
                                        "'" + msGetGid + "'," +
                                        "'" + values.application_gid + "'," +
                                        "'" + values.Email_Address + "'," +
                                        "'" + values.Email_Primary_Status + "'," +
                                        "'" + lsemployee_gid + "'," +
                                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                msGetGid = objcmnfunctions.GetMasterGID("AP2P");
                                msSQL = " insert into ocs_mst_tapplication2product (" +
                                        " application2product_gid," +
                                        " application2loan_gid," +
                                        " application_gid," +
                                        " product_gid," +
                                        " product_name," +
                                        " variety_gid," +
                                        " variety_name," +
                                        " sector_name," +
                                        " category_name," +
                                        " botanical_name," +
                                        " alternative_name," +
                                        " created_by," +
                                        " created_date)" +
                                        " values(" +
                                        "'" + msGetGid + "'," +
                                        "null," +
                                        "'" + values.application_gid + "'," +
                                        "'" + lsproduct_gid + "'," +
                                        "'" + values.Product + "'," +
                                        "'" + lsvariety_gid + "'," +
                                        "'" + values.Variety + "'," +
                                        "'" + values.Sector_Strategic_BusinessUnit + "'," +
                                        "'" + values.Category + "'," +
                                        "'" + values.Botanical_Name + "'," +
                                        "'" + values.Alternative_Names + "'," +
                                        "'" + lsemployee_gid + "'," +
                                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                DaApplicationList.Add(values);
                                LogForAudit(" General - '" + values.ApplicationNumber + "' Completed");
                            }
                            else
                            {
                                LogForAudit(" Application No: '" + values.ApplicationNumber + "' - Location / Vertical not Assign for Business Approval'");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogForAudit(" General Info Error - '" + values.ApplicationNumber + "' - '" + ex.ToString() + "'");
                    }
                }
                else
                {
                    msSQL = " SELECT  application_gid,customer_urn FROM ocs_trn_tcadapplication a" +
                                        " where process_type ='Accept' and application_no ='" + values.Migrated_ARN_number + "' ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == false)
                    {
                        LogForAudit("Migrated Application Not CAD Accepted '" + values.ApplicationNumber + "'");

                    }
                    else
                    {
                        msSQL = " SELECT  application_gid FROM ocs_mst_tapplication a" +
                         " where  application_no ='" + objODBCDatareader["customer_urn"].ToString() + "' and (renewal_flag='Y' or enhancement_flag='Y') and approval_status in ('Incomplete'," +
                         "'Submitted to Approval','Submitted to Underwriting','Submitted to Heads Approval','Submitted to Credit Approval','Submitted to CC'," +
                         "'Sent Back to CC','Sent Back to Credit','Sendback to Credit - Without CC') ";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            LogForAudit("Application Already Renew or Enhancement '" + values.ApplicationNumber + "'");

                        }
                        else
                        {
                            try
                            {


                                string lsemployee_name = "", lsemployee_gid = "";
                                string lsdrm_gid = "", lsdrm_name = "";
                                //string lsapplication_gid = string.Empty;
                                string lsvertical_gid = "", lsConstitution_gid = "", lsvernacularlan_gid = "", lsdesignation_gid = "";
                                string lscredit_groupgid = "", lsprogram_gid = "", lsproduct_gid = "", lsvariety_gid = "";
                                string lsclusterhead, lsregionalhead, lszonalhead, lsbusinesshead;
                                string lsclusterheadgid, lsregionalheadgid, lszonalheadgid, lsbusinessheadgid;
                                string lsbaselocationgid, lsclustergid, lsregiongid, lszonalgid;
                                string lsbaselocationname, lsclustername, lsregionname, lszonalname;


                                msSQL = " select a.employee_gid, concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as employee_name " +
                                        " from hrm_mst_temployee a" +
                                        " left join adm_mst_tuser b on a.user_gid=b.user_gid" +
                                        " where b.user_code='" + values.RM_Employee_Code + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
                                    lsemployee_name = objODBCDatareader["employee_name"].ToString();
                                }
                                objODBCDatareader.Close();


                                msSQL = " select concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as level_one ,a.employeereporting_to from adm_mst_tmodule2employee a " +
                                       " left join hrm_mst_temployee f on f.employee_gid = a.employeereporting_to " +
                                       " left join adm_mst_tuser g on g.user_gid = f.user_gid " +
                                       " where  a.module_gid in  (select module_gid_parent from adm_mst_tmodule where module_gid in  " +
                                       " (select modulereportingto_gid from adm_mst_tcompany)) and g.user_status = 'Y' and a.employee_gid ='" + lsemployee_gid + "' " +
                                       "  group by a.employee_gid ";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsdrm_gid = objODBCDatareader["employeereporting_to"].ToString();
                                    lsdrm_name = objODBCDatareader["level_one"].ToString();
                                }
                                objODBCDatareader.Close();
                                values.RM_EmployeeGid = lsemployee_gid;
                                if (values.ApplicationNumber != "" || values.ApplicationNumber != null)
                                {
                                    var lsvertical = MstverticalList.Where(x => x.vertical_name.ToLower().Trim() == values.Vertical.ToLower().Trim()).FirstOrDefault();
                                    if (lsvertical != null)
                                        lsvertical_gid = lsvertical.vertical_gid;
                                    var lsConstitution = MstconstitutionList.Where(x => x.constitution_name.ToLower().Trim() == values.Constitution.ToLower().Trim()).FirstOrDefault();
                                    if (lsConstitution != null)
                                        lsConstitution_gid = lsConstitution.constitution_gid;
                                    var lsvernacularlan = Mstvernacularlang_list.Where(x => x.vernacular_language.ToLower().Trim() == values.Vernacular_Language.ToLower().Trim()).FirstOrDefault();
                                    if (lsvernacularlan != null)
                                        lsvernacularlan_gid = lsvernacularlan.vernacularlanguage_gid;
                                    var lsdesignation = MstdesignationList.Where(x => x.designation_type.ToLower().Trim() == values.Designation.ToLower().Trim()).FirstOrDefault();
                                    if (lsdesignation != null)
                                        lsdesignation_gid = lsdesignation.designation_gid;
                                    var lscredit_group = MstCreditgroupList.Where(x => x.creditgroup_name.ToLower().Trim() == values.Credit_Group.ToLower().Trim()).FirstOrDefault();
                                    if (lscredit_group != null)
                                        lscredit_groupgid = lscredit_group.creditgroup_gid;
                                    var lsprogram = MstprogramList.Where(x => x.program.ToLower().Trim() == values.Program.ToLower().Trim()).FirstOrDefault();
                                    if (lsprogram != null)
                                        lsprogram_gid = lsprogram.program_gid;
                                    var lsproduct_dtl = MstproductnameList.Where(x => x.product_name.ToLower().Trim() == values.Product.ToLower().Trim()).FirstOrDefault();
                                    if (lsproduct_dtl != null)
                                    {
                                        lsproduct_gid = lsproduct_dtl.product_gid;
                                        values.Sector_Strategic_BusinessUnit = lsproduct_dtl.businessunit_name;
                                        values.Category = lsproduct_dtl.valuechain_name;

                                        try
                                        {
                                            var lsvarietydtl = MstVarietynameList.Where(x => x.variety_name.ToLower() == values.Variety.ToLower() && x.product_gid == lsproduct_gid).FirstOrDefault();
                                            if (lsvarietydtl != null)
                                            {
                                                lsvariety_gid = lsvarietydtl.variety_gid;
                                                values.Botanical_Name = lsvarietydtl.botanical_name;
                                                values.Alternative_Names = lsvarietydtl.alternative_name;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            lsvariety_gid = "";
                                            values.Botanical_Name = "";
                                            values.Alternative_Names = "";
                                        }
                                    }

                                    msSQL = " select distinct b.baselocation_gid,b.baselocation_name,c.cluster_gid,c.cluster_name,c.employee_name as clusterhead,c.vertical_name as clustervertical," +
                                          " e.region_gid,e.region_name,e.employee_name as regionhead,g.zonal_gid,g.zonal_name,g.employee_name as zonalhead ," +
                                          " c.employee_gid as clusterhead_gid,e.employee_gid as regionhead_gid, " +
                                          " g.employee_gid as zonalhead_gid,h.employee_gid as businesshead_gid, " +
                                          " h.employee_name as businesshead from hrm_mst_temployee a" +
                                          " left join sys_mst_tcluster2baselocation b on b.baselocation_gid = a.baselocation_gid" +
                                          " left  join sys_mst_tclusterhead c on b.cluster_gid = c.cluster_gid" +
                                          " left join sys_mst_tregion2cluster d on c.cluster_gid = d.cluster_gid" +
                                          " left join sys_mst_tregionhead e on d.region_gid = e.region_gid" +
                                          " left join sys_mst_tzone2region f on f.region_gid = d.region_gid" +
                                          " left join sys_mst_tzonalhead g on f.zone_gid = g.zonal_gid" +
                                          " left join sys_mst_tbusinesshead h on h.zone_gid = g.zonal_gid where a.employee_gid = '" + lsemployee_gid + "' and" +
                                          " c.vertical_gid = '" + lsvertical_gid + "'" +
                                          " and e.vertical_gid = '" + lsvertical_gid + "' and " +
                                          " g.vertical_gid = '" + lsvertical_gid + "' and h.vertical_gid = '" + lsvertical_gid + "'" +
                                          " and c.program_gid = '" + lsprogram_gid + "' and e.program_gid = '" + lsprogram_gid + "' and " +
                                          " g.program_gid = '" + lsprogram_gid + "' and h.program_gid = '" + lsprogram_gid + "' " +
                                          " and e.status='Y' and g.status='Y' and h.status='Y' and c.status='Y'";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        lsclusterhead = objODBCDatareader["clusterhead"].ToString();
                                        lsregionalhead = objODBCDatareader["regionhead"].ToString();
                                        lszonalhead = objODBCDatareader["zonalhead"].ToString();
                                        lsbusinesshead = objODBCDatareader["businesshead"].ToString();
                                        lsclusterheadgid = objODBCDatareader["clusterhead_gid"].ToString();
                                        lsregionalheadgid = objODBCDatareader["regionhead_gid"].ToString();
                                        lszonalheadgid = objODBCDatareader["zonalhead_gid"].ToString();
                                        lsbusinessheadgid = objODBCDatareader["businesshead_gid"].ToString();
                                        lsbaselocationgid = objODBCDatareader["baselocation_gid"].ToString();
                                        lsbaselocationname = objODBCDatareader["baselocation_name"].ToString();
                                        lsclustergid = objODBCDatareader["cluster_gid"].ToString();
                                        lsclustername = objODBCDatareader["cluster_name"].ToString();
                                        lsregiongid = objODBCDatareader["region_gid"].ToString();
                                        lsregionname = objODBCDatareader["region_name"].ToString();
                                        lszonalgid = objODBCDatareader["zonal_gid"].ToString();
                                        lszonalname = objODBCDatareader["zonal_name"].ToString();
                                        objODBCDatareader.Close();

                                        values.application_gid = objcmnfunctions.GetMasterGID("APPC");
                                        msSQL = " insert into ocs_mst_tapplication(" +
                                                " application_gid," +
                                                " customerref_name, " +
                                                " application_no," +
                                                " migration_applicationno, " +
                                                " vertical_gid," +
                                                " vertical_name," +
                                                " constitution_gid," +
                                                " constitution_name," +
                                                " sa_status," +
                                                " saname_gid," +
                                                " sa_name," +
                                                " relationshipmanager_name," +
                                                " relationshipmanager_gid," +
                                                " drm_gid," +
                                                " drm_name," +
                                                " vernacular_language," +
                                                " vernacularlanguage_gid," +
                                                " contactpersonfirst_name," +
                                                " contactpersonmiddle_name," +
                                                " contactpersonlast_name," +
                                                " designation_gid," +
                                                " designation_type," +
                                                " baselocation_gid," +
                                                " baselocation_name," +
                                                " cluster_gid," +
                                                " cluster_name," +
                                                " region_gid," +
                                                " region_name," +
                                                " zone_gid," +
                                                " zone_name," +
                                                " clustermanager_gid," +
                                                " clustermanager_name," +
                                                " zonalhead_name," +
                                                " zonalhead_gid," +
                                                " regionalhead_name," +
                                                " regionalhead_gid," +
                                                " businesshead_name," +
                                                " businesshead_gid," +
                                                " creditgroup_gid," +
                                                " creditgroup_name," +
                                                " program_gid," +
                                                " program_name," +
                                                " product_gid," +
                                                " product_name," +
                                                " variety_gid," +
                                                " variety_name," +
                                                " sector_name," +
                                                " category_name," +
                                                " botanical_name," +
                                                " alternative_name," +
                                                " status,";
                                        if (values.Renew_Enhancement_Flag == "R")
                                        {
                                            msSQL += " renewal_flag,";
                                        }
                                        else
                                        {
                                            msSQL += " enhancement_flag,";
                                        }
                                        msSQL += " created_by," +
                                                " created_date) values(" +
                                                "'" + values.application_gid + "'," +
                                                "'" + values.Applicant_Name + "'," +
                                                "'" + values.ARN + "'," +
                                                "'" + values.ApplicationNumber + "'," +
                                                "'" + lsvertical_gid + "'," +
                                                "'" + values.Vertical + "'," +
                                                "'" + lsConstitution_gid + "'," +
                                                "'" + values.Constitution + "'," +
                                                "'" + values.Application_FromSA_Yes_No + "'," +
                                                "'" + values.SAMAssociate_IDName + "'," +
                                                "'" + values.SAMAssociate_IDName + "'," +
                                                "'" + lsemployee_name + "'," +
                                                "'" + lsemployee_gid + "'," +
                                                "'" + lsdrm_gid + "'," +
                                                "'" + lsdrm_name + "'," +
                                                "'" + values.Vernacular_Language + "'," +
                                                "'" + lsvernacularlan_gid + "'," +
                                                "'" + values.First_Name + "'," +
                                                "'" + values.Middle_Name + "'," +
                                                "'" + values.Last_Name + "'," +
                                                "'" + lsdesignation_gid + "'," +
                                                "'" + values.Designation + "'," +
                                                "'" + lsbaselocationgid + "'," +
                                                "'" + lsbaselocationname + "'," +
                                                "'" + lsclustergid + "'," +
                                                "'" + lsclustername + "'," +
                                                "'" + lsregiongid + "'," +
                                                "'" + lsregionname + "'," +
                                                "'" + lszonalgid + "'," +
                                                "'" + lszonalname + "'," +
                                                "'" + lsclusterheadgid + "'," +
                                                "'" + lsclusterhead + "'," +
                                                "'" + lszonalhead + "'," +
                                                "'" + lszonalheadgid + "'," +
                                                "'" + lsregionalhead + "'," +
                                                "'" + lsregionalheadgid + "'," +
                                                "'" + lsbusinesshead + "'," +
                                                "'" + lsbusinessheadgid + "'," +
                                                "'" + lscredit_groupgid + "'," +
                                                "'" + values.Credit_Group + "'," +
                                                "'" + lsprogram_gid + "'," +
                                                "'" + values.Program + "'," +
                                                "'" + lsproduct_gid + "'," +
                                                "'" + values.Product + "'," +
                                                "'" + lsvariety_gid + "'," +
                                                "'" + values.Variety + "'," +
                                                "'" + values.Sector_Strategic_BusinessUnit + "'," +
                                                "'" + values.Category + "'," +
                                                "'" + values.Botanical_Name + "'," +
                                                "'" + values.Alternative_Names + "'," +
                                                "'Completed',";
                                        if (values.Renew_Enhancement_Flag == "R")
                                        {
                                            msSQL += "'Y',";
                                        }
                                        else
                                        {
                                            msSQL += "'Y',";
                                        }
                                        msSQL += "'" + lsemployee_gid + "'," +
                                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                        // Genetic Code by Business Team 
                                        foreach (var i in MstGeneticList)
                                        {
                                            msGetGid = objcmnfunctions.GetMasterGID("A2GC");
                                            msSQL = " insert into ocs_mst_tapplication2geneticcode(" +
                                                   " application2geneticcode_gid," +
                                                   " application_gid," +
                                                   " geneticcode_gid," +
                                                   " geneticcode_name," +
                                                   " genetic_status," +
                                                   " genetic_remarks," +
                                                   " created_by," +
                                                   " created_date)" +
                                                   " values(" +
                                                   "'" + msGetGid + "'," +
                                                   "'" + values.application_gid + "'," +
                                                   "'" + i.geneticcode_gid + "'," +
                                                   "'" + i.geneticcode_name.Replace("'", " ") + "'," +
                                                   "'" + values.Genetic_Status + "'," +
                                                   "'" + values.Observations.Replace("'", " ") + "'," +
                                                   "'" + lsemployee_gid + "'," +
                                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        }

                                        //    Mobile Number(s)  
                                        msGetGid = objcmnfunctions.GetMasterGID("A2CN");
                                        msSQL = " insert into ocs_mst_tapplication2contactno(" +
                                                " application2contact_gid," +
                                                " application_gid," +
                                                " mobile_no," +
                                                " primary_mobileno," +
                                                " whatsapp_mobileno," +
                                                " created_by," +
                                                " created_date)" +
                                                " values(" +
                                                "'" + msGetGid + "'," +
                                                "'" + values.application_gid + "'," +
                                                "'" + values.Mobile_Number + "'," +
                                                "'" + values.Mobile_Primary_Status + "'," +
                                                "'" + values.Whatapp_Number + "'," +
                                                "'" + lsemployee_gid + "'," +
                                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                        // Mail ID(s) 
                                        msGetGid = objcmnfunctions.GetMasterGID("A2EA");
                                        msSQL = " insert into ocs_mst_tapplication2email(" +
                                                " application2email_gid," +
                                                " application_gid," +
                                                " email_address," +
                                                " primary_emailaddress," +
                                                " created_by," +
                                                " created_date)" +
                                                " values(" +
                                                "'" + msGetGid + "'," +
                                                "'" + values.application_gid + "'," +
                                                "'" + values.Email_Address + "'," +
                                                "'" + values.Email_Primary_Status + "'," +
                                                "'" + lsemployee_gid + "'," +
                                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                        msGetGid = objcmnfunctions.GetMasterGID("AP2P");
                                        msSQL = " insert into ocs_mst_tapplication2product (" +
                                                " application2product_gid," +
                                                " application2loan_gid," +
                                                " application_gid," +
                                                " product_gid," +
                                                " product_name," +
                                                " variety_gid," +
                                                " variety_name," +
                                                " sector_name," +
                                                " category_name," +
                                                " botanical_name," +
                                                " alternative_name," +
                                                " created_by," +
                                                " created_date)" +
                                                " values(" +
                                                "'" + msGetGid + "'," +
                                                "null," +
                                                "'" + values.application_gid + "'," +
                                                "'" + lsproduct_gid + "'," +
                                                "'" + values.Product + "'," +
                                                "'" + lsvariety_gid + "'," +
                                                "'" + values.Variety + "'," +
                                                "'" + values.Sector_Strategic_BusinessUnit + "'," +
                                                "'" + values.Category + "'," +
                                                "'" + values.Botanical_Name + "'," +
                                                "'" + values.Alternative_Names + "'," +
                                                "'" + lsemployee_gid + "'," +
                                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                        DaApplicationList.Add(values);
                                        LogForAudit(" General - '" + values.ApplicationNumber + "' Completed");
                                    }
                                    else
                                    {
                                        LogForAudit(" Application No: '" + values.ApplicationNumber + "' - Location / Vertical not Assign for Business Approval'");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                LogForAudit(" General Info Error - '" + values.ApplicationNumber + "' - '" + ex.ToString() + "'");
                            }
                        }
                    }

                }
                 }
               
            }
            LogForAudit("---------General Info - Completed successfully !--------------");
            return DaApplicationList;
        }


        // Company Info 
        public List<CompanyDetailsList> DaSubmitCompanyInfo(List<CompanyDetailsList> datalist, List<ApplicationList> applicationdtl)
        {
            LogForAudit("----Company Info - Started -----------");

            // Company Type List
            msSQL = " SELECT companytype_gid,companytype_name FROM ocs_mst_tcompanytype where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<Mstcompanytype_list> Mstcompanytype_list = new List<Mstcompanytype_list>();
            Mstcompanytype_list = ConvertDataTable<Mstcompanytype_list>(dt_datatable);

            // Stakeholder Type

            msSQL = " SELECT usertype_gid,user_type FROM ocs_mst_tusertype where status_log='Y' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<Mstusertypelist> Mstusertypelist = new List<Mstusertypelist>();
            Mstusertypelist = ConvertDataTable<Mstusertypelist>(dt_datatable);

            // Assessment Agency  
            msSQL = " select assessmentagency_gid, assessmentagency_name from ocs_mst_tassessmentagency where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<Mstassessmentagencylist> Mstassessmentagencylist = new List<Mstassessmentagencylist>();
            Mstassessmentagencylist = ConvertDataTable<Mstassessmentagencylist>(dt_datatable);

            // Agency rating  
            msSQL = " select assessmentagencyrating_gid, assessmentagencyrating_name from ocs_mst_tassessmentagencyrating where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<MstassessmentagencyratingList> MstassessmentagencyratingList = new List<MstassessmentagencyratingList>();
            MstassessmentagencyratingList = ConvertDataTable<MstassessmentagencyratingList>(dt_datatable);

            // AML Category list  
            msSQL = " SELECT amlcategory_gid,amlcategory_name FROM ocs_mst_tamlcategory where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<MstamlcategoryList> MstamlcategoryList = new List<MstamlcategoryList>();
            MstamlcategoryList = ConvertDataTable<MstamlcategoryList>(dt_datatable);

            // Business Category 
            msSQL = " SELECT businesscategory_gid,businesscategory_name FROM ocs_mst_tbusinesscategory where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<MstbusinesscategoryList> MstbusinesscategoryList = new List<MstbusinesscategoryList>();
            MstbusinesscategoryList = ConvertDataTable<MstbusinesscategoryList>(dt_datatable);

            // Designation List 
            msSQL = " SELECT designation_gid,designation_type FROM ocs_mst_tdesignation where status_log='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<Mstdesignationlist> Mstdesignationlist = new List<Mstdesignationlist>();
            Mstdesignationlist = ConvertDataTable<Mstdesignationlist>(dt_datatable);

            // Address Type 
            msSQL = " SELECT address_gid,address_type FROM ocs_mst_taddresstype order by address_gid asc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<Mstaddresstypelist> Mstaddresstypelist = new List<Mstaddresstypelist>();
            Mstaddresstypelist = ConvertDataTable<Mstaddresstypelist>(dt_datatable);

            // Postal Code
            msSQL = " select postalcode_value,city,taluka,district,state from ocs_mst_tpostalcode";
            //" postalcode_value='" + postal_code + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<MstContactAddresslist> MstContactAddresslist = new List<MstContactAddresslist>();
            MstContactAddresslist = ConvertDataTable<MstContactAddresslist>(dt_datatable);

            // license type
            msSQL = " SELECT licensetype_gid,licensetype_name FROM ocs_mst_tlicensetype where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<Mstlicensetypelist> Mstlicensetypelist = new List<Mstlicensetypelist>();
            Mstlicensetypelist = ConvertDataTable<Mstlicensetypelist>(dt_datatable);

            // Nearest Samunnati Branch
            msSQL = " SELECT branch_gid,branch_name FROM hrm_mst_tbranch";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<Mstbranchlist> Mstbranchlist = new List<Mstbranchlist>();
            Mstbranchlist = ConvertDataTable<Mstbranchlist>(dt_datatable);

            // TAN State
            msSQL = " SELECT state_gid,state_name FROM ocs_mst_tstate";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<MstTanStatelist> MstTanStatelist = new List<MstTanStatelist>();
            MstTanStatelist = ConvertDataTable<MstTanStatelist>(dt_datatable);

            // Internal Rating
            msSQL = " SELECT internalrating_gid,internalrating_name FROM ocs_mst_tinternalrating where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<MstInternalratinglist> MstInternalratinglist = new List<MstInternalratinglist>();
            MstInternalratinglist = ConvertDataTable<MstInternalratinglist>(dt_datatable);

            // City List
            msSQL = " SELECT city_gid,city_name FROM ocs_mst_tcity ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<MstCitylist> MstCitylist = new List<MstCitylist>();
            MstCitylist = ConvertDataTable<MstCitylist>(dt_datatable);

            // Equiqment Name List
            msSQL = " SELECT equipment_gid,equipment_name FROM ocs_mst_tequipment where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<Mstequipmentlist> Mstequipmentlist = new List<Mstequipmentlist>();
            Mstequipmentlist = ConvertDataTable<Mstequipmentlist>(dt_datatable);

            // Livestock List
            msSQL = " SELECT livestock_gid,livestock_name FROM ocs_mst_tlivestock where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<Mstlivestocklist> Mstlivestocklist = new List<Mstlivestocklist>();
            Mstlivestocklist = ConvertDataTable<Mstlivestocklist>(dt_datatable);

            List<CompanyDetailsList> Dacompanydtl = new List<CompanyDetailsList>();
            foreach (var values in datalist)
            {
                try
                {
                    string lsapplication_gid = "", employee_gid = "", lscompanytype_gid = "", lsstackholder_gid = "", lsassessmentagency_gid = "";
                    string lsassessmentagencyrating_gid = "", lsamlcategory_gid = "", lsbusinesscategory_gid = "", lsdesignation_gid = "", lsmobileno = "";
                    string lsemail_address = "", lsaddress_gid = "", lslicense_gid = "", lsbranch_gid = "", lsstate_gid = "", lsinternalrating_gid = "";
                    string lscity_gid = "", lsequipment_gid = "", lslivestock_gid="";

                    var getapplicationgid = applicationdtl.Where(x => x.ApplicationNumber == values.ApplicationNumber).FirstOrDefault();
                    if (getapplicationgid != null)
                    {
                        values.application_gid = getapplicationgid.application_gid;
                        employee_gid = getapplicationgid.RM_EmployeeGid;
                        values.created_by = getapplicationgid.RM_EmployeeGid;
                    }
                    if (values.application_gid != "")
                    {
                        var lscompanytype = Mstcompanytype_list.Where(x => x.companytype_name.ToLower().Trim() == values.CompanyType.ToLower().Trim()).FirstOrDefault();
                        if (lscompanytype != null)
                            lscompanytype_gid = lscompanytype.companytype_gid;
                        var lsstackholder = Mstusertypelist.Where(x => x.user_type.ToLower().Trim() == values.StakeholderType.ToLower().Trim()).FirstOrDefault();
                        if (lsstackholder != null)
                            lsstackholder_gid = lsstackholder.usertype_gid;
                        var lsassessmentagency = Mstassessmentagencylist.Where(x => x.assessmentagency_name.ToLower().Trim() == values.CreditRatingAgency.ToLower().Trim()).FirstOrDefault();
                        if (lsassessmentagency != null)
                            lsassessmentagency_gid = lsassessmentagency.assessmentagency_gid;
                        var lsassessmentagencyrating = MstassessmentagencyratingList.Where(x => x.assessmentagencyrating_name.ToLower().Trim() == values.CreditRating.ToLower().Trim()).FirstOrDefault();
                        if (lsassessmentagencyrating != null)
                            lsassessmentagencyrating_gid = lsassessmentagencyrating.assessmentagencyrating_gid;
                        var lsamlcategory = MstamlcategoryList.Where(x => x.amlcategory_name.ToLower().Trim() == values.CategoryAML.ToLower().Trim()).FirstOrDefault();
                        if (lsamlcategory != null)
                            lsamlcategory_gid = lsamlcategory.amlcategory_gid;
                        var lsbusinesscategory = MstbusinesscategoryList.Where(x => x.businesscategory_name.ToLower().Trim() == values.CategoryBusiness.ToLower().Trim()).FirstOrDefault();
                        if (lsbusinesscategory != null)
                            lsbusinesscategory_gid = lsbusinesscategory.businesscategory_gid;
                        var lsdesignation = Mstdesignationlist.Where(x => x.designation_type.ToLower().Trim() == values.Designation.ToLower().Trim()).FirstOrDefault();
                        if (lsdesignation != null)
                            lsdesignation_gid = lsdesignation.designation_gid;
                        var lsaddress = Mstaddresstypelist.Where(x => x.address_type.ToLower().Trim() == values.AddressType.ToLower().Trim()).FirstOrDefault();
                        if (lsaddress != null)
                            lsaddress_gid = lsaddress.address_gid;
                        var lslicense = Mstlicensetypelist.Where(x => x.licensetype_name.ToLower().Trim() == values.LicenseType.ToLower().Trim()).FirstOrDefault();
                        if (lslicense != null)
                            lslicense_gid = lslicense.licensetype_gid;
                        var lsbranch = Mstbranchlist.Where(x => x.branch_name.ToLower().Trim() == values.NearestSamunnatiBranch.ToLower().Trim()).FirstOrDefault();
                        if (lsbranch != null)
                            lsbranch_gid = lsbranch.branch_gid;
                        var lstanstate = MstTanStatelist.Where(x => x.state_name.ToLower().Trim() == values.TANState.ToLower().Trim()).FirstOrDefault();
                        if (lstanstate != null)
                            lsstate_gid = lstanstate.state_gid;
                        var lsinternalrating = MstInternalratinglist.Where(x => x.internalrating_name.ToLower().Trim() == values.InternalRating.ToLower().Trim()).FirstOrDefault();
                        if (lsinternalrating != null)
                            lsinternalrating_gid = lsinternalrating.internalrating_gid;
                        var lscity = MstCitylist.Where(x => x.city_name.ToLower().Trim() == values.FPOCity.ToLower().Trim()).FirstOrDefault();
                        if (lscity != null)
                            lscity_gid = lscity.city_gid;
                        var lsequipment = Mstequipmentlist.Where(x => x.equipment_name.ToLower().Trim() == values.EquiqmentName.ToLower().Trim()).FirstOrDefault();
                        if (lsequipment != null)
                            lsequipment_gid = lsequipment.equipment_gid;
                        var lslivestock = Mstlivestocklist.Where(x => x.livestock_name.ToLower().Trim() == values.LivestockName.ToLower().Trim()).FirstOrDefault();
                        if (lslivestock != null)
                            lslivestock_gid = lslivestock.livestock_gid;
                        var contact_dtl = MstContactAddresslist.Where(x => x.postalcode_value.ToLower().Trim() == values.PostalCode.ToLower().Trim()).FirstOrDefault();
                        if (contact_dtl != null)
                        {
                            values.City = contact_dtl.city;
                            values.Taluka = contact_dtl.taluka;
                            values.District = contact_dtl.district;
                            values.State = contact_dtl.state;
                        }
                        if (values.BusinessStartDate == "" || values.BusinessStartDate == null)
                        {
                        }
                        else
                        {
                            var date = DateTime.Parse(new string(values.BusinessStartDate.Take(24).ToArray()));
                            var businessstartdate = date.ToString("yyyy/MM/dd");

                            msSQL = "select TIMESTAMPDIFF( YEAR, ('" + businessstartdate + "'), now() ) as year";
                            values.YearsinBusiness = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = "select TIMESTAMPDIFF( MONTH, ('" + businessstartdate + "'), now() ) % 12 as month";
                            values.MonthsinBusiness = objdbconn.GetExecuteScalar(msSQL);
                        }

                        msGetGid = objcmnfunctions.GetMasterGID("APIN");
                        values.institution_gid = msGetGid;
                        string institution_gid = msGetGid;
                        msSQL = " insert into ocs_mst_tinstitution(" +
                                 " institution_gid," +
                                 " application_gid," +
                                 " company_name," +
                                 " date_incorporation," +
                                 " businessstart_date," +
                                 " year_business," +
                                 " month_business," +
                                 " companypan_no," +
                                 " cin_no," +
                                 " official_telephoneno," +
                                 " officialemail_address," +
                                 " companytype_gid," +
                                 " companytype_name," +
                                 " stakeholdertype_gid," +
                                 " stakeholder_type," +
                                 " assessmentagency_gid," +
                                 " assessmentagency_name," +
                                 " assessmentagencyrating_gid," +
                                 " assessmentagencyrating_name," +
                                 " ratingas_on," +
                                 " amlcategory_gid," +
                                 " amlcategory_name," +
                                 " businesscategory_gid," +
                                 " businesscategory_name," +
                                 " contactperson_firstname," +
                                 " contactperson_middlename," +
                                 " contactperson_lastname," +
                                 " designation_gid," +
                                 " designation," +
                                 " start_date," +
                                 " end_date," +
                                 " lastyear_turnover," +
                                 " escrow," +
                                 " urn_status," +
                                 " urn," +
                                 " institution_status," +
                                 " nearsamunnatiabranch_gid," +
                                 " nearsamunnatiabranch_name," +
                                 " udhayam_registration," +
                                 " tan_number," +
                                 " business_description," +
                                 " tanstate_gid," +
                                 " tanstate_name," +
                                 " internalrating_gid," +
                                 " internalrating_name," +
                                 " sales," +
                                 " purchase," +
                                 " credit_summation," +
                                 " cheque_bounce," +
                                 " numberof_boardmeetings," +
                                 " farmer_count," +
                                 " crop_cycle," +
                                 " calamities_prone," +
                                 " created_by," +
                                 " created_date) values(" +
                               "'" + msGetGid + "'," +
                               "'" + values.application_gid + "'," +
                               "'" + values.LegalTradeName + "',";
                        if ((values.CertificateofIncorporation == null) || (values.CertificateofIncorporation == ""))
                        {
                            msSQL += "null,";
                        }
                        else
                        {
                            msSQL += "'" + Convert.ToDateTime(values.CertificateofIncorporation).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                        }
                        if ((values.BusinessStartDate == null) || (values.BusinessStartDate == ""))
                        {
                            msSQL += "null,";
                        }
                        else
                        {
                            msSQL += "'" + Convert.ToDateTime(values.BusinessStartDate).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                        }
                        msSQL += "'" + values.YearsinBusiness + "'," +
                                "'" + values.MonthsinBusiness + "'," +
                                "'" + values.PANValue + "'," +
                                "'" + values.CorporateIdentificationNumber + "'," +
                                "'" + values.OfficialContactNumber + "'," +
                                "'" + values.OfficialMailID + "'," +
                                "'" + lscompanytype_gid + "'," +
                                "'" + values.CompanyType + "'," +
                                "'" + lsstackholder_gid + "'," +
                                "'" + values.StakeholderType + "'," +
                                "'" + lsassessmentagency_gid + "'," +
                                "'" + values.CreditRatingAgency + "'," +
                                "'" + lsassessmentagencyrating_gid + "'," +
                                "'" + values.CreditRating + "',";
                        if ((values.AssessedOn == null) || (values.AssessedOn == ""))
                        {
                            msSQL += "null,";
                        }
                        else
                        {
                            msSQL += "'" + Convert.ToDateTime(values.AssessedOn).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                        }
                        msSQL += "'" + lsamlcategory_gid + "'," +
                                "'" + values.CategoryAML + "'," +
                                "'" + lsbusinesscategory_gid + "'," +
                                "'" + values.CategoryBusiness + "'," +
                                "'" + values.FirstName + "'," +
                                "'" + values.MiddleName + "'," +
                                "'" + values.LastName + "'," +
                                "'" + lsdesignation_gid + "'," +
                                "'" + values.Designation + "',";
                        if ((values.StartDate == null) || (values.StartDate == ""))
                        {
                            msSQL += "null,";
                        }
                        else
                        {
                            msSQL += "'" + Convert.ToDateTime(values.StartDate).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                        }
                        if ((values.EndDate == null) || (values.EndDate == ""))
                        {
                            msSQL += "null,";
                        }
                        else
                        {
                            msSQL += "'" + Convert.ToDateTime(values.EndDate).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                        }
                        msSQL += "'" + values.LastYearTurnOver + "'," +
                                "'" + values.Escrow + "'," +
                                "'" + values.HavingURN + "'," +
                                "'" + values.IfYesURN + "'," +
                                "'Completed'," +
                                "'" + lsbranch_gid + "'," +
                                "'" + values.NearestSamunnatiBranch + "'," +
                                "'" + values.UdhayamRegistration + "'," +
                                "'" + values.TAN + "'," +
                                "'" + values.BusinessDescription + "'," +
                                "'" + lsstate_gid + "'," +
                                "'" + values.TANState + "'," +
                                "'" + lsinternalrating_gid + "'," +
                                "'" + values.InternalRating + "'," +
                                "'" + values.Sales + "'," +
                                "'" + values.Purchase + "'," +
                                "'" + values.CreditSummation + "'," +
                                "'" + values.ChequeBounce + "'," +
                                "'" + values.NoofBoardMeetings + "'," +
                                "'" + values.FarmerCount + "'," +
                                "'" + values.CropCycle + "'," +
                                "'" + values.CalamitiesProne + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            // Mobile Number 
                            msGetGid = objcmnfunctions.GetMasterGID("IT2M");
                            msSQL = " insert into ocs_mst_tinstitution2mobileno(" +
                                    " institution2mobileno_gid," +
                                    " institution_gid," +
                                    " mobile_no," +
                                    " primary_status," +
                                    " whatsapp_no," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + institution_gid + "'," +
                                    "'" + values.MobileNumber + "'," +
                                    "'" + values.MobilePrimaryStatus + "'," +
                                    "'" + values.WhatappNumber + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            // Email Address
                            msGetGid = objcmnfunctions.GetMasterGID("IT2E");
                            msSQL = " insert into ocs_mst_tinstitution2email(" +
                                    " institution2email_gid," +
                                    " institution_gid," +
                                    " email_address," +
                                    " primary_status," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + institution_gid + "'," +
                                    "'" + values.EmailAddress + "'," +
                                    "'" + values.EmailPrimaryStatus + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            // Address Details
                            msGetGid = objcmnfunctions.GetMasterGID("IT2A");
                            msSQL = " insert into ocs_mst_tinstitution2address(" +
                                    " institution2address_gid," +
                                    " institution_gid," +
                                    " addresstype_gid," +
                                    " addresstype_name," +
                                    " addressline1," +
                                    " addressline2," +
                                    " primary_status," +
                                    " landmark," +
                                    " postal_code," +
                                    " city," +
                                    " taluka," +
                                    " district," +
                                    " state," +
                                    " country," +
                                    " latitude," +
                                    " longitude," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + institution_gid + "'," +
                                    "'" + lsaddress_gid + "'," +
                                    "'" + values.AddressType + "'," +
                                    "'" + values.AddressLine1 + "'," +
                                    "'" + values.AddressLine2 + "'," +
                                    "'" + values.PrimaryStatus + "'," +
                                    "'" + values.LandMark + "'," +
                                    "'" + values.PostalCode + "'," +
                                    "'" + values.City + "'," +
                                    "'" + values.Taluka + "'," +
                                    "'" + values.District + "'," +
                                    "'" + values.State + "'," +
                                    "'" + values.Country + "'," +
                                    "'" + values.Latitude + "'," +
                                    "'" + values.Longitude + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            // License Details
                            msGetGid = objcmnfunctions.GetMasterGID("IT2L");
                            msSQL = " insert into ocs_mst_tinstitution2licensedtl(" +
                                    " institution2licensedtl_gid," +
                                    " institution_gid," +
                                    " licensetype_gid," +
                                    " licensetype_name," +
                                    " license_no," +
                                    " issue_date," +
                                    " expiry_date," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + institution_gid + "'," +
                                    "'" + lslicense_gid + "'," +
                                    "'" + values.LicenseType + "'," +
                                    "'" + values.Number + "',";
                            if ((values.IssueDate == null) || (values.IssueDate == ""))
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(values.IssueDate).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }
                            if ((values.ExpiryDate == null) || (values.ExpiryDate == ""))
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(values.ExpiryDate).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }
                            msSQL += "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            //  FPO Coverage Area
                            msGetGid = objcmnfunctions.GetMasterGID("I2FC");
                            msSQL = " insert into ocs_mst_tinstitution2fpocacity(" +
                                    " institution2fpocacity_gid," +
                                    " institution_gid," +
                                    " city_gid," +
                                    " city_name," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + institution_gid + "'," +
                                    "'" + lscity_gid + "'," +
                                    "'" + values.FPOCity + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            // Equipment Holding
                            msGetGid = objcmnfunctions.GetMasterGID("I2EH");
                            msSQL = " insert into ocs_mst_tinstitution2equipment(" +
                                    " institution2equipment_gid," +
                                    " institution_gid," +
                                    " equipment_gid," +
                                    " equipment_name," +
                                    " availablerenthire," +
                                    " quantity," +
                                    " description," +
                                    " insurance_status," +
                                    " insurance_details," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + institution_gid + "'," +
                                    "'" + lsequipment_gid + "'," +
                                    "'" + values.EquiqmentName + "'," +
                                    "'" + values.AvailableforRentHire + "'," +
                                    "'" + values.Quantity + "'," +
                                    "'" + values.Description + "'," +
                                    "'" + values.EquipmentInsuranceStatus + "'," +
                                    "'" + values.EquipmentInsuranceDetails + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            //  Livestock Holdings
                            msGetGid = objcmnfunctions.GetMasterGID("I2LH");
                            msSQL = " insert into ocs_mst_tinstitution2livestock(" +
                                    " institution2livestock_gid," +
                                    " institution_gid," +
                                    " livestock_gid," +
                                    " livestock_name," +
                                    " count," +
                                    " Breed," +
                                    " insurance_status," +
                                    " insurance_details," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + institution_gid + "'," +
                                    "'" + lslivestock_gid + "'," +
                                    "'" + values.LivestockName + "'," +
                                    "'" + values.LivestockCount + "'," +
                                    "'" + values.Breed + "'," +
                                    "'" + values.LivestockInsuranceStatus + "'," +
                                    "'" + values.LivestockInsuranceDetails + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            // Receivable
                            msGetGid = objcmnfunctions.GetMasterGID("I2RE");
                            msSQL = " insert into ocs_mst_tinstitution2receivable(" +
                                    " institution2receivable_gid," +
                                    " institution_gid," +
                                    " receivable_date," +
                                    " onetothirty_days," +
                                    " thirtyonetosixty_days," +
                                    " sixtyonetoninety_days," +
                                    " ninety_days," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + institution_gid + "',";
                                    if (values.ReceivableDate == null || values.ReceivableDate == "")
                                    {
                                        msSQL += "null,";
                                    }
                                    else
                                    {
                                        msSQL += "'" + Convert.ToDateTime(values.ReceivableDate).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                                    }
                           msSQL += "'" + values.onetothirty_days + "'," +
                                    "'" + values.thirtyonetosixty_days + "'," +
                                    "'" + values.sixtyonetoninety_days + "'," +
                                    "'" + values.ninety_days + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (values.StakeholderType == "Borrower" || values.StakeholderType == "Applicant")
                            {
                                msSQL = "select mobile_no from ocs_mst_tinstitution2mobileno where institution_gid='" + institution_gid + "' and primary_status='yes'";
                                lsmobileno = objdbconn.GetExecuteScalar(msSQL);

                                msSQL = "select email_address from ocs_mst_tinstitution2email where institution_gid='" + institution_gid + "' and primary_status='yes'";
                                lsemail_address = objdbconn.GetExecuteScalar(msSQL);

                                msSQL = "update ocs_mst_tapplication set applicant_type ='Institution' where application_gid='" + values.application_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                msSQL = "update ocs_mst_tinstitution set mobile_no='" + lsmobileno + "'," +
                                 " email_address='" + lsemail_address + "' where institution_gid='" + institution_gid + "' ";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }

                            Dacompanydtl.Add(values);
                        }
                        else
                        {
                            LogForAudit("----Company Info Error - Not added Error '" + values.ApplicationNumber + "'");
                        }
                    }
                    else
                    {
                        LogForAudit("----Company Info Error - Application Gid Not Found '" + values.ApplicationNumber + "'");
                    }
                }
                catch (Exception ex)
                {
                    LogForAudit("----Company Info Error - '" + values.ApplicationNumber + "' - '" + ex.ToString() + "'-----------");
                }
            }
            LogForAudit("---------Company Info - Completed successfully !--------------");
            return Dacompanydtl;
        }

        // Individual Info 

        public List<IndividualDetailsList> DaSubmitIndividualInfo(List<IndividualDetailsList> datalist, List<ApplicationList> applicationdtl)
        {

            LogForAudit("----Individual Info - Started ------");
            // gender
            msSQL = " SELECT gender_gid,gender_name FROM ocs_mst_tgender where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<MstGender_list> MstGender_list = new List<MstGender_list>();
            MstGender_list = ConvertDataTable<MstGender_list>(dt_datatable);

            // Designation Master
            msSQL = " SELECT a.designation_gid,a.designation_type from ocs_mst_tdesignation a" +
                 "  where status_log='Y' order by a.designation_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<MstdesignationList> MstdesignationList = new List<MstdesignationList>();
            MstdesignationList = ConvertDataTable<MstdesignationList>(dt_datatable);

            // Educational Qualification
            msSQL = " SELECT educationalqualification_gid,educationalqualification_name FROM ocs_mst_teducationalqualification where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<MsteducationalqualificationList> MsteducationalqualificationList = new List<MsteducationalqualificationList>();
            MsteducationalqualificationList = ConvertDataTable<MsteducationalqualificationList>(dt_datatable);

            // Stakeholder Type

            msSQL = " SELECT usertype_gid,user_type FROM ocs_mst_tusertype where status_log='Y' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<Mstusertypelist> Mstusertypelist = new List<Mstusertypelist>();
            Mstusertypelist = ConvertDataTable<Mstusertypelist>(dt_datatable);

            // Marital Status 
            msSQL = " SELECT maritalstatus_gid,maritalstatus_name FROM ocs_mst_tmaritalstatus where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<MstMaritalStatusList> MstMaritalStatusList = new List<MstMaritalStatusList>();
            MstMaritalStatusList = ConvertDataTable<MstMaritalStatusList>(dt_datatable);

            // Ownership type 
            msSQL = " SELECT ownershiptype_gid,ownershiptype_name FROM ocs_mst_townershiptype where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<MstownershiptypeList> MstownershiptypeList = new List<MstownershiptypeList>();
            MstownershiptypeList = ConvertDataTable<MstownershiptypeList>(dt_datatable);

            // Income Type 

            msSQL = " SELECT incometype_gid,incometype_name FROM ocs_mst_tincometype where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<MstIncometypeList> MstIncometypeList = new List<MstIncometypeList>();
            MstIncometypeList = ConvertDataTable<MstIncometypeList>(dt_datatable);

            // Property Holder
            msSQL = " SELECT propertyin_gid,propertyin_name FROM ocs_mst_tpropertyinname where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<MstPropertyList> MstPropertyList = new List<MstPropertyList>();
            MstPropertyList = ConvertDataTable<MstPropertyList>(dt_datatable);

            // Resistance Type
            msSQL = " SELECT residencetype_gid,residencetype_name FROM ocs_mst_tresidencetype where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<MstResistanceList> MstResistanceList = new List<MstResistanceList>();
            MstResistanceList = ConvertDataTable<MstResistanceList>(dt_datatable);

            // Postal Code
            msSQL = " select postalcode_value,city,taluka,district,state from ocs_mst_tpostalcode";
            //" postalcode_value='" + postal_code + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<MstContactAddresslist> MstContactAddresslist = new List<MstContactAddresslist>();
            MstContactAddresslist = ConvertDataTable<MstContactAddresslist>(dt_datatable);

            // Address Type 
            msSQL = " SELECT address_gid,address_type FROM ocs_mst_taddresstype order by address_gid asc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<Mstaddresstypelist> Mstaddresstypelist = new List<Mstaddresstypelist>();
            Mstaddresstypelist = ConvertDataTable<Mstaddresstypelist>(dt_datatable);

            // Physical Status
            msSQL = " SELECT physicalstatus_gid,physicalstatus_name FROM ocs_mst_tphysicalstatus where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<Mstphysicalstatuslist> Mstphysicalstatuslist = new List<Mstphysicalstatuslist>();
            Mstphysicalstatuslist = ConvertDataTable<Mstphysicalstatuslist>(dt_datatable);

            // Nearest Samunnati Branch
            msSQL = " SELECT branch_gid,branch_name FROM hrm_mst_tbranch";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<Mstbranchlist> Mstbranchlist = new List<Mstbranchlist>();
            Mstbranchlist = ConvertDataTable<Mstbranchlist>(dt_datatable);

            // Internal Rating
            msSQL = " SELECT internalrating_gid,internalrating_name FROM ocs_mst_tinternalrating where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<MstInternalratinglist> MstInternalratinglist = new List<MstInternalratinglist>();
            MstInternalratinglist = ConvertDataTable<MstInternalratinglist>(dt_datatable);

            // Equiqment Name List
            msSQL = " SELECT equipment_gid,equipment_name FROM ocs_mst_tequipment where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<Mstequipmentlist> Mstequipmentlist = new List<Mstequipmentlist>();
            Mstequipmentlist = ConvertDataTable<Mstequipmentlist>(dt_datatable);

            // Livestock List
            msSQL = " SELECT livestock_gid,livestock_name FROM ocs_mst_tlivestock where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<Mstlivestocklist> Mstlivestocklist = new List<Mstlivestocklist>();
            Mstlivestocklist = ConvertDataTable<Mstlivestocklist>(dt_datatable);

            List<IndividualDetailsList> DaIndividualList = new List<IndividualDetailsList>();
            foreach (var values in datalist)
            {
                try
                {
                    string lsapplication_gid = "", employee_gid = "", lsgender_gid = "", lsemail_address = "", lsdesignation_gid = "", lsqualification_gid = "";
                    string lsstackholder_gid = "", lsMaritalstatus_gid = "", lsownershiptype_gid = "", lsincometype_gid = "", lsgroup_gid = "", lspropertyholder_gid = "";
                    string lsresidencetype_gid = "", lsaddress_gid = "", lsphysicalstatus_gid= "", lsbranch_gid = "", lsinternalrating_gid = "";
                    string lsequipment_gid = "", lslivestock_gid = "";

                    var getapplicationgid = applicationdtl.Where(x => x.ApplicationNumber == values.ApplicationNumber).FirstOrDefault();
                    if (getapplicationgid != null)
                    {
                        lsapplication_gid = getapplicationgid.application_gid;
                        values.application_gid = lsapplication_gid;
                        employee_gid = getapplicationgid.RM_EmployeeGid;
                        values.created_by = getapplicationgid.RM_EmployeeGid;
                    }
                    if (lsapplication_gid != "")
                    {
                        var lsdesignation = MstdesignationList.Where(x => x.designation_type.ToLower().Trim() == values.Designation.ToLower().Trim()).FirstOrDefault();
                        if (lsdesignation != null)
                            lsdesignation_gid = lsdesignation.designation_gid;
                        var lsstackholder = Mstusertypelist.Where(x => x.user_type.ToLower().Trim() == values.Stakeholder_Type.ToLower().Trim()).FirstOrDefault();
                        if (lsstackholder != null)
                            lsstackholder_gid = lsstackholder.usertype_gid;
                        var lsgender = MstGender_list.Where(x => x.gender_name.ToLower().Trim() == values.Gender.ToLower().Trim()).FirstOrDefault();
                        if (lsgender != null)
                            lsgender_gid = lsgender.gender_gid;
                        var lsMaritalstatus = MstMaritalStatusList.Where(x => x.maritalstatus_name.ToLower().Trim() == values.Marital_Status.ToLower().Trim()).FirstOrDefault();
                        if (lsMaritalstatus != null)
                            lsMaritalstatus_gid = lsMaritalstatus.maritalstatus_gid;
                        var lsownershiptype = MstownershiptypeList.Where(x => x.ownershiptype_name.ToLower().Trim() == values.Ownership_Type.ToLower().Trim()).FirstOrDefault();
                        if (lsownershiptype != null)
                            lsownershiptype_gid = lsownershiptype.ownershiptype_gid;
                        var lsqualification = MsteducationalqualificationList.Where(x => x.educationalqualification_name.ToLower().Trim() == values.Educational_Qualification.ToLower().Trim()).FirstOrDefault();
                        if (lsqualification != null)
                            lsqualification_gid = lsqualification.educationalqualification_gid;
                        var lsincometype = MstIncometypeList.Where(x => x.incometype_name.ToLower().Trim() == values.Income_Type.ToLower().Trim()).FirstOrDefault();
                        if (lsincometype != null)
                            lsincometype_gid = lsincometype.incometype_gid;
                        var lspropertyholder = MstPropertyList.Where(x => x.propertyin_name.ToLower().Trim() == values.Property_intheNameof.ToLower().Trim()).FirstOrDefault();
                        if (lspropertyholder != null)
                            lspropertyholder_gid = lspropertyholder.propertyin_gid;
                        var lsresidencetype = MstResistanceList.Where(x => x.residencetype_name.ToLower().Trim() == values.Residence_Type.ToLower().Trim()).FirstOrDefault();
                        if (lsresidencetype != null)
                            lsresidencetype_gid = lsresidencetype.residencetype_gid;
                        var lsaddress = Mstaddresstypelist.Where(x => x.address_type.ToLower().Trim() == values.Address_Type.ToLower().Trim()).FirstOrDefault();
                        if (lsaddress != null)
                            lsaddress_gid = lsaddress.address_gid;
                        var lsphysicalstatus = Mstphysicalstatuslist.Where(x => x.physicalstatus_name.ToLower().Trim() == values.PhysicalStatus.ToLower().Trim()).FirstOrDefault();
                        if (lsphysicalstatus != null)
                            lsphysicalstatus_gid = lsphysicalstatus.physicalstatus_gid;
                        var lsinternalrating = MstInternalratinglist.Where(x => x.internalrating_name.ToLower().Trim() == values.InternalRating.ToLower().Trim()).FirstOrDefault();
                        if (lsinternalrating != null)
                            lsinternalrating_gid = lsinternalrating.internalrating_gid;
                        var lsbranch = Mstbranchlist.Where(x => x.branch_name.ToLower().Trim() == values.NearestSamunnatiBranch.ToLower().Trim()).FirstOrDefault();
                        if (lsbranch != null)
                            lsbranch_gid = lsbranch.branch_gid;
                        var lsequipment = Mstequipmentlist.Where(x => x.equipment_name.ToLower().Trim() == values.EquiqmentName.ToLower().Trim()).FirstOrDefault();
                        if (lsequipment != null)
                            lsequipment_gid = lsequipment.equipment_gid;
                        var lslivestock = Mstlivestocklist.Where(x => x.livestock_name.ToLower().Trim() == values.LivestockName.ToLower().Trim()).FirstOrDefault();
                        if (lslivestock != null)
                            lslivestock_gid = lslivestock.livestock_gid;
                        var contact_dtl = MstContactAddresslist.Where(x => x.postalcode_value.Trim() == values.Postal_Code.Trim()).FirstOrDefault();
                        if (contact_dtl != null)
                        {
                            values.City = contact_dtl.city;
                            values.Taluka = contact_dtl.taluka;
                            values.District = contact_dtl.district;
                            values.State = contact_dtl.state;
                        }

                        msGetGid = objcmnfunctions.GetMasterGID("CTCT");
                        string lscontact_gid = msGetGid;
                        values.individual_gid = lscontact_gid;
                        msSQL = " SELECT company_name, institution_gid from ocs_mst_tinstitution where application_gid='" + lsapplication_gid + "' " +
                                " and company_name='" + values.If_company_Institution_yes_Company_Name + "' order by institution_gid desc ";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            values.If_company_Institution_yes_Company_Name = objODBCDatareader["company_name"].ToString();
                            values.institution_gid = objODBCDatareader["institution_gid"].ToString();
                        }
                        objODBCDatareader.Close();
                        if (values.If_Group_Yes_Group_Name == "NA")
                            lsgroup_gid = "NA";
                        if (values.PAN_Status_Yes_No.ToLower() == "yes")
                            values.PAN_Status_Yes_No = "Customer Submitting PAN";
                        else
                            values.PAN_Status_Yes_No = "Customer Submitting Form 60";
                        msSQL = " insert into ocs_mst_tcontact(" +
                               " contact_gid," +
                               " application_gid," +
                               " application_no," +
                               " pan_status," +
                               " pan_no," +
                               " aadhar_no," +
                               " first_name," +
                               " middle_name," +
                               " last_name," +
                               " individual_dob," +
                               " age," +
                               " gender_gid," +
                               " gender_name," +
                               " designation_gid," +
                               " designation_name," +
                               " educationalqualification_gid," +
                               " educationalqualification_name," +
                               " main_occupation," +
                               " annual_income," +
                               " monthly_income," +
                               " pep_status," +
                               " pepverified_date," +
                               " stakeholdertype_gid," +
                               " stakeholder_type," +
                               " maritalstatus_gid," +
                               " maritalstatus_name," +
                               " father_firstname," +
                               " father_middlename," +
                               " father_lastname," +
                               " father_dob," +
                               " father_age," +
                               " mother_firstname," +
                               " mother_middlename," +
                               " mother_lastname," +
                               " mother_dob," +
                               " mother_age," +
                               " spouse_firstname," +
                               " spouse_middlename," +
                               " spouse_lastname," +
                               " spouse_dob," +
                               " spouse_age," +
                               " ownershiptype_gid," +
                               " ownershiptype_name," +
                               " propertyholder_gid," +
                               " propertyholder_name," +
                               " residencetype_gid," +
                               " residencetype_name," +
                               " incometype_gid," +
                               " incometype_name," +
                               " currentresidence_years," +
                               " branch_distance," +
                               " group_gid," +
                               " group_name," +
                               " profile," +
                               " urn_status," +
                               " urn," +
                               " fathernominee_status," +
                               " mothernominee_status," +
                               " spousenominee_status," +
                               " othernominee_status," +
                               " relationshiptype," +
                               " nomineefirst_name," +
                               " nominee_middlename," +
                               " nominee_lastname," +
                               " nominee_dob," +
                               " nominee_age," +
                               " totallandinacres," +
                               " cultivatedland," +
                               " previouscrop," +
                               " prposedcrop," +
                               " institution_gid," +
                               " institution_name," +
                               " contact_status," +
                               " nearsamunnatiabranch_gid," +
                               " nearsamunnatiabranch_name," +
                               " internalrating_gid," +
                               " internalrating_name," +
                               " physicalstatus_gid," +
                               " physicalstatus_name," +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGetGid + "'," +
                               "'" + lsapplication_gid + "'," +
                               "'" + values.ApplicationNumber + "'," +
                               "'" + values.PAN_Status_Yes_No + "'," +
                               "'" + values.PAN_Value + "'," +
                               "'" + values.Aadhar_Number + "',";
                        if (values.First_Name == "" || values.First_Name == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.First_Name.Replace("'", "") + "',";
                        }
                        if (values.Middle_Name == "" || values.Middle_Name == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.Middle_Name.Replace("'", "") + "',";
                        }
                        if (values.Last_Name == "" || values.Last_Name == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.Last_Name.Replace("'", "") + "',";
                        }
                        msSQL += "'" +  Convert.ToDateTime(values.Date_of_Birth).ToString("dd-MM-yyyy") + "'," +
                                 "'" + values.Age + "'," +
                                 "'" + lsgender_gid + "'," +
                                 "'" + values.Gender + "'," +
                                 "'" + lsdesignation_gid + "'," +
                                 "'" + values.Designation + "'," +
                                 "'" + lsqualification_gid + "'," +
                                 "'" + values.Educational_Qualification + "'," +
                                 "'" + values.Main_Occupation + "'," +
                                 "'" + values.Annual_Income + "'," +
                                 "'" + values.Monthly_Income + "'," +
                                 "'" + values.Politically_Exposed_person + "',";

                        if ((values.PEP_Verified_On == null) || (values.PEP_Verified_On == ""))
                        {
                            msSQL += "null,";
                        }
                        else
                        {
                            msSQL += "'" + Convert.ToDateTime(values.PEP_Verified_On).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                        }
                        msSQL += "'" + lsstackholder_gid + "'," +
                                 "'" + values.Stakeholder_Type + "'," +
                                 "'" + lsMaritalstatus_gid + "'," +
                                 "'" + values.Marital_Status + "',";
                        if (values.Father_First_Name == "" || values.Father_First_Name == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.Father_First_Name.Replace("'", "") + "',";
                        }
                        if (values.Father_Middle_Name == "" || values.Father_Middle_Name == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.Father_Middle_Name.Replace("'", "") + "',";
                        }
                        if (values.Father_Last_Name == "" || values.Father_Last_Name == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.Father_Last_Name.Replace("'", "") + "',";
                        }
                        msSQL += "'" + values.Father_Date_of_Birth + "'," +
                                 "'" + values.Father_Age + "',";
                        if (values.Mother_First_Name == "" || values.Mother_First_Name == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.Mother_First_Name.Replace("'", "") + "',";
                        }
                        if (values.Mother_Middle_Name == "" || values.Mother_Middle_Name == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.Mother_Middle_Name.Replace("'", "") + "',";
                        }
                        if (values.Mother_Last_Name == "" || values.Mother_Last_Name == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.Mother_Last_Name.Replace("'", "") + "',";
                        }
                        msSQL += "'" + values.Mother_Date_of_Birth + "'," +
                                 "'" + values.Mother_Age + "',";
                        if (values.Spouse_First_Name == "" || values.Spouse_First_Name == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.Spouse_First_Name.Replace("'", "") + "',";
                        }
                        if (values.Spouse_Middle_Name == "" || values.Spouse_Middle_Name == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.Spouse_Middle_Name.Replace("'", "") + "',";
                        }
                        if (values.Spouse_Last_Name == "" || values.Spouse_Last_Name == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.Spouse_Last_Name.Replace("'", "") + "',";
                        }
                        msSQL += "'" + values.Spouse_Date_of_Birth + "'," +
                                 "'" + values.Spouse_Age + "'," +
                                 "'" + lsownershiptype_gid + "'," +
                                 "'" + values.Ownership_Type + "'," +
                                 "'" + lspropertyholder_gid + "'," +
                                 "'" + values.Property_intheNameof + "'," +
                                 "'" + lsresidencetype_gid + "'," +
                                 "'" + values.Residence_Type + "'," +
                                 "'" + lsincometype_gid + "'," +
                                 "'" + values.Income_Type + "'," +
                                 "'" + values.Yearsin_Current_Residence + "'," +
                                 "'" + values.Distance_from_BranchRegional_Office + "'," +
                                 "'" + lsgroup_gid + "'," +
                                 "'" + values.If_Group_Yes_Group_Name + "'," +
                                 "'" + values.Profile + "'," +
                                 "'" + values.Having_URN + "'," +
                                 "'" + values.If_Yes_URN + "'," +
                                 "'" + values.Nominee_Yes_No + "'," +
                                 "'" + values.Mother_Nominee_Yes_No + "'," +
                                 "'" + values.Spouse_Nominee_Yes_No + "'," +
                                 "'" + values.Other_Nominee_Yes_No + "'," +
                                 "'" + values.Relationship_type + "',";
                        if (values.Nominee_First_Name == "" || values.Nominee_First_Name == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.Nominee_First_Name.Replace("'", "") + "',";
                        }
                        if (values.Nominee_Middle_Name == "" || values.Nominee_Middle_Name == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.Nominee_Middle_Name.Replace("'", "") + "',";
                        }
                        if (values.Nominee_Last_Name == "" || values.Nominee_Last_Name == null)
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + values.Nominee_Last_Name.Replace("'", "") + "',";
                        }
                        msSQL += "'" + values.Nominee_Date_of_Birth + "'," +
                                 "'" + values.Nominee_Age + "'," +
                                 "'" + values.Total_Land_in_Acres + "'," +
                                 "'" + values.Cultivated_Land + "'," +
                                 "'" + values.Previous_Crop + "'," +
                                 "'" + values.Proposed_Crop + "'," +
                                 "'" + values.institution_gid + "'," +
                                 "'" + values.If_company_Institution_yes_Company_Name + "'," +
                                 "'Completed'," +
                                 "'" + lsbranch_gid + "'," +
                                 "'" + values.NearestSamunnatiBranch + "'," +
                                 "'" + lsinternalrating_gid + "'," +
                                 "'" + values.InternalRating + "'," +
                                 "'" + lsphysicalstatus_gid + "'," +
                                 "'" + values.PhysicalStatus + "'," +
                                 "'" + employee_gid + "'," +
                                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult != 0)
                        {
                            msGetGid = objcmnfunctions.GetMasterGID("C2MN");
                            msSQL = " insert into ocs_mst_tcontact2mobileno(" +
                                    " contact2mobileno_gid," +
                                    " contact_gid," +
                                    " mobile_no," +
                                    " primary_status," +
                                    " whatsapp_no," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + lscontact_gid + "'," +
                                    "'" + values.Mobile_Number + "'," +
                                    "'" + values.Mobile_Primary_Status + "'," +
                                    "'" + values.Whatapp_Number + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msGetGid = objcmnfunctions.GetMasterGID("C2EA");
                            msSQL = " insert into ocs_mst_tcontact2email(" +
                                    " contact2email_gid," +
                                    " contact_gid," +
                                    " email_address," +
                                    " primary_status," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + lscontact_gid + "'," +
                                    "'" + values.Email_Address + "'," +
                                    "'" + values.Email_Primary_Status + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msGetGid = objcmnfunctions.GetMasterGID("C2AD");
                            msSQL = " insert into ocs_mst_tcontact2address(" +
                                    " contact2address_gid," +
                                    " contact_gid," +
                                    " addresstype_gid," +
                                    " addresstype_name," +
                                    " primary_status," +
                                    " addressline1," +
                                    " addressline2," +
                                    " landmark," +
                                    " postal_code," +
                                    " city," +
                                    " taluka," +
                                    " district," +
                                    " state," +
                                    " country," +
                                    " latitude," +
                                    " longitude," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + lscontact_gid + "'," +
                                    "'" + lsaddress_gid + "'," +
                                    "'" + values.Address_Type + "'," +
                                    "'" + values.AddressType_Primary_Status + "'," +
                                    "'" + values.Address_Line_1 + "'," +
                                    "'" + values.Address_Line_2 + "'," +
                                    "'" + values.Land_Mark + "'," +
                                    "'" + values.Postal_Code + "'," +
                                    "'" + values.City + "'," +
                                    "'" + values.Taluka + "'," +
                                    "'" + values.District + "'," +
                                    "'" + values.State + "'," +
                                    "'" + values.Country + "'," +
                                    "'" + values.Latitude + "'," +
                                    "'" + values.Longitude + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msGetGid = objcmnfunctions.GetMasterGID("C2EH");
                            msSQL = " insert into ocs_mst_tcontact2equipment(" +
                                    " contact2equipment_gid," +
                                    " contact_gid," +
                                    " equipment_gid," +
                                    " equipment_name," +
                                    " availablerenthire," +
                                    " quantity," +
                                    " description," +
                                    " insurance_status," +
                                    " insurance_details," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + lscontact_gid + "'," +
                                    "'" + lsequipment_gid + "'," + 
                                    "'" + values.EquiqmentName + "'," +
                                    "'" + values.AvailableforRentHire + "'," +
                                    "'" + values.Quantity + "'," +
                                    "'" + values.Description + "'," +
                                    "'" + values.InsuranceStatus + "'," +
                                    "'" + values.EquipmentInsuranceDetails + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msGetGid = objcmnfunctions.GetMasterGID("C2LH");
                            msSQL = " insert into ocs_mst_tcontact2livestock(" +
                                    " contact2livestock_gid," +
                                    " contact_gid," +
                                    " livestock_gid," +
                                    " livestock_name," +
                                    " count," +
                                    " Breed," +
                                    " insurance_status," +
                                    " insurance_details," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + lscontact_gid + "'," +
                                    "'" + lslivestock_gid + "'," +
                                    "'" + values.LivestockName + "'," +
                                    "'" + values.LivestockCount + "'," +
                                    "'" + values.Breed + "'," +
                                    "'" + values.LivestockInsuranceStatus + "'," +
                                    "'" + values.LivestockInsuranceDetails + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (values.Stakeholder_Type == "Borrower" || values.Stakeholder_Type == "Applicant")
                            {
                                msSQL = "select mobile_no from ocs_mst_tcontact2mobileno where contact_gid='" + lscontact_gid + "' and primary_status='yes'";
                                string lsmobileno = objdbconn.GetExecuteScalar(msSQL);

                                msSQL = "select email_address from ocs_mst_tcontact2email where contact_gid='" + lscontact_gid + "' and primary_status='yes'";
                                lsemail_address = objdbconn.GetExecuteScalar(msSQL);

                                msSQL = "update ocs_mst_tapplication set applicant_type ='Individual' where application_gid='" + lsapplication_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                msSQL = "update ocs_mst_tcontact set mobile_no='" + lsmobileno + "'," +
                                    " email_address='" + lsemail_address + "' where contact_gid='" + lscontact_gid + "' ";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }

                            DaIndividualList.Add(values);
                        }
                        else
                        {
                            LogForAudit("Individual Info Error - Not added Error '" + values.ApplicationNumber + "'");
                        }
                    }
                    else
                    {
                        LogForAudit("Individual Info Error - Application Gid Not Found '" + values.ApplicationNumber + "'");
                    }

                }
                catch (Exception ex)
                {
                    LogForAudit("Individual Info Error - '" + values.ApplicationNumber + "' - '" + ex.ToString() + "'");
                }

            } 
            LogForAudit("---------Individual Info - Completed successfully !--------------");
            return DaIndividualList;
        }

        // Economic Capital Details
        public void DaSubmitEconomicCapitalDtl(List<EconomicCapitalDetails> datalist, List<ApplicationList> applicationdtl)
        {
            LogForAudit("---------Economic Capital Details - Started !--------------");
            foreach (var values in datalist)
            {
                try
                {
                    string lsapplication_gid = "";
                    var getapplicationgid = applicationdtl.Where(x => x.ApplicationNumber == values.ApplicationNumber).FirstOrDefault();
                    if (getapplicationgid != null)
                    {
                        lsapplication_gid = getapplicationgid.application_gid;
                    }
                    if (lsapplication_gid != "")
                    {
                        msSQL = " update ocs_mst_tapplication set ";
                        if (values.SocialCapital == null || values.SocialCapital == "")
                        {
                            msSQL += " social_capital='',";
                        }
                        else
                        {
                            msSQL += " social_capital='" + values.SocialCapital.Replace("'", "") + "',";
                        }
                        if (values.TradeCapital == null || values.TradeCapital == "")
                        {
                            msSQL += " trade_capital='',";
                        }
                        else
                        {
                            msSQL += " trade_capital='" + values.TradeCapital.Replace("'", "") + "',";
                        }
                        msSQL += " economical_flag='Y' " +
                                 " where application_gid ='" + lsapplication_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {
                        LogForAudit("Economic Capital Info Error - Application Gid Not Found '" + values.ApplicationNumber + "'");
                    }

                }
                catch (Exception ex)
                {
                    LogForAudit("----Economic Capital Info Error - '" + values.ApplicationNumber + "' - '" + ex.ToString() + "'-----------");
                }
            }

            LogForAudit("---------Economic Capital Details - Completed successfully !--------------");
        }

        public void DaSubmitOverallLimitDtl(List<OverallLimitDetails> datalist, List<ApplicationList> applicationdtl)
        {
            LogForAudit("---------OverallLimit Details - Started !--------------");
            foreach (var values in datalist)
            {
                try
                {
                    string lsapplication_gid = "";
                    var getapplicationgid = applicationdtl.Where(x => x.ApplicationNumber == values.ApplicationNumber).FirstOrDefault();
                    if (getapplicationgid != null)
                        lsapplication_gid = getapplicationgid.application_gid;
                    if (lsapplication_gid != "")
                    {
                        msSQL = " update ocs_mst_tapplication set " +
                                " overalllimit_amount='" + values.Overalllimitamount + "'," +
                                " validityoveralllimit_year='" + values.Validityofoveralllimityears + "'," +
                                " validityoveralllimit_month='" + values.Validityofoveralllimitmonth + "'," +
                                " validityoveralllimit_days='" + values.Validityofoveralllimitdays + "'," +
                                " calculationoveralllimit_validity='" + values.Calculationofoveralllimitvalidity + "'," +
                                " productcharge_flag='Y'," +
                                " productcharges_status='Incomplete' " +
                                " where application_gid='" + lsapplication_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {
                        LogForAudit("----OverallLimit Info Error -  Application Gid Not Found'" + values.ApplicationNumber + "'");
                    }

                }
                catch (Exception ex)
                {
                    LogForAudit("----OverallLimit Info Error - '" + values.ApplicationNumber + "' - '" + ex.ToString() + "'-----------");
                }
            }

            LogForAudit("---------OverallLimit Details Completed successfully !--------------");
        }

        public void DaSubmitServiceChargesDtl(List<ServiceChargesDetails> datalist, List<ApplicationList> applicationdtl)
        {
            LogForAudit("---------Service Charges Details - Started !--------------");
            //msSQL = "select producttype_gid,product_type from ocs_mst_tapplication2loan where application_gid='" + application_gid + "'";
            //dt_datatable = objdbconn.GetDataTable(msSQL);
            // Product Master
            msSQL = " SELECT product_gid,product_name,businessunit_gid,businessunit_name,valuechain_gid,valuechain_name FROM ocs_mst_tproducts a" +
                     " where status='Y' order by a.product_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<MstproductnameList> MstproductnameList = new List<MstproductnameList>();
            MstproductnameList = ConvertDataTable<MstproductnameList>(dt_datatable);
            foreach (var values in datalist)
            {
                try
                {
                    string lsapplication_gid = "";
                    string lsemployee_gid = string.Empty;
                    var getapplicationgid = applicationdtl.Where(x => x.ApplicationNumber == values.ApplicationNumber).FirstOrDefault();
                    if (getapplicationgid != null)
                    {
                        lsapplication_gid = getapplicationgid.application_gid;
                        lsemployee_gid = getapplicationgid.RM_EmployeeGid;
                    }
                    if (lsapplication_gid != "")
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("AP2C");
                        string lsproduct_gid = string.Empty;

                        var lsproduct = MstproductnameList.Where(x => x.product_name.ToLower().Trim() == values.ProductType.ToLower().Trim()).FirstOrDefault();
                        if (lsproduct != null)
                            lsproduct_gid = lsproduct.product_gid;

                        msSQL = "insert into ocs_mst_tapplicationservicecharge(" +
                            " application2servicecharge_gid," +
                            " application_gid," +
                            " processing_fee," +
                            " processing_collectiontype," +
                            " doc_charges," +
                            " doccharge_collectiontype," +
                            " fieldvisit_charges," +
                            " fieldvisit_charges_collectiontype," +
                            " adhoc_fee," +
                            " adhoc_collectiontype," +
                            " life_insurance," +
                            " lifeinsurance_collectiontype," +
                            " acct_insurance," +
                            " acctinsurance_collectiontype," +
                            " total_collect," +
                            " total_deduct," +
                            " product_type," +
                            " producttype_gid," +
                            " created_by," +
                            " created_date) values(" +
                             "'" + msGetGid + "'," +
                                   "'" + lsapplication_gid + "'," +
                                   "'" + values.ProcessingFeeInitiationFee + "'," +
                                   "'" + values.Processing_CollectionType + "'," +
                                   "'" + values.DocumentationCharges + "'," +
                                   "'" + values.Documentation_CollectionType + "'," +
                                   "'" + values.FieldVisitCharges + "'," +
                                   "'" + values.FieldVisit_CollectionType + "'," +
                                   "'" + values.AdhocFee + "'," +
                                   "'" + values.AdhocFee_CollectionType + "'," +
                                   "'" + values.TermLifeInsurance + "'," +
                                   "'" + values.TermLife_CollectionType + "'," +
                                   "'" + values.PersonalAccidentInsurance + "'," +
                                   "'" + values.PersonalAccident_CollectionType + "'," +
                                   "'" + values.TotalCollectable + "'," +
                                   "'" + values.TotalDeductable + "'," +
                                   "'" + values.ProductType + "'," +
                                   "'" + lsproduct_gid + "'," +
                                   "'" + lsemployee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            msSQL = "update ocs_mst_tapplication set productcharges_status='Completed',productcharge_flag='Y' where application_gid = '" + lsapplication_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        LogForAudit("----Service Charges Info Error - Application Gid Not Found '" + values.ApplicationNumber + "'");
                    }

                }
                catch (Exception ex)
                {
                    LogForAudit("----Service Charges Info Error - '" + values.ApplicationNumber + "' - '" + ex.ToString() + "'-----------");
                }
            }
            LogForAudit("---------Service Charges - Completed successfully !--------------");
        }

        // Product Details 
        public void DaSubmitProductLoanDtl(List<ProductDetails> datalist, List<ApplicationList> applicationdtl)
        {
            LogForAudit("---------Product Loan Details - Started !--------------");
            // Product Master
            msSQL = " SELECT product_gid,product_name,businessunit_gid,businessunit_name,valuechain_gid,valuechain_name FROM ocs_mst_tproducts a" +
                     " where status='Y' order by a.product_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<MstproductnameList> MstproductnameList = new List<MstproductnameList>();
            MstproductnameList = ConvertDataTable<MstproductnameList>(dt_datatable);

            // Variety Master 
            msSQL = " SELECT product_gid,variety_gid,variety_name,botanical_name,alternative_name FROM ocs_mst_tvariety a " +
                    " order by a.product_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<MstVarietynameList> MstVarietynameList = new List<MstVarietynameList>();
            MstVarietynameList = ConvertDataTable<MstVarietynameList>(dt_datatable);

            // Loan product   
            msSQL = " SELECT loanproduct_gid,loanproduct_name FROM ocs_mst_tloanproduct a" +
                      " where status='Y' order by a.loanproduct_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<Mstloanproductlist> Mstloanproductlist = new List<Mstloanproductlist>();
            Mstloanproductlist = ConvertDataTable<Mstloanproductlist>(dt_datatable);

            // Loan Sub product 

            msSQL = "select loanproduct_gid,loansubproduct_gid,loansubproduct_name from ocs_mst_tloansubproduct";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<MstloanSubproductlist> MstloanSubproductlist = new List<MstloanSubproductlist>();
            MstloanSubproductlist = ConvertDataTable<MstloanSubproductlist>(dt_datatable);

            //LoanType
            msSQL = " SELECT loantype_gid,loan_type FROM ocs_mst_tloantype a" +
                       " where status_log='Y' order by a.loantype_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<Mstloantypelist> Mstloantypelist = new List<Mstloantypelist>();
            Mstloantypelist = ConvertDataTable<Mstloantypelist>(dt_datatable);

            //Principal Frequency
            msSQL = " SELECT principalfrequency_gid,principalfrequency_name FROM ocs_mst_tprincipalfrequency a" +
                        " where status='Y' order by a.principalfrequency_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<Mstprincipalfrequencylist> Mstprincipalfrequencylist = new List<Mstprincipalfrequencylist>();
            Mstprincipalfrequencylist = ConvertDataTable<Mstprincipalfrequencylist>(dt_datatable);

            //Interest Frequency
            msSQL = " SELECT  interestfrequency_gid,interestfrequency_name FROM ocs_mst_tinterestfrequency a" +
                       " where status='Y' order by a.interestfrequency_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<Mstinterestfrequencylist> Mstinterestfrequencylist = new List<Mstinterestfrequencylist>();
            Mstinterestfrequencylist = ConvertDataTable<Mstinterestfrequencylist>(dt_datatable);

            //Program
            msSQL = " SELECT  program_gid,program FROM ocs_mst_tprogram a" +
                       " where status='Y' order by a.program_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<Mstprogramlist> Mstprogramlist = new List<Mstprogramlist>();
            Mstprogramlist = ConvertDataTable<Mstprogramlist>(dt_datatable);
            foreach (var values in datalist)
            {
                try
                {
                    string lsapplication_gid = "", employee_gid = "", lsproducttype_gid = "", lsproductsubtype_gid = "", lsloantype_gid = "";
                    string lsvariety_gid = "", lsproduct_gid = "", lsprogram_gid = "", lsprincipalfrequency_gid = "", lsinterestfrequency_gid = "";

                    var getapplicationgid = applicationdtl.Where(x => x.ApplicationNumber == values.ApplicationNumber).FirstOrDefault();
                    if (getapplicationgid != null)
                    {
                        lsapplication_gid = getapplicationgid.application_gid;
                        employee_gid = getapplicationgid.RM_EmployeeGid;
                    }
                    if (lsapplication_gid != "")
                    {
                        var lsproducttype = Mstloanproductlist.Where(x => x.loanproduct_name.ToLower() == values.Product_Type.ToLower()).First();
                        if (lsproducttype != null)
                            lsproducttype_gid = lsproducttype.loanproduct_gid;
                        var lsproductsubtype = MstloanSubproductlist.Where(x => x.loansubproduct_name.ToLower() == values.Product_Sub_Type.ToLower() && x.loanproduct_gid == lsproducttype_gid).FirstOrDefault();
                        if (lsproductsubtype != null)
                            lsproductsubtype_gid = lsproductsubtype.loansubproduct_gid;
                        var lsloantype = Mstloantypelist.Where(x => x.loan_type.ToLower() == values.Loan_Type.ToLower()).FirstOrDefault();
                        if (lsloantype != null)
                            lsloantype_gid = lsloantype.loantype_gid;
                        var lsprincipalfrequency = Mstprincipalfrequencylist.Where(x => x.principalfrequency_name.ToLower() == values.Principal_Frequency.ToLower()).FirstOrDefault();
                        if (lsprincipalfrequency != null)
                            lsprincipalfrequency_gid = lsprincipalfrequency.principalfrequency_gid;
                        var lsinterestfrequency = Mstinterestfrequencylist.Where(x => x.interestfrequency_name.ToLower() == values.Interest_Frequency.ToLower()).FirstOrDefault();
                        if (lsinterestfrequency != null)
                            lsinterestfrequency_gid = lsinterestfrequency.interestfrequency_gid;
                        var lsprogram = Mstprogramlist.Where(x => x.program.ToLower() == values.Product_Program.ToLower()).FirstOrDefault();
                        if (lsprogram != null)
                            lsprogram_gid = lsprogram.program_gid;
                        var lsproduct_dtl = MstproductnameList.Where(x => x.product_name.ToLower() == values.Product_product.ToLower()).FirstOrDefault();
                        if (lsproduct_dtl != null)
                        {
                            lsproduct_gid = lsproduct_dtl.product_gid;
                            values.Sector_Strategic_Business_Unit = lsproduct_dtl.businessunit_name;
                            values.Category = lsproduct_dtl.valuechain_name;

                            var lsvarietydtl = MstVarietynameList.Where(x => x.variety_name == values.Variety && x.product_gid == lsproduct_gid).FirstOrDefault();
                            if (lsvarietydtl != null)
                            {
                                lsvariety_gid = lsvarietydtl.variety_gid;
                                values.Botanical_Name = lsvarietydtl.botanical_name;
                                values.Alternative_Names = lsvarietydtl.alternative_name;
                            }
                        }

                        //msSQL = " select application2loan_gid from ocs_mst_tapplication2loan where producttype_gid='" + lsproducttype_gid + "' and " +
                        //        " productsubtype_gid='" + lsproductsubtype_gid + "' and application_gid='" + lsapplication_gid + "'";
                        //objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        //if (objODBCDatareader.HasRows == false)
                        //{
                        //    objODBCDatareader.Close();
                            //if (values.product_type == "Agri Receivable Finance (ARF)")
                            //{
                            //    msSQL = "select application2buyer_gid from agr_mst_tapplication2buyer  where application2loan_gid='" + employee_gid + "'";
                            //    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            //    if (objODBCDatareader.HasRows == false)
                            //    {
                            //        objODBCDatareader.Close();
                            //        LogForAudit("Kindly add atleast one Buyer"); 
                            //        return;
                            //    }
                            //}
                            msGetGid = objcmnfunctions.GetMasterGID("AP2L");
                            msSQL = " insert into ocs_mst_tapplication2loan(" +
                                   " application2loan_gid ," +
                                   " application_gid," +
                                   " facilityrequested_date," +
                                   " product_type," +
                                   " producttype_gid," +
                                   " productsub_type," +
                                   " productsubtype_gid," +
                                   " loantype_gid," +
                                   " loan_type ," +
                                   " loanfacility_amount," +
                                   " rate_interest," +
                                   " penal_interest," +
                                   " facilityvalidity_year," +
                                   " facilityvalidity_month," +
                                   " facilityvalidity_days," +
                                   " facilityoverall_limit ," +
                                   " tenureproduct_year," +
                                   " tenureproduct_month," +
                                   " tenureproduct_days," +
                                   " tenureoverall_limit ," +
                                   " scheme_type," +
                                   " facility_type," +
                                   " facility_mode," +
                                   " principalfrequency_name," +
                                   " principalfrequency_gid," +
                                   " interestfrequency_name," +
                                   " interestfrequency_gid," +
                                   " program_gid," +
                                   " program," +
                                   " interest_status," +
                                   " moratorium_status," +
                                   " moratorium_type," +
                                   " moratorium_startdate," +
                                   " moratorium_enddate," +
                                   " source_type," +
                                   " guideline_value," +
                                   " guideline_date," +
                                   " marketvalue_date ," +
                                   " market_value," +
                                   " forcedsource_value," +
                                   " collateralSSV_value," +
                                   " forcedvalueassessed_on," +
                                   " collateralobservation_summary," +
                                   " enduse_purpose," +
                                   " product_gid," +
                                   " product_name," +
                                   " variety_gid," +
                                   " variety_name," +
                                   " sector_name," +
                                   " category_name," +
                                   " botanical_name," +
                                   " alternative_name," +
                                   " created_by," +
                                   " created_date)" +
                                       " values(" +
                                       "'" + msGetGid + "'," +
                                       "'" + lsapplication_gid + "'," +
                                       "'" + Convert.ToDateTime(values.Facility_Requested_On).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                       "'" + values.Product_Type + "'," +
                                       "'" + lsproducttype_gid + "'," +
                                       "'" + values.Product_Sub_Type + "'," +
                                       "'" + lsproductsubtype_gid + "'," +
                                       "'" + lsloantype_gid + "'," +
                                       "'" + values.Loan_Type + "'," +
                                       "'" + values.Facility_Loan_Amount + "'," +
                                       "'" + values.Rate_of_Interest_Margin + "'," +
                                       "'" + values.Penal_Interest + "'," +
                                       "'" + values.Validity_ofthe_facility_years + "'," +
                                       "'" + values.Validity_ofthe_facility_months + "'," +
                                       "'" + values.Validity_ofthe_facility_days + "'," +
                                       "'" + values.Calculation_of_overall_limit_validity + "'," +
                                       "'" + values.Tenure_oftheproductCreditPeriod_years + "'," +
                                       "'" + values.Tenure_oftheproductCreditPeriod_months + "'," +
                                       "'" + values.Tenure_oftheproductCreditPeriod_days + "'," +
                                       "'" + values.Tenure_Calculation_ofoveralllimitvalidity + "'," +
                                       "'" + values.Scheme_Type + "'," +
                                       "'" + values.Facility_Type + "'," +
                                       "'" + values.Facility_mode + "'," +
                                       "'" + values.Principal_Frequency + "'," +
                                       "'" + lsprincipalfrequency_gid + "'," +
                                       "'" + values.Interest_Frequency + "'," +
                                       "'" + lsinterestfrequency_gid + "'," +
                                       "'" + lsprogram_gid + "'," +
                                       "'" + values.Product_Program + "'," +
                                       "'" + values.Interest_tobe_deductedupfront + "'," +
                                       "'" + values.Moratorium_Applicable + "'," +
                                       "'" + values.Moratorium_Type + "',";
                            if (values.Moratorium_Start_Date == null || values.Moratorium_Start_Date == "")
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(values.Moratorium_Start_Date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }
                            if (values.Moratorium_End_Date == null || values.Moratorium_End_Date == "")
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(values.Moratorium_End_Date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }
                            msSQL += "'" + values.Source_Type + "',";
                            if (values.Guideline_Value == null || values.Guideline_Value == "")
                            {
                                msSQL += "'0.00',";
                            }
                            else
                            {
                                msSQL += "'" + values.Guideline_Value.Replace(",", "") + "',";
                            }
                            if (values.Guideline_Value_assessedon == null || values.Guideline_Value_assessedon == "")
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(values.Guideline_Value_assessedon).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }
                            if (values.Market_Value_assessedon == null || values.Market_Value_assessedon == "")
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(values.Market_Value_assessedon).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }
                            if (values.Market_Value == null || values.Market_Value == "")
                            {
                                msSQL += "'0.00',";
                            }
                            else
                            {
                                msSQL += "'" + values.Market_Value.Replace(",", "") + "',";
                            }
                            if (values.Forced_Source_Value == null || values.Forced_Source_Value == "")
                            {
                                msSQL += "'0.00',";
                            }
                            else
                            {
                                msSQL += "'" + values.Forced_Source_Value.Replace(",", "") + "',";
                            }
                            if (values.Collateral_FSV == null || values.Collateral_FSV == "")
                            {
                                msSQL += "'0.00',";
                            }
                            else
                            {
                                msSQL += "'" + values.Collateral_FSV.Replace(",", "") + "',";
                            }
                            if (values.Forced_Value_AssessedOn == null || values.Forced_Value_AssessedOn == "")
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(values.Forced_Value_AssessedOn).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }
                            if (values.Observation_Summary == null || values.Observation_Summary == "")
                            {
                                msSQL += "'',";
                            }
                            else
                            {
                                msSQL += "'" + values.Observation_Summary.Replace("'", "") + "',";
                            }
                            if (values.Purpose_of_Loan == null || values.Purpose_of_Loan == "")
                            {
                                msSQL += "'',";
                            }
                            else
                            {
                                msSQL += "'" + values.Purpose_of_Loan.Replace("'", "") + "',";
                            }
                            msSQL += "'" + lsproduct_gid + "'," +
                                     "'" + values.Product_product + "'," +
                                     "'" + lsvariety_gid + "'," +
                                     "'" + values.Variety + "'," +
                                     "'" + values.Sector_Strategic_Business_Unit + "'," +
                                     "'" + values.Category + "'," +
                                     "'" + values.Botanical_Name + "'," +
                                     "'" + values.Alternative_Names + "'," +
                                     "'" + employee_gid + "'," +
                                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " select application2loan_gid from ocs_mst_tapplication2loan where application_gid = '" + lsapplication_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                values.application2loan_gid = objODBCDatareader["application2loan_gid"].ToString();                               
                            }
                            objODBCDatareader.Close();

                            msGetGid = objcmnfunctions.GetMasterGID("AP2P");
                            msSQL = " insert into ocs_mst_tapplication2product (" +
                                    " application2product_gid," +
                                    " application2loan_gid," +
                                    " application_gid," +
                                    " product_gid," +
                                    " product_name," +
                                    " variety_gid," +
                                    " variety_name," +
                                    " sector_name," +
                                    " category_name," +
                                    " botanical_name," +
                                    " alternative_name," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + values.application2loan_gid + "'," +
                                    "null," +
                                    "'" + lsproduct_gid + "'," +
                                    "'" + values.Product_product + "'," +
                                    "'" + lsvariety_gid + "'," +
                                    "'" + values.Variety + "'," +
                                    "'" + values.Sector_Strategic_Business_Unit + "'," +
                                    "'" + values.Category + "'," +
                                    "'" + values.Botanical_Name + "'," +
                                    "'" + values.Alternative_Names + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (mnResult != 0)
                            {
                                //msSQL = "update agr_mst_tapplication2buyer set application2loan_gid='" + msGetGid + "' where application2loan_gid='" + employee_gid + "'";

                                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                msSQL = "update ocs_mst_tapplication set productcharges_status='Completed',productcharge_flag='Y' where application_gid = '" + lsapplication_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            else
                            {
                                LogForAudit(" Product Loan Info Error -  while Adding Loan - '" + values.ApplicationNumber + "'");
                            }
                        //}
                        //else
                        //{
                        //    objODBCDatareader.Close();
                        //    LogForAudit(" Product Loan Info Error -  Already this product sub type added - '" + values.ApplicationNumber + "'");
                        //}
                    }
                    else
                    {
                        LogForAudit(" Product Loan Info Error - Application Gid not found - '" + values.ApplicationNumber + "'");
                    }
                }
                catch (Exception ex)
                {
                    LogForAudit(" Product Loan Info Error - '" + values.ApplicationNumber + "' - '" + ex.ToString() + "' ");
                }


            }
            LogForAudit("---------Product Loan Details - Completed Successfully !--------------");
        }

        // Company  Bureau Details

        public void DaSubmitBureauDtl(List<BureauDetails> datalist, List<CompanyDetailsList> CompanyDtl)
        {
            LogForAudit("---------Company Bureau Details - Started !--------------");
            msSQL = " select bureauname_gid, bureauname_name from ocs_mst_tbureauname where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<MstbureaunameList> MstbureaunameList = new List<MstbureaunameList>();
            MstbureaunameList = ConvertDataTable<MstbureaunameList>(dt_datatable);

            foreach (var values in datalist)
            {
                try
                {
                    string lstype = "", lsbureauname_gid = "";
                    var lsbureauname = MstbureaunameList.Where(x => x.bureauname_name.ToLower() == values.BureauName.ToLower()).FirstOrDefault();
                    if (lsbureauname != null)
                        lsbureauname_gid = lsbureauname.bureauname_gid;

                    string lsapplication_gid = "", employee_gid = "",  lsinstitution_gid = "";
                    var getCompanydtl = CompanyDtl.Where(x => x.ApplicationNumber == values.ApplicationNumber && x.LegalTradeName == values.NameofIndividualorcompany).FirstOrDefault();
                    if (getCompanydtl != null)
                    {
                        lsapplication_gid = getCompanydtl.application_gid;
                        employee_gid = getCompanydtl.created_by;
                        lsinstitution_gid = getCompanydtl.institution_gid;

                        if (lsapplication_gid != "" && lsinstitution_gid != "")
                        { 
                            msGetGid = objcmnfunctions.GetMasterGID("I2BR");
                            msSQL = " insert into ocs_mst_tinstitution2bureau(" +
                                   " institution2bureau_gid ," +
                                   " institution_gid," +
                                   " bureauname_gid," +
                                   " bureauname_name," +
                                   " bureau_score," +
                                   " bureauscore_date," +
                                   " bureau_response," +
                                   " observations," +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + msGetGid + "'," +
                                   "'" + lsinstitution_gid + "'," +
                                   "'" + lsbureauname_gid + "'," +
                                   "'" + values.BureauName + "'," +
                                   "'" + values.BureauScore + "',";

                            if (values.ScoreAsOn == null || values.ScoreAsOn == "")
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(values.ScoreAsOn).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }


                            msSQL += "'" + values.BureauResponse.Replace("'", "") + "'," +
                                      "'" + values.Observations.Replace("'", "") + "'," +
                                      "'" + employee_gid + "'," +
                                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        else
                        {
                            LogForAudit(" Company Bureau Info Error - Not found '" + values.ApplicationNumber + "'");
                        }
                    }  
                    else
                    {
                        LogForAudit(" Company Bureau Info Error - Company Detail Not found '" + values.ApplicationNumber + "'");
                    }

                }
                catch (Exception ex)
                {
                    LogForAudit(" Company Bureau Info Error - '" + values.ApplicationNumber + "' - '" + ex.ToString() + "'");
                }
            }

            LogForAudit("---------Company Bureau Details - Completed Successfully !--------------");
        }

        // Individual  Bureau Details
        public void DaSubmitIndividualBureauDtl(List<BureauDetails> datalist, List<IndividualDetailsList> IndividualDtl)
        {
            LogForAudit("---------Individual Bureau Details - Started !--------------");
            msSQL = " select bureauname_gid, bureauname_name from ocs_mst_tbureauname where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<MstbureaunameList> MstbureaunameList = new List<MstbureaunameList>();
            MstbureaunameList = ConvertDataTable<MstbureaunameList>(dt_datatable);

            foreach (var values in datalist)
            {
                try
                {
                    string lstype = "", lsbureauname_gid = "";
                    var lsbureauname = MstbureaunameList.Where(x => x.bureauname_name.ToLower() == values.BureauName.ToLower()).FirstOrDefault();
                    if (lsbureauname != null)
                        lsbureauname_gid = lsbureauname.bureauname_gid;

                    string lsapplication_gid = "", employee_gid = "", lscontact_gid =""; 
                    var getIndividualDtl = IndividualDtl.Where(x => x.ApplicationNumber == values.ApplicationNumber && x.First_Name == values.NameofIndividualorcompany).FirstOrDefault();
                    if (getIndividualDtl != null)
                    {
                        lsapplication_gid = getIndividualDtl.application_gid;
                        employee_gid = getIndividualDtl.created_by;
                        lscontact_gid = getIndividualDtl.individual_gid;

                        if (lsapplication_gid != "" && lscontact_gid != "")
                        {
                            msGetGid = objcmnfunctions.GetMasterGID("C2BR");
                            msSQL = " insert into ocs_mst_tcontact2bureau(" +
                                   " contact2bureau_gid ," +
                                   " contact_gid," +
                                   " bureauname_gid," +
                                   " bureauname_name," +
                                   " bureau_score," +
                                   " bureauscore_date," +
                                   " bureau_response," +
                                   " observations," +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + msGetGid + "'," +
                                   "'" + lscontact_gid + "'," +
                                   "'" + lsbureauname_gid + "'," +
                                   "'" + values.BureauName + "'," +
                                   "'" + values.BureauScore + "',";

                            if (values.ScoreAsOn == null || values.ScoreAsOn == "")
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(values.ScoreAsOn).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }


                            msSQL += "'" + values.BureauResponse.Replace("'", "") + "'," +
                                      "'" + values.Observations.Replace("'", "") + "'," +
                                      "'" + employee_gid + "'," +
                                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL); 
                        }
                  
                    }
                    else
                    {
                        LogForAudit(" Individual Bureau Info Error - Individual dtl Not found '" + values.ApplicationNumber + "'");
                    }

                }
                catch (Exception ex)
                {
                    LogForAudit(" Individual Bureau Info Error - '" + values.ApplicationNumber + "' - '" + ex.ToString() + "'");
                }
            }

            LogForAudit("---------Individual Bureau Details - Completed Successfully !--------------");
        }
        
        public void DaSubmitDocumentMappingDtl(List<DocumentMappingDetails> datalist, List<ApplicationList> applicationdtl, List<CompanyDetailsList> companydtl, List<IndividualDetailsList> individualdtl)
        {
            LogForAudit("---------Document Mapping Details - Started !--------------");
            foreach (var values in datalist)
            {
                string lsdocumenttypes_gid = "";
                string lscompanydocument_gid = "", lsindividualdocument_gid="";
                // Document Type Master
                msSQL = " SELECT  documenttypes_gid ,documenttype_name FROM ocs_mst_tdocumenttypes a" +
                          " where status='Y' order by a.documenttypes_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                List<MstDocumentTypeList> MstDocumentTypeList = new List<MstDocumentTypeList>();
                MstDocumentTypeList = ConvertDataTable<MstDocumentTypeList>(dt_datatable);

                // Company Document
                msSQL = " SELECT companydocument_gid,companydocument_name from ocs_mst_tcompanydocument where status='Y'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                List<MstDocumentMapping_list> MstDocumentMapping_list = new List<MstDocumentMapping_list>();
                MstDocumentMapping_list = ConvertDataTable<MstDocumentMapping_list>(dt_datatable);

                // Individual Document
                msSQL = " SELECT individualdocument_gid,individualdocument_name from ocs_mst_tindividualdocument where status='Y'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                List<MstIndividualDocumentMapping_list> MstIndividualDocumentMapping_list = new List<MstIndividualDocumentMapping_list>();
                MstIndividualDocumentMapping_list = ConvertDataTable<MstIndividualDocumentMapping_list>(dt_datatable);

                var lscompanydocument = MstDocumentMapping_list.Where(x => x.companydocument_name.ToLower().Trim() == values.DocumentTitle.ToLower().Trim()).FirstOrDefault();
                if (lscompanydocument != null)
                    lscompanydocument_gid = lscompanydocument.companydocument_gid;
                var lsIndividualdocument = MstIndividualDocumentMapping_list.Where(x => x.individualdocument_name.ToLower().Trim() == values.DocumentTitle.ToLower().Trim()).FirstOrDefault();
                if (lsIndividualdocument != null)
                    lsindividualdocument_gid = lsIndividualdocument.individualdocument_gid;
                var lsdocumenttypes_group = MstDocumentTypeList.Where(x => x.documenttype_name.ToLower().Trim() == values.DocumentType.ToLower().Trim()).FirstOrDefault();
                if (lsdocumenttypes_group != null)
                    lsdocumenttypes_gid = lsdocumenttypes_group.documenttypes_gid;

                try
                {
                    string strPath = "'" + values.DocumentPath + "'";

                    // initialize the value of filename
                    string filename = null;

                    // using the method
                    filename = Path.GetFileName(strPath);
                    // Console.WriteLine("Filename = " + filename);

                    string employee_gid = "", lsapplication_gid = "";

                    var getapplicationgid = applicationdtl.Where(x => x.ApplicationNumber == values.ApplicationNumber).FirstOrDefault();
                    lsapplication_gid = getapplicationgid.application_gid;
                    if (values.ApplicantType == "Institution")
                    {
                        string lsinstitution_gid = "";
                        var getcompanydtl = companydtl.Where(x => x.ApplicationNumber == values.ApplicationNumber && x.LegalTradeName == values.NameofIndividualorcompany).FirstOrDefault();
                        lsinstitution_gid = getcompanydtl.institution_gid;
                        lsapplication_gid = getapplicationgid.application_gid;
                        employee_gid = getapplicationgid.RM_EmployeeGid;

                        msGetGid = objcmnfunctions.GetMasterGID("INDO");
                        msSQL = " insert into ocs_mst_tinstitution2documentupload( " +
                                    " institution2documentupload_gid," +
                                    " institution_gid," +
                                    " document_title ," +
                                    " document_name ," +
                                    " document_path," +
                                    " companydocument_gid, " +
                                    " documenttype_gid," +
                                    " documenttype_name," +
                                    " migration_flag, " +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + lsinstitution_gid + "'," +
                                    "'" + values.DocumentTitle.Replace("'", "") + "'," +
                                    "'" + filename.Replace("'", "") + "'," +
                                    "'" + values.DocumentPath.Replace("'", "") + "'," +
                                    "'" + lscompanydocument_gid + "'," +
                                    "'" + lsdocumenttypes_gid + "'," +
                                    "'" + values.DocumentType + "'," +
                                    "'Y'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else if (values.ApplicantType == "Individual")
                    {
                        string lscontact_gid = "";
                        var getdocindividualdtl = individualdtl.Where(x => x.ApplicationNumber == values.ApplicationNumber && x.First_Name == values.NameofIndividualorcompany).FirstOrDefault();
                        lscontact_gid = getdocindividualdtl.individual_gid;
                        lsapplication_gid = getapplicationgid.application_gid;
                        employee_gid = getapplicationgid.RM_EmployeeGid;

                        msGetGid = objcmnfunctions.GetMasterGID("C2DO");

                        msSQL = " insert into ocs_mst_tcontact2document( " +
                                    " contact2document_gid ," +
                                    " contact_gid ," +
                                    " document_title ," +
                                    " document_name ," +
                                    " document_path," +
                                    " individualdocument_gid, " +
                                    " documenttype_gid," +
                                    " documenttype_name," +
                                    " migration_flag, " +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + lscontact_gid + "'," +
                                    "'" + values.DocumentTitle.Replace("'", "") + "'," +
                                    "'" + filename.Replace("'", "") + "'," +
                                    "'" + values.DocumentPath.Replace("'", "") + "'," +
                                    "'" + lsindividualdocument_gid + "'," +
                                    "'" + lsdocumenttypes_gid + "'," +
                                    "'" + values.DocumentType + "'," +
                                    "'Y'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }   
                    else
                    {
                        LogForAudit("Document Mapping Error - Application Gid Not Found '" + values.ApplicationNumber + "'");
                    }

                }
                catch (Exception ex)
                {
                    LogForAudit("----Document Mapping Error - '" + values.ApplicationNumber + "' - '" + ex.ToString() + "'-----------");
                }
            }

            LogForAudit("---------Document Mapping - Completed successfully !--------------");
        }

        public void LogForAudit(string strVal)
        {
            try
            {
                string lspath = ConfigurationManager.AppSettings["file_path"].ToString() + "/erp_documents/MigrationLog/" + DateTime.Now.Year + @"\" + DateTime.Now.Month;
                if ((!System.IO.Directory.Exists(lspath)))
                    System.IO.Directory.CreateDirectory(lspath);

                lspath = lspath + @"\" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt";
                System.IO.StreamWriter sw = new System.IO.StreamWriter(lspath, true);
                sw.WriteLine(strVal);
                sw.Close();
            }
            catch (Exception ex)
            {
            }
        }

        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            dt.Dispose();
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    //in case you have a enum/GUID datatype in your model
                    //We will check field's dataType, and convert the value in it.
                    if (pro.Name == column.ColumnName.TrimEnd())
                    {
                        try
                        {
                            var convertedValue = GetValueByDataType(pro.PropertyType, dr[column.ColumnName.TrimEnd()]);
                            pro.SetValue(obj, convertedValue, null);
                        }
                        catch (Exception e)
                        {
                            //ex handle code                   
                            throw;
                        }
                        //pro.SetValue(obj, dr[column.ColumnName], null);
                    }
                    else
                        continue;
                }
            }
            return obj;
        }
        private static object GetValueByDataType(Type propertyType, object o)
        {
            if (o.ToString() == "null")
            {
                return null;
            }
            if (propertyType == (typeof(Guid)) || propertyType == typeof(Guid?))
            {
                return Guid.Parse(o.ToString());
            }
            else if (propertyType == typeof(int) || propertyType.IsEnum)
            {
                return Convert.ToInt32(o);
            }
            else if (propertyType == typeof(decimal))
            {
                return Convert.ToDecimal(o);
            }
            else if (propertyType == typeof(long))
            {
                return Convert.ToInt64(o);
            }
            else if (propertyType == typeof(bool) || propertyType == typeof(bool?))
            {
                return Convert.ToBoolean(o);
            }
            else if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
            {
                return Convert.ToDateTime(o);
            }
            return o.ToString();
        }
        //private static T GetItem<T>(DataRow dr)
        //{
        //    Type temp = typeof(T);
        //    T obj = Activator.CreateInstance<T>();

        //    foreach (DataColumn column in dr.Table.Columns)
        //    {
        //        foreach (PropertyInfo pro in temp.GetProperties())
        //        {
        //            if (pro.Name == column.ColumnName)
        //                pro.SetValue(obj, dr[column.ColumnName], null);
        //            else
        //                continue;
        //        }
        //    }
        //    return obj;
        //}

        //public DataTable ExcelToDataTable(string FileName, string range)
        //{
        //    DataTable datatable = new DataTable();
        //    int totalSheet = 1;
        //    using (OleDbConnection objConn = new OleDbConnection(lsConnectionString))
        //    {
        //        objConn.Open();
        //        OleDbCommand cmd = new OleDbCommand();
        //        OleDbDataAdapter oleda = new OleDbDataAdapter();
        //        DataSet ds = new DataSet();
        //        DataTable dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        //        string sheetName = string.Empty;
        //        if (dt != null)
        //        {
        //            var tempDataTable = (from dataRow in dt.AsEnumerable()
        //                                 where !dataRow["TABLE_NAME"].ToString().Contains("FilterDatabase")
        //                                 select dataRow).CopyToDataTable();
        //            dt = tempDataTable;
        //            totalSheet = dt.Rows.Count;
        //            sheetName = dt.Rows[0]["TABLE_NAME"].ToString();
        //        }
        //        sheetName = sheetName.Replace("'", "").Trim() + range;
        //        cmd.Connection = objConn;
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = "SELECT * FROM [" + sheetName + "]";
        //        oleda = new OleDbDataAdapter(cmd);
        //        oleda.Fill(ds, "excelData");

        //        datatable = ds.Tables["excelData"];
        //        objConn.Close();
        //    }
        //    return datatable;
        //}


        //public void DaApplicationInsert(values)
        //{
        //    try
        //    {


        //        string lsemployee_name = "", lsemployee_gid = "";
        //        string lsdrm_gid = "", lsdrm_name = "";
        //        //string lsapplication_gid = string.Empty;
        //        string lsvertical_gid = "", lsConstitution_gid = "", lsvernacularlan_gid = "", lsdesignation_gid = "";
        //        string lscredit_groupgid = "", lsprogram_gid = "", lsproduct_gid = "", lsvariety_gid = "";
        //        string lsclusterhead, lsregionalhead, lszonalhead, lsbusinesshead;
        //        string lsclusterheadgid, lsregionalheadgid, lszonalheadgid, lsbusinessheadgid;
        //        string lsbaselocationgid, lsclustergid, lsregiongid, lszonalgid;
        //        string lsbaselocationname, lsclustername, lsregionname, lszonalname;


        //        msSQL = " select a.employee_gid, concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as employee_name " +
        //                " from hrm_mst_temployee a" +
        //                " left join adm_mst_tuser b on a.user_gid=b.user_gid" +
        //                " where b.user_code='" + values.RM_Employee_Code + "'";
        //        objODBCDatareader = objdbconn.GetDataReader(msSQL);
        //        if (objODBCDatareader.HasRows == true)
        //        {
        //            lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
        //            lsemployee_name = objODBCDatareader["employee_name"].ToString();
        //        }
        //        objODBCDatareader.Close();


        //        msSQL = " select concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as level_one ,a.employeereporting_to from adm_mst_tmodule2employee a " +
        //               " left join hrm_mst_temployee f on f.employee_gid = a.employeereporting_to " +
        //               " left join adm_mst_tuser g on g.user_gid = f.user_gid " +
        //               " where  a.module_gid in  (select module_gid_parent from adm_mst_tmodule where module_gid in  " +
        //               " (select modulereportingto_gid from adm_mst_tcompany)) and g.user_status = 'Y' and a.employee_gid ='" + lsemployee_gid + "' " +
        //               "  group by a.employee_gid ";
        //        objODBCDatareader = objdbconn.GetDataReader(msSQL);
        //        if (objODBCDatareader.HasRows == true)
        //        {
        //            lsdrm_gid = objODBCDatareader["employeereporting_to"].ToString();
        //            lsdrm_name = objODBCDatareader["level_one"].ToString();
        //        }
        //        objODBCDatareader.Close();
        //        values.RM_EmployeeGid = lsemployee_gid;
        //        if (values.ApplicationNumber != "" || values.ApplicationNumber != null)
        //        {
        //            var lsvertical = MstverticalList.Where(x => x.vertical_name.ToLower().Trim() == values.Vertical.ToLower().Trim()).FirstOrDefault();
        //            if (lsvertical != null)
        //                lsvertical_gid = lsvertical.vertical_gid;
        //            var lsConstitution = MstconstitutionList.Where(x => x.constitution_name.ToLower().Trim() == values.Constitution.ToLower().Trim()).FirstOrDefault();
        //            if (lsConstitution != null)
        //                lsConstitution_gid = lsConstitution.constitution_gid;
        //            var lsvernacularlan = Mstvernacularlang_list.Where(x => x.vernacular_language.ToLower().Trim() == values.Vernacular_Language.ToLower().Trim()).FirstOrDefault();
        //            if (lsvernacularlan != null)
        //                lsvernacularlan_gid = lsvernacularlan.vernacularlanguage_gid;
        //            var lsdesignation = MstdesignationList.Where(x => x.designation_type.ToLower().Trim() == values.Designation.ToLower().Trim()).FirstOrDefault();
        //            if (lsdesignation != null)
        //                lsdesignation_gid = lsdesignation.designation_gid;
        //            var lscredit_group = MstCreditgroupList.Where(x => x.creditgroup_name.ToLower().Trim() == values.Credit_Group.ToLower().Trim()).FirstOrDefault();
        //            if (lscredit_group != null)
        //                lscredit_groupgid = lscredit_group.creditgroup_gid;
        //            var lsprogram = MstprogramList.Where(x => x.program.ToLower().Trim() == values.Program.ToLower().Trim()).FirstOrDefault();
        //            if (lsprogram != null)
        //                lsprogram_gid = lsprogram.program_gid;
        //            var lsproduct_dtl = MstproductnameList.Where(x => x.product_name.ToLower().Trim() == values.Product.ToLower().Trim()).FirstOrDefault();
        //            if (lsproduct_dtl != null)
        //            {
        //                lsproduct_gid = lsproduct_dtl.product_gid;
        //                values.Sector_Strategic_BusinessUnit = lsproduct_dtl.businessunit_name;
        //                values.Category = lsproduct_dtl.valuechain_name;

        //                try
        //                {
        //                    var lsvarietydtl = MstVarietynameList.Where(x => x.variety_name.ToLower() == values.Variety.ToLower() && x.product_gid == lsproduct_gid).FirstOrDefault();
        //                    if (lsvarietydtl != null)
        //                    {
        //                        lsvariety_gid = lsvarietydtl.variety_gid;
        //                        values.Botanical_Name = lsvarietydtl.botanical_name;
        //                        values.Alternative_Names = lsvarietydtl.alternative_name;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    lsvariety_gid = "";
        //                    values.Botanical_Name = "";
        //                    values.Alternative_Names = "";
        //                }
        //            }

        //            msSQL = " select distinct b.baselocation_gid,b.baselocation_name,c.cluster_gid,c.cluster_name,c.employee_name as clusterhead,c.vertical_name as clustervertical," +
        //                  " e.region_gid,e.region_name,e.employee_name as regionhead,g.zonal_gid,g.zonal_name,g.employee_name as zonalhead ," +
        //                  " c.employee_gid as clusterhead_gid,e.employee_gid as regionhead_gid, " +
        //                  " g.employee_gid as zonalhead_gid,h.employee_gid as businesshead_gid, " +
        //                  " h.employee_name as businesshead from hrm_mst_temployee a" +
        //                  " left join sys_mst_tcluster2baselocation b on b.baselocation_gid = a.baselocation_gid" +
        //                  " left  join sys_mst_tclusterhead c on b.cluster_gid = c.cluster_gid" +
        //                  " left join sys_mst_tregion2cluster d on c.cluster_gid = d.cluster_gid" +
        //                  " left join sys_mst_tregionhead e on d.region_gid = e.region_gid" +
        //                  " left join sys_mst_tzone2region f on f.region_gid = d.region_gid" +
        //                  " left join sys_mst_tzonalhead g on f.zone_gid = g.zonal_gid" +
        //                  " left join sys_mst_tbusinesshead h on h.zone_gid = g.zonal_gid where a.employee_gid = '" + lsemployee_gid + "' and" +
        //                  " c.vertical_gid = '" + lsvertical_gid + "'" +
        //                  " and e.vertical_gid = '" + lsvertical_gid + "' and " +
        //                  " g.vertical_gid = '" + lsvertical_gid + "' and h.vertical_gid = '" + lsvertical_gid + "'" +
        //                  " and c.program_gid = '" + lsprogram_gid + "' and e.program_gid = '" + lsprogram_gid + "' and " +
        //                  " g.program_gid = '" + lsprogram_gid + "' and h.program_gid = '" + lsprogram_gid + "' " +
        //                  " and e.status='Y' and g.status='Y' and h.status='Y' and c.status='Y'";
        //            objODBCDatareader = objdbconn.GetDataReader(msSQL);
        //            if (objODBCDatareader.HasRows == true)
        //            {
        //                lsclusterhead = objODBCDatareader["clusterhead"].ToString();
        //                lsregionalhead = objODBCDatareader["regionhead"].ToString();
        //                lszonalhead = objODBCDatareader["zonalhead"].ToString();
        //                lsbusinesshead = objODBCDatareader["businesshead"].ToString();
        //                lsclusterheadgid = objODBCDatareader["clusterhead_gid"].ToString();
        //                lsregionalheadgid = objODBCDatareader["regionhead_gid"].ToString();
        //                lszonalheadgid = objODBCDatareader["zonalhead_gid"].ToString();
        //                lsbusinessheadgid = objODBCDatareader["businesshead_gid"].ToString();
        //                lsbaselocationgid = objODBCDatareader["baselocation_gid"].ToString();
        //                lsbaselocationname = objODBCDatareader["baselocation_name"].ToString();
        //                lsclustergid = objODBCDatareader["cluster_gid"].ToString();
        //                lsclustername = objODBCDatareader["cluster_name"].ToString();
        //                lsregiongid = objODBCDatareader["region_gid"].ToString();
        //                lsregionname = objODBCDatareader["region_name"].ToString();
        //                lszonalgid = objODBCDatareader["zonal_gid"].ToString();
        //                lszonalname = objODBCDatareader["zonal_name"].ToString();
        //                objODBCDatareader.Close();

        //                values.application_gid = objcmnfunctions.GetMasterGID("APPC");
        //                msSQL = " insert into ocs_mst_tapplication(" +
        //                        " application_gid," +
        //                        " customerref_name, " +
        //                        " application_no," +
        //                        " migration_applicationno, " +
        //                        " vertical_gid," +
        //                        " vertical_name," +
        //                        " constitution_gid," +
        //                        " constitution_name," +
        //                        " sa_status," +
        //                        " saname_gid," +
        //                        " sa_name," +
        //                        " relationshipmanager_name," +
        //                        " relationshipmanager_gid," +
        //                        " drm_gid," +
        //                        " drm_name," +
        //                        " vernacular_language," +
        //                        " vernacularlanguage_gid," +
        //                        " contactpersonfirst_name," +
        //                        " contactpersonmiddle_name," +
        //                        " contactpersonlast_name," +
        //                        " designation_gid," +
        //                        " designation_type," +
        //                        " baselocation_gid," +
        //                        " baselocation_name," +
        //                        " cluster_gid," +
        //                        " cluster_name," +
        //                        " region_gid," +
        //                        " region_name," +
        //                        " zone_gid," +
        //                        " zone_name," +
        //                        " clustermanager_gid," +
        //                        " clustermanager_name," +
        //                        " zonalhead_name," +
        //                        " zonalhead_gid," +
        //                        " regionalhead_name," +
        //                        " regionalhead_gid," +
        //                        " businesshead_name," +
        //                        " businesshead_gid," +
        //                        " creditgroup_gid," +
        //                        " creditgroup_name," +
        //                        " program_gid," +
        //                        " program_name," +
        //                        " product_gid," +
        //                        " product_name," +
        //                        " variety_gid," +
        //                        " variety_name," +
        //                        " sector_name," +
        //                        " category_name," +
        //                        " botanical_name," +
        //                        " alternative_name," +
        //                        " status," +
        //                        " created_by," +
        //                        " created_date) values(" +
        //                        "'" + values.application_gid + "'," +
        //                        "'" + values.Applicant_Name + "'," +
        //                        "'" + values.ARN + "'," +
        //                        "'" + values.ApplicationNumber + "'," +
        //                        "'" + lsvertical_gid + "'," +
        //                        "'" + values.Vertical + "'," +
        //                        "'" + lsConstitution_gid + "'," +
        //                        "'" + values.Constitution + "'," +
        //                        "'" + values.Application_FromSA_Yes_No + "'," +
        //                        "'" + values.SAMAssociate_IDName + "'," +
        //                        "'" + values.SAMAssociate_IDName + "'," +
        //                        "'" + lsemployee_name + "'," +
        //                        "'" + lsemployee_gid + "'," +
        //                        "'" + lsdrm_gid + "'," +
        //                        "'" + lsdrm_name + "'," +
        //                        "'" + values.Vernacular_Language + "'," +
        //                        "'" + lsvernacularlan_gid + "'," +
        //                        "'" + values.First_Name + "'," +
        //                        "'" + values.Middle_Name + "'," +
        //                        "'" + values.Last_Name + "'," +
        //                        "'" + lsdesignation_gid + "'," +
        //                        "'" + values.Designation + "'," +
        //                        "'" + lsbaselocationgid + "'," +
        //                        "'" + lsbaselocationname + "'," +
        //                        "'" + lsclustergid + "'," +
        //                        "'" + lsclustername + "'," +
        //                        "'" + lsregiongid + "'," +
        //                        "'" + lsregionname + "'," +
        //                        "'" + lszonalgid + "'," +
        //                        "'" + lszonalname + "'," +
        //                        "'" + lsclusterheadgid + "'," +
        //                        "'" + lsclusterhead + "'," +
        //                        "'" + lszonalhead + "'," +
        //                        "'" + lszonalheadgid + "'," +
        //                        "'" + lsregionalhead + "'," +
        //                        "'" + lsregionalheadgid + "'," +
        //                        "'" + lsbusinesshead + "'," +
        //                        "'" + lsbusinessheadgid + "'," +
        //                        "'" + lscredit_groupgid + "'," +
        //                        "'" + values.Credit_Group + "'," +
        //                        "'" + lsprogram_gid + "'," +
        //                        "'" + values.Program + "'," +
        //                        "'" + lsproduct_gid + "'," +
        //                        "'" + values.Product + "'," +
        //                        "'" + lsvariety_gid + "'," +
        //                        "'" + values.Variety + "'," +
        //                        "'" + values.Sector_Strategic_BusinessUnit + "'," +
        //                        "'" + values.Category + "'," +
        //                        "'" + values.Botanical_Name + "'," +
        //                        "'" + values.Alternative_Names + "'," +
        //                        "'Completed'," +
        //                        "'" + lsemployee_gid + "'," +
        //                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
        //                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        //                // Genetic Code by Business Team 
        //                foreach (var i in MstGeneticList)
        //                {
        //                    msGetGid = objcmnfunctions.GetMasterGID("A2GC");
        //                    msSQL = " insert into ocs_mst_tapplication2geneticcode(" +
        //                           " application2geneticcode_gid," +
        //                           " application_gid," +
        //                           " geneticcode_gid," +
        //                           " geneticcode_name," +
        //                           " genetic_status," +
        //                           " genetic_remarks," +
        //                           " created_by," +
        //                           " created_date)" +
        //                           " values(" +
        //                           "'" + msGetGid + "'," +
        //                           "'" + values.application_gid + "'," +
        //                           "'" + i.geneticcode_gid + "'," +
        //                           "'" + i.geneticcode_name.Replace("'", " ") + "'," +
        //                           "'" + values.Genetic_Status + "'," +
        //                           "'" + values.Observations.Replace("'", " ") + "'," +
        //                           "'" + lsemployee_gid + "'," +
        //                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
        //                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        //                }

        //                //    Mobile Number(s)  
        //                msGetGid = objcmnfunctions.GetMasterGID("A2CN");
        //                msSQL = " insert into ocs_mst_tapplication2contactno(" +
        //                        " application2contact_gid," +
        //                        " application_gid," +
        //                        " mobile_no," +
        //                        " primary_mobileno," +
        //                        " whatsapp_mobileno," +
        //                        " created_by," +
        //                        " created_date)" +
        //                        " values(" +
        //                        "'" + msGetGid + "'," +
        //                        "'" + values.application_gid + "'," +
        //                        "'" + values.Mobile_Number + "'," +
        //                        "'" + values.Mobile_Primary_Status + "'," +
        //                        "'" + values.Whatapp_Number + "'," +
        //                        "'" + lsemployee_gid + "'," +
        //                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
        //                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        //                // Mail ID(s) 
        //                msGetGid = objcmnfunctions.GetMasterGID("A2EA");
        //                msSQL = " insert into ocs_mst_tapplication2email(" +
        //                        " application2email_gid," +
        //                        " application_gid," +
        //                        " email_address," +
        //                        " primary_emailaddress," +
        //                        " created_by," +
        //                        " created_date)" +
        //                        " values(" +
        //                        "'" + msGetGid + "'," +
        //                        "'" + values.application_gid + "'," +
        //                        "'" + values.Email_Address + "'," +
        //                        "'" + values.Email_Primary_Status + "'," +
        //                        "'" + lsemployee_gid + "'," +
        //                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
        //                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        //                msGetGid = objcmnfunctions.GetMasterGID("AP2P");
        //                msSQL = " insert into ocs_mst_tapplication2product (" +
        //                        " application2product_gid," +
        //                        " application2loan_gid," +
        //                        " application_gid," +
        //                        " product_gid," +
        //                        " product_name," +
        //                        " variety_gid," +
        //                        " variety_name," +
        //                        " sector_name," +
        //                        " category_name," +
        //                        " botanical_name," +
        //                        " alternative_name," +
        //                        " created_by," +
        //                        " created_date)" +
        //                        " values(" +
        //                        "'" + msGetGid + "'," +
        //                        "null," +
        //                        "'" + values.application_gid + "'," +
        //                        "'" + lsproduct_gid + "'," +
        //                        "'" + values.Product + "'," +
        //                        "'" + lsvariety_gid + "'," +
        //                        "'" + values.Variety + "'," +
        //                        "'" + values.Sector_Strategic_BusinessUnit + "'," +
        //                        "'" + values.Category + "'," +
        //                        "'" + values.Botanical_Name + "'," +
        //                        "'" + values.Alternative_Names + "'," +
        //                        "'" + lsemployee_gid + "'," +
        //                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
        //                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        //                DaApplicationList.Add(values);
        //                LogForAudit(" General - '" + values.ApplicationNumber + "' Completed");
        //            }
        //            else
        //            {
        //                LogForAudit(" Application No: '" + values.ApplicationNumber + "' - Location / Vertical not Assign for Business Approval'");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogForAudit(" General Info Error - '" + values.ApplicationNumber + "' - '" + ex.ToString() + "'");
        //    }
        //}

    }
}