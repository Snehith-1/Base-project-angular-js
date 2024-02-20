using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.mastersamagro.Models;
using System.Configuration;
using ems.storage.Functions;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System.Globalization;
using OfficeOpenXml;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading;
using ems.hbapiconn.Functions;
using ems.hbapiconn.Models;

namespace ems.mastersamagro.DataAccess
{

    /// <summary>
    /// This DataAccess will provide access to edit single and multiple datas in Supplier Onboard stage.  (Includes editing of onboarded buyer general, company & individual info)
    /// </summary>
    /// <remarks>Written by Sherin Augusta.A, Premchander.K </remarks>
    public class DaAgrMstSupplierOnboardEdit
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        HttpPostedFile httpPostedFile;
        string msSQL, msGetGid, msGetDocumentGid, msGetGidpan;
        string lsemployee_name, lsapp_refno;

        int mnResult;

        string lsapplication_gid, lsapplication_no, lscompany_name, lsdate_incorporation, lscompanypan_no, lsyear_business, lsmonth_business, lscin_no;
        string lsofficial_telephoneno, lsofficial_mailid, lscompanytype_gid, lscompanytype_name, lsstakeholder_type, lsstakeholdertype_gid, lsassessmentagency_gid;
        string lsassessmentagency_name, lsassessmentagencyrating_gid, lsassessmentagencyrating_name, lsratingas_on, lsamlcategory_gid, lsamlcategory_name;
        string lsbusinesscategory_gid, lsbusinesscategory_name, lscontactperson_firstname, lscontactperson_middlename, lscontactperson_lastname, lsdesignation_gid, lsdesignation_name;
        string lsdesignation, lslastyear_turnover, lsescrow, lsstart_date, lsend_date, lsbusinessstart_date, lsemail_address, lsmobileno;
        string lsprimary_emailaddress, lsapplication2email_gid, lsprimary_mobileno, lswhatsapp_mobileno, lsapplication2contact_gid;
        string lsgst_state, lsgst_no, lsgst_registered, lsinstitution2branch_gid, lsprimary_status, lswhatsapp_no, lsinstitution2mobileno_gid, lsinstitution2email_gid;
        string lsaddress_typegid, lsaddress_type, lsaddressline1, lsaddressline2, lslandmark, lspostal_code, lscity, lstaluka, lsdistrict, lsstate, lscountry, lslatitude, lslongitude, lsinstitution2address_gid;

        string lspan_no, lsaadhar_no, lsfirst_name, lsmiddle_name, lslast_name, lsindividual_dob, lsage, lsgender_gid, lsgender_name, lseducationalqualification_gid,
              lseducationalqualification_name, lsmain_occupation, lsannual_income, lsmonthly_income, lspep_status, lspepverified_date, lsmaritalstatus_gid,
              lsmaritalstatus_name, lsfather_firstname, lsfather_middlename, lsfather_lastname, lsfather_dob, lsfather_age, lscontact2mobileno_gid,
              lsmother_firstname, lsmother_middlename, lsmother_lastname, lsmother_dob, lsmother_age, lsspouse_firstname, lsspouse_middlename, lsspouse_lastname,
              lsspouse_dob, lsspouse_age, lsownershiptype_gid, lsownershiptype_name, lsresidencetype_gid, lsresidencetype_name, lscurrentresidence_years, lsbranch_distance;

        string lscustomer_urn, lscustomer_name, lsvertical_gid, lsvertical_name, lsmobile_no, lscontact2email_gid, lscontact2address_gid,
                     lstan_number, lsconstitution_gid, lsconstitution_name, lsbusinessunit_gid, lsbusinessunit_name, lssa_status, lssa_id, lssa_name, lsvernacularlanguage_gid,
                         lsvernacular_language, lscontactpersonfirst_name, lscontactpersonmiddle_name, lscontactpersonlast_name, lsdesignation_type, lslandline_no;
        string lspropertyholder_gid, lspropertyholder_name, lsincometype_gid, lsincometype_name, lspreviouscrop, lsprposedcrop, lsinstitution_name;
        string lsgroup_gid, lsgroup_name, lsprofile, lsurn_status, lsurn, lsfathernominee_status, lsmothernominee_status, lsspousenominee_status, lsothernominee_status,
        lsrelationshiptype, lsnomineefirst_name, lsnominee_middlename, lsnominee_lastname, lsnominee_dob, lsnominee_age, lstotallandinacres, lscultivatedland, lsregion;
        string lsprogram, lsprogram_gid, lscreditgroup_gid, lscreditgroup_name, lsprogram_name, lscontact_gid;
        string lsproduct_gid, lsproduct_name, lsvariety_gid, lsvariety_name, lssector_name, lscategory_name, lsbotanical_name, lsalternative_name, lsmsme_registration, lslglentity_id, lslei_renewaldate, lskin;

        int matchCount1, matchCount2;
        string lspan_status, lsinstitution_gid;
        string lsbuyersuppliertype_gid, lsbuyersuppliertype_name;

        FnSamAgroHBAPIConn objFnSamAgroHBAPIConn = new FnSamAgroHBAPIConn();
        FnSamAgroHBAPIConnEdit objFnSamAgroHBAPIConnEdit = new FnSamAgroHBAPIConnEdit();

        public void DaGetGeneralInfoEdit(string application_gid, string employee_gid, MdlMstBuyerOnboardApplicationAdd values)
        {
            //msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            //lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select application_gid,if(application_no is null,'-',application_no) as application_no,customerref_name as customer_name,customer_urn,social_capital," +
                    " vertical_name,trade_capital,overalllimit_amount,processing_fee,doc_charges,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,status,applicant_type,hypothecation_flag,productcharge_flag, approval_submittedflag, " +
                    " product_gid,variety_gid from agr_mst_tsupronboard a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " where a.application_gid='" + application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.application_gid = objODBCDatareader["application_gid"].ToString();
                values.social_capital = objODBCDatareader["social_capital"].ToString();
                values.trade_capital = objODBCDatareader["trade_capital"].ToString();
                values.overalllimit_amount = objODBCDatareader["overalllimit_amount"].ToString();
                values.processing_fee = objODBCDatareader["processing_fee"].ToString();
                values.doc_charges = objODBCDatareader["doc_charges"].ToString();
                values.application_no = objODBCDatareader["application_no"].ToString();
                values.customer_name = objODBCDatareader["customer_name"].ToString();
                values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                values.vertical_name = objODBCDatareader["vertical_name"].ToString();
                values.created_by = objODBCDatareader["created_by"].ToString();
                values.created_date = objODBCDatareader["created_date"].ToString();
                values.application_status = objODBCDatareader["status"].ToString();
                values.applicant_type = objODBCDatareader["applicant_type"].ToString();
                values.hypothecation_flag = objODBCDatareader["hypothecation_flag"].ToString();
                values.productcharge_flag = objODBCDatareader["productcharge_flag"].ToString();
                values.product_gid = objODBCDatareader["product_gid"].ToString();
                values.variety_gid = objODBCDatareader["variety_gid"].ToString();
                values.approval_submittedflag = objODBCDatareader["approval_submittedflag"].ToString();

            }
            objODBCDatareader.Close();
            values.status = true;
        }

        public void DaGetInstitutionSummaryEdit(string application_gid, string employee_gid, MdlBuyerOnboardCICInstitution values)
        {
            //msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            //lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

            //if (lsapplication_gid != "")
            //{
            msSQL = " select institution_gid,company_name,date_incorporation,stakeholder_type,institution_status,businessstart_date," +
               " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
               " from agr_mst_tsupronboard2institution a " +
               " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
               " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
               " where a.application_gid='" + application_gid + "' order by institution_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcicinstitutionList = new List<BuyerOnboardcicinstitution_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcicinstitutionList.Add(new BuyerOnboardcicinstitution_list
                    {
                        institution_gid = dt["institution_gid"].ToString(),
                        company_name = dt["company_name"].ToString(),
                        date_incorporation = dt["date_incorporation"].ToString(),
                        stakeholder_type = dt["stakeholder_type"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        institution_status = dt["institution_status"].ToString(),
                        businessstart_date = dt["businessstart_date"].ToString(),
                    });

                }
            }
            values.BuyerOnboardcicinstitution_list = getcicinstitutionList;
            dt_datatable.Dispose();

            values.status = true;

        }

        public void DaGetIndividualSummaryEdit(string application_gid, string employee_gid, MdlBuyerOnboardCICIndividual values)
        {
            //msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            //lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            //if (lsapplication_gid != "")
            //{
            msSQL = " select contact_gid,concat(first_name, ' ',middle_name,' ',last_name) as individual_name," +
                    " a.pan_no,aadhar_no,stakeholder_type,contact_status,institution_name,group_name," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                    " from agr_mst_tsupronboardcontact a " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " where a.application_gid='" + application_gid + "' order by contact_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcicindividualList = new List<BuyerOnboardcicindividual_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcicindividualList.Add(new BuyerOnboardcicindividual_list
                    {
                        contact_gid = dt["contact_gid"].ToString(),
                        individual_name = dt["individual_name"].ToString(),
                        pan_no = dt["pan_no"].ToString(),
                        aadhar_no = dt["aadhar_no"].ToString(),
                        stakeholder_type = dt["stakeholder_type"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        contact_status = dt["contact_status"].ToString(),
                        institution_name = dt["institution_name"].ToString(),
                        group_name = dt["group_name"].ToString(),
                    });

                }
            }
            values.BuyerOnboardcicindividual_list = getcicindividualList;
            dt_datatable.Dispose();

            values.status = true;

        }


        public bool DaSubmitInstitutionDtlEdit(MdlMstBuyerOnboardInstitutionAdd values, string employee_gid)
        {
            msSQL = "select institution_gid from agr_mst_tsupronboardinstitution2mobileno where institution_gid='" + employee_gid + "' and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Mobile Number";
                return false;
            }
            objODBCDatareader.Close();
            msSQL = "select institution_gid from agr_mst_tsupronboardinstitution2mobileno where institution_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Mobile Number";
                return false;
            }
            objODBCDatareader.Close();
            msSQL = "select institution_gid from agr_mst_tsupronboardinstitution2email where institution_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Email Address";
                return false;
            }
            objODBCDatareader.Close();
            msSQL = "select institution_gid from agr_mst_tsupronboardinstitution2address where institution_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Address Detail";
                return false;
            }
            objODBCDatareader.Close();
            //msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            //lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select institution_gid from agr_mst_tbyronboardinstitution2mobileno where (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "') and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Primary Mobile Number";
                return false;
            }

            msSQL = "select institution_gid from agr_mst_tsupronboardinstitution2address where (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "') and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Primary Email Address";
                return false;
            }

            msSQL = "select institution_gid from agr_mst_tsupronboardinstitution2email where (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "') and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Primary Email Address";
                return false;
            }


            if (values.Gstflag == "Yes")
            {

                msSQL = "select institution2branch_gid from agr_mst_tsupronboardinstitution2branch where institution_gid='" + employee_gid + "' and headoffice_status ='Yes' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == false)
                {
                    values.status = false;
                    values.message = "Atleast Select One GST Number as Head Office";
                    return false;
                }
                objODBCDatareader.Close();
            }

            msSQL = "select stakeholder_type from agr_mst_tsupronboardcontact where application_gid='" + values.application_gid + "' and stakeholder_type in ('Borrower','Applicant')";
            string lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholder_type)
            {

                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return false;
            }

            msSQL = "select stakeholder_type from agr_mst_tsupronboard2institution where application_gid='" + values.application_gid + "' and stakeholder_type in ('Borrower','Applicant')";
            lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholder_type)
            {

                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("APIN");
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
                  " lei_renewaldate," +
                " msme_registration," +
                " lglentity_id," +
                " kin," +
                " created_by," +
                " created_date) values(" +
                  "'" + msGetGid + "'," +
                  "'" + values.application_gid + "'," +
                  "'" + values.company_name.Replace("'", "") + "',";
            if ((values.date_incorporation == null) || (values.date_incorporation == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.date_incorporation).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if ((values.businessstartdate == null) || (values.businessstartdate == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.businessstartdate).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + values.year_business + "'," +
                    "'" + values.month_business + "'," +
                    "'" + values.companypan_no + "'," +
                    "'" + values.cin_no + "'," +
                    "'" + values.official_telephoneno + "'," +
                    "'" + values.official_mailid + "'," +
                    "'" + values.companytype_gid + "'," +
                    "'" + values.companytype_name + "'," +
                    "'" + values.stakeholdertype_gid + "'," +
                    "'" + values.stakeholder_type + "'," +
                    "'" + values.assessmentagency_gid + "'," +
                    "'" + values.assessmentagency_name + "'," +
                    "'" + values.assessmentagencyrating_gid + "'," +
                    "'" + values.assessmentagencyrating_name + "',";
            if ((values.ratingas_on == null) || (values.ratingas_on == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.ratingas_on).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + values.amlcategory_gid + "'," +
                    "'" + values.amlcategory_name + "'," +
                    "'" + values.businesscategory_gid + "'," +
                    "'" + values.businesscategory_name + "'," +
                    "'" + values.contactperson_firstname + "'," +
                    "'" + values.contactperson_middlename + "'," +
                    "'" + values.contactperson_lastname + "'," +
                    "'" + values.designation_gid + "'," +
                    "'" + values.designation + "',";
            if ((values.start_date == null) || (values.start_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.start_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if ((values.end_date == null) || (values.end_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.end_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + values.lastyear_turnover + "'," +
                    "'" + values.escrow + "'," +
                    "'" + values.urn_status + "'," +
                    "'" + values.urn + "'," +
                    "'Completed'," +
                    "'" + values.tan_number + "'," +
                    "'" + values.incometax_returnsstatus + "',";
            if (values.revenue == null || values.revenue == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.revenue.Replace(",", "") + "',";
            }
            if (values.profit == null || values.profit == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.profit.Replace(",", "") + "',";
            }
            if (values.fixed_assets == null || values.fixed_assets == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.fixed_assets.Replace(",", "") + "',";
            }
            if (values.sundrydebt_adv == null || values.sundrydebt_adv == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.sundrydebt_adv.Replace(",", "") + "',";
            }
            if ((values.lei_renewaldate == null) || (values.lei_renewaldate == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.lei_renewaldate).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL +=
                "'" + values.msme_registration + "'," +
                "'" + values.lglentity_id + "'," +
                "'" + values.kin + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "update agr_mst_tsupronboardinstitution2branch set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardinstitution2mobileno set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardinstitution2email set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardinstitution2address set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardinstitution2licensedtl set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardinstitution2ratingdetail set institution_gid='" + msGetGid + "', application_gid ='" + values.application_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardinstitution2bankdtl set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_trn_ttandtl set function_gid='" + msGetGid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                msSQL = "update agr_mst_tsupronboardinstitution2documentupload set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardinstitution2form60documentupload set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycgstsbpan set function_gid ='" + msGetGid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                msSQL = "select mobile_no from agr_mst_tsupronboardinstitution2mobileno where institution_gid='" + msGetGid + "' and primary_status='yes'";
                lsmobileno = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select email_address from agr_mst_tsupronboardinstitution2email where institution_gid='" + msGetGid + "' and primary_status='yes'";
                lsemail_address = objdbconn.GetExecuteScalar(msSQL);
                if (values.stakeholder_type == "Borrower" || values.stakeholder_type == "Applicant")
                {
                    msSQL = "update agr_mst_tsupronboard set applicant_type ='Institution' where application_gid='" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsupronboard2institution set mobile_no='" + lsmobileno + "'," +
                     " email_address='" + lsemail_address + "' where institution_gid='" + msGetGid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }


                values.message = "Institution Information Submitted Successfully";
                values.status = true;
                return true;

            }
            else
            {
                values.message = "Error Occured";
                values.status = false;
                return false;
            }
        }

        public void DaDeleteIndividualAddress(string contact2address_gid, MdlBuyerOnboardContactAddress values)
        {
            msSQL = "delete from agr_mst_tsupronboardcontact2address where contact2address_gid='" + contact2address_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tsupronboardcontact2addressupdatelog where contact2address_gid='" + contact2address_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Address Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }


        public void DaIndividualSubmit(string employee_gid, MdlMstBuyerOnboardContact values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("CTCT");

            msSQL = "select stakeholder_type from agr_mst_tsupronboardcontact where application_gid='" + values.application_gid + "' and stakeholder_type in ('Borrower','Applicant')";
            string lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholdertype_name)
            {

                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return;
            }

            msSQL = "select stakeholder_type from agr_mst_tsupronboard2institution where application_gid='" + values.application_gid + "' and stakeholder_type in ('Borrower','Applicant')";
            lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholdertype_name)
            {

                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return;
            }

            msSQL = "select contact_gid from agr_mst_tsupronboardcontact2mobileno where contact_gid='" + employee_gid + "' and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Mobile Number ";
                return;
            }
            objODBCDatareader.Close();

            msSQL = "select contact_gid from agr_mst_tsupronboardcontact2email where contact_gid='" + employee_gid + "' and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add primary Email Address";
                return;
            }
            objODBCDatareader.Close();
            msSQL = "select contact_gid from agr_mst_tsupronboardcontact2address where contact_gid='" + employee_gid + "' and primary_status='Yes' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add primary Address";
                return;
            }
            objODBCDatareader.Close();
            msSQL = " insert into agr_mst_tsupronboardcontact(" +
                   " contact_gid," +
                   " application_gid," +
                   " application_no," +
                   " pan_status, " +
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
                   "'" + values.application_gid + "'," +
                   "'" + employee_gid + "'," +
                   "'" + values.pan_status + "'," +
                   "'" + values.pan_no + "'," +
                   "'" + values.aadhar_no + "',";
            if (values.first_name == "" || values.first_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.first_name.Replace("'", "") + "',";
            }
            if (values.middle_name == "" || values.middle_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.middle_name.Replace("'", "") + "',";
            }
            if (values.last_name == "" || values.last_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.last_name.Replace("'", "") + "',";
            }
            msSQL += "'" + values.individual_dob + "'," +
                     "'" + values.age + "'," +
                     "'" + values.gender_gid + "'," +
                     "'" + values.gender_name + "'," +
                     "'" + values.designation_gid + "'," +
                     "'" + values.designation_name + "'," +
                     "'" + values.educationalqualification_gid + "'," +
                     "'" + values.educationalqualification_name + "'," +
                     "'" + values.main_occupation + "'," +
                     "'" + values.annual_income + "'," +
                     "'" + values.monthly_income + "'," +
                     "'" + values.pep_status + "',";

            if ((values.pepverified_date == null) || (values.pepverified_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.pepverified_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }



            msSQL += "'" + values.stakeholdertype_gid + "'," +
                "'" + values.stakeholdertype_name + "'," +
                     "'" + values.maritalstatus_gid + "'," +
                     "'" + values.maritalstatus_name + "',";
            if (values.father_firstname == "" || values.father_firstname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.father_firstname.Replace("'", "") + "',";
            }
            if (values.father_middlename == "" || values.father_middlename == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.father_middlename.Replace("'", "") + "',";
            }
            if (values.father_lastname == "" || values.father_lastname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.father_lastname.Replace("'", "") + "',";
            }
            msSQL += "'" + values.father_dob + "'," +
                     "'" + values.father_age + "',";
            if (values.mother_firstname == "" || values.mother_firstname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.mother_firstname.Replace("'", "") + "',";
            }
            if (values.mother_middlename == "" || values.mother_middlename == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.mother_middlename.Replace("'", "") + "',";
            }
            if (values.mother_lastname == "" || values.mother_lastname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.mother_lastname.Replace("'", "") + "',";
            }
            msSQL += "'" + values.mother_dob + "'," +
                     "'" + values.mother_age + "',";
            if (values.spouse_firstname == "" || values.spouse_firstname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.spouse_firstname.Replace("'", "") + "',";
            }
            if (values.spouse_middlename == "" || values.spouse_middlename == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.spouse_middlename.Replace("'", "") + "',";
            }
            if (values.spouse_lastname == "" || values.spouse_lastname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.spouse_lastname.Replace("'", "") + "',";
            }
            msSQL += "'" + values.spouse_dob + "'," +
                     "'" + values.spouse_age + "'," +
                     "'" + values.ownershiptype_gid + "'," +
                     "'" + values.ownershiptype_name + "'," +
                     "'" + values.propertyholder_gid + "'," +
                     "'" + values.propertyholder_name + "'," +
                     "'" + values.residencetype_gid + "'," +
                     "'" + values.residencetype_name + "'," +
                     "'" + values.incometype_gid + "'," +
                     "'" + values.incometype_name + "'," +
                     "'" + values.currentresidence_years + "'," +
                     "'" + values.branch_distance + "'," +
                     "'" + values.group_gid + "'," +
                     "'" + values.group_name + "'," +
                     "'" + values.profile + "'," +
                     "'" + values.urn_status + "'," +
                     "'" + values.urn + "'," +
                     "'" + values.fathernominee_status + "'," +
                     "'" + values.mothernominee_status + "'," +
                     "'" + values.spousenominee_status + "'," +
                     "'" + values.othernominee_status + "'," +
                     "'" + values.relationshiptype + "',";
            if (values.nomineefirst_name == "" || values.nomineefirst_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.nomineefirst_name.Replace("'", "") + "',";
            }
            if (values.nominee_middlename == "" || values.nominee_middlename == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.nominee_middlename.Replace("'", "") + "',";
            }
            if (values.nominee_lastname == "" || values.nominee_lastname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.nominee_lastname.Replace("'", "") + "',";
            }
            msSQL += "'" + values.nominee_dob + "'," +
                     "'" + values.nominee_age + "'," +
                     "'" + values.totallandinacres + "'," +
                     "'" + values.cultivatedland + "'," +
                     "'" + values.previouscrop + "'," +
                     "'" + values.prposedcrop + "'," +
                     "'" + values.institution_gid + "'," +
                     "'" + values.institution_name + "'," +
                     "'Completed'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                // PAN Update
                if (values.pan_status == "Customer Submitting Form 60")
                {
                    foreach (string reason in values.panabsencereason_selectedlist)
                    {
                        msGetGidpan = objcmnfunctions.GetMasterGID("C2PR");
                        msSQL = " INSERT INTO agr_mst_tsupronboardcontact2panabsencereason(" +
                               " contact2panabsencereason_gid," +
                               " contact_gid," +
                               " panabsencereason," +
                               " created_date," +
                               " created_by)" +
                               " VALUES(" +
                               "'" + msGetGidpan + "'," +
                               "'" + msGetGid + "'," +
                               "'" + reason + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               "'" + employee_gid + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                //Updates

                msSQL = "update agr_mst_tsupronboardcontact2mobileno set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2email set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2address set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2idproof set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2panform60 set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2panabsencereason set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2bankdtl set contact_gid='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2document set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpanauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpanaadhaarlink set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycdlauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycepicauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpassportauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                msSQL = "select mobile_no from agr_mst_tsupronboardcontact2mobileno where contact_gid='" + msGetGid + "' and primary_status='yes'";
                lsmobileno = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select email_address from agr_mst_tsupronboardcontact2email where contact_gid='" + msGetGid + "' and primary_status='yes'";
                lsemail_address = objdbconn.GetExecuteScalar(msSQL);

                if (values.stakeholdertype_name == "Borrower" || values.stakeholdertype_name == "Applicant")
                {
                    msSQL = "update agr_mst_tsupronboard set applicant_type ='Individual' where application_gid='" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsupronboardcontact set mobile_no='" + lsmobileno + "'," +
                        " email_address='" + lsemail_address + "' where contact_gid='" + msGetGid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                values.status = true;
                values.message = "Individual Details Submitted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }


        }



        public void DaInstitutionDetailsEdit(string institution_gid, MdlMstBuyerOnboardInstitutionAdd values)
        {
            try
            {
                msSQL = " select application_gid, application_no, company_name, date_incorporation, businessstart_date, companypan_no, year_business, month_business, cin_no," +
                   " official_telephoneno, officialemail_address, companytype_gid, companytype_name, stakeholder_type, stakeholdertype_gid, assessmentagency_gid, " +
                   " assessmentagency_name, assessmentagencyrating_gid, assessmentagencyrating_name, ratingas_on, amlcategory_gid, amlcategory_name,businesscategory_gid, " +
                   " businesscategory_name, contactperson_firstname, contactperson_middlename, contactperson_lastname, designation_gid, designation,  msme_registration, lglentity_id, lei_renewaldate, kin, " +
                   " lastyear_turnover, escrow, start_date, end_date, institution_status, urn, urn_status,tan_number, incometax_returnsstatus, revenue, profit, fixed_assets, sundrydebt_adv  " +
                   " from agr_mst_tsupronboard2institution where institution_gid='" + institution_gid + "'";
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
                    values.tan_number = objODBCDatareader["tan_number"].ToString();
                    values.revenue = objODBCDatareader["revenue"].ToString();
                    values.incometax_returnsstatus = objODBCDatareader["incometax_returnsstatus"].ToString();
                    values.profit = objODBCDatareader["profit"].ToString();
                    values.fixed_assets = objODBCDatareader["fixed_assets"].ToString();
                    values.sundrydebt_adv = objODBCDatareader["sundrydebt_adv"].ToString();
                    values.stakeholder_type = objODBCDatareader["stakeholder_type"].ToString();
                    if (objODBCDatareader["lei_renewaldate"].ToString() == "")
                    {
                    }
                    else
                    {
                        values.editlei_renewaldate = Convert.ToDateTime(objODBCDatareader["lei_renewaldate"]).ToString("dd-MM-yyyy");
                    }
                    values.msme_registration = objODBCDatareader["msme_registration"].ToString();
                    values.lglentity_id = objODBCDatareader["lglentity_id"].ToString();
                    values.kin = objODBCDatareader["kin"].ToString();
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


        public void DaInstitutionGSTList(string institution_gid, string employee_gid, MdlMstBuyerOnboardGST values)
        {
            msSQL = "select institution2branch_gid,gst_state,gst_no, gst_registered,authentication_status,returnfilling_status,verification_status, headoffice_status from agr_mst_tsupronboardinstitution2branch where institution_gid='" + institution_gid + "'or institution_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstgst_list = new List<mstBuyerOnboardgst_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstgst_list.Add(new mstBuyerOnboardgst_list
                    {
                        institution2branch_gid = (dr_datarow["institution2branch_gid"].ToString()),
                        gst_state = (dr_datarow["gst_state"].ToString()),
                        gst_no = (dr_datarow["gst_no"].ToString()),
                        gst_registered = (dr_datarow["gst_registered"].ToString()),
                        authentication_status = (dr_datarow["authentication_status"].ToString()),
                        returnfilling_status = (dr_datarow["returnfilling_status"].ToString()),
                        verification_status = (dr_datarow["verification_status"].ToString()),
                        headoffice_status = (dr_datarow["headoffice_status"].ToString())

                    });
                }
                values.mstBuyerOnboardgst_list = getmstgst_list;
            }
            dt_datatable.Dispose();
        }

        public void DaInstitutionMobileNoList(string institution_gid, string employee_gid, MdlMstBuyerOnboardMobileNo values)
        {
            msSQL = "select mobile_no,institution2mobileno_gid,primary_status,whatsapp_no from agr_mst_tsupronboardinstitution2mobileno where " +
              " institution_gid='" + institution_gid + "'or institution_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstmobileno_list = new List<mstBuyerOnboardmobileno_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstmobileno_list.Add(new mstBuyerOnboardmobileno_list
                    {
                        institution2mobileno_gid = (dr_datarow["institution2mobileno_gid"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        whatsapp_no = (dr_datarow["whatsapp_no"].ToString()),
                        
                    });
                }
                values.mstBuyerOnboardmobileno_list = getmstmobileno_list;
            }
            dt_datatable.Dispose();
        }

        public void DaInstitutionEmailAddressList(string institution_gid, string employee_gid, MdlMstBuyerOnboardEmailAddress values)
        {
            msSQL = "select email_address,institution2email_gid,primary_status from agr_mst_tsupronboardinstitution2email where institution_gid='" + institution_gid + "'or institution_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstemailaddress_list = new List<mstBuyerOnboardemailaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstemailaddress_list.Add(new mstBuyerOnboardemailaddress_list
                    {
                        institution2email_gid = (dr_datarow["institution2email_gid"].ToString()),
                        email_address = (dr_datarow["email_address"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString())
                    });
                }
                values.mstBuyerOnboardemailaddress_list = getmstemailaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaInstitutionAddressList(string institution_gid, string employee_gid, MdlMstBuyerOnboardAddressDetails values)
        {
            msSQL = "  select institution2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, latitude, longitude, landmark," +
                    " postal_code from agr_mst_tsupronboardinstitution2address where institution_gid='" + institution_gid + "'or institution_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstaddress_list = new List<mstBuyerOnboardaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstaddress_list.Add(new mstBuyerOnboardaddress_list
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
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString()),
                        landmark = (dr_datarow["landmark"].ToString())
                    });
                }
                values.mstBuyerOnboardaddress_list = getmstaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaInstitutionLicenseList(string institution_gid, string employee_gid, MdlMstBuyerOnboardLicenseDetails values)
        {
            msSQL = " select institution2licensedtl_gid,licensetype_gid,licensetype_name,license_no,date_format(issue_date,'%d-%m-%Y') as issue_date," +
                    " date_format(expiry_date,'%d-%m-%Y') as expiry_date from agr_mst_tsupronboardinstitution2licensedtl" +
                    " where institution_gid='" + institution_gid + "'or institution_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstlicense_list = new List<mstBuyerOnboardlicense_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstlicense_list.Add(new mstBuyerOnboardlicense_list
                    {
                        institution2licensedtl_gid = (dr_datarow["institution2licensedtl_gid"].ToString()),
                        licensetype_gid = (dr_datarow["licensetype_gid"].ToString()),
                        licensetype_name = (dr_datarow["licensetype_name"].ToString()),
                        license_number = (dr_datarow["license_no"].ToString()),
                        licenseissue_date = (dr_datarow["issue_date"].ToString()),
                        licenseexpiry_date = (dr_datarow["expiry_date"].ToString())
                    });
                }
                values.mstBuyerOnboardlicense_list = getmstlicense_list;
            }
            dt_datatable.Dispose();
        }

        public void DaInstitutionDocumentList(string institution_gid, string employee_gid, institutionBuyerOnboarduploaddocument values)
        {
            msSQL = " select institution2documentupload_gid,institution_gid,document_name,document_path,document_title,document_id," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by, " +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as uploaded_date" +
                    " from agr_mst_tsupronboardinstitution2documentupload a" +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " where institution_gid='" + institution_gid + "'or institution_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<institutionupload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new institutionupload_list
                    {
                        document_name = dt["document_name"].ToString(),
                        //document_path = (dt["document_path"].ToString()),
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

        public bool DaUpdateInstitutionDtl(MdlMstInstitutionAdd values, string employee_gid)
        {
            msSQL = "select institution_gid from agr_mst_tsupronboardinstitution2mobileno where (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "') and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Primary Mobile Number";
                return false;
            }

            msSQL = "select institution_gid from agr_mst_tsupronboardinstitution2mobileno where institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Atleast One Mobile Number";
                return false;
            }

            msSQL = "select institution_gid from agr_mst_tsupronboardinstitution2email where institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Atleast One Email Address";
                return false;
            }

            msSQL = "select institution_gid from agr_mst_tsupronboardinstitution2address where institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Atleast One Address Detail";
                return false;
            }

            msSQL = "select institution_gid  from agr_mst_tsupronboardinstitution2mobileno where (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "') and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Mobile Number ";
                return false;
            }
            objODBCDatareader.Close();

            msSQL = "select institution_gid  from agr_mst_tsupronboardinstitution2email where primary_status='Yes' and (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "'  )";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add primary Email Address";
                return false;
            }
            objODBCDatareader.Close();
            msSQL = "select institution_gid  from agr_mst_tsupronboardinstitution2address where primary_status='Yes' and (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add primary Address";
                return false;
            }
            objODBCDatareader.Close();

            if (values.Gstflag == "Yes")
            {
                msSQL = "select institution2branch_gid from agr_mst_tsupronboardinstitution2branch where (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "') and headoffice_status ='Yes' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == false)
                {
                    values.status = false;
                    values.message = "Atleast Select One GST Number as Head Office";
                    return false;
                }
                objODBCDatareader.Close();

            }
            msSQL = "select application_gid from agr_mst_tsupronboard2institution where institution_gid='" + values.institution_gid + "'";
            values.application_gid = objdbconn.GetExecuteScalar(msSQL);
            if (values.stakeholder_type == "Borrower" || values.stakeholder_type == "Applicant")
            {
                msSQL = "select stakeholder_type from agr_mst_tsupronboardcontact where application_gid='" + values.application_gid + "' and stakeholder_type in ('Borrower','Applicant')";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Already Added Applicant/Borrower Information";
                    return false;
                }
                msSQL = "select stakeholder_type from agr_mst_tsupronboard2institution where application_gid='" + values.application_gid + "' and" +
                    " stakeholder_type in ('Borrower','Applicant') and institution_gid<> '" + values.institution_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Already Added Applicant/Borrower Information";
                    return false;
                }
            }

            msSQL = " select application_gid, application_no, company_name, date_incorporation, businessstart_date,companypan_no, year_business, month_business, cin_no," +
                     " official_telephoneno, officialemail_address, companytype_gid, companytype_name, stakeholder_type, stakeholdertype_gid, assessmentagency_gid, " +
                     " assessmentagency_name, assessmentagencyrating_gid, assessmentagencyrating_name, ratingas_on, amlcategory_gid, amlcategory_name,businesscategory_gid, " +
                     " businesscategory_name, contactperson_firstname, contactperson_middlename, contactperson_lastname, designation_gid, designation, " +
                     " lastyear_turnover, escrow, start_date, end_date, urn_status, urn, msme_registration, lglentity_id, lei_renewaldate, kin " +
                     " from agr_mst_tsupronboard2institution where institution_gid='" + values.institution_gid + "'";
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
                lsofficial_mailid = objODBCDatareader["officialemail_address"].ToString();
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
                lsmsme_registration = objODBCDatareader["msme_registration"].ToString();
                lslglentity_id = objODBCDatareader["lglentity_id"].ToString();
                lskin = objODBCDatareader["kin"].ToString();
                if (objODBCDatareader["lei_renewaldate"].ToString() == "")
                {
                }
                else
                {
                    lslei_renewaldate = Convert.ToDateTime(objODBCDatareader["lei_renewaldate"]).ToString("dd-MM-yyyy");
                }
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tsupronboard2institution set " +
                        " company_name='" + values.company_name.Replace("'", "") + "',";
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
                         " contactperson_firstname='" + values.contactperson_firstname.Replace("'", "") + "'," +
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
                          " tan_number='" + values.tan_number + "'," +
                         " incometax_returnsstatus='" + values.incometax_returnsstatus + "',";
                if (values.revenue == null || values.revenue == "")
                {
                    msSQL += "revenue='0.00',";
                }
                else
                {
                    msSQL += " revenue ='" + values.revenue.Replace(",", "") + "',";
                }
                if (values.profit == null || values.profit == "")
                {
                    msSQL += "profit='0.00',";
                }
                else
                {
                    msSQL += "profit='" + values.profit.Replace(",", "") + "',";
                }
                if (values.fixed_assets == null || values.fixed_assets == "")
                {
                    msSQL += "fixed_assets='0.00',";
                }
                else
                {
                    msSQL += "fixed_assets='" + values.fixed_assets.Replace(",", "") + "',";
                }
                if (values.sundrydebt_adv == null || values.sundrydebt_adv == "")
                {
                    msSQL += "sundrydebt_adv='0.00',";
                }
                else
                {
                    msSQL += "sundrydebt_adv='" + values.sundrydebt_adv.Replace(",", "") + "',";
                }
                if (Convert.ToDateTime(values.lei_renewaldate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {
                }
                else
                {
                    msSQL += " lei_renewaldate='" + Convert.ToDateTime(values.lei_renewaldate).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                msSQL +=
                    "msme_registration='" + values.msme_registration + "'," +
                    "lglentity_id='" + values.lglentity_id + "'," +
                    "kin='" + values.kin + "'," +
                             " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where institution_gid='" + values.institution_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("IULG");

                    msSQL = " insert into agr_mst_tsupronboard2institutionupdateLOG(" +
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
                    " created_by," +
                    " created_date) values(" +
                      "'" + msGetGid + "'," +
                      "'" + values.institution_gid + "'," +
                      "'" + employee_gid + "'," +
                      "'" + employee_gid + "'," +
                      "'" + lscompany_name + "'," +
                        "'" + lsdate_incorporation + "'," +
                        "'" + lsbusinessstart_date + "'," +
                        "'" + lsyear_business + "'," +
                               "'" + lsmonth_business + "'," +
                               "'" + lscompanypan_no + "'," +
                               "'" + lscin_no + "'," +
                               "'" + lsofficial_telephoneno + "'," +
                               "'" + lsofficial_mailid + "'," +
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
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                   

                    //Taking newly added Contact details for posting to ERP
                    //Mobileno
                    msSQL = " select institution2mobileno_gid" +
                            " from agr_mst_tsupronboardinstitution2mobileno" +
                            " where institution_gid='" + employee_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var mobilenogid_list = new List<string>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            mobilenogid_list.Add(dt["institution2mobileno_gid"].ToString());
                        }
                    }
                    //Email
                    msSQL = " select institution2email_gid" +
                            " from agr_mst_tsupronboardinstitution2email" +
                            " where institution_gid='" + employee_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var emailgid_list = new List<string>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            emailgid_list.Add(dt["institution2email_gid"].ToString());
                        }
                    }

                    // Updates for Multiple Add
                    msSQL = "update agr_mst_tsupronboardinstitution2branch set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsupronboardinstitution2mobileno set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsupronboardinstitution2email set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsupronboardinstitution2address set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsupronboardinstitution2licensedtl set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsupronboardinstitution2ratingdetail set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsupronboardinstitution2bankdtl set institution_gid='" + values.institution_gid + "', application_gid ='" + values.application_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_trn_ttandtl set function_gid='" + values.institution_gid + "' where function_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "select companydocument_gid, institution2documentupload_gid from agr_mst_tsupronboardinstitution2documentupload where institution_gid='" + employee_gid + "'";
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
                        msSQL = " insert into agr_trn_tdocumentchecktls(" +
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
                            "'" + values.application_gid + "'," +
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
                            msSQL = " insert into agr_trn_tcovanantdocumentcheckdtls(" +
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
                           "'" + values.application_gid + "'," +
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

                    //DaAgrMstScannedDocument objvalues = new DaAgrMstScannedDocument();
                    //objvalues.DaGroupDocChecklistinfo(lsapplication_gid, values.institution_gid, employee_gid);

                    msSQL = "update agr_mst_tsupronboardinstitution2documentupload set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsupronboardinstitution2form60documentupload set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    msSQL = "select mobile_no from agr_mst_tsupronboardinstitution2mobileno where institution_gid='" + values.institution_gid + "' and primary_status='yes'";
                    string lsmobileno = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select email_address from agr_mst_tsupronboardinstitution2email where institution_gid='" + values.institution_gid + "' and primary_status='yes'";
                    lsemail_address = objdbconn.GetExecuteScalar(msSQL);
                    if (values.stakeholder_type == "Borrower" || values.stakeholder_type == "Applicant")
                    {
                        msSQL = "update agr_mst_tsupronboard set applicant_type ='Institution' where application_gid='" + values.application_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_mst_tsupronboard2institution set mobile_no='" + lsmobileno + "'," +
                         " email_address='" + lsemail_address + "' where institution_gid='" + values.institution_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                    //Updating Overall Company Details
                    List<string> existingString = new List<string> { lsofficial_mailid, lsofficial_telephoneno, lscompany_name, lscompanypan_no, lscompanytype_name, lstan_number, lslastyear_turnover };
                    List<string> updatedString = new List<string> { values.official_mailid, values.official_telephoneno, values.company_name, values.companypan_no, values.companytype_name, values.tan_number, values.lastyear_turnover };

                    if (ConfigurationManager.AppSettings["sysSamagroHyperbrdigeAPIEnable"].ToString() == "Yes")
                    {
                        HttpContext ctx = HttpContext.Current;

                        Thread t = new Thread(new ThreadStart(() =>
                        {
                            HttpContext.Current = ctx;


                            if (!(existingString.SequenceEqual(updatedString)))
                            {
                                objFnSamAgroHBAPIConnEdit.UpdateSupplierInstitutionHBAPI(values.institution_gid);
                            }

                            List<string> existingContactValues = new List<string> { lscontactperson_firstname, lscontactperson_middlename, lscontactperson_lastname, lsdesignation };
                            List<string> updatedContactValues = new List<string> { values.contactperson_firstname, values.contactperson_middlename, values.contactperson_lastname, values.designation };

                            if (!(existingContactValues.SequenceEqual(updatedContactValues)))
                            {
                                objFnSamAgroHBAPIConnEdit.UpdateSupplierInstitutionContactBasicHBAPI(values.institution_gid);
                            }

                            //Calling if newly added address are there.

                                objFnSamAgroHBAPIConnEdit.UpdateSupplierInstitutionAddressAddHBAPI(values.institution_gid);
                           

                            //Calling to update contacts not updated in ERP

                                objFnSamAgroHBAPIConnEdit.UpdateSupplierInstitutionContactAddHBAPI(mobilenogid_list, emailgid_list, values.institution_gid);
                            

                        }));

                        t.Start();
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
                values.message = "Error Occured..";
                return false;
            }
        }

        public void DaGetIndividualMobileNoTempList(string contact_gid, string employee_gid, MdlBuyerOnboardContactMobileNo values)
        {
            msSQL = "select mobile_no,contact2mobileno_gid,primary_status,whatsapp_no from agr_mst_tsupronboardcontact2mobileno where " +
              " contact_gid = '" + employee_gid + "' or contact_gid = '" + contact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcontactmobileno_list = new List<BuyerOnboardcontactmobileno_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcontactmobileno_list.Add(new BuyerOnboardcontactmobileno_list
                    {
                        contact2mobileno_gid = (dr_datarow["contact2mobileno_gid"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        whatsapp_no = (dr_datarow["whatsapp_no"].ToString()),
                    });
                }
            }
            values.BuyerOnboardcontactmobileno_list = getcontactmobileno_list;
            dt_datatable.Dispose();
        }

        public void DaGetIndividualEmailAddressTempList(string contact_gid, string employee_gid, MdlBuyerOnboardContactEmail values)
        {
            msSQL = "select email_address,contact2email_gid,primary_status from agr_mst_tsupronboardcontact2email where " +
              " contact_gid='" + employee_gid + "' or contact_gid = '" + contact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcontactemail_list = new List<BuyerOnboardcontactemail_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcontactemail_list.Add(new BuyerOnboardcontactemail_list
                    {
                        contact2email_gid = (dr_datarow["contact2email_gid"].ToString()),
                        email_address = (dr_datarow["email_address"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                    });
                }
            }
            values.BuyerOnboardcontactemail_list = getcontactemail_list;
            dt_datatable.Dispose();
        }


        public void DaGetIndividualAddressTempList(string contact_gid, string employee_gid, MdlBuyerOnboardContactAddress values)
        {
            msSQL = " select contact2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, latitude, longitude," +
                    " postal_code from agr_mst_tsupronboardcontact2address where contact_gid='" + employee_gid + "' or contact_gid = '" + contact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcontactaddress_list = new List<BuyerOnboardcontactaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcontactaddress_list.Add(new BuyerOnboardcontactaddress_list
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
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString())
                    });
                }
                values.BuyerOnboardcontactaddress_list = getcontactaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGetIndividualProofTempList(string contact_gid, string employee_gid, MdlBuyerOnboardContactIdProof values)
        {
            msSQL = "select contact2idproof_gid,idproof_name,idproof_no,idproof_dob,file_no,document_name, document_path from agr_mst_tsupronboardcontact2idproof where " +
              " contact_gid='" + employee_gid + "' or contact_gid = '" + contact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcontactidproof_list = new List<contactBuyerOnboardidproof_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcontactidproof_list.Add(new contactBuyerOnboardidproof_list
                    {
                        contact2idproof_gid = (dr_datarow["contact2idproof_gid"].ToString()),
                        idproof_name = (dr_datarow["idproof_name"].ToString()),
                        idproof_no = (dr_datarow["idproof_no"].ToString()),
                        idproof_dob = (dr_datarow["idproof_dob"].ToString()),
                        file_no = (dr_datarow["file_no"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        //document_path = ((dr_datarow["document_path"].ToString())),
                        document_path = objcmnstorage.EncryptData(((dr_datarow["document_path"].ToString()))),
                    });

                    values.contactBuyerOnboardidproof_list = getcontactidproof_list;
                }
                dt_datatable.Dispose();
            }

        }

        public void DaGetIndividualDocTempList(string contact_gid, string employee_gid, MdlBuyerOnboardContactDocument values)
        {
            msSQL = " select contact2document_gid,document_name,document_title,document_path from agr_mst_tsupronboardcontact2document " +
                                 " where contact_gid='" + employee_gid + "' or contact_gid = '" + contact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<uploadBuyerOnboardindividualdoc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new uploadBuyerOnboardindividualdoc_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        //document_path = (dt["document_path"].ToString()),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        contact2document_gid = dt["contact2document_gid"].ToString(),
                    });
                    values.uploadBuyerOnboardindividualdoc_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaEditIndividual(string contact_gid, MdlMstBuyerOnboardContact values)
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
                        " from agr_mst_tsupronboardcontact where contact_gid='" + contact_gid + "'";


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

        public void DaGetPANForm60TempList(string contact_gid, string employee_gid, MdlBuyerOnboardContactPANForm60 values)
        {
            msSQL = "select contact2panform60_gid,document_name, document_path from agr_mst_tsupronboardcontact2panform60 where " +
             " contact_gid='" + employee_gid + "' or contact_gid = '" + contact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcontactpanform60_list = new List<BuyerOnboardcontactpanform60_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcontactpanform60_list.Add(new BuyerOnboardcontactpanform60_list
                    {
                        contact2panform60_gid = (dr_datarow["contact2panform60_gid"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        //document_path = ((dr_datarow["document_path"].ToString())),
                        document_path = objcmnstorage.EncryptData(((dr_datarow["document_path"].ToString()))),
                    });

                    values.BuyerOnboardcontactpanform60_list = getcontactpanform60_list;
                }
                dt_datatable.Dispose();
            }
        }

        public void DaGetPANForm60List(string contact_gid, string employee_gid, MdlBuyerOnboardContactPANForm60 values)
        {
            msSQL = "select contact2panform60_gid,document_name, document_path from agr_mst_tsupronboardcontact2panform60 where " +
             " contact_gid='" + contact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcontactpanform60_list = new List<BuyerOnboardcontactpanform60_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcontactpanform60_list.Add(new BuyerOnboardcontactpanform60_list
                    {
                        contact2panform60_gid = (dr_datarow["contact2panform60_gid"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        //document_path = ((dr_datarow["document_path"].ToString())),
                        document_path = objcmnstorage.EncryptData(((dr_datarow["document_path"].ToString()))),
                    });

                    values.BuyerOnboardcontactpanform60_list = getcontactpanform60_list;
                }
                dt_datatable.Dispose();
            }
        }

        public void DaContactPANAbsenceReasonList(string contact_gid, string employee_gid, MdlBuyerOnboardPANAbsenceReason objMdlPANAbsenceReason)
        {
            try
            {
                msSQL = " SELECT panabsencereason" +
                   " from agr_mst_tsupronboardcontact2panabsencereason where contact_gid = '" + contact_gid + "' or contact_gid = '" + employee_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcontactpanabsencereason_list = new List<BuyerOnboardcontactpanabsencereason_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    objMdlPANAbsenceReason.BuyerOnboardcontactpanabsencereason_list = dt_datatable.AsEnumerable().Select(row =>
                      new BuyerOnboardcontactpanabsencereason_list
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

        public void DaEditPANAbsenceReasonList(string contact_gid, MdlBuyerOnboardPANAbsenceReason values)
        {
            try
            {
                msSQL = " SELECT panabsencereason" +
                   " from agr_mst_tsuprpanabsencereason";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var panabsencereason_existinglist = new List<BuyerOnboardpanabsencereason_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    values.BuyerOnboardpanabsencereason_list = dt_datatable.AsEnumerable().Select(row =>
                     new BuyerOnboardpanabsencereason_list
                     {
                         panabsencereason = row["panabsencereason"].ToString(),
                     }
                   ).ToList();
                }
                dt_datatable.Dispose();

                msSQL = " SELECT panabsencereason" +
                   " from agr_mst_tsupronboardcontact2panabsencereason where contact_gid = '" + contact_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var panabsencereason_contactlist = new List<BuyerOnboardpanabsencereason_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    panabsencereason_contactlist = dt_datatable.AsEnumerable().Select(row =>
                      new BuyerOnboardpanabsencereason_list
                      {
                          panabsencereason = row["panabsencereason"].ToString(),
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();

                for (var i = 0; i < values.BuyerOnboardpanabsencereason_list.Count; i++)
                {
                    for (var j = 0; j < panabsencereason_contactlist.Count; j++)
                    {
                        if (values.BuyerOnboardpanabsencereason_list[i].panabsencereason == panabsencereason_contactlist[j].panabsencereason)
                        {
                            values.BuyerOnboardpanabsencereason_list[i].check_status = true;
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

        public void DaUpdateIndividual(string employee_gid, MdlMstBuyerOnboardContact values)
        {
            //msSQL = "select application_gid from agr_mst_tcontact where contact_gid='" + values.contact_gid + "'";
            //lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            if (values.stakeholder_type == "Borrower" || values.stakeholder_type == "Applicant")
            {
                msSQL = "select stakeholder_type from agr_mst_tsupronboardcontact where application_gid='" + values.application_gid + "' and stakeholder_type in ('Borrower','Applicant')" +
                    " and contact_gid<>'" + values.contact_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Already Added Applicant/Borrower Information";
                    return;
                }
                msSQL = "select stakeholder_type from agr_mst_tsupronboard2institution where application_gid='" + values.application_gid + "' and " +
                    " stakeholder_type in ('Borrower','Applicant') ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Already Added Applicant/Borrower Information";
                    return;
                }
            }

            msSQL = "select contact_gid from agr_mst_tsupronboardcontact2mobileno where contact_gid='" + employee_gid + "' or contact_gid='" + values.contact_gid + "'  and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Mobile Number ";
                return;
            }
            objODBCDatareader.Close();

            msSQL = "select contact_gid from agr_mst_tsupronboardcontact2email where contact_gid='" + employee_gid + "' or contact_gid='" + values.contact_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add primary Email Address";
                return;
            }
            objODBCDatareader.Close();
            msSQL = "select contact_gid from agr_mst_tsupronboardcontact2address where contact_gid='" + employee_gid + "' or contact_gid='" + values.contact_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add primary Address";
                return;
            }
            objODBCDatareader.Close();

            msSQL = "select contact_gid  from agr_mst_tsupronboardcontact2mobileno where  (contact_gid='" + employee_gid + "' or contact_gid='" + values.contact_gid + "') and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Mobile Number ";
                return ;
            }
            objODBCDatareader.Close();

            msSQL = "select contact_gid  from agr_mst_tsupronboardcontact2email where primary_status='Yes' and (contact_gid='" + employee_gid + "' or contact_gid='" + values.contact_gid + "')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add primary Email Address";
                return ;
            }
            objODBCDatareader.Close();
            msSQL = "select contact_gid  from agr_mst_tsupronboardcontact2address where primary_status='Yes' and (contact_gid='" + employee_gid + "' or contact_gid='" + values.contact_gid + "')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add primary Address";
                return ;
            }
            objODBCDatareader.Close();




            msSQL = "select pan_status from agr_mst_tsupronboardcontact where contact_gid='" + values.contact_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (values.pan_status == "Customer Submitting PAN")
            {
                msSQL = "delete from agr_mst_tsupronboardcontact2panform60 where contact_gid='" + values.contact_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "delete from agr_mst_tsupronboardcontact2panabsencereason where contact_gid='" + values.contact_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }
            objODBCDatareader.Close();

            msSQL = " select application_gid,pan_status,pan_no,aadhar_no,first_name,middle_name,last_name,individual_dob,age,gender_gid,gender_name,designation_gid,designation_name," +
                        " educationalqualification_gid,educationalqualification_name,main_occupation,annual_income,monthly_income," +
                        " pep_status,pepverified_date,maritalstatus_gid,maritalstatus_name,stakeholdertype_gid,stakeholder_type," +
                        " father_firstname,father_middlename,father_lastname,father_dob,father_age," +
                        " mother_firstname,mother_middlename,mother_lastname,mother_dob,mother_age," +
                        " spouse_firstname,spouse_middlename,spouse_lastname,spouse_dob,spouse_age," +
                        " ownershiptype_gid,ownershiptype_name,residencetype_gid,residencetype_name,currentresidence_years,branch_distance," +
                        " propertyholder_gid, propertyholder_name, incometype_gid, incometype_name, previouscrop, prposedcrop,institution_gid,institution_name," +
                        " group_gid, group_name, profile, urn_status, urn, fathernominee_status, mothernominee_status, spousenominee_status, othernominee_status," +
                        " relationshiptype, nomineefirst_name, nominee_middlename, nominee_lastname, nominee_dob, nominee_age, totallandinacres, cultivatedland" +
                        " from agr_mst_tsupronboardcontact where contact_gid='" + values.contact_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                lsapplication_gid = objODBCDatareader["application_gid"].ToString();
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

            msSQL = " update agr_mst_tsupronboardcontact set " +
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
                msSQL += " pepverified_date=null,";
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
                msSQL += " father_firstname='',";
            }
            else
            {
                msSQL += " father_firstname='" + values.father_firstname.Replace("'", "") + "',";
            }
            if (values.father_middlename == "" || values.father_middlename == null)
            {
                msSQL += " father_middlename='',";
            }
            else
            {
                msSQL += " father_middlename='" + values.father_middlename.Replace("'", "") + "',";
            }
            if (values.father_lastname == "" || values.father_lastname == null)
            {
                msSQL += " father_middlename='',";
            }
            else
            {
                msSQL += " father_lastname='" + values.father_lastname.Replace("'", "") + "',";
            }
            msSQL += " father_dob='" + values.father_dob + "'," +
                     " father_age='" + values.father_age + "',";
            if (values.mother_firstname == "" || values.mother_firstname == null)
            {
                msSQL += " mother_firstname='',";
            }
            else
            {
                msSQL += " mother_firstname='" + values.mother_firstname.Replace("'", "") + "',";
            }
            if (values.mother_middlename == "" || values.mother_middlename == null)
            {
                msSQL += " mother_middlename='',";
            }
            else
            {
                msSQL += " mother_middlename='" + values.mother_middlename.Replace("'", "") + "',";
            }
            if (values.mother_lastname == "" || values.mother_lastname == null)
            {
                msSQL += " mother_lastname='',";
            }
            else
            {
                msSQL += " mother_lastname='" + values.mother_lastname.Replace("'", "") + "',";
            }
            msSQL += " mother_dob='" + values.mother_dob + "'," +
                     " mother_age='" + values.mother_age + "',";
            if (values.spouse_firstname == "" || values.spouse_firstname == null)
            {
                msSQL += " spouse_firstname='',";
            }
            else
            {
                msSQL += " spouse_firstname='" + values.spouse_firstname.Replace("'", "") + "',";
            }
            if (values.spouse_middlename == "" || values.spouse_middlename == null)
            {
                msSQL += " spouse_middlename='',";
            }
            else
            {
                msSQL += " spouse_middlename='" + values.spouse_middlename.Replace("'", "") + "',";
            }
            if (values.spouse_lastname == "" || values.spouse_lastname == null)
            {
                msSQL += " spouse_lastname='',";
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
                msSQL += " nomineefirst_name='',";
            }
            else
            {
                msSQL += " nomineefirst_name='" + values.nomineefirst_name.Replace("'", "") + "',";
            }
            if (values.nominee_middlename == "" || values.nominee_middlename == null)
            {
                msSQL += " nominee_middlename='',";
            }
            else
            {
                msSQL += " nominee_middlename='" + values.nominee_middlename.Replace("'", "") + "',";
            }
            if (values.nominee_lastname == "" || values.nominee_lastname == null)
            {
                msSQL += " nominee_lastname='',";
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

            //objODBCDatareader.Close();

            if (mnResult != 0)
            {
                if (values.pan_status == "Customer Submitting Form 60")
                {
                    matchCount1 = 0;
                    matchCount2 = 0;

                    msSQL = " select panabsencereason from agr_mst_tsupronboardcontact2panabsencereason" +
                           " where contact_gid='" + values.contact_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        values.BuyerOnboardcontactpanabsencereason_list = dt_datatable.AsEnumerable().Select(row =>
                         new BuyerOnboardcontactpanabsencereason_list
                         {
                             panabsencereason = row["panabsencereason"].ToString(),
                         }
                       ).ToList();
                    }
                    dt_datatable.Dispose();

                    if (values.BuyerOnboardcontactpanabsencereason_list == null)
                    {
                        foreach (string reason in values.panabsencereason_selectedlist)
                        {
                            msGetGid = objcmnfunctions.GetMasterGID("C2PR");
                            msSQL = " INSERT INTO agr_mst_tsupronboardcontact2panabsencereason(" +
                                    " contact2panabsencereason_gid," +
                                    " contact_gid," +
                                    " panabsencereason," +
                                    " created_date," +
                                    " created_by)" +
                                    " VALUES(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + reason.Replace("'", "") + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                    "'" + employee_gid + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        for (var i = 0; i < values.panabsencereason_selectedlist.Count; i++)
                        {
                            for (var j = 0; j < values.BuyerOnboardcontactpanabsencereason_list.Count; j++)
                            {
                                if (values.panabsencereason_selectedlist[i] == values.BuyerOnboardcontactpanabsencereason_list[j].panabsencereason)
                                {
                                    matchCount1++;
                                }
                            }
                            if (matchCount1 == 0)
                            {
                                msGetGid = objcmnfunctions.GetMasterGID("C2PR");
                                msSQL = " INSERT INTO agr_mst_tsupronboardcontact2panabsencereason(" +
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

                        for (var i = 0; i < values.BuyerOnboardcontactpanabsencereason_list.Count; i++)
                        {
                            for (var j = 0; j < values.panabsencereason_selectedlist.Count; j++)
                            {
                                if (values.BuyerOnboardcontactpanabsencereason_list[i].panabsencereason == values.panabsencereason_selectedlist[j])
                                {
                                    matchCount2++;
                                }
                            }
                            if (matchCount2 == 0)
                            {
                                msSQL = "delete from agr_mst_tsupronboardcontact2panabsencereason where panabsencereason='" + values.BuyerOnboardcontactpanabsencereason_list[i].panabsencereason + "' and contact_gid = '" + values.contact_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            matchCount2 = 0;
                        }
                    }

                }
                msGetGid = objcmnfunctions.GetMasterGID("CTUL");

                msSQL = " insert into agr_mst_tsupronboardcontactupdatelog(" +
              " contactupdatelog_gid," +
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
              " created_by," +
              " created_date)" +
              " values(" +
              "'" + msGetGid + "'," +
              "'" + values.contact_gid + "'," +
              "'" + values.contact_gid + "'," +
              "'" + values.contact_gid + "'," +
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
                     "'" + employee_gid + "'," +
                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

               

                //Taking newly added Contact details for posting to ERP
                //Mobileno
                msSQL = " select contact2mobileno_gid" +
                        " from agr_mst_tsupronboardcontact2mobileno" +
                        " where contact_gid='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var mobilenogid_list = new List<string>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        mobilenogid_list.Add(dt["contact2mobileno_gid"].ToString());
                    }
                }
                //Email
                msSQL = " select contact2email_gid" +
                        " from agr_mst_tsupronboardcontact2email" +
                        " where contact_gid='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var emailgid_list = new List<string>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        emailgid_list.Add(dt["contact2email_gid"].ToString());
                    }
                }

                //Updates
                msSQL = "update agr_mst_tsupronboardcontact2mobileno set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2email set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2address set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2idproof set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2panform60 set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2panabsencereason set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2bankdtl set contact_gid='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select individualdocument_gid , contact2document_gid from agr_mst_tsupronboardcontact2document where contact_gid='" + employee_gid + "'";
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
                    msSQL = " insert into agr_trn_tdocumentchecktls(" +
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
                        msSQL = " insert into agr_trn_tcovanantdocumentcheckdtls(" +
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

                //DaAgrMstScannedDocument objvalues = new DaAgrMstScannedDocument();
                //objvalues.DaGroupDocChecklistinfo(lsapplication_gid, values.contact_gid, employee_gid);

                msSQL = "update agr_mst_tsupronboardcontact2document set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select mobile_no from agr_mst_tsupronboardcontact2mobileno where contact_gid='" + values.contact_gid + "' and primary_status='yes'";
                string lsmobileno = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select email_address from agr_mst_tsupronboardcontact2email where contact_gid='" + values.contact_gid + "' and primary_status='yes'";
                lsemail_address = objdbconn.GetExecuteScalar(msSQL);

                if (values.stakeholder_type == "Borrower" || values.stakeholder_type == "Applicant")
                {
                    msSQL = "update agr_mst_tsupronboard set applicant_type ='Individual' where application_gid='" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsupronboardcontact set mobile_no='" + lsmobileno + "'," +
                        " email_address='" + lsemail_address + "' where contact_gid='" + values.contact_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (ConfigurationManager.AppSettings["sysSamagroHyperbrdigeAPIEnable"].ToString() == "Yes")
                {
                    HttpContext ctx = HttpContext.Current;

                    Thread t = new Thread(new ThreadStart(() =>
                    {
                        HttpContext.Current = ctx;
                        //Updating Overall Individual Details
                        List<string> existingString = new List<string> { lsfirst_name, lsfather_middlename, lslast_name, lsaadhar_no };
                        List<string> updatedString = new List<string> { values.first_name, values.middle_name, values.last_name, values.aadhar_no };

                        if (!(existingString.SequenceEqual(updatedString)))
                        {
                            objFnSamAgroHBAPIConnEdit.UpdateSupplierIndividualHBAPI(values.contact_gid);
                        }

                        //Updating Contact Basic Details Change
                        List<string> existingContactValues = new List<string> { lsfirst_name, lsmiddle_name, lslast_name, lsdesignation_name };
                        List<string> updatedContactValues = new List<string> { values.first_name, values.middle_name, values.last_name, values.designation_name };

                        if (!(existingContactValues.SequenceEqual(updatedContactValues)))
                        {
                            objFnSamAgroHBAPIConnEdit.UpdateSupplierIndividualContactBasicHBAPI(values.contact_gid);
                        }

                        //Calling if newly added address are there.

                            objFnSamAgroHBAPIConnEdit.UpdateSupplierIndividualAddressAddHBAPI(values.contact_gid);
                        

                        //Calling to update contacts not updated in ERP

                            objFnSamAgroHBAPIConnEdit.UpdateSupplierIndividualContactAddHBAPI(mobilenogid_list, emailgid_list, values.contact_gid);
                        
                    }));

                    t.Start();
                }
                
                values.status = true;
                values.message = "Individual Details Updated Successfully";
            }

            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }


        public void DaGetAppMobileNoTempList(string application_gid, string employee_gid, MdlMstBuyerOnboardMobileNo values)
        {
            msSQL = "select mobile_no,application2contact_gid,primary_mobileno,whatsapp_mobileno from agr_mst_tsupronboard2contactno where " +
              " application_gid='" + employee_gid + "' or application_gid = '" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstmobileno_list = new List<mstBuyerOnboardmobileno_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstmobileno_list.Add(new mstBuyerOnboardmobileno_list
                    {
                        application2contact_gid = (dr_datarow["application2contact_gid"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_mobileno = (dr_datarow["primary_mobileno"].ToString()),
                        whatsapp_mobileno = (dr_datarow["whatsapp_mobileno"].ToString()),
                        
                    });
                }
                values.mstBuyerOnboardmobileno_list = getmstmobileno_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGetAppEmailAddressTempList(string application_gid, string employee_gid, MdlMstBuyerOnboardEmailAddress values)
        {
            msSQL = "select email_address,application2email_gid,primary_emailaddress from agr_mst_tsupronboard2email where " +
              " application_gid='" + employee_gid + "' or application_gid = '" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstemailaddress_list = new List<mstBuyerOnboardemailaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstemailaddress_list.Add(new mstBuyerOnboardemailaddress_list
                    {
                        application2email_gid = (dr_datarow["application2email_gid"].ToString()),
                        email_address = (dr_datarow["email_address"].ToString()),
                        primary_emailaddress = (dr_datarow["primary_emailaddress"].ToString()),
                       
                    });
                }
                values.mstBuyerOnboardemailaddress_list = getmstemailaddress_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }


        public void DaGetAppGeneticCodeTempList(string application_gid, string employee_gid, MdlMstBuyerOnboardGeneticCode values)
        {
            msSQL = " select application2geneticcode_gid,geneticcode_gid,geneticcode_name,genetic_status,genetic_remarks,application_gid" +
                      " from agr_mst_tsupronboard2geneticcode where " +
                      " application_gid='" + employee_gid + "' or application_gid = '" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getgeneticcode_list = new List<mstBuyerOnboardgeneticcode_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getgeneticcode_list.Add(new mstBuyerOnboardgeneticcode_list
                    {
                        application2geneticcode_gid = (dr_datarow["application2geneticcode_gid"].ToString()),
                        geneticcode_gid = (dr_datarow["geneticcode_gid"].ToString()),
                        geneticcode_name = (dr_datarow["geneticcode_name"].ToString()),
                        genetic_status = (dr_datarow["genetic_status"].ToString()),
                        genetic_remarks = (dr_datarow["genetic_remarks"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                    });
                }
                values.mstBuyerOnboardgeneticcode_list = getgeneticcode_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGetAppProductTempList(string application_gid, string employee_gid, MdlMstBuyerOnboardProductDetailList values)
        {
            msSQL = " select application2product_gid,product_gid,product_name,variety_gid,variety_name,sector_name,category_name,hsn_code, " +
                    " botanical_name,alternative_name from agr_mst_tsupronboard2product " +
                    " where application_gid = '" + employee_gid + "' or application_gid = '" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstproduct_list = new List<mstBuyerOnboardproduct_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstproduct_list.Add(new mstBuyerOnboardproduct_list
                    {
                        application2product_gid = (dr_datarow["application2product_gid"].ToString()),
                        product_gid = (dr_datarow["product_gid"].ToString()),
                        product_name = (dr_datarow["product_name"].ToString()),
                        variety_gid = (dr_datarow["variety_gid"].ToString()),
                        variety_name = (dr_datarow["variety_name"].ToString()),
                        sector_name = (dr_datarow["sector_name"].ToString()),
                        category_name = (dr_datarow["category_name"].ToString()),
                        botanical_name = (dr_datarow["botanical_name"].ToString()),
                        alternative_name = (dr_datarow["alternative_name"].ToString()),
                        hsn_code = (dr_datarow["hsn_code"].ToString())
                    });
                }
                values.mstBuyerOnboardproduct_list = getmstproduct_list;
            }
            dt_datatable.Dispose();
        }


        public void DaEditAppBasicDetail(string application_gid, MdlMstBuyerOnboardApplicationAdd values)
        {
            try
            {
                msSQL = " select application_gid,customer_urn,customerref_name as customer_name,vertical_gid,vertical_name,verticaltaggs_gid,verticaltaggs_name," +
                        " constitution_gid,constitution_name,businessunit_gid,businessunit_name,sa_status,sa_id,sa_name,saname_gid,vernacularlanguage_gid," +
                        " vernacular_language,contactpersonfirst_name,contactpersonmiddle_name,contactpersonlast_name,designation_gid,designation_type,landline_no," +
                        " creditgroup_gid,creditgroup_name,program_gid,program_name, product_gid,product_name,variety_gid,variety_name,sector_name,category_name, " +
                        " botanical_name,alternative_name,onboarding_status,buyersuppliertype_gid,buyersuppliertype_name from agr_mst_tsupronboard where application_gid='" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                    values.customer_name = objODBCDatareader["customer_name"].ToString();
                    values.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                    values.vertical_name = objODBCDatareader["vertical_name"].ToString();
                    values.constitution_gid = objODBCDatareader["constitution_gid"].ToString();
                    values.constitution_name = objODBCDatareader["constitution_name"].ToString();
                    values.businessunit_gid = objODBCDatareader["businessunit_gid"].ToString();
                    values.businessunit_name = objODBCDatareader["businessunit_name"].ToString();
                    values.sa_status = objODBCDatareader["sa_status"].ToString();
                    values.saname_gid = objODBCDatareader["saname_gid"].ToString();
                    values.sa_name = objODBCDatareader["sa_name"].ToString();
                    values.onboarding_status = objODBCDatareader["onboarding_status"].ToString();

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
                    values.buyersuppliertype_gid = objODBCDatareader["buyersuppliertype_gid"].ToString();
                    values.buyersuppliertype_name = objODBCDatareader["buyersuppliertype_name"].ToString();
                }


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

        public void DaUpdateAppBasicDetail(string employee_gid, MdlMstBuyerOnboardApplicationAdd values)
        {
            //lsapplication_gid = objdbconn.GetExecuteScalar("select application_gid from agr_mst_tapplication where application_gid='" + values.application_gid + "' and " +
            //    " headapproval_status='Comment Raised'");
            //if (lsapplication_gid == "" || lsapplication_gid == null)
            //{
            //}
            //else
            //{
            //    string lsverticalgid = objdbconn.GetExecuteScalar("select vertical_gid from agr_mst_tapplication where application_gid='" + values.application_gid + "'");

            //    if (lsverticalgid != values.vertical_gid)
            //    {
            //        values.status = false;
            //        values.message = "Already Approval Initiated.. You Can't Change the Vertical";
            //        return;

            //    }

            //}

            msSQL = " select application_gid from ocs_trn_tAppcreditapproval where application_gid='" + values.application_gid + "' and hierary_level<>'0'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                string lscreditgroup_gid = objdbconn.GetExecuteScalar("select creditgroup_gid from agr_mst_tapplication where application_gid='" + values.application_gid + "'");

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
            string lsapp_refno = "";
            msSQL = "select count(*) from ocs_mst_tgeneticcode where status='Y'";
            string lsmastercount = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(*) from agr_mst_tsupronboard2geneticcode where application_gid='" + employee_gid + "' or application_gid='" + values.application_gid + "'";
            string lsgeneticcount = objdbconn.GetExecuteScalar(msSQL);
            if (lsmastercount == lsgeneticcount)
            {
                msSQL = "select application_gid from agr_mst_tsupronboard2contactno where (application_gid='" + employee_gid + "' or application_gid='" + values.application_gid + "')" +
                  " and primary_mobileno='Yes'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Kindly Add Primary Mobile Number ";
                    return;
                }
                //objODBCDatareader.Close();

                msSQL = "select application_gid from agr_mst_tsupronboard2email where (application_gid='" + employee_gid + "' or application_gid='" + values.application_gid + "')" +
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
                             " constitution_gid,constitution_name,businessunit_gid,businessunit_name,sa_status,sa_id,sa_name,vernacularlanguage_gid," +
                             " vernacular_language,contactpersonfirst_name,contactpersonmiddle_name,contactpersonlast_name,designation_gid,designation_type,landline_no, creditgroup_gid,creditgroup_name, " +
                             " program_gid,program_name,product_gid,product_name,variety_gid,variety_name,sector_name, " +
                             " category_name, botanical_name, alternative_name, buyersuppliertype_gid,buyersuppliertype_name from agr_mst_tsupronboard " +
                             " where application_gid='" + values.application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lscustomer_urn = objODBCDatareader["customer_urn"].ToString();
                    lsvertical_gid = objODBCDatareader["vertical_gid"].ToString();
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
                    lsbuyersuppliertype_gid = objODBCDatareader["buyersuppliertype_gid"].ToString();
                    lsbuyersuppliertype_name = objODBCDatareader["buyersuppliertype_name"].ToString();

                }
                objODBCDatareader.Close();
                try
                {
                    string lsstatus = "", lsonboard_applicationno = "";
                    msSQL = "select status,application_no from agr_mst_tsupronboard where application_gid='" + values.application_gid + "' ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsstatus = objODBCDatareader["status"].ToString();
                        lsonboard_applicationno = objODBCDatareader["application_no"].ToString();
                        objODBCDatareader.Close();
                        if ((lsstatus == "" || lsstatus == null) || (lsonboard_applicationno == "" || lsonboard_applicationno == null))
                        {
                            msSQL = "select vertical_refno from ocs_mst_tvertical where vertical_gid='" + values.vertical_gid + "'";
                            string lsvertical_refno = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = "select entity_gid from ocs_mst_tvertical where vertical_gid='" + values.vertical_gid + "'";
                            string lsentity_gid = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = "select entity_code from adm_mst_tentity where entity_gid='" + lsentity_gid + "'";
                            string lsentity_code = objdbconn.GetExecuteScalar(msSQL);

                            lsapp_refno = "ARN" + lsentity_code + lsvertical_refno + DateTime.Now.ToString("ddMMyyyy");

                            string msGETRef = objcmnfunctions.GetMasterGID("APP");
                            msGETRef = msGETRef.Replace("APP", "");
                            lsapp_refno = lsapp_refno + msGETRef + "IN01";

                        }
                        else
                        {
                            msSQL = "select application_no from agr_mst_tsupronboard where application_gid='" + values.application_gid + "' ";
                            lsapp_refno = objdbconn.GetExecuteScalar(msSQL);
                        }
                    }
                    else
                    {
                        objODBCDatareader.Close();
                    }

                    msSQL = " update agr_mst_tsupronboard set " +
                        " application_no='" + lsapp_refno + "'," +
                         " customer_urn='" + values.customer_urn + "'," +
                         " customerref_name='" + values.customer_name.Replace("'", "") + "'," +
                         " vertical_gid='" + values.vertical_gid + "'," +
                         " vertical_name='" + values.vertical_name + "'," +
                         " onboarding_status='" + values.onboarding_status + "'," +
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
                         " contactpersonlast_name='" + values.contactpersonlast_name+ "'," +
                         " designation_gid='" + values.designation_gid + "'," +
                         " designation_type='" + values.designation_type + "'," +
                         " landline_no='" + values.landline_no + "'," +
                         " creditgroup_gid='" + values.creditgroup_gid + "'," +
                         " creditgroup_name='" + values.creditgroup_name + "'," +
                         " program_gid='" + values.program_gid + "'," +
                         " program_name='" + values.program_name + "'," +

                         " status = 'Completed'," +
                         " buyersuppliertype_gid='" + values.buyersuppliertype_gid + "'," +
                         " buyersuppliertype_name='" + values.buyersuppliertype_name.Replace("'", "\\'") + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where application_gid='" + values.application_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    objODBCDatareader.Close();
                    if (mnResult == 1)
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("ABUL");

                        // msSQL = "Insert into agr_mst_tsupronboardbasicdetailsupdatelog(" +
                        //" applicationbasicdetailsupdatelog_gid, " +
                        //" application_gid, " +
                        //" customer_urn, " +
                        //" customer_name, " +
                        //" vertical_gid, " +
                        //" vertical_name," +
                        ////" verticaltaggs_gid," +
                        ////" verticaltaggs_name," +
                        //" constitution_gid," +
                        //" constitution_name," +
                        //" businessunit_gid," +
                        //" businessunit_name," +
                        //" sa_status," +
                        //" vernacularlanguage_gid," +
                        //" vernacularlanguage_name," +
                        //" contactpersonfirst_name," +
                        //" contactpersonmiddle_name," +
                        //" contactpersonlast_name," +
                        //" designation_gid," +
                        //" designation_type," +
                        //" landline_no," +
                        //" creditgroup_gid," +
                        //" creditgroup_name," +
                        //" program_gid," +
                        //" program_name," +
                        //" product_gid," +
                        //" product_name," +
                        //" variety_gid," +
                        //" variety_name," +
                        //" sector_name," +
                        //" category_name," +
                        //" botanical_name," +
                        //" alternative_name," +
                        //" created_by," +
                        //" created_date)" +
                        //" values (" +
                        //"'" + msGetGid + "'," +
                        //"'" + values.application_gid + "'," +
                        //"'" + lscustomer_urn + "'," +
                        //"'" + lscustomer_name + "'," +
                        //"'" + lsvertical_gid + "'," +
                        //"'" + lsvertical_name + "'," +              
                        //"'" + lsconstitution_gid + "'," +
                        //"'" + lsconstitution_name + "'," +
                        //"'" + lsbusinessunit_gid + "'," +
                        //"'" + lsbusinessunit_name + "'," +
                        //"'" + lssa_status + "'," +
                        //"'" + lsvernacularlanguage_gid + "'," +
                        //"'" + lsvernacular_language + "'," +
                        //"'" + lscontactpersonfirst_name + "'," +
                        //"'" + lscontactpersonmiddle_name + "'," +
                        //"'" + lscontactpersonlast_name + "'," +
                        //"'" + lsdesignation_gid + "'," +
                        //"'" + lsdesignation_type + "'," +
                        //"'" + lslandline_no + "'," +
                        //"'" + lscreditgroup_gid + "'," +
                        //"'" + lscreditgroup_name + "'," +
                        //"'" + lsprogram_gid + "'," +
                        //"'" + lsprogram_name + "'," +
                        //"'" + lsproduct_gid + "'," +
                        //"'" + lsproduct_name + "'," +
                        //"'" + lsvariety_gid + "'," +
                        //"'" + lsvariety_name + "'," +
                        //"'" + lssector_name + "'," +
                        //"'" + lscategory_name + "'," +
                        //"'" + lsbotanical_name + "'," +
                        //"'" + lsalternative_name + "'," +
                        //"'" + employee_gid + "'," +
                        //"'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        // mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        //Taking newly added Contact details for posting to ERP
                        //Mobileno
                        msSQL = " select application2contact_gid" +
                                " from agr_mst_tsupronboard2contactno" +
                                " where application_gid='" + employee_gid + "'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        var mobilenogid_list = new List<string>();
                        if (dt_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                mobilenogid_list.Add(dt["application2contact_gid"].ToString());
                            }
                        }
                        //Email
                        msSQL = " select application2email_gid" +
                                " from agr_mst_tsupronboard2email" +
                                " where application_gid='" + employee_gid + "'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        var emailgid_list = new List<string>();
                        if (dt_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                emailgid_list.Add(dt["application2email_gid"].ToString());
                            }
                        }


                        //Updates

                        msSQL = "update agr_mst_tsupronboard2contactno set application_gid ='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_mst_tsupronboard2email set application_gid ='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_mst_tsupronboard2geneticcode set application_gid ='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_mst_tsupronboard2product set application_gid ='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (ConfigurationManager.AppSettings["sysSamagroHyperbrdigeAPIEnable"].ToString() == "Yes")
                        {
                            HttpContext ctx = HttpContext.Current;

                            Thread t = new Thread(new ThreadStart(() =>
                            {
                                HttpContext.Current = ctx;

                                List<string> existingString = new List<string> { lsvertical_name, lsconstitution_name };
                                List<string> updatedString = new List<string> { values.vertical_name, values.constitution_name };

                                if (!(existingString.SequenceEqual(updatedString)) || 1 == 1)
                                {
                                    objFnSamAgroHBAPIConnEdit.UpdateSupplierGeneralHBAPI(values.application_gid);
                                }


                                //Updating Contact Basic Details Change
                                List<string> existingContactValues = new List<string> { lscontactpersonfirst_name, lscontactpersonmiddle_name, lscontactpersonlast_name, lsdesignation_type };
                                List<string> updatedContactValues = new List<string> { values.contactpersonfirst_name, values.contactpersonmiddle_name, values.contactpersonlast_name, values.designation_type };

                                if (!(existingContactValues.SequenceEqual(updatedContactValues)))
                                {
                                    objFnSamAgroHBAPIConnEdit.UpdateSupplierGeneralContactBasicHBAPI(values.application_gid);
                                }

                                //Calling to update contacts not updated in ERP

                                    objFnSamAgroHBAPIConnEdit.UpdateSupplierGeneralContactAddHBAPI(mobilenogid_list, emailgid_list, values.application_gid);
                                

                            }));

                            t.Start();
                        }
                        
                        values.status = true;
                        values.message = "Basic Details Updated Successfully";
                    }
                    else
                    {
                        values.message = "Error Occured";
                        values.status = false;
                        return;
                    }

                }

                catch (Exception ex)
                {
                    values.status = false;
                    values.message = "Error Occured..";
                }

            }

            else
            {
                values.message = "Kindly Add all Genetic details";
                values.status = false;
            }
        }


        public bool DaSaveInstitutionDtl(MdlMstBuyerOnboardInstitutionAdd values, string employee_gid)
        {

            //msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            //lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select stakeholder_type from agr_mst_tsupronboardcontact where application_gid='" + values.application_gid + "' and stakeholder_type in ('Borrower','Applicant')";
            string lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholder_type)
            {

                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return false;
            }

            msSQL = "select stakeholder_type from agr_mst_tsupronboard2institution where application_gid='" + values.application_gid + "' and stakeholder_type in ('Borrower','Applicant')";
            lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholder_type)
            {

                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return false;
            }
            msGetGid = objcmnfunctions.GetMasterGID("APIN");
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
                 " lei_renewaldate," +
                " msme_registration," +
                " lglentity_id," +
                " kin," +
                " created_by," +
                " created_date) values(" +
                  "'" + msGetGid + "'," +
                  "'" + values.application_gid + "'," +
                  "'" + values.company_name.Replace("'", "") + "',";
            if ((values.date_incorporation == null) || (values.date_incorporation == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.date_incorporation).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if ((values.businessstartdate == null) || (values.businessstartdate == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.businessstartdate).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + values.year_business + "'," +
                    "'" + values.month_business + "'," +
                    "'" + values.companypan_no + "'," +
                    "'" + values.cin_no + "'," +
                    "'" + values.official_telephoneno + "'," +
                    "'" + values.official_mailid + "'," +
                    "'" + values.companytype_gid + "'," +
                    "'" + values.companytype_name + "'," +
                    "'" + values.stakeholdertype_gid + "'," +
                    "'" + values.stakeholder_type + "'," +
                    "'" + values.assessmentagency_gid + "'," +
                    "'" + values.assessmentagency_name + "'," +
                    "'" + values.assessmentagencyrating_gid + "'," +
                    "'" + values.assessmentagencyrating_name + "',";
            if ((values.ratingas_on == null) || (values.ratingas_on == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.ratingas_on).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + values.amlcategory_gid + "'," +
                    "'" + values.amlcategory_name + "'," +
                    "'" + values.businesscategory_gid + "'," +
                    "'" + values.businesscategory_name + "'," +
                    "'" + values.contactperson_firstname + "'," +
                    "'" + values.contactperson_middlename + "'," +
                    "'" + values.contactperson_lastname + "'," +
                    "'" + values.designation_gid + "'," +
                    "'" + values.designation + "',";
            if ((values.start_date == null) || (values.start_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.start_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if ((values.end_date == null) || (values.end_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.end_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + values.lastyear_turnover + "'," +
                    "'" + values.escrow + "'," +
                    "'" + values.urn_status + "'," +
                    "'" + values.urn + "'," +
                    "'Incomplete'," +
                    "'" + values.tan_number + "'," +
                    "'" + values.incometax_returnsstatus + "',";
            if (values.revenue == null || values.revenue == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.revenue.Replace(",", "") + "',";
            }
            if (values.profit == null || values.profit == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.profit.Replace(",", "") + "',";
            }
            if (values.fixed_assets == null || values.fixed_assets == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.fixed_assets.Replace(",", "") + "',";
            }
            if (values.sundrydebt_adv == null || values.sundrydebt_adv == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.sundrydebt_adv.Replace(",", "") + "',";
            }
            if ((values.lei_renewaldate == null) || (values.lei_renewaldate == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.lei_renewaldate).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL +=
                "'" + values.msme_registration + "'," +
                "'" + values.lglentity_id + "'," +
                "'" + values.kin + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "update agr_mst_tsupronboardinstitution2branch set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardinstitution2mobileno set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardinstitution2email set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardinstitution2address set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardinstitution2ratingdetail set institution_gid='" + msGetGid + "', application_gid ='" + values.application_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardinstitution2licensedtl set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardinstitution2bankdtl set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_trn_ttandtl set function_gid='" + msGetGid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select companydocument_gid, institution2documentupload_gid from agr_mst_tsupronboardinstitution2documentupload where institution_gid='" + employee_gid + "'";
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
                    msSQL = " insert into agr_trn_tdocumentchecktls(" +
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
                        "'" + values.application_gid + "'," +
                        "'" + msGetGid + "'," +
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
                        msSQL = " insert into agr_trn_tcovanantdocumentcheckdtls(" +
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
                       "'" + values.application_gid + "'," +
                       "'" + msGetGid + "'," +
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

                //DaAgrMstScannedDocument objvalues = new DaAgrMstScannedDocument();
                //objvalues.DaGroupDocChecklistinfo(lsapplication_gid, msGetGid, employee_gid);

                msSQL = "update agr_mst_tsupronboardinstitution2documentupload set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardinstitution2form60documentupload set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycgstsbpan set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Institution Information Saved Successfully";
                values.status = true;
                return true;

            }
            else
            {
                values.message = "Error Occured";
                values.status = false;
                return false;
            }
        }


        public void DaIndividualSave(string employee_gid, MdlMstBuyerOnboardContact values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("CTCT");

            //msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            //lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select stakeholder_type from agr_mst_tsupronboardcontact where application_gid='" + values.application_gid + "' and stakeholder_type in ('Borrower','Applicant')";
            string lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholdertype_name)
            {

                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return;
            }

            msSQL = "select stakeholder_type from agr_mst_tsupronboard2institution where application_gid='" + values.application_gid + "' and stakeholder_type in ('Borrower','Applicant')";
            lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholdertype_name)
            {

                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return;
            }
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
                   "'" + values.application_gid + "'," +
                   "'" + employee_gid + "'," +
                   "'" + values.pan_status + "'," +
                   "'" + values.pan_no + "'," +
                   "'" + values.aadhar_no + "',";
            if (values.first_name == "" || values.first_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.first_name.Replace("'", "") + "',";
            }
            if (values.middle_name == "" || values.middle_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.middle_name.Replace("'", "") + "',";
            }
            if (values.last_name == "" || values.last_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.last_name.Replace("'", "") + "',";
            }
            msSQL += "'" + values.individual_dob + "'," +
                     "'" + values.age + "'," +
                         "'" + values.gender_gid + "'," +
                         "'" + values.gender_name + "'," +
                         "'" + values.designation_gid + "'," +
                         "'" + values.designation_name + "'," +
                         "'" + values.educationalqualification_gid + "'," +
                         "'" + values.educationalqualification_name + "'," +
                         "'" + values.main_occupation + "'," +
                         "'" + values.annual_income + "'," +
                         "'" + values.monthly_income + "'," +
                         "'" + values.pep_status + "',";

            if ((values.pepverified_date == null) || (values.pepverified_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.pepverified_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }



            msSQL += "'" + values.stakeholdertype_gid + "'," +
                     "'" + values.stakeholdertype_name + "'," +
                     "'" + values.maritalstatus_gid + "'," +
                     "'" + values.maritalstatus_name + "',";
            if (values.father_firstname == "" || values.father_firstname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.father_firstname.Replace("'", "") + "',";
            }
            if (values.father_middlename == "" || values.father_middlename == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.father_middlename.Replace("'", "") + "',";
            }
            if (values.father_lastname == "" || values.father_lastname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.father_lastname.Replace("'", "") + "',";
            }
            msSQL += "'" + values.father_dob + "'," +
                     "'" + values.father_age + "',";
            if (values.mother_firstname == "" || values.mother_firstname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.mother_firstname.Replace("'", "") + "',";
            }
            if (values.mother_middlename == "" || values.mother_middlename == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.mother_middlename.Replace("'", "") + "',";
            }
            if (values.mother_lastname == "" || values.mother_lastname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.mother_lastname.Replace("'", "") + "',";
            }
            msSQL += "'" + values.mother_dob + "'," +
                     "'" + values.mother_age + "',";
            if (values.spouse_firstname == "" || values.spouse_firstname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.spouse_firstname.Replace("'", "") + "',";
            }
            if (values.spouse_middlename == "" || values.spouse_middlename == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.spouse_middlename.Replace("'", "") + "',";
            }
            if (values.spouse_lastname == "" || values.spouse_lastname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.spouse_lastname.Replace("'", "") + "',";
            }
            msSQL += "'" + values.spouse_dob + "'," +
                     "'" + values.spouse_age + "'," +
                         "'" + values.ownershiptype_gid + "'," +
                         "'" + values.ownershiptype_name + "'," +
                         "'" + values.propertyholder_gid + "'," +
                         "'" + values.propertyholder_name + "'," +
                         "'" + values.residencetype_gid + "'," +
                         "'" + values.residencetype_name + "'," +
                         "'" + values.incometype_gid + "'," +
                         "'" + values.incometype_name + "'," +
                         "'" + values.currentresidence_years + "'," +
                         "'" + values.branch_distance + "'," +
                          "'" + values.group_gid + "'," +
                     "'" + values.group_name + "'," +
                     "'" + values.profile + "'," +
                     "'" + values.urn_status + "'," +
                     "'" + values.urn + "'," +
                     "'" + values.fathernominee_status + "'," +
                     "'" + values.mothernominee_status + "'," +
                     "'" + values.spousenominee_status + "'," +
                     "'" + values.othernominee_status + "'," +
                     "'" + values.relationshiptype + "',";
            if (values.nomineefirst_name == "" || values.nomineefirst_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.nomineefirst_name.Replace("'", "") + "',";
            }
            if (values.nominee_middlename == "" || values.nominee_middlename == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.nominee_middlename.Replace("'", "") + "',";
            }
            if (values.nominee_lastname == "" || values.nominee_lastname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.nominee_lastname.Replace("'", "") + "',";
            }
            msSQL += "'" + values.nominee_dob + "'," +
                     "'" + values.nominee_age + "'," +
                     "'" + values.totallandinacres + "'," +
                     "'" + values.cultivatedland + "'," +
                     "'" + values.previouscrop + "'," +
                     "'" + values.prposedcrop + "'," +
                     "'" + values.institution_gid + "'," +
                     "'" + values.institution_name + "'," +
                         "'Incomplete'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                if (values.pan_status == "Customer Submitting Form 60")
                {
                    // PAN Update
                    foreach (string reason in values.panabsencereason_selectedlist)
                    {
                        msGetGidpan = objcmnfunctions.GetMasterGID("C2PR");
                        msSQL = " INSERT INTO agr_mst_tsupronboardcontact2panabsencereason(" +
                               " contact2panabsencereason_gid," +
                               " contact_gid," +
                               " panabsencereason," +
                               " created_date," +
                               " created_by)" +
                               " VALUES(" +
                               "'" + msGetGidpan + "'," +
                               "'" + msGetGid + "'," +
                               "'" + reason + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               "'" + employee_gid + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                // Updates

                msSQL = "update agr_mst_tsupronboardcontact2mobileno set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2email set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2address set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2idproof set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2bankdtl set contact_gid='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                msSQL = "select individualdocument_gid, contact2document_gid from agr_mst_tsupronboardcontact2document where contact_gid='" + employee_gid + "'";
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
                    msSQL = " insert into agr_trn_tdocumentchecktls(" +
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
                        "'" + msGetGid + "'," +
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
                        msSQL = " insert into agr_trn_tcovanantdocumentcheckdtls(" +
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
                       "'" + msGetGid + "'," +
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

                //DaAgrMstScannedDocument objvalues = new DaAgrMstScannedDocument();
                //objvalues.DaGroupDocChecklistinfo(lsapplication_gid, msGetGid, employee_gid);

                msSQL = "update agr_mst_tsupronboardcontact2document set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpanauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpanaadhaarlink set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycdlauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycepicauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpassportauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2panform60 set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2panabsencereason set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Individual Details Saved Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        //public void DaSaveGeneralDtl(MdlMstBuyerOnboardApplicationAdd values, string employee_gid)
        //{

        //    msGetGid = objcmnfunctions.GetMasterGID("APPC");
        //    string gsvernacularlanguage_gid = string.Empty;
        //    string gsvernacular_language = string.Empty;

        //    if (values.vernacularlanguage_list != null)
        //    {
        //        for (var i = 0; i < values.vernacularlanguage_list.Count; i++)
        //        {
        //            gsvernacularlanguage_gid += values.vernacularlanguage_list[i].vernacularlanguage_gid + ",";
        //            gsvernacular_language += values.vernacularlanguage_list[i].vernacular_language + ",";

        //        }

        //        gsvernacularlanguage_gid = gsvernacularlanguage_gid.TrimEnd(',');
        //        gsvernacular_language = gsvernacular_language.TrimEnd(',');
        //    }

        //    msSQL = "select concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as employee_name" +
        //            " from hrm_mst_temployee a" +
        //            " left join adm_mst_tuser b on a.user_gid=b.user_gid" +
        //            " where a.employee_gid='" + employee_gid + "'";
        //    lsemployee_name = objdbconn.GetExecuteScalar(msSQL);


        //    msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
        //    lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

        //    if (lsapplication_gid == "" || lsapplication_gid == null)
        //    {
        //        lsapplication_gid = values.application_gid;
        //    }

        //            msSQL = " insert into agr_mst_tsupronboard(" +
        //                " application_gid," +
        //                " customer_urn," +
        //                " customerref_name," +
        //                " vertical_gid," +
        //                " vertical_name," +
        //                " constitution_gid," +
        //                " constitution_name," +
        //                " onboarding_status, " +
        //                " sa_status," +
        //                " saname_gid," +
        //                " sa_name," +
        //                " relationshipmanager_name," +
        //                " relationshipmanager_gid," +
        //                " drm_gid," +
        //                " drm_name," +
        //                " vernacular_language," +
        //                " vernacularlanguage_gid," +
        //                " contactpersonfirst_name," +
        //                " contactpersonmiddle_name," +
        //                " contactpersonlast_name," +
        //                " designation_gid," +
        //                " designation_type," +
        //                " landline_no," +
        //                " baselocation_gid," +
        //               " baselocation_name," +
        //               " program_gid," +
        //               " program_name," +
        //               " product_gid," +
        //               " product_name," +
        //               " variety_gid," +
        //               " variety_name," +
        //               " sector_name," +
        //               " category_name," +
        //               " botanical_name," +
        //               " alternative_name," +
        //             " status," +
        //                " created_by," +
        //                " created_date) values(" +
        //                  "'" + msGetGid + "'," +
        //                    "'" + values.customer_urn + "'," +
        //                    "'" + values.customer_name + "'," +
        //                    "'" + values.vertical_gid + "'," +
        //                    "'" + values.vertical_name + "'," +
        //                    "'" + values.constitution_gid + "'," +
        //                    "'" + values.constitution_name + "'," +
        //                    "'" + values.onboarding_status + "'," +
        //                    "'" + values.sa_status + "'," +
        //                    "'" + values.saname_gid + "'," +
        //                    "'" + values.sa_name + "'," +
        //                    "'" + lsemployee_name + "'," +
        //                    "'" + employee_gid + "'," +
        //                    "'" + gsvernacular_language + "'," +
        //                    "'" + gsvernacularlanguage_gid + "'," +
        //                    "'" + values.contactpersonfirst_name + "'," +
        //                    "'" + values.contactpersonmiddle_name + "'," +
        //                    "'" + values.contactpersonlast_name + "'," +
        //                    "'" + values.designation_gid + "'," +
        //                    "'" + values.designation_type + "'," +
        //                    "'" + values.landline_no + "'," +
        //                    "'" + values.program_gid + "'," +
        //                    "'" + values.program_name + "'," +
        //                    "'" + values.product_gid + "'," +
        //                    "'" + values.product_name + "'," +
        //                    "'" + values.variety_gid + "'," +
        //                    "'" + values.variety_name + "'," +
        //                    "'" + values.sector_name + "'," +
        //                    "'" + values.category_name + "'," +
        //                    "'" + values.botanical_name + "'," +
        //                    "'" + values.alternative_name + "'," +
        //                    "'Incomplete'," +
        //                    "'" + employee_gid + "'," +
        //                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
        //            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        //            objODBCDatareader.Close();
        //        }
        //        else
        //        {
        //            objODBCDatareader.Close();
        //            values.message = "Location / Customer/Supplier Type not Assigned for Business Approval";
        //            values.status = false;
        //            return;
        //        }

        //        if (mnResult != 0)
        //        {

        //            msSQL = "update agr_mst_tapplication2product set application_gid ='" + msGetGid + "' where application_gid='" + employee_gid + "'";
        //            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        //            msSQL = "update agr_mst_tapplication2contactno set application_gid='" + msGetGid + "' where application_gid='" + employee_gid + "'";
        //            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        //            if (mnResult != 0)
        //            {
        //                msSQL = "update agr_mst_tapplication2email set application_gid='" + msGetGid + "' where application_gid='" + employee_gid + "'";
        //                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        //                if (mnResult != 0)
        //                {
        //                    msSQL = "update agr_mst_tapplication2geneticcode set application_gid='" + msGetGid + "' where application_gid='" + employee_gid + "'";
        //                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        //                    if (mnResult != 0)
        //                    {
        //                        msSQL = "insert into tmp_application(application_gid,employee_gid)values('" + msGetGid + "','" + employee_gid + "')";
        //                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        //                        values.message = "General Information Saved successfully";
        //                        values.status = true;
        //                    }
        //                    else
        //                    {
        //                        values.message = "Error Occured while Saving Information";
        //                        values.status = false;
        //                    }
        //                }
        //                else
        //                {
        //                    values.message = "Error Occured while Saving Information";
        //                    values.status = false;
        //                }
        //            }
        //            else
        //            {
        //                values.message = "Error Occured while Saving Information";
        //                values.status = false;
        //            }
        //        }
        //        else
        //        {
        //            values.message = "Error Occured while Saving Information";
        //            values.status = false;
        //        }
        //    }
        //    else
        //    {
        //        if (lsapplication_gid == null || lsapplication_gid == "")
        //        {
        //            lsapplication_gid = values.application_gid;
        //        }

        //            msSQL = " update agr_mst_tapplication set " +
        //                  " application_no='" + lsapp_refno + "'," +
        //                   " customer_urn='" + values.customer_urn + "'," +
        //                   " customerref_name='" + values.customer_name + "'," +
        //                   " vertical_gid='" + values.vertical_gid + "'," +
        //                   " vertical_name='" + values.vertical_name + "'," +
        //                   " constitution_gid='" + values.constitution_gid + "'," +
        //                   " constitution_name='" + values.constitution_name + "'," +
        //                   " onboarding_status='" + values.onboarding_status + "'," +
        //                   " sa_status='" + values.sa_status + "'," +
        //                   " sa_name='" + values.sa_name + "'," +
        //                   " saname_gid='" + values.saname_gid + "'," +
        //                   " vernacularlanguage_gid='" + gsvernacularlanguage_gid + "'," +
        //                   " vernacular_language='" + gsvernacular_language + "'," +
        //                   " contactpersonfirst_name='" + values.contactpersonfirst_name + "'," +
        //                   " contactpersonmiddle_name='" + values.contactpersonmiddle_name + "'," +
        //                   " contactpersonlast_name='" + values.contactpersonlast_name + "'," +
        //                   " designation_gid='" + values.designation_gid + "'," +
        //                   " designation_type='" + values.designation_type + "'," +
        //                   " landline_no='" + values.landline_no + "'," +
        //                   " product_gid= '" + values.product_gid + "'," +
        //                   " product_name='" + values.product_name + "'," +
        //                   " variety_gid= '" + values.variety_gid + "'," +
        //                   " variety_name='" + values.variety_name + "'," +
        //                   " sector_name= '" + values.sector_name + "'," +
        //                   " category_name='" + values.category_name + "'," +
        //                   " botanical_name= '" + values.botanical_name + "'," +
        //                   " alternative_name='" + values.alternative_name + "'," +
        //                   " status = 'Incomplete'," +
        //                   " updated_by='" + employee_gid + "'," +
        //                   " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
        //                   " where application_gid='" + lsapplication_gid + "' ";
        //            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        //            objODBCDatareader.Close();
        //        }
        //        else
        //        {
        //            objODBCDatareader.Close();
        //            values.message = "Location / Customer/Supplier Type not Assigned for Business Approval";
        //            values.status = false;
        //            return;
        //        }

        //        if (mnResult != 0)
        //        {

        //            msSQL = "update agr_mst_tapplication2product set application_gid ='" + lsapplication_gid + "' where application_gid='" + employee_gid + "'";
        //            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        //            msSQL = "update agr_mst_tapplication2contactno set application_gid='" + lsapplication_gid + "' where application_gid='" + employee_gid + "'";
        //            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        //            msSQL = "update agr_mst_tapplication2email set application_gid='" + lsapplication_gid + "' where application_gid='" + employee_gid + "'";
        //            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        //            msSQL = "update agr_mst_tapplication2geneticcode set application_gid='" + lsapplication_gid + "' where application_gid='" + employee_gid + "'";
        //            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        //            values.message = "General Information Saved successfully";
        //            values.status = true;
        //        }
        //        else
        //        {
        //            values.message = "Error Occured while Saving Information";
        //            values.status = false;
        //        }
        //    }

        //}

        public bool DaSaveInstitutionEditDtl(MdlMstBuyerOnboardInstitutionAdd values, string employee_gid)
        {

            msSQL = "select application_gid from agr_mst_tsupronboard2institution where institution_gid='" + values.institution_gid + "'";
            values.application_gid = objdbconn.GetExecuteScalar(msSQL);
            if (values.stakeholder_type == "Borrower" || values.stakeholder_type == "Applicant")
            {
                msSQL = "select stakeholder_type from agr_mst_tsupronboardcontact where application_gid='" + values.application_gid + "' and stakeholder_type in ('Borrower','Applicant')";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Already Added Applicant/Borrower Information";
                    return false;
                }
                msSQL = "select stakeholder_type from agr_mst_tsupronboard2institution where application_gid='" + values.application_gid + "' and " +
                    " stakeholder_type in ('Borrower','Applicant') and institution_gid<> '" + values.institution_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Already Added Applicant/Borrower Information";
                    return false;
                }
            }
            msSQL = " update agr_mst_tsupronboard2institution set " +
                        " company_name='" + values.company_name.Replace("'", "") + "',";
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
            msSQL += " lastyear_turnover='" + values.lastyear_turnover.Replace("'", "") + "'," +
                     " escrow='" + values.escrow + "'," +
                     " urn_status='" + values.urn_status + "'," +
                     " urn='" + values.urn + "'," +
                     " updated_by='" + employee_gid + "'," +
                     " institution_status='Incomplete'," +
                    " tan_number='" + values.tan_number.Replace("'", "") + "'," +
                         " incometax_returnsstatus='" + values.incometax_returnsstatus + "',";
            if (values.revenue == null || values.revenue == "")
            {
                msSQL += "revenue='0.00',";
            }
            else
            {
                msSQL += " revenue ='" + values.revenue.Replace(",", "") + "',";
            }
            if (values.profit == null || values.profit == "")
            {
                msSQL += "profit='0.00',";
            }
            else
            {
                msSQL += "profit='" + values.profit.Replace(",", "") + "',";
            }
            if (values.fixed_assets == null || values.fixed_assets == "")
            {
                msSQL += "fixed_assets='0.00',";
            }
            else
            {
                msSQL += "fixed_assets='" + values.fixed_assets.Replace(",", "") + "',";
            }
            if (values.sundrydebt_adv == null || values.sundrydebt_adv == "")
            {
                msSQL += "sundrydebt_adv='0.00',";
            }
            else
            {
                msSQL += "sundrydebt_adv='" + values.sundrydebt_adv.Replace(",", "") + "',";
            }
            if (Convert.ToDateTime(values.lei_renewaldate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {
            }
            else
            {
                msSQL += " lei_renewaldate='" + Convert.ToDateTime(values.lei_renewaldate).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
            }
            msSQL +=
                "msme_registration='" + values.msme_registration + "'," +
                "lglentity_id='" + values.lglentity_id + "'," +
                "kin='" + values.kin + "'," +
             " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where institution_gid='" + values.institution_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {

                // Updates for Multiple Add
                msSQL = "update agr_mst_tsupronboardinstitution2branch set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardinstitution2mobileno set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardinstitution2email set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardinstitution2address set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardinstitution2licensedtl set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardinstitution2bankdtl set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardinstitution2documentupload set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardinstitution2form60documentupload set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardinstitution2ratingdetail set institution_gid='" + values.institution_gid + "', application_gid ='" + values.application_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycgstsbpan set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_trn_ttandtl set function_gid='" + values.institution_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Institution Details Saved Successfully";
                return true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;
                return false;
            }
        }


        public void DaSaveIndividualEditDtl(MdlMstBuyerOnboardContact values, string employee_gid)
        {

            msSQL = "select application_gid from agr_mst_tsupronboardcontact where contact_gid='" + values.contact_gid + "'";
            values.application_gid = objdbconn.GetExecuteScalar(msSQL);
            if (values.stakeholder_type == "Borrower" || values.stakeholder_type == "Applicant")
            {
                msSQL = "select stakeholder_type from agr_mst_tsupronboardcontact where application_gid='" + values.application_gid + "' and stakeholder_type in ('Borrower','Applicant')" +
                " and contact_gid<>'" + values.contact_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Already Added Applicant/Borrower Information";
                    return;
                }
                msSQL = "select stakeholder_type from agr_mst_tsupronboard2institution where application_gid='" + values.application_gid + "' and " +
                    " stakeholder_type in ('Borrower','Applicant') ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Already Added Applicant/Borrower Information";
                    return;
                }

            }

            msSQL = "select pan_status from agr_mst_tsupronboardcontact where contact_gid='" + values.contact_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (values.pan_status == "Customer Submitting PAN")
            {
                msSQL = "delete from agr_mst_tsupronboardcontact2panform60 where contact_gid='" + values.contact_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "delete from agr_mst_tsupronboardcontact2panabsencereason where contact_gid='" + values.contact_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            objODBCDatareader.Close();

            msSQL = " update agr_mst_tsupronboardcontact set " +
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
                   " contact_status='Incomplete'," +
                   " updated_by='" + employee_gid + "'," +
                   " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                   " where contact_gid='" + values.contact_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                if (values.pan_status == "Customer Submitting Form 60")
                {
                    matchCount1 = 0;
                    matchCount2 = 0;

                    msSQL = " select panabsencereason from agr_mst_tsupronboardcontact2panabsencereason" +
                            " where contact_gid='" + values.contact_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        values.BuyerOnboardpanabsencereason_list = dt_datatable.AsEnumerable().Select(row =>
                         new BuyerOnboardpanabsencereason_list
                         {
                             panabsencereason = row["panabsencereason"].ToString(),
                         }
                       ).ToList();
                    }
                    dt_datatable.Dispose();
                    if (values.BuyerOnboardpanabsencereason_list == null)
                    {
                        foreach (string reason in values.panabsencereason_selectedlist)
                        {
                            msGetGid = objcmnfunctions.GetMasterGID("C2PR");
                            msSQL = " INSERT INTO agr_mst_tsupronboardcontact2panabsencereason(" +
                                    " contact2panabsencereason_gid," +
                                    " contact_gid," +
                                    " panabsencereason," +
                                    " created_date," +
                                    " created_by)" +
                                    " VALUES(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + reason.Replace("'", "") + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                    "'" + employee_gid + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        for (var i = 0; i < values.panabsencereason_selectedlist.Count; i++)
                        {
                            for (var j = 0; j < values.BuyerOnboardcontactpanabsencereason_list.Count; j++)
                            {
                                if (values.panabsencereason_selectedlist[i] == values.BuyerOnboardcontactpanabsencereason_list[j].panabsencereason)
                                {
                                    matchCount1++;
                                }
                            }
                            if (matchCount1 == 0)
                            {
                                msGetGid = objcmnfunctions.GetMasterGID("C2PR");
                                msSQL = " INSERT INTO agr_mst_tsupronboardcontact2panabsencereason(" +
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

                        for (var i = 0; i < values.BuyerOnboardcontactpanabsencereason_list.Count; i++)
                        {
                            for (var j = 0; j < values.panabsencereason_selectedlist.Count; j++)
                            {
                                if (values.BuyerOnboardcontactpanabsencereason_list[i].panabsencereason == values.panabsencereason_selectedlist[j])
                                {
                                    matchCount2++;
                                }
                            }
                            if (matchCount2 == 0)
                            {
                                msSQL = "delete from agr_mst_tsupronboardcontact2panabsencereason where panabsencereason='" + values.BuyerOnboardcontactpanabsencereason_list[i].panabsencereason + "' and contact_gid = '" + values.contact_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            matchCount2 = 0;
                        }
                    }
                }
                //Updates
                msSQL = "update agr_mst_tsupronboardcontact2mobileno set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2email set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2address set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2idproof set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2panform60 set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2panabsencereason set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2bankdtl set contact_gid='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select individualdocument_gid, contact2document_gid from agr_mst_tsupronboardcontact2document where contact_gid='" + employee_gid + "'";
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
                    msSQL = " insert into agr_trn_tdocumentchecktls(" +
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
                        msSQL = " insert into agr_trn_tcovanantdocumentcheckdtls(" +
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

                //DaAgrMstScannedDocument objvalues = new DaAgrMstScannedDocument();
                //objvalues.DaGroupDocChecklistinfo(lsapplication_gid, values.contact_gid, employee_gid);

                msSQL = "update agr_mst_tsupronboardcontact2document set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpanauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpanaadhaarlink set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycdlauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycepicauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpassportauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2panform60 set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2panabsencereason set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Individual Details Saved Successfully";
            }

            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        public void DaSubmitIndividualEditDtl(string employee_gid, MdlMstBuyerOnboardContact values)
        {
            //msSQL = "select application_gid from agr_mst_tsupronboardcontact where contact_gid='" + values.contact_gid + "'";
            //lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            if (values.stakeholder_type == "Borrower" || values.stakeholder_type == "Applicant")
            {
                msSQL = "select stakeholder_type from agr_mst_tsupronboardcontact where application_gid='" + values.application_gid + "' and stakeholder_type in ('Borrower','Applicant')" +
                    " and contact_gid<>'" + values.contact_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Already Added Applicant/Borrower Information";
                    return;
                }
                msSQL = "select stakeholder_type from agr_mst_tsupronboard2institution where application_gid='" + values.application_gid + "' and " +
                    " stakeholder_type in ('Borrower','Applicant') ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Already Added Applicant/Borrower Information";
                    return;
                }
            }
            msSQL = "select contact_gid from agr_mst_tsupronboardcontact2mobileno where  (contact_gid='" + employee_gid + "' or  contact_gid='" + values.contact_gid + "') and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Mobile Number ";
                return;
            }
            objODBCDatareader.Close();

            msSQL = "select contact_gid from agr_mst_tsupronboardcontact2email where primary_status='Yes' and (contact_gid='" + employee_gid + "' or  contact_gid='" + values.contact_gid + "')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add primary Email Address";
                return;
            }
            objODBCDatareader.Close();
            msSQL = "select contact_gid from agr_mst_tsupronboardcontact2address where primary_status='Yes' and ( contact_gid='" + employee_gid + "' or  contact_gid='" + values.contact_gid + "') ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add primary Address";
                return;
            }
            objODBCDatareader.Close();

            msSQL = "select pan_status from agr_mst_tsupronboardcontact where contact_gid='" + values.contact_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (values.pan_status == "Customer Submitting PAN")
            {
                msSQL = "delete from agr_mst_tsupronboardcontact2panform60 where contact_gid='" + values.contact_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "delete from agr_mst_tsupronboardcontact2panabsencereason where contact_gid='" + values.contact_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }
            objODBCDatareader.Close();

            msSQL = " update agr_mst_tsupronboardcontact set " +
                    " pan_status ='" + values.pan_status + "'," +
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
                   " contact_status='Completed'," +
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

                    msSQL = " select panabsencereason from agr_mst_tsupronboardcontact2panabsencereason" +
                            " where contact_gid='" + values.contact_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        values.BuyerOnboardcontactpanabsencereason_list = dt_datatable.AsEnumerable().Select(row =>
                         new BuyerOnboardcontactpanabsencereason_list
                         {
                             panabsencereason = row["panabsencereason"].ToString(),
                         }
                       ).ToList();
                    }
                    dt_datatable.Dispose();
                    if (values.BuyerOnboardcontactpanabsencereason_list == null)
                    {
                        foreach (string reason in values.panabsencereason_selectedlist)
                        {
                            msGetGid = objcmnfunctions.GetMasterGID("C2PR");
                            msSQL = " INSERT INTO agr_mst_tsupronboardcontact2panabsencereason(" +
                                    " contact2panabsencereason_gid," +
                                    " contact_gid," +
                                    " panabsencereason," +
                                    " created_date," +
                                    " created_by)" +
                                    " VALUES(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + reason.Replace("'", "") + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                    "'" + employee_gid + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else
                    {
                        for (var i = 0; i < values.panabsencereason_selectedlist.Count; i++)
                        {
                            for (var j = 0; j < values.BuyerOnboardcontactpanabsencereason_list.Count; j++)
                            {
                                if (values.panabsencereason_selectedlist[i] == values.BuyerOnboardcontactpanabsencereason_list[j].panabsencereason)
                                {
                                    matchCount1++;
                                }
                            }
                            if (matchCount1 == 0)
                            {
                                msGetGid = objcmnfunctions.GetMasterGID("C2PR");
                                msSQL = " INSERT INTO agr_mst_tsupronboardcontact2panabsencereason(" +
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

                        for (var i = 0; i < values.BuyerOnboardcontactpanabsencereason_list.Count; i++)
                        {
                            for (var j = 0; j < values.panabsencereason_selectedlist.Count; j++)
                            {
                                if (values.BuyerOnboardcontactpanabsencereason_list[i].panabsencereason == values.panabsencereason_selectedlist[j])
                                {
                                    matchCount2++;
                                }
                            }
                            if (matchCount2 == 0)
                            {
                                msSQL = "delete from agr_mst_tsupronboardcontact2panabsencereason where panabsencereason='" + values.BuyerOnboardcontactpanabsencereason_list[i].panabsencereason + "' and contact_gid = '" + values.contact_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            matchCount2 = 0;
                        }
                    }
                }
                //Updates
                msSQL = "update agr_mst_tsupronboardcontact2mobileno set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2email set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2address set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2idproof set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2panform60 set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2panabsencereason set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2bankdtl set contact_gid='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select individualdocument_gid , contact2document_gid from agr_mst_tsupronboardcontact2document where contact_gid='" + employee_gid + "'";
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
                    msSQL = " insert into agr_trn_tdocumentchecktls(" +
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
                        msSQL = " insert into agr_trn_tcovanantdocumentcheckdtls(" +
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

                //DaAgrMstScannedDocument objvalues = new DaAgrMstScannedDocument();
                //objvalues.DaGroupDocChecklistinfo(lsapplication_gid, values.contact_gid, employee_gid);

                msSQL = "update agr_mst_tsupronboardcontact2document set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpanauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpanaadhaarlink set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycdlauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycepicauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpassportauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2panform60 set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsupronboardcontact2panabsencereason set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select mobile_no from agr_mst_tsupronboardcontact2mobileno where contact_gid='" + values.contact_gid + "' and primary_status='yes'";
                string lsmobileno = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select email_address from agr_mst_tsupronboardcontact2email where contact_gid='" + values.contact_gid + "' and primary_status='yes'";
                lsemail_address = objdbconn.GetExecuteScalar(msSQL);

                if (values.stakeholder_type == "Borrower" || values.stakeholder_type == "Applicant")
                {
                    msSQL = "update agr_mst_tsupronboard set applicant_type ='Individual' where application_gid='" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsupronboardcontact set mobile_no='" + lsmobileno + "'," +
                        " email_address='" + lsemail_address + "' where contact_gid='" + values.contact_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                values.status = true;
                values.message = "Individual Details Submitted Successfully";
            }

            else
            {
                values.status = false;
                values.message = "Error Occured while submit individual";
            }
        }

        public bool DaSubmitInstitutionEditDtl(MdlMstBuyerOnboardInstitutionAdd values, string employee_gid)
        {
            msSQL = "select institution_gid from agr_mst_tsupronboardinstitution2mobileno where institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "' and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Primary Mobile Number";
                return false;
            }

            msSQL = "select institution_gid from agr_mst_tsupronboardinstitution2mobileno where institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Atleast One Mobile Number";
                return false;
            }

            msSQL = "select institution_gid from agr_mst_tsupronboardinstitution2email where institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Atleast One Email Address";
                return false;
            }

            msSQL = "select institution_gid from agr_mst_tsupronboardinstitution2address where institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Atleast One Address Detail";
                return false;
            }

            if (values.Gstflag == "Yes")
            {
                msSQL = "select institution2branch_gid from agr_mst_tsupronboardinstitution2branch where (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "') and headoffice_status ='Yes' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == false)
                {
                    values.status = false;
                    values.message = "Atleast Select One GST Number as Head Office";
                    return false;
                }
                objODBCDatareader.Close();
            }

            msSQL = "select application_gid from agr_mst_tsupronboard2institution where institution_gid='" + values.institution_gid + "'";
            values.application_gid = objdbconn.GetExecuteScalar(msSQL);
            if (values.stakeholder_type == "Borrower" || values.stakeholder_type == "Applicant")
            {
                msSQL = "select stakeholder_type from agr_mst_tsupronboardcontact where application_gid='" + values.application_gid + "' and stakeholder_type in ('Borrower','Applicant')";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Already Added Applicant/Borrower Information";
                    return false;
                }
                msSQL = "select stakeholder_type from agr_mst_tsupronboard2institution where application_gid='" + values.application_gid + "' and" +
                    " stakeholder_type in ('Borrower','Applicant') and institution_gid<> '" + values.institution_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Already Added Applicant/Borrower Information";
                    return false;
                }
            }
            try
            {
                msSQL = " update agr_mst_tsupronboard2institution set " +
                        " company_name='" + values.company_name.Replace("'", "") + "',";
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
                          " tan_number='" + values.tan_number + "'," +
                         " incometax_returnsstatus='" + values.incometax_returnsstatus + "',";
                if (values.revenue == null || values.revenue == "")
                {
                    msSQL += "revenue='0.00',";
                }
                else
                {
                    msSQL += " revenue ='" + values.revenue.Replace(",", "") + "',";
                }
                if (values.profit == null || values.profit == "")
                {
                    msSQL += "profit='0.00',";
                }
                else
                {
                    msSQL += "profit='" + values.profit.Replace(",", "") + "',";
                }
                if (values.fixed_assets == null || values.fixed_assets == "")
                {
                    msSQL += "fixed_assets='0.00',";
                }
                else
                {
                    msSQL += "fixed_assets='" + values.fixed_assets.Replace(",", "") + "',";
                }
                if (values.sundrydebt_adv == null || values.sundrydebt_adv == "")
                {
                    msSQL += "sundrydebt_adv='0.00',";
                }
                else
                {
                    msSQL += "sundrydebt_adv='" + values.sundrydebt_adv.Replace(",", "") + "',";
                }
                if (Convert.ToDateTime(values.lei_renewaldate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {
                }
                else
                {
                    msSQL += " lei_renewaldate='" + Convert.ToDateTime(values.lei_renewaldate).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                msSQL +=
                    "msme_registration='" + values.msme_registration + "'," +
                    "lglentity_id='" + values.lglentity_id + "'," +
                    "kin='" + values.kin + "'," +
                                 " institution_status='Completed'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where institution_gid='" + values.institution_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {

                    // Updates for Multiple Add
                    msSQL = "update agr_mst_tsupronboardinstitution2branch set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsupronboardinstitution2mobileno set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsupronboardinstitution2email set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsupronboardinstitution2address set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsupronboardinstitution2licensedtl set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsupronboardinstitution2bankdtl set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsupronboardinstitution2ratingdetail set institution_gid='" + values.institution_gid + "', application_gid ='" + values.application_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_trn_ttandtl set function_gid='" + values.institution_gid + "' where function_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "select companydocument_gid , institution2documentupload_gid from agr_mst_tsupronboardinstitution2documentupload where institution_gid='" + employee_gid + "'";
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
                        msSQL = " insert into agr_trn_tdocumentchecktls(" +
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
                            "'" + values.application_gid + "'," +
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
                            msSQL = " insert into agr_trn_tcovanantdocumentcheckdtls(" +
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
                           "'" + values.application_gid + "'," +
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

                    //DaAgrMstScannedDocument objvalues = new DaAgrMstScannedDocument();
                    //objvalues.DaGroupDocChecklistinfo(lsapplication_gid, values.institution_gid, employee_gid);

                    msSQL = "update agr_mst_tsupronboardinstitution2documentupload set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsupronboardinstitution2form60documentupload set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tkycgstsbpan set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "select mobile_no from agr_mst_tsupronboardinstitution2mobileno where institution_gid='" + values.institution_gid + "' and primary_status='yes'";
                    string lsmobileno = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select email_address from agr_mst_tsupronboardinstitution2email where institution_gid='" + values.institution_gid + "' and primary_status='yes'";
                    lsemail_address = objdbconn.GetExecuteScalar(msSQL);

                    if (values.stakeholder_type == "Borrower" || values.stakeholder_type == "Applicant")
                    {
                        msSQL = "update agr_mst_tsupronboard set applicant_type ='Institution' where application_gid='" + values.application_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_mst_tsupronboard2institution set mobile_no='" + lsmobile_no + "'," +
                         " email_address='" + lsemail_address + "' where institution_gid='" + values.institution_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                    values.status = true;
                    values.message = "Institution Details Submitted Successfully";
                    return true;
                }
                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
                return false;
            }
        }

        public void DaDeleteAppGeneticCode(string application2geneticcode_gid, MdlMstGeneticCode values, string employee_gid)
        {
            msSQL = "delete from agr_mst_tsupronboard2geneticcode where application2geneticcode_gid='" + application2geneticcode_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Genetic Code Details Deleted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaEditAppEmailAddress(string application2email_gid, MdlMstBuyerOnboardEmailAddress values)
        {
            try
            {
                msSQL = " select email_address,application2email_gid,primary_emailaddress from agr_mst_tsupronboard2email where " +
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

        public bool DaUpdateAppEmailAddress(string employee_gid, MdlMstBuyerOnboardEmailAddress values)
        {
            msSQL = " select email_address,application2email_gid,primary_emailaddress, application_gid from agr_mst_tsupronboard2email where " +
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

            msSQL = "select application2email_gid from agr_mst_tsupronboard2email where primary_status='Yes' and  (application2email_gid='" + values.application2email_gid + "')";
            string application2email_gid = objdbconn.GetExecuteScalar(msSQL);
            if (application2email_gid != (values.application2email_gid))
            {

                msSQL = "select primary_emailaddress from agr_mst_tsupronboard2email where primary_emailaddress='Yes' and (application_gid='" + employee_gid + "' or application_gid='" + values.application_gid + "')";
                string primary_status = objdbconn.GetExecuteScalar(msSQL);
                if (primary_status == (values.primary_emailaddress))
                {
                    values.status = false;
                    values.message = "Already Primary Email Address Added";
                    return false;
                }
            }


            try
            {
                msSQL = " update agr_mst_tsupronboard2email set " +
                         " email_address='" + values.email_address.Replace("'", "") + "'," +
                         " primary_emailaddress='" + values.primary_emailaddress.Replace("'", "") + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where application2email_gid='" + values.application2email_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("AEUL");

                    msSQL = "Insert into agr_mst_tsupronboard2emailupdatelog(" +
                   " application2emailupdatelog_gid, " +
                   " application2email_gid, " +
                   " application_gid " +
                   " email_address," +
                   " primary_emailaddress," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + values.application2email_gid + "'," +
                   "'" + values.application_gid + "'," +
                   "'" + lsemail_address + "'," +
                   "'" + lsprimary_emailaddress + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Email Address Updated Successfully";
                }

                if (ConfigurationManager.AppSettings["sysSamagroHyperbrdigeAPIEnable"].ToString() == "Yes")
                {
                    HttpContext ctx = HttpContext.Current;

                    Thread t = new Thread(new ThreadStart(() =>
                    {
                        HttpContext.Current = ctx;


                        if (lsemail_address != values.email_address)
                        {
                            objFnSamAgroHBAPIConnEdit.UpdateSupplierGeneralContactHBAPI(values.application2email_gid, UpdateContactHBAPIFrom.Email);
                        }

                    }));

                    t.Start();

                }


                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
                return false;
            }
        }

        public void DaEditAppMobileNo(string application2contact_gid, MdlMstBuyerOnboardMobileNo values)
        {
            try
            {
                msSQL = " select mobile_no,application2contact_gid,primary_mobileno,whatsapp_mobileno from agr_mst_tsupronboard2contactno where " +
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

        public bool DaUpdateAppMobileNo(string employee_gid, MdlMstBuyerOnboardMobileNo values)
        {
            msSQL = " select mobile_no,application2contact_gid,primary_mobileno,whatsapp_mobileno, application_gid from agr_mst_tsupronboard2contactno where " +
                    " application2contact_gid='" + values.application2contact_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsmobile_no = objODBCDatareader["mobile_no"].ToString();
                lsprimary_mobileno = objODBCDatareader["primary_mobileno"].ToString();
                lswhatsapp_mobileno = objODBCDatareader["whatsapp_mobileno"].ToString();
                lsapplication2contact_gid = objODBCDatareader["application2contact_gid"].ToString();
                lsapplication_gid = objODBCDatareader["application_gid"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = "select application2contact_gid from agr_mst_tsupronboard2contactno where primary_status='Yes' and  (application2contact_gid='" + values.application2contact_gid + "')";
            string application2contact_gid = objdbconn.GetExecuteScalar(msSQL);
            if (application2contact_gid != (values.application2contact_gid))
            {

                msSQL = "select primary_mobileno from agr_mst_tsupronboard2contactno where primary_status='Yes' and (application_gid='" + employee_gid + "' or application_gid='" + values.application_gid + "')";
                string primary_status = objdbconn.GetExecuteScalar(msSQL);
                if (primary_status == (values.primary_mobileno))
                {
                    values.status = false;
                    values.message = "Already Primary Mobile Number Added";
                    return false;
                }
            }

            try
            {
                msSQL = " update agr_mst_tsupronboard2contactno set " +
                         " mobile_no='" + values.mobile_no.Replace("'", "") + "'," +
                         " primary_mobileno='" + values.primary_mobileno + "'," +
                         " whatsapp_mobileno='" + values.whatsapp_mobileno + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where application2contact_gid='" + values.application2contact_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("AMUL");

                    msSQL = "Insert into agr_mst_tsupronboard2contactnoupdatelog(" +
                   " application2contactnoupdatelog_gid, " +
                   " application2contact_gid, " +
                   " application_gid, " +
                   " mobile_no," +
                   " primary_mobileno," +
                   " whatsapp_no," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + values.application2contact_gid + "'," +
                   "'" + values.application_gid + "'," +
                   "'" + lsmobile_no + "'," +
                   "'" + lsprimary_mobileno + "'," +
                   "'" + lswhatsapp_mobileno + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Mobile Number Updated Successfully";
                }

                if (ConfigurationManager.AppSettings["sysSamagroHyperbrdigeAPIEnable"].ToString() == "Yes")
                {
                    HttpContext ctx = HttpContext.Current;

                    Thread t = new Thread(new ThreadStart(() =>
                    {
                        HttpContext.Current = ctx;

                        if (lsmobile_no != values.mobile_no)
                        {
                            objFnSamAgroHBAPIConnEdit.UpdateSupplierGeneralContactHBAPI(values.application2contact_gid, UpdateContactHBAPIFrom.MobileNo);
                        }

                    }));

                    t.Start();
                }

                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
                return false;
            }
        }


        public void DaEditInstitutionGST(string institution2branch_gid, MdlMstBuyerOnboardGST values)
        {
            try
            {
                msSQL = "select gst_state, gst_no, institution_gid, institution2branch_gid, gst_registered" +
                    " from agr_mst_tsupronboardinstitution2branch where institution2branch_gid='" + institution2branch_gid + "'";
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

        public void DaUpdateInstitutionGST(string employee_gid, MdlMstBuyerOnboardGST values)
        {
            msSQL = "select gst_state, gst_no, gst_registered, institution_gid, institution2branch_gid" +
                " from agr_mst_tsupronboardinstitution2branch where institution2branch_gid='" + values.institution2branch_gid + "'";
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
                msSQL = " update agr_mst_tsupronboardinstitution2branch set " +
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

                    msSQL = "Insert into agr_mst_tsupronboardinstitution2branchupdatelog(" +
                   " institution2gstupdatelog_gid, " +
                   " institution2branch_gid, " +
                   " institution_gid, " +
                   " gst_state," +
                   " gst_no," +
                   " gst_registered," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + values.institution2branch_gid + "'," +
                   "'" + values.institution_gid + "'," +
                   "'" + lsgst_state + "'," +
                   "'" + lsgst_no + "'," +
                   "'" + lsgst_registered + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "GST Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaEditInstitutionMobileNo(string institution2mobileno_gid, MdlMstBuyerOnboardMobileNo values)
        {
            try
            {
                msSQL = " select mobile_no,institution2mobileno_gid,primary_status,whatsapp_no from agr_mst_tsupronboardinstitution2mobileno where " +
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

        public bool DaUpdateInstitutionMobileNo(string employee_gid, MdlMstBuyerOnboardMobileNo values)
        {
            msSQL = " select mobile_no,institution2mobileno_gid,primary_status,whatsapp_no, institution_gid from agr_mst_tsupronboardinstitution2mobileno where " +
                    " institution2mobileno_gid='" + values.institution2mobileno_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsmobile_no = objODBCDatareader["mobile_no"].ToString();
                lsprimary_status = objODBCDatareader["primary_status"].ToString();
                lswhatsapp_no = objODBCDatareader["whatsapp_no"].ToString();
                lsinstitution2mobileno_gid = objODBCDatareader["institution2mobileno_gid"].ToString();
                lsinstitution_gid = objODBCDatareader["institution_gid"].ToString();

            }
            objODBCDatareader.Close();


            msSQL = "select institution2mobileno_gid from agr_mst_tsupronboardinstitution2mobileno where primary_status='Yes' and  (institution2mobileno_gid='" + values.institution2mobileno_gid + "')";
            string application2contact_gid = objdbconn.GetExecuteScalar(msSQL);
            if (application2contact_gid != (values.application2contact_gid))
            {

                msSQL = "select primary_status from agr_mst_tsupronboardinstitution2mobileno where primary_status='Yes' and (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "')";
                string primary_status = objdbconn.GetExecuteScalar(msSQL);
                if (primary_status == (values.primary_status))
                {
                    values.status = false;
                    values.message = "Already Primary Mobile Number Added";
                    return false;
                }
            }


            try
            {
                msSQL = " update agr_mst_tsupronboardinstitution2mobileno set " +
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

                    msSQL = "Insert into agr_mst_tsupronboardinstitution2mobilenoupdatelog(" +
                   " institution2mobilenoupdatelog_gid, " +
                   " institution2mobileno_gid, " +
                   " institution_gid, " +
                   " mobile_no," +
                   " primary_status," +
                   " whatsapp_no," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + values.institution2mobileno_gid + "'," +
                   "'" + values.institution_gid + "'," +
                   "'" + lsmobile_no + "'," +
                   "'" + lsprimary_status + "'," +
                   "'" + lswhatsapp_no + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Institution Mobile Number Updated Successfully";
                }

                if (ConfigurationManager.AppSettings["sysSamagroHyperbrdigeAPIEnable"].ToString() == "Yes")
                {
                    HttpContext ctx = HttpContext.Current;

                    Thread t = new Thread(new ThreadStart(() =>
                    {
                        HttpContext.Current = ctx;

                        if (lsmobile_no != values.mobile_no)
                        {
                            objFnSamAgroHBAPIConnEdit.UpdateSupplierInstitutionContactHBAPI(values.institution2mobileno_gid, UpdateContactHBAPIFrom.MobileNo);
                        }

                    }));

                    t.Start();
                }

                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";

                return false;
            }
        }

        public void DaEditInstitutionEmailAddress(string institution2email_gid, MdlMstBuyerOnboardEmailAddress values)
        {
            try
            {
                msSQL = " select email_address,institution2email_gid,primary_status from agr_mst_tsupronboardinstitution2email where " +
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

        public bool DaUpdateInstitutionEmailAddress(string employee_gid, MdlMstBuyerOnboardEmailAddress values)
        {
            msSQL = " select email_address,institution2email_gid,primary_status, institution_gid from agr_mst_tsupronboardinstitution2email where " +
                        " institution2email_gid='" + values.institution2email_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsemail_address = objODBCDatareader["email_address"].ToString();
                lsprimary_status = objODBCDatareader["primary_status"].ToString();
                lsinstitution2email_gid = objODBCDatareader["institution2email_gid"].ToString();
                lsinstitution_gid = objODBCDatareader["institution_gid"].ToString();
            }
            objODBCDatareader.Close();


            msSQL = "select institution2email_gid from agr_mst_tsupronboardinstitution2email where primary_status='Yes' and  (institution2email_gid='" + values.institution2email_gid + "')";
            string institution2email_gid = objdbconn.GetExecuteScalar(msSQL);
            if (institution2email_gid != (values.institution2email_gid))
            {

                msSQL = "select primary_status from agr_mst_tsupronboardinstitution2email where primary_status='Yes' and (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "')";
                string primary_status = objdbconn.GetExecuteScalar(msSQL);
                if (primary_status == (values.primary_status))
                {
                    values.status = false;
                    values.message = "Already Primary Mail Number Added";
                    return false;
                }
            }

            try
            {
                msSQL = " update agr_mst_tsupronboardinstitution2email set " +
                         " email_address='" + values.email_address + "'," +
                         " primary_status='" + values.primary_status + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where institution2email_gid='" + values.institution2email_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("IEUL");

                    msSQL = "Insert into agr_mst_tsupronboardinstitution2emailupdatelog(" +
                   " institution2emailaddressupdatelog_gid, " +
                   " institution2email_gid, " +
                   " institution_gid, " +
                   " email_address," +
                   " primary_status," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + values.institution2email_gid + "'," +
                   "'" + values.institution_gid + "'," +
                   "'" + lsemail_address + "'," +
                   "'" + lsprimary_status + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Email Address Updated Successfully";
                }

                if (ConfigurationManager.AppSettings["sysSamagroHyperbrdigeAPIEnable"].ToString() == "Yes")
                {
                    HttpContext ctx = HttpContext.Current;

                    Thread t = new Thread(new ThreadStart(() =>
                    {
                        HttpContext.Current = ctx;

                        if (lsemail_address != values.email_address)
                        {
                            objFnSamAgroHBAPIConnEdit.UpdateSupplierInstitutionContactHBAPI(values.institution2email_gid, UpdateContactHBAPIFrom.Email);
                        }

                    }));

                    t.Start();
                }

                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
                return false;
            }
        }


        public void DaEditInstitutionAddressDetail(string institution2address_gid, MdlMstBuyerOnboardAddressDetails values)
        {
            try
            {
                msSQL = "select addresstype_gid, addresstype_name, addressline1, addressline2, landmark, taluka, primary_status, postal_code, city," +
                    " district, state, country, latitude, longitude, institution_gid, institution2address_gid " +
                    " from agr_mst_tsupronboardinstitution2address where institution2address_gid='" + institution2address_gid + "'";
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
                    values.latitude = objODBCDatareader["latitude"].ToString();
                    values.longitude = objODBCDatareader["longitude"].ToString();
                    values.institution_gid = objODBCDatareader["institution_gid"].ToString();
                    values.institution2address_gid = objODBCDatareader["institution2address_gid"].ToString();
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

        public bool DaUpdateInstitutionAddressDetail(string employee_gid, MdlMstBuyerOnboardAddressDetails values)
        {
            msSQL = "select addresstype_gid, addresstype_name, addressline1, addressline2, landmark, taluka, primary_status, postal_code, city," +
                    " district, state, country, latitude, longitude, institution_gid, institution2address_gid " +
                    " from agr_mst_tsupronboardinstitution2address where institution2address_gid='" + values.institution2address_gid + "'";
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
                lslatitude = objODBCDatareader["latitude"].ToString();
                lslongitude = objODBCDatareader["longitude"].ToString();
                lsinstitution_gid = objODBCDatareader["institution_gid"].ToString();
                lsinstitution2address_gid = objODBCDatareader["institution2address_gid"].ToString();
            }
            objODBCDatareader.Close();


            msSQL = "select institution2address_gid from agr_mst_tsupronboardinstitution2address where primary_status='Yes' and  (institution2address_gid='" + values.institution2address_gid + "')";
            string institution2address_gid = objdbconn.GetExecuteScalar(msSQL);
            if (institution2address_gid != (values.institution2address_gid))
            {

                msSQL = "select primary_status from agr_mst_tsupronboardinstitution2address where primary_status='Yes' and (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "')";
                string primary_status = objdbconn.GetExecuteScalar(msSQL);
                if (primary_status == (values.primary_status))
                {
                    values.status = false;
                    values.message = "Already Primary Address Added";
                    return false;
                }
            }

            try
            {
                msSQL = " update agr_mst_tsupronboardinstitution2address set " +
                         " addresstype_gid='" + values.address_typegid + "'," +
                         " addresstype_name='" + values.address_type.Replace("'", "") + "'," +
                         " addressline1='" + values.addressline1.Replace("'", "") + "'," +
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

                    msSQL = " insert into agr_mst_tsupronboardinstitution2addressupdatelog(" +
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
                  " created_by," +
                  " created_date)" +
                  " values(" +
                  "'" + msGetGid + "'," +
                  "'" + values.institution2address_gid + "'," +
                  "'" + lsinstitution_gid + "'," +
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
                  "'" + employee_gid + "'," +
                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Address Details Updated Successfully";
                    
                }

                if (ConfigurationManager.AppSettings["sysSamagroHyperbrdigeAPIEnable"].ToString() == "Yes")
                {
                    HttpContext ctx = HttpContext.Current;

                    Thread t = new Thread(new ThreadStart(() =>
                    {
                        HttpContext.Current = ctx;
                        List<string> existingStringList = new List<string> { lsaddressline1, lsaddressline2, lscity, lsstate, lspostal_code, lslatitude, lslongitude };
                        List<string> updatedStringList = new List<string> { values.addressline1, values.addressline2, values.city, values.state, values.postal_code, values.latitude, values.longitude };

                        if (!(existingStringList.SequenceEqual(updatedStringList)))
                        {
                            objFnSamAgroHBAPIConnEdit.UpdateSupplierInstitutionAddressHBAPI(values.institution2address_gid);
                        }


                    }));

                    t.Start();
                }

                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
                return false;
            }
        }

        public void DaEditIndividualMobileNo(string contact2mobileno_gid, MdlBuyerOnboardContactMobileNo values)
        {
            try
            {
                msSQL = " select mobile_no,contact2mobileno_gid,primary_status,whatsapp_no from agr_mst_tsupronboardcontact2mobileno where " +
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

        public bool DaUpdateIndividualMobileNo(string employee_gid, MdlBuyerOnboardContactMobileNo values)
        {
            msSQL = " select mobile_no,contact2mobileno_gid,primary_status,whatsapp_no, contact_gid from agr_mst_tsupronboardcontact2mobileno where " +
                    " contact2mobileno_gid='" + values.contact2mobileno_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsmobile_no = objODBCDatareader["mobile_no"].ToString();
                lsprimary_status = objODBCDatareader["primary_status"].ToString();
                lswhatsapp_no = objODBCDatareader["whatsapp_no"].ToString();
                lscontact2mobileno_gid = objODBCDatareader["contact2mobileno_gid"].ToString();
                lscontact_gid = objODBCDatareader["contact_gid"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = "select contact2mobileno_gid from agr_mst_tsupronboardcontact2mobileno where primary_status='Yes' and  (contact2mobileno_gid='" + values.contact2mobileno_gid + "')";
            string contact2mobileno_gid = objdbconn.GetExecuteScalar(msSQL);
            if (contact2mobileno_gid != (values.contact2mobileno_gid))
            {

                msSQL = "select primary_status from agr_mst_tsupronboardcontact2mobileno where primary_status='Yes' and (contact_gid='" + employee_gid + "' or contact_gid='" + values.contact_gid + "')";
                string primary_status = objdbconn.GetExecuteScalar(msSQL);
                if (primary_status == (values.primary_status))
                {
                    values.status = false;
                    values.message = "Already Primary Mobile Number Added";
                    return false;
                }
            }

            try
            {
                msSQL = " update agr_mst_tsupronboardcontact2mobileno set " +
                         " mobile_no='" + values.mobile_no.Replace("'", "") + "'," +
                         " primary_status='" + values.primary_status.Replace("'", "") + "'," +
                         " whatsapp_no='" + values.whatsapp_no + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where contact2mobileno_gid='" + values.contact2mobileno_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("CMUL");

                    msSQL = "Insert into agr_mst_tsupronboardcontact2mobilenoupdatelog(" +
                   " contact2mobilenoupdatelog_gid, " +
                   " contact2mobileno_gid, " +
                   " contact_gid, " +
                   " mobile_no," +
                   " primary_status," +
                   " whatsapp_no," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + values.contact2mobileno_gid + "'," +
                   "'" + values.contact_gid + "'," +
                   "'" + lsmobile_no + "'," +
                   "'" + lsprimary_status + "'," +
                   "'" + lswhatsapp_no + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Individual Mobile Number Updated Successfully";
                }

                if (ConfigurationManager.AppSettings["sysSamagroHyperbrdigeAPIEnable"].ToString() == "Yes")
                {
                    HttpContext ctx = HttpContext.Current;

                    Thread t = new Thread(new ThreadStart(() =>
                    {
                        HttpContext.Current = ctx;

                        if (lsmobile_no != values.mobile_no)
                        {
                            objFnSamAgroHBAPIConnEdit.UpdateSupplierIndividualContactHBAPI(values.contact2mobileno_gid, UpdateContactHBAPIFrom.MobileNo);
                        }

                    }));

                    t.Start();
                }

                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
                return false;
            }
        }

        public void DaEditIndividualEmailAddress(string contact2email_gid, MdlBuyerOnboardContactEmail values)
        {
            try
            {
                msSQL = " select email_address,contact2email_gid,primary_status from agr_mst_tsupronboardcontact2email where " +
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

        public bool DaUpdateIndividualEmailAddress(string employee_gid, MdlBuyerOnboardContactEmail values)
        {
            msSQL = " select email_address,contact2email_gid,primary_status, contact_gid from agr_mst_tsupronboardcontact2email where " +
                        " contact2email_gid='" + values.contact2email_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsemail_address = objODBCDatareader["email_address"].ToString();
                lsprimary_status = objODBCDatareader["primary_status"].ToString();
                lscontact2email_gid = objODBCDatareader["contact2email_gid"].ToString();
            }
            objODBCDatareader.Close();


            msSQL = "select contact2email_gid from agr_mst_tsupronboardcontact2email where primary_status='Yes' and  (contact2email_gid='" + values.contact2email_gid + "')";
            string contact2email_gid = objdbconn.GetExecuteScalar(msSQL);
            if (contact2email_gid != (values.contact2email_gid))
            {

                msSQL = "select primary_status from agr_mst_tsupronboardcontact2email where primary_status='Yes' and (contact_gid='" + employee_gid + "' or contact_gid='" + values.contact_gid + "')";
                string primary_status = objdbconn.GetExecuteScalar(msSQL);
                if (primary_status == (values.primary_status))
                {
                    values.status = false;
                    values.message = "Already Primary Email Address Added";
                    return false;
                }
            }


            try
            {
                msSQL = " update agr_mst_tsupronboardcontact2email set " +
                         " email_address='" + values.email_address.Replace("'", "") + "'," +
                         " primary_status='" + values.primary_status + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where contact2email_gid='" + values.contact2email_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("IEUL");

                    msSQL = "Insert into agr_mst_tsupronboardcontact2emailupdatelog(" +
                   " contact2emailaddressupdatelog_gid, " +
                   " contact2email_gid, " +
                   " contact_gid, " +
                   " email_address," +
                   " primary_status," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + values.contact2email_gid + "'," +
                   "'" + values.contact_gid + "'," +
                   "'" + lsemail_address + "'," +
                   "'" + lsprimary_status + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Individual Email Address Updated Successfully";
                }

                if (ConfigurationManager.AppSettings["sysSamagroHyperbrdigeAPIEnable"].ToString() == "Yes")
                {
                    HttpContext ctx = HttpContext.Current;

                    Thread t = new Thread(new ThreadStart(() =>
                    {
                        HttpContext.Current = ctx;

                        if (lsemail_address != values.email_address)
                        {
                            objFnSamAgroHBAPIConnEdit.UpdateSupplierIndividualContactHBAPI(values.contact2email_gid, UpdateContactHBAPIFrom.Email);
                        }

                    }));

                    t.Start();
                }

                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
                return false;
            }
        }

        public void DaEditIndividualAddress(string contact2address_gid, MdlBuyerOnboardContactAddress values)
        {
            try
            {
                msSQL = "select addresstype_gid, addresstype_name, addressline1, addressline2, landmark, taluka, primary_status, postal_code, city," +
                    " district, state, country, latitude, longitude, contact_gid, contact2address_gid " +
                    " from agr_mst_tsupronboardcontact2address where contact2address_gid='" + contact2address_gid + "'";
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
                    values.latitude = objODBCDatareader["latitude"].ToString();
                    values.longitude = objODBCDatareader["longitude"].ToString();
                    values.contact_gid = objODBCDatareader["contact_gid"].ToString();
                    values.contact2address_gid = objODBCDatareader["contact2address_gid"].ToString();
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

        public bool DaUpdateIndividualAddress(string employee_gid, MdlBuyerOnboardContactAddress values)
        {
            msSQL = "select addresstype_gid, addresstype_name, addressline1, addressline2, landmark, taluka, primary_status, postal_code, city," +
                    " district, state, country, latitude, longitude, contact_gid, contact2address_gid " +
                    " from agr_mst_tsupronboardcontact2address where contact2address_gid='" + values.contact2address_gid + "'";
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
                lslatitude = objODBCDatareader["latitude"].ToString();
                lslongitude = objODBCDatareader["longitude"].ToString();
                lscontact_gid = objODBCDatareader["contact_gid"].ToString();
                lscontact2address_gid = objODBCDatareader["contact2address_gid"].ToString();
            }
            objODBCDatareader.Close();


            //msSQL = "select contact2address_gid from agr_mst_tsupronboardcontact2address where primary_status='Yes' and  (contact2address_gid='" + values.contact2address_gid + "')";
            //string contact2address_gid = objdbconn.GetExecuteScalar(msSQL);
            //if (contact2address_gid != (values.contact2address_gid))
            //{

            //    msSQL = "select primary_status from agr_mst_tsupronboardcontact2address where primary_status='Yes' and (contact_gid='" + employee_gid + "' or contact_gid='" + values.contact_gid + "')";
            //    string primary_status = objdbconn.GetExecuteScalar(msSQL);
            //    if (primary_status == (values.primary_status))
            //    {
            //        values.status = false;
            //        values.message = "Already Primary Address Added";
            //        return false;
            //    }
            //}


            try
            {
                msSQL = " update agr_mst_tsupronboardcontact2address set " +
                         " addresstype_gid='" + values.addresstype_gid + "'," +
                         " addresstype_name='" + values.addresstype_name.Replace("'", "") + "'," +
                         " addressline1='" + values.addressline1.Replace("'", "") + "'," +
                         " addressline2='" + values.addressline2 + "'," +
                         " landmark='" + values.landmark + "'," +
                         " taluka='" + values.taluka + "'," +
                         " primary_status='" + values.primary_status + "'," +
                         " postal_code='" + values.postal_code.Replace("'", "") + "'," +
                         " city='" + values.city + "'," +
                         " district='" + values.district + "'," +
                         " state='" + values.state + "'," +
                         " country='" + values.country + "'," +
                         " latitude='" + values.latitude + "'," +
                         " longitude='" + values.longitude + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where contact2address_gid='" + values.contact2address_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("IAUL");

                    msSQL = " insert into agr_mst_tsupronboardcontact2addressupdatelog(" +
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
                  " created_by," +
                  " created_date)" +
                  " values(" +
                  "'" + msGetGid + "'," +
                  "'" + lscontact2address_gid + "'," +
                  "'" + lscontact_gid + "'," +
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
                  "'" + employee_gid + "'," +
                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Address Details Updated Successfully";

                    
                }

                if (ConfigurationManager.AppSettings["sysSamagroHyperbrdigeAPIEnable"].ToString() == "Yes")
                {
                    HttpContext ctx = HttpContext.Current;

                    Thread t = new Thread(new ThreadStart(() =>
                    {
                        HttpContext.Current = ctx;

                        List<string> existingStringList = new List<string> { lsaddressline1, lsaddressline2, lscity, lsstate, lspostal_code, lslatitude, lslongitude };
                        List<string> updatedStringList = new List<string> { values.addressline1, values.addressline2, values.city, values.state, values.postal_code, values.latitude, values.longitude };

                        if (!(existingStringList.SequenceEqual(updatedStringList)))
                        {
                            objFnSamAgroHBAPIConnEdit.UpdateSupplierIndividualAddressHBAPI(values.contact2address_gid);
                        }

                    }));

                    t.Start();
                }

                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
                return false;
            }
        }

        public void DaPostSuprRaiseQuery(mdlraisequery values, string user_gid, string employee_gid)
        {

            msGetGid = objcmnfunctions.GetMasterGID("BSQR");
            msSQL = "Insert into agr_mst_tonboardquery( " +
                   " onboardquery_gid, " +
                   " supronboard_gid," +
                   " query_title," +
                   " query_description," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.application_gid + "', " +
                   "'" + values.query_title.Replace("'", "") + "'," +
                   "'" + values.description.Replace("'", "") + "'," +
                   "'" + user_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {

                msSQL = "update agr_mst_tsupronboard set  query_status ='Query Raised' " +
                           " where application_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                values.status = true;
                values.message = "Query Raised Successfully";
            }
            else
            {
                values.message = "Error Occured While Raising Query";
                values.status = false;
            }
        }


        public void DaGetSuprQuerySummary(mdlraisequery values, string application_gid)
        {
            msSQL = " select onboardquery_gid,query_title,query_status,query_description,close_remarks, " +
                     " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as created_by," +
                     " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date from agr_mst_tonboardquery a" +
                     " left join adm_mst_tuser b on b.user_gid = a.created_by " +
                     " where a.supronboard_gid ='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbyrraisequerylist = new List<byrraisequerylist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getbyrraisequerylist.Add(new byrraisequerylist
                    {
                        onboardquery_gid = dt["onboardquery_gid"].ToString(),
                        query_title = dt["query_title"].ToString(),
                        query_status = dt["query_status"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        query_description = dt["query_description"].ToString(),
                        close_remarks = dt["close_remarks"].ToString()
                    });
                    values.byrraisequerylist = getbyrraisequerylist;
                }
            }
            dt_datatable.Dispose();


        }

        public void DaGetUpdateSuprQueryStatus(mdlraisequery values,  string user_gid)
        {
            msSQL = " update agr_mst_tonboardquery set  query_status='Closed', close_remarks='" + values.close_remarks.Replace("'", "") + "'," +
                    " closed_by='" + user_gid + "', closed_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where onboardquery_gid ='" + values.onboardquery_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = " select onboardquery_gid from agr_mst_tonboardquery where supronboard_gid ='" + values.application_gid + "'" +
                        " and query_status ='Open'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                }
                else
                {
                    msSQL = "update agr_mst_tsupronboard set  query_status ='Closed' " +
                           " where application_gid='" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                objODBCDatareader.Close();


                values.status = true;
                values.message = "Query Closed Successfully..!";

            }

            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }

        }

        public void DaGetSuprRaiseQuerydesc(mdlraisequery values, string onboardquery_gid)
        {
            msSQL = "select query_title, query_description, close_remarks from agr_mst_tonboardquery where onboardquery_gid='" + onboardquery_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.description = objODBCDatareader["query_description"].ToString();
                values.query_title = objODBCDatareader["query_title"].ToString();
                values.close_remarks = objODBCDatareader["close_remarks"].ToString();
            }
            objODBCDatareader.Close();
        }


        public void DaGetOpenSuprQueryStatus(mdlraisequery values, string application_gid)
        {

            msSQL = " select onboardquery_gid from agr_mst_tonboardquery where supronboard_gid ='" + application_gid + "'" +
                    " and query_status ='Open'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.openquery_flag = "Y";
            }
            else
            {
                values.openquery_flag = "N";
            }
            objODBCDatareader.Close();
        }


        public void DaUpdateGSTHeadOffice(string employee_gid, MdlGSTHeadOffice values)
        {
            msSQL = " update agr_mst_tsupronboardinstitution2branch set headoffice_status = 'Yes' " +
                    " where institution2branch_gid = '" + values.institution2branch_gid + "' " +
                    " and (institution_gid = '" + employee_gid + "' or institution_gid = '" + values.institution_gid + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = " update agr_mst_tsupronboardinstitution2branch set headoffice_status='No' " +
                        " where institution2branch_gid<>'" + values.institution2branch_gid + "' " +
                        " and (institution_gid = '" + employee_gid + "' or institution_gid = '" + values.institution_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Head Office Confirmed Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }



    }
}

