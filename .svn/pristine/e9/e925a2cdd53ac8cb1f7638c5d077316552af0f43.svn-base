using ems.master.Models;
using ems.utilities.Functions;
using Spire.Doc;
using Spire.Doc.Documents;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Text.RegularExpressions;
using ems.storage.Functions;

/// <summary>
/// (It's used for pages in credit action icon in cad accepted page)CADCreditAction DataAccess Class accessed by API methods from related Controller class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash</remarks>

namespace ems.master.DataAccess
{
    public class DaCADCreditAction
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable, dt_datatable1, dt_child, dt_childindividual, dt_childgroup;
        string msSQL, msGetGid, msGetGid1, msGetGidCC, msGetGid2, msGetGid3, lscadapplication_gid;
        int mnResult, mnResult1, mnResult2, mnResultCAD, mnResultUntag;
        HttpPostedFile httpPostedFile;
        OdbcDataReader objODBCDataReader, objODBCDataReader1, objODBCDataReader2, objODBCDatareader;
        string lssanctionref_no, lstemplate_content, lscompany_code, lspath, lsdocument_path, fileName;
        string msGetRef, msGetGID, lsdocument_code, lsdocument_name, lsdocumenttype_name, lscompanydocument_name, lsindividualdocument_name, lsgroupdocument_name, lsdocumenttype_gid;
        string lscontent = string.Empty;
        string lsgeneticcode_gid, lsgeneticcode_name, lsapplication_gid, lsgeneticcode_status, lsgeneticcode_remarks, lscreditgeneticcode_gid, lscredit_gid;
        string lssupplier_gid, lssupplier_name, lsrelationship_vintage_year, lsrelationship_vintage_month, lspurchase_amount, lsbankdebit_amount, lsrelationship_supplier,
            lsstart_date, lsend_date,  lsapplication_no, lsinstitution2branch_gid, lsinstitution2mobileno_gid;
        string lsbuyer_gid, lsbuyer_name, lsbuyer_limit, lsavailed_limit, lsbalance_limit, lstop_buyer, lsbill_tenuredays, lsmargin, lsbankcredit_value, lsbankcredit_date;
        string lssource_deduction, lsrelationship_borrower, lsenduse_monitoring, lsinstitution2email_gid, lsinstitution2address_gid, lsinstitution2licensedtl_gid;
        string lsbank_address, lsmicr_code, lsbankaccount_name, lsbankaccounttype_gid, lsbankaccounttype_name, lsbankaccount_number, lsconfirmbankaccountnumber,
                lsjoinaccount_status, lsjoinaccount_name, lschequebook_status, lsaccountopen_date;
        string lsexistingbankfacility_gid, lsbank_gid, lsbank_name, lsfacilitysanctioned_on, lsfundedtypeindicator_gid, lsfundedtypeindicator_name, lssanction_limit,  lsifsc_code, lsbranch_name;
        string lscreditinstalmentfrequency_gid, lscreditinstalmentfrequency_name, lsinstalment_amount, lsoutstanding_amount, lsoverdue_amount, lsnumberofdays_overdue;
        string lsoverdueifany_dpd, lscreditaccountclassification_gid, lscreditaccountclassification_name, lsremarks, lsrecord_date, lscreditrepaymentdtl_gid;
        string lsaccountstatus_on, lscurrentoutsatnding_amount, lsinstalment_frequency, lsdemanddue_date, lsoringinaltenure_year, lsoringinaltenure_month, lsfacilitytype_gid;
        string lslendertype_gid, lslender_type,  lsnbfc_name,  lsfacility_type, lssanctionreference_id, lssanctioned_on, lssanction_amount;
        string lsoringinaltenure_days, lsbalancetenure_year, lsbalancetenure_month, lsbalancetenure_days, lsaccountclassification_gid, lsaccount_classification;
        string lsmonth, lsyear, lstotaldebits, lstotalcredits, lsaccttransferdebits, lsaccttransfercredits, lsloansrepayment, lscashdeposits, lspurchasepayments, lssalesreceipts, lschequeneftinward, lschequeneftoutward, lsoverdrawingscc, lssalesgst, excelRange, lsprogram_gid, lsprogram_name;
        int rowCount, columnCount;
        int columnNumber;
        string endRange, colName;
        int columnInsertCount;
        string lsproduct_gid, lsproduct_name, lsvariety_gid, lsvariety_name, lssector_name, lscategory_name, lsbotanical_name, lsalternative_name;
        int matchCount1, matchCount2;
        string lspan_status, msGetGidpan;
        string lscreditpolicy_gid, lscredit_policy, lscomplied_status, lsobservation;
        public string DocList, CreditList, DocUntagList;


        public void DaGetCreditOperationsView(string credit_gid, string applicant_type, MdlMstAppCreditUnderWriting values)
        {
            if (applicant_type == "Institution")
            {
                msSQL = " select company_name,stakeholder_type,urn_status, urn from ocs_trn_tcadinstitution where institution_gid='" + credit_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.company_name = objODBCDatareader["company_name"].ToString();
                    values.stakeholder_type = objODBCDatareader["stakeholder_type"].ToString();
                    values.urn_status = objODBCDatareader["urn_status"].ToString();
                    values.urn = objODBCDatareader["urn"].ToString();
                  
                }
                objODBCDatareader.Close();
            }
            else if (applicant_type == "Individual")
            {
                msSQL = " select first_name,stakeholder_type,urn_status, urn from ocs_trn_tcadcontact where contact_gid='" + credit_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.individual_name = objODBCDatareader["first_name"].ToString();
                    values.stakeholder_type = objODBCDatareader["stakeholder_type"].ToString();
                    values.urn_status = objODBCDatareader["urn_status"].ToString();
                    values.urn = objODBCDatareader["urn"].ToString();
                   
                }
                objODBCDatareader.Close();
            }
            else if (applicant_type == "Group")
            {
                msSQL = " select group_name,group_type,groupurn_status, group_urn from ocs_trn_tcadgroup where group_gid='" + credit_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.group_name = objODBCDatareader["group_name"].ToString();
                    values.group_type = objODBCDatareader["group_type"].ToString();
                    values.urn_status = objODBCDatareader["groupurn_status"].ToString();
                    values.urn = objODBCDatareader["group_urn"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;
            }
            values.message = "success";
        }
        public void DaApplyALLDocumentList(MdlMstCAD values, string user_gid, string employee_gid)
        {
            string lscovenant_type = "", lscompanydocument_gid = "", lsindividualdocument_gid = "", lsgroupdocument_gid = "";

            List<string> DocumentList = new List<string>();


            string documenttype_gid = "", documenttype_code = "", documenttype_name = "", groupdocumentchecklist_gid = "", documentuploaded_gid = "";
            string lsStackholdertype;

            msSQL = "select concat(user_firstname,' ', user_lastname, ' / ', user_code) as username from adm_mst_tuser where user_gid='" + user_gid + "'";
            string tagged_name = objdbconn.GetExecuteScalar(msSQL);


            if (values.applicant_type == "Institution")
            {
                //values.application_gid = "APPC20220628489";

                msSQL = " select stakeholder_type from ocs_trn_tcadinstitution where institution_gid = '" + values.credit_gid + "'";

                lsStackholdertype = objdbconn.GetExecuteScalar(msSQL);

                if (lsStackholdertype == "Guarantor")
                {
                    msSQL = " select institution_gid from ocs_trn_tcadinstitution where application_gid = '" + values.application_gid + "'" +
                   " and  stakeholder_type = 'Guarantor'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {

                        msSQL = " select group_concat(distinct companydocument_gid) " +
                                " from ocs_trn_tcaddocumentchecktls where  " +
                                " application_gid = '" + values.application_gid + "'" +
                                " and credit_gid = '" + values.credit_gid + "' " +
                                // " and companyalldoc_flag <> 'Y' " +
                                " and com_gur_flag = 'Y' and untagged_type is null";


                        DocList = objdbconn.GetExecuteScalar(msSQL);
                        DocumentList = DocList.Split(',').ToList();

                        msSQL = " select group_concat(distinct companydocument_gid) " +
                               " from ocs_trn_tcaddocumentchecktls where  " +
                               " application_gid = '" + values.application_gid + "'" +
                               " and credit_gid = '" + values.credit_gid + "' " +
                               // " and companyalldoc_flag <> 'Y' " +
                               " and com_gur_flag = 'Y' and untagged_type is not null";


                        DocUntagList = objdbconn.GetExecuteScalar(msSQL);

                    };
                    if ((DocList != "") || DocUntagList != "")
                    {

                        foreach (DataRow dt in dt_datatable.Rows)
                        {


                            // Applying newly added records

                            List<string> list = new List<string>();
                            List<string> toRemove = new List<string>();
                            List<string> FinalList = new List<string>();
                            List<string> CreditDocList = new List<string>();
                            List<string> DocumentUntagList = new List<string>();

                            CreditList = string.Empty;

                            msSQL = " select group_concat(distinct companydocument_gid) " +
                                " from ocs_trn_tcaddocumentchecktls where  " +
                                " application_gid = '" + values.application_gid + "'" +
                                " and credit_gid = '" + dt["institution_gid"].ToString() + "'";

                            CreditList = objdbconn.GetExecuteScalar(msSQL);
                            if (CreditList.Any(",".Contains))
                            {
                                CreditDocList = CreditList.Split(',').ToList();
                            }
                            else { CreditDocList.Add(CreditList); }

                            list = DocumentList;
                            toRemove = CreditDocList;
                            FinalList = list.Except(toRemove).ToList();
                            DocumentUntagList = list.Intersect(toRemove).ToList();

                            // Applying already untagged doc to tagged doc

                            if (DocumentUntagList != null)
                            {
                                foreach (string UntagListitem in DocumentUntagList)
                                {
                                    msSQL = " select* from ocs_trn_tcaddocumentchecktls " +
                                             " where  application_gid = '" + values.application_gid + "'" +
                                             " and credit_gid = '" + dt["institution_gid"].ToString() + "' and " +
                                             " companydocument_gid='" + UntagListitem + "' and untagged_type is not null ";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        msSQL = "update ocs_trn_tcaddocumentchecktls set " +
                                             " untagged_type = null where  application_gid = '" + values.application_gid + "'" +
                                             " and credit_gid = '" + dt["institution_gid"].ToString() + "' and " +
                                             " companydocument_gid='" + UntagListitem + "'";

                                        mnResultUntag = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }
                                    objODBCDatareader.Close();
                                }
                            }

                            if (FinalList != null)
                            {
                                bool lsgroupupdate = false;

                                foreach (string Listitem in FinalList)
                                {
                                    msSQL = " select companydocument_gid,documenttypes_gid,documenttype_name,companydocument_name,covenant_type " +
                                               " from ocs_mst_tcompanydocument where companydocument_gid='" + Listitem + "'";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                        lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                        lscompanydocument_name = objODBCDatareader["companydocument_name"].ToString();
                                        lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                                        lscompanydocument_gid = objODBCDatareader["companydocument_gid"].ToString();
                                    }
                                    objODBCDatareader.Close();

                                    msGetGID = objcmnfunctions.GetMasterGID("DOCG");

                                    msSQL = " insert into ocs_trn_tcaddocumentchecktls(" +
                                            " documentcheckdtl_gid," +
                                            " application_gid," +
                                            " credit_gid, " +
                                            " companydocument_gid, " +
                                            " documenttype_gid," +
                                            " documenttype_code," +
                                            " documenttype_name," +
                                            " covenant_type, " +
                                            " tagged_by, " +
                                            " tagged_name, " +
                                            " created_date," +
                                            " created_by,com_gur_flag,untagged_type)" +
                                            " VALUES(" +
                                            "'" + msGetGID + "'," +
                                            "'" + values.application_gid + "'," +
                                            "'" + dt["institution_gid"].ToString() + "'," +
                                            "'" + lscompanydocument_gid + "'," +
                                            "'" + lsdocumenttype_gid + "'," +
                                            "'" + lsdocumenttype_name + "'," +
                                            "'" + lscompanydocument_name.Replace("'", "") + "'," +
                                            "'" + lscovenant_type + "'," +
                                            "'" + values.taggedby + "'," +
                                            "'" + tagged_name + "'," +
                                            "current_timestamp," +
                                            "'" + user_gid + "','Y',null)";

                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                                    DaCADCreditAction objvalues = new DaCADCreditAction();
                                    objvalues.DaCADGroupDocChecklistinfo(values.application_gid, dt["institution_gid"].ToString(), employee_gid);


                                }
                                dt_datatable.Dispose();

                            }

                        }
                        if ((mnResult == 1) || (mnResultUntag == 1))
                        {
                            values.status = true;
                            values.message = "Document type checklist applied successfully to all Guarantors ";
                        }
                        else
                        {
                            values.status = false;
                            values.message = "There is No Document type checklist to apply for Guarantors";
                        }
                    }
                }
                else if (lsStackholdertype == "Member")
                {
                    msSQL = " select institution_gid from ocs_trn_tcadinstitution where application_gid = '" + values.application_gid + "'" +
                                       " and  stakeholder_type ='Member'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        msSQL = " select group_concat(distinct companydocument_gid) " +
                                " from ocs_trn_tcaddocumentchecktls where  " +
                                " application_gid = '" + values.application_gid + "'" +
                                " and credit_gid = '" + values.credit_gid + "' " +
                                " and com_mem_flag = 'Y' and untagged_type is null";


                        DocList = objdbconn.GetExecuteScalar(msSQL);
                        DocumentList = DocList.Split(',').ToList();

                        msSQL = " select group_concat(distinct companydocument_gid) " +
                              " from ocs_trn_tcaddocumentchecktls where  " +
                              " application_gid = '" + values.application_gid + "'" +
                              " and credit_gid = '" + values.credit_gid + "' " +
                              " and com_mem_flag = 'Y' and untagged_type is not null";


                        DocUntagList = objdbconn.GetExecuteScalar(msSQL);
                    };
                    if ((DocList != "") || DocUntagList != "")
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            // Applying newly added records
                            List<string> list = new List<string>();
                            List<string> toRemove = new List<string>();
                            List<string> FinalList = new List<string>();
                            List<string> CreditDocList = new List<string>();
                            List<string> DocumentUntagList = new List<string>();
                            CreditList = string.Empty;

                            msSQL = " select group_concat(distinct companydocument_gid) " +
                                " from ocs_trn_tcaddocumentchecktls where  " +
                                " application_gid = '" + values.application_gid + "'" +
                                " and credit_gid = '" + dt["institution_gid"].ToString() + "'";

                            CreditList = objdbconn.GetExecuteScalar(msSQL);
                            if (CreditList.Any(",".Contains))
                            {
                                CreditDocList = CreditList.Split(',').ToList();
                            }
                            else { CreditDocList.Add(CreditList); }

                            list = DocumentList;
                            toRemove = CreditDocList;
                            FinalList = list.Except(toRemove).ToList();
                            DocumentUntagList = list.Intersect(toRemove).ToList();

                            // Applying already untagged doc to tagged doc

                            if (DocumentUntagList != null)
                            {
                                foreach (string UntagListitem in DocumentUntagList)
                                {
                                    msSQL = " select* from ocs_trn_tcaddocumentchecktls " +
                                            " where  application_gid = '" + values.application_gid + "'" +
                                            " and credit_gid = '" + dt["institution_gid"].ToString() + "' and " +
                                            " companydocument_gid='" + UntagListitem + "' and untagged_type is not null ";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        msSQL = "update ocs_trn_tcaddocumentchecktls set " +
                                             " untagged_type = null where  application_gid = '" + values.application_gid + "'" +
                                             " and credit_gid = '" + dt["institution_gid"].ToString() + "' and " +
                                             " companydocument_gid='" + UntagListitem + "'";

                                        mnResultUntag = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }
                                    objODBCDatareader.Close();

                                }
                            }
                            if (FinalList != null)
                            {
                                foreach (string Listitem in FinalList)
                                {
                                    msSQL = " select companydocument_gid,documenttypes_gid,documenttype_name,companydocument_name,covenant_type " +
                                               " from ocs_mst_tcompanydocument where companydocument_gid='" + Listitem + "'";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                        lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                        lscompanydocument_name = objODBCDatareader["companydocument_name"].ToString();
                                        lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                                        lscompanydocument_gid = objODBCDatareader["companydocument_gid"].ToString();
                                    }
                                    objODBCDatareader.Close();

                                    msGetGID = objcmnfunctions.GetMasterGID("DOCG");

                                    msSQL = " insert into ocs_trn_tcaddocumentchecktls(" +
                                            " documentcheckdtl_gid," +
                                            " application_gid," +
                                            " credit_gid, " +
                                            " companydocument_gid, " +
                                            " documenttype_gid," +
                                            " documenttype_code," +
                                            " documenttype_name," +
                                            " covenant_type, " +
                                            " tagged_by, " +
                                            " tagged_name, " +
                                            " created_date," +
                                            " created_by,com_mem_flag,untagged_type)" +
                                            " VALUES(" +
                                            "'" + msGetGID + "'," +
                                            "'" + values.application_gid + "'," +
                                            "'" + dt["institution_gid"].ToString() + "'," +
                                            "'" + lscompanydocument_gid + "'," +
                                            "'" + lsdocumenttype_gid + "'," +
                                            "'" + lsdocumenttype_name + "'," +
                                            "'" + lscompanydocument_name.Replace("'", "") + "'," +
                                            "'" + lscovenant_type + "'," +
                                            "'" + values.taggedby + "'," +
                                            "'" + tagged_name + "'," +
                                            "current_timestamp," +
                                            "'" + user_gid + "','Y',null)";

                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                                    DaCADCreditAction objvalues = new DaCADCreditAction();
                                    objvalues.DaCADGroupDocChecklistinfo(values.application_gid, dt["institution_gid"].ToString(), employee_gid);

                                }
                            }
                            dt_datatable.Dispose();

                        }

                    }
                    if ((mnResult == 1) || (mnResultUntag == 1))
                    {
                        values.status = true;
                        values.message = "Document type checklist applied successfully to all Members ";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "There is No Document type checklist to apply for Members";
                    }

                }

            }
            else if (values.applicant_type == "Individual")
            {
                //values.application_gid = "APPC20220628489";

                msSQL = " select stakeholder_type from ocs_trn_tcadcontact where contact_gid = '" + values.credit_gid + "'";

                lsStackholdertype = objdbconn.GetExecuteScalar(msSQL);

                if (lsStackholdertype == "Guarantor")
                {
                    msSQL = " select contact_gid from ocs_trn_tcadcontact where application_gid = '" + values.application_gid + "'" +
                   " and  stakeholder_type = 'Guarantor'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {

                        msSQL = " select group_concat(distinct individualdocument_gid) " +
                                " from ocs_trn_tcaddocumentchecktls where  " +
                                " application_gid = '" + values.application_gid + "'" +
                                " and credit_gid = '" + values.credit_gid + "' " +
                                " and ind_gur_flag = 'Y' and untagged_type is null";


                        DocList = objdbconn.GetExecuteScalar(msSQL);
                        DocumentList = DocList.Split(',').ToList();

                        msSQL = " select group_concat(distinct individualdocument_gid) " +
                             " from ocs_trn_tcaddocumentchecktls where  " +
                             " application_gid = '" + values.application_gid + "'" +
                             " and credit_gid = '" + values.credit_gid + "' " +
                             " and ind_gur_flag = 'Y' and untagged_type is not null";


                        DocUntagList = objdbconn.GetExecuteScalar(msSQL);
                    };
                    if (DocList != "")
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            // Applying newly added records
                            List<string> list = new List<string>();
                            List<string> toRemove = new List<string>();
                            List<string> FinalList = new List<string>();
                            List<string> CreditDocList = new List<string>();
                            List<string> DocumentUntagList = new List<string>();
                            CreditList = string.Empty;

                            msSQL = " select group_concat(distinct individualdocument_gid) " +
                                " from ocs_trn_tcaddocumentchecktls where  " +
                                " application_gid = '" + values.application_gid + "'" +
                                " and credit_gid = '" + dt["contact_gid"].ToString() + "'";

                            CreditList = objdbconn.GetExecuteScalar(msSQL);
                            if (CreditList.Any(",".Contains))
                            {
                                CreditDocList = CreditList.Split(',').ToList();
                            }
                            else { CreditDocList.Add(CreditList); }

                            list = DocumentList;
                            toRemove = CreditDocList;
                            FinalList = list.Except(toRemove).ToList();
                            DocumentUntagList = list.Intersect(toRemove).ToList();

                            // Applying already untagged doc to tagged doc

                            if (DocumentUntagList != null)
                            {
                                foreach (string UntagListitem in DocumentUntagList)
                                {
                                    msSQL = " select* from ocs_trn_tcaddocumentchecktls " +
                                            " where  application_gid = '" + values.application_gid + "'" +
                                            " and credit_gid = '" + dt["contact_gid"].ToString() + "' and " +
                                            " individualdocument_gid='" + UntagListitem + "' and untagged_type is not null ";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        msSQL = "update ocs_trn_tcaddocumentchecktls set " +
                                             " untagged_type = null where  application_gid = '" + values.application_gid + "'" +
                                             " and credit_gid = '" + dt["contact_gid"].ToString() + "' and " +
                                             " individualdocument_gid='" + UntagListitem + "'";

                                        mnResultUntag = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }
                                    objODBCDatareader.Close();

                                }
                                if (FinalList != null)
                                {
                                    foreach (string Listitem in FinalList)
                                    {
                                        msSQL = " select individualdocument_gid,documenttypes_gid,documenttype_name, " +
                                                " individualdocument_name,covenant_type " +
                                                " from ocs_mst_tindividualdocument where individualdocument_gid='" + Listitem + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                            lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                            lscompanydocument_name = objODBCDatareader["individualdocument_name"].ToString();
                                            lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                                            lsindividualdocument_gid = objODBCDatareader["individualdocument_gid"].ToString();
                                        }
                                        objODBCDatareader.Close();


                                        msGetGID = objcmnfunctions.GetMasterGID("DOCG");

                                        msSQL = " insert into ocs_trn_tcaddocumentchecktls(" +
                                               " documentcheckdtl_gid," +
                                               " application_gid," +
                                               " credit_gid, " +
                                               " individualdocument_gid, " +
                                               " documenttype_gid," +
                                               " documenttype_code," +
                                               " documenttype_name," +
                                               " covenant_type, " +
                                               " tagged_by, " +
                                               " tagged_name, " +
                                               " created_date," +
                                               " created_by,ind_gur_flag,untagged_type)" +
                                               " VALUES(" +
                                               "'" + msGetGID + "'," +
                                               "'" + values.application_gid + "'," +
                                               "'" + dt["contact_gid"].ToString() + "'," +
                                               "'" + lsindividualdocument_gid + "'," +
                                               "'" + lsdocumenttype_gid + "'," +
                                               "'" + lsdocumenttype_name + "'," +
                                               "'" + lscompanydocument_name.Replace("'", "") + "'," +
                                               "'" + lscovenant_type + "'," +
                                               "'" + values.taggedby + "'," +
                                               "'" + tagged_name + "'," +
                                               "current_timestamp," +
                                               "'" + user_gid + "','Y',null)";

                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                        DaCADCreditAction objvalues = new DaCADCreditAction();
                                        objvalues.DaCADGroupDocChecklistinfo(values.application_gid, dt["contact_gid"].ToString(), employee_gid);

                                    }

                                }
                                dt_datatable.Dispose();

                            }

                        }
                    }
                    if ((mnResult == 1) || (mnResultUntag == 1))
                    {
                        values.status = true;
                        values.message = "Document type checklist applied successfully to all Guarantors ";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "There is No Document type checklist to apply for Guarantors";
                    }

                }

                else if (lsStackholdertype == "Member")
                {
                    msSQL = " select contact_gid from ocs_trn_tcadcontact where application_gid = '" + values.application_gid + "'" +
                                       " and  stakeholder_type ='Member'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        msSQL = " select group_concat(distinct individualdocument_gid) " +
                                " from ocs_trn_tcaddocumentchecktls where  " +
                                " application_gid = '" + values.application_gid + "'" +
                                " and credit_gid = '" + values.credit_gid + "' " +
                                " and ind_mem_flag = 'Y' and untagged_type is null";

                        DocList = objdbconn.GetExecuteScalar(msSQL);
                        DocumentList = DocList.Split(',').ToList();
                        msSQL = " select group_concat(distinct companydocument_gid) " +
                                " from ocs_trn_tcaddocumentchecktls where  " +
                                " application_gid = '" + values.application_gid + "'" +
                                " and credit_gid = '" + values.credit_gid + "' " +
                                " and ind_mem_flag = 'Y' and untagged_type is not null";


                        DocUntagList = objdbconn.GetExecuteScalar(msSQL);
                    };
                    if (DocList != "")
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            // Applying newly added records
                            List<string> list = new List<string>();
                            List<string> toRemove = new List<string>();
                            List<string> FinalList = new List<string>();
                            List<string> CreditDocList = new List<string>();
                            List<string> DocumentUntagList = new List<string>();
                            CreditList = string.Empty;

                            msSQL = " select group_concat(distinct individualdocument_gid) " +
                                " from ocs_trn_tcaddocumentchecktls where  " +
                                " application_gid = '" + values.application_gid + "'" +
                                " and credit_gid = '" + dt["contact_gid"].ToString() + "'";

                            CreditList = objdbconn.GetExecuteScalar(msSQL);
                            if (CreditList.Any(",".Contains))
                            {
                                CreditDocList = CreditList.Split(',').ToList();
                            }
                            else { CreditDocList.Add(CreditList); }

                            list = DocumentList;
                            toRemove = CreditDocList;
                            FinalList = list.Except(toRemove).ToList();
                            DocumentUntagList = list.Intersect(toRemove).ToList();

                            // Applying already untagged doc to tagged doc

                            if (DocumentUntagList != null)
                            {
                                foreach (string UntagListitem in DocumentUntagList)
                                {
                                    msSQL = " select* from ocs_trn_tcaddocumentchecktls " +
                                            " where  application_gid = '" + values.application_gid + "'" +
                                            " and credit_gid = '" + dt["contact_gid"].ToString() + "' and " +
                                            " individualdocument_gid='" + UntagListitem + "' and untagged_type is not null ";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        msSQL = "update ocs_trn_tcaddocumentchecktls set " +
                                             " untagged_type = null where  application_gid = '" + values.application_gid + "'" +
                                             " and credit_gid = '" + dt["contact_gid"].ToString() + "' and " +
                                             " individualdocument_gid='" + UntagListitem + "'";

                                        mnResultUntag = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }
                                    objODBCDatareader.Close();

                                }

                                if (FinalList != null)
                                {
                                    foreach (string Listitem in FinalList)
                                    {
                                        msSQL = " select individualdocument_gid,documenttypes_gid,documenttype_name, " +
                                                " individualdocument_name,covenant_type " +
                                                " from ocs_mst_tindividualdocument where individualdocument_gid='" + Listitem + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                            lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                            lscompanydocument_name = objODBCDatareader["individualdocument_name"].ToString();
                                            lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                                            lsindividualdocument_gid = objODBCDatareader["individualdocument_gid"].ToString();
                                        }
                                        objODBCDatareader.Close();

                                        msGetGID = objcmnfunctions.GetMasterGID("DOCG");

                                        msSQL = " insert into ocs_trn_tcaddocumentchecktls(" +
                                               " documentcheckdtl_gid," +
                                               " application_gid," +
                                               " credit_gid, " +
                                               " individualdocument_gid, " +
                                               " documenttype_gid," +
                                               " documenttype_code," +
                                               " documenttype_name," +
                                               " covenant_type, " +
                                               " tagged_by, " +
                                               " tagged_name, " +
                                               " created_date," +
                                               " created_by,ind_mem_flag,untagged_type)" +
                                               " VALUES(" +
                                               "'" + msGetGID + "'," +
                                               "'" + values.application_gid + "'," +
                                               "'" + dt["contact_gid"].ToString() + "'," +
                                               "'" + lsindividualdocument_gid + "'," +
                                               "'" + lsdocumenttype_gid + "'," +
                                               "'" + lsdocumenttype_name + "'," +
                                               "'" + lscompanydocument_name.Replace("'", "") + "'," +
                                               "'" + lscovenant_type + "'," +
                                               "'" + values.taggedby + "'," +
                                               "'" + tagged_name + "'," +
                                               "current_timestamp," +
                                               "'" + user_gid + "','Y',null)";

                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                        DaCADCreditAction objvalues = new DaCADCreditAction();
                                        objvalues.DaCADGroupDocChecklistinfo(values.application_gid, dt["contact_gid"].ToString(), employee_gid);

                                    }
                                }
                                dt_datatable.Dispose();
                            }
                        }
                        if ((mnResult == 1) || (mnResultUntag == 1))
                        {
                            values.status = true;
                            values.message = "Document type checklist applied successfully to all Members ";
                        }
                        else
                        {
                            values.status = false;
                            values.message = "There is No Document type checklist to apply for Members";
                        }
                    }
                }

            }
        }

        public void DaApplyALLCovenantDocumentList(MdlMstCAD values, string user_gid, string employee_gid)
        {
            //List<CovenantPeriod> TaggedDocumentChecklist = values.CovenantPeriod.Where(a => a.covenantchecked == true).ToList();
            //List<CovenantPeriod> UnTaggedDocumentChecklist = values.CovenantPeriod.Where(a => a.covenantchecked == false).ToList();

            //List<CovenantPeriod> caddocchecklist = new List<CovenantPeriod>();
            //List<CovenantPeriod> newlyaddedchecklist = new List<CovenantPeriod>();
            //List<CovenantPeriod> untaggedcaddocchecklist = new List<CovenantPeriod>();

            //caddocchecklist = TaggedDocumentChecklist.Where(a => a.groupcovdocumentchecklist_gid != "" && a.groupcovdocumentchecklist_gid != null).ToList();
            //newlyaddedchecklist = TaggedDocumentChecklist.Where(a => a.groupcovdocumentchecklist_gid == "" || a.groupcovdocumentchecklist_gid == null).ToList();
            //untaggedcaddocchecklist = UnTaggedDocumentChecklist.Where(a => a.groupcovdocumentchecklist_gid != "" && a.groupcovdocumentchecklist_gid != null).ToList();
            bool Isgroupnew = false;

            string lscovenant_type = "", lscompanydocument_gid = "", lsindividualdocument_gid = "", lsgroupdocument_gid = "";

            List<string> DocumentList = new List<string>();

            string documenttype_gid = "", documenttype_code = "", documenttype_name = "", groupdocumentchecklist_gid = "", documentuploaded_gid = "";
            string lsStackholdertype;
            string lsApplication_gid = string.Empty;
            string lsCredit_gid = string.Empty;
            string buffer_days = string.Empty;
            string covenant_periods = string.Empty;

            lsApplication_gid = values.application_gid;
            lsCredit_gid = values.credit_gid;

            msSQL = "select concat(user_firstname,' ', user_lastname, ' / ', user_code) as username from adm_mst_tuser where user_gid='" + user_gid + "'";
            string tagged_name = objdbconn.GetExecuteScalar(msSQL);


            if (values.applicant_type == "Institution")
            {
                //values.application_gid = "APPC20220628489";

                msSQL = " select stakeholder_type from ocs_trn_tcadinstitution where institution_gid = '" + lsCredit_gid + "'";

                lsStackholdertype = objdbconn.GetExecuteScalar(msSQL);

                if (lsStackholdertype == "Guarantor")
                {
                    msSQL = " select institution_gid from ocs_trn_tcadinstitution where application_gid = '" + lsApplication_gid + "'" +
                   " and  stakeholder_type = 'Guarantor'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {

                        msSQL = " select group_concat(distinct companydocument_gid) " +
                                " from ocs_trn_tcadcovanantdocumentcheckdtls where  " +
                                " application_gid = '" + lsApplication_gid + "'" +
                                " and credit_gid = '" + lsCredit_gid + "' " +
                                // " and companyalldoc_flag <> 'Y' " +
                                " and com_gur_flag = 'Y' and untagged_type is null";


                        DocList = objdbconn.GetExecuteScalar(msSQL);
                        DocumentList = DocList.Split(',').ToList();
                    };
                    if ((DocList != ""))
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            List<string> list = new List<string>();
                            List<string> toRemove = new List<string>();
                            List<string> FinalList = new List<string>();
                            List<string> CreditDocList = new List<string>();
                            CreditList = string.Empty;

                            msSQL = " select group_concat(distinct companydocument_gid) " +
                                " from ocs_trn_tcadcovanantdocumentcheckdtls where  " +
                                " application_gid = '" + lsApplication_gid + "'" +
                                " and credit_gid = '" + dt["institution_gid"].ToString() + "'";

                            CreditList = objdbconn.GetExecuteScalar(msSQL);
                            if (CreditList.Any(",".Contains))
                            {
                                CreditDocList = CreditList.Split(',').ToList();
                            }
                            else { CreditDocList.Add(CreditList); }

                            list = DocumentList;
                            toRemove = CreditDocList;
                            FinalList = list.Except(toRemove).ToList();

                            if (FinalList != null)
                            {
                                bool lsgroupupdate = false;

                                foreach (string Listitem in FinalList)
                                {
                                    if (values.applicant_type == "Individual")
                                    {
                                        msGetGID = objcmnfunctions.GetMasterGID("CDCL");
                                        msSQL = " select individualdocument_gid,documenttypes_gid,documenttype_name,individualdocument_name,covenant_type " +
                                                    " from ocs_mst_tindividualdocument where individualdocument_gid='" + Listitem + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                            lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                            lscompanydocument_name = objODBCDatareader["individualdocument_name"].ToString();
                                            lscovenant_type = objODBCDatareader["covenant_type"].ToString();

                                        }
                                        objODBCDatareader.Close();
                                    }
                                    else if (values.applicant_type == "Group")
                                    {
                                        msGetGID = objcmnfunctions.GetMasterGID("CDCL");
                                        msSQL = " select groupdocument_gid,documenttypes_gid,documenttype_name,groupdocument_name,covenant_type " +
                                                    " from ocs_mst_tgroupdocument  where groupdocument_gid='" + Listitem + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                            lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                            lscompanydocument_name = objODBCDatareader["groupdocument_name"].ToString();
                                            lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                                        }
                                        objODBCDatareader.Close();
                                    }
                                    else
                                    {
                                        msGetGID = objcmnfunctions.GetMasterGID("CDCL");
                                        msSQL = " select companydocument_gid,documenttypes_gid,documenttype_name,companydocument_name,covenant_type " +
                                                    " from ocs_mst_tcompanydocument where companydocument_gid='" + Listitem + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                            lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                            lscompanydocument_name = objODBCDatareader["companydocument_name"].ToString();
                                            lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                                        }
                                        objODBCDatareader.Close();
                                    }

                                    msSQL = " select covenant_periods, buffer_days " +
                                            " from ocs_trn_tcadcovanantdocumentcheckdtls where " +
                                            " application_gid = '" + lsApplication_gid + "'  and " +
                                            " credit_gid = '" + lsCredit_gid + "' and " +
                                            " com_gur_flag = 'Y' and companydocument_gid = '" + Listitem + "'";

                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        covenant_periods = objODBCDatareader["covenant_periods"].ToString();
                                        buffer_days = objODBCDatareader["buffer_days"].ToString();

                                    }
                                    objODBCDatareader.Close();

                                    msSQL = " insert into ocs_trn_tcadcovanantdocumentcheckdtls(" +
                                             " covenantdocumentcheckdtl_gid," +
                                             " application_gid," +
                                             " credit_gid, ";
                                    if (values.applicant_type == "Institution")
                                        msSQL += " companydocument_gid, ";
                                    else if (values.applicant_type == "Individual")
                                        msSQL += " individualdocument_gid, ";
                                    else
                                    {
                                        msSQL += " groupdocument_gid,";
                                    }
                                    msSQL += " documenttype_gid," +
                                        " documenttype_code," +
                                        " documenttype_name," +
                                        " covenant_type, " +
                                        " tagged_by, " +
                                        " tagged_name, " +
                                        " covenant_periods, " +
                                        " covenantperiod_updatedby, " +
                                        " covenantperiod_updateddate, " +
                                        " buffer_days, " +
                                        " bufferday_updatedby, " +
                                        " bufferday_updateddate, " +
                                        " created_date," +
                                        " created_by)" +
                                        " VALUES(" +
                                        "'" + msGetGID + "'," +
                                        "'" + lsApplication_gid + "'," +
                                        "'" + dt["institution_gid"].ToString() + "'," +
                                        "'" + Listitem + "'," +
                                        "'" + lsdocumenttype_gid + "'," +
                                        "'" + lsdocumenttype_name + "'," +
                                        "'" + lscompanydocument_name.Replace("'", "") + "'," +
                                        "'" + lscovenant_type + "'," +
                                        "'" + values.taggedby + "'," +
                                        "'" + tagged_name + "'," +
                                        "'" + covenant_periods + "'," +
                                        "'" + user_gid + "'," +
                                        "current_timestamp," +
                                        "'" + buffer_days + "'," +
                                        "'" + user_gid + "'," +
                                        "current_timestamp," +
                                        "current_timestamp," +
                                        "'" + user_gid + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    Isgroupnew = true;


                                    DaMstScannedDocument objvalues = new DaMstScannedDocument();
                                    objvalues.DaGroupDocChecklistinfoCredit(lsApplication_gid, dt["institution_gid"].ToString(), employee_gid);


                                }
                                dt_datatable.Dispose();

                            }

                        }
                        if (mnResult == 1)
                        {
                            values.status = true;
                            values.message = "Document type checklist applied successfully to all Guarantors ";
                        }
                        else
                        {
                            values.status = false;
                            values.message = "There is No Document type checklist to apply for Guarantors";
                        }
                    }
                }
                else if (lsStackholdertype == "Member")
                {
                    msSQL = " select institution_gid from ocs_trn_tcadinstitution where application_gid = '" + lsApplication_gid + "'" +
                                       " and  stakeholder_type ='Member'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        msSQL = " select group_concat(distinct companydocument_gid) " +
                                " from ocs_trn_tcadcovanantdocumentcheckdtls where  " +
                                " application_gid = '" + lsApplication_gid + "'" +
                                " and credit_gid = '" + lsCredit_gid + "' " +
                                " and com_mem_flag = 'Y' and untagged_type is null";


                        DocList = objdbconn.GetExecuteScalar(msSQL);
                        DocumentList = DocList.Split(',').ToList();
                    };
                    if ((DocList != ""))
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            List<string> list = new List<string>();
                            List<string> toRemove = new List<string>();
                            List<string> FinalList = new List<string>();
                            List<string> CreditDocList = new List<string>();
                            CreditList = string.Empty;

                            msSQL = " select group_concat(distinct companydocument_gid) " +
                                " from ocs_trn_tcadcovanantdocumentcheckdtls where  " +
                                " application_gid = '" + lsApplication_gid + "'" +
                                " and credit_gid = '" + dt["institution_gid"].ToString() + "'";

                            CreditList = objdbconn.GetExecuteScalar(msSQL);
                            if (CreditList.Any(",".Contains))
                            {
                                CreditDocList = CreditList.Split(',').ToList();
                            }
                            else { CreditDocList.Add(CreditList); }

                            list = DocumentList;
                            toRemove = CreditDocList;
                            FinalList = list.Except(toRemove).ToList();

                            if (FinalList != null)
                            {
                                foreach (string Listitem in FinalList)
                                {
                                    if (values.applicant_type == "Individual")
                                    {
                                        msGetGID = objcmnfunctions.GetMasterGID("CDCL");
                                        msSQL = " select individualdocument_gid,documenttypes_gid,documenttype_name,individualdocument_name,covenant_type " +
                                                    " from ocs_mst_tindividualdocument where individualdocument_gid='" + Listitem + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                            lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                            lscompanydocument_name = objODBCDatareader["individualdocument_name"].ToString();
                                            lscovenant_type = objODBCDatareader["covenant_type"].ToString();

                                        }
                                        objODBCDatareader.Close();
                                    }
                                    else if (values.applicant_type == "Group")
                                    {
                                        msGetGID = objcmnfunctions.GetMasterGID("CDCL");
                                        msSQL = " select groupdocument_gid,documenttypes_gid,documenttype_name,groupdocument_name,covenant_type " +
                                                    " from ocs_mst_tgroupdocument  where groupdocument_gid='" + Listitem + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                            lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                            lscompanydocument_name = objODBCDatareader["groupdocument_name"].ToString();
                                            lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                                        }
                                        objODBCDatareader.Close();
                                    }
                                    else
                                    {
                                        msGetGID = objcmnfunctions.GetMasterGID("CDCL");
                                        msSQL = " select companydocument_gid,documenttypes_gid,documenttype_name,companydocument_name,covenant_type " +
                                                    " from ocs_mst_tcompanydocument where companydocument_gid='" + Listitem + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                            lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                            lscompanydocument_name = objODBCDatareader["companydocument_name"].ToString();
                                            lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                                        }
                                        objODBCDatareader.Close();
                                    }

                                    msSQL = " select covenant_periods, buffer_days " +
                                            " from ocs_trn_tcadcovanantdocumentcheckdtls where " +
                                            " application_gid = '" + lsApplication_gid + "'  and " +
                                            " credit_gid = '" + lsCredit_gid + "' and " +
                                            " com_mem_flag = 'Y' and companydocument_gid = '" + Listitem + "'";

                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        covenant_periods = objODBCDatareader["covenant_periods"].ToString();
                                        buffer_days = objODBCDatareader["buffer_days"].ToString();

                                    }
                                    objODBCDatareader.Close();

                                    msSQL = " insert into ocs_trn_tcadcovanantdocumentcheckdtls(" +
                                             " covenantdocumentcheckdtl_gid," +
                                             " application_gid," +
                                             " credit_gid, ";
                                    if (values.applicant_type == "Institution")
                                        msSQL += " companydocument_gid, ";
                                    else if (values.applicant_type == "Individual")
                                        msSQL += " individualdocument_gid, ";
                                    else
                                    {
                                        msSQL += " groupdocument_gid,";
                                    }
                                    msSQL += " documenttype_gid," +
                                        " documenttype_code," +
                                        " documenttype_name," +
                                        " covenant_type, " +
                                        " tagged_by, " +
                                        " tagged_name, " +
                                        " covenant_periods, " +
                                        " covenantperiod_updatedby, " +
                                        " covenantperiod_updateddate, " +
                                        " buffer_days, " +
                                        " bufferday_updatedby, " +
                                        " bufferday_updateddate, " +
                                        " created_date," +
                                        " created_by)" +
                                        " VALUES(" +
                                        "'" + msGetGID + "'," +
                                        "'" + lsApplication_gid + "'," +
                                        "'" + dt["institution_gid"].ToString() + "'," +
                                        "'" + Listitem + "'," +
                                        "'" + lsdocumenttype_gid + "'," +
                                        "'" + lsdocumenttype_name + "'," +
                                        "'" + lscompanydocument_name.Replace("'", "") + "'," +
                                        "'" + lscovenant_type + "'," +
                                        "'" + values.taggedby + "'," +
                                        "'" + tagged_name + "'," +
                                        "'" + covenant_periods + "'," +
                                        "'" + user_gid + "'," +
                                        "current_timestamp," +
                                        "'" + buffer_days + "'," +
                                        "'" + user_gid + "'," +
                                        "current_timestamp," +
                                        "current_timestamp," +
                                        "'" + user_gid + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    Isgroupnew = true;


                                    DaMstScannedDocument objvalues = new DaMstScannedDocument();
                                    objvalues.DaGroupDocChecklistinfoCredit(lsApplication_gid, dt["institution_gid"].ToString(), employee_gid);


                                }



                            }
                            dt_datatable.Dispose();

                        }

                    }
                    if (mnResult == 1)
                    {
                        values.status = true;
                        values.message = "Document type checklist applied successfully to all Members ";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "There is No Document type checklist to apply for Members";
                    }

                }

            }
            else if (values.applicant_type == "Individual")
            {
                //values.application_gid = "APPC20220628489";

                msSQL = " select stakeholder_type from ocs_trn_tcadcontact where contact_gid = '" + lsCredit_gid + "'";

                lsStackholdertype = objdbconn.GetExecuteScalar(msSQL);

                if (lsStackholdertype == "Guarantor")
                {
                    msSQL = " select contact_gid from ocs_trn_tcadcontact where application_gid = '" + lsApplication_gid + "'" +
                   " and  stakeholder_type = 'Guarantor'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {

                        msSQL = " select group_concat(distinct individualdocument_gid) " +
                                " from ocs_trn_tcadcovanantdocumentcheckdtls where  " +
                                " application_gid = '" + lsApplication_gid + "'" +
                                " and credit_gid = '" + lsCredit_gid + "' " +
                                " and ind_gur_flag = 'Y' and untagged_type is null";


                        DocList = objdbconn.GetExecuteScalar(msSQL);
                        DocumentList = DocList.Split(',').ToList();
                    };
                    if (DocList != "")
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            List<string> list = new List<string>();
                            List<string> toRemove = new List<string>();
                            List<string> FinalList = new List<string>();
                            List<string> CreditDocList = new List<string>();
                            CreditList = string.Empty;

                            msSQL = " select group_concat(distinct individualdocument_gid) " +
                                " from ocs_trn_tcadcovanantdocumentcheckdtls where  " +
                                " application_gid = '" + lsApplication_gid + "'" +
                                " and credit_gid = '" + dt["contact_gid"].ToString() + "'";

                            CreditList = objdbconn.GetExecuteScalar(msSQL);
                            if (CreditList.Any(",".Contains))
                            {
                                CreditDocList = CreditList.Split(',').ToList();
                            }
                            else { CreditDocList.Add(CreditList); }

                            list = DocumentList;
                            toRemove = CreditDocList;
                            FinalList = list.Except(toRemove).ToList();

                            if (FinalList != null)
                            {
                                foreach (string Listitem in FinalList)
                                {
                                    if (values.applicant_type == "Individual")
                                    {
                                        msGetGID = objcmnfunctions.GetMasterGID("CDCL");
                                        msSQL = " select individualdocument_gid,documenttypes_gid,documenttype_name,individualdocument_name,covenant_type " +
                                                    " from ocs_mst_tindividualdocument where individualdocument_gid='" + Listitem + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                            lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                            lscompanydocument_name = objODBCDatareader["individualdocument_name"].ToString();
                                            lscovenant_type = objODBCDatareader["covenant_type"].ToString();

                                        }
                                        objODBCDatareader.Close();
                                    }
                                    else if (values.applicant_type == "Group")
                                    {
                                        msGetGID = objcmnfunctions.GetMasterGID("CDCL");
                                        msSQL = " select groupdocument_gid,documenttypes_gid,documenttype_name,groupdocument_name,covenant_type " +
                                                    " from ocs_mst_tgroupdocument  where groupdocument_gid='" + Listitem + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                            lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                            lscompanydocument_name = objODBCDatareader["groupdocument_name"].ToString();
                                            lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                                        }
                                        objODBCDatareader.Close();
                                    }
                                    else
                                    {
                                        msGetGID = objcmnfunctions.GetMasterGID("CDCL");
                                        msSQL = " select companydocument_gid,documenttypes_gid,documenttype_name,companydocument_name,covenant_type " +
                                                    " from ocs_mst_tcompanydocument where companydocument_gid='" + Listitem + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                            lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                            lscompanydocument_name = objODBCDatareader["companydocument_name"].ToString();
                                            lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                                        }
                                        objODBCDatareader.Close();
                                    }

                                    msSQL = " select covenant_periods, buffer_days " +
                                            " from ocs_trn_tcadcovanantdocumentcheckdtls where " +
                                            " application_gid = '" + lsApplication_gid + "'  and " +
                                            " credit_gid = '" + lsCredit_gid + "' and " +
                                            " ind_gur_flag = 'Y' and individualdocument_gid = '" + Listitem + "'";

                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        covenant_periods = objODBCDatareader["covenant_periods"].ToString();
                                        buffer_days = objODBCDatareader["buffer_days"].ToString();

                                    }
                                    objODBCDatareader.Close();

                                    msSQL = " insert into ocs_trn_tcadcovanantdocumentcheckdtls(" +
                                             " covenantdocumentcheckdtl_gid," +
                                             " application_gid," +
                                             " credit_gid, ";
                                    if (values.applicant_type == "Institution")
                                        msSQL += " companydocument_gid, ";
                                    else if (values.applicant_type == "Individual")
                                        msSQL += " individualdocument_gid, ";
                                    else
                                    {
                                        msSQL += " groupdocument_gid,";
                                    }
                                    msSQL += " documenttype_gid," +
                                        " documenttype_code," +
                                        " documenttype_name," +
                                        " covenant_type, " +
                                        " tagged_by, " +
                                        " tagged_name, " +
                                        " covenant_periods, " +
                                        " covenantperiod_updatedby, " +
                                        " covenantperiod_updateddate, " +
                                        " buffer_days, " +
                                        " bufferday_updatedby, " +
                                        " bufferday_updateddate, " +
                                        " created_date," +
                                        " created_by)" +
                                        " VALUES(" +
                                        "'" + msGetGID + "'," +
                                        "'" + lsApplication_gid + "'," +
                                        "'" + dt["contact_gid"].ToString() + "'," +
                                        "'" + Listitem + "'," +
                                        "'" + lsdocumenttype_gid + "'," +
                                        "'" + lsdocumenttype_name + "'," +
                                        "'" + lscompanydocument_name.Replace("'", "") + "'," +
                                        "'" + lscovenant_type + "'," +
                                        "'" + values.taggedby + "'," +
                                        "'" + tagged_name + "'," +
                                        "'" + covenant_periods + "'," +
                                        "'" + user_gid + "'," +
                                        "current_timestamp," +
                                        "'" + buffer_days + "'," +
                                        "'" + user_gid + "'," +
                                        "current_timestamp," +
                                        "current_timestamp," +
                                        "'" + user_gid + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    Isgroupnew = true;


                                    DaMstScannedDocument objvalues = new DaMstScannedDocument();
                                    objvalues.DaGroupDocChecklistinfoCredit(lsApplication_gid, dt["contact_gid"].ToString(), employee_gid);


                                }

                            }
                            dt_datatable.Dispose();

                        }

                    }
                    if (mnResult == 1)
                    {
                        values.status = true;
                        values.message = "Document type checklist applied successfully to all Guarantors ";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "There is No Document type checklist to apply for Guarantors";
                    }

                }

                else if (lsStackholdertype == "Member")
                {
                    msSQL = " select contact_gid from ocs_trn_tcadcontact where application_gid = '" + lsApplication_gid + "'" +
                                       " and  stakeholder_type ='Member'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        msSQL = " select group_concat(distinct individualdocument_gid) " +
                                " from ocs_trn_tcadcovanantdocumentcheckdtls where  " +
                                " application_gid = '" + lsApplication_gid + "'" +
                                " and credit_gid = '" + lsCredit_gid + "' " +
                                " and ind_mem_flag = 'Y'";

                        DocList = objdbconn.GetExecuteScalar(msSQL);
                        DocumentList = DocList.Split(',').ToList();
                    };
                    if (DocList != "")
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            List<string> list = new List<string>();
                            List<string> toRemove = new List<string>();
                            List<string> FinalList = new List<string>();
                            List<string> CreditDocList = new List<string>();
                            CreditList = string.Empty;

                            msSQL = " select group_concat(distinct individualdocument_gid) " +
                                " from ocs_trn_tcadcovanantdocumentcheckdtls where  " +
                                " application_gid = '" + lsApplication_gid + "'" +
                                " and credit_gid = '" + dt["contact_gid"].ToString() + "'";

                            CreditList = objdbconn.GetExecuteScalar(msSQL);
                            if (CreditList.Any(",".Contains))
                            {
                                CreditDocList = CreditList.Split(',').ToList();
                            }
                            else { CreditDocList.Add(CreditList); }

                            list = DocumentList;
                            toRemove = CreditDocList;
                            FinalList = list.Except(toRemove).ToList();

                            if (FinalList != null)
                            {
                                foreach (string Listitem in FinalList)
                                {
                                    if (values.applicant_type == "Individual")
                                    {
                                        msGetGID = objcmnfunctions.GetMasterGID("CDCL");
                                        msSQL = " select individualdocument_gid,documenttypes_gid,documenttype_name,individualdocument_name,covenant_type " +
                                                    " from ocs_mst_tindividualdocument where individualdocument_gid='" + Listitem + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                            lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                            lscompanydocument_name = objODBCDatareader["individualdocument_name"].ToString();
                                            lscovenant_type = objODBCDatareader["covenant_type"].ToString();

                                        }
                                        objODBCDatareader.Close();
                                    }
                                    else if (values.applicant_type == "Group")
                                    {
                                        msGetGID = objcmnfunctions.GetMasterGID("CDCL");
                                        msSQL = " select groupdocument_gid,documenttypes_gid,documenttype_name,groupdocument_name,covenant_type " +
                                                    " from ocs_mst_tgroupdocument  where groupdocument_gid='" + Listitem + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                            lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                            lscompanydocument_name = objODBCDatareader["groupdocument_name"].ToString();
                                            lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                                        }
                                        objODBCDatareader.Close();
                                    }
                                    else
                                    {
                                        msGetGID = objcmnfunctions.GetMasterGID("CDCL");
                                        msSQL = " select companydocument_gid,documenttypes_gid,documenttype_name,companydocument_name,covenant_type " +
                                                    " from ocs_mst_tcompanydocument where companydocument_gid='" + Listitem + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                            lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                            lscompanydocument_name = objODBCDatareader["companydocument_name"].ToString();
                                            lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                                        }
                                        objODBCDatareader.Close();
                                    }

                                    msSQL = " select covenant_periods, buffer_days " +
                                            " from ocs_trn_tcadcovanantdocumentcheckdtls where " +
                                            " application_gid = '" + lsApplication_gid + "'  and " +
                                            " credit_gid = '" + lsCredit_gid + "' and " +
                                            " ind_mem_flag = 'Y' and individualdocument_gid = '" + Listitem + "'";

                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        covenant_periods = objODBCDatareader["covenant_periods"].ToString();
                                        buffer_days = objODBCDatareader["buffer_days"].ToString();

                                    }
                                    objODBCDatareader.Close();

                                    msSQL = " insert into ocs_trn_tcadcovanantdocumentcheckdtls(" +
                                             " covenantdocumentcheckdtl_gid," +
                                             " application_gid," +
                                             " credit_gid, ";
                                    if (values.applicant_type == "Institution")
                                        msSQL += " companydocument_gid, ";
                                    else if (values.applicant_type == "Individual")
                                        msSQL += " individualdocument_gid, ";
                                    else
                                    {
                                        msSQL += " groupdocument_gid,";
                                    }
                                    msSQL += " documenttype_gid," +
                                        " documenttype_code," +
                                        " documenttype_name," +
                                        " covenant_type, " +
                                        " tagged_by, " +
                                        " tagged_name, " +
                                        " covenant_periods, " +
                                        " covenantperiod_updatedby, " +
                                        " covenantperiod_updateddate, " +
                                        " buffer_days, " +
                                        " bufferday_updatedby, " +
                                        " bufferday_updateddate, " +
                                        " created_date," +
                                        " created_by)" +
                                        " VALUES(" +
                                        "'" + msGetGID + "'," +
                                        "'" + lsApplication_gid + "'," +
                                        "'" + dt["contact_gid"].ToString() + "'," +
                                        "'" + Listitem + "'," +
                                        "'" + lsdocumenttype_gid + "'," +
                                        "'" + lsdocumenttype_name + "'," +
                                        "'" + lscompanydocument_name.Replace("'", "") + "'," +
                                        "'" + lscovenant_type + "'," +
                                        "'" + values.taggedby + "'," +
                                        "'" + tagged_name + "'," +
                                        "'" + covenant_periods + "'," +
                                        "'" + user_gid + "'," +
                                        "current_timestamp," +
                                        "'" + buffer_days + "'," +
                                        "'" + user_gid + "'," +
                                        "current_timestamp," +
                                        "current_timestamp," +
                                        "'" + user_gid + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    Isgroupnew = true;


                                    DaMstScannedDocument objvalues = new DaMstScannedDocument();
                                    objvalues.DaGroupDocChecklistinfoCredit(lsApplication_gid, dt["contact_gid"].ToString(), employee_gid);


                                }
                            }
                            dt_datatable.Dispose();
                        }
                    }
                    if (mnResult == 1)
                    {
                        values.status = true;
                        values.message = "Document type checklist applied successfully to all Members ";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "There is No Document type checklist to apply for Members";
                    }
                }
            }


        }
        //Individual Document
        public bool DaGetIndividualTypeList(MdlMstCAD values, string credit_gid, string application_gid)
        {

            //string program_gid = "";

            //msSQL = " select program_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "' ";

            //program_gid = objdbconn.GetExecuteScalar(msSQL);

            //msSQL = " select a.individualdocument_gid, a.documenttypes_gid, a.documenttype_name, a.individualdocument_name, a.covenant_type " +
            //       " from ocs_mst_tindividualdocument a " +
            //       " left join ocs_mst_tindividualdocumentprogram b on a.individualdocument_gid = b.individualdocument_gid " +
            //       " where a.status='Y' and b.program_gid = '" + program_gid + "' and a.individualdocument_gid not in (select individualdocument_gid " +
            //       " from ocs_trn_tcaddocumentchecktls where credit_gid = '" + credit_gid + "' and untagged_type is null group by individualdocument_gid)";


            msSQL = " SELECT individualdocument_gid,individualdocument_name,documenttypes_gid,documenttype_name,covenant_type " +
                   " FROM ocs_mst_tindividualdocument where status='Y' and individualdocument_gid not in (select individualdocument_gid " +
                  " from ocs_trn_tcaddocumentchecktls where credit_gid = '" + credit_gid + "' and untagged_type is null group by individualdocument_gid)";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<CADDocument>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getDocList.Add(new CADDocument
                    {
                        document_gid = dt["individualdocument_gid"].ToString(),
                        documenttype_gid = dt["documenttypes_gid"].ToString(),
                        document_name = dt["individualdocument_name"].ToString(),
                        documenttype_name = dt["documenttype_name"].ToString(),
                        covenant_type = dt["covenant_type"].ToString(),
                    });
                }
                values.CADDocument = getDocList;
                values.status = true;
                values.message = "Data Fetched";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();

            return true;
        }


        //Group Document
        public bool DaGetGroupTypeList(MdlMstCAD values, string credit_gid)
        {
            msSQL = " SELECT groupdocument_gid,groupdocument_name,documenttypes_gid,documenttype_name,covenant_type " +
                   " FROM ocs_mst_tgroupdocument where status='Y' and delete_flag='N' and groupdocument_gid not in (select groupdocument_gid " +
                  " from ocs_trn_tcaddocumentchecktls where credit_gid = '" + credit_gid + "' and untagged_type is null  group by groupdocument_gid)";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<CADDocument>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getDocList.Add(new CADDocument
                    {
                        document_gid = dt["groupdocument_gid"].ToString(),
                        documenttype_gid = dt["documenttypes_gid"].ToString(),
                        document_name = dt["groupdocument_name"].ToString(),
                        documenttype_name = dt["documenttype_name"].ToString(),
                        covenant_type = dt["covenant_type"].ToString(),
                    });
                }
                values.CADDocument = getDocList;
                values.status = true;
                values.message = "Data Fetched";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();

            return true;
        }


        public bool DaGetDocumentTypeList(string credit_gid, string application_gid, MdlMstCADCompany values)
        {
            //string program_gid = "";

            //msSQL = " select program_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "' ";

            //program_gid = objdbconn.GetExecuteScalar(msSQL);

            //msSQL = " select a.companydocument_gid,a.documenttypes_gid,a.documenttype_name,a.companydocument_name,a.covenant_type " +
            //        " from ocs_mst_tcompanydocument a " +
            //        " left join ocs_mst_tcompanydocumentprogram b on a.companydocument_gid = b.companydocument_gid " +
            //        " where a.status='Y' and b.program_gid = '" + program_gid + "' " +
            //        " and a.companydocument_gid not in (select companydocument_gid " +
            //        " from ocs_trn_tcaddocumentchecktls where credit_gid = '" + credit_gid + "' and untagged_type is null  group by companydocument_gid)";

            msSQL = " SELECT companydocument_gid,companydocument_name,documenttypes_gid,documenttype_name,covenant_type " +
                   " FROM ocs_mst_tcompanydocument where status='Y' and companydocument_gid not in (select companydocument_gid " +
                   " from ocs_trn_tcaddocumentchecktls where credit_gid = '" + credit_gid + "' and untagged_type is null group by companydocument_gid)";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<CADDocument>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getDocList.Add(new CADDocument
                    {
                        document_gid = dt["companydocument_gid"].ToString(),
                        documenttype_gid = dt["documenttypes_gid"].ToString(),
                        document_name = dt["companydocument_name"].ToString(),
                        documenttype_name = dt["documenttype_name"].ToString(),
                        covenant_type = dt["covenant_type"].ToString(),
                    });
                }
                values.CADDocument = getDocList;
                values.status = true;
                values.message = "Data Fetched";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetCADTrnTaggedDocList(MdlMstCAD values, string credit_gid)
        {
            msSQL = "select institution_gid from ocs_trn_tcadinstitution where institution_gid='" + credit_gid + "'";
            string lsinstitution = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select contact_gid from ocs_trn_tcadcontact where contact_gid='" + credit_gid + "'";
            string lsindividual = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select group_gid from ocs_trn_tcadgroup where group_gid='" + credit_gid + "'";
            string lsgroup = objdbconn.GetExecuteScalar(msSQL);

            if (lsinstitution != "")
            {
                msSQL = " SELECT groupdocumentchecklist_gid, documentcheckdtl_gid,documenttype_gid,documenttype_code,a.documenttype_name,a.tagged_by, " +
                        " a.tagged_name, a.covenant_type,application_gid, " +
                        " case when (select  overall_docstatus from ocs_trn_tcadgroupdocumentchecklist " +
                        " where groupdocumentchecklist_gid = a.groupdocumentchecklist_gid and mstdocument_gid=a.companydocument_gid) is null and " +
                        " (select  physicaloverall_docstatus from ocs_trn_tcadgroupdocumentchecklist " +
                        " where groupdocumentchecklist_gid = a.groupdocumentchecklist_gid and mstdocument_gid=a.companydocument_gid) is null and " +
                        " (SELECT COUNT(*) FROM ocs_trn_tphysicaldocument y WHERE y.groupdocumentchecklist_gid = a.groupdocumentchecklist_gid) = '0' and " +
                        " (SELECT COUNT(*) FROM ocs_trn_tscanneddocument y WHERE y.groupdocumentchecklist_gid = a.groupdocumentchecklist_gid " +
                        " and signeddocument_flag = 'Y') = '0' then 'true' else 'false' end as document_status " +
                        " FROM ocs_trn_tcaddocumentchecktls a" +
                        " WHERE credit_gid = '" + credit_gid + "' and a.untagged_type is null or a.untagged_type = 'N' GROUP BY a.companydocument_gid";
            }
            else if (lsindividual != "")
            {

                msSQL = " SELECT groupdocumentchecklist_gid, documentcheckdtl_gid,documenttype_gid,documenttype_code,a.documenttype_name,a.tagged_by, " +
                       " a.tagged_name, a.covenant_type,application_gid, " +
                        " case when (select  overall_docstatus from ocs_trn_tcadgroupdocumentchecklist " +
                        " where groupdocumentchecklist_gid = a.groupdocumentchecklist_gid and mstdocument_gid=a.individualdocument_gid) is null and " +
                        " (select  physicaloverall_docstatus from ocs_trn_tcadgroupdocumentchecklist " +
                        " where groupdocumentchecklist_gid = a.groupdocumentchecklist_gid and mstdocument_gid=a.individualdocument_gid) is null and " +
                        " (SELECT COUNT(*) FROM ocs_trn_tphysicaldocument y WHERE y.groupdocumentchecklist_gid = a.groupdocumentchecklist_gid) = '0' and " +
                        " (SELECT COUNT(*) FROM ocs_trn_tscanneddocument y WHERE y.groupdocumentchecklist_gid = a.groupdocumentchecklist_gid " +
                        " and signeddocument_flag = 'Y') = '0' then 'true' else 'false' end as document_status " +
                       " FROM ocs_trn_tcaddocumentchecktls a" +
                       " WHERE credit_gid = '" + credit_gid + "' and a.untagged_type is null or a.untagged_type = 'N' GROUP BY a.individualdocument_gid";
            }
            else if (lsgroup != "")
            {
                msSQL = " SELECT groupdocumentchecklist_gid, documentcheckdtl_gid,documenttype_gid,documenttype_code,a.documenttype_name,a.tagged_by, " +
                       " a.tagged_name, a.covenant_type,application_gid, " +
                        " case when (select  overall_docstatus from ocs_trn_tcadgroupdocumentchecklist " +
                        " where groupdocumentchecklist_gid = a.groupdocumentchecklist_gid and mstdocument_gid=a.groupdocument_gid) is null and " +
                        " (select  physicaloverall_docstatus from ocs_trn_tcadgroupdocumentchecklist " +
                        " where groupdocumentchecklist_gid = a.groupdocumentchecklist_gid and mstdocument_gid=a.groupdocument_gid) is null and " +
                        " (SELECT COUNT(*) FROM ocs_trn_tphysicaldocument y WHERE y.groupdocumentchecklist_gid = a.groupdocumentchecklist_gid) = '0' and " +
                        " (SELECT COUNT(*) FROM ocs_trn_tscanneddocument y WHERE y.groupdocumentchecklist_gid = a.groupdocumentchecklist_gid " +
                        " and signeddocument_flag = 'Y') = '0' then 'true' else 'false' end as document_status " +
                       " FROM ocs_trn_tcaddocumentchecktls a" +
                       " WHERE credit_gid = '" + credit_gid + "' and a.untagged_type is null or a.untagged_type = 'N' GROUP BY a.groupdocument_gid";
            }
            dt_datatable = objdbconn.GetDataTable(msSQL);

            if (dt_datatable.Rows.Count != 0)
            {
                values.TaggedDocument = dt_datatable.AsEnumerable().Select(row => new TaggedDocument
                {
                    groupdocumentchecklist_gid = row["groupdocumentchecklist_gid"].ToString(),
                    documenttype_gid = row["documenttype_gid"].ToString(),
                    documenttype_code = row["documenttype_code"].ToString(),
                    documenttype_name = row["documenttype_name"].ToString(),
                    covenant_type = row["covenant_type"].ToString(),
                    taggedby = row["tagged_by"].ToString(),
                    tagged_name = row["tagged_name"].ToString(),
                    document_status = row["document_status"].ToString(),
                }).ToList();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaGetCADTaggedDocList(MdlMstCAD values, string credit_gid)
        {
            msSQL = "select institution_gid from ocs_trn_tcadinstitution where institution_gid='" + credit_gid + "'";
            string lsinstitution = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select contact_gid from ocs_trn_tcadcontact where contact_gid='" + credit_gid + "'";
            string lsindividual = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select group_gid from ocs_trn_tcadgroup where group_gid='" + credit_gid + "'";
            string lsgroup = objdbconn.GetExecuteScalar(msSQL);

            if (lsinstitution != "")
            {
                msSQL = " SELECT groupdocumentchecklist_gid,documentcheckdtl_gid,documenttype_gid,documenttype_code,a.documenttype_name, " +
                       " a.tagged_by, b.covenant_type,application_gid FROM ocs_trn_tcaddocumentchecktls a " +
                       " left join ocs_mst_tcompanydocument b on a.companydocument_gid = b.companydocument_gid " +
                       " WHERE credit_gid = '" + credit_gid + "' and a.untagged_type is null or a.untagged_type = 'N' GROUP BY a.companydocument_gid";
            }
            else if (lsindividual != "")
            {
                msSQL = " SELECT groupdocumentchecklist_gid,documentcheckdtl_gid,documenttype_gid,documenttype_code,a.documenttype_name, " +
                     " a.tagged_by, b.covenant_type,application_gid FROM ocs_trn_tcaddocumentchecktls a " +
                     " left join ocs_mst_tindividualdocument b on a.individualdocument_gid = b.individualdocument_gid " +
                     " WHERE credit_gid = '" + credit_gid + "' and a.untagged_type is null or a.untagged_type = 'N' GROUP BY a.individualdocument_gid";

            }
            else if (lsgroup != "")
            {
                msSQL = " SELECT groupdocumentchecklist_gid,documentcheckdtl_gid,documenttype_gid,documenttype_code,a.documenttype_name, " +
                  " a.tagged_by, b.covenant_type,application_gid FROM ocs_trn_tcaddocumentchecktls a " +
                  " left join ocs_mst_tgroupdocument b on a.groupdocument_gid = b.groupdocument_gid " +
                  " WHERE credit_gid = '" + credit_gid + "' and a.untagged_type is null or a.untagged_type = 'N' GROUP BY a.groupdocument_gid";

            }
            dt_datatable = objdbconn.GetDataTable(msSQL);

            if (dt_datatable.Rows.Count != 0)
            {
                values.TaggedDocument = dt_datatable.AsEnumerable().Select(row => new TaggedDocument
                {
                    groupdocumentchecklist_gid = row["groupdocumentchecklist_gid"].ToString(),
                    documenttype_gid = row["documenttype_gid"].ToString(),
                    documenttype_code = row["documenttype_code"].ToString(),
                    documenttype_name = row["documenttype_name"].ToString(),
                    // documenttype_count = row["documenttype_count"].ToString(),
                    covenant_type = row["covenant_type"].ToString(),
                    taggedby = row["tagged_by"].ToString(),
                }).ToList();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();
            return true;
        }

        public void DaUnTagDocument(string documentcheckdtl_gid, result objResult, string user_gid)
        {


            msSQL = "select groupdocumentchecklist_gid from ocs_trn_tcadgroupdocumentchecklist where groupdocumentchecklist_gid='" + documentcheckdtl_gid + "'";
            string lsdeferral = objdbconn.GetExecuteScalar(msSQL);
            if (lsdeferral != "")
            {
                msSQL = " update ocs_trn_tcaddocumentchecktls set untagged_type='Y', " +
                         " com_gur_flag= 'N',ind_gur_flag='N',com_mem_flag='N',ind_mem_flag='N'," +
                         " untagged_by = '" + user_gid + "', " +
                     " untagged_date=current_timestamp" +
                     " where groupdocumentchecklist_gid='" + documentcheckdtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_trn_tcadgroupdocumentchecklist set untagged_type='Y' where groupdocumentchecklist_gid='" + documentcheckdtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = " update ocs_trn_tcadcovanantdocumentcheckdtls set untagged_type='Y', " +
                     " com_gur_flag= 'N',ind_gur_flag='N',com_mem_flag='N',ind_mem_flag='N'," +
                    " untagged_by='" + user_gid + "', " +
                    " untagged_date=current_timestamp" +
                    " where groupcovdocumentchecklist_gid='" + documentcheckdtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_trn_tcadgroupcovenantdocumentchecklist set untagged_type='Y' where groupcovdocumentchecklist_gid='" + documentcheckdtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            //msSQL = "select covenantdocumentcheckdtl_gid from ocs_trn_tcadcovanantdocumentcheckdtls where covenantdocumentcheckdtl_gid='" + documentcheckdtl_gid + "'";
            //string lscovenant = objdbconn.GetExecuteScalar(msSQL);
            //if (lscovenant != "")
            //    msSQL = "DELETE FROM ocs_trn_tcadcovanantdocumentcheckdtls WHERE covenantdocumentcheckdtl_gid='" + documentcheckdtl_gid + "'";
            //else
            //    msSQL = "DELETE FROM ocs_trn_tcaddocumentchecktls WHERE documentcheckdtl_gid='" + documentcheckdtl_gid + "'";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //msSQL = "select institution2documentupload_gid from ocs_trn_tcadinstitution2documentupload where institution2documentupload_gid='" + documentcheckdtl_gid + "'";
            //string lsinstitution = objdbconn.GetExecuteScalar(msSQL);
            //msSQL = "select contact2document_gid from ocs_trn_tcadcontact2document where contact2document_gid='" + documentcheckdtl_gid + "'";
            //string lsindividual = objdbconn.GetExecuteScalar(msSQL);
            //msSQL = "select group2document_gid from ocs_mst_tgroup2document where group2document_gid='" + documentcheckdtl_gid + "'";
            //string lsgroup = objdbconn.GetExecuteScalar(msSQL);
            //if (lsinstitution != "")
            //{

            //}
            //else if (lsindividual != "")
            //{
            //    msSQL = " update ocs_trn_tcadcontact2document set untagged_type='Y', " +
            //       " untagged_by='" + user_gid + "', " +
            //       " untagged_date=current_timestamp" +
            //       " where contact2document_gid='" + documentcheckdtl_gid + "'";
            //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //}
            //else if (lsgroup != "")
            //{
            //    msSQL = " update ocs_mst_tgroup2document set untagged_type='Y', " +
            //       " untagged_by='" + user_gid + "', " +
            //       " untagged_date=current_timestamp" +
            //       " where group2document_gid='" + documentcheckdtl_gid + "'";
            //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //}
            //else
            //{
            //    msSQL = "DELETE FROM ocs_trn_tcaddocumentchecktls WHERE documentcheckdtl_gid='" + documentcheckdtl_gid + "'";
            //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //}
            if (mnResult == 1)
            {
                objResult.status = true;
                objResult.message = "Document Type UnTagged Successfully..!";
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";
            }
        }

        public void DaPostDocumentCheckList(MdlMstCAD values, string user_gid, string employee_gid)
        {
            string lsStackholdertype;
            msSQL = " select stakeholder_type from ocs_trn_tcadinstitution where institution_gid = '" + values.credit_gid + "'";

            lsStackholdertype = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select institution_gid from ocs_trn_tcadinstitution where institution_gid='" + values.credit_gid + "'";
            string lsinstitution = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select contact_gid from ocs_trn_tcadcontact where contact_gid='" + values.credit_gid + "'";
            string lsindividual = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select group_gid from ocs_trn_tcadgroup where group_gid='" + values.credit_gid + "'";
            string lsgroup = objdbconn.GetExecuteScalar(msSQL);
            bool lsgroupupdate = false;

            foreach (string i in values.document_gid)
            {
                string lscovenant_type = "", lscompanydocument_gid = "", lsindividualdocument_gid = "", lsgroupdocument_gid = "";

                msGetGID = objcmnfunctions.GetMasterGID("DOCG");
                if (lsinstitution != "")
                {
                    msSQL = " select companydocument_gid,documenttypes_gid,documenttype_name,companydocument_name,covenant_type " +
                            " from ocs_mst_tcompanydocument where companydocument_gid='" + i + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                        lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                        lscompanydocument_name = objODBCDatareader["companydocument_name"].ToString();
                        lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                        lscompanydocument_gid = objODBCDatareader["companydocument_gid"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = " select groupdocumentchecklist_gid from ocs_trn_tcaddocumentchecktls where application_gid='" + values.application_gid + "'" +
                      " and credit_gid='" + values.credit_gid + "' and companydocument_gid ='" + lscompanydocument_gid + "'";
                }
                else if (lsindividual != "")
                {
                    msSQL = " select individualdocument_gid,documenttypes_gid,documenttype_name,individualdocument_name,covenant_type " +
                          " from ocs_mst_tindividualdocument where individualdocument_gid='" + i + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                        lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                        lscompanydocument_name = objODBCDatareader["individualdocument_name"].ToString();
                        lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                        lsindividualdocument_gid = objODBCDatareader["individualdocument_gid"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = " select groupdocumentchecklist_gid from ocs_trn_tcaddocumentchecktls where application_gid='" + values.application_gid + "'" +
                      " and credit_gid='" + values.credit_gid + "' and individualdocument_gid ='" + lsindividualdocument_gid + "'";
                }
                else if (lsgroup != "")
                {
                    msSQL = " select groupdocument_gid,documenttypes_gid,documenttype_name,groupdocument_name,covenant_type " +
                         " from ocs_mst_tgroupdocument where groupdocument_gid='" + i + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                        lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                        lscompanydocument_name = objODBCDatareader["groupdocument_name"].ToString();
                        lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                        lsgroupdocument_gid = objODBCDatareader["groupdocument_gid"].ToString();
                    }
                    objODBCDatareader.Close();
                    msSQL = " select groupdocumentchecklist_gid from ocs_trn_tcaddocumentchecktls where application_gid='" + values.application_gid + "'" +
                      " and credit_gid='" + values.credit_gid + "' and groupdocument_gid ='" + lsgroupdocument_gid + "'";
                }
                string lsgroupdocumentchecklist_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = "select concat(user_firstname,' ', user_lastname, ' / ', user_code) as username from adm_mst_tuser where user_gid='" + user_gid + "'";
                string tagged_name = objdbconn.GetExecuteScalar(msSQL);
                if (lsgroupdocumentchecklist_gid != "")
                {
                    msSQL = " update ocs_trn_tcaddocumentchecktls set untagged_type=null, " +
                   " untagged_by='" + user_gid + "', " +
                   " tagged_name='" + tagged_name + "'," +
                   " tagged_by='" + values.taggedby + "'," +
                   " untagged_date=current_timestamp" +
                   " where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_tcadgroupdocumentchecklist set untagged_type=null, " +
                            " tagged_by = '" + values.taggedby + "' where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (lsStackholdertype == "Guarantor")
                    {
                        msSQL = "update ocs_trn_tcaddocumentchecktls set com_gur_flag = 'Y' " +
                           " where groupdocumentchecklist_gid = '" + lsgroupdocumentchecklist_gid + "'";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                    if (lsStackholdertype == "Member")
                    {
                        msSQL = "update ocs_trn_tcaddocumentchecktls set com_mem_flag = 'Y' " +
                           " where groupdocumentchecklist_gid = '" + lsgroupdocumentchecklist_gid + "'";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                }
                else
                {
                    msSQL = " insert into ocs_trn_tcaddocumentchecktls(" +
                   " documentcheckdtl_gid," +
                   " application_gid," +
                   " credit_gid, ";
                    if (lsinstitution != "")
                        msSQL += " companydocument_gid, ";
                    else if (lsindividual != "")
                        msSQL += " individualdocument_gid, ";
                    else
                    {
                        msSQL += " groupdocument_gid,";
                    }
                    msSQL += " documenttype_gid," +
                        " documenttype_code," +
                        " documenttype_name," +
                        " covenant_type, " +
                        " tagged_by, " +
                        " tagged_name, " +
                        " created_date," +
                        " created_by)" +
                        " VALUES(" +
                        "'" + msGetGID + "'," +
                        "'" + values.application_gid + "'," +
                        "'" + values.credit_gid + "',";
                    if (lsinstitution != "")
                        msSQL += "'" + lscompanydocument_gid + "',";
                    else if (lsindividual != "")
                        msSQL += "'" + lsindividualdocument_gid + "',";
                    else
                    {
                        msSQL += "'" + lsgroupdocument_gid + "',";
                    }
                    msSQL += "'" + lsdocumenttype_gid + "'," +
                        "'" + lsdocumenttype_name + "'," +
                        "'" + lscompanydocument_name.Replace("'", "") + "'," +
                        "'" + lscovenant_type + "'," +
                        "'" + values.taggedby + "'," +
                        "'" + tagged_name + "'," +
                        "current_timestamp," +
                        "'" + user_gid + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    lsgroupupdate = true;

                    if (lsStackholdertype == "Guarantor")
                    {
                        msSQL = "update ocs_trn_tcaddocumentchecktls set com_gur_flag = 'Y' " +
                           " where application_gid = '" + values.application_gid + "' and " +
                           " credit_gid = '" + values.credit_gid + "' and documentcheckdtl_gid = '" + msGetGID + "'";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                    if (lsStackholdertype == "Member")
                    {
                        msSQL = "update ocs_trn_tcaddocumentchecktls set com_mem_flag = 'Y' " +
                           " where application_gid = '" + values.application_gid + "' and " +
                           " credit_gid = '" + values.credit_gid + "' and documentcheckdtl_gid = '" + msGetGID + "'";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
            }
            if (lsgroupupdate)
            {
                DaCADCreditAction objvalues = new DaCADCreditAction();
                objvalues.DaCADGroupDocChecklistinfo(values.application_gid, values.credit_gid, employee_gid);
            }
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Document Type Tagged Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        public void DaCheckALLDocumentList(MdlMstCAD values, string user_gid)
        {
            if (values.applicant_type == "Institution")
            {
                msSQL = " select institution_gid from ocs_trn_tcadinstitution where application_gid = '" + values.application_gid + "' and institution_gid<> '" + values.credit_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msSQL = " select documenttype_gid, documenttype_code, documenttype_name from ocs_trn_tcaddocumentchecktls where credit_gid='" + values.credit_gid + "'";
                        dt_child = objdbconn.GetDataTable(msSQL);
                        if (dt_child.Rows.Count != 0)
                        {
                            foreach (DataRow dt1 in dt_child.Rows)
                            {
                                msGetGID = objcmnfunctions.GetMasterGID("DOCG");

                                msSQL = " insert into ocs_trn_tcaddocumentchecktls(" +
                                        " documentcheckdtl_gid," +
                                        " application_gid," +
                                        " credit_gid," +
                                        " documenttype_gid," +
                                        " documenttype_code," +
                                        " documenttype_name," +
                                        " created_date," +
                                        " created_by)" +
                                        " VALUES(" +
                                        "'" + msGetGID + "'," +
                                        "'" + values.application_gid + "'," +
                                        "'" + dt["institution_gid"].ToString() + "'," +
                                        "'" + dt1["documenttype_gid"].ToString() + "'," +
                                        "'" + dt1["documenttype_code"].ToString() + "'," +
                                        "'" + dt1["documenttype_name"].ToString() + "'," +
                                        "current_timestamp," +
                                        "'" + user_gid + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                        }
                        dt_child.Dispose();
                    }
                    if (mnResult == 1)
                    {
                        values.status = true;
                        values.message = "Document Type Tagged Successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured";
                    }
                }
                dt_datatable.Dispose();
            }
            else if (values.applicant_type == "Individual")
            {
                msSQL = " select contact_gid from ocs_trn_tcadcontact where application_gid = '" + values.application_gid + "' and contact_gid<> '" + values.credit_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msSQL = " select documenttype_gid, documenttype_code, documenttype_name from ocs_trn_tcaddocumentchecktls where credit_gid='" + values.credit_gid + "'";
                        dt_child = objdbconn.GetDataTable(msSQL);
                        if (dt_child.Rows.Count != 0)
                        {
                            foreach (DataRow dt1 in dt_child.Rows)
                            {
                                msGetGID = objcmnfunctions.GetMasterGID("DOCG");

                                msSQL = " insert into ocs_trn_tcaddocumentchecktls(" +
                                        " documentcheckdtl_gid," +
                                        " application_gid," +
                                        " credit_gid," +
                                        " documenttype_gid," +
                                        " documenttype_code," +
                                        " documenttype_name," +
                                        " created_date," +
                                        " created_by)" +
                                        " VALUES(" +
                                        "'" + msGetGID + "'," +
                                        "'" + values.application_gid + "'," +
                                        "'" + dt["contact_gid"].ToString() + "'," +
                                        "'" + dt1["documenttype_gid"].ToString() + "'," +
                                        "'" + dt1["documenttype_code"].ToString() + "'," +
                                        "'" + dt1["documenttype_name"].ToString() + "'," +
                                        "current_timestamp," +
                                        "'" + user_gid + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                        }
                        dt_child.Dispose();
                    }
                    if (mnResult == 1)
                    {
                        values.status = true;
                        values.message = "Document Type Tagged Successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured";
                    }
                }
                dt_datatable.Dispose();
            }
            else
            {
                msSQL = " select group_gid from ocs_trn_tcadgroup where application_gid = '" + values.application_gid + "' and group_gid<> '" + values.credit_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msSQL = " select documenttype_gid, documenttype_code, documenttype_name from ocs_trn_tcaddocumentchecktls where credit_gid='" + values.credit_gid + "'";
                        dt_child = objdbconn.GetDataTable(msSQL);
                        if (dt_child.Rows.Count != 0)
                        {
                            foreach (DataRow dt1 in dt_child.Rows)
                            {
                                msGetGID = objcmnfunctions.GetMasterGID("DOCG");

                                msSQL = " insert into ocs_trn_tcaddocumentchecktls(" +
                                        " documentcheckdtl_gid," +
                                        " application_gid," +
                                        " credit_gid," +
                                        " documenttype_gid," +
                                        " documenttype_code," +
                                        " documenttype_name," +
                                        " created_date," +
                                        " created_by)" +
                                        " VALUES(" +
                                        "'" + msGetGID + "'," +
                                        "'" + values.application_gid + "'," +
                                        "'" + dt["group_gid"].ToString() + "'," +
                                        "'" + dt1["documenttype_gid"].ToString() + "'," +
                                        "'" + dt1["documenttype_code"].ToString() + "'," +
                                        "'" + dt1["documenttype_name"].ToString() + "'," +
                                        "current_timestamp," +
                                        "'" + user_gid + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                        }
                        dt_child.Dispose();
                    }
                    if (mnResult == 1)
                    {
                        values.status = true;
                        values.message = "Document Type Tagged Successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured";
                    }
                }
                dt_datatable.Dispose();
            }
        }


        //Covenant

        public bool DaGetCovenantDocumentTypeList(MdlMstCADCompany values, string credit_gid, string application_gid)
        {

            //string program_gid = "";

            //msSQL = " select program_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "' ";

            //program_gid = objdbconn.GetExecuteScalar(msSQL);


            if (credit_gid != "")
            {


                //msSQL = " select a.companydocument_gid,a.documenttypes_gid,a.documenttype_name,a.companydocument_name,a.covenant_type " +
                //   " from ocs_mst_tcompanydocument a " +
                //   " left join ocs_mst_tcompanydocumentprogram b on a.companydocument_gid = b.companydocument_gid " +
                //   " where a.status='Y' and b.program_gid = '" + program_gid + "' and a.covenant_type = 'Y' " +
                //     " and a.companydocument_gid not in (select companydocument_gid " +
                //     " from ocs_trn_tcadcovanantdocumentcheckdtls where credit_gid = '" + credit_gid + "' and untagged_type is null  group by companydocument_gid)";


                msSQL = " SELECT companydocument_gid,companydocument_name,documenttypes_gid,documenttype_name,covenant_type " +
                        " FROM ocs_mst_tcompanydocument where status='Y' and covenant_type='Y' and companydocument_gid not in (select companydocument_gid " +
                        " from ocs_trn_tcadcovanantdocumentcheckdtls where credit_gid = '" + credit_gid + "' and untagged_type is null group by companydocument_gid)";
            }
            else
            {
                msSQL = " SELECT companydocument_gid,companydocument_name,documenttypes_gid,documenttype_name,covenant_type " +
                        " FROM ocs_mst_tcompanydocument where status='Y' and covenant_type='Y'";

                //msSQL = " select a.companydocument_gid,a.documenttypes_gid,a.documenttype_name,a.companydocument_name,a.covenant_type " +
                // " from ocs_mst_tcompanydocument a " +
                // " left join ocs_mst_tcompanydocumentprogram b on a.companydocument_gid = b.companydocument_gid " +
                // " where a.status='Y' and b.program_gid = '" + program_gid + "' and a.covenant_type = 'Y' ";


            }

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<CADDocument>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getDocList.Add(new CADDocument
                    {
                        document_gid = dt["companydocument_gid"].ToString(),
                        documenttype_gid = dt["documenttypes_gid"].ToString(),
                        document_name = dt["companydocument_name"].ToString(),
                        documenttype_name = dt["documenttype_name"].ToString(),
                        covenant_type = dt["covenant_type"].ToString(),
                    });
                }
                values.CADDocument = getDocList;
                values.status = true;
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetCADTrnCovenantTaggedDocList(MdlMstCAD values, string credit_gid)
        {
            msSQL = "select institution_gid from ocs_trn_tcadinstitution where institution_gid='" + credit_gid + "'";
            string lsinstitution = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select contact_gid from ocs_trn_tcadcontact where contact_gid='" + credit_gid + "'";
            string lsindividual = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select group_gid from ocs_trn_tcadgroup where group_gid='" + credit_gid + "'";
            string lsgroup = objdbconn.GetExecuteScalar(msSQL);

            if (lsinstitution != "")
            {
                msSQL = " SELECT groupcovdocumentchecklist_gid,a.companydocument_gid,documenttype_gid,documenttype_code,a.documenttype_name,a.covenant_periods, " +
                        " a.covenant_type,a.buffer_days,application_gid,a.tagged_name, a.tagged_by as taggedby, " +
                        " case when (select  overall_docstatus from ocs_trn_tcadgroupcovenantdocumentchecklist " +
                        " where groupcovdocumentchecklist_gid = a.groupcovdocumentchecklist_gid and mstdocument_gid=a.companydocument_gid) is null and " +
                        " (select  physicaloverall_docstatus from ocs_trn_tcadgroupcovenantdocumentchecklist " +
                        " where groupcovdocumentchecklist_gid = a.groupcovdocumentchecklist_gid and mstdocument_gid=a.companydocument_gid) is null and " +
                        " (SELECT COUNT(*) FROM ocs_trn_tphysicaldocument y WHERE y.groupdocumentchecklist_gid = a.groupcovdocumentchecklist_gid) = '0' and " +
                        " (SELECT COUNT(*) FROM ocs_trn_tscanneddocument y WHERE y.groupdocumentchecklist_gid = a.groupcovdocumentchecklist_gid " +
                        " and signeddocument_flag = 'Y') = '0' then 'true' else 'false' end as document_status " +
                        " FROM ocs_trn_tcadcovanantdocumentcheckdtls a" +
                        " WHERE credit_gid = '" + credit_gid + "' and a.covenant_type='Y' and a.untagged_type is null or a.untagged_type = 'N'" +
                        " GROUP BY a.companydocument_gid";
            }
            else if (lsindividual != "")
            {
                msSQL = " SELECT groupcovdocumentchecklist_gid,a.individualdocument_gid as companydocument_gid,documenttype_gid,documenttype_code, " +
                        " a.documenttype_name,a.covenant_periods, a.covenant_type,a.buffer_days,application_gid,a.tagged_name, a.tagged_by as taggedby, " +
                        " case when (select  overall_docstatus from ocs_trn_tcadgroupcovenantdocumentchecklist " +
                        " where groupcovdocumentchecklist_gid = a.groupcovdocumentchecklist_gid and mstdocument_gid=a.companydocument_gid) is null and " +
                        " (select  physicaloverall_docstatus from ocs_trn_tcadgroupcovenantdocumentchecklist " +
                        " where groupcovdocumentchecklist_gid = a.groupcovdocumentchecklist_gid and mstdocument_gid=a.companydocument_gid) is null and " +
                        " (SELECT COUNT(*) FROM ocs_trn_tphysicaldocument y WHERE y.groupdocumentchecklist_gid = a.groupcovdocumentchecklist_gid) = '0' and " +
                        " (SELECT COUNT(*) FROM ocs_trn_tscanneddocument y WHERE y.groupdocumentchecklist_gid = a.groupcovdocumentchecklist_gid " +
                        " and signeddocument_flag = 'Y') = '0' then 'true' else 'false' end as document_status " +
                       " FROM ocs_trn_tcadcovanantdocumentcheckdtls a" +
                       " WHERE credit_gid = '" + credit_gid + "' and a.covenant_type='Y' and a.untagged_type is null or a.untagged_type = 'N'" +
                       " GROUP BY a.individualdocument_gid";
            }
            else if (lsgroup != "")
            {
                msSQL = " SELECT groupcovdocumentchecklist_gid,a.groupdocument_gid as companydocument_gid,documenttype_gid,documenttype_code, " +
                      " a.documenttype_name,a.covenant_periods, a.covenant_type,a.buffer_days,application_gid,a.tagged_name, a.tagged_by as taggedby, " +
                        " case when (select  overall_docstatus from ocs_trn_tcadgroupcovenantdocumentchecklist " +
                        " where groupcovdocumentchecklist_gid = a.groupcovdocumentchecklist_gid and mstdocument_gid=a.companydocument_gid) is null and " +
                        " (select  physicaloverall_docstatus from ocs_trn_tcadgroupcovenantdocumentchecklist " +
                        " where groupcovdocumentchecklist_gid = a.groupcovdocumentchecklist_gid and mstdocument_gid=a.companydocument_gid) is null and " +
                        " (SELECT COUNT(*) FROM ocs_trn_tphysicaldocument y WHERE y.groupdocumentchecklist_gid = a.groupcovdocumentchecklist_gid) = '0' and " +
                        " (SELECT COUNT(*) FROM ocs_trn_tscanneddocument y WHERE y.groupdocumentchecklist_gid = a.groupcovdocumentchecklist_gid " +
                        " and signeddocument_flag = 'Y') = '0' then 'true' else 'false' end as document_status " +
                     " FROM ocs_trn_tcadcovanantdocumentcheckdtls a" +
                     " WHERE credit_gid = '" + credit_gid + "' and a.covenant_type='Y' and a.untagged_type is null or a.untagged_type = 'N'" +
                     " GROUP BY a.groupdocument_gid";
            }
            dt_datatable = objdbconn.GetDataTable(msSQL);

            if (dt_datatable.Rows.Count != 0)
            {
                values.TaggedDocument = dt_datatable.AsEnumerable().Select(row => new TaggedDocument
                {
                    groupcovdocumentchecklist_gid = row["groupcovdocumentchecklist_gid"].ToString(),
                    documenttype_gid = row["documenttype_gid"].ToString(),
                    documenttype_code = row["documenttype_code"].ToString(),
                    documenttype_name = row["documenttype_name"].ToString(),
                    covenant_type = row["covenant_type"].ToString(),
                    companydocument_gid = row["companydocument_gid"].ToString(),
                    taggedby = row["taggedby"].ToString(),
                    covenantperiod = row["covenant_periods"].ToString(),
                    tagged_name = row["tagged_name"].ToString(),
                    document_status = row["document_status"].ToString(),
                    buffer_days = row["buffer_days"].ToString(),
                }).ToList();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaGetCADCovenantTaggedDocList(MdlMstCAD values, string credit_gid)
        {
            msSQL = "select institution_gid from ocs_trn_tcadinstitution where institution_gid='" + credit_gid + "'";
            string lsinstitution = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select contact_gid from ocs_trn_tcadcontact where contact_gid='" + credit_gid + "'";
            string lsindividual = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select group_gid from ocs_trn_tcadgroup where group_gid='" + credit_gid + "'";
            string lsgroup = objdbconn.GetExecuteScalar(msSQL);

            //msSQL = "select institution2documentupload_gid from ocs_trn_tcadinstitution2documentupload where institution_gid='" + credit_gid + "'";
            //string lsinstitution = objdbconn.GetExecuteScalar(msSQL);
            //msSQL = "select contact2document_gid from ocs_trn_tcadcontact2document where contact_gid='" + credit_gid + "'";
            //string lsindividual = objdbconn.GetExecuteScalar(msSQL);
            //msSQL = "select group2document_gid from ocs_mst_tgroup2document where group_gid='" + credit_gid + "'";
            //string lsgroup = objdbconn.GetExecuteScalar(msSQL);


            if (lsinstitution != "")
            {
                msSQL = " SELECT groupcovdocumentchecklist_gid,covenantdocumentcheckdtl_gid,a.companydocument_gid,documenttype_gid, " +
                    " documenttype_code,a.documenttype_name,a.covenant_periods, b.covenant_type, application_gid, a.buffer_days,a.tagged_by as taggedby  " +
                    " FROM ocs_trn_tcadcovanantdocumentcheckdtls a" +
                    " left join ocs_mst_tcompanydocument b on a.companydocument_gid = b.companydocument_gid " +
                    " WHERE credit_gid = '" + credit_gid + "' and b.covenant_type='Y' and a.untagged_type is null or a.untagged_type = 'N' " +
                    " GROUP BY a.companydocument_gid";
            }
            else if (lsindividual != "")
            {
                msSQL = " SELECT groupcovdocumentchecklist_gid,covenantdocumentcheckdtl_gid,a.individualdocument_gid as companydocument_gid,documenttype_gid, " +
                    " documenttype_code,a.documenttype_name,a.covenant_periods, b.covenant_type, application_gid,a.buffer_days, a.tagged_by as taggedby  " +
                    " FROM ocs_trn_tcadcovanantdocumentcheckdtls a" +
                    " left join ocs_mst_tindividualdocument b on a.individualdocument_gid = b.individualdocument_gid " +
                    " WHERE credit_gid = '" + credit_gid + "' and b.covenant_type='Y' and a.untagged_type is null or a.untagged_type = 'N' " +
                    " GROUP BY a.individualdocument_gid";
            }
            else if (lsgroup != "")
            {
                msSQL = " SELECT groupcovdocumentchecklist_gid,covenantdocumentcheckdtl_gid,a.groupdocument_gid as companydocument_gid,documenttype_gid, " +
                    " documenttype_code,a.documenttype_name,a.covenant_periods, b.covenant_type, application_gid,a.buffer_days, a.tagged_by as taggedby  " +
                    " FROM ocs_trn_tcadcovanantdocumentcheckdtls a" +
                    " left join ocs_mst_tgroupdocument b on a.groupdocument_gid = b.groupdocument_gid " +
                    " WHERE credit_gid = '" + credit_gid + "' and b.covenant_type='Y' and a.untagged_type is null or a.untagged_type = 'N' " +
                    " GROUP BY a.groupdocument_gid";
            }
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.TaggedDocument = dt_datatable.AsEnumerable().Select(row => new TaggedDocument
                {
                    groupcovdocumentchecklist_gid = row["groupcovdocumentchecklist_gid"].ToString(),
                    documenttype_gid = row["documenttype_gid"].ToString(),
                    documenttype_code = row["documenttype_code"].ToString(),
                    documenttype_name = row["documenttype_name"].ToString(),
                    covenant_type = row["covenant_type"].ToString(),
                    companydocument_gid = row["companydocument_gid"].ToString(),
                    taggedby = row["taggedby"].ToString(),
                    covenantperiod = row["covenant_periods"].ToString(),
                    buffer_days = row["buffer_days"].ToString(),
                }).ToList();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();
            return true;
        }

        public void DaPostCovenantPeriods(MdlCovenantPeriodlist values, result objResult, string user_gid, string employee_gid)
        {
            string lsStackholdertype;
            List<CovenantPeriod> TaggedDocumentChecklist = values.CovenantPeriod.Where(a => a.covenantchecked == true).ToList();
            List<CovenantPeriod> UnTaggedDocumentChecklist = values.CovenantPeriod.Where(a => a.covenantchecked == false).ToList();

            List<CovenantPeriod> caddocchecklist = new List<CovenantPeriod>();
            List<CovenantPeriod> newlyaddedchecklist = new List<CovenantPeriod>();
            List<CovenantPeriod> untaggedcaddocchecklist = new List<CovenantPeriod>();

            caddocchecklist = TaggedDocumentChecklist.Where(a => a.groupcovdocumentchecklist_gid != "" && a.groupcovdocumentchecklist_gid != null).ToList();
            newlyaddedchecklist = TaggedDocumentChecklist.Where(a => a.groupcovdocumentchecklist_gid == "" || a.groupcovdocumentchecklist_gid == null).ToList();
            untaggedcaddocchecklist = UnTaggedDocumentChecklist.Where(a => a.groupcovdocumentchecklist_gid != "" && a.groupcovdocumentchecklist_gid != null).ToList();
            bool Isgroupnew = false;
           

                foreach (CovenantPeriod i in caddocchecklist)
            {
                if ((i.buffer_days == "--Select Buffer Days--") || (i.buffer_days == null) || (i.buffer_days == "") || (i.buffer_days == "undefined") || (i.covenantperiod == "--Select Covenant Periods--") || (i.covenantperiod == null) || (i.covenantperiod == "") || (i.covenantperiod == "undefined"))
                {
                    objResult.status = false;
                    objResult.message = "Kindly Select the Covenant Periods & Buffer Days for the selected Document..!";
                    return;
                }
                
            }
            if (caddocchecklist.Count > 0)
            {
                foreach (CovenantPeriod i in caddocchecklist)
                {
                    msSQL = " update ocs_trn_tcadgroupcovenantdocumentchecklist set covenant_periods='" + i.covenantperiod + "', " +
                            " buffer_days = '" + i.buffer_days + "', " +
                           " covenantperiod_updatedby='" + user_gid + "', " +
                           " bufferday_updatedby='" + user_gid + "', " +
                           " untagged_type=null," +
                           " untagged_type=null," +
                           " covenantperiod_updateddate=current_timestamp," +
                           " bufferday_updateddate=current_timestamp" +
                           " where groupcovdocumentchecklist_gid='" + i.groupcovdocumentchecklist_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_tcadcovanantdocumentcheckdtls set covenant_periods='" + i.covenantperiod + "', " +
                            " buffer_days = '" + i.buffer_days + "', " +
                            " covenantperiod_updatedby='" + user_gid + "', " +
                            " bufferday_updatedby='" + user_gid + "', " +
                            " untagged_type=null," +
                            " covenantperiod_updateddate=current_timestamp," +
                            " bufferday_updateddate=current_timestamp" +
                            " where groupcovdocumentchecklist_gid='" + i.groupcovdocumentchecklist_gid + "'";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (values.CovenantPeriod[0].lstype == "Institution")
                    {
                        msSQL = " select stakeholder_type from ocs_trn_tcadinstitution where institution_gid = '" + i.credit_gid + "'";

                        lsStackholdertype = objdbconn.GetExecuteScalar(msSQL);

                        if (lsStackholdertype == "Guarantor")
                        {
                            msSQL = "update ocs_trn_tcadcovanantdocumentcheckdtls set com_gur_flag = 'Y' " +
                                    " where  " +
                                    " groupcovdocumentchecklist_gid='" + i.groupcovdocumentchecklist_gid + "'";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                        if (lsStackholdertype == "Member")
                        {
                            msSQL = "update ocs_trn_tcadcovanantdocumentcheckdtls set com_mem_flag = 'Y' " +
                               " where  " +
                               "  groupcovdocumentchecklist_gid='" + i.groupcovdocumentchecklist_gid + "'";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                    }
                    if (values.CovenantPeriod[0].lstype == "Individual")
                    {
                        msSQL = " select stakeholder_type from ocs_trn_tcadcontact where contact_gid = '" + i.credit_gid + "'";

                        lsStackholdertype = objdbconn.GetExecuteScalar(msSQL);

                        if (lsStackholdertype == "Guarantor")
                        {
                            msSQL = "update ocs_trn_tcadcovanantdocumentcheckdtls set ind_gur_flag = 'Y' " +
                                " where  " +
                               "  groupcovdocumentchecklist_gid='" + i.groupcovdocumentchecklist_gid + "'";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                        if (lsStackholdertype == "Member")
                        {
                            msSQL = "update ocs_trn_tcadcovanantdocumentcheckdtls set ind_mem_flag = 'Y' " +
                                 " where  " +
                               "  groupcovdocumentchecklist_gid='" + i.groupcovdocumentchecklist_gid + "'";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                }
            }
            if (untaggedcaddocchecklist.Count > 0)
            {
                string[] getid = untaggedcaddocchecklist.Where(p => p.groupcovdocumentchecklist_gid != "").Select(p => p.groupcovdocumentchecklist_gid.ToString()).ToArray();
                var getdocumentid = DaGetvalueswithComma(getid);
                //msSQL = "DELETE FROM ocs_trn_tcadcovanantdocumentcheckdtls WHERE covenantdocumentcheckdtl_gid in (" + getdocumentid + ")";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = " UPDATE ocs_trn_tcadgroupcovenantdocumentchecklist SET covenant_periods = '', " +
                         " buffer_days = '', " +
                        " untagged_type = 'Y', " +
                        " covenantperiod_updatedby = '" + user_gid + "', " +
                        " bufferday_updatedby='" + user_gid + "', " +
                        " covenantperiod_updateddate = current_timestamp() , " +
                        " bufferday_updateddate=current_timestamp()" +
                        " WHERE groupcovdocumentchecklist_gid in (" + getdocumentid + ")";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " UPDATE ocs_trn_tcadcovanantdocumentcheckdtls SET covenant_periods = '', " +
                       " buffer_days = '', " +
                      " untagged_type = 'Y', " +
                      " untagged_by='" + user_gid + "', " +
                      " untagged_date=current_timestamp," +
                      " bufferday_updatedby='" + user_gid + "', " +
                      " covenantperiod_updatedby = '" + user_gid + "', " +
                      " covenantperiod_updateddate = current_timestamp(), " +
                      " bufferday_updateddate=current_timestamp()" +
                      " WHERE groupcovdocumentchecklist_gid in (" + getdocumentid + ")";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            string lscovenant_type = "";
            if (newlyaddedchecklist.Count > 0)
            {
                foreach (CovenantPeriod i in newlyaddedchecklist)
                {
                    msSQL = "select concat(user_firstname,' ', user_lastname, ' / ', user_code) as username from adm_mst_tuser where user_gid='" + user_gid + "'";
                    string tagged_name = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select groupcovdocumentchecklist_gid from ocs_trn_tcadgroupcovenantdocumentchecklist " +
                            " where mstdocument_gid='" + i.companydocument_gid + "' and credit_gid='" + i.credit_gid + "'";
                    string lsgroupcovdocumentchecklist_gid = objdbconn.GetExecuteScalar(msSQL);
                    if (lsgroupcovdocumentchecklist_gid != "")
                    {
                        msSQL = " update ocs_trn_tcadgroupcovenantdocumentchecklist set covenant_periods='" + i.covenantperiod + "', " +
                            " buffer_days = '" + i.buffer_days + "', " +
                          " covenantperiod_updatedby='" + user_gid + "', " +
                          " bufferday_updatedby='" + user_gid + "', " +
                          " untagged_type=null," +
                          " tagged_by='" + values.taggedby + "'," +
                          " covenantperiod_updateddate=current_timestamp," +
                          " bufferday_updateddate=current_timestamp" +
                          " where groupcovdocumentchecklist_gid='" + lsgroupcovdocumentchecklist_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tcadcovanantdocumentcheckdtls set covenant_periods='" + i.covenantperiod + "', " +
                                " buffer_days = '" + i.buffer_days + "', " +
                                " covenantperiod_updatedby='" + user_gid + "', " +
                                " bufferday_updatedby='" + user_gid + "', " +
                                " untagged_type=null," +
                                " tagged_by='" + values.taggedby + "'," +
                                " tagged_name='" + tagged_name + "'," +
                                " covenantperiod_updateddate=current_timestamp," +
                                " bufferday_updateddate=current_timestamp" +
                                " where groupcovdocumentchecklist_gid='" + lsgroupcovdocumentchecklist_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {
                        if (values.CovenantPeriod[0].lstype == "Individual")
                        {
                            msGetGID = objcmnfunctions.GetMasterGID("CDCL");
                            msSQL = " select individualdocument_gid,documenttypes_gid,documenttype_name,individualdocument_name,covenant_type " +
                                        " from ocs_mst_tindividualdocument where individualdocument_gid='" + i.companydocument_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                lscompanydocument_name = objODBCDatareader["individualdocument_name"].ToString();
                                lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                            }
                            objODBCDatareader.Close();
                        }
                        else if (values.CovenantPeriod[0].lstype == "Group")
                        {
                            msGetGID = objcmnfunctions.GetMasterGID("CDCL");
                            msSQL = " select groupdocument_gid,documenttypes_gid,documenttype_name,groupdocument_name,covenant_type " +
                                        " from ocs_mst_tgroupdocument  where groupdocument_gid='" + i.companydocument_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                lscompanydocument_name = objODBCDatareader["groupdocument_name"].ToString();
                                lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                            }
                            objODBCDatareader.Close();
                        }
                        else
                        {
                            msGetGID = objcmnfunctions.GetMasterGID("CDCL");
                            msSQL = " select companydocument_gid,documenttypes_gid,documenttype_name,companydocument_name,covenant_type " +
                                        " from ocs_mst_tcompanydocument where companydocument_gid='" + i.companydocument_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                                lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                                lscompanydocument_name = objODBCDatareader["companydocument_name"].ToString();
                                lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                            }
                            objODBCDatareader.Close();
                        }



                        msSQL = " insert into ocs_trn_tcadcovanantdocumentcheckdtls(" +
                                " covenantdocumentcheckdtl_gid," +
                                " application_gid," +
                                " credit_gid, ";
                        if (values.CovenantPeriod[0].lstype == "Institution")
                            msSQL += " companydocument_gid, ";
                        else if (values.CovenantPeriod[0].lstype == "Individual")
                            msSQL += " individualdocument_gid, ";
                        else
                        {
                            msSQL += " groupdocument_gid,";
                        }
                        msSQL += " documenttype_gid," +
                            " documenttype_code," +
                            " documenttype_name," +
                            " covenant_type, " +
                            " tagged_by, " +
                            " tagged_name, " +
                            " covenant_periods, " +
                            " covenantperiod_updatedby, " +
                            " covenantperiod_updateddate, " +
                            " buffer_days, " +
                            " bufferday_updatedby, " +
                            " bufferday_updateddate, " +
                            " created_date," +
                            " created_by)" +
                            " VALUES(" +
                            "'" + msGetGID + "'," +
                            "'" + i.application_gid + "'," +
                            "'" + i.credit_gid + "'," +
                            "'" + i.companydocument_gid + "'," +
                            "'" + lsdocumenttype_gid + "'," +
                            "'" + lsdocumenttype_name + "'," +
                            "'" + lscompanydocument_name.Replace("'", "") + "'," +
                            "'" + lscovenant_type + "'," +
                            "'" + values.taggedby + "'," +
                            "'" + tagged_name + "'," +
                            "'" + i.covenantperiod + "'," +
                            "'" + user_gid + "'," +
                            "current_timestamp," +
                             "'" + i.buffer_days + "'," +
                            "'" + user_gid + "'," +
                            "current_timestamp," +
                            "current_timestamp," +
                            "'" + user_gid + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        Isgroupnew = true;
                      //  string lsStackholdertype;
                        if (values.CovenantPeriod[0].lstype == "Institution")
                        {
                            msSQL = " select stakeholder_type from ocs_trn_tcadinstitution where institution_gid = '" + i.credit_gid + "'";

                            lsStackholdertype = objdbconn.GetExecuteScalar(msSQL);

                            if (lsStackholdertype == "Guarantor")
                            {
                                msSQL = "update ocs_trn_tcadcovanantdocumentcheckdtls set com_gur_flag = 'Y' " +
                                   " where application_gid = '" + i.application_gid + "' and " +
                                   " credit_gid = '" + i.credit_gid + "' and covenantdocumentcheckdtl_gid = '" + msGetGID + "'";

                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }

                            if (lsStackholdertype == "Member")
                            {
                                msSQL = "update ocs_trn_tcadcovanantdocumentcheckdtls set com_mem_flag = 'Y' " +
                                   " where application_gid = '" + i.application_gid + "' and " +
                                   " credit_gid = '" + i.credit_gid + "' and covenantdocumentcheckdtl_gid = '" + msGetGID + "'";

                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                        }
                        else if (values.CovenantPeriod[0].lstype == "Individual")
                        {
                            msSQL = " select stakeholder_type from ocs_trn_tcadcontact where contact_gid = '" + i.credit_gid + "'";

                            lsStackholdertype = objdbconn.GetExecuteScalar(msSQL);

                            if (lsStackholdertype == "Guarantor")
                            {
                                msSQL = "update ocs_trn_tcadcovanantdocumentcheckdtls set ind_gur_flag = 'Y' " +
                                   " where application_gid = '" + i.application_gid + "' and " +
                                   " credit_gid = '" + i.credit_gid + "' and covenantdocumentcheckdtl_gid = '" + msGetGID + "'";

                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }

                            if (lsStackholdertype == "Member")
                            {
                                msSQL = "update ocs_trn_tcadcovanantdocumentcheckdtls set ind_mem_flag = 'Y' " +
                                   " where application_gid = '" + i.application_gid + "' and " +
                                   " credit_gid = '" + i.credit_gid + "' and covenantdocumentcheckdtl_gid = '" + msGetGID + "'";

                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }

                        }
                    }
                }
                if (Isgroupnew)
                {
                    DaCADCreditAction objvalues = new DaCADCreditAction();
                    objvalues.DaCADGroupDocChecklistinfo(newlyaddedchecklist[0].application_gid, newlyaddedchecklist[0].credit_gid, employee_gid);
                }
            }

            if (mnResult == 1)
            {
                objResult.status = true;
                objResult.message = "Covenant Periods are Updated Successfully..!";
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured..!";
            }
        }

        public string DaGetvalueswithComma(string[] array)
        {
            var value = "";
            foreach (var h in array)
            {
                value += "'" + h + "',";
            }
            value = value.TrimEnd(',');
            return value;
        }
        //Bureau

        public void DaGetInstitutionBureauTempClear(string employee_gid, result values)
        {
            msSQL = "delete from ocs_trn_tcadinstitution2cicdocumentupload where institution2bureau_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }

        public void DaGetInstitutionBureauList(string institution_gid, MdlInstitutionBureau values)
        {
            msSQL = "select institution2bureau_gid,bureauname_name,bureau_score,date_format(bureauscore_date, '%d-%m-%Y') as bureauscore_date from ocs_trn_tcadinstitution2bureau where " +
              " institution_gid='" + institution_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getinstitutionbureau_list = new List<institutionbureau_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getinstitutionbureau_list.Add(new institutionbureau_list
                    {
                        institution2bureau_gid = (dr_datarow["institution2bureau_gid"].ToString()),
                        bureauname_name = (dr_datarow["bureauname_name"].ToString()),
                        bureau_score = (dr_datarow["bureau_score"].ToString()),
                        bureauscore_date = (dr_datarow["bureauscore_date"].ToString()),
                    });
                }
            }
            values.institutionbureau_list = getinstitutionbureau_list;
            dt_datatable.Dispose();
        }

        public void DaCICUploadInstitutionDocList(string institution2bureau_gid, string employee_gid, MdlCICInstitution values)
        {
            msSQL = " select institution2cicdocumentupload_gid, institution2bureau_gid,cicdocument_name,cicdocument_path,document_content,migration_flag from ocs_trn_tcadinstitution2cicdocumentupload " +
                                 " where institution2bureau_gid='" + institution2bureau_gid + "' or institution2bureau_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<cicuploaddoc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new cicuploaddoc_list
                    {
                        document_name = dt["cicdocument_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["cicdocument_path"].ToString()),
                        institution2bureau_gid = dt["institution2bureau_gid"].ToString(),
                        tmpcicdocument_gid = dt["institution2cicdocumentupload_gid"].ToString(),
                        document_content = dt["document_content"].ToString(),
                        migration_flag = dt["migration_flag"].ToString(),
                    });
                    values.cicuploaddoc_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public bool DaPostCICUploadInstitution(string employee_gid, MdlCICInstitution values)
        {
            // Document Attachments
            msSQL = "select document_name from ocs_tmp_tcadcicdocument where created_by='" + employee_gid + "'";
            lsdocument_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select document_path from ocs_tmp_tcadcicdocument where created_by='" + employee_gid + "'";
            lsdocument_path = objdbconn.GetExecuteScalar(msSQL);

            msGetGid = objcmnfunctions.GetMasterGID("I2BR");
            msSQL = " insert into ocs_trn_tcadinstitution2bureau(" +
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
                   "'" + values.institution_gid + "'," +
                   "'" + values.bureauname_gid + "'," +
                   "'" + values.bureauname_name + "'," +
                   "'" + values.bureau_score + "',";

            if (values.bureauscore_date == null || values.bureauscore_date == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.bureauscore_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }


            msSQL += "'" + values.bureau_response.Replace("'", "") + "',";

            if (values.observations == null || values.observations == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.observations.Replace("'", "") + "',";
            }


            msSQL += "'" + employee_gid + "'," +
                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                msSQL = "update ocs_trn_tcadinstitution2cicdocumentupload set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_trn_tcadinstitution2cicdocumentupload set institution2bureau_gid='" + msGetGid + "' where institution2bureau_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                values.status = true;
                values.message = "Bureau Updates Added for Institution Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }

            if(mnResult != 0)
            {
                msSQL = "update ocs_trn_tcadinstitution2cicdocumentupload set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Bureau Updates Uploaded for Institution Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }

        }

        public void DaDeleteInstitutionBureau(string institution2bureau_gid, MdlInstitutionBureau values)
        {
            msSQL = "delete from ocs_trn_tcadinstitution2bureau where institution2bureau_gid='" + institution2bureau_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Bureau Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public bool DaCICInstitutionDocumentUpload(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/CICUploadInstitutionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                        string FileExtension = httpPostedFile.FileName;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        Stream ls_readStream;
                        ls_readStream = httpPostedFile.InputStream;
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);
                        //lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "Master/CICUploadInstitutionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        //FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                        //ms.WriteTo(file);
                        //file.Close();
                        //ms.Close();

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/CICUploadInstitutionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();

                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/CICUploadInstitutionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("INCU");
                        msSQL = " insert into ocs_trn_tcadinstitution2cicdocumentupload( " +
                                    " institution2cicdocumentupload_gid, " +
                                    " institution_gid," +
                                    " institution2bureau_gid," +
                                    " cicdocument_name," +
                                    " cicdocument_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension.Replace("'", "") + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            objfilename.status = true;
                            objfilename.message = "Document Uploaded Successfully..!";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured..!";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }
            return true;
        }

        public void DaCICUploadInstitutionDocDelete(string tmpcicdocument_gid, MdlCICIndividual values)
        {
            msSQL = " delete from ocs_trn_tcadinstitution2cicdocumentupload where institution2cicdocumentupload_gid='" + tmpcicdocument_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Document Deleted Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaCICInstitutionEdit(string institution2bureau_gid, MdlCICInstitution values)
        {
            try
            {
                msSQL = " select institution2bureau_gid, bureauname_gid,bureauname_name, bureau_score,date_format(bureauscore_date,'%d-%m-%Y') as bureauscore_date, " +
                    " observations, bureau_response" +
                        " from ocs_trn_tcadinstitution2bureau where institution2bureau_gid='" + institution2bureau_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.institution2bureau_gid = objODBCDatareader["institution2bureau_gid"].ToString();
                    values.bureauname_gid = objODBCDatareader["bureauname_gid"].ToString();
                    values.bureauname_name = objODBCDatareader["bureauname_name"].ToString();
                    values.bureau_score = objODBCDatareader["bureau_score"].ToString();
                    values.observations = objODBCDatareader["observations"].ToString();
                    values.bureau_response = objODBCDatareader["bureau_response"].ToString();
                    values.bureauscore_date = objODBCDatareader["bureauscore_date"].ToString();
                    if (objODBCDatareader["bureauscore_date"].ToString() != "")
                    {
                        values.bureauscoredate_edit = Convert.ToDateTime(objODBCDatareader["bureauscore_date"].ToString());
                    }
                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public bool DaUpdateCICUploadInstitution(string employee_gid, MdlCICInstitution values)
        {


            msSQL = " update ocs_trn_tcadinstitution2bureau set " +
                       " bureauname_gid='" + values.bureauname_gid + "'," +
                       " bureauname_name='" + values.bureauname_name.Replace("'", "") + "'," +
                       " bureau_score='" + values.bureau_score.Replace("'", "") + "',";

            if (Convert.ToDateTime(values.bureauscoredate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += " bureauscore_date='" + Convert.ToDateTime(values.bureauscoredate).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
            }

            if ((values.observations == null) || (values.observations == ""))
            {
                msSQL += "observations=null,";
            }
            else
            {
                msSQL += " observations='" + values.observations.Replace("'", "") + "',";
            }

           
        msSQL +=       " bureau_response='" + values.bureau_response.Replace("'", "") + "'," +
                       " updated_by='" + employee_gid + "'," +
                       " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                       " where institution2bureau_gid='" + values.institution2bureau_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                msSQL = "update ocs_trn_tcadinstitution2cicdocumentupload set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_trn_tcadinstitution2cicdocumentupload set institution2bureau_gid='" + values.institution2bureau_gid + "' where institution2bureau_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                values.status = true;
                values.message = "Bureau Details Updated for Institution Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }

        }


        //Genetic Code
        public void DaGetGeneticCodeList(string credit_gid, string employee_gid, MdlMstCUWGeneticCode values)
        {
            msSQL = " select creditgeneticcode_gid,geneticcode_gid,geneticcode_name,geneticcode_status,geneticcode_remarks,application_gid, credit_gid, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                    " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as updated_by " +
                    " from ocs_trn_tcadcreditgeneticcode a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                    " where credit_gid = '" + credit_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getgeneticcode_list = new List<mstcuwgeneticcode_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getgeneticcode_list.Add(new mstcuwgeneticcode_list
                    {
                        creditgeneticcode_gid = (dr_datarow["creditgeneticcode_gid"].ToString()),
                        geneticcode_gid = (dr_datarow["geneticcode_gid"].ToString()),
                        geneticcode_name = (dr_datarow["geneticcode_name"].ToString()),
                        geneticcode_status = (dr_datarow["geneticcode_status"].ToString()),
                        geneticcode_remarks = (dr_datarow["geneticcode_remarks"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        credit_gid = (dr_datarow["credit_gid"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        updated_by = (dr_datarow["updated_by"].ToString()),
                        updated_date = (dr_datarow["updated_date"].ToString()),
                    });
                }
                values.mstcuwgeneticcode_list = getgeneticcode_list;
            }
            dt_datatable.Dispose();
        }

        public void DaPostGeneticCode(string employee_gid, MdlMstCUWGeneticCode values)
        {
            msSQL = "select geneticcode_gid from ocs_trn_tcadcreditgeneticcode where application_gid='" + values.application_gid + "' and geneticcode_gid='" + values.geneticcode_gid + "'";
            string lsgenetic_code = objdbconn.GetExecuteScalar(msSQL);
            if (lsgenetic_code == (values.geneticcode_gid))
            {
                values.status = false;
                values.message = "Already Genetic Code Added";
                return;
            }
            msGetGid = objcmnfunctions.GetMasterGID("CRGN");
            msSQL = " insert into ocs_trn_tcadcreditgeneticcode(" +
                   " creditgeneticcode_gid," +
                   " credit_gid," +
                   " application_gid," +
                   " geneticcode_gid," +
                   " geneticcode_name," +
                   " geneticcode_status," +
                   " geneticcode_remarks," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.credit_gid + "'," +
                   "'" + values.application_gid + "'," +
                   "'" + values.geneticcode_gid + "'," +
                   "'" + values.geneticcode_name.Replace("'", " ") + "'," +
                   "'" + values.geneticcode_status + "'," +
                   "'" + values.geneticcode_remarks.Replace("'", " ") + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Genetic Code Details Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Adding Genetic Code";
            }
        }

        public void DaEditGeneticCode(string creditgeneticcode_gid, MdlMstCUWGeneticCode values)
        {
            try
            {
                msSQL = " select creditgeneticcode_gid,geneticcode_gid,geneticcode_name,geneticcode_status,geneticcode_remarks, credit_gid" +
                    " from ocs_trn_tcadcreditgeneticcode where creditgeneticcode_gid='" + creditgeneticcode_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.creditgeneticcode_gid = objODBCDatareader["creditgeneticcode_gid"].ToString();
                    values.geneticcode_gid = objODBCDatareader["geneticcode_gid"].ToString();
                    values.geneticcode_name = objODBCDatareader["geneticcode_name"].ToString();
                    values.geneticcode_status = objODBCDatareader["geneticcode_status"].ToString();
                    values.geneticcode_remarks = objODBCDatareader["geneticcode_remarks"].ToString();
                    values.credit_gid = objODBCDatareader["credit_gid"].ToString();
                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaUpdateGeneticCode(string employee_gid, MdlMstCUWGeneticCode values)
        {
            msSQL = " select creditgeneticcode_gid,geneticcode_gid,geneticcode_name,geneticcode_status,geneticcode_remarks, credit_gid, application_gid" +
                   " from ocs_trn_tcadcreditgeneticcode where creditgeneticcode_gid='" + values.creditgeneticcode_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lscreditgeneticcode_gid = objODBCDatareader["creditgeneticcode_gid"].ToString();
                lsgeneticcode_gid = objODBCDatareader["geneticcode_gid"].ToString();
                lsgeneticcode_name = objODBCDatareader["geneticcode_name"].ToString();
                lsgeneticcode_status = objODBCDatareader["geneticcode_status"].ToString();
                lsgeneticcode_remarks = objODBCDatareader["geneticcode_remarks"].ToString();
                lscredit_gid = objODBCDatareader["credit_gid"].ToString();
                lsapplication_gid = objODBCDatareader["application_gid"].ToString();
            }

            msSQL = " update ocs_trn_tcadcreditgeneticcode set " +
                     " geneticcode_status='" + values.geneticcode_status + "'," +
                     " geneticcode_remarks='" + values.geneticcode_remarks.Replace("'", " ") + "'," +
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where creditgeneticcode_gid='" + values.creditgeneticcode_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                msGetGid = objcmnfunctions.GetMasterGID("GCUL");

                msSQL = "Insert into ocs_trn_tcadcreditgeneticcodeupdatelog(" +
               " credit2geneticcodeupdatelog_gid, " +
               " creditgeneticcode_gid, " +
               " credit_gid, " +
               " application_gid, " +
               " geneticcode_gid, " +
               " geneticcode_name," +
               " geneticcode_status," +
               " geneticcode_remarks," +
               " created_by," +
               " created_date)" +
               " values (" +
               "'" + msGetGid + "'," +
               "'" + values.creditgeneticcode_gid + "'," +
               "'" + lscredit_gid + "'," +
               "'" + lsapplication_gid + "'," +
               "'" + lsgeneticcode_gid + "'," +
               "'" + lsgeneticcode_name + "'," +
               "'" + lsgeneticcode_status + "'," +
               "'" + lsgeneticcode_remarks.Replace("'", " ") + "'," +
               "'" + employee_gid + "'," +
               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Genetic Code Details Updated Successfully";
            }
        }

        public void DaDeleteGeneticCode(string creditgeneticcode_gid, MdlMstCUWGeneticCode values, string employee_gid)
        {
            msSQL = "delete from ocs_trn_tcadcreditgeneticcode where creditgeneticcode_gid='" + creditgeneticcode_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from ocs_trn_tcadcreditgeneticcodeupdatelog where creditgeneticcode_gid='" + creditgeneticcode_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Genetic Code details Deleted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Deleting Genetic Code";
            }
        }

        //Economic Capital

        public void DaEditSocialAndTradeCapital(string credit_gid, string applicant_type, MdlMstAppCreditUnderWriting values)
        {
            try
            {
                if (applicant_type == "Institution")
                {
                    msSQL = " select social_capital, trade_capital, economical_flag from ocs_trn_tcadinstitution where institution_gid='" + credit_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.social_capital = objODBCDatareader["social_capital"].ToString();
                        values.trade_capital = objODBCDatareader["trade_capital"].ToString();
                        values.economical_flag = objODBCDatareader["economical_flag"].ToString();
                    }
                    objODBCDatareader.Close();
                }
                else if (applicant_type == "Individual")
                {
                    msSQL = " select social_capital, trade_capital,economical_flag from ocs_trn_tcadcontact where contact_gid='" + credit_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.social_capital = objODBCDatareader["social_capital"].ToString();
                        values.trade_capital = objODBCDatareader["trade_capital"].ToString();
                        values.economical_flag = objODBCDatareader["economical_flag"].ToString();
                    }
                    objODBCDatareader.Close();
                }
                else if (applicant_type == "Group")
                {
                    msSQL = " select social_capital, trade_capital, economical_flag from ocs_trn_tcadgroup where group_gid='" + credit_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.social_capital = objODBCDatareader["social_capital"].ToString();
                        values.trade_capital = objODBCDatareader["trade_capital"].ToString();
                        values.economical_flag = objODBCDatareader["economical_flag"].ToString();
                    }
                    objODBCDatareader.Close();
                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaGetSocialAndTradeCapital(string credit_gid, string applicant_type, MdlMstAppCreditUnderWriting values)
        {
            if (applicant_type == "Institution")
            {
                msSQL = " SELECT date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, " +
                        " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                        " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as updated_by " +
                        " FROM ocs_trn_tcadinstitution a left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
                        " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                        " where a.institution_gid = '" + credit_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getinstitutionsocialtrade_list = new List<institutionsocialtrade_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getinstitutionsocialtrade_list.Add(new institutionsocialtrade_list
                        {
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),

                        });
                    }
                    values.institutionsocialtrade_list = getinstitutionsocialtrade_list;
                }
                dt_datatable.Dispose();
            }
            if (applicant_type == "Individual")
            {
                msSQL = " SELECT date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, " +
                        " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                        " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as updated_by " +
                        " FROM ocs_trn_tcadcontact a left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
                        " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                        " where a.contact_gid = '" + credit_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getindividualsocialtrade_list = new List<individualsocialtrade_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getindividualsocialtrade_list.Add(new individualsocialtrade_list
                        {
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),

                        });
                    }
                    values.individualsocialtrade_list = getindividualsocialtrade_list;
                }
                dt_datatable.Dispose();
            }
            if (applicant_type == "Group")
            {
                msSQL = " SELECT date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, " +
                        " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                        " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as updated_by " +
                        " FROM ocs_trn_tcadgroup a left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
                        " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                        " where a.group_gid = '" + credit_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getgroupsocialtrade_list = new List<groupsocialtrade_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getgroupsocialtrade_list.Add(new groupsocialtrade_list
                        {
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),

                        });
                    }
                    values.groupsocialtrade_list = getgroupsocialtrade_list;
                }
                dt_datatable.Dispose();
            }
        }

        public void DaSocialAndTradeCapitalSubmit(string employee_gid, MdlMstAppCreditUnderWriting values)
        {
            if (values.applicant_type == "Institution")
            {
                msSQL = " update ocs_trn_tcadinstitution set ";
                if (values.social_capital == null || values.social_capital == "")
                {
                    msSQL += " social_capital='',";
                }
                else
                {
                    msSQL += " social_capital='" + values.social_capital.Replace("'", "") + "',";

                }
                if (values.trade_capital == null || values.trade_capital == "")
                {
                    msSQL += " trade_capital='',";
                }
                else
                {
                    msSQL += " trade_capital='" + values.trade_capital.Replace("'", "") + "',";

                }
                msSQL += " economical_flag='Y'," +
                          " updated_by='" + employee_gid + "'," +
                          " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                          " where institution_gid='" + values.institution_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else if (values.applicant_type == "Individual")
            {
                msSQL = " update ocs_trn_tcadcontact set ";
                if (values.social_capital == null || values.social_capital == "")
                {
                    msSQL += " social_capital='',";
                }
                else
                {
                    msSQL += " social_capital='" + values.social_capital.Replace("'", "") + "',";

                }
                if (values.trade_capital == null || values.trade_capital == "")
                {
                    msSQL += " trade_capital='',";
                }
                else
                {
                    msSQL += " trade_capital='" + values.trade_capital.Replace("'", "") + "',";

                }
                msSQL += " economical_flag='Y'," +
                         " updated_by='" + employee_gid + "'," +
                          " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                          " where contact_gid='" + values.contact_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else if (values.applicant_type == "Group")
            {
                msSQL = " update ocs_trn_tcadgroup set ";
                if (values.social_capital == null || values.social_capital == "")
                {
                    msSQL += " social_capital='',";
                }
                else
                {
                    msSQL += " social_capital='" + values.social_capital.Replace("'", "") + "',";

                }
                if (values.trade_capital == null || values.trade_capital == "")
                {
                    msSQL += " trade_capital='',";
                }
                else
                {
                    msSQL += " trade_capital='" + values.trade_capital.Replace("'", "") + "',";

                }
                msSQL += " economical_flag='Y'," +
                         " updated_by='" + employee_gid + "'," +
                          " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                          " where group_gid='" + values.group_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Social/Trade Capital Details Submitted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Submitting Social/Trade Capital Details";
            }
        }

        public void DaSocialAndTradeCapitalUpdate(string employee_gid, MdlMstAppCreditUnderWriting values)
        {
            if (values.applicant_type == "Institution")
            {
                msSQL = " update ocs_trn_tcadinstitution set ";
                if (values.social_capital == null || values.social_capital == "")
                {
                    msSQL += " social_capital='',";
                }
                else
                {
                    msSQL += " social_capital='" + values.social_capital.Replace("'", "") + "',";

                }
                if (values.trade_capital == null || values.trade_capital == "")
                {
                    msSQL += " trade_capital='',";
                }
                else
                {
                    msSQL += " trade_capital='" + values.trade_capital.Replace("'", "") + "',";

                }
                msSQL += " economical_flag='Y'," +
                          " updated_by='" + employee_gid + "'," +
                          " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                          " where institution_gid='" + values.institution_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else if (values.applicant_type == "Individual")
            {
                msSQL = " update ocs_trn_tcadcontact set ";
                if (values.social_capital == null || values.social_capital == "")
                {
                    msSQL += " social_capital='',";
                }
                else
                {
                    msSQL += " social_capital='" + values.social_capital.Replace("'", "") + "',";

                }
                if (values.trade_capital == null || values.trade_capital == "")
                {
                    msSQL += " trade_capital='',";
                }
                else
                {
                    msSQL += " trade_capital='" + values.trade_capital.Replace("'", "") + "',";

                }
                msSQL += " economical_flag='Y'," +
                         " updated_by='" + employee_gid + "'," +
                          " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                          " where contact_gid='" + values.contact_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else if (values.applicant_type == "Group")
            {
                msSQL = " update ocs_trn_tcadgroup set ";
                if (values.social_capital == null || values.social_capital == "")
                {
                    msSQL += " social_capital='',";
                }
                else
                {
                    msSQL += " social_capital='" + values.social_capital.Replace("'", "") + "',";

                }
                if (values.trade_capital == null || values.trade_capital == "")
                {
                    msSQL += " trade_capital='',";
                }
                else
                {
                    msSQL += " trade_capital='" + values.trade_capital.Replace("'", "") + "',";

                }
                msSQL += " economical_flag='Y'," +
                         " updated_by='" + employee_gid + "'," +
                          " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                          " where group_gid='" + values.group_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Social/Trade Capital Details Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Updating Social/Trade Capital Details";
            }
        }


        //PSL
        public void DaGetPSLDropdownList(MdlPSLDropDown values)
        {
            //Occupation
            msSQL = " SELECT a.occupation_gid,a.occupation_name " +
                    " FROM ocs_mst_toccupation a  where status='Y' order by a.occupation_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getoccupation_list = new List<occupation_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getoccupation_list.Add(new occupation_list
                    {
                        occupation_gid = (dr_datarow["occupation_gid"].ToString()),
                        occupation_name = (dr_datarow["occupation_name"].ToString()),
                    });
                }
                values.occupation_list = getoccupation_list;
            }
            dt_datatable.Dispose();
            //Line of Activity
            msSQL = " SELECT lineofactivity_gid,lineof_activity" +
                        " FROM ocs_mst_tlineofactivity a  where status='Y' order by a.lineofactivity_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlineofactivity_list = new List<lineofactivity_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlineofactivity_list.Add(new lineofactivity_list
                    {
                        lineofactivity_gid = (dr_datarow["lineofactivity_gid"].ToString()),
                        lineof_activity = (dr_datarow["lineof_activity"].ToString()),
                    });
                }
                values.lineofactivity_list = getlineofactivity_list;
            }
            dt_datatable.Dispose();
            //BSR code
            msSQL = " SELECT bsrcode_gid,bsr_code FROM ocs_mst_tbsrcode a" +
                   " where status='Y' order by bsrcode_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbsrcode_list = new List<bsrcode_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbsrcode_list.Add(new bsrcode_list
                    {
                        bsrcode_gid = (dr_datarow["bsrcode_gid"].ToString()),
                        bsr_code = (dr_datarow["bsr_code"].ToString()),
                    });
                }
                values.bsrcode_list = getbsrcode_list;
            }
            dt_datatable.Dispose();
            //PSL Category 
            msSQL = " SELECT pslcategory_gid,psl_category from ocs_mst_tpslcategory a" +
                   " where status='Y' order by pslcategory_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getpslcategorylist = new List<pslcategorylist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getpslcategorylist.Add(new pslcategorylist
                    {
                        pslcategory_gid = (dr_datarow["pslcategory_gid"].ToString()),
                        psl_category = (dr_datarow["psl_category"].ToString()),
                    });
                }
                values.pslcategorylist = getpslcategorylist;
            }
            dt_datatable.Dispose();
            //Weaker section
            msSQL = " SELECT weakersection_gid,weaker_section from ocs_mst_tweakersection a" +
                    " where status='Y' order by weakersection_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getweakersectionlist = new List<weakersectionlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getweakersectionlist.Add(new weakersectionlist
                    {
                        weakersection_gid = (dr_datarow["weakersection_gid"].ToString()),
                        weaker_section = (dr_datarow["weaker_section"].ToString()),
                    });
                }
                values.weakersectionlist = getweakersectionlist;
            }
            dt_datatable.Dispose();
            //PSL purpose 
            msSQL = " SELECT pslpurpose_gid,psl_purpose FROM ocs_mst_tpslpurpose a" +
                      " where status='Y' order by a.pslpurpose_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getpslpurpose_list = new List<pslpurpose_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getpslpurpose_list.Add(new pslpurpose_list
                    {
                        pslpurpose_gid = (dr_datarow["pslpurpose_gid"].ToString()),
                        psl_purpose = (dr_datarow["psl_purpose"].ToString()),
                    });
                }
                values.pslpurpose_list = getpslpurpose_list;
            }
            dt_datatable.Dispose();
            //Nature of Entity
            msSQL = " SELECT natureofentity_gid,natureofentity_name FROM ocs_mst_tnatureofentity" +
                " where status='Y' order by natureofentity_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getnatureofentitylist = new List<natureofentitylist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getnatureofentitylist.Add(new natureofentitylist
                    {
                        natureofentity_gid = (dr_datarow["natureofentity_gid"].ToString()),
                        natureofentity_name = (dr_datarow["natureofentity_name"].ToString()),
                    });
                }
                values.natureofentitylist = getnatureofentitylist;
            }
            dt_datatable.Dispose();
            //Turnover
            msSQL = " SELECT a.turnover_gid,a.turnover_name from ocs_mst_tturnover a" +
                   "  where status='Y' order by a.turnover_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getturnoverlist = new List<turnoverlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getturnoverlist.Add(new turnoverlist
                    {
                        turnover_gid = (dr_datarow["turnover_gid"].ToString()),
                        turnover_name = (dr_datarow["turnover_name"].ToString()),
                    });
                }
                values.turnoverlist = getturnoverlist;
            }
            dt_datatable.Dispose();
            //MSME Classification
            msSQL = " SELECT a.msme_gid,a.msme_name from ocs_mst_tmsme a" +
                   "  where status='Y' order by a.msme_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmsmelist = new List<msmelist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmsmelist.Add(new msmelist
                    {
                        msme_gid = (dr_datarow["msme_gid"].ToString()),
                        msme_name = (dr_datarow["msme_name"].ToString()),
                    });
                }
                values.msmelist = getmsmelist;
            }
            dt_datatable.Dispose();
            //Investment
            msSQL = " SELECT a.investment_gid,a.investment_name from ocs_mst_tinvestment a" +
                   "  where status='Y' order by a.investment_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getinvestmentlist = new List<investmentlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getinvestmentlist.Add(new investmentlist
                    {
                        investment_gid = (dr_datarow["investment_gid"].ToString()),
                        investment_name = (dr_datarow["investment_name"].ToString()),
                    });
                }
                values.investmentlist = getinvestmentlist;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaEditPSLDataFlagging(string credit_gid, string applicant_type, MdlMstAppCreditUnderWriting values)
        {
            try
            {
                if (applicant_type == "Institution")
                {
                    msSQL = " select startupasofloansanction_date, occupation_gid, occupation, lineofactivity_gid, lineofactivity, bsrcode_gid, clientdtl_gid, client_dtl, " +
                        " bsrcode, pslcategory_gid, pslcategory, weakersection_gid, weakersection, pslpurpose_gid, pslpurpose, totalsanction_financialinstitution, " +
                        " pslsanction_limit, natureofentity_gid, natureofentity, indulgeinmarketing_activity, plantandmachineryinvestment_gid, hq_metropolitancity," +
                        " plantandmachineryinvestment, turnover_gid, turnover, msmeclassification_gid, msmeclassification, psltagging_flag," +
                        " date_format(loansanction_date, '%d-%m-%Y') loansanction_date, date_format(entityincorporation_date, '%d-%m-%Y') entityincorporation_date " +
                        " from ocs_trn_tcadinstitution where institution_gid='" + credit_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.startupasofloansanction_date = objODBCDatareader["startupasofloansanction_date"].ToString();
                        values.occupation_gid = objODBCDatareader["occupation_gid"].ToString();
                        values.occupation = objODBCDatareader["occupation"].ToString();
                        values.lineofactivity_gid = objODBCDatareader["lineofactivity_gid"].ToString();
                        values.lineofactivity = objODBCDatareader["lineofactivity"].ToString();
                        values.bsrcode_gid = objODBCDatareader["bsrcode_gid"].ToString();
                        values.clientdtl_gid = objODBCDatareader["clientdtl_gid"].ToString();
                        values.clientdtl_name = objODBCDatareader["client_dtl"].ToString();
                        values.bsrcode = objODBCDatareader["bsrcode"].ToString();
                        values.pslcategory_gid = objODBCDatareader["pslcategory_gid"].ToString();
                        values.pslcategory = objODBCDatareader["pslcategory"].ToString();
                        values.weakersection_gid = objODBCDatareader["weakersection_gid"].ToString();
                        values.weakersection = objODBCDatareader["weakersection"].ToString();
                        values.pslpurpose_gid = objODBCDatareader["pslpurpose_gid"].ToString();
                        values.pslpurpose = objODBCDatareader["pslpurpose"].ToString();
                        values.totalsanction_financialinstitution = objODBCDatareader["totalsanction_financialinstitution"].ToString();
                        values.pslsanction_limit = objODBCDatareader["pslsanction_limit"].ToString();
                        values.natureofentity_gid = objODBCDatareader["natureofentity_gid"].ToString();
                        values.natureofentity = objODBCDatareader["natureofentity"].ToString();
                        values.indulgeinmarketing_activity = objODBCDatareader["indulgeinmarketing_activity"].ToString();
                        values.plantandmachineryinvestment_gid = objODBCDatareader["plantandmachineryinvestment_gid"].ToString();
                        values.hq_metropolitancity = objODBCDatareader["hq_metropolitancity"].ToString();
                        values.plantandmachineryinvestment = objODBCDatareader["plantandmachineryinvestment"].ToString();
                        values.turnover_gid = objODBCDatareader["turnover_gid"].ToString();
                        values.turnover = objODBCDatareader["turnover"].ToString();
                        values.msmeclassification_gid = objODBCDatareader["msmeclassification_gid"].ToString();
                        values.msmeclassification = objODBCDatareader["msmeclassification"].ToString();
                        values.loansanction_date = objODBCDatareader["loansanction_date"].ToString();
                        values.entityincorporation_date = objODBCDatareader["entityincorporation_date"].ToString();
                        values.psltagging_flag = objODBCDatareader["psltagging_flag"].ToString();
                    }
                    objODBCDatareader.Close();
                }
                else if (applicant_type == "Individual")
                {
                    msSQL = " select startupasofloansanction_date, occupation_gid, occupation, lineofactivity_gid, lineofactivity, bsrcode_gid, clientdtl_gid, client_dtl, " +
                       " bsrcode, pslcategory_gid, pslcategory, weakersection_gid, weakersection, pslpurpose_gid, pslpurpose, totalsanction_financialinstitution, " +
                       " pslsanction_limit, natureofentity_gid, natureofentity, indulgeinmarketing_activity, plantandmachineryinvestment_gid, hq_metropolitancity," +
                       " plantandmachineryinvestment, turnover_gid, turnover, msmeclassification_gid, msmeclassification, psltagging_flag," +
                       " date_format(loansanction_date, '%d-%m-%Y') loansanction_date, date_format(entityincorporation_date, '%d-%m-%Y') entityincorporation_date " +
                         " from ocs_trn_tcadcontact where contact_gid='" + credit_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.startupasofloansanction_date = objODBCDatareader["startupasofloansanction_date"].ToString();
                        values.occupation_gid = objODBCDatareader["occupation_gid"].ToString();
                        values.occupation = objODBCDatareader["occupation"].ToString();
                        values.lineofactivity_gid = objODBCDatareader["lineofactivity_gid"].ToString();
                        values.lineofactivity = objODBCDatareader["lineofactivity"].ToString();
                        values.bsrcode_gid = objODBCDatareader["bsrcode_gid"].ToString();
                        values.clientdtl_gid = objODBCDatareader["clientdtl_gid"].ToString();
                        values.clientdtl_name = objODBCDatareader["client_dtl"].ToString();
                        values.bsrcode = objODBCDatareader["bsrcode"].ToString();
                        values.pslcategory_gid = objODBCDatareader["pslcategory_gid"].ToString();
                        values.pslcategory = objODBCDatareader["pslcategory"].ToString();
                        values.weakersection_gid = objODBCDatareader["weakersection_gid"].ToString();
                        values.weakersection = objODBCDatareader["weakersection"].ToString();
                        values.pslpurpose_gid = objODBCDatareader["pslpurpose_gid"].ToString();
                        values.pslpurpose = objODBCDatareader["pslpurpose"].ToString();
                        values.totalsanction_financialinstitution = objODBCDatareader["totalsanction_financialinstitution"].ToString();
                        values.pslsanction_limit = objODBCDatareader["pslsanction_limit"].ToString();
                        values.natureofentity_gid = objODBCDatareader["natureofentity_gid"].ToString();
                        values.natureofentity = objODBCDatareader["natureofentity"].ToString();
                        values.indulgeinmarketing_activity = objODBCDatareader["indulgeinmarketing_activity"].ToString();
                        values.plantandmachineryinvestment_gid = objODBCDatareader["plantandmachineryinvestment_gid"].ToString();
                        values.hq_metropolitancity = objODBCDatareader["hq_metropolitancity"].ToString();
                        values.plantandmachineryinvestment = objODBCDatareader["plantandmachineryinvestment"].ToString();
                        values.turnover_gid = objODBCDatareader["turnover_gid"].ToString();
                        values.turnover = objODBCDatareader["turnover"].ToString();
                        values.msmeclassification_gid = objODBCDatareader["msmeclassification_gid"].ToString();
                        values.msmeclassification = objODBCDatareader["msmeclassification"].ToString();
                        values.loansanction_date = objODBCDatareader["loansanction_date"].ToString();
                        values.entityincorporation_date = objODBCDatareader["entityincorporation_date"].ToString();
                        values.psltagging_flag = objODBCDatareader["psltagging_flag"].ToString();
                    }
                    objODBCDatareader.Close();
                }
                else if (applicant_type == "Group")
                {
                    msSQL = " select startupasofloansanction_date, occupation_gid, occupation, lineofactivity_gid, lineofactivity, bsrcode_gid, clientdtl_gid, client_dtl, " +
                    " bsrcode, pslcategory_gid, pslcategory, weakersection_gid, weakersection, pslpurpose_gid, pslpurpose, totalsanction_financialinstitution, " +
                    " pslsanction_limit, natureofentity_gid, natureofentity, indulgeinmarketing_activity, plantandmachineryinvestment_gid, hq_metropolitancity," +
                    " plantandmachineryinvestment, turnover_gid, turnover, msmeclassification_gid, msmeclassification, psltagging_flag," +
                    " date_format(loansanction_date, '%d-%m-%Y') loansanction_date, date_format(entityincorporation_date, '%d-%m-%Y') entityincorporation_date " +
                    " from ocs_trn_tcadgroup where group_gid='" + credit_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.startupasofloansanction_date = objODBCDatareader["startupasofloansanction_date"].ToString();
                        values.occupation_gid = objODBCDatareader["occupation_gid"].ToString();
                        values.occupation = objODBCDatareader["occupation"].ToString();
                        values.lineofactivity_gid = objODBCDatareader["lineofactivity_gid"].ToString();
                        values.lineofactivity = objODBCDatareader["lineofactivity"].ToString();
                        values.bsrcode_gid = objODBCDatareader["bsrcode_gid"].ToString();
                        values.clientdtl_gid = objODBCDatareader["clientdtl_gid"].ToString();
                        values.clientdtl_name = objODBCDatareader["client_dtl"].ToString();
                        values.bsrcode = objODBCDatareader["bsrcode"].ToString();
                        values.pslcategory_gid = objODBCDatareader["pslcategory_gid"].ToString();
                        values.pslcategory = objODBCDatareader["pslcategory"].ToString();
                        values.weakersection_gid = objODBCDatareader["weakersection_gid"].ToString();
                        values.weakersection = objODBCDatareader["weakersection"].ToString();
                        values.pslpurpose_gid = objODBCDatareader["pslpurpose_gid"].ToString();
                        values.pslpurpose = objODBCDatareader["pslpurpose"].ToString();
                        values.totalsanction_financialinstitution = objODBCDatareader["totalsanction_financialinstitution"].ToString();
                        values.pslsanction_limit = objODBCDatareader["pslsanction_limit"].ToString();
                        values.natureofentity_gid = objODBCDatareader["natureofentity_gid"].ToString();
                        values.natureofentity = objODBCDatareader["natureofentity"].ToString();
                        values.indulgeinmarketing_activity = objODBCDatareader["indulgeinmarketing_activity"].ToString();
                        values.plantandmachineryinvestment_gid = objODBCDatareader["plantandmachineryinvestment_gid"].ToString();
                        values.hq_metropolitancity = objODBCDatareader["hq_metropolitancity"].ToString();
                        values.plantandmachineryinvestment = objODBCDatareader["plantandmachineryinvestment"].ToString();
                        values.turnover_gid = objODBCDatareader["turnover_gid"].ToString();
                        values.turnover = objODBCDatareader["turnover"].ToString();
                        values.msmeclassification_gid = objODBCDatareader["msmeclassification_gid"].ToString();
                        values.msmeclassification = objODBCDatareader["msmeclassification"].ToString();
                        values.loansanction_date = objODBCDatareader["loansanction_date"].ToString();
                        values.entityincorporation_date = objODBCDatareader["entityincorporation_date"].ToString();
                        values.psltagging_flag = objODBCDatareader["psltagging_flag"].ToString();
                    }
                    objODBCDatareader.Close();
                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaGetPSLDataFlagging(string credit_gid, string applicant_type, MdlMstAppCreditUnderWriting values)
        {
            if (applicant_type == "Institution")
            {
                msSQL = " SELECT startupasofloansanction_date, occupation, lineofactivity, bsrcode, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                    " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as updated_by " +
                    " FROM ocs_trn_tcadinstitution a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                         " where a.institution_gid = '" + credit_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getinstitutionpsltagging_list = new List<institutionpsltagging_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getinstitutionpsltagging_list.Add(new institutionpsltagging_list
                        {
                            startupasofloansanction_date = (dr_datarow["startupasofloansanction_date"].ToString()),
                            occupation = (dr_datarow["occupation"].ToString()),
                            lineofactivity = (dr_datarow["lineofactivity"].ToString()),
                            bsrcode = (dr_datarow["bsrcode"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                        });
                    }
                    values.institutionpsltagging_list = getinstitutionpsltagging_list;
                }
                dt_datatable.Dispose();
            }
            if (applicant_type == "Individual")
            {
                msSQL = " SELECT startupasofloansanction_date, occupation, lineofactivity, bsrcode, " +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                        " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as updated_by " +
                        " FROM ocs_trn_tcadcontact a " +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
                        " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                        " where a.contact_gid = '" + credit_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getindividualpsltagging_list = new List<individualpsltagging_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getindividualpsltagging_list.Add(new individualpsltagging_list
                        {
                            startupasofloansanction_date = (dr_datarow["startupasofloansanction_date"].ToString()),
                            occupation = (dr_datarow["occupation"].ToString()),
                            lineofactivity = (dr_datarow["lineofactivity"].ToString()),
                            bsrcode = (dr_datarow["bsrcode"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                        });
                    }
                    values.individualpsltagging_list = getindividualpsltagging_list;
                }
                dt_datatable.Dispose();
            }
            if (applicant_type == "Group")
            {
                msSQL = " SELECT startupasofloansanction_date, occupation, lineofactivity, bsrcode, " +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                        " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as updated_by " +
                        " FROM ocs_trn_tcadgroup a " +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
                        " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                        " where a.group_gid = '" + credit_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getgrouppsltagging_list = new List<grouppsltagging_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getgrouppsltagging_list.Add(new grouppsltagging_list
                        {
                            startupasofloansanction_date = (dr_datarow["startupasofloansanction_date"].ToString()),
                            occupation = (dr_datarow["occupation"].ToString()),
                            lineofactivity = (dr_datarow["lineofactivity"].ToString()),
                            bsrcode = (dr_datarow["bsrcode"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                        });
                    }
                    values.grouppsltagging_list = getgrouppsltagging_list;
                }
                dt_datatable.Dispose();
            }
        }


        public void DaPSLDataFlaggingSave(string employee_gid, MdlMstAppCreditUnderWriting values)
        {
            if (values.applicant_type == "Institution")
            {
                msSQL = " update ocs_trn_tcadinstitution set " +
                    " startupasofloansanction_date='" + values.startupasofloansanction_date + "'," +
                    " occupation_gid='" + values.occupation_gid + "'," +
                    " occupation='" + values.occupation + "'," +
                    " lineofactivity_gid='" + values.lineofactivity_gid + "'," +
                    " lineofactivity='" + values.lineofactivity + "'," +
                    " bsrcode_gid='" + values.bsrcode_gid + "'," +
                    " bsrcode='" + values.bsrcode + "'," +
                    " pslcategory_gid='" + values.pslcategory_gid + "'," +
                    " pslcategory='" + values.pslcategory + "'," +
                    " weakersection_gid='" + values.weakersection_gid + "'," +
                    " weakersection='" + values.weakersection + "'," +
                    " pslpurpose_gid='" + values.pslpurpose_gid + "'," +
                    " pslpurpose='" + values.pslpurpose + "'," +
                    " totalsanction_financialinstitution='" + values.totalsanction_financialinstitution + "'," +
                    " pslsanction_limit='" + values.pslsanction_limit + "'," +
                    " natureofentity_gid='" + values.natureofentity_gid + "'," +
                    " natureofentity='" + values.natureofentity + "'," +
                    " indulgeinmarketing_activity='" + values.indulgeinmarketing_activity + "'," +
                    " plantandmachineryinvestment_gid='" + values.plantandmachineryinvestment_gid + "'," +
                    " plantandmachineryinvestment='" + values.plantandmachineryinvestment + "'," +
                    " turnover_gid='" + values.turnover_gid + "'," +
                    " turnover='" + values.turnover + "'," +
                    " msmeclassification_gid='" + values.msmeclassification_gid + "'," +
                    " msmeclassification='" + values.msmeclassification + "',";
                if (Convert.ToDateTime(values.loansanction_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " loansanction_date='" + Convert.ToDateTime(values.loansanction_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                if (Convert.ToDateTime(values.entityincorporation_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " entityincorporation_date='" + Convert.ToDateTime(values.entityincorporation_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                msSQL += " hq_metropolitancity='" + values.hq_metropolitancity + "'," +
                    " clientdtl_gid='" + values.clientdtl_gid + "'," +
                    " client_dtl='" + values.clientdtl_name + "'," +
                    " psltagging_flag='Y'," +
                    " updated_by='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where institution_gid='" + values.institution_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else if (values.applicant_type == "Individual")
            {
                msSQL = " update ocs_trn_tcadcontact set " +
                    " startupasofloansanction_date='" + values.startupasofloansanction_date + "'," +
                    " occupation_gid='" + values.occupation_gid + "'," +
                    " occupation='" + values.occupation + "'," +
                    " lineofactivity_gid='" + values.lineofactivity_gid + "'," +
                    " lineofactivity='" + values.lineofactivity + "'," +
                    " bsrcode_gid='" + values.bsrcode_gid + "'," +
                    " bsrcode='" + values.bsrcode + "'," +
                    " pslcategory_gid='" + values.pslcategory_gid + "'," +
                    " pslcategory='" + values.pslcategory + "'," +
                    " weakersection_gid='" + values.weakersection_gid + "'," +
                    " weakersection='" + values.weakersection + "'," +
                    " pslpurpose_gid='" + values.pslpurpose_gid + "'," +
                    " pslpurpose='" + values.pslpurpose + "'," +
                    " totalsanction_financialinstitution='" + values.totalsanction_financialinstitution + "'," +
                    " pslsanction_limit='" + values.pslsanction_limit + "'," +
                    " natureofentity_gid='" + values.natureofentity_gid + "'," +
                    " natureofentity='" + values.natureofentity + "'," +
                    " indulgeinmarketing_activity='" + values.indulgeinmarketing_activity + "'," +
                    " plantandmachineryinvestment_gid='" + values.plantandmachineryinvestment_gid + "'," +
                    " plantandmachineryinvestment='" + values.plantandmachineryinvestment + "'," +
                    " turnover_gid='" + values.turnover_gid + "'," +
                    " turnover='" + values.turnover + "'," +
                    " msmeclassification_gid='" + values.msmeclassification_gid + "'," +
                    " msmeclassification='" + values.msmeclassification + "',";
                if (Convert.ToDateTime(values.loansanction_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " loansanction_date='" + Convert.ToDateTime(values.loansanction_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                if (Convert.ToDateTime(values.entityincorporation_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " entityincorporation_date='" + Convert.ToDateTime(values.entityincorporation_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                msSQL += " hq_metropolitancity='" + values.hq_metropolitancity + "'," +
                    " clientdtl_gid='" + values.clientdtl_gid + "'," +
                    " client_dtl='" + values.clientdtl_name + "'," +
                          " psltagging_flag='Y'," +
                          " updated_by='" + employee_gid + "'," +
                          " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                          " where contact_gid='" + values.contact_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else if (values.applicant_type == "Group")
            {
                msSQL = " update ocs_trn_tcadgroup set " +
                    " startupasofloansanction_date='" + values.startupasofloansanction_date + "'," +
                    " occupation_gid='" + values.occupation_gid + "'," +
                    " occupation='" + values.occupation + "'," +
                    " lineofactivity_gid='" + values.lineofactivity_gid + "'," +
                    " lineofactivity='" + values.lineofactivity + "'," +
                    " bsrcode_gid='" + values.bsrcode_gid + "'," +
                    " bsrcode='" + values.bsrcode + "'," +
                    " pslcategory_gid='" + values.pslcategory_gid + "'," +
                    " pslcategory='" + values.pslcategory + "'," +
                    " weakersection_gid='" + values.weakersection_gid + "'," +
                    " weakersection='" + values.weakersection + "'," +
                    " pslpurpose_gid='" + values.pslpurpose_gid + "'," +
                    " pslpurpose='" + values.pslpurpose + "'," +
                    " totalsanction_financialinstitution='" + values.totalsanction_financialinstitution + "'," +
                    " pslsanction_limit='" + values.pslsanction_limit + "'," +
                    " natureofentity_gid='" + values.natureofentity_gid + "'," +
                    " natureofentity='" + values.natureofentity + "'," +
                    " indulgeinmarketing_activity='" + values.indulgeinmarketing_activity + "'," +
                    " plantandmachineryinvestment_gid='" + values.plantandmachineryinvestment_gid + "'," +
                    " plantandmachineryinvestment='" + values.plantandmachineryinvestment + "'," +
                    " turnover_gid='" + values.turnover_gid + "'," +
                    " turnover='" + values.turnover + "'," +
                    " msmeclassification_gid='" + values.msmeclassification_gid + "'," +
                    " msmeclassification='" + values.msmeclassification + "',";
                if (Convert.ToDateTime(values.loansanction_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " loansanction_date='" + Convert.ToDateTime(values.loansanction_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                if (Convert.ToDateTime(values.entityincorporation_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " entityincorporation_date='" + Convert.ToDateTime(values.entityincorporation_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                msSQL += " hq_metropolitancity='" + values.hq_metropolitancity + "'," +
                    " clientdtl_gid='" + values.clientdtl_gid + "'," +
                    " client_dtl='" + values.clientdtl_name + "'," +
                          " psltagging_flag='Y'," +
                          " updated_by='" + employee_gid + "'," +
                          " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                          " where group_gid='" + values.group_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "PSL Tagging Flag Details Saved Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Saving PSL Tagging Flag Details";
            }
        }

        public void DaPSLDataFlaggingSubmit(string employee_gid, MdlMstAppCreditUnderWriting values)
        {
            if (values.applicant_type == "Institution")
            {
                msSQL = " update ocs_trn_tcadinstitution set " +
                        " startupasofloansanction_date='" + values.startupasofloansanction_date + "'," +
                    " occupation_gid='" + values.occupation_gid + "'," +
                    " occupation='" + values.occupation + "'," +
                    " lineofactivity_gid='" + values.lineofactivity_gid + "'," +
                    " lineofactivity='" + values.lineofactivity + "'," +
                    " bsrcode_gid='" + values.bsrcode_gid + "'," +
                    " bsrcode='" + values.bsrcode + "'," +
                    " pslcategory_gid='" + values.pslcategory_gid + "'," +
                    " pslcategory='" + values.pslcategory + "'," +
                    " weakersection_gid='" + values.weakersection_gid + "'," +
                    " weakersection='" + values.weakersection + "'," +
                    " pslpurpose_gid='" + values.pslpurpose_gid + "'," +
                    " pslpurpose='" + values.pslpurpose + "'," +
                    " totalsanction_financialinstitution='" + values.totalsanction_financialinstitution + "'," +
                    " pslsanction_limit='" + values.pslsanction_limit + "'," +
                    " natureofentity_gid='" + values.natureofentity_gid + "'," +
                    " natureofentity='" + values.natureofentity + "'," +
                    " indulgeinmarketing_activity='" + values.indulgeinmarketing_activity + "'," +
                    " plantandmachineryinvestment_gid='" + values.plantandmachineryinvestment_gid + "'," +
                    " plantandmachineryinvestment='" + values.plantandmachineryinvestment + "'," +
                    " turnover_gid='" + values.turnover_gid + "'," +
                    " turnover='" + values.turnover + "'," +
                    " msmeclassification_gid='" + values.msmeclassification_gid + "'," +
                    " msmeclassification='" + values.msmeclassification + "',";
                if (Convert.ToDateTime(values.loansanction_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " loansanction_date='" + Convert.ToDateTime(values.loansanction_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                if (Convert.ToDateTime(values.entityincorporation_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " entityincorporation_date='" + Convert.ToDateTime(values.entityincorporation_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                msSQL += " hq_metropolitancity='" + values.hq_metropolitancity + "'," +
                    " clientdtl_gid='" + values.clientdtl_gid + "'," +
                    " client_dtl='" + values.clientdtl_name + "'," +
                    " psltagging_flag='Y'," +
                    " updated_by='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where institution_gid='" + values.institution_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else if (values.applicant_type == "Individual")
            {
                msSQL = " update ocs_trn_tcadcontact set " +
                    " startupasofloansanction_date='" + values.startupasofloansanction_date + "'," +
                    " occupation_gid='" + values.occupation_gid + "'," +
                    " occupation='" + values.occupation + "'," +
                    " lineofactivity_gid='" + values.lineofactivity_gid + "'," +
                    " lineofactivity='" + values.lineofactivity + "'," +
                    " bsrcode_gid='" + values.bsrcode_gid + "'," +
                    " bsrcode='" + values.bsrcode + "'," +
                    " pslcategory_gid='" + values.pslcategory_gid + "'," +
                    " pslcategory='" + values.pslcategory + "'," +
                    " weakersection_gid='" + values.weakersection_gid + "'," +
                    " weakersection='" + values.weakersection + "'," +
                    " pslpurpose_gid='" + values.pslpurpose_gid + "'," +
                    " pslpurpose='" + values.pslpurpose + "'," +
                    " totalsanction_financialinstitution='" + values.totalsanction_financialinstitution + "'," +
                    " pslsanction_limit='" + values.pslsanction_limit + "'," +
                    " natureofentity_gid='" + values.natureofentity_gid + "'," +
                    " natureofentity='" + values.natureofentity + "'," +
                    " indulgeinmarketing_activity='" + values.indulgeinmarketing_activity + "'," +
                    " plantandmachineryinvestment_gid='" + values.plantandmachineryinvestment_gid + "'," +
                    " plantandmachineryinvestment='" + values.plantandmachineryinvestment + "'," +
                    " turnover_gid='" + values.turnover_gid + "'," +
                    " turnover='" + values.turnover + "'," +
                    " msmeclassification_gid='" + values.msmeclassification_gid + "'," +
                    " msmeclassification='" + values.msmeclassification + "',";
                if (Convert.ToDateTime(values.loansanction_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " loansanction_date='" + Convert.ToDateTime(values.loansanction_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                if (Convert.ToDateTime(values.entityincorporation_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " entityincorporation_date='" + Convert.ToDateTime(values.entityincorporation_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                msSQL += " hq_metropolitancity='" + values.hq_metropolitancity + "'," +
                    " clientdtl_gid='" + values.clientdtl_gid + "'," +
                    " client_dtl='" + values.clientdtl_name + "'," +
                    " psltagging_flag='Y'," +
                          " updated_by='" + employee_gid + "'," +
                          " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                          " where contact_gid='" + values.contact_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else if (values.applicant_type == "Group")
            {
                msSQL = " update ocs_trn_tcadgroup set " +
                        " startupasofloansanction_date='" + values.startupasofloansanction_date + "'," +
                    " occupation_gid='" + values.occupation_gid + "'," +
                    " occupation='" + values.occupation + "'," +
                    " lineofactivity_gid='" + values.lineofactivity_gid + "'," +
                    " lineofactivity='" + values.lineofactivity + "'," +
                    " bsrcode_gid='" + values.bsrcode_gid + "'," +
                    " bsrcode='" + values.bsrcode + "'," +
                    " pslcategory_gid='" + values.pslcategory_gid + "'," +
                    " pslcategory='" + values.pslcategory + "'," +
                    " weakersection_gid='" + values.weakersection_gid + "'," +
                    " weakersection='" + values.weakersection + "'," +
                    " pslpurpose_gid='" + values.pslpurpose_gid + "'," +
                    " pslpurpose='" + values.pslpurpose + "'," +
                    " totalsanction_financialinstitution='" + values.totalsanction_financialinstitution + "'," +
                    " pslsanction_limit='" + values.pslsanction_limit + "'," +
                    " natureofentity_gid='" + values.natureofentity_gid + "'," +
                    " natureofentity='" + values.natureofentity + "'," +
                    " indulgeinmarketing_activity='" + values.indulgeinmarketing_activity + "'," +
                    " plantandmachineryinvestment_gid='" + values.plantandmachineryinvestment_gid + "'," +
                    " plantandmachineryinvestment='" + values.plantandmachineryinvestment + "'," +
                    " turnover_gid='" + values.turnover_gid + "'," +
                    " turnover='" + values.turnover + "'," +
                    " msmeclassification_gid='" + values.msmeclassification_gid + "'," +
                    " msmeclassification='" + values.msmeclassification + "',";
                if (Convert.ToDateTime(values.loansanction_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " loansanction_date='" + Convert.ToDateTime(values.loansanction_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                if (Convert.ToDateTime(values.entityincorporation_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " entityincorporation_date='" + Convert.ToDateTime(values.entityincorporation_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                msSQL += " hq_metropolitancity='" + values.hq_metropolitancity + "'," +
                    " clientdtl_gid='" + values.clientdtl_gid + "'," +
                    " client_dtl='" + values.clientdtl_name + "'," +
                    " psltagging_flag='Y'," +
                          " updated_by='" + employee_gid + "'," +
                          " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                          " where group_gid='" + values.group_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "PSL Tagging Flag Details Submitted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Submitting PSL Tagging Flag Details";
            }
        }

        public void DaPSLDataFlaggingUpdate(string employee_gid, MdlMstAppCreditUnderWriting values)
        {
            if (values.applicant_type == "Institution")
            {
                msSQL = " update ocs_trn_tcadinstitution set " +
                        " startupasofloansanction_date='" + values.startupasofloansanction_date.Replace("'", "") + "'," +
                    " occupation_gid='" + values.occupation_gid + "'," +
                    " occupation='" + values.occupation + "'," +
                    " lineofactivity_gid='" + values.lineofactivity_gid + "'," +
                    " lineofactivity='" + values.lineofactivity + "'," +
                    " bsrcode_gid='" + values.bsrcode_gid + "'," +
                    " bsrcode='" + values.bsrcode + "'," +
                    " pslcategory_gid='" + values.pslcategory_gid + "'," +
                    " pslcategory='" + values.pslcategory + "'," +
                    " weakersection_gid='" + values.weakersection_gid + "'," +
                    " weakersection='" + values.weakersection + "'," +
                    " pslpurpose_gid='" + values.pslpurpose_gid + "'," +
                    " pslpurpose='" + values.pslpurpose + "'," +
                    " totalsanction_financialinstitution='" + values.totalsanction_financialinstitution.Replace("'", "") + "'," +
                    " pslsanction_limit='" + values.pslsanction_limit.Replace("'", "") + "'," +
                    " natureofentity_gid='" + values.natureofentity_gid + "'," +
                    " natureofentity='" + values.natureofentity + "'," +
                    " indulgeinmarketing_activity='" + values.indulgeinmarketing_activity + "'," +
                    " plantandmachineryinvestment_gid='" + values.plantandmachineryinvestment_gid + "'," +
                    " plantandmachineryinvestment='" + values.plantandmachineryinvestment + "'," +
                    " turnover_gid='" + values.turnover_gid + "'," +
                    " turnover='" + values.turnover + "'," +
                    " msmeclassification_gid='" + values.msmeclassification_gid + "'," +
                    " msmeclassification='" + values.msmeclassification + "',";
                if (Convert.ToDateTime(values.loansanctiondate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " loansanction_date='" + Convert.ToDateTime(values.loansanctiondate).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                if (Convert.ToDateTime(values.entityincorporationdate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " entityincorporation_date='" + Convert.ToDateTime(values.entityincorporationdate).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                msSQL += " hq_metropolitancity='" + values.hq_metropolitancity + "'," +
                    " clientdtl_gid='" + values.clientdtl_gid + "'," +
                    " client_dtl='" + values.clientdtl_name + "'," +
                    " psltagging_flag='Y'," +
                    " updated_by='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where institution_gid='" + values.institution_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else if (values.applicant_type == "Individual")
            {
                msSQL = " update ocs_trn_tcadcontact set " +
                " startupasofloansanction_date='" + values.startupasofloansanction_date + "'," +
                    " occupation_gid='" + values.occupation_gid + "'," +
                    " occupation='" + values.occupation + "'," +
                    " lineofactivity_gid='" + values.lineofactivity_gid + "'," +
                    " lineofactivity='" + values.lineofactivity + "'," +
                    " bsrcode_gid='" + values.bsrcode_gid + "'," +
                    " bsrcode='" + values.bsrcode + "'," +
                    " pslcategory_gid='" + values.pslcategory_gid + "'," +
                    " pslcategory='" + values.pslcategory + "'," +
                    " weakersection_gid='" + values.weakersection_gid + "'," +
                    " weakersection='" + values.weakersection + "'," +
                    " pslpurpose_gid='" + values.pslpurpose_gid + "'," +
                    " pslpurpose='" + values.pslpurpose + "'," +
                    " totalsanction_financialinstitution='" + values.totalsanction_financialinstitution + "'," +
                    " pslsanction_limit='" + values.pslsanction_limit + "'," +
                    " natureofentity_gid='" + values.natureofentity_gid + "'," +
                    " natureofentity='" + values.natureofentity + "'," +
                    " indulgeinmarketing_activity='" + values.indulgeinmarketing_activity + "'," +
                    " plantandmachineryinvestment_gid='" + values.plantandmachineryinvestment_gid + "'," +
                    " plantandmachineryinvestment='" + values.plantandmachineryinvestment + "'," +
                    " turnover_gid='" + values.turnover_gid + "'," +
                    " turnover='" + values.turnover + "'," +
                    " msmeclassification_gid='" + values.msmeclassification_gid + "'," +
                    " msmeclassification='" + values.msmeclassification + "',";
                if (Convert.ToDateTime(values.loansanctiondate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " loansanction_date='" + Convert.ToDateTime(values.loansanctiondate).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                if (Convert.ToDateTime(values.entityincorporationdate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " entityincorporation_date='" + Convert.ToDateTime(values.entityincorporationdate).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                msSQL += " hq_metropolitancity='" + values.hq_metropolitancity + "'," +
                    " clientdtl_gid='" + values.clientdtl_gid + "'," +
                    " client_dtl='" + values.clientdtl_name + "'," +
                    " psltagging_flag='Y'," +
                          " updated_by='" + employee_gid + "'," +
                          " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                          " where contact_gid='" + values.contact_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else if (values.applicant_type == "Group")
            {
                msSQL = " update ocs_trn_tcadgroup set " +
                       " startupasofloansanction_date='" + values.startupasofloansanction_date + "'," +
                    " occupation_gid='" + values.occupation_gid + "'," +
                    " occupation='" + values.occupation + "'," +
                    " lineofactivity_gid='" + values.lineofactivity_gid + "'," +
                    " lineofactivity='" + values.lineofactivity + "'," +
                    " bsrcode_gid='" + values.bsrcode_gid + "'," +
                    " bsrcode='" + values.bsrcode + "'," +
                    " pslcategory_gid='" + values.pslcategory_gid + "'," +
                    " pslcategory='" + values.pslcategory + "'," +
                    " weakersection_gid='" + values.weakersection_gid + "'," +
                    " weakersection='" + values.weakersection + "'," +
                    " pslpurpose_gid='" + values.pslpurpose_gid + "'," +
                    " pslpurpose='" + values.pslpurpose + "'," +
                    " totalsanction_financialinstitution='" + values.totalsanction_financialinstitution + "'," +
                    " pslsanction_limit='" + values.pslsanction_limit + "'," +
                    " natureofentity_gid='" + values.natureofentity_gid + "'," +
                    " natureofentity='" + values.natureofentity + "'," +
                    " indulgeinmarketing_activity='" + values.indulgeinmarketing_activity + "'," +
                    " plantandmachineryinvestment_gid='" + values.plantandmachineryinvestment_gid + "'," +
                    " plantandmachineryinvestment='" + values.plantandmachineryinvestment + "'," +
                    " turnover_gid='" + values.turnover_gid + "'," +
                    " turnover='" + values.turnover + "'," +
                    " msmeclassification_gid='" + values.msmeclassification_gid + "'," +
                    " msmeclassification='" + values.msmeclassification + "',";
                if (Convert.ToDateTime(values.loansanctiondate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " loansanction_date='" + Convert.ToDateTime(values.loansanctiondate).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                if (Convert.ToDateTime(values.entityincorporationdate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " entityincorporation_date='" + Convert.ToDateTime(values.entityincorporationdate).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                msSQL += " hq_metropolitancity='" + values.hq_metropolitancity + "'," +
                    " clientdtl_gid='" + values.clientdtl_gid + "'," +
                    " client_dtl='" + values.clientdtl_name + "'," +
                    " psltagging_flag='Y'," +
                          " updated_by='" + employee_gid + "'," +
                          " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                          " where group_gid='" + values.group_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "PSL Tagging Flag Details Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Updating PSL Tagging Flag Details";
            }
        }


        //Supplier
        public void DaGetCreditSupplierList(string credit_gid, string employee_gid, MdlMstSupplier values)
        {
            msSQL = " select creditsupplier_gid,credit_gid,application_gid,supplier_gid,supplier_name,relationship_vintage_year,relationship_vintage_month," +
                     " format(purchase_amount,'en-IN') as purchase_amount,format(bankdebit_amount,'en-IN')as bankdebit_amount,relationship_supplier," +
                     " date_format(start_date,'%d-%m-%Y') as start_date,date_format(end_date,'%d-%m-%Y') as end_date, " +
                     " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                     " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date, " +
                     " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                     " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as updated_by " +
                     " from ocs_trn_tcadcreditsupplier a " +
                     " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                     " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
                     " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                     " where credit_gid='" + credit_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getsupplier_list = new List<supplier_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getsupplier_list.Add(new supplier_list
                    {
                        creditsupplier_gid = (dr_datarow["creditsupplier_gid"].ToString()),
                        credit_gid = (dr_datarow["credit_gid"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        supplier_gid = (dr_datarow["supplier_gid"].ToString()),
                        supplier_name = (dr_datarow["supplier_name"].ToString()),
                        relationship_vintage_year = (dr_datarow["relationship_vintage_year"].ToString()),
                        relationship_vintage_month = (dr_datarow["relationship_vintage_month"].ToString()),
                        purchase_amount = (dr_datarow["purchase_amount"].ToString()),
                        bankdebit_amount = (dr_datarow["bankdebit_amount"].ToString()),
                        relationship_supplier = (dr_datarow["relationship_supplier"].ToString()),
                        start_date = (dr_datarow["start_date"].ToString()),
                        end_date = (dr_datarow["end_date"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        updated_date = (dr_datarow["updated_date"].ToString()),
                        updated_by = (dr_datarow["updated_by"].ToString()),
                    });
                }
                values.supplier_list = getsupplier_list;
            }
            dt_datatable.Dispose();
        }

        public void DaPostCreditSupplier(string employee_gid, MdlMstSupplier values)
        {
            msSQL = "select supplier_gid from ocs_trn_tcadcreditsupplier where application_gid='" + values.application_gid + "' and supplier_gid='" + values.supplier_gid + "'";
            string lssupplier_gid = objdbconn.GetExecuteScalar(msSQL);
            if (lssupplier_gid == (values.supplier_gid))
            {
                values.status = false;
                values.message = "Already Supplier Added";
                return;
            }
            msGetGid = objcmnfunctions.GetMasterGID("CRSP");
            msSQL = " insert into ocs_trn_tcadcreditsupplier(" +
                   " creditsupplier_gid," +
                   " credit_gid," +
                   " application_gid," +
                   " supplier_gid," +
                   " supplier_name," +
                   " relationship_vintage_year," +
                   " relationship_vintage_month," +
                   " start_date," +
                   " end_date," +
                   " purchase_amount," +
                   " bankdebit_amount ," +
                   " relationship_supplier," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.credit_gid + "'," +
                   "'" + values.application_gid + "'," +
                   "'" + values.supplier_gid + "'," +
                   "'" + values.supplier_name.Replace("'", " ") + "'," +
                   "'" + values.relationship_vintage_year + "'," +
                   "'" + values.relationship_vintage_month + "'," +
                   "'" + Convert.ToDateTime(values.start_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   "'" + Convert.ToDateTime(values.end_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   "'" + values.purchase_amount.Replace(",", "") + "'," +
                   "'" + values.bankdebit_amount.Replace(",", "") + "'," +
                   "'" + values.relationship_supplier.Replace("'", " ") + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Supplier details Added successfully";

                msSQL = " select creditsupplier_gid,credit_gid,application_gid,supplier_gid,supplier_name,relationship_vintage_year,relationship_vintage_month," +
                     " format(purchase_amount,'en-IN') as purchase_amount,format(bankdebit_amount,'en-IN')as bankdebit_amount,relationship_supplier," +
                     " date_format(start_date,'%d-%m-%Y') as start_date,date_format(end_date,'%d-%m-%Y') as end_date " +
                     " from ocs_trn_tcadcreditsupplier where " +
                     " credit_gid='" + values.credit_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getsupplier_list = new List<supplier_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getsupplier_list.Add(new supplier_list
                        {
                            creditsupplier_gid = (dr_datarow["creditsupplier_gid"].ToString()),
                            credit_gid = (dr_datarow["credit_gid"].ToString()),
                            application_gid = (dr_datarow["application_gid"].ToString()),
                            supplier_gid = (dr_datarow["supplier_gid"].ToString()),
                            supplier_name = (dr_datarow["supplier_name"].ToString()),
                            relationship_vintage_year = (dr_datarow["relationship_vintage_year"].ToString()),
                            relationship_vintage_month = (dr_datarow["relationship_vintage_month"].ToString()),
                            purchase_amount = (dr_datarow["purchase_amount"].ToString()),
                            bankdebit_amount = (dr_datarow["bankdebit_amount"].ToString()),
                            relationship_supplier = (dr_datarow["relationship_supplier"].ToString()),
                            start_date = (dr_datarow["start_date"].ToString()),
                            end_date = (dr_datarow["end_date"].ToString()),
                        });
                    }
                    values.supplier_list = getsupplier_list;
                }
                dt_datatable.Dispose();
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while Adding";
            }
        }

        public void DaDeleteCreditSupplier(string creditsupplier_gid, string credit_gid, MdlMstSupplier values, string employee_gid)
        {
            msSQL = "delete from ocs_trn_tcadcreditsupplier where creditsupplier_gid='" + creditsupplier_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Supplier details Deleted successfully";

                msSQL = " select creditsupplier_gid,credit_gid,application_gid,supplier_gid,supplier_name,relationship_vintage_year,relationship_vintage_month," +
                     " format(purchase_amount,'en-IN') as purchase_amount,format(bankdebit_amount,'en-IN')as bankdebit_amount,relationship_supplier," +
                     " date_format(start_date,'%d-%m-%Y') as start_date,date_format(end_date,'%d-%m-%Y') as end_date " +
                     " from ocs_trn_tcadcreditsupplier where credit_gid='" + credit_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getsupplier_list = new List<supplier_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getsupplier_list.Add(new supplier_list
                        {
                            creditsupplier_gid = (dr_datarow["creditsupplier_gid"].ToString()),
                            credit_gid = (dr_datarow["credit_gid"].ToString()),
                            application_gid = (dr_datarow["application_gid"].ToString()),
                            supplier_gid = (dr_datarow["supplier_gid"].ToString()),
                            supplier_name = (dr_datarow["supplier_name"].ToString()),
                            relationship_vintage_year = (dr_datarow["relationship_vintage_year"].ToString()),
                            relationship_vintage_month = (dr_datarow["relationship_vintage_month"].ToString()),
                            purchase_amount = (dr_datarow["purchase_amount"].ToString()),
                            bankdebit_amount = (dr_datarow["bankdebit_amount"].ToString()),
                            relationship_supplier = (dr_datarow["relationship_supplier"].ToString()),
                            start_date = (dr_datarow["start_date"].ToString()),
                            end_date = (dr_datarow["end_date"].ToString()),
                        });
                    }
                    values.supplier_list = getsupplier_list;
                }
                dt_datatable.Dispose();
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred while deleting";
            }
        }


        public void DaEditGetCreditSupplier(string creditsupplier_gid, MdlMstSupplier values)
        {
            try
            {
                msSQL = " select creditsupplier_gid,credit_gid,application_gid,supplier_gid,supplier_name,relationship_vintage_year,relationship_vintage_month," +
                     " format(purchase_amount,'en-IN') as purchase_amount,format(bankdebit_amount,'en-IN') as bankdebit_amount,relationship_supplier,start_date,end_date" +
                     " from ocs_trn_tcadcreditsupplier where creditsupplier_gid='" + creditsupplier_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.creditsupplier_gid = objODBCDatareader["creditsupplier_gid"].ToString();
                    values.credit_gid = objODBCDatareader["credit_gid"].ToString();
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.supplier_gid = objODBCDatareader["supplier_gid"].ToString();
                    values.supplier_name = objODBCDatareader["supplier_name"].ToString();
                    values.relationship_vintage_year = objODBCDatareader["relationship_vintage_year"].ToString();
                    values.relationship_vintage_month = objODBCDatareader["relationship_vintage_month"].ToString();
                    values.purchase_amount = objODBCDatareader["purchase_amount"].ToString();
                    values.bankdebit_amount = objODBCDatareader["bankdebit_amount"].ToString();
                    values.relationship_supplier = objODBCDatareader["relationship_supplier"].ToString();
                    if (objODBCDatareader["start_date"].ToString() == "")
                    {
                    }
                    else
                    {
                        values.start_date = Convert.ToDateTime(objODBCDatareader["start_date"]).ToString("MM-dd-yyyy");
                    }
                    if (objODBCDatareader["end_date"].ToString() == "")
                    {
                    }
                    else
                    {
                        values.end_date = Convert.ToDateTime(objODBCDatareader["end_date"]).ToString("MM-dd-yyyy");
                    }
                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaGetCreditAccountType(MdlCreditBankAcc values)
        {
            msSQL = " select bankaccounttype_gid,bankaccounttype_name from ocs_mst_tbankaccounttype where status = 'Y' order by bankaccounttype_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var creditbankacc_list = new List<creditbankacc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    creditbankacc_list.Add(new creditbankacc_list
                    {
                        bankaccounttype_gid = (dr_datarow["bankaccounttype_gid"].ToString()),
                        bankaccounttype_name = (dr_datarow["bankaccounttype_name"].ToString()),
                    });
                }
                values.creditbankacc_list = creditbankacc_list;
            }
            dt_datatable.Dispose();
        }

        public void DaUpdateCreditSupplier(string employee_gid, MdlMstSupplier values)
        {
            msSQL = " select creditsupplier_gid,credit_gid,application_gid,supplier_gid,supplier_name,relationship_vintage_year,relationship_vintage_month," +
                    " format(purchase_amount,'en-IN') as purchase_amount,format(bankdebit_amount,'en-IN') as bankdebit_amount,relationship_supplier," +
                    " start_date,end_date " +
                    " from ocs_trn_tcadcreditsupplier where creditsupplier_gid='" + values.creditsupplier_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lssupplier_gid = objODBCDatareader["supplier_gid"].ToString();
                lssupplier_name = objODBCDatareader["supplier_name"].ToString();
                lsrelationship_vintage_year = objODBCDatareader["relationship_vintage_year"].ToString();
                lsrelationship_vintage_month = objODBCDatareader["relationship_vintage_month"].ToString();
                lspurchase_amount = objODBCDatareader["purchase_amount"].ToString();
                lsbankdebit_amount = objODBCDatareader["bankdebit_amount"].ToString();
                lsrelationship_supplier = objODBCDatareader["relationship_supplier"].ToString();
                lsstart_date = objODBCDatareader["start_date"].ToString();
                lsend_date = objODBCDatareader["end_date"].ToString();
                lscredit_gid = objODBCDatareader["credit_gid"].ToString();
                lsapplication_gid = objODBCDatareader["application_gid"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " update ocs_trn_tcadcreditsupplier set " +
                  " supplier_name='" + values.supplier_name + "'," +
                     " supplier_gid='" + values.supplier_gid + "',";
            if ((values.start_date == null) || (values.start_date == ""))
            {
                msSQL += "start_date=null,";
            }
            else
            {
                msSQL += "start_date= '" + Convert.ToDateTime(values.start_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if ((values.end_date == null) || (values.end_date == ""))
            {
                msSQL += "end_date=null,";
            }
            else
            {
                msSQL += "end_date= '" + Convert.ToDateTime(values.end_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += " relationship_vintage_year='" + values.relationship_vintage_year + "'," +
                        " relationship_vintage_month='" + values.relationship_vintage_month + "'," +
                     " purchase_amount='" + values.purchase_amount.Replace(",", "") + "'," +
                     " bankdebit_amount='" + values.bankdebit_amount.Replace(",", "") + "'," +
                      " relationship_supplier='" + values.relationship_supplier.Replace("'", " ") + "'," +
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where creditsupplier_gid='" + values.creditsupplier_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {//Date Updation
                if (Convert.ToDateTime(values.startdate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL = "update ocs_trn_tcadcreditsupplier set start_date='" + Convert.ToDateTime(values.startdate).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'" +
                        "where creditsupplier_gid='" + values.creditsupplier_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (Convert.ToDateTime(values.enddate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL = "update ocs_trn_tcadcreditsupplier set end_date='" + Convert.ToDateTime(values.enddate).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'" +
                        "where creditsupplier_gid='" + values.creditsupplier_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                msGetGid = objcmnfunctions.GetMasterGID("CSUL");

                msSQL = "Insert into ocs_trn_tcadcreditsupplierupdatelog(" +
               " creditsupplierupdatelog_gid, " +
               " creditsupplier_gid, " +
               " credit_gid, " +
               " application_gid, " +
               " supplier_gid," +
               " supplier_name," +
               " relationship_vintage_year, " +
               " relationship_vintage_month," +
               " purchase_amount," +
               " bankdebit_amount," +
               " relationship_supplier," +
               " start_date," +
               " end_date," +
               " created_by," +
               " created_date)" +
               " values (" +
               "'" + msGetGid + "'," +
               "'" + values.creditsupplier_gid + "'," +
               "'" + lscredit_gid + "'," +
               "'" + lsapplication_gid + "'," +
               "'" + lssupplier_gid + "'," +
               "'" + lssupplier_name + "'," +
               "'" + lsrelationship_vintage_year + "'," +
               "'" + lsrelationship_vintage_month + "'," +
               "'" + lspurchase_amount.Replace(",", "") + "'," +
               "'" + lsbankdebit_amount + "'," +
               "'" + lsrelationship_supplier.Replace("'", "") + "'," +
               "'" + lsstart_date + "'," +
               "'" + lsend_date + "'," +
               "'" + employee_gid + "'," +
               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Supplier Details Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating suppplier";
            }
        }

        //Buyer

        public void DaPostCreditBuyer(string employee_gid, MdlMstCreditBuyer values)
        {
            msSQL = "select buyer_gid from ocs_trn_tcadcreditbuyer where application_gid='" + values.application_gid + "' and buyer_gid='" + values.buyer_gid + "'";
            string lsbuyer_gid = objdbconn.GetExecuteScalar(msSQL);
            if (lsbuyer_gid == (values.buyer_gid))
            {
                values.status = false;
                values.message = "Already Buyer Added";
                return;
            }
            msGetGid = objcmnfunctions.GetMasterGID("CRBY");
            msSQL = " insert into ocs_trn_tcadcreditbuyer(" +
                   " creditbuyer_gid," +
                   " credit_gid," +
                   " application_gid," +
                   " buyer_gid ," +
                   " buyer_name," +
                   " buyer_limit," +
                   " availed_limit," +
                   " balance_limit ," +
                   " top_buyer," +
                   " bill_tenuredays," +
                   " margin," +
                   " relationship_vintage_year," +
                   " relationship_vintage_month ," +
                   " start_date," +
                   " end_date," +
                   " purchase_amount," +
                   " bankcredit_date  ," +
                   " bankcredit_value," +
                   " source_deduction  ," +
                   " relationship_borrower," +
                   " enduse_monitoring ," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.credit_gid + "'," +
                   "'" + values.application_gid + "'," +
                   "'" + values.buyer_gid + "'," +
                   "'" + values.buyer_name.Replace("'", " ") + "'," +
                   "'" + values.buyer_limit + "'," +
                   "'" + values.availed_limit + "'," +
                   "'" + values.balance_limit + "'," +
                   "'" + values.top_buyer + "'," +
                   "'" + values.bill_tenuredays.Replace("'", " ") + "'," +
                   "'" + values.margin.Replace("'", " ") + "'," +
                   "'" + values.relationship_vintage_year + "'," +
                   "'" + values.relationship_vintage_month + "'," +
                   "'" + Convert.ToDateTime(values.start_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   "'" + Convert.ToDateTime(values.end_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   "'" + values.purchase_amount.Replace(",", "") + "',";
            if (values.bankcredit_date == null || values.bankcredit_date == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.bankcredit_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + values.bankcredit_value.Replace(",", "") + "'," +
                    "'" + values.source_deduction.Replace("'", " ") + "',";
            if (values.relationship_borrower == null || values.relationship_borrower == "")
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.relationship_borrower.Replace("'", " ") + "',";
            }
            if (values.enduse_monitoring == null || values.enduse_monitoring == "")
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.enduse_monitoring.Replace("'", " ") + "',";
            }
            msSQL += "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Buyer details Added successfully";

                msSQL = " select creditbuyer_gid,credit_gid,application_gid,buyer_gid,buyer_name,relationship_vintage_year,relationship_vintage_month," +
                     " format(purchase_amount,'en-IN') as purchase_amount,format(buyer_limit,'en-IN')as buyer_limit,format(availed_limit,'en-IN')as availed_limit," +
                     " format(balance_limit,'en-IN')as balance_limit,top_buyer,bill_tenuredays,margin,bankcredit_value,source_deduction,relationship_borrower," +
                     " enduse_monitoring, date_format(start_date,'%d-%m-%Y') as start_date,date_format(end_date,'%d-%m-%Y') as end_date," +
                     " date_format(bankcredit_date,'%d-%m-%Y') as bankcredit_date from ocs_trn_tcadcreditbuyer where " +
                     " credit_gid='" + values.credit_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcreditbuyer_list = new List<creditbuyer_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcreditbuyer_list.Add(new creditbuyer_list
                        {
                            creditbuyer_gid = (dr_datarow["creditbuyer_gid"].ToString()),
                            credit_gid = (dr_datarow["credit_gid"].ToString()),
                            application_gid = (dr_datarow["application_gid"].ToString()),
                            buyer_gid = (dr_datarow["buyer_gid"].ToString()),
                            buyer_name = (dr_datarow["buyer_name"].ToString()),
                            relationship_vintage_year = (dr_datarow["relationship_vintage_year"].ToString()),
                            relationship_vintage_month = (dr_datarow["relationship_vintage_month"].ToString()),
                            purchase_amount = (dr_datarow["purchase_amount"].ToString()),
                            buyer_limit = (dr_datarow["buyer_limit"].ToString()),
                            availed_limit = (dr_datarow["availed_limit"].ToString()),
                            balance_limit = (dr_datarow["balance_limit"].ToString()),
                            top_buyer = (dr_datarow["top_buyer"].ToString()),
                            bill_tenuredays = (dr_datarow["bill_tenuredays"].ToString()),
                            margin = (dr_datarow["margin"].ToString()),
                            bankcredit_value = (dr_datarow["bankcredit_value"].ToString()),
                            source_deduction = (dr_datarow["source_deduction"].ToString()),
                            relationship_borrower = (dr_datarow["relationship_borrower"].ToString()),
                            enduse_monitoring = (dr_datarow["enduse_monitoring"].ToString()),
                            start_date = (dr_datarow["start_date"].ToString()),
                            end_date = (dr_datarow["end_date"].ToString()),
                            bankcredit_date = (dr_datarow["bankcredit_date"].ToString()),
                        });
                    }
                    values.creditbuyer_list = getcreditbuyer_list;
                }
                dt_datatable.Dispose();
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while Adding buyer";
            }
        }

        public void DaGetCreditBuyerList(string credit_gid, string employee_gid, MdlMstCreditBuyer values)
        {
            msSQL = " select creditbuyer_gid,credit_gid,application_gid,buyer_gid,buyer_name,relationship_vintage_year,relationship_vintage_month," +
                    " format(purchase_amount,'en-IN') as purchase_amount,format(buyer_limit,'en-IN')as buyer_limit,format(availed_limit,'en-IN')as availed_limit," +
                    " format(balance_limit,'en-IN')as balance_limit,top_buyer,bill_tenuredays,margin,bankcredit_value,source_deduction,relationship_borrower," +
                    " enduse_monitoring, date_format(start_date,'%d-%m-%Y') as start_date,date_format(end_date,'%d-%m-%Y') as end_date," +
                    " date_format(bankcredit_date,'%d-%m-%Y') as bankcredit_date, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                    " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as updated_by " +
                    " from ocs_trn_tcadcreditbuyer a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                    " where credit_gid='" + credit_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditbuyer_list = new List<creditbuyer_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcreditbuyer_list.Add(new creditbuyer_list
                    {
                        creditbuyer_gid = (dr_datarow["creditbuyer_gid"].ToString()),
                        credit_gid = (dr_datarow["credit_gid"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        buyer_gid = (dr_datarow["buyer_gid"].ToString()),
                        buyer_name = (dr_datarow["buyer_name"].ToString()),
                        relationship_vintage_year = (dr_datarow["relationship_vintage_year"].ToString()),
                        relationship_vintage_month = (dr_datarow["relationship_vintage_month"].ToString()),
                        purchase_amount = (dr_datarow["purchase_amount"].ToString()),
                        buyer_limit = (dr_datarow["buyer_limit"].ToString()),
                        availed_limit = (dr_datarow["availed_limit"].ToString()),
                        balance_limit = (dr_datarow["balance_limit"].ToString()),
                        top_buyer = (dr_datarow["top_buyer"].ToString()),
                        bill_tenuredays = (dr_datarow["bill_tenuredays"].ToString()),
                        margin = (dr_datarow["margin"].ToString()),
                        bankcredit_value = (dr_datarow["bankcredit_value"].ToString()),
                        source_deduction = (dr_datarow["source_deduction"].ToString()),
                        relationship_borrower = (dr_datarow["relationship_borrower"].ToString()),
                        enduse_monitoring = (dr_datarow["enduse_monitoring"].ToString()),
                        start_date = (dr_datarow["start_date"].ToString()),
                        end_date = (dr_datarow["end_date"].ToString()),
                        bankcredit_date = (dr_datarow["bankcredit_date"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        updated_date = (dr_datarow["updated_date"].ToString()),
                        updated_by = (dr_datarow["updated_by"].ToString()),
                    });
                }
                values.creditbuyer_list = getcreditbuyer_list;
            }
            dt_datatable.Dispose();
        }

        public void DaEditGetCreditBuyer(string creditbuyer_gid, MdlMstCreditBuyer values)
        {
            try
            {
                msSQL = " select creditbuyer_gid,credit_gid,application_gid,buyer_gid,buyer_name,relationship_vintage_year,relationship_vintage_month," +
                    " format(purchase_amount,'en-IN') as purchase_amount,format(buyer_limit,'en-IN')as buyer_limit,format(availed_limit,'en-IN')as availed_limit," +
                    " format(balance_limit,'en-IN')as balance_limit,top_buyer,bill_tenuredays,margin,bankcredit_value,source_deduction,relationship_borrower," +
                    " enduse_monitoring,start_date,end_date,bankcredit_date from ocs_trn_tcadcreditbuyer where creditbuyer_gid='" + creditbuyer_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.creditbuyer_gid = objODBCDatareader["creditbuyer_gid"].ToString();
                    values.credit_gid = objODBCDatareader["credit_gid"].ToString();
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.buyer_gid = objODBCDatareader["buyer_gid"].ToString();
                    values.buyer_name = objODBCDatareader["buyer_name"].ToString();
                    values.relationship_vintage_year = objODBCDatareader["relationship_vintage_year"].ToString();
                    values.relationship_vintage_month = objODBCDatareader["relationship_vintage_month"].ToString();
                    values.purchase_amount = objODBCDatareader["purchase_amount"].ToString();
                    values.buyer_limit = objODBCDatareader["buyer_limit"].ToString();
                    values.availed_limit = objODBCDatareader["availed_limit"].ToString();
                    values.balance_limit = objODBCDatareader["balance_limit"].ToString();
                    values.top_buyer = objODBCDatareader["top_buyer"].ToString();
                    values.bill_tenuredays = objODBCDatareader["bill_tenuredays"].ToString();
                    values.margin = objODBCDatareader["margin"].ToString();
                    values.bankcredit_value = objODBCDatareader["bankcredit_value"].ToString();
                    values.source_deduction = objODBCDatareader["source_deduction"].ToString();
                    values.relationship_borrower = objODBCDatareader["relationship_borrower"].ToString();
                    values.enduse_monitoring = objODBCDatareader["enduse_monitoring"].ToString();
                    if (objODBCDatareader["start_date"].ToString() == "")
                    {
                    }
                    else
                    {
                        values.start_date = Convert.ToDateTime(objODBCDatareader["start_date"]).ToString("MM-dd-yyyy");
                    }
                    if (objODBCDatareader["end_date"].ToString() == "")
                    {
                    }
                    else
                    {
                        values.end_date = Convert.ToDateTime(objODBCDatareader["end_date"]).ToString("MM-dd-yyyy");
                    }
                    if (objODBCDatareader["bankcredit_date"].ToString() == "")
                    {
                    }
                    else
                    {
                        values.bankcredit_date = Convert.ToDateTime(objODBCDatareader["bankcredit_date"]).ToString("MM-dd-yyyy");
                    }
                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaUpdateCreditBuyer(string employee_gid, MdlMstCreditBuyer values)
        {

            msSQL = " select creditbuyer_gid,credit_gid,application_gid,buyer_gid,buyer_name,relationship_vintage_year,relationship_vintage_month," +
                    " format(purchase_amount,'en-IN') as purchase_amount,format(buyer_limit,'en-IN')as buyer_limit,format(availed_limit,'en-IN')as availed_limit," +
                    " format(balance_limit,'en-IN')as balance_limit,top_buyer,bill_tenuredays,margin,bankcredit_value,source_deduction,relationship_borrower," +
                    " enduse_monitoring,start_date,end_date,bankcredit_date from ocs_trn_tcadcreditbuyer where creditbuyer_gid='" + values.creditbuyer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsbuyer_gid = objODBCDatareader["buyer_gid"].ToString();
                lsbuyer_name = objODBCDatareader["buyer_name"].ToString();
                lsrelationship_vintage_year = objODBCDatareader["relationship_vintage_year"].ToString();
                lsrelationship_vintage_month = objODBCDatareader["relationship_vintage_month"].ToString();
                lspurchase_amount = objODBCDatareader["purchase_amount"].ToString();
                lsbuyer_limit = objODBCDatareader["buyer_limit"].ToString();
                lsavailed_limit = objODBCDatareader["availed_limit"].ToString();
                lsbalance_limit = objODBCDatareader["balance_limit"].ToString();
                lstop_buyer = objODBCDatareader["top_buyer"].ToString();
                lsbill_tenuredays = objODBCDatareader["bill_tenuredays"].ToString();
                lsmargin = objODBCDatareader["margin"].ToString();
                lsbankcredit_value = objODBCDatareader["bankcredit_value"].ToString();
                lssource_deduction = objODBCDatareader["source_deduction"].ToString();
                lsrelationship_borrower = objODBCDatareader["relationship_borrower"].ToString();
                lsenduse_monitoring = objODBCDatareader["enduse_monitoring"].ToString();
                lsstart_date = objODBCDatareader["start_date"].ToString();
                lsend_date = objODBCDatareader["end_date"].ToString();
                lsbankcredit_date = objODBCDatareader["bankcredit_date"].ToString();
                lscredit_gid = objODBCDatareader["credit_gid"].ToString();
                lsapplication_gid = objODBCDatareader["application_gid"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " update ocs_trn_tcadcreditbuyer set " +
                 " buyer_gid='" + values.buyer_gid + "'," +
                     " buyer_name='" + values.buyer_name.Replace("'", " ") + "'," +
                     " relationship_vintage_year='" + values.relationship_vintage_year.Replace(",", "").Replace("'", " ") + "'," +
                     " relationship_vintage_month='" + values.relationship_vintage_month.Replace(",", "").Replace("'", " ") + "'," +
                     " purchase_amount='" + values.purchase_amount.Replace(",", "").Replace("'", " ") + "'," +
                     " buyer_limit='" + values.buyer_limit.Replace(",", "") + "'," +
                     " availed_limit='" + values.availed_limit.Replace(",", "") + "'," +
                     " balance_limit='" + values.balance_limit.Replace(",", "") + "'," +
                     " top_buyer='" + values.top_buyer + "'," +
                     " bill_tenuredays='" + values.bill_tenuredays.Replace("'", " ") + "'," +
                     " margin='" + values.margin.Replace("'", " ") + "'," +
                     " bankcredit_value='" + values.bankcredit_value.Replace(",", "").Replace("'", " ") + "'," +
                     " source_deduction='" + values.source_deduction.Replace("'", " ").Replace("'", " ") + "',";
            if (values.relationship_borrower == "" || values.relationship_borrower == null)
            {
                msSQL += " relationship_borrower='',";
            }
            else
            {
                msSQL += " relationship_borrower='" + values.relationship_borrower.Replace("'", " ") + "',";
            }
            if (values.enduse_monitoring == "" || values.enduse_monitoring == null)
            {
                msSQL += " enduse_monitoring='',";
            }
            else
            {
                msSQL += " enduse_monitoring='" + values.enduse_monitoring.Replace("'", " ") + "',";
            }

            msSQL += " updated_by='" + employee_gid + "'," +
             " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
             " where creditbuyer_gid='" + values.creditbuyer_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                //Date Updation
                if (Convert.ToDateTime(values.startdate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL = "update ocs_trn_tcadcreditbuyer set start_date='" + Convert.ToDateTime(values.startdate).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'" +
                        "where creditbuyer_gid='" + values.creditbuyer_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (Convert.ToDateTime(values.enddate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL = "update ocs_trn_tcadcreditbuyer set end_date='" + Convert.ToDateTime(values.enddate).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'" +
                        "where creditbuyer_gid='" + values.creditbuyer_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (Convert.ToDateTime(values.bankcreditdate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL = "update ocs_trn_tcadcreditbuyer set bankcredit_date='" + Convert.ToDateTime(values.bankcreditdate).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'" +
                        "where creditbuyer_gid='" + values.creditbuyer_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msGetGid = objcmnfunctions.GetMasterGID("CBUL");

                msSQL = "Insert into ocs_trn_tcadcreditbuyerupdatelog(" +
               " creditbuyerupdatelog_gid, " +
               " creditbuyer_gid, " +
               " credit_gid, " +
               " application_gid, " +
               " buyer_gid ," +
               " buyer_name," +
               " buyer_limit," +
                   " availed_limit," +
                   " balance_limit ," +
                   " top_buyer," +
                   " bill_tenuredays," +
                   " margin," +
                   " relationship_vintage_year," +
                   " relationship_vintage_month ," +
                   " purchase_amount," +
                   " bankcredit_value," +
                   " source_deduction  ," +
                   " relationship_borrower," +
                   " enduse_monitoring ," +
                   " start_date," +
                   " end_date," +
                   " bankcredit_date," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.creditbuyer_gid + "'," +
                   "'" + lscredit_gid + "'," +
                   "'" + lsapplication_gid + "'," +
                   "'" + lsbuyer_gid + "'," +
                   "'" + lsbuyer_name.Replace("'", " ") + "'," +
                   "'" + lsbuyer_limit + "'," +
                   "'" + lsavailed_limit + "'," +
                   "'" + lsbalance_limit + "'," +
                   "'" + lstop_buyer + "'," +
                   "'" + lsbill_tenuredays.Replace("'", " ") + "'," +
                   "'" + lsmargin.Replace("'", " ") + "'," +
                   "'" + lsrelationship_vintage_year + "'," +
                   "'" + lsrelationship_vintage_month + "'," +
                   "'" + lspurchase_amount.Replace("'", " ") + "'," +
                   "'" + lsbankcredit_value.Replace("'", " ") + "'," +
                   "'" + lssource_deduction.Replace("'", " ") + "'," +
                   "'" + lsrelationship_borrower.Replace("'", " ") + "'," +
                   "'" + lsenduse_monitoring.Replace("'", " ") + "'," +
                   "'" + lsstart_date + "'," +
                   "'" + lsend_date + "'," +
                   "'" + lsbankcredit_date + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Buyer Details Updated Successfully";
            }
        }

        public void DaDeleteCreditBuyer(string creditbuyer_gid, string credit_gid, MdlMstCreditBuyer values, string employee_gid)
        {
            msSQL = "delete from ocs_trn_tcadcreditbuyer where creditbuyer_gid='" + creditbuyer_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Buyer details Deleted successfully";

                msSQL = " select creditbuyer_gid,credit_gid,application_gid,buyer_gid,buyer_name,relationship_vintage_year,relationship_vintage_month," +
                    " format(purchase_amount,'en-IN') as purchase_amount,format(buyer_limit,'en-IN')as buyer_limit,format(availed_limit,'en-IN')as availed_limit," +
                    " format(balance_limit,'en-IN')as balance_limit,top_buyer,bill_tenuredays,margin,bankcredit_value,source_deduction,relationship_borrower," +
                    " enduse_monitoring, date_format(start_date,'%d-%m-%Y') as start_date,date_format(end_date,'%d-%m-%Y') as end_date," +
                     " date_format(bankcredit_date,'%d-%m-%Y') as bankcredit_date from ocs_trn_tcadcreditbuyer where " +
                    " credit_gid='" + credit_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcreditbuyer_list = new List<creditbuyer_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcreditbuyer_list.Add(new creditbuyer_list
                        {
                            creditbuyer_gid = (dr_datarow["creditbuyer_gid"].ToString()),
                            credit_gid = (dr_datarow["credit_gid"].ToString()),
                            application_gid = (dr_datarow["application_gid"].ToString()),
                            buyer_gid = (dr_datarow["buyer_gid"].ToString()),
                            buyer_name = (dr_datarow["buyer_name"].ToString()),
                            relationship_vintage_year = (dr_datarow["relationship_vintage_year"].ToString()),
                            relationship_vintage_month = (dr_datarow["relationship_vintage_month"].ToString()),
                            purchase_amount = (dr_datarow["purchase_amount"].ToString()),
                            buyer_limit = (dr_datarow["buyer_limit"].ToString()),
                            availed_limit = (dr_datarow["availed_limit"].ToString()),
                            balance_limit = (dr_datarow["balance_limit"].ToString()),
                            top_buyer = (dr_datarow["top_buyer"].ToString()),
                            bill_tenuredays = (dr_datarow["bill_tenuredays"].ToString()),
                            margin = (dr_datarow["margin"].ToString()),
                            bankcredit_value = (dr_datarow["bankcredit_value"].ToString()),
                            source_deduction = (dr_datarow["source_deduction"].ToString()),
                            relationship_borrower = (dr_datarow["relationship_borrower"].ToString()),
                            enduse_monitoring = (dr_datarow["enduse_monitoring"].ToString()),
                            start_date = (dr_datarow["start_date"].ToString()),
                            end_date = (dr_datarow["end_date"].ToString()),
                            bankcredit_date = (dr_datarow["bankcredit_date"].ToString()),
                        });
                    }
                    values.creditbuyer_list = getcreditbuyer_list;
                }
                dt_datatable.Dispose();
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred while deleting";
            }
        }

        //Bank Account
        public void DaGetCrediBankAccDtl(string credit_gid, string employee_gid, MdlCreditBankAcc values)
        {
            msSQL = " select creditbankdtl_gid,bank_name,branch_name,ifsc_code,bankaccount_number, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                    " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as updated_by " +
                    " from ocs_trn_tcadcreditbankdtl a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                    " where credit_gid='" + credit_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditbankacc_list = new List<creditbankacc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcreditbankacc_list.Add(new creditbankacc_list
                    {
                        bank_name = dt["bank_name"].ToString(),
                        branch_name = dt["branch_name"].ToString(),
                        ifsc_code = dt["ifsc_code"].ToString(),
                        bankaccount_number = dt["bankaccount_number"].ToString(),
                        creditbankdtl_gid = dt["creditbankdtl_gid"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        updated_date = dt["updated_date"].ToString(),
                        updated_by = dt["updated_by"].ToString(),
                    });
                    values.creditbankacc_list = getcreditbankacc_list;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaChequeTmpClear(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = "delete from ocs_trn_tcadcreditbankdtl2cheque where creditbankdtl_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            values.status = true;
        }


        public bool DaPostCreditBank(string employee_gid, MdlCreditBankAcc values)
        {

            msGetGid = objcmnfunctions.GetMasterGID("B2BK");
            msSQL = " insert into ocs_trn_tcadcreditbankdtl(" +
                    " creditbankdtl_gid," +
                    " credit_gid," +
                    " application_gid," +
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
                    " chequebook_status," +
                    " accountopen_date," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.credit_gid + "'," +
                    "'" + values.application_gid + "'," +
                    "'" + values.bank_name + "'," +
                    "'" + values.branch_name + "'," +
                    "'" + values.bank_address + "'," +
                    "'" + values.micr_code + "'," +
                    "'" + values.ifsc_code + "'," +
                    "'" + values.bankaccount_name + "'," +
                    "'" + values.bankaccounttype_gid + "'," +
                    "'" + values.bankaccounttype_name + "'," +
                    "'" + values.bankaccount_number + "'," +
                    "'" + values.confirmbankaccountnumber + "'," +
                    "'" + values.joint_account + "'," +
                    "'" + values.jointaccountholder_name + "'," +
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

            if (mnResult != 0)
            {
                msSQL = "update ocs_trn_tcadcreditbankdtl2cheque set creditbankdtl_gid='" + msGetGid + "' where creditbankdtl_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select creditbankdtl_gid,bank_name,branch_name,ifsc_code,bankaccount_number from " +
                          " ocs_trn_tcadcreditbankdtl where credit_gid='" + values.credit_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcreditbankacc_list = new List<creditbankacc_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getcreditbankacc_list.Add(new creditbankacc_list
                        {
                            bank_name = dt["bank_name"].ToString(),
                            branch_name = dt["branch_name"].ToString(),
                            ifsc_code = dt["ifsc_code"].ToString(),
                            bankaccount_number = dt["bankaccount_number"].ToString(),

                        });
                        values.creditbankacc_list = getcreditbankacc_list;
                    }
                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Bank Account Details Added Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Adding Bank Account Details";
                return false;
            }

        }

        public void DaDeletecreditBankAcc(string creditbankdtl_gid, string credit_gid, MdlCreditBankAcc values, string employee_gid)
        {
            msSQL = "delete from ocs_trn_tcadcreditbankdtl where creditbankdtl_gid='" + creditbankdtl_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = "delete from  ocs_trn_tcadcreditbankdtl2cheque where creditbankdtl_gid='" + creditbankdtl_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Bank Account Details Deleted successfully";

                msSQL = " select creditbankdtl_gid,bank_name,branch_name,ifsc_code,bankaccount_number from " +
                           " ocs_trn_tcadcreditbankdtl where credit_gid='" + credit_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcreditbankacc_list = new List<creditbankacc_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getcreditbankacc_list.Add(new creditbankacc_list
                        {
                            bank_name = dt["bank_name"].ToString(),
                            branch_name = dt["branch_name"].ToString(),
                            ifsc_code = dt["ifsc_code"].ToString(),
                            bankaccount_number = dt["bankaccount_number"].ToString(),

                        });
                        values.creditbankacc_list = getcreditbankacc_list;
                    }
                }
                dt_datatable.Dispose();
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred while deleting";
            }
        }


        public bool DachequeleafdocumentUpload(HttpRequest httpRequest, credituploaddocument objfilename, string employee_gid)
        {

            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string lsdocument_title = httpRequest.Form["document_title"].ToString();
            string lscreditbankdtl_gid = httpRequest.Form["creditbankdtl_gid"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();
            // string lsdocument_id = httpRequest.Form["document_id"].ToString();
            String path = lspath;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/CreditCheque/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                        string FileExtension = httpPostedFile.FileName;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        Stream ls_readStream;
                        ls_readStream = httpPostedFile.InputStream;
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);
                        //lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "Master/CreditCheque/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        //FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                        //ms.WriteTo(file);
                        //file.Close();
                        //ms.Close();

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/CreditCheque/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/CreditCheque/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "Master/InstitutionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("CRCQ");
                        msSQL = " insert into ocs_trn_tcadcreditbankdtl2cheque( " +
                                    " creditbankdtl2cheque_gid," +
                                    " creditbankdtl_gid," +
                                    " document_title," +
                                    " chequeleaf_name," +
                                    " chequeleaf_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                      "'" + lsdocument_title.Replace("'", "") + "'," +
                                    "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension.Replace("'", "") + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            objfilename.status = true;
                            objfilename.message = "Document Uploaded Successfully..!";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured..!";
                        }

                        msSQL = " select creditbankdtl2cheque_gid,creditbankdtl_gid,chequeleaf_name,chequeleaf_path,document_title from " +
                            " ocs_trn_tcadcreditbankdtl2cheque where creditbankdtl_gid='" + employee_gid + "' or  creditbankdtl_gid='" + lscreditbankdtl_gid + "'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        var getdocumentdtlList = new List<credituploaddocument_list>();
                        if (dt_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                getdocumentdtlList.Add(new credituploaddocument_list
                                {
                                    chequeleaf_name = dt["chequeleaf_name"].ToString(),
                                    document_title = dt["document_title"].ToString(),
                                    chequeleaf_path = objcmnstorage.EncryptData(dt["chequeleaf_path"].ToString()),
                                    creditbankdtl_gid = dt["creditbankdtl_gid"].ToString(),
                                    creditbankdtl2cheque_gid = dt["creditbankdtl2cheque_gid"].ToString(),

                                });
                                objfilename.credituploaddocument_list = getdocumentdtlList;
                            }
                        }
                        dt_datatable.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }
            return true;
        }

        public void DaDeleteCreditcheque(string creditbankdtl2cheque_gid, string credit_gid, credituploaddocument values, string employee_gid)
        {
            msSQL = "delete from ocs_trn_tcadcreditbankdtl2cheque where creditbankdtl2cheque_gid='" + creditbankdtl2cheque_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Document Deleted successfully";

                msSQL = " select creditbankdtl2cheque_gid,creditbankdtl_gid,chequeleaf_name,chequeleaf_path,document_title from " +
                         " ocs_trn_tcadcreditbankdtl2cheque where creditbankdtl_gid='" + employee_gid + "' or creditbankdtl_gid='" + credit_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdocumentdtlList = new List<credituploaddocument_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdocumentdtlList.Add(new credituploaddocument_list
                        {
                            chequeleaf_name = dt["chequeleaf_name"].ToString(),
                            document_title = dt["document_title"].ToString(),
                            chequeleaf_path = objcmnstorage.EncryptData(dt["chequeleaf_path"].ToString()),
                            creditbankdtl_gid = dt["creditbankdtl_gid"].ToString(),
                            creditbankdtl2cheque_gid = dt["creditbankdtl2cheque_gid"].ToString(),

                        });
                        values.credituploaddocument_list = getdocumentdtlList;
                    }
                }
                dt_datatable.Dispose();

            }
            else
            {
                values.status = false;
                values.message = "Error Occurred while deleting Document";
            }
        }

        public void DaEditGetCreditBankAccDtl(string creditbankdtl_gid, MdlCreditBankAcc values)
        {
            try
            {
                msSQL = "select creditbankdtl_gid,credit_gid,application_gid,bank_name,branch_name,bank_address,micr_code,ifsc_code,bankaccount_name," +
                " bankaccounttype_gid,bankaccounttype_name,bankaccount_number,confirmbankaccountnumber,joinaccount_status,joinaccount_name," +
                " chequebook_status,accountopen_date from ocs_trn_tcadcreditbankdtl where creditbankdtl_gid='" + creditbankdtl_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.creditbankdtl_gid = objODBCDatareader["creditbankdtl_gid"].ToString();
                    values.credit_gid = objODBCDatareader["credit_gid"].ToString();
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.bank_name = objODBCDatareader["bank_name"].ToString();
                    values.branch_name = objODBCDatareader["branch_name"].ToString();
                    values.bank_address = objODBCDatareader["bank_address"].ToString();
                    values.micr_code = objODBCDatareader["micr_code"].ToString();
                    values.ifsc_code = objODBCDatareader["ifsc_code"].ToString();
                    values.bankaccount_name = objODBCDatareader["bankaccount_name"].ToString();
                    values.chequebook_status = objODBCDatareader["chequebook_status"].ToString();
                    values.bankaccounttype_gid = objODBCDatareader["bankaccounttype_gid"].ToString();
                    values.bankaccounttype_name = objODBCDatareader["bankaccounttype_name"].ToString();
                    values.bankaccount_number = objODBCDatareader["bankaccount_number"].ToString();
                    values.confirmbankaccountnumber = objODBCDatareader["confirmbankaccountnumber"].ToString();
                    values.joint_account = objODBCDatareader["joinaccount_status"].ToString();
                    values.jointaccountholder_name = objODBCDatareader["joinaccount_name"].ToString();
                    values.chequebook_status = objODBCDatareader["chequebook_status"].ToString();

                    if (objODBCDatareader["accountopen_date"].ToString() == "")
                    {
                    }
                    else
                    {
                        values.accountopen_date = Convert.ToDateTime(objODBCDatareader["accountopen_date"]).ToString("MM-dd-yyyy");
                    }

                }
                values.status = true;

                msSQL = " select creditbankdtl2cheque_gid,creditbankdtl_gid,chequeleaf_name,chequeleaf_path,document_title from " +
                            " ocs_trn_tcadcreditbankdtl2cheque where  creditbankdtl_gid='" + creditbankdtl_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdocumentdtlList = new List<credituploaddocument_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdocumentdtlList.Add(new credituploaddocument_list
                        {
                            chequeleaf_name = dt["chequeleaf_name"].ToString(),
                            document_title = dt["document_title"].ToString(),
                            chequeleaf_path = objcmnstorage.EncryptData(dt["chequeleaf_path"].ToString()),
                            creditbankdtl_gid = dt["creditbankdtl_gid"].ToString(),
                            creditbankdtl2cheque_gid = dt["creditbankdtl2cheque_gid"].ToString(),

                        });
                        values.credituploaddocument_list = getdocumentdtlList;
                    }
                }
                dt_datatable.Dispose();
                values.message = "success";
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void UpdateCreditBankAccDtl(string employee_gid, MdlCreditBankAcc values)
        {

            msSQL = " select creditbankdtl_gid,credit_gid,application_gid,bank_name,branch_name,bank_address,micr_code,ifsc_code,bankaccount_name," +
                " bankaccounttype_gid,bankaccounttype_name,bankaccount_number,confirmbankaccountnumber,joinaccount_status,joinaccount_name," +
                " chequebook_status,accountopen_date from ocs_trn_tcadcreditbankdtl where creditbankdtl_gid='" + values.creditbankdtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsbank_name = objODBCDatareader["bank_name"].ToString();
                lsbranch_name = objODBCDatareader["branch_name"].ToString();
                lsbank_address = objODBCDatareader["bank_address"].ToString();
                lsmicr_code = objODBCDatareader["micr_code"].ToString();
                lsifsc_code = objODBCDatareader["ifsc_code"].ToString();
                lsbankaccount_name = objODBCDatareader["bankaccount_name"].ToString();
                lsbankaccounttype_gid = objODBCDatareader["bankaccounttype_gid"].ToString();
                lsbankaccounttype_name = objODBCDatareader["bankaccounttype_name"].ToString();
                lsbankaccount_number = objODBCDatareader["bankaccount_number"].ToString();
                lsconfirmbankaccountnumber = objODBCDatareader["confirmbankaccountnumber"].ToString();
                lsjoinaccount_status = objODBCDatareader["joinaccount_status"].ToString();
                lsjoinaccount_name = objODBCDatareader["joinaccount_name"].ToString();
                lschequebook_status = objODBCDatareader["chequebook_status"].ToString();
                lsaccountopen_date = objODBCDatareader["accountopen_date"].ToString();
                lscredit_gid = objODBCDatareader["credit_gid"].ToString();
                lsapplication_gid = objODBCDatareader["application_gid"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " update ocs_trn_tcadcreditbankdtl set " +
                     " bank_name='" + values.bank_name + "'," +
                     " branch_name='" + values.branch_name + "'," +
                     " bank_address='" + values.bank_address + "'," +
                     " micr_code='" + values.micr_code + "'," +
                     " ifsc_code='" + values.ifsc_code + "'," +
                     " bankaccount_name='" + values.bankaccount_name + "'," +
                     " bankaccounttype_gid='" + values.bankaccounttype_gid + "'," +
                     " bankaccounttype_name='" + values.bankaccounttype_name + "'," +
                     " bankaccount_number='" + values.bankaccount_number + "'," +
                     " confirmbankaccountnumber='" + values.confirmbankaccountnumber + "'," +
                     " joinaccount_status='" + values.joint_account + "'," +
                     " joinaccount_name='" + values.jointaccountholder_name + "'," +
                     " chequebook_status='" + values.chequebook_status + "'," +
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where creditbankdtl_gid='" + values.creditbankdtl_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {//Date Updation
                if (Convert.ToDateTime(values.accountopendate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL = "update ocs_trn_tcadcreditbankdtl set accountopen_date='" + Convert.ToDateTime(values.accountopendate).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'" +
                        "where creditbankdtl_gid='" + values.creditbankdtl_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                msSQL = "update ocs_trn_tcadcreditbankdtl2cheque set creditbankdtl_gid='" + values.creditbankdtl_gid + "' where creditbankdtl_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("CBDU");

                msSQL = " insert into ocs_trn_tcadcreditbankdtllog(" +
                    " creditbankdtllog_gid," +
                    " creditbankdtl_gid," +
                    " credit_gid," +
                    " application_gid," +
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
                    " chequebook_status," +
                    " accountopen_date," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.creditbankdtl_gid + "'," +
                    "'" + lscredit_gid + "'," +
                    "'" + lsapplication_gid + "'," +
                    "'" + lsbank_name + "'," +
                    "'" + lsbranch_name + "'," +
                    "'" + lsbank_address + "'," +
                    "'" + lsmicr_code + "'," +
                    "'" + lsifsc_code + "'," +
                    "'" + lsbankaccount_name + "'," +
                    "'" + lsbankaccounttype_gid + "'," +
                    "'" + lsbankaccounttype_name + "'," +
                    "'" + lsbankaccount_number + "'," +
                    "'" + lsconfirmbankaccountnumber + "'," +
                    "'" + lsjoinaccount_status + "'," +
                    "'" + lsjoinaccount_name + "'," +
                    "'" + lschequebook_status + "'," +
                    "'" + lsaccountopen_date + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Bank Account Details Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Bank Account";
            }
        }

        //Existing Bank Facility
        public void DaGetExistingBankFacility(string credit_gid, string employee_gid, MdlMstCUWExistingBankFacility values)
        {
            msSQL = " select existingbankfacility_gid,credit_gid,application_gid,a.bank_gid,bank_name,date_format(facilitysanctioned_on, '%d-%m-%Y') as facilitysanctioned_on, " +
                    " sanctioned_limit, instalmentfrequency_gid, instalmentfrequency_name, instalment_amount, outstanding_amount, " +
                    " date_format(record_date, '%d-%m-%Y') as record_date, overdue_amount, fundedtypeindicator_gid,fundedtypeindicator_name," +
                    " overdue_dpd, accountclassification_gid, account_classification, a.remarks, facilitytype_gid, facility_type, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                    " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as updated_by " +
                    " from ocs_trn_tcadcreditbankfacilitydtl a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                    " where credit_gid='" + credit_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcuwexistingbankfacility_list = new List<cuwexistingbankfacility_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcuwexistingbankfacility_list.Add(new cuwexistingbankfacility_list
                    {
                        existingbankfacility_gid = (dr_datarow["existingbankfacility_gid"].ToString()),
                        credit_gid = (dr_datarow["credit_gid"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        bank_gid = (dr_datarow["bank_gid"].ToString()),
                        bank_name = (dr_datarow["bank_name"].ToString()),
                        facilitysanctioned_on = (dr_datarow["facilitysanctioned_on"].ToString()),
                        fundedtypeindicator_gid = (dr_datarow["fundedtypeindicator_gid"].ToString()),
                        fundedtypeindicator_name = (dr_datarow["fundedtypeindicator_name"].ToString()),
                        sanctioned_limit = (dr_datarow["sanctioned_limit"].ToString()),
                        instalmentfrequency_gid = (dr_datarow["instalmentfrequency_gid"].ToString()),
                        instalmentfrequency_name = (dr_datarow["instalmentfrequency_name"].ToString()),
                        instalment_amount = (dr_datarow["instalment_amount"].ToString()),
                        outstanding_amount = (dr_datarow["outstanding_amount"].ToString()),
                        record_date = (dr_datarow["record_date"].ToString()),
                        overdue_amount = (dr_datarow["overdue_amount"].ToString()),
                        overdue_dpd = (dr_datarow["overdue_dpd"].ToString()),
                        accountclassification_gid = (dr_datarow["accountclassification_gid"].ToString()),
                        account_classification = (dr_datarow["account_classification"].ToString()),
                        remarks = (dr_datarow["remarks"].ToString()),
                        facilitytype_gid = (dr_datarow["facilitytype_gid"].ToString()),
                        facility_type = (dr_datarow["facility_type"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        updated_date = (dr_datarow["updated_date"].ToString()),
                        updated_by = (dr_datarow["updated_by"].ToString()),
                    });
                }
                values.cuwexistingbankfacility_list = getcuwexistingbankfacility_list;
            }
            dt_datatable.Dispose();
        }

        public void DaPostExistingBankFacility(string employee_gid, MdlMstCUWExistingBankFacility values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("CRFD");
            msSQL = " insert into ocs_trn_tcadcreditbankfacilitydtl(" +
                   " existingbankfacility_gid," +
                   " credit_gid," +
                   " application_gid," +
                   " bank_gid," +
                   " bank_name," +
                   " facilitytype_gid," +
                   " facility_type," +
                   " facilitysanctioned_on," +
                   " fundedtypeindicator_gid," +
                   " fundedtypeindicator_name," +
                   " sanctioned_limit," +
                   " instalmentfrequency_gid," +
                   " instalmentfrequency_name," +
                   " instalment_amount," +
                   " outstanding_amount," +
                   " record_date," +
                   " overdue_amount," +
                   " overdue_dpd ," +
                   " accountclassification_gid," +
                   " account_classification," +
                   " remarks," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.credit_gid + "'," +
                   "'" + values.application_gid + "'," +
                   "'" + values.bank_gid + "'," +
                   "'" + values.bank_name + "'," +
                   "'" + values.facilitytype_gid + "'," +
                   "'" + values.facility_type + "',";
            if ((values.facilitysanctioned_on == null) || (values.facilitysanctioned_on == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.facilitysanctioned_on).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + values.fundedtypeindicator_gid + "'," +
                   "'" + values.fundedtypeindicator_name + "'," +
                   "'" + values.sanctioned_limit + "'," +
                   "'" + values.instalmentfrequency_gid + "'," +
                   "'" + values.instalmentfrequency_name + "'," +
                   "'" + values.instalment_amount + "'," +
                   "'" + values.outstanding_amount + "',";
            if ((values.record_date == null) || (values.record_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.record_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + values.overdue_amount + "'," +
                   "'" + values.overdue_dpd + "'," +
                   "'" + values.accountclassification_gid + "'," +
                   "'" + values.account_classification + "',";
            if (values.remarks == null || values.remarks == "")
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.remarks.Replace("'", "") + "',";
            }
            msSQL += "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Existing Bank Facility Details Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Adding Existing Bank Facility";
            }
        }

        public void DaDeleteExistingBankFacility(string existingbankfacility_gid, MdlMstCUWExistingBankFacility values)
        {
            msSQL = "delete from ocs_trn_tcadcreditbankfacilitydtl where existingbankfacility_gid='" + existingbankfacility_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from ocs_trn_tcadcreditbankfacilitydtlupdatelog where existingbankfacility_gid='" + existingbankfacility_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Existing Bank Facility Details Deleted successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Deleting Existing Bank Facility Details";
            }
        }

        public void DaEditExistingBankFacility(string existingbankfacility_gid, MdlMstCUWExistingBankFacility values)
        {
            try
            {
                msSQL = " select existingbankfacility_gid,credit_gid,application_gid,bank_gid,bank_name, date_format(facilitysanctioned_on, '%d-%m-%Y') as facilitysanctioned_on, " +
                    " fundedtypeindicator_gid,fundedtypeindicator_name, sanctioned_limit, instalmentfrequency_gid, instalmentfrequency_name, " +
                    " instalment_amount, outstanding_amount, date_format(record_date, '%d-%m-%Y') as record_date, overdue_amount, " +
                   " overdue_dpd, accountclassification_gid, account_classification, remarks, facilitytype_gid, facility_type" +
                   " from ocs_trn_tcadcreditbankfacilitydtl where existingbankfacility_gid='" + existingbankfacility_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.existingbankfacility_gid = objODBCDatareader["existingbankfacility_gid"].ToString();
                    values.credit_gid = objODBCDatareader["credit_gid"].ToString();
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.bank_gid = objODBCDatareader["bank_gid"].ToString();
                    values.bank_name = objODBCDatareader["bank_name"].ToString();
                    values.facilitysanctioned_on = objODBCDatareader["facilitysanctioned_on"].ToString();
                    values.fundedtypeindicator_gid = objODBCDatareader["fundedtypeindicator_gid"].ToString();
                    values.fundedtypeindicator_name = objODBCDatareader["fundedtypeindicator_name"].ToString();
                    values.sanctioned_limit = objODBCDatareader["sanctioned_limit"].ToString();
                    values.instalmentfrequency_gid = objODBCDatareader["instalmentfrequency_gid"].ToString();
                    values.instalmentfrequency_name = objODBCDatareader["instalmentfrequency_name"].ToString();
                    values.instalment_amount = objODBCDatareader["instalment_amount"].ToString();
                    values.outstanding_amount = objODBCDatareader["outstanding_amount"].ToString();
                    values.record_date = objODBCDatareader["record_date"].ToString();
                    values.overdue_amount = objODBCDatareader["overdue_amount"].ToString();
                    values.overdue_dpd = objODBCDatareader["overdue_dpd"].ToString();
                    values.accountclassification_gid = objODBCDatareader["accountclassification_gid"].ToString();
                    values.account_classification = objODBCDatareader["account_classification"].ToString();
                    values.remarks = objODBCDatareader["remarks"].ToString();
                    values.facilitytype_gid = objODBCDatareader["facilitytype_gid"].ToString();
                    values.facility_type = objODBCDatareader["facility_type"].ToString();
                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaUpdateExistingBankFacility(string employee_gid, MdlMstCUWExistingBankFacility values)
        {
            msSQL = " select existingbankfacility_gid,credit_gid,application_gid,bank_gid,bank_name, date_format(facilitysanctioned_on, '%d-%m-%Y') as facilitysanctioned_on, " +
                   " fundedtypeindicator_gid,fundedtypeindicator_name, sanctioned_limit, instalmentfrequency_gid, instalmentfrequency_name, " +
                   " instalment_amount, outstanding_amount, date_format(record_date, '%d-%m-%Y') as record_date, overdue_amount, " +
                  " overdue_dpd, accountclassification_gid, account_classification, remarks, facilitytype_gid, facility_type" +
                  " from ocs_trn_tcadcreditbankfacilitydtl where existingbankfacility_gid='" + values.existingbankfacility_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsexistingbankfacility_gid = objODBCDatareader["existingbankfacility_gid"].ToString();
                lscredit_gid = objODBCDatareader["credit_gid"].ToString();
                lsapplication_gid = objODBCDatareader["application_gid"].ToString();
                lsbank_gid = objODBCDatareader["bank_gid"].ToString();
                lsbank_name = objODBCDatareader["bank_name"].ToString();
                lsfacilitysanctioned_on = objODBCDatareader["facilitysanctioned_on"].ToString();
                lsfundedtypeindicator_gid = objODBCDatareader["fundedtypeindicator_gid"].ToString();
                lsfundedtypeindicator_name = objODBCDatareader["fundedtypeindicator_name"].ToString();
                lssanction_limit = objODBCDatareader["sanctioned_limit"].ToString();
                lscreditinstalmentfrequency_gid = objODBCDatareader["instalmentfrequency_gid"].ToString();
                lscreditinstalmentfrequency_name = objODBCDatareader["instalmentfrequency_name"].ToString();
                lsinstalment_amount = objODBCDatareader["instalment_amount"].ToString();
                lsoutstanding_amount = objODBCDatareader["outstanding_amount"].ToString();
                lsrecord_date = objODBCDatareader["record_date"].ToString();
                lsoverdue_amount = objODBCDatareader["overdue_amount"].ToString();
                lsoverdueifany_dpd = objODBCDatareader["overdue_dpd"].ToString();
                lscreditaccountclassification_gid = objODBCDatareader["accountclassification_gid"].ToString();
                lscreditaccountclassification_name = objODBCDatareader["account_classification"].ToString();
                lsremarks = objODBCDatareader["remarks"].ToString();
                lsfacilitytype_gid = objODBCDatareader["facilitytype_gid"].ToString();
                lsfacility_type = objODBCDatareader["facility_type"].ToString();
            }

            msSQL = " update ocs_trn_tcadcreditbankfacilitydtl set " +
                    " bank_gid='" + values.bank_gid + "'," +
                    " bank_name='" + values.bank_name + "'," +
                    " facilitytype_gid='" + values.facilitytype_gid + "'," +
                    " facility_type='" + values.facility_type + "',";
            if (Convert.ToDateTime(values.facilitysanctionedon).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += " facilitysanctioned_on='" + Convert.ToDateTime(values.facilitysanctionedon).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
            }
            msSQL += " fundedtypeindicator_gid='" + values.fundedtypeindicator_gid + "'," +
                    " fundedtypeindicator_name='" + values.fundedtypeindicator_name + "'," +
                    " sanctioned_limit='" + values.sanctioned_limit + "'," +
                    " instalmentfrequency_gid='" + values.instalmentfrequency_gid + "'," +
                    " instalmentfrequency_name='" + values.instalmentfrequency_name + "'," +
                    " instalment_amount='" + values.instalment_amount + "'," +
                    " outstanding_amount='" + values.outstanding_amount + "',";
            if (Convert.ToDateTime(values.recorddate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += " record_date='" + Convert.ToDateTime(values.recorddate).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
            }
            msSQL += " overdue_amount='" + values.overdue_amount + "'," +
                    " overdue_dpd='" + values.overdue_dpd + "'," +
                    " accountclassification_gid='" + values.accountclassification_gid + "'," +
                    " account_classification='" + values.account_classification + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'," +
                    " updated_by='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where existingbankfacility_gid='" + values.existingbankfacility_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                msGetGid = objcmnfunctions.GetMasterGID("EBFU");

                msSQL = " insert into ocs_trn_tcadcreditbankfacilitydtlupdatelog(" +
                    " existingbankfacilityupdatelog_gid," +
                    " existingbankfacility_gid," +
                    " credit_gid," +
                    " application_gid," +
                    " bank_gid," +
                    " bank_name," +
                    " facilitytype_gid," +
                    " facility_type," +
                    " facilitysanctioned_on," +
                    " fundedtypeindicator_gid," +
                    " fundedtypeindicator_name," +
                    " sanction_limit," +
                    " creditinstalmentfrequency_gid," +
                    " creditinstalmentfrequency_name," +
                    " instalment_amount," +
                    " outstanding_amount," +
                    " record_date," +
                    " overdue_amount," +
                    " overdueifany_dpd," +
                    " creditaccountclassification_gid," +
                    " creditaccountclassification_name," +
                    " remarks," +
                    " created_by," +
                    " created_date)" +
                    " values (" +
                "'" + msGetGid + "'," +
                "'" + values.existingbankfacility_gid + "'," +
                "'" + lscredit_gid + "'," +
                "'" + lsapplication_gid + "'," +
                "'" + lsbank_gid + "'," +
                "'" + lsbank_name + "'," +
                "'" + lsfacilitytype_gid + "'," +
                "'" + lsfacility_type + "'," +
                "'" + lsfacilitysanctioned_on + "'," +
                "'" + lsfundedtypeindicator_gid + "'," +
                "'" + lsfundedtypeindicator_name + "'," +
                "'" + lssanction_limit + "'," +
                "'" + lscreditinstalmentfrequency_gid + "'," +
                "'" + lscreditinstalmentfrequency_name + "'," +
                "'" + lsinstalment_amount + "'," +
                "'" + lsoutstanding_amount + "'," +
                "'" + lsrecord_date + "'," +
                "'" + lsoverdue_amount + "'," +
                "'" + lsoverdueifany_dpd + "'," +
                "'" + lscreditaccountclassification_gid + "'," +
                "'" + lscreditaccountclassification_name + "'," +
                "'" + lsremarks.Replace("'", "") + "'," +
                "'" + employee_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Existing Bank Facility Details Updated Successfully";
            }
        }


        // Repayment Track
        public void DaPostRepaymentTrack(string employee_gid, MdlMstCUWRepaymentTrack values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("CRPY");
            msSQL = " insert into ocs_trn_tcadcreditrepaymentdtl(" +
                   " creditrepaymentdtl_gid," +
                   " credit_gid," +
                   " application_gid," +
                   " lendertype_gid," +
                   " lender_type," +
                   " ifsc_code," +
                   " bank_name," +
                   " nbfc_name," +
                   " branch_name," +
                   " facility_type ," +
                   " sanctionreference_id," +
                   " sanctioned_on," +
                   " sanction_amount," +
                   " accountstatus_on," +
                   " currentoutsatnding_amount," +
                   " instalment_frequency," +
                   " instalment_amount," +
                   " demanddue_date," +
                   " oringinaltenure_year," +
                   " oringinaltenure_month," +
                   " oringinaltenure_days ," +
                   " balancetenure_year," +
                   " balancetenure_month," +
                   " balancetenure_days ," +
                   " accountclassification_gid," +
                   " account_classification," +
                   " overdue_amount ," +
                   " numberofdays_overdue," +
                   " remarks," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.credit_gid + "'," +
                   "'" + values.application_gid + "'," +
                   "'" + values.lendertype_gid + "'," +
                   "'" + values.lender_type + "'," +
                   "'" + values.ifsc_code + "'," +
                   "'" + values.bank_name + "'," +
                   "'" + values.nbfc_name + "'," +
                   "'" + values.branch_name + "'," +
                   "'" + values.facility_type + "'," +
                   "'" + values.sanctionreference_id + "',";
            if ((values.sanctioned_on == null) || (values.sanctioned_on == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.sanctioned_on).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if (values.sanction_amount == null || values.sanction_amount == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.sanction_amount.Replace(",", "") + "',";
            }
            if ((values.accountstatus_on == null) || (values.accountstatus_on == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.accountstatus_on).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if (values.currentoutsatnding_amount == null || values.currentoutsatnding_amount == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.currentoutsatnding_amount.Replace(",", "") + "',";
            }
            msSQL += "'" + values.instalment_frequency + "',";
            if (values.instalment_amount == null || values.instalment_amount == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.instalment_amount.Replace(",", "") + "',";
            }
            if ((values.demanddue_date == null) || (values.demanddue_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.demanddue_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + values.oringinaltenure_year + "'," +
                    "'" + values.oringinaltenure_month + "'," +
                    "'" + values.oringinaltenure_days + "'," +
                    "'" + values.balancetenure_year + "'," +
                    "'" + values.balancetenure_month + "'," +
                    "'" + values.balancetenure_days + "'," +
                    "'" + values.accountclassification_gid + "'," +
                    "'" + values.account_classification + "',";
            if (values.overdue_amount == null || values.overdue_amount == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.overdue_amount.Replace(",", "") + "',";
            }
            msSQL += "'" + values.numberofdays_overdue + "',";
            if (values.remarks == null || values.remarks == "")
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.remarks.Replace("'", "") + "',";
            }
            msSQL += "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Repayment Track Details Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Adding Repayment Track";
            }
        }

        public void DaGetRepaymentTrack(string credit_gid, string employee_gid, MdlMstCUWRepaymentTrack values)
        {
            msSQL = " select creditrepaymentdtl_gid,lendertype_gid,lender_type,ifsc_code,bank_name,nbfc_name, branch_name,facility_type, sanctionreference_id," +
                    " date_format(sanctioned_on, '%d-%m-%Y') as sanctioned_on, sanction_amount, date_format(accountstatus_on, '%d-%m-%Y') as accountstatus_on, " +
                    " currentoutsatnding_amount, instalment_frequency, instalment_amount, date_format(demanddue_date, '%d-%m-%Y') as demanddue_date," +
                    " oringinaltenure_year, oringinaltenure_month, oringinaltenure_days, balancetenure_year, balancetenure_month, balancetenure_days, " +
                    " accountclassification_gid, account_classification, overdue_amount, numberofdays_overdue, a.remarks, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                    " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as updated_by " +
                    " from ocs_trn_tcadcreditrepaymentdtl a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                    " where credit_gid='" + credit_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcuwrepaymenttrack_list = new List<cuwrepaymenttrack_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcuwrepaymenttrack_list.Add(new cuwrepaymenttrack_list
                    {
                        creditrepaymentdtl_gid = (dr_datarow["creditrepaymentdtl_gid"].ToString()),
                        lendertype_gid = (dr_datarow["lendertype_gid"].ToString()),
                        lender_type = (dr_datarow["lender_type"].ToString()),
                        ifsc_code = (dr_datarow["ifsc_code"].ToString()),
                        bank_name = (dr_datarow["bank_name"].ToString()),
                        nbfc_name = (dr_datarow["nbfc_name"].ToString()),
                        branch_name = (dr_datarow["branch_name"].ToString()),
                        facility_type = (dr_datarow["facility_type"].ToString()),
                        sanctionreference_id = (dr_datarow["sanctionreference_id"].ToString()),
                        sanctioned_on = (dr_datarow["sanctioned_on"].ToString()),
                        sanction_amount = (dr_datarow["sanction_amount"].ToString()),
                        accountstatus_on = (dr_datarow["accountstatus_on"].ToString()),
                        currentoutsatnding_amount = (dr_datarow["currentoutsatnding_amount"].ToString()),
                        instalment_frequency = (dr_datarow["instalment_frequency"].ToString()),
                        instalment_amount = (dr_datarow["instalment_amount"].ToString()),
                        demanddue_date = (dr_datarow["demanddue_date"].ToString()),
                        oringinaltenure_year = (dr_datarow["oringinaltenure_year"].ToString()),
                        oringinaltenure_month = (dr_datarow["oringinaltenure_month"].ToString()),
                        oringinaltenure_days = (dr_datarow["oringinaltenure_days"].ToString()),
                        balancetenure_year = (dr_datarow["balancetenure_year"].ToString()),
                        balancetenure_month = (dr_datarow["balancetenure_month"].ToString()),
                        balancetenure_days = (dr_datarow["balancetenure_days"].ToString()),
                        accountclassification_gid = (dr_datarow["accountclassification_gid"].ToString()),
                        account_classification = (dr_datarow["account_classification"].ToString()),
                        overdue_amount = (dr_datarow["overdue_amount"].ToString()),
                        numberofdays_overdue = (dr_datarow["numberofdays_overdue"].ToString()),
                        remarks = (dr_datarow["remarks"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        updated_date = (dr_datarow["updated_date"].ToString()),
                        updated_by = (dr_datarow["updated_by"].ToString()),
                    });
                }
                values.cuwrepaymenttrack_list = getcuwrepaymenttrack_list;
            }
            dt_datatable.Dispose();
        }

        public void DaEditRepaymentTrack(string creditrepaymentdtl_gid, MdlMstCUWRepaymentTrack values)
        {
            try
            {
                msSQL = " select creditrepaymentdtl_gid,lendertype_gid,lender_type,ifsc_code,bank_name,nbfc_name, branch_name,facility_type," +
                     " sanctioned_on, sanction_amount, accountstatus_on, currentoutsatnding_amount, instalment_frequency, instalment_amount, " +
                     " demanddue_date, oringinaltenure_year, sanctionreference_id, accountclassification_gid, oringinaltenure_month, oringinaltenure_days, " +
                     " balancetenure_year, balancetenure_month, balancetenure_days, account_classification, overdue_amount, numberofdays_overdue, remarks" +
                     " from ocs_trn_tcadcreditrepaymentdtl where creditrepaymentdtl_gid='" + creditrepaymentdtl_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.creditrepaymentdtl_gid = objODBCDatareader["creditrepaymentdtl_gid"].ToString();
                    values.lendertype_gid = objODBCDatareader["lendertype_gid"].ToString();
                    values.lender_type = objODBCDatareader["lender_type"].ToString();
                    values.ifsc_code = objODBCDatareader["ifsc_code"].ToString();
                    values.bank_name = objODBCDatareader["bank_name"].ToString();
                    values.nbfc_name = objODBCDatareader["nbfc_name"].ToString();
                    values.branch_name = objODBCDatareader["branch_name"].ToString();
                    values.facility_type = objODBCDatareader["facility_type"].ToString();
                    values.sanctionreference_id = objODBCDatareader["sanctionreference_id"].ToString();
                    if (objODBCDatareader["sanctioned_on"].ToString() == "")
                    {
                    }
                    else
                    {
                        values.sanctioned_on = Convert.ToDateTime(objODBCDatareader["sanctioned_on"]).ToString("MM-dd-yyyy");
                    }
                    values.sanction_amount = objODBCDatareader["sanction_amount"].ToString();
                    if (objODBCDatareader["accountstatus_on"].ToString() == "")
                    {
                    }
                    else
                    {
                        values.accountstatus_on = Convert.ToDateTime(objODBCDatareader["accountstatus_on"]).ToString("MM-dd-yyyy");
                    }
                    values.currentoutsatnding_amount = objODBCDatareader["currentoutsatnding_amount"].ToString();
                    values.instalment_frequency = objODBCDatareader["instalment_frequency"].ToString();
                    values.instalment_amount = objODBCDatareader["instalment_amount"].ToString();
                    if (objODBCDatareader["demanddue_date"].ToString() == "")
                    {
                    }
                    else
                    {
                        values.demanddue_date = Convert.ToDateTime(objODBCDatareader["demanddue_date"]).ToString("MM-dd-yyyy");
                    }
                    values.oringinaltenure_year = objODBCDatareader["oringinaltenure_year"].ToString();
                    values.oringinaltenure_month = objODBCDatareader["oringinaltenure_month"].ToString();
                    values.oringinaltenure_days = objODBCDatareader["oringinaltenure_days"].ToString();
                    values.balancetenure_year = objODBCDatareader["balancetenure_year"].ToString();
                    values.balancetenure_month = objODBCDatareader["balancetenure_month"].ToString();
                    values.balancetenure_days = objODBCDatareader["balancetenure_days"].ToString();
                    values.accountclassification_gid = objODBCDatareader["accountclassification_gid"].ToString();
                    values.account_classification = objODBCDatareader["account_classification"].ToString();
                    values.overdue_amount = objODBCDatareader["overdue_amount"].ToString();
                    values.numberofdays_overdue = objODBCDatareader["numberofdays_overdue"].ToString();
                    values.remarks = objODBCDatareader["remarks"].ToString();
                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaUpdateRepaymentTrack(string employee_gid, MdlMstCUWRepaymentTrack values)
        {
            msSQL = " select creditrepaymentdtl_gid,lendertype_gid,lender_type,ifsc_code,bank_name,nbfc_name, branch_name,facility_type," +
                     " sanctioned_on, sanction_amount, accountstatus_on, currentoutsatnding_amount, instalment_frequency, instalment_amount,  " +
                     " demanddue_date, oringinaltenure_year, sanctionreference_id, accountclassification_gid, oringinaltenure_month, oringinaltenure_days, " +
                     " balancetenure_year, balancetenure_month, balancetenure_days, account_classification, overdue_amount, numberofdays_overdue, remarks" +
                     " from ocs_trn_tcadcreditrepaymentdtl where creditrepaymentdtl_gid='" + values.creditrepaymentdtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lscreditrepaymentdtl_gid = objODBCDatareader["creditrepaymentdtl_gid"].ToString();
                lslendertype_gid = objODBCDatareader["lendertype_gid"].ToString();
                lslender_type = objODBCDatareader["lender_type"].ToString();
                lsifsc_code = objODBCDatareader["ifsc_code"].ToString();
                lsbank_name = objODBCDatareader["bank_name"].ToString();
                lsnbfc_name = objODBCDatareader["nbfc_name"].ToString();
                lsbranch_name = objODBCDatareader["branch_name"].ToString();
                lsfacility_type = objODBCDatareader["facility_type"].ToString();
                lssanctionreference_id = objODBCDatareader["sanctionreference_id"].ToString();
                lssanctioned_on = objODBCDatareader["sanctioned_on"].ToString();
                lssanction_amount = objODBCDatareader["sanction_amount"].ToString();
                lsaccountstatus_on = objODBCDatareader["accountstatus_on"].ToString();
                lscurrentoutsatnding_amount = objODBCDatareader["currentoutsatnding_amount"].ToString();
                lsinstalment_frequency = objODBCDatareader["instalment_frequency"].ToString();
                lsinstalment_amount = objODBCDatareader["instalment_amount"].ToString();
                lsdemanddue_date = objODBCDatareader["demanddue_date"].ToString();
                lsoringinaltenure_year = objODBCDatareader["oringinaltenure_year"].ToString();
                lsoringinaltenure_month = objODBCDatareader["oringinaltenure_month"].ToString();
                lsoringinaltenure_days = objODBCDatareader["oringinaltenure_days"].ToString();
                lsbalancetenure_year = objODBCDatareader["balancetenure_year"].ToString();
                lsbalancetenure_month = objODBCDatareader["balancetenure_month"].ToString();
                lsbalancetenure_days = objODBCDatareader["balancetenure_days"].ToString();
                lsaccountclassification_gid = objODBCDatareader["accountclassification_gid"].ToString();
                lsaccount_classification = objODBCDatareader["account_classification"].ToString();
                lsoverdue_amount = objODBCDatareader["overdue_amount"].ToString();
                lsnumberofdays_overdue = objODBCDatareader["numberofdays_overdue"].ToString();
                lsremarks = objODBCDatareader["remarks"].ToString();
            }

            msSQL = " update ocs_trn_tcadcreditrepaymentdtl set " +
                    " lendertype_gid='" + values.lendertype_gid + "'," +
                    " lender_type='" + values.lender_type + "'," +
                    " ifsc_code='" + values.ifsc_code + "'," +
                    " bank_name='" + values.bank_name + "'," +
                    " nbfc_name='" + values.nbfc_name + "'," +
                    " branch_name='" + values.branch_name + "'," +
                    " facility_type='" + values.facility_type + "'," +
                    " sanctionreference_id='" + values.sanctionreference_id + "',";
            if (Convert.ToDateTime(values.sanctionedon).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += " sanctioned_on='" + Convert.ToDateTime(values.sanctionedon).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
            }
            if (values.sanction_amount == null || values.sanction_amount == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "sanction_amount='" + values.sanction_amount.Replace(",", "") + "',";
            }
            if (values.currentoutsatnding_amount == null || values.currentoutsatnding_amount == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "currentoutsatnding_amount='" + values.currentoutsatnding_amount.Replace(",", "") + "',";
            }
            msSQL += " instalment_frequency='" + values.instalment_frequency + "',";
            if (values.instalment_amount == null || values.instalment_amount == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "instalment_amount='" + values.instalment_amount.Replace(",", "") + "',";
            }
            if (Convert.ToDateTime(values.demandduedate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += " demanddue_date='" + Convert.ToDateTime(values.demandduedate).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
            }
            msSQL += " oringinaltenure_year='" + values.oringinaltenure_year + "',";
            if (Convert.ToDateTime(values.accountstatuson).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += " accountstatus_on='" + Convert.ToDateTime(values.accountstatuson).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
            }
            msSQL += " oringinaltenure_month='" + values.oringinaltenure_month + "'," +
                   " oringinaltenure_days='" + values.oringinaltenure_days + "'," +
                   " balancetenure_year='" + values.balancetenure_year + "'," +
                   " balancetenure_month='" + values.balancetenure_month + "'," +
                   " balancetenure_days='" + values.balancetenure_days + "'," +
                   " accountclassification_gid='" + values.accountclassification_gid + "'," +
                   " account_classification='" + values.account_classification + "',";
            if (values.overdue_amount == null || values.overdue_amount == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "overdue_amount='" + values.overdue_amount.Replace("'", "") + "',";
            }
            msSQL += " numberofdays_overdue='" + values.numberofdays_overdue + "'," +
                   " remarks='" + values.remarks.Replace("'", "") + "'," +
                   " updated_by='" + employee_gid + "'," +
                   " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                   " where creditrepaymentdtl_gid='" + values.creditrepaymentdtl_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                msGetGid = objcmnfunctions.GetMasterGID("RTUL");

                msSQL = " insert into ocs_trn_tcadcreditrepaymentdtlupdatelog(" +
                 " creditrepaymentdtlupdatelog_gid," +
                 " creditrepaymentdtl_gid," +
                 " credit_gid," +
                 " application_gid," +
                 " lendertype_gid," +
                 " lender_type," +
                 " ifsc_code," +
                 " bank_name," +
                 " nbfc_name," +
                 " branch_name," +
                 " facility_type ," +
                 " sanctionreference_id," +
                 " sanctioned_on," +
                 " sanction_amount," +
                 " accountstatus_on," +
                 " currentoutsatnding_amount," +
                 " instalment_frequency," +
                 " instalment_amount," +
                 " demanddue_date," +
                 " oringinaltenure_year," +
                 " oringinaltenure_month," +
                 " oringinaltenure_days ," +
                 " balancetenure_year," +
                 " balancetenure_month," +
                 " balancetenure_days ," +
                 " accountclassification_gid," +
                 " account_classification," +
                 " overdue_amount ," +
                 " numberofdays_overdue," +
                 " remarks," +
                 " created_by," +
                 " created_date)" +
                 " values(" +
                 "'" + msGetGid + "'," +
                 "'" + values.creditrepaymentdtl_gid + "'," +
                 "'" + values.credit_gid + "'," +
                 "'" + values.application_gid + "'," +
                 "'" + lslendertype_gid + "'," +
                 "'" + lslender_type + "'," +
                 "'" + lsifsc_code + "'," +
                 "'" + lsbank_name + "'," +
                 "'" + lsnbfc_name + "'," +
                 "'" + lsbranch_name + "'," +
                 "'" + lsfacility_type + "'," +
                 "'" + lssanctionreference_id + "'," +
                 "'" + lssanctioned_on + "'," +
                 "'" + lssanction_amount + "'," +
                 "'" + lsaccountstatus_on + "'," +
                 "'" + lscurrentoutsatnding_amount + "'," +
                 "'" + lsinstalment_frequency + "'," +
                 "'" + lsinstalment_amount + "'," +
                 "'" + lsdemanddue_date + "'," +
                 "'" + lsoringinaltenure_year + "'," +
                 "'" + lsoringinaltenure_month + "'," +
                 "'" + lsoringinaltenure_days + "'," +
                 "'" + lsbalancetenure_year + "'," +
                 "'" + lsbalancetenure_month + "'," +
                 "'" + lsbalancetenure_days + "'," +
                 "'" + lsaccountclassification_gid + "'," +
                 "'" + lsaccount_classification + "'," +
                 "'" + lsoverdue_amount + "'," +
                 "'" + lsnumberofdays_overdue + "'," +
                 "'" + lsremarks.Replace("'", "") + "'," +
                 "'" + employee_gid + "'," +
                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Repayment Track Details Updated Successfully";
            }
        }

        public void DaDeleteRepaymentTrack(string creditrepaymentdtl_gid, MdlMstCUWRepaymentTrack values)
        {
            msSQL = "delete from ocs_trn_tcadcreditrepaymentdtl where creditrepaymentdtl_gid='" + creditrepaymentdtl_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from ocs_trn_tcadcreditrepaymentdtlupdatelog where creditrepaymentdtl_gid='" + creditrepaymentdtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Repayment Track Details Deleted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Deleting Repayment Track Details";
            }
        }

        //Bank Statement

        public void DaImportExcelBankStatement(HttpRequest httpRequest, string employee_gid, result objResult)
        {
            try
            {
                HttpFileCollection httpFileCollection;
                DataTable dt = null;
                string lspath, lsfilePath, path, lsfile_name;
                string application_gid = httpRequest.Form["application_gid"];
                string credit_gid = httpRequest.Form["credit_gid"];
                string project_flag = httpRequest.Form["project_flag"].ToString();
                string lscompany_code = string.Empty;

                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                // Create Directory
                //lsfilePath = HttpContext.Current.Server.MapPath("erpdocument" + "/" + lscompany_code + "/Master/BankstatementDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

                lsfilePath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "/Master/BankstatementDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

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
                    return;
                }

                // path creation
                lspath = lsfilePath + "/";
                FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);
                using (ExcelPackage xlPackage = new ExcelPackage(ms))
                {
                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];
                    rowCount = worksheet.Dimension.End.Row;
                }
                file.Close();
                //ms.Close();
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/BankstatementDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                ms.Close();

                objcmnfunctions.uploadFile(lspath, lsfile_gid);

                //Excel To DataTable                
                lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";
                excelRange = "A2:N" + rowCount.ToString();
                dt = objcmnfunctions.ExcelToDataTable(lsfilePath, excelRange);

                foreach (DataRow row in dt.Rows)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("CBSA");

                    lsmonth = row["Month"].ToString();
                    lsyear = row["Year"].ToString();
                    lstotaldebits = row["Total Debits"].ToString();
                    lstotalcredits = row["Total Credits"].ToString();
                    lsaccttransferdebits = row["Account Transfer Debits"].ToString();
                    lsaccttransfercredits = row["Account Transfer Credits"].ToString();
                    lsloansrepayment = row["Loans Repayment"].ToString();
                    lscashdeposits = row["Cash Deposits"].ToString();
                    lspurchasepayments = row["Business transactions Purchase payments"].ToString();
                    lssalesreceipts = row["Business transactions Sales receipts"].ToString();
                    lschequeneftinward = row["Cheque/NEFT return Inward"].ToString();
                    lschequeneftoutward = row["Cheque/NEFT return Outward"].ToString();
                    lsoverdrawingscc = row["Over-drawings in CC"].ToString();
                    lssalesgst = row["Sales during month as per GST return"].ToString();

                    msSQL = " insert into ocs_trn_tcadbankstatementanalysis(" +
                            " bankstatementanalysis_gid," +
                            " application_gid," +
                            " credit_gid," +
                            " month," +
                            " year," +
                            " total_debits," +
                            " total_credits," +
                            " accttransfer_debits," +
                            " accttransfer_credits," +
                            " loans_repayment," +
                            " cash_deposits," +
                            " purchase_payments," +
                            " sales_receipets, " +
                            " chequeneft_inward," +
                            " chequeneft_outward," +
                            " overdrawings_cc," +
                            " sales_gst," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + application_gid + "'," +
                            "'" + credit_gid + "'," +
                            "'" + lsmonth + "'," +
                            "'" + lsyear + "'," +
                            "'" + lstotaldebits + "'," +
                            "'" + lstotalcredits + "'," +
                            "'" + lsaccttransferdebits + "'," +
                            "'" + lsaccttransfercredits + "'," +
                            "'" + lsloansrepayment + "'," +
                            "'" + lscashdeposits + "'," +
                            "'" + lspurchasepayments + "'," +
                            "'" + lssalesreceipts + "'," +
                            "'" + lschequeneftinward + "'," +
                            "'" + lschequeneftoutward + "'," +
                            "'" + lsoverdrawingscc + "'," +
                            "'" + lssalesgst + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (mnResult == 1)
                {
                    objResult.status = true;
                    objResult.message = "Excel Sheet Details Uploaded Successfully";
                }
                else
                {
                    objResult.status = false;
                    objResult.message = "Error occured in uploading Excel Sheet Details";
                }

                dt.Dispose();

            }
            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = ex.ToString();
            }
        }

        public void DaGetBankStatementList(string credit_gid, string application_gid, string employee_gid, MdlMstCUWBankStatement values)
        {
            msSQL = " select bankstatementanalysis_gid, credit_gid, application_gid, month, year, total_debits, total_credits, accttransfer_debits, accttransfer_credits," +
                    " accttransfer_credits, loans_repayment, cash_deposits, purchase_payments, sales_receipets, chequeneft_inward, " +
                    " chequeneft_outward, overdrawings_cc, sales_gst, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by " +
                    " from ocs_trn_tcadbankstatementanalysis a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " where credit_gid = '" + credit_gid + "' and a.application_gid='" + application_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getBankStatement_list = new List<BankStatement_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getBankStatement_list.Add(new BankStatement_list
                    {
                        bankstatementanalysis_gid = (dr_datarow["bankstatementanalysis_gid"].ToString()),
                        credit_gid = (dr_datarow["credit_gid"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        month = (dr_datarow["month"].ToString()),
                        year = (dr_datarow["year"].ToString()),
                        total_debits = (dr_datarow["total_debits"].ToString()),
                        total_credits = (dr_datarow["total_credits"].ToString()),
                        accttransfer_debits = (dr_datarow["accttransfer_debits"].ToString()),
                        accttransfer_credits = (dr_datarow["accttransfer_credits"].ToString()),
                        loans_repayment = (dr_datarow["loans_repayment"].ToString()),
                        cash_deposits = (dr_datarow["cash_deposits"].ToString()),
                        purchase_payments = (dr_datarow["purchase_payments"].ToString()),
                        sales_receipets = (dr_datarow["sales_receipets"].ToString()),
                        chequeneft_inward = (dr_datarow["chequeneft_inward"].ToString()),
                        chequeneft_outward = (dr_datarow["chequeneft_outward"].ToString()),
                        overdrawings_cc = (dr_datarow["overdrawings_cc"].ToString()),
                        sales_gst = (dr_datarow["sales_gst"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                    });
                }
                values.BankStatement_list = getBankStatement_list;
            }
            dt_datatable.Dispose();
        }

        public void DaBankStatementTemplateExport(BankStatementExportExcel objBankStatementExportExcel)
        {
            msSQL = "select month as ` Month`," + "year as `Year`," + "total_debits as `Total Debits`," + "total_credits as `Total Credits`," +
                    " accttransfer_debits as `Account Transfer Debits`," + "accttransfer_credits as `Account Transfer Credits`," +
                    " loans_repayment as `Loans Repayment`," + "cash_deposits as `Cash Deposits`," +
                    "purchase_payments as `Business transactions Purchase payments`," +
                    " sales_receipets  as `Business transactions Sales receipts`," + "chequeneft_inward as `Cheque/NEFT return Inward`, " +
                    " chequeneft_outward as `Cheque/NEFT return Outward`," + "overdrawings_cc as `Over-drawings in CC`," +
                    "sales_gst as `Sales during month as per GST return`" +
                    " from ocs_trn_tcadbankstatementanalysis a " +
                    " where credit_gid = '" + objBankStatementExportExcel.credit_gid + "' and a.application_gid='" + objBankStatementExportExcel.application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Bank Statement Analysis");
            try
            {
                msSQL = "select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objBankStatementExportExcel.lsname = "Bank Statement Analysis.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Bank Statement Analysis/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objBankStatementExportExcel.lscloudpath = lscompany_code + "/" + "Master/Bank Statement Analysis/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objBankStatementExportExcel.lsname;
                objBankStatementExportExcel.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/Bank Statement Analysis/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objBankStatementExportExcel.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                workSheet.Cells[1, 1].Value = "Bank Statement Analysis";
                using (var range = workSheet.Cells[1, 1, 1, 14])
                {
                    range.Merge = true;
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.Sienna);
                    range.Style.Font.Color.SetColor(Color.White);
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }
                workSheet.Cells[2, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objBankStatementExportExcel.lspath);
                using (var range = workSheet.Cells[2, 1, 2, 14])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objBankStatementExportExcel.lscloudpath, ms);
                ms.Close();
            }
            catch (Exception ex)
            {
                objBankStatementExportExcel.status = false;
                objBankStatementExportExcel.message = "Failure";
            }
            objBankStatementExportExcel.lscloudpath = objcmnstorage.EncryptData(objBankStatementExportExcel.lscloudpath);
            objBankStatementExportExcel.lspath = objcmnstorage.EncryptData(objBankStatementExportExcel.lspath);
            objBankStatementExportExcel.status = true;
            objBankStatementExportExcel.message = "Success";

        }

        //FSA
        //FSA Summary

        public void DaGetFSASUmmary(string credit_gid, string application_gid, MdlMstFSASummary values)
        {
            msSQL = "select a.template_name,a.document_name,a.document_path,a.credit_gid,a.application_gid," +
                " date_format(a.created_date,'%d-%m-%Y %H:%i %p')as created_date,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                " from ocs_trn_tcadfsaupload a, " +
                " hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                " and b.user_gid = c.user_gid and application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMstFSASummary_list = new List<MstFSASummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getMstFSASummary_list.Add(new MstFSASummary_list
                    {
                        template_name = dr_datarow["template_name"].ToString(),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                        credit_gid = dr_datarow["credit_gid"].ToString(),
                        application_gid = dr_datarow["application_gid"].ToString(),
                        created_date = dr_datarow["created_date"].ToString(),
                        created_by = (dr_datarow["created_by"].ToString()),
                    });
                }
                values.MstFSASummary_list = getMstFSASummary_list;
            }
            dt_datatable.Dispose();
        }


        //Balance sheet Template-1

        public void DaImportExcelBalanceSheetTemplate1(HttpRequest httpRequest, string employee_gid, result objResult)
        {
            try
            {
                HttpFileCollection httpFileCollection;
                DataTable dt = null;
                string lspath, lsfilePath;
                string application_gid = httpRequest.Form["application_gid"];
                string credit_gid = httpRequest.Form["credit_gid"];
                string lstemplate_type = httpRequest.Form["template_type"];
                string lscompany_code = string.Empty;
                string lsdate_type = string.Empty;
                string colName;
                string project_flag = httpRequest.Form["project_flag"].ToString();

                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                // Create Directory
                //lsfilePath = HttpContext.Current.Server.MapPath("erpdocument" + "/" + lscompany_code + "/Master/Balance Sheet Template 1/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
                lsfilePath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "/Master/Balance Sheet Template 1/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

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
                    return;
                }

                //path creation        
                lspath = lsfilePath + "/";
                FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);

                using (ExcelPackage xlPackage = new ExcelPackage(ms))
                {
                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];
                    rowCount = worksheet.Dimension.End.Row;
                    string endRange = worksheet.Dimension.End.Address;
                    colName = Regex.Replace(endRange, @"[\d-]", string.Empty);
                }

                file.Close();
                //ms.Close();
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/Balance Sheet Template 1/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                ms.Close();

                objcmnfunctions.uploadFile(lspath, lsfile_gid);

                //Excel To DataTable

                lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";

                lspath = "erpdocument" + "/" + lscompany_code + "/Master/Balance Sheet Template 1/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                msSQL = "select application_gid from ocs_trn_tcadfsaupload where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                   " and template_name='" + lstemplate_type + "'";
                string lsGID = objdbconn.GetExecuteScalar(msSQL);
                if (lsGID == null || lsGID == "")
                {
                    msGetGid = objcmnfunctions.GetMasterGID("FSAU");
                    msSQL = " insert into ocs_trn_tcadfsaupload(" +
                       " fasupload_gid," +
                       " template_name," +
                       " document_name," +
                       " document_path," +
                       " application_gid," +
                       " credit_gid," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid + "'," +
                       "'" + lstemplate_type.Replace(" ", "") + "'," +
                       "'" + httpPostedFile.FileName + "'," +
                       "'" + lspath + msdocument_gid + FileExtension + "'," +
                       "'" + application_gid + "'," +
                       "'" + credit_gid + "'," +
                        "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }
                else
                {
                    msSQL = "delete from ocs_trn_tcadfsaupload where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                    " and template_name='" + lstemplate_type + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        msSQL = "delete from ocs_trn_tcadcreditbalancesheet where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                   " and template_type='" + lstemplate_type + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msGetGid = objcmnfunctions.GetMasterGID("FSAU");
                        msSQL = " insert into ocs_trn_tcadfsaupload(" +
                                   " fasupload_gid," +
                                   " template_name," +
                                   " document_name," +
                                   " document_path," +
                                   " application_gid," +
                                   " credit_gid," +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + msGetGid + "'," +
                                   "'" + lstemplate_type.Replace(" ", "") + "'," +
                                   "'" + httpPostedFile.FileName + "'," +
                                   "'" + lspath + msdocument_gid + FileExtension + "'," +
                                   "'" + application_gid + "'," +
                                   "'" + credit_gid + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                excelRange = "A2:" + colName + rowCount.ToString();

                dt = objcmnfunctions.ExcelToDataTable(lsfilePath, excelRange);

                foreach (DataColumn column in dt.Columns)
                {

                    if (column.ToString() == "Field Name")
                    {

                    }
                    else
                    {
                        String[] colData = new String[rowCount];
                        int i = 0;
                        foreach (DataRow row in dt.Rows)
                        {

                            colData[i] = row[column.ToString()].ToString();
                            i++;
                        }

                        msGetGid = objcmnfunctions.GetMasterGID("CBS1");

                        msSQL = " insert into ocs_trn_tcadcreditbalancesheet(" +
                                " creditbalancesheet_gid," +
                                " application_gid," +
                                " credit_gid," +
                                " template_type," +
                                " allfiguresin_inr," +
                                " audit_type," +
                                " applicant_bank," +
                                " other_banks," +
                                " billpurchased_disc," +
                                " wcb_total," +
                                " shortterm_borrowings," +
                                " sundrycreditors_acceptances," +
                                " advancesreceived_customers," +
                                " provision_taxation," +
                                " dividend_taxthereon," +
                                " other_provisions," +
                                " instalmentsof_tl," +
                                " othercurrent_liabilitiesin1year," +
                                " dues_directors," +
                                " creditors_expenses," +
                                " unclaimed_debentures," +
                                " interestaccrued_loans," +
                                " othercurrent_liabilities," +
                                " total," +
                                " totalcurrent_liabilities," +
                                " debenturesin_1year," +
                                " prefshares_after1Year," +
                                " term_loans," +
                                " fixed_deposits," +
                                " otherterm_liabilities," +
                                " deferredtax_liability," +
                                " creditorscapital_expenses," +
                                " totallongterm_liabilities," +
                                " ordinaryshare_capital," +
                                " capitalredemption_reserve," +
                                " general_reserve," +
                                " investmentallowance_reserve," +
                                " surplusdeficitpandl_account," +
                                " prefsharecapital_debenture," +
                                " revaluation_reserve," +
                                " other_reserves," +
                                " capital_reserve," +
                                " sharepremium_account," +
                                " advancetowards_capital," +
                                " depositfriends_relatives," +
                                " totalnet_worth," +
                                " total_loabilities," +
                                " claimscmpyack_debts," +
                                " arrearsofsalary_susemployee," +
                                " estimated_capitalacct," +
                                " guaranteesbankers_company," +
                                " current_assets," +
                                " cash_balance," +
                                " bank_balance," +
                                " Govtandother_securities," +
                                " fixeddeposits_banks," +
                                " domestic_receivables," +
                                " export_receivables ," +
                                " instalmentsdeferred_receivables," +
                                " rawmaterials_indigenous," +
                                " rawmaterial_imported," +
                                " stock_inprocess," +
                                " finished_goods," +
                                " other_sparesindigenous," +
                                " other_sparesindimported," +
                                " advances_suppliers," +
                                " advancepayment_tax," +
                                " othercurrent_asset," +
                                " advances_recoverable," +
                                " interestservice_receivable," +
                                " incomeaccrued_investments," +
                                " others," +
                                " totalcurrent_assets," +
                                " land_building," +
                                " computers," +
                                " plant_machinery," +
                                " furnitures_fixtures," +
                                " otherfixed_assets," +
                                " capitalwip_machniery ," +
                                " less_depriciation," +
                                " net_block," +
                                " investmentssubsidiary_compy," +
                                " other_investments," +
                                " advancesto_subsidiaries," +
                                " receivables_sixmonths," +
                                " marginmoney_banks," +
                                " defferredrevenue_expenditure," +
                                " deposits_departments," +
                                " nonconsumablestores_spares," +
                                " othernoncurrent_assets," +
                                " total_othernoncurrentassets," +
                                " miscellaneous_expenses," +
                                " patents_good," +
                                " debitbalancepandl_account," +
                                " unsecureddebtors_doubtful," +
                                " total_intangibleassets," +
                                " total_assets," +
                                " totalliabilities_totalasset," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid + "'," +
                                "'" + application_gid + "'," +
                                "'" + credit_gid + "'," +
                                "'" + lstemplate_type + "'," +
                                "'" + colData[0] + "'," +
                                "'" + colData[1] + "'," +
                                "'" + colData[2] + "'," +
                                "'" + colData[3] + "'," +
                                "'" + colData[4] + "'," +
                                "'" + colData[5] + "'," +
                                "'" + colData[6] + "'," +
                                "'" + colData[7] + "'," +
                                "'" + colData[8] + "'," +
                                "'" + colData[9] + "'," +
                                "'" + colData[10] + "'," +
                                "'" + colData[11] + "'," +
                                "'" + colData[12] + "'," +
                                "'" + colData[13] + "'," +
                                "'" + colData[14] + "'," +
                                "'" + colData[15] + "'," +
                                "'" + colData[16] + "'," +
                                "'" + colData[17] + "'," +
                                "'" + colData[18] + "'," +
                                "'" + colData[19] + "'," +
                                "'" + colData[20] + "'," +
                                "'" + colData[22] + "'," +
                                "'" + colData[23] + "'," +
                                "'" + colData[24] + "'," +
                                "'" + colData[25] + "'," +
                                "'" + colData[26] + "'," +
                                "'" + colData[27] + "'," +
                                "'" + colData[28] + "'," +
                                "'" + colData[29] + "'," +
                                "'" + colData[31] + "'," +
                                "'" + colData[32] + "'," +
                                "'" + colData[33] + "'," +
                                "'" + colData[34] + "'," +
                                "'" + colData[35] + "'," +
                                "'" + colData[36] + "'," +
                                "'" + colData[37] + "'," +
                                "'" + colData[38] + "'," +
                                "'" + colData[39] + "'," +
                                "'" + colData[40] + "'," +
                                "'" + colData[41] + "'," +
                                "'" + colData[42] + "'," +
                                "'" + colData[43] + "'," +
                                "'" + colData[44] + "'," +
                                "'" + colData[46] + "'," +
                                "'" + colData[47] + "'," +
                                "'" + colData[48] + "'," +
                                "'" + colData[49] + "'," +
                                "'" + colData[51] + "'," +
                                "'" + colData[52] + "'," +
                                "'" + colData[53] + "'," +
                                "'" + colData[55] + "'," +
                                "'" + colData[56] + "'," +
                                "'" + colData[58] + "'," +
                                "'" + colData[59] + "'," +
                                "'" + colData[60] + "'," +
                                "'" + colData[62] + "'," +
                                "'" + colData[63] + "'," +
                                "'" + colData[64] + "'," +
                                "'" + colData[65] + "'," +
                                "'" + colData[66] + "'," +
                                "'" + colData[67] + "'," +
                                "'" + colData[68] + "'," +
                                "'" + colData[69] + "'," +
                                "'" + colData[70] + "'," +
                                "'" + colData[71] + "'," +
                                "'" + colData[72] + "'," +
                                "'" + colData[73] + "'," +
                                "'" + colData[74] + "'," +
                                "'" + colData[75] + "'," +
                                "'" + colData[77] + "'," +
                                "'" + colData[78] + "'," +
                                "'" + colData[79] + "'," +
                                "'" + colData[80] + "'," +
                                "'" + colData[81] + "'," +
                                "'" + colData[82] + "'," +
                                "'" + colData[83] + "'," +
                                "'" + colData[84] + "'," +
                                "'" + colData[86] + "'," +
                                "'" + colData[87] + "'," +
                                "'" + colData[88] + "'," +
                                "'" + colData[89] + "'," +
                                "'" + colData[90] + "'," +
                                "'" + colData[91] + "'," +
                                "'" + colData[92] + "'," +
                                "'" + colData[93] + "'," +
                                "'" + colData[94] + "'," +
                                "'" + colData[95] + "'," +
                                "'" + colData[97] + "'," +
                                "'" + colData[98] + "'," +
                                "'" + colData[99] + "'," +
                                "'" + colData[100] + "'," +
                                "'" + colData[101] + "'," +
                                "'" + colData[102] + "'," +
                                "'" + colData[103] + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }


                if (mnResult == 1)
                {
                    objResult.status = true;
                    objResult.message = "Excel Sheet Uploaded successfully";
                }
                else
                {
                    objResult.status = false;
                    objResult.message = "Error occured in uploading Excel Sheet Details";
                }

                dt.Dispose();

            }
            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = ex.ToString();
            }
        }

        //Balance sheet Template-2

        public void DaImportExcelBalanceSheetTemplate2(HttpRequest httpRequest, string employee_gid, result objResult)
        {
            try
            {
                HttpFileCollection httpFileCollection;
                DataTable dt = null;
                string lspath, lsfilePath;
                string application_gid = httpRequest.Form["application_gid"];
                string credit_gid = httpRequest.Form["credit_gid"];
                string lstemplate_type = httpRequest.Form["template_type"];
                string lscompany_code = string.Empty;
                string lsdate_type = string.Empty;
                string colName;
                string project_flag = httpRequest.Form["project_flag"].ToString();

                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                // Create Directory
                //lsfilePath = HttpContext.Current.Server.MapPath("erpdocument" + "/" + lscompany_code + "/Master/Balance Sheet Template 2/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
                lsfilePath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "/Master/Balance Sheet Template 2/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

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
                    return;
                }

                //path creation        
                lspath = lsfilePath + "/";
                FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);

                using (ExcelPackage xlPackage = new ExcelPackage(ms))
                {
                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];
                    rowCount = worksheet.Dimension.End.Row;
                    string endRange = worksheet.Dimension.End.Address;
                    colName = Regex.Replace(endRange, @"[\d-]", string.Empty);
                }

                file.Close();
                //ms.Close();
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/Balance Sheet Template 2/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                ms.Close();

                objcmnfunctions.uploadFile(lspath, lsfile_gid);

                //Excel To DataTable

                lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";

                lspath = "erpdocument" + "/" + lscompany_code + "/Master/Balance Sheet Template 2/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                msSQL = "select application_gid from ocs_trn_tcadfsaupload where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                   " and template_name='" + lstemplate_type + "'";
                string lsGID = objdbconn.GetExecuteScalar(msSQL);
                if (lsGID == null || lsGID == "")
                {
                    msGetGid = objcmnfunctions.GetMasterGID("FSAU");
                    msSQL = " insert into ocs_trn_tcadfsaupload(" +
                       " fasupload_gid," +
                       " template_name," +
                       " document_name," +
                       " document_path," +
                       " application_gid," +
                       " credit_gid," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid + "'," +
                       "'" + lstemplate_type.Replace(" ", "") + "'," +
                       "'" + httpPostedFile.FileName + "'," +
                       "'" + lspath + msdocument_gid + FileExtension + "'," +
                       "'" + application_gid + "'," +
                       "'" + credit_gid + "'," +
                        "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }
                else
                {
                    msSQL = "delete from ocs_trn_tcadfsaupload where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                    " and template_name='" + lstemplate_type + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        msSQL = "delete from ocs_trn_tcadcreditbalancesheet2 where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                   " and template_type='" + lstemplate_type + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msGetGid = objcmnfunctions.GetMasterGID("FSAU");
                        msSQL = " insert into ocs_trn_tcadfsaupload(" +
                                   " fasupload_gid," +
                                   " template_name," +
                                   " document_name," +
                                   " document_path," +
                                   " application_gid," +
                                   " credit_gid," +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + msGetGid + "'," +
                                   "'" + lstemplate_type.Replace(" ", "") + "'," +
                                   "'" + httpPostedFile.FileName + "'," +
                                   "'" + lspath + msdocument_gid + FileExtension + "'," +
                                   "'" + application_gid + "'," +
                                   "'" + credit_gid + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                excelRange = "A2:" + colName + rowCount.ToString();

                dt = objcmnfunctions.ExcelToDataTable(lsfilePath, excelRange);

                foreach (DataColumn column in dt.Columns)
                {

                    if (column.ToString() == "All figures in INR")
                    {

                    }
                    else
                    {
                        lsdate_type = column.ToString();
                        String[] colData = new String[rowCount];
                        int i = 0;
                        foreach (DataRow row in dt.Rows)
                        {

                            colData[i] = row[column.ToString()].ToString();
                            i++;
                        }

                        msGetGid = objcmnfunctions.GetMasterGID("CBS2");

                        msSQL = " insert into ocs_trn_tcadcreditbalancesheet2(" +
                                " creditbalancesheet2_gid," +
                                " application_gid," +
                                " credit_gid," +
                                " template_type," +
                                " allfiguresin_inr," +
                                " audit_type," +
                                " demand_deposits," +
                                " term_deposits," +
                                " other_deposits," +
                                " deposits_total," +
                                " shortterm_borrowings," +
                                " term_borrowings," +
                                " other_borrowings," +
                                " borrowings_total," +
                                " provision_taxation," +
                                " provisionstandard_assets," +
                                " provisionnon_assets," +
                                " interestaccrued_loans," +
                                " creditors_expenses," +
                                " other_provisions," +
                                " othercurrent_liabilities," +
                                " deferredtax_liability," +
                                " otherprovision_total," +
                                " ordinaryshare_capital," +
                                " capitalredemption_reserve," +
                                " general_reserve," +
                                " investment_allowancereserve," +
                                " surplusdeficitpandl_account," +
                                " prefsharecapital_debenture," +
                                " revaluation_reserve," +
                                " other_reserves," +
                                " capital_reserves," +
                                " sharepremium_acct," +
                                " advancetowards_capital," +
                                " deposit_relatives," +
                                " net_worth," +
                                " total_liabilities," +
                                " claimscmpyack_debts," +
                                " arrearsofsalary_susemployee," +
                                " estimated_capitalacct," +
                                " guaranteesbankers_company," +
                                " incurrent_account," +
                                " fixeddeposits_banks," +
                                " othercash_bankbalance," +
                                " cashbankbalance_total," +
                                " government_securities," +
                                " otherapproved_securities," +
                                " shares," +
                                " debentures_bonds," +
                                " subsidiariesjoint_ventures," +
                                " other_investments," +
                                " investments_total," +
                                " loanspayable_demand," +
                                " term_loans," +
                                " other_advances," +
                                " advances_total," +
                                " interest_accrued," +
                                " advancetax_deducted ," +
                                " asset_others," +
                                " othersssets_total," +
                                " land_building," +
                                " computers," +
                                " plant_machinery," +
                                " furnitures_fixtures," +
                                " otherfixed_assets," +
                                " capital_wip," +
                                " less_depreciation," +
                                " net_block," +
                                " miscellaneous_expenses," +
                                " patents_good," +
                                " debitbalancepandl_account," +
                                " unsecureddebtors_doubtful," +
                                " total_intangibleassets," +
                                " total_assets," +
                                " totalliabilities_totalasset," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid + "'," +
                                "'" + application_gid + "'," +
                                "'" + credit_gid + "'," +
                                "'" + lstemplate_type + "'," +
                                "'" + lsdate_type + "'," +
                                "'" + colData[0] + "'," +
                                "'" + colData[3] + "'," +
                                "'" + colData[4] + "'," +
                                "'" + colData[5] + "'," +
                                "'" + colData[6] + "'," +
                                "'" + colData[8] + "'," +
                                "'" + colData[9] + "'," +
                                "'" + colData[10] + "'," +
                                "'" + colData[11] + "'," +
                                "'" + colData[13] + "'," +
                                "'" + colData[14] + "'," +
                                "'" + colData[15] + "'," +
                                "'" + colData[16] + "'," +
                                "'" + colData[17] + "'," +
                                "'" + colData[18] + "'," +
                                "'" + colData[19] + "'," +
                                "'" + colData[20] + "'," +
                                "'" + colData[21] + "'," +
                                "'" + colData[22] + "'," +
                                "'" + colData[23] + "'," +
                                "'" + colData[24] + "'," +
                                "'" + colData[25] + "'," +
                                "'" + colData[26] + "'," +
                                "'" + colData[27] + "'," +
                                "'" + colData[28] + "'," +
                                "'" + colData[29] + "'," +
                                "'" + colData[30] + "'," +
                                "'" + colData[31] + "'," +
                                "'" + colData[32] + "'," +
                                "'" + colData[33] + "'," +
                                "'" + colData[34] + "'," +
                                "'" + colData[35] + "'," +
                                "'" + colData[37] + "'," +
                                "'" + colData[38] + "'," +
                                "'" + colData[39] + "'," +
                                "'" + colData[40] + "'," +
                                "'" + colData[43] + "'," +
                                "'" + colData[44] + "'," +
                                "'" + colData[45] + "'," +
                                "'" + colData[46] + "'," +
                                "'" + colData[48] + "'," +
                                "'" + colData[49] + "'," +
                                "'" + colData[50] + "'," +
                                "'" + colData[51] + "'," +
                                "'" + colData[52] + "'," +
                                "'" + colData[53] + "'," +
                                "'" + colData[54] + "'," +
                                "'" + colData[56] + "'," +
                                "'" + colData[57] + "'," +
                                "'" + colData[58] + "'," +
                                "'" + colData[59] + "'," +
                                "'" + colData[61] + "'," +
                                "'" + colData[62] + "'," +
                                "'" + colData[63] + "'," +
                                "'" + colData[64] + "'," +
                                "'" + colData[66] + "'," +
                                "'" + colData[67] + "'," +
                                "'" + colData[68] + "'," +
                                "'" + colData[69] + "'," +
                                "'" + colData[70] + "'," +
                                "'" + colData[71] + "'," +
                                "'" + colData[72] + "'," +
                                "'" + colData[73] + "'," +
                                "'" + colData[75] + "'," +
                                "'" + colData[76] + "'," +
                                "'" + colData[77] + "'," +
                                "'" + colData[78] + "'," +
                                "'" + colData[79] + "'," +
                                "'" + colData[80] + "'," +
                                "'" + colData[81] + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }


                if (mnResult == 1)
                {
                    objResult.status = true;
                    objResult.message = "Excel Sheet Uploaded successfully";
                }
                else
                {
                    objResult.status = false;
                    objResult.message = "Error occured in uploading Excel Sheet Details";
                }

                dt.Dispose();

            }
            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = ex.ToString();
            }
        }

        // P & L Template 2

        public void DaImportProfitLossTemp2(HttpRequest httpRequest, string employee_gid, result objResult)
        {
            try
            {
                HttpFileCollection httpFileCollection;
                DataTable dt = null;
                string lspath, lsfilePath;
                string application_gid = httpRequest.Form["application_gid"];
                string credit_gid = httpRequest.Form["credit_gid"];
                string template_name = httpRequest.Form["template_name"];
                string project_flag = httpRequest.Form["project_flag"].ToString();

                msSQL = " select company_code from adm_mst_tcompany";
                string lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                // Create Directory
                //lsfilePath = HttpContext.Current.Server.MapPath("erpdocument" + "/" + lscompany_code + "/Master/P&LTemp2/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
                lsfilePath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "/Master/P&LTemp2/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

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
                    return;
                }

                //path creation        
                lspath = lsfilePath + "/";
                FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);
                string colName;
                using (ExcelPackage xlPackage = new ExcelPackage(ms))
                {
                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];
                    rowCount = worksheet.Dimension.End.Row;
                    columnCount = worksheet.Dimension.End.Column;
                    string endRange = worksheet.Dimension.End.Address;
                    colName = Regex.Replace(endRange, @"[\d-]", string.Empty);
                }
                file.Close();
                //ms.Close();
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/P&LTemp2/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                ms.Close();

                objcmnfunctions.uploadFile(lspath, lsfile_gid);

                //Excel To DataTable

                lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";
                lspath = "erpdocument" + "/" + lscompany_code + "/Master/P&LTemp2/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                msSQL = "select application_gid from ocs_trn_tcadfsaupload where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                    " and template_name='" + template_name + "'";
                string lsGID = objdbconn.GetExecuteScalar(msSQL);
                if (lsGID == null || lsGID == "")
                {
                    msGetGid = objcmnfunctions.GetMasterGID("FSAU");
                    msSQL = " insert into ocs_trn_tcadfsaupload(" +
                       " fasupload_gid," +
                       " application_gid," +
                       " credit_gid," +
                       " template_name," +
                       " document_name," +
                       " document_path," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid + "'," +
                       "'" + application_gid + "'," +
                       "'" + credit_gid + "'," +
                       "'" + template_name.Replace(" ", "") + "'," +
                       "'" + httpPostedFile.FileName + "'," +
                       "'" + lspath + msdocument_gid + FileExtension + "'," +
                        "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }
                else
                {
                    msSQL = "delete from ocs_trn_tcadfsaupload where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                    " and template_name='" + template_name + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        msSQL = "delete from ocs_trn_tcadcreditprofitlosstemp2 where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                   " and template_name='" + template_name + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msGetGid = objcmnfunctions.GetMasterGID("FSAU");
                        msSQL = " insert into ocs_trn_tcadfsaupload(" +
                                   " fasupload_gid," +
                                   " template_name," +
                                   " document_name," +
                                   " document_path," +
                                   " application_gid," +
                                   " credit_gid," +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + msGetGid + "'," +
                                   "'" + template_name.Replace(" ", "") + "'," +
                                   "'" + httpPostedFile.FileName + "'," +
                                   "'" + lspath + msdocument_gid + FileExtension + "'," +
                                   "'" + application_gid + "'," +
                                   "'" + credit_gid + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                excelRange = "A2:" + colName + rowCount.ToString();

                dt = objcmnfunctions.ExcelToDataTable(lsfilePath, excelRange);


                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "All figures in INR")
                    {

                    }
                    else
                    {
                        string lsdate_type = column.ToString();
                        String[] colData = new String[rowCount];
                        int i = 0;
                        foreach (DataRow row in dt.Rows)
                        {

                            colData[i] = row[column.ToString()].ToString();
                            i++;
                        }
                        msGetGid = objcmnfunctions.GetMasterGID("CPLT");

                        msSQL = " insert into ocs_trn_tcadcreditprofitlosstemp2(" +
                                " credirprofitlosstemp2_gid," +
                                " template_name," +
                                " application_gid," +
                                " credit_gid," +
                                " allfiguresin_inr," +
                                " audited," +
                                " interest_income," +
                                " incomeon_investments," +
                                " interest_others," +
                                " income_others," +
                                " totalinterest_earned," +
                                " other_income," +
                                " profit_sales," +
                                " miscellaneous_income," +
                                " totalother_income," +
                                " total_income," +
                                " increase_income," +
                                " expenditure," +
                                " expenedinterest_borrower," +
                                " expenedinterest_deposit," +
                                " expened_other," +
                                " totalinterest_expened," +
                                " operating_expenses," +
                                " employee_cost," +
                                " depreciation," +
                                " other_operating_cost," +
                                " total_operating_expenses," +
                                " provision_asset," +
                                " provision_nonasset," +
                                " provision_tax," +
                                " other_provision," +
                                " total_provision," +
                                " total_expenditure," +
                                " pbt," +
                                " income_tax," +
                                " pat," +
                                " amount," +
                                " rent," +
                                " retained_profit," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid + "'," +
                                "'" + template_name.Replace(" ", "") + "'," +
                                "'" + application_gid + "'," +
                                "'" + credit_gid + "'," +
                                "'" + lsdate_type + "'," +
                                "'" + colData[0] + "'," +
                                "'" + colData[3] + "'," +
                                "'" + colData[4] + "'," +
                                "'" + colData[5] + "'," +
                                "'" + colData[6] + "'," +
                                "'" + colData[7] + "'," +
                                "'" + colData[8] + "'," +
                                "'" + colData[10] + "'," +
                                "'" + colData[11] + "'," +
                                "'" + colData[12] + "'," +
                                "'" + colData[13] + "'," +
                                "'" + colData[14] + "'," +
                                "'" + colData[15] + "'," +
                                "'" + colData[17] + "'," +
                                "'" + colData[18] + "'," +
                                "'" + colData[19] + "'," +
                                "'" + colData[20] + "'," +
                                "'" + colData[21] + "'," +
                                "'" + colData[22] + "'," +
                                "'" + colData[23] + "'," +
                                "'" + colData[24] + "'," +
                                "'" + colData[25] + "'," +
                                "'" + colData[27] + "'," +
                                "'" + colData[28] + "'," +
                                "'" + colData[29] + "'," +
                                "'" + colData[30] + "'," +
                                "'" + colData[31] + "'," +
                                "'" + colData[32] + "'," +
                                "'" + colData[33] + "'," +
                                "'" + colData[34] + "'," +
                                "'" + colData[35] + "'," +
                                "'" + colData[36] + "'," +
                                "'" + colData[37] + "'," +
                                "'" + colData[38] + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                if (mnResult == 1)
                {
                    objResult.status = true;
                    objResult.message = "Excel Sheet Uploaded successfully";
                }
                else
                {
                    objResult.status = false;
                    objResult.message = "Error occured in uploading Excel Sheet Details";
                }

                dt.Dispose();
            }
            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = ex.ToString();
            }
        }

        //P & L Template 1

        public void DaImportProfitLoss(HttpRequest httpRequest, string employee_gid, result objResult)
        {
            try
            {
                HttpFileCollection httpFileCollection;
                DataTable dt = null;
                string lspath, lsfilePath;
                string application_gid = httpRequest.Form["application_gid"];
                string credit_gid = httpRequest.Form["credit_gid"];
                string template_name = httpRequest.Form["template_name"];
                string project_flag = httpRequest.Form["project_flag"].ToString();

                msSQL = " select company_code from adm_mst_tcompany";
                string lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                // Create Directory
                //lsfilePath = HttpContext.Current.Server.MapPath("erpdocument" + "/" + lscompany_code + "/Master/P&L/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
                lsfilePath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "/Master/P&L/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

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
                    return;
                }

                //path creation        
                lspath = lsfilePath + "/";
                FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);
                string colName;
                using (ExcelPackage xlPackage = new ExcelPackage(ms))
                {
                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];
                    rowCount = worksheet.Dimension.End.Row;
                    columnCount = worksheet.Dimension.End.Column;
                    string endRange = worksheet.Dimension.End.Address;
                    colName = Regex.Replace(endRange, @"[\d-]", string.Empty);
                }
                file.Close();
                //ms.Close();

                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/SummaryTemplate/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                ms.Close();

                objcmnfunctions.uploadFile(lspath, lsfile_gid);

                //Excel To DataTable

                lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";
                lspath = "erpdocument" + "/" + lscompany_code + "/Master/P&L/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                msSQL = "select application_gid from ocs_trn_tcadfsaupload where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                    " and template_name='" + template_name + "'";
                string lsGID = objdbconn.GetExecuteScalar(msSQL);
                if (lsGID == null || lsGID == "")
                {
                    msGetGid = objcmnfunctions.GetMasterGID("FSAU");
                    msSQL = " insert into ocs_trn_tcadfsaupload(" +
                       " fasupload_gid," +
                       " application_gid," +
                       " credit_gid," +
                       " template_name," +
                       " document_name," +
                       " document_path," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid + "'," +
                       "'" + application_gid + "'," +
                       "'" + credit_gid + "'," +
                       "'" + template_name.Replace(" ", "") + "'," +
                       "'" + httpPostedFile.FileName + "'," +
                       "'" + lspath + msdocument_gid + FileExtension + "'," +
                        "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }
                else
                {
                    msSQL = "delete from ocs_trn_tcadfsaupload where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                    " and template_name='" + template_name + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        msSQL = "delete from ocs_trn_tcadcreditprofitloss where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                   " and template_name='" + template_name + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msGetGid = objcmnfunctions.GetMasterGID("FSAU");
                        msSQL = " insert into ocs_trn_tcadfsaupload(" +
                                   " fasupload_gid," +
                                   " template_name," +
                                   " document_name," +
                                   " document_path," +
                                   " application_gid," +
                                   " credit_gid," +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + msGetGid + "'," +
                                   "'" + template_name.Replace(" ", "") + "'," +
                                   "'" + httpPostedFile.FileName + "'," +
                                   "'" + lspath + msdocument_gid + FileExtension + "'," +
                                   "'" + application_gid + "'," +
                                   "'" + credit_gid + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                excelRange = "A3:" + colName + rowCount.ToString();

                dt = objcmnfunctions.ExcelToDataTable(lsfilePath, excelRange);


                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "All figures in INR")
                    {

                    }
                    else
                    {
                        string lsdate_type = column.ToString();
                        String[] colData = new String[rowCount];
                        int i = 0;
                        foreach (DataRow row in dt.Rows)
                        {

                            colData[i] = row[column.ToString()].ToString();
                            i++;
                        }
                        msGetGid = objcmnfunctions.GetMasterGID("CPLT");

                        msSQL = " insert into ocs_trn_tcadcreditprofitloss(" +
                                " credirprofitloss_gid," +
                                " template_name," +
                                " application_gid," +
                                " credit_gid," +
                                " allfiguresin_inr, " +
                                " audited," +
                                " domestic_sales," +
                                " export_sales," +
                                " totalgross_sales," +
                                " excise_duty," +
                                " otheroperating_income," +
                                " other_income," +
                                " net_sales," +
                                " increasenet_sales," +
                                " import_rawmaterial," +
                                " indigenous_rawmaterial," +
                                " import_spares," +
                                " indigenous_spares," +
                                " power_fuel," +
                                " direct_labour," +
                                " othersoperating_expenses," +
                                " depreciation," +
                                " repair_maintenance," +
                                " rent," +
                                " otherdirect_cost," +
                                " totalcost_sales," +
                                " openingstock_progress," +
                                " closingstock_progress," +
                                " costof_production," +
                                " copnet_sales," +
                                " openingstock_finishedgoods," +
                                " closingstock_finishedgoods," +
                                " costof_sales," +
                                " cosnet_sales," +
                                " sgam_expenses," +
                                " pbit," +
                                " pbitnet_sales," +
                                " interest_finance_charges," +
                                " interest_financenet_sales," +
                                " pbt," +
                                " pbtnet_sales," +
                                " interest_earned," +
                                " mics_receipts," +
                                " divdend," +
                                " profitsales_invetments," +
                                " exchange_gain," +
                                " total_nonoperative_income," +
                                " wrieoff_provision," +
                                " proir_year," +
                                " baddebts_written_off," +
                                " othernoncash_expenses," +
                                " othernonoperating_exoeses," +
                                " total_nonoperative_expenses," +
                                " profitbefore_tax," +
                                " current_tax," +
                                " deferred_tax," +
                                " pat," +
                                " net_profitloss," +
                                " amount," +
                                " rate," +
                                " retained_profit," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid + "'," +
                                "'" + template_name.Replace(" ", "") + "'," +
                                "'" + application_gid + "'," +
                                "'" + credit_gid + "'," +
                                "'" + lsdate_type + "'," +
                                "'" + colData[0] + "'," +
                                "'" + colData[2] + "'," +
                                "'" + colData[3] + "'," +
                                "'" + colData[4] + "'," +
                                "'" + colData[5] + "'," +
                                "'" + colData[6] + "'," +
                                "'" + colData[7] + "'," +
                                "'" + colData[8] + "'," +
                                "'" + colData[9] + "'," +
                                "'" + colData[12] + "'," +
                                "'" + colData[13] + "'," +
                                "'" + colData[15] + "'," +
                                "'" + colData[16] + "'," +
                                "'" + colData[17] + "'," +
                                "'" + colData[18] + "'," +
                                "'" + colData[19] + "'," +
                                "'" + colData[20] + "'," +
                                "'" + colData[21] + "'," +
                                "'" + colData[22] + "'," +
                                "'" + colData[23] + "'," +
                                "'" + colData[24] + "'," +
                                "'" + colData[25] + "'," +
                                "'" + colData[26] + "'," +
                                "'" + colData[27] + "'," +
                                "'" + colData[28] + "'," +
                                "'" + colData[29] + "'," +
                                "'" + colData[30] + "'," +
                                "'" + colData[31] + "'," +
                                "'" + colData[32] + "'," +
                                "'" + colData[33] + "'," +
                                "'" + colData[34] + "'," +
                                "'" + colData[35] + "'," +
                                "'" + colData[36] + "'," +
                                "'" + colData[37] + "'," +
                                "'" + colData[38] + "'," +
                                "'" + colData[39] + "'," +
                                "'" + colData[41] + "'," +
                                "'" + colData[42] + "'," +
                                "'" + colData[43] + "'," +
                                "'" + colData[44] + "'," +
                                "'" + colData[45] + "'," +
                                "'" + colData[46] + "'," +
                                "'" + colData[48] + "'," +
                                "'" + colData[49] + "'," +
                                "'" + colData[50] + "'," +
                                "'" + colData[51] + "'," +
                                "'" + colData[52] + "'," +
                                "'" + colData[53] + "'," +
                                "'" + colData[54] + "'," +
                                "'" + colData[55] + "'," +
                                "'" + colData[56] + "'," +
                                "'" + colData[57] + "'," +
                                "'" + colData[58] + "'," +
                                "'" + colData[60] + "'," +
                                "'" + colData[61] + "'," +
                                "'" + colData[62] + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                if (mnResult == 1)
                {
                    objResult.status = true;
                    objResult.message = "Excel Sheet Uploaded successfully";
                }
                else
                {
                    objResult.status = false;
                    objResult.message = "Error occured in uploading Excel Sheet Details";
                }

                dt.Dispose();
            }
            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = ex.ToString();
            }
        }

        //Summary Template-1

        public void DaImportExcelSummaryTemplate1(HttpRequest httpRequest, string employee_gid, result objResult)
        {
            try
            {
                HttpFileCollection httpFileCollection;
                DataTable dt = null;
                string lspath, lsfilePath, path, lsfile_name;
                string application_gid = httpRequest.Form["application_gid"];
                string credit_gid = httpRequest.Form["credit_gid"];
                string template_name = httpRequest.Form["template_name"];
                string lscompany_code = string.Empty;
                string project_flag = httpRequest.Form["project_flag"].ToString();

                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                // Create Directory
                //lsfilePath = HttpContext.Current.Server.MapPath("erpdocument" + "/" + lscompany_code + "/Master/SummaryTemplate/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
                lsfilePath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "/Master/SummaryTemplate/" + DateTime.Now.Year + "/" + DateTime.Now.Month;


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
                    return;
                }

                // path creation
                lspath = lsfilePath + "/";
                FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);
                using (ExcelPackage xlPackage = new ExcelPackage(ms))
                {
                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];
                    rowCount = worksheet.Dimension.End.Row;
                    columnCount = worksheet.Dimension.End.Column;
                    endRange = worksheet.Dimension.End.Address;
                    colName = Regex.Replace(endRange, @"[\d-]", string.Empty);
                }
                file.Close();
                //ms.Close();

                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/SummaryTemplate/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                ms.Close();


                objcmnfunctions.uploadFile(lspath, lsfile_gid);

                //Excel To DataTable                
                lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";
                excelRange = "A3:" + endRange;

                msSQL = "select application_gid from ocs_trn_tcadfsaupload where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                   " and template_name='" + template_name + "'";
                string lsGID = objdbconn.GetExecuteScalar(msSQL);

                lspath = "erpdocument" + "/" + lscompany_code + "/Master/SummaryTemplate/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                if (lsGID == null || lsGID == "")
                {
                    msGetGid = objcmnfunctions.GetMasterGID("FSAU");
                    msSQL = " insert into ocs_trn_tcadfsaupload(" +
                       " fasupload_gid," +
                       " template_name," +
                       " document_name," +
                       " document_path," +
                       " application_gid," +
                       " credit_gid," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid + "'," +
                       "'" + template_name.Replace(" ", "") + "'," +
                       "'" + httpPostedFile.FileName + "'," +
                       "'" + lspath + msdocument_gid + FileExtension + "'," +
                       "'" + application_gid + "'," +
                       "'" + credit_gid + "'," +
                        "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }
                else
                {
                    msSQL = "delete from ocs_trn_tcadfsaupload where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                    " and template_name='" + template_name + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        msSQL = "delete from ocs_trn_tcadsummarytemplate1 where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                                " and template_name='" + template_name + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msGetGid = objcmnfunctions.GetMasterGID("FSAU");
                        msSQL = " insert into ocs_trn_tcadfsaupload(" +
                                   " fasupload_gid," +
                                   " template_name," +
                                   " document_name," +
                                   " document_path," +
                                   " application_gid," +
                                   " credit_gid," +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + msGetGid + "'," +
                                   "'" + template_name.Replace(" ", "") + "'," +
                                   "'" + httpPostedFile.FileName + "'," +
                                   "'" + lspath + msdocument_gid + FileExtension + "'," +
                                   "'" + application_gid + "'," +
                                   "'" + credit_gid + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }


                dt = objcmnfunctions.ExcelToDataTable(lsfilePath, excelRange);



                columnInsertCount = 0;

                foreach (DataColumn column in dt.Columns)
                {

                    if (column.ToString() == "Particulars")
                    {

                    }
                    else
                    {
                        string lsdate = column.ToString();
                        String[] colData = new String[30];
                        int i = 0;
                        foreach (DataRow row in dt.Rows)
                        {

                            colData[i] = row[column.ToString()].ToString();
                            i++;
                        }

                        msGetGid = objcmnfunctions.GetMasterGID("STL1");

                        msSQL = " insert into ocs_trn_tcadsummarytemplate1(" +
                       " summarytemplate1_gid," +
                       " credit_gid," +
                       " application_gid," +
                       " template_name," +

                       " date," +
                       " audit_type," +
                       " net_sales," +
                       " other_income," +
                       " total_revenue," +
                       " growth_in_revenues," +
                       " ebitda," +
                       " ebitda_margin," +
                       " depreciation," +
                       " interest," +
                       " pat," +
                       " pat_margin," +

                       " total_outside_liabilities," +
                       " total_bank_borrowings," +
                       " tangible_net_worth," +
                       " current_ratio," +
                       " tol_tnw," +
                       " interest_coverage_ratio," +
                       " dscr," +
                       " sundry_creditors," +
                       " sundry_debtors," +
                       " inventories," +
                       " payable_noofdays," +
                       " recievable_noofdays," +
                       " inventory_noofdays," +

                       " workingcapital_noofdays," +
                       " debt_ebitda," +
                       " msme," +

                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid + "'," +
                       "'" + credit_gid + "'," +
                       "'" + application_gid + "'," +
                       "'" + template_name.Replace(" ", "") + "'," +


                       "'" + lsdate + "'," +
                       "'" + colData[0] + "'," +
                       "'" + colData[1] + "'," +
                       "'" + colData[2] + "'," +
                       "'" + colData[3] + "'," +
                       "'" + colData[4] + "'," +
                       "'" + colData[5] + "'," +
                       "'" + colData[6] + "'," +
                       "'" + colData[7] + "'," +
                       "'" + colData[8] + "'," +
                       "'" + colData[9] + "'," +

                       "'" + colData[10] + "'," +
                       "'" + colData[11] + "'," +
                       "'" + colData[12] + "'," +
                       "'" + colData[13] + "'," +
                       "'" + colData[14] + "'," +
                       "'" + colData[15] + "'," +
                       "'" + colData[16] + "'," +
                       "'" + colData[17] + "'," +
                       "'" + colData[18] + "'," +
                       "'" + colData[19] + "'," +
                       "'" + colData[20] + "'," +
                       "'" + colData[21] + "'," +
                       "'" + colData[22] + "'," +

                       "'" + colData[23] + "'," +
                       "'" + colData[24] + "'," +
                       "'" + colData[25] + "'," +
                       "'" + colData[26] + "'," +

                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult == 1)
                        {
                            columnInsertCount++;
                        }

                    }
                }


                if (columnInsertCount == (columnCount - 1))
                {
                    objResult.status = true;
                    objResult.message = "Excel Sheet Uploaded successfully";
                }
                else
                {
                    objResult.status = false;
                    objResult.message = "Error occured in uploading Excel Sheet Details";
                }

                dt.Dispose();


            }
            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = ex.ToString();
            }
        }

        public void DaImportExcelSummaryTemplate2(HttpRequest httpRequest, string employee_gid, result objResult)
        {
            try
            {
                HttpFileCollection httpFileCollection;
                DataTable dt = null;
                string lspath, lsfilePath, path, lsfile_name;
                string application_gid = httpRequest.Form["application_gid"];
                string credit_gid = httpRequest.Form["credit_gid"];
                string template_name = httpRequest.Form["template_name"];
                string lscompany_code = string.Empty;
                string project_flag = httpRequest.Form["project_flag"].ToString();

                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                // Create Directory
                //lsfilePath = HttpContext.Current.Server.MapPath("erpdocument" + "/" + lscompany_code + "/Master/SummaryTemplate/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
                lsfilePath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "/Master/SummaryTemplate/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

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
                    return;
                }

                // path creation
                lspath = lsfilePath + "/";
                FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);
                using (ExcelPackage xlPackage = new ExcelPackage(ms))
                {
                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];
                    rowCount = worksheet.Dimension.End.Row;
                    columnCount = worksheet.Dimension.End.Column;
                    endRange = worksheet.Dimension.End.Address;
                    colName = Regex.Replace(endRange, @"[\d-]", string.Empty);
                }
                file.Close();
                //ms.Close();
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/SummaryTemplate/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                ms.Close();


                objcmnfunctions.uploadFile(lspath, lsfile_gid);

                //Excel To DataTable                
                lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";
                excelRange = "A3:" + endRange;

                msSQL = "select application_gid from ocs_trn_tcadfsaupload where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                   " and template_name='" + template_name + "'";
                string lsGID = objdbconn.GetExecuteScalar(msSQL);

                lspath = "erpdocument" + "/" + lscompany_code + "/Master/SummaryTemplate/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                if (lsGID == null || lsGID == "")
                {
                    msGetGid = objcmnfunctions.GetMasterGID("FSAU");
                    msSQL = " insert into ocs_trn_tcadfsaupload(" +
                       " fasupload_gid," +
                       " template_name," +
                       " document_name," +
                       " document_path," +
                       " application_gid," +
                       " credit_gid," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid + "'," +
                       "'" + template_name.Replace(" ", "") + "'," +
                       "'" + httpPostedFile.FileName + "'," +
                       "'" + lspath + msdocument_gid + FileExtension + "'," +
                       "'" + application_gid + "'," +
                       "'" + credit_gid + "'," +
                        "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }
                else
                {
                    msSQL = "delete from ocs_trn_tcadfsaupload where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                    " and template_name='" + template_name + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        msSQL = "delete from ocs_trn_tcadsummarytemplate2 where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                                " and template_name='" + template_name + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msGetGid = objcmnfunctions.GetMasterGID("FSAU");
                        msSQL = " insert into ocs_trn_tcadfsaupload(" +
                                   " fasupload_gid," +
                                   " template_name," +
                                   " document_name," +
                                   " document_path," +
                                   " application_gid," +
                                   " credit_gid," +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + msGetGid + "'," +
                                   "'" + template_name.Replace(" ", "") + "'," +
                                   "'" + httpPostedFile.FileName + "'," +
                                   "'" + lspath + msdocument_gid + FileExtension + "'," +
                                   "'" + application_gid + "'," +
                                   "'" + credit_gid + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }


                dt = objcmnfunctions.ExcelToDataTable(lsfilePath, excelRange);



                columnInsertCount = 0;

                foreach (DataColumn column in dt.Columns)
                {

                    if (column.ToString() == "Particulars")
                    {

                    }
                    else
                    {
                        string lsdate = column.ToString();
                        String[] colData = new String[30];
                        int i = 0;
                        foreach (DataRow row in dt.Rows)
                        {

                            colData[i] = row[column.ToString()].ToString();
                            i++;
                        }

                        msGetGid = objcmnfunctions.GetMasterGID("STL2");

                        msSQL = " insert into ocs_trn_tcadsummarytemplate2(" +
                       " summarytemplate2_gid," +
                       " credit_gid," +
                       " application_gid," +
                       " template_name," +

                       " date," +
                       " audit_type," +
                       " interest_earned," +
                       " other_income," +
                       " total_income," +
                       " growth_in_income," +
                       " interest_expenses," +
                       " operating_expenses," +
                       " provision_and_contingencies," +
                       " net_profit," +
                       " total_debt," +
                       " tangible_networth," +
                       " net_interest_income," +

                       " assets_undermanagement," +
                       " nim," +
                       " loan_disbursed," +
                       " crar," +
                       " debt_equity," +
                       " operational_selfsufficiency_ratio," +
                       " costtoincome_ratio," +
                       " returnon_avgassets," +
                       " returnon_avgnetworth," +
                       " gross_npa," +
                       " net_npa," +
                       " gross_npapercent," +
                       " net_npapercent," +

                       " netnpa_networth," +


                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid + "'," +
                       "'" + credit_gid + "'," +
                       "'" + application_gid + "'," +
                       "'" + template_name.Replace(" ", "") + "'," +


                       "'" + lsdate + "'," +
                       "'" + colData[0] + "'," +
                       "'" + colData[1] + "'," +
                       "'" + colData[2] + "'," +
                       "'" + colData[3] + "'," +
                       "'" + colData[4] + "'," +
                       "'" + colData[5] + "'," +
                       "'" + colData[6] + "'," +
                       "'" + colData[7] + "'," +
                       "'" + colData[8] + "'," +
                       "'" + colData[9] + "'," +

                       "'" + colData[10] + "'," +
                       "'" + colData[11] + "'," +
                       "'" + colData[12] + "'," +
                       "'" + colData[13] + "'," +
                       "'" + colData[14] + "'," +
                       "'" + colData[15] + "'," +
                       "'" + colData[16] + "'," +
                       "'" + colData[17] + "'," +
                       "'" + colData[18] + "'," +
                       "'" + colData[19] + "'," +
                       "'" + colData[20] + "'," +
                       "'" + colData[21] + "'," +
                       "'" + colData[22] + "'," +

                       "'" + colData[23] + "'," +
                       "'" + colData[24] + "'," +
                       "'" + colData[25] + "'," +

                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult == 1)
                        {
                            columnInsertCount++;
                        }

                    }
                }


                if (columnInsertCount == (columnCount - 1))
                {
                    objResult.status = true;
                    objResult.message = "Excel Sheet Uploaded successfully";
                }
                else
                {
                    objResult.status = false;
                    objResult.message = "Error occured in uploading Excel Sheet Details";
                }

                dt.Dispose();


            }
            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = ex.ToString();
            }
        }



        //Credit Observations

        public void DaPostCreditObservation(string employee_gid, MdlMstCreditObservation values)
        {
            msSQL = "select creditpolicy_gid from ocs_trn_tcadcreditsbuyer where application_gid='" + values.application_gid + "'" +
                " and creditpolicy_gid='" + values.creditpolicy_gid + "'";
            string lscreditpolicy_gid = objdbconn.GetExecuteScalar(msSQL);
            if (lscreditpolicy_gid == (values.creditpolicy_gid))
            {
                values.status = false;
                values.message = "Already Credit Observation (Credit Policy Compliance) Added";
                return;
            }
            msGetGid = objcmnfunctions.GetMasterGID("CROB");
            msSQL = " insert into ocs_trn_tcadcreditobservation(" +
                   " creditobservation_gid," +
                   " credit_gid," +
                   " application_gid," +
                   " creditpolicy_gid ," +
                   " credit_policy," +
                   " complied_status," +
                   " observation," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.credit_gid + "'," +
                   "'" + values.application_gid + "'," +
                   "'" + values.creditpolicy_gid + "'," +
                   "'" + values.credit_policy.Replace("'", " ") + "'," +
                   "'" + values.complied_status + "',";

            if (values.observation == null || values.observation == "")
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.observation.Replace("'", " ") + "',";
            }
            msSQL += "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Credit Observation Details Added successfully";

                msSQL = " select creditobservation_gid,credit_gid,application_gid,creditpolicy_gid,credit_policy,complied_status,observation " +
                         "  from ocs_trn_tcadcreditobservation where " +
                         " credit_gid='" + values.credit_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getCreditObservation_list = new List<CreditObservation_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getCreditObservation_list.Add(new CreditObservation_list
                        {
                            creditobservation_gid = (dr_datarow["creditobservation_gid"].ToString()),
                            credit_gid = (dr_datarow["credit_gid"].ToString()),
                            application_gid = (dr_datarow["application_gid"].ToString()),
                            creditpolicy_gid = (dr_datarow["creditpolicy_gid"].ToString()),
                            credit_policy = (dr_datarow["credit_policy"].ToString()),
                            complied_status = (dr_datarow["complied_status"].ToString()),
                            observation = (dr_datarow["observation"].ToString()),
                        });
                    }
                    values.CreditObservation_list = getCreditObservation_list;
                }
                dt_datatable.Dispose();
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while Adding";
            }
        }

        public void DaGetCreditObservationList(string credit_gid, string employee_gid, MdlMstCreditObservation values)
        {
            msSQL = " select creditobservation_gid,credit_gid,application_gid,creditpolicy_gid,credit_policy,complied_status,observation, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                    " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as updated_by " +
                    " from ocs_trn_tcadcreditobservation a" +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                    "  where credit_gid='" + credit_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getCreditObservation_list = new List<CreditObservation_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getCreditObservation_list.Add(new CreditObservation_list
                    {
                        creditobservation_gid = (dr_datarow["creditobservation_gid"].ToString()),
                        credit_gid = (dr_datarow["credit_gid"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        creditpolicy_gid = (dr_datarow["creditpolicy_gid"].ToString()),
                        credit_policy = (dr_datarow["credit_policy"].ToString()),
                        complied_status = (dr_datarow["complied_status"].ToString()),
                        observation = (dr_datarow["observation"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        updated_date = (dr_datarow["updated_date"].ToString()),
                        updated_by = (dr_datarow["updated_by"].ToString()),
                    });
                }
                values.CreditObservation_list = getCreditObservation_list;
            }
            dt_datatable.Dispose();
        }

        public void DaEditGetCreditObservation(string creditobservation_gid, MdlMstCreditObservation values)
        {
            try
            {
                msSQL = " select creditobservation_gid,credit_gid,application_gid,creditpolicy_gid,credit_policy,complied_status,observation " +
                         "  from ocs_trn_tcadcreditobservation where creditobservation_gid='" + creditobservation_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.creditobservation_gid = objODBCDatareader["creditobservation_gid"].ToString();
                    values.credit_gid = objODBCDatareader["credit_gid"].ToString();
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.creditpolicy_gid = objODBCDatareader["creditpolicy_gid"].ToString();
                    values.credit_policy = objODBCDatareader["credit_policy"].ToString();
                    values.complied_status = objODBCDatareader["complied_status"].ToString();
                    values.observation = objODBCDatareader["observation"].ToString();
                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaUpdateCreditObservation(string employee_gid, MdlMstCreditObservation values)
        {

            msSQL = "select creditobservation_gid,credit_gid,application_gid,creditpolicy_gid,credit_policy,complied_status,observation" +
                " from ocs_trn_tcadcreditobservation where creditobservation_gid='" + values.creditobservation_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lscredit_gid = objODBCDatareader["credit_gid"].ToString();
                lsapplication_gid = objODBCDatareader["application_gid"].ToString();
                lscreditpolicy_gid = objODBCDatareader["creditpolicy_gid"].ToString();
                lscredit_policy = objODBCDatareader["credit_policy"].ToString();
                lscomplied_status = objODBCDatareader["complied_status"].ToString();
                lsobservation = objODBCDatareader["observation"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " update ocs_trn_tcadcreditobservation set " +
                     " creditpolicy_gid='" + values.creditpolicy_gid + "'," +
                     " credit_policy='" + values.credit_policy + "'," +
                     " complied_status='" + values.complied_status + "',";
            if (values.observation == "" || values.observation == null)
            {
                msSQL += " observation='',";
            }
            else
            {
                msSQL += " observation='" + values.observation.Replace("'", " ") + "',";
            }
            msSQL += " updated_by='" + employee_gid + "'," +
            " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
            " where creditobservation_gid='" + values.creditobservation_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                msGetGid = objcmnfunctions.GetMasterGID("COUL");

                msSQL = "Insert into ocs_trn_tcadcreditobservationupdatelog(" +
                   " creditobservationupdatelog_gid, " +
                   " creditobservation_gid, " +
                   " credit_gid, " +
                   " application_gid, " +
                   " creditpolicy_gid," +
                   " credit_policy," +
                   " complied_status ," +
                   " observation," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.creditobservation_gid + "'," +
                   "'" + lscredit_gid + "'," +
                   "'" + lsapplication_gid + "'," +
                   "'" + lscreditpolicy_gid + "'," +
                   "'" + lscredit_policy.Replace("'", " ") + "'," +
                   "'" + lscomplied_status + "'," +
                   "'" + lsobservation.Replace("'", " ") + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Credit Observation Details Updated Successfully";
            }
        }

        public void DaDeleteCreditObservation(string creditobservation_gid, string credit_gid, MdlMstCreditObservation values, string employee_gid)
        {
            msSQL = "delete from ocs_trn_tcadcreditobservation where creditobservation_gid='" + creditobservation_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Credit Observation details Deleted successfully";

                msSQL = " select creditobservation_gid,credit_gid,application_gid,creditpolicy_gid,credit_policy,complied_status,observation " +
                           "  from ocs_trn_tcadcreditobservation where " +
                           " credit_gid='" + credit_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getCreditObservation_list = new List<CreditObservation_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getCreditObservation_list.Add(new CreditObservation_list
                        {
                            creditobservation_gid = (dr_datarow["creditobservation_gid"].ToString()),
                            credit_gid = (dr_datarow["credit_gid"].ToString()),
                            application_gid = (dr_datarow["application_gid"].ToString()),
                            creditpolicy_gid = (dr_datarow["creditpolicy_gid"].ToString()),
                            credit_policy = (dr_datarow["credit_policy"].ToString()),
                            complied_status = (dr_datarow["complied_status"].ToString()),
                            observation = (dr_datarow["observation"].ToString()),
                        });
                    }
                    values.CreditObservation_list = getCreditObservation_list;
                }
                dt_datatable.Dispose();
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred while deleting";
            }
        }

        //Individual

        //Document Checklist

        public void DaPostIndividualCheckList(MdlMstCAD values, string user_gid, string employee_gid)
        {
            bool lsgroupupdate = false;
            string lsindividualdocument_gid = "", lsStackholdertype="";
            msSQL = " select stakeholder_type from ocs_trn_tcadcontact where contact_gid= '" + values.credit_gid + "'";

            lsStackholdertype = objdbconn.GetExecuteScalar(msSQL);
            foreach (string i in values.document_gid)
            {
                msGetGID = objcmnfunctions.GetMasterGID("DOCG");
                string lscovenant_type = "";
                msSQL = " select individualdocument_gid,documenttypes_gid,documenttype_name,individualdocument_name,covenant_type " +
                        " from ocs_mst_tindividualdocument where individualdocument_gid='" + i + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                    lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                    lsindividualdocument_name = objODBCDatareader["individualdocument_name"].ToString();
                    lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                    lsindividualdocument_gid = objODBCDatareader["individualdocument_gid"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select groupdocumentchecklist_gid from ocs_trn_tcaddocumentchecktls where application_gid='" + values.application_gid + "'" +
                        " and credit_gid='" + values.credit_gid + "' and individualdocument_gid ='" + lsindividualdocument_gid + "'";
                string lsgroupdocumentchecklist_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select concat(user_firstname,' ', user_lastname, ' / ', user_code) as username from adm_mst_tuser where user_gid='" + user_gid + "'";
                string tagged_name = objdbconn.GetExecuteScalar(msSQL);
                if (lsgroupdocumentchecklist_gid != "")
                {
                    msSQL = " update ocs_trn_tcaddocumentchecktls set untagged_type=null, " +
                   " untagged_by='" + user_gid + "', " +
                   " tagged_name='" + tagged_name + "'," +
                   " tagged_by='" + values.taggedby + "'," +
                   " untagged_date=current_timestamp" +
                   " where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_tcadgroupdocumentchecklist set untagged_type=null where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (lsStackholdertype == "Guarantor")
                    {
                        msSQL = "update ocs_trn_tcaddocumentchecktls set ind_gur_flag = 'Y' " +
                           " where groupdocumentchecklist_gid = '" + lsgroupdocumentchecklist_gid + "'";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                    if (lsStackholdertype == "Member")
                    {
                        msSQL = "update ocs_trn_tcaddocumentchecktls set ind_mem_flag = 'Y' " +
                           " where groupdocumentchecklist_gid = '" + lsgroupdocumentchecklist_gid + "'";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }


                }
                else
                {
                    msSQL = " insert into ocs_trn_tcaddocumentchecktls(" +
                        " documentcheckdtl_gid," +
                        " application_gid," +
                        " credit_gid," +
                        " individualdocument_gid," +
                        " documenttype_gid," +
                        " documenttype_code," +
                        " documenttype_name," +
                        " covenant_type, " +
                        " tagged_by, " +
                        " created_date," +
                        " created_by)" +
                        " VALUES(" +
                        "'" + msGetGID + "'," +
                        "'" + values.application_gid + "'," +
                        "'" + values.credit_gid + "'," +
                        "'" + i + "'," +
                        "'" + lsdocumenttype_gid + "'," +
                        "'" + lsdocumenttype_name + "'," +
                        "'" + lsindividualdocument_name.Replace("'", "") + "'," +
                        "'" + lscovenant_type + "'," +
                        "'" + values.taggedby + "'," +
                        "current_timestamp," +
                        "'" + user_gid + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    lsgroupupdate = true;
                }
            }
            if (lsgroupupdate)
            {
                DaCADCreditAction objvalues = new DaCADCreditAction();
                objvalues.DaCADGroupDocChecklistinfo(values.application_gid, values.credit_gid, employee_gid);
            }
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Document Type Tagged Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        //Covenant

        public bool DaGetCovenantIndividualDocumentList(MdlMstCADCompany values, string credit_gid, string application_gid)
        {

            //string program_gid = "";

            //msSQL = " select program_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "' ";

            //program_gid = objdbconn.GetExecuteScalar(msSQL);

            if (credit_gid != "")
            {
                msSQL = " SELECT individualdocument_gid,individualdocument_name,documenttypes_gid,documenttype_name,covenant_type " +
                  " FROM ocs_mst_tindividualdocument where status='Y' and covenant_type='Y' and individualdocument_gid not in (select individualdocument_gid " +
                  " from ocs_trn_tcadcovanantdocumentcheckdtls where credit_gid = '" + credit_gid + "' and untagged_type is null group by individualdocument_gid)";

                //msSQL = " select a.individualdocument_gid, a.individualdocument_name, a.documenttypes_gid, a.documenttype_name, a.covenant_type " +
                //   " from ocs_mst_tindividualdocument a " +
                //   " left join ocs_mst_tindividualdocumentprogram b on a.individualdocument_gid = b.individualdocument_gid " +
                //   " where a.status='Y' and b.program_gid = '" + program_gid + "' and a.covenant_type = 'Y' " +
                //     " and a.individualdocument_gid not in (select individualdocument_gid " +
                //     " from ocs_trn_tcadcovanantdocumentcheckdtls where credit_gid = '" + credit_gid + "' and untagged_type is null  group by individualdocument_gid)";
            }
            else
            {
                msSQL = " SELECT individualdocument_gid,individualdocument_name,documenttypes_gid,documenttype_name,covenant_type " +
                " FROM ocs_mst_tindividualdocument where status='Y' and covenant_type='Y'";

                //msSQL = " select a.individualdocument_gid,a.documenttypes_gid,a.documenttype_name,a.individualdocument_name,a.covenant_type " +
                //    " from ocs_mst_tindividualdocument a " +
                //    " left join ocs_mst_tindividualdocumentprogram b on a.individualdocument_gid = b.individualdocument_gid " +
                //    " where a.status='Y' and b.program_gid = '" + program_gid + "' and a.covenant_type = 'Y' ";
            }

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<CADDocument>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getDocList.Add(new CADDocument
                    {
                        document_gid = dt["individualdocument_gid"].ToString(),
                        documenttype_gid = dt["documenttypes_gid"].ToString(),
                        document_name = dt["individualdocument_name"].ToString(),
                        documenttype_name = dt["documenttype_name"].ToString(),
                        covenant_type = dt["covenant_type"].ToString(),
                    });
                }
                values.CADDocument = getDocList;
                values.status = true;
                values.message = "Data Fetched";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();

            return true;
        }

        //Bureau

        public void DaGetIndividualBureauTempClear(string employee_gid, result values)
        {
            msSQL = "delete from ocs_trn_tcadindividual2cicdocumentupload where contact2bureau_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_trn_tcadcontact2tuhighriskalert where contact2bureau_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }

        public void DaGetContactBureauList(string contact_gid, MdlContactBureau values)
        {
            msSQL = "select contact2bureau_gid,bureauname_name,bureau_score,date_format(bureauscore_date, '%d-%m-%Y') as bureauscore_date from ocs_trn_tcadcontact2bureau where " +
              " contact_gid='" + contact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcontactbureau_list = new List<contactbureau_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcontactbureau_list.Add(new contactbureau_list
                    {
                        contact2bureau_gid = (dr_datarow["contact2bureau_gid"].ToString()),
                        bureauname_name = (dr_datarow["bureauname_name"].ToString()),
                        bureau_score = (dr_datarow["bureau_score"].ToString()),
                        bureauscore_date = (dr_datarow["bureauscore_date"].ToString()),
                    });
                }
            }
            values.contactbureau_list = getcontactbureau_list;
            dt_datatable.Dispose();
        }

        public void DaCICUploadIndividualDocList(string contact2bureau_gid, string employee_gid, MdlCICIndividual values)
        {
            msSQL = " select individual2cicdocumentupload_gid,contact2bureau_gid,cicdocument_name,cicdocument_path,document_content,migration_flag from ocs_trn_tcadindividual2cicdocumentupload " +
                                 " where contact2bureau_gid='" + contact2bureau_gid + "' or contact2bureau_gid='" + employee_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<cicuploaddoc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new cicuploaddoc_list
                    {
                        document_name = dt["cicdocument_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["cicdocument_path"].ToString()),
                        contact2bureau_gid = dt["contact2bureau_gid"].ToString(),
                        tmpcicdocument_gid = dt["individual2cicdocumentupload_gid"].ToString(),
                        document_content = dt["document_content"].ToString(),

                        migration_flag = dt["migration_flag"].ToString()

                    });
                    values.cicuploaddoc_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }


        public bool DaPostCICUploadIndividual(string employee_gid, MdlCICIndividual values)
        {
            // Document Attachments
            msSQL = "select document_name from ocs_tmp_tcadcicdocument where created_by='" + employee_gid + "'";
            lsdocument_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select document_path from ocs_tmp_tcadcicdocument where created_by='" + employee_gid + "'";
            lsdocument_path = objdbconn.GetExecuteScalar(msSQL);

            msGetGid = objcmnfunctions.GetMasterGID("C2BR");
            msSQL = " insert into ocs_trn_tcadcontact2bureau(" +
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
                   "'" + values.contact_gid + "'," +
                   "'" + values.bureauname_gid + "'," +
                   "'" + values.bureauname_name + "'," +
                   "'" + values.bureau_score + "',";

            if (values.bureauscore_date == null || values.bureauscore_date == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.bureauscore_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }


            msSQL += "'" + values.bureau_response.Replace("'", "") + "',";

            if (values.observations == null || values.observations == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.observations.Replace("'", "") + "',";
            }

           
       msSQL +=       "'" + employee_gid + "'," +
                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                msSQL = "update ocs_trn_tcadindividual2cicdocumentupload set contact_gid='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_trn_tcadindividual2cicdocumentupload set contact2bureau_gid='" + msGetGid + "' where contact2bureau_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_trn_tcadcontact2tuhighriskalert set contact_gid='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_trn_tcadcontact2tuhighriskalert set contact2bureau_gid='" + msGetGid + "' where contact2bureau_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Bureau Updates Added for Individual Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }

        }

        public void DaDeleteContactBureau(string contact2bureau_gid, MdlContactBureau values)
        {
            msSQL = "delete from ocs_trn_tcadcontact2bureau where contact2bureau_gid='" + contact2bureau_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Bureau Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }


        public bool DaUpdateCICUploadIndividual(string employee_gid, MdlCICIndividual values)
        {



            msSQL = " update ocs_trn_tcadcontact2bureau set " +
                       " bureauname_gid='" + values.bureauname_gid + "'," +
                       " bureauname_name='" + values.bureauname_name + "'," +
                       " bureau_score='" + values.bureau_score + "',";

            if (Convert.ToDateTime(values.bureauscoredate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += " bureauscore_date='" + Convert.ToDateTime(values.bureauscoredate).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
            }

            if ((values.observations == null) || (values.observations == ""))
            {
                msSQL += "observations=null,";
            }
            else
            {
                msSQL += " observations='" + values.observations.Replace("'", "") + "',";
            }

           
                msSQL += " bureau_response='" + values.bureau_response.Replace("'", "") + "'," +
                       " updated_by='" + employee_gid + "'," +
                       " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                       " where contact2bureau_gid='" + values.contact2bureau_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                msSQL = "update ocs_trn_tcadindividual2cicdocumentupload set contact_gid='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_trn_tcadindividual2cicdocumentupload set contact2bureau_gid='" + values.contact2bureau_gid + "' where contact2bureau_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



                values.status = true;
                values.message = "Bureau Details Updated for Individual Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }

        }

        public void DaCICUploadIndividualDocDelete(string tmpcicdocument_gid, MdlCICIndividual values)
        {
            msSQL = " delete from ocs_trn_tcadindividual2cicdocumentupload where individual2cicdocumentupload_gid='" + tmpcicdocument_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Document Deleted Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaCICIndividualEdit(string contact2bureau_gid, MdlCICIndividual values)
        {
            try
            {
                msSQL = " select contact2bureau_gid, bureauname_gid,bureauname_name, bureau_score, date_format(bureauscore_date,'%d-%m-%Y') as bureauscore_date," +
                    " observations, bureau_response" +
                    " from ocs_trn_tcadcontact2bureau where contact2bureau_gid='" + contact2bureau_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.bureauname_gid = objODBCDatareader["bureauname_gid"].ToString();
                    values.bureauname_name = objODBCDatareader["bureauname_name"].ToString();
                    values.bureau_score = objODBCDatareader["bureau_score"].ToString();
                    values.observations = objODBCDatareader["observations"].ToString();
                    values.bureau_response = objODBCDatareader["bureau_response"].ToString();
                    values.bureauscore_date = objODBCDatareader["bureauscore_date"].ToString();
                    values.contact2bureau_gid = objODBCDatareader["contact2bureau_gid"].ToString();
                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();

            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "failure";
            }
        }

        //Group

        public void DaPostGroupCheckList(MdlMstCAD values, string user_gid, string employee_gid)
        {
            bool lsgroupupdate = false;
            foreach (string i in values.document_gid)
            {

                string lscovenant_type = "";
                msSQL = " select documenttypes_gid,documenttype_name,groupdocument_name,covenant_type " +
                        " from ocs_mst_tgroupdocument where groupdocument_gid='" + i + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                    lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                    lsgroupdocument_name = objODBCDatareader["groupdocument_name"].ToString();
                    lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select groupdocumentchecklist_gid from ocs_trn_tcaddocumentchecktls where application_gid='" + values.application_gid + "'" +
                      " and credit_gid='" + values.credit_gid + "' and individualdocument_gid ='" + i + "'";
                string lsgroupdocumentchecklist_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select concat(user_firstname,' ', user_lastname, ' / ', user_code) as username from adm_mst_tuser where user_gid='" + user_gid + "'";
                string tagged_name = objdbconn.GetExecuteScalar(msSQL);
                if (lsgroupdocumentchecklist_gid != "")
                {
                    msSQL = " update ocs_trn_tcaddocumentchecktls set untagged_type=null, " +
                   " untagged_by='" + user_gid + "', " +
                   " tagged_name='" + tagged_name + "'," +
                   " tagged_by='" + values.taggedby + "'," +
                   " untagged_date=current_timestamp" +
                   " where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_tcadgroupdocumentchecklist set untagged_type=null where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    msGetGID = objcmnfunctions.GetMasterGID("DOCG");
                    msSQL = " insert into ocs_trn_tcaddocumentchecktls(" +
                            " documentcheckdtl_gid," +
                            " application_gid," +
                            " credit_gid," +
                            " groupdocument_gid, " +
                            " documenttype_gid," +
                            " documenttype_code," +
                            " documenttype_name," +
                            " covenant_type, " +
                            " tagged_by, " +
                            " created_date," +
                            " created_by)" +
                            " VALUES(" +
                            "'" + msGetGID + "'," +
                            "'" + values.application_gid + "'," +
                            "'" + values.credit_gid + "'," +
                            "'" + i + "'," +
                            "'" + lsdocumenttype_gid + "'," +
                            "'" + lsdocumenttype_name + "'," +
                            "'" + lsgroupdocument_name.Replace("'", "") + "'," +
                            "'" + lscovenant_type + "'," +
                            "'" + values.taggedby + "'," +
                            "current_timestamp," +
                            "'" + user_gid + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    lsgroupupdate = true;
                }
            }

            if (lsgroupupdate)
            {
                DaCADCreditAction objvalues = new DaCADCreditAction();
                objvalues.DaCADGroupDocChecklistinfo(values.application_gid, values.credit_gid, employee_gid);
            }

            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Document Type Tagged Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }


        public bool DaGetCovenantGroupDocumentList(MdlMstCADCompany values, string credit_gid)
        {
            if (credit_gid != "")
            {
                msSQL = " SELECT groupdocument_gid,groupdocument_name,documenttypes_gid,documenttype_name,covenant_type " +
                        " FROM ocs_mst_tgroupdocument where status='Y' and covenant_type='Y' and groupdocument_gid not in (select groupdocument_gid " +
                        " from ocs_trn_tcadcovanantdocumentcheckdtls where credit_gid = '" + credit_gid + "' and untagged_type is null  group by groupdocument_gid)";
            }
            else
            {
                msSQL = " SELECT groupdocument_gid,groupdocument_name,documenttypes_gid,documenttype_name,covenant_type " +
                       " FROM ocs_mst_tgroupdocument where status='Y' and covenant_type='Y'";
            }

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<CADDocument>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getDocList.Add(new CADDocument
                    {
                        document_gid = dt["groupdocument_gid"].ToString(),
                        documenttype_gid = dt["documenttypes_gid"].ToString(),
                        document_name = dt["groupdocument_name"].ToString(),
                        documenttype_name = dt["documenttype_name"].ToString(),
                        covenant_type = dt["covenant_type"].ToString(),
                    });
                }
                values.CADDocument = getDocList;
                values.status = true;
                values.message = "Data Fetched";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaCICIndividualDocumentUpload(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/CICUploadIndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                        string FileExtension = httpPostedFile.FileName;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        Stream ls_readStream;
                        ls_readStream = httpPostedFile.InputStream;
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);
                        //lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "Master/CICUploadIndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        //FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                        //ms.WriteTo(file);
                        //file.Close();
                        //ms.Close();

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/CICUploadIndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/CICUploadIndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("IDCU");
                        msSQL = " insert into ocs_trn_tcadindividual2cicdocumentupload( " +
                                    " individual2cicdocumentupload_gid, " +
                                    " contact_gid," +
                                    " contact2bureau_gid," +
                                    " cicdocument_name," +
                                    " cicdocument_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension.Replace("'", "") + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            objfilename.status = true;
                            objfilename.message = "Document Uploaded Successfully..!";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured..!";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }
            return true;
        }


        public void DaGetProfitLoss(string application_gid, string credit_gid, string template_name, MdlMstProfitLoss values)
        {
            msSQL = "select audited,domestic_sales,export_sales,totalgross_sales,excise_duty,otheroperating_income,other_income,net_sales,increasenet_sales, " +
                " import_rawmaterial,indigenous_rawmaterial,import_spares,indigenous_spares,power_fuel,direct_labour,othersoperating_expenses," +
                "  depreciation,repair_maintenance,rent,otherdirect_cost,totalcost_sales,openingstock_progress,closingstock_progress,costof_production,copnet_sales," +
                " openingstock_finishedgoods,closingstock_finishedgoods,costof_sales,cosnet_sales,sgam_expenses,pbit,pbitnet_sales,interest_finance_charges," +
                " interest_financenet_sales,pbt,pbtnet_sales,interest_earned,mics_receipts,divdend,profitsales_invetments,exchange_gain,total_nonoperative_income," +
                " wrieoff_provision,proir_year,baddebts_written_off,othernoncash_expenses,othernonoperating_exoeses,total_nonoperative_expenses,profitbefore_tax," +
                " current_tax,deferred_tax,pat,net_profitloss,amount,rate,retained_profit,template_name,application_gid,credit_gid,allfiguresin_inr from ocs_trn_tcadcreditprofitloss " +
                " where application_gid='" + application_gid + "'" +
                 " and credit_gid='" + credit_gid + "' and template_name='" + template_name + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getprofitloss_list = new List<profitloss_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getprofitloss_list.Add(new profitloss_list
                    {
                        audited = dr_datarow["audited"].ToString(),
                        domestic_sales = (dr_datarow["domestic_sales"].ToString()),
                        export_sales = (dr_datarow["export_sales"].ToString()),
                        totalgross_sales = dr_datarow["totalgross_sales"].ToString(),
                        excise_duty = dr_datarow["excise_duty"].ToString(),
                        otheroperating_income = dr_datarow["otheroperating_income"].ToString(),
                        other_income = (dr_datarow["other_income"].ToString()),
                        net_sales = (dr_datarow["net_sales"].ToString()),
                        increasenet_sales = dr_datarow["increasenet_sales"].ToString(),
                        import_rawmaterial = dr_datarow["import_rawmaterial"].ToString(),
                        indigenous_rawmaterial = dr_datarow["indigenous_rawmaterial"].ToString(),
                        import_spares = (dr_datarow["import_spares"].ToString()),
                        indigenous_spares = (dr_datarow["indigenous_spares"].ToString()),
                        power_fuel = dr_datarow["power_fuel"].ToString(),
                        direct_labour = dr_datarow["direct_labour"].ToString(),
                        othersoperating_expenses = dr_datarow["othersoperating_expenses"].ToString(),
                        depreciation = (dr_datarow["depreciation"].ToString()),
                        repair_maintenance = (dr_datarow["repair_maintenance"].ToString()),
                        rent = dr_datarow["rent"].ToString(),
                        otherdirect_cost = dr_datarow["otherdirect_cost"].ToString(),
                        totalcost_sales = dr_datarow["totalcost_sales"].ToString(),
                        openingstock_progress = (dr_datarow["openingstock_progress"].ToString()),
                        closingstock_progress = (dr_datarow["closingstock_progress"].ToString()),
                        costof_production = dr_datarow["costof_production"].ToString(),
                        copnet_sales = dr_datarow["copnet_sales"].ToString(),
                        openingstock_finishedgoods = dr_datarow["openingstock_finishedgoods"].ToString(),
                        closingstock_finishedgoods = dr_datarow["closingstock_finishedgoods"].ToString(),
                        costof_sales = dr_datarow["costof_sales"].ToString(),
                        cosnet_sales = (dr_datarow["cosnet_sales"].ToString()),
                        sgam_expenses = (dr_datarow["sgam_expenses"].ToString()),
                        pbit = (dr_datarow["pbit"].ToString()),
                        pbitnet_sales = dr_datarow["pbitnet_sales"].ToString(),
                        interest_finance_charges = dr_datarow["interest_finance_charges"].ToString(),
                        interest_financenet_sales = dr_datarow["interest_financenet_sales"].ToString(),
                        pbt = (dr_datarow["pbt"].ToString()),
                        pbtnet_sales = (dr_datarow["pbtnet_sales"].ToString()),
                        interest_earned = dr_datarow["interest_earned"].ToString(),
                        mics_receipts = dr_datarow["mics_receipts"].ToString(),
                        divdend = dr_datarow["divdend"].ToString(),
                        profitsales_invetments = dr_datarow["profitsales_invetments"].ToString(),
                        exchange_gain = dr_datarow["exchange_gain"].ToString(),
                        total_nonoperative_income = (dr_datarow["total_nonoperative_income"].ToString()),
                        wrieoff_provision = (dr_datarow["wrieoff_provision"].ToString()),
                        proir_year = dr_datarow["proir_year"].ToString(),
                        baddebts_written_off = dr_datarow["baddebts_written_off"].ToString(),
                        othernoncash_expenses = dr_datarow["othernoncash_expenses"].ToString(),
                        othernonoperating_exoeses = (dr_datarow["othernonoperating_exoeses"].ToString()),
                        total_nonoperative_expenses = (dr_datarow["total_nonoperative_expenses"].ToString()),
                        profitbefore_tax = dr_datarow["profitbefore_tax"].ToString(),
                        current_tax = dr_datarow["current_tax"].ToString(),
                        deferred_tax = dr_datarow["deferred_tax"].ToString(),
                        pat = (dr_datarow["pat"].ToString()),
                        net_profitloss = (dr_datarow["net_profitloss"].ToString()),
                        amount = dr_datarow["amount"].ToString(),
                        rate = (dr_datarow["rate"].ToString()),
                        retained_profit = dr_datarow["retained_profit"].ToString(),
                        template_name = dr_datarow["template_name"].ToString(),
                        application_gid = dr_datarow["application_gid"].ToString(),
                        credit_gid = dr_datarow["credit_gid"].ToString(),
                        allfiguresin_inr = (dr_datarow["allfiguresin_inr"].ToString().Replace('#', '.')),
                    });
                }
                values.profitloss_list = getprofitloss_list;
            }
            dt_datatable.Dispose();
        }


        public void DaGetBalanceSheetTemplate1List(string credit_gid, string application_gid, string template_type, string employee_gid, MdlMstCUWBalancesheettemplate1 values)
        {
            msSQL = " select creditbalancesheet_gid, credit_gid, application_gid, template_type, allfiguresin_inr, audit_type, applicant_bank, other_banks, billpurchased_disc, wcb_total," +
                    " shortterm_borrowings, sundrycreditors_acceptances, advancesreceived_customers, provision_taxation, dividend_taxthereon, other_provisions, " +
                    " instalmentsof_tl, othercurrent_liabilitiesin1year, dues_directors, creditors_expenses, unclaimed_debentures, interestaccrued_loans, " +
                    " othercurrent_liabilities, total, totalcurrent_liabilities, debenturesin_1year, prefshares_after1Year, term_loans, fixed_deposits, " +
                    " otherterm_liabilities, deferredtax_liability, creditorscapital_expenses, totallongterm_liabilities, ordinaryshare_capital, capitalredemption_reserve, " +
                    " general_reserve, investmentallowance_reserve, surplusdeficitpandl_account, prefsharecapital_debenture, revaluation_reserve, other_reserves, " +
                    " capital_reserve, sharepremium_account, advancetowards_capital, depositfriends_relatives, totalnet_worth, total_loabilities, claimscmpyack_debts, " +
                    " arrearsofsalary_susemployee, estimated_capitalacct, guaranteesbankers_company, current_assets, cash_balance, bank_balance, Govtandother_securities, " +
                    " fixeddeposits_banks, domestic_receivables, export_receivables, instalmentsdeferred_receivables, rawmaterials_indigenous, rawmaterial_imported, " +
                    " stock_inprocess, finished_goods, other_sparesindigenous, other_sparesindimported, advances_suppliers, advancepayment_tax, othercurrent_asset, " +
                    " advances_recoverable, interestservice_receivable, incomeaccrued_investments, others, totalcurrent_assets, land_building, computers, plant_machinery, " +
                    " furnitures_fixtures, otherfixed_assets, capitalwip_machniery, less_depriciation, net_block, investmentssubsidiary_compy, other_investments, advancesto_subsidiaries, " +
                    " receivables_sixmonths, marginmoney_banks, defferredrevenue_expenditure, deposits_departments, nonconsumablestores_spares, othernoncurrent_assets, " +
                    " total_othernoncurrentassets, miscellaneous_expenses, patents_good, debitbalancepandl_account, unsecureddebtors_doubtful, total_intangibleassets, " +
                    " total_assets, totalliabilities_totalasset, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by " +
                    " from ocs_trn_tcadcreditbalancesheet a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " where credit_gid = '" + credit_gid + "' and a.application_gid='" + application_gid + "' and template_type='" + template_type + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditbalancesheettemplate1_list = new List<creditbalancesheettemplate1_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcreditbalancesheettemplate1_list.Add(new creditbalancesheettemplate1_list
                    {
                        creditbalancesheet_gid = (dr_datarow["creditbalancesheet_gid"].ToString()),
                        credit_gid = (dr_datarow["credit_gid"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        template_type = (dr_datarow["template_type"].ToString()),
                        allfiguresin_inr = ((dr_datarow["allfiguresin_inr"].ToString().Replace('#', '.'))),
                        audit_type = (dr_datarow["audit_type"].ToString()),
                        applicant_bank = (dr_datarow["applicant_bank"].ToString()),
                        other_banks = (dr_datarow["other_banks"].ToString()),
                        billpurchased_disc = (dr_datarow["billpurchased_disc"].ToString()),
                        wcb_total = (dr_datarow["wcb_total"].ToString()),
                        shortterm_borrowings = (dr_datarow["shortterm_borrowings"].ToString()),
                        sundrycreditors_acceptances = (dr_datarow["sundrycreditors_acceptances"].ToString()),
                        advancesreceived_customers = (dr_datarow["advancesreceived_customers"].ToString()),
                        provision_taxation = (dr_datarow["provision_taxation"].ToString()),
                        dividend_taxthereon = (dr_datarow["dividend_taxthereon"].ToString()),
                        other_provisions = (dr_datarow["other_provisions"].ToString()),
                        instalmentsof_tl = (dr_datarow["instalmentsof_tl"].ToString()),
                        othercurrent_liabilitiesin1year = (dr_datarow["othercurrent_liabilitiesin1year"].ToString()),
                        dues_directors = (dr_datarow["dues_directors"].ToString()),
                        creditors_expenses = (dr_datarow["creditors_expenses"].ToString()),
                        unclaimed_debentures = (dr_datarow["unclaimed_debentures"].ToString()),
                        interestaccrued_loans = (dr_datarow["interestaccrued_loans"].ToString()),
                        othercurrent_liabilities = (dr_datarow["othercurrent_liabilities"].ToString()),
                        total = (dr_datarow["total"].ToString()),
                        totalcurrent_liabilities = (dr_datarow["totalcurrent_liabilities"].ToString()),
                        debenturesin_1year = (dr_datarow["debenturesin_1year"].ToString()),
                        prefshares_after1Year = (dr_datarow["prefshares_after1Year"].ToString()),
                        term_loans = (dr_datarow["term_loans"].ToString()),
                        fixed_deposits = (dr_datarow["fixed_deposits"].ToString()),
                        otherterm_liabilities = (dr_datarow["otherterm_liabilities"].ToString()),
                        deferredtax_liability = (dr_datarow["deferredtax_liability"].ToString()),
                        creditorscapital_expenses = (dr_datarow["creditorscapital_expenses"].ToString()),
                        totallongterm_liabilities = (dr_datarow["totallongterm_liabilities"].ToString()),
                        ordinaryshare_capital = (dr_datarow["ordinaryshare_capital"].ToString()),
                        capitalredemption_reserve = (dr_datarow["capitalredemption_reserve"].ToString()),
                        general_reserve = (dr_datarow["general_reserve"].ToString()),
                        investmentallowance_reserve = (dr_datarow["investmentallowance_reserve"].ToString()),
                        surplusdeficitpandl_account = (dr_datarow["surplusdeficitpandl_account"].ToString()),
                        prefsharecapital_debenture = (dr_datarow["prefsharecapital_debenture"].ToString()),
                        revaluation_reserve = (dr_datarow["revaluation_reserve"].ToString()),
                        other_reserves = (dr_datarow["other_reserves"].ToString()),
                        capital_reserve = (dr_datarow["capital_reserve"].ToString()),
                        sharepremium_account = (dr_datarow["sharepremium_account"].ToString()),
                        advancetowards_capital = (dr_datarow["advancetowards_capital"].ToString()),
                        depositfriends_relatives = (dr_datarow["depositfriends_relatives"].ToString()),
                        totalnet_worth = (dr_datarow["totalnet_worth"].ToString()),
                        total_loabilities = (dr_datarow["total_loabilities"].ToString()),
                        claimscmpyack_debts = (dr_datarow["claimscmpyack_debts"].ToString()),
                        arrearsofsalary_susemployee = (dr_datarow["arrearsofsalary_susemployee"].ToString()),
                        estimated_capitalacct = (dr_datarow["estimated_capitalacct"].ToString()),
                        guaranteesbankers_company = (dr_datarow["guaranteesbankers_company"].ToString()),
                        current_assets = (dr_datarow["current_assets"].ToString()),
                        cash_balance = (dr_datarow["cash_balance"].ToString()),
                        bank_balance = (dr_datarow["bank_balance"].ToString()),
                        Govtandother_securities = (dr_datarow["Govtandother_securities"].ToString()),
                        fixeddeposits_banks = (dr_datarow["fixeddeposits_banks"].ToString()),
                        domestic_receivables = (dr_datarow["domestic_receivables"].ToString()),
                        export_receivables = (dr_datarow["export_receivables"].ToString()),
                        instalmentsdeferred_receivables = (dr_datarow["instalmentsdeferred_receivables"].ToString()),
                        rawmaterials_indigenous = (dr_datarow["rawmaterials_indigenous"].ToString()),
                        rawmaterial_imported = (dr_datarow["rawmaterial_imported"].ToString()),
                        stock_inprocess = (dr_datarow["stock_inprocess"].ToString()),
                        finished_goods = (dr_datarow["finished_goods"].ToString()),
                        other_sparesindigenous = (dr_datarow["other_sparesindigenous"].ToString()),
                        other_sparesindimported = (dr_datarow["other_sparesindimported"].ToString()),
                        advances_suppliers = (dr_datarow["advances_suppliers"].ToString()),
                        advancepayment_tax = (dr_datarow["advancepayment_tax"].ToString()),
                        othercurrent_asset = (dr_datarow["othercurrent_asset"].ToString()),
                        advances_recoverable = (dr_datarow["advances_recoverable"].ToString()),
                        interestservice_receivable = (dr_datarow["interestservice_receivable"].ToString()),
                        incomeaccrued_investments = (dr_datarow["incomeaccrued_investments"].ToString()),
                        others = (dr_datarow["others"].ToString()),
                        totalcurrent_assets = (dr_datarow["totalcurrent_assets"].ToString()),
                        land_building = (dr_datarow["land_building"].ToString()),
                        computers = (dr_datarow["computers"].ToString()),
                        plant_machinery = (dr_datarow["plant_machinery"].ToString()),
                        furnitures_fixtures = (dr_datarow["furnitures_fixtures"].ToString()),
                        otherfixed_assets = (dr_datarow["otherfixed_assets"].ToString()),
                        capitalwip_machniery = (dr_datarow["capitalwip_machniery"].ToString()),
                        less_depriciation = (dr_datarow["less_depriciation"].ToString()),
                        net_block = (dr_datarow["net_block"].ToString()),
                        investmentssubsidiary_compy = (dr_datarow["investmentssubsidiary_compy"].ToString()),
                        other_investments = (dr_datarow["other_investments"].ToString()),
                        advancesto_subsidiaries = (dr_datarow["advancesto_subsidiaries"].ToString()),
                        receivables_sixmonths = (dr_datarow["receivables_sixmonths"].ToString()),
                        marginmoney_banks = (dr_datarow["marginmoney_banks"].ToString()),
                        defferredrevenue_expenditure = (dr_datarow["defferredrevenue_expenditure"].ToString()),
                        deposits_departments = (dr_datarow["deposits_departments"].ToString()),
                        nonconsumablestores_spares = (dr_datarow["nonconsumablestores_spares"].ToString()),
                        othernoncurrent_assets = (dr_datarow["othernoncurrent_assets"].ToString()),
                        total_othernoncurrentassets = (dr_datarow["total_othernoncurrentassets"].ToString()),
                        miscellaneous_expenses = (dr_datarow["miscellaneous_expenses"].ToString()),
                        patents_good = (dr_datarow["patents_good"].ToString()),
                        debitbalancepandl_account = (dr_datarow["debitbalancepandl_account"].ToString()),
                        unsecureddebtors_doubtful = (dr_datarow["unsecureddebtors_doubtful"].ToString()),
                        total_intangibleassets = (dr_datarow["total_intangibleassets"].ToString()),
                        totalliabilities_totalasset = (dr_datarow["totalliabilities_totalasset"].ToString()),
                        total_assets = (dr_datarow["total_assets"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                    });
                }
                values.creditbalancesheettemplate1_list = getcreditbalancesheettemplate1_list;
            }
            dt_datatable.Dispose();
        }



        public void DaGetBalanceSheetTemplate2List(string credit_gid, string application_gid, string template_type, string employee_gid, MdlMstCUWBalancesheettemplate2 values)
        {
            msSQL = " select creditbalancesheet2_gid, credit_gid, application_gid, template_type, allfiguresin_inr, audit_type,demand_deposits, term_deposits, " +
                    " other_deposits, deposits_total, shortterm_borrowings, term_borrowings, other_borrowings, borrowings_total, provision_taxation, provisionstandard_assets, " +
                    " provisionnon_assets, interestaccrued_loans, creditors_expenses, other_provisions, othercurrent_liabilities, deferredtax_liability, otherprovision_total, " +
                    " ordinaryshare_capital, capitalredemption_reserve, general_reserve, investment_allowancereserve, surplusdeficitpandl_account, prefsharecapital_debenture, " +
                    " revaluation_reserve, other_reserves, capital_reserves, sharepremium_acct, advancetowards_capital, deposit_relatives, net_worth, total_liabilities, claimscmpyack_debts, " +
                    " arrearsofsalary_susemployee, estimated_capitalacct, guaranteesbankers_company, incurrent_account, fixeddeposits_banks, othercash_bankbalance, cashbankbalance_total, " +
                    " government_securities, otherapproved_securities, shares, debentures_bonds, subsidiariesjoint_ventures, other_investments, investments_total, loanspayable_demand, " +
                    " term_loans, other_advances, advances_total, interest_accrued, advancetax_deducted, asset_others, othersssets_total, land_building, computers, plant_machinery, " +
                    " furnitures_fixtures, otherfixed_assets, capital_wip, less_depreciation, net_block, miscellaneous_expenses, patents_good, debitbalancepandl_account, " +
                    " unsecureddebtors_doubtful, total_intangibleassets, total_assets, totalliabilities_totalasset, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by " +
                    " from ocs_trn_tcadcreditbalancesheet2 a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " where credit_gid = '" + credit_gid + "' and a.application_gid='" + application_gid + "' and template_type='" + template_type + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditbalancesheettemplate2_list = new List<creditbalancesheettemplate2_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcreditbalancesheettemplate2_list.Add(new creditbalancesheettemplate2_list
                    {
                        creditbalancesheet2_gid = (dr_datarow["creditbalancesheet2_gid"].ToString()),
                        credit_gid = (dr_datarow["credit_gid"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        template_type = (dr_datarow["template_type"].ToString()),
                        allfiguresin_inr = ((dr_datarow["allfiguresin_inr"].ToString().Replace('#', '.'))),
                        audit_type = (dr_datarow["audit_type"].ToString()),
                        demand_deposits = (dr_datarow["demand_deposits"].ToString()),
                        term_deposits = (dr_datarow["term_deposits"].ToString()),
                        other_deposits = (dr_datarow["other_deposits"].ToString()),
                        deposits_total = (dr_datarow["deposits_total"].ToString()),
                        shortterm_borrowings = (dr_datarow["shortterm_borrowings"].ToString()),
                        term_borrowings = (dr_datarow["term_borrowings"].ToString()),
                        other_borrowings = (dr_datarow["other_borrowings"].ToString()),
                        borrowings_total = (dr_datarow["borrowings_total"].ToString()),
                        provision_taxation = (dr_datarow["provision_taxation"].ToString()),
                        provisionstandard_assets = (dr_datarow["provisionstandard_assets"].ToString()),
                        provisionnon_assets = (dr_datarow["provisionnon_assets"].ToString()),
                        interestaccrued_loans = (dr_datarow["interestaccrued_loans"].ToString()),
                        creditors_expenses = (dr_datarow["creditors_expenses"].ToString()),
                        other_provisions = (dr_datarow["other_provisions"].ToString()),
                        othercurrent_liabilities = (dr_datarow["othercurrent_liabilities"].ToString()),
                        deferredtax_liability = (dr_datarow["deferredtax_liability"].ToString()),
                        otherprovision_total = (dr_datarow["otherprovision_total"].ToString()),
                        ordinaryshare_capital = (dr_datarow["ordinaryshare_capital"].ToString()),
                        capitalredemption_reserve = (dr_datarow["capitalredemption_reserve"].ToString()),
                        general_reserve = (dr_datarow["general_reserve"].ToString()),
                        investment_allowancereserve = (dr_datarow["investment_allowancereserve"].ToString()),
                        surplusdeficitpandl_account = (dr_datarow["surplusdeficitpandl_account"].ToString()),
                        prefsharecapital_debenture = (dr_datarow["prefsharecapital_debenture"].ToString()),
                        revaluation_reserve = (dr_datarow["revaluation_reserve"].ToString()),
                        other_reserves = (dr_datarow["other_reserves"].ToString()),
                        capital_reserves = (dr_datarow["capital_reserves"].ToString()),
                        sharepremium_acct = (dr_datarow["sharepremium_acct"].ToString()),
                        advancetowards_capital = (dr_datarow["advancetowards_capital"].ToString()),
                        deposit_relatives = (dr_datarow["deposit_relatives"].ToString()),
                        net_worth = (dr_datarow["net_worth"].ToString()),
                        total_liabilities = (dr_datarow["total_liabilities"].ToString()),
                        claimscmpyack_debts = (dr_datarow["claimscmpyack_debts"].ToString()),
                        arrearsofsalary_susemployee = (dr_datarow["arrearsofsalary_susemployee"].ToString()),
                        estimated_capitalacct = (dr_datarow["estimated_capitalacct"].ToString()),
                        guaranteesbankers_company = (dr_datarow["guaranteesbankers_company"].ToString()),
                        incurrent_account = (dr_datarow["incurrent_account"].ToString()),
                        fixeddeposits_banks = (dr_datarow["fixeddeposits_banks"].ToString()),
                        othercash_bankbalance = (dr_datarow["othercash_bankbalance"].ToString()),
                        cashbankbalance_total = (dr_datarow["cashbankbalance_total"].ToString()),
                        government_securities = (dr_datarow["government_securities"].ToString()),
                        otherapproved_securities = (dr_datarow["otherapproved_securities"].ToString()),
                        shares = (dr_datarow["shares"].ToString()),
                        debentures_bonds = (dr_datarow["debentures_bonds"].ToString()),
                        subsidiariesjoint_ventures = (dr_datarow["subsidiariesjoint_ventures"].ToString()),
                        other_investments = (dr_datarow["other_investments"].ToString()),
                        investments_total = (dr_datarow["investments_total"].ToString()),
                        loanspayable_demand = (dr_datarow["loanspayable_demand"].ToString()),
                        term_loans = (dr_datarow["term_loans"].ToString()),
                        other_advances = (dr_datarow["other_advances"].ToString()),
                        advances_total = (dr_datarow["advances_total"].ToString()),
                        interest_accrued = (dr_datarow["interest_accrued"].ToString()),
                        advancetax_deducted = (dr_datarow["advancetax_deducted"].ToString()),
                        asset_others = (dr_datarow["asset_others"].ToString()),
                        othersssets_total = (dr_datarow["othersssets_total"].ToString()),
                        land_building = (dr_datarow["land_building"].ToString()),
                        computers = (dr_datarow["computers"].ToString()),
                        plant_machinery = (dr_datarow["plant_machinery"].ToString()),
                        furnitures_fixtures = (dr_datarow["furnitures_fixtures"].ToString()),
                        otherfixed_assets = (dr_datarow["otherfixed_assets"].ToString()),
                        capital_wip = (dr_datarow["capital_wip"].ToString()),
                        less_depreciation = (dr_datarow["less_depreciation"].ToString()),
                        net_block = (dr_datarow["net_block"].ToString()),
                        miscellaneous_expenses = (dr_datarow["miscellaneous_expenses"].ToString()),
                        patents_good = (dr_datarow["patents_good"].ToString()),
                        debitbalancepandl_account = (dr_datarow["debitbalancepandl_account"].ToString()),
                        unsecureddebtors_doubtful = (dr_datarow["unsecureddebtors_doubtful"].ToString()),
                        total_intangibleassets = (dr_datarow["total_intangibleassets"].ToString()),
                        total_assets = (dr_datarow["total_assets"].ToString()),
                        totalliabilities_totalasset = (dr_datarow["totalliabilities_totalasset"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                    });
                }
                values.creditbalancesheettemplate2_list = getcreditbalancesheettemplate2_list;
            }
            dt_datatable.Dispose();
        }


        public void DaGetSummaryTemplate1View(string credit_gid, string application_gid, string template_name, MdlSummaryTemplate1View values)
        {

            try
            {
                msSQL = " select date, audit_type, net_sales, other_income, total_revenue, growth_in_revenues, ebitda, ebitda_margin, depreciation," +
                        " interest, pat, pat_margin, total_outside_liabilities, total_bank_borrowings, tangible_net_worth, current_ratio, tol_tnw," +
                        " interest_coverage_ratio, dscr, sundry_creditors, sundry_debtors, inventories, payable_noofdays, recievable_noofdays, inventory_noofdays," +
                        " workingcapital_noofdays, debt_ebitda, msme" +
                        " from ocs_trn_tcadsummarytemplate1" +
                        " where application_gid='" + application_gid + "'" +
                        " and credit_gid='" + credit_gid + "' and template_name='" + template_name + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);

                var getsummarytemplate1_list = new List<summarytemplate1_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getsummarytemplate1_list.Add(new summarytemplate1_list
                        {
                            date = (dr_datarow["date"].ToString().Replace('#', '.')),
                            audit_type = (dr_datarow["audit_type"].ToString()),
                            net_sales = (dr_datarow["net_sales"].ToString()),
                            other_income = (dr_datarow["other_income"].ToString()),
                            total_revenue = (dr_datarow["total_revenue"].ToString()),
                            growth_in_revenues = (dr_datarow["growth_in_revenues"].ToString()),
                            ebitda = (dr_datarow["ebitda"].ToString()),
                            ebitda_margin = (dr_datarow["ebitda_margin"].ToString()),
                            depreciation = (dr_datarow["depreciation"].ToString()),
                            interest = (dr_datarow["interest"].ToString()),
                            pat = (dr_datarow["pat"].ToString()),

                            pat_margin = (dr_datarow["pat_margin"].ToString()),
                            total_outside_liabilities = (dr_datarow["total_outside_liabilities"].ToString()),
                            total_bank_borrowings = (dr_datarow["total_bank_borrowings"].ToString()),
                            tangible_net_worth = (dr_datarow["tangible_net_worth"].ToString()),
                            current_ratio = (dr_datarow["current_ratio"].ToString()),
                            tol_tnw = (dr_datarow["tol_tnw"].ToString()),
                            interest_coverage_ratio = (dr_datarow["interest_coverage_ratio"].ToString()),
                            dscr = (dr_datarow["dscr"].ToString()),
                            sundry_creditors = (dr_datarow["sundry_creditors"].ToString()),
                            sundry_debtors = (dr_datarow["sundry_debtors"].ToString()),

                            inventories = (dr_datarow["inventories"].ToString()),
                            payable_noofdays = (dr_datarow["payable_noofdays"].ToString()),
                            recievable_noofdays = (dr_datarow["recievable_noofdays"].ToString()),
                            inventory_noofdays = (dr_datarow["inventory_noofdays"].ToString()),
                            workingcapital_noofdays = (dr_datarow["workingcapital_noofdays"].ToString()),
                            debt_ebitda = (dr_datarow["debt_ebitda"].ToString()),
                            msme = (dr_datarow["msme"].ToString()),


                        });
                    }
                    values.summarytemplate1_list = getsummarytemplate1_list;
                }
                dt_datatable.Dispose();
                values.status = true;

            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }


        public void DaGetSummaryTemplate2View(string credit_gid, string application_gid, string template_name, MdlSummaryTemplate2View values)
        {

            try
            {
                msSQL = " select date, audit_type, interest_earned, other_income, total_income, growth_in_income, interest_expenses, operating_expenses, provision_and_contingencies," +
                        " net_profit, total_debt, tangible_networth, net_interest_income, assets_undermanagement, nim, loan_disbursed, crar," +
                        " debt_equity, operational_selfsufficiency_ratio, costtoincome_ratio, returnon_avgassets, returnon_avgnetworth, gross_npa, net_npa, gross_npapercent," +
                        " net_npapercent, netnpa_networth" +
                        " from ocs_trn_tcadsummarytemplate2" +
                        " where application_gid='" + application_gid + "'" +
                        " and credit_gid='" + credit_gid + "' and template_name='" + template_name + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);

                var getsummarytemplate2_list = new List<summarytemplate2_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getsummarytemplate2_list.Add(new summarytemplate2_list
                        {
                            date = (dr_datarow["date"].ToString().Replace('#', '.')),
                            audit_type = (dr_datarow["audit_type"].ToString()),
                            interest_earned = (dr_datarow["interest_earned"].ToString()),
                            other_income = (dr_datarow["other_income"].ToString()),
                            total_income = (dr_datarow["total_income"].ToString()),
                            growth_in_income = (dr_datarow["growth_in_income"].ToString()),
                            interest_expenses = (dr_datarow["interest_expenses"].ToString()),
                            operating_expenses = (dr_datarow["operating_expenses"].ToString()),
                            provision_and_contingencies = (dr_datarow["provision_and_contingencies"].ToString()),
                            net_profit = (dr_datarow["net_profit"].ToString()),
                            total_debt = (dr_datarow["total_debt"].ToString()),

                            tangible_networth = (dr_datarow["tangible_networth"].ToString()),
                            net_interest_income = (dr_datarow["net_interest_income"].ToString()),
                            assets_undermanagement = (dr_datarow["assets_undermanagement"].ToString()),
                            nim = (dr_datarow["nim"].ToString()),
                            loan_disbursed = (dr_datarow["loan_disbursed"].ToString()),
                            crar = (dr_datarow["crar"].ToString()),
                            debt_equity = (dr_datarow["debt_equity"].ToString()),
                            operational_selfsufficiency_ratio = (dr_datarow["operational_selfsufficiency_ratio"].ToString()),
                            costtoincome_ratio = (dr_datarow["costtoincome_ratio"].ToString()),
                            returnon_avgassets = (dr_datarow["returnon_avgassets"].ToString()),

                            returnon_avgnetworth = (dr_datarow["returnon_avgnetworth"].ToString()),
                            gross_npa = (dr_datarow["gross_npa"].ToString()),
                            net_npa = (dr_datarow["net_npa"].ToString()),
                            gross_npapercent = (dr_datarow["gross_npapercent"].ToString()),
                            net_npapercent = (dr_datarow["net_npapercent"].ToString()),
                            netnpa_networth = (dr_datarow["netnpa_networth"].ToString()),
                        });
                    }
                    values.summarytemplate2_list = getsummarytemplate2_list;
                }
                dt_datatable.Dispose();
                values.status = true;

            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetProfitLossTemp2List(string application_gid, string credit_gid, string template_name, MdlMstProfitLosstemp2 values)
        {
            msSQL = " select credirprofitlosstemp2_gid, audited, interest_income, incomeon_investments, interest_others, income_others, " +
                    " totalinterest_earned, other_income, profit_sales, miscellaneous_income, totalother_income, total_income, increase_income, expenditure, " +
                    " expenedinterest_borrower, expenedinterest_deposit, expened_other, totalinterest_expened, operating_expenses, employee_cost, depreciation, " +
                    " other_operating_cost, total_operating_expenses, provision_asset, provision_nonasset, provision_tax, other_provision, total_provision, " +
                    " total_expenditure, pbt, income_tax, pat, amount, rent, retained_profit, allfiguresin_inr, " +
                    " template_name, application_gid, credit_gid from ocs_trn_tcadcreditprofitlosstemp2 " +
                    " where application_gid='" + application_gid + "'" +
                    " and credit_gid='" + credit_gid + "' and template_name='" + template_name + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getprofitlosstemp2_list = new List<profitlosstemp2_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getprofitlosstemp2_list.Add(new profitlosstemp2_list
                    {
                        audited = dr_datarow["audited"].ToString(),
                        interest_income = (dr_datarow["interest_income"].ToString()),
                        incomeon_investments = (dr_datarow["incomeon_investments"].ToString()),
                        interest_others = dr_datarow["interest_others"].ToString(),
                        income_others = dr_datarow["income_others"].ToString(),
                        totalinterest_earned = dr_datarow["totalinterest_earned"].ToString(),
                        other_income = (dr_datarow["other_income"].ToString()),
                        profit_sales = (dr_datarow["profit_sales"].ToString()),
                        miscellaneous_income = dr_datarow["miscellaneous_income"].ToString(),
                        totalother_income = dr_datarow["totalother_income"].ToString(),
                        total_income = dr_datarow["total_income"].ToString(),
                        increase_income = (dr_datarow["increase_income"].ToString()),
                        expenditure = (dr_datarow["expenditure"].ToString()),
                        expenedinterest_borrower = dr_datarow["expenedinterest_borrower"].ToString(),
                        expenedinterest_deposit = dr_datarow["expenedinterest_deposit"].ToString(),
                        expened_other = dr_datarow["expened_other"].ToString(),
                        totalinterest_expened = (dr_datarow["totalinterest_expened"].ToString()),
                        operating_expenses = (dr_datarow["operating_expenses"].ToString()),
                        employee_cost = dr_datarow["employee_cost"].ToString(),
                        depreciation = dr_datarow["depreciation"].ToString(),
                        other_operating_cost = dr_datarow["other_operating_cost"].ToString(),
                        total_operating_expenses = (dr_datarow["total_operating_expenses"].ToString()),
                        provision_asset = (dr_datarow["provision_asset"].ToString()),
                        provision_nonasset = dr_datarow["provision_nonasset"].ToString(),
                        provision_tax = dr_datarow["provision_tax"].ToString(),
                        other_provision = dr_datarow["other_provision"].ToString(),
                        total_provision = dr_datarow["total_provision"].ToString(),
                        total_expenditure = dr_datarow["total_expenditure"].ToString(),
                        pbt = (dr_datarow["pbt"].ToString()),
                        income_tax = (dr_datarow["income_tax"].ToString()),
                        pat = (dr_datarow["pat"].ToString()),
                        amount = dr_datarow["amount"].ToString(),
                        rent = dr_datarow["rent"].ToString(),
                        retained_profit = dr_datarow["retained_profit"].ToString(),
                        template_name = dr_datarow["template_name"].ToString(),
                        application_gid = dr_datarow["application_gid"].ToString(),
                        credit_gid = dr_datarow["credit_gid"].ToString(),
                        allfiguresin_inr = (dr_datarow["allfiguresin_inr"].ToString().Replace('#', '.')),
                    });
                }
                values.profitlosstemp2_list = getprofitlosstemp2_list;
            }
            dt_datatable.Dispose();
        }

        public void DaCADGroupDocChecklistinfo(string application_gid, string credit_gid, string employee_gid)
        {
            try
            {
                msSQL = "select institution_gid from ocs_trn_tcadinstitution where institution_gid='" + credit_gid + "'";
                string lsinstitution = objdbconn.GetExecuteScalar(msSQL);
                msSQL = "select contact_gid from ocs_trn_tcadcontact where contact_gid='" + credit_gid + "'";
                string lsindividual = objdbconn.GetExecuteScalar(msSQL);
                msSQL = "select group_gid from ocs_trn_tcadgroup where group_gid='" + credit_gid + "'";
                string lsgroup = objdbconn.GetExecuteScalar(msSQL);

                mdlgroupdocumentchecklist groupdocument = new mdlgroupdocumentchecklist();
                msSQL = " select groupdocumentchecklist_gid,application_gid,mstdocument_gid,credit_gid " +
                           " from ocs_trn_tcadgroupdocumentchecklist where application_gid='" + application_gid + "'" +
                           " and  credit_gid='" + credit_gid + "'";
                dt_datatable1 = objdbconn.GetDataTable(msSQL);
                var getgroupdocumentdtlList = new List<mdlgroupdocumentchecklist>();
                if (dt_datatable1.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable1.Rows)
                    {
                        getgroupdocumentdtlList.Add(new mdlgroupdocumentchecklist
                        {
                            groupdocumentchecklist_gid = dt["groupdocumentchecklist_gid"].ToString(),
                            application_gid = dt["application_gid"].ToString(),
                            credit_gid = dt["credit_gid"].ToString(),
                            mstdocument_gid = dt["mstdocument_gid"].ToString(),
                        });
                    }
                }
                dt_datatable1.Dispose();
                string lsmstgid = "", lsdocument_code = "";
                msSQL = " select companydocument_gid,individualdocument_gid,groupdocument_gid,documentcheckdtl_gid,documenttype_name,covenant_type,documenttype_gid,documenttype_code,tagged_by from ocs_trn_tcaddocumentchecktls where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                        " and groupdocumentchecklist_gid is null";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        if (lsinstitution != "")
                        {
                            groupdocument = getgroupdocumentdtlList.Where(a => a.mstdocument_gid == dt["companydocument_gid"].ToString()).FirstOrDefault();
                            lsmstgid = dt["companydocument_gid"].ToString();
                            msSQL = "select document_code from ocs_mst_tcompanydocument where companydocument_gid='" + lsmstgid + "'";
                            lsdocument_code = objdbconn.GetExecuteScalar(msSQL);
                        }
                        //msSQL ="select groupdocumentchecklist_gid from ocs_trn_tgroupdocumentchecklist where application_gid = '" + application_gid + "' and credit_gid = '" + credit_gid + "'
                        else if (lsindividual != "")
                        {
                            groupdocument = getgroupdocumentdtlList.Where(a => a.mstdocument_gid == dt["individualdocument_gid"].ToString()).FirstOrDefault();
                            lsmstgid = dt["individualdocument_gid"].ToString();
                            msSQL = "select document_code from ocs_mst_tindividualdocument where individualdocument_gid='" + lsmstgid + "'";
                            lsdocument_code = objdbconn.GetExecuteScalar(msSQL);
                        }
                        else
                        {
                            groupdocument = getgroupdocumentdtlList.Where(a => a.mstdocument_gid == dt["groupdocument_gid"].ToString()).FirstOrDefault();
                            lsmstgid = dt["groupdocument_gid"].ToString();
                            msSQL = "select document_code from ocs_mst_tgroupdocument where groupdocument_gid='" + lsmstgid + "'";
                            lsdocument_code = objdbconn.GetExecuteScalar(msSQL);
                        }

                        if (groupdocument != null && groupdocument.groupdocumentchecklist_gid != null)
                        {
                            // Update Event ...//
                            msSQL = " update ocs_trn_tcaddocumentchecktls set groupdocumentchecklist_gid ='" + groupdocument.groupdocumentchecklist_gid + "'" +
                                    " where documentcheckdtl_gid ='" + dt["documentcheckdtl_gid"].ToString() + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        else
                        {
                            // Insert Event ...//
                            string msGetgroupDocGID = objcmnfunctions.GetMasterGID("GDCG");
                            msSQL = " insert into ocs_trn_tcadgroupdocumentchecklist(" +
                                      " groupdocumentchecklist_gid," +
                                      " application_gid," +
                                      " credit_gid, " +
                                       " document_code, " +
                                      " mstdocument_gid, " +
                                      " mstdocument_name, " +
                                      " mstcovenant_type," +
                                  " mstdocumenttype_gid," +
                                  " mstdocumenttype_name," +
                                  " tagged_by, " +
                                  " created_date," +
                                  " created_by)" +
                                  " VALUES(" +
                                  "'" + msGetgroupDocGID + "'," +
                                  "'" + application_gid + "'," +
                                  "'" + credit_gid + "'," +
                                  "'" + lsdocument_code + "'," +
                                  "'" + lsmstgid + "'," +
                                  "'" + dt["documenttype_name"].ToString() + "'," +
                                  "'" + dt["covenant_type"].ToString() + "'," +
                                  "'" + dt["documenttype_gid"].ToString() + "'," +
                                  "'" + dt["documenttype_code"].ToString() + "'," +
                                  "'" + dt["tagged_by"].ToString() + "'," +
                                  "current_timestamp," +
                                  "'" + employee_gid + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " update ocs_trn_tcaddocumentchecktls set groupdocumentchecklist_gid ='" + msGetgroupDocGID + "'" +
                                   " where documentcheckdtl_gid ='" + dt["documentcheckdtl_gid"].ToString() + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            getgroupdocumentdtlList.Add(new mdlgroupdocumentchecklist
                            {
                                groupdocumentchecklist_gid = msGetgroupDocGID,
                                mstdocument_gid = lsmstgid,
                            });
                        }

                    }
                }
                dt_datatable.Dispose();

                // For Covenant Document 
                mdlgroupdocumentchecklist groupcovenantdocument = new mdlgroupdocumentchecklist();
                msSQL = " select groupcovdocumentchecklist_gid,application_gid,mstdocument_gid,credit_gid " +
                        " from ocs_trn_tcadgroupcovenantdocumentchecklist where application_gid='" + application_gid + "'" +
                        " and credit_gid='" + credit_gid + "'";
                dt_datatable1 = objdbconn.GetDataTable(msSQL);
                var getgroupcovenantdocumentList = new List<mdlgroupdocumentchecklist>();
                if (dt_datatable1.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable1.Rows)
                    {
                        getgroupcovenantdocumentList.Add(new mdlgroupdocumentchecklist
                        {
                            groupcovdocumentchecklist_gid = dt["groupcovdocumentchecklist_gid"].ToString(),
                            application_gid = dt["application_gid"].ToString(),
                            credit_gid = dt["credit_gid"].ToString(),
                            mstdocument_gid = dt["mstdocument_gid"].ToString(),
                        });
                    }
                }
                dt_datatable1.Dispose();

                msSQL = " select companydocument_gid,individualdocument_gid,groupdocument_gid,covenantdocumentcheckdtl_gid,documenttype_name,covenant_type,documenttype_gid,documenttype_code,tagged_by from ocs_trn_tcadcovanantdocumentcheckdtls where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                       " and groupcovdocumentchecklist_gid is null";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        if (lsinstitution != "")
                        {
                            groupcovenantdocument = getgroupcovenantdocumentList.Where(a => a.mstdocument_gid == dt["companydocument_gid"].ToString()).FirstOrDefault();
                            lsmstgid = dt["companydocument_gid"].ToString();
                            msSQL = "select document_code from ocs_mst_tcompanydocument where companydocument_gid='" + lsmstgid + "'";
                            lsdocument_code = objdbconn.GetExecuteScalar(msSQL);
                        }
                        else if (lsindividual != "")
                        {
                            groupcovenantdocument = getgroupcovenantdocumentList.Where(a => a.mstdocument_gid == dt["individualdocument_gid"].ToString()).FirstOrDefault();
                            lsmstgid = dt["individualdocument_gid"].ToString();
                            msSQL = "select document_code from ocs_mst_tindividualdocument where individualdocument_gid='" + lsmstgid + "'";
                            lsdocument_code = objdbconn.GetExecuteScalar(msSQL);
                        }
                        else
                        {
                            groupcovenantdocument = getgroupcovenantdocumentList.Where(a => a.mstdocument_gid == dt["groupdocument_gid"].ToString()).FirstOrDefault();
                            lsmstgid = dt["groupdocument_gid"].ToString();
                            msSQL = "select document_code from ocs_mst_tgroupdocument where groupdocument_gid='" + lsmstgid + "'";
                            lsdocument_code = objdbconn.GetExecuteScalar(msSQL);
                        }

                        if (groupcovenantdocument != null && groupcovenantdocument.groupcovdocumentchecklist_gid != null)
                        {
                            // Update Event ...//
                            msSQL = " update ocs_trn_tcadcovanantdocumentcheckdtls set groupcovdocumentchecklist_gid ='" + groupcovenantdocument.groupcovdocumentchecklist_gid + "'" +
                                    " where covenantdocumentcheckdtl_gid ='" + dt["covenantdocumentcheckdtl_gid"].ToString() + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        else
                        {

                            // Insert Event ...//
                            string msGetgroupCovDocGID = objcmnfunctions.GetMasterGID("GCDG");
                            msSQL = " insert into ocs_trn_tcadgroupcovenantdocumentchecklist(" +
                                      " groupcovdocumentchecklist_gid," +
                                      " application_gid," +
                                      " credit_gid, " +
                                      " document_code, " +
                                      " mstdocument_gid, " +
                                      " mstdocument_name, " +
                                      " mstcovenant_type," +
                                  " mstdocumenttype_gid," +
                                  " mstdocumenttype_name," +
                                  " tagged_by, " +
                                  " created_date," +
                                  " created_by)" +
                                  " VALUES(" +
                                  "'" + msGetgroupCovDocGID + "'," +
                                  "'" + application_gid + "'," +
                                  "'" + credit_gid + "'," +
                                  "'" + lsdocument_code + "'," +
                                  "'" + lsmstgid + "'," + 
                                  "'" + dt["documenttype_name"].ToString() + "'," +
                                  "'" + dt["covenant_type"].ToString() + "'," +
                                  "'" + dt["documenttype_gid"].ToString() + "'," +
                                  "'" + dt["documenttype_code"].ToString() + "'," +
                                  "'" + dt["tagged_by"].ToString() + "'," +
                                  "current_timestamp," +
                                  "'" + employee_gid + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " update ocs_trn_tcadcovanantdocumentcheckdtls set groupcovdocumentchecklist_gid ='" + msGetgroupCovDocGID + "'" +
                                   " where covenantdocumentcheckdtl_gid ='" + dt["covenantdocumentcheckdtl_gid"].ToString() + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            getgroupcovenantdocumentList.Add(new mdlgroupdocumentchecklist
                            {
                                groupcovdocumentchecklist_gid = msGetgroupCovDocGID,
                                mstdocument_gid = lsmstgid,
                            });
                        }

                    }
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {

            }
        }


        //Get Lables

        public void DaGetlabels(string credit_gid, MdlCreditView values)
        {

            msSQL = " select company_name, stakeholder_type from ocs_trn_tcadinstitution " +

               " where institution_gid = '" + credit_gid + "' ";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.customer_name = objODBCDatareader["company_name"].ToString();
                values.stakeholder_type = objODBCDatareader["stakeholder_type"].ToString();

            }

            else if (objODBCDatareader.HasRows == false)
            {

                msSQL = " select concat_ws(' ', first_name, last_name, middle_name) as individual_name, stakeholder_type from ocs_trn_tcadcontact " +

              " where contact_gid = '" + credit_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.customer_name = objODBCDatareader["individual_name"].ToString();
                    values.stakeholder_type = objODBCDatareader["stakeholder_type"].ToString();

                }
            }

            objODBCDatareader.Close();

        }

    }
}