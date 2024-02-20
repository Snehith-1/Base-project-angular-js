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
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System.Globalization;
using OfficeOpenXml;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Web.Script.Serialization;
using System.Net;
using System.Reflection;
using ems.mastersamagro.Models;
using RestSharp;
using Newtonsoft.Json;
using ems.storage.Functions;

namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess provide access for various events (Onboard Creation - Bulk Migration) in Excel Data Migration
    /// </summary>
    /// <remarks>Written by Premchander.K , Sherin.A</remarks>
    public class DaBuyerOnboardExcelDataMigration
    {

        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid, lscompany_code;
        int mnResult;
        HttpPostedFile httpPostedFile;

        public void DaOnboardDataImport(HttpRequest httpRequest, string employee_gid, result objResult)
        {

            DataTable dt = null;
            DataTable Generaltable = new DataTable();
            DataTable CompanyDetailtable = new DataTable();
            DataTable IndividualDetailtable = new DataTable();
            //List<GeneralApplication_List> ApplicationInfoList = new List<GeneralApplication_List>();
            List<ApplicationList> ApplicationInfoList = new List<ApplicationList>();
            List<CompanyDetailsList> CompanyInfoList = new List<CompanyDetailsList>();
            List<IndividualDetailsList> IndividualInfoList = new List<IndividualDetailsList>();

            try
            {
                //int insertCount = 0;
                HttpFileCollection httpFileCollection;

                string lspath, lsfilePath;
                string project_flag = httpRequest.Form["project_flag"].ToString();

                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                // Create Directory
                lsfilePath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/Samagro/ExcelDataMigration/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

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

                // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objResult.message = "File format is not supported";
                            objResult.status = false;
                            return;
                        }

                //path creation        
                lspath = lsfilePath + "/";
                FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);
                var obj = new List<MdlExcelData_list>();
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
                            obj.Add(new MdlExcelData_list
                            {
                                sheetName = "" + worksheet.Name + "$",
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


                            Generaltable = ds.Tables["GeneralDetails"];
                            CompanyDetailtable = ds.Tables["CompanyDetails"];
                            IndividualDetailtable = ds.Tables["IndividualDetails"];
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

                            List<ApplicationList> applicationdtl = DaSubmitGeneralDtl(ApplicationInfoList);

                            List<CompanyDetailsList> companydtl = DaSubmitCompanyInfo(CompanyInfoList, applicationdtl);
                            // Individual Info  
                            //List<IndividualDetailsList> individualdtl = DaSubmitIndividualInfo(IndividualInfoList, applicationdtl);
                        }

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

        }

        // General Info 
        public List<ApplicationList> DaSubmitGeneralDtl(List<ApplicationList> datalist)
        {
            LogForAudit("----General Info - Started -----------");
            // From Vertical Master
            msSQL = " SELECT a.vertical_gid,a.vertical_name,vertical_code " +
                    " FROM ocs_mst_tvertical a  where status_log='Y' order by a.vertical_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<verticalList> verticalList = new List<verticalList>();
            verticalList = ConvertDataTable<verticalList>(dt_datatable);
            //dt_datatable.Dispose();

            // Credit Group Master
            msSQL = " SELECT creditmapping_gid as creditgroup_gid ,creditgroup_name FROM ocs_mst_tcreditmapping a" +
                      " where status='Y' order by a.creditmapping_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<CreditgroupList> CreditgroupList = new List<CreditgroupList>();
            CreditgroupList = ConvertDataTable<CreditgroupList>(dt_datatable);

            // Constitution 
            msSQL = " SELECT constitution_gid,constitution_name FROM ocs_mst_tconstitution a" +
                   " where status_log='Y' order by constitution_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<constitutionList> constitutionList = new List<constitutionList>();
            constitutionList = ConvertDataTable<constitutionList>(dt_datatable);

            // Product Master
            msSQL = " SELECT product_gid,product_name,businessunit_gid,businessunit_name,valuechain_gid,valuechain_name FROM ocs_mst_tproducts a" +
                     " where status='Y' order by a.product_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<productnameList> productnameList = new List<productnameList>();
            productnameList = ConvertDataTable<productnameList>(dt_datatable);

            // Variety Master

            msSQL = " SELECT product_gid,variety_gid,variety_name,botanical_name,alternative_name,hsn_code  FROM ocs_mst_tvariety a " +
                    " order by a.product_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<VarietynameList> VarietynameList = new List<VarietynameList>();
            VarietynameList = ConvertDataTable<VarietynameList>(dt_datatable);

            // Program Master
            msSQL = " SELECT program_gid,program FROM ocs_mst_tprogram a" +
                       " where status='Y' order by a.program_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<programList> programList = new List<programList>();
            programList = ConvertDataTable<programList>(dt_datatable);

            // Vernacular Master
            msSQL = " SELECT vernacularlanguage_gid,vernacular_language FROM ocs_mst_tvernacularlanguage a" +
                     " where status='Y' order by a.vernacularlanguage_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<vernacularlang_list> vernacularlang_list = new List<vernacularlang_list>();
            vernacularlang_list = ConvertDataTable<vernacularlang_list>(dt_datatable);

            // Designation Master
            msSQL = " SELECT a.designation_gid,a.designation_type from ocs_mst_tdesignation a" +
                 "  where status_log='Y' order by a.designation_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<designationList> designationList = new List<designationList>();
            designationList = ConvertDataTable<designationList>(dt_datatable);

            msSQL = " SELECT geneticcode_gid,geneticcode_name FROM ocs_mst_tgeneticcode a" +
                  " where status='Y' order by a.geneticcode_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<GeneticList> GeneticList = new List<GeneticList>();
            GeneticList = ConvertDataTable<GeneticList>(dt_datatable);
            List<ApplicationList> DaApplicationList = new List<ApplicationList>();

            foreach (var values in datalist)
            {
                try
                {
                    string lsemployee_name = "", lsemployee_gid = "";
                    string lsdrm_gid = "", lsdrm_name = "";
                    //string lsapplication_gid = string.Empty;
                    string lsvertical_gid = "", lsConstitution_gid = "", lsvernacularlan_gid = "", lsdesignation_gid = "";
                    string lscredit_groupgid = "", lsprogram_gid = "", lsproduct_gid = "", lsvariety_gid = "";



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
                    values.RM_EmployeeGid = lsemployee_gid;
                    if (values.ApplicationNumber != "" || values.ApplicationNumber != null)
                    {
                        var lsvertical = verticalList.Where(x => x.vertical_name.ToLower().Trim() == values.Vertical.ToLower().Trim()).FirstOrDefault();
                        if (lsvertical != null)
                            lsvertical_gid = lsvertical.vertical_gid;
                        var lsConstitution = constitutionList.Where(x => x.constitution_name.ToLower().Trim() == values.Constitution.ToLower().Trim()).FirstOrDefault();
                        if (lsConstitution != null)
                            lsConstitution_gid = lsConstitution.constitution_gid;
                        var lsvernacularlan = vernacularlang_list.Where(x => x.vernacular_language.ToLower().Trim() == values.Vernacular_Language.ToLower().Trim()).FirstOrDefault();
                        if (lsvernacularlan != null)
                            lsvernacularlan_gid = lsvernacularlan.vernacularlanguage_gid;
                        var lsdesignation = designationList.Where(x => x.designation_type.ToLower().Trim() == values.Designation.ToLower().Trim()).FirstOrDefault();
                        if (lsdesignation != null)
                            lsdesignation_gid = lsdesignation.designation_gid;
                        //var lscredit_group = CreditgroupList.Where(x => x.creditgroup_name.ToLower().Trim() == values.Credit_Group.ToLower().Trim()).FirstOrDefault();
                        //if (lscredit_group != null)
                        //    lscredit_groupgid = lscredit_group.creditgroup_gid;
                        //var lsprogram = programList.Where(x => x.program.ToLower().Trim() == values.Program.ToLower().Trim()).FirstOrDefault();
                        //if (lsprogram != null)
                        //    lsprogram_gid = lsprogram.program_gid;
                        var lsproduct_dtl = productnameList.Where(x => x.product_name.ToLower().Trim() == values.Product.ToLower().Trim()).FirstOrDefault();
                        if (lsproduct_dtl != null)
                        {
                            lsproduct_gid = lsproduct_dtl.product_gid;
                            values.Sector_Strategic_BusinessUnit = lsproduct_dtl.businessunit_name;
                            values.Category = lsproduct_dtl.valuechain_name;

                            try
                            {
                                var lsvarietydtl = VarietynameList.Where(x => x.variety_name.ToLower() == values.Variety.ToLower() && x.product_gid == lsproduct_gid).FirstOrDefault();
                                if (lsvarietydtl != null)
                                {
                                    lsvariety_gid = lsvarietydtl.variety_gid;
                                    values.Botanical_Name = lsvarietydtl.botanical_name;
                                    values.Alternative_Names = lsvarietydtl.alternative_name;
                                    values.hsn_code = lsvarietydtl.hsn_code;
                                }
                            }
                            catch (Exception ex)
                            {
                                lsvariety_gid = "";
                                values.Botanical_Name = "";
                                values.Alternative_Names = "";
                                values.hsn_code = "";
                            }
                        }


                        values.application_gid = objcmnfunctions.GetMasterGID("BYOG");
                        msSQL = " insert into agr_mst_tbyronboard(" +
                         " application_gid," +
                         " application_no, " +
                         " virtualaccount_number," +
                         " migration_applicationno, " +
                         " customerref_name," +
                         " vertical_gid," +
                         " vertical_name," +
                         " constitution_gid," +
                         " constitution_name," +
                         " sa_status," +
                         " saname_gid," +
                         " sa_name," +
                         " vernacular_language," +
                         " vernacularlanguage_gid," +
                         " contactpersonfirst_name," +
                         " contactpersonmiddle_name," +
                         " contactpersonlast_name," +
                         " designation_gid," +
                         " designation_type," +
                         " landline_no," +
                         " program_gid," +
                         " program_name," +
                         "createdby_name," +
                         " status," +
                         " created_by," +
                         " created_date) values(" +
                           "'" + values.application_gid + "'," +
                           "'" + values.ApplicationNumber + "'," +
                           "'" + values.VirtualAccountNumber + "'," +
                           "'" + values.ApplicationNumber + "'," +
                           "'" + values.customer_name + "'," +
                           "'" + lsvertical_gid + "'," +
                           "'" + values.Vertical + "'," +
                           "'" + lsConstitution_gid + "'," +
                           "'" + values.Constitution + "'," +
                           "'" + values.Application_FromSA_Yes_No + "'," +
                           "'" + values.SAMAssociate_IDName + "'," +
                           "'" + values.SAMAssociate_IDName + "'," +
                           "'" + values.Vernacular_Language + "'," +
                           "'" + lsvernacularlan_gid + "'," +
                           "'" + values.First_Name + "'," +
                           "'" + values.Middle_Name + "'," +
                           "'" + values.Last_Name + "'," +
                           "'" + lsdesignation_gid + "'," +
                           "'" + values.Designation + "'," +
                           "'" + values.Landline_Number + "'," +
                           "'" + lsprogram_gid + "'," +
                           "'" + values.Program + "'," +
                             "'" + lsemployee_name + "'," +
                             "'Completed'," +
                             "'" + lsemployee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        // Genetic Code by Business Team 
                        foreach (var i in GeneticList)
                        {
                            msGetGid = objcmnfunctions.GetMasterGID("A2GC");
                            msSQL = " insert into agr_mst_tbyronboard2geneticcode(" +
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
                        msSQL = " insert into agr_mst_tbyronboard2contactno(" +
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
                        msSQL = " insert into agr_mst_tbyronboard2email(" +
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
                        msSQL = " insert into agr_mst_tbyronboard2product (" +
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
                                " hsn_code, " +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid + "'," +
                                "null," +
                                "'" + values.application_gid + "'," +
                                "'" + lsproduct_gid + "'," +
                                "'" + values.product_name + "'," +
                                "'" + lsvariety_gid + "'," +
                                "'" + values.variety_name + "'," +
                                "'" + values.Sector_Strategic_BusinessUnit + "'," +
                                "'" + values.Category + "'," +
                                "'" + values.Botanical_Name + "'," +
                                "'" + values.Alternative_Names + "'," +
                                "'" + values.hsn_code + "'," +
                                "'" + lsemployee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL); 

                        DaApplicationList.Add(values);
                        LogForAudit(" General - '" + values.ApplicationNumber + "' Completed");
                    }
                    else
                    {
                        LogForAudit(" Application No: '" + values.customer_name + "' - Empty'");
                    }
                }
                catch (Exception ex)
                {
                    LogForAudit(" General Info Error - '" + values.ApplicationNumber + "' - '" + ex.ToString() + "'");
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
            List<companytype_list> companytype_list = new List<companytype_list>();
            companytype_list = ConvertDataTable<companytype_list>(dt_datatable);

            // Stakeholder Type

            msSQL = " SELECT usertype_gid,user_type FROM ocs_mst_tusertype where status_log='Y' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<usertypelist> usertypelist = new List<usertypelist>();
            usertypelist = ConvertDataTable<usertypelist>(dt_datatable);

            // Assessment Agency  
            msSQL = " select assessmentagency_gid, assessmentagency_name from ocs_mst_tassessmentagency where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<assessmentagencylist> assessmentagencylist = new List<assessmentagencylist>();
            assessmentagencylist = ConvertDataTable<assessmentagencylist>(dt_datatable);

            // Agency rating  
            msSQL = " select assessmentagencyrating_gid, assessmentagencyrating_name from ocs_mst_tassessmentagencyrating where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<assessmentagencyratingList> assessmentagencyratingList = new List<assessmentagencyratingList>();
            assessmentagencyratingList = ConvertDataTable<assessmentagencyratingList>(dt_datatable);

            // AML Category list  
            msSQL = " SELECT amlcategory_gid,amlcategory_name FROM ocs_mst_tamlcategory where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<amlcategoryList> amlcategoryList = new List<amlcategoryList>();
            amlcategoryList = ConvertDataTable<amlcategoryList>(dt_datatable);

            // Business Category 
            msSQL = " SELECT businesscategory_gid,businesscategory_name FROM ocs_mst_tbusinesscategory where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<businesscategoryList> businesscategoryList = new List<businesscategoryList>();
            businesscategoryList = ConvertDataTable<businesscategoryList>(dt_datatable);

            // Designation List 
            msSQL = " SELECT designation_gid,designation_type FROM ocs_mst_tdesignation where status_log='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<designationlist> designationlist = new List<designationlist>();
            designationlist = ConvertDataTable<designationlist>(dt_datatable);

            // Address Type 
            msSQL = " SELECT address_gid,address_type FROM ocs_mst_taddresstype order by address_gid asc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<addresstypelist> addresstypelist = new List<addresstypelist>();
            addresstypelist = ConvertDataTable<addresstypelist>(dt_datatable);

            // Postal Code
            msSQL = " select postalcode_value,city,taluka,district,state from ocs_mst_tpostalcode";
            //" postalcode_value='" + postal_code + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<ContactAddresslist> ContactAddresslist = new List<ContactAddresslist>();
            ContactAddresslist = ConvertDataTable<ContactAddresslist>(dt_datatable);

            // license type
            msSQL = " SELECT licensetype_gid,licensetype_name FROM ocs_mst_tlicensetype where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<licensetypelist> Mstlicensetypelist = new List<licensetypelist>();
            Mstlicensetypelist = ConvertDataTable<licensetypelist>(dt_datatable);

            List<CompanyDetailsList> companydtl = new List<CompanyDetailsList>();
            foreach (var values in datalist)
            {
                try
                {
                    string lsapplication_gid = "", employee_gid = "", lscompanytype_gid = "", lsstackholder_gid = "", lsassessmentagency_gid = "";
                    string lsassessmentagencyrating_gid = "", lsamlcategory_gid = "", lsbusinesscategory_gid = "", lsdesignation_gid = "", lsmobileno = "";
                    string lsemail_address = "", lsaddress_gid = "", lslicense_gid = "";

                    var getapplicationgid = applicationdtl.Where(x => x.ApplicationNumber == values.ApplicationNumber).FirstOrDefault();
                    if (getapplicationgid != null)
                    {
                        values.application_gid = getapplicationgid.application_gid;
                        employee_gid = getapplicationgid.RM_EmployeeGid;
                        values.created_by = getapplicationgid.RM_EmployeeGid;
                    }
                    if (values.application_gid != "")
                    {
                        var lscompanytype = companytype_list.Where(x => x.companytype_name.ToLower().Trim() == values.CompanyType.ToLower().Trim()).FirstOrDefault();
                        if (lscompanytype != null)
                            lscompanytype_gid = lscompanytype.companytype_gid;
                        var lsstackholder = usertypelist.Where(x => x.user_type.ToLower().Trim() == values.StakeholderType.ToLower().Trim()).FirstOrDefault();
                        if (lsstackholder != null)
                            lsstackholder_gid = lsstackholder.usertype_gid;
                        var lsassessmentagency = assessmentagencylist.Where(x => x.assessmentagency_name.ToLower().Trim() == values.CreditRatingAgency.ToLower().Trim()).FirstOrDefault();
                        if (lsassessmentagency != null)
                            lsassessmentagency_gid = lsassessmentagency.assessmentagency_gid;
                        var lsassessmentagencyrating = assessmentagencyratingList.Where(x => x.assessmentagencyrating_name.ToLower().Trim() == values.CreditRating.ToLower().Trim()).FirstOrDefault();
                        if (lsassessmentagencyrating != null)
                            lsassessmentagencyrating_gid = lsassessmentagencyrating.assessmentagencyrating_gid;
                        var lsamlcategory = amlcategoryList.Where(x => x.amlcategory_name.ToLower().Trim() == values.CategoryAML.ToLower().Trim()).FirstOrDefault();
                        if (lsamlcategory != null)
                            lsamlcategory_gid = lsamlcategory.amlcategory_gid;
                        var lsbusinesscategory = businesscategoryList.Where(x => x.businesscategory_name.ToLower().Trim() == values.CategoryBusiness.ToLower().Trim()).FirstOrDefault();
                        if (lsbusinesscategory != null)
                            lsbusinesscategory_gid = lsbusinesscategory.businesscategory_gid;
                        var lsdesignation = designationlist.Where(x => x.designation_type.ToLower().Trim() == values.Designation.ToLower().Trim()).FirstOrDefault();
                        if (lsdesignation != null)
                            lsdesignation_gid = lsdesignation.designation_gid;
                        var lsaddress = addresstypelist.Where(x => x.address_type.ToLower().Trim() == values.AddressType.ToLower().Trim()).FirstOrDefault();
                        if (lsaddress != null)
                            lsaddress_gid = lsaddress.address_gid;
                        var lslicense = Mstlicensetypelist.Where(x => x.licensetype_name.ToLower().Trim() == values.LicenseType.ToLower().Trim()).FirstOrDefault();
                        if (lslicense != null)
                            lslicense_gid = lslicense.licensetype_gid;
                        var contact_dtl = ContactAddresslist.Where(x => x.postalcode_value.ToLower().Trim() == values.PostalCode.ToLower().Trim()).FirstOrDefault();
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
                        msSQL = " insert into agr_mst_tbyronboard2institution(" +
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
                             " tan_number," +
                             " incometax_returnsstatus," +
                             " revenue," +
                             " profit," +
                             " fixed_assets," +
                             " sundrydebt_adv," +
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
                                "'" + values.Tan_number + "'," +
                                "'" + values.IncomeTaxreturnsstatus + "'," +
                                "'" + values.Revenue + "'," +
                                "'" + values.Profit + "'," +
                                "'" + values.FixedAssets + "'," +
                                "'" + values.SundryDebtorsandAdvances + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            // Mobile Number 
                            msGetGid = objcmnfunctions.GetMasterGID("IT2M");
                            msSQL = " insert into agr_mst_tbyronboardinstitution2mobileno(" +
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
                            msSQL = " insert into agr_mst_tbyronboardinstitution2email(" +
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
                            msSQL = " insert into agr_mst_tbyronboardinstitution2address(" +
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
                            msSQL = " insert into agr_mst_tbyronboardinstitution2licensedtl(" +
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

                            if(values.ifsc_code != "No")
                            {
                                msGetGid = objcmnfunctions.GetMasterGID("I2BD");
                                msSQL = " insert into agr_mst_tbyronboardinstitution2bankdtl(" +
                                        " institution2bankdtl_gid," +
                                        " institution_gid," +
                                        //" application_gid," +
                                        " bank_name," +
                                        " branch_name," +
                                        " bank_address," +
                                        " micr_code," +
                                        " ifsc_code," +
                                        " bankaccount_name," +
                                        " bankaccounttype_gid," +
                                        " bankaccounttype_name," +
                                        " bankaccount_number," +
                                        " confirmbankaccountnumber," +
                                        " joinaccount_status," +
                                        " joinaccount_name," +
                                        " primary_status," +
                                        " chequebook_status," +
                                        " accountopen_date," +
                                        " created_by," +
                                        " created_date)" +
                                        " values(" +
                                        "'" + msGetGid + "'," +
                                        "'" + institution_gid + "'," +
                                        //"'" + values.application_gid + "'," +
                                        "'" + values.bank_name + "'," +
                                        "'" + values.branch_name + "'," +
                                        "'" + values.bank_address + "'," +
                                        "'" + values.micr_code + "'," +
                                        "'" + values.ifsc_code + "'," +
                                        "'" + values.bankaccount_name.Replace("'", "") + "'," +
                                        "'" + values.bankaccounttype_gid + "'," +
                                        "'" + values.bankaccounttype_name + "'," +
                                        "'" + values.bankaccount_number + "'," +
                                        "'" + values.confirmbankaccountnumber + "'," +
                                        "'" + values.joint_account + "'," +
                                        "'" + values.jointaccountholder_name + "'," +
                                        "'" + values.primary_status + "'," +
                                        "'" + values.chequebook_status + "',";
                                if (values.accountopen_date == null || values.accountopen_date == "")
                                {
                                    msSQL += "null,";
                                }
                                else
                                {
                                    msSQL += "'" + Convert.ToDateTime(values.accountopen_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                                }
                                msSQL += "'" + employee_gid + "'," +
                                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            }

                            msGetGid = objcmnfunctions.GetMasterGID("ITGS");
                            msSQL = " insert into agr_mst_tbyronboardinstitution2branch(" +
                                    " institution2branch_gid," +
                                    " institution_gid," +
                                    " gst_state," +
                                    " gst_no," +
                                    " gst_registered," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + institution_gid + "'," +
                                    "'" + values.GstState + "'," +
                                    "'" + values.GstNo + "'," +
                                    "'" + values.GstRegistered + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msGetGid = objcmnfunctions.GetMasterGID("INRD");

                            msSQL = " insert into agr_mst_tbyronboardinstitution2ratingdetail(" +
                                         " institution2ratingdetail_gid," +
                                         " institution_gid," +
                                         " application_gid," +
                                         " creditrating_agencygid," +
                                         " creditrating_agencyname," +
                                         " creditrating_gid," +
                                         " creditrating_name," +
                                         " assessed_on," +
                                         " creditrating_link," +
                                         " created_by," +
                                         " created_date) values(" +
                                         "'" + msGetGid + "'," +
                                         "'" + institution_gid + "'," +
                                         "'" + values.application_gid + "'," +
                                         "'" + lsassessmentagency_gid + "'," +
                                         "'" + values.CreditRatingAgency + "'," +
                                         "'" + lsassessmentagencyrating_gid + "'," +
                                         "'" + values.CreditRating + "'," +
                                         "'" + Convert.ToDateTime(values.Assessed_on).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                         "'" + values.Creditrating_Link.Replace("'", "") + "'," +
                                         "'" + employee_gid + "'," +
                                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (values.StakeholderType == "Borrower" || values.StakeholderType == "Applicant")
                            {
                                msSQL = "select mobile_no from agr_mst_tbyronboardinstitution2mobileno where institution_gid='" + institution_gid + "' and primary_status='yes'";
                                lsmobileno = objdbconn.GetExecuteScalar(msSQL);

                                msSQL = "select email_address from agr_mst_tbyronboardinstitution2email where institution_gid='" + institution_gid + "' and primary_status='yes'";
                                lsemail_address = objdbconn.GetExecuteScalar(msSQL);

                                msSQL = "update agr_mst_tbyronboard set applicant_type ='Institution' where application_gid='" + values.application_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                msSQL = "update agr_mst_tbyronboard2institution set mobile_no='" + lsmobileno + "'," +
                                 " email_address='" + lsemail_address + "' where institution_gid='" + institution_gid + "' ";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }

                            companydtl.Add(values); 
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
            return companydtl;
        }

        // Individual Info 

        public List<IndividualDetailsList> DaSubmitIndividualInfo(List<IndividualDetailsList> datalist, List<ApplicationList> applicationdtl)
        {
            LogForAudit("----Individual Info - Started ------");
            // gender
            msSQL = " SELECT gender_gid,gender_name FROM ocs_mst_tgender where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<Gender_list> Gender_list = new List<Gender_list>();
            Gender_list = ConvertDataTable<Gender_list>(dt_datatable);

            // Designation Master
            msSQL = " SELECT a.designation_gid,a.designation_type from ocs_mst_tdesignation a" +
                 "  where status_log='Y' order by a.designation_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<designationList> designationList = new List<designationList>();
            designationList = ConvertDataTable<designationList>(dt_datatable);

            // Educational Qualification
            msSQL = " SELECT educationalqualification_gid,educationalqualification_name FROM ocs_mst_teducationalqualification where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<educationalqualificationList> educationalqualificationList = new List<educationalqualificationList>();
            educationalqualificationList = ConvertDataTable<educationalqualificationList>(dt_datatable);

            // Stakeholder Type

            msSQL = " SELECT usertype_gid,user_type FROM ocs_mst_tusertype where status_log='Y' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<usertypelist> usertypelist = new List<usertypelist>();
            usertypelist = ConvertDataTable<usertypelist>(dt_datatable);

            // Marital Status 
            msSQL = " SELECT maritalstatus_gid,maritalstatus_name FROM ocs_mst_tmaritalstatus where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<MaritalStatusList> MaritalStatusList = new List<MaritalStatusList>();
            MaritalStatusList = ConvertDataTable<MaritalStatusList>(dt_datatable);

            // Ownership type 
            msSQL = " SELECT ownershiptype_gid,ownershiptype_name FROM ocs_mst_townershiptype where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<ownershiptypeList> ownershiptypeList = new List<ownershiptypeList>();
            ownershiptypeList = ConvertDataTable<ownershiptypeList>(dt_datatable);

            // Income Type 

            msSQL = " SELECT incometype_gid,incometype_name FROM ocs_mst_tincometype where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<IncometypeList> IncometypeList = new List<IncometypeList>();
            IncometypeList = ConvertDataTable<IncometypeList>(dt_datatable);

            // Property Holder
            msSQL = " SELECT propertyin_gid,propertyin_name FROM ocs_mst_tproperty where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<PropertyList> PropertyList = new List<PropertyList>();
            PropertyList = ConvertDataTable<PropertyList>(dt_datatable);

            // Resistance Type
            msSQL = " SELECT residencetype_gid,residencetype_name FROM ocs_mst_tresidencetype where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<ResistanceList> ResistanceList = new List<ResistanceList>();
            ResistanceList = ConvertDataTable<ResistanceList>(dt_datatable);

            // Postal Code
            msSQL = " select postalcode_value,city,taluka,district,state from ocs_mst_tpostalcode";
            //" postalcode_value='" + postal_code + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<ContactAddresslist> ContactAddresslist = new List<ContactAddresslist>();
            ContactAddresslist = ConvertDataTable<ContactAddresslist>(dt_datatable);

            // Address Type 
            msSQL = " SELECT address_gid,address_type FROM ocs_mst_taddresstype order by address_gid asc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<addresstypelist> addresstypelist = new List<addresstypelist>();
            addresstypelist = ConvertDataTable<addresstypelist>(dt_datatable);

            List<IndividualDetailsList> DaIndividualList = new List<IndividualDetailsList>();
            foreach (var values in datalist)
            {
                try
                {
                    string lsapplication_gid = "", employee_gid = "", lsgender_gid = "", lsemail_address = "", lsdesignation_gid = "", lsqualification_gid = "";
                    string lsstackholder_gid = "", lsMaritalstatus_gid = "", lsownershiptype_gid = "", lsincometype_gid = "", lsgroup_gid = "", lspropertyholder_gid = "";
                    string lsresidencetype_gid = "", lsaddress_gid = "";

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
                        var lsdesignation = designationList.Where(x => x.designation_type.ToLower().Trim() == values.Designation.ToLower().Trim()).FirstOrDefault();
                        if (lsdesignation != null)
                            lsdesignation_gid = lsdesignation.designation_gid;
                        var lsstackholder = usertypelist.Where(x => x.user_type.ToLower().Trim() == values.Stakeholder_Type.ToLower().Trim()).FirstOrDefault();
                        if (lsstackholder != null)
                            lsstackholder_gid = lsstackholder.usertype_gid;
                        var lsgender = Gender_list.Where(x => x.gender_name.ToLower().Trim() == values.Gender.ToLower().Trim()).FirstOrDefault();
                        if (lsgender != null)
                            lsgender_gid = lsgender.gender_gid;
                        var lsMaritalstatus = MaritalStatusList.Where(x => x.maritalstatus_name.ToLower().Trim() == values.Marital_Status.ToLower().Trim()).FirstOrDefault();
                        if (lsMaritalstatus != null)
                            lsMaritalstatus_gid = lsMaritalstatus.maritalstatus_gid;
                        var lsownershiptype = ownershiptypeList.Where(x => x.ownershiptype_name.ToLower().Trim() == values.Ownership_Type.ToLower().Trim()).FirstOrDefault();
                        if (lsownershiptype != null)
                            lsownershiptype_gid = lsownershiptype.ownershiptype_gid;
                        var lsqualification = educationalqualificationList.Where(x => x.educationalqualification_name.ToLower().Trim() == values.Educational_Qualification.ToLower().Trim()).FirstOrDefault();
                        if (lsqualification != null)
                            lsqualification_gid = lsqualification.educationalqualification_gid;
                        var lsincometype = IncometypeList.Where(x => x.incometype_name.ToLower().Trim() == values.Income_Type.ToLower().Trim()).FirstOrDefault();
                        if (lsincometype != null)
                            lsincometype_gid = lsincometype.incometype_gid;
                        var lspropertyholder = PropertyList.Where(x => x.propertyin_name.ToLower().Trim() == values.Property_intheNameof.ToLower().Trim()).FirstOrDefault();
                        if (lsincometype != null)
                            lspropertyholder_gid = lspropertyholder.propertyin_gid;
                        var lsresidencetype = ResistanceList.Where(x => x.residencetype_name.ToLower().Trim() == values.Residence_Type.ToLower().Trim()).FirstOrDefault();
                        if (lsresidencetype != null)
                            lsresidencetype_gid = lsresidencetype.residencetype_gid;
                        var lsaddress = addresstypelist.Where(x => x.address_type.ToLower().Trim() == values.Address_Type.ToLower().Trim()).FirstOrDefault();
                        if (lsaddress != null)
                            lsaddress_gid = lsaddress.address_gid;
                        var contact_dtl = ContactAddresslist.Where(x => x.postalcode_value.Trim() == values.Postal_Code.Trim()).FirstOrDefault();
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
                        msSQL = " SELECT company_name, institution_gid from agr_mst_tbyronboard2institution where application_gid='" + lsapplication_gid + "' " +
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
                        msSQL = " insert into agr_mst_tbyronboardcontact(" +
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
                        msSQL += "'" + Convert.ToDateTime(values.Date_of_Birth).ToString("dd-MM-yyyy") + "'," +
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
                                 "'" + employee_gid + "'," +
                                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult != 0)
                        {
                            msGetGid = objcmnfunctions.GetMasterGID("C2MN");
                            msSQL = " insert into agr_mst_tbyronboardcontact2mobileno(" +
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
                            msSQL = " insert into agr_mst_tbyronboardcontact2email(" +
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
                            msSQL = " insert into agr_mst_tbyronboardcontact2address(" +
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

                            if (values.Stakeholder_Type == "Borrower" || values.Stakeholder_Type == "Applicant")
                            {
                                msSQL = "select mobile_no from agr_mst_tbyronboardcontact2mobileno where contact_gid='" + lscontact_gid + "' and primary_status='yes'";
                                string lsmobileno = objdbconn.GetExecuteScalar(msSQL);

                                msSQL = "select email_address from agr_mst_tbyronboardcontact2email where contact_gid='" + lscontact_gid + "' and primary_status='yes'";
                                lsemail_address = objdbconn.GetExecuteScalar(msSQL);

                                msSQL = "update agr_mst_tbyronboard set applicant_type ='Individual' where application_gid='" + lsapplication_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                msSQL = "update agr_mst_tbyronboardcontact set mobile_no='" + lsmobileno + "'," +
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
            return DaIndividualList;
        }

        public void LogForAudit(string strVal)
        {
            try
            {
                string lspath = ConfigurationManager.AppSettings["file_path"].ToString() + "/erp_documents/SamagroMigrationLog/" + DateTime.Now.Year + @"\" + DateTime.Now.Month;
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

        public void LogForSuprAudit(string strVal)
        {
            try
            {
                string lspath = ConfigurationManager.AppSettings["file_path"].ToString() + "/erp_documents/SamagroMigrationLogSupplier/" + DateTime.Now.Year + @"\" + DateTime.Now.Month;
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

        /// Supplier Import onboard start 

        public void DaSupplierOnboardDataImport(HttpRequest httpRequest, string employee_gid, result objResult)
        {

            DataTable dt = null;
            DataTable Generaltable = new DataTable();
            DataTable CompanyDetailtable = new DataTable();
            DataTable IndividualDetailtable = new DataTable();
            //List<GeneralApplication_List> ApplicationInfoList = new List<GeneralApplication_List>();
            List<ApplicationList> ApplicationInfoList = new List<ApplicationList>();
            List<CompanyDetailsList> CompanyInfoList = new List<CompanyDetailsList>();
            List<IndividualDetailsList> IndividualInfoList = new List<IndividualDetailsList>();

            try
            {
                //int insertCount = 0;
                HttpFileCollection httpFileCollection;

                string lspath, lsfilePath;

                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                // Create Directory
                lsfilePath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/Samagro/SupplierExcelDataMigration/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

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
                var obj = new List<MdlExcelData_list>();
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
                            obj.Add(new MdlExcelData_list
                            {
                                sheetName = "" + worksheet.Name + "$",
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


                            Generaltable = ds.Tables["SupplierGeneralDetails"];
                            CompanyDetailtable = ds.Tables["SupplierCompanyDetails"];
                            IndividualDetailtable = ds.Tables["IndividualDetails"];
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

                            List<ApplicationList> applicationdtl = DaSubmitSuprGeneralDtl(ApplicationInfoList);

                            List<CompanyDetailsList> companydtl = DaSubmitSuprCompanyInfo(CompanyInfoList, applicationdtl);
                            // Individual Info  
                            // List<IndividualDetailsList> individualdtl = DaSubmitSuprIndividualInfo(IndividualInfoList, applicationdtl);
                        }

                    }
                    catch (Exception ex)
                    {
                        LogForSuprAudit(ex.ToString());
                        objResult.status = false;
                        objResult.message = ex.ToString();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    LogForSuprAudit(ex.ToString());
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
        public List<ApplicationList> DaSubmitSuprGeneralDtl(List<ApplicationList> datalist)
        {
            LogForSuprAudit("----General Info - Supplier Started -----------");
            // From Vertical Master
            msSQL = " SELECT a.vertical_gid,a.vertical_name,vertical_code " +
                    " FROM ocs_mst_tvertical a  where status_log='Y' order by a.vertical_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<verticalList> verticalList = new List<verticalList>();
            verticalList = ConvertDataTable<verticalList>(dt_datatable);
            dt_datatable.Dispose();

            // Credit Group Master
            msSQL = " SELECT creditmapping_gid as creditgroup_gid ,creditgroup_name FROM ocs_mst_tcreditmapping a" +
                      " where status='Y' order by a.creditmapping_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<CreditgroupList> CreditgroupList = new List<CreditgroupList>();
            CreditgroupList = ConvertDataTable<CreditgroupList>(dt_datatable);
            dt_datatable.Dispose();

            // Constitution 
            msSQL = " SELECT constitution_gid,constitution_name FROM ocs_mst_tconstitution a" +
                   " where status_log='Y' order by constitution_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<constitutionList> constitutionList = new List<constitutionList>();
            constitutionList = ConvertDataTable<constitutionList>(dt_datatable);
            dt_datatable.Dispose();

            // Product Master
            msSQL = " SELECT product_gid,product_name,businessunit_gid,businessunit_name,valuechain_gid,valuechain_name FROM ocs_mst_tproducts a" +
                     " where status='Y' order by a.product_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<productnameList> productnameList = new List<productnameList>();
            productnameList = ConvertDataTable<productnameList>(dt_datatable);
            dt_datatable.Dispose();

            // Variety Master

            msSQL = " SELECT product_gid,variety_gid,variety_name,botanical_name,alternative_name, hsn_code FROM ocs_mst_tvariety a " +
                    " order by a.product_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<VarietynameList> VarietynameList = new List<VarietynameList>();
            VarietynameList = ConvertDataTable<VarietynameList>(dt_datatable);
            dt_datatable.Dispose();

            // Program Master
            msSQL = " SELECT program_gid,program FROM ocs_mst_tprogram a" +
                       " where status='Y' order by a.program_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<programList> programList = new List<programList>();
            programList = ConvertDataTable<programList>(dt_datatable);
            dt_datatable.Dispose();

            // Vernacular Master
            msSQL = " SELECT vernacularlanguage_gid,vernacular_language FROM ocs_mst_tvernacularlanguage a" +
                     " where status='Y' order by a.vernacularlanguage_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<vernacularlang_list> vernacularlang_list = new List<vernacularlang_list>();
            vernacularlang_list = ConvertDataTable<vernacularlang_list>(dt_datatable);
            dt_datatable.Dispose();

            // Designation Master
            msSQL = " SELECT a.designation_gid,a.designation_type from ocs_mst_tdesignation a" +
                 "  where status_log='Y' order by a.designation_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<designationList> designationList = new List<designationList>();
            designationList = ConvertDataTable<designationList>(dt_datatable);
            dt_datatable.Dispose();

            msSQL = " SELECT geneticcode_gid,geneticcode_name FROM ocs_mst_tgeneticcode a" +
                  " where status='Y' order by a.geneticcode_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<GeneticList> GeneticList = new List<GeneticList>();
            GeneticList = ConvertDataTable<GeneticList>(dt_datatable);
            List<ApplicationList> DaApplicationList = new List<ApplicationList>();
            dt_datatable.Dispose();

            foreach (var values in datalist)
            {
                try
                {
                    string lsemployee_name = "", lsemployee_gid = "";
                    string lsdrm_gid = "", lsdrm_name = "";
                    //string lsapplication_gid = string.Empty;
                    string lsvertical_gid = "", lsConstitution_gid = "", lsvernacularlan_gid = "", lsdesignation_gid = "";
                    string lscredit_groupgid = "", lsprogram_gid = "", lsproduct_gid = "", lsvariety_gid = "";



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
                   
                    values.RM_EmployeeGid = lsemployee_gid;
                    if (values.ApplicationNumber != "" || values.ApplicationNumber != null)
                    {
                        var lsvertical = verticalList.Where(x => x.vertical_name.ToLower().Trim() == values.Vertical.ToLower().Trim()).FirstOrDefault();
                        if (lsvertical != null)
                            lsvertical_gid = lsvertical.vertical_gid;
                        var lsConstitution = constitutionList.Where(x => x.constitution_name.ToLower().Trim() == values.Constitution.ToLower().Trim()).FirstOrDefault();
                        if (lsConstitution != null)
                            lsConstitution_gid = lsConstitution.constitution_gid;
                        var lsvernacularlan = vernacularlang_list.Where(x => x.vernacular_language.ToLower().Trim() == values.Vernacular_Language.ToLower().Trim()).FirstOrDefault();
                        if (lsvernacularlan != null)
                            lsvernacularlan_gid = lsvernacularlan.vernacularlanguage_gid;
                        var lsdesignation = designationList.Where(x => x.designation_type.ToLower().Trim() == values.Designation.ToLower().Trim()).FirstOrDefault();
                        if (lsdesignation != null)
                            lsdesignation_gid = lsdesignation.designation_gid;
                        //var lscredit_group = CreditgroupList.Where(x => x.creditgroup_name.ToLower().Trim() == values.Credit_Group.ToLower().Trim()).FirstOrDefault();
                        //if (lscredit_group != null)
                        //    lscredit_groupgid = lscredit_group.creditgroup_gid;
                        //var lsprogram = programList.Where(x => x.program.ToLower().Trim() == values.Program.ToLower().Trim()).FirstOrDefault();
                        //if (lsprogram != null)
                        //    lsprogram_gid = lsprogram.program_gid;
                        var lsproduct_dtl = productnameList.Where(x => x.product_name.ToLower().Trim() == values.Product.ToLower().Trim()).FirstOrDefault();
                        if (lsproduct_dtl != null)
                        {
                            lsproduct_gid = lsproduct_dtl.product_gid;
                            values.Sector_Strategic_BusinessUnit = lsproduct_dtl.businessunit_name;
                            values.Category = lsproduct_dtl.valuechain_name;

                            try
                            {
                                var lsvarietydtl = VarietynameList.Where(x => x.variety_name.ToLower() == values.Variety.ToLower() && x.product_gid == lsproduct_gid).FirstOrDefault();
                                if (lsvarietydtl != null)
                                {
                                    lsvariety_gid = lsvarietydtl.variety_gid;
                                    values.Botanical_Name = lsvarietydtl.botanical_name;
                                    values.Alternative_Names = lsvarietydtl.alternative_name;
                                    values.hsn_code = lsvarietydtl.hsn_code;
                                }
                            }
                            catch (Exception ex)
                            {
                                lsvariety_gid = "";
                                values.Botanical_Name = "";
                                values.Alternative_Names = "";
                                values.hsn_code = "";
                            }
                        }


                        values.application_gid = objcmnfunctions.GetMasterGID("SYOG");
                        msSQL = " insert into agr_mst_tsupronboard(" +
                         " application_gid," +
                          " application_no, " +
                         " virtualaccount_number," +
                         " migration_applicationno, " +
                         " customerref_name," +
                         " vertical_gid," +
                         " vertical_name," +
                         " constitution_gid," +
                         " constitution_name," +
                         " sa_status," +
                         " saname_gid," +
                         " sa_name," +
                         " vernacular_language," +
                         " vernacularlanguage_gid," +
                         " contactpersonfirst_name," +
                         " contactpersonmiddle_name," +
                         " contactpersonlast_name," +
                         " designation_gid," +
                         " designation_type," +
                         " landline_no," +
                         " program_gid," +
                         " program_name," +
                         "createdby_name," +
                         " status," +
                         " created_by," +
                         " created_date) values(" +
                           "'" + values.application_gid + "'," +
                           "'" + values.ApplicationNumber + "'," +
                           "'" + values.VirtualAccountNumber + "'," +
                           "'" + values.ApplicationNumber + "'," +
                           "'" + values.customer_name + "'," +
                           "'" + lsvertical_gid + "'," +
                           "'" + values.Vertical + "'," +
                           "'" + lsConstitution_gid + "'," +
                           "'" + values.Constitution + "'," +
                           "'" + values.Application_FromSA_Yes_No + "'," +
                           "'" + values.SAMAssociate_IDName + "'," +
                           "'" + values.SAMAssociate_IDName + "'," +
                           "'" + values.Vernacular_Language + "'," +
                           "'" + lsvernacularlan_gid + "'," +
                           "'" + values.First_Name + "'," +
                           "'" + values.Middle_Name + "'," +
                           "'" + values.Last_Name + "'," +
                           "'" + lsdesignation_gid + "'," +
                           "'" + values.Designation + "'," +
                           "'" + values.Landline_Number + "'," +
                           "'" + lsprogram_gid + "'," +
                           "'" + values.Program + "'," +
                             "'" + lsemployee_name + "'," +
                             "'Completed'," +
                             "'" + lsemployee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msGetGid = objcmnfunctions.GetMasterGID("AP2P");
                        msSQL = " insert into agr_mst_tsupronboard2product (" +
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
                                " hsn_code, " +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid + "'," +
                                "null," +
                                "'" + values.application_gid + "'," +
                                "'" + lsproduct_gid + "'," +
                                "'" + values.Product + "'," +
                                "'" + lsvariety_gid + "'," +
                                "'" + values.variety_name + "'," +
                                "'" + values.Sector_Strategic_BusinessUnit + "'," +
                                "'" + values.Category + "'," +
                                "'" + values.Botanical_Name + "'," +
                                "'" + values.Alternative_Names + "'," +
                                "'" + values.hsn_code + "'," +
                                "'" + lsemployee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        // Genetic Code by Business Team 
                        foreach (var i in GeneticList)
                        {
                            msGetGid = objcmnfunctions.GetMasterGID("A2GC");
                            msSQL = " insert into agr_mst_tsupronboard2geneticcode(" +
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
                        msSQL = " insert into agr_mst_tsupronboard2contactno(" +
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
                        msSQL = " insert into agr_mst_tsupronboard2email(" +
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

                        DaApplicationList.Add(values);
                        LogForSuprAudit(" General Supplier - '" + values.ApplicationNumber + "' Completed");
                    }
                    else
                    {
                        LogForSuprAudit(" Application No: '" + values.ApplicationNumber + "' - Location / Vertical not Assign for Business Approval'");
                    }
                }

                catch (Exception ex)
                {
                    LogForSuprAudit(" General Supplier Info Error - '" + values.ApplicationNumber + "' - '" + ex.ToString() + "'");
                }
            }
            LogForSuprAudit("---------General Supplier Info - Completed successfully !--------------");
            return DaApplicationList;

        }

        // Company Info 
        public List<CompanyDetailsList> DaSubmitSuprCompanyInfo(List<CompanyDetailsList> datalist, List<ApplicationList> applicationdtl)
        {
            LogForSuprAudit("----Company Supplier Info - Started -----------");

            // Company Type List
            msSQL = " SELECT companytype_gid,companytype_name FROM ocs_mst_tcompanytype where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<companytype_list> companytype_list = new List<companytype_list>();
            companytype_list = ConvertDataTable<companytype_list>(dt_datatable);

            // Stakeholder Type

            msSQL = " SELECT usertype_gid,user_type FROM ocs_mst_tusertype where status_log='Y' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<usertypelist> usertypelist = new List<usertypelist>();
            usertypelist = ConvertDataTable<usertypelist>(dt_datatable);

            // Assessment Agency  
            msSQL = " select assessmentagency_gid, assessmentagency_name from ocs_mst_tassessmentagency where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<assessmentagencylist> assessmentagencylist = new List<assessmentagencylist>();
            assessmentagencylist = ConvertDataTable<assessmentagencylist>(dt_datatable);

            // Agency rating  
            msSQL = " select assessmentagencyrating_gid, assessmentagencyrating_name from ocs_mst_tassessmentagencyrating where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<assessmentagencyratingList> assessmentagencyratingList = new List<assessmentagencyratingList>();
            assessmentagencyratingList = ConvertDataTable<assessmentagencyratingList>(dt_datatable);

            // AML Category list  
            msSQL = " SELECT amlcategory_gid,amlcategory_name FROM ocs_mst_tamlcategory where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<amlcategoryList> amlcategoryList = new List<amlcategoryList>();
            amlcategoryList = ConvertDataTable<amlcategoryList>(dt_datatable);

            // Business Category 
            msSQL = " SELECT businesscategory_gid,businesscategory_name FROM ocs_mst_tbusinesscategory where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<businesscategoryList> businesscategoryList = new List<businesscategoryList>();
            businesscategoryList = ConvertDataTable<businesscategoryList>(dt_datatable);

            // Designation List 
            msSQL = " SELECT designation_gid,designation_type FROM ocs_mst_tdesignation where status_log='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<designationlist> designationlist = new List<designationlist>();
            designationlist = ConvertDataTable<designationlist>(dt_datatable);

            // Address Type 
            msSQL = " SELECT address_gid,address_type FROM ocs_mst_taddresstype order by address_gid asc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<addresstypelist> addresstypelist = new List<addresstypelist>();
            addresstypelist = ConvertDataTable<addresstypelist>(dt_datatable);

            // Postal Code
            msSQL = " select postalcode_value,city,taluka,district,state from ocs_mst_tpostalcode";
            //" postalcode_value='" + postal_code + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<ContactAddresslist> ContactAddresslist = new List<ContactAddresslist>();
            ContactAddresslist = ConvertDataTable<ContactAddresslist>(dt_datatable);

            // license type
            msSQL = " SELECT licensetype_gid,licensetype_name FROM ocs_mst_tlicensetype where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<licensetypelist> Mstlicensetypelist = new List<licensetypelist>();
            Mstlicensetypelist = ConvertDataTable<licensetypelist>(dt_datatable);

            List<CompanyDetailsList> companydtl = new List<CompanyDetailsList>();
            foreach (var values in datalist)
            {
                try
                {
                    string lsapplication_gid = "", employee_gid = "", lscompanytype_gid = "", lsstackholder_gid = "", lsassessmentagency_gid = "";
                    string lsassessmentagencyrating_gid = "", lsamlcategory_gid = "", lsbusinesscategory_gid = "", lsdesignation_gid = "", lsmobileno = "";
                    string lsemail_address = "", lsaddress_gid = "", lslicense_gid = "";

                    var getapplicationgid = applicationdtl.Where(x => x.ApplicationNumber == values.ApplicationNumber).FirstOrDefault();
                    if (getapplicationgid != null)
                    {
                        values.application_gid = getapplicationgid.application_gid;
                        employee_gid = getapplicationgid.RM_EmployeeGid;
                        values.created_by = getapplicationgid.RM_EmployeeGid;
                    }
                    if (values.application_gid != "")
                    {
                        var lscompanytype = companytype_list.Where(x => x.companytype_name.ToLower().Trim() == values.CompanyType.ToLower().Trim()).FirstOrDefault();
                        if (lscompanytype != null)
                            lscompanytype_gid = lscompanytype.companytype_gid;
                        var lsstackholder = usertypelist.Where(x => x.user_type.ToLower().Trim() == values.StakeholderType.ToLower().Trim()).FirstOrDefault();
                        if (lsstackholder != null)
                            lsstackholder_gid = lsstackholder.usertype_gid;
                        var lsassessmentagency = assessmentagencylist.Where(x => x.assessmentagency_name.ToLower().Trim() == values.CreditRatingAgency.ToLower().Trim()).FirstOrDefault();
                        if (lsassessmentagency != null)
                            lsassessmentagency_gid = lsassessmentagency.assessmentagency_gid;
                        var lsassessmentagencyrating = assessmentagencyratingList.Where(x => x.assessmentagencyrating_name.ToLower().Trim() == values.CreditRating.ToLower().Trim()).FirstOrDefault();
                        if (lsassessmentagencyrating != null)
                            lsassessmentagencyrating_gid = lsassessmentagencyrating.assessmentagencyrating_gid;
                        var lsamlcategory = amlcategoryList.Where(x => x.amlcategory_name.ToLower().Trim() == values.CategoryAML.ToLower().Trim()).FirstOrDefault();
                        if (lsamlcategory != null)
                            lsamlcategory_gid = lsamlcategory.amlcategory_gid;
                        var lsbusinesscategory = businesscategoryList.Where(x => x.businesscategory_name.ToLower().Trim() == values.CategoryBusiness.ToLower().Trim()).FirstOrDefault();
                        if (lsbusinesscategory != null)
                            lsbusinesscategory_gid = lsbusinesscategory.businesscategory_gid;
                        var lsdesignation = designationlist.Where(x => x.designation_type.ToLower().Trim() == values.Designation.ToLower().Trim()).FirstOrDefault();
                        if (lsdesignation != null)
                            lsdesignation_gid = lsdesignation.designation_gid;
                        var lsaddress = addresstypelist.Where(x => x.address_type.ToLower().Trim() == values.AddressType.ToLower().Trim()).FirstOrDefault();
                        if (lsaddress != null)
                            lsaddress_gid = lsaddress.address_gid;
                        var lslicense = Mstlicensetypelist.Where(x => x.licensetype_name.ToLower().Trim() == values.LicenseType.ToLower().Trim()).FirstOrDefault();
                        if (lslicense != null)
                            lslicense_gid = lslicense.licensetype_gid;
                        var contact_dtl = ContactAddresslist.Where(x => x.postalcode_value.ToLower().Trim() == values.PostalCode.ToLower().Trim()).FirstOrDefault();
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
                        msSQL = " insert into agr_mst_tsupronboard2institution(" +
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
                             " tan_number," +
                             " incometax_returnsstatus," +
                             " revenue," +
                             " profit," +
                             " fixed_assets," +
                             " sundrydebt_adv," +
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
                               "'" + values.Tan_number + "'," +
                                "'" + values.IncomeTaxreturnsstatus + "'," +
                                "'" + values.Revenue + "'," +
                                "'" + values.Profit + "'," +
                                "'" + values.FixedAssets + "'," +
                                "'" + values.SundryDebtorsandAdvances + "',"+
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
 
                            // Mobile Number 
                            msGetGid = objcmnfunctions.GetMasterGID("IT2M");
                            msSQL = " insert into agr_mst_tsupronboardinstitution2mobileno(" +
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
                            msSQL = " insert into agr_mst_tsupronboardinstitution2email(" +
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

                            msGetGid = objcmnfunctions.GetMasterGID("ITGS");
                            msSQL = " insert into agr_mst_tsupronboardinstitution2branch(" +
                                    " institution2branch_gid," +
                                    " institution_gid," +
                                    " gst_state," +
                                    " gst_no," +
                                    " gst_registered," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + institution_gid + "'," +
                                    "'" + values.GstState + "'," +
                                    "'" + values.GstNo + "'," +
                                    "'" + values.GstRegistered + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " insert into agr_mst_tsupronboardinstitution2ratingdetail(" +
                                     " institution2ratingdetail_gid," +
                                     " institution_gid," +
                                     " application_gid," +
                                      " creditrating_agencygid," +
                                      " creditrating_agencyname," +
                                      " creditrating_gid," +
                                      " creditrating_name," +
                                     " assessed_on," +
                                     " creditrating_link," +
                                     " created_by," +
                                   " created_date) values(" +
                                 "'" + msGetGid + "'," +
                                "'" + institution_gid + "'," +
                                "'" + values.application_gid + "'," +
                                "'" + lsassessmentagency_gid + "'," +
                                "'" + values.CreditRatingAgency + "'," +
                                "'" + lsassessmentagencyrating_gid + "'," +
                                   "'" + values.CreditRating + "'," +
                            "'" + Convert.ToDateTime(values.Assessed_on).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                              "'" + values.Creditrating_Link.Replace("'", "") + "'," +
                                  "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            // Address Details
                            msGetGid = objcmnfunctions.GetMasterGID("IT2A");
                            msSQL = " insert into agr_mst_tsupronboardinstitution2address(" +
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

                            msGetGid = objcmnfunctions.GetMasterGID("I2BD");
                            msSQL = " insert into agr_mst_tsupronboardinstitution2bankdtl(" +
                                    " institution2bankdtl_gid," +
                                    " institution_gid," +
                                    //" application_gid," +
                                    " bank_name," +
                                    " branch_name," +
                                    " bank_address," +
                                    " micr_code," +
                                    " ifsc_code," +
                                    " bankaccount_name," +
                                    " bankaccounttype_gid," +
                                    " bankaccounttype_name," +
                                    " bankaccount_number," +
                                    " confirmbankaccountnumber," +
                                    " joinaccount_status," +
                                    " joinaccount_name," +
                                    " primary_status," +
                                    " chequebook_status," +
                                    " accountopen_date," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + institution_gid + "'," +
                                    //"'" + values.application_gid + "'," +
                                    "'" + values.bank_name + "'," +
                                    "'" + values.branch_name + "'," +
                                    "'" + values.bank_address + "'," +
                                    "'" + values.micr_code + "'," +
                                    "'" + values.ifsc_code + "'," +
                                    "'" + values.bankaccount_name.Replace("'", "") + "'," +
                                    "'" + values.bankaccounttype_gid + "'," +
                                    "'" + values.bankaccounttype_name + "'," +
                                    "'" + values.bankaccount_number + "'," +
                                    "'" + values.confirmbankaccountnumber + "'," +
                                    "'" + values.joint_account + "'," +
                                    "'" + values.jointaccountholder_name + "'," +
                                    "'" + values.primary_status + "'," +
                                    "'" + values.chequebook_status + "',";
                            if (values.accountopen_date == null || values.accountopen_date == "")
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(values.accountopen_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }
                            msSQL += "'" + employee_gid + "'," +
                                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                            // License Details
                            msGetGid = objcmnfunctions.GetMasterGID("IT2L");
                            msSQL = " insert into agr_mst_tsupronboardinstitution2licensedtl(" +
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
                            if (values.StakeholderType == "Borrower" || values.StakeholderType == "Applicant")
                            {
                                msSQL = "select mobile_no from agr_mst_tsupronboardinstitution2mobileno where institution_gid='" + institution_gid + "' and primary_status='yes'";
                                lsmobileno = objdbconn.GetExecuteScalar(msSQL);

                                msSQL = "select email_address from agr_mst_tsupronboardinstitution2email where institution_gid='" + institution_gid + "' and primary_status='yes'";
                                lsemail_address = objdbconn.GetExecuteScalar(msSQL);

                                msSQL = "update agr_mst_tsupronboard set applicant_type ='Institution' where application_gid='" + values.application_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                msSQL = "update agr_mst_tsupronboard2institution set mobile_no='" + lsmobileno + "'," +
                                 " email_address='" + lsemail_address + "' where institution_gid='" + institution_gid + "' ";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }

                            companydtl.Add(values);
                        }
                        else
                        {
                            LogForSuprAudit("----Company Info Error - Not added Error '" + values.ApplicationNumber + "'"); 
                        }
                    }
                    else
                    {
                        LogForSuprAudit("----Company Info Error - Application Gid Not Found '" + values.ApplicationNumber + "'");
                    }
                }
                catch (Exception ex)
                {
                    LogForSuprAudit("----Company Info Error - '" + values.ApplicationNumber + "' - '" + ex.ToString() + "'-----------");
                }
                LogForSuprAudit("---------Company Info - Completed successfully !--------------");
            }
            return companydtl;
        }

        // Individual Info 

        public List<IndividualDetailsList> DaSubmitSuprIndividualInfo(List<IndividualDetailsList> datalist, List<ApplicationList> applicationdtl)
        {
            LogForSuprAudit("----Individual Info - Started ------");
            // gender
            msSQL = " SELECT gender_gid,gender_name FROM ocs_mst_tgender where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<Gender_list> Gender_list = new List<Gender_list>();
            Gender_list = ConvertDataTable<Gender_list>(dt_datatable);

            // Designation Master
            msSQL = " SELECT a.designation_gid,a.designation_type from ocs_mst_tdesignation a" +
                 "  where status_log='Y' order by a.designation_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<designationList> designationList = new List<designationList>();
            designationList = ConvertDataTable<designationList>(dt_datatable);

            // Educational Qualification
            msSQL = " SELECT educationalqualification_gid,educationalqualification_name FROM ocs_mst_teducationalqualification where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<educationalqualificationList> educationalqualificationList = new List<educationalqualificationList>();
            educationalqualificationList = ConvertDataTable<educationalqualificationList>(dt_datatable);

            // Stakeholder Type

            msSQL = " SELECT usertype_gid,user_type FROM ocs_mst_tusertype where status_log='Y' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<usertypelist> usertypelist = new List<usertypelist>();
            usertypelist = ConvertDataTable<usertypelist>(dt_datatable);

            // Marital Status 
            msSQL = " SELECT maritalstatus_gid,maritalstatus_name FROM ocs_mst_tmaritalstatus where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<MaritalStatusList> MaritalStatusList = new List<MaritalStatusList>();
            MaritalStatusList = ConvertDataTable<MaritalStatusList>(dt_datatable);

            // Ownership type 
            msSQL = " SELECT ownershiptype_gid,ownershiptype_name FROM ocs_mst_townershiptype where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<ownershiptypeList> ownershiptypeList = new List<ownershiptypeList>();
            ownershiptypeList = ConvertDataTable<ownershiptypeList>(dt_datatable);

            // Income Type 

            msSQL = " SELECT incometype_gid,incometype_name FROM ocs_mst_tincometype where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<IncometypeList> IncometypeList = new List<IncometypeList>();
            IncometypeList = ConvertDataTable<IncometypeList>(dt_datatable);

            // Property Holder
            msSQL = " SELECT propertyin_gid,propertyin_name FROM ocs_mst_tproperty where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<PropertyList> PropertyList = new List<PropertyList>();
            PropertyList = ConvertDataTable<PropertyList>(dt_datatable);

            // Resistance Type
            msSQL = " SELECT residencetype_gid,residencetype_name FROM ocs_mst_tresidencetype where status='Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<ResistanceList> ResistanceList = new List<ResistanceList>();
            ResistanceList = ConvertDataTable<ResistanceList>(dt_datatable);

            // Postal Code
            msSQL = " select postalcode_value,city,taluka,district,state from ocs_mst_tpostalcode";
            //" postalcode_value='" + postal_code + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<ContactAddresslist> ContactAddresslist = new List<ContactAddresslist>();
            ContactAddresslist = ConvertDataTable<ContactAddresslist>(dt_datatable);

            // Address Type 
            msSQL = " SELECT address_gid,address_type FROM ocs_mst_taddresstype order by address_gid asc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<addresstypelist> addresstypelist = new List<addresstypelist>();
            addresstypelist = ConvertDataTable<addresstypelist>(dt_datatable);

            List<IndividualDetailsList> DaIndividualList = new List<IndividualDetailsList>();
            foreach (var values in datalist)
            {
                try
                {
                    string lsapplication_gid = "", employee_gid = "", lsgender_gid = "", lsemail_address = "", lsdesignation_gid = "", lsqualification_gid = "";
                    string lsstackholder_gid = "", lsMaritalstatus_gid = "", lsownershiptype_gid = "", lsincometype_gid = "", lsgroup_gid = "", lspropertyholder_gid = "";
                    string lsresidencetype_gid = "", lsaddress_gid = "";

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
                        var lsdesignation = designationList.Where(x => x.designation_type.ToLower().Trim() == values.Designation.ToLower().Trim()).FirstOrDefault();
                        if (lsdesignation != null)
                            lsdesignation_gid = lsdesignation.designation_gid;
                        var lsstackholder = usertypelist.Where(x => x.user_type.ToLower().Trim() == values.Stakeholder_Type.ToLower().Trim()).FirstOrDefault();
                        if (lsstackholder != null)
                            lsstackholder_gid = lsstackholder.usertype_gid;
                        var lsgender = Gender_list.Where(x => x.gender_name.ToLower().Trim() == values.Gender.ToLower().Trim()).FirstOrDefault();
                        if (lsgender != null)
                            lsgender_gid = lsgender.gender_gid;
                        var lsMaritalstatus = MaritalStatusList.Where(x => x.maritalstatus_name.ToLower().Trim() == values.Marital_Status.ToLower().Trim()).FirstOrDefault();
                        if (lsMaritalstatus != null)
                            lsMaritalstatus_gid = lsMaritalstatus.maritalstatus_gid;
                        var lsownershiptype = ownershiptypeList.Where(x => x.ownershiptype_name.ToLower().Trim() == values.Ownership_Type.ToLower().Trim()).FirstOrDefault();
                        if (lsownershiptype != null)
                            lsownershiptype_gid = lsownershiptype.ownershiptype_gid;
                        var lsqualification = educationalqualificationList.Where(x => x.educationalqualification_name.ToLower().Trim() == values.Educational_Qualification.ToLower().Trim()).FirstOrDefault();
                        if (lsqualification != null)
                            lsqualification_gid = lsqualification.educationalqualification_gid;
                        var lsincometype = IncometypeList.Where(x => x.incometype_name.ToLower().Trim() == values.Income_Type.ToLower().Trim()).FirstOrDefault();
                        if (lsincometype != null)
                            lsincometype_gid = lsincometype.incometype_gid;
                        var lspropertyholder = PropertyList.Where(x => x.propertyin_name.ToLower().Trim() == values.Property_intheNameof.ToLower().Trim()).FirstOrDefault();
                        if (lsincometype != null)
                            lspropertyholder_gid = lspropertyholder.propertyin_gid;
                        var lsresidencetype = ResistanceList.Where(x => x.residencetype_name.ToLower().Trim() == values.Residence_Type.ToLower().Trim()).FirstOrDefault();
                        if (lsresidencetype != null)
                            lsresidencetype_gid = lsresidencetype.residencetype_gid;
                        var lsaddress = addresstypelist.Where(x => x.address_type.ToLower().Trim() == values.Address_Type.ToLower().Trim()).FirstOrDefault();
                        if (lsaddress != null)
                            lsaddress_gid = lsaddress.address_gid;
                        var contact_dtl = ContactAddresslist.Where(x => x.postalcode_value.Trim() == values.Postal_Code.Trim()).FirstOrDefault();
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
                        msSQL = " SELECT company_name, institution_gid from agr_mst_tsupronboard2institution where application_gid='" + lsapplication_gid + "' " +
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
                        msSQL = " insert into agr_mst_tsupronboardcontact(" +
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
                        msSQL += "'" + Convert.ToDateTime(values.Date_of_Birth).ToString("dd-MM-yyyy") + "'," +
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
                                 "'" + employee_gid + "'," +
                                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult != 0)
                        {

                            msGetGid = objcmnfunctions.GetMasterGID("I2BD");
                            msSQL = " insert into agr_mst_tsupronboardcontact2bankdtl(" +
                                    " contact2bankdtl_gid," +
                                    " contact_gid," +
                                    //" application_gid," +
                                    " bank_name," +
                                    " branch_name," +
                                    " bank_address," +
                                    " micr_code," +
                                    " ifsc_code," +
                                    " bankaccount_name," +
                                    " bankaccounttype_gid," +
                                    " bankaccounttype_name," +
                                    " bankaccount_number," +
                                    " confirmbankaccountnumber," +
                                    " joinaccount_status," +
                                    " joinaccount_name," +
                                    " primary_status," +
                                    " chequebook_status," +
                                    " accountopen_date," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + lscontact_gid + "'," +
                                    //"'" + values.application_gid + "'," +
                                    "'" + values.bank_name + "'," +
                                    "'" + values.branch_name + "'," +
                                    "'" + values.bank_address + "'," +
                                    "'" + values.micr_code + "'," +
                                    "'" + values.ifsc_code + "'," +
                                    "'" + values.bankaccount_name.Replace("'", "") + "'," +
                                    "'" + values.bankaccounttype_gid + "'," +
                                    "'" + values.bankaccounttype_name + "'," +
                                    "'" + values.bankaccount_number + "'," +
                                    "'" + values.confirmbankaccountnumber + "'," +
                                    "'" + values.joint_account + "'," +
                                    "'" + values.jointaccountholder_name + "'," +
                                    "'" + values.primary_status + "'," +
                                    "'" + values.chequebook_status + "',";
                            if (values.accountopen_date == null || values.accountopen_date == "")
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(values.accountopen_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }
                            msSQL += "'" + employee_gid + "'," +
                                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                          

                            msGetGid = objcmnfunctions.GetMasterGID("C2MN");
                            msSQL = " insert into agr_mst_tsupronboardcontact2mobileno(" +
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
                            msSQL = " insert into agr_mst_tsupronboardcontact2email(" +
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
                            msSQL = " insert into agr_mst_tsupronboardcontact2address(" +
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

                            if (values.Stakeholder_Type == "Borrower" || values.Stakeholder_Type == "Applicant")
                            {
                                msSQL = "select mobile_no from agr_mst_tsupronboardcontact2mobileno where contact_gid='" + lscontact_gid + "' and primary_status='yes'";
                                string lsmobileno = objdbconn.GetExecuteScalar(msSQL);

                                msSQL = "select email_address from agr_mst_tsupronboardcontact2email where contact_gid='" + lscontact_gid + "' and primary_status='yes'";
                                lsemail_address = objdbconn.GetExecuteScalar(msSQL);

                                msSQL = "update agr_mst_tsupronboard set applicant_type ='Individual' where application_gid='" + lsapplication_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                msSQL = "update agr_mst_tsupronboardcontact set mobile_no='" + lsmobileno + "'," +
                                    " email_address='" + lsemail_address + "' where contact_gid='" + lscontact_gid + "' ";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }

                            DaIndividualList.Add(values);
                        }
                        else
                        {
                            LogForSuprAudit("Individual Info Error - Not added Error '" + values.ApplicationNumber + "'");
                        }
                    }
                    else
                    {
                        LogForSuprAudit("Individual Info Error - Application Gid Not Found '" + values.ApplicationNumber + "'");
                    }

                }
                catch (Exception ex)
                {
                    LogForSuprAudit("Individual Info Error - '" + values.ApplicationNumber + "' - '" + ex.ToString() + "'");
                }
            }
            LogForSuprAudit("---------Individual Info - Completed successfully !--------------");
            return DaIndividualList;
        }
    }

}