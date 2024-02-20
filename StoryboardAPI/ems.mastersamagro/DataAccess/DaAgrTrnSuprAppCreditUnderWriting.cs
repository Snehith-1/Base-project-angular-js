using ems.mastersamagro.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Web;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Text.RegularExpressions;
using ems.storage.Functions;

namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess will provide access to add, edit, view datas in supplier credit stages.
    /// </summary>
    /// <remarks>Written by Sherin Augusta, Logapriya, Abilash.A, Premchander.K </remarks>
    public class DaAgrTrnSuprAppCreditUnderWriting
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        HttpPostedFile httpPostedFile;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGetDocumentGid, msGetGid1;
        int mnResult;
        string lsgeneticcode_gid, lsgeneticcode_name, lsgeneticcode_status, lsgeneticcode_remarks, lscreditgeneticcode_gid, lscredit_gid;
        string lsexistingbankfacility_gid, lsbank_gid, lsbank_name, lsfacilitysanctioned_on, lsfundedtypeindicator_gid, lsfundedtypeindicator_name, lssanction_limit;
        string lscreditinstalmentfrequency_gid, lscreditinstalmentfrequency_name, lsinstalment_amount, lsoutstanding_amount, lsoverdue_amount, lsnumberofdays_overdue;
        string lsoverdueifany_dpd, lscreditaccountclassification_gid, lscreditaccountclassification_name, lsremarks, lsrecord_date, lscreditrepaymentdtl_gid;
        string lslendertype_gid, lslender_type, lsifsc_code, lsnbfc_name, lsbranch_name, lsfacility_type, lssanctionreference_id, lssanctioned_on, lssanction_amount;
        string lsaccountstatus_on, lscurrentoutsatnding_amount, lsinstalment_frequency, lsdemanddue_date, lsoringinaltenure_year, lsoringinaltenure_month, lsfacilitytype_gid;
        string lsoringinaltenure_days, lsbalancetenure_year, lsbalancetenure_month, lsbalancetenure_days, lsaccountclassification_gid, lsaccount_classification;
        string lssupplier_gid, lssupplier_name, lsrelationship_vintage_year, lsrelationship_vintage_month, lspurchase_amount, lsbankdebit_amount, lsrelationship_supplier,
             lsstart_date, lsend_date, lsapplication_gid, lsapplication_no, lsinstitution2branch_gid, lsinstitution2mobileno_gid;
        string lsbuyer_gid, lsbuyer_name, lsbuyer_limit, lsavailed_limit, lsbalance_limit, lstop_buyer, lsbill_tenuredays, lsmargin, lsbankcredit_value, lsbankcredit_date;
        string lssource_deduction, lsrelationship_borrower, lsenduse_monitoring, lsinstitution2email_gid, lsinstitution2address_gid, lsinstitution2licensedtl_gid;
        string lscreditpolicy_gid, lscredit_policy, lscomplied_status, lsobservation;
        string lspath, lslatitude, lslongitude;
        string lsbank_address, lsmicr_code, lsbankaccount_name, lsbankaccounttype_gid, lsbankaccounttype_name, lsbankaccount_number, lsconfirmbankaccountnumber,
                   lsjoinaccount_status, lsjoinaccount_name, lschequebook_status, lsaccountopen_date;
        string lscompany_name, lsdate_incorporation, lsbusinessstart_date, lscompanypan_no, lsmonth_business, lscin_no,
        lsofficial_telephoneno, lsofficialemail_address, lscompanytype_gid, lscompanytype_name, lsstakeholder_type, lsstakeholdertype_gid, lsassessmentagency_gid,
        lsassessmentagency_name, lsassessmentagencyrating_gid, lsratingas_on, lsamlcategory_gid, lsamlcategory_name, lsbusinesscategory_gid,
        lsbusinesscategory_name, lscontactperson_firstname, lscontactperson_middlename, lscontactperson_lastname, lsdesignation_gid, lsdesignation, lslastyear_turnover,
        lsescrow, lsurn_status, lsurn, lsyear_business, lsassessmentagencyrating_name, lsgst_state, lsgst_no, lsgst_registered;
        string lsmobile_no, lsprimary_status, lswhatsapp_no, lsemail_address;
        string lsaddress_typegid, lsaddress_type, lsaddressline1, lsaddressline2, lslandmark, lstaluka, lspostal_code, lscity, lsdistrict, lsstate, lscountry;
        string lslicensetype_gid, lslicensetype_name, lslicense_number, lslicenseissue_date, lslicenseexpiry_date,
        lscontact2mobileno_gid, lscontact2email_gid, lscontact_gid, lscontact2address_gid, lscustomer_urn, lscustomer_name, lsvertical_gid,
        lsvertical_name, lsverticaltaggs_gid, lsverticaltaggs_name, lsconstitution_gid, lsconstitution_name, lsbusinessunit_gid, lsbusinessunit_name,
        lssa_status, lsvernacularlanguage_gid, lsvernacular_language, lscontactpersonfirst_name, lscontactpersonmiddle_name, lscontactpersonlast_name,
        lsdesignation_type, lslandline_no, lssa_name, lsapplication2geneticcode_gid, lsgenetic_status, lsgenetic_remarks, lsprimary_emailaddress,
        lsapplication2email_gid, lsprimary_mobileno, lswhatsapp_mobileno, lsapplication2contact_gid, lsmaritalstatus_gid, lsmaritalstatus_name;
        string lspan_no, lsaadhar_no, lsfirst_name, lsmiddle_name, lslast_name, lsindividual_dob, lsage, lsgender_gid, lsgender_name, lsdesignation_name,
        lseducationalqualification_gid, lseducationalqualification_name, lsmain_occupation, lsannual_income, lsmonthly_income, lspep_status, lspepverified_date;
        string lsfather_firstname, lsfather_middlename, lsfather_lastname, lsfather_dob, lsfather_age, lsmother_firstname, lsmother_middlename,
        lsmother_lastname, lsmother_dob, lsmother_age, lsspouse_firstname, lsspouse_middlename, lsspouse_lastname, lsspouse_age, lsownershiptype_gid,
        lsownershiptype_name, lsresidencetype_gid, lsresidencetype_name, lscurrentresidence_years, lsbranch_distance, lspropertyholder_gid,
        lspropertyholder_name, lsincometype_gid, lsincometype_name, lsgroup_gid, lsgroup_name, lsprofile, lsspouse_dob, lsfathernominee_status,
        lsmothernominee_status, lsspousenominee_status, lsothernominee_status, lsrelationshiptype, lsnomineefirst_name, lsnominee_middlename,
        lsnominee_lastname, lsnominee_dob, lsnominee_age, lstotallandinacres, lscultivatedland, lspreviouscrop, lsprposedcrop, lsinstitution_gid, lsinstitution_name;
        string lsbank_accountno, lsaccountholder_name, lsbank_branch, lsgroup2bank_gid, lsgroup2address_gid, lsapplication2hypothecation_gid;
        string lssecuritytype_gid, lssecurity_type, lssecurity_description, lssecurity_value, lssecurityassessed_date, lsasset_id, lsroc_fillingid, lsgroup_status,
        lshypoobservation_summary, lsprimary_security, lsCERSAI_fillingid, lsdate_of_formation, lsgroup_type, lsgroupmember_count, lsgroupurn_status, lsgroup_urn;
        string lsprocessing_fee, lsprocessing_collectiontype, lsdoc_charges, lsdoccharge_collectiontype, lsfieldvisit_charge, lsadhoc_fee,
        lsadhoc_collectiontype, lslife_insurance, lslifeinsurance_collectiontype, lsacct_insurance, lstotal_collect, lstotal_deduct, lsproduct_type, lsproducttype_gid, lsacctinsurance_collectiontype,
            lsfieldvisit_charges_collectiontype, lscreditgroup_gid, lscreditgroup_name, lsregion;
        string lsmonth, lsyear, lstotaldebits, lstotalcredits, lsaccttransferdebits, lsaccttransfercredits, lsloansrepayment, lscashdeposits, lspurchasepayments, lssalesreceipts, lschequeneftinward, lschequeneftoutward, lsoverdrawingscc, lssalesgst, excelRange, lsprogram_gid, lsprogram_name;
        int rowCount, columnCount;
        int columnNumber;
        string endRange, colName;
        int columnInsertCount;
        string lsproduct_gid, lsproduct_name, lsvariety_gid, lsvariety_name, lssector_name, lscategory_name, lsbotanical_name, lsalternative_name;
        int matchCount1, matchCount2;
        string lspan_status, msGetGidpan;

        public void DaPostGeneticCode(string employee_gid, MdlMstCUWGeneticCode values)
        {
            msSQL = "select geneticcode_gid from agr_mst_tcreditgeneticcode where application_gid='" + values.application_gid + "' and geneticcode_gid='" + values.geneticcode_gid + "'";
            string lsgenetic_code = objdbconn.GetExecuteScalar(msSQL);
            if (lsgenetic_code == (values.geneticcode_gid))
            {
                values.status = false;
                values.message = "Already Genetic Code Added";
                return;
            }
            msGetGid = objcmnfunctions.GetMasterGID("CRGN");
            msSQL = " insert into agr_mst_tcreditgeneticcode(" +
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

        public void DaGetGeneticCodeList(string credit_gid, string employee_gid, MdlMstCUWGeneticCode values)
        {
            msSQL = " select creditgeneticcode_gid,geneticcode_gid,geneticcode_name,geneticcode_status,geneticcode_remarks,application_gid, credit_gid, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                    " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as updated_by " +
                    " from agr_mst_tcreditgeneticcode a " +
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

        public void DaEditGeneticCode(string creditgeneticcode_gid, MdlMstCUWGeneticCode values)
        {
            try
            {
                msSQL = " select creditgeneticcode_gid,geneticcode_gid,geneticcode_name,geneticcode_status,geneticcode_remarks, credit_gid" +
                    " from agr_mst_tcreditgeneticcode where creditgeneticcode_gid='" + creditgeneticcode_gid + "'";
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
                   " from agr_mst_tcreditgeneticcode where creditgeneticcode_gid='" + values.creditgeneticcode_gid + "'";
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
            objODBCDatareader.Close();
            msSQL = " update agr_mst_tcreditgeneticcode set " +
                         " geneticcode_status='" + values.geneticcode_status + "'," +
                         " geneticcode_remarks='" + values.geneticcode_remarks.Replace("'", " ") + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where creditgeneticcode_gid='" + values.creditgeneticcode_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                msGetGid = objcmnfunctions.GetMasterGID("GCUL");

                msSQL = "Insert into agr_mst_tcreditgeneticcodeupdatelog(" +
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
               "'" + lsgeneticcode_remarks + "'," +
               "'" + employee_gid + "'," +
               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Genetic Code Details Updated Successfully";
            }

        }

        public void DaDeleteGeneticCode(string creditgeneticcode_gid, MdlMstCUWGeneticCode values, string employee_gid)
        {
            msSQL = "delete from agr_mst_tcreditgeneticcode where creditgeneticcode_gid='" + creditgeneticcode_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tcreditgeneticcodeupdatelog where creditgeneticcode_gid='" + creditgeneticcode_gid + "'";
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

        public void DaSocialAndTradeCapitalSave(string employee_gid, MdlMstAppCreditUnderWriting values)
        {
            if (values.applicant_type == "Institution")
            {
                msSQL = " update agr_mst_tsuprinstitution set ";
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
                msSQL = " update agr_mst_tsuprcontact set ";
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
                msSQL = " update agr_mst_tsuprgroup set ";
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
                values.message = "Social/Trade Capital Details Saved Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Saving Social/Trade Capital Details";
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
                        " FROM agr_mst_tsuprinstitution a left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
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
                        " FROM agr_mst_tsuprcontact a left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
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
                        " FROM agr_mst_tsuprgroup a left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
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

        public void DaEditSocialAndTradeCapital(string credit_gid, string applicant_type, MdlMstAppCreditUnderWriting values)
        {
            try
            {
                if (applicant_type == "Institution")
                {
                    msSQL = " select social_capital, trade_capital, economical_flag from agr_mst_tsuprinstitution where institution_gid='" + credit_gid + "'";
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
                    msSQL = " select social_capital, trade_capital,economical_flag from agr_mst_tsuprcontact where contact_gid='" + credit_gid + "'";
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
                    msSQL = " select social_capital, trade_capital, economical_flag from agr_mst_tsuprgroup where group_gid='" + credit_gid + "'";
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

            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaSocialAndTradeCapitalSubmit(string employee_gid, MdlMstAppCreditUnderWriting values)
        {
            if (values.applicant_type == "Institution")
            {
                msSQL = " update agr_mst_tsuprinstitution set ";
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
                msSQL = " update agr_mst_tsuprcontact set ";
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
                msSQL = " update agr_mst_tsuprgroup set ";
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
                msSQL = " update agr_mst_tsuprinstitution set ";
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
                msSQL = " update agr_mst_tsuprcontact set ";
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
                msSQL = " update agr_mst_tsuprgroup set ";
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

        public void DaPSLDataFlaggingSave(string employee_gid, MdlMstAppCreditUnderWriting values)
        {
            if (values.applicant_type == "Institution")
            {
                msSQL = " update agr_mst_tsuprinstitution set " +
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
                msSQL = " update agr_mst_tsuprcontact set " +
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
                msSQL = " update agr_mst_tsuprgroup set " +
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

        public void DaGetPSLDataFlagging(string credit_gid, string applicant_type, MdlMstAppCreditUnderWriting values)
        {
            if (applicant_type == "Institution")
            {
                msSQL = " SELECT startupasofloansanction_date, occupation, lineofactivity, bsrcode, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                    " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as updated_by " +
                    " FROM agr_mst_tsuprinstitution a " +
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
                        " FROM agr_mst_tsuprcontact a " +
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
                        " FROM agr_mst_tsuprgroup a " +
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
                        " from agr_mst_tsuprinstitution where institution_gid='" + credit_gid + "'";
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
                         " from agr_mst_tsuprcontact where contact_gid='" + credit_gid + "'";
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
                    " from agr_mst_tsuprgroup where group_gid='" + credit_gid + "'";
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

            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaPSLDataFlaggingSubmit(string employee_gid, MdlMstAppCreditUnderWriting values)
        {
            if (values.applicant_type == "Institution")
            {
                msSQL = " update agr_mst_tsuprinstitution set " +
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
                msSQL = " update agr_mst_tsuprcontact set " +
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
                msSQL = " update agr_mst_tsuprgroup set " +
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
                msSQL = " update agr_mst_tsuprinstitution set " +
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
                    " where institution_gid='" + values.institution_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else if (values.applicant_type == "Individual")
            {
                msSQL = " update agr_mst_tsuprcontact set " +
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
                msSQL = " update agr_mst_tsuprgroup set " +
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

        public void DaPostExistingBankFacility(string employee_gid, MdlMstCUWExistingBankFacility values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("CRFD");
            msSQL = " insert into agr_mst_tsuprcreditbankfacilitydtl(" +
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
                msSQL += "'" + values.remarks.Replace(",", "") + "',";
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
                    " from agr_mst_tsuprcreditbankfacilitydtl a " +
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

        public void DaEditExistingBankFacility(string existingbankfacility_gid, MdlMstCUWExistingBankFacility values)
        {
            try
            {
                msSQL = " select existingbankfacility_gid,credit_gid,application_gid,bank_gid,bank_name, date_format(facilitysanctioned_on, '%d-%m-%Y') as facilitysanctioned_on, " +
                    " fundedtypeindicator_gid,fundedtypeindicator_name, sanctioned_limit, instalmentfrequency_gid, instalmentfrequency_name, " +
                    " instalment_amount, outstanding_amount, date_format(record_date, '%d-%m-%Y') as record_date, overdue_amount, " +
                   " overdue_dpd, accountclassification_gid, account_classification, remarks, facilitytype_gid, facility_type" +
                   " from agr_mst_tsuprcreditbankfacilitydtl where existingbankfacility_gid='" + existingbankfacility_gid + "'";
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
                  " from agr_mst_tsuprcreditbankfacilitydtl where existingbankfacility_gid='" + values.existingbankfacility_gid + "'";
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

            msSQL = " update agr_mst_tsuprcreditbankfacilitydtl set " +
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
                    " remarks='" + values.remarks + "'," +
                    " updated_by='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where existingbankfacility_gid='" + values.existingbankfacility_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                msGetGid = objcmnfunctions.GetMasterGID("EBFU");

                msSQL = " insert into agr_mst_tsuprcreditbankfacilitydtlupdatelog(" +
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
                "'" + lsremarks + "'," +
                "'" + employee_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Existing Bank Facility Details Updated Successfully";
            }
        }

        public void DaDeleteExistingBankFacility(string existingbankfacility_gid, MdlMstCUWExistingBankFacility values)
        {
            msSQL = "delete from agr_mst_tsuprcreditbankfacilitydtl where existingbankfacility_gid='" + existingbankfacility_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tsuprcreditbankfacilitydtlupdatelog where existingbankfacility_gid='" + existingbankfacility_gid + "'";
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

        public void DaPostRepaymentTrack(string employee_gid, MdlMstCUWRepaymentTrack values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("CRPY");
            msSQL = " insert into agr_mst_tsuprcreditrepaymentdtl(" +
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
                msSQL += "'" + values.remarks.Replace(",", "") + "',";
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
                    " from agr_mst_tsuprcreditrepaymentdtl a " +
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
                     " from agr_mst_tsuprcreditrepaymentdtl where creditrepaymentdtl_gid='" + creditrepaymentdtl_gid + "'";
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
                     " from agr_mst_tsuprcreditrepaymentdtl where creditrepaymentdtl_gid='" + values.creditrepaymentdtl_gid + "'";
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
            objODBCDatareader.Close();
            msSQL = " update agr_mst_tsuprcreditrepaymentdtl set " +
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
                msSQL += "overdue_amount='" + values.overdue_amount.Replace(",", "") + "',";
            }
            msSQL += " numberofdays_overdue='" + values.numberofdays_overdue + "'," +
                   " remarks='" + values.remarks.Replace(",", "") + "'," +
                   " updated_by='" + employee_gid + "'," +
                   " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                   " where creditrepaymentdtl_gid='" + values.creditrepaymentdtl_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                msGetGid = objcmnfunctions.GetMasterGID("RTUL");

                msSQL = " insert into agr_mst_tsuprcreditrepaymentdtlupdatelog(" +
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
                 "'" + lsremarks + "'," +
                 "'" + employee_gid + "'," +
                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Repayment Track Details Updated Successfully";
            }
        }

        public void DaDeleteRepaymentTrack(string creditrepaymentdtl_gid, MdlMstCUWRepaymentTrack values)
        {
            msSQL = "delete from agr_mst_tsuprcreditrepaymentdtl where creditrepaymentdtl_gid='" + creditrepaymentdtl_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tsuprcreditrepaymentdtlupdatelog where creditrepaymentdtl_gid='" + creditrepaymentdtl_gid + "'";
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

        // Lender Type List

        public void DaLenderTypeList(MdlPSLDropDown values)
        {
            msSQL = " select lendertype_gid, lendertype_name from ocs_mst_tlendertype where status='Y'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_lendertype = new List<lendertype_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                values.lendertype_list = dt_datatable.AsEnumerable().Select(row => new lendertype_list
                {
                    lendertype_gid = row["lendertype_gid"].ToString(),
                    lendertype_name = row["lendertype_name"].ToString()
                }
                ).ToList();
            }
            dt_datatable.Dispose();
        }

        // Credit Account Classification List

        public void DaCreditAccountClassificationList(MdlPSLDropDown values)
        {
            msSQL = " select creditaccountclassification_gid, creditaccountclassification_name from ocs_mst_tcreditaccountclassification where status='Y'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_creditaccountclassification = new List<creditaccountclassification_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                values.creditaccountclassification_list = dt_datatable.AsEnumerable().Select(row => new creditaccountclassification_list
                {
                    creditaccountclassification_gid = row["creditaccountclassification_gid"].ToString(),
                    creditaccountclassification_name = row["creditaccountclassification_name"].ToString()
                }
                ).ToList();
            }
            dt_datatable.Dispose();
        }

        // Credit Instalment Frequency List

        public void DaCreditInstalmentFrequencyList(MdlPSLDropDown values)
        {
            msSQL = " select creditinstalmentfrequency_gid, creditinstalmentfrequency_name from ocs_mst_tcreditinstalmentfrequency where status='Y'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_creditinstalmentfrequency = new List<creditinstalmentfrequency_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                values.creditinstalmentfrequency_list = dt_datatable.AsEnumerable().Select(row => new creditinstalmentfrequency_list
                {
                    creditinstalmentfrequency_gid = row["creditinstalmentfrequency_gid"].ToString(),
                    creditinstalmentfrequency_name = row["creditinstalmentfrequency_name"].ToString()
                }
                ).ToList();
            }
            dt_datatable.Dispose();
        }

        // Funded Type Indicator List

        public void DaFundedTypeIndicatorList(MdlPSLDropDown values)
        {
            msSQL = " SELECT fundedtypeindicator_gid,fundedtypeindicator_name FROM ocs_mst_tfundedtypeindicator where status='Y'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_fundedtypeindicator = new List<fundedtypeindicator_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                values.fundedtypeindicator_list = dt_datatable.AsEnumerable().Select(row => new fundedtypeindicator_list
                {
                    fundedtypeindicator_gid = row["fundedtypeindicator_gid"].ToString(),
                    fundedtypeindicator_name = row["fundedtypeindicator_name"].ToString()
                }
                ).ToList();
            }
            dt_datatable.Dispose();
        }

        // Credit Underwriting Facility Type List

        public void DaCreditUnderwritingFacilityTypeList(MdlPSLDropDown values)
        {
            msSQL = " SELECT creditunderwritingfacilitytype_gid,credit_underwriting_facility_type FROM ocs_mst_tcreditunderwritingfacilitytype where status='Y'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_creditunderwritingfacilitytype = new List<creditunderwritingfacilitytype_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                values.creditunderwritingfacilitytype_list = dt_datatable.AsEnumerable().Select(row => new creditunderwritingfacilitytype_list
                {
                    creditunderwritingfacilitytype_gid = row["creditunderwritingfacilitytype_gid"].ToString(),
                    credit_underwriting_facility_type = row["credit_underwriting_facility_type"].ToString()
                }
                ).ToList();
            }
            dt_datatable.Dispose();
        }

        // Bank Name List

        public void DaBankNameList(MdlPSLDropDown values)
        {
            msSQL = " SELECT bankname_gid,bankname_name FROM ocs_mst_tbankname where status='Y'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_bankdtl = new List<bankdtl_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                values.bankdtl_list = dt_datatable.AsEnumerable().Select(row => new bankdtl_list
                {
                    bankdtl_gid = row["bankname_gid"].ToString(),
                    bankdtl_name = row["bankname_name"].ToString()
                }
                ).ToList();
            }
            dt_datatable.Dispose();
        }

        // Client Details List

        public void DaClientDetailsList(MdlPSLDropDown values)
        {
            msSQL = " SELECT clientdetails_gid,clientdetails_name FROM ocs_mst_tclientdetails where status='Y'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_clientdetail = new List<clientdetail_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                values.clientdetail_list = dt_datatable.AsEnumerable().Select(row => new clientdetail_list
                {
                    clientdtl_gid = row["clientdetails_gid"].ToString(),
                    clientdtl_name = row["clientdetails_name"].ToString()
                }
                ).ToList();
            }
            dt_datatable.Dispose();
        }

        public void DaPostCreditSupplier(string employee_gid, MdlMstSupplier values)
        {
            msSQL = "select supplier_gid from agr_mst_tsuprcreditsupplier where application_gid='" + values.application_gid + "' and supplier_gid='" + values.supplier_gid + "'";
            string lssupplier_gid = objdbconn.GetExecuteScalar(msSQL);
            if (lssupplier_gid == (values.supplier_gid))
            {
                values.status = false;
                values.message = "Already Supplier Added";
                return;
            }
            msGetGid = objcmnfunctions.GetMasterGID("CRSP");
            msSQL = " insert into agr_mst_tsuprcreditsupplier(" +
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
                     " from agr_mst_tsuprcreditsupplier where " +
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

        public void DaGetCreditSupplierList(string credit_gid, string employee_gid, MdlMstSupplier values)
        {
            msSQL = " select creditsupplier_gid,credit_gid,application_gid,supplier_gid,supplier_name,relationship_vintage_year,relationship_vintage_month," +
                     " format(purchase_amount,'en-IN') as purchase_amount,format(bankdebit_amount,'en-IN')as bankdebit_amount,relationship_supplier," +
                     " date_format(start_date,'%d-%m-%Y') as start_date,date_format(end_date,'%d-%m-%Y') as end_date, " +
                     " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                     " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date, " +
                     " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                     " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as updated_by " +
                     " from agr_mst_tsuprcreditsupplier a " +
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

        public void DaEditGetCreditSupplier(string creditsupplier_gid, MdlMstSupplier values)
        {
            try
            {
                msSQL = " select creditsupplier_gid,credit_gid,application_gid,supplier_gid,supplier_name,relationship_vintage_year,relationship_vintage_month," +
                     " format(purchase_amount,'en-IN') as purchase_amount,format(bankdebit_amount,'en-IN') as bankdebit_amount,relationship_supplier,start_date,end_date" +
                     " from agr_mst_tsuprcreditsupplier where creditsupplier_gid='" + creditsupplier_gid + "'";
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

        public void DaUpdateCreditSupplier(string employee_gid, MdlMstSupplier values)
        {
            msSQL = " select creditsupplier_gid,credit_gid,application_gid,supplier_gid,supplier_name,relationship_vintage_year,relationship_vintage_month," +
                    " format(purchase_amount,'en-IN') as purchase_amount,format(bankdebit_amount,'en-IN') as bankdebit_amount,relationship_supplier," +
                    " start_date,end_date " +
                    " from agr_mst_tsuprcreditsupplier where creditsupplier_gid='" + values.creditsupplier_gid + "'";
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

            msSQL = " update agr_mst_tsuprcreditsupplier set " +
                     " relationship_vintage_year='" + values.relationship_vintage_year + "'," +
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
                    msSQL = "update agr_mst_tsuprcreditsupplier set start_date='" + Convert.ToDateTime(values.startdate).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'" +
                        "where creditsupplier_gid='" + values.creditsupplier_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (Convert.ToDateTime(values.enddate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL = "update agr_mst_tsuprcreditsupplier set end_date='" + Convert.ToDateTime(values.enddate).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'" +
                        "where creditsupplier_gid='" + values.creditsupplier_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                msGetGid = objcmnfunctions.GetMasterGID("CSUL");

                msSQL = "Insert into agr_mst_tsuprcreditsupplierupdatelog(" +
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
               "'" + lsrelationship_supplier + "'," +
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
                values.status = true;
                values.message = "Error Occured while updating suppplier";
            }
        }

        public void DaDeleteCreditSupplier(string creditsupplier_gid, string credit_gid, MdlMstSupplier values, string employee_gid)
        {
            msSQL = "delete from agr_mst_tsuprcreditsupplier where creditsupplier_gid='" + creditsupplier_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Supplier details Deleted successfully";

                msSQL = " select creditsupplier_gid,credit_gid,application_gid,supplier_gid,supplier_name,relationship_vintage_year,relationship_vintage_month," +
                     " format(purchase_amount,'en-IN') as purchase_amount,format(bankdebit_amount,'en-IN')as bankdebit_amount,relationship_supplier," +
                     " date_format(start_date,'%d-%m-%Y') as start_date,date_format(end_date,'%d-%m-%Y') as end_date " +
                     " from agr_mst_tsuprcreditsupplier where credit_gid='" + credit_gid + "'";
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

        public void DaPostCreditBuyer(string employee_gid, MdlMstCreditBuyer values)
        {
            msSQL = "select buyer_gid from agr_mst_tsuprcreditbuyer where application_gid='" + values.application_gid + "' and buyer_gid='" + values.buyer_gid + "'";
            string lsbuyer_gid = objdbconn.GetExecuteScalar(msSQL);
            if (lsbuyer_gid == (values.buyer_gid))
            {
                values.status = false;
                values.message = "Already Buyer Added";
                return;
            }
            msGetGid = objcmnfunctions.GetMasterGID("CRBY");
            msSQL = " insert into agr_mst_tsuprcreditbuyer(" +
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
                     " date_format(bankcredit_date,'%d-%m-%Y') as bankcredit_date from agr_mst_tsuprcreditbuyer where " +
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
                    " from agr_mst_tsuprcreditbuyer a " +
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
                    " enduse_monitoring,start_date,end_date,bankcredit_date from agr_mst_tsuprcreditbuyer where creditbuyer_gid='" + creditbuyer_gid + "'";
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
                    " enduse_monitoring,start_date,end_date,bankcredit_date from agr_mst_tsuprcreditbuyer where creditbuyer_gid='" + values.creditbuyer_gid + "'";
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

            msSQL = " update agr_mst_tsuprcreditbuyer set " +
                     " relationship_vintage_year='" + values.relationship_vintage_year + "'," +
                     " relationship_vintage_month='" + values.relationship_vintage_month + "'," +
                     " purchase_amount='" + values.purchase_amount.Replace(",", "") + "'," +
                     " buyer_limit='" + values.buyer_limit.Replace(",", "") + "'," +
                     " availed_limit='" + values.availed_limit.Replace(",", "") + "'," +
                     " balance_limit='" + values.balance_limit.Replace(",", "") + "'," +
                     " top_buyer='" + values.top_buyer + "'," +
                     " bill_tenuredays='" + values.bill_tenuredays.Replace("'", " ") + "'," +
                     " margin='" + values.margin.Replace("'", " ") + "'," +
                     " bankcredit_value='" + values.bankcredit_value.Replace(",", "") + "'," +
                     " source_deduction='" + values.source_deduction.Replace("'", " ") + "',";
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
                    msSQL = "update agr_mst_tsuprcreditbuyer set start_date='" + Convert.ToDateTime(values.startdate).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'" +
                        "where creditbuyer_gid='" + values.creditbuyer_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (Convert.ToDateTime(values.enddate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL = "update agr_mst_tsuprcreditbuyer set end_date='" + Convert.ToDateTime(values.enddate).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'" +
                        "where creditbuyer_gid='" + values.creditbuyer_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (Convert.ToDateTime(values.bankcreditdate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL = "update agr_mst_tsuprcreditbuyer set bankcredit_date='" + Convert.ToDateTime(values.bankcreditdate).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'" +
                        "where creditbuyer_gid='" + values.creditbuyer_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msGetGid = objcmnfunctions.GetMasterGID("CBUL");

                msSQL = "Insert into agr_mst_tsuprcreditbuyerupdatelog (" +
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
            msSQL = "delete from agr_mst_tsuprcreditsbuyer where creditbuyer_gid='" + creditbuyer_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Buyer details Deleted successfully";

                msSQL = " select creditbuyer_gid,credit_gid,application_gid,buyer_gid,buyer_name,relationship_vintage_year,relationship_vintage_month," +
                    " format(purchase_amount,'en-IN') as purchase_amount,format(buyer_limit,'en-IN')as buyer_limit,format(availed_limit,'en-IN')as availed_limit," +
                    " format(balance_limit,'en-IN')as balance_limit,top_buyer,bill_tenuredays,margin,bankcredit_value,source_deduction,relationship_borrower," +
                    " enduse_monitoring, date_format(start_date,'%d-%m-%Y') as start_date,date_format(end_date,'%d-%m-%Y') as end_date," +
                     " date_format(bankcredit_date,'%d-%m-%Y') as bankcredit_date from agr_mst_tsuprcreditsbuyer where " +
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

        public void DaPostCreditObservation(string employee_gid, MdlMstCreditObservation values)
        {
            msSQL = "select creditpolicy_gid from agr_mst_tsuprcreditsbuyer where application_gid='" + values.application_gid + "'" +
                " and creditpolicy_gid='" + values.creditpolicy_gid + "'";
            string lscreditpolicy_gid = objdbconn.GetExecuteScalar(msSQL);
            if (lscreditpolicy_gid == (values.creditpolicy_gid))
            {
                values.status = false;
                values.message = "Already Credit Observation (Credit Policy Compliance) Added";
                return;
            }
            msGetGid = objcmnfunctions.GetMasterGID("CROB");
            msSQL = " insert into agr_mst_tsuprcreditobservation(" +
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
                         "  from agr_mst_tsuprcreditobservation where " +
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
                    " from agr_mst_tsuprcreditobservation a" +
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
                         "  from agr_mst_tsuprcreditobservation where creditobservation_gid='" + creditobservation_gid + "'";
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
                " from agr_mst_tsuprcreditobservation where creditobservation_gid='" + values.creditobservation_gid + "'";
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

            msSQL = " update agr_mst_tsuprcreditobservation set " +
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

                msSQL = "Insert into agr_mst_tsuprcreditobservationupdatelog(" +
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
                   "'" + lsobservation + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Credit Observation Details Updated Successfully";
            }
        }

        public void DaDeleteCreditObservation(string creditobservation_gid, string credit_gid, MdlMstCreditObservation values, string employee_gid)
        {
            msSQL = "delete from agr_mst_tsuprcreditobservation where creditobservation_gid='" + creditobservation_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Credit Observation details Deleted successfully";

                msSQL = " select creditobservation_gid,credit_gid,application_gid,creditpolicy_gid,credit_policy,complied_status,observation " +
                           "  from agr_mst_tsuprcreditobservation where " +
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

        public bool DaPostCreditBank(string employee_gid, MdlCreditBankAcc values)
        {

            msGetGid = objcmnfunctions.GetMasterGID("B2BK");
            msSQL = " insert into agr_mst_tsuprcreditbankdtl (" +
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
                msSQL = "update agr_mst_tsuprcreditbankdtl 2cheque set creditbankdtl_gid='" + msGetGid + "' where creditbankdtl_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select creditbankdtl_gid,bank_name,branch_name,ifsc_code,bankaccount_number from " +
                          " agr_mst_tsuprcreditbankdtl  where credit_gid='" + values.credit_gid + "'";
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
                values.status = true;
                values.message = "Error Occured While Adding Bank Account Details";
                return false;
            }

        }

        public void DaGetCrediBankAccDtl(string credit_gid, string employee_gid, MdlCreditBankAcc values)
        {
            msSQL = " select creditbankdtl_gid,bank_name,branch_name,ifsc_code,bankaccount_number, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                    " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as updated_by " +
                    " from agr_mst_tsuprcreditbankdtl  a " +
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

        public void DaGetSupplierList(MdlMstSupplier values)
        {
            msSQL = " SELECT a.supplier_gid,a.supplier_name,supplier_ref_no " +
                    " FROM agr_mst_tsupplier a  where status='Y' order by a.supplier_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getsupplier_list = new List<supplier_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getsupplier_list.Add(new supplier_list
                    {
                        supplier_gid = (dr_datarow["supplier_gid"].ToString()),
                        supplier_name = (dr_datarow["supplier_name"].ToString()),
                        supplier_ref_no = (dr_datarow["supplier_ref_no"].ToString()),
                    });
                }
                values.supplier_list = getsupplier_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGetCreBuyerList(MdlMstCreditBuyer values)
        {
            msSQL = " SELECT buyer_gid,concat(buyer_name,' / ',buyer_code) as buyer_name " +
                   " from ocs_mst_tbuyer where creditstatus_Approval in ('Y','N') and creditActive_status = 'Y' order by buyer_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbuyerlist = new List<creditbuyer_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbuyerlist.Add(new creditbuyer_list
                    {
                        buyer_name = (dr_datarow["buyer_name"].ToString()),
                        buyer_gid = (dr_datarow["buyer_gid"].ToString()),
                    });
                }
                values.creditbuyer_list = getbuyerlist;
            }
            dt_datatable.Dispose();
        }

        public void DaGetCrepolicy(MdlMstCreditObservation values)
        {
            msSQL = " SELECT creditpolicycompliance_gid,creditpolicycompliance_name " +
                   " from ocs_mst_tcreditpolicycompliance where status = 'Y' order by creditpolicycompliance_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var opsCreditObservation_list = new List<CreditObservation_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    opsCreditObservation_list.Add(new CreditObservation_list
                    {
                        creditpolicy_gid = (dr_datarow["creditpolicycompliance_gid"].ToString()),
                        credit_policy = (dr_datarow["creditpolicycompliance_name"].ToString()),
                    });
                }
                values.CreditObservation_list = opsCreditObservation_list;
            }
            dt_datatable.Dispose();
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
            // string lsdocument_id = httpRequest.Form["document_id"].ToString();
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/CreditCheque/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/InstitutionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/InstitutionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("CRCQ");
                        msSQL = " insert into agr_mst_tsuprcreditbankdtl2cheque( " +
                                    " creditbankdtl2cheque_gid," +
                                    " creditbankdtl_gid," +
                                    " document_title  ," +
                                    " chequeleaf_name  ," +
                                    " chequeleaf_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                      "'" + lsdocument_title + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
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
                            " agr_mst_tsuprcreditbankdtl2cheque where creditbankdtl_gid='" + employee_gid + "' or  creditbankdtl_gid='" + lscreditbankdtl_gid + "'";
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
                                    chequeleaf_path = objcmnstorage.EncryptData((dt["chequeleaf_path"].ToString())),
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
            msSQL = "delete from agr_mst_tsuprcreditbankdtl 2cheque where creditbankdtl2cheque_gid='" + creditbankdtl2cheque_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Document Deleted successfully";

                msSQL = " select creditbankdtl2cheque_gid,creditbankdtl_gid,chequeleaf_name,chequeleaf_path,document_title from " +
                         " agr_mst_tsuprcreditbankdtl 2cheque where creditbankdtl_gid='" + employee_gid + "' or creditbankdtl_gid='" + credit_gid + "'";
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
                            chequeleaf_path = objcmnstorage.EncryptData((dt["chequeleaf_path"].ToString())),
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

        public void DaGetCrediBankAccList(string credit_gid, string employee_gid, MdlCreditBankAcc values)
        {
            msSQL = "select creditbankdtl_gid,credit_gid,application_gid,bank_name,branch_name,bank_address,micr_code,ifsc_code,bankaccount_name," +
                 " bankaccounttype_gid,bankaccounttype_name,bankaccount_number,confirmbankaccountnumber,joinaccount_status,joinaccount_name," +
                 " chequebook_status,DATE_FORMAT(accountopen_date,'%d-%m-%Y') as accountopen_date, bankaccount_name as accountholder_name, " +
                 " joinaccount_name as jointaccountholder_name" +
                 " from agr_mst_tsuprcreditbankdtl  where credit_gid='" + credit_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditbankacc_list = new List<creditbankacc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcreditbankacc_list.Add(new creditbankacc_list
                    {
                        creditbankdtl_gid = dt["creditbankdtl_gid"].ToString(),
                        bank_name = dt["bank_name"].ToString(),
                        branch_name = dt["branch_name"].ToString(),
                        bank_address = dt["bank_address"].ToString(),
                        micr_code = dt["micr_code"].ToString(),
                        ifsc_code = dt["ifsc_code"].ToString(),
                        bankaccount_name = dt["bankaccount_name"].ToString(),
                        bankaccounttype_name = dt["bankaccounttype_name"].ToString(),
                        bankaccount_number = dt["bankaccount_number"].ToString(),
                        confirmbankaccountnumber = dt["confirmbankaccountnumber"].ToString(),
                        joinaccount_status = dt["joinaccount_status"].ToString(),
                        joinaccount_name = dt["joinaccount_name"].ToString(),
                        chequebook_status = dt["chequebook_status"].ToString(),
                        accountopen_date = dt["accountopen_date"].ToString(),
                        accountholder_name = dt["accountholder_name"].ToString(),
                        jointaccountholder_name = dt["jointaccountholder_name"].ToString(),
                    });
                    values.creditbankacc_list = getcreditbankacc_list;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetCreditOperationsView(string credit_gid, string applicant_type, MdlMstAppCreditUnderWriting values)
        {
            if (applicant_type == "Institution")
            {
                msSQL = " select company_name,stakeholder_type,urn_status, urn from agr_mst_tsuprinstitution where institution_gid='" + credit_gid + "'";
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
                msSQL = " select first_name,stakeholder_type,urn_status, urn from agr_mst_tsuprcontact where contact_gid='" + credit_gid + "'";
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
                msSQL = " select group_name,group_type,groupurn_status, group_urn from agr_mst_tsuprgroup where group_gid='" + credit_gid + "'";
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

        public void DaGetCreditBuyerTextData(string creditbuyer_gid, MdlMstCreditBuyer values)
        {
            msSQL = " select creditbuyer_gid,credit_gid,application_gid,relationship_borrower, enduse_monitoring " +
                    " from agr_mst_tsuprcreditbuyer where creditbuyer_gid='" + creditbuyer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.creditbuyer_gid = objODBCDatareader["creditbuyer_gid"].ToString();
                values.credit_gid = objODBCDatareader["credit_gid"].ToString();
                values.application_gid = objODBCDatareader["application_gid"].ToString();
                values.relationship_borrower = objODBCDatareader["relationship_borrower"].ToString();
                values.enduse_monitoring = objODBCDatareader["enduse_monitoring"].ToString();
            }
            values.status = true;
            values.message = "success";
            objODBCDatareader.Close();
        }

        public void DaGetCreditSupplierTextData(string creditsupplier_gid, MdlMstSupplier values)
        {
            msSQL = " select creditsupplier_gid,credit_gid,application_gid, relationship_supplier" +
                     " from agr_mst_tsuprcreditsupplier where creditsupplier_gid='" + creditsupplier_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.creditsupplier_gid = objODBCDatareader["creditsupplier_gid"].ToString();
                values.credit_gid = objODBCDatareader["credit_gid"].ToString();
                values.application_gid = objODBCDatareader["application_gid"].ToString();
                values.relationship_supplier = objODBCDatareader["relationship_supplier"].ToString();
            }
            values.status = true;
            values.message = "success";
            objODBCDatareader.Close();
        }

        public void DaGetCreditBankDocumentUpload(string creditbankdtl_gid, credituploaddocument values)
        {
            msSQL = " select creditbankdtl2cheque_gid,creditbankdtl_gid,chequeleaf_name,chequeleaf_path,document_title, " +
                " CONCAT(c.user_firstname, ' / ', c.user_code) as uploaded_by, DATE_FORMAT(a.created_date,'%d-%m-%Y') as updated_date " +
                " from agr_mst_tsuprcreditbankdtl 2cheque a left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                " left join adm_mst_tuser c on b.user_gid=c.user_gid where creditbankdtl_gid='" + creditbankdtl_gid + "'";
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
                        chequeleaf_path = objcmnstorage.EncryptData((dt["chequeleaf_path"].ToString())),
                        creditbankdtl_gid = dt["creditbankdtl_gid"].ToString(),
                        creditbankdtl2cheque_gid = dt["creditbankdtl2cheque_gid"].ToString(),
                        uploaded_by = dt["uploaded_by"].ToString(),
                        updated_date = dt["updated_date"].ToString(),
                    });
                    values.credituploaddocument_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaChequeTmpClear(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = "delete from agr_mst_tsuprcreditbankdtl 2cheque where creditbankdtl_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            values.status = true;
        }

        public void DaEditGetCreditBankAccDtl(string creditbankdtl_gid, MdlCreditBankAcc values)
        {
            try
            {
                msSQL = "select creditbankdtl_gid,credit_gid,application_gid,bank_name,branch_name,bank_address,micr_code,ifsc_code,bankaccount_name," +
                " bankaccounttype_gid,bankaccounttype_name,bankaccount_number,confirmbankaccountnumber,joinaccount_status,joinaccount_name," +
                " chequebook_status,accountopen_date from agr_mst_tsuprcreditbankdtl  where creditbankdtl_gid='" + creditbankdtl_gid + "'";
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
                            " agr_mst_tsuprcreditbankdtl 2cheque where  creditbankdtl_gid='" + creditbankdtl_gid + "'";
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
                            chequeleaf_path = objcmnstorage.EncryptData((dt["chequeleaf_path"].ToString())),
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
                " chequebook_status,accountopen_date from agr_mst_tsuprcreditbankdtl  where creditbankdtl_gid='" + values.creditbankdtl_gid + "'";
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

            msSQL = " update agr_mst_tsuprcreditbankdtl  set " +
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
                    msSQL = "update agr_mst_tsuprcreditbankdtl  set accountopen_date='" + Convert.ToDateTime(values.accountopendate).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'" +
                        "where creditbankdtl_gid='" + values.creditbankdtl_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                msSQL = "update agr_mst_tsuprcreditbankdtl 2cheque set creditbankdtl_gid='" + values.creditbankdtl_gid + "' where creditbankdtl_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("CBDU");

                msSQL = " insert into agr_mst_tsuprcreditbankdtl log(" +
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
                values.status = true;
                values.message = "Error Occured while updating Bank Account";
            }
        }

        public void DaDeletecreditBankAcc(string creditbankdtl_gid, string credit_gid, MdlCreditBankAcc values, string employee_gid)
        {
            msSQL = "delete from agr_mst_tsuprcreditbankdtl  where creditbankdtl_gid='" + creditbankdtl_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = "delete from  agr_mst_tsuprcreditbankdtl 2cheque where creditbankdtl_gid='" + creditbankdtl_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Bank Account Details Deleted successfully";

                msSQL = " select creditbankdtl_gid,bank_name,branch_name,ifsc_code,bankaccount_number from " +
                           " agr_mst_tsuprcreditbankdtl  where credit_gid='" + credit_gid + "'";
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

        public void DaGetCreditExistingBankDtlRemarks(string existingbankfacility_gid, MdlMstExistingRemarks values)
        {
            msSQL = " select credit_gid, application_gid, existingbankfacility_gid, remarks " +
                     " from agr_mst_tsuprcreditbankfacilitydtl where existingbankfacility_gid='" + existingbankfacility_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.credit_gid = objODBCDatareader["credit_gid"].ToString();
                values.application_gid = objODBCDatareader["application_gid"].ToString();
                values.existingbankfacility_gid = objODBCDatareader["existingbankfacility_gid"].ToString();
                values.Existingbank_remarks = objODBCDatareader["remarks"].ToString();
            }
            values.status = true;
            values.message = "success";
            objODBCDatareader.Close();
        }

        public void DaGetCreditRepaymentDtlRemarks(string creditrepaymentdtl_gid, MdlMstRepaymentRemarks values)
        {
            msSQL = " select credit_gid, application_gid, creditrepaymentdtl_gid, remarks " +
                     " from agr_mst_tsuprcreditrepaymentdtl where creditrepaymentdtl_gid='" + creditrepaymentdtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.credit_gid = objODBCDatareader["credit_gid"].ToString(); 
                values.application_gid = objODBCDatareader["application_gid"].ToString();
                values.creditrepaymentdtl_gid = objODBCDatareader["creditrepaymentdtl_gid"].ToString();
                values.Repayment_remarks = objODBCDatareader["remarks"].ToString();
            }
            values.status = true;
            values.message = "success";
            objODBCDatareader.Close();
        }

        // Edit Institution Form
        public bool DaInstitutionEditDocumentUpload(HttpRequest httpRequest, institutionuploaddocument objfilename, string employee_gid)
        {
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string lscompany_code = string.Empty;
            string lsdocument_title = httpRequest.Form["document_title"].ToString();
            string lsdocument_id = httpRequest.Form["document_id"].ToString();
            string lsinstitution_gid = httpRequest.Form["institution_gid"].ToString();
            string lscompanydocument_gid = httpRequest.Form["companydocument_gid"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();

            String path = lspath;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/InstitutionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/InstitutionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/InstitutionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msSQL = "select covenant_type from ocs_mst_tcompanydocument where companydocument_gid='" + lscompanydocument_gid + "'";
                        string lscovenant_type = objdbconn.GetExecuteScalar(msSQL);

                        msGetGid = objcmnfunctions.GetMasterGID("INDO");
                        msSQL = " insert into agr_mst_tsuprinstitution2documentupload( " +
                                    " institution2documentupload_gid," +
                                    " institution_gid," +
                                    " document_title ," +
                                    " document_id," +
                                    " document_name ," +
                                    " companydocument_gid, " +
                                    " document_path," +
                                    " covenant_type," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + lsdocument_title + "'," +
                                    "'" + lsdocument_id + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lscompanydocument_gid + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + lscovenant_type + "'," +
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

        public void DaInstitutionEditDocumentTmpList(string institution_gid, string employee_gid, institutionuploaddocument values)
        {
            msSQL = " select institution2documentupload_gid,institution_gid,document_name,document_path,document_title,document_id " +
                " from agr_mst_tsuprinstitution2documentupload where institution_gid='" + employee_gid + "' or institution_gid='" + institution_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<institutionupload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new institutionupload_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        institution_gid = dt["institution_gid"].ToString(),
                        institution2documentupload_gid = dt["institution2documentupload_gid"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        document_id = dt["document_id"].ToString(),
                    });
                    values.institutionupload_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaInstitutionEditDocumentDelete(string institution2documentupload_gid, institutionuploaddocument objfilename, string employee_gid)
        {
            msSQL = "delete from agr_mst_tsuprinstitution2documentupload where institution2documentupload_gid='" + institution2documentupload_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                msSQL = " select groupdocumentchecklist_gid from agr_trn_tsuprdocumentchecktls where documentuploaded_gid='" + institution2documentupload_gid + "'";
                string lsgroupdocumentchecklist_gid = objdbconn.GetExecuteScalar(msSQL);

                if (lsgroupdocumentchecklist_gid != "")
                {
                    msSQL = " select count(*) as documentcount from agr_trn_tsuprdocumentchecktls " +
                            " where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                    string lsdocumentcount = objdbconn.GetExecuteScalar(msSQL);
                    if (lsdocumentcount == "1")
                    {
                        msSQL = "delete from agr_trn_tsuprgroupdocumentchecklist where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                msSQL = " select groupcovdocumentchecklist_gid from agr_trn_tsuprcovanantdocumentcheckdtls where documentuploaded_gid='" + institution2documentupload_gid + "'";
                string lschecklist_gid = objdbconn.GetExecuteScalar(msSQL);

                if (lschecklist_gid != "")
                {
                    msSQL = " select count(*) as documentcount from agr_trn_tsuprcovanantdocumentcheckdtls " +
                      " where groupcovdocumentchecklist_gid='" + lschecklist_gid + "'";
                    string lsdocumentcount = objdbconn.GetExecuteScalar(msSQL);
                    if (lsdocumentcount == "1")
                    {
                        msSQL = "delete from agr_trn_tsuprgroupcovenantdocumentchecklist where groupcovdocumentchecklist_gid='" + lschecklist_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                msSQL = "delete from agr_trn_tsuprcovanantdocumentcheckdtls where documentuploaded_gid='" + institution2documentupload_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "delete from agr_trn_tsuprdocumentchecktls where documentuploaded_gid='" + institution2documentupload_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
                objfilename.message = "Document Deleted Successfully";
                objfilename.status = true;
            }
            else
            {
                objfilename.message = "Error Occured While Deleting Document";
                objfilename.status = false;

            }
        }

        public bool DaInstitutionEditForm_60DocumentUpload(HttpRequest httpRequest, institutionuploaddocument objfilename, string employee_gid)
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
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/IntitutionForm_60Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/IntitutionForm_60Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/IntitutionForm_60Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("IF6D");
                        msSQL = " insert into agr_mst_tsuprinstitution2form60documentupload( " +
                                    " institution2form60documentupload_gid, " +
                                    " institution_gid," +
                                    " form60document_name ," +
                                    " form60document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
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

        public void DaInstitutionEditForm60TmpList(string institution_gid, string employee_gid, institutionuploaddocument values)
        {
            msSQL = " select institution2form60documentupload_gid,form60document_name,form60document_path from agr_mst_tsuprinstitution2form60documentupload " +
                               " where institution_gid='" + employee_gid + "' or institution_gid='" + institution_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<institutionupload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new institutionupload_list
                    {
                        document_name = dt["form60document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["form60document_path"].ToString())),
                        institution2form60documentupload_gid = dt["institution2form60documentupload_gid"].ToString()
                    });
                    values.institutionupload_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaInstitutionEditForm_60DocumentDelete(string institution2form60documentupload_gid, institutionuploaddocument objfilename, string employee_gid)
        {
            msSQL = "delete from agr_mst_tsuprinstitution2form60documentupload where institution2form60documentupload_gid='" + institution2form60documentupload_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                objfilename.message = "Document Deleted Successfully";
                objfilename.status = true;
            }
            else
            {
                objfilename.message = "Error Occured While Deleting Document";
                objfilename.status = false;

            }
        }

        public void DaInstitutionGSTList(string institution_gid, MdlMstGST values)
        {
            msSQL = "select institution2branch_gid,gst_state,gst_no, gst_registered from agr_mst_tsuprinstitution2branch where institution_gid='" + institution_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstgst_list = new List<mstgst_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstgst_list.Add(new mstgst_list
                    {
                        institution2branch_gid = (dr_datarow["institution2branch_gid"].ToString()),
                        gst_state = (dr_datarow["gst_state"].ToString()),
                        gst_no = (dr_datarow["gst_no"].ToString()),
                        gst_registered = (dr_datarow["gst_registered"].ToString())
                    });
                }
                values.mstgst_list = getmstgst_list;
            }
            dt_datatable.Dispose();
        }

        public void DaInstitutionMobileNoList(string institution_gid, MdlMstMobileNo values)
        {
            msSQL = "select mobile_no,institution2mobileno_gid,primary_status,whatsapp_no from agr_mst_tsuprinstitution2mobileno where " +
              " institution_gid='" + institution_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstmobileno_list = new List<mstmobileno_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstmobileno_list.Add(new mstmobileno_list
                    {
                        institution2mobileno_gid = (dr_datarow["institution2mobileno_gid"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        whatsapp_no = (dr_datarow["whatsapp_no"].ToString()),
                    });
                }
                values.mstmobileno_list = getmstmobileno_list;
            }
            dt_datatable.Dispose();
        }

        public void DaInstitutionEmailAddressList(string institution_gid, MdlMstEmailAddress values)
        {
            msSQL = "select email_address,institution2email_gid,primary_status from agr_mst_tsuprinstitution2email where institution_gid='" + institution_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstemailaddress_list = new List<mstemailaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstemailaddress_list.Add(new mstemailaddress_list
                    {
                        institution2email_gid = (dr_datarow["institution2email_gid"].ToString()),
                        email_address = (dr_datarow["email_address"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString())
                    });
                }
                values.mstemailaddress_list = getmstemailaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaInstitutionAddressList(string institution_gid, MdlMstAddressDetails values)
        {
            msSQL = "  select institution2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, landmark," +
                    " postal_code,latitude,longitude  from agr_mst_tsuprinstitution2address where institution_gid='" + institution_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstaddress_list = new List<mstaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstaddress_list.Add(new mstaddress_list
                    {
                        institution2address_gid = (dr_datarow["institution2address_gid"].ToString()),
                        address_type = (dr_datarow["addresstype_name"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        addressline1 = (dr_datarow["addressline1"].ToString()),
                        addressline2 = (dr_datarow["addressline2"].ToString()),
                        taluka = (dr_datarow["taluka"].ToString()),
                        district = (dr_datarow["district"].ToString()),
                        state = (dr_datarow["state"].ToString()),
                        country = (dr_datarow["country"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString()),
                        landmark = (dr_datarow["landmark"].ToString()),
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                    });
                }
                values.mstaddress_list = getmstaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaInstitutionLicenseList(string institution_gid, MdlMstLicenseDetails values)
        {
            msSQL = " select institution2licensedtl_gid,licensetype_gid,licensetype_name,license_no,date_format(issue_date,'%d-%m-%Y') as issue_date," +
                    " date_format(expiry_date,'%d-%m-%Y') as expiry_date from agr_mst_tsuprinstitution2licensedtl" +
                    " where institution_gid='" + institution_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstlicense_list = new List<mstlicense_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstlicense_list.Add(new mstlicense_list
                    {
                        institution2licensedtl_gid = (dr_datarow["institution2licensedtl_gid"].ToString()),
                        licensetype_gid = (dr_datarow["licensetype_gid"].ToString()),
                        licensetype_name = (dr_datarow["licensetype_name"].ToString()),
                        license_number = (dr_datarow["license_no"].ToString()),
                        licenseissue_date = (dr_datarow["issue_date"].ToString()),
                        licenseexpiry_date = (dr_datarow["expiry_date"].ToString())
                    });
                }
                values.mstlicense_list = getmstlicense_list;
            }
            dt_datatable.Dispose();
        }

        public void DaInstitutionDocumentList(string institution_gid, institutionuploaddocument values)
        {
            msSQL = " select institution2documentupload_gid,institution_gid,document_name,document_path,document_title,document_id," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by, " +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as uploaded_date" +
                    " from agr_mst_tsuprinstitution2documentupload a" +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " where institution_gid='" + institution_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<institutionupload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new institutionupload_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        institution_gid = dt["institution_gid"].ToString(),
                        institution2documentupload_gid = dt["institution2documentupload_gid"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        document_id = dt["document_id"].ToString(),
                        uploaded_by = dt["uploaded_by"].ToString(),
                        uploaded_date = dt["uploaded_date"].ToString(),
                    });
                    values.institutionupload_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaInstitutionForm60DocumentList(string institution_gid, institutionuploaddocument values)
        {
            msSQL = " select institution2form60documentupload_gid,institution_gid,form60document_name,form60document_path," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by, " +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as uploaded_date" +
                    " from agr_mst_tsuprinstitution2form60documentupload a" +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " where institution_gid='" + institution_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<institutionupload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new institutionupload_list
                    {
                        document_name = dt["form60document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["form60document_path"].ToString())),
                        institution_gid = dt["institution_gid"].ToString(),
                        institution2form60documentupload_gid = dt["institution2form60documentupload_gid"].ToString(),
                        uploaded_by = dt["uploaded_by"].ToString(),
                        uploaded_date = dt["uploaded_date"].ToString(),
                    });
                    values.institutionupload_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaInstitutionDetailsEdit(string institution_gid, MdlMstInstitutionAdd values)
        {
            try
            {
                msSQL = " select application_gid, application_no, company_name, date_incorporation, businessstart_date, companypan_no, year_business, month_business, cin_no," +
                   " official_telephoneno, officialemail_address, companytype_gid, companytype_name, stakeholder_type, stakeholdertype_gid, assessmentagency_gid, " +
                   " assessmentagency_name, assessmentagencyrating_gid, assessmentagencyrating_name, ratingas_on, amlcategory_gid, amlcategory_name,businesscategory_gid, " +
                   " businesscategory_name, contactperson_firstname, contactperson_middlename, contactperson_lastname, designation_gid, designation, " +
                   " lastyear_turnover, escrow, start_date, end_date, institution_status, urn, urn_status " +
                   " from agr_mst_tsuprinstitution where institution_gid='" + institution_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.application_no = objODBCDatareader["application_no"].ToString();
                    values.company_name = objODBCDatareader["company_name"].ToString();
                    if (objODBCDatareader["date_incorporation"].ToString() == "")
                    {
                    }
                    else
                    {
                        values.editdate_incorporation = Convert.ToDateTime(objODBCDatareader["date_incorporation"]).ToString("dd-MM-yyyy");
                    }
                    if (objODBCDatareader["businessstart_date"].ToString() == "")
                    {
                    }
                    else
                    {
                        values.editbusinessstart_date = Convert.ToDateTime(objODBCDatareader["businessstart_date"]).ToString("dd-MM-yyyy");
                    }
                    values.companypan_no = objODBCDatareader["companypan_no"].ToString();
                    values.year_business = objODBCDatareader["year_business"].ToString();
                    values.month_business = objODBCDatareader["month_business"].ToString();
                    values.cin_no = objODBCDatareader["cin_no"].ToString();
                    values.official_telephoneno = objODBCDatareader["official_telephoneno"].ToString();
                    values.official_mailid = objODBCDatareader["officialemail_address"].ToString();
                    values.companytype_gid = objODBCDatareader["companytype_gid"].ToString();
                    values.companytype_name = objODBCDatareader["companytype_name"].ToString();
                    values.stakeholder_type = objODBCDatareader["stakeholder_type"].ToString();
                    values.stakeholdertype_gid = objODBCDatareader["stakeholdertype_gid"].ToString();
                    values.assessmentagency_gid = objODBCDatareader["assessmentagency_gid"].ToString();
                    values.assessmentagency_name = objODBCDatareader["assessmentagency_name"].ToString();
                    values.assessmentagencyrating_gid = objODBCDatareader["assessmentagencyrating_gid"].ToString();
                    values.assessmentagencyrating_name = objODBCDatareader["assessmentagencyrating_name"].ToString();
                    if (objODBCDatareader["ratingas_on"].ToString() == "")
                    {
                    }
                    else
                    {
                        values.editratingas_on = Convert.ToDateTime(objODBCDatareader["ratingas_on"]).ToString("dd-MM-yyyy");
                    }
                    values.amlcategory_gid = objODBCDatareader["amlcategory_gid"].ToString();
                    values.amlcategory_name = objODBCDatareader["amlcategory_name"].ToString();
                    values.businesscategory_gid = objODBCDatareader["businesscategory_gid"].ToString();
                    values.businesscategory_name = objODBCDatareader["businesscategory_name"].ToString();
                    values.contactperson_firstname = objODBCDatareader["contactperson_firstname"].ToString();
                    values.contactperson_middlename = objODBCDatareader["contactperson_middlename"].ToString();
                    values.contactperson_lastname = objODBCDatareader["contactperson_lastname"].ToString();
                    values.designation_gid = objODBCDatareader["designation_gid"].ToString();
                    values.designation = objODBCDatareader["designation"].ToString();
                    values.lastyear_turnover = objODBCDatareader["lastyear_turnover"].ToString();
                    values.escrow = objODBCDatareader["escrow"].ToString();
                    if (objODBCDatareader["start_date"].ToString() == "")
                    {
                    }
                    else
                    {
                        values.editstart_date = Convert.ToDateTime(objODBCDatareader["start_date"]).ToString("dd-MM-yyyy");
                    }
                    if (objODBCDatareader["end_date"].ToString() == "")
                    {
                    }
                    else
                    {
                        values.editend_date = Convert.ToDateTime(objODBCDatareader["end_date"]).ToString("dd-MM-yyyy");
                    }
                    values.institution_status = objODBCDatareader["institution_status"].ToString();
                    values.urn = objODBCDatareader["urn"].ToString();
                    values.urn_status = objODBCDatareader["urn_status"].ToString();
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

        public bool DaUpdateInstitutionDtl(MdlMstInstitutionAdd values, string employee_gid)
        {
            msSQL = "select institution_gid from agr_mst_tsuprinstitution2mobileno where (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "') and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Primary Mobile Number";
                return false;
            }

            msSQL = "select institution_gid from agr_mst_tsuprinstitution2mobileno where institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Atleast One Mobile Number";
                return false;
            }

            msSQL = "select institution_gid from agr_mst_tsuprinstitution2email where institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Atleast One Email Address";
                return false;
            }

            msSQL = "select institution_gid from agr_mst_tsuprinstitution2address where institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Atleast One Address Detail";
                return false;
            }

            msSQL = " select application_gid, application_no, company_name, date_incorporation, businessstart_date,companypan_no, year_business, month_business, cin_no," +
                     " official_telephoneno, officialemail_address, companytype_gid, companytype_name, stakeholder_type, stakeholdertype_gid, assessmentagency_gid, " +
                     " assessmentagency_name, assessmentagencyrating_gid, assessmentagencyrating_name, ratingas_on, amlcategory_gid, amlcategory_name,businesscategory_gid, " +
                     " businesscategory_name, contactperson_firstname, contactperson_middlename, contactperson_lastname, designation_gid, designation, " +
                     " lastyear_turnover, escrow, start_date, end_date, urn_status, urn " +
                     " from agr_mst_tsuprinstitution where institution_gid='" + values.institution_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsapplication_gid = objODBCDatareader["application_gid"].ToString();
                lsapplication_no = objODBCDatareader["application_no"].ToString();
                lscompany_name = objODBCDatareader["company_name"].ToString();
                if (objODBCDatareader["date_incorporation"].ToString() == "")
                {
                }
                else
                {
                    lsdate_incorporation = Convert.ToDateTime(objODBCDatareader["date_incorporation"]).ToString("dd-MM-yyyy");
                }
                if (objODBCDatareader["businessstart_date"].ToString() == "")
                {
                }
                else
                {
                    lsbusinessstart_date = Convert.ToDateTime(objODBCDatareader["businessstart_date"]).ToString("dd-MM-yyyy");
                }
                lscompanypan_no = objODBCDatareader["companypan_no"].ToString();
                lsyear_business = objODBCDatareader["year_business"].ToString();
                lsmonth_business = objODBCDatareader["month_business"].ToString();
                lscin_no = objODBCDatareader["cin_no"].ToString();
                lsofficial_telephoneno = objODBCDatareader["official_telephoneno"].ToString();
                lsofficialemail_address = objODBCDatareader["officialemail_address"].ToString();
                lscompanytype_gid = objODBCDatareader["companytype_gid"].ToString();
                lscompanytype_name = objODBCDatareader["companytype_name"].ToString();
                lsstakeholder_type = objODBCDatareader["stakeholder_type"].ToString();
                lsstakeholdertype_gid = objODBCDatareader["stakeholdertype_gid"].ToString();
                lsassessmentagency_gid = objODBCDatareader["assessmentagency_gid"].ToString();
                lsassessmentagency_name = objODBCDatareader["assessmentagency_name"].ToString();
                lsassessmentagencyrating_gid = objODBCDatareader["assessmentagencyrating_gid"].ToString();
                lsassessmentagencyrating_name = objODBCDatareader["assessmentagencyrating_name"].ToString();
                if (objODBCDatareader["ratingas_on"].ToString() == "")
                {
                }
                else
                {
                    lsratingas_on = Convert.ToDateTime(objODBCDatareader["ratingas_on"]).ToString("dd-MM-yyyy");
                }
                lsamlcategory_gid = objODBCDatareader["amlcategory_gid"].ToString();
                lsamlcategory_name = objODBCDatareader["amlcategory_name"].ToString();
                lsbusinesscategory_gid = objODBCDatareader["businesscategory_gid"].ToString();
                lsbusinesscategory_name = objODBCDatareader["businesscategory_name"].ToString();
                lscontactperson_firstname = objODBCDatareader["contactperson_firstname"].ToString();
                lscontactperson_middlename = objODBCDatareader["contactperson_middlename"].ToString();
                lscontactperson_lastname = objODBCDatareader["contactperson_lastname"].ToString();
                lsdesignation_gid = objODBCDatareader["designation_gid"].ToString();
                lsdesignation = objODBCDatareader["designation"].ToString();
                lslastyear_turnover = objODBCDatareader["lastyear_turnover"].ToString();
                lsescrow = objODBCDatareader["escrow"].ToString();
                if (objODBCDatareader["start_date"].ToString() == "")
                {
                }
                else
                {
                    lsstart_date = Convert.ToDateTime(objODBCDatareader["start_date"]).ToString("dd-MM-yyyy");
                }
                if (objODBCDatareader["end_date"].ToString() == "")
                {
                }
                else
                {
                    lsend_date = Convert.ToDateTime(objODBCDatareader["end_date"]).ToString("dd-MM-yyyy");
                }
                lsurn_status = objODBCDatareader["urn_status"].ToString();
                lsurn = objODBCDatareader["urn"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tsuprinstitution set " +
                        " company_name='" + values.company_name + "',";
                if (Convert.ToDateTime(values.dateincorporation).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " date_incorporation='" + Convert.ToDateTime(values.dateincorporation).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                if (Convert.ToDateTime(values.businessstart_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " businessstart_date='" + Convert.ToDateTime(values.businessstart_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                msSQL += " year_business='" + values.year_business + "'," +
                         " month_business='" + values.month_business + "'," +
                         " companypan_no='" + values.companypan_no + "'," +
                         " cin_no='" + values.cin_no + "'," +
                         " official_telephoneno='" + values.official_telephoneno + "'," +
                         " officialemail_address='" + values.official_mailid + "'," +
                         " companytype_gid='" + values.companytype_gid + "'," +
                         " companytype_name='" + values.companytype_name + "'," +
                         " stakeholdertype_gid='" + values.stakeholdertype_gid + "'," +
                         " stakeholder_type='" + values.stakeholder_type + "'," +
                         " assessmentagency_gid='" + values.assessmentagency_gid + "'," +
                         " assessmentagency_name='" + values.assessmentagency_name + "'," +
                         " assessmentagencyrating_gid='" + values.assessmentagencyrating_gid + "'," +
                         " assessmentagencyrating_name='" + values.assessmentagencyrating_name + "',";
                if (Convert.ToDateTime(values.ratingason).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " ratingas_on='" + Convert.ToDateTime(values.ratingason).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                msSQL += " amlcategory_gid='" + values.amlcategory_gid + "'," +
                         " amlcategory_name='" + values.amlcategory_name + "'," +
                         " businesscategory_gid='" + values.businesscategory_gid + "'," +
                         " businesscategory_name='" + values.businesscategory_name + "'," +
                         " contactperson_firstname='" + values.contactperson_firstname + "'," +
                         " contactperson_middlename='" + values.contactperson_middlename + "'," +
                         " contactperson_lastname='" + values.contactperson_lastname + "'," +
                         " designation_gid='" + values.designation_gid + "'," +
                         " designation='" + values.designation + "',";
                if (Convert.ToDateTime(values.startdate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " start_date='" + Convert.ToDateTime(values.startdate).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                if (Convert.ToDateTime(values.enddate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " end_date='" + Convert.ToDateTime(values.enddate).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                msSQL += " lastyear_turnover='" + values.lastyear_turnover + "'," +
                         " escrow='" + values.escrow + "'," +
                         " urn_status='" + values.urn_status + "'," +
                         " urn='" + values.urn + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where institution_gid='" + values.institution_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("IULG");

                    msSQL = " insert into agr_mst_tsuprinstitutionupdateLOG(" +
                    " institution_gidupdateLOG_GID," +
                    " institution_gid," +
                    " application_gid," +
                    " application_no," +
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
                    " statusupdated_by," +
                    " created_by," +
                    " created_date) values(" +
                      "'" + msGetGid + "'," +
                      "'" + values.institution_gid + "'," +
                      "'" + lsapplication_gid + "'," +
                      "'" + lsapplication_no + "'," +
                      "'" + lscompany_name + "'," +
                        "'" + lsdate_incorporation + "'," +
                        "'" + lsbusinessstart_date + "'," +
                        "'" + lsyear_business + "'," +
                               "'" + lsmonth_business + "'," +
                               "'" + lscompanypan_no + "'," +
                               "'" + lscin_no + "'," +
                               "'" + lsofficial_telephoneno + "'," +
                               "'" + lsofficialemail_address + "'," +
                               "'" + lscompanytype_gid + "'," +
                               "'" + lscompanytype_name + "'," +
                               "'" + lsstakeholdertype_gid + "'," +
                               "'" + lsstakeholder_type + "'," +
                               "'" + lsassessmentagency_gid + "'," +
                               "'" + lsassessmentagency_name + "'," +
                               "'" + lsassessmentagencyrating_gid + "'," +
                               "'" + lsassessmentagencyrating_name + "'," +
                               "'" + lsratingas_on + "'," +
                               "'" + lsamlcategory_gid + "'," +
                               "'" + lsamlcategory_name + "'," +
                               "'" + lsbusinesscategory_gid + "'," +
                               "'" + lsbusinesscategory_name + "'," +
                               "'" + lscontactperson_firstname + "'," +
                               "'" + lscontactperson_middlename + "'," +
                               "'" + lscontactperson_lastname + "'," +
                               "'" + lsdesignation_gid + "'," +
                               "'" + lsdesignation + "'," +
                               "'" + lsstart_date + "'," +
                               "'" + lsend_date + "'," +
                               "'" + lslastyear_turnover + "'," +
                               "'" + lsescrow + "'," +
                               "'" + lsurn_status + "'," +
                               "'" + lsurn + "'," +
                               "'" + values.statusupdated_by + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    // Updates for Multiple Add
                    msSQL = "update agr_mst_tsuprinstitution2branch set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsuprinstitution2mobileno set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsuprinstitution2email set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsuprinstitution2address set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsuprinstitution2licensedtl set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsuprinstitution2ratingdetail set institution_gid='" + values.institution_gid + "', application_gid ='" + lsapplication_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "select institution_gid from agr_mst_tsuprinstitution2documentupload where institution_gid='" + employee_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        string lscovenant_type = "", lsdocumenttype_gid = "", lsdocumenttype_name = "", lscompanydocument_name = "";

                        string msGetdefDocchecklistGID = objcmnfunctions.GetMasterGID("DOCG");
                        msSQL = " select companydocument_gid,documenttypes_gid,documenttype_name,companydocument_name,covenant_type " +
                                    " from ocs_mst_tcompanydocument where companydocument_gid='" + dt["companydocument_gid"].ToString() + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                            lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                            lscompanydocument_name = objODBCDatareader["companydocument_name"].ToString();
                            lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = " insert into agr_trn_tsuprdocumentchecktls(" +
                                " documentcheckdtl_gid," +
                                " application_gid," +
                                " credit_gid, " +
                                " companydocument_gid, " +
                                " documentuploaded_gid, " +
                                " documenttype_gid," +
                            " documenttype_code," +
                            " documenttype_name," +
                            " covenant_type, " +
                            " tagged_by, " +
                            " created_date," +
                            " created_by)" +
                            " VALUES(" +
                            "'" + msGetdefDocchecklistGID + "'," +
                            "'" + lsapplication_gid + "'," +
                            "'" + values.institution_gid + "'," +
                            "'" + dt["companydocument_gid"].ToString() + "'," +
                            "'" + dt["institution2documentupload_gid"].ToString() + "'," +
                            "'" + lsdocumenttype_gid + "'," +
                            "'" + lsdocumenttype_name + "'," +
                            "'" + lscompanydocument_name.Replace("'", "") + "'," +
                            "'" + lscovenant_type + "'," +
                            "'N'," +
                            "current_timestamp," +
                            "'" + employee_gid + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (lscovenant_type == "Y")
                        {
                            string msGetDocchecklistGID = objcmnfunctions.GetMasterGID("CDCL");
                            msSQL = " insert into agr_trn_tsuprcovanantdocumentcheckdtls(" +
                           " covenantdocumentcheckdtl_gid," +
                           " application_gid," +
                           " credit_gid," +
                           " companydocument_gid," +
                            " documentuploaded_gid, " +
                           " documenttype_gid," +
                           " documenttype_code," +
                           " documenttype_name," +
                           " covenant_type, " +
                           " tagged_by, " +
                           " created_date," +
                           " created_by)" +
                           " VALUES(" +
                           "'" + msGetDocchecklistGID + "'," +
                           "'" + lsapplication_gid + "'," +
                           "'" + values.institution_gid + "'," +
                           "'" + dt["companydocument_gid"].ToString() + "'," +
                           "'" + dt["institution2documentupload_gid"].ToString() + "'," +
                           "'" + lsdocumenttype_gid + "'," +
                           "'" + lsdocumenttype_name + "'," +
                           "'" + lscompanydocument_name.Replace("'", "") + "'," +
                           "'" + lscovenant_type + "'," +
                           "'N'," +
                           "current_timestamp," +
                           "'" + employee_gid + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    dt_datatable.Dispose();
                    DaAgrMstSuprScannedDocument objvalues = new DaAgrMstSuprScannedDocument();
                    objvalues.DaGroupDocChecklistinfo(lsapplication_gid, values.institution_gid, employee_gid);

                    msSQL = "update agr_mst_tsuprinstitution2documentupload set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsuprinstitution2form60documentupload set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        msSQL = "select applicant_type from agr_mst_tsuprapplication where application_gid='" + lsapplication_gid + "' ";
                        string lsapplicant_type = objdbconn.GetExecuteScalar(msSQL);

                        if (lsapplicant_type == "Institution")
                        {
                            msSQL = "select company_name,mobile_no,email_address,institution_gid from agr_mst_tsuprinstitution where " +
                               " application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Applicant','Borrower')";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lscustomer_name = objODBCDatareader["company_name"].ToString();
                                lsmobile_no = objODBCDatareader["mobile_no"].ToString();
                                lsemail_address = objODBCDatareader["email_address"].ToString();
                                //Region
                                msSQL = "select state from agr_mst_tsuprinstitution2address where primary_status='Yes' and institution_gid='" + objODBCDatareader["institution_gid"].ToString() + "'";
                                lsregion = objdbconn.GetExecuteScalar(msSQL);

                                //Main Table 
                                msSQL = " update agr_mst_tsuprapplication set customer_name='" + lscustomer_name + "'," +
                               " mobile_no='" + lsmobile_no + "'," +
                               " email_address='" + lsemail_address + "'," +
                               " region='" + lsregion + "'," +
                               " updated_by='" + employee_gid + "'," +
                               " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                               " where application_gid='" + lsapplication_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            objODBCDatareader.Close();
                            values.status = true;
                            values.message = "Institution Details Updated Successfully";
                            return true;
                        }
                        else
                        {

                        }
                    }
                    values.status = true;
                    values.message = "Institution Details Updated Successfully";
                    return true;
                }
                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured While Updating Institution";
                return false;
            }
        }

        public void DaInstitutionGSTTmpList(string employee_gid, string institution_gid, MdlMstGST values)
        {
            msSQL = "select institution2branch_gid,gst_state,gst_no, gst_registered from agr_mst_tsuprinstitution2branch " +
                " where institution_gid='" + institution_gid + "' or institution_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstgst_list = new List<mstgst_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstgst_list.Add(new mstgst_list
                    {
                        institution2branch_gid = (dr_datarow["institution2branch_gid"].ToString()),
                        gst_state = (dr_datarow["gst_state"].ToString()),
                        gst_no = (dr_datarow["gst_no"].ToString()),
                        gst_registered = (dr_datarow["gst_registered"].ToString())
                    });
                }
                values.mstgst_list = getmstgst_list;
            }
            dt_datatable.Dispose();
        }

        public void DaInstitutionMobileNoTmpList(string employee_gid, string institution_gid, MdlMstMobileNo values)
        {
            msSQL = "select mobile_no,institution2mobileno_gid,primary_status,whatsapp_no from agr_mst_tsuprinstitution2mobileno where " +
              " institution_gid='" + institution_gid + "' or institution_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstmobileno_list = new List<mstmobileno_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstmobileno_list.Add(new mstmobileno_list
                    {
                        institution2mobileno_gid = (dr_datarow["institution2mobileno_gid"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        whatsapp_no = (dr_datarow["whatsapp_no"].ToString()),
                    });
                }
                values.mstmobileno_list = getmstmobileno_list;
            }
            dt_datatable.Dispose();
        }

        public void DaInstitutionEmailAddressTmpList(string employee_gid, string institution_gid, MdlMstEmailAddress values)
        {
            msSQL = "select email_address,institution2email_gid,primary_status from agr_mst_tsuprinstitution2email " +
                " where institution_gid='" + institution_gid + "' or institution_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstemailaddress_list = new List<mstemailaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstemailaddress_list.Add(new mstemailaddress_list
                    {
                        institution2email_gid = (dr_datarow["institution2email_gid"].ToString()),
                        email_address = (dr_datarow["email_address"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString())
                    });
                }
                values.mstemailaddress_list = getmstemailaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaInstitutionAddressTmpList(string employee_gid, string institution_gid, MdlMstAddressDetails values)
        {
            msSQL = "  select institution2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, landmark," +
                    " postal_code,latitude,longitude  from agr_mst_tsuprinstitution2address where institution_gid='" + institution_gid + "' or institution_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstaddress_list = new List<mstaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstaddress_list.Add(new mstaddress_list
                    {
                        institution2address_gid = (dr_datarow["institution2address_gid"].ToString()),
                        address_type = (dr_datarow["addresstype_name"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        addressline1 = (dr_datarow["addressline1"].ToString()),
                        addressline2 = (dr_datarow["addressline2"].ToString()),
                        taluka = (dr_datarow["taluka"].ToString()),
                        district = (dr_datarow["district"].ToString()),
                        state = (dr_datarow["state"].ToString()),
                        country = (dr_datarow["country"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString()),
                        landmark = (dr_datarow["landmark"].ToString()),
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                    });
                }
                values.mstaddress_list = getmstaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaInstitutionLicenseTmpList(string employee_gid, string institution_gid, MdlMstLicenseDetails values)
        {
            msSQL = " select institution2licensedtl_gid,licensetype_gid,licensetype_name,license_no,date_format(issue_date,'%d-%m-%Y') as issue_date," +
                    " date_format(expiry_date,'%d-%m-%Y') as expiry_date from agr_mst_tsuprinstitution2licensedtl" +
                    " where institution_gid='" + institution_gid + "' or institution_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstlicense_list = new List<mstlicense_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstlicense_list.Add(new mstlicense_list
                    {
                        institution2licensedtl_gid = (dr_datarow["institution2licensedtl_gid"].ToString()),
                        licensetype_gid = (dr_datarow["licensetype_gid"].ToString()),
                        licensetype_name = (dr_datarow["licensetype_name"].ToString()),
                        license_number = (dr_datarow["license_no"].ToString()),
                        licenseissue_date = (dr_datarow["issue_date"].ToString()),
                        licenseexpiry_date = (dr_datarow["expiry_date"].ToString())
                    });
                }
                values.mstlicense_list = getmstlicense_list;
            }
            dt_datatable.Dispose();
        }

        // Institution GST Details

        public bool DaPostInstitutionGST(string employee_gid, MdlMstGST values)
        {
            msSQL = "select institution_gid from agr_mst_tsuprinstitution2branch where gst_no='" + values.gst_no + "' and (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "GST Number Already Added";
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("ITGS");
            msSQL = " insert into agr_mst_tsuprinstitution2branch(" +
                    " institution2branch_gid," +
                    " institution_gid," +
                    " gst_state," +
                    " gst_no," +
                    " gst_registered," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.gst_state + "'," +
                    "'" + values.gst_no + "'," +
                    "'" + values.gst_registered + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "GST Details Added Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Adding GST Details";
                return false;
            }
        }

        public bool DaPostInstitutionGSTList(string employee_gid, MdlMstGST values)
        {

            InstitutionGSTDetails[] GstArray = values.GSTArray;
            string GSTValue, GSTStateCode, GSTState;

            for (int i = 0; i < GstArray.Length; i++)
            {
                GSTValue = GstArray[i].gstinId;
                GSTStateCode = GSTValue.Substring(0, 2);

                msSQL = "select gst_state from agr_mst_tgstcode2state where " +
                       " gst_code='" + GSTStateCode + "'";
                GSTState = objdbconn.GetExecuteScalar(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("ITGS");
                msSQL = " insert into agr_mst_tsuprinstitution2branch(" +
                    " institution2branch_gid," +
                    " institution_gid," +
                    " gst_state," +
                    " gst_no," +
                    " gst_registered," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + GSTState + "'," +
                    "'" + GSTValue + "'," +
                    "'" + "Yes" + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "GST Details Added Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Addign GST Details";
                return false;
            }
        }

        public void DaEditInstitutionGST(string institution2branch_gid, MdlMstGST values)
        {
            try
            {
                msSQL = "select gst_state, gst_no, institution_gid, institution2branch_gid, gst_registered" +
                    " from agr_mst_tsuprinstitution2branch where institution2branch_gid='" + institution2branch_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.gst_state = objODBCDatareader["gst_state"].ToString();
                    values.gst_no = objODBCDatareader["gst_no"].ToString();
                    values.institution2branch_gid = objODBCDatareader["institution2branch_gid"].ToString();
                    values.institution_gid = objODBCDatareader["institution_gid"].ToString();
                    values.gst_registered = objODBCDatareader["gst_registered"].ToString();
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

        public void DaUpdateInstitutionGST(string employee_gid, MdlMstGST values)
        {
            msSQL = "select gst_state, gst_no, gst_registered, institution_gid, institution2branch_gid" +
                " from agr_mst_tsuprinstitution2branch where institution2branch_gid='" + values.institution2branch_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsgst_state = objODBCDatareader["gst_state"].ToString();
                lsgst_no = objODBCDatareader["gst_no"].ToString();
                lsinstitution2branch_gid = objODBCDatareader["institution2branch_gid"].ToString();
                lsinstitution_gid = objODBCDatareader["institution_gid"].ToString();
                lsgst_registered = objODBCDatareader["gst_registered"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tsuprinstitution2branch set " +
                         " gst_state='" + values.gst_state + "'," +
                         " gst_no='" + values.gst_no + "'," +
                         " gst_registered='" + values.gst_registered + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where institution2branch_gid='" + values.institution2branch_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("IGUL");

                    msSQL = "Insert into agr_mst_tsuprinstitution2branchupdatelog(" +
                   " institution2gstupdatelog_gid, " +
                   " institution2branch_gid, " +
                   " institution_gid, " +
                   " gst_state," +
                   " gst_no," +
                   " gst_registered," +
                   " statusupdated_by," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + values.institution2branch_gid + "'," +
                   "'" + values.institution_gid + "'," +
                   "'" + lsgst_state + "'," +
                   "'" + lsgst_no + "'," +
                   "'" + lsgst_registered + "'," +
                   "'" + values.statusupdated_by + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "GST Details Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured While Updating GST Details";
            }
        }

        public void DaDeleteInstitutionGST(string institution2branch_gid, MdlMstGST values)
        {
            msSQL = "delete from agr_mst_tsuprinstitution2branch where institution2branch_gid='" + institution2branch_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tsuprinstitution2branchupdatelog where institution2branch_gid='" + institution2branch_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "GST Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting GST Details";
                values.status = false;

            }
        }

        // Institution Mobile Number

        public bool DaPostInstitutionMobileNo(string employee_gid, MdlMstMobileNo values)
        {
            msSQL = "select primary_status from agr_mst_tsuprinstitution2mobileno where primary_status='Yes' and (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Mobile Number Added";
                return false;
            }

            msSQL = "select institution2mobileno_gid from agr_mst_tsuprinstitution2mobileno where mobile_no='" + values.mobile_no + "' and (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already This Mobile Number Added";
                return false;
            }
            msGetGid = objcmnfunctions.GetMasterGID("IT2M");
            msSQL = " insert into agr_mst_tsuprinstitution2mobileno(" +
                    " institution2mobileno_gid," +
                    " institution_gid," +
                    " mobile_no," +
                    " primary_status," +
                    " whatsapp_no," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.mobile_no + "'," +
                    "'" + values.primary_status + "'," +
                    "'" + values.whatsapp_no + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Mobile Number Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Addig Mobile Number";
                return false;
            }
        }

        public void DaEditInstitutionMobileNo(string institution2mobileno_gid, MdlMstMobileNo values)
        {
            try
            {
                msSQL = " select mobile_no,institution2mobileno_gid,primary_status,whatsapp_no from agr_mst_tsuprinstitution2mobileno where " +
                        " institution2mobileno_gid='" + institution2mobileno_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.mobile_no = objODBCDatareader["mobile_no"].ToString();
                    values.primary_status = objODBCDatareader["primary_status"].ToString();
                    values.whatsapp_no = objODBCDatareader["whatsapp_no"].ToString();
                    values.institution2mobileno_gid = objODBCDatareader["institution2mobileno_gid"].ToString();
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

        public void DaUpdateInstitutionMobileNo(string employee_gid, MdlMstMobileNo values)
        {
            msSQL = " select mobile_no,institution2mobileno_gid,primary_status,whatsapp_no from agr_mst_tsuprinstitution2mobileno where " +
                    " institution2mobileno_gid='" + values.institution2mobileno_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsmobile_no = objODBCDatareader["mobile_no"].ToString();
                lsprimary_status = objODBCDatareader["primary_status"].ToString();
                lswhatsapp_no = objODBCDatareader["whatsapp_no"].ToString();
                lsinstitution2mobileno_gid = objODBCDatareader["institution2mobileno_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tsuprinstitution2mobileno set " +
                         " mobile_no='" + values.mobile_no + "'," +
                         " primary_status='" + values.primary_status + "'," +
                         " whatsapp_no='" + values.whatsapp_no + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where institution2mobileno_gid='" + values.institution2mobileno_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("IMUL");

                    msSQL = "Insert into agr_mst_tsuprinstitution2mobilenoupdatelog(" +
                   " institution2mobilenoupdatelog_gid, " +
                   " institution2mobileno_gid, " +
                   " institution_gid, " +
                   " mobile_no," +
                   " primary_status," +
                   " whatsapp_no," +
                   " statusupdated_by," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + values.institution2mobileno_gid + "'," +
                   "'" + values.institution_gid + "'," +
                   "'" + lsmobile_no + "'," +
                   "'" + lsprimary_status + "'," +
                   "'" + lswhatsapp_no + "'," +
                   "'" + values.statusupdated_by + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Institution Mobile Number Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured While Updating Mobile Number";
            }
        }

        public void DaDeleteInstitutionMobileNo(string institution2mobileno_gid, MdlMstMobileNo values)
        {
            msSQL = "delete from agr_mst_tsuprinstitution2mobileno where institution2mobileno_gid='" + institution2mobileno_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tsuprinstitution2mobilenoupdatelog where institution2mobileno_gid='" + institution2mobileno_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Mobile Number Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting Mobile Number";
                values.status = false;
            }
        }

        // Institution Email Address

        public bool DaPostInstitutionEmailAddress(string employee_gid, MdlMstEmailAddress values)
        {
            msSQL = "select primary_status from agr_mst_tsuprinstitution2email where primary_status='Yes' and (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {

                values.status = false;
                values.message = "Already Primary Email Address Added";
                return false;
            }
            msSQL = "select institution2email_gid from agr_mst_tsuprinstitution2email where email_address='" + values.email_address + "' and (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already This Email Address Added";
                return false;
            }
            msGetGid = objcmnfunctions.GetMasterGID("IT2E");
            msSQL = " insert into agr_mst_tsuprinstitution2email(" +
                    " institution2email_gid," +
                    " institution_gid," +
                    " email_address," +
                    " primary_status," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.email_address + "'," +
                    "'" + values.primary_status + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Email Address Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Adding Email Address";
                return false;
            }
        }

        public void DaEditInstitutionEmailAddress(string institution2email_gid, MdlMstEmailAddress values)
        {
            try
            {
                msSQL = " select email_address,institution2email_gid,primary_status from agr_mst_tsuprinstitution2email where " +
                        " institution2email_gid='" + institution2email_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.email_address = objODBCDatareader["email_address"].ToString();
                    values.primary_status = objODBCDatareader["primary_status"].ToString();
                    values.institution2email_gid = objODBCDatareader["institution2email_gid"].ToString();
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

        public void DaUpdateInstitutionEmailAddress(string employee_gid, MdlMstEmailAddress values)
        {
            msSQL = " select email_address,institution2email_gid,primary_status from agr_mst_tsuprinstitution2email where " +
                        " institution2email_gid='" + values.institution2email_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsemail_address = objODBCDatareader["email_address"].ToString();
                lsprimary_status = objODBCDatareader["primary_status"].ToString();
                lsinstitution2email_gid = objODBCDatareader["institution2email_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tsuprinstitution2email set " +
                         " email_address='" + values.email_address + "'," +
                         " primary_status='" + values.primary_status + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where institution2email_gid='" + values.institution2email_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("IEUL");

                    msSQL = "Insert into agr_mst_tsuprinstitution2emailupdatelog(" +
                   " institution2emailaddressupdatelog_gid, " +
                   " institution2email_gid, " +
                   " institution_gid, " +
                   " email_address," +
                   " primary_status," +
                   " statusupdated_by," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + values.institution2email_gid + "'," +
                   "'" + values.institution_gid + "'," +
                   "'" + lsemail_address + "'," +
                   "'" + lsprimary_status + "'," +
                   "'" + values.statusupdated_by + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Email Address Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured Updating Email Address";
            }
        }

        public void DaDeleteInstitutionEmailAddress(string institution2email_gid, MdlMstEmailAddress values)
        {
            msSQL = "delete from agr_mst_tsuprinstitution2email where institution2email_gid='" + institution2email_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tsuprinstitution2emailupdatelog where institution2email_gid='" + institution2email_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Email Address Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting Email Address";
                values.status = false;

            }
        }

        // Institution Address Details

        public bool DaPostInstitutionAddressDetail(string employee_gid, string user_gid, MdlMstAddressDetails values)
        {
            msSQL = "select primary_status from agr_mst_tsuprinstitution2address where primary_status='Yes' and (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Address Added";
                return false;
            }
            msSQL = "select institution2address_gid from agr_mst_tsuprinstitution2address where addresstype_name='" + values.address_type + "' and (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already Address Type Added";
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("IT2A");
            msSQL = " insert into agr_mst_tsuprinstitution2address(" +
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
                    "'" + employee_gid + "'," +
                    "'" + values.address_typegid + "'," +
                    "'" + values.address_type + "'," +
                    "'" + values.addressline1 + "'," +
                    "'" + values.addressline2 + "'," +
                    "'" + values.primary_status + "'," +
                    "'" + values.landmark + "'," +
                    "'" + values.postal_code + "'," +
                    "'" + values.city + "'," +
                    "'" + values.taluka + "'," +
                    "'" + values.district + "'," +
                    "'" + values.state + "'," +
                    "'" + values.country + "'," +
                    "'" + values.latitude + "'," +
                    "'" + values.longitude + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Address Details Added Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Adding Address Details";
                return false;
            }

        }

        public void DaEditInstitutionAddressDetail(string institution2address_gid, MdlMstAddressDetails values)
        {
            try
            {
                msSQL = "select addresstype_gid, addresstype_name, addressline1, addressline2, landmark, taluka, primary_status, postal_code, city," +
                    " district, state, country,latitude,longitude, institution_gid, institution2address_gid " +
                    " from agr_mst_tsuprinstitution2address where institution2address_gid='" + institution2address_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.address_typegid = objODBCDatareader["addresstype_gid"].ToString();
                    values.address_type = objODBCDatareader["addresstype_name"].ToString();
                    values.addressline1 = objODBCDatareader["addressline1"].ToString();
                    values.addressline2 = objODBCDatareader["addressline2"].ToString();
                    values.landmark = objODBCDatareader["landmark"].ToString();
                    values.taluka = objODBCDatareader["taluka"].ToString();
                    values.primary_status = objODBCDatareader["primary_status"].ToString();
                    values.postal_code = objODBCDatareader["postal_code"].ToString();
                    values.city = objODBCDatareader["city"].ToString();
                    values.district = objODBCDatareader["district"].ToString();
                    values.state = objODBCDatareader["state"].ToString();
                    values.country = objODBCDatareader["country"].ToString();
                    values.institution_gid = objODBCDatareader["institution_gid"].ToString();
                    values.institution2address_gid = objODBCDatareader["institution2address_gid"].ToString();
                    values.latitude = objODBCDatareader["latitude"].ToString();
                    values.longitude = objODBCDatareader["longitude"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;
                values.message = "success";
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaUpdateInstitutionAddressDetail(string employee_gid, MdlMstAddressDetails values)
        {
            msSQL = "select addresstype_gid, addresstype_name, addressline1, addressline2, landmark, taluka, primary_status, postal_code, city," +
                    " district, state, country, latitude, longitude ,institution_gid, institution2address_gid " +
                    " from agr_mst_tsuprinstitution2address where institution2address_gid='" + values.institution2address_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsaddress_typegid = objODBCDatareader["addresstype_gid"].ToString();
                lsaddress_type = objODBCDatareader["addresstype_name"].ToString();
                lsaddressline1 = objODBCDatareader["addressline1"].ToString();
                lsaddressline2 = objODBCDatareader["addressline2"].ToString();
                lslandmark = objODBCDatareader["landmark"].ToString();
                lstaluka = objODBCDatareader["taluka"].ToString();
                lsprimary_status = objODBCDatareader["primary_status"].ToString();
                lspostal_code = objODBCDatareader["postal_code"].ToString();
                lscity = objODBCDatareader["city"].ToString();
                lsdistrict = objODBCDatareader["district"].ToString();
                lsstate = objODBCDatareader["state"].ToString();
                lscountry = objODBCDatareader["country"].ToString();
                lsinstitution_gid = objODBCDatareader["institution_gid"].ToString();
                lsinstitution2address_gid = objODBCDatareader["institution2address_gid"].ToString();
                lslatitude = objODBCDatareader["latitude"].ToString();
                lslongitude = objODBCDatareader["longitude"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tsuprinstitution2address set " +
                         " addresstype_gid='" + values.address_typegid + "'," +
                         " addresstype_name='" + values.address_type + "'," +
                         " addressline1='" + values.addressline1 + "'," +
                         " addressline2='" + values.addressline2 + "'," +
                         " landmark='" + values.landmark + "'," +
                         " taluka='" + values.taluka + "'," +
                         " primary_status='" + values.primary_status + "'," +
                         " postal_code='" + values.postal_code + "'," +
                         " city='" + values.city + "'," +
                         " district='" + values.district + "'," +
                         " state='" + values.state + "'," +
                         " country='" + values.country + "'," +
                         " latitude='" + values.latitude + "'," +
                         " longitude='" + values.longitude + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where institution2address_gid='" + values.institution2address_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("IAUL");

                    msSQL = " insert into agr_mst_tsuprinstitution2addressupdatelog(" +
                  " institution2addressupdatelog_gid," +
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
                  " statusupdated_by," +
                  " created_by," +
                  " created_date)" +
                  " values(" +
                  "'" + msGetGid + "'," +
                  "'" + values.institution2address_gid + "'," +
                  "'" + values.institution_gid + "'," +
                  "'" + lsaddress_typegid + "'," +
                  "'" + lsaddress_type + "'," +
                  "'" + lsaddressline1 + "'," +
                  "'" + lsaddressline2 + "'," +
                  "'" + lsprimary_status + "'," +
                  "'" + lslandmark + "'," +
                  "'" + lspostal_code + "'," +
                  "'" + lscity + "'," +
                  "'" + lstaluka + "'," +
                  "'" + lsdistrict + "'," +
                  "'" + lsstate + "'," +
                  "'" + lscountry + "'," +
                  "'" + lslatitude + "'," +
                  "'" + lslongitude + "'," +
                  "'" + values.statusupdated_by + "'," +
                  "'" + employee_gid + "'," +
                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Address Details Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured While Updating Address Details";
            }
        }

        public void DaDeleteInstitutionAddressDetail(string institution2address_gid, string employee_gid, MdlMstAddressDetails values)
        {
            msSQL = "delete from agr_mst_tsuprinstitution2address where institution2address_gid='" + institution2address_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tsuprinstitution2addressupdatelog where institution2address_gid='" + institution2address_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Address Deatils Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting Address Deatils";
                values.status = false;

            }
        }

        // Institution License Details

        public bool DaPostInstitutionLicenseDetail(string employee_gid, string user_gid, MdlMstLicenseDetails values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("IT2L");
            msSQL = " insert into agr_mst_tsuprinstitution2licensedtl(" +
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
                    "'" + employee_gid + "'," +
                    "'" + values.licensetype_gid + "'," +
                    "'" + values.licensetype_name + "'," +
                    "'" + values.license_number + "',";
            if ((values.licenseissue_date == null) || (values.licenseissue_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.licenseissue_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if ((values.licenseexpiry_date == null) || (values.licenseexpiry_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.licenseexpiry_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + employee_gid + "'," +
             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "License Details Added Sucessfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Adding License Details";
                return false;
            }

        }

        public void DaEditInstitutionLicenseDetail(string institution2licensedtl_gid, MdlMstLicenseDetails values)
        {
            try
            {
                msSQL = " select institution2licensedtl_gid,licensetype_gid,licensetype_name,license_no,date_format(issue_date,'%d-%m-%Y') as issue_date," +
                   " date_format(expiry_date,'%d-%m-%Y') as expiry_date, date_format(expiry_date,'%Y-%m-%d') as expiry_dateedit,date_format(issue_date,'%Y-%m-%d') as issue_dateedit,institution_gid from agr_mst_tsuprinstitution2licensedtl" +
                   " where institution2licensedtl_gid='" + institution2licensedtl_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.licensetype_gid = objODBCDatareader["licensetype_gid"].ToString();
                    values.licensetype_name = objODBCDatareader["licensetype_name"].ToString();
                    values.license_number = objODBCDatareader["license_no"].ToString();
                    values.licenseissue_date = objODBCDatareader["issue_date"].ToString();
                    values.licenseexpiry_date = objODBCDatareader["expiry_date"].ToString();
                    values.licenseissue_dateedit = objODBCDatareader["issue_dateedit"].ToString();
                    values.licenseexpiry_dateedit = objODBCDatareader["expiry_dateedit"].ToString();
                    values.institution2licensedtl_gid = objODBCDatareader["institution2licensedtl_gid"].ToString();
                    values.institution_gid = objODBCDatareader["institution_gid"].ToString();
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

        public void DaUpdateInstitutionLicenseDetail(string employee_gid, MdlMstLicenseDetails values)
        {
            msSQL = " select institution2licensedtl_gid,licensetype_gid,licensetype_name,license_no,date_format(issue_date,'%d-%m-%Y') as issue_date," +
                  " date_format(expiry_date,'%d-%m-%Y') as expiry_date, institution_gid from agr_mst_tsuprinstitution2licensedtl" +
                  " where institution2licensedtl_gid='" + values.institution2licensedtl_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lslicensetype_gid = objODBCDatareader["licensetype_gid"].ToString();
                lslicensetype_name = objODBCDatareader["licensetype_name"].ToString();
                lslicense_number = objODBCDatareader["license_no"].ToString();
                lslicenseissue_date = objODBCDatareader["issue_date"].ToString();
                lslicenseexpiry_date = objODBCDatareader["expiry_date"].ToString();
                lsinstitution2licensedtl_gid = objODBCDatareader["institution2licensedtl_gid"].ToString();
                lsinstitution_gid = objODBCDatareader["institution_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tsuprinstitution2licensedtl set " +
                         " licensetype_gid='" + values.licensetype_gid + "'," +
                         " licensetype_name='" + values.licensetype_name + "'," +
                         " license_no='" + values.license_number + "',";
                if (Convert.ToDateTime(values.licenseissue_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " issue_date='" + Convert.ToDateTime(values.licenseissue_date).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                if (Convert.ToDateTime(values.licenseexpiry_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " expiry_date='" + Convert.ToDateTime(values.licenseexpiry_date).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                msSQL += " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where institution2licensedtl_gid='" + values.institution2licensedtl_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("ILUL");

                    msSQL = "Insert into agr_mst_tsuprinstitution2licenseupdatelog(" +
                   " institution2licenseupdatelog_gid, " +
                   " institution2licensedtl_gid, " +
                   " institution_gid, " +
                   " licensetype_gid," +
                   " licensetype_name," +
                   " license_no," +
                   " issue_date," +
                   " expiry_date," +
                   " statusupdated_by," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + values.institution2licensedtl_gid + "'," +
                   "'" + values.institution_gid + "'," +
                   "'" + lslicensetype_gid + "'," +
                   "'" + lslicensetype_name + "'," +
                   "'" + lslicense_number + "'," +
                   "'" + lslicenseissue_date + "'," +
                   "'" + lslicenseexpiry_date + "'," +
                   "'" + values.statusupdated_by + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "License Details Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured While Updating License Details";
            }
        }

        public void DaDeleteInstitutionLicenseDetail(string institution2licensedtl_gid, MdlMstLicenseDetails values)
        {
            msSQL = "delete from agr_mst_tsuprinstitution2licensedtl where institution2licensedtl_gid='" + institution2licensedtl_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tsuprinstitution2licenseupdatelog where institution2licensedtl_gid='" + institution2licensedtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "License Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting License Details";
                values.status = false;

            }
        }

        public void DaGetIntitutionTempClear(string employee_gid, result values)
        {
            msSQL = "delete from agr_mst_tsuprinstitution2mobileno where institution_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprinstitution2email where institution_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprinstitution2address where institution_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprinstitution2branch where institution_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprinstitution2licensedtl where institution_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprinstitution2documentupload where institution_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprinstitution2form60documentupload where institution_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprinstitution2ratingdetail where institution_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }

        // Individual Edit Form
        public bool DaPostIndividualMobileNumber(string employee_gid, MdlContactMobileNo values)
        {
            msSQL = "select primary_status from agr_mst_tocontact2mobileno where primary_status='Yes' and (contact_gid='" + employee_gid + "' or contact_gid='" + values.contact_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Mobile Number Added";
                objdbconn.CloseConn();
                return false;
            }

            msSQL = "select mobile_no from agr_mst_tsuprcontact2mobileno where mobile_no='" + values.mobile_no + "' and (contact_gid='" + employee_gid + "' or contact_gid='" + values.contact_gid + "') ";
            string lsmobile_no = objdbconn.GetExecuteScalar(msSQL);
            if (lsmobile_no == (values.mobile_no))
            {

                values.status = false;
                values.message = "Already This Mobile Number Added";
                objdbconn.CloseConn();
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("C2MN");

            msSQL = " insert into agr_mst_tsuprcontact2mobileno(" +
                    " contact2mobileno_gid," +
                    " contact_gid," +
                    " mobile_no," +
                    " primary_status," +
                    " whatsapp_no," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.mobile_no + "'," +
                    "'" + values.primary_status + "'," +
                    "'" + values.whatsapp_no + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            objdbconn.CloseConn();

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Mobile Number Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Adding Mobile Number";
                return false;
            }
        }

        public void DaGetIndividualMobileNoTempList(string contact_gid, string employee_gid, MdlContactMobileNo values)
        {
            msSQL = "select mobile_no,contact2mobileno_gid,primary_status,whatsapp_no from agr_mst_tsuprcontact2mobileno where " +
              " contact_gid = '" + employee_gid + "' or contact_gid = '" + contact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcontactmobileno_list = new List<contactmobileno_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcontactmobileno_list.Add(new contactmobileno_list
                    {
                        contact2mobileno_gid = (dr_datarow["contact2mobileno_gid"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        whatsapp_no = (dr_datarow["whatsapp_no"].ToString()),
                    });
                }
            }
            values.contactmobileno_list = getcontactmobileno_list;
            dt_datatable.Dispose();
        }

        public void DaGetIndividualMobileNoList(string contact_gid, MdlContactMobileNo values)
        {
            msSQL = "select mobile_no,contact2mobileno_gid,primary_status,whatsapp_no from agr_mst_tsuprcontact2mobileno where " +
              " contact_gid = '" + contact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcontactmobileno_list = new List<contactmobileno_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcontactmobileno_list.Add(new contactmobileno_list
                    {
                        contact2mobileno_gid = (dr_datarow["contact2mobileno_gid"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        whatsapp_no = (dr_datarow["whatsapp_no"].ToString()),
                    });
                }
            }
            values.contactmobileno_list = getcontactmobileno_list;
            dt_datatable.Dispose();
        }

        public void DaEditIndividualMobileNo(string contact2mobileno_gid, MdlContactMobileNo values)
        {
            try
            {
                msSQL = " select mobile_no,contact2mobileno_gid,primary_status,whatsapp_no from agr_mst_tsuprcontact2mobileno where " +
                        " contact2mobileno_gid='" + contact2mobileno_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.mobile_no = objODBCDatareader["mobile_no"].ToString();
                    values.primary_status = objODBCDatareader["primary_status"].ToString();
                    values.whatsapp_no = objODBCDatareader["whatsapp_no"].ToString();
                    values.contact2mobileno_gid = objODBCDatareader["contact2mobileno_gid"].ToString();
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

        public void DaUpdateIndividualMobileNo(string employee_gid, MdlContactMobileNo values)
        {
            msSQL = " select mobile_no,contact2mobileno_gid,primary_status,whatsapp_no from agr_mst_tsuprcontact2mobileno where " +
                    " contact2mobileno_gid='" + values.contact2mobileno_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsmobile_no = objODBCDatareader["mobile_no"].ToString();
                lsprimary_status = objODBCDatareader["primary_status"].ToString();
                lswhatsapp_no = objODBCDatareader["whatsapp_no"].ToString();
                lscontact2mobileno_gid = objODBCDatareader["contact2mobileno_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tsuprcontact2mobileno set " +
                         " mobile_no='" + values.mobile_no + "'," +
                         " primary_status='" + values.primary_status + "'," +
                         " whatsapp_no='" + values.whatsapp_no + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where contact2mobileno_gid='" + values.contact2mobileno_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("CMUL");

                    msSQL = "Insert into agr_mst_tsuprcontact2mobilenoupdatelog(" +
                   " contact2mobilenoupdatelog_gid, " +
                   " contact2mobileno_gid, " +
                   " contact_gid, " +
                   " mobile_no," +
                   " primary_status," +
                   " whatsapp_no," +
                   " statusupdated_by," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + values.contact2mobileno_gid + "'," +
                   "'" + values.contact_gid + "'," +
                   "'" + lsmobile_no + "'," +
                   "'" + lsprimary_status + "'," +
                   "'" + lswhatsapp_no + "'," +
                   "'" + values.statusupdated_by + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Individual Mobile Number Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured While Updating Mobile Number";
            }
        }

        public void DaDeleteIndividualMobileNo(string contact2mobileno_gid, MdlContactMobileNo values)
        {
            msSQL = "delete from agr_mst_tsuprcontact2mobileno where contact2mobileno_gid='" + contact2mobileno_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tsuprcontact2mobilenoupdatelog where contact2mobileno_gid='" + contact2mobileno_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Mobile Number Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting Mobile Number";
                values.status = false;
            }
        }

        public bool DaPostIndividualEmailAddress(string employee_gid, MdlContactEmail values)
        {
            msSQL = "select primary_status from agr_mst_tsuprcontact2email where primary_status='Yes' and (contact_gid='" + employee_gid + "' or contact_gid='" + values.contact_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Email Address Added";
                objdbconn.CloseConn();
                return false;
            }

            msSQL = "select email_address from agr_mst_tsuprcontact2email where email_address='" + values.email_address + "' and (contact_gid='" + employee_gid + "' or contact_gid='" + values.contact_gid + "')";
            string lsemail_address = objdbconn.GetExecuteScalar(msSQL);
            if (lsemail_address == (values.email_address))
            {
                values.status = false;
                values.message = "Already This Email Address Added";
                objdbconn.CloseConn();
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("C2EA");
            msSQL = " insert into agr_mst_tsuprcontact2email(" +
                    " contact2email_gid," +
                    " contact_gid," +
                    " email_address," +
                    " primary_status," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.email_address + "'," +
                    "'" + values.primary_status + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            objdbconn.CloseConn();
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Email Address Added Sucessfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Adding Email Address";
                return false;
            }
        }

        public void DaGetIndividualEmailAddressTempList(string contact_gid, string employee_gid, MdlContactEmail values)
        {
            msSQL = "select email_address,contact2email_gid,primary_status from agr_mst_tsuprcontact2email where " +
              " contact_gid='" + employee_gid + "' or contact_gid = '" + contact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcontactemail_list = new List<contactemail_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcontactemail_list.Add(new contactemail_list
                    {
                        contact2email_gid = (dr_datarow["contact2email_gid"].ToString()),
                        email_address = (dr_datarow["email_address"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                    });
                }
            }
            values.contactemail_list = getcontactemail_list;
            dt_datatable.Dispose();
        }

        public void DaGetIndividualEmailAddressList(string contact_gid, string employee_gid, MdlContactEmail values)
        {
            msSQL = "select email_address,contact2email_gid,primary_status from agr_mst_tsuprcontact2email where " +
              " contact_gid='" + contact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcontactemail_list = new List<contactemail_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcontactemail_list.Add(new contactemail_list
                    {
                        contact2email_gid = (dr_datarow["contact2email_gid"].ToString()),
                        email_address = (dr_datarow["email_address"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                    });
                }
            }
            values.contactemail_list = getcontactemail_list;
            dt_datatable.Dispose();
        }

        public void DaEditIndividualEmailAddress(string contact2email_gid, MdlContactEmail values)
        {
            try
            {
                msSQL = " select email_address,contact2email_gid,primary_status from agr_mst_tsuprcontact2email where " +
                        " contact2email_gid='" + contact2email_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.email_address = objODBCDatareader["email_address"].ToString();
                    values.primary_status = objODBCDatareader["primary_status"].ToString();
                    values.contact2email_gid = objODBCDatareader["contact2email_gid"].ToString();
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

        public void DaUpdateIndividualEmailAddress(string employee_gid, MdlContactEmail values)
        {
            msSQL = " select email_address,contact2email_gid,primary_status from agr_mst_tsuprcontact2email where " +
                        " contact2email_gid='" + values.contact2email_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsemail_address = objODBCDatareader["email_address"].ToString();
                lsprimary_status = objODBCDatareader["primary_status"].ToString();
                lscontact2email_gid = objODBCDatareader["contact2email_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tsuprcontact2email set " +
                         " email_address='" + values.email_address + "'," +
                         " primary_status='" + values.primary_status + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where contact2email_gid='" + values.contact2email_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("IEUL");

                    msSQL = "Insert into agr_mst_tsuprcontact2emailupdatelog(" +
                   " contact2emailaddressupdatelog_gid, " +
                   " contact2email_gid, " +
                   " contact_gid, " +
                   " email_address," +
                   " primary_status," +
                   " statusupdated_by," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + values.contact2email_gid + "'," +
                   "'" + values.contact_gid + "'," +
                   "'" + lsemail_address + "'," +
                   "'" + lsprimary_status + "'," +
                   "'" + values.statusupdated_by + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Individual Email Address Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured While Updating Email Address";
            }
        }

        public void DaDeleteIndividualEmailAddress(string contact2email_gid, MdlContactEmail values)
        {
            msSQL = "delete from agr_mst_tsuprcontact2email where contact2email_gid='" + contact2email_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tsuprcontact2emailupdatelog where contact2email_gid='" + contact2email_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Email Address Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting Email Address";
                values.status = false;

            }
        }

        public bool DaPostIndividualAddress(string employee_gid, MdlContactAddress values)
        {
            msSQL = "select primary_status from agr_mst_tsuprcontact2address where primary_status='Yes' and (contact_gid='" + employee_gid + "' or contact_gid='" + values.contact_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Address Added";
                return false;
            }

            msSQL = "select contact2address_gid from agr_mst_tsuprcontact2address where addresstype_name='" + values.addresstype_name + "' and " +
               " (contact_gid='" + employee_gid + "' or contact_gid='" + values.contact_gid + "')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already Address Type Added";
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("C2AD");
            msSQL = " insert into agr_mst_tsuprcontact2address(" +
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
                    "'" + employee_gid + "'," +
                    "'" + values.addresstype_gid + "'," +
                    "'" + values.addresstype_name + "'," +
                    "'" + values.primary_status + "'," +
                    "'" + values.addressline1 + "'," +
                    "'" + values.addressline2 + "'," +
                    "'" + values.landmark + "'," +
                    "'" + values.postal_code + "'," +
                    "'" + values.city + "'," +
                    "'" + values.taluka + "'," +
                    "'" + values.district + "'," +
                    "'" + values.state + "'," +
                    "'" + values.country + "'," +
                    "'" + values.latitude + "'," +
                    "'" + values.longitude + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Address Details Added Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Adding Address Details";
                return false;
            }

        }

        public void DaGetIndividualAddressTempList(string contact_gid, string employee_gid, MdlContactAddress values)
        {
            msSQL = " select contact2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country," +
                    " postal_code,latitude,longitude from agr_mst_tsuprcontact2address where contact_gid='" + employee_gid + "' or contact_gid = '" + contact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcontactaddress_list = new List<contactaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcontactaddress_list.Add(new contactaddress_list
                    {
                        contact2address_gid = (dr_datarow["contact2address_gid"].ToString()),
                        addresstype_name = (dr_datarow["addresstype_name"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        addressline1 = (dr_datarow["addressline1"].ToString()),
                        addressline2 = (dr_datarow["addressline2"].ToString()),
                        taluka = (dr_datarow["taluka"].ToString()),
                        district = (dr_datarow["district"].ToString()),
                        state = (dr_datarow["state"].ToString()),
                        country = (dr_datarow["country"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString()),
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                    });
                }
                values.contactaddress_list = getcontactaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGetIndividualAddressList(string contact_gid, string employee_gid, MdlContactAddress values)
        {
            msSQL = " select contact2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country," +
                    " postal_code,latitude, longitude from agr_mst_tsuprcontact2address where contact_gid='" + contact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcontactaddress_list = new List<contactaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcontactaddress_list.Add(new contactaddress_list
                    {
                        contact2address_gid = (dr_datarow["contact2address_gid"].ToString()),
                        addresstype_name = (dr_datarow["addresstype_name"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        addressline1 = (dr_datarow["addressline1"].ToString()),
                        addressline2 = (dr_datarow["addressline2"].ToString()),
                        taluka = (dr_datarow["taluka"].ToString()),
                        district = (dr_datarow["district"].ToString()),
                        state = (dr_datarow["state"].ToString()),
                        country = (dr_datarow["country"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString()),
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                    });
                }
                values.contactaddress_list = getcontactaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaEditIndividualAddress(string contact2address_gid, MdlContactAddress values)
        {
            try
            {
                msSQL = "select addresstype_gid, addresstype_name, addressline1, addressline2, landmark, taluka, primary_status, postal_code, city," +
                    " district, state, country, contact_gid, contact2address_gid ,latitude, longitude " +
                    " from agr_mst_tsuprcontact2address where contact2address_gid='" + contact2address_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.addresstype_gid = objODBCDatareader["addresstype_gid"].ToString();
                    values.addresstype_name = objODBCDatareader["addresstype_name"].ToString();
                    values.addressline1 = objODBCDatareader["addressline1"].ToString();
                    values.addressline2 = objODBCDatareader["addressline2"].ToString();
                    values.landmark = objODBCDatareader["landmark"].ToString();
                    values.taluka = objODBCDatareader["taluka"].ToString();
                    values.primary_status = objODBCDatareader["primary_status"].ToString();
                    values.postal_code = objODBCDatareader["postal_code"].ToString();
                    values.city = objODBCDatareader["city"].ToString();
                    values.district = objODBCDatareader["district"].ToString();
                    values.state = objODBCDatareader["state"].ToString();
                    values.country = objODBCDatareader["country"].ToString();
                    values.contact_gid = objODBCDatareader["contact_gid"].ToString();
                    values.contact2address_gid = objODBCDatareader["contact2address_gid"].ToString();
                    values.latitude = objODBCDatareader["latitude"].ToString();
                    values.longitude = objODBCDatareader["longitude"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;
                values.message = "success";
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaUpdateIndividualAddress(string employee_gid, MdlContactAddress values)
        {
            msSQL = "select addresstype_gid, addresstype_name, addressline1, addressline2, landmark, taluka, primary_status, postal_code, city," +
                    " district, state, country, contact_gid, contact2address_gid , latitude, longitude " +
                    " from agr_mst_tsuprcontact2address where contact2address_gid='" + values.contact2address_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsaddress_typegid = objODBCDatareader["addresstype_gid"].ToString();
                lsaddress_type = objODBCDatareader["addresstype_name"].ToString();
                lsaddressline1 = objODBCDatareader["addressline1"].ToString();
                lsaddressline2 = objODBCDatareader["addressline2"].ToString();
                lslandmark = objODBCDatareader["landmark"].ToString();
                lstaluka = objODBCDatareader["taluka"].ToString();
                lsprimary_status = objODBCDatareader["primary_status"].ToString();
                lspostal_code = objODBCDatareader["postal_code"].ToString();
                lscity = objODBCDatareader["city"].ToString();
                lsdistrict = objODBCDatareader["district"].ToString();
                lsstate = objODBCDatareader["state"].ToString();
                lscountry = objODBCDatareader["country"].ToString();
                lscontact_gid = objODBCDatareader["contact_gid"].ToString();
                lscontact2address_gid = objODBCDatareader["contact2address_gid"].ToString();
                lslatitude = objODBCDatareader["latitude"].ToString();
                lslongitude = objODBCDatareader["longitude"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tsuprcontact2address set " +
                         " addresstype_gid='" + values.addresstype_gid + "'," +
                         " addresstype_name='" + values.addresstype_name + "'," +
                         " addressline1='" + values.addressline1 + "'," +
                         " addressline2='" + values.addressline2 + "'," +
                         " landmark='" + values.landmark + "'," +
                         " taluka='" + values.taluka + "'," +
                         " primary_status='" + values.primary_status + "'," +
                         " postal_code='" + values.postal_code + "'," +
                         " city='" + values.city + "'," +
                         " district='" + values.district + "'," +
                         " state='" + values.state + "'," +
                         " country='" + values.country + "'," +
                         " latitude = '" + values.latitude + "'," +
                         " longitude = '" + values.longitude + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where contact2address_gid='" + values.contact2address_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("IAUL");

                    msSQL = " insert into agr_mst_tsuprcontact2addressupdatelog(" +
                  " contact2addressupdatelog_gid," +
                  " contact2address_gid," +
                  " contact_gid," +
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
                  " statusupdated_by," +
                  " created_by," +
                  " created_date)" +
                  " values(" +
                  "'" + msGetGid + "'," +
                  "'" + values.contact2address_gid + "'," +
                  "'" + values.contact_gid + "'," +
                  "'" + lsaddress_typegid + "'," +
                  "'" + lsaddress_type + "'," +
                  "'" + lsaddressline1 + "'," +
                  "'" + lsaddressline2 + "'," +
                  "'" + lsprimary_status + "'," +
                  "'" + lslandmark + "'," +
                  "'" + lspostal_code + "'," +
                  "'" + lscity + "'," +
                  "'" + lstaluka + "'," +
                  "'" + lsdistrict + "'," +
                  "'" + lsstate + "'," +
                  "'" + lscountry + "'," +
                  "'" + lslatitude + "'," +
                  "'" + lslongitude + "'," +
                  "'" + values.statusupdated_by + "'," +
                  "'" + employee_gid + "'," +
                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Address Details Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured While Updating Address Details";
            }
        }

        public void DaDeleteIndividualAddress(string contact2address_gid, MdlContactAddress values)
        {
            msSQL = "delete from agr_mst_tsuprcontact2address where contact2address_gid='" + contact2address_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tsuprcontact2addressupdatelog where contact2address_gid='" + contact2address_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Address Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while Deleting Address Details";
                values.status = false;

            }
        }

        public bool DaIndividualProofDocumentUpload(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string lsidproof_type = httpRequest.Form["idproof_type"].ToString();
            string lsidproof_no = httpRequest.Form["idproof_no"].ToString();
            String path = lspath;
            string lsidproof_dob = httpRequest.Form["idproof_dob"].ToString();
            string lsfile_no = httpRequest.Form["file_no"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/IndividualProofDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                        //lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SamAgro/IndividualProofDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        //FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                        //ms.WriteTo(file);
                        //file.Close();
                        //ms.Close();
                        //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "SamAgro/IndividualProofDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/IndividualProofDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/IndividualProofDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        msGetGid = objcmnfunctions.GetMasterGID("C2IP");
                        msSQL = " insert into agr_mst_tsuprcontact2idproof(" +
                                " contact2idproof_gid," +
                                " contact_gid," +
                                " idproof_name," +
                                " idproof_no," +
                                " idproof_dob," +
                                " file_no," +
                                " document_name," +
                                " document_path," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid + "'," +
                                "'" + employee_gid + "'," +
                                "'" + lsidproof_type + "'," +
                                "'" + lsidproof_no + "'," +
                                "'" + lsidproof_dob + "'," +
                                "'" + lsfile_no + "'," +
                                "'" + httpPostedFile.FileName + "'," +
                                "'" + lspath + msdocument_gid + FileExtension + "'," +
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

        public void DaGetIndividualProofTempList(string contact_gid, string employee_gid, MdlContactIdProof values)
        {
            msSQL = "select contact2idproof_gid,idproof_name,idproof_no,document_name, document_path,idproof_dob,file_no from agr_mst_tsuprcontact2idproof where " +
              " contact_gid='" + employee_gid + "' or contact_gid = '" + contact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcontactidproof_list = new List<contactidproof_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcontactidproof_list.Add(new contactidproof_list
                    {
                        contact2idproof_gid = (dr_datarow["contact2idproof_gid"].ToString()),
                        idproof_name = (dr_datarow["idproof_name"].ToString()),
                        idproof_no = (dr_datarow["idproof_no"].ToString()),
                        idproof_dob = (dr_datarow["idproof_dob"].ToString()),
                        file_no = (dr_datarow["file_no"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = objcmnstorage.EncryptData(((dr_datarow["document_path"].ToString()))),
                    });

                    values.contactidproof_list = getcontactidproof_list;
                }
                dt_datatable.Dispose();
            }

        }

        public void DaGetIndividualProofList(string contact_gid, string employee_gid, MdlContactIdProof values)
        {
            msSQL = "select contact2idproof_gid,idproof_name,idproof_no,document_name, document_path,idproof_dob,file_no from agr_mst_tsuprcontact2idproof where " +
              " contact_gid='" + contact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcontactidproof_list = new List<contactidproof_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcontactidproof_list.Add(new contactidproof_list
                    {
                        contact2idproof_gid = (dr_datarow["contact2idproof_gid"].ToString()),
                        idproof_name = (dr_datarow["idproof_name"].ToString()),
                        idproof_no = (dr_datarow["idproof_no"].ToString()),
                        idproof_dob = (dr_datarow["idproof_dob"].ToString()),
                        file_no = (dr_datarow["file_no"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = objcmnstorage.EncryptData(((dr_datarow["document_path"].ToString()))),
                    });

                    values.contactidproof_list = getcontactidproof_list;
                }
                dt_datatable.Dispose();
            }

        }

        public void DaIndividualProofDelete(string contact2idproof_gid, MdlContactIdProof values)
        {
            msSQL = "delete from agr_mst_tsuprcontact2idproof where contact2idproof_gid='" + contact2idproof_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "ID Proof Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;
            }
        }

        public bool DaIndividualDocumentUpload(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string lsdocument_title = httpRequest.Form["document_title"].ToString();
            string lsindividualdocument_gid = httpRequest.Form["individualdocument_gid"].ToString();
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/IndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                        //lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SamAgro/IndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        //FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                        //ms.WriteTo(file);
                        //file.Close();
                        //ms.Close();
                        //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "SamAgro/IndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/IndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/IndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        msSQL = "select covenant_type from ocs_mst_tindividualdocument where individualdocument_gid='" + lsindividualdocument_gid + "'";
                        string lscovenant_type = objdbconn.GetExecuteScalar(msSQL);

                        msGetGid = objcmnfunctions.GetMasterGID("C2DO");
                        msGetDocumentGid = objcmnfunctions.GetMasterGID("BSDA");

                        msSQL = " insert into agr_mst_tsuprcontact2document( " +
                                    " contact2document_gid ," +
                                    " contact_gid ," +
                                    " individualdocument_gid, " +
                                    " covenant_type," +
                                    " document_gid ," +
                                    " document_title ," +
                                    " document_name ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + lsindividualdocument_gid + "'," +
                                    "'" + lscovenant_type + "'," +
                                    "'" + msGetDocumentGid + "'," +
                                    "'" + lsdocument_title + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
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

        public void DaGetIndividualDocTempList(string contact_gid, string employee_gid, MdlContactDocument values)
        {
            msSQL = " select contact2document_gid,document_name,document_title,document_path from agr_mst_tsuprcontact2document " +
                                 " where contact_gid='" + employee_gid + "' or contact_gid = '" + contact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<uploadindividualdoc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new uploadindividualdoc_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        contact2document_gid = dt["contact2document_gid"].ToString(),
                    });
                    values.uploadindividualdoc_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetIndividualDocList(string contact_gid, MdlContactDocument values)
        {
            msSQL = " select contact2document_gid,document_name,document_title,document_path from agr_mst_tsuprcontact2document " +
                                 " where contact_gid='" + contact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<uploadindividualdoc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new uploadindividualdoc_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        contact2document_gid = dt["contact2document_gid"].ToString(),
                    });
                    values.uploadindividualdoc_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaIndividualDocDelete(string contact2document_gid, MdlContactDocument values)
        {
            msSQL = "delete from agr_mst_tsuprcontact2document where contact2document_gid='" + contact2document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                msSQL = " select groupdocumentchecklist_gid from agr_trn_tsuprdocumentchecktls where documentuploaded_gid='" + contact2document_gid + "'";
                string lsgroupdocumentchecklist_gid = objdbconn.GetExecuteScalar(msSQL);

                if (lsgroupdocumentchecklist_gid != "")
                {
                    msSQL = " select count(*) as documentcount from agr_trn_tsuprdocumentchecktls " +
                            " where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                    string lsdocumentcount = objdbconn.GetExecuteScalar(msSQL);
                    if (lsdocumentcount == "1")
                    {
                        msSQL = "delete from agr_trn_tsuprgroupdocumentchecklist where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                msSQL = " select groupcovdocumentchecklist_gid from agr_trn_tsuprcovanantdocumentcheckdtls where documentuploaded_gid='" + contact2document_gid + "'";
                string lschecklist_gid = objdbconn.GetExecuteScalar(msSQL);

                if (lschecklist_gid != "")
                {
                    msSQL = " select count(*) as documentcount from agr_trn_tsuprcovanantdocumentcheckdtls " +
                      " where groupcovdocumentchecklist_gid='" + lschecklist_gid + "'";
                    string lsdocumentcount = objdbconn.GetExecuteScalar(msSQL);
                    if (lsdocumentcount == "1")
                    {
                        msSQL = "delete from agr_trn_tsuprgroupcovenantdocumentchecklist where groupcovdocumentchecklist_gid='" + lschecklist_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                msSQL = "delete from agr_trn_tsuprcovanantdocumentcheckdtls where documentuploaded_gid='" + contact2document_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "delete from agr_trn_tsuprdocumentchecktls where documentuploaded_gid='" + contact2document_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (mnResult != 0)
            {

                values.message = "Document deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While deleting the Document";
                values.status = false;

            }
        }

        public void DaEditIndividual(string contact_gid, MdlMstContact values)
        {
            try
            {
                msSQL = " select pan_status,pan_no,aadhar_no,first_name,middle_name,last_name,individual_dob,age,gender_gid,gender_name,designation_gid,designation_name," +
                        " educationalqualification_gid,educationalqualification_name,main_occupation,annual_income,monthly_income," +
                        " pep_status,date_format(pepverified_date,'%d-%m-%Y') as pepverified_date,maritalstatus_gid,maritalstatus_name,stakeholdertype_gid,stakeholder_type," +
                        " father_firstname,father_middlename,father_lastname,father_dob,father_age," +
                        " mother_firstname,mother_middlename,mother_lastname,mother_dob,mother_age," +
                        " spouse_firstname,spouse_middlename,spouse_lastname,spouse_dob,spouse_age," +
                        " ownershiptype_gid,ownershiptype_name,residencetype_gid,residencetype_name,currentresidence_years,branch_distance, contact_status," +
                        " propertyholder_gid, propertyholder_name, incometype_gid, incometype_name, previouscrop, prposedcrop,institution_gid,institution_name," +
                        " group_gid, group_name, profile, urn_status, urn, fathernominee_status, mothernominee_status, spousenominee_status, othernominee_status," +
                        " relationshiptype, nomineefirst_name, nominee_middlename, nominee_lastname, nominee_dob, nominee_age, totallandinacres, cultivatedland" +
                        " from agr_mst_tsuprcontact where contact_gid='" + contact_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.pan_status = objODBCDatareader["pan_status"].ToString();
                    values.pan_no = objODBCDatareader["pan_no"].ToString();
                    values.aadhar_no = objODBCDatareader["aadhar_no"].ToString();
                    values.first_name = objODBCDatareader["first_name"].ToString();
                    values.middle_name = objODBCDatareader["middle_name"].ToString();
                    values.last_name = objODBCDatareader["last_name"].ToString();
                    values.individual_dob = objODBCDatareader["individual_dob"].ToString();
                    values.age = objODBCDatareader["age"].ToString();
                    values.gender_gid = objODBCDatareader["gender_gid"].ToString();
                    values.gender_name = objODBCDatareader["gender_name"].ToString();
                    values.designation_gid = objODBCDatareader["designation_gid"].ToString();
                    values.designation_name = objODBCDatareader["designation_name"].ToString();

                    values.educationalqualification_gid = objODBCDatareader["educationalqualification_gid"].ToString();
                    values.educationalqualification_name = objODBCDatareader["educationalqualification_name"].ToString();
                    values.main_occupation = objODBCDatareader["main_occupation"].ToString();
                    values.annual_income = objODBCDatareader["annual_income"].ToString();
                    values.monthly_income = objODBCDatareader["monthly_income"].ToString();

                    values.pep_status = objODBCDatareader["pep_status"].ToString();
                    values.pepverified_date = objODBCDatareader["pepverified_date"].ToString();
                    values.maritalstatus_gid = objODBCDatareader["maritalstatus_gid"].ToString();
                    values.maritalstatus_name = objODBCDatareader["maritalstatus_name"].ToString();
                    values.stakeholdertype_gid = objODBCDatareader["stakeholdertype_gid"].ToString();
                    values.stakeholdertype_name = objODBCDatareader["stakeholder_type"].ToString();

                    values.father_firstname = objODBCDatareader["father_firstname"].ToString();
                    values.father_middlename = objODBCDatareader["father_middlename"].ToString();
                    values.father_lastname = objODBCDatareader["father_lastname"].ToString();
                    values.father_dob = objODBCDatareader["father_dob"].ToString();
                    values.father_age = objODBCDatareader["father_age"].ToString();

                    values.mother_firstname = objODBCDatareader["mother_firstname"].ToString();
                    values.mother_middlename = objODBCDatareader["mother_middlename"].ToString();
                    values.mother_lastname = objODBCDatareader["mother_lastname"].ToString();
                    values.mother_dob = objODBCDatareader["mother_dob"].ToString();
                    values.mother_age = objODBCDatareader["mother_age"].ToString();

                    values.spouse_firstname = objODBCDatareader["spouse_firstname"].ToString();
                    values.spouse_middlename = objODBCDatareader["spouse_middlename"].ToString();
                    values.spouse_lastname = objODBCDatareader["spouse_lastname"].ToString();
                    values.spouse_dob = objODBCDatareader["spouse_dob"].ToString();
                    values.spouse_age = objODBCDatareader["spouse_age"].ToString();

                    values.ownershiptype_gid = objODBCDatareader["ownershiptype_gid"].ToString();
                    values.ownershiptype_name = objODBCDatareader["ownershiptype_name"].ToString();
                    values.residencetype_gid = objODBCDatareader["residencetype_gid"].ToString();
                    values.residencetype_name = objODBCDatareader["residencetype_name"].ToString();
                    values.currentresidence_years = objODBCDatareader["currentresidence_years"].ToString();
                    values.branch_distance = objODBCDatareader["branch_distance"].ToString();
                    values.contact_status = objODBCDatareader["contact_status"].ToString();

                    values.propertyholder_gid = objODBCDatareader["propertyholder_gid"].ToString();
                    values.propertyholder_name = objODBCDatareader["propertyholder_name"].ToString();
                    values.incometype_gid = objODBCDatareader["incometype_gid"].ToString();
                    values.incometype_name = objODBCDatareader["incometype_name"].ToString();

                    values.group_gid = objODBCDatareader["group_gid"].ToString();
                    values.group_name = objODBCDatareader["group_name"].ToString();
                    values.profile = objODBCDatareader["profile"].ToString();
                    values.urn_status = objODBCDatareader["urn_status"].ToString();
                    values.urn = objODBCDatareader["urn"].ToString();
                    values.fathernominee_status = objODBCDatareader["fathernominee_status"].ToString();
                    values.mothernominee_status = objODBCDatareader["mothernominee_status"].ToString();
                    values.spousenominee_status = objODBCDatareader["spousenominee_status"].ToString();
                    values.othernominee_status = objODBCDatareader["othernominee_status"].ToString();
                    values.relationshiptype = objODBCDatareader["relationshiptype"].ToString();
                    values.nomineefirst_name = objODBCDatareader["nomineefirst_name"].ToString();
                    values.nominee_middlename = objODBCDatareader["nominee_middlename"].ToString();
                    values.nominee_lastname = objODBCDatareader["nominee_lastname"].ToString();
                    values.nominee_dob = objODBCDatareader["nominee_dob"].ToString();
                    values.nominee_age = objODBCDatareader["nominee_age"].ToString();
                    values.totallandinacres = objODBCDatareader["totallandinacres"].ToString();
                    values.cultivatedland = objODBCDatareader["cultivatedland"].ToString();
                    values.previouscrop = objODBCDatareader["previouscrop"].ToString();
                    values.prposedcrop = objODBCDatareader["prposedcrop"].ToString();
                    values.institution_gid = objODBCDatareader["institution_gid"].ToString();
                    values.institution_name = objODBCDatareader["institution_name"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;
                values.message = "success";
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public bool DaUpdateIndividual(string employee_gid, MdlMstContact values)
        {
            msSQL = "select contact_gid from agr_mst_tsuprcontact2mobileno where (contact_gid='" + employee_gid + "' or  contact_gid='" + values.contact_gid + "') and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Mobile Number ";
                return false;
            }
            objODBCDatareader.Close();

            msSQL = "select contact_gid from agr_mst_tsuprcontact2email where contact_gid='" + employee_gid + "' or  contact_gid='" + values.contact_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Email Address";
                return false;
            }
            objODBCDatareader.Close();
            msSQL = "select contact_gid from agr_mst_tsuprcontact2address where contact_gid='" + employee_gid + "' or contact_gid='" + values.contact_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Address";
                return false;
            }
            objODBCDatareader.Close();
            msSQL = "select pan_status from agr_mst_tsuprcontact where contact_gid='" + values.contact_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (values.pan_status == "Customer Submitting PAN")
            {
                msSQL = "delete from agr_mst_tsuprcontact2panform60 where contact_gid='" + values.contact_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            objODBCDatareader.Close();

            msSQL = " select pan_status,pan_no,aadhar_no,first_name,middle_name,last_name,individual_dob,age,gender_gid,gender_name,designation_gid,designation_name," +
                        " educationalqualification_gid,educationalqualification_name,main_occupation,annual_income,monthly_income," +
                        " pep_status,pepverified_date,maritalstatus_gid,maritalstatus_name,stakeholdertype_gid,stakeholder_type," +
                        " father_firstname,father_middlename,father_lastname,father_dob,father_age," +
                        " mother_firstname,mother_middlename,mother_lastname,mother_dob,mother_age," +
                        " spouse_firstname,spouse_middlename,spouse_lastname,spouse_dob,spouse_age," +
                        " ownershiptype_gid,ownershiptype_name,residencetype_gid,residencetype_name,currentresidence_years,branch_distance," +
                        " propertyholder_gid, propertyholder_name, incometype_gid, incometype_name, previouscrop, prposedcrop,institution_gid,institution_name," +
                        " group_gid, group_name, profile, urn_status, urn, fathernominee_status, mothernominee_status, spousenominee_status, othernominee_status," +
                        " relationshiptype, nomineefirst_name, nominee_middlename, nominee_lastname, nominee_dob, nominee_age, totallandinacres, cultivatedland" +
                        " from agr_mst_tsuprcontact where contact_gid='" + values.contact_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                lspan_status = objODBCDatareader["pan_status"].ToString();
                lspan_no = objODBCDatareader["pan_no"].ToString();
                lsaadhar_no = objODBCDatareader["aadhar_no"].ToString();
                lsfirst_name = objODBCDatareader["first_name"].ToString();
                lsmiddle_name = objODBCDatareader["middle_name"].ToString();
                lslast_name = objODBCDatareader["last_name"].ToString();
                lsindividual_dob = objODBCDatareader["individual_dob"].ToString();
                lsage = objODBCDatareader["age"].ToString();

                lsgender_gid = objODBCDatareader["gender_gid"].ToString();
                lsgender_name = objODBCDatareader["gender_name"].ToString();
                lsdesignation_gid = objODBCDatareader["designation_gid"].ToString();
                lsdesignation_name = objODBCDatareader["designation_name"].ToString();
                lseducationalqualification_gid = objODBCDatareader["educationalqualification_gid"].ToString();
                lseducationalqualification_name = objODBCDatareader["educationalqualification_name"].ToString();

                lsmain_occupation = objODBCDatareader["main_occupation"].ToString();
                lsannual_income = objODBCDatareader["annual_income"].ToString();
                lsmonthly_income = objODBCDatareader["monthly_income"].ToString();
                lspep_status = objODBCDatareader["pep_status"].ToString();
                lspepverified_date = objODBCDatareader["pepverified_date"].ToString();

                lsmaritalstatus_gid = objODBCDatareader["maritalstatus_gid"].ToString();
                lsmaritalstatus_name = objODBCDatareader["maritalstatus_name"].ToString();
                lsstakeholdertype_gid = objODBCDatareader["stakeholdertype_gid"].ToString();
                lsstakeholder_type = objODBCDatareader["stakeholder_type"].ToString();

                lsfather_firstname = objODBCDatareader["father_firstname"].ToString();
                lsfather_middlename = objODBCDatareader["father_middlename"].ToString();
                lsfather_lastname = objODBCDatareader["father_lastname"].ToString();
                lsfather_dob = objODBCDatareader["father_dob"].ToString();
                lsfather_age = objODBCDatareader["father_age"].ToString();

                lsmother_firstname = objODBCDatareader["mother_firstname"].ToString();
                lsmother_middlename = objODBCDatareader["mother_middlename"].ToString();
                lsmother_lastname = objODBCDatareader["mother_lastname"].ToString();
                lsmother_dob = objODBCDatareader["mother_dob"].ToString();
                lsmother_age = objODBCDatareader["mother_age"].ToString();

                lsspouse_firstname = objODBCDatareader["spouse_firstname"].ToString();
                lsspouse_middlename = objODBCDatareader["spouse_middlename"].ToString();
                lsspouse_lastname = objODBCDatareader["spouse_lastname"].ToString();
                lsspouse_dob = objODBCDatareader["spouse_dob"].ToString();
                lsspouse_age = objODBCDatareader["spouse_age"].ToString();

                lsownershiptype_gid = objODBCDatareader["ownershiptype_gid"].ToString();
                lsownershiptype_name = objODBCDatareader["ownershiptype_name"].ToString();
                lsresidencetype_gid = objODBCDatareader["residencetype_gid"].ToString();
                lsresidencetype_name = objODBCDatareader["residencetype_name"].ToString();
                lscurrentresidence_years = objODBCDatareader["currentresidence_years"].ToString();
                lsbranch_distance = objODBCDatareader["branch_distance"].ToString();

                lsresidencetype_gid = objODBCDatareader["residencetype_gid"].ToString();
                lsresidencetype_name = objODBCDatareader["residencetype_name"].ToString();
                lscurrentresidence_years = objODBCDatareader["currentresidence_years"].ToString();
                lsbranch_distance = objODBCDatareader["branch_distance"].ToString();

                lspropertyholder_gid = objODBCDatareader["propertyholder_gid"].ToString();
                lspropertyholder_name = objODBCDatareader["propertyholder_name"].ToString();
                lsincometype_gid = objODBCDatareader["incometype_gid"].ToString();
                lsincometype_name = objODBCDatareader["incometype_name"].ToString();

                lsgroup_gid = objODBCDatareader["group_gid"].ToString();
                lsgroup_name = objODBCDatareader["group_name"].ToString();
                lsprofile = objODBCDatareader["profile"].ToString();
                lsurn_status = objODBCDatareader["urn_status"].ToString();
                lsurn = objODBCDatareader["urn"].ToString();
                lsfathernominee_status = objODBCDatareader["fathernominee_status"].ToString();
                lsmothernominee_status = objODBCDatareader["mothernominee_status"].ToString();
                lsspousenominee_status = objODBCDatareader["spousenominee_status"].ToString();
                lsothernominee_status = objODBCDatareader["othernominee_status"].ToString();
                lsrelationshiptype = objODBCDatareader["relationshiptype"].ToString();
                lsnomineefirst_name = objODBCDatareader["nomineefirst_name"].ToString();
                lsnominee_middlename = objODBCDatareader["nominee_middlename"].ToString();
                lsnominee_lastname = objODBCDatareader["nominee_lastname"].ToString();
                lsnominee_dob = objODBCDatareader["nominee_dob"].ToString();
                lsnominee_age = objODBCDatareader["nominee_age"].ToString();
                lstotallandinacres = objODBCDatareader["totallandinacres"].ToString();
                lscultivatedland = objODBCDatareader["cultivatedland"].ToString();
                lspreviouscrop = objODBCDatareader["previouscrop"].ToString();
                lsprposedcrop = objODBCDatareader["prposedcrop"].ToString();
                lsinstitution_gid = objODBCDatareader["institution_gid"].ToString();
                lsinstitution_name = objODBCDatareader["institution_name"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tsuprcontact set " +
                        " pan_status='" + values.pan_status + "'," +
                        " pan_no='" + values.pan_no + "'," +
                        " aadhar_no='" + values.aadhar_no + "',";
                if (values.first_name == "" || values.first_name == null)
                {

                }
                else
                {
                    msSQL += " first_name='" + values.first_name.Replace("'", "") + "',";
                }
                if (values.middle_name == "" || values.middle_name == null)
                {

                }
                else
                {
                    msSQL += " middle_name='" + values.middle_name.Replace("'", "") + "',";
                }
                if (values.last_name == "" || values.last_name == null)
                {

                }
                else
                {
                    msSQL += " last_name='" + values.last_name.Replace("'", "") + "',";
                }
                msSQL += " stakeholdertype_gid='" + values.stakeholdertype_gid + "'," +
                             " stakeholder_type='" + values.stakeholder_type + "'," +
                             " individual_dob='" + values.individual_dob + "'," +
                           " age='" + values.age + "'," +
                           " gender_gid='" + values.gender_gid + "'," +
                           " gender_name='" + values.gender_name + "'," +
                           " designation_gid='" + values.designation_gid + "'," +
                           " designation_name='" + values.designation_name + "'," +
                           " educationalqualification_gid='" + values.educationalqualification_gid + "'," +
                           " educationalqualification_name='" + values.educationalqualification_name + "'," +
                           " main_occupation='" + values.main_occupation + "'," +
                           " annual_income='" + values.annual_income + "'," +
                           " monthly_income='" + values.monthly_income + "'," +
                           " pep_status='" + values.pep_status + "',";
                if (Convert.ToDateTime(values.pepverifieddate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " pepverified_date='" + Convert.ToDateTime(values.pepverifieddate).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                msSQL += " user_type='" + values.user_type + "'," +
                         " maritalstatus_gid='" + values.maritalstatus_gid + "'," +
                         " maritalstatus_name='" + values.maritalstatus_name + "',";
                if (values.father_firstname == "" || values.father_firstname == null)
                {

                }
                else
                {
                    msSQL += " father_firstname='" + values.father_firstname.Replace("'", "") + "',";
                }
                if (values.father_middlename == "" || values.father_middlename == null)
                {

                }
                else
                {
                    msSQL += " father_middlename='" + values.father_middlename.Replace("'", "") + "',";
                }
                if (values.father_lastname == "" || values.father_lastname == null)
                {

                }
                else
                {
                    msSQL += " father_lastname='" + values.father_lastname.Replace("'", "") + "',";
                }
                msSQL += " father_dob='" + values.father_dob + "'," +
                         " father_age='" + values.father_age + "',";
                if (values.mother_firstname == "" || values.mother_firstname == null)
                {

                }
                else
                {
                    msSQL += " mother_firstname='" + values.mother_firstname.Replace("'", "") + "',";
                }
                if (values.mother_middlename == "" || values.mother_middlename == null)
                {

                }
                else
                {
                    msSQL += " mother_middlename='" + values.mother_middlename.Replace("'", "") + "',";
                }
                if (values.mother_lastname == "" || values.mother_lastname == null)
                {

                }
                else
                {
                    msSQL += " mother_lastname='" + values.mother_lastname.Replace("'", "") + "',";
                }
                msSQL += " mother_dob='" + values.mother_dob + "'," +
                         " mother_age='" + values.mother_age + "',";
                if (values.spouse_firstname == "" || values.spouse_firstname == null)
                {

                }
                else
                {
                    msSQL += " spouse_firstname='" + values.spouse_firstname.Replace("'", "") + "',";
                }
                if (values.spouse_middlename == "" || values.spouse_middlename == null)
                {

                }
                else
                {
                    msSQL += " spouse_middlename='" + values.spouse_middlename.Replace("'", "") + "',";
                }
                if (values.spouse_lastname == "" || values.spouse_lastname == null)
                {

                }
                else
                {
                    msSQL += " spouse_lastname='" + values.spouse_lastname.Replace("'", "") + "',";
                }
                msSQL += " spouse_dob='" + values.spouse_dob + "'," +
                         " spouse_age='" + values.spouse_age + "'," +
                       " ownershiptype_gid='" + values.ownershiptype_gid + "'," +
                       " ownershiptype_name='" + values.ownershiptype_name + "'," +
                       " propertyholder_gid='" + values.propertyholder_gid + "'," +
                       " propertyholder_name='" + values.propertyholder_name + "'," +
                       " residencetype_gid='" + values.residencetype_gid + "'," +
                       " residencetype_name='" + values.residencetype_name + "'," +
                       " incometype_gid='" + values.incometype_gid + "'," +
                       " incometype_name='" + values.incometype_name + "'," +
                       " currentresidence_years='" + values.currentresidence_years + "'," +
                       " branch_distance='" + values.branch_distance + "'," +
                        " group_gid='" + values.group_gid + "'," +
                       " group_name='" + values.group_name + "'," +
                       " profile='" + values.profile + "'," +
                       " urn_status='" + values.urn_status + "'," +
                       " urn='" + values.urn + "'," +
                       " fathernominee_status='" + values.fathernominee_status + "'," +
                       " mothernominee_status='" + values.mothernominee_status + "'," +
                       " spousenominee_status='" + values.spousenominee_status + "'," +
                       " othernominee_status='" + values.othernominee_status + "'," +
                       " relationshiptype='" + values.relationshiptype + "',";
                if (values.nomineefirst_name == "" || values.nomineefirst_name == null)
                {

                }
                else
                {
                    msSQL += " nomineefirst_name='" + values.nomineefirst_name.Replace("'", "") + "',";
                }
                if (values.nominee_middlename == "" || values.nominee_middlename == null)
                {

                }
                else
                {
                    msSQL += " nominee_middlename='" + values.nominee_middlename.Replace("'", "") + "',";
                }
                if (values.nominee_lastname == "" || values.nominee_lastname == null)
                {

                }
                else
                {
                    msSQL += " nominee_lastname='" + values.nominee_lastname.Replace("'", "") + "',";
                }

                msSQL += " nominee_dob='" + values.nominee_dob + "'," +
                       " nominee_age='" + values.nominee_age + "'," +
                       " totallandinacres='" + values.totallandinacres + "'," +
                       " cultivatedland='" + values.cultivatedland + "'," +
                       " previouscrop='" + values.previouscrop + "'," +
                       " prposedcrop='" + values.prposedcrop + "'," +
                       " institution_gid='" + values.institution_gid + "'," +
                       " institution_name='" + values.institution_name + "'," +
                       " updated_by='" + employee_gid + "'," +
                       " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                       " where contact_gid='" + values.contact_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                objODBCDatareader.Close();

                if (mnResult != 0)
                {
                    if (values.pan_status == "Customer Submitting Form 60")
                    {
                        matchCount1 = 0;
                        matchCount2 = 0;

                        msSQL = " select panabsencereason from agr_mst_tsuprcontact2panabsencereason" +
                               " where contact_gid='" + values.contact_gid + "'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        if (dt_datatable.Rows.Count != 0)
                        {
                            values.contactpanabsencereason_list = dt_datatable.AsEnumerable().Select(row =>
                             new contactpanabsencereason_list
                             {
                                 panabsencereason = row["panabsencereason"].ToString(),
                             }
                           ).ToList();
                        }
                        dt_datatable.Dispose();

                        if (values.contactpanabsencereason_list == null)
                        {
                            foreach (string reason in values.panabsencereason_selectedlist)
                            {
                                msGetGid = objcmnfunctions.GetMasterGID("C2PR");
                                msSQL = " INSERT INTO agr_mst_tsuprcontact2panabsencereason(" +
                                        " contact2panabsencereason_gid," +
                                        " contact_gid," +
                                        " panabsencereason," +
                                        " created_date," +
                                        " created_by)" +
                                        " VALUES(" +
                                        "'" + msGetGid + "'," +
                                        "'" + employee_gid + "'," +
                                        "'" + reason + "'," +
                                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                        "'" + employee_gid + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                        }
                        else
                        {
                            for (var i = 0; i < values.panabsencereason_selectedlist.Count; i++)
                            {
                                for (var j = 0; j < values.contactpanabsencereason_list.Count; j++)
                                {
                                    if (values.panabsencereason_selectedlist[i] == values.contactpanabsencereason_list[j].panabsencereason)
                                    {
                                        matchCount1++;
                                    }
                                }
                                if (matchCount1 == 0)
                                {
                                    msGetGid = objcmnfunctions.GetMasterGID("C2PR");
                                    msSQL = " INSERT INTO agr_mst_tsuprcontact2panabsencereason(" +
                                            " contact2panabsencereason_gid," +
                                            " contact_gid," +
                                            " panabsencereason," +
                                            " created_date," +
                                            " created_by)" +
                                            " VALUES(" +
                                            "'" + msGetGid + "'," +
                                            "'" + employee_gid + "'," +
                                            "'" + values.panabsencereason_selectedlist[i] + "'," +
                                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                            "'" + employee_gid + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                                matchCount1 = 0;
                            }

                            for (var i = 0; i < values.contactpanabsencereason_list.Count; i++)
                            {
                                for (var j = 0; j < values.panabsencereason_selectedlist.Count; j++)
                                {
                                    if (values.contactpanabsencereason_list[i].panabsencereason == values.panabsencereason_selectedlist[j])
                                    {
                                        matchCount2++;
                                    }
                                }
                                if (matchCount2 == 0)
                                {
                                    msSQL = "delete from agr_mst_tsuprcontact2panabsencereason where panabsencereason='" + values.contactpanabsencereason_list[i].panabsencereason + "' and contact_gid = '" + values.contact_gid + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                                matchCount2 = 0;
                            }
                        }
                    }
                    msGetGid = objcmnfunctions.GetMasterGID("CTUL");

                    msSQL = " insert into agr_mst_tsuprcontactupdatelog(" +
                  " contactupdatelog_gid," +
                  " contact_gid," +
                  " application_gid," +
                  " application_no," +
                  " stakeholdertype_gid," +
                  " stakeholder_type," +
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
                  " residencetype_gid," +
                  " residencetype_name," +

                  " propertyholder_gid," +
                  " propertyholder_name," +
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
                       " statusupdated_by," +
                  " created_by," +
                  " created_date)" +
                  " values(" +
                  "'" + msGetGid + "'," +
                  "'" + values.contact_gid + "'," +
                  "'" + values.application_gid + "'," +
                  "'" + values.application_no + "'," +
                  "'" + lsstakeholdertype_gid + "'," +
                  "'" + lsstakeholder_type + "'," +
                  "'" + lspan_status + "'," +
                  "'" + lspan_no + "'," +
                  "'" + lsaadhar_no + "'," +
                  "'" + lsfirst_name + "'," +
                  "'" + lsmiddle_name + "'," +
                  "'" + lslast_name + "'," +
                  "'" + lsindividual_dob + "'," +
                  "'" + lsage + "'," +

                  "'" + lsgender_gid + "'," +
                  "'" + lsgender_name + "'," +
                  "'" + lsdesignation_gid + "'," +
                  "'" + lsdesignation_name + "'," +
                  "'" + lseducationalqualification_gid + "'," +
                  "'" + lseducationalqualification_name + "'," +

                  "'" + lsmain_occupation + "'," +
                  "'" + lsannual_income + "'," +
                  "'" + lsmonthly_income + "'," +
                  "'" + lspep_status + "',";

                    if ((lspepverified_date == null) || (lspepverified_date == ""))
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(lspepverified_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }

                    msSQL += "'" + lsmaritalstatus_gid + "'," +
                      "'" + lsmaritalstatus_name + "'," +

                      "'" + lsfather_firstname + "'," +
                      "'" + lsfather_middlename + "'," +
                      "'" + lsfather_lastname + "'," +
                      "'" + lsfather_dob + "'," +
                      "'" + lsfather_age + "'," +

                       "'" + lsmother_firstname + "'," +
                       "'" + lsmother_middlename + "'," +
                       "'" + lsmother_lastname + "'," +
                       "'" + lsmother_dob + "'," +
                       "'" + lsmother_age + "'," +

                      "'" + lsspouse_firstname + "'," +
                      "'" + lsspouse_middlename + "'," +
                      "'" + lsspouse_lastname + "'," +
                      "'" + lsspouse_dob + "'," +
                      "'" + lsspouse_age + "'," +
                     "'" + lsownershiptype_gid + "'," +
                     "'" + lsownershiptype_name + "'," +
                     "'" + lsresidencetype_gid + "'," +
                     "'" + lsresidencetype_name + "'," +
                     "'" + lspropertyholder_gid + "'," +
                     "'" + lspropertyholder_name + "'," +
                     "'" + lsincometype_gid + "'," +
                     "'" + lsincometype_name + "'," +
                     "'" + lscurrentresidence_years + "'," +
                     "'" + lsbranch_distance + "'," +
                     "'" + lsgroup_gid + "'," +
                         "'" + lsgroup_name + "'," +
                         "'" + lsprofile + "'," +
                         "'" + lsurn_status + "'," +
                         "'" + lsurn + "'," +
                         "'" + lsfathernominee_status + "'," +
                         "'" + lsmothernominee_status + "'," +
                         "'" + lsspousenominee_status + "'," +
                         "'" + lsothernominee_status + "'," +
                         "'" + lsrelationshiptype + "'," +
                       "'" + lsnomineefirst_name + "'," +
                       "'" + lsnominee_middlename + "'," +
                       "'" + lsnominee_lastname + "'," +
                       "'" + lsnominee_dob + "'," +
                             "'" + lsnominee_age + "'," +
                             "'" + lstotallandinacres + "'," +
                             "'" + lscultivatedland + "'," +
                             "'" + lspreviouscrop + "'," +
                             "'" + lsprposedcrop + "'," +
                         "'" + lsinstitution_gid + "'," +
                         "'" + lsinstitution_name + "'," +
                         "'" + values.statusupdated_by + "'," +
                         "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    //Updates
                    msSQL = "update agr_mst_tsuprcontact2mobileno set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsuprcontact2email set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsuprcontact2address set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsuprcontact2idproof set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsuprcontact2panform60 set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsuprcontact2panabsencereason set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "select contact_gid from agr_mst_tsuprcontact2document where contact_gid='" + employee_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        string lscovenant_type = "", lsdocumenttype_gid = "", lsdocumenttype_name = "", lscompanydocument_name = "";

                        string msGetdefDocchecklistGID = objcmnfunctions.GetMasterGID("DOCG");
                        msSQL = " select individualdocument_gid,documenttypes_gid,documenttype_name,individualdocument_name,covenant_type " +
                             " from ocs_mst_tindividualdocument where individualdocument_gid='" + dt["individualdocument_gid"].ToString() + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                            lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                            lscompanydocument_name = objODBCDatareader["individualdocument_name"].ToString();
                            lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                        }
                        objODBCDatareader.Close();
                        msSQL = " insert into agr_trn_tsuprdocumentchecktls(" +
                                " documentcheckdtl_gid," +
                                " application_gid," +
                                " credit_gid, " +
                                " individualdocument_gid, " +
                                 " documentuploaded_gid, " +
                                " documenttype_gid," +
                            " documenttype_code," +
                            " documenttype_name," +
                            " covenant_type, " +
                            " tagged_by, " +
                            " created_date," +
                            " created_by)" +
                            " VALUES(" +
                            "'" + msGetdefDocchecklistGID + "'," +
                            "'" + values.application_gid + "'," +
                            "'" + values.contact_gid + "'," +
                            "'" + dt["individualdocument_gid"].ToString() + "'," +
                            "'" + dt["contact2document_gid"].ToString() + "'," +
                            "'" + lsdocumenttype_gid + "'," +
                            "'" + lsdocumenttype_name + "'," +
                            "'" + lscompanydocument_name.Replace("'", "") + "'," +
                            "'" + lscovenant_type + "'," +
                            "'N'," +
                            "current_timestamp," +
                            "'" + employee_gid + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (lscovenant_type == "Y")
                        {
                            string msGetDocchecklistGID = objcmnfunctions.GetMasterGID("CDCL");
                            msSQL = " insert into agr_trn_tsuprcovanantdocumentcheckdtls(" +
                           " covenantdocumentcheckdtl_gid," +
                           " application_gid," +
                           " credit_gid," +
                           " individualdocument_gid," +
                            " documentuploaded_gid, " +
                           " documenttype_gid," +
                           " documenttype_code," +
                           " documenttype_name," +
                           " covenant_type, " +
                           " tagged_by, " +
                           " created_date," +
                           " created_by)" +
                           " VALUES(" +
                           "'" + msGetDocchecklistGID + "'," +
                           "'" + values.application_gid + "'," +
                           "'" + values.contact_gid + "'," +
                           "'" + dt["individualdocument_gid"].ToString() + "'," +
                           "'" + dt["contact2document_gid"].ToString() + "'," +
                           "'" + lsdocumenttype_gid + "'," +
                           "'" + lsdocumenttype_name + "'," +
                           "'" + lscompanydocument_name.Replace("'", "") + "'," +
                           "'" + lscovenant_type + "'," +
                           "'N'," +
                           "current_timestamp," +
                           "'" + employee_gid + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    dt_datatable.Dispose();

                    DaAgrMstSuprScannedDocument objvalues = new DaAgrMstSuprScannedDocument();
                    objvalues.DaGroupDocChecklistinfo(values.application_gid, values.contact_gid, employee_gid);

                    msSQL = "update agr_mst_tsuprcontact2document set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        msSQL = "select applicant_type from agr_mst_tsuprapplication where application_gid='" + lsapplication_gid + "'";
                        string lsapplicant_type = objdbconn.GetExecuteScalar(msSQL);

                        if (lsapplicant_type == "Individual")
                        {

                            msSQL = "select concat(first_name,middle_name,last_name) as customer_name,mobile_no,email_address,contact_gid from agr_mst_tsuprcontact where" +
                                " application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Applicant','Borrower')";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lscustomer_name = objODBCDatareader["customer_name"].ToString();
                                lsmobile_no = objODBCDatareader["mobile_no"].ToString();
                                lsemail_address = objODBCDatareader["email_address"].ToString();
                                //Region
                                msSQL = "select state from agr_mst_tsuprcontact2address where primary_status='Yes' and contact_gid='" + objODBCDatareader["contact_gid"].ToString() + "'";
                                lsregion = objdbconn.GetExecuteScalar(msSQL);


                                msSQL = " update agr_mst_tsuprapplication set customer_name='" + lscustomer_name + "'," +
                               " mobile_no='" + lsmobile_no + "'," +
                               " email_address='" + lsemail_address + "'," +
                               " region='" + lsregion + "'," +
                               " updated_by='" + employee_gid + "'," +
                               " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                               " where application_gid='" + lsapplication_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            objODBCDatareader.Close();
                            values.status = true;
                            values.message = "Individual Details Updated Successfully";
                        }
                        else
                        {
                        }
                    }
                    values.status = true;
                    values.message = "Individual Details Updated Successfully";
                }
                return true;
            }
            catch
            {
                values.status = false;
                values.message = "Error Occured While Updating Institution";
                return false;
            }
        }

        // Individual Temp Clear 
        public void GetIndividualTempClear(string employee_gid, result values)
        {
            msSQL = "delete from agr_mst_tsuprcontact2mobileno where contact_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprcontact2email where contact_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprcontact2address where contact_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprcontact2idproof where contact_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprcontact2document where contact_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tindividual2cicdocumentupload where contact_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprinstitution2cicdocumentupload where institution_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprcontact2panform60 where contact_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprcontact2panabsencereason where contact_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }

        // General Edit Form

        public bool DaPostAppMobileNo(string employee_gid, MdlMstMobileNo values)
        {
            msSQL = "select primary_mobileno from agr_mst_tsuprapplication2contactno where primary_mobileno='Yes' and (application_gid='" + employee_gid + "' or" +
               " application_gid='" + values.application_gid + "') ";
            string lsprimary_mobileno = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_mobileno == (values.primary_mobileno))
            {
                values.status = false;
                values.message = "Already Primary Mobile Number Added";
                return false;
            }
            msSQL = "select application2contact_gid from agr_mst_tsuprapplication2contactno where mobile_no='" + values.mobile_no + "' " +
                " and (application_gid='" + employee_gid + "' or application_gid='" + values.application_gid + "')";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already This Mobile Number Added";
                return false;
            }
            msGetGid = objcmnfunctions.GetMasterGID("A2CN");
            msSQL = " insert into agr_mst_tsuprapplication2contactno(" +
                    " application2contact_gid," +
                    " application_gid," +
                    " mobile_no," +
                    " primary_mobileno," +
                    " whatsapp_mobileno," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.mobile_no + "'," +
                    "'" + values.primary_mobileno + "'," +
                    "'" + values.whatsapp_mobileno + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Mobile Number Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Adding Mobile Number";
                return false;
            }
        }

        public void DaGetAppMobileNoTempList(string application_gid, string employee_gid, MdlMstMobileNo values)
        {
            msSQL = "select mobile_no,application2contact_gid,primary_mobileno,whatsapp_mobileno from agr_mst_tsuprapplication2contactno where " +
              " application_gid='" + employee_gid + "' or application_gid = '" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstmobileno_list = new List<mstmobileno_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstmobileno_list.Add(new mstmobileno_list
                    {
                        application2contact_gid = (dr_datarow["application2contact_gid"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_mobileno = (dr_datarow["primary_mobileno"].ToString()),
                        whatsapp_mobileno = (dr_datarow["whatsapp_mobileno"].ToString()),
                    });
                }
                values.mstmobileno_list = getmstmobileno_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGetAppMobileNoList(string application_gid, string employee_gid, MdlMstMobileNo values)
        {
            msSQL = "select mobile_no,application2contact_gid,primary_mobileno,whatsapp_mobileno from agr_mst_tsuprapplication2contactno where " +
              " application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstmobileno_list = new List<mstmobileno_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstmobileno_list.Add(new mstmobileno_list
                    {
                        application2contact_gid = (dr_datarow["application2contact_gid"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_mobileno = (dr_datarow["primary_mobileno"].ToString()),
                        whatsapp_mobileno = (dr_datarow["whatsapp_mobileno"].ToString()),
                    });
                }
                values.mstmobileno_list = getmstmobileno_list;
            }
            dt_datatable.Dispose();
        }

        public void DaEditAppMobileNo(string application2contact_gid, MdlMstMobileNo values)
        {
            try
            {
                msSQL = " select mobile_no,application2contact_gid,primary_mobileno,whatsapp_mobileno from agr_mst_tsuprapplication2contactno where " +
                        " application2contact_gid='" + application2contact_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.mobile_no = objODBCDatareader["mobile_no"].ToString();
                    values.primary_mobileno = objODBCDatareader["primary_mobileno"].ToString();
                    values.whatsapp_mobileno = objODBCDatareader["whatsapp_mobileno"].ToString();
                    values.application2contact_gid = objODBCDatareader["application2contact_gid"].ToString();
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

        public void DaUpdateAppMobileNo(string employee_gid, MdlMstMobileNo values)
        {
            msSQL = " select mobile_no,application2contact_gid,primary_mobileno,whatsapp_mobileno from agr_mst_tsuprapplication2contactno where " +
                    " application2contact_gid='" + values.application2contact_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsmobile_no = objODBCDatareader["mobile_no"].ToString();
                lsprimary_mobileno = objODBCDatareader["primary_mobileno"].ToString();
                lswhatsapp_mobileno = objODBCDatareader["whatsapp_mobileno"].ToString();
                lsapplication2contact_gid = objODBCDatareader["application2contact_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tsuprapplication2contactno set " +
                         " mobile_no='" + values.mobile_no + "'," +
                         " primary_mobileno='" + values.primary_mobileno + "'," +
                         " whatsapp_mobileno='" + values.whatsapp_mobileno + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where application2contact_gid='" + values.application2contact_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("AMUL");

                    msSQL = "Insert into agr_mst_tsuprapplication2contactnoupdatelog(" +
                   " application2contactnoupdatelog_gid, " +
                   " application2contact_gid, " +
                   " application_gid, " +
                   " mobile_no," +
                   " primary_mobileno," +
                   " whatsapp_mobileno," +
                   " statusupdated_by," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + values.application2contact_gid + "'," +
                   "'" + values.application_gid + "'," +
                   "'" + lsmobile_no + "'," +
                   "'" + lsprimary_mobileno + "'," +
                   "'" + lswhatsapp_mobileno + "'," +
                   "'" + values.statusupdated_by + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Mobile Number Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured While Upating Mobile Number";
            }
        }

        public void DaDeleteAppMobileNo(string application2contact_gid, MdlMstMobileNo values)
        {
            msSQL = "delete from agr_mst_tsuprapplication2contactno where application2contact_gid='" + application2contact_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tsuprapplication2contactnoupdatelog where application2contact_gid='" + application2contact_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Mobile Number Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting Mobile Number";
                values.status = false;
            }
        }

        public bool DaPostAppEmailAddress(string employee_gid, MdlMstEmailAddress values)
        {
            msSQL = "select primary_emailaddress from agr_mst_tsuprapplication2email where primary_emailaddress='Yes' and (application_gid='" + employee_gid + "' or application_gid='" + values.application_gid + "')";
            string lsprimary_emailaddress = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_emailaddress == (values.primary_emailaddress))
            {
                values.status = false;
                values.message = "Already Primary Email Address Added";
                return false;
            }
            msSQL = "select application2email_gid from agr_mst_tsuprapplication2email where email_address='" + values.email_address + "' and (application_gid='" + employee_gid + "' or application_gid='" + values.application_gid + "')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already This Email Address Added";
                return false;
            }
            msGetGid = objcmnfunctions.GetMasterGID("A2EA");
            msSQL = " insert into agr_mst_tsuprapplication2email(" +
                    " application2email_gid," +
                    " application_gid," +
                    " email_address," +
                    " primary_emailaddress," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.email_address + "'," +
                    "'" + values.primary_emailaddress + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Email Address Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Adding Email Address";
                return false;
            }
        }

        public void DaGetAppEmailAddressTempList(string application_gid, string employee_gid, MdlMstEmailAddress values)
        {
            msSQL = "select email_address,application2email_gid,primary_emailaddress, application_gid from agr_mst_tsuprapplication2email where " +
              " application_gid='" + employee_gid + "' or application_gid = '" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstemailaddress_list = new List<mstemailaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstemailaddress_list.Add(new mstemailaddress_list
                    {
                        application2email_gid = (dr_datarow["application2email_gid"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        email_address = (dr_datarow["email_address"].ToString()),
                        primary_emailaddress = (dr_datarow["primary_emailaddress"].ToString())
                    });
                }
                values.mstemailaddress_list = getmstemailaddress_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetAppEmailAddressList(string application_gid, string employee_gid, MdlMstEmailAddress values)
        {
            msSQL = "select email_address,application2email_gid,primary_emailaddress, application_gid from agr_mst_tsuprapplication2email where " +
              " application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstemailaddress_list = new List<mstemailaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstemailaddress_list.Add(new mstemailaddress_list
                    {
                        application2email_gid = (dr_datarow["application2email_gid"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        email_address = (dr_datarow["email_address"].ToString()),
                        primary_emailaddress = (dr_datarow["primary_emailaddress"].ToString())
                    });
                }
                values.mstemailaddress_list = getmstemailaddress_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaEditAppEmailAddress(string application2email_gid, MdlMstEmailAddress values)
        {
            try
            {
                msSQL = " select email_address,application2email_gid,primary_emailaddress from agr_mst_tsuprapplication2email where " +
                        " application2email_gid='" + application2email_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.email_address = objODBCDatareader["email_address"].ToString();
                    values.primary_emailaddress = objODBCDatareader["primary_emailaddress"].ToString();
                    values.application2email_gid = objODBCDatareader["application2email_gid"].ToString();
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

        public void DaUpdateAppEmailAddress(string employee_gid, MdlMstEmailAddress values)
        {
            msSQL = " select email_address,application2email_gid,primary_emailaddress, application_gid from agr_mst_tsuprapplication2email where " +
                        " application2email_gid='" + values.application2email_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsemail_address = objODBCDatareader["email_address"].ToString();
                lsprimary_emailaddress = objODBCDatareader["primary_emailaddress"].ToString();
                lsapplication2email_gid = objODBCDatareader["application2email_gid"].ToString();
                lsapplication_gid = objODBCDatareader["application_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tsuprapplication2email set " +
                         " email_address='" + values.email_address + "'," +
                         " primary_emailaddress='" + values.primary_emailaddress + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where application2email_gid='" + values.application2email_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("AEUL");

                    msSQL = "Insert into agr_mst_tsuprapplication2emailupdatelog(" +
                   " application2emailupdatelog_gid, " +
                   " application2email_gid, " +
                   " application_gid, " +
                   " email_address," +
                   " primary_emailaddress," +
                   " statusupdated_by," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + values.application2email_gid + "'," +
                   "'" + values.application_gid + "'," +
                   "'" + lsemail_address + "'," +
                   "'" + lsprimary_emailaddress + "'," +
                   "'" + values.statusupdated_by + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Email Address Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured While Updating Email Address";
            }
        }

        public void DaDeleteAppEmailAddress(string application2email_gid, MdlMstEmailAddress values)
        {
            msSQL = "delete from agr_mst_tsuprapplication2email where application2email_gid='" + application2email_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tsuprapplication2emailupdatelog where application2email_gid='" + application2email_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Email Address Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting Email Address";
                values.status = false;
            }
        }

        public void DaPostAppGeneticCode(string employee_gid, MdlMstGeneticCode values)
        {
            msSQL = "select geneticcode_gid from agr_mst_tsuprapplication2geneticcode where (application_gid='" + employee_gid + "' or " +
                " application_gid='" + values.application_gid + "') and geneticcode_gid='" + values.geneticcode_gid + "'";
            string lsgenetic_code = objdbconn.GetExecuteScalar(msSQL);
            if (lsgenetic_code == (values.geneticcode_gid))
            {
                values.status = false;
                values.message = "Already Genetic Code Added";
                return;
            }
            msGetGid = objcmnfunctions.GetMasterGID("A2GC");
            msSQL = " insert into agr_mst_tsuprapplication2geneticcode(" +
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
                   "'" + employee_gid + "'," +
                   "'" + values.geneticcode_gid + "'," +
                   "'" + values.geneticcode_name.Replace("'", " ") + "'," +
                   "'" + values.genetic_status + "'," +
                   "'" + values.genetic_remarks.Replace("'", " ") + "'," +
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

        public void DaGetAppGeneticCodeTempList(string application_gid, string employee_gid, MdlMstGeneticCode values)
        {
            msSQL = " select application2geneticcode_gid,geneticcode_gid,geneticcode_name,genetic_status,genetic_remarks,application_gid" +
                      " from agr_mst_tsuprapplication2geneticcode where " +
                      " application_gid='" + employee_gid + "' or application_gid = '" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getgeneticcode_list = new List<mstgeneticcode_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getgeneticcode_list.Add(new mstgeneticcode_list
                    {
                        application2geneticcode_gid = (dr_datarow["application2geneticcode_gid"].ToString()),
                        geneticcode_gid = (dr_datarow["geneticcode_gid"].ToString()),
                        geneticcode_name = (dr_datarow["geneticcode_name"].ToString()),
                        genetic_status = (dr_datarow["genetic_status"].ToString()),
                        genetic_remarks = (dr_datarow["genetic_remarks"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                    });
                }
                values.mstgeneticcode_list = getgeneticcode_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGetAppGeneticCodeList(string application_gid, string employee_gid, MdlMstGeneticCode values)
        {
            msSQL = " select application2geneticcode_gid,geneticcode_gid,geneticcode_name,genetic_status,genetic_remarks,application_gid " +
                      " from agr_mst_tsuprapplication2geneticcode where " +
                      " application_gid='" + application_gid + "' or application_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getgeneticcode_list = new List<mstgeneticcode_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getgeneticcode_list.Add(new mstgeneticcode_list
                    {
                        application2geneticcode_gid = (dr_datarow["application2geneticcode_gid"].ToString()),
                        geneticcode_gid = (dr_datarow["geneticcode_gid"].ToString()),
                        geneticcode_name = (dr_datarow["geneticcode_name"].ToString()),
                        genetic_status = (dr_datarow["genetic_status"].ToString()),
                        genetic_remarks = (dr_datarow["genetic_remarks"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                    });
                }
                values.mstgeneticcode_list = getgeneticcode_list;
            }
            dt_datatable.Dispose();
        }

        public void DaEditAppGeneticCode(string application2geneticcode_gid, MdlMstGeneticCode values)
        {
            try
            {
                msSQL = " select application2geneticcode_gid,geneticcode_gid,geneticcode_name,genetic_status,genetic_remarks from agr_mst_tsuprapplication2geneticcode where " +
                        " application2geneticcode_gid='" + application2geneticcode_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application2geneticcode_gid = objODBCDatareader["application2geneticcode_gid"].ToString();
                    values.geneticcode_gid = objODBCDatareader["geneticcode_gid"].ToString();
                    values.geneticcode_name = objODBCDatareader["geneticcode_name"].ToString();
                    values.genetic_status = objODBCDatareader["genetic_status"].ToString();
                    values.genetic_remarks = objODBCDatareader["genetic_remarks"].ToString();
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

        public void DaUpdateAppGeneticCode(string employee_gid, MdlMstGeneticCode values)
        {
            msSQL = " select application2geneticcode_gid,geneticcode_gid,geneticcode_name,genetic_status,genetic_remarks from agr_mst_tsuprapplication2geneticcode where " +
                        " application2geneticcode_gid='" + values.application2geneticcode_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsapplication2geneticcode_gid = objODBCDatareader["application2geneticcode_gid"].ToString();
                lsgeneticcode_gid = objODBCDatareader["geneticcode_gid"].ToString();
                lsgeneticcode_name = objODBCDatareader["geneticcode_name"].ToString();
                lsgenetic_status = objODBCDatareader["genetic_status"].ToString();
                lsgenetic_remarks = objODBCDatareader["genetic_remarks"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " update agr_mst_tsuprapplication2geneticcode set " +
                     " genetic_status='" + values.genetic_status + "'," +
                     " genetic_remarks='" + values.genetic_remarks.Replace("'", " ") + "'," +
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where application2geneticcode_gid='" + values.application2geneticcode_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                msGetGid = objcmnfunctions.GetMasterGID("AGUL");

                msSQL = "Insert into agr_mst_tsuprapplication2geneticcodeupdatelog(" +
               " application2geneticcodeupdatelog_gid, " +
               " application2geneticcode_gid, " +
               " application_gid, " +
               " geneticcode_gid, " +
               " geneticcode_name," +
               " genetic_status," +
               " genetic_remarks," +
               " statusupdated_by," +
               " created_by," +
               " created_date)" +
               " values (" +
               "'" + msGetGid + "'," +
               "'" + values.application2geneticcode_gid + "'," +
               "'" + values.application_gid + "'," +
               "'" + lsgeneticcode_gid + "'," +
               "'" + lsgeneticcode_name + "'," +
               "'" + lsgenetic_status + "'," +
               "'" + lsgenetic_remarks + "'," +
               "'" + values.statusupdated_by + "'," +
               "'" + employee_gid + "'," +
               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Genetic Code Details Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating Genetic Code";
            }
        }

        public void DaDeleteAppGeneticCode(string application2geneticcode_gid, MdlMstGeneticCode values, string employee_gid)
        {
            msSQL = "delete from agr_mst_tsuprapplication2geneticcode where application2geneticcode_gid='" + application2geneticcode_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tsuprapplication2geneticcodeupdatelog where application2geneticcode_gid='" + application2geneticcode_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Genetic Code Details Deleted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Deleting Genetic Code";
            }
        }

        public void DaEditAppBasicDetail(string application_gid, MdlMstApplicationAdd values)
        {
            try
            {
                msSQL = " select application_gid,customer_urn,customerref_name as customer_name,vertical_gid,vertical_name,verticaltaggs_gid,verticaltaggs_name," +
                        " constitution_gid,constitution_name,businessunit_gid,businessunit_name,sa_status,sa_id,sa_name,saname_gid,vernacularlanguage_gid," +
                        " vernacular_language,contactpersonfirst_name,contactpersonmiddle_name,contactpersonlast_name,designation_gid,designation_type,landline_no,creditgroup_gid,creditgroup_name," +
                        " program_gid,program_name,product_gid,product_name,variety_gid,variety_name,sector_name,category_name,botanical_name,alternative_name from agr_mst_tsuprapplication where application_gid='" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                    values.customer_name = objODBCDatareader["customer_name"].ToString();
                    values.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                    values.vertical_name = objODBCDatareader["vertical_name"].ToString();
                    //values.verticaltaggs_gid = objODBCDatareader["verticaltaggs_gid"].ToString();
                    //values.verticaltaggs_name = objODBCDatareader["verticaltaggs_name"].ToString();
                    values.constitution_gid = objODBCDatareader["constitution_gid"].ToString();
                    values.constitution_name = objODBCDatareader["constitution_name"].ToString();
                    values.businessunit_gid = objODBCDatareader["businessunit_gid"].ToString();
                    values.businessunit_name = objODBCDatareader["businessunit_name"].ToString();
                    values.sa_status = objODBCDatareader["sa_status"].ToString();
                    values.saname_gid = objODBCDatareader["saname_gid"].ToString();
                    values.sa_name = objODBCDatareader["sa_name"].ToString();

                    String[] verlanggid_list = objODBCDatareader["vernacularlanguage_gid"].ToString().Split(',');
                    String[] verlangname_list = objODBCDatareader["vernacular_language"].ToString().Split(',');

                    var getvernacularLanguageList = new List<vernacularlanguage_list>();

                    for (var i = 0; i < verlanggid_list.Length; i++)
                    {
                        getvernacularLanguageList.Add(new vernacularlanguage_list
                        {
                            vernacularlanguage_gid = verlanggid_list[i],
                            vernacular_language = verlangname_list[i],
                        });

                    }
                    values.vernacularlanguage_list = getvernacularLanguageList;

                    values.contactpersonfirst_name = objODBCDatareader["contactpersonfirst_name"].ToString();
                    values.contactpersonmiddle_name = objODBCDatareader["contactpersonmiddle_name"].ToString();
                    values.contactpersonlast_name = objODBCDatareader["contactpersonlast_name"].ToString();
                    values.designation_gid = objODBCDatareader["designation_gid"].ToString();
                    values.designation_type = objODBCDatareader["designation_type"].ToString();
                    values.landline_no = objODBCDatareader["landline_no"].ToString();
                    values.creditgroup_gid = objODBCDatareader["creditgroup_gid"].ToString();
                    values.creditgroup_name = objODBCDatareader["creditgroup_name"].ToString();
                    values.program_gid = objODBCDatareader["program_gid"].ToString();
                    values.program_name = objODBCDatareader["program_name"].ToString();
                    values.product_gid = objODBCDatareader["product_gid"].ToString();
                    values.product_name = objODBCDatareader["product_name"].ToString();
                    values.variety_gid = objODBCDatareader["variety_gid"].ToString();
                    values.variety_name = objODBCDatareader["variety_name"].ToString();
                    values.sector_name = objODBCDatareader["sector_name"].ToString();
                    values.category_name = objODBCDatareader["category_name"].ToString();
                    values.botanical_name = objODBCDatareader["botanical_name"].ToString();
                    values.alternative_name = objODBCDatareader["alternative_name"].ToString();

                }

                //Value Chain
                msSQL = " SELECT valuechain_gid,valuechain_name from ocs_mst_tvaluechain a" +
                        " where status_log='Y' order by valuechain_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getvaluechain = new List<valuechainlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getvaluechain.Add(new valuechainlist
                        {
                            valuechain_gid = (dr_datarow["valuechain_gid"].ToString()),
                            valuechain_name = (dr_datarow["valuechain_name"].ToString()),
                        });
                    }
                    values.valuechainlist = getvaluechain;
                }
                dt_datatable.Dispose();

                //Vernacular Language
                msSQL = " SELECT vernacularlanguage_gid,vernacular_language FROM ocs_mst_tvernacularlanguage a" +
                          " where status='Y' order by a.vernacularlanguage_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getvernacularlang_list = new List<vernacularlang_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getvernacularlang_list.Add(new vernacularlang_list
                        {
                            vernacularlanguage_gid = (dr_datarow["vernacularlanguage_gid"].ToString()),
                            vernacular_language = (dr_datarow["vernacular_language"].ToString()),
                        });
                    }
                    values.vernacularlang_list = getvernacularlang_list;
                }
                dt_datatable.Dispose();
                //     msSQL = "select primaryvaluechain_gid,primaryvaluechain_name from agr_mst_tsuprapplication2primaryvaluechain where application_gid='" + application_gid + "'";
                //     dt_datatable = objdbconn.GetDataTable(msSQL);

                //     values.primaryvaluechain_list = dt_datatable.AsEnumerable().Select(row =>
                //new primaryvaluechain_list
                //{
                //    valuechain_gid = row["primaryvaluechain_gid"].ToString(),
                //    valuechain_name = row["primaryvaluechain_name"].ToString()
                //}).ToList();
                //     dt_datatable.Dispose();

                //     msSQL = "select secondaryvaluechain_gid,secondaryvaluechain_name from agr_mst_tsuprapplication2secondaryvaluechain where application_gid='" + application_gid + "'";
                //     dt_datatable = objdbconn.GetDataTable(msSQL);

                //     values.secondaryvaluechain_list = dt_datatable.AsEnumerable().Select(row =>
                //  new secondaryvaluechain_list
                //  {
                //      valuechain_gid = row["secondaryvaluechain_gid"].ToString(),
                //      valuechain_name = row["secondaryvaluechain_name"].ToString()
                //  }).ToList();
                //     dt_datatable.Dispose();


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

        public void DaUpdateAppBasicDetail(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = " select application_gid from agr_trn_tsuprAppcreditapproval where application_gid='" + values.application_gid + "' and hierary_level<>'0'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                string lscreditgroup_gid = objdbconn.GetExecuteScalar("select creditgroup_gid from agr_mst_tsuprapplication where application_gid='" + values.application_gid + "'");

                if (lscreditgroup_gid != values.creditgroup_gid)
                {
                    values.status = false;
                    values.message = "Already Approval Initiated.. You Can't Change the Credit Group";
                    return;

                }
            }
            else
            {
                objODBCDatareader.Close();
            }
            msSQL = "select count(*) from ocs_mst_tgeneticcode where status='Y'";
            string lsmastercount = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(*) from agr_mst_tsuprapplication2geneticcode where application_gid='" + employee_gid + "' or application_gid='" + values.application_gid + "'";
            string lsgeneticcount = objdbconn.GetExecuteScalar(msSQL);
            if (lsmastercount == lsgeneticcount)
            {
                msSQL = "select application_gid from agr_mst_tsuprapplication2contactno where (application_gid='" + employee_gid + "' or application_gid='" + values.application_gid + "')" +
                  " and primary_mobileno='Yes'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Kindly Add Primary Mobile Number ";
                    return;
                }


                msSQL = "select application_gid from agr_mst_tsuprapplication2email where (application_gid='" + employee_gid + "' or application_gid='" + values.application_gid + "')" +
                     " and primary_emailaddress='Yes'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Kindly Add Primary Email Adddress";
                    return;
                }

                string gsvernacularlanguage_gid = string.Empty;
                string gsvernacular_language = string.Empty;

                for (var i = 0; i < values.vernacularlanguage_list.Count; i++)
                {
                    gsvernacularlanguage_gid += values.vernacularlanguage_list[i].vernacularlanguage_gid + ",";
                    gsvernacular_language += values.vernacularlanguage_list[i].vernacular_language + ",";

                }
                gsvernacularlanguage_gid = gsvernacularlanguage_gid.TrimEnd(',');
                gsvernacular_language = gsvernacular_language.TrimEnd(',');

                msSQL = " select application_gid,customer_urn,customerref_name as customer_name,vertical_gid,vertical_name,verticaltaggs_gid,verticaltaggs_name," +
                             " constitution_gid,constitution_name,businessunit_gid,businessunit_name,sa_status,sa_id,sa_name,vernacularlanguage_gid,application_no," +
                             " vernacular_language,contactpersonfirst_name,contactpersonmiddle_name,contactpersonlast_name,designation_gid,designation_type,landline_no,creditgroup_gid,creditgroup_name, " +
                             " program_gid,program_name,product_gid,product_name,variety_gid,variety_name,sector_name,category_name, botanical_name, alternative_name from agr_mst_tsuprapplication " +
                             " where application_gid='" + values.application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsapplication_no = objODBCDatareader["application_no"].ToString();
                    lscustomer_urn = objODBCDatareader["customer_urn"].ToString();
                    lscustomer_name = objODBCDatareader["customer_name"].ToString();
                    lsvertical_gid = objODBCDatareader["vertical_gid"].ToString();
                    lsvertical_name = objODBCDatareader["vertical_name"].ToString();
                    //lsverticaltaggs_gid = objODBCDatareader["verticaltaggs_gid"].ToString();
                    //lsverticaltaggs_name = objODBCDatareader["verticaltaggs_name"].ToString();
                    lsconstitution_gid = objODBCDatareader["constitution_gid"].ToString();
                    lsconstitution_name = objODBCDatareader["constitution_name"].ToString();
                    lsbusinessunit_gid = objODBCDatareader["businessunit_gid"].ToString();
                    lsbusinessunit_name = objODBCDatareader["businessunit_name"].ToString();
                    lssa_status = objODBCDatareader["sa_status"].ToString();
                    lssa_name = objODBCDatareader["sa_name"].ToString();
                    lsvernacularlanguage_gid = objODBCDatareader["vernacularlanguage_gid"].ToString();
                    lsvernacular_language = objODBCDatareader["vernacular_language"].ToString();
                    lscontactpersonfirst_name = objODBCDatareader["contactpersonfirst_name"].ToString();
                    lscontactpersonmiddle_name = objODBCDatareader["contactpersonmiddle_name"].ToString();
                    lscontactpersonlast_name = objODBCDatareader["contactpersonlast_name"].ToString();
                    lsdesignation_gid = objODBCDatareader["designation_gid"].ToString();
                    lsdesignation_type = objODBCDatareader["designation_type"].ToString();
                    lslandline_no = objODBCDatareader["landline_no"].ToString();
                    lscreditgroup_gid = objODBCDatareader["creditgroup_gid"].ToString();
                    lscreditgroup_name = objODBCDatareader["creditgroup_name"].ToString();
                    lsprogram_gid = objODBCDatareader["program_gid"].ToString();
                    lsprogram_name = objODBCDatareader["program_name"].ToString();
                    lsproduct_gid = objODBCDatareader["product_gid"].ToString();
                    lsproduct_name = objODBCDatareader["product_name"].ToString();
                    lsvariety_gid = objODBCDatareader["variety_gid"].ToString();
                    lsvariety_name = objODBCDatareader["variety_name"].ToString();
                    lssector_name = objODBCDatareader["sector_name"].ToString();
                    lscategory_name = objODBCDatareader["category_name"].ToString();
                    lsbotanical_name = objODBCDatareader["botanical_name"].ToString();
                    lsalternative_name = objODBCDatareader["alternative_name"].ToString();
                }
                objODBCDatareader.Close();
                try
                {
                    if (values.vertical_gid == lsvertical_gid)
                    {
                    }
                    else
                    {
                        msSQL = "select vertical_refno from ocs_mst_tvertical where vertical_gid='" + values.vertical_gid + "'";
                        string lsvertical_refno = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select entity_gid from ocs_mst_tvertical where vertical_gid='" + values.vertical_gid + "'";
                        string lsentity_gid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select entity_code from adm_mst_tentity where entity_gid='" + lsentity_gid + "'";
                        string lsentity_code = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select substring('" + lsapplication_no + "',8,16)";
                        string lsrefno = objdbconn.GetExecuteScalar(msSQL);
                        lsapplication_no = "ARN" + lsentity_code + lsvertical_refno + lsrefno;

                    }

                    msSQL = " update agr_mst_tsuprapplication set " +
                             " application_no='" + lsapplication_no + "'," +
                             " customer_urn='" + values.customer_urn + "'," +
                             " customerref_name='" + values.customer_name + "'," +
                             " vertical_gid='" + values.vertical_gid + "'," +
                             " vertical_name='" + values.vertical_name + "'," +
                             //" verticaltaggs_gid='" + values.verticaltaggs_gid + "'," +
                             //" verticaltaggs_name='" + values.verticaltaggs_name + "'," +
                             " constitution_gid='" + values.constitution_gid + "'," +
                             " constitution_name='" + values.constitution_name + "'," +
                             " businessunit_gid='" + values.businessunit_gid + "'," +
                             " businessunit_name='" + values.businessunit_name + "'," +
                             " sa_status='" + values.sa_status + "'," +
                             " saname_gid='" + values.saname_gid + "'," +
                             " sa_name='" + values.sa_name + "'," +
                             " vernacularlanguage_gid='" + gsvernacularlanguage_gid + "'," +
                             " vernacular_language='" + gsvernacular_language + "'," +
                             " contactpersonfirst_name='" + values.contactpersonfirst_name + "'," +
                             " contactpersonmiddle_name='" + values.contactpersonmiddle_name + "'," +
                             " contactpersonlast_name='" + values.contactpersonlast_name + "'," +
                             " designation_gid='" + values.designation_gid + "'," +
                             " designation_type='" + values.designation_type + "'," +
                             " landline_no='" + values.landline_no + "'," +
                             " creditgroup_gid='" + values.creditgroup_gid + "'," +
                             " creditgroup_name='" + values.creditgroup_name + "'," +
                             " program_gid='" + values.program_gid + "'," +
                             " program_name='" + values.program_name + "'," +
                             " product_gid= '" + values.product_gid + "'," +
                             " product_name='" + values.product_name + "'," +
                             " variety_gid= '" + values.variety_gid + "'," +
                             " variety_name='" + values.variety_name + "'," +
                             " sector_name= '" + values.sector_name + "'," +
                             " category_name='" + values.category_name + "'," +
                             " botanical_name= '" + values.botanical_name + "'," +
                             " alternative_name='" + values.alternative_name + "'," +
                             " status = 'Completed'," +
                             " updated_by='" + employee_gid + "'," +
                             " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                             " where application_gid='" + values.application_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult == 1)
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("ABUL");

                        msSQL = "Insert into agr_mst_tsuprapplicationbasicdetailsupdatelog(" +
                       " applicationbasicdetailsupdatelog_gid, " +
                       " application_gid, " +
                       " customer_urn, " +
                       " customer_name, " +
                       " vertical_gid, " +
                       " vertical_name," +
                       //" verticaltaggs_gid," +
                       //" verticaltaggs_name," +
                       " constitution_gid," +
                       " constitution_name," +
                       " businessunit_gid," +
                       " businessunit_name," +
                       " sa_status," +
                       " vernacularlanguage_gid," +
                       " vernacularlanguage_name," +
                       " contactpersonfirst_name," +
                       " contactpersonmiddle_name," +
                       " contactpersonlast_name," +
                       " designation_gid," +
                       " designation_type," +
                       " landline_no," +
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
                       " statusupdated_by," +
                       " created_by," +
                       " created_date)" +
                       " values (" +
                       "'" + msGetGid + "'," +
                       "'" + values.application_gid + "'," +
                       "'" + lscustomer_urn + "'," +
                       "'" + lscustomer_name + "'," +
                       "'" + lsvertical_gid + "'," +
                       "'" + lsvertical_name + "'," +
                       //"'" + lsverticaltaggs_gid + "'," +
                       //"'" + lsverticaltaggs_name + "'," +
                       "'" + lsconstitution_gid + "'," +
                       "'" + lsconstitution_name + "'," +
                       "'" + lsbusinessunit_gid + "'," +
                       "'" + lsbusinessunit_name + "'," +
                       "'" + lssa_status + "'," +
                       "'" + lsvernacularlanguage_gid + "'," +
                       "'" + lsvernacular_language + "'," +
                       "'" + lscontactpersonfirst_name + "'," +
                       "'" + lscontactpersonmiddle_name + "'," +
                       "'" + lscontactpersonlast_name + "'," +
                       "'" + lsdesignation_gid + "'," +
                       "'" + lsdesignation_type + "'," +
                       "'" + lslandline_no + "'," +
                       "'" + lscreditgroup_gid + "'," +
                       "'" + lscreditgroup_name + "'," +
                       "'" + lsprogram_gid + "'," +
                       "'" + lsprogram_name + "'," +
                       "'" + lsproduct_gid + "'," +
                       "'" + lsproduct_name + "'," +
                       "'" + lsvariety_gid + "'," +
                       "'" + lsvariety_name + "'," +
                       "'" + lssector_name + "'," +
                       "'" + lscategory_name + "'," +
                       "'" + lsbotanical_name + "'," +
                       "'" + lsalternative_name + "'," +
                       "'" + values.statusupdated_by + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        //msSQL = "select primaryvaluechain_gid,primaryvaluechain_name from agr_mst_tsuprapplication2primaryvaluechain where application_gid='" + values.application_gid + "'";
                        //dt_datatable = objdbconn.GetDataTable(msSQL);
                        //List<primaryvaluechain_list> existingprimaryvaluechain_list = new List<primaryvaluechain_list>();
                        //if (dt_datatable.Rows.Count != 0)
                        //{
                        //    foreach (DataRow dr_datarow in dt_datatable.Rows)
                        //    {
                        //        existingprimaryvaluechain_list.Add(new primaryvaluechain_list
                        //        {
                        //            valuechain_gid = dr_datarow["primaryvaluechain_gid"].ToString(),
                        //            valuechain_name = dr_datarow["primaryvaluechain_name"].ToString(),
                        //        });
                        //    }
                        //}

                        //for (var i = 0; i < values.primaryvaluechain_list.Count; i++)
                        //{

                        //    if (existingprimaryvaluechain_list.Contains(values.primaryvaluechain_list[i]) == false)
                        //    {
                        //        msGetGid1 = objcmnfunctions.GetMasterGID("CPRI");
                        //        msSQL = " insert into agr_mst_tsuprapplication2primaryvaluechain(" +
                        //                " application2primaryvaluechain_gid," +
                        //                " application_gid," +
                        //                " primaryvaluechain_name," +
                        //                " primaryvaluechain_gid," +
                        //                " created_by," +
                        //                " created_date)" +
                        //                " values(" +
                        //                "'" + msGetGid1 + "'," +
                        //                "'" + values.application_gid + "'," +
                        //                "'" + values.primaryvaluechain_list[i].valuechain_name + "'," +
                        //                "'" + values.primaryvaluechain_list[i].valuechain_gid + "'," +
                        //                "'" + employee_gid + "'," +
                        //                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        //    }

                        //}

                        //for (var i = 0; i < existingprimaryvaluechain_list.Count; i++)
                        //{
                        //    if (values.primaryvaluechain_list.Contains(existingprimaryvaluechain_list[i]) == false)
                        //    {
                        //        msSQL = "select application2primaryvaluechain_gid from agr_mst_tsuprapplication2primaryvaluechain where primaryvaluechain_gid='" + existingprimaryvaluechain_list[i].valuechain_gid + "' and application_gid = '" + values.application_gid + "'";
                        //        string lsapplication2primaryvaluechain_gid = objdbconn.GetExecuteScalar(msSQL);

                        //        msSQL = "delete from agr_mst_tsuprapplication2primaryvaluechain where application2primaryvaluechain_gid='" + lsapplication2primaryvaluechain_gid + "'";
                        //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        //    }
                        //}

                        ////Secondary Value Chain

                        //msSQL = "select secondaryvaluechain_gid,secondaryvaluechain_name from agr_mst_tsuprapplication2secondaryvaluechain where application_gid='" + values.application_gid + "'";
                        //dt_datatable = objdbconn.GetDataTable(msSQL);
                        //List<secondaryvaluechain_list> existingsecondaryvaluechain_list = new List<secondaryvaluechain_list>();
                        //if (dt_datatable.Rows.Count != 0)
                        //{
                        //    foreach (DataRow dr_datarow in dt_datatable.Rows)
                        //    {
                        //        existingsecondaryvaluechain_list.Add(new secondaryvaluechain_list
                        //        {
                        //            valuechain_gid = dr_datarow["secondaryvaluechain_gid"].ToString(),
                        //            valuechain_name = dr_datarow["secondaryvaluechain_name"].ToString(),
                        //        });
                        //    }
                        //}
                        //if (values.secondaryvaluechain_list == null)
                        //{

                        //}
                        //else
                        //{
                        //    for (var i = 0; i < values.secondaryvaluechain_list.Count; i++)
                        //    {

                        //        if (existingsecondaryvaluechain_list.Contains(values.secondaryvaluechain_list[i]) == false)
                        //        {
                        //            msGetGid1 = objcmnfunctions.GetMasterGID("CSEC");
                        //            msSQL = " insert into agr_mst_tsuprapplication2secondaryvaluechain(" +
                        //                    " application2secondaryvaluechain_gid," +
                        //                    " application_gid," +
                        //                    " secondaryvaluechain_name," +
                        //                    " secondaryvaluechain_gid," +
                        //                    " created_by," +
                        //                    " created_date)" +
                        //                    " values(" +
                        //                    "'" + msGetGid1 + "'," +
                        //                    "'" + values.application_gid + "'," +
                        //                    "'" + values.secondaryvaluechain_list[i].valuechain_name + "'," +
                        //                    "'" + values.secondaryvaluechain_list[i].valuechain_gid + "'," +
                        //                    "'" + employee_gid + "'," +
                        //                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        //            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        //        }

                        //    }
                        //}

                        //for (var i = 0; i < existingsecondaryvaluechain_list.Count; i++)
                        //{
                        //    if (values.secondaryvaluechain_list.Contains(existingsecondaryvaluechain_list[i]) == false)
                        //    {
                        //        msSQL = "select application2secondaryvaluechain_gid from agr_mst_tsuprapplication2secondaryvaluechain where secondaryvaluechain_gid='" + existingsecondaryvaluechain_list[i].valuechain_gid + "' and application_gid = '" + values.application_gid + "'";
                        //        string lsapplication2secondaryvaluechain_gid = objdbconn.GetExecuteScalar(msSQL);

                        //        msSQL = "delete from agr_mst_tsuprapplication2secondaryvaluechain where application2secondaryvaluechain_gid='" + lsapplication2secondaryvaluechain_gid + "'";
                        //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        //    }
                        //}

                        //Updates

                        msSQL = "update agr_mst_tsuprapplication2contactno set application_gid ='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_mst_tsuprapplication2email set application_gid ='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_mst_tsuprapplication2geneticcode set application_gid ='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        values.status = true;
                        values.message = "Basic Details Updated Successfully";
                    }

                }
                catch (Exception ex)
                {
                    values.status = false;
                    values.message = "Error Occured While Updating Basic Details";
                }
            }
            else
            {
                values.message = "Kindly Add all Genetic details";
                values.status = false;
            }
        }

        public void DaGetApplicationBasicDetailsTempClear(string employee_gid, result values)
        {
            msSQL = "delete from agr_mst_tsuprapplication2contactno where application_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprapplication2email where application_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprapplication2geneticcode where application_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }

        // Group Address Details

        public bool DaPostGroupAddressDetail(string employee_gid, string user_gid, MdlMstAddressDetails values)
        {
            msSQL = "select primary_status from agr_mst_tsuprgroup2address where primary_status='Yes' and (group_gid='" + employee_gid + "' or group_gid='" + values.group_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Address Added";
                return false;
            }
            msSQL = "select group2address_gid from agr_mst_tsuprgroup2address where addresstype_name='" + values.address_type + "' and (group_gid='" + employee_gid + "' or group_gid='" + values.group_gid + "')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already Address Type Added";
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("G2AD");
            msSQL = " insert into agr_mst_tsuprgroup2address(" +
                    " group2address_gid," +
                    " group_gid," +
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
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.address_typegid + "'," +
                    "'" + values.address_type + "'," +
                    "'" + values.addressline1 + "'," +
                    "'" + values.addressline2 + "'," +
                    "'" + values.primary_status + "'," +
                    "'" + values.landmark + "'," +
                    "'" + values.postal_code + "'," +
                    "'" + values.city + "'," +
                    "'" + values.taluka + "'," +
                    "'" + values.district + "'," +
                    "'" + values.state + "'," +
                    "'" + values.country + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Address Details Added Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Adding Address Details";
                return false;
            }

        }

        public void DaEditGroupAddressDetail(string group2address_gid, MdlMstAddressDetails values)
        {
            try
            {
                msSQL = "select addresstype_gid, addresstype_name, addressline1, addressline2, landmark, taluka, primary_status, postal_code, city," +
                    " district, state, country, group_gid, group2address_gid " +
                    " from agr_mst_tsuprgroup2address where group2address_gid='" + group2address_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.address_typegid = objODBCDatareader["addresstype_gid"].ToString();
                    values.address_type = objODBCDatareader["addresstype_name"].ToString();
                    values.addressline1 = objODBCDatareader["addressline1"].ToString();
                    values.addressline2 = objODBCDatareader["addressline2"].ToString();
                    values.landmark = objODBCDatareader["landmark"].ToString();
                    values.taluka = objODBCDatareader["taluka"].ToString();
                    values.primary_status = objODBCDatareader["primary_status"].ToString();
                    values.postal_code = objODBCDatareader["postal_code"].ToString();
                    values.city = objODBCDatareader["city"].ToString();
                    values.district = objODBCDatareader["district"].ToString();
                    values.state = objODBCDatareader["state"].ToString();
                    values.country = objODBCDatareader["country"].ToString();
                    values.group_gid = objODBCDatareader["group_gid"].ToString();
                    values.group2address_gid = objODBCDatareader["group2address_gid"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;
                values.message = "success";
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaUpdateGroupAddressDetail(string employee_gid, MdlMstAddressDetails values)
        {
            msSQL = "select addresstype_gid, addresstype_name, addressline1, addressline2, landmark, taluka, primary_status, postal_code, city," +
                    " district, state, country, group_gid, group2address_gid " +
                    " from agr_mst_tsuprgroup2address where group2address_gid='" + values.group2address_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsaddress_typegid = objODBCDatareader["addresstype_gid"].ToString();
                lsaddress_type = objODBCDatareader["addresstype_name"].ToString();
                lsaddressline1 = objODBCDatareader["addressline1"].ToString();
                lsaddressline2 = objODBCDatareader["addressline2"].ToString();
                lslandmark = objODBCDatareader["landmark"].ToString();
                lstaluka = objODBCDatareader["taluka"].ToString();
                lsprimary_status = objODBCDatareader["primary_status"].ToString();
                lspostal_code = objODBCDatareader["postal_code"].ToString();
                lscity = objODBCDatareader["city"].ToString();
                lsdistrict = objODBCDatareader["district"].ToString();
                lsstate = objODBCDatareader["state"].ToString();
                lscountry = objODBCDatareader["country"].ToString();
                lsgroup_gid = objODBCDatareader["group_gid"].ToString();
                lsgroup2address_gid = objODBCDatareader["group2address_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tsuprgroup2address set " +
                         " addresstype_gid='" + values.address_typegid + "'," +
                         " addresstype_name='" + values.address_type + "'," +
                         " addressline1='" + values.addressline1 + "'," +
                         " addressline2='" + values.addressline2 + "'," +
                         " landmark='" + values.landmark + "'," +
                         " taluka='" + values.taluka + "'," +
                         " primary_status='" + values.primary_status + "'," +
                         " postal_code='" + values.postal_code + "'," +
                         " city='" + values.city + "'," +
                         " district='" + values.district + "'," +
                         " state='" + values.state + "'," +
                         " country='" + values.country + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where group2address_gid='" + values.group2address_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("GAUL");

                    msSQL = " insert into agr_mst_tsuprgroup2addressupdatelog(" +
                  " group2addressupdatelog_gid," +
                  " group2address_gid," +
                  " group_gid," +
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
                  " statusupdated_by," +
                  " created_by," +
                  " created_date)" +
                  " values(" +
                  "'" + msGetGid + "'," +
                  "'" + values.group2address_gid + "'," +
                  "'" + values.group_gid + "'," +
                  "'" + lsaddress_typegid + "'," +
                  "'" + lsaddress_type + "'," +
                  "'" + lsaddressline1 + "'," +
                  "'" + lsaddressline2 + "'," +
                  "'" + lsprimary_status + "'," +
                  "'" + lslandmark + "'," +
                  "'" + lspostal_code + "'," +
                  "'" + lscity + "'," +
                  "'" + lstaluka + "'," +
                  "'" + lsdistrict + "'," +
                  "'" + lsstate + "'," +
                  "'" + lscountry + "'," +
                  "'" + values.statusupdated_by + "'," +
                  "'" + employee_gid + "'," +
                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Address Details Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured While Updating Address Details";
            }
        }

        public void DaDeleteGroupAddressDetail(string group2address_gid, string employee_gid, MdlMstAddressDetails values)
        {
            msSQL = "delete from agr_mst_tsuprgroup2address where group2address_gid='" + group2address_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tsuprgroup2addressupdatelog where group2address_gid='" + group2address_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Address Deatils Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting Address Details";
                values.status = false;

            }
        }

        // Group Address Details

        public bool DaPostGroupBankDetail(string employee_gid, string user_gid, MdlMstBankDetails values)
        {
            msSQL = "select group2bank_gid from agr_mst_tsuprgroup2bank where ifsc_code='" + values.ifsc_code + "' and (group_gid='" + employee_gid + "' or group_gid='" + values.group_gid + "')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already Bank Added";
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("G2BK");
            msSQL = " insert into agr_mst_tsuprgroup2bank(" +
                    " group2bank_gid," +
                    " group_gid," +
                    " ifsc_code," +
                    " bank_accountno," +
                    " accountholder_name," +
                    " bank_name," +
                    " bank_branch," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.ifsc_code + "'," +
                    "'" + values.bank_accountno + "'," +
                    "'" + values.accountholder_name + "'," +
                    "'" + values.bank_name + "'," +
                    "'" + values.bank_branch + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Bank Details Added Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Adding Bank Details";
                return false;
            }
        }

        public void DaEditGroupBankDetail(string group2bank_gid, MdlMstBankDetails values)
        {
            try
            {
                msSQL = "select ifsc_code, bank_accountno, accountholder_name, bank_name, bank_branch," +
                    " group_gid, group2bank_gid " +
                    " from agr_mst_tsuprgroup2bank where group2bank_gid='" + group2bank_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.ifsc_code = objODBCDatareader["ifsc_code"].ToString();
                    values.bank_accountno = objODBCDatareader["bank_accountno"].ToString();
                    values.accountholder_name = objODBCDatareader["accountholder_name"].ToString();
                    values.bank_name = objODBCDatareader["bank_name"].ToString();
                    values.bank_branch = objODBCDatareader["bank_branch"].ToString();
                    values.group_gid = objODBCDatareader["group_gid"].ToString();
                    values.group2bank_gid = objODBCDatareader["group2bank_gid"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;
                values.message = "success";
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaUpdateGroupBankDetail(string employee_gid, MdlMstBankDetails values)
        {
            msSQL = "select ifsc_code, bank_accountno, accountholder_name, bank_name, bank_branch," +
                    " group_gid, group2bank_gid " +
                    " from agr_mst_tsuprgroup2bank where group2bank_gid='" + values.group2bank_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsifsc_code = objODBCDatareader["ifsc_code"].ToString();
                lsbank_accountno = objODBCDatareader["bank_accountno"].ToString();
                lsaccountholder_name = objODBCDatareader["accountholder_name"].ToString();
                lsbank_name = objODBCDatareader["bank_name"].ToString();
                lsbank_branch = objODBCDatareader["bank_branch"].ToString();
                lsgroup_gid = objODBCDatareader["group_gid"].ToString();
                lsgroup2bank_gid = objODBCDatareader["group2bank_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tsuprgroup2bank set " +
                         " ifsc_code='" + values.ifsc_code + "'," +
                         " bank_accountno='" + values.bank_accountno + "'," +
                         " accountholder_name='" + values.accountholder_name + "'," +
                         " bank_name='" + values.bank_name + "'," +
                         " bank_branch='" + values.bank_branch + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where group2bank_gid='" + values.group2bank_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("GBUL");

                    msSQL = " insert into agr_mst_tsuprgroup2bankupdatelog(" +
                  " group2bankupdatelog_gid," +
                  " group2bank_gid," +
                  " group_gid," +
                  " ifsc_code," +
                  " bank_accountno," +
                  " accountholder_name," +
                  " bank_name," +
                  " bank_branch," +
                  " statusupdated_by," +
                  " created_by," +
                  " created_date)" +
                  " values(" +
                  "'" + msGetGid + "'," +
                  "'" + values.group2bank_gid + "'," +
                  "'" + values.group_gid + "'," +
                  "'" + lsifsc_code + "'," +
                  "'" + lsbank_accountno + "'," +
                  "'" + lsaccountholder_name + "'," +
                  "'" + lsbank_name + "'," +
                  "'" + lsbank_branch + "'," +
                  "'" + values.statusupdated_by + "'," +
                  "'" + employee_gid + "'," +
                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Bank Details Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured While Updating Bank Details";
            }
        }

        public void DaDeleteGroupBankDetail(string group2bank_gid, string employee_gid, MdlMstBankDetails values)
        {
            msSQL = "delete from agr_mst_tsuprgroup2bank where group2bank_gid='" + group2bank_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tsuprgroup2bankupdatelog where group2bank_gid='" + group2bank_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Bank Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting Bank Details";
                values.status = false;

            }
        }

        public bool DaGroupDocumentUpload(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string lsdocument_title = httpRequest.Form["document_title"].ToString();
            string lsgroupdocument_gid = httpRequest.Form["groupdocument_gid"].ToString();
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/GroupDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                        //lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SamAgro/GroupDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        //FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                        //ms.WriteTo(file);
                        //file.Close();
                        //ms.Close();
                        //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "SamAgro/GroupDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/GroupDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/GroupDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        msGetGid = objcmnfunctions.GetMasterGID("G2DO");
                        msGetDocumentGid = objcmnfunctions.GetMasterGID("GPDA");

                        msSQL = "select covenant_type from ocs_mst_tgroupdocument where groupdocument_gid='" + lsgroupdocument_gid + "'";
                        string lscovenant_type = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " insert into agr_mst_tsuprgroup2document( " +
                                    " group2document_gid ," +
                                    " group_gid ," +
                                    " document_gid ," +
                                    " document_title ," +
                                    " document_name ," +
                                    " document_path," +
                                    " groupdocument_gid, " +
                                    " covenant_type," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + msGetDocumentGid + "'," +
                                    "'" + lsdocument_title + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + lsgroupdocument_gid + "'," +
                                    "'" + lscovenant_type + "'," +
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

        public void DaGroupDocumentDelete(string group2document_gid, MdlGroupDocument values)
        {
            msSQL = "delete from agr_mst_tsuprgroup2document where group2document_gid='" + group2document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                msSQL = " select groupdocumentchecklist_gid from agr_trn_tsuprdocumentchecktls where documentuploaded_gid='" + group2document_gid + "'";
                string lsgroupdocumentchecklist_gid = objdbconn.GetExecuteScalar(msSQL);

                if (lsgroupdocumentchecklist_gid != "")
                {
                    msSQL = " select count(*) as documentcount from agr_trn_tsuprdocumentchecktls " +
                            " where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                    string lsdocumentcount = objdbconn.GetExecuteScalar(msSQL);
                    if (lsdocumentcount == "1")
                    {
                        msSQL = "delete from agr_trn_tsuprgroupdocumentchecklist where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                msSQL = " select groupcovdocumentchecklist_gid from agr_trn_tsuprcovanantdocumentcheckdtls where documentuploaded_gid='" + group2document_gid + "'";
                string lschecklist_gid = objdbconn.GetExecuteScalar(msSQL);

                if (lschecklist_gid != "")
                {
                    msSQL = " select count(*) as documentcount from agr_trn_tsuprcovanantdocumentcheckdtls " +
                      " where groupcovdocumentchecklist_gid='" + lschecklist_gid + "'";
                    string lsdocumentcount = objdbconn.GetExecuteScalar(msSQL);
                    if (lsdocumentcount == "1")
                    {
                        msSQL = "delete from agr_trn_tsuprgroupcovenantdocumentchecklist where groupcovdocumentchecklist_gid='" + lschecklist_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                msSQL = "delete from agr_trn_tsuprcovanantdocumentcheckdtls where documentuploaded_gid='" + group2document_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "delete from agr_trn_tsuprdocumentchecktls where documentuploaded_gid='" + group2document_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
                values.message = "Document Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting Document";
                values.status = false;
            }
        }

        public void DaGroupAddressList(string group_gid, MdlMstAddressDetails values)
        {
            msSQL = "  select group2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, landmark," +
                    " postal_code from agr_mst_tsuprgroup2address where group_gid='" + group_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstaddress_list = new List<mstaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstaddress_list.Add(new mstaddress_list
                    {
                        group2address_gid = (dr_datarow["group2address_gid"].ToString()),
                        address_type = (dr_datarow["addresstype_name"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        addressline1 = (dr_datarow["addressline1"].ToString()),
                        addressline2 = (dr_datarow["addressline2"].ToString()),
                        taluka = (dr_datarow["taluka"].ToString()),
                        district = (dr_datarow["district"].ToString()),
                        state = (dr_datarow["state"].ToString()),
                        country = (dr_datarow["country"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString()),
                        landmark = (dr_datarow["landmark"].ToString())
                    });
                }
                values.mstaddress_list = getmstaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGroupAddressTmpList(string employee_gid, string group_gid, MdlMstAddressDetails values)
        {
            msSQL = "  select group2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, landmark," +
                    " postal_code from agr_mst_tsuprgroup2address where group_gid='" + group_gid + "' or group_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstaddress_list = new List<mstaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstaddress_list.Add(new mstaddress_list
                    {
                        group2address_gid = (dr_datarow["group2address_gid"].ToString()),
                        address_type = (dr_datarow["addresstype_name"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        addressline1 = (dr_datarow["addressline1"].ToString()),
                        addressline2 = (dr_datarow["addressline2"].ToString()),
                        taluka = (dr_datarow["taluka"].ToString()),
                        district = (dr_datarow["district"].ToString()),
                        state = (dr_datarow["state"].ToString()),
                        country = (dr_datarow["country"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString()),
                        landmark = (dr_datarow["landmark"].ToString())
                    });
                }
                values.mstaddress_list = getmstaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGroupBankList(string group_gid, MdlMstBankDetails values)
        {
            msSQL = "  select group2bank_gid,ifsc_code,bank_accountno, accountholder_name, bank_name, bank_branch" +
                    " from agr_mst_tsuprgroup2bank where group_gid='" + group_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstbank_list = new List<mstbank_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstbank_list.Add(new mstbank_list
                    {
                        group2bank_gid = (dr_datarow["group2bank_gid"].ToString()),
                        ifsc_code = (dr_datarow["ifsc_code"].ToString()),
                        bank_accountno = (dr_datarow["bank_accountno"].ToString()),
                        accountholder_name = (dr_datarow["accountholder_name"].ToString()),
                        bank_name = (dr_datarow["bank_name"].ToString()),
                        bank_branch = (dr_datarow["bank_branch"].ToString())
                    });
                }
                values.mstbank_list = getmstbank_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGroupBankTmpList(string employee_gid, string group_gid, MdlMstBankDetails values)
        {
            msSQL = "  select group2bank_gid,ifsc_code,bank_accountno, accountholder_name, bank_name, bank_branch" +
                    " from agr_mst_tsuprgroup2bank where group_gid='" + group_gid + "' or group_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstbank_list = new List<mstbank_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstbank_list.Add(new mstbank_list
                    {
                        group2bank_gid = (dr_datarow["group2bank_gid"].ToString()),
                        ifsc_code = (dr_datarow["ifsc_code"].ToString()),
                        bank_accountno = (dr_datarow["bank_accountno"].ToString()),
                        accountholder_name = (dr_datarow["accountholder_name"].ToString()),
                        bank_name = (dr_datarow["bank_name"].ToString()),
                        bank_branch = (dr_datarow["bank_branch"].ToString())
                    });
                }
                values.mstbank_list = getmstbank_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGroupDocumentList(string group_gid, MdlGroupDocument values)
        {
            msSQL = " select group2document_gid,document_name,document_title,document_path from agr_mst_tsuprgroup2document " +
                                 " where group_gid='" + group_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<groupdocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new groupdocument_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        group2document_gid = dt["group2document_gid"].ToString(),
                    });
                    values.groupdocument_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGroupDocumentTmpList(string group_gid, string employee_gid, MdlGroupDocument values)
        {
            msSQL = " select group2document_gid,document_name,document_title,document_path from agr_mst_tsuprgroup2document " +
                                 " where group_gid='" + employee_gid + "' or group_gid = '" + group_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<groupdocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new groupdocument_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        group2document_gid = dt["group2document_gid"].ToString(),
                    });
                    values.groupdocument_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaEditGroup(string group_gid, MdlMstGroup values)
        {
            try
            {
                msSQL = " select group_name,date_format(date_of_formation,'%d-%m-%Y') as date_of_formation,group_type,groupmember_count,groupurn_status,group_urn,group_status" +
                        " from agr_mst_tsuprgroup where group_gid='" + group_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.group_name = objODBCDatareader["group_name"].ToString();
                    values.date_of_formation = objODBCDatareader["date_of_formation"].ToString();
                    values.group_type = objODBCDatareader["group_type"].ToString();
                    values.groupmember_count = objODBCDatareader["groupmember_count"].ToString();
                    values.groupurn_status = objODBCDatareader["groupurn_status"].ToString();
                    values.group_urn = objODBCDatareader["group_urn"].ToString();
                    values.group_status = objODBCDatareader["group_status"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;
                values.message = "success";
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }

        }

        public void DaUpdateGroupDtl(string employee_gid, MdlMstGroup values)
        {
            msSQL = "select group_gid from agr_mst_tsuprgroup2address where (group_gid='" + employee_gid + "' or group_gid='" + values.group_gid + "')" + " and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Primary Address ";
                return;
            }

            msSQL = "select group_gid from agr_mst_tsuprgroup2bank where group_gid='" + employee_gid + "' or group_gid='" + values.group_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Atleast one Bank detail ";
                return;
            }

            msSQL = "select group_gid from agr_mst_tsuprgroup2document where group_gid='" + employee_gid + "' or group_gid='" + values.group_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Atleast one Document detail ";
                return;
            }

            msSQL = " select group_name,date_format(date_of_formation,'%d-%m-%Y') as date_of_formation,group_type,groupmember_count,groupurn_status,group_urn,group_status" +
                       " from agr_mst_tsuprgroup where group_gid='" + values.group_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsgroup_name = objODBCDatareader["group_name"].ToString();
                lsdate_of_formation = objODBCDatareader["date_of_formation"].ToString();
                lsgroup_type = objODBCDatareader["group_type"].ToString();
                lsgroupmember_count = objODBCDatareader["groupmember_count"].ToString();
                lsgroupurn_status = objODBCDatareader["groupurn_status"].ToString();
                lsgroup_urn = objODBCDatareader["group_urn"].ToString();
                lsgroup_status = objODBCDatareader["group_status"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " update agr_mst_tsuprgroup set ";
            if (values.group_name == "" || values.group_name == null)
            {

            }
            else
            {
                msSQL += " group_name='" + values.group_name.Replace("'", "") + "',";
            }
            if (Convert.ToDateTime(values.dateofformation).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += " date_of_formation='" + Convert.ToDateTime(values.dateofformation).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
            }
            msSQL += " group_type='" + values.group_type + "'," +
                     " groupmember_count='" + values.groupmember_count + "'," +
                     " groupurn_status='" + values.groupurn_status + "'," +
                   " group_urn='" + values.group_urn + "'," +
                   " group_status='Completed'," +
                   " updated_by='" + employee_gid + "'," +
                   " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                   " where group_gid='" + values.group_gid + "' ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                //Updates
                msSQL = "update agr_mst_tsuprgroup2address set group_gid ='" + values.group_gid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprgroup2bank set group_gid ='" + values.group_gid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select groupdocument_gid from agr_mst_tsuprgroup2document where group_gid='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lscovenant_type = "", lsdocumenttype_gid = "", lsdocumenttype_name = "", lscompanydocument_name = "";

                    string msGetdefDocchecklistGID = objcmnfunctions.GetMasterGID("DOCG");
                    msSQL = " select groupdocument_gid,documenttypes_gid,documenttype_name,groupdocument_name,covenant_type " +
                           " from ocs_mst_tgroupdocument where groupdocument_gid='" + dt["groupdocument_gid"].ToString() + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                        lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                        lscompanydocument_name = objODBCDatareader["groupdocument_name"].ToString();
                        lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                    }
                    objODBCDatareader.Close();
                    msSQL = " insert into agr_trn_tsuprdocumentchecktls(" +
                            " documentcheckdtl_gid," +
                            " application_gid," +
                            " credit_gid, " +
                            " groupdocument_gid, " +
                            " documentuploaded_gid, " +
                            " documenttype_gid," +
                        " documenttype_code," +
                        " documenttype_name," +
                        " covenant_type, " +
                        " tagged_by, " +
                        " created_date," +
                        " created_by)" +
                        " VALUES(" +
                        "'" + msGetdefDocchecklistGID + "'," +
                        "'" + values.application_gid + "'," +
                        "'" + values.group_gid + "'," +
                        "'" + dt["groupdocument_gid"].ToString() + "'," +
                        "'" + dt["group2document_gid"].ToString() + "'," +
                        "'" + lsdocumenttype_gid + "'," +
                        "'" + lsdocumenttype_name + "'," +
                        "'" + lscompanydocument_name.Replace("'", "") + "'," +
                        "'" + lscovenant_type + "'," +
                        "'N'," +
                        "current_timestamp," +
                        "'" + employee_gid + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (lscovenant_type == "Y")
                    {
                        string msGetDocchecklistGID = objcmnfunctions.GetMasterGID("CDCL");
                        msSQL = " insert into agr_trn_tsuprcovanantdocumentcheckdtls(" +
                       " covenantdocumentcheckdtl_gid," +
                       " application_gid," +
                       " credit_gid," +
                       " groupdocument_gid," +
                       " documentuploaded_gid, " +
                       " documenttype_gid," +
                       " documenttype_code," +
                       " documenttype_name," +
                       " covenant_type, " +
                       " tagged_by, " +
                       " created_date," +
                       " created_by)" +
                       " VALUES(" +
                       "'" + msGetDocchecklistGID + "'," +
                       "'" + values.application_gid + "'," +
                       "'" + values.group_gid + "'," +
                       "'" + dt["groupdocument_gid"].ToString() + "'," +
                       "'" + dt["group2document_gid"].ToString() + "'," +
                       "'" + lsdocumenttype_gid + "'," +
                       "'" + lsdocumenttype_name + "'," +
                       "'" + lscompanydocument_name.Replace("'", "") + "'," +
                       "'" + lscovenant_type + "'," +
                       "'N'," +
                       "current_timestamp," +
                       "'" + employee_gid + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();

                msSQL = "update agr_mst_tsuprgroup2document set group_gid ='" + values.group_gid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                DaAgrMstSuprScannedDocument objvalues = new DaAgrMstSuprScannedDocument();
                objvalues.DaGroupDocChecklistinfo(values.application_gid, values.group_gid, employee_gid);

                msGetGid = objcmnfunctions.GetMasterGID("CGUL");

                msSQL = " insert into agr_mst_tsuprgroupupdatelog(" +
                     " groupupdatelog_gid," +
                     " group_gid," +
                     " application_gid," +
                     " group_name," +
                     " date_of_formation," +
                     " group_type," +
                     " groupmember_count," +
                     " groupurn_status," +
                     " group_urn," +
                     " group_status," +
                     " statusupdated_by," +
                     " created_by," +
                     " created_date)" +
                     " values(" +
                     "'" + msGetGid + "'," +
                     "'" + values.group_gid + "'," +
                     "'" + values.application_gid + "'," +
                     "'" + lsgroup_name.Replace("'", "") + "'," +
                     "'" + lsdate_of_formation + "'," +
                     "'" + lsgroup_type + "'," +
                     "'" + lsgroupmember_count + "'," +
                     "'" + lsgroupurn_status + "'," +
                     "'" + lsgroup_urn + "'," +
                     "'" + lsgroup_status + "'," +
                     "'" + values.statusupdated_by + "'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Group Details Updated Successfully";
            }

            else
            {
                values.status = false;
                values.message = "Error Occured While Updating Group Details..!";
            }
        }

        public void DaGetGroupTempClear(string employee_gid, result values)
        {
            msSQL = "delete from agr_mst_tsuprgroup2address where group_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprgroup2bank where group_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprgroup2document where group_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }

        public bool DaPostHypoDoc(HttpRequest httpRequest, Documentname objfilename, string employee_gid)
        {
            DocumentList objdocumentmodel = new DocumentList();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();


            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            //path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "SamAgro/HypothecationDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/HypothecationDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }

            string document_title = httpRequest.Form["document_title"].ToString();


            if (httpRequest.Files.Count > 0)
            {
                string lsfirstdocument_filepath = string.Empty;
                httpFileCollection = httpRequest.Files;
                for (int i = 0; i < httpFileCollection.Count; i++)
                {
                    string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                    httpPostedFile = httpFileCollection[i];
                    string FileExtension = httpPostedFile.FileName;
                    //string lsfile_gid = msdocument_gid + FileExtension;
                    string lsfile_gid = msdocument_gid;
                    FileExtension = Path.GetExtension(FileExtension).ToLower();
                    lsfile_gid = lsfile_gid + FileExtension;
                    if ((FileExtension == ".xls") || (FileExtension == ".xlsx") || (FileExtension == ".doc") || (FileExtension == ".docx") || (FileExtension == ".pdf") || (FileExtension == ".zip") || (FileExtension == ".msg") || (FileExtension == ".oft") || (FileExtension == ".txt") || (FileExtension == ".txtx"))
                    {
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);
                        //lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "SamAgro/HypothecationDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);
                        //lspath = "../../../erp_documents" + "/" + lscompany_code + "/" + "SamAgro/HypothecationDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/HypothecationDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/HypothecationDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        msGetGid = objcmnfunctions.GetMasterGID("HYPD");
                        msSQL = " insert into agr_mst_tuploadhypothecationdocument( " +
                                     " uploadhypothecationdocument_gid," +
                                     " document_name, " +
                                     " document_title," +
                                     " document_path, " +
                                     " application2hypothecation_gid," +
                                     " created_by ," +
                                     " created_date " +
                                     " )values(" +
                                     "'" + msGetGid + "'," +
                                     "'" + httpPostedFile.FileName + "'," +
                                     "'" + document_title + "'," +
                                     "'" + lspath + msdocument_gid + FileExtension + "'," +
                                     "'" + employee_gid + "'," +
                                     "'" + employee_gid + "'," +
                                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult != 0)
                        {
                            msSQL = " select uploadhypothecationdocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                               " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by,a.document_title " +
                               " from agr_mst_tuploadhypothecationdocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                               " and b.user_gid = c.user_gid and application2hypothecation_gid='" + employee_gid + "'";

                            dt_datatable = objdbconn.GetDataTable(msSQL);
                            var get_filename = new List<DocumentList>();
                            if (dt_datatable.Rows.Count != 0)
                            {
                                foreach (DataRow dr_datarow in dt_datatable.Rows)
                                {
                                    get_filename.Add(new DocumentList
                                    {
                                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                                        document_name = (dr_datarow["document_name"].ToString()),
                                        document_gid = (dr_datarow["uploadhypothecationdocument_gid"].ToString()),
                                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                                        updated_date = dr_datarow["uploaded_date"].ToString(),
                                        document_title = dr_datarow["document_title"].ToString()
                                    });
                                }
                                objfilename.DocumentList = get_filename;
                            }
                            dt_datatable.Dispose();

                            objfilename.status = true;
                            objfilename.message = "Hypothecation Document uploaded successfully";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured While Uploading Document";
                        }
                    }
                    else
                    {
                        objfilename.status = false;
                        objfilename.message = "File format is not supported";
                    }
                }
            }
            return true;
        }

        public void DadeleteHypoDoc(string document_gid, Documentname values, string employee_gid)
        {
            msSQL = "delete from agr_mst_tuploadhypothecationdocument where uploadhypothecationdocument_gid='" + document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Document deleted successfully";
                values.status = true;

                msSQL = " select uploadhypothecationdocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                      " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by,a.document_title " +
                      " from agr_mst_tuploadhypothecationdocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                      " and b.user_gid = c.user_gid and application2hypothecation_gid='" + employee_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<DocumentList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new DocumentList
                        {
                            document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                            document_name = (dr_datarow["document_name"].ToString()),
                            document_gid = (dr_datarow["uploadhypothecationdocument_gid"].ToString()),
                            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                            updated_date = dr_datarow["uploaded_date"].ToString(),
                            document_title = dr_datarow["document_title"].ToString()
                        });
                    }
                    values.DocumentList = get_filename;
                }
                dt_datatable.Dispose();
            }
            else
            {
                values.message = "Error Occrued While Deleting Document";
                values.status = false;
            }
        }

        public void DaHypothecationDocumentTempList(string employee_gid, string application2hypothecation_gid, Documentname objfilename)
        {
            msSQL = " select application2hypothecation_gid,uploadhypothecationdocument_gid,document_name,document_path,document_title from agr_mst_tuploadhypothecationdocument " +
                           " where application2hypothecation_gid='" + employee_gid + "' or application2hypothecation_gid='" + application2hypothecation_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<DocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_filename.Add(new DocumentList
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        document_gid = dt["uploadhypothecationdocument_gid"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        application2hypothecation_gid = dt["application2hypothecation_gid"].ToString(),
                    });
                    objfilename.DocumentList = get_filename;
                }
            }
            dt_datatable.Dispose();
            objfilename.status = true;
        }

        public void DaPostHypothecation(string employee_gid, MdlMstHypothecation values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("AP2C");
            msSQL = " insert into agr_mst_tsuprapplication2hypothecation(" +
                   " application2hypothecation_gid ," +
                   " application_gid," +
                   " securitytype_gid," +
                   " security_type," +
                   " security_description," +
                   " security_value ," +
                   " securityassessed_date," +
                   " asset_id," +
                   " roc_fillingid," +
                   " CERSAI_fillingid," +
                   " hypoobservation_summary," +
                   " primary_security," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.application_gid + "'," +
                   "'" + values.securitytype_gid + "'," +
                   "'" + values.security_type + "'," +
                   "'" + values.security_description + "',";
            if (values.security_value == null || values.security_value == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.security_value.Replace(",", "") + "',";
            }
            if (values.securityassessed_date == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.securityassessed_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }

            msSQL += "'" + values.asset_id + "'," +
                     "'" + values.roc_fillingid + "'," +
                     "'" + values.CERSAI_fillingid + "',";
            if (values.hypoobservation_summary == null || values.hypoobservation_summary == "")
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.hypoobservation_summary.Replace("'", "") + "',";
            }
            msSQL += "'" + values.primary_security + "'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Hypothecation Details Added Successfully";

                msSQL = "update agr_mst_tuploadhypothecationdocument set application2hypothecation_gid='" + msGetGid + "' where application2hypothecation_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprapplication set hypothecation_flag='Y' where application_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select application2hypothecation_gid,securitytype_gid,security_type,security_description,security_value," +
                    " date_format(securityassessed_date,'%d-%m-%Y') as securityassessed_date,asset_id,roc_fillingid,CERSAI_fillingid," +
                    " hypoobservation_summary,primary_security " +
                    " from agr_mst_tsuprapplication2hypothecation where application_gid='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gethypothecation_list = new List<hypothecation_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        gethypothecation_list.Add(new hypothecation_list
                        {
                            application2hypothecation_gid = (dr_datarow["application2hypothecation_gid"].ToString()),
                            securitytype_gid = (dr_datarow["securitytype_gid"].ToString()),
                            security_type = (dr_datarow["security_type"].ToString()),
                            security_description = (dr_datarow["security_description"].ToString()),
                            security_value = (dr_datarow["security_value"].ToString()),
                            securityassessed_date = (dr_datarow["securityassessed_date"].ToString()),
                            asset_id = (dr_datarow["asset_id"].ToString()),
                            roc_fillingid = (dr_datarow["roc_fillingid"].ToString()),
                            CERSAI_fillingid = (dr_datarow["CERSAI_fillingid"].ToString()),
                            hypoobservation_summary = (dr_datarow["hypoobservation_summary"].ToString()),
                            primary_security = (dr_datarow["primary_security"].ToString()),
                        });
                    }
                    values.hypothecation_list = gethypothecation_list;
                }
                dt_datatable.Dispose();
                msSQL = " select uploadhypothecationdocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                       " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by,a.document_title " +
                       " from agr_mst_tuploadhypothecationdocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                       " and b.user_gid = c.user_gid and application2hypothecation_gid='" + employee_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<DocumentList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new DocumentList
                        {
                            document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                            document_name = (dr_datarow["document_name"].ToString()),
                            document_gid = (dr_datarow["uploadhypothecationdocument_gid"].ToString()),
                            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                            updated_date = dr_datarow["uploaded_date"].ToString(),
                            document_title = dr_datarow["document_title"].ToString()
                        });
                    }
                    values.DocumentList = get_filename;
                }
                dt_datatable.Dispose();
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while Adding";
            }
        }

        public void DaDeleteHypothecation(string application2hypothecation_gid, MdlMstHypothecation values, string employee_gid)
        {
            msSQL = "delete from agr_mst_tsuprapplication2hypothecation where application2hypothecation_gid='" + application2hypothecation_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Hypothecation Details Deleted Successfully";

                msSQL = "select application2hypothecation_gid,securitytype_gid,security_type,security_description,security_value," +
                   " date_format(securityassessed_date,'%d-%m-%Y') as securityassessed_date,asset_id,roc_fillingid,CERSAI_fillingid," +
                   " hypoobservation_summary,primary_security " +
                   " from agr_mst_tsuprapplication2hypothecation where application_gid='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gethypothecation_list = new List<hypothecation_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        gethypothecation_list.Add(new hypothecation_list
                        {
                            application2hypothecation_gid = (dr_datarow["application2hypothecation_gid"].ToString()),
                            securitytype_gid = (dr_datarow["securitytype_gid"].ToString()),
                            security_type = (dr_datarow["security_type"].ToString()),
                            security_description = (dr_datarow["security_description"].ToString()),
                            security_value = (dr_datarow["security_value"].ToString()),
                            securityassessed_date = (dr_datarow["securityassessed_date"].ToString()),
                            asset_id = (dr_datarow["asset_id"].ToString()),
                            roc_fillingid = (dr_datarow["roc_fillingid"].ToString()),
                            CERSAI_fillingid = (dr_datarow["CERSAI_fillingid"].ToString()),
                            hypoobservation_summary = (dr_datarow["hypoobservation_summary"].ToString()),
                            primary_security = (dr_datarow["primary_security"].ToString()),
                        });
                    }
                    values.hypothecation_list = gethypothecation_list;
                }
                dt_datatable.Dispose();
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred while deleting";
            }
        }

        public void DaHypothecationDetailsEdit(string application_gid, MdlMstHypothecation values, string employee_gid)
        {
            try
            {
                msSQL = " select application2hypothecation_gid, application_gid, securitytype_gid,security_type,security_description,security_value," +
                    " date_format(securityassessed_date, '%Y-%m-%d') as securityassessed_dateedit," +
                    " asset_id,roc_fillingid,CERSAI_fillingid,hypoobservation_summary,primary_security " +
                    " from agr_mst_tsuprapplication2hypothecation where application_gid='" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsapplication2hypothecation_gid = objODBCDatareader["application2hypothecation_gid"].ToString();
                    values.application2hypothecation_gid = objODBCDatareader["application2hypothecation_gid"].ToString();
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.securitytype_gid = objODBCDatareader["securitytype_gid"].ToString();
                    values.security_type = objODBCDatareader["security_type"].ToString();
                    values.security_description = objODBCDatareader["security_description"].ToString();
                    values.security_value = objODBCDatareader["security_value"].ToString();
                    values.securityassessed_date = objODBCDatareader["securityassessed_dateedit"].ToString();
                    values.asset_id = objODBCDatareader["asset_id"].ToString();
                    values.roc_fillingid = objODBCDatareader["roc_fillingid"].ToString();
                    values.CERSAI_fillingid = objODBCDatareader["CERSAI_fillingid"].ToString();
                    values.hypoobservation_summary = objODBCDatareader["hypoobservation_summary"].ToString();
                    values.primary_security = objODBCDatareader["primary_security"].ToString();
                }

                objODBCDatareader.Close();
                msSQL = " select application2hypothecation_gid,uploadhypothecationdocument_gid,document_name,document_path,document_title from agr_mst_tuploadhypothecationdocument " +
                          " where application2hypothecation_gid='" + employee_gid + "' or application2hypothecation_gid='" + lsapplication2hypothecation_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<DocumentList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        get_filename.Add(new DocumentList
                        {
                            document_name = dt["document_name"].ToString(),
                            document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                            document_gid = dt["uploadhypothecationdocument_gid"].ToString(),
                            document_title = dt["document_title"].ToString(),
                            application2hypothecation_gid = dt["application2hypothecation_gid"].ToString(),
                        });
                        values.DocumentList = get_filename;
                    }
                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "success";
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaHypothecationDetailsUpdate(string employee_gid, MdlMstHypothecation values)
        {
            msSQL = " select application2hypothecation_gid, application_gid, securitytype_gid,security_type,security_description,security_value,securityassessed_date," +
                    " asset_id,roc_fillingid,CERSAI_fillingid,hypoobservation_summary,primary_security " +
                    " from agr_mst_tsuprapplication2hypothecation where application2hypothecation_gid='" + values.application2hypothecation_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsapplication2hypothecation_gid = objODBCDatareader["application2hypothecation_gid"].ToString();
                lsapplication_gid = objODBCDatareader["application_gid"].ToString();
                lssecuritytype_gid = objODBCDatareader["securitytype_gid"].ToString();
                lssecurity_type = objODBCDatareader["security_type"].ToString();
                lssecurity_description = objODBCDatareader["security_description"].ToString();
                lssecurity_value = objODBCDatareader["security_value"].ToString();
                if (objODBCDatareader["securityassessed_date"].ToString() == "")
                {
                }
                else
                {
                    lssecurityassessed_date = Convert.ToDateTime(objODBCDatareader["securityassessed_date"]).ToString("dd-MM-yyyy");
                }
                lsasset_id = objODBCDatareader["asset_id"].ToString();
                lsroc_fillingid = objODBCDatareader["roc_fillingid"].ToString();
                lsCERSAI_fillingid = objODBCDatareader["CERSAI_fillingid"].ToString();
                lshypoobservation_summary = objODBCDatareader["hypoobservation_summary"].ToString();
                lsprimary_security = objODBCDatareader["primary_security"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tsuprapplication2hypothecation set " +
                        " securitytype_gid='" + values.securitytype_gid + "'," +
                         " security_type='" + values.security_type + "',";
                if (values.security_description == null || values.security_description == "")
                {

                }
                else
                {
                    msSQL += " security_description='" + values.security_description.Replace("'", " ") + "',";
                }
                msSQL += " security_value='" + values.security_value + "',";
                if (Convert.ToDateTime(values.securityassessed_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " securityassessed_date='" + Convert.ToDateTime(values.securityassessed_date).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                if (values.asset_id == null || values.asset_id == "")
                {

                }
                else
                {
                    msSQL += " asset_id='" + values.asset_id.Replace("'", " ") + "',";
                }
                if (values.roc_fillingid == null || values.roc_fillingid == "")
                {

                }
                else
                {
                    msSQL += " roc_fillingid='" + values.roc_fillingid.Replace("'", " ") + "',";
                }
                if (values.CERSAI_fillingid == null || values.CERSAI_fillingid == "")
                {

                }
                else
                {
                    msSQL += " CERSAI_fillingid='" + values.CERSAI_fillingid.Replace("'", " ") + "',";
                }
                if (values.hypoobservation_summary == null || values.hypoobservation_summary == "")
                {

                }
                else
                {
                    msSQL += " hypoobservation_summary='" + values.hypoobservation_summary.Replace("'", " ") + "',";
                }
                if (values.primary_security == null || values.primary_security == "")
                {

                }
                else
                {
                    msSQL += " primary_security='" + values.primary_security.Replace("'", " ") + "',";
                }
                msSQL += " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where application2hypothecation_gid='" + values.application2hypothecation_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msSQL = "update agr_mst_tuploadhypothecationdocument set application2hypothecation_gid='" + values.application2hypothecation_gid + "'" +
                       " where application2hypothecation_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msGetGid = objcmnfunctions.GetMasterGID("A2HU");
                    msSQL = " insert into agr_mst_tsuprapplication2hypothecationUpdateLOG(" +
                   " application2hypothecation_UpdateLOGgid ," +
                   " application2hypothecation_gid ," +
                   " application_gid," +
                   " securitytype_gid," +
                   " security_type," +
                   " security_description," +
                   " security_value ," +
                   " securityassessed_date," +
                   " asset_id," +
                   " roc_fillingid," +
                   " CERSAI_fillingid," +
                   " hypoobservation_summary," +
                   " primary_security," +
                   " statusupdated_by," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.application2hypothecation_gid + "'," +
                   "'" + values.application_gid + "'," +
                   "'" + lssecuritytype_gid + "'," +
                   "'" + lssecurity_type + "'," +
                   "'" + lssecurity_description + "'," +
                   "'" + lssecurity_value + "'," +
                   "'" + lssecurityassessed_date + "'," +
                   "'" + lsasset_id + "'," +
                             "'" + lsroc_fillingid + "'," +
                             "'" + lsCERSAI_fillingid + "'," +
                             "'" + lshypoobservation_summary + "'," +
                             "'" + lsprimary_security + "'," +
                             "'" + values.statusupdated_by + "'," +
                             "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Hypothecation Details Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }
        public void DaServicechargeEdit(string application2servicecharge_gid, MdlProductCharges values, string employee_gid)
        {
            try
            {
                msSQL = " select application2servicecharge_gid,application_gid,processing_fee,processing_collectiontype,doc_charges,doccharge_collectiontype," +
                        " fieldvisit_charges,fieldvisit_charges_collectiontype,adhoc_fee,adhoc_collectiontype,life_insurance,lifeinsurance_collectiontype," +
                        " acct_insurance,total_collect,total_deduct,product_type,producttype_gid,acctinsurance_collectiontype " +
                        " from agr_mst_tsuprapplicationservicecharge where application2servicecharge_gid ='" + application2servicecharge_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application2servicecharge_gid = objODBCDatareader["application2servicecharge_gid"].ToString();
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.processing_fee = objODBCDatareader["processing_fee"].ToString();
                    values.processing_collectiontype = objODBCDatareader["processing_collectiontype"].ToString();
                    values.doc_charges = objODBCDatareader["doc_charges"].ToString();
                    values.doccharge_collectiontype = objODBCDatareader["doccharge_collectiontype"].ToString();
                    values.fieldvisit_charge = objODBCDatareader["fieldvisit_charges"].ToString();
                    values.fieldvisit_charges_collectiontype = objODBCDatareader["fieldvisit_charges_collectiontype"].ToString();
                    values.adhoc_fee = objODBCDatareader["adhoc_fee"].ToString();
                    values.adhoc_collectiontype = objODBCDatareader["adhoc_collectiontype"].ToString();
                    values.life_insurance = objODBCDatareader["life_insurance"].ToString();
                    values.lifeinsurance_collectiontype = objODBCDatareader["lifeinsurance_collectiontype"].ToString();
                    values.acct_insurance = objODBCDatareader["acct_insurance"].ToString();
                    values.total_collect = objODBCDatareader["total_collect"].ToString();
                    values.total_deduct = objODBCDatareader["total_deduct"].ToString();
                    values.product_type = objODBCDatareader["product_type"].ToString();
                    values.producttype_gid = objODBCDatareader["producttype_gid"].ToString();
                    values.acctinsurance_collectiontype = objODBCDatareader["acctinsurance_collectiontype"].ToString();
                }

                objODBCDatareader.Close();
                values.status = true;
                values.message = "success";
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaServicechargeUpdate(string employee_gid, MdlProductCharges values)
        {

            msSQL = " select application2servicecharge_gid,application_gid,processing_fee,processing_collectiontype,doc_charges,doccharge_collectiontype," +
                          " fieldvisit_charges,fieldvisit_charges_collectiontype,adhoc_fee,adhoc_collectiontype,life_insurance,lifeinsurance_collectiontype," +
                          " acct_insurance,total_collect,total_deduct,product_type,producttype_gid,acctinsurance_collectiontype " +
                          " from agr_mst_tsuprapplicationservicecharge where application2servicecharge_gid ='" + values.application2servicecharge_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsapplication_gid = objODBCDatareader["application_gid"].ToString();
                lsprocessing_fee = objODBCDatareader["processing_fee"].ToString();
                lsprocessing_collectiontype = objODBCDatareader["processing_collectiontype"].ToString();
                lsdoc_charges = objODBCDatareader["doc_charges"].ToString();
                lsdoccharge_collectiontype = objODBCDatareader["doccharge_collectiontype"].ToString();
                lsfieldvisit_charge = objODBCDatareader["fieldvisit_charges"].ToString();
                lsfieldvisit_charges_collectiontype = objODBCDatareader["fieldvisit_charges_collectiontype"].ToString();
                lsadhoc_fee = objODBCDatareader["adhoc_fee"].ToString();
                lsadhoc_collectiontype = objODBCDatareader["adhoc_collectiontype"].ToString();
                lslife_insurance = objODBCDatareader["life_insurance"].ToString();
                lslifeinsurance_collectiontype = objODBCDatareader["lifeinsurance_collectiontype"].ToString();
                lsacct_insurance = objODBCDatareader["acct_insurance"].ToString();
                lstotal_collect = objODBCDatareader["total_collect"].ToString();
                lstotal_deduct = objODBCDatareader["total_deduct"].ToString();
                lsproduct_type = objODBCDatareader["product_type"].ToString();
                lsproducttype_gid = objODBCDatareader["producttype_gid"].ToString();
                lsacctinsurance_collectiontype = objODBCDatareader["acctinsurance_collectiontype"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tsuprapplicationservicecharge set " +
                        " processing_fee='" + values.processing_fee.Replace(",", "") + "'," +
                         " processing_collectiontype='" + values.processing_collectiontype + "'," +
                         " doc_charges='" + values.doc_charges.Replace(",", "") + "'," +
                         " doccharge_collectiontype='" + values.doccharge_collectiontype + "'," +
                         " fieldvisit_charges='" + values.fieldvisit_charge.Replace(",", "") + "'," +
                         " fieldvisit_charges_collectiontype='" + values.fieldvisit_collectiontype + "'," +
                         " adhoc_fee='" + values.adhoc_fee.Replace(",", "") + "'," +
                         " adhoc_collectiontype='" + values.adhoc_collectiontype + "'," +
                         " life_insurance='" + values.life_insurance.Replace(",", "") + "'," +
                         " lifeinsurance_collectiontype='" + values.lifeinsurance_collectiontype + "'," +
                         " acct_insurance='" + values.acct_insurance.Replace(",", "") + "'," +
                         " acctinsurance_collectiontype='" + values.acctinsurance_collectiontype + "'," +
                         " total_collect='" + values.total_collect + "'," +
                         " total_deduct='" + values.total_deduct + "'," +
                          " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where application2servicecharge_gid='" + values.application2servicecharge_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select generatelsa_gid from agr_trn_tgeneratelsa a " +
                   " left join agr_trn_tsuprprocesstype_assign b on b.application_gid = a.application_gid " +
                   " where a.application_gid = '" + lsapplication_gid + "' and b.menu_gid = '" + getMenuClass.LSA + "' and maker_approvalflag = 'N'";
                string generatelsa_gid = objdbconn.GetExecuteScalar(msSQL);
                if (generatelsa_gid != "")
                {
                    msSQL = " update agr_trn_tsuprlsafeescharge set " +
                       " processing_fee='" + values.processing_fee.Replace(",", "") + "'," +
                       " processing_collectiontype='" + values.processing_collectiontype + "'," +
                       " doc_charges='" + values.doc_charges.Replace(",", "") + "'," +
                       " doccharge_collectiontype='" + values.doccharge_collectiontype + "'," +
                       " fieldvisit_charges='" + values.fieldvisit_charge.Replace(",", "") + "'," +
                       " fieldvisit_charges_collectiontype='" + values.fieldvisit_collectiontype + "'," +
                       " adhoc_fee='" + values.adhoc_fee.Replace(",", "") + "'," +
                       " adhoc_collectiontype='" + values.adhoc_collectiontype + "'," +
                       " life_insurance='" + values.life_insurance.Replace(",", "") + "'," +
                       " lifeinsurance_collectiontype='" + values.lifeinsurance_collectiontype + "'," +
                       " acct_insurance='" + values.acct_insurance.Replace(",", "") + "'," +
                       " acctinsurance_collectiontype='" + values.acctinsurance_collectiontype + "'," +
                       " total_collect='" + values.total_collect + "'," +
                       " total_deduct='" + values.total_deduct + "'," +
                        " updated_by='" + employee_gid + "'," +
                       " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                       " where application2servicecharge_gid='" + values.application2servicecharge_gid + "' and generatelsa_gid ='" + generatelsa_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (mnResult == 1)
                {

                    values.status = true;
                    values.message = "Service Charge Details Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }
        public void DaPostUnderwrite(string employee_gid, MdlMstCUWGeneticCode values)
        {
            msSQL = "update agr_mst_tsuprapplication set ccsubmit_flag='Y', approval_status='Submitted to CC',ccsubmitted_by='" + employee_gid + "'," +
                   " ccsubmitted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                   " where application_gid='" + values.application_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Application Submitted to CC Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Submitting";
            }
        }
        public bool DaCAMocumentUpload(HttpRequest httpRequest, MdlMstCC objfilename, string employee_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string lsdocument_title = httpRequest.Form["document_title"].ToString();
            string lsapplication_gid = httpRequest.Form["application_gid"].ToString();
            string path, lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/CAMDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/CAMDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/CAMDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("A2MD");
                        msSQL = " insert into agr_mst_tsuprapplication2camdoc( " +
                                    " application2camdoc_gid," +
                                    " application_gid," +
                                    " document_title  ," +
                                    " document_name  ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + lsapplication_gid + "'," +
                                    "'" + lsdocument_title + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            msSQL = " update agr_mst_tsuprapplication set " +
                                    " camdocumentupload_flag='Y'," +
                                    " updated_by='" + employee_gid + "'," +
                                      " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                      " where application_gid='" + lsapplication_gid + "' ";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            objfilename.status = true;
                            objfilename.message = "Document Uploaded Successfully..!";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured..!";
                        }

                        msSQL = " select application2camdoc_gid,application_gid,document_name,document_path,document_title from " +
                            " agr_mst_tsuprapplication2camdoc where application_gid='" + lsapplication_gid + "'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        var getcamdocument_list = new List<camdocument_list>();
                        if (dt_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                getcamdocument_list.Add(new camdocument_list
                                {
                                    document_name = dt["document_name"].ToString(),
                                    document_title = dt["document_title"].ToString(),
                                    document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                                    application_gid = dt["application_gid"].ToString(),
                                    application2camdoc_gid = dt["application2camdoc_gid"].ToString(),

                                });
                                objfilename.camdocument_list = getcamdocument_list;
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
        public void Dagetcamdoc_delete(string application2camdoc_gid, string application_gid, MdlMstCC values)
        {
            msSQL = "delete from agr_mst_tsuprapplication2camdoc where application2camdoc_gid='" + application2camdoc_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = " select application2camdoc_gid,application_gid,document_name,document_path,document_title from " +
                           " agr_mst_tsuprapplication2camdoc where application_gid='" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcamdocument_list = new List<camdocument_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getcamdocument_list.Add(new camdocument_list
                        {
                            document_name = dt["document_name"].ToString(),
                            document_title = dt["document_title"].ToString(),
                            document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                            application_gid = dt["application_gid"].ToString(),
                            application2camdoc_gid = dt["application2camdoc_gid"].ToString(),

                        });
                        values.camdocument_list = getcamdocument_list;
                    }
                }
                dt_datatable.Dispose();
                values.message = "CAM Document deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occrued while deleting document";
                values.status = false;
            }
        }

        public void DaGetCAM(string application_gid, MdlMstCC values)
        {
            msSQL = " select application2camdoc_gid,application_gid,document_name,document_path,document_title from " +
                       " agr_mst_tsuprapplication2camdoc where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcamdocument_list = new List<camdocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcamdocument_list.Add(new camdocument_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        application_gid = dt["application_gid"].ToString(),
                        application2camdoc_gid = dt["application2camdoc_gid"].ToString(),

                    });
                    values.camdocument_list = getcamdocument_list;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaImportExcelBankStatement(HttpRequest httpRequest, string employee_gid, result objResult)
        {
            try
            {
                HttpFileCollection httpFileCollection;
                DataTable dt = null;
                string lspath, lsfilePath, path, lsfile_name;
                string application_gid = httpRequest.Form["application_gid"];
                string credit_gid = httpRequest.Form["credit_gid"];
                string lscompany_code = string.Empty;
                string project_flag = httpRequest.Form["project_flag"].ToString();

                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                // Create Directory
                //lsfilePath = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/SamAgro/BankstatementDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
                lsfilePath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/BankstatementDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

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

                // path creation
                lspath = lsfilePath + "/";
                FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);
                using (ExcelPackage xlPackage = new ExcelPackage(ms))
                {
                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];
                    rowCount = worksheet.Dimension.End.Row;
                }
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/BankstatementDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                file.Close();
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

                    msSQL = " insert into agr_mst_tsuprbankstatementanalysis(" +
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
                    " from agr_mst_tsuprbankstatementanalysis a " +
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
                    " from agr_mst_tsuprbankstatementanalysis a " +
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
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/Bank Statement Analysis/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objBankStatementExportExcel.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/Bank Statement Analysis/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objBankStatementExportExcel.lsname;
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
                objBankStatementExportExcel.lscloudpath = lscompany_code + "/" + "SamAgro/Bank Statement Analysis/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objBankStatementExportExcel.lsname;
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objBankStatementExportExcel.lscloudpath, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                objBankStatementExportExcel.status = false;
                objBankStatementExportExcel.message = "Failure";
            }
            objBankStatementExportExcel.status = true;
            objBankStatementExportExcel.message = "Success";
            dt_datatable.Dispose();
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
                //lsfilePath = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/SamAgro/P&L/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
                lsfilePath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/P&L/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                string colName;
                using (ExcelPackage xlPackage = new ExcelPackage(ms))
                {
                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];
                    rowCount = worksheet.Dimension.End.Row;
                    columnCount = worksheet.Dimension.End.Column;
                    string endRange = worksheet.Dimension.End.Address;
                    colName = Regex.Replace(endRange, @"[\d-]", string.Empty);
                }
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/P&L/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                file.Close();
                ms.Close();

                objcmnfunctions.uploadFile(lspath, lsfile_gid);

                //Excel To DataTable

                lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";
                lspath = "erpdocument" + "/" + lscompany_code + "/SamAgro/P&L/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                msSQL = "select application_gid from agr_trn_tsuprfsaupload where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                    " and template_name='" + template_name + "'";
                string lsGID = objdbconn.GetExecuteScalar(msSQL);
                if (lsGID == null || lsGID == "")
                {
                    msGetGid = objcmnfunctions.GetMasterGID("FSAU");
                    msSQL = " insert into agr_trn_tsuprfsaupload(" +
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
                    msSQL = "delete from agr_trn_tsuprfsaupload where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                    " and template_name='" + template_name + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        msSQL = "delete from agr_trn_tsuprcreditprofitloss where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                   " and template_name='" + template_name + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msGetGid = objcmnfunctions.GetMasterGID("FSAU");
                        msSQL = " insert into agr_trn_tsuprfsaupload(" +
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

                        msSQL = " insert into agr_trn_tsuprcreditprofitloss(" +
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

        public void DaGetProfitLoss(string application_gid, string credit_gid, string template_name, MdlMstProfitLoss values)
        {
            msSQL = "select audited,domestic_sales,export_sales,totalgross_sales,excise_duty,otheroperating_income,other_income,net_sales,increasenet_sales, " +
                " import_rawmaterial,indigenous_rawmaterial,import_spares,indigenous_spares,power_fuel,direct_labour,othersoperating_expenses," +
                "  depreciation,repair_maintenance,rent,otherdirect_cost,totalcost_sales,openingstock_progress,closingstock_progress,costof_production,copnet_sales," +
                " openingstock_finishedgoods,closingstock_finishedgoods,costof_sales,cosnet_sales,sgam_expenses,pbit,pbitnet_sales,interest_finance_charges," +
                " interest_financenet_sales,pbt,pbtnet_sales,interest_earned,mics_receipts,divdend,profitsales_invetments,exchange_gain,total_nonoperative_income," +
                " wrieoff_provision,proir_year,baddebts_written_off,othernoncash_expenses,othernonoperating_exoeses,total_nonoperative_expenses,profitbefore_tax," +
                " current_tax,deferred_tax,pat,net_profitloss,amount,rate,retained_profit,template_name,application_gid,credit_gid,allfiguresin_inr from agr_trn_tsuprcreditprofitloss " +
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

        //FSA Summary

        public void DaGetFSASUmmary(string credit_gid, string application_gid, MdlMstFSASummary values)
        {
            msSQL = "select a.template_name,a.document_name,a.document_path,a.credit_gid,a.application_gid," +
                " date_format(a.created_date,'%d-%m-%Y %H:%i %p')as created_date,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                " from agr_trn_tsuprfsaupload a, " +
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
                        document_path = objcmnstorage.EncryptData(((dr_datarow["document_path"].ToString()))),
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
                //lsfilePath = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/SamAgro/Balance Sheet Template 1/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
                lsfilePath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/Balance Sheet Template 1/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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

                using (ExcelPackage xlPackage = new ExcelPackage(ms))
                {
                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];
                    rowCount = worksheet.Dimension.End.Row;
                    string endRange = worksheet.Dimension.End.Address;
                    colName = Regex.Replace(endRange, @"[\d-]", string.Empty);
                }

                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/Balance Sheet Template 1/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                file.Close();
                ms.Close();

                objcmnfunctions.uploadFile(lspath, lsfile_gid);

                //Excel To DataTable

                lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";

                lspath = "erpdocument" + "/" + lscompany_code + "/SamAgro/Balance Sheet Template 1/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                msSQL = "select application_gid from agr_trn_tsuprfsaupload where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                   " and template_name='" + lstemplate_type + "'";
                string lsGID = objdbconn.GetExecuteScalar(msSQL);
                if (lsGID == null || lsGID == "")
                {
                    msGetGid = objcmnfunctions.GetMasterGID("FSAU");
                    msSQL = " insert into agr_trn_tsuprfsaupload(" +
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
                    msSQL = "delete from agr_trn_tsuprfsaupload where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                    " and template_name='" + lstemplate_type + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        msSQL = "delete from agr_trn_tsuprcreditbalancesheet where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                   " and template_type='" + lstemplate_type + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msGetGid = objcmnfunctions.GetMasterGID("FSAU");
                        msSQL = " insert into agr_trn_tsuprfsaupload(" +
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

                        msSQL = " insert into agr_trn_tsuprcreditbalancesheet(" +
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
                    " from agr_trn_tsuprcreditbalancesheet a " +
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
                //lsfilePath = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/SamAgro/SummaryTemplate/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
                lsfilePath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/SummaryTemplate/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

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
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/SummaryTemplate/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                file.Close();
                ms.Close();


                objcmnfunctions.uploadFile(lspath, lsfile_gid);

                //Excel To DataTable                
                lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";
                excelRange = "A3:" + endRange;

                msSQL = "select application_gid from agr_trn_tsuprfsaupload where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                   " and template_name='" + template_name + "'";
                string lsGID = objdbconn.GetExecuteScalar(msSQL);

                lspath = "erpdocument" + "/" + lscompany_code + "/SamAgro/SummaryTemplate/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                if (lsGID == null || lsGID == "")
                {
                    msGetGid = objcmnfunctions.GetMasterGID("FSAU");
                    msSQL = " insert into agr_trn_tsuprfsaupload(" +
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
                    msSQL = "delete from agr_trn_tsuprfsaupload where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                    " and template_name='" + template_name + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        msSQL = "delete from agr_trn_tsuprsummarytemplate1 where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                                " and template_name='" + template_name + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msGetGid = objcmnfunctions.GetMasterGID("FSAU");
                        msSQL = " insert into agr_trn_tsuprfsaupload(" +
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

                        msSQL = " insert into agr_trn_tsuprsummarytemplate1 (" +
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

        public void DaGetSummaryTemplate1View(string credit_gid, string application_gid, string template_name, MdlSummaryTemplate1View values)
        {

            try
            {
                msSQL = " select date, audit_type, net_sales, other_income, total_revenue, growth_in_revenues, ebitda, ebitda_margin, depreciation," +
                        " interest, pat, pat_margin, total_outside_liabilities, total_bank_borrowings, tangible_net_worth, current_ratio, tol_tnw," +
                        " interest_coverage_ratio, dscr, sundry_creditors, sundry_debtors, inventories, payable_noofdays, recievable_noofdays, inventory_noofdays," +
                        " workingcapital_noofdays, debt_ebitda, msme" +
                        " from agr_trn_tsuprsummarytemplate1 " +
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
                //lsfilePath = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/SamAgro/Balance Sheet Template 2/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
                lsfilePath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/Balance Sheet Template 2/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

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

                using (ExcelPackage xlPackage = new ExcelPackage(ms))
                {
                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];
                    rowCount = worksheet.Dimension.End.Row;
                    string endRange = worksheet.Dimension.End.Address;
                    colName = Regex.Replace(endRange, @"[\d-]", string.Empty);
                }

                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/Balance Sheet Template 2/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                file.Close();
                ms.Close();


                objcmnfunctions.uploadFile(lspath, lsfile_gid);

                //Excel To DataTable

                lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";

                lspath = "erpdocument" + "/" + lscompany_code + "/SamAgro/Balance Sheet Template 2/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                msSQL = "select application_gid from agr_trn_tsuprfsaupload where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                   " and template_name='" + lstemplate_type + "'";
                string lsGID = objdbconn.GetExecuteScalar(msSQL);
                if (lsGID == null || lsGID == "")
                {
                    msGetGid = objcmnfunctions.GetMasterGID("FSAU");
                    msSQL = " insert into agr_trn_tsuprfsaupload(" +
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
                    msSQL = "delete from agr_trn_tsuprfsaupload where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                    " and template_name='" + lstemplate_type + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        msSQL = "delete from agr_trn_tsuprcreditbalancesheet2 where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                   " and template_type='" + lstemplate_type + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msGetGid = objcmnfunctions.GetMasterGID("FSAU");
                        msSQL = " insert into agr_trn_tsuprfsaupload(" +
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

                        msSQL = " insert into agr_trn_tsuprcreditbalancesheet2(" +
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
                    " from agr_trn_tsuprcreditbalancesheet2 a " +
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
                //lsfilePath = HttpContext.Current.Server.MapPath("erpdocument" + "/" + lscompany_code + "/SamAgro/P&LTemp2/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
                lsfilePath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/P&LTemp2/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                string colName;
                using (ExcelPackage xlPackage = new ExcelPackage(ms))
                {
                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];
                    rowCount = worksheet.Dimension.End.Row;
                    columnCount = worksheet.Dimension.End.Column;
                    string endRange = worksheet.Dimension.End.Address;
                    colName = Regex.Replace(endRange, @"[\d-]", string.Empty);
                }
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/P&LTemp2/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                file.Close();
                ms.Close();

                objcmnfunctions.uploadFile(lspath, lsfile_gid);

                //Excel To DataTable

                lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";
                lspath = "erpdocument" + "/" + lscompany_code + "/SamAgro/P&LTemp2/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                msSQL = "select application_gid from agr_trn_tsuprfsaupload where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                    " and template_name='" + template_name + "'";
                string lsGID = objdbconn.GetExecuteScalar(msSQL);
                if (lsGID == null || lsGID == "")
                {
                    msGetGid = objcmnfunctions.GetMasterGID("FSAU");
                    msSQL = " insert into agr_trn_tsuprfsaupload(" +
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
                    msSQL = "delete from agr_trn_tsuprfsaupload where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                    " and template_name='" + template_name + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        msSQL = "delete from agr_trn_tsuprcreditprofitlosstemp2 where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                   " and template_name='" + template_name + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msGetGid = objcmnfunctions.GetMasterGID("FSAU");
                        msSQL = " insert into agr_trn_tsuprfsaupload(" +
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

                        msSQL = " insert into agr_trn_tsuprcreditprofitlosstemp2(" +
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
                //lsfilePath = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/SamAgro/SummaryTemplate/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
                lsfilePath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "cc/SummaryTemplate/" + DateTime.Now.Year + "/" + DateTime.Now.Month;


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
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/SummaryTemplate/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                file.Close();
                ms.Close();


                objcmnfunctions.uploadFile(lspath, lsfile_gid);

                //Excel To DataTable                
                lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";
                excelRange = "A3:" + endRange;

                msSQL = "select application_gid from agr_trn_tsuprfsaupload where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                   " and template_name='" + template_name + "'";
                string lsGID = objdbconn.GetExecuteScalar(msSQL);

                lspath = "erpdocument" + "/" + lscompany_code + "/SamAgro/SummaryTemplate/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                if (lsGID == null || lsGID == "")
                {
                    msGetGid = objcmnfunctions.GetMasterGID("FSAU");
                    msSQL = " insert into agr_trn_tsuprfsaupload(" +
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
                    msSQL = "delete from agr_trn_tsuprfsaupload where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                    " and template_name='" + template_name + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        msSQL = "delete from agr_trn_tsuprsummarytemplate2 where application_gid='" + application_gid + "' and credit_gid='" + credit_gid + "'" +
                                " and template_name='" + template_name + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msGetGid = objcmnfunctions.GetMasterGID("FSAU");
                        msSQL = " insert into agr_trn_tsuprfsaupload(" +
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

                        msSQL = " insert into agr_trn_tsuprsummarytemplate2(" +
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

        public void DaGetSummaryTemplate2View(string credit_gid, string application_gid, string template_name, MdlSummaryTemplate2View values)
        {

            try
            {
                msSQL = " select date, audit_type, interest_earned, other_income, total_income, growth_in_income, interest_expenses, operating_expenses, provision_and_contingencies," +
                        " net_profit, total_debt, tangible_networth, net_interest_income, assets_undermanagement, nim, loan_disbursed, crar," +
                        " debt_equity, operational_selfsufficiency_ratio, costtoincome_ratio, returnon_avgassets, returnon_avgnetworth, gross_npa, net_npa, gross_npapercent," +
                        " net_npapercent, netnpa_networth" +
                        " from agr_trn_tsuprsummarytemplate2" +
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

        public void DaGetApplicationCreditAprovalinfo(string application_gid, MdlMstcreditApprovalInfo values)
        {
            msSQL = "select remarks from agr_mst_tsuprapplication  where application_gid ='" + application_gid + "'";
            values.approval_remarks = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select approval_gid, approval_name, approval_status, hierary_level, case when hierary_level = 0 then 'creditManager' " +
                    " when hierary_level = 1 then 'RegionalCreditManger' when hierary_level = 2 then 'NationalCreditManager' " +
                    " when hierary_level = 3 then 'CreditHead'  end as approver from agr_trn_tsuprAppcreditapproval  where application_gid ='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            List<creditApproval_list> creditapprovallist = new List<creditApproval_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    if (dt_datatable.Rows.Count == 1 && dr_datarow["approval_status"].ToString() == "Pending" && dr_datarow["hierary_level"].ToString() == "0")
                    {
                        msSQL = " select application_gid, credithead_gid ,credithead_name,creditnationalmanager_gid, creditnationalmanager_name, " +
                                " creditregionalmanager_gid, creditregionalmanager_name, creditmanager_gid, creditmanager_name,remarks " +
                                " from agr_mst_tsuprapplication where application_gid = '" + application_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            values.credithead_name = objODBCDatareader["credithead_name"].ToString();
                            values.credithead_gid = objODBCDatareader["credithead_gid"].ToString();
                            values.creditnationalmanager_name = objODBCDatareader["creditnationalmanager_name"].ToString();
                            values.creditnationalmanager_gid = objODBCDatareader["creditnationalmanager_gid"].ToString();
                            values.creditregionalmanager_name = objODBCDatareader["creditregionalmanager_name"].ToString();
                            values.creditregionalmanager_gid = objODBCDatareader["creditregionalmanager_gid"].ToString();
                            values.creditmanager_name = objODBCDatareader["creditmanager_name"].ToString();
                            values.creditmanager_gid = objODBCDatareader["creditmanager_gid"].ToString();
                            values.pendingapproval = "Y";
                        }
                        objODBCDatareader.Close();

                        return;
                    }

                    creditapprovallist.Add(new creditApproval_list
                    {
                        approval_gid = dr_datarow["approval_gid"].ToString(),
                        approval_name = dr_datarow["approval_name"].ToString(),
                        approval_status = dr_datarow["approval_status"].ToString(),
                        hierary_level = dr_datarow["hierary_level"].ToString(),
                        approver = dr_datarow["approver"].ToString(),
                    });
                }
            }
            values.creditApproval_list = creditapprovallist;
            dt_datatable.Dispose();
        }

        public void DaGetProfitLossTemp2List(string application_gid, string credit_gid, string template_name, MdlMstProfitLosstemp2 values)
        {
            msSQL = " select credirprofitlosstemp2_gid, audited, interest_income, incomeon_investments, interest_others, income_others, " +
                    " totalinterest_earned, other_income, profit_sales, miscellaneous_income, totalother_income, total_income, increase_income, expenditure, " +
                    " expenedinterest_borrower, expenedinterest_deposit, expened_other, totalinterest_expened, operating_expenses, employee_cost, depreciation, " +
                    " other_operating_cost, total_operating_expenses, provision_asset, provision_nonasset, provision_tax, other_provision, total_provision, " +
                    " total_expenditure, pbt, income_tax, pat, amount, rent, retained_profit, allfiguresin_inr, " +
                    " template_name, application_gid, credit_gid from agr_trn_tsuprcreditprofitlosstemp2 " +
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

        public void DaGetPANForm60TempList(string contact_gid, string employee_gid, MdlContactPANForm60 values)
        {
            msSQL = "select contact2panform60_gid,document_name, document_path from agr_mst_tsuprcontact2panform60 where " +
             " contact_gid='" + employee_gid + "' or contact_gid = '" + contact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcontactpanform60_list = new List<contactpanform60_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcontactpanform60_list.Add(new contactpanform60_list
                    {
                        contact2panform60_gid = (dr_datarow["contact2panform60_gid"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = objcmnstorage.EncryptData(((dr_datarow["document_path"].ToString()))),
                    });

                    values.contactpanform60_list = getcontactpanform60_list;
                }
                dt_datatable.Dispose();
            }
        }

        public void DaGetPANForm60List(string contact_gid, string employee_gid, MdlContactPANForm60 values)
        {
            msSQL = "select contact2panform60_gid,document_name, document_path from agr_mst_tsuprcontact2panform60 where " +
             " contact_gid='" + contact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcontactpanform60_list = new List<contactpanform60_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcontactpanform60_list.Add(new contactpanform60_list
                    {
                        contact2panform60_gid = (dr_datarow["contact2panform60_gid"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = objcmnstorage.EncryptData(((dr_datarow["document_path"].ToString()))),
                    });

                    values.contactpanform60_list = getcontactpanform60_list;
                }
                dt_datatable.Dispose();
            }
        }

        public void DaContactPANAbsenceReasonList(string contact_gid, string employee_gid, MdlPANAbsenceReason objMdlPANAbsenceReason)
        {
            try
            {
                msSQL = " SELECT panabsencereason" +
                   " from agr_mst_tsuprcontact2panabsencereason where contact_gid = '" + contact_gid + "' or contact_gid = '" + employee_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcontactpanabsencereason_list = new List<contactpanabsencereason_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    objMdlPANAbsenceReason.contactpanabsencereason_list = dt_datatable.AsEnumerable().Select(row =>
                      new contactpanabsencereason_list
                      {
                          panabsencereason = row["panabsencereason"].ToString(),
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();
                objMdlPANAbsenceReason.status = true;
            }
            catch (Exception ex)
            {
                objMdlPANAbsenceReason.status = false;
            }

        }

        public void DaUpdatePANAbsenceReasons(MdlPANAbsenceReason values, string employee_gid)
        {
            try
            {
                matchCount1 = 0;
                matchCount2 = 0;

                msSQL = " select panabsencereason from agr_mst_tsuprcontact2panabsencereason" +
                        " where contact_gid='" + values.contact_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    values.contactpanabsencereason_list = dt_datatable.AsEnumerable().Select(row =>
                     new contactpanabsencereason_list
                     {
                         panabsencereason = row["panabsencereason"].ToString(),
                     }
                   ).ToList();
                }
                dt_datatable.Dispose();

                for (var i = 0; i < values.panabsencereason_selectedlist.Count; i++)
                {
                    for (var j = 0; j < values.contactpanabsencereason_list.Count; j++)
                    {
                        if (values.panabsencereason_selectedlist[i] == values.contactpanabsencereason_list[j].panabsencereason)
                        {
                            matchCount1++;
                        }
                    }
                    if (matchCount1 == 0)
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("C2PR");
                        msSQL = " INSERT INTO agr_mst_tsuprcontact2panabsencereason(" +
                                " contact2panabsencereason_gid," +
                                " contact_gid," +
                                " panabsencereason," +
                                " created_date," +
                                " created_by)" +
                                " VALUES(" +
                                "'" + msGetGid + "'," +
                                "'" + employee_gid + "'," +
                                "'" + values.panabsencereason_selectedlist[i] + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                "'" + employee_gid + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    matchCount1 = 0;
                }

                for (var i = 0; i < values.contactpanabsencereason_list.Count; i++)
                {
                    for (var j = 0; j < values.panabsencereason_selectedlist.Count; j++)
                    {
                        if (values.contactpanabsencereason_list[i].panabsencereason == values.panabsencereason_selectedlist[j])
                        {
                            matchCount2++;
                        }
                    }
                    if (matchCount2 == 0)
                    {
                        msSQL = "delete from agr_mst_tsuprcontact2panabsencereason where panabsencereason='" + values.contactpanabsencereason_list[i].panabsencereason + "' and contact_gid = '" + values.contact_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    matchCount2 = 0;
                }


                values.status = true;
                values.message = "PAN Absence Reasons Updated successfully...";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        public void DaEditPANAbsenceReasonList(string contact_gid, MdlPANAbsenceReason values)
        {
            try
            {
                msSQL = " SELECT panabsencereason" +
                   " from agr_mst_tpanabsencereason";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var panabsencereason_existinglist = new List<panabsencereason_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    values.panabsencereason_list = dt_datatable.AsEnumerable().Select(row =>
                     new panabsencereason_list
                     {
                         panabsencereason = row["panabsencereason"].ToString(),
                     }
                   ).ToList();
                }
                dt_datatable.Dispose();

                msSQL = " SELECT panabsencereason" +
                   " from agr_mst_tsuprcontact2panabsencereason where contact_gid = '" + contact_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var panabsencereason_contactlist = new List<panabsencereason_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    panabsencereason_contactlist = dt_datatable.AsEnumerable().Select(row =>
                      new panabsencereason_list
                      {
                          panabsencereason = row["panabsencereason"].ToString(),
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();

                for (var i = 0; i < values.panabsencereason_list.Count; i++)
                {
                    for (var j = 0; j < panabsencereason_contactlist.Count; j++)
                    {
                        if (values.panabsencereason_list[i].panabsencereason == panabsencereason_contactlist[j].panabsencereason)
                        {
                            values.panabsencereason_list[i].check_status = true;
                            break;
                        }
                    }
                }

                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }

        }

        public bool DaPANForm60DocumentUpload(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            String path = lspath;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/PANForm60Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                        //lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SamAgro/PANForm60Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        //FileStream file = new FileStream(lspath + httpPostedFile.FileName, FileMode.Create, FileAccess.Write);
                        //ms.WriteTo(file);
                        //file.Close();
                        //ms.Close();
                        //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "SamAgro/PANForm60Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/PANForm60Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/PANForm60Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        msGetGid = objcmnfunctions.GetMasterGID("CF60");
                        msSQL = " insert into agr_mst_tsuprcontact2panform60(" +
                                " contact2panform60_gid," +
                                " contact_gid," +
                                " document_name," +
                                " document_path," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid + "'," +
                                "'" + employee_gid + "'," +
                                "'" + httpPostedFile.FileName + "'," +
                                "'" + lspath + httpPostedFile.FileName + "'," +
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

        public void DaPANForm60Delete(string contact2panform60_gid, MdlContactPANForm60 values)
        {
            msSQL = "delete from agr_mst_tsuprcontact2panform60 where contact2panform60_gid='" + contact2panform60_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Form-60 Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }
        public void DaPANReasonsCheck(MdlPANAbsenceReason objMdlPANAbsenceReason, string employee_gid)
        {
            try
            {
                msSQL = " SELECT count(panabsencereason)" +
                   " from agr_mst_tsuprcontact2panabsencereason" +
                   " where contact_gid='" + employee_gid + "'";

                string lspanabsencereason_count = objdbconn.GetExecuteScalar(msSQL);

                if (int.Parse(lspanabsencereason_count) > 0)
                {
                    objMdlPANAbsenceReason.status = true;
                }
                else
                {
                    objMdlPANAbsenceReason.status = false;
                }
            }
            catch (Exception ex)
            {
                objMdlPANAbsenceReason.status = false;
            }
        }
    }
}