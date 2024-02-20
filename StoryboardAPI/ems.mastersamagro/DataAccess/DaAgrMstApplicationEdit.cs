using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using System.Net;
using System.IO;
using ems.utilities.Functions;
using ems.mastersamagro.Models;
using System.Configuration;
using System.Drawing;
using System.Net.Mail;
using ems.storage.Functions;
using System.Threading;
using ems.hbapiconn.Functions;

namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess provide access to edit datas from various stages in Application creation (General, Company, Individual, Overall limit, Product, charges, trade, Bureau & Done)
    /// </summary>
    /// <remarks>Written by Sherin Augusta, Logapriya, Abilash.A, Premchander.K </remarks>
    public class DaAgrMstApplicationEdit
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader, objODBCDatareader1, objODBCDatareader2;
        HttpPostedFile httpPostedFile;
        DataTable dt_datatable, dt_tloan, dt_tcontact, dt_tinstitution, dt_thypothecation;
        string msSQL, msGetGid, lspath, msGetGid1, lsdocument_name, lsdocument_path, msGetDocumentGid;
        int mnResult;
        string sToken = string.Empty;
        Random rand = new Random();
        string lsapplication_gid, lsapplication_no, lscompany_name, lsdate_incorporation, lscompanypan_no, lspayment_value, lsyear_business, lsmonth_business, lscin_no;
        string lsofficial_telephoneno, lsofficial_mailid, lscompanytype_gid, lscompanytype_name, lsstakeholder_type, lsstakeholdertype_gid, lsassessmentagency_gid;
        string lsassessmentagency_name, lsassessmentagencyrating_gid, lsassessmentagencyrating_name, lsratingas_on, lsamlcategory_gid, lsamlcategory_name;
        string lsbusinesscategory_gid, lsbusinesscategory_name, lscontactperson_firstname, lscontactperson_middlename, lscontactperson_lastname, lsdesignation_gid, lsdesignation_name;
        string lsdesignation, lslastyear_turnover, lsescrow, lsstart_date, lsend_date, lsbusinessstart_date;
        string lsapplication2loan_gid, lsfacilityrequested_date, lsproduct_type, lsproducttype_gid, lsproductsub_type, lsfacilityvalidity_month, lsfacilityvalidity_days;
        string lsproductsubtype_gid, lsloantype_gid, lsloan_type, lsfacilityloan_amount, lsrate_interest, lspenal_interest, lsfacilityvalidity_year;
        string lsfacilityoverall_limit, lstenureproduct_year, lstenureproduct_month, lstenureproduct_days, lstenureoverall_limit, lsfacility_type, lsfacility_mode;
        string lsscheme_type, lsprincipalfrequency_name, lsprincipalfrequency_gid, lsinterestfrequency_name, lsinterestfrequency_gid, lsinterest_status, lsmoratorium_status;
        string lsmoratorium_type, lsmoratorium_startdate, lsmoratorium_enddate, lsapplication2buyer_gid, lsbuyer_gid, lsbuyer_name, lsbuyer_limit, lsavailed_limit;
        string lsapplication2collateral_gid, lssource_type, lsguideline_value, lsguideline_date, lsmarketvalue_date, lsmarket_value, lsforcedsource_value;
        string lscollateralSSV_value, lsforcedvalueassessed_on, lscollateralobservation_summary, lsbalance_limit, lsbill_tenure, lsmargin;
        string lsapplication2hypothecation_gid, lssecuritytype_gid, lssecurity_type, lssecurity_description, lssecurity_value, lssecurityassessed_date;
        string lsasset_id, lsroc_fillingid, lsCERSAI_fillingid, lshypoobservation_summary, lsprimary_security;
        string lsoveralllimit_amount, lsvalidityoveralllimit_year, lsvalidityoveralllimit_month, lsvalidityoveralllimit_days, lscalculationoveralllimit_validity;
        string lsenduse_purpose, lsprocessing_fee, lsprocessing_collectiontype, lsdoc_charges, lsdoccharge_collectiontype, lsfieldvisit_charge, lsfieldvisit_collectiontype;
        string lsadhoc_fee, lsadhoc_collectiontype, lslife_insurance, lslifeinsurance_collectiontype, lsacct_insurance, lstotal_collect, lstotal_deduct;

        string lsmobile_no, lsprimary_status, lswhatsapp_no, lsprimary_mobileno, lswhatsapp_mobileno, lsapplication2contact_gid, lsinstitution2mobileno_gid, lsemail_address, lsapplication2email_gid, lsprimary_emailaddress, lsinstitution2email_gid;
        string lsaddress_typegid, lsaddress_type, lsaddressline1, lsaddressline2, lslandmark, lstaluka, lspostal_code, lscity, lsdistrict, lsinstitution2branch_gid;
        string lsstate_gid, lsstate, lscountry, lslatitude, lslongitude, lsinstitution2address_gid, lsinstitution_gid, lsgststate_gid, lsgst_state, lsgst_no, lsgst_registered;
        string lsinstitution2licensedtl_gid, lslicenseexpiry_date, lslicenseissue_date, lslicense_number, lslicensetype_name, lslicensetype_gid;
        string lscontact2mobileno_gid, lscontact2email_gid;
        string lscontact_gid, lscontact2address_gid, lsloan2supplierdtl_gid, lsuistakeholder_type;
        string lsgeneticcode_gid, lsgeneticcode_name, lsgenetic_status, lsgenetic_remarks;
        string lspan_no, lsaadhar_no, lsfirst_name, lsmiddle_name, lslast_name, lsindividual_dob, lsage, lsgender_gid, lsgender_name, lseducationalqualification_gid,
               lseducationalqualification_name, lsmain_occupation, lsannual_income, lsmonthly_income, lspep_status, lspepverified_date, lsmaritalstatus_gid,
               lsmaritalstatus_name, lsfather_firstname, lsfather_middlename, lsfather_lastname, lsfather_dob, lsfather_age,
               lsmother_firstname, lsmother_middlename, lsmother_lastname, lsmother_dob, lsmother_age, lsspouse_firstname, lsspouse_middlename, lsspouse_lastname,
               lsspouse_dob, lsspouse_age, lsownershiptype_gid, lsownershiptype_name, lsresidencetype_gid, lsresidencetype_name, lscurrentresidence_years, lsbranch_distance;

        string lscustomer_urn, lscustomer_name, lsvertical_gid, lsvertical_name, lsverticaltaggs_gid, lsverticaltaggs_name,
                     lstan_number, lsconstitution_gid, lsconstitution_name, lsbusinessunit_gid, lsbusinessunit_name, lssa_status, lssa_id, lssa_name, lsvernacularlanguage_gid,
                         lsvernacular_language, lscontactpersonfirst_name, lscontactpersonmiddle_name, lscontactpersonlast_name, lsdesignation_type, lslandline_no;
        string lspropertyholder_gid, lspropertyholder_name, lsincometype_gid, lsincometype_name, lspreviouscrop, lsprposedcrop, lsinstitution_name;
        string lsgroup_gid, lsgroup_name, lsprofile, lsurn_status, lsurn, lsfathernominee_status, lsmothernominee_status, lsspousenominee_status, lsothernominee_status,
        lsrelationshiptype, lsnomineefirst_name, lsnominee_middlename, lsnominee_lastname, lsnominee_dob, lsnominee_age, lstotallandinacres, lscultivatedland, lsregion;
        string lsprogram, lsprogram_gid, lsprimaryvaluechain_gid, lsprimaryvaluechain_name, lssecondaryvaluechain_gid, lssecondaryvaluechain_name, lscreditgroup_gid, lscreditgroup_name, lsprogram_name;
        string lsproduct_gid, lsproduct_name, lsvariety_gid, lsvariety_name, lssector_name, lscategory_name, lsbotanical_name, lsalternative_name, lscontractref_Name,lsloanproduct_name, lssupplier_refno;
        private string cc_mailid;
        private IEnumerable<string> lsCCReceipients, lsBCCReceipients;
        private string body;
        private string sub;
        private int ls_port;
        private string ls_server;
        private string ls_username;
        private string ls_password;
        private string tomail_id, lsBccmail_id;
        private string lssource;
        private string cluster_head_mailid;
        private string regional_head_mailid;
        private string business_head_mailid;
        private string zonalhead_mailid;
        private string reportingto_name;
        private string reportingto_mailid;
        private string creater_mailid;
        private string customer_name;
        private string application_no;
        private string reportingto_gid;
        private string business_head_gid;
        private string regional_head_gid;
        private string zonal_head_gid;
        private string cluster_head_gid;
        private string lsdrm_gid, lsdrm_name;
        int matchCount1, matchCount2, lspayment_count;
        string lspan_status, msGetGidpan;
        string ls_Product, ls_Program, ls_Margin, regionalhead_name;
        FnSamAgroHBAPIContract objFnSamAgroHBAPIContract = new FnSamAgroHBAPIContract();

        public void DaSocialAndTradeEdit(string application_gid, MdlMstApplicationEdit values)
        {
            try
            {
                msSQL = " select application_gid, social_capital, trade_capital from agr_mst_tapplication where application_gid='" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.social_capital = objODBCDatareader["social_capital"].ToString();
                    values.trade_capital = objODBCDatareader["trade_capital"].ToString();
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
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

        public void DaSocialAndTradeCapitalUpdate(string employee_gid, MdlMstApplicationEdit values)
        {
            msSQL = " update agr_mst_tapplication set ";
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
                       " where application_gid='" + values.application_gid + "' ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Social/Trade Capital Details Updated Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Updating Social/Trade Capital Details..!";
            }
        }

        public void DaCICIndividualEdit(string contact2bureau_gid, MdlCICIndividual values)
        {
            try
            {
                msSQL = " select contact2bureau_gid, bureauname_gid,bureauname_name, bureau_score, date_format(bureauscore_date,'%d-%m-%Y') as bureauscore_date," +
                    " observations, bureau_response" +
                    " from agr_mst_tcontact2bureau where contact2bureau_gid='" + contact2bureau_gid + "'";
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

        public void DaCICIndividualDocEdit(string contact_gid, MdlCICIndividual values, string employee_gid)
        {
            // Document Attachments
            msSQL = "select cicdocument_name from agr_mst_tcontact where contact_gid='" + contact_gid + "'";
            lsdocument_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select cicdocument_path from agr_mst_tcontact where contact_gid='" + contact_gid + "'";
            lsdocument_path = objdbconn.GetExecuteScalar(msSQL);
            if (lsdocument_name != null && lsdocument_name != "")
            {

                msSQL = "delete from agr_tmp_tcicdocument where created_by='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " insert into agr_tmp_tcicdocument( " +
                                    " document_name ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + lsdocument_name + "'," +
                                    "'" + lsdocument_path + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
        }

        public void DaCICInstitutionEdit(string institution2bureau_gid, MdlCICInstitution values)
        {
            try
            {
                msSQL = " select institution2bureau_gid, bureauname_gid,bureauname_name, bureau_score,date_format(bureauscore_date,'%d-%m-%Y') as bureauscore_date, " +
                    " observations, bureau_response" +
                        " from agr_mst_tinstitution2bureau where institution2bureau_gid='" + institution2bureau_gid + "'";
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

        public void DaCICInstitutionDocEdit(string institution_gid, MdlCICIndividual values, string employee_gid)
        {
            // Document Attachments
            msSQL = "select cicdocument_name from agr_mst_tinstitution where institution_gid='" + institution_gid + "'";
            lsdocument_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select cicdocument_path from agr_mst_tinstitution where institution_gid='" + institution_gid + "'";
            lsdocument_path = objdbconn.GetExecuteScalar(msSQL);
            if (lsdocument_path != null && lsdocument_path != "")
            {

                msSQL = "delete from agr_tmp_tcicdocument where created_by='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " insert into agr_tmp_tcicdocument( " +
                                    " document_name ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + lsdocument_name + "'," +
                                    "'" + lsdocument_path + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
        }

        public void DaCICIndividualUpdate(string employee_gid, MdlCICIndividual values)
        {
            // Document Attachments
            msSQL = "select document_name from agr_tmp_tcicdocument where created_by='" + employee_gid + "'";
            lsdocument_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select document_path from agr_tmp_tcicdocument where created_by='" + employee_gid + "'";
            lsdocument_path = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " update agr_mst_tcontact set " +
                      " bureauname_name='" + values.bureauname_name + "'," +
                      " bureauname_gid='" + values.bureauname_gid + "'," +
                      " bureau_score='" + values.bureau_score + "',";

            if ((values.bureauscore_date == null) || (values.bureauscore_date == ""))
            {
                msSQL += "bureauscore_date=null,";
            }
            else
            {
                msSQL += "bureauscore_date= '" + Convert.ToDateTime(values.bureauscore_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }

            msSQL += " observations='" + values.observations + "'," +
                      " bureau_response='" + values.bureau_response + "'," +
                      " cicdocument_name='" + lsdocument_name + "'," +
                      " cicdocument_path='" + lsdocument_path + "'," +
                      " updated_by='" + employee_gid + "'," +
                      " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                      " where contact_gid='" + values.contact_gid + "' ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "CIC Updated for Individual successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Updating for Individual CIC..!";
            }
        }

        public void DaCICInstitutionUpdate(string employee_gid, MdlCICInstitution values)
        {
            // Document Attachments
            msSQL = "select document_name from agr_tmp_tcicdocument where created_by='" + employee_gid + "'";
            lsdocument_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select document_path from agr_tmp_tcicdocument where created_by='" + employee_gid + "'";
            lsdocument_path = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " update agr_mst_tinstitution set " +
                      " bureauname_name='" + values.bureauname_name + "'," +
                      " bureauname_gid='" + values.bureauname_gid + "'," +
                      " bureau_score='" + values.bureau_score + "',";

            if ((values.bureauscore_date == null) || (values.bureauscore_date == ""))
            {
                msSQL += "bureauscore_date=null,";
            }
            else
            {
                msSQL += "bureauscore_date= '" + Convert.ToDateTime(values.bureauscore_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }

            msSQL += " observations='" + values.observations + "'," +
                      " bureau_response='" + values.bureau_response + "'," +
                      " cicdocument_name='" + lsdocument_name + "'," +
                      " cicdocument_path='" + lsdocument_path + "'," +
                      " updated_by='" + employee_gid + "'," +
                      " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                      " where institution_gid='" + values.institution_gid + "' ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "CIC Updated for Institution Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Updating for Institution CIC..!";
            }
        }

        public bool DaCICIndividualDocumentUploadEdit(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid)
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

            msSQL = " SELECT count(*) FROM agr_tmp_tcicdocument where created_by = '" + employee_gid + "' ";
            string count = objdbconn.GetExecuteScalar(msSQL);

            int counts = Convert.ToInt32(count);

            if (counts < 1)
            {

                msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/CICUploadIndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                            //lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SamAgro/CICUploadIndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                            //FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                            //ms.WriteTo(file);
                            //file.Close();
                            //ms.Close();
                            //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "SamAgro/CICUploadIndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                            bool status;
                            status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/CICUploadIndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                            ms.Close();
                            lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/CICUploadIndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                            msSQL = " insert into agr_tmp_tcicdocument( " +
                                        " document_name ," +
                                        " document_path," +
                                        " created_by," +
                                        " created_date" +
                                        " )values(" +
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

            }
            else
            {
                objfilename.message = "Only One file should Upload..!";
            }
            return true;
        }

        public void DaTempCICUploadDocDelete(string employee_gid, MdlCICIndividual values)
        {
            msSQL = " delete from agr_tmp_tcicdocument where created_by='" + employee_gid + "'";
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

        public void DaGetSocialTradeSummary(MdlMstApplicationAdd values, String application_gid)
        {
            try
            {
                msSQL = "SELECT application_gid,application_no," +
                         "date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, " +
                         " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                        " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as updated_by " +
                        " FROM agr_mst_tapplication a left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
                        " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                        " where a.application_gid = '" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplication_list = new List<applicationlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getapplication_list.Add(new applicationlist
                        {
                            application_gid = (dr_datarow["application_gid"].ToString()),
                            application_no = (dr_datarow["application_no"].ToString()),

                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),

                        });
                    }
                    values.applicationlist = getapplication_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetCICIndividualSummary(string application_gid, MdlCICIndividual values)
        {
            msSQL = " select contact_gid,first_name,middle_name,last_name," +
                    " case when bureauname_name is null then '-'" +
                    " else bureauname_name end as bureauname_name," +
                    " case when bureau_score is null then '-'" +
                    " else bureau_score end as bureau_score," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                    " from agr_mst_tcontact a " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " where a.application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcicindividualList = new List<cicindividual_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcicindividualList.Add(new cicindividual_list
                    {
                        contact_gid = dt["contact_gid"].ToString(),
                        first_name = dt["first_name"].ToString(),
                        middle_name = dt["middle_name"].ToString(),
                        last_name = dt["last_name"].ToString(),
                        bureauname_name = dt["bureauname_name"].ToString(),
                        bureau_score = dt["bureau_score"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                    });

                }
            }
            values.cicindividual_list = getcicindividualList;
            dt_datatable.Dispose();
        }

        public void DaGetCICInstitutionSummary(string application_gid, MdlCICInstitution values)
        {
            msSQL = " select institution_gid,company_name," +
                    " case when bureauname_name is null then '-'" +
                    " else bureauname_name end as bureauname_name," +
                    " case when bureau_score is null then '-'" +
                    " else bureau_score end as bureau_score," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                    " from agr_mst_tinstitution a " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " where a.application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcicinstitutionList = new List<cicinstitution_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcicinstitutionList.Add(new cicinstitution_list
                    {
                        institution_gid = dt["institution_gid"].ToString(),
                        company_name = dt["company_name"].ToString(),
                        bureauname_name = dt["bureauname_name"].ToString(),
                        bureau_score = dt["bureau_score"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                    });

                }
            }
            values.cicinstitution_list = getcicinstitutionList;
            dt_datatable.Dispose();
        }

        public bool DaInstitutionEditDocumentUpload(HttpRequest httpRequest, institutionuploaddocument objfilename, string employee_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;

            string lsdocument_title = httpRequest.Form["document_title"].ToString();
            string lsdocument_id = httpRequest.Form["document_id"].ToString();
            string lsinstitution_gid = httpRequest.Form["institution_gid"].ToString();
            string lscompanydocument_gid = httpRequest.Form["companydocument_gid"].ToString();
            string lsdocumenttype_gid = httpRequest.Form["documenttype_gid"].ToString();
            string lsdocumenttype_name = httpRequest.Form["documenttype_name"].ToString();
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
                        //lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SamAgro/InstitutionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        //FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                        //ms.WriteTo(file);
                        //file.Close();
                        //ms.Close();
                        //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "SamAgro/InstitutionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/InstitutionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/InstitutionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        msSQL = "select covenant_type from ocs_mst_tcompanydocument where companydocument_gid='" + lscompanydocument_gid + "'";
                        string lscovenant_type = objdbconn.GetExecuteScalar(msSQL);

                        msGetGid = objcmnfunctions.GetMasterGID("INDO");
                        msSQL = " insert into agr_mst_tinstitution2documentupload( " +
                                    " institution2documentupload_gid," +
                                    " institution_gid," +
                                    " document_title ," +
                                    " document_id," +
                                    " document_name," +
                                    " companydocument_gid, " +
                                    " document_path," +
                                    " covenant_type," +
                                    " documenttype_gid," +
                                    " documenttype_name," +
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
                                    "'" + lsdocumenttype_gid + "'," +
                                    "'" + lsdocumenttype_name + "'," +
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
            msSQL = " select institution2documentupload_gid,institution_gid,document_name,document_path,document_title,document_id, documenttype_name from agr_mst_tinstitution2documentupload " +
                             " where institution_gid='" + employee_gid + "' or institution_gid='" + institution_gid + "'";
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
                        documenttype_name = dt["documenttype_name"].ToString()
                    });
                    values.institutionupload_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaInstitutionEditDocumentDelete(string institution2documentupload_gid, institutionuploaddocument objfilename, string employee_gid)
        {
            msSQL = "delete from agr_mst_tinstitution2documentupload where institution2documentupload_gid='" + institution2documentupload_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                msSQL = " select groupdocumentchecklist_gid from agr_trn_tdocumentchecktls where documentuploaded_gid='" + institution2documentupload_gid + "'";
                string lsgroupdocumentchecklist_gid = objdbconn.GetExecuteScalar(msSQL);

                if (lsgroupdocumentchecklist_gid != "")
                {
                    msSQL = " select count(documentcheckdtl_gid) as documentcount from agr_trn_tdocumentchecktls " +
                            " where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                    string lsdocumentcount = objdbconn.GetExecuteScalar(msSQL);
                    if (lsdocumentcount == "1")
                    {
                        msSQL = "delete from agr_trn_tgroupdocumentchecklist where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                msSQL = " select groupcovdocumentchecklist_gid from agr_trn_tcovanantdocumentcheckdtls where documentuploaded_gid='" + institution2documentupload_gid + "'";
                string lschecklist_gid = objdbconn.GetExecuteScalar(msSQL);

                if (lschecklist_gid != "")
                {
                    msSQL = " select count(*) as documentcount from agr_trn_tcovanantdocumentcheckdtls " +
                      " where groupcovdocumentchecklist_gid='" + lschecklist_gid + "'";
                    string lsdocumentcount = objdbconn.GetExecuteScalar(msSQL);
                    if (lsdocumentcount == "1")
                    {
                        msSQL = "delete from agr_trn_tgroupcovenantdocumentchecklist where groupcovdocumentchecklist_gid='" + lschecklist_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                msSQL = "delete from agr_trn_tcovanantdocumentcheckdtls where documentuploaded_gid='" + institution2documentupload_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "delete from agr_trn_tdocumentchecktls where documentuploaded_gid='" + institution2documentupload_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
                objfilename.message = "Document Deleted Successfully";
                objfilename.status = true;
            }
            else
            {
                objfilename.message = "Error Occured";
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
                        //lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SamAgro/IntitutionForm_60Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        //FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                        //ms.WriteTo(file);
                        //file.Close();
                        //ms.Close();
                        //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "SamAgro/IntitutionForm_60Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/IntitutionForm_60Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/IntitutionForm_60Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        msGetGid = objcmnfunctions.GetMasterGID("IF6D");
                        msSQL = " insert into agr_mst_tinstitution2form60documentupload( " +
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
            msSQL = " select institution2form60documentupload_gid,form60document_name,form60document_path from agr_mst_tinstitution2form60documentupload " +
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
            msSQL = "delete from agr_mst_tinstitution2form60documentupload where institution2form60documentupload_gid='" + institution2form60documentupload_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                objfilename.message = "Document Deleted Successfully";
                objfilename.status = true;
            }
            else
            {
                objfilename.message = "Error Occured";
                objfilename.status = false;

            }
        }

        public void DaInstitutionGSTList(string institution_gid, MdlMstGST values)
        {
            msSQL = "select institution2branch_gid,gst_state,gst_no, gst_registered , headoffice_status  from agr_mst_tinstitution2branch where institution_gid='" + institution_gid + "'";
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
                        gst_registered = (dr_datarow["gst_registered"].ToString()),
                        headoffice_status = (dr_datarow["headoffice_status"].ToString())
                    });
                }
                values.mstgst_list = getmstgst_list;
            }
            dt_datatable.Dispose();
        }

        public void DaInstitutionMobileNoList(string institution_gid, MdlMstMobileNo values)
        {
            msSQL = "select mobile_no,institution2mobileno_gid,primary_status,whatsapp_no from agr_mst_tinstitution2mobileno where " +
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
            msSQL = "select email_address,institution2email_gid,primary_status from agr_mst_tinstitution2email where institution_gid='" + institution_gid + "'";
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
            msSQL = "  select institution2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, latitude, longitude, landmark," +
                    " postal_code from agr_mst_tinstitution2address where institution_gid='" + institution_gid + "'";
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
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString()),
                        landmark = (dr_datarow["landmark"].ToString())
                    });
                }
                values.mstaddress_list = getmstaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaInstitutionLicenseList(string institution_gid, MdlMstLicenseDetails values)
        {
            msSQL = " select institution2licensedtl_gid,licensetype_gid,licensetype_name,license_no,date_format(issue_date,'%d-%m-%Y') as issue_date," +
                    " date_format(expiry_date,'%d-%m-%Y') as expiry_date from agr_mst_tinstitution2licensedtl" +
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
            msSQL = " select institution2documentupload_gid,institution_gid,document_name,document_path,document_title,document_id, documenttype_name, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by, " +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as uploaded_date" +
                    " from agr_mst_tinstitution2documentupload a" +
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
                        documenttype_name = dt["documenttype_name"].ToString()
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
                    " from agr_mst_tinstitution2form60documentupload a" +
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
                   " businesscategory_name, contactperson_firstname, contactperson_middlename, contactperson_lastname, designation_gid, designation, msme_registration,lei_renewaldate,kin, lglentity_id, " +
                   " lastyear_turnover, escrow, start_date, end_date, institution_status, urn, urn_status, tan_number, incometax_returnsstatus, revenue, profit, fixed_assets, sundrydebt_adv " +
                   " from agr_mst_tinstitution where institution_gid='" + institution_gid + "'";
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

        public bool DaUpdateInstitutionDtl(MdlMstInstitutionAdd values, string employee_gid)
        {


            msSQL = " select a.companydocument_gid from ocs_mst_tcompanydocument a " +
                   " left join ocs_mst_tcompanydocumentprogram b on a.companydocument_gid = b.companydocument_gid " +
                   " where a.documenttypes_gid = 'DOCT2022010611' and " +
                   " status = 'Y' and b.program_gid ='" + values.program_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmasterdocument_list = new List<string>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmasterdocument_list.Add(dr_datarow["companydocument_gid"].ToString());
                }
            }
            dt_datatable.Dispose();

            msSQL = " select distinct(a.companydocument_gid) " +
                    " from agr_mst_tinstitution2documentupload a where a.documenttype_gid = 'DOCT2022010611' and " +
                    " (institution_gid='" + values.institution_gid + "' or institution_gid = '" + employee_gid + "')";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettransactiondocument_list = new List<string>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    gettransactiondocument_list.Add(dr_datarow["companydocument_gid"].ToString());
                }
            }
            dt_datatable.Dispose();

            var set1 = new HashSet<string>(getmasterdocument_list);
            var set2 = new HashSet<string>(gettransactiondocument_list);

            if (set1.SetEquals(set2) == false)
            {
                values.status = false;
                values.message = "Upload All KYC Documents";
                return false;
            }



            msSQL = "select institution_gid from agr_mst_tinstitution2mobileno where (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "') and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Primary Mobile Number";
                return false;
            }

            msSQL = "select institution_gid from agr_mst_tinstitution2mobileno where institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Atleast One Mobile Number";
                return false;
            }

            msSQL = "select institution_gid from agr_mst_tinstitution2email where institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Atleast One Email Address";
                return false;
            }

            msSQL = "select institution_gid from agr_mst_tinstitution2address where institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "'";
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

                msSQL = "select institution2branch_gid from agr_mst_tinstitution2branch where (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "') and headoffice_status ='Yes' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == false)
                {
                    values.status = false;
                    values.message = "Atleast Select One GST Number as Head Office";
                    return false;
                }
                objODBCDatareader.Close();
            }

            //if (values.stakeholdertype_name == "Borrower" || values.stakeholdertype_name == "Applicant")
            //{

            //    msSQL = "select institution_gid from agr_mst_tinstitution2bankdtl where institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "' and primary_status='Yes'";
            //    objODBCDatareader = objdbconn.GetDataReader(msSQL);

            //    if (objODBCDatareader.HasRows == false)
            //    {
            //        objODBCDatareader.Close();
            //        values.status = false;
            //        values.message = "Add Primary Bank Account Detail";
            //        return false;
            //    }

            //}

            //msSQL = "select institution_gid from agr_mst_tinstitution2bankdtl where institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "'";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);

            //if (objODBCDatareader.HasRows == false)
            //{
            //    objODBCDatareader.Close();
            //    values.status = false;
            //    values.message = "Add Atleast One Bank Account Detail";
            //    return false;
            //}

            msSQL = "select user_type from ocs_mst_tusertype where usertype_gid ='" + values.stakeholdertype_gid + "'";
             lsuistakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select application_gid from agr_mst_tinstitution where institution_gid='" + values.institution_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            if (lsuistakeholder_type == "Borrower" || lsuistakeholder_type == "Applicant")
            {
                msSQL = "select stakeholder_type from agr_mst_tcontact where application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Borrower','Applicant')";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Already Added Applicant/Borrower Information";
                    return false;
                }
                msSQL = "select stakeholder_type from agr_mst_tinstitution where application_gid='" + lsapplication_gid + "' and" +
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
                     " lastyear_turnover, escrow, start_date, end_date, urn_status, urn, tan_number " +
                     " from agr_mst_tinstitution where institution_gid='" + values.institution_gid + "'";
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
                lstan_number = objODBCDatareader["tan_number"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tinstitution set " +
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
                         " stakeholder_type='" + lsuistakeholder_type + "'," +
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
                    msSQL += "fixed_assets='" + values.sundrydebt_adv.Replace(",", "") + "',";
                }
                if (values.sundrydebt_adv == null || values.sundrydebt_adv == "")
                {
                    msSQL += "sundrydebt_adv='0.00',";
                }
                else
                {
                    msSQL += "sundrydebt_adv='" + values.sundrydebt_adv.Replace(",", "") + "',";
                }

                msSQL +=
                             " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where institution_gid='" + values.institution_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("IULG");

                    msSQL = " insert into agr_mst_tinstitutionupdateLOG(" +
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
                    " tan_number," +
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
                               "'" + lstan_number + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    // Updates for Multiple Add
                    msSQL = "update agr_mst_tinstitution2branch set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tinstitution2mobileno set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tinstitution2email set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tinstitution2address set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tinstitution2licensedtl set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tinstitution2bankdtl set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tinstitution2ratingdetail set institution_gid='" + values.institution_gid + "', application_gid ='" + lsapplication_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_trn_ttandtl set function_gid='" + values.institution_gid + "' where function_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tkycpanauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tkycpanaadhaarlink set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tkycdlauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tkycepicauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tkycpassportauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tkycifscauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tkycbankaccverification set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tkycgstsbpan set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "select companydocument_gid, institution2documentupload_gid from agr_mst_tinstitution2documentupload where institution_gid='" + employee_gid + "'";
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

                    DaAgrMstScannedDocument objvalues = new DaAgrMstScannedDocument();
                    objvalues.DaGroupDocChecklistinfo(lsapplication_gid, values.institution_gid, employee_gid);

                    msSQL = "update agr_mst_tinstitution2documentupload set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tinstitution2form60documentupload set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    msSQL = "select mobile_no from agr_mst_tinstitution2mobileno where institution_gid='" + values.institution_gid + "' and primary_status='yes'";
                    string lsmobileno = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select email_address from agr_mst_tinstitution2email where institution_gid='" + values.institution_gid + "' and primary_status='yes'";
                    lsemail_address = objdbconn.GetExecuteScalar(msSQL);
                    if (values.stakeholder_type == "Borrower" || values.stakeholder_type == "Applicant")
                    {
                        msSQL = "update agr_mst_tapplication set applicant_type ='Institution' where application_gid='" + lsapplication_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_mst_tinstitution set mobile_no='" + lsmobileno + "'," +
                         " email_address='" + lsemail_address + "' where institution_gid='" + values.institution_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
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

        public void DaInstitutionGSTTmpList(string institution_gid, string employee_gid, MdlMstGST values)
        {
            msSQL = "select institution2branch_gid,gst_state,gst_no, gst_registered, headoffice_status from agr_mst_tinstitution2branch " +
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
                        gst_registered = (dr_datarow["gst_registered"].ToString()),
                        headoffice_status = (dr_datarow["headoffice_status"].ToString())

                    });
                }
                values.mstgst_list = getmstgst_list;
            }
            dt_datatable.Dispose();
        }

        public void DaInstitutionMobileNoTmpList(string institution_gid, string employee_gid, MdlMstMobileNo values)
        {
            msSQL = "select mobile_no,institution2mobileno_gid,primary_status,whatsapp_no from agr_mst_tinstitution2mobileno where " +
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

        public void DaInstitutionEmailAddressTmpList(string institution_gid, string employee_gid, MdlMstEmailAddress values)
        {
            msSQL = "select email_address,institution2email_gid,primary_status from agr_mst_tinstitution2email " +
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

        public void DaInstitutionAddressTmpList(string institution_gid, string employee_gid, MdlMstAddressDetails values)
        {
            msSQL = "  select institution2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, latitude, longitude, landmark," +
                    " postal_code from agr_mst_tinstitution2address where institution_gid='" + institution_gid + "' or institution_gid='" + employee_gid + "'";
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
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString()),
                        landmark = (dr_datarow["landmark"].ToString())
                    });
                }
                values.mstaddress_list = getmstaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaInstitutionLicenseTmpList(string institution_gid, string employee_gid, MdlMstLicenseDetails values)
        {
            msSQL = " select institution2licensedtl_gid,licensetype_gid,licensetype_name,license_no,date_format(issue_date,'%d-%m-%Y') as issue_date," +
                    " date_format(expiry_date,'%d-%m-%Y') as expiry_date from agr_mst_tinstitution2licensedtl" +
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

        public void DaGetProductChargesEdit(string application_gid, MdlProductCharges values)
        {
            try
            {
                msSQL = " select application_gid, overalllimit_amount, validityoveralllimit_year, date_format(validityto_date, '%d-%m-%Y') as validityto_date, date_format(validityfrom_date, '%d-%m-%Y') as validityfrom_date,  validityoveralllimit_month, validityoveralllimit_days, calculationoveralllimit_validity," +
                   " enduse_purpose, processing_fee, processing_collectiontype, doc_charges, doccharge_collectiontype, fieldvisit_charge, fieldvisit_collectiontype, onboarding_status, " +
                  " adhoc_fee, adhoc_collectiontype, life_insurance, lifeinsurance_collectiontype, acct_insurance, total_collect, total_deduct, productcharges_status,sa_status " +
                   " from agr_mst_tapplication where application_gid='" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.overalllimit_amount = objODBCDatareader["overalllimit_amount"].ToString();
                    //values.validityoveralllimit_year = objODBCDatareader["validityoveralllimit_year"].ToString();
                    //values.validityoveralllimit_month = objODBCDatareader["validityoveralllimit_month"].ToString();
                    //values.validityoveralllimit_days = objODBCDatareader["validityoveralllimit_days"].ToString();
                    values.validityfrom_date = objODBCDatareader["validityfrom_date"].ToString();
                    values.validityto_date = objODBCDatareader["validityto_date"].ToString();
                    values.calculationoveralllimit_validity = objODBCDatareader["calculationoveralllimit_validity"].ToString();
                    values.enduse_purpose = objODBCDatareader["enduse_purpose"].ToString();
                    values.processing_fee = objODBCDatareader["processing_fee"].ToString();
                    values.processing_collectiontype = objODBCDatareader["processing_collectiontype"].ToString();
                    values.doc_charges = objODBCDatareader["doc_charges"].ToString();
                    values.doccharge_collectiontype = objODBCDatareader["doccharge_collectiontype"].ToString();
                    values.fieldvisit_charge = objODBCDatareader["fieldvisit_charge"].ToString();
                    values.fieldvisit_collectiontype = objODBCDatareader["fieldvisit_collectiontype"].ToString();
                    values.adhoc_fee = objODBCDatareader["adhoc_fee"].ToString();
                    values.adhoc_collectiontype = objODBCDatareader["adhoc_collectiontype"].ToString();
                    values.life_insurance = objODBCDatareader["life_insurance"].ToString();
                    values.lifeinsurance_collectiontype = objODBCDatareader["lifeinsurance_collectiontype"].ToString();
                    values.acct_insurance = objODBCDatareader["acct_insurance"].ToString();
                    values.total_collect = objODBCDatareader["total_collect"].ToString();
                    values.total_deduct = objODBCDatareader["total_deduct"].ToString();
                    values.productcharges_status = objODBCDatareader["productcharges_status"].ToString();
                    values.sa_status = objODBCDatareader["sa_status"].ToString();
                    values.validityfrom_date = objODBCDatareader["validityfrom_date"].ToString();
                    values.validityto_date = objODBCDatareader["validityto_date"].ToString();
                    values.onboarding_status = objODBCDatareader["onboarding_status"].ToString();
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

        public void DaLoanDetailList(string application_gid, MdlMstLoanDtl values, string employee_gid)
        {
            msSQL = "select date_format(facilityrequested_date, '%d-%m-%Y') as facilityrequested_date, product_type,facilityrequested_date,scheme_type, " +
                               " productsub_type, loanfacility_amount, loan_type, rate_interest, penal_interest, programoverall_limit, " +
                               " tenureoverall_limit, facility_type, facility_mode, principalfrequency_name, interestfrequency_name, " +
                               " interest_status, moratorium_type, moratorium_status, date_format(moratorium_startdate, '%d-%m-%Y') as moratorium_startdate,application2loan_gid, " +
                               " date_format(moratorium_enddate, '%d-%m-%Y') as moratorium_enddate from agr_mst_tapplication2loan " +
                               " where (application_gid='" + application_gid + "' or application_gid='" + employee_gid + "')";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstloan_list = new List<mstloan_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstloan_list.Add(new mstloan_list
                    {
                        facilityrequested_date = (dr_datarow["facilityrequested_date"].ToString()),
                        product_type = (dr_datarow["product_type"].ToString()),
                        productsub_type = (dr_datarow["productsub_type"].ToString()),
                        loanfacility_amount = (dr_datarow["loanfacility_amount"].ToString()),
                        loan_type = (dr_datarow["loan_type"].ToString()),
                        rate_interest = (dr_datarow["rate_interest"].ToString()),
                        penal_interest = (dr_datarow["penal_interest"].ToString()),
                        facilityoverall_limit = (dr_datarow["programoverall_limit"].ToString()),
                        tenureoverall_limit = (dr_datarow["tenureoverall_limit"].ToString()),
                        facility_type = (dr_datarow["facility_type"].ToString()),
                        facility_mode = (dr_datarow["facility_mode"].ToString()),
                        principalfrequency_name = (dr_datarow["principalfrequency_name"].ToString()),
                        interestfrequency_name = (dr_datarow["interestfrequency_name"].ToString()),
                        interest_status = (dr_datarow["interest_status"].ToString()),
                        moratorium_status = (dr_datarow["moratorium_status"].ToString()),
                        moratorium_type = (dr_datarow["moratorium_type"].ToString()),
                        moratorium_startdate = (dr_datarow["moratorium_startdate"].ToString()),
                        moratorium_enddate = (dr_datarow["moratorium_enddate"].ToString()),
                        scheme_type = (dr_datarow["scheme_type"].ToString()),
                        application2loan_gid = (dr_datarow["application2loan_gid"].ToString()),

                    });
                }
                values.mstloan_list = getmstloan_list;
            }
            dt_datatable.Dispose();
            msSQL = "select application_gid from agr_mst_tapplication2loan where product_type='Agri Receivable Finance (ARF)' and " +
            " (application_gid='" + application_gid + "' or application_gid='" + employee_gid + "')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.buyer_status = "Y";
            }
            objODBCDatareader.Close();

            msSQL = "select application_gid from agr_mst_tapplication2loan where loan_type='Secured' and" +
            " (application_gid='" + application_gid + "' or application_gid='" + employee_gid + "')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.collateral_status = "Y";
            }
            objODBCDatareader.Close();
            values.status = true;
        }

        public void DaLoanTempDetailList(string employee_gid, string application_gid, MdlMstLoanDtl values)
        {
            msSQL = "select date_format(facilityrequested_date, '%d-%m-%Y') as facilityrequested_date, product_type,facilityrequested_date,scheme_type, " +
                               " productsub_type, loanfacility_amount, loan_type, rate_interest, penal_interest, facilityoverall_limit, " +
                               " tenureoverall_limit, facility_type, facility_mode, principalfrequency_name, interestfrequency_name, " +
                               " interest_status, moratorium_type, moratorium_status, date_format(moratorium_startdate, '%d-%m-%Y') as moratorium_startdate,application2loan_gid, " +
                               " date_format(moratorium_enddate, '%d-%m-%Y') as moratorium_enddate" +
                               " from agr_mst_tapplication2loan where application_gid='" + application_gid + "' or application_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstloan_list = new List<mstloan_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstloan_list.Add(new mstloan_list
                    {
                        facilityrequested_date = (dr_datarow["facilityrequested_date"].ToString()),
                        product_type = (dr_datarow["product_type"].ToString()),
                        productsub_type = (dr_datarow["productsub_type"].ToString()),
                        loanfacility_amount = (dr_datarow["loanfacility_amount"].ToString()),
                        loan_type = (dr_datarow["loan_type"].ToString()),
                        rate_interest = (dr_datarow["rate_interest"].ToString()),
                        penal_interest = (dr_datarow["penal_interest"].ToString()),
                        facilityoverall_limit = (dr_datarow["facilityoverall_limit"].ToString()),
                        tenureoverall_limit = (dr_datarow["tenureoverall_limit"].ToString()),
                        facility_type = (dr_datarow["facility_type"].ToString()),
                        facility_mode = (dr_datarow["facility_mode"].ToString()),
                        principalfrequency_name = (dr_datarow["principalfrequency_name"].ToString()),
                        interestfrequency_name = (dr_datarow["interestfrequency_name"].ToString()),
                        interest_status = (dr_datarow["interest_status"].ToString()),
                        moratorium_status = (dr_datarow["moratorium_status"].ToString()),
                        moratorium_type = (dr_datarow["moratorium_type"].ToString()),
                        moratorium_startdate = (dr_datarow["moratorium_startdate"].ToString()),
                        moratorium_enddate = (dr_datarow["moratorium_enddate"].ToString()),
                        scheme_type = (dr_datarow["scheme_type"].ToString()),
                        application2loan_gid = (dr_datarow["application2loan_gid"].ToString()),
                    });
                }
                values.mstloan_list = getmstloan_list;
            }
            dt_datatable.Dispose();
        }

        public void DaLoanDetailsEdit(string application2loan_gid, MdlMstLoanDtl values)
        {
            try
            {
                msSQL = " select application2loan_gid, application_gid, date_format(facilityrequested_date,'%d-%m-%Y') as facilityrequested_dateedit, product_type, producttype_gid, productsub_type, productsubtype_gid," +
              " loantype_gid, loan_type, loanfacility_amount, rate_interest, penal_interest," +
              " tenureproduct_year, tenureproduct_month, tenureproduct_days, tenureoverall_limit, facility_type,facility_mode, " +
              " scheme_type, principalfrequency_name, principalfrequency_gid, interestfrequency_name, interestfrequency_gid, program, program_gid, primaryvaluechain_gid, primaryvaluechain_name, secondaryvaluechain_gid, secondaryvaluechain_name, interest_status, moratorium_status, enduse_purpose, " +
              " moratorium_type, date_format(moratorium_startdate,'%d-%m-%Y') as moratorium_startdateedit, date_format(moratorium_enddate,'%d-%m-%Y') as moratorium_enddateedit, " +
              " source_type, guideline_value, date_format(guideline_date,'%d-%m-%Y') as guideline_dateedit, date_format(marketvalue_date,'%d-%m-%Y') as marketvalue_dateedit," +
              " market_value, forcedsource_value, collateralSSV_value, date_format(forcedvalueassessed_on,'%d-%m-%Y') as forcedvalueassessed_onedit, collateralobservation_summary," +
              " product_gid,product_name,variety_gid,variety_name,sector_name,category_name,botanical_name,alternative_name,  " +
              " trade_orginatedby,date_format(programlimit_validdfrom,'%d-%m-%Y') as programlimit_validdfrom,date_format(programlimit_validdto,'%d-%m-%Y') as programlimit_validdto, " +
              " programoverall_limit,SA_Brokerage,holding_periods,holdingmonthly_procurement, " +
              " extendedholding_periods,extendedmonthly_procurement, charges_extendedperiod,customer_advance,reimburesementof_expenses,reimburesementof_expensespenalty, " +
              " bankfundingdata_filename,bankfundingdata_filepath, needfor_stocking, product_portfolio,production_capacity, natureof_operations,averagemonthly_inventoryholding, " +
              " financialinstitutions_relationship " +
              " from agr_mst_tapplication2loan where application2loan_gid='" + application2loan_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application2loan_gid = objODBCDatareader["application2loan_gid"].ToString();
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.facilityrequested_date = objODBCDatareader["facilityrequested_dateedit"].ToString();
                    values.product_type = objODBCDatareader["product_type"].ToString();
                    values.producttype_gid = objODBCDatareader["producttype_gid"].ToString();
                    values.productsub_type = objODBCDatareader["productsub_type"].ToString();
                    values.productsubtype_gid = objODBCDatareader["productsubtype_gid"].ToString();
                    values.loantype_gid = objODBCDatareader["loantype_gid"].ToString();
                    values.loan_type = objODBCDatareader["loan_type"].ToString();
                    values.facilityloan_amount = objODBCDatareader["loanfacility_amount"].ToString();
                    values.rate_interest = objODBCDatareader["rate_interest"].ToString();
                    values.penal_interest = objODBCDatareader["penal_interest"].ToString();
                    values.tenureproduct_year = objODBCDatareader["tenureproduct_year"].ToString();
                    values.tenureproduct_month = objODBCDatareader["tenureproduct_month"].ToString();
                    values.tenureproduct_days = objODBCDatareader["tenureproduct_days"].ToString();
                    values.tenureoverall_limit = objODBCDatareader["tenureoverall_limit"].ToString();
                    values.facility_type = objODBCDatareader["facility_type"].ToString();
                    values.facility_mode = objODBCDatareader["facility_mode"].ToString();
                    values.scheme_type = objODBCDatareader["scheme_type"].ToString();
                    values.principalfrequency_name = objODBCDatareader["principalfrequency_name"].ToString();
                    values.principalfrequency_gid = objODBCDatareader["principalfrequency_gid"].ToString();
                    values.interestfrequency_name = objODBCDatareader["interestfrequency_name"].ToString();
                    values.interestfrequency_gid = objODBCDatareader["interestfrequency_gid"].ToString();
                    values.program = objODBCDatareader["program"].ToString();
                    values.program_gid = objODBCDatareader["program_gid"].ToString();
                    values.interest_status = objODBCDatareader["interest_status"].ToString();
                    values.moratorium_status = objODBCDatareader["moratorium_status"].ToString();
                    values.moratorium_type = objODBCDatareader["moratorium_type"].ToString();
                    values.moratorium_startdate = objODBCDatareader["moratorium_startdateedit"].ToString();
                    values.moratorium_enddate = objODBCDatareader["moratorium_enddateedit"].ToString();
                    values.enduse_purpose = objODBCDatareader["enduse_purpose"].ToString();
                    values.product_gid = objODBCDatareader["product_gid"].ToString();
                    values.product_name = objODBCDatareader["product_name"].ToString();
                    values.variety_gid = objODBCDatareader["variety_gid"].ToString();
                    values.variety_name = objODBCDatareader["variety_name"].ToString();
                    values.sector_name = objODBCDatareader["sector_name"].ToString();
                    values.category_name = objODBCDatareader["category_name"].ToString();
                    values.botanical_name = objODBCDatareader["botanical_name"].ToString();
                    values.alternative_name = objODBCDatareader["alternative_name"].ToString();
                    values.trade_orginatedby = objODBCDatareader["trade_orginatedby"].ToString();
                    values.programlimit_validdfrom = objODBCDatareader["programlimit_validdfrom"].ToString();
                    values.programlimit_validdto = objODBCDatareader["programlimit_validdto"].ToString();
                    values.programoverall_limit = objODBCDatareader["programoverall_limit"].ToString();
                    values.SA_Brokerage = objODBCDatareader["SA_Brokerage"].ToString();
                    values.holding_periods = objODBCDatareader["holding_periods"].ToString();
                    values.holdingmonthly_procurement = objODBCDatareader["holdingmonthly_procurement"].ToString();
                    values.extendedholding_periods = objODBCDatareader["extendedholding_periods"].ToString();
                    values.extendedmonthly_procurement = objODBCDatareader["extendedmonthly_procurement"].ToString();
                    values.charges_extendedperiod = objODBCDatareader["charges_extendedperiod"].ToString();
                    values.customer_advance = objODBCDatareader["customer_advance"].ToString();
                    values.reimburesementof_expenses = objODBCDatareader["reimburesementof_expenses"].ToString();
                    values.reimburesementof_expensespenalty = objODBCDatareader["reimburesementof_expensespenalty"].ToString();
                    values.bankfundingdata_filename = objODBCDatareader["bankfundingdata_filename"].ToString();
                    values.bankfundingdata_filepath = objcmnstorage.EncryptData(objODBCDatareader["bankfundingdata_filepath"].ToString());
                    values.needfor_stocking = objODBCDatareader["needfor_stocking"].ToString();
                    values.product_portfolio = objODBCDatareader["product_portfolio"].ToString();
                    values.production_capacity = objODBCDatareader["production_capacity"].ToString();
                    values.natureof_operations = objODBCDatareader["natureof_operations"].ToString();
                    values.averagemonthly_inventoryholding = objODBCDatareader["averagemonthly_inventoryholding"].ToString();
                    values.financialinstitutions_relationship = objODBCDatareader["financialinstitutions_relationship"].ToString();

                    if (values.loan_type == "Secured")
                    {
                        values.source_type = objODBCDatareader["source_type"].ToString();
                        values.guideline_value = objODBCDatareader["guideline_value"].ToString();
                        values.guideline_date = objODBCDatareader["guideline_dateedit"].ToString();
                        values.marketvalue_date = objODBCDatareader["marketvalue_dateedit"].ToString();
                        values.market_value = objODBCDatareader["market_value"].ToString();
                        values.forcedsource_value = objODBCDatareader["forcedsource_value"].ToString();
                        values.collateralSSV_value = objODBCDatareader["collateralSSV_value"].ToString();
                        values.forcedvalueassessed_on = objODBCDatareader["forcedvalueassessed_onedit"].ToString();
                        values.collateralobservation_summary = objODBCDatareader["collateralobservation_summary"].ToString();
                    }

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

                msSQL = "select sa_status from agr_mst_tapplication where application_gid='" + values.application_gid + "'";
                values.sa_status = objdbconn.GetExecuteScalar(msSQL);

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

        public void DaLoanDetailsUpdate(string employee_gid, MdlMstLoanDtl values)
        {
            //string fsprimaryvaluechain_gid = string.Empty;
            //string fsprimaryvaluechain_name = string.Empty;
            //if (values.primaryvaluechain_list != null)
            //{
            //    for (var i = 0; i < values.primaryvaluechain_list.Count; i++)
            //    {
            //        fsprimaryvaluechain_gid += values.primaryvaluechain_list[i].valuechain_gid + ",";
            //        fsprimaryvaluechain_name += values.primaryvaluechain_list[i].valuechain_name + ",";

            //    }
            //    fsprimaryvaluechain_gid = fsprimaryvaluechain_gid.TrimEnd(',');
            //    fsprimaryvaluechain_name = fsprimaryvaluechain_name.TrimEnd(',');
            //}

            //string fssecondaryvaluechain_gid = string.Empty;
            //string fssecondaryvaluechain_name = string.Empty;
            //if (values.secondaryvaluechain_list != null)
            //{
            //    for (var i = 0; i < values.secondaryvaluechain_list.Count; i++)
            //    {
            //        fssecondaryvaluechain_gid += values.secondaryvaluechain_list[i].valuechain_gid + ",";
            //        fssecondaryvaluechain_name += values.secondaryvaluechain_list[i].valuechain_name + ",";

            //    }
            //    fssecondaryvaluechain_gid = fssecondaryvaluechain_gid.TrimEnd(',');
            //    fssecondaryvaluechain_name = fssecondaryvaluechain_name.TrimEnd(',');
            //} 

            msSQL = " select application2loan_gid, application_gid, facilityrequested_date, product_type, producttype_gid, productsub_type, productsubtype_gid," +
               " loantype_gid, loan_type, loanfacility_amount, rate_interest, penal_interest," +
               " tenureproduct_year, tenureproduct_month, tenureproduct_days, tenureoverall_limit, facility_type,facility_mode, " +
               " scheme_type, principalfrequency_name, principalfrequency_gid, interestfrequency_name, interestfrequency_gid, program, program_gid, primaryvaluechain_gid, primaryvaluechain_name, secondaryvaluechain_gid, secondaryvaluechain_name, interest_status, moratorium_status, " +
               " moratorium_type, moratorium_startdate, moratorium_enddate, enduse_purpose,product_gid,product_name," +
               " variety_gid,variety_name,sector_name,category_name,botanical_name,alternative_name " +
               " from agr_mst_tapplication2loan where application2loan_gid='" + values.application2loan_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsapplication2loan_gid = objODBCDatareader["application2loan_gid"].ToString();
                lsapplication_gid = objODBCDatareader["application_gid"].ToString();
                if (objODBCDatareader["facilityrequested_date"].ToString() == "")
                {
                }
                else
                {
                    lsfacilityrequested_date = Convert.ToDateTime(objODBCDatareader["facilityrequested_date"]).ToString("yyyy-MM-dd HH:mm:ss");
                }
                lsproduct_type = objODBCDatareader["product_type"].ToString();
                lsproducttype_gid = objODBCDatareader["producttype_gid"].ToString();
                lsproductsub_type = objODBCDatareader["productsub_type"].ToString();
                lsproductsubtype_gid = objODBCDatareader["productsubtype_gid"].ToString();
                lsloantype_gid = objODBCDatareader["loantype_gid"].ToString();
                lsloan_type = objODBCDatareader["loan_type"].ToString();
                lsfacilityloan_amount = objODBCDatareader["loanfacility_amount"].ToString();
                lsrate_interest = objODBCDatareader["rate_interest"].ToString();
                lspenal_interest = objODBCDatareader["penal_interest"].ToString();
                //lsfacilityvalidity_year = objODBCDatareader["facilityvalidity_year"].ToString();
                //lsfacilityvalidity_month = objODBCDatareader["facilityvalidity_month"].ToString();
                //lsfacilityvalidity_days = objODBCDatareader["facilityvalidity_days"].ToString();
                //lsfacilityoverall_limit = objODBCDatareader["facilityoverall_limit"].ToString();
                lstenureproduct_year = objODBCDatareader["tenureproduct_year"].ToString();
                lstenureproduct_month = objODBCDatareader["tenureproduct_month"].ToString();
                lstenureproduct_days = objODBCDatareader["tenureproduct_days"].ToString();
                lstenureoverall_limit = objODBCDatareader["tenureoverall_limit"].ToString();
                lsfacility_type = objODBCDatareader["facility_type"].ToString();
                lsfacility_mode = objODBCDatareader["facility_mode"].ToString();
                lsscheme_type = objODBCDatareader["scheme_type"].ToString();
                lsprincipalfrequency_name = objODBCDatareader["principalfrequency_name"].ToString();
                lsprincipalfrequency_gid = objODBCDatareader["principalfrequency_gid"].ToString();
                lsinterestfrequency_name = objODBCDatareader["interestfrequency_name"].ToString();
                lsinterestfrequency_gid = objODBCDatareader["interestfrequency_gid"].ToString();
                lsprogram = objODBCDatareader["interestfrequency_name"].ToString();
                lsprogram_gid = objODBCDatareader["interestfrequency_gid"].ToString();
                lsproduct_gid = objODBCDatareader["product_gid"].ToString();
                lsproduct_name = objODBCDatareader["product_name"].ToString();
                lsvariety_gid = objODBCDatareader["variety_gid"].ToString();
                lsvariety_name = objODBCDatareader["variety_name"].ToString();
                lssector_name = objODBCDatareader["sector_name"].ToString();
                lscategory_name = objODBCDatareader["category_name"].ToString();
                lsbotanical_name = objODBCDatareader["botanical_name"].ToString();
                lsalternative_name = objODBCDatareader["alternative_name"].ToString();
                //lsprimaryvaluechain_gid = objODBCDatareader["primaryvaluechain_gid"].ToString();
                //lsprimaryvaluechain_name = objODBCDatareader["primaryvaluechain_name"].ToString();
                //lssecondaryvaluechain_gid = objODBCDatareader["secondaryvaluechain_gid"].ToString();
                //lssecondaryvaluechain_name = objODBCDatareader["secondaryvaluechain_name"].ToString();
                lsinterest_status = objODBCDatareader["interest_status"].ToString();
                lsmoratorium_status = objODBCDatareader["moratorium_status"].ToString();
                lsmoratorium_type = objODBCDatareader["moratorium_type"].ToString();
                if (objODBCDatareader["moratorium_startdate"].ToString() == "")
                {
                }
                else
                {
                    lsmoratorium_startdate = Convert.ToDateTime(objODBCDatareader["moratorium_startdate"]).ToString("yyyy-MM-dd HH:mm:ss");
                }
                if (objODBCDatareader["moratorium_enddate"].ToString() == "")
                {
                }
                else
                {
                    lsmoratorium_enddate = Convert.ToDateTime(objODBCDatareader["moratorium_enddate"]).ToString("yyyy-MM-dd HH:mm:ss");
                }
                lsenduse_purpose = objODBCDatareader["enduse_purpose"].ToString();

                objODBCDatareader.Close();
            }
            try
            {
                // msSQL = "select application_gid from agr_mst_tapplication2loan where producttype_gid='" + values.producttype_gid + "' and " +
                //" productsubtype_gid='" + values.productsubtype_gid + "' and application_gid='" + values.application_gid + "' and  " +
                //" application2loan_gid<>'" + values.application2loan_gid + "'"; ;
                // objODBCDatareader = objdbconn.GetDataReader(msSQL);
                // if (objODBCDatareader.HasRows == false)
                // {
                //     objODBCDatareader.Close();
                if (values.product_type == "Agri Receivable Finance (ARF)")
                {
                    msSQL = "select application2loan_gid from agr_mst_tapplication2buyer  where application2loan_gid='" + employee_gid + "' or " +
                        " application2loan_gid='" + values.application2loan_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == false)
                    {

                        values.message = "Kindly add atleast one Buyer";
                        values.status = false;
                        return;

                    }
                    else
                    {
                        msSQL = " update agr_mst_tapplication2loan set " +
                                " product_type='" + values.product_type + "'," +
                                " producttype_gid='" + values.producttype_gid + "',";
                        if (Convert.ToDateTime(values.facilityrequested_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                        {

                        }
                        else
                        {
                            msSQL += " facilityrequested_date='" + Convert.ToDateTime(values.facilityrequested_date).ToString("yyyy-MM-dd 00:00:00") + "',";
                        }
                        msSQL += " productsub_type='" + values.productsub_type + "'," +
                                 " productsubtype_gid='" + values.productsubtype_gid + "'," +
                                 " loantype_gid='" + values.loantype_gid + "'," +
                                 " loan_type='" + values.loan_type + "'," +
                                 " loanfacility_amount='" + values.facilityloan_amount + "'," +
                                 " rate_interest='" + values.rate_interest + "'," +
                                 " penal_interest='" + values.penal_interest + "'," +
                                 " tenureproduct_year='" + values.tenureproduct_year + "'," +
                                 " tenureproduct_month='" + values.tenureproduct_month + "'," +
                                 " tenureproduct_days='" + values.tenureproduct_days + "'," +
                                 " tenureoverall_limit='" + values.tenureoverall_limit + "'," +
                                 " facility_type='" + values.facility_type + "'," +
                                 " facility_mode='" + values.facility_mode + "'," +
                                 " principalfrequency_name='" + values.principalfrequency_name + "'," +
                                 " principalfrequency_gid='" + values.principalfrequency_gid + "'," +
                                 " interestfrequency_name='" + values.interestfrequency_name + "'," +
                                 " interestfrequency_gid='" + values.interestfrequency_gid + "'," +
                                 " program_gid='" + values.program_gid + "'," +
                                 " program='" + values.program + "'," +
                                 //" primaryvaluechain_gid='" + fsprimaryvaluechain_gid + "'," +
                                 //" primaryvaluechain_name='" + fsprimaryvaluechain_name + "'," +
                                 //" secondaryvaluechain_gid='" + fssecondaryvaluechain_gid + "'," +
                                 //" secondaryvaluechain_name='" + fssecondaryvaluechain_name + "'," +
                                 " interest_status='" + values.interest_status + "'," +
                                 " moratorium_status='" + values.moratorium_status + "',";
                        if (values.moratorium_status == "Yes")
                        {
                            msSQL += " moratorium_type='" + values.moratorium_type + "',";
                            if (Convert.ToDateTime(values.moratorium_startdate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                            {

                            }
                            else
                            {
                                msSQL += " moratorium_startdate='" + Convert.ToDateTime(values.moratorium_startdate).ToString("yyyy-MM-dd 00:00:00") + "',";
                            }
                            if (Convert.ToDateTime(values.moratorium_enddate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                            {

                            }
                            else
                            {
                                msSQL += " moratorium_enddate='" + Convert.ToDateTime(values.moratorium_enddate).ToString("yyyy-MM-dd 00:00:00") + "',";
                            }
                        }
                        else
                        {
                            msSQL += " moratorium_type= null," +
                                    " moratorium_startdate = null," +
                                    " moratorium_enddate = null,";
                        }


                        msSQL += " enduse_purpose='" + values.enduse_purpose + "'," +

                                 " source_type='" + values.source_type + "'," +
                                 " guideline_value='" + values.guideline_value + "',";
                        if (values.guideline_date == null || values.guideline_date == "")
                        {
                            msSQL += " guideline_date=null,";
                        }
                        else
                        {
                            msSQL += " guideline_date='" + Convert.ToDateTime(values.guideline_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                        }
                        if (values.marketvalue_date == null || values.marketvalue_date == "")
                        {
                            msSQL += " marketvalue_date=null,";
                        }
                        else
                        {
                            msSQL += " marketvalue_date='" + Convert.ToDateTime(values.marketvalue_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                        }
                        if (values.market_value == null || values.market_value == "")
                        {
                            msSQL += " market_value='0.00',";
                        }
                        else
                        {
                            msSQL += " market_value='" + values.market_value.Replace(",", "") + "',";
                        }
                        if (values.forcedsource_value == null || values.forcedsource_value == "")
                        {
                            msSQL += " forcedsource_value='0.00',";
                        }
                        else
                        {
                            msSQL += " forcedsource_value='" + values.forcedsource_value.Replace(",", "") + "',";
                        }
                        if (values.collateralSSV_value == null || values.collateralSSV_value == "")
                        {
                            msSQL += " collateralSSV_value='0.00',";
                        }
                        else
                        {
                            msSQL += " collateralSSV_value='" + values.collateralSSV_value.Replace(",", "") + "',";
                        }
                        if (values.forcedvalueassessed_on == null || values.forcedvalueassessed_on == "")
                        {
                            msSQL += " forcedvalueassessed_on=null,";
                        }
                        else
                        {
                            msSQL += " forcedvalueassessed_on='" + Convert.ToDateTime(values.forcedvalueassessed_on).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                        }
                        if (values.collateralobservation_summary == null)
                        {
                            msSQL += " collateralobservation_summary=null,";
                        }
                        else
                        {
                            msSQL += " collateralobservation_summary='" + values.collateralobservation_summary.Replace("'", " ") + "',";
                        }

                        msSQL += " product_gid='" + values.product_gid + "'," +
                                 " product_name='" + values.product_name + "'," +
                                 " variety_gid='" + values.variety_gid + "'," +
                                 " variety_name='" + values.variety_name + "'," +
                                 " sector_name='" + values.sector_name + "'," +
                                 " category_name='" + values.category_name + "'," +
                                 " botanical_name='" + values.botanical_name + "'," +
                                 " alternative_name='" + values.alternative_name + "'," +
                                 " trade_orginatedby='" + values.trade_orginatedby + "'," +
                                 " SA_Brokerage='" + values.SA_Brokerage + "',";
                        if (values.programlimit_validdfrom == null || values.programlimit_validdfrom == "")
                            msSQL += " programlimit_validdfrom= null,";
                        else
                            msSQL += " programlimit_validdfrom='" + Convert.ToDateTime(values.programlimit_validdfrom).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                        if (values.programlimit_validdto == null || values.programlimit_validdto == "")
                            msSQL += " programlimit_validdto = null,";
                        else
                            msSQL += " programlimit_validdto='" + Convert.ToDateTime(values.programlimit_validdto).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                        msSQL += " programoverall_limit='" + values.programoverall_limit + "'," +
                                 " updated_by='" + employee_gid + "'," +
                                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                 " where application2loan_gid='" + values.application2loan_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult == 1)
                        {
                            if (values.product_type == "Agri Receivable Finance (ARF)")
                            {
                                msSQL = "update agr_mst_tapplication2buyer set application2loan_gid='" + values.application2loan_gid + "' where application2loan_gid='" + employee_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            else
                            {
                                msSQL = "delete from  agr_mst_tapplication2buyer  where application2loan_gid='" + employee_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            }
                            if (values.loan_type == "Secured")
                            {
                                msSQL = "update agr_mst_tuploadcollateraldocument set application2loan_gid='" + values.application2loan_gid + "' where application2loan_gid='" + employee_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            else
                            {
                                msSQL = "delete from agr_mst_tuploadcollateraldocument where application2loan_gid='" + employee_gid + "' or application2loan_gid='" + values.application2loan_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }

                            msSQL = "update agr_mst_tapploan2supplierdtl set application2loan_gid='" + values.application2loan_gid + "' where application2loan_gid='" + employee_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = "update agr_mst_tapploan2paymenttypecustomer set application2loan_gid='" + values.application2loan_gid + "' where application2loan_gid='" + employee_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = "update agr_mst_tapploan2supplierpayment set application2loan_gid='" + values.application2loan_gid + "' where application2loan_gid='" + employee_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = "update agr_mst_tapplication2product set application2loan_gid='" + values.application2loan_gid + "' where application2loan_gid='" + employee_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            //msSQL = "update agr_mst_tapplication2product set application_gid='" + lsapplication_gid + "'" +
                            //        " where application2loan_gid='" + values.application2loan_gid + "' and application_gid is null ";
                            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msGetGid = objcmnfunctions.GetMasterGID("A2LU");
                            msSQL = " insert into agr_mst_tapplication2loanupdateLOG(" +
                                   " application2loanupdateLOG_gid ," +
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
                                   " programlimit_validdfrom," +
                                   " programlimit_validdto," +
                                   " programoverall_limit," +
                                   " tenureproduct_year," +
                                   " tenureproduct_month," +
                                   " tenureproduct_days," +
                                   " tenureoverall_limit ," +
                                   " facility_type," +
                                   " facility_mode," +
                                       " principalfrequency_name," +
                                       " principalfrequency_gid," +
                                       " interestfrequency_name," +
                                       " interestfrequency_gid," +
                                       " program_gid," +
                                       " program," +
                                       //" primaryvaluechain_gid," +
                                       //" primaryvaluechain_name," +
                                       //" secondaryvaluechain_gid," +
                                       //" secondaryvaluechain_name," +
                                       " interest_status," +
                                       " moratorium_status," +
                                       " moratorium_type," +
                                       " moratorium_startdate," +
                                       " moratorium_enddate," +
                                       " product_gid," +
                                       " product_name," +
                                       " variety_gid," +
                                       " variety_name," +
                                       " sector_name," +
                                       " category_name," +
                                       " botanical_name," +
                                       " alternative_name," +
                                       " trade_orginatedby, " +
                                       " SA_Brokerage, " +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + msGetGid + "'," +
                                   "'" + values.application2loan_gid + "'," +
                                   "'" + values.application_gid + "'," +
                                   "'" + lsfacilityrequested_date + "'," +
                                   "'" + lsproduct_type + "'," +
                                   "'" + lsproducttype_gid + "'," +
                                   "'" + lsproductsub_type + "'," +
                                   "'" + lsproductsubtype_gid + "'," +
                                   "'" + lsloantype_gid + "'," +
                                   "'" + lsloan_type + "'," +
                                   "'" + lsfacilityloan_amount.Replace(",", "") + "',";
                                   if (lsrate_interest == null || lsrate_interest == "")
                            {
                                msSQL += " 0.00,";
                            }
                            else
                            {
                                msSQL += "'" + lsrate_interest + "'," ;
                            }

                            msSQL +=
                                //"'" + lsrate_interest + "'," +
                                   "'" + lspenal_interest + "',";
                            if (values.programlimit_validdfrom == null || values.programlimit_validdfrom == "")
                                msSQL += "null,";
                            else
                                msSQL += "'" + Convert.ToDateTime(values.programlimit_validdfrom).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            if (values.programlimit_validdto == null || values.programlimit_validdto == "")
                                msSQL += "null,";
                            else
                                msSQL += "'" + Convert.ToDateTime(values.programlimit_validdto).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            msSQL += "'" + values.programoverall_limit + "'," +
                                    "'" + lstenureproduct_year + "'," +
                                   "'" + lstenureproduct_month + "'," +
                                   "'" + lstenureproduct_days + "'," +
                                   "'" + lstenureoverall_limit + "'," +
                                   "'" + lsfacility_type + "'," +
                                   "'" + values.facility_mode + "'," +
                                   "'" + lsprincipalfrequency_name + "'," +
                                   "'" + lsprincipalfrequency_gid + "'," +
                                   "'" + lsinterestfrequency_name + "'," +
                                   "'" + lsinterestfrequency_gid + "'," +
                                   "'" + lsprogram_gid + "'," +
                                   "'" + lsprogram + "'," +
                                   //"'" + lsprimaryvaluechain_gid + "'," +
                                   //"'" + lsprimaryvaluechain_name + "'," +
                                   //"'" + lssecondaryvaluechain_gid + "'," +
                                   //"'" + lssecondaryvaluechain_name + "'," +
                                   "'" + lsinterest_status + "'," +
                                   "'" + lsmoratorium_status + "'," +
                                   "'" + lsmoratorium_type + "',";
                            if (lsmoratorium_startdate == null || lsmoratorium_startdate == "")
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + lsmoratorium_startdate + "',";
                            }
                            if (lsmoratorium_enddate == null || lsmoratorium_enddate == "")
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + lsmoratorium_enddate + "',";
                            }

                            msSQL += "'" + values.product_gid + "'," +
                            "'" + values.product_name + "'," +
                            "'" + values.variety_gid + "'," +
                            "'" + values.variety_name + "'," +
                            "'" + values.sector_name + "'," +
                            "'" + values.category_name + "'," +
                            "'" + values.botanical_name + "'," +
                            "'" + values.alternative_name + "'," +
                            "'" + values.trade_orginatedby + "'," +
                            "'" + values.SA_Brokerage + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (ConfigurationManager.AppSettings["sysSamagroHyperbrdigeAPIEnable"].ToString() == "Yes")
                            {
                                HttpContext ctx = HttpContext.Current;

                                Thread t = new Thread(new ThreadStart(() =>
                                {
                                    HttpContext.Current = ctx;

                                    objFnSamAgroHBAPIContract.UpdateContractProductHBAPI(values.application2loan_gid);

                                    objFnSamAgroHBAPIContract.PostCommodityHBAPI(values.application2loan_gid);

                                    objFnSamAgroHBAPIContract.AddSupplierToContractHBAPI(values.application2loan_gid);

                                }));

                                t.Start();
                            }

                            values.status = true;
                            values.message = "Loan Details Updated Successfully";


                        }
                    }
                    objODBCDatareader.Close();
                }
                else
                {
                    msSQL = " update agr_mst_tapplication2loan set " +
                    " product_type='" + values.product_type + "'," +
                     " producttype_gid='" + values.producttype_gid + "',";
                    if (Convert.ToDateTime(values.facilityrequested_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                    {

                    }
                    else
                    {
                        msSQL += " facilityrequested_date='" + Convert.ToDateTime(values.facilityrequested_date).ToString("yyyy-MM-dd 00:00:00") + "',";
                    }
                    msSQL += " productsub_type='" + values.productsub_type + "'," +
                             " productsubtype_gid='" + values.productsubtype_gid + "'," +
                             " loantype_gid='" + values.loantype_gid + "'," +
                             " loan_type='" + values.loan_type + "'," +
                             " loanfacility_amount='" + values.facilityloan_amount + "'," +
                             " rate_interest='" + values.rate_interest + "'," +
                             " penal_interest='" + values.penal_interest + "'," +
                            " tenureproduct_year='" + values.tenureproduct_year + "'," +
                             " tenureproduct_month='" + values.tenureproduct_month + "'," +
                             " tenureproduct_days='" + values.tenureproduct_days + "'," +
                             " tenureoverall_limit='" + values.tenureoverall_limit + "'," +
                             " facility_type='" + values.facility_type + "'," +
                             " facility_mode='" + values.facility_mode + "'," +
                             " principalfrequency_name='" + values.principalfrequency_name + "'," +
                             " principalfrequency_gid='" + values.principalfrequency_gid + "'," +
                             " interestfrequency_name='" + values.interestfrequency_name + "'," +
                             " interestfrequency_gid='" + values.interestfrequency_gid + "'," +
                             " program_gid='" + values.program_gid + "'," +
                             " program='" + values.program + "'," +
                             //" primaryvaluechain_gid='" + fsprimaryvaluechain_gid + "'," +
                             //" primaryvaluechain_name='" + fsprimaryvaluechain_name + "'," +
                             //" secondaryvaluechain_gid='" + fssecondaryvaluechain_gid + "'," +
                             //" secondaryvaluechain_name='" + fssecondaryvaluechain_name + "'," +
                             " interest_status='" + values.interest_status + "'," +
                             " moratorium_status='" + values.moratorium_status + "',";
                    if (values.moratorium_status == "Yes")
                    {
                        msSQL += " moratorium_type='" + values.moratorium_type + "',";
                        if (Convert.ToDateTime(values.moratorium_startdate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                        {

                        }
                        else
                        {
                            msSQL += " moratorium_startdate='" + Convert.ToDateTime(values.moratorium_startdate).ToString("yyyy-MM-dd 00:00:00") + "',";
                        }
                        if (Convert.ToDateTime(values.moratorium_enddate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                        {

                        }
                        else
                        {
                            msSQL += " moratorium_enddate='" + Convert.ToDateTime(values.moratorium_enddate).ToString("yyyy-MM-dd 00:00:00") + "',";
                        }
                    }
                    else
                    {
                        msSQL += " moratorium_type= null," +
                                " moratorium_startdate = null," +
                                " moratorium_enddate = null,";
                    }
                    msSQL += " source_type='" + values.source_type + "'," +
                                 " guideline_value='" + values.guideline_value + "',";
                    if (values.guideline_date == null || values.guideline_date == "")
                    {
                        msSQL += " guideline_date=null,";
                    }
                    else
                    {
                        msSQL += " guideline_date='" + Convert.ToDateTime(values.guideline_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }
                    if (values.marketvalue_date == null || values.marketvalue_date == "")
                    {
                        msSQL += " marketvalue_date=null,";
                    }
                    else
                    {
                        msSQL += " marketvalue_date='" + Convert.ToDateTime(values.marketvalue_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }
                    if (values.market_value == null || values.market_value == "")
                    {
                        msSQL += " market_value='0.00',";
                    }
                    else
                    {
                        msSQL += " market_value='" + values.market_value.Replace(",", "") + "',";
                    }
                    if (values.forcedsource_value == null || values.forcedsource_value == "")
                    {
                        msSQL += " forcedsource_value='0.00',";
                    }
                    else
                    {
                        msSQL += " forcedsource_value='" + values.forcedsource_value.Replace(",", "") + "',";
                    }
                    if (values.collateralSSV_value == null || values.collateralSSV_value == "")
                    {
                        msSQL += " collateralSSV_value='0.00',";
                    }
                    else
                    {
                        msSQL += " collateralSSV_value='" + values.collateralSSV_value.Replace(",", "") + "',";
                    }
                    if (values.forcedvalueassessed_on == null || values.forcedvalueassessed_on == "")
                    {
                        msSQL += " forcedvalueassessed_on=null,";
                    }
                    else
                    {
                        msSQL += " forcedvalueassessed_on='" + Convert.ToDateTime(values.forcedvalueassessed_on).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }
                    if (values.collateralobservation_summary == null)
                    {
                        msSQL += " collateralobservation_summary=null,";
                    }
                    else
                    {
                        msSQL += " collateralobservation_summary='" + values.collateralobservation_summary.Replace("'", " ") + "',";
                    }

                    msSQL += " enduse_purpose='" + values.enduse_purpose + "'," +
                           " product_gid= '" + values.product_gid + "'," +
                         " product_name='" + values.product_name + "'," +
                         " variety_gid= '" + values.variety_gid + "'," +
                         " variety_name='" + values.variety_name + "'," +
                         " sector_name= '" + values.sector_name + "'," +
                         " category_name='" + values.category_name + "'," +
                         " botanical_name= '" + values.botanical_name + "'," +
                         " alternative_name='" + values.alternative_name + "'," +
                         " trade_orginatedby='" + values.trade_orginatedby + "'," +
                         " SA_Brokerage='" + values.SA_Brokerage + "',";
                    if (values.programlimit_validdfrom == null || values.programlimit_validdfrom == "")
                        msSQL += " programlimit_validdfrom= null,";
                    else
                        msSQL += " programlimit_validdfrom='" + Convert.ToDateTime(values.programlimit_validdfrom).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    if (values.programlimit_validdto == null || values.programlimit_validdto == "")
                        msSQL += " programlimit_validdto = null,";
                    else
                        msSQL += " programlimit_validdto='" + Convert.ToDateTime(values.programlimit_validdto).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    msSQL += " programoverall_limit='" + values.programoverall_limit + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where application2loan_gid='" + values.application2loan_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult == 1)
                    {
                        if (values.product_type == "Agri Receivable Finance (ARF)")
                        {
                            msSQL = "update agr_mst_tapplication2buyer set application2loan_gid='" + values.application2loan_gid + "' where application2loan_gid='" + employee_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        else
                        {
                            msSQL = "delete from  agr_mst_tapplication2buyer  where application2loan_gid='" + employee_gid + "' or application2loan_gid='" + values.application2loan_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        if (values.loan_type == "Secured")
                        {
                            msSQL = "update agr_mst_tuploadcollateraldocument set application2loan_gid='" + values.application2loan_gid + "' where application2loan_gid='" + employee_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        else
                        {
                            msSQL = "delete from agr_mst_tuploadcollateraldocument where application2loan_gid='" + employee_gid + "' or application2loan_gid='" + values.application2loan_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }


                        msSQL = "update agr_mst_tapploan2supplierdtl set application2loan_gid='" + values.application2loan_gid + "' where application2loan_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_mst_tapploan2paymenttypecustomer set application2loan_gid='" + values.application2loan_gid + "' where application2loan_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_mst_tapploan2supplierpayment set application2loan_gid='" + values.application2loan_gid + "' where application2loan_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_mst_tapplication2product set application2loan_gid='" + values.application2loan_gid + "' where application2loan_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msGetGid = objcmnfunctions.GetMasterGID("A2LU");
                        msSQL = " insert into agr_mst_tapplication2loanupdateLOG(" +
                               " application2loanupdateLOG_gid ," +
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
                               " programlimit_validdfrom," +
                               " programlimit_validdto," +
                               " programoverall_limit," +
                               " tenureproduct_year," +
                               " tenureproduct_month," +
                               " tenureproduct_days," +
                               " tenureoverall_limit ," +
                               " facility_type," +
                               " facility_mode," +
                                   " principalfrequency_name," +
                                   " principalfrequency_gid," +
                                   " interestfrequency_name," +
                                   " interestfrequency_gid," +
                                   " program_gid," +
                                   " program," +
                                   //" primaryvaluechain_gid," +
                                   //" primaryvaluechain_name," +
                                   //" secondaryvaluechain_gid," +
                                   //" secondaryvaluechain_name," +
                                   " interest_status," +
                                   " moratorium_status," +
                                   " moratorium_type," +
                                   " moratorium_startdate," +
                                   " moratorium_enddate," +
                                    " product_gid," +
                                       " product_name," +
                                       " variety_gid," +
                                       " variety_name," +
                                       " sector_name," +
                                       " category_name," +
                                       " botanical_name," +
                                       " alternative_name," +
                                       " trade_orginatedby," +
                                       " SA_Brokerage," +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGetGid + "'," +
                               "'" + values.application2loan_gid + "'," +
                               "'" + values.application_gid + "'," +
                                "'" + Convert.ToDateTime(lsfacilityrequested_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                "'" + lsproduct_type + "'," +
                               "'" + lsproducttype_gid + "'," +
                               "'" + lsproductsub_type + "'," +
                               "'" + lsproductsubtype_gid + "'," +
                               "'" + lsloantype_gid + "'," +
                               "'" + lsloan_type + "'," +
                               "'" + lsfacilityloan_amount.Replace(",", "") + "'," +
                               "'" + lsrate_interest + "'," +
                               "'" + lspenal_interest + "',";
                        if (values.programlimit_validdfrom == null || values.programlimit_validdfrom == "")
                            msSQL += "null,";
                        else
                            msSQL += "'" + Convert.ToDateTime(values.programlimit_validdfrom).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                        if (values.programlimit_validdto == null || values.programlimit_validdto == "")
                            msSQL += "null,";
                        else
                            msSQL += "'" + Convert.ToDateTime(values.programlimit_validdto).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                        msSQL += "'" + values.programoverall_limit + "'," +
                                   "'" + lstenureproduct_year + "'," +
                               "'" + lstenureproduct_month + "'," +
                               "'" + lstenureproduct_days + "'," +
                               "'" + lstenureoverall_limit + "'," +
                               "'" + lsfacility_type + "'," +
                               "'" + values.facility_mode + "'," +
                               "'" + lsprincipalfrequency_name + "'," +
                               "'" + lsprincipalfrequency_gid + "'," +
                               "'" + lsinterestfrequency_name + "'," +
                               "'" + lsinterestfrequency_gid + "'," +
                               "'" + lsprogram_gid + "'," +
                               "'" + lsprogram + "'," +
                               //"'" + lsprimaryvaluechain_gid + "'," +
                               //"'" + lsprimaryvaluechain_name + "'," +
                               //"'" + lssecondaryvaluechain_gid + "'," +
                               //"'" + lssecondaryvaluechain_name + "'," +
                               "'" + lsinterest_status + "'," +
                               "'" + lsmoratorium_status + "'," +
                               "'" + lsmoratorium_type + "',";
                        if (lsmoratorium_startdate == null || lsmoratorium_startdate == "")
                        {
                            msSQL += "null,";
                        }
                        else
                        {
                            msSQL += "'" + Convert.ToDateTime(lsmoratorium_startdate).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                        }
                        if (lsmoratorium_enddate == null || lsmoratorium_enddate == "")
                        {
                            msSQL += "null,";
                        }
                        else
                        {
                            msSQL += "'" + Convert.ToDateTime(lsmoratorium_enddate).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                        }
                        msSQL += "'" + values.product_gid + "'," +
                                   "'" + values.product_name + "'," +
                                   "'" + values.variety_gid + "'," +
                                   "'" + values.variety_name + "'," +
                                   "'" + values.sector_name + "'," +
                                   "'" + values.category_name + "'," +
                                   "'" + values.botanical_name + "'," +
                                   "'" + values.alternative_name + "'," +
                                    "'" + values.trade_orginatedby + "'," +
                                   "'" + values.SA_Brokerage + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (ConfigurationManager.AppSettings["sysSamagroHyperbrdigeAPIEnable"].ToString() == "Yes")
                        {
                            HttpContext ctx = HttpContext.Current;

                            Thread t = new Thread(new ThreadStart(() =>
                            {
                                HttpContext.Current = ctx;
                                
                                objFnSamAgroHBAPIContract.UpdateContractProductHBAPI(values.application2loan_gid);

                                objFnSamAgroHBAPIContract.PostCommodityHBAPI(values.application2loan_gid);

                                objFnSamAgroHBAPIContract.AddSupplierToContractHBAPI(values.application2loan_gid);

                            }));

                            t.Start();
                        }

                        values.status = true;
                        values.message = "Loan Details Updated Successfully";
                    }
                }

                if (values.productsub_type == "STF")
                {
                    if (values.holding_periods == null)
                        values.holding_periods = "";
                    if (values.holdingmonthly_procurement == null)
                        values.holdingmonthly_procurement = "";
                    if (values.extendedholding_periods == null)
                        values.extendedholding_periods = "";
                    if (values.extendedmonthly_procurement == null)
                        values.extendedmonthly_procurement = "";
                    if (values.reimburesementof_expenses == null)
                        values.reimburesementof_expenses = "";
                    if (values.bankfundingdata_filename == null)
                        values.bankfundingdata_filename = "";
                    if (values.bankfundingdata_filepath == null)
                        values.bankfundingdata_filepath = "";
                    if (values.needfor_stocking == null)
                        values.needfor_stocking = "";
                    if (values.product_portfolio == null)
                        values.product_portfolio = "";
                    if (values.production_capacity == null)
                        values.production_capacity = "";
                    if (values.natureof_operations == null)
                        values.natureof_operations = "";
                    if (values.averagemonthly_inventoryholding == null)
                        values.averagemonthly_inventoryholding = "";
                    if (values.financialinstitutions_relationship == null)
                        values.financialinstitutions_relationship = "";
                    if (values.reimburesementof_expensespenalty == null)
                        values.reimburesementof_expensespenalty = "";

                    msSQL = " update agr_mst_tapplication2loan set " +
                            " holding_periods='" + values.holding_periods.Replace("'", "") + "'," +
                            " holdingmonthly_procurement='" + values.holdingmonthly_procurement.Replace("'", "") + "'," +
                            " extendedholding_periods='" + values.extendedholding_periods.Replace("'", "") + "'," +
                            " extendedmonthly_procurement='" + values.extendedmonthly_procurement.Replace("'", "") + "'," +
                            " charges_extendedperiod='" + values.charges_extendedperiod.Replace("'", "") + "'," +
                            " customer_advance='" + values.customer_advance.Replace("'", "") + "'," +
                            " reimburesementof_expenses='" + values.reimburesementof_expenses.Replace("'", "") + "'," +
                            " reimburesementof_expensespenalty='" + values.reimburesementof_expensespenalty.Replace("'", "") + "'," +
                            " bankfundingdata_filename='" + values.bankfundingdata_filename + "'," +
                            " bankfundingdata_filepath='" + values.bankfundingdata_filepath + "'," +
                            " needfor_stocking='" + values.needfor_stocking.Replace("'", "") + "'," +
                            " product_portfolio='" + values.product_portfolio.Replace("'", "") + "'," +
                            " production_capacity='" + values.production_capacity.Replace("'", "") + "'," +
                            " natureof_operations='" + values.natureof_operations.Replace("'", "") + "'," +
                            " averagemonthly_inventoryholding='" + values.averagemonthly_inventoryholding.Replace("'", "") + "'," +
                            " financialinstitutions_relationship='" + values.financialinstitutions_relationship.Replace("'", "") + "'" +
                            " where application2loan_gid='" + values.application2loan_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult == 1)
                    {
                        msSQL = " delete from agr_tmp_tbankfundingdataupload where  application_gid='" + values.application_gid + "' " +
                                " and application2loan_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaDeleteLoanDetail(string application2loan_gid, MdlMstLoanDtl values)
        {
            msSQL = "delete from agr_mst_tapplication2loan where application2loan_gid='" + application2loan_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tapplication2loanupdateLOG where application2loan_gid='" + application2loan_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Loan Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public void DaBuyerDetailsList(string application2loan_gid, MdlMstBuyer values)
        {
            msSQL = "select application2buyer_gid,buyer_name,buyer_gid,buyer_limit,availed_limit,balance_limit,margin,bill_tenure " +
                    " from agr_mst_tapplication2buyer where application2loan_gid='" + application2loan_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstbuyer_list = new List<mstbuyer_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstbuyer_list.Add(new mstbuyer_list
                    {
                        application2buyer_gid = (dr_datarow["application2buyer_gid"].ToString()),
                        buyer_gid = (dr_datarow["buyer_gid"].ToString()),
                        buyer_name = (dr_datarow["buyer_name"].ToString()),
                        buyer_limit = (dr_datarow["buyer_limit"].ToString()),
                        availed_limit = (dr_datarow["availed_limit"].ToString()),
                        balance_limit = (dr_datarow["balance_limit"].ToString()),
                        bill_tenure = (dr_datarow["bill_tenure"].ToString()),
                        margin = (dr_datarow["margin"].ToString())
                    });
                }
                values.mstbuyer_list = getmstbuyer_list;
            }
            dt_datatable.Dispose();
        }

        public void DaBuyerTempDetailsList(string employee_gid, string application2loan_gid, MdlMstBuyer values)
        {
            msSQL = "select application2buyer_gid,buyer_name,buyer_gid,buyer_limit,availed_limit,balance_limit,margin,bill_tenure " +
                    " from agr_mst_tapplication2buyer where application2loan_gid='" + application2loan_gid + "' or application2loan_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstbuyer_list = new List<mstbuyer_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstbuyer_list.Add(new mstbuyer_list
                    {
                        application2buyer_gid = (dr_datarow["application2buyer_gid"].ToString()),
                        buyer_gid = (dr_datarow["buyer_gid"].ToString()),
                        buyer_name = (dr_datarow["buyer_name"].ToString()),
                        buyer_limit = (dr_datarow["buyer_limit"].ToString()),
                        availed_limit = (dr_datarow["availed_limit"].ToString()),
                        balance_limit = (dr_datarow["balance_limit"].ToString()),
                        bill_tenure = (dr_datarow["bill_tenure"].ToString()),
                        margin = (dr_datarow["margin"].ToString())
                    });
                }
                values.mstbuyer_list = getmstbuyer_list;
            }
            dt_datatable.Dispose();
        }

        public void DaBuyerDetailsEdit(string application2buyer_gid, MdlMstBuyer values)
        {
            try
            {
                msSQL = " select application2buyer_gid, application2loan_gid, buyer_gid,buyer_name,buyer_limit,availed_limit,balance_limit,bill_tenure,margin" +
              " from agr_mst_tapplication2buyer where application2buyer_gid='" + application2buyer_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application2buyer_gid = objODBCDatareader["application2buyer_gid"].ToString();
                    values.application2loan_gid = objODBCDatareader["application2loan_gid"].ToString();
                    values.buyer_gid = objODBCDatareader["buyer_gid"].ToString();
                    values.buyer_name = objODBCDatareader["buyer_name"].ToString();
                    values.buyer_limit = objODBCDatareader["buyer_limit"].ToString();
                    values.availed_limit = objODBCDatareader["availed_limit"].ToString();
                    values.balance_limit = objODBCDatareader["balance_limit"].ToString();
                    values.bill_tenure = objODBCDatareader["bill_tenure"].ToString();
                    values.margin = objODBCDatareader["margin"].ToString();
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

        public void DBuyerDetailsUpdate(string employee_gid, MdlMstBuyer values)
        {
            msSQL = " select application2buyer_gid, application2loan_gid, buyer_gid,buyer_name,buyer_limit,availed_limit,balance_limit,bill_tenure,margin" +
             " from agr_mst_tapplication2buyer where application2buyer_gid='" + values.application2buyer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsapplication2buyer_gid = objODBCDatareader["application2buyer_gid"].ToString();
                lsapplication2loan_gid = objODBCDatareader["application2loan_gid"].ToString();
                lsbuyer_gid = objODBCDatareader["buyer_gid"].ToString();
                lsbuyer_name = objODBCDatareader["buyer_name"].ToString();
                lsbuyer_limit = objODBCDatareader["buyer_limit"].ToString();
                lsavailed_limit = objODBCDatareader["availed_limit"].ToString();
                lsbalance_limit = objODBCDatareader["balance_limit"].ToString();
                lsbill_tenure = objODBCDatareader["bill_tenure"].ToString();
                lsmargin = objODBCDatareader["margin"].ToString();
                objODBCDatareader.Close();
            }
            try
            {
                msSQL = " update agr_mst_tapplication2buyer set " +
                        " buyer_gid='" + values.buyer_gid + "'," +
                         " buyer_name='" + values.buyer_name + "',";
                if (values.buyer_limit == null)
                {
                    msSQL += " buyer_limit='0.00',";
                }
                else
                {
                    msSQL += " buyer_limit='" + values.buyer_limit.Replace(",", "") + "',";
                }
                if (values.availed_limit == null)
                {
                    msSQL += " availed_limit='0.00',";
                }
                else
                {
                    msSQL += " availed_limit='" + values.availed_limit.Replace(",", "") + "',";
                }


                if (values.balance_limit == null)
                {
                    msSQL += " balance_limit='0.00',";
                }
                else
                {
                    msSQL += " balance_limit='" + values.balance_limit.Replace(",", "") + "',";
                }
                msSQL += " bill_tenure='" + values.bill_tenure + "'," +
                         " margin='" + values.margin + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where application2buyer_gid='" + values.application2buyer_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("A2BU");
                    msSQL = " insert into agr_mst_tapplication2buyerUpdateLog(" +
                   " application2buyerUpdateLOG_gid ," +
                   " application2buyer_gid ," +
                   " application2loan_gid," +
                   " buyer_gid," +
                   " buyer_name," +
                   " buyer_limit," +
                   " availed_limit," +
                   " balance_limit ," +
                   " bill_tenure," +
                   " margin," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.application2buyer_gid + "'," +
                   "'" + values.application2loan_gid + "'," +
                   "'" + lsbuyer_gid + "'," +
                   "'" + lsbuyer_name + "',";
                    if (lsbuyer_limit == null)
                    {
                        msSQL += "'0.00',";
                    }
                    else
                    {
                        msSQL += "'" + lsbuyer_limit.Replace(",", "") + "',";
                    }
                    if (values.availed_limit == null)
                    {
                        msSQL += "'0.00',";
                    }
                    else
                    {
                        msSQL += "'" + lsavailed_limit.Replace(",", "") + "',";
                    }


                    if (lsbalance_limit == null)
                    {
                        msSQL += "'0.00',";
                    }
                    else
                    {
                        msSQL += "'" + lsbalance_limit.Replace(",", "") + "',";
                    }
                    msSQL += "'" + lsbill_tenure + "'," +
                             "'" + lsmargin + "'," +
                             "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Buyer Details Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaDeleteBuyerDetails(string application2buyer_gid, MdlMstBuyer values, string employee_gid)
        {
            msSQL = "delete from agr_mst_tapplication2buyer where application2buyer_gid='" + application2buyer_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tapplication2buyerUpdateLog where application2buyer_gid='" + application2buyer_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select application2buyer_gid,buyer_name,buyer_gid,buyer_limit,availed_limit,balance_limit,margin,bill_tenure " +
                    " from agr_mst_tapplication2buyer where application2loan_gid='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstbuyer_list = new List<mstbuyer_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstbuyer_list.Add(new mstbuyer_list
                        {
                            application2buyer_gid = (dr_datarow["application2buyer_gid"].ToString()),
                            buyer_gid = (dr_datarow["buyer_gid"].ToString()),
                            buyer_name = (dr_datarow["buyer_name"].ToString()),
                            buyer_limit = (dr_datarow["buyer_limit"].ToString()),
                            availed_limit = (dr_datarow["availed_limit"].ToString()),
                            balance_limit = (dr_datarow["balance_limit"].ToString()),
                            bill_tenure = (dr_datarow["bill_tenure"].ToString()),
                            margin = (dr_datarow["margin"].ToString())
                        });
                    }
                    values.mstbuyer_list = getmstbuyer_list;
                }
                dt_datatable.Dispose();

                values.message = "Buyer Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }
        public void DaPostEditServiceCharges(string employee_gid, MdlProductCharges values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("AP2C");

            string producttypegid = string.Empty;
            string producttype = string.Empty;
            string application2loan_gid = string.Empty;
            if (values.producttypelist != null)
            {
                for (var i = 0; i < values.producttypelist.Count; i++)
                {
                    producttypegid += values.producttypelist[i].producttype_gid + ",";
                    producttype += values.producttypelist[i].product_type + ",";
                    application2loan_gid += values.producttypelist[i].application2loan_gid + ",";

                }
                producttypegid = producttypegid.TrimEnd(',');
                producttype = producttype.TrimEnd(',');
                application2loan_gid = application2loan_gid.TrimEnd(',');
            }

            msSQL = "insert into agr_mst_tapplicationservicecharge(" +
                " application2servicecharge_gid," +
                " application2loan_gid, " +
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
                 "'" + application2loan_gid + "'," +
                       "'" + values.application_gid + "'," +
                       "'" + values.processing_fee + "'," +
                       "'" + values.processing_collectiontype + "'," +
                       "'" + values.doc_charges + "'," +
                       "'" + values.doccharge_collectiontype + "'," +
                       "'" + values.fieldvisit_charge + "'," +
                       "'" + values.fieldvisit_collectiontype + "'," +
                       "'" + values.adhoc_fee + "'," +
                       "'" + values.adhoc_collectiontype + "'," +
                       "'" + values.life_insurance + "'," +
                       "'" + values.lifeinsurance_collectiontype + "'," +
                       "'" + values.acct_insurance + "'," +
                       "'" + values.acctinsurance_collectiontype + "'," +
                       "'" + values.total_collect + "'," +
                       "'" + values.total_deduct + "'," +
                       "'" + producttype + "'," +
                       "'" + producttypegid + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



            msSQL = " select application_gid,application2servicecharge_gid, processing_fee,processing_collectiontype,doc_charges," +
                    " doccharge_collectiontype,fieldvisit_charges,fieldvisit_charges_collectiontype,adhoc_fee,adhoc_collectiontype," +
                    " life_insurance,lifeinsurance_collectiontype,acct_insurance,total_collect,total_deduct,product_type,acctinsurance_collectiontype, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                    " from agr_mst_tapplicationservicecharge a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                      " where a.application_gid = '" + values.application_gid + "' order by application2servicecharge_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getproductcharges_list = new List<servicecharges_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getproductcharges_list.Add(new servicecharges_list
                    {
                        application2servicecharge_gid = (dr_datarow["application2servicecharge_gid"].ToString()),
                        processing_fee = (dr_datarow["processing_fee"].ToString()),
                        processing_collectiontype = (dr_datarow["processing_collectiontype"].ToString()),
                        doc_charges = (dr_datarow["doc_charges"].ToString()),
                        doccharge_collectiontype = (dr_datarow["doccharge_collectiontype"].ToString()),
                        fieldvisit_charge = (dr_datarow["fieldvisit_charges"].ToString()),
                        fieldvisit_collectiontype = (dr_datarow["fieldvisit_charges_collectiontype"].ToString()),
                        adhoc_fee = (dr_datarow["adhoc_fee"].ToString()),
                        adhoc_collectiontype = (dr_datarow["adhoc_collectiontype"].ToString()),
                        life_insurance = (dr_datarow["life_insurance"].ToString()),
                        lifeinsurance_collectiontype = (dr_datarow["lifeinsurance_collectiontype"].ToString()),
                        acct_insurance = (dr_datarow["acct_insurance"].ToString()),
                        total_collect = (dr_datarow["total_collect"].ToString()),
                        total_deduct = (dr_datarow["total_deduct"].ToString()),
                        product_type = (dr_datarow["product_type"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        acctinsurance_collectiontype = (dr_datarow["acctinsurance_collectiontype"].ToString()),
                    });
                }
                values.servicecharges_list = getproductcharges_list;
            }
            dt_datatable.Dispose();


            msSQL = " select generatelsa_gid from ocs_trn_tgeneratelsa a " +
                    " left join ocs_trn_tprocesstype_assign b on b.application_gid = a.application_gid " +
                    " where a.application_gid = '" + values.application_gid + "' and b.menu_gid = '" + getMenuClass.LSA + "' and maker_approvalflag = 'N'";
            string generatelsa_gid = objdbconn.GetExecuteScalar(msSQL);
            if (generatelsa_gid != "")
            {
                string msgetfeechargeGid = objcmnfunctions.GetMasterGID("LFCG");

                msSQL = " insert into ocs_trn_tlsafeescharge(" +
                        " lsafeescharge_gid, " +
                        " application2servicecharge_gid," +
                        " application_gid," +
                        " generatelsa_gid, " +
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
                        " created_by," +
                        " created_date) values(" +
                        "'" + msgetfeechargeGid + "'," +
                        "'" + msGetGid + "'," +
                        "'" + values.application_gid + "'," +
                        "'" + generatelsa_gid + "'," +
                        "'" + values.processing_fee + "'," +
                        "'" + values.processing_collectiontype + "'," +
                        "'" + values.doc_charges + "'," +
                        "'" + values.doccharge_collectiontype + "'," +
                        "'" + values.fieldvisit_charge + "'," +
                        "'" + values.fieldvisit_collectiontype + "'," +
                        "'" + values.adhoc_fee + "'," +
                        "'" + values.adhoc_collectiontype + "'," +
                        "'" + values.life_insurance + "'," +
                        "'" + values.lifeinsurance_collectiontype + "'," +
                        "'" + values.acct_insurance + "'," +
                        "'" + values.acctinsurance_collectiontype + "'," +
                        "'" + values.total_collect + "'," +
                        "'" + values.total_deduct + "'," +
                        "'" + producttype + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (mnResult != 0)
            {
                msSQL = "update agr_mst_tapplication set productcharges_status='Completed' where application_gid = '" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.status = true;
                values.message = "Service Charge Details added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }
        public void DaCollateralDetailsList(string employee_gid, string application_gid, MdlMstCollatertal values)
        {
            msSQL = "select application2collateral_gid,source_type,guideline_value,market_value,forcedsource_value,collateralSSV_value," +
                    " date_format(guideline_date,'%d-%m-%Y') as guideline_date,date_format(forcedvalueassessed_on,'%d-%m-%Y') as forcedvalueassessed_on," +
                    " date_format(marketvalue_date,'%d-%m-%Y') as marketvalue_date,collateralobservation_summary " +
                    " from agr_mst_tapplication2collateral where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcollatertal_list = new List<collatertal_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcollatertal_list.Add(new collatertal_list
                    {
                        application2collateral_gid = (dr_datarow["application2collateral_gid"].ToString()),
                        source_type = (dr_datarow["source_type"].ToString()),
                        guideline_value = (dr_datarow["guideline_value"].ToString()),
                        market_value = (dr_datarow["market_value"].ToString()),
                        forcedsource_value = (dr_datarow["forcedsource_value"].ToString()),
                        collateralSSV_value = (dr_datarow["collateralSSV_value"].ToString()),
                        collateralobservation_summary = (dr_datarow["collateralobservation_summary"].ToString()),
                        guideline_date = (dr_datarow["guideline_date"].ToString()),
                        forcedvalueassessed_on = (dr_datarow["forcedvalueassessed_on"].ToString()),
                        marketvalue_date = (dr_datarow["marketvalue_date"].ToString()),
                    });
                }
                values.collatertal_list = getcollatertal_list;
            }
            dt_datatable.Dispose();
            msSQL = " select collateraldocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by,a.document_title " +
                    " from agr_mst_tuploadcollateraldocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                    " and b.user_gid = c.user_gid and application2collateral_gid='" + employee_gid + "'";

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
                        document_gid = (dr_datarow["collateraldocument_gid"].ToString()),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString(),
                        document_title = dr_datarow["document_title"].ToString()
                    });
                }
                values.DocumentList = get_filename;
            }
            dt_datatable.Dispose();
        }

        public void DaCollateralTempDetailsList(string employee_gid, string application_gid, MdlMstCollatertal values)
        {
            msSQL = "select application2collateral_gid,source_type,guideline_value,market_value,forcedsource_value,collateralSSV_value," +
                    " date_format(guideline_date,'%d-%m-%Y') as guideline_date,date_format(forcedvalueassessed_on,'%d-%m-%Y') as forcedvalueassessed_on," +
                    " date_format(marketvalue_date,'%d-%m-%Y') as marketvalue_date,collateralobservation_summary " +
                    " from agr_mst_tapplication2collateral where application_gid='" + employee_gid + "' or application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcollatertal_list = new List<collatertal_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcollatertal_list.Add(new collatertal_list
                    {
                        application2collateral_gid = (dr_datarow["application2collateral_gid"].ToString()),
                        source_type = (dr_datarow["source_type"].ToString()),
                        guideline_value = (dr_datarow["guideline_value"].ToString()),
                        market_value = (dr_datarow["market_value"].ToString()),
                        forcedsource_value = (dr_datarow["forcedsource_value"].ToString()),
                        collateralSSV_value = (dr_datarow["collateralSSV_value"].ToString()),
                        collateralobservation_summary = (dr_datarow["collateralobservation_summary"].ToString()),
                        guideline_date = (dr_datarow["guideline_date"].ToString()),
                        forcedvalueassessed_on = (dr_datarow["forcedvalueassessed_on"].ToString()),
                        marketvalue_date = (dr_datarow["marketvalue_date"].ToString()),
                    });
                }
                values.collatertal_list = getcollatertal_list;
            }
            dt_datatable.Dispose();
        }

        public void DaCollateralDetailsEdit(string application2collateral_gid, MdlMstCollatertal values)
        {
            try
            {
                msSQL = " select application2collateral_gid, application_gid, source_type,guideline_value,market_value," +
                    " date_format(guideline_date, '%Y-%m-%d') as guideline_dateedit,date_format(marketvalue_date, '%Y-%m-%d') as marketvalue_dateedit,forcedsource_value," +
                    " collateralSSV_value,date_format(forcedvalueassessed_on, '%Y-%m-%d') as forcedvalueassessed_onedit,collateralobservation_summary " +
                    " from agr_mst_tapplication2collateral where application2collateral_gid='" + application2collateral_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application2collateral_gid = objODBCDatareader["application2collateral_gid"].ToString();
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.source_type = objODBCDatareader["source_type"].ToString();
                    values.guideline_value = objODBCDatareader["guideline_value"].ToString();
                    values.guideline_date = objODBCDatareader["guideline_dateedit"].ToString();
                    values.marketvalue_date = objODBCDatareader["marketvalue_dateedit"].ToString();
                    values.market_value = objODBCDatareader["market_value"].ToString();
                    values.forcedsource_value = objODBCDatareader["forcedsource_value"].ToString();
                    values.collateralSSV_value = objODBCDatareader["collateralSSV_value"].ToString();
                    values.forcedvalueassessed_on = objODBCDatareader["forcedvalueassessed_onedit"].ToString();
                    values.collateralobservation_summary = objODBCDatareader["collateralobservation_summary"].ToString();
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

        public void DaCollateralDetailsUpdate(string employee_gid, MdlMstCollatertal values)
        {
            msSQL = " select application2collateral_gid, application_gid, source_type,guideline_value,guideline_date,marketvalue_date,market_value," +
                    " forcedsource_value,collateralSSV_value,forcedvalueassessed_on,collateralobservation_summary " +
                    " from agr_mst_tapplication2collateral where application2collateral_gid='" + values.application2collateral_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsapplication2collateral_gid = objODBCDatareader["application2collateral_gid"].ToString();
                lsapplication_gid = objODBCDatareader["application_gid"].ToString();
                lssource_type = objODBCDatareader["source_type"].ToString();
                lsguideline_value = objODBCDatareader["guideline_value"].ToString();
                if (objODBCDatareader["guideline_date"].ToString() == "")
                {
                }
                else
                {
                    lsguideline_date = Convert.ToDateTime(objODBCDatareader["guideline_date"]).ToString("dd-MM-yyyy");
                }
                if (objODBCDatareader["marketvalue_date"].ToString() == "")
                {
                }
                else
                {
                    lsmarketvalue_date = Convert.ToDateTime(objODBCDatareader["marketvalue_date"]).ToString("dd-MM-yyyy");
                }
                lsmarket_value = objODBCDatareader["market_value"].ToString();
                lsforcedsource_value = objODBCDatareader["forcedsource_value"].ToString();
                lscollateralSSV_value = objODBCDatareader["collateralSSV_value"].ToString();
                if (objODBCDatareader["forcedvalueassessed_on"].ToString() == "")
                {
                }
                else
                {
                    lsforcedvalueassessed_on = Convert.ToDateTime(objODBCDatareader["forcedvalueassessed_on"]).ToString("dd-MM-yyyy");
                }
                lscollateralobservation_summary = objODBCDatareader["collateralobservation_summary"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tapplication2collateral set " +
                        " source_type='" + values.source_type + "'," +
                         " guideline_value='" + values.guideline_value + "',";
                if (values.guideline_date == null)
                {
                    msSQL += " guideline_date='null',";
                }
                else
                {
                    msSQL += " guideline_date='" + Convert.ToDateTime(values.guideline_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                if (values.marketvalue_date == null)
                {
                    msSQL += " marketvalue_date='null',";
                }
                else
                {
                    msSQL += " marketvalue_date='" + Convert.ToDateTime(values.marketvalue_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                msSQL += " market_value='" + values.market_value + "'," +
                         " forcedsource_value='" + values.forcedsource_value + "'," +
                         " collateralSSV_value='" + values.collateralSSV_value + "',";
                if (values.forcedvalueassessed_on == null)
                {
                    msSQL += " forcedvalueassessed_on='null',";
                }
                else
                {
                    msSQL += " forcedvalueassessed_on='" + Convert.ToDateTime(values.forcedvalueassessed_on).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                msSQL += " collateralobservation_summary='" + values.collateralobservation_summary + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where application2collateral_gid='" + values.application2collateral_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msSQL = "update agr_mst_tuploadcollateraldocument set application2collateral_gid='" + values.application2collateral_gid + "'" +
                        " where application2collateral_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msGetGid = objcmnfunctions.GetMasterGID("A2CU");
                    msSQL = " insert into agr_mst_tapplication2collateralUpdate_LOG(" +
                    " application2collateral_UpdateLOGgid ," +
                    " application2collateral_gid ," +
                    " application_gid," +
                    " source_type," +
                    " guideline_value," +
                    " guideline_date," +
                    " marketvalue_date ," +
                    " market_value," +
                    " forcedsource_value," +
                    " collateralSSV_value," +
                    " forcedvalueassessed_on," +
                    " collateralobservation_summary," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.application2collateral_gid + "'," +
                    "'" + values.application_gid + "'," +
                    "'" + lssource_type + "'," +
                    "'" + lsguideline_value + "'," +
                    "'" + lsguideline_date + "'," +
                    "'" + lsmarketvalue_date + "'," +
                    "'" + lsmarket_value + "'," +
                   "'" + lsforcedsource_value + "'," +
                   "'" + lscollateralSSV_value + "'," +
                   "'" + lsforcedvalueassessed_on + "'," +
                   "'" + lscollateralobservation_summary + "'," +
                             "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Collateral Details Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaDeleteCollateralDetails(string application2collateral_gid, MdlMstCollatertal values)
        {
            msSQL = "delete from agr_mst_tapplication2collateral where application2collateral_gid='" + application2collateral_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Collateral Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;
            }
        }

        public void DaHypothecationDetailsList(string application_gid, MdlMstHypothecation values)
        {
            msSQL = "select application2hypothecation_gid,securitytype_gid,security_type,security_description,security_value," +
                    " date_format(securityassessed_date,'%d-%m-%Y') as securityassessed_date,asset_id,roc_fillingid,CERSAI_fillingid," +
                    " hypoobservation_summary,primary_security " +
                    " from agr_mst_tapplication2hypothecation where application_gid='" + application_gid + "'";
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

        public void DaHypothecationTempDetailsList(string employee_gid, string application_gid, MdlMstHypothecation values)
        {
            msSQL = "select application2hypothecation_gid,securitytype_gid,security_type,security_description,security_value," +
                    " date_format(securityassessed_date,'%d-%m-%Y') as securityassessed_date,asset_id,roc_fillingid,CERSAI_fillingid," +
                    " hypoobservation_summary,primary_security " +
                    " from agr_mst_tapplication2hypothecation where application_gid='" + employee_gid + "' or application_gid='" + application_gid + "'";
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
            msSQL = " select hypothecationdocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                      " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by,a.document_title " +
                      " from agr_mst_tuploadhypothecationocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
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
                        document_gid = (dr_datarow["hypothecationdocument_gid"].ToString()),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString(),
                        document_title = dr_datarow["document_title"].ToString()
                    });
                }
                values.DocumentList = get_filename;
            }
            dt_datatable.Dispose();
        }

        public void DaHypothecationDetailsEdit(string application_gid, MdlMstHypothecation values, string employee_gid)
        {
            try
            {
                msSQL = " select application2hypothecation_gid, application_gid, securitytype_gid,security_type,security_description,security_value," +
                    " date_format(securityassessed_date, '%Y-%m-%d') as securityassessed_dateedit," +
                    " asset_id,roc_fillingid,CERSAI_fillingid,hypoobservation_summary,primary_security " +
                    " from agr_mst_tapplication2hypothecation where application_gid='" + application_gid + "'";
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
                msSQL = " select application2hypothecation_gid,hypothecationdocument_gid,document_name,document_path,document_title from agr_mst_tuploadhypothecationocument " +
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
                            document_gid = dt["hypothecationdocument_gid"].ToString(),
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
        public void DaUpdateOverallLimit(string employee_gid, MdlProductCharges values)
        {


            msSQL = " update agr_mst_tapplication set " +
                  " overalllimit_amount='" + values.overalllimit_amount.Replace(",", "") + "',";
            //" validityoveralllimit_year='" + values.validityoveralllimit_year + "'," +
            //" validityoveralllimit_month='" + values.validityoveralllimit_month + "'," +
            //" validityoveralllimit_days='" + values.validityoveralllimit_days + "'," +
            if ((values.validityfrom_date == null) || (values.validityfrom_date == ""))
            {
                msSQL += "validityfrom_date=null,";
            }
            else if (Convert.ToDateTime(values.validityfrom_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += " validityfrom_date='" + Convert.ToDateTime(values.validityfrom_date).ToString("yyyy-MM-dd 00:00:00") + "',";
            }
            if ((values.validityto_date == null) || (values.validityto_date == ""))
            {
                msSQL += "validityto_date=null,";
            }
            else if (Convert.ToDateTime(values.validityto_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += " validityto_date='" + Convert.ToDateTime(values.validityto_date).ToString("yyyy-MM-dd 00:00:00") + "',";
            }
            msSQL += " calculationoveralllimit_validity='" + values.calculationoveralllimit_validity + "'," +
                   " productcharge_flag='Y'," +
                  " productcharges_status='Incomplete'," +
             " updated_by='" + employee_gid + "'," +
             " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
             " where application_gid='" + values.application_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Overall Limit Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }


        }
        public void DaGetEditLoanDtl(string application_gid, MdlMstLoanDtl values, string employee_gid)
        {


            msSQL = "select date_format(facilityrequested_date, '%d-%m-%Y') as facilityrequested_date, product_type,facilityrequested_date,scheme_type, " +
                            " productsub_type, loanfacility_amount, loan_type, rate_interest, penal_interest, programoverall_limit, " +
                            " tenureoverall_limit, facility_type, facility_mode, principalfrequency_name, interestfrequency_name,producttype_gid, " +
                            " interest_status, moratorium_type, moratorium_status, date_format(moratorium_startdate, '%d-%m-%Y') as moratorium_startdate,application2loan_gid, " +
                            " date_format(moratorium_enddate, '%d-%m-%Y') as moratorium_enddate,product_gid,variety_gid from agr_mst_tapplication2loan " +
                            " where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstloan_list = new List<mstloan_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstloan_list.Add(new mstloan_list
                    {
                        facilityrequested_date = (dr_datarow["facilityrequested_date"].ToString()),
                        producttype_gid = (dr_datarow["producttype_gid"].ToString()),
                        product_type = (dr_datarow["product_type"].ToString()),
                        productsub_type = (dr_datarow["productsub_type"].ToString()),
                        loanfacility_amount = (dr_datarow["loanfacility_amount"].ToString()),
                        loan_type = (dr_datarow["loan_type"].ToString()),
                        rate_interest = (dr_datarow["rate_interest"].ToString()),
                        penal_interest = (dr_datarow["penal_interest"].ToString()),
                        facilityoverall_limit = (dr_datarow["programoverall_limit"].ToString()),
                        tenureoverall_limit = (dr_datarow["tenureoverall_limit"].ToString()),
                        facility_type = (dr_datarow["facility_type"].ToString()),
                        facility_mode = (dr_datarow["facility_mode"].ToString()),
                        principalfrequency_name = (dr_datarow["principalfrequency_name"].ToString()),
                        interestfrequency_name = (dr_datarow["interestfrequency_name"].ToString()),
                        interest_status = (dr_datarow["interest_status"].ToString()),
                        moratorium_status = (dr_datarow["moratorium_status"].ToString()),
                        moratorium_type = (dr_datarow["moratorium_type"].ToString()),
                        moratorium_startdate = (dr_datarow["moratorium_startdate"].ToString()),
                        moratorium_enddate = (dr_datarow["moratorium_enddate"].ToString()),
                        scheme_type = (dr_datarow["scheme_type"].ToString()),
                        application2loan_gid = (dr_datarow["application2loan_gid"].ToString()),
                        product_gid = (dr_datarow["product_gid"].ToString()),
                        variety_gid = (dr_datarow["variety_gid"].ToString())

                    });
                }
                values.mstloan_list = getmstloan_list;
            }
            dt_datatable.Dispose();
            msSQL = " select application_gid,application2servicecharge_gid, processing_fee,processing_collectiontype,doc_charges," +
                   " doccharge_collectiontype,fieldvisit_charges,fieldvisit_charges_collectiontype,adhoc_fee,adhoc_collectiontype," +
                   " life_insurance,lifeinsurance_collectiontype,acct_insurance,total_collect,total_deduct,product_type," +
                   " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                   " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, acctinsurance_collectiontype" +
                   " from agr_mst_tapplicationservicecharge a " +
                   " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                   " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                     " where a.application_gid = '" + application_gid + "' order by application2servicecharge_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getproductcharges_list = new List<servicecharges_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getproductcharges_list.Add(new servicecharges_list
                    {
                        application2servicecharge_gid = (dr_datarow["application2servicecharge_gid"].ToString()),
                        processing_fee = (dr_datarow["processing_fee"].ToString()),
                        processing_collectiontype = (dr_datarow["processing_collectiontype"].ToString()),
                        doc_charges = (dr_datarow["doc_charges"].ToString()),
                        doccharge_collectiontype = (dr_datarow["doccharge_collectiontype"].ToString()),
                        fieldvisit_charge = (dr_datarow["fieldvisit_charges"].ToString()),
                        fieldvisit_collectiontype = (dr_datarow["fieldvisit_charges_collectiontype"].ToString()),
                        adhoc_fee = (dr_datarow["adhoc_fee"].ToString()),
                        adhoc_collectiontype = (dr_datarow["adhoc_collectiontype"].ToString()),
                        life_insurance = (dr_datarow["life_insurance"].ToString()),
                        lifeinsurance_collectiontype = (dr_datarow["lifeinsurance_collectiontype"].ToString()),
                        acct_insurance = (dr_datarow["acct_insurance"].ToString()),
                        total_collect = (dr_datarow["total_collect"].ToString()),
                        total_deduct = (dr_datarow["total_deduct"].ToString()),
                        product_type = (dr_datarow["product_type"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        acctinsurance_collectiontype = (dr_datarow["acctinsurance_collectiontype"].ToString()),
                    });
                }
                values.servicecharges_list = getproductcharges_list;
            }
            dt_datatable.Dispose();

            values.status = true;
        }
        public void DaPostLoanEditDtl(string employee_gid, MdlMstLoanDtl values)
        {
            //string fsprimaryvaluechain_gid = string.Empty;
            //string fsprimaryvaluechain_name = string.Empty;
            //if (values.primaryvaluechain_list != null)
            //{
            //    for (var i = 0; i < values.primaryvaluechain_list.Count; i++)
            //    {
            //        fsprimaryvaluechain_gid += values.primaryvaluechain_list[i].valuechain_gid + ",";
            //        fsprimaryvaluechain_name += values.primaryvaluechain_list[i].valuechain_name + ",";

            //    }
            //    fsprimaryvaluechain_gid = fsprimaryvaluechain_gid.TrimEnd(',');
            //    fsprimaryvaluechain_name = fsprimaryvaluechain_name.TrimEnd(',');
            //}

            //string fssecondaryvaluechain_gid = string.Empty;
            //string fssecondaryvaluechain_name = string.Empty;
            //if (values.secondaryvaluechain_list != null)
            //{
            //    for (var i = 0; i < values.secondaryvaluechain_list.Count; i++)
            //    {
            //        fssecondaryvaluechain_gid += values.secondaryvaluechain_list[i].valuechain_gid + ",";
            //        fssecondaryvaluechain_name += values.secondaryvaluechain_list[i].valuechain_name + ",";

            //    }
            //    fssecondaryvaluechain_gid = fssecondaryvaluechain_gid.TrimEnd(',');
            //    fssecondaryvaluechain_name = fssecondaryvaluechain_name.TrimEnd(',');
            //}
            //msSQL = "select application_gid from agr_mst_tapplication2loan where producttype_gid='" + values.producttype_gid + "' and " +
            //   " productsubtype_gid='" + values.productsubtype_gid + "' and application_gid='" + values.application_gid + "'";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);
            //if (objODBCDatareader.HasRows == false)
            //{
            //    objODBCDatareader.Close();
            if (values.product_type == "Agri Receivable Finance (ARF)")
            {
                msSQL = "select application2loan_gid from agr_mst_tapplication2buyer  where application2loan_gid='" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    values.message = "Kindly add atleast one Buyer";
                    values.status = false;
                    return;
                }
                else
                {
                    msGetGid = objcmnfunctions.GetMasterGID("AP2L");
                    msSQL = " insert into agr_mst_tapplication2loan(" +
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
                            " programlimit_validdfrom," +
                            " programlimit_validdfrom," +
                            " programoverall_limit," +
                            " tenureproduct_year," +
                            " tenureproduct_month," +
                            " tenureproduct_days," +
                            " tenureoverall_limit ," +
                            " facility_type," +
                            " facility_mode," +
                            " principalfrequency_name," +
                            " principalfrequency_gid," +
                            " interestfrequency_name," +
                            " interestfrequency_gid," +
                            " program_gid," +
                            " program," +
                            //" primaryvaluechain_gid," +
                            //" primaryvaluechain_name," +
                            //" secondaryvaluechain_gid," +
                            //" secondaryvaluechain_name," +
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
                            " trade_orginatedby, " +
                            " SA_Brokerage, " +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + values.application_gid + "'," +
                            "'" + Convert.ToDateTime(values.facilityrequested_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'" + values.product_type + "'," +
                            "'" + values.producttype_gid + "'," +
                            "'" + values.productsub_type + "'," +
                            "'" + values.productsubtype_gid + "'," +
                            "'" + values.loantype_gid + "'," +
                            "'" + values.loan_type + "'," +
                            "'" + values.facilityloan_amount + "'," +
                            "'" + values.rate_interest + "'," +
                            "'" + values.penal_interest + "',";
                    if (values.programlimit_validdfrom == null || values.programlimit_validdfrom == "")
                        msSQL += "null,";
                    else
                        msSQL += "'" + Convert.ToDateTime(values.programlimit_validdfrom).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    if (values.programlimit_validdto == null || values.programlimit_validdto == "")
                        msSQL += "null,";
                    else
                        msSQL += "'" + Convert.ToDateTime(values.programlimit_validdto).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    msSQL += "'" + values.programoverall_limit + "'," +
                                "'" + values.tenureproduct_year + "'," +
                            "'" + values.tenureproduct_month + "'," +
                            "'" + values.tenureproduct_days + "'," +
                            "'" + values.tenureoverall_limit + "'," +
                            "'" + values.facility_type + "'," +
                            "'" + values.facility_mode + "'," +
                             "'" + values.principalfrequency_name + "'," +
                            "'" + values.principalfrequency_gid + "'," +
                            "'" + values.interestfrequency_name + "'," +
                            "'" + values.interestfrequency_gid + "'," +
                            "'" + values.program_gid + "'," +
                            "'" + values.program + "'," +
                            //"'" + fsprimaryvaluechain_gid + "'," +
                            //"'" + fsprimaryvaluechain_name + "'," +
                            //"'" + fssecondaryvaluechain_gid + "'," +
                            //"'" + fssecondaryvaluechain_name + "'," +
                            "'" + values.interest_status + "'," +
                            "'" + values.moratorium_status + "'," +
                            "'" + values.moratorium_type + "',";
                    if (values.moratorium_startdate == null || values.moratorium_startdate == "")
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(values.moratorium_startdate).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }
                    if (values.moratorium_enddate == null || values.moratorium_enddate == "")
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(values.moratorium_enddate).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }
                    msSQL += "'" + values.source_type + "',";
                    if (values.guideline_value == null || values.guideline_value == "")
                    {
                        msSQL += "'0.00',";
                    }
                    else
                    {
                        msSQL += "'" + values.guideline_value.Replace(",", "") + "',";
                    }
                    if (values.guideline_date == null || values.guideline_date == "")
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(values.guideline_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }
                    if (values.marketvalue_date == null || values.marketvalue_date == "")
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(values.marketvalue_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }
                    if (values.market_value == null || values.market_value == "")
                    {
                        msSQL += "'0.00',";
                    }
                    else
                    {
                        msSQL += "'" + values.market_value.Replace(",", "") + "',";
                    }
                    if (values.forcedsource_value == null || values.forcedsource_value == "")
                    {
                        msSQL += "'0.00',";
                    }
                    else
                    {
                        msSQL += "'" + values.forcedsource_value.Replace(",", "") + "',";
                    }
                    if (values.collateralSSV_value == null || values.collateralSSV_value == "")
                    {
                        msSQL += "'0.00',";
                    }
                    else
                    {
                        msSQL += "'" + values.collateralSSV_value.Replace(",", "") + "',";
                    }
                    if (values.forcedvalueassessed_on == null || values.forcedvalueassessed_on == "")
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(values.forcedvalueassessed_on).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }
                    if (values.collateralobservation_summary == null || values.collateralobservation_summary == "")
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + values.collateralobservation_summary.Replace("'", "") + "',";
                    }
                    if (values.enduse_purpose == null || values.enduse_purpose == "")
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + values.enduse_purpose.Replace("'", "") + "',";
                    }
                    msSQL += "'" + values.product_gid + "'," +
                             "'" + values.product_name + "'," +
                             "'" + values.variety_gid + "'," +
                             "'" + values.variety_name + "'," +
                             "'" + values.sector_name + "'," +
                             "'" + values.category_name + "'," +
                             "'" + values.botanical_name + "'," +
                             "'" + values.alternative_name + "'," +
                             "'" + values.trade_orginatedby + "'," +
                             "'" + values.SA_Brokerage + "'," +
                             "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {

                        msSQL = "update agr_mst_tapplication2product set application2loan_gid ='" + msGetGid + "' where application2loan_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_mst_tapploan2supplierdtl set application2loan_gid='" + msGetGid + "' where application2loan_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_mst_tapploan2paymenttypecustomer set application2loan_gid='" + msGetGid + "' where application2loan_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_mst_tapploan2supplierpayment set application2loan_gid='" + msGetGid + "' where application2loan_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                        msSQL = "update agr_mst_tapplication2buyer set application2loan_gid='" + msGetGid + "' where application2loan_gid='" + employee_gid + "'";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        msSQL = "update agr_mst_tapplication set productcharges_status='Completed',productcharge_flag='Y' where application_gid = '" + values.application_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        values.status = true;


                        values.message = "Loan details Added successfully";
                        msSQL = " select date_format(facilityrequested_date, '%d-%m-%Y') as facilityrequested_date, product_type,facilityrequested_date,scheme_type, " +
                                " productsub_type, loanfacility_amount, loan_type, rate_interest, penal_interest, programoverall_limit, " +
                                " tenureoverall_limit, facility_type, facility_mode, principalfrequency_name, interestfrequency_name,producttype_gid, " +
                                " interest_status, moratorium_type, moratorium_status, date_format(moratorium_startdate, '%d-%m-%Y') as moratorium_startdate,application2loan_gid, " +
                                " date_format(moratorium_enddate, '%d-%m-%Y') as moratorium_enddate,product_gid,product_name,variety_gid,variety_name,  " +
                                " sector_name, category_name, botanical_name, alternative_name from agr_mst_tapplication2loan " +
                                " where application_gid='" + values.application_gid + "'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        var getmstloan_list = new List<mstloan_list>();
                        if (dt_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow dr_datarow in dt_datatable.Rows)
                            {
                                getmstloan_list.Add(new mstloan_list
                                {
                                    facilityrequested_date = (dr_datarow["facilityrequested_date"].ToString()),
                                    producttype_gid = (dr_datarow["producttype_gid"].ToString()),
                                    product_type = (dr_datarow["product_type"].ToString()),
                                    productsub_type = (dr_datarow["productsub_type"].ToString()),
                                    loanfacility_amount = (dr_datarow["loanfacility_amount"].ToString()),
                                    loan_type = (dr_datarow["loan_type"].ToString()),
                                    rate_interest = (dr_datarow["rate_interest"].ToString()),
                                    penal_interest = (dr_datarow["penal_interest"].ToString()),
                                    facilityoverall_limit = (dr_datarow["programoverall_limit"].ToString()),
                                    tenureoverall_limit = (dr_datarow["tenureoverall_limit"].ToString()),
                                    facility_type = (dr_datarow["facility_type"].ToString()),
                                    facility_mode = (dr_datarow["facility_mode"].ToString()),
                                    principalfrequency_name = (dr_datarow["principalfrequency_name"].ToString()),
                                    interestfrequency_name = (dr_datarow["interestfrequency_name"].ToString()),
                                    interest_status = (dr_datarow["interest_status"].ToString()),
                                    moratorium_status = (dr_datarow["moratorium_status"].ToString()),
                                    moratorium_type = (dr_datarow["moratorium_type"].ToString()),
                                    moratorium_startdate = (dr_datarow["moratorium_startdate"].ToString()),
                                    moratorium_enddate = (dr_datarow["moratorium_enddate"].ToString()),
                                    scheme_type = (dr_datarow["scheme_type"].ToString()),
                                    application2loan_gid = (dr_datarow["application2loan_gid"].ToString()),
                                    product_gid = (dr_datarow["product_gid"].ToString()),
                                    product_name = (dr_datarow["product_name"].ToString()),
                                    variety_gid = (dr_datarow["variety_gid"].ToString()),
                                    variety_name = (dr_datarow["variety_name"].ToString()),
                                    sector_name = (dr_datarow["sector_name"].ToString()),
                                    category_name = (dr_datarow["category_name"].ToString()),
                                    botanical_name = (dr_datarow["botanical_name"].ToString()),
                                    alternative_name = (dr_datarow["alternative_name"].ToString()),

                                });
                            }
                            values.mstloan_list = getmstloan_list;
                        }
                        dt_datatable.Dispose();

                        msSQL = "update agr_mst_tuploadcollateraldocument set application2loan_gid='" + msGetGid + "' where application2loan_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " select collateraldocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                                            " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by,a.document_title " +
                                            " from agr_mst_tuploadcollateraldocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                                            " and b.user_gid = c.user_gid and application2loan_gid='" + msGetGid + "'";

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
                                    document_gid = (dr_datarow["collateraldocument_gid"].ToString()),
                                    uploaded_by = dr_datarow["uploaded_by"].ToString(),
                                    updated_date = dr_datarow["uploaded_date"].ToString(),
                                    document_title = dr_datarow["document_title"].ToString()
                                });
                            }
                            values.DocumentList = get_filename;
                        }
                        dt_datatable.Dispose();
                        msSQL = "select application_gid from agr_mst_tapplication2loan where product_type='Agri Receivable Finance (ARF)' and " +
                             " (application_gid='" + employee_gid + "' or  application_gid='" + values.application_gid + "' )";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            values.buyer_status = "Y";
                        }
                        objODBCDatareader.Close();

                        msSQL = "select application_gid from agr_mst_tapplication2loan where loan_type='Secured' and " +
                                " (application_gid='" + employee_gid + "' or  application_gid='" + values.application_gid + "' )";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            values.collateral_status = "Y";
                        }
                        objODBCDatareader.Close();

                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured while Adding Loan";
                    }
                }
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("AP2L");
                msSQL = " insert into agr_mst_tapplication2loan(" +
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
                        " programlimit_validdfrom," +
                       " programlimit_validdto," +
                       " programoverall_limit ," +
                        " tenureproduct_year," +
                        " tenureproduct_month," +
                        " tenureproduct_days," +
                        " tenureoverall_limit ," +
                        " facility_type," +
                        " facility_mode," +
                        " principalfrequency_name," +
                        " principalfrequency_gid," +
                        " interestfrequency_name," +
                        " interestfrequency_gid," +
                        " program_gid," +
                        " program," +
                        //" primaryvaluechain_gid," +
                        //" primaryvaluechain_name," +
                        //" secondaryvaluechain_gid," +
                        //" secondaryvaluechain_name," +
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
                        " trade_orginatedby, " +
                        " SA_Brokerage, " +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.application_gid + "'," +
                        "'" + Convert.ToDateTime(values.facilityrequested_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + values.product_type + "'," +
                        "'" + values.producttype_gid + "'," +
                        "'" + values.productsub_type + "'," +
                        "'" + values.productsubtype_gid + "'," +
                        "'" + values.loantype_gid + "'," +
                        "'" + values.loan_type + "'," +
                        "'" + values.facilityloan_amount + "'," +
                        "'" + values.rate_interest + "'," +
                        "'" + values.penal_interest + "',";
                if (values.programlimit_validdfrom == null || values.programlimit_validdfrom == "")
                    msSQL += "null,";
                else
                    msSQL += "'" + Convert.ToDateTime(values.programlimit_validdfrom).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                if (values.programlimit_validdto == null || values.programlimit_validdto == "")
                    msSQL += "null,";
                else
                    msSQL += "'" + Convert.ToDateTime(values.programlimit_validdto).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                msSQL += "'" + values.programoverall_limit + "'," +
                        "'" + values.tenureproduct_year + "'," +
                        "'" + values.tenureproduct_month + "'," +
                        "'" + values.tenureproduct_days + "'," +
                        "'" + values.tenureoverall_limit + "'," +
                        "'" + values.facility_type + "'," +
                        "'" + values.facility_mode + "'," +
                        "'" + values.principalfrequency_name + "'," +
                        "'" + values.principalfrequency_gid + "'," +
                        "'" + values.interestfrequency_name + "'," +
                        "'" + values.interestfrequency_gid + "'," +
                        "'" + values.program_gid + "'," +
                        "'" + values.program + "'," +
                        //"'" + fsprimaryvaluechain_gid + "'," +
                        //"'" + fsprimaryvaluechain_name + "'," +
                        //"'" + fssecondaryvaluechain_gid + "'," +
                        //"'" + fssecondaryvaluechain_name + "'," +
                        "'" + values.interest_status + "'," +
                        "'" + values.moratorium_status + "'," +
                        "'" + values.moratorium_type + "',";
                if (values.moratorium_startdate == null || values.moratorium_startdate == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.moratorium_startdate).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                if (values.moratorium_enddate == null || values.moratorium_enddate == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.moratorium_enddate).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                msSQL += "'" + values.source_type + "',";
                if (values.guideline_value == null || values.guideline_value == "")
                {
                    msSQL += "'0.00',";
                }
                else
                {
                    msSQL += "'" + values.guideline_value.Replace(",", "") + "',";
                }
                if (values.guideline_date == null || values.guideline_date == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.guideline_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                if (values.marketvalue_date == null || values.marketvalue_date == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.marketvalue_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                if (values.market_value == null || values.market_value == "")
                {
                    msSQL += "'0.00',";
                }
                else
                {
                    msSQL += "'" + values.market_value.Replace(",", "") + "',";
                }
                if (values.forcedsource_value == null || values.forcedsource_value == "")
                {
                    msSQL += "'0.00',";
                }
                else
                {
                    msSQL += "'" + values.forcedsource_value.Replace(",", "") + "',";
                }
                if (values.collateralSSV_value == null || values.collateralSSV_value == "")
                {
                    msSQL += "'0.00',";
                }
                else
                {
                    msSQL += "'" + values.collateralSSV_value.Replace(",", "") + "',";
                }
                if (values.forcedvalueassessed_on == null || values.forcedvalueassessed_on == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.forcedvalueassessed_on).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                if (values.collateralobservation_summary == null || values.collateralobservation_summary == "")
                {
                    msSQL += "'',";
                }
                else
                {
                    msSQL += "'" + values.collateralobservation_summary.Replace("'", "") + "',";
                }
                if (values.enduse_purpose == null || values.enduse_purpose == "")
                {
                    msSQL += "'',";
                }
                else
                {
                    msSQL += "'" + values.enduse_purpose.Replace("'", "") + "',";
                }
                msSQL += "'" + values.product_gid + "'," +
                         "'" + values.product_name + "'," +
                         "'" + values.variety_gid + "'," +
                         "'" + values.variety_name + "'," +
                         "'" + values.sector_name + "'," +
                         "'" + values.category_name + "'," +
                         "'" + values.botanical_name + "'," +
                         "'" + values.alternative_name + "'," +
                         "'" + values.trade_orginatedby + "'," +
                         "'" + values.SA_Brokerage + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = "update agr_mst_tapplication2product set application2loan_gid ='" + msGetGid + "' where application2loan_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    msSQL = "update agr_mst_tapploan2supplierdtl set application2loan_gid='" + msGetGid + "' where application2loan_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tapploan2paymenttypecustomer set application2loan_gid='" + msGetGid + "' where application2loan_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tapploan2supplierpayment set application2loan_gid='" + msGetGid + "' where application2loan_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tapplication2buyer set application2loan_gid='" + msGetGid + "' where application2loan_gid='" + employee_gid + "'";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    msSQL = "update agr_mst_tapplication set productcharges_status='Completed',productcharge_flag='Y' where application_gid = '" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;


                    values.message = "Loan details Added successfully";
                    msSQL = "select date_format(facilityrequested_date, '%d-%m-%Y') as facilityrequested_date, product_type,facilityrequested_date,scheme_type, " +
                              " productsub_type, loanfacility_amount, loan_type, rate_interest, penal_interest, programoverall_limit, " +
                              " tenureoverall_limit, facility_type, facility_mode, principalfrequency_name, interestfrequency_name,producttype_gid, " +
                              " interest_status, moratorium_type, moratorium_status, date_format(moratorium_startdate, '%d-%m-%Y') as moratorium_startdate,application2loan_gid, " +
                              " date_format(moratorium_enddate, '%d-%m-%Y') as moratorium_enddate,product_gid,product_name, " +
                              " variety_gid,variety_name,sector_name,category_name,botanical_name,alternative_name from agr_mst_tapplication2loan " +
                              " where application_gid='" + values.application_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getmstloan_list = new List<mstloan_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getmstloan_list.Add(new mstloan_list
                            {
                                facilityrequested_date = (dr_datarow["facilityrequested_date"].ToString()),
                                producttype_gid = (dr_datarow["producttype_gid"].ToString()),
                                product_type = (dr_datarow["product_type"].ToString()),
                                productsub_type = (dr_datarow["productsub_type"].ToString()),
                                loanfacility_amount = (dr_datarow["loanfacility_amount"].ToString()),
                                loan_type = (dr_datarow["loan_type"].ToString()),
                                rate_interest = (dr_datarow["rate_interest"].ToString()),
                                penal_interest = (dr_datarow["penal_interest"].ToString()),
                                facilityoverall_limit = (dr_datarow["programoverall_limit"].ToString()),
                                tenureoverall_limit = (dr_datarow["tenureoverall_limit"].ToString()),
                                facility_type = (dr_datarow["facility_type"].ToString()),
                                facility_mode = (dr_datarow["facility_mode"].ToString()),
                                principalfrequency_name = (dr_datarow["principalfrequency_name"].ToString()),
                                interestfrequency_name = (dr_datarow["interestfrequency_name"].ToString()),
                                interest_status = (dr_datarow["interest_status"].ToString()),
                                moratorium_status = (dr_datarow["moratorium_status"].ToString()),
                                moratorium_type = (dr_datarow["moratorium_type"].ToString()),
                                moratorium_startdate = (dr_datarow["moratorium_startdate"].ToString()),
                                moratorium_enddate = (dr_datarow["moratorium_enddate"].ToString()),
                                scheme_type = (dr_datarow["scheme_type"].ToString()),
                                application2loan_gid = (dr_datarow["application2loan_gid"].ToString()),
                                product_gid = (dr_datarow["product_gid"].ToString()),
                                product_name = (dr_datarow["product_name"].ToString()),
                                variety_gid = (dr_datarow["variety_gid"].ToString()),
                                variety_name = (dr_datarow["variety_name"].ToString()),
                                sector_name = (dr_datarow["sector_name"].ToString()),
                                category_name = (dr_datarow["category_name"].ToString()),
                                botanical_name = (dr_datarow["botanical_name"].ToString()),
                                alternative_name = (dr_datarow["alternative_name"].ToString()),

                            });
                        }
                        values.mstloan_list = getmstloan_list;
                    }
                    dt_datatable.Dispose();

                    msSQL = "update agr_mst_tuploadcollateraldocument set application2loan_gid='" + msGetGid + "' where application2loan_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " select collateraldocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by,a.document_title " +
                                        " from agr_mst_tuploadcollateraldocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                                        " and b.user_gid = c.user_gid and application2loan_gid='" + msGetGid + "'";

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
                                document_gid = (dr_datarow["collateraldocument_gid"].ToString()),
                                uploaded_by = dr_datarow["uploaded_by"].ToString(),
                                updated_date = dr_datarow["uploaded_date"].ToString(),
                                document_title = dr_datarow["document_title"].ToString()
                            });
                        }
                        values.DocumentList = get_filename;
                    }
                    dt_datatable.Dispose();
                    msSQL = "select application_gid from agr_mst_tapplication2loan where product_type='Agri Receivable Finance (ARF)' and " +
                        "(application_gid = '" + employee_gid + "' or  application_gid = '" + values.application_gid + "')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.buyer_status = "Y";
                    }
                    objODBCDatareader.Close();

                    msSQL = "select application_gid from agr_mst_tapplication2loan where loan_type='Secured' and " +
                        "(application_gid = '" + employee_gid + "' or  application_gid = '" + values.application_gid + "')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.collateral_status = "Y";
                    }
                    objODBCDatareader.Close();

                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured while Adding Loan";
                }
            }
            if (values.productsub_type == "STF")
            {
                if (values.holding_periods == null)
                    values.holding_periods = "";
                if (values.holdingmonthly_procurement == null)
                    values.holdingmonthly_procurement = "";
                if (values.extendedholding_periods == null)
                    values.extendedholding_periods = "";
                if (values.extendedmonthly_procurement == null)
                    values.extendedmonthly_procurement = "";
                if (values.reimburesementof_expenses == null)
                    values.reimburesementof_expenses = "";
                if (values.bankfundingdata_filename == null)
                    values.bankfundingdata_filename = "";
                if (values.bankfundingdata_filepath == null)
                    values.bankfundingdata_filepath = "";
                if (values.needfor_stocking == null)
                    values.needfor_stocking = "";
                if (values.product_portfolio == null)
                    values.product_portfolio = "";
                if (values.production_capacity == null)
                    values.production_capacity = "";
                if (values.natureof_operations == null)
                    values.natureof_operations = "";
                if (values.averagemonthly_inventoryholding == null)
                    values.averagemonthly_inventoryholding = "";
                if (values.financialinstitutions_relationship == null)
                    values.financialinstitutions_relationship = "";
                if (values.reimburesementof_expensespenalty == null)
                    values.reimburesementof_expensespenalty = "";

                msSQL = " update agr_mst_tapplication2loan set " +
                        " holding_periods='" + values.holding_periods.Replace("'", "") + "'," +
                        " holdingmonthly_procurement='" + values.holdingmonthly_procurement.Replace("'", "") + "'," +
                        " extendedholding_periods='" + values.extendedholding_periods.Replace("'", "") + "'," +
                        " extendedmonthly_procurement='" + values.extendedmonthly_procurement.Replace("'", "") + "'," +
                        " charges_extendedperiod='" + values.charges_extendedperiod.Replace("'", "") + "'," +
                        " customer_advance='" + values.customer_advance.Replace("'", "") + "'," +
                        " reimburesementof_expenses='" + values.reimburesementof_expenses.Replace("'", "") + "'," +
                        " reimburesementof_expensespenalty='" + values.reimburesementof_expensespenalty.Replace("'", "") + "'," +
                        " bankfundingdata_filename='" + values.bankfundingdata_filename + "'," +
                        " bankfundingdata_filepath='" + values.bankfundingdata_filepath + "'" +
                        " needfor_stocking='" + values.needfor_stocking.Replace("'", "") + "'," +
                        " product_portfolio='" + values.product_portfolio.Replace("'", "") + "'" +
                        " production_capacity='" + values.production_capacity.Replace("'", "") + "'," +
                        " natureof_operations='" + values.natureof_operations.Replace("'", "") + "'" +
                        " averagemonthly_inventoryholding='" + values.averagemonthly_inventoryholding.Replace("'", "") + "'" +
                        " financialinstitutions_relationship='" + values.financialinstitutions_relationship.Replace("'", "") + "'" +
                        " where application2loan_gid='" + msGetGid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    msSQL = " delete from agr_tmp_tbankfundingdataupload where  application_gid='" + values.application_gid + "' " +
                            " and application2loan_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            //}
            //else
            //{
            //    objODBCDatareader.Close();
            //    values.status = false;
            //    values.message = "Already this Pragram Name added.";
            //}
        }
        public void DaGetEditLimit(string application_gid, MdlMstApplicationAdd values, string employee_gid)
        {

            msSQL = "select onboarding_status from agr_mst_tapplication where application_gid='" + application_gid + "'";
            values.onboarding_status = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select format(overalllimit_amount,2,'en_IN') from agr_mst_tapplication where application_gid='" + application_gid + "'";
            values.overalllimit_amount = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select sum(loanfacility_amount) from agr_mst_tapplication2loan where application_gid='" + employee_gid + "'" +
                " or application_gid='" + application_gid + "'";
            values.loanfacility_amount = objdbconn.GetExecuteScalar(msSQL);

        }
        public void GetEditLoanLimit(MdlMstLoanDtl values, string employee_gid)
        {
            msSQL = "select onboarding_status from agr_mst_tapplication where application_gid='" + values.application_gid + "'";
            values.onboarding_status = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select format(overalllimit_amount,2,'en_IN') from agr_mst_tapplication where application_gid='" + values.application_gid + "'";
            values.overalllimit_amount = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select sum(loanfacility_amount) from agr_mst_tapplication2loan where ( application_gid='" + employee_gid + "'" +
                " or application_gid='" + values.application_gid + "') and application2loan_gid <>'" + values.application2loan_gid + "'";
            values.loanfacility_amount = objdbconn.GetExecuteScalar(msSQL);
            values.status = true;
        }
        public void DaGetEditproduct(string application_gid, MdlList values, string employee_gid)
        {

            msSQL = "select producttype_gid,product_type from agr_mst_tapplication2loan where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getSegment = new List<product_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getSegment.Add(new product_list
                    {
                        producttype_gid = (dr_datarow["producttype_gid"].ToString()),
                        product_type = (dr_datarow["product_type"].ToString()),
                    });
                }
                values.product_list = getSegment;
            }
            dt_datatable.Dispose();
            values.status = true;

        }
        public void DaHypothecationDetailsUpdate(string employee_gid, MdlMstHypothecation values)
        {
            msSQL = " select application2hypothecation_gid, application_gid, securitytype_gid,security_type,security_description,security_value,securityassessed_date," +
                    " asset_id,roc_fillingid,CERSAI_fillingid,hypoobservation_summary,primary_security " +
                    " from agr_mst_tapplication2hypothecation where application2hypothecation_gid='" + values.application2hypothecation_gid + "'";
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
                msSQL = " update agr_mst_tapplication2hypothecation set " +
                        " securitytype_gid='" + values.securitytype_gid + "'," +
                //if (values.security_type == null || values.security_type == "")
                //{
                //    msSQL += " security_type='',";
                //}
                //else
                //{
                //    msSQL += " security_type='" + values.security_type.Replace("'", " ") + "',";
                //}
                " security_type='" + values.security_type + "',";
                if (values.security_description == null || values.security_description == "")
                {

                }
                else
                {
                    msSQL += " security_description='" + values.security_description.Replace("'", " ") + "',";
                }
                //msSQL += " security_value='" + values.security_value + "',";
                if (values.security_value == null || values.security_value == "")
                {
                    msSQL += " security_value=null,";
                }
                else
                {
                    msSQL += " security_value='" + values.security_value + "',";
                }


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
                    msSQL = "update agr_mst_tuploadhypothecationocument set application2hypothecation_gid='" + values.application2hypothecation_gid + "'" +
                       " where application2hypothecation_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msGetGid = objcmnfunctions.GetMasterGID("A2HU");
                    msSQL = " insert into agr_mst_tapplication2hypothecationUpdateLOG(" +
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
        public void DaGetEditHypothecation(string application_gid, string employee_gid, MdlMstHypothecation values)
        {

            msSQL = "select application2hypothecation_gid,securitytype_gid,security_type,security_description,security_value," +
                    " date_format(securityassessed_date,'%d-%m-%Y') as securityassessed_date,asset_id,roc_fillingid,CERSAI_fillingid," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " hypoobservation_summary,primary_security " +
                    " from agr_mst_tapplication2hypothecation a " +
                     " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid where application_gid='" + application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.application2hypothecation_gid = objODBCDatareader["application2hypothecation_gid"].ToString();
                values.securitytype_gid = objODBCDatareader["securitytype_gid"].ToString();
                values.security_type = objODBCDatareader["security_type"].ToString();
                values.security_description = objODBCDatareader["security_description"].ToString();
                values.security_value = objODBCDatareader["security_value"].ToString();
                values.securityassessed_date = objODBCDatareader["securityassessed_date"].ToString();
                values.asset_id = objODBCDatareader["asset_id"].ToString();
                values.roc_fillingid = objODBCDatareader["roc_fillingid"].ToString();
                values.CERSAI_fillingid = objODBCDatareader["CERSAI_fillingid"].ToString();
                values.hypoobservation_summary = objODBCDatareader["hypoobservation_summary"].ToString();
                values.primary_security = objODBCDatareader["primary_security"].ToString();
                values.created_by = objODBCDatareader["created_by"].ToString();
                values.created_date = objODBCDatareader["created_date"].ToString();
            }
            values.status = true;
            values.message = "success";
            objODBCDatareader.Close();
        }

        public void DaGetEditHypoDoc(string application2hypothecation_gid, string employee_gid, MdlMstHypothecation values)
        {
            msSQL = " select hypothecationdocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                      " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by,a.document_title " +
                      " from agr_mst_tuploadhypothecationocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                      " and b.user_gid = c.user_gid and application2hypothecation_gid='" + employee_gid + "' or" +
                      " application2hypothecation_gid='" + application2hypothecation_gid + "'";

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
                        document_gid = (dr_datarow["hypothecationdocument_gid"].ToString()),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString(),
                        document_title = dr_datarow["document_title"].ToString()
                    });
                }
                values.DocumentList = get_filename;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        public void DaDeleteHypothecationDetails(string application2hypothecation_gid, MdlMstHypothecation values)
        {
            msSQL = "delete from agr_mst_tapplication2hypothecation where application2hypothecation_gid='" + application2hypothecation_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Hypothecation Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;
            }
        }

        public void DaUpdateProductCharges(string employee_gid, MdlProductCharges values)
        {
            msSQL = " select application_gid, overalllimit_amount, validityoveralllimit_year, validityoveralllimit_month, validityoveralllimit_days, calculationoveralllimit_validity," +
                   " enduse_purpose, processing_fee, processing_collectiontype, doc_charges, doccharge_collectiontype, fieldvisit_charge, fieldvisit_collectiontype, " +
                  " adhoc_fee, adhoc_collectiontype, life_insurance, lifeinsurance_collectiontype, acct_insurance, total_collect, total_deduct " +
                   " from agr_mst_tapplication where application_gid='" + values.application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsapplication_gid = objODBCDatareader["application_gid"].ToString();
                lsoveralllimit_amount = objODBCDatareader["overalllimit_amount"].ToString();
                lsvalidityoveralllimit_year = objODBCDatareader["validityoveralllimit_year"].ToString();
                lsvalidityoveralllimit_month = objODBCDatareader["validityoveralllimit_month"].ToString();
                lsvalidityoveralllimit_days = objODBCDatareader["validityoveralllimit_days"].ToString();
                lscalculationoveralllimit_validity = objODBCDatareader["calculationoveralllimit_validity"].ToString();
                lsenduse_purpose = objODBCDatareader["enduse_purpose"].ToString();
                lsprocessing_fee = objODBCDatareader["processing_fee"].ToString();
                lsprocessing_collectiontype = objODBCDatareader["processing_collectiontype"].ToString();
                lsdoc_charges = objODBCDatareader["doc_charges"].ToString();
                lsdoccharge_collectiontype = objODBCDatareader["doccharge_collectiontype"].ToString();
                lsfieldvisit_charge = objODBCDatareader["fieldvisit_charge"].ToString();
                lsfieldvisit_collectiontype = objODBCDatareader["fieldvisit_collectiontype"].ToString();
                lsadhoc_fee = objODBCDatareader["adhoc_fee"].ToString();
                lsadhoc_collectiontype = objODBCDatareader["adhoc_collectiontype"].ToString();
                lslife_insurance = objODBCDatareader["life_insurance"].ToString();
                lslifeinsurance_collectiontype = objODBCDatareader["lifeinsurance_collectiontype"].ToString();
                lsacct_insurance = objODBCDatareader["acct_insurance"].ToString();
                lstotal_collect = objODBCDatareader["total_collect"].ToString();
                lstotal_deduct = objODBCDatareader["total_deduct"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tapplication set " +
                      " overalllimit_amount='" + values.overalllimit_amount + "'," +
                      " validityoveralllimit_year='" + values.validityoveralllimit_year + "'," +
                      " validityoveralllimit_month='" + values.validityoveralllimit_month + "'," +
                      " validityoveralllimit_days='" + values.validityoveralllimit_days + "'," +
                      " calculationoveralllimit_validity='" + values.calculationoveralllimit_validity + "'," +
                      " enduse_purpose='" + values.enduse_purpose + "'," +
                       " processing_fee='" + values.processing_fee + "'," +
                       " processing_collectiontype='" + values.processing_collectiontype + "'," +
                       " doc_charges='" + values.doc_charges + "'," +
                       " doccharge_collectiontype='" + values.doccharge_collectiontype + "'," +
                       " fieldvisit_charge='" + values.fieldvisit_charge + "'," +
                       " fieldvisit_collectiontype='" + values.fieldvisit_collectiontype + "'," +
                       " adhoc_fee='" + values.adhoc_fee + "'," +
                       " adhoc_collectiontype='" + values.adhoc_collectiontype + "'," +
                       " life_insurance='" + values.life_insurance + "'," +
                       " lifeinsurance_collectiontype='" + values.lifeinsurance_collectiontype + "'," +
                       " acct_insurance='" + values.acct_insurance + "'," +
                       " total_collect='" + values.total_collect + "'," +
                       " total_deduct='" + values.total_deduct + "'," +
                       " updated_by='" + employee_gid + "'," +
                       " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                       " where application_gid='" + values.application_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msSQL = " update agr_mst_tapplication2loan set application_gid='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    //msSQL = " update agr_mst_tapplication2buyer set application_gid='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                    //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_mst_tapplication2collateral set application_gid='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_mst_tapplication2hypothecation set application_gid='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msGetGid = objcmnfunctions.GetMasterGID("APUL");
                    msSQL = " insert into agr_mst_tapplicationUpdateLOG(" +
                          " application_UpdateLOGgid," +
                           " application_gid," +
                           " overalllimit_amount," +
                           " validityoveralllimit_year," +
                           " validityoveralllimit_month," +
                           " validityoveralllimit_days," +
                           " calculationoveralllimit_validity," +
                           " enduse_purpose," +
                            " processing_fee," +
                            " processing_collectiontype," +
                            " doc_charges," +
                            " doccharge_collectiontype," +
                            " fieldvisit_charge," +
                            " fieldvisit_collectiontype," +
                            " adhoc_fee," +
                            " adhoc_collectiontype," +
                            " life_insurance," +
                            " lifeinsurance_collectiontype," +
                            " acct_insurance," +
                            " total_collect," +
                            " total_deduct," +
                            " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid + "'," +
                       "'" + values.application_gid + "'," +
                       "'" + lsoveralllimit_amount + "'," +
                       "'" + lsvalidityoveralllimit_year + "'," +
                       "'" + lsvalidityoveralllimit_month + "'," +
                       "'" + lsvalidityoveralllimit_days + "'," +
                       "'" + lscalculationoveralllimit_validity + "'," +
                       "'" + lsenduse_purpose + "'," +
                       "'" + lsprocessing_fee + "'," +
                       "'" + lsprocessing_collectiontype + "'," +
                       "'" + lsdoc_charges + "'," +
                       "'" + lsdoccharge_collectiontype + "'," +
                       "'" + lsfieldvisit_charge + "'," +
                       "'" + lsfieldvisit_collectiontype + "'," +
                       "'" + lsadhoc_fee + "'," +
                       "'" + lsadhoc_collectiontype + "'," +
                       "'" + lslife_insurance + "'," +
                       "'" + lslifeinsurance_collectiontype + "'," +
                       "'" + lsacct_insurance + "'," +
                       "'" + lstotal_collect + "'," +
                       "'" + lstotal_deduct + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Product&Charges Details Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaGetProductChargesTempClear(string employee_gid, result values)
        {
            msSQL = "delete from agr_mst_tapplication2buyer where application2loan_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tapplication2loan where application_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tapplication2collateral where application_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tapplication2hypothecation where application_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tuploadcollateraldocument where application2loan_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tuploadhypothecationocument where application2hypothecation_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from agr_mst_tappproduct2commoditygststatus where application2product_gid in " +
                  " (select application2product_gid from agr_mst_tapplication2product where application2loan_gid='" + employee_gid + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from agr_mst_tappproduct2commoditytradedtl where application2product_gid in " +
                    " (select application2product_gid from agr_mst_tapplication2product  where application2loan_gid='" + employee_gid + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from agr_mst_tappproduct2commoditydocument where application2product_gid in " +
                     " (select application2product_gid from agr_mst_tapplication2product  where application2loan_gid='" + employee_gid + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from agr_mst_tapplication2product  where application2loan_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from agr_tmp_tbankfundingdataupload where application2loan_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from agr_mst_tapploan2paymenttypecustomer where application2loan_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from agr_mst_tapploan2supplierdtl  where application2loan_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from agr_mst_tapploan2supplierpayment  where application2loan_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from agr_mst_tapploan2supplierpayment  where apploan2supplierdtl_gid = '" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }

        public void DaCollateralDocumentTempList(string employee_gid, string application2loan_gid, Documentname objfilename)
        {
            msSQL = " select application2loan_gid,collateraldocument_gid,document_name,document_path,document_title from agr_mst_tuploadcollateraldocument " +
                            " where application2loan_gid='" + employee_gid + "' or application2loan_gid='" + application2loan_gid + "'";
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
                        document_gid = dt["collateraldocument_gid"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        application2loan_gid = dt["application2loan_gid"].ToString(),
                    });
                    objfilename.DocumentList = get_filename;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaCollateralDocumentList(string application2loan_gid, Documentname objfilename)
        {
            msSQL = " select application2loan_gid,collateraldocument_gid,document_name,document_path,document_title from agr_mst_tuploadcollateraldocument " +
                            " where application2loan_gid='" + application2loan_gid + "'";
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
                        document_gid = dt["collateraldocument_gid"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        application2loan_gid = dt["application2loan_gid"].ToString(),
                    });
                    objfilename.DocumentList = get_filename;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaHypothecationDocumentTempList(string employee_gid, string application2hypothecation_gid, Documentname objfilename)
        {
            msSQL = " select application2hypothecation_gid,hypothecationdocument_gid,document_name,document_path,document_title from agr_mst_tuploadhypothecationocument " +
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
                        document_gid = dt["hypothecationdocument_gid"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        application2hypothecation_gid = dt["application2hypothecation_gid"].ToString(),
                    });
                    objfilename.DocumentList = get_filename;
                }
            }
            dt_datatable.Dispose();
            objfilename.status = true;
        }

        public bool DaEditcollateraldocument(HttpRequest httpRequest, Documentname objfilename, string employee_gid)
        {
            DocumentList objdocumentmodel = new DocumentList();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            //MemoryStream ms = new MemoryStream();
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
            //path = HttpContext.Current.Server.MapPath("../../erpdocument" + "/" + lscompany_code + "/" + "SamAgro/CollateralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/CollateralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
            //string lsfirstdocument_filepath = string.Empty;
            string document_title = httpRequest.Form["document_title"].ToString();
            string lsapplication2collateral_gid = httpRequest.Form["application2collateral_gid"].ToString();
            httpFileCollection = httpRequest.Files;

            if (httpRequest.Files.Count > 0)
            {
                string lsfirstdocument_filepath = string.Empty;
                httpFileCollection = httpRequest.Files;
                for (int i = 0; i < httpFileCollection.Count; i++)
                {

                    httpPostedFile = httpFileCollection[0];
                    string FileExtension = httpPostedFile.FileName;
                    //string lsfile_gid = msdocument_gid + FileExtension;
                    string lsfile_gid = msdocument_gid;
                    FileExtension = Path.GetExtension(FileExtension).ToLower();
                    lsfile_gid = lsfile_gid + FileExtension;
                    if ((FileExtension == ".xls") || (FileExtension == ".xlsx") || (FileExtension == ".doc") || (FileExtension == ".docx") || (FileExtension == ".pdf") || (FileExtension == ".zip") || (FileExtension == ".msg") || (FileExtension == ".oft") || (FileExtension == ".txt") || (FileExtension == ".txtx"))
                    {
                        MemoryStream ms = new MemoryStream();
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);
                        //lspath = HttpContext.Current.Server.MapPath("../../erp_documents" + "/" + lscompany_code + "/" + "SamAgro/CollateralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);
                        //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "SamAgro/CollateralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            objfilename.status = false;
                            return false;

                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/CollateralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/CollateralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("CAMD");
                        msSQL = " insert into agr_mst_tuploadcollateraldocument( " +
                                     " collateraldocument_gid," +
                                     " document_name, " +
                                     " document_title," +
                                     " document_path, " +
                                     " application2collateral_gid," +
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
                            msSQL = " select collateraldocument_gid,document_name,document_path,document_title from agr_mst_tuploadcollateraldocument " +
                                    " where application2collateral_gid='" + employee_gid + "' or application2collateral_gid='" + lsapplication2collateral_gid + "'";
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
                                        document_gid = dt["collateraldocument_gid"].ToString(),
                                        document_title = dt["document_title"].ToString(),
                                    });
                                    objfilename.DocumentList = get_filename;
                                }
                            }
                            dt_datatable.Dispose();

                            objfilename.status = true;
                            objfilename.message = "Collateral Document uploaded successfully";
                            return true;
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured while uploading Collateral document";
                            return false;
                        }

                    }

                    else
                    {
                        objfilename.status = false;
                        objfilename.message = "File format is not supported";

                    }

                }
                //else
                //{
                //    objfilename.status = false;
                //    objfilename.message = "File format is not supported";
                //    return false;
                //}
            }
            return true;
        }

        public bool Dapostcollateraldocument(HttpRequest httpRequest, Documentname objfilename, string employee_gid)
        {
            DocumentList objdocumentmodel = new DocumentList();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            //MemoryStream ms = new MemoryStream();
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
            path = HttpContext.Current.Server.MapPath("erpdocument" + "/" + lscompany_code + "/" + "SamAgro/CollateralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }


            string document_title = httpRequest.Form["document_title"].ToString();
            string lsapplication2loan_gid = httpRequest.Form["application2loan_gid"].ToString();
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
                        //ls_readStream = httpPostedFile.InputStream;
                        //ls_readStream.CopyTo(ms);
                        //lspath = ("erpdocument" + "/" + lscompany_code + "/" + "SamAgro/CollateralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                        //bool status;
                        //status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/CollateralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        //ms.Close();

                        MemoryStream ms = new MemoryStream();
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);
                        
                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.status = false;
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/CollateralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/CollateralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("CAMD");
                        msSQL = " insert into agr_mst_tuploadcollateraldocument( " +
                                     " collateraldocument_gid," +
                                     " document_name, " +
                                     " document_title," +
                                     " document_path, " +
                                     " application2loan_gid," +
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
                            msSQL = " select collateraldocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                               " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by,a.document_title " +
                               " from agr_mst_tuploadcollateraldocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                               " and b.user_gid = c.user_gid and (application2loan_gid='" + employee_gid + "' or application2loan_gid='" + lsapplication2loan_gid + "')";

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
                                        document_gid = (dr_datarow["collateraldocument_gid"].ToString()),
                                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                                        updated_date = dr_datarow["uploaded_date"].ToString(),
                                        document_title = dr_datarow["document_title"].ToString()
                                    });
                                }
                                objfilename.DocumentList = get_filename;
                            }
                            dt_datatable.Dispose();

                            objfilename.status = true;
                            objfilename.message = "Collateral Document uploaded successfully";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured while uploading Collateral document";
                        }
                    }
                    else
                    {
                        objfilename.status = false;
                        objfilename.message = "File format is not supported";
                        return false;
                    }
                }
            }
            return true;
        }

        public bool DaEditHypoDoc(HttpRequest httpRequest, Documentname objfilename, string employee_gid)
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
            //path = HttpContext.Current.Server.MapPath("../../erpdocument" + "/" + lscompany_code + "/" + "SamAgro/HypothecationDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/HypothecationDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
            string lsfirstdocument_filepath = string.Empty;
            string document_title = httpRequest.Form["document_title"].ToString();
            string lsapplication2hypothecation_gid = httpRequest.Form["application2hypothecation_gid"].ToString();
            httpFileCollection = httpRequest.Files;

            httpPostedFile = httpFileCollection[0];
            string FileExtension = httpPostedFile.FileName;
            //string lsfile_gid = msdocument_gid + FileExtension;
            string lsfile_gid = msdocument_gid;
            FileExtension = Path.GetExtension(FileExtension).ToLower();
            lsfile_gid = lsfile_gid + FileExtension;
            if ((FileExtension == ".xls") || (FileExtension == ".xlsx") || (FileExtension == ".doc") || (FileExtension == ".docx") || (FileExtension == ".pdf") || (FileExtension == ".zip") || (FileExtension == ".msg") || (FileExtension == ".oft") || (FileExtension == ".txt") || (FileExtension == ".txtx"))
            {
                ls_readStream = httpPostedFile.InputStream;
                ls_readStream.CopyTo(ms);
                //lspath = HttpContext.Current.Server.MapPath("../../erpdocument" + "/" + lscompany_code + "/" + "SamAgro/HypothecationDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                //objcmnfunctions.uploadFile(lspath, lsfile_gid);
                //lspath = "../../erpdocument" + "/" + lscompany_code + "/" + "SamAgro/HypothecationDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

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
                msSQL = " insert into agr_mst_tuploadhypothecationocument( " +
                             " hypothecationdocument_gid," +
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
                    msSQL = " select hypothecationdocument_gid,document_name,document_path,document_title from agr_mst_tuploadhypothecationocument " +
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
                                document_gid = dt["hypothecationdocument_gid"].ToString(),
                                document_title = dt["document_title"].ToString(),
                            });
                            objfilename.DocumentList = get_filename;
                        }
                    }
                    dt_datatable.Dispose();

                    objfilename.status = true;
                    objfilename.message = "Hypothecation Document uploaded successfully";
                    return true;
                }
                else
                {
                    objfilename.status = false;
                    objfilename.message = "Error Occured while uploading document";
                    return false;
                }
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "File format is not supported";
                return false;
            }
        }

        public bool DaPostIndividualMobileNumber(string employee_gid, MdlContactMobileNo values)
        {
            msSQL = "select primary_status from agr_mst_tcontact2mobileno where primary_status='Yes' and (contact_gid='" + employee_gid + "' or contact_gid='" + values.contact_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Mobile Number Added";
                objdbconn.CloseConn();
                return false;
            }

            msSQL = "select mobile_no from agr_mst_tcontact2mobileno where mobile_no='" + values.mobile_no + "' and (contact_gid='" + employee_gid + "' or contact_gid='" + values.contact_gid + "') ";
            string lsmobile_no = objdbconn.GetExecuteScalar(msSQL);
            if (lsmobile_no == (values.mobile_no))
            {

                values.status = false;
                values.message = "Already This Mobile Number Added";
                objdbconn.CloseConn();
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("C2MN");

            msSQL = " insert into agr_mst_tcontact2mobileno(" +
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
                values.message = "Error Occured";
                return false;
            }
        }

        public void DaGetIndividualMobileNoTempList(string contact_gid, string employee_gid, MdlContactMobileNo values)
        {
            msSQL = "select mobile_no,contact2mobileno_gid,primary_status,whatsapp_no from agr_mst_tcontact2mobileno where " +
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
            msSQL = "select mobile_no,contact2mobileno_gid,primary_status,whatsapp_no from agr_mst_tcontact2mobileno where " +
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
                msSQL = " select mobile_no,contact2mobileno_gid,primary_status,whatsapp_no from agr_mst_tcontact2mobileno where " +
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
            msSQL = " select mobile_no,contact2mobileno_gid,primary_status,whatsapp_no from agr_mst_tcontact2mobileno where " +
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
                msSQL = " update agr_mst_tcontact2mobileno set " +
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

                    msSQL = "Insert into agr_mst_tcontact2mobilenoupdatelog(" +
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
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaDeleteIndividualMobileNo(string contact2mobileno_gid, MdlContactMobileNo values)
        {
            msSQL = "delete from agr_mst_tcontact2mobileno where contact2mobileno_gid='" + contact2mobileno_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tindividual2mobilenoupdatelog where contact2mobileno_gid='" + contact2mobileno_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Mobile Number Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;
            }
        }

        public bool DaPostIndividualEmailAddress(string employee_gid, MdlContactEmail values)
        {
            msSQL = "select primary_status from agr_mst_tcontact2email where primary_status='Yes' and (contact_gid='" + employee_gid + "' or contact_gid='" + values.contact_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Email Address Added";
                objdbconn.CloseConn();
                return false;
            }

            msSQL = "select email_address from agr_mst_tcontact2email where email_address='" + values.email_address + "' and (contact_gid='" + employee_gid + "' or contact_gid='" + values.contact_gid + "')";
            string lsemail_address = objdbconn.GetExecuteScalar(msSQL);
            if (lsemail_address == (values.email_address))
            {
                values.status = false;
                values.message = "Already This Email Address Added";
                objdbconn.CloseConn();
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("C2EA");
            msSQL = " insert into agr_mst_tcontact2email(" +
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
                values.message = "Email Address Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }

        public void DaGetIndividualEmailAddressTempList(string contact_gid, string employee_gid, MdlContactEmail values)
        {
            msSQL = "select email_address,contact2email_gid,primary_status from agr_mst_tcontact2email where " +
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
            msSQL = "select email_address,contact2email_gid,primary_status from agr_mst_tcontact2email where " +
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
                msSQL = " select email_address,contact2email_gid,primary_status from agr_mst_tcontact2email where " +
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
            msSQL = " select email_address,contact2email_gid,primary_status from agr_mst_tcontact2email where " +
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
                msSQL = " update agr_mst_tcontact2email set " +
                         " email_address='" + values.email_address + "'," +
                         " primary_status='" + values.primary_status + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where contact2email_gid='" + values.contact2email_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("IEUL");

                    msSQL = "Insert into agr_mst_tcontact2emailupdatelog(" +
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
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaDeleteIndividualEmailAddress(string contact2email_gid, MdlContactEmail values)
        {
            msSQL = "delete from agr_mst_tcontact2email where contact2email_gid='" + contact2email_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tcontact2emailupdatelog where contact2email_gid='" + contact2email_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Email Address Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public bool DaPostIndividualAddress(string employee_gid, MdlContactAddress values)
        {
            msSQL = "select primary_status from agr_mst_tcontact2address where primary_status='Yes' and (contact_gid='" + employee_gid + "' or contact_gid='" + values.contact_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Address Added";
                return false;
            }

            msSQL = "select contact2address_gid from agr_mst_tcontact2address where addresstype_name='" + values.addresstype_name + "' and " +
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
            msSQL = " insert into agr_mst_tcontact2address(" +
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
                values.message = "Error Occured";
                return false;
            }

        }


        public void DaGetIndividualAddressTempList(string contact_gid, string employee_gid, MdlContactAddress values)
        {
            msSQL = " select contact2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, latitude, longitude," +
                    " postal_code from agr_mst_tcontact2address where contact_gid='" + employee_gid + "' or contact_gid = '" + contact_gid + "'";
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
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString())
                    });
                }
                values.contactaddress_list = getcontactaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGetIndividualAddressList(string contact_gid, string employee_gid, MdlContactAddress values)
        {
            msSQL = " select contact2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, latitude, longitude," +
                    " postal_code from agr_mst_tcontact2address where contact_gid='" + contact_gid + "'";
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
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString())
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
                    " district, state, country, latitude, longitude, contact_gid, contact2address_gid " +
                    " from agr_mst_tcontact2address where contact2address_gid='" + contact2address_gid + "'";
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

        public void DaUpdateIndividualAddress(string employee_gid, MdlContactAddress values)
        {
            msSQL = "select addresstype_gid, addresstype_name, addressline1, addressline2, landmark, taluka, primary_status, postal_code, city," +
                    " district, state, country, latitude, longitude, contact_gid, contact2address_gid " +
                    " from agr_mst_tcontact2address where contact2address_gid='" + values.contact2address_gid + "'";
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
            try
            {
                msSQL = " update agr_mst_tcontact2address set " +
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
                         " latitude='" + values.latitude + "'," +
                         " longitude='" + values.longitude + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where contact2address_gid='" + values.contact2address_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("IAUL");

                    msSQL = " insert into agr_mst_tcontact2addressupdatelog(" +
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
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaDeleteIndividualAddress(string contact2address_gid, MdlContactAddress values)
        {
            msSQL = "delete from agr_mst_tcontact2address where contact2address_gid='" + contact2address_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tcontact2addressupdatelog where contact2address_gid='" + contact2address_gid + "'";
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

        public void DaGetIndividualProofTempList(string contact_gid, string employee_gid, MdlContactIdProof values)
        {
            msSQL = "select contact2idproof_gid,idproof_name,idproof_no,idproof_dob,file_no,document_name, document_path from agr_mst_tcontact2idproof where " +
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
            msSQL = "select contact2idproof_gid,idproof_name,idproof_no,idproof_dob,file_no,document_name, document_path from agr_mst_tcontact2idproof where " +
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
            String path = lspath;
            string lsindividualdocument_gid = httpRequest.Form["individualdocument_gid"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();
            string lsdocumenttype_gid = httpRequest.Form["documenttype_gid"].ToString();
            string lsdocumenttype_name = httpRequest.Form["documenttype_name"].ToString();

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
                        //lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SamAgro/IndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        //FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                        //ms.WriteTo(file);
                        //file.Close();
                        //ms.Close();
                        //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "SamAgro/IndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/IndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/IndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        msSQL = "select covenant_type from ocs_mst_tindividualdocument where individualdocument_gid='" + lsindividualdocument_gid + "'";
                        string lscovenant_type = objdbconn.GetExecuteScalar(msSQL);

                        msGetGid = objcmnfunctions.GetMasterGID("C2DO");
                        msGetDocumentGid = objcmnfunctions.GetMasterGID("BSDA");

                        msSQL = " insert into agr_mst_tcontact2document( " +
                                    " contact2document_gid, " +
                                    " contact_gid, " +
                                    " individualdocument_gid, " +
                                    " covenant_type," +
                                    " document_gid ," +
                                    " document_title ," +
                                    " document_name ," +
                                    " document_path," +
                                    " documenttype_gid," +
                                    " documenttype_name," +
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
                                    "'" + lsdocumenttype_gid + "'," +
                                    "'" + lsdocumenttype_name + "'," +
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
            msSQL = " select contact2document_gid,document_name,document_title,document_path,documenttype_name from agr_mst_tcontact2document " +
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
                        documenttype_name = dt["documenttype_name"].ToString(),
                    });
                    values.uploadindividualdoc_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetIndividualDocList(string contact_gid, MdlContactDocument values)
        {
            msSQL = " select contact2document_gid,document_name,document_title,document_path, documenttype_name from agr_mst_tcontact2document " +
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
                        documenttype_name = dt["documenttype_name"].ToString()
                    });
                    values.uploadindividualdoc_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaIndividualDocDelete(string contact2document_gid, MdlContactDocument values)
        {
            msSQL = "delete from agr_mst_tcontact2document where contact2document_gid='" + contact2document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                msSQL = " select groupdocumentchecklist_gid from agr_trn_tdocumentchecktls where documentuploaded_gid='" + contact2document_gid + "'";
                string lsgroupdocumentchecklist_gid = objdbconn.GetExecuteScalar(msSQL);

                if (lsgroupdocumentchecklist_gid != "")
                {
                    msSQL = " select count(documentcheckdtl_gid) as documentcount from agr_trn_tdocumentchecktls " +
                            " where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                    string lsdocumentcount = objdbconn.GetExecuteScalar(msSQL);
                    if (lsdocumentcount == "1")
                    {
                        msSQL = "delete from agr_trn_tgroupdocumentchecklist where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                msSQL = " select groupcovdocumentchecklist_gid from agr_trn_tcovanantdocumentcheckdtls where documentuploaded_gid='" + contact2document_gid + "'";
                string lschecklist_gid = objdbconn.GetExecuteScalar(msSQL);
                if (lschecklist_gid != "")
                {
                    msSQL = " select count(covenantdocumentcheckdtl_gid) as documentcount from agr_trn_tcovanantdocumentcheckdtls " +
                      " where groupcovdocumentchecklist_gid='" + lschecklist_gid + "'";
                    string lsdocumentcount = objdbconn.GetExecuteScalar(msSQL);
                    if (lsdocumentcount == "1")
                    {
                        msSQL = "delete from agr_trn_tgroupcovenantdocumentchecklist where groupcovdocumentchecklist_gid='" + lschecklist_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                msSQL = "delete from agr_trn_tcovanantdocumentcheckdtls where documentuploaded_gid='" + contact2document_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "delete from agr_trn_tdocumentchecktls where documentuploaded_gid='" + contact2document_gid + "'";
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
                msSQL = " select application_gid,pan_status,pan_no,aadhar_no,first_name,middle_name,last_name,individual_dob,age,gender_gid,gender_name,designation_gid,designation_name," +
                        " educationalqualification_gid,educationalqualification_name,main_occupation,annual_income,monthly_income," +
                        " pep_status,date_format(pepverified_date,'%d-%m-%Y') as pepverified_date,maritalstatus_gid,maritalstatus_name,stakeholdertype_gid,stakeholder_type," +
                        " father_firstname,father_middlename,father_lastname,father_dob,father_age," +
                        " mother_firstname,mother_middlename,mother_lastname,mother_dob,mother_age," +
                        " spouse_firstname,spouse_middlename,spouse_lastname,spouse_dob,spouse_age," +
                        " ownershiptype_gid,ownershiptype_name,residencetype_gid,residencetype_name,currentresidence_years,branch_distance, contact_status," +
                        " propertyholder_gid, propertyholder_name, incometype_gid, incometype_name, previouscrop, prposedcrop,institution_gid,institution_name," +
                        " group_gid, group_name, profile, urn_status, urn, fathernominee_status, mothernominee_status, spousenominee_status, othernominee_status," +
                        " relationshiptype, nomineefirst_name, nominee_middlename, nominee_lastname, nominee_dob, nominee_age, totallandinacres, cultivatedland" +
                        " from agr_mst_tcontact where contact_gid='" + contact_gid + "'";


                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
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

        public void DaUpdateIndividual(string employee_gid, MdlMstContact values)
        {


            msSQL = " select a.individualdocument_gid from ocs_mst_tindividualdocument a" +
                   " left join ocs_mst_tindividualdocumentprogram b on a.individualdocument_gid = b.individualdocument_gid " +
                  " where  a.documenttypes_gid = 'DOCT2022010611' and " +
                  " status = 'Y' and  b.program_gid ='" + values.program_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmasterdocument_list = new List<string>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmasterdocument_list.Add(dr_datarow["individualdocument_gid"].ToString());
                }
            }
            dt_datatable.Dispose();

            msSQL = " select distinct(a.individualdocument_gid) " +
                    " from agr_mst_tcontact2document a where a.documenttype_gid = 'DOCT2022010611' and " +
                    " ( contact_gid='" + values.contact_gid + "' or contact_gid = '" + employee_gid + "') ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettransactiondocument_list = new List<string>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    gettransactiondocument_list.Add(dr_datarow["individualdocument_gid"].ToString());
                }
            }
            dt_datatable.Dispose();

            var set1 = new HashSet<string>(getmasterdocument_list);
            var set2 = new HashSet<string>(gettransactiondocument_list);

            if (set1.SetEquals(set2) == false)
            {
                values.status = false;
                values.message = "Upload All KYC Documents";
                return;
            }


            msSQL = "select application_gid from agr_mst_tcontact where contact_gid='" + values.contact_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            if (values.stakeholdertype_name == "Borrower" || values.stakeholdertype_name == "Applicant")
            {
                msSQL = "select stakeholder_type from agr_mst_tcontact where application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Borrower','Applicant')" +
                    " and contact_gid<>'" + values.contact_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Already Added Applicant/Borrower Information";
                    return;
                }
                msSQL = "select stakeholder_type from agr_mst_tinstitution where application_gid='" + lsapplication_gid + "' and " +
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

            //if (values.stakeholdertype_name == "Applicant")
            //{
            //    msSQL = "select contact_gid from agr_mst_tcontact2bankdtl where contact_gid='" + employee_gid + "' or contact_gid='" + values.contact_gid + "' and primary_status='Yes'";
            //    objODBCDatareader = objdbconn.GetDataReader(msSQL);

            //    if (objODBCDatareader.HasRows == false)
            //    {
            //        objODBCDatareader.Close();
            //        values.status = false;
            //        values.message = "Add Primary Bank Account Detail";
            //        return;
            //    }
            //    objODBCDatareader.Close();
            //}

            msSQL = "select pan_status from agr_mst_tcontact where contact_gid='" + values.contact_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (values.pan_status == "Customer Submitting PAN")
            {
                msSQL = "delete from agr_mst_tcontact2panform60 where contact_gid='" + values.contact_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "delete from agr_mst_tcontact2panabsencereason where contact_gid='" + values.contact_gid + "'";
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
                        " from agr_mst_tcontact where contact_gid='" + values.contact_gid + "'";
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

            msSQL = " update agr_mst_tcontact set " +
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

                    msSQL = " select panabsencereason from agr_mst_tcontact2panabsencereason" +
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
                            msSQL = " INSERT INTO agr_mst_tcontact2panabsencereason(" +
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
                                msSQL = " INSERT INTO agr_mst_tcontact2panabsencereason(" +
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
                                msSQL = "delete from agr_mst_tcontact2panabsencereason where panabsencereason='" + values.contactpanabsencereason_list[i].panabsencereason + "' and contact_gid = '" + values.contact_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            matchCount2 = 0;
                        }
                    }

                }
                msGetGid = objcmnfunctions.GetMasterGID("CTUL");

                msSQL = " insert into agr_mst_tcontactupdatelog(" +
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

                //Updates
                msSQL = "update agr_mst_tcontact2mobileno set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcontact2email set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcontact2address set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcontact2idproof set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcontact2panform60 set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcontact2panabsencereason set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcontact2bankdtl set contact_gid='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpanauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpanaadhaarlink set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycdlauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycepicauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpassportauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycifscauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycbankaccverification set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                msSQL = "update agr_mst_tcontact2bankdtl set contact_gid='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select individualdocument_gid, contact2document_gid from agr_mst_tcontact2document where contact_gid='" + employee_gid + "'";
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
                        "'" + lsapplication_gid + "'," +
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
                       "'" + lsapplication_gid + "'," +
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

                DaAgrMstScannedDocument objvalues = new DaAgrMstScannedDocument();
                objvalues.DaGroupDocChecklistinfo(lsapplication_gid, values.contact_gid, employee_gid);

                msSQL = "update agr_mst_tcontact2document set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select mobile_no from agr_mst_tcontact2mobileno where contact_gid='" + values.contact_gid + "' and primary_status='yes'";
                string lsmobileno = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select email_address from agr_mst_tcontact2email where contact_gid='" + values.contact_gid + "' and primary_status='yes'";
                lsemail_address = objdbconn.GetExecuteScalar(msSQL);

                if (values.stakeholder_type == "Borrower" || values.stakeholder_type == "Applicant")
                {
                    msSQL = "update agr_mst_tapplication set applicant_type ='Individual' where application_gid='" + lsapplication_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tcontact set mobile_no='" + lsmobileno + "'," +
                        " email_address='" + lsemail_address + "' where contact_gid='" + values.contact_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
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

        public void DaGetIndividualSummary(string application_gid, MdlMstContact values)
        {
            msSQL = "select a.contact_gid,a.pan_no,a.aadhar_no,concat(first_name, ' ',middle_name,' ',last_name) as individual_name,stakeholder_type,contact_status," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " institution_name,group_name from agr_mst_tcontact a " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " where application_gid='" + application_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcontact_list = new List<contact_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcontact_list.Add(new contact_list
                    {
                        contact_gid = (dr_datarow["contact_gid"].ToString()),
                        individual_name = (dr_datarow["individual_name"].ToString()),
                        pan_no = (dr_datarow["pan_no"].ToString()),
                        aadhar_no = (dr_datarow["aadhar_no"].ToString()),
                        stakeholder_type = (dr_datarow["stakeholder_type"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        contact_status = (dr_datarow["contact_status"].ToString()),
                        institution_name = dr_datarow["institution_name"].ToString(),
                        group_name = dr_datarow["group_name"].ToString(),
                    });
                }
            }
            values.contact_list = getcontact_list;
            dt_datatable.Dispose();
        }

        public void DaGetBasicDetailsSummary(string application_gid, MdlMstApplicationAdd values)
        {
            msSQL = " select a.application_no, a.application_gid,a.customer_urn,a.customerref_name as customer_name,status," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " product_gid, variety_gid,renewal_flag,amendment_flag from agr_mst_tapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbasicdetails_list = new List<basicdetails_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbasicdetails_list.Add(new basicdetails_list
                    {
                        application_no = (dr_datarow["application_no"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        customer_urn = (dr_datarow["customer_urn"].ToString()),
                        customer_name = (dr_datarow["customer_name"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        application_status = (dr_datarow["status"].ToString()),
                        product_gid = (dr_datarow["product_gid"].ToString()),
                        variety_gid = (dr_datarow["variety_gid"].ToString()),
                        renewal_flag = (dr_datarow["renewal_flag"].ToString()),
                        amendment_flag = (dr_datarow["amendment_flag"].ToString())
                    });
                }
            }
            values.basicdetails_list = getbasicdetails_list;
            dt_datatable.Dispose();
        }

        public bool DaPostAppMobileNo(string employee_gid, MdlMstMobileNo values)
        {
            msSQL = "select primary_mobileno from agr_mst_tapplication2contactno where primary_mobileno='Yes' and (application_gid='" + employee_gid + "' or" +
               " application_gid='" + values.application_gid + "') ";
            string lsprimary_mobileno = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_mobileno == (values.primary_mobileno))
            {

                values.status = false;
                values.message = "Already Primary Mobile Number Added";
                return false;
            }
            msSQL = "select application2contact_gid from agr_mst_tapplication2contactno where mobile_no='" + values.mobile_no + "' " +
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
            msSQL = " insert into agr_mst_tapplication2contactno(" +
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
                values.message = "Error Occured";
                return false;
            }
        }

        public void DaGetAppMobileNoTempList(string application_gid, string employee_gid, MdlMstMobileNo values)
        {
            msSQL = "select mobile_no,application2contact_gid,primary_mobileno,whatsapp_mobileno from agr_mst_tapplication2contactno where " +
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
            msSQL = "select mobile_no,application2contact_gid,primary_mobileno,whatsapp_mobileno from agr_mst_tapplication2contactno where " +
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
                msSQL = " select mobile_no,application2contact_gid,primary_mobileno,whatsapp_mobileno from agr_mst_tapplication2contactno where " +
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
            msSQL = " select mobile_no,application2contact_gid,primary_mobileno,whatsapp_mobileno from agr_mst_tapplication2contactno where " +
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
                msSQL = " update agr_mst_tapplication2contactno set " +
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

                    msSQL = "Insert into agr_mst_tapplication2contactnoupdatelog(" +
                   " application2contactnoupdatelog_gid, " +
                   " application2contact_gid, " +
                   " application_gid, " +
                   " mobile_no," +
                   " primary_status," +
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
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }


        public void DaDeleteAppMobileNo(string application2contact_gid, MdlMstMobileNo values)
        {
            msSQL = "delete from agr_mst_tapplication2contactno where application2contact_gid='" + application2contact_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Mobile Number Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public bool DaPostAppEmailAddress(string employee_gid, MdlMstEmailAddress values)
        {
            msSQL = "select primary_emailaddress from agr_mst_tapplication2email where primary_emailaddress='Yes' and (application_gid='" + employee_gid + "' or application_gid='" + values.application_gid + "')";
            string lsprimary_emailaddress = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_emailaddress == (values.primary_emailaddress))
            {

                values.status = false;
                values.message = "Already Primary Email Address Added";
                return false;
            }
            msSQL = "select application2email_gid from agr_mst_tapplication2email where email_address='" + values.email_address + "' and (application_gid='" + employee_gid + "' or application_gid='" + values.application_gid + "')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already This Email Address Added";
                return false;
            }
            msGetGid = objcmnfunctions.GetMasterGID("A2EA");
            msSQL = " insert into agr_mst_tapplication2email(" +
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
                values.message = "Error Occured";
                return false;
            }
        }

        public void DaGetAppEmailAddressTempList(string application_gid, string employee_gid, MdlMstEmailAddress values)
        {
            msSQL = "select email_address,application2email_gid,primary_emailaddress from agr_mst_tapplication2email where " +
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
            msSQL = "select email_address,application2email_gid,primary_emailaddress from agr_mst_tapplication2email where " +
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
                msSQL = " select email_address,application2email_gid,primary_emailaddress from agr_mst_tapplication2email where " +
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
            msSQL = " select email_address,application2email_gid,primary_emailaddress from agr_mst_tapplication2email where " +
                        " application2email_gid='" + values.application2email_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsemail_address = objODBCDatareader["email_address"].ToString();
                lsprimary_emailaddress = objODBCDatareader["primary_emailaddress"].ToString();
                lsapplication2email_gid = objODBCDatareader["application2email_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tapplication2email set " +
                         " email_address='" + values.email_address + "'," +
                         " primary_emailaddress='" + values.primary_emailaddress + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where application2email_gid='" + values.application2email_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("AEUL");

                    msSQL = "Insert into agr_mst_tapplication2emailupdatelog(" +
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
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaDeleteAppEmailAddress(string application2email_gid, MdlMstEmailAddress values)
        {
            msSQL = "delete from agr_mst_tapplication2email where application2email_gid='" + application2email_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Email Address Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public void DaPostAppGeneticCode(string employee_gid, MdlMstGeneticCode values)
        {
            msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            if (lsapplication_gid == "" || lsapplication_gid == null)
            {
                lsapplication_gid = values.application_gid;
            }
            msSQL = "select geneticcode_gid from agr_mst_tapplication2geneticcode where (application_gid='" + employee_gid + "' or " +
                " application_gid='" + lsapplication_gid + "') and geneticcode_gid='" + values.geneticcode_gid + "'";
            string lsgenetic_code = objdbconn.GetExecuteScalar(msSQL);
            if (lsgenetic_code == (values.geneticcode_gid))
            {

                values.status = false;
                values.message = "Already Genetic Code Added";
                return;

            }
            msGetGid = objcmnfunctions.GetMasterGID("A2GC");
            msSQL = " insert into agr_mst_tapplication2geneticcode(" +
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
                values.message = "Error Occured ";
            }
        }

        public void DaGetAppGeneticCodeTempList(string application_gid, string employee_gid, MdlMstGeneticCode values)
        {
            msSQL = " select application2geneticcode_gid,geneticcode_gid,geneticcode_name,genetic_status,genetic_remarks,application_gid" +
                      " from agr_mst_tapplication2geneticcode where " +
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
                      " from agr_mst_tapplication2geneticcode where " +
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
                msSQL = " select application2geneticcode_gid,geneticcode_gid,geneticcode_name,genetic_status,genetic_remarks from agr_mst_tapplication2geneticcode where " +
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

            msSQL = " update agr_mst_tapplication2geneticcode set " +
                     " genetic_status='" + values.genetic_status + "'," +
                     " genetic_remarks='" + values.genetic_remarks.Replace("'", " ") + "'," +
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where application2geneticcode_gid='" + values.application2geneticcode_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                msGetGid = objcmnfunctions.GetMasterGID("AGUL");

                msSQL = "Insert into agr_mst_tapplication2geneticcodeupdatelog(" +
               " application2geneticcodeupdatelog_gid, " +
               " application2geneticcode_gid, " +
               " geneticcode_gid " +
               " geneticcode_name," +
               " genetic_status," +
               " genetic_remarks," +
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
               "'" + employee_gid + "'," +
               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Genetic Code Details Updated Successfully";
            }

        }


        public void DaDeleteAppGeneticCode(string application2geneticcode_gid, MdlMstGeneticCode values, string employee_gid)
        {
            msSQL = "delete from agr_mst_tapplication2geneticcode where application2geneticcode_gid='" + application2geneticcode_gid + "'";
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

        public void DaEditAppBasicDetail(string application_gid, MdlMstApplicationAdd values)
        {
            try
            {
                msSQL = " select application_gid,customer_urn,customerref_name as customer_name,vertical_gid,vertical_name,verticaltaggs_gid,verticaltaggs_name," +
                        " constitution_gid,constitution_name,businessunit_gid,businessunit_name,sa_status,sa_id,sa_name,saname_gid,vernacularlanguage_gid," +
                        " vernacular_language,contactpersonfirst_name,contactpersonmiddle_name,contactpersonlast_name,designation_gid,designation_type,landline_no," +
                        " creditgroup_gid,creditgroup_name,program_gid,program_name, product_gid,product_name,variety_gid,variety_name,sector_name,category_name, " +
                        " botanical_name,alternative_name,onboarding_status from agr_mst_tapplication where application_gid='" + application_gid + "'";
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
                    values.onboarding_status = objODBCDatareader["onboarding_status"].ToString();


                    //values.vernacularlanguage_gid = objODBCDatareader["vernacularlanguage_gid"].ToString();
                    //values.vernacular_language = objODBCDatareader["vernacular_language"].ToString();


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

                    /*      for (var i = 0; i < verlanggid_list.Length; i++)
                          {
                              values.vernacularlanguage_list[i].vernacularlanguage_gid = verlanggid_list[i];
                          }

                          for (var j = 0; j < verlanggid_list.Length; j++)
                          {
                              values.vernacularlanguage_list[j].vernacular_language = verlangname_list[j];
                          } */


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

                msSQL = "select primaryvaluechain_gid,primaryvaluechain_name from agr_mst_tapplication2primaryvaluechain where application_gid='" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                values.primaryvaluechain_list = dt_datatable.AsEnumerable().Select(row =>
           new primaryvaluechain_list
           {
               valuechain_gid = row["primaryvaluechain_gid"].ToString(),
               valuechain_name = row["primaryvaluechain_name"].ToString()
           }).ToList();
                dt_datatable.Dispose();

                msSQL = "select secondaryvaluechain_gid,secondaryvaluechain_name from agr_mst_tapplication2secondaryvaluechain where application_gid='" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                values.secondaryvaluechain_list = dt_datatable.AsEnumerable().Select(row =>
             new secondaryvaluechain_list
             {
                 valuechain_gid = row["secondaryvaluechain_gid"].ToString(),
                 valuechain_name = row["secondaryvaluechain_name"].ToString()
             }).ToList();
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

        public void DaUpdateAppBasicDetail(string employee_gid, MdlMstApplicationAdd values)
        {
            lsapplication_gid = objdbconn.GetExecuteScalar("select application_gid from agr_mst_tapplication where application_gid='" + values.application_gid + "' and " +
                " headapproval_status='Comment Raised'");
            if (lsapplication_gid == "" || lsapplication_gid == null)
            {
            }
            else
            {
                string lsverticalgid = objdbconn.GetExecuteScalar("select vertical_gid from agr_mst_tapplication where application_gid='" + values.application_gid + "'");

                if (lsverticalgid != values.vertical_gid)
                {
                    values.status = false;
                    values.message = "Already Approval Initiated.. You Can't Change the Vertical";
                    return;

                }

            }

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
            //if (values.onboarding_status == "Proposal")
            //{

            //    lsapplication_gid = objdbconn.GetExecuteScalar(" select application_gid from agr_mst_tapplication2loan where application_gid='" + values.application_gid + "'");
            //    //if (lsapplication_gid != "" || lsapplication_gid != null)
            //    if (!string.IsNullOrEmpty(lsapplication_gid))
            //    {
            //        values.status = false;
            //        values.message = "Kindly remove the product details. To change to Advance flow";
            //        return;
            //    }

            //    else
            //    {
                   
            //    }
            //}

            //else
            //{
            //    //values.status = false;
            //    //values.message = "Onboarding Status could not be changed to credit, Kindly the remove the product details....!";
            //    //return;
            //}
            string lsapp_refno = "";
            msSQL = "select count(*) from ocs_mst_tgeneticcode where status='Y'";
            string lsmastercount = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(*) from agr_mst_tapplication2geneticcode where application_gid='" + employee_gid + "' or application_gid='" + values.application_gid + "'";
            string lsgeneticcount = objdbconn.GetExecuteScalar(msSQL);
            if (lsmastercount == lsgeneticcount)
            {
                msSQL = "select application_gid from agr_mst_tapplication2contactno where (application_gid='" + employee_gid + "' or application_gid='" + values.application_gid + "')" +
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

                msSQL = "select application_gid from agr_mst_tapplication2email where (application_gid='" + employee_gid + "' or application_gid='" + values.application_gid + "')" +
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
                             " category_name, botanical_name, alternative_name from agr_mst_tapplication " +
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

                }
                objODBCDatareader.Close();
                try
                {
                    string lsstatus = "", lsonboard_applicationno = "";
                    msSQL = "select status,application_no from agr_mst_tapplication where application_gid='" + values.application_gid + "' ";
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

                            //msSQL = "select entity_code from adm_mst_tentity where entity_gid='" + lsentity_gid + "'";
                            //string lsentity_code = objdbconn.GetExecuteScalar(msSQL);
                            string lsentity_code = "SA";

                            lsapp_refno = "ARN" + lsentity_code + lsvertical_refno + DateTime.Now.ToString("ddMMyyyy");

                            string msGETRef = objcmnfunctions.GetMasterGID("APP");
                            msGETRef = msGETRef.Replace("APP", "");
                            lsapp_refno = lsapp_refno + msGETRef + "IN01";

                        }
                        else
                        {
                            msSQL = "select application_no from agr_mst_tapplication where application_gid='" + values.application_gid + "' ";
                            lsapp_refno = objdbconn.GetExecuteScalar(msSQL);
                        }
                    }
                    else
                    {
                        objODBCDatareader.Close();
                    }

                    msSQL = " select concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as level_one ,a.employeereporting_to " +
                                       "  from adm_mst_tmodule2employee a " +
                                       "  left join hrm_mst_temployee f on a.employeereporting_to = f.employee_gid " +
                                       "  left join adm_mst_tuser g on g.user_gid = f.user_gid  " +
                                        "  where a.module_gid in  (select module_gid_parent from adm_mst_tmodule where module_gid in " +
                                        "  (select modulereportingto_gid from adm_mst_tcompany)) " +
                                        " and g.user_status = 'Y' and a.employee_gid ='" + employee_gid + "' group by a.employee_gid ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsdrm_gid = objODBCDatareader["employeereporting_to"].ToString();
                        lsdrm_name = objODBCDatareader["level_one"].ToString();
                    }
                    objODBCDatareader.Close();

                    string lsclusterhead, lsregionalhead, lszonalhead, lsbusinesshead;
                    string lsclusterheadgid, lsregionalheadgid, lszonalheadgid, lsbusinessheadgid;
                    string lsbaselocationgid, lsclustergid, lsregiongid, lszonalgid;
                    string lsbaselocationname, lsclustername, lsregionname, lszonalname, lsrmemployee_name;

                    msSQL = "select concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as employee_name" +
                            " from hrm_mst_temployee a" +
                            " left join adm_mst_tuser b on a.user_gid=b.user_gid" +
                            " where a.employee_gid='" + employee_gid + "'";
                    lsrmemployee_name = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select distinct b.baselocation_gid,b.baselocation_name,c.cluster_gid,c.cluster_name, " +
                            " c.employee_name as clusterhead,c.vertical_name as clustervertical," +
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
                            " left join sys_mst_tbusinesshead h on h.zone_gid = g.zonal_gid where a.employee_gid = '" + employee_gid + "' and" +
                            " c.vertical_gid = '" + values.vertical_gid + "'" +
                            " and e.vertical_gid = '" + values.vertical_gid + "' and " +
                            " g.vertical_gid = '" + values.vertical_gid + "' and h.vertical_gid = '" + values.vertical_gid + "'" +
                            " and c.program_gid = '" + values.program_gid + "' and e.program_gid = '" + values.program_gid + "' and " +
                            " g.program_gid = '" + values.program_gid + "' and h.program_gid = '" + values.program_gid + "' " +
                            " and e.status='Y' and g.status='Y' and h.status='Y' and c.status='Y'";
                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader1.HasRows == true)
                    {
                        lsclusterhead = objODBCDatareader1["clusterhead"].ToString();
                        lsregionalhead = objODBCDatareader1["regionhead"].ToString();
                        lszonalhead = objODBCDatareader1["zonalhead"].ToString();
                        lsbusinesshead = objODBCDatareader1["businesshead"].ToString();
                        lsclusterheadgid = objODBCDatareader1["clusterhead_gid"].ToString();
                        lsregionalheadgid = objODBCDatareader1["regionhead_gid"].ToString();
                        lszonalheadgid = objODBCDatareader1["zonalhead_gid"].ToString();
                        lsbusinessheadgid = objODBCDatareader1["businesshead_gid"].ToString();
                        lsbaselocationgid = objODBCDatareader1["baselocation_gid"].ToString();
                        lsbaselocationname = objODBCDatareader1["baselocation_name"].ToString();
                        lsclustergid = objODBCDatareader1["cluster_gid"].ToString();
                        lsclustername = objODBCDatareader1["cluster_name"].ToString();
                        lsregiongid = objODBCDatareader1["region_gid"].ToString();
                        lsregionname = objODBCDatareader1["region_name"].ToString();
                        lszonalgid = objODBCDatareader1["zonal_gid"].ToString();
                        lszonalname = objODBCDatareader1["zonal_name"].ToString();

                        if (values.onboarding_status == "Direct")
                        {
                            msSQL = " SELECT creditgroup_name,creditmapping_gid from ocs_mst_tcreditmapping where creditgroup_name='" + getAutoApprovalClass.CreditGroupName + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                values.creditgroup_gid = objODBCDatareader["creditmapping_gid"].ToString();
                                values.creditgroup_name = objODBCDatareader["creditgroup_name"].ToString();
                            }
                            objODBCDatareader.Close();
                        }

                        msSQL = " update agr_mst_tapplication set " +
                            " application_no='" + lsapp_refno + "'," +
                             " customer_urn='" + values.customer_urn + "'," +
                             " customerref_name='" + values.customer_name + "'," +
                             " vertical_gid='" + values.vertical_gid + "'," +
                             " vertical_name='" + values.vertical_name + "'," +
                             " onboarding_status='" + values.onboarding_status + "'," +
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
                             " cluster_gid='" + lsclustergid + "'," +
                             " cluster_name='" + lsclustername + "'," +
                             " region_gid='" + lsregiongid + "'," +
                             " region_name='" + lsregionname + "'," +
                             " zone_gid='" + lszonalgid + "'," +
                             " zone_name='" + lszonalname + "'," +
                             " drm_gid='" + lsdrm_gid + "'," +
                             " drm_name='" + lsdrm_name + "'," +
                             " clustermanager_gid='" + lsclusterheadgid + "'," +
                             " clustermanager_name='" + lsclusterhead + "'," +
                             " zonalhead_name='" + lszonalhead + "'," +
                             " zonalhead_gid='" + lszonalheadgid + "'," +
                             " regionalhead_name='" + lsregionalhead + "'," +
                             " regionalhead_gid='" + lsregionalheadgid + "'," +
                             " businesshead_name='" + lsbusinesshead + "'," +
                             " businesshead_gid='" + lsbusinessheadgid + "'," +
                             " relationshipmanager_name='" + lsrmemployee_name + "'," +
                             " relationshipmanager_gid='" + employee_gid + "'," +
                             " creditgroup_gid='" + values.creditgroup_gid + "'," +
                             " creditgroup_name='" + values.creditgroup_name + "'," +
                             " program_gid='" + values.program_gid + "'," +
                             " program_name='" + values.program_name + "'," +
                             //" product_gid= '" + values.product_gid + "'," +
                             //" product_name='" + values.product_name + "'," +
                             //" variety_gid= '" + values.variety_gid + "'," +
                             //" variety_name='" + values.variety_name + "'," +
                             //" sector_name= '" + values.sector_name + "'," +
                             //" category_name='" + values.category_name + "'," +
                             //" botanical_name= '" + values.botanical_name + "'," +
                             //" alternative_name='" + values.alternative_name + "'," +
                             " status = 'Completed'," +
                             " updated_by='" + employee_gid + "'," +
                             " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                             " where application_gid='" + values.application_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    objODBCDatareader1.Close();
                    if (mnResult == 1)
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("ABUL");

                        msSQL = "Insert into agr_mst_tapplicationbasicdetailsupdatelog(" +
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
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        //msSQL = "select primaryvaluechain_gid,primaryvaluechain_name from agr_mst_tapplication2primaryvaluechain where application_gid='" + values.application_gid + "'";
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
                        //        msSQL = " insert into agr_mst_tapplication2primaryvaluechain(" +
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
                        //        msSQL = "select application2primaryvaluechain_gid from agr_mst_tapplication2primaryvaluechain where primaryvaluechain_gid='" + existingprimaryvaluechain_list[i].valuechain_gid + "' and application_gid = '" + values.application_gid + "'";
                        //        string lsapplication2primaryvaluechain_gid = objdbconn.GetExecuteScalar(msSQL);

                        //        msSQL = "delete from agr_mst_tapplication2primaryvaluechain where application2primaryvaluechain_gid='" + lsapplication2primaryvaluechain_gid + "'";
                        //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        //    }
                        //}

                        //Secondary Value Chain

                        //msSQL = "select secondaryvaluechain_gid,secondaryvaluechain_name from agr_mst_tapplication2secondaryvaluechain where application_gid='" + values.application_gid + "'";
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
                        //            msSQL = " insert into agr_mst_tapplication2secondaryvaluechain(" +
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
                        //        msSQL = "select application2secondaryvaluechain_gid from agr_mst_tapplication2secondaryvaluechain where secondaryvaluechain_gid='" + existingsecondaryvaluechain_list[i].valuechain_gid + "' and application_gid = '" + values.application_gid + "'";
                        //        string lsapplication2secondaryvaluechain_gid = objdbconn.GetExecuteScalar(msSQL);

                        //        msSQL = "delete from agr_mst_tapplication2secondaryvaluechain where application2secondaryvaluechain_gid='" + lsapplication2secondaryvaluechain_gid + "'";
                        //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        //    }
                        //}

                        //Updates

                        msSQL = "update agr_mst_tapplication2contactno set application_gid ='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_mst_tapplication2email set application_gid ='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_mst_tapplication2geneticcode set application_gid ='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_mst_tapplication2product set application_gid ='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                        if (values.onboarding_status == "Direct")
                        {

                            msSQL = "update agr_mst_tapplication set overalllimit_amount ='0' where application_gid='" + values.application_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }

                            values.status = true;
                        values.message = "Basic Details Updated Successfully";
                    }
                    else
                    {
                        values.message = "Location / Customer / Supplier Type not Assigned for Business Approval";
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

        public void DaGetApplicationBasicDetailsTempClear(string employee_gid, result values)
        {
            msSQL = "delete from agr_mst_tapplication2contactno where application_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tapplication2email where application_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tapplication2geneticcode where application_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tapplication2product where application_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }

        public void DaEditProceed(string application_gid, MdlMstApplicationAdd values, string employee_gid)
        {
            msSQL = "select a.application_gid from agr_mst_tapplication a" +
               " left join agr_mst_tcontact b on a.application_gid = b.application_gid " +
               " left join agr_mst_tinstitution c on a.application_gid = c.application_gid" +
               " where a.application_gid ='" + application_gid + "'" +
               " and(b.stakeholder_type in ('Applicant','Borrower') or c.stakeholder_type in ('Applicant','Borrower'))";
            string lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            if (lsapplication_gid == "" || lsapplication_gid == null)
            {
                values.proceed_flag = "N";
            }
            else
            {

                msSQL = "select applicant_type from agr_mst_tapplication where application_gid='" + application_gid + "'";
                string lsapplicant_type = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select status from agr_mst_tapplication where application_gid='" + application_gid + "' and status!='Incomplete'";
                string lsgeneral_status = objdbconn.GetExecuteScalar(msSQL);

                if (lsapplicant_type == "" || lsapplicant_type == null || lsgeneral_status == "" || lsgeneral_status == null)
                {
                    values.proceed_flag = "N";
                }
                else
                {
                    msSQL = "select productcharge_flag from  agr_mst_tapplication where application_gid='" + application_gid + "'";
                    string lsproductcharge_flag = objdbconn.GetExecuteScalar(msSQL);
                    if (lsproductcharge_flag == "N" || lsproductcharge_flag == null || lsproductcharge_flag == "")
                    {
                        values.proceed_flag = "N";
                    }
                    else
                    {
                        msSQL = "select application_gid from agr_mst_tapplication2loan where application_gid='" + application_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {

                            msSQL = "select application_gid from agr_mst_tgroup where application_gid = '" + application_gid + "' and group_status='Incomplete'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == false)
                            {

                                msSQL = "select application_gid from agr_mst_tcontact where application_gid = '" + application_gid + "' and contact_status='Incomplete'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == false)
                                {

                                    msSQL = "select application_gid from agr_mst_tinstitution where application_gid = '" + application_gid + "' and institution_status='Incomplete'";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == false)
                                    {
                                        objODBCDatareader.Close();
                                        msSQL = " select clustermanager_name,zonalhead_name,regionalhead_name,businesshead_name from agr_mst_tapplication where application_gid = '" + lsapplication_gid + "'";
                                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader1.HasRows == true)
                                        {
                                            values.cluster_head = objODBCDatareader1["clustermanager_name"].ToString();
                                            values.zonal_head = objODBCDatareader1["zonalhead_name"].ToString();
                                            values.regional_head = objODBCDatareader1["regionalhead_name"].ToString();
                                            values.business_head = objODBCDatareader1["businesshead_name"].ToString();
                                        }


                                        objODBCDatareader1.Close();
                                        msSQL = " select a.employeereporting_to,concat(c.user_firstname,' ',c.user_lastname,' || ',c.user_code) as level_zero,b.employee_gid, " +
                                                "  concat( g.user_firstname, ' ', g.user_lastname, ' || ', g.user_code) as level_one  " +
                                                "  from adm_mst_tmodule2employee a " +
                                                "  left join hrm_mst_temployee b on b.employee_gid = a.employee_gid " +
                                                "  left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                                                "  left join hrm_mst_temployee f on a.employeereporting_to = f.employee_gid " +
                                                "  left join adm_mst_tuser g on g.user_gid = f.user_gid  " +
                                                "  where a.module_gid in  (select module_gid_parent from adm_mst_tmodule where module_gid in " +
                                                 "  (select modulereportingto_gid from adm_mst_tcompany)) " +
                                                " and c.user_status = 'Y' and b.employee_gid ='" + employee_gid + "'  group by a.employee_gid ";

                                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader1.HasRows == true)
                                        {
                                            values.level_zero = objODBCDatareader1["level_zero"].ToString();
                                            values.level_one = objODBCDatareader1["level_one"].ToString();
                                            objODBCDatareader1.Close();
                                        }


                                        //objODBCDatareader1.Close();
                                        values.proceed_flag = "Y";
                                    }
                                    else
                                    {
                                        objODBCDatareader.Close();
                                        values.proceed_flag = "N";
                                    }
                                }
                                else
                                {
                                    objODBCDatareader.Close();
                                    values.proceed_flag = "N";
                                }
                            }
                            else
                            {
                                objODBCDatareader.Close();
                                values.proceed_flag = "N";
                            }
                            objODBCDatareader.Close();
                        }
                        else
                        {
                            objODBCDatareader.Close();
                            values.proceed_flag = "N";
                        }
                    }
                }
            }

            msSQL = "select applicationapproval_gid from agr_trn_tapplicationapproval where application_gid='" + application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                values.approveinitiated_flag = "N";
            }
            else
            {
                values.approveinitiated_flag = "Y";
            }
            objODBCDatareader.Close();
        }

        public void DaEditAppProceed(string employee_gid, string user_gid, MdlMstApplicationAdd values)
        {

            int k;
            string lsapproval_gid;
            string lsapprovalname;
            
            msSQL = " select drm_gid, drm_name from agr_mst_tapplication where application_gid = '" + values.application_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                k = 1;
                string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                sToken = "";
                int Length = 100;
                for (int j = 0; j < Length; j++)
                {
                    string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                    sToken += sTempChars;
                }

                msSQL = " select approval_gid, approval_name from agr_trn_tapplicationapproval where application_gid = '" + values.application_gid + "' and hierary_level='1' ";
                objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader1.HasRows == false)
                {

                    msGetGid = objcmnfunctions.GetMasterGID("APAP");
                msSQL = "Insert into agr_trn_tapplicationapproval( " +
                       " applicationapproval_gid, " +
                       " application_gid," +
                       " approval_gid," +
                       " approval_name," +
                       " approval_type," +
                       " hierary_level," +
                       " approval_token," +
                       " initiate_flag," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid + "'," +
                       "'" + values.application_gid + "'," +
                       "'" + objODBCDatareader["drm_gid"].ToString() + "'," +
                       "'" + objODBCDatareader["drm_name"].ToString() + "'," +
                       "'sequence'," +
                       "'" + k + "'," +
                       "'" + sToken + "'," +
                       "'Y'," +
                       "'" + user_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                objODBCDatareader1.Close();
            }
            objODBCDatareader.Close();

            msSQL = " select clustermanager_name,zonalhead_name,regionalhead_name,businesshead_name,clustermanager_gid,zonalhead_gid,regionalhead_gid,businesshead_gid from agr_mst_tapplication where application_gid = '" + values.application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {

                for (k = 2; k < 6; k++)
                {
                    char level;
                    level = Convert.ToChar(k);
                    lsapproval_gid = "";
                    lsapprovalname = "";

                    if (level == '\u0002')
                    {
                        lsapproval_gid = objODBCDatareader["clustermanager_gid"].ToString();
                        lsapprovalname = objODBCDatareader["clustermanager_name"].ToString();
                    }
                    else if (level == '\u0003')
                    {
                        lsapproval_gid = objODBCDatareader["regionalhead_gid"].ToString();
                        lsapprovalname = objODBCDatareader["regionalhead_name"].ToString();
                    }
                    else if (level == '\u0004')
                    {
                        lsapproval_gid = objODBCDatareader["zonalhead_gid"].ToString();
                        lsapprovalname = objODBCDatareader["zonalhead_name"].ToString();
                    }
                    else if (level == '\u0005')
                    {
                        lsapproval_gid = objODBCDatareader["businesshead_gid"].ToString();
                        lsapprovalname = objODBCDatareader["businesshead_name"].ToString();
                    }
                    string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                    sToken = "";
                    int Length = 100;
                    for (int j = 0; j < Length; j++)
                    {
                        string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                        sToken += sTempChars;
                    }

                    //msGetGid = objcmnfunctions.GetMasterGID("APAP");

                    //msSQL = "Insert into agr_trn_tapplicationapproval( " +
                    //       " applicationapproval_gid, " +
                    //       " application_gid," +
                    //       " approval_gid," +
                    //       " approval_name," +
                    //       " approval_type," +
                    //       " hierary_level," +
                    //       " approval_token," +
                    //       " created_by," +
                    //       " created_date)" +
                    //       " values(" +
                    //       "'" + msGetGid + "'," +
                    //       "'" + values.application_gid + "'," +
                    //       "'" + lsapproval_gid + "'," +
                    //       "'" + lsapprovalname + "'," +
                    //       "'sequence'," +
                    //       "'" + k + "'," +
                    //       "'" + sToken + "'," +
                    //       "'" + user_gid + "'," +
                    //       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (level == '\u0002')
                    {
                        msSQL = " select approval_gid, approval_name from agr_trn_tapplicationapproval where application_gid = '" + values.application_gid + "' and hierary_level='2' ";
                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader1.HasRows == false)
                        {
                            msGetGid = objcmnfunctions.GetMasterGID("APAP");
                            msSQL = "Insert into agr_trn_tapplicationapproval( " +
                                   " applicationapproval_gid, " +
                                   " application_gid," +
                                   " approval_gid," +
                                   " approval_name," +
                                   " approval_type," +
                                   " hierary_level," +
                                   " approval_token," +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + msGetGid + "'," +
                                   "'" + values.application_gid + "'," +
                                   "'" + lsapproval_gid + "'," +
                                   "'" + lsapprovalname + "'," +
                                   "'sequence'," +
                                   "'" + k + "'," +
                                   "'" + sToken + "'," +
                                   "'" + user_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    else if (level == '\u0003')
                    {
                        msSQL = " select approval_gid, approval_name from agr_trn_tapplicationapproval where application_gid = '" + values.application_gid + "' and hierary_level='3' ";
                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader1.HasRows == false)
                        {

                            msGetGid = objcmnfunctions.GetMasterGID("APAP");
                            msSQL = "Insert into agr_trn_tapplicationapproval( " +
                                   " applicationapproval_gid, " +
                                   " application_gid," +
                                   " approval_gid," +
                                   " approval_name," +
                                   " approval_type," +
                                   " hierary_level," +
                                   " approval_token," +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + msGetGid + "'," +
                                   "'" + values.application_gid + "'," +
                                   "'" + lsapproval_gid + "'," +
                                   "'" + lsapprovalname + "'," +
                                   "'sequence'," +
                                   "'" + k + "'," +
                                   "'" + sToken + "'," +
                                   "'" + user_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }
                    }
                    else if (level == '\u0004')
                    {
                        msSQL = " select approval_gid, approval_name from agr_trn_tapplicationapproval where application_gid = '" + values.application_gid + "' and hierary_level='4' ";
                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader1.HasRows == false)
                        {

                            msGetGid = objcmnfunctions.GetMasterGID("APAP");
                            msSQL = "Insert into agr_trn_tapplicationapproval( " +
                                   " applicationapproval_gid, " +
                                   " application_gid," +
                                   " approval_gid," +
                                   " approval_name," +
                                   " approval_type," +
                                   " hierary_level," +
                                   " approval_token," +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + msGetGid + "'," +
                                   "'" + values.application_gid + "'," +
                                   "'" + lsapproval_gid + "'," +
                                   "'" + lsapprovalname + "'," +
                                   "'sequence'," +
                                   "'" + k + "'," +
                                   "'" + sToken + "'," +
                                   "'" + user_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }
                    }
                    else if (level == '\u0005')
                    {
                        msSQL = " select approval_gid, approval_name from agr_trn_tapplicationapproval where application_gid = '" + values.application_gid + "' and hierary_level='5' ";
                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader1.HasRows == false)
                        {

                            msGetGid = objcmnfunctions.GetMasterGID("APAP");
                            msSQL = "Insert into agr_trn_tapplicationapproval( " +
                                   " applicationapproval_gid, " +
                                   " application_gid," +
                                   " approval_gid," +
                                   " approval_name," +
                                   " approval_type," +
                                   " hierary_level," +
                                   " approval_token," +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + msGetGid + "'," +
                                   "'" + values.application_gid + "'," +
                                   "'" + lsapproval_gid + "'," +
                                   "'" + lsapprovalname + "'," +
                                   "'sequence'," +
                                   "'" + k + "'," +
                                   "'" + sToken + "'," +
                                   "'" + user_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }
                    }
                    objODBCDatareader1.Close();

                }
            }

            objODBCDatareader.Close();
            //msSQL = "update agr_mst_tapplication set approval_flag='Y', approval_status='Submitted to Underwriting',submitted_by='"+employee_gid+"',"+
            //    " submitted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where application_gid='" + values.application_gid + "'";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update agr_mst_tapplication set approval_status='Submitted to Approval',submitted_by='" + employee_gid + "'," +
            " submitted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
            " where application_gid='" + values.application_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "select applicant_type from agr_mst_tapplication where application_gid='" + values.application_gid + "'";
                string lsapplicant_type = objdbconn.GetExecuteScalar(msSQL);

                if (lsapplicant_type == "Individual")
                {

                    msSQL = "select concat(first_name,middle_name,last_name) as customer_name,mobile_no,email_address,contact_gid from agr_mst_tcontact where" +
                        " application_gid='" + values.application_gid + "' and stakeholder_type in ('Applicant','Borrower')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lscustomer_name = objODBCDatareader["customer_name"].ToString();
                        lsmobile_no = objODBCDatareader["mobile_no"].ToString();
                        lsemail_address = objODBCDatareader["email_address"].ToString();
                        //Region
                        msSQL = "select state from agr_mst_tcontact2address where primary_status='Yes' and contact_gid='" + objODBCDatareader["contact_gid"].ToString() + "'";
                        lsregion = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = " update agr_mst_tapplication set customer_name='" + lscustomer_name + "'," +
                       " mobile_no='" + lsmobile_no + "'," +
                       " email_address='" + lsemail_address + "'," +
                       " region='" + lsregion + "'," +
                       " updated_by='" + employee_gid + "'," +
                       " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                       " where application_gid='" + values.application_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    objODBCDatareader.Close();

                }
                else
                {


                    msSQL = "select company_name,mobile_no,email_address,institution_gid from agr_mst_tinstitution where " +
                        " application_gid='" + values.application_gid + "' and stakeholder_type in ('Applicant','Borrower')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lscustomer_name = objODBCDatareader["company_name"].ToString();
                        lsmobile_no = objODBCDatareader["mobile_no"].ToString();
                        lsemail_address = objODBCDatareader["email_address"].ToString();
                        //Region
                        msSQL = "select state from agr_mst_tinstitution2address where primary_status='Yes' and institution_gid='" + objODBCDatareader["institution_gid"].ToString() + "'";
                        lsregion = objdbconn.GetExecuteScalar(msSQL);

                        //Main Table 
                        msSQL = " update agr_mst_tapplication set customer_name='" + lscustomer_name + "'," +
                       " mobile_no='" + lsmobile_no + "'," +
                       " email_address='" + lsemail_address + "'," +
                       " region='" + lsregion + "'," +
                       " updated_by='" + employee_gid + "'," +
                       " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" + 
                       " where application_gid='" + values.application_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    objODBCDatareader.Close();
                }

              
                msSQL = "select onboarding_status from agr_mst_tapplication where application_gid='" + values.application_gid + "'";
                string lsonboarding_status = objdbconn.GetExecuteScalar(msSQL);
                if (lsonboarding_status == "Direct")
                {
                    if (mnResult != 0)
                    {
                        string lsproductdesk_gid = "";
                        msSQL = " select productdesk_gid from agr_mst_tproductdeskmapping where products_gid in (select producttype_gid from agr_mst_tapplication2loan	" +
                                " where application_gid='" + values.application_gid + "') and app_productdesk='Y'  and productdesk_status='Y'";
                        lsproductdesk_gid = objdbconn.GetExecuteScalar(msSQL);
                        if (lsproductdesk_gid != "")
                        {
                            // Started - Business Approval 
                            msSQL = " update agr_trn_tapplicationapproval set  approval_status='Approved',approval_remarks='Auto Approval', initiate_flag='Y', " +
                                    " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                    " where application_gid='" + values.application_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " update agr_mst_tapplication set  approval_flag='Y', approval_status='Submitted to Product Desk', headapproval_status='All Heads Approved'" +
                                    " where application_gid='" + values.application_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            // Completed - Business Approval 

                            msSQL = " update agr_mst_tapplication set productdesk_flag='Y', productdesk_gid ='" + lsproductdesk_gid + "'" +
                                    " where application_gid='" + values.application_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        else
                        {
                            DaAgrTrnProductApproval objapproval = new DaAgrTrnProductApproval();
                            objapproval.FnAutoProductApprovalFlow(values.application_gid, employee_gid, user_gid);
                            DaAgrMstApplicationEdit objDaAgrMstApplicationEdit = new DaAgrMstApplicationEdit();
                            objDaAgrMstApplicationEdit.FnAutoApprovalFlow(values.application_gid, employee_gid, user_gid);

                        }

                        string lspage = "";

                        DaAgrMstApplicationAdd objMstApplicationAdd = new DaAgrMstApplicationAdd();
                        objMstApplicationAdd.FnProgramBasedDcoument(values.application_gid, employee_gid, user_gid, lsonboarding_status, lspage);

                        values.status = true;
                        values.message = "Buyer Proposal Submitted successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured while updated";
                    }
                }
                else
                {

                    if (mnResult != 0)
                    {

                        try
                        {
                            string lspage = "";
                            DaAgrMstApplicationAdd objMstApplicationAdd = new DaAgrMstApplicationAdd();
                            objMstApplicationAdd.FnProgramBasedDcoument(values.application_gid, employee_gid, user_gid, lsonboarding_status, lspage);


                            msSQL = " select clustermanager_gid,zonalhead_gid,regionalhead_gid,businesshead_gid from agr_mst_tapplication where application_gid = '" + values.application_gid + "'";
                            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader1.HasRows == true)
                            {
                                cluster_head_gid = objODBCDatareader1["clustermanager_gid"].ToString();
                                zonal_head_gid = objODBCDatareader1["zonalhead_gid"].ToString();
                                regional_head_gid = objODBCDatareader1["regionalhead_gid"].ToString();
                                business_head_gid = objODBCDatareader1["businesshead_gid"].ToString();
                            }

                            objODBCDatareader1.Close();
                            msSQL = " select approval_gid,approval_name from agr_trn_tapplicationapproval where application_gid = '" + values.application_gid + "' and hierary_level ='1'";
                            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader1.HasRows == true)
                            {
                                reportingto_gid = objODBCDatareader1["approval_gid"].ToString();
                                reportingto_name = objODBCDatareader1["approval_name"].ToString();
                            }
                            objODBCDatareader1.Close();
                            msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                                    " FROM adm_mst_tcompany";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                ls_server = objODBCDatareader["pop_server"].ToString();
                                ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                                ls_username = objODBCDatareader["pop_username"].ToString();
                                ls_password = objODBCDatareader["pop_password"].ToString();
                            }
                            objODBCDatareader.Close();

                            msSQL = "select application_no from agr_mst_tapplication where application_gid='" + values.application_gid + "'";
                            application_no = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = "select customerref_name from agr_mst_tapplication where application_gid='" + values.application_gid + "'";
                            customer_name = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = "select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.clustermanager_gid where application_gid='" + values.application_gid + "'";
                            cluster_head_mailid = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = " select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.zonalhead_gid where a.application_gid='" + values.application_gid + "'";
                            zonalhead_mailid = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = " select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.regionalhead_gid where a.application_gid='" + values.application_gid + "'";
                            regional_head_mailid = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = " select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.businesshead_gid where a.application_gid='" + values.application_gid + "'";
                            business_head_mailid = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = " select b.employee_emailid from agr_trn_tapplicationapproval a left join hrm_mst_temployee b on b.employee_gid = a.approval_gid where a.application_gid='" + values.application_gid + "'  and hierary_level ='1'";
                            reportingto_mailid = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = "select  concat( c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code)  from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.clustermanager_gid join adm_mst_tuser c on c.user_gid = b.user_gid where application_gid='" + values.application_gid + "'";
                            string cluster_head = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = " select  concat( c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code)  from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.zonalhead_gid left join adm_mst_tuser c on c.user_gid = b.user_gid where a.application_gid='" + values.application_gid + "'";
                            string zonal_head = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = " select  concat( c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code)  from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.relationshipmanager_gid left join adm_mst_tuser c on c.user_gid = b.user_gid where a.application_gid='" + values.application_gid + "'";
                            string rm_name = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = " select b.employee_emailid from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.created_by where a.application_gid='" + values.application_gid + "'";
                            creater_mailid = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = " select  concat( c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code)  from agr_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.regionalhead_gid left join adm_mst_tuser c on c.user_gid = b.user_gid where a.application_gid='" + values.application_gid + "'";
                            regionalhead_name = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = " select  a.overalllimit_amount  from agr_mst_tapplication a  where a.application_gid='" + values.application_gid + "'";
                            lsoveralllimit_amount = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = " select  group_concat(product_type) as Product, group_concat(productsub_type) as Program, group_concat(rate_interest) as Margin from agr_mst_tapplication2loan  where application_gid = '" + values.application_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                ls_Product = objODBCDatareader["Product"].ToString();
                                ls_Program = objODBCDatareader["Program"].ToString();
                                ls_Margin = objODBCDatareader["Margin"].ToString();
                            }
                            objODBCDatareader.Close();

                            tomail_id = reportingto_mailid;
                            //lssource = ConfigurationManager.AppSettings["img_path"];

                            sub = " ARN(" + application_no + ") : Application approval required  ";
                            body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
                            body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;align:center;'>";
                            body = body + "<br />";
                            body = body + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <img style='height:150px; width:380px;' src='" + lssource + "'><br />";
                            body = body + "<br />";
                            body = body + " &nbsp&nbsp Dear Sir/Madam,<br />";
                            body = body + "<br />";
                            body = body + "&nbsp&nbsp Greetings<br />";
                            body = body + "<br />";
                            body = body + "&nbsp&nbsp The below application has been submitted, please validate and approve to proceed for underwriting.<br />";
                            body = body + "<br />";
                            body = body + "&nbsp&nbsp <b>Application Number:</b> " + application_no + "<br /><br />";
                            body = body + "&nbsp&nbsp <b>Customer Name:</b> " + HttpUtility.HtmlEncode(customer_name) + "<br /><br />";
                            body = body + "&nbsp&nbsp <b>RM Name: </b>" + HttpUtility.HtmlEncode(rm_name) + "<br /><br />";
                            body = body + "&nbsp&nbsp <b>Cluster Head Name:</b> " + HttpUtility.HtmlEncode(cluster_head)+ "<br /><br />";
                            body = body + "&nbsp&nbsp <b>Zonal Head Name:</b> " + HttpUtility.HtmlEncode(zonal_head )+ "<br /><br />";
                            body = body + "&nbsp&nbsp <b>Regional Head Name:</b> " + HttpUtility.HtmlEncode(regionalhead_name)+ "<br /><br />";
                            body = body + "&nbsp&nbsp <b>Product:</b> " + ls_Product + "<br /><br />";
                            body = body + "&nbsp&nbsp <b>Program:</b> " + ls_Program + "<br /><br />";
                            body = body + "&nbsp&nbsp <b>Overall Limit Amount:</b> " + lsoveralllimit_amount + "<br /><br />";
                            body = body + "&nbsp&nbsp <b>Margin:</b> " + ls_Margin + "<br /><br />";
                            body = body + "&nbsp&nbsp <b>Action Time:</b> " + DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") + "<br /><br />";
                            body = body + "<br />";
                            //body = body + "&nbsp&nbsp Regards,";
                            //body = body + "<br />";
                            //body = body + "&nbsp&nbsp Sam-Custopedia <br /> ";
                            body = body + "&nbsp&nbsp Login " + ConfigurationManager.AppSettings["livedomain_url"].ToString() + " and complete the necessary actions. <br /> ";
                            body = body + "<br />";
                            body = body + "</td><td style='margin-left:20px; border-left-color:white;'>&nbsp&nbsp</td></tr></table>";
                            //sub = " ARN(" + application_no + ") : Application approval required  ";
                            //body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
                            //body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;align:center;'>";
                            //body = body + "<br />";
                            //body = body + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <img style='height:150px; width:380px;' src='" + lssource + "'><br />";
                            //body = body + "<br />";
                            //body = body + " &nbsp&nbsp Hello," + reportingto_name + " <br />";
                            //body = body + "<br />";
                            //body = body + "&nbsp&nbsp Greetings! Quick Heads-up on the below<br />";
                            //body = body + "<br />";
                            //body = body + "<table style='margin-left:18px; margin-right:18px;'><tr><th >Group</th><th>ARN</th><th>Customer Name</th><th>Comments</th></tr>";
                            //body = body + "<tr><td>Awaiting approval</td><td>" + application_no + "</td><td>" + customer_name + "</td><td>Pending L1 Approval</td></tr>";
                            //body = body + "<tr><td>Actions on Comments</td><td></td><td></td><td></td></tr>";
                            //body = body + "<tr><td>Queries to be addressed</td><td></td><td></td><td></td></tr></table>";
                            //body = body + "<br />";
                            //body = body + "&nbsp&nbsp Log into Sam-Custopedia and complete the necessary actions. <br />";
                            //body = body + "<br />";
                            //body = body + "&nbsp&nbsp Have a fantastic day!<br />";
                            //body = body + "<br />";
                            //body = body + "&nbsp&nbsp Thanks";
                            //body = body + "<br />";
                            //body = body + "&nbsp&nbsp Sam-Custopedia <br /> ";
                            //body = body + "<br />";
                            //body = body + "&nbsp&nbsp<hr>&nbsp&nbsp";
                            //body = body + "&nbsp&nbsp Reach out to us at samcustopedia@samunnati.com <br /> ";
                            //body = body + "<br />";
                            //body = body + "</td><td style='margin-left:20px; border-left-color:white;'>&nbsp&nbsp</td></tr></table>";

                            MailMessage message = new MailMessage();
                            SmtpClient smtp = new SmtpClient();
                            message.From = new MailAddress(ls_username);
                            message.To.Add(new MailAddress(tomail_id));
                            lsBccmail_id = ConfigurationManager.AppSettings["SamagroApprovalBccMail"].ToString();

                            if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                            {
                                lsBCCReceipients = lsBccmail_id.Split(',');
                                if (lsBccmail_id.Length == 0)
                                {
                                    message.Bcc.Add(new MailAddress(lsBccmail_id));
                                }
                                else
                                {
                                    foreach (string BCCEmail in lsBCCReceipients)
                                    {
                                        message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                                    }
                                }
                            }
                            cc_mailid = "" + cluster_head_mailid + "," + regional_head_mailid + "," + zonalhead_mailid + "";

                            if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                            {
                                lsCCReceipients = cc_mailid.Split(',');
                                if (cc_mailid.Length == 0)
                                {
                                    message.CC.Add(new MailAddress(cc_mailid));
                                }
                                else
                                {
                                    foreach (string CCEmail in lsCCReceipients)
                                    {
                                        message.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                                    }
                                }
                            }



                            message.Subject = sub;
                            message.IsBodyHtml = true; //to make message body as html  
                            message.Body = body;
                            smtp.Port = ls_port;
                            smtp.Host = ls_server; //for gmail host  
                            smtp.EnableSsl = true;
                            smtp.UseDefaultCredentials = false;
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                            smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.Send(message);

                            values.status = true;
                            values.message = "Customer / Supplier Creation Submitted successfully";
                        }
                        catch (Exception ex)
                        {
                            values.message = ex.ToString();
                            values.status = false;

                        }

                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured";
                    }
                }
            }
        }

        public void FnAutoApprovalFlow(string application_gid, string employee_gid, string user_gid, int? fromproductapproval = 0)
        {
            if (fromproductapproval == 0)
            {
                // Started - Business Approval 
                msSQL = " update agr_trn_tapplicationapproval set  approval_status='Approved',approval_remarks='Auto Approval', initiate_flag='Y', " +
                        " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where application_gid='" + application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update agr_mst_tapplication set  approval_flag='Y', approval_status='Submitted to Underwriting', headapproval_status='All Heads Approved'" +
                        " where application_gid='" + application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                // Completed - Business Approval 
            }
            // Credit Underwriting - started
            string lscreditmapping_gid = "", lscreditgroup_name = "", lscredithead_gid = "", lscredithead_name = "";
            string lsnationalcredit_gid = "", lsnationalcredit_name = "", lsregionalcredit_gid = "", lsregionalcredit_name = "";
            string lscreditmanager_gid = "", lscreditmanager_name = "";

            msSQL = " SELECT creditgroup_name,creditmapping_gid from ocs_mst_tcreditmapping where creditgroup_name='" + getAutoApprovalClass.CreditGroupName + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lscreditmapping_gid = objODBCDatareader["creditmapping_gid"].ToString();
                lscreditgroup_name = objODBCDatareader["creditgroup_name"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select credit2credithead_gid,employee_gid,employee_name, creditmapping_gid from ocs_mst_tcredit2credithead " +
                 " where creditmapping_gid='" + lscreditmapping_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lscredithead_gid = objODBCDatareader["employee_gid"].ToString();
                lscredithead_name = objODBCDatareader["employee_name"].ToString();
            }
            objODBCDatareader.Close();


            msSQL = " select credit2nationalmanager_gid,employee_gid,employee_name, creditmapping_gid from ocs_mst_tcredit2nationalmanager " +
               " where creditmapping_gid='" + lscreditmapping_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsnationalcredit_gid = objODBCDatareader["employee_gid"].ToString();
                lsnationalcredit_name = objODBCDatareader["employee_name"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select creditr2regionalmanager_gid,employee_gid,employee_name, creditmapping_gid from ocs_mst_tcreditr2regionalmanager " +
              " where creditmapping_gid='" + lscreditmapping_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsregionalcredit_gid = objODBCDatareader["employee_gid"].ToString();
                lsregionalcredit_name = objODBCDatareader["employee_name"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select credit2creditmanager_gid,employee_gid,employee_name, creditmapping_gid from ocs_mst_tcredit2creditmanager " +
              " where creditmapping_gid='" + lscreditmapping_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lscreditmanager_gid = objODBCDatareader["employee_gid"].ToString();
                lscreditmanager_name = objODBCDatareader["employee_name"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = "update agr_mst_tapplication set " +
                  " creditgroup_gid='" + lscreditmapping_gid + "'," +
                  " creditgroup_name='" + lscreditgroup_name + "'," +
                  " credithead_gid='" + lscredithead_gid + "'," +
                  " credithead_name='" + lscredithead_name + "'," +
                  " creditnationalmanager_gid='" + lsnationalcredit_gid + "'," +
                  " creditnationalmanager_name ='" + lsnationalcredit_name + "'," +
                  " creditregionalmanager_gid='" + lsregionalcredit_gid + "'," +
                  " creditregionalmanager_name='" + lsregionalcredit_name + "'," +
                  " creditmanager_gid='" + lscreditmanager_gid + "'," +
                  " creditmanager_name='" + lscreditmanager_name + "'," +
                  " creditgroup_status = 'Assigned'," +
                  " remarks ='Auto Approval'," +
                  " creditassigned_by='" + employee_gid + "'," +
                  " creditassigned_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                  " ccsubmitted_by = '" + employee_gid + "'," +
                  " ccsubmitted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                  " where application_gid='" + application_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            int k;
            if (mnResult != 0)
            {

                k = 0;
                string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                sToken = "";
                int Length = 100;
                for (int j = 0; j < Length; j++)
                {
                    string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                    sToken += sTempChars;
                }

                msGetGid = objcmnfunctions.GetMasterGID("CRAP");
                msSQL = "Insert into agr_trn_tAppcreditapproval( " +
                       " appcreditapproval_gid, " +
                       " application_gid," +
                       " approval_gid," +
                       " approval_name," +
                       " approval_type," +
                       " hierary_level," +
                       " approval_token," +
                       " initiate_flag," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid + "'," +
                       "'" + application_gid + "'," +
                       "'" + lscreditmanager_gid + "'," +
                       "'" + lscreditmanager_name + "'," +
                       "'sequence'," +
                       "'" + k + "'," +
                       "'" + sToken + "'," +
                       "'Y'," +
                       "'" + user_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            k = 0;
            string lsapproval_gid, lsinitiate_flag, lsapprovalname;
            msSQL = " select credithead_name,creditnationalmanager_name,creditregionalmanager_name,credithead_gid,creditnationalmanager_gid,creditregionalmanager_gid " +
                    "  from agr_mst_tapplication where application_gid = '" + application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {

                for (k = 1; k < 4; k++)
                {
                    char level;
                    level = Convert.ToChar(k);
                    lsapproval_gid = "";
                    lsapprovalname = "";
                    lsinitiate_flag = "";
                    if (level == '\u0001')
                    {
                        lsapproval_gid = objODBCDatareader["creditregionalmanager_gid"].ToString();
                        lsapprovalname = objODBCDatareader["creditregionalmanager_name"].ToString();
                        lsinitiate_flag = "Y";

                    }
                    else if (level == '\u0002')
                    {
                        lsapproval_gid = objODBCDatareader["creditnationalmanager_gid"].ToString();
                        lsapprovalname = objODBCDatareader["creditnationalmanager_name"].ToString();
                        lsinitiate_flag = "N";
                    }
                    else if (level == '\u0003')
                    {
                        lsapproval_gid = objODBCDatareader["credithead_gid"].ToString();
                        lsapprovalname = objODBCDatareader["credithead_name"].ToString();
                        lsinitiate_flag = "N";
                    }

                    string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                    sToken = "";
                    int Length = 100;
                    for (int j = 0; j < Length; j++)
                    {
                        string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                        sToken += sTempChars;
                    }

                    msGetGid = objcmnfunctions.GetMasterGID("CRAP");

                    msSQL = "Insert into agr_trn_tAppcreditapproval( " +
                           " appcreditapproval_gid," +
                           " application_gid, " +
                           " approval_gid," +
                           " approval_name," +
                           " approval_type," +
                           " hierary_level," +
                           " approval_token," +
                           " initiate_flag," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid + "'," +
                           "'" + application_gid + "'," +
                           "'" + lsapproval_gid + "'," +
                           "'" + lsapprovalname + "'," +
                           "'sequence'," +
                           "'" + k + "'," +
                           "'" + sToken + "'," +
                           "'" + lsinitiate_flag + "'," +
                           "'" + user_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }
            }

            objODBCDatareader.Close();

            msSQL = "update agr_mst_tapplication set approval_status='Submitted to Credit Approval' " +
                          " where application_gid='" + application_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            // Credit Approval 
            msSQL = " update agr_trn_tAppcreditapproval set  approval_status='Approved',approval_remarks='Auto Approval',initiate_flag='Y', " +
               " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
               " where application_gid='" + application_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update agr_mst_tapplication set  approval_flag='Y', creditheadapproval_status='All Heads Approved'," +
                   " ccsubmit_flag='Y', approval_status='Submitted to CC'" +
                   " where application_gid='" + application_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            // Credit Approval - Completed

            // CC scheduled Meeting - started

            string lsccgroup_gid = string.Empty;
            string lsccgroup_name = string.Empty;
            string lsotheruser_gid = string.Empty;
            string lsotheruser_name = string.Empty;
            string loopccgroup_gid = string.Empty;
            string lsccadmin_gid = string.Empty;
            string lsccadmin_name = string.Empty;
            string lsccmeeting_title = "Auto Approval";
            string lsccmeeting_no = "1";
            string lsccmeeting_mode = "Online";

            msSQL = "select ccgroupname_gid,ccgroup_code,ccgroup_name from ocs_mst_tccgroupname where ccgroup_name='" + getAutoApprovalClass.CCGroupName + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsccgroup_gid = objODBCDatareader["ccgroupname_gid"].ToString();
                lsccgroup_name = objODBCDatareader["ccgroup_name"].ToString();
                loopccgroup_gid = lsccgroup_gid;
            }
            objODBCDatareader.Close();

            msSQL = " select a.employee_gid, concat(b.user_firstname,' ', b.user_lastname, ' / ',b.user_code) as employee_name  from hrm_mst_temployee a " +
                    " left join adm_mst_tuser b on a.user_gid = b.user_gid " +
                    " where employee_emailid = '" + ConfigurationManager.AppSettings["SamagroAutoApprovalID"] + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsccadmin_gid = objODBCDatareader["employee_gid"].ToString();
                lsccadmin_name = objODBCDatareader["employee_name"].ToString();
            }
            objODBCDatareader.Close();

            msGetGid = objcmnfunctions.GetMasterGID("CCSM");
            msSQL = " insert into agr_mst_tccschedulemeeting(" +
                    " ccschedulemeeting_gid," +
                    " application_gid," +
                    " ccmeeting_title," +
                    " ccmeeting_no," +
                    " ccmeeting_date," +
                    " start_time," +
                    " end_time," +
                    " ccmeeting_mode," +
                    " ccgroupname_gid," +
                    " ccgroup_name," +
                    " otheruser_gid," +
                    " otheruser_name," +
                    " ccadmin_gid," +
                    " ccadmin_name," +
                    " description," +
                    " non_users," +
                    " created_by," +
                    " created_date," +
                    " updated_by," +
                    " updated_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + application_gid + "'," +
                    "'" + lsccmeeting_title + "'," +
                    "'" + lsccmeeting_no + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                    "null," +
                    "null," +
                    "'" + lsccmeeting_mode + "'," +
                    "'" + lsccgroup_gid + "'," +
                    "'" + lsccgroup_name + "'," +
                    "''," +
                    "''," +
                    "'" + lsccadmin_gid + "'," +
                    "'" + lsccadmin_name + "'," +
                    "''," +
                    "''," +
                    "'" + employee_gid + "'," +
                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                      "'" + employee_gid + "'," +
                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = " update agr_mst_tapplication set ccgroup_name ='" + lsccgroup_name + "'," +
                         " meeting_status='Scheduled'," +
                         " updated_by = '" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where application_gid='" + application_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //CC members 

                msSQL = " select a.ccmember_name,a.ccmember_gid,b.ccgroup_name from ocs_mst_tccmember a" +
                    " left join ocs_mst_tccgroupname b on a.ccgroupname_gid=b.ccgroupname_gid where" +
                    " a.ccgroupname_gid = '" + loopccgroup_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {

                        msGetGid1 = objcmnfunctions.GetMasterGID("CM2M");
                        msSQL = " insert into agr_mst_tccmeeting2members(" +
                                " ccmeeting2members_gid," +
                                " application_gid," +
                                " ccschedulemeeting_gid," +
                                " ccmember_name," +
                                " ccgroup_name," +
                                " ccmember_gid," +
                                " attendance_status, " +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid1 + "'," +
                                "'" + application_gid + "'," +
                                "'" + msGetGid + "'," +
                                "'" + dt["ccmember_name"].ToString() + "'," +
                                "'" + dt["ccgroup_name"].ToString() + "'," +
                                "'" + dt["ccmember_gid"].ToString() + "'," +
                                "'P'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();
            }

            msSQL = " update agr_mst_tccmeeting2members set ccapproval_flag='Y'," +
                    " approvalinitiate_by = '" + employee_gid + "'" +
                    " where application_gid='" + application_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            // MOm submitted

            msSQL = " update agr_mst_tapplication set " +
                            " momapproval_flag='Y'," +
                            " momupdated_by='" + employee_gid + "'," +
                            " momupdated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            " updated_by='" + employee_gid + "'," +
                            " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                            " where application_gid='" + application_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update agr_mst_tccmeeting2members set approval_status='Pending', ccmail_flag = 'Y' where application_gid='" + application_gid + "' and ccapproval_flag = 'Y'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            string lsemployee_name = "", lsemployee_gid = "";
            msSQL = " select a.application_gid, a.ccmember_name,a.ccmember_gid,a.ccgroup_name,a.ccmeeting2members_gid,f.ccadmin_gid,  " +
                         " a.ccapproval_flag,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as approvalinitiate_by, " +
                         " date_format(a.approved_date,'%d-%m-%Y %h:%i %p') as approved_date, " +
                         " a.attendance_status,a.approval_status,b.approval_status as overapproval_status " +
                         " from agr_mst_tccmeeting2members a  " +
                         " left join agr_mst_tapplication b on b.application_gid = a.application_gid " +
                         " left join agr_mst_tccschedulemeeting f on a.application_gid = f.application_gid " +
                         " left join hrm_mst_temployee d on a.approvalinitiate_by = d.employee_gid" +
                         " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                         " where a.application_gid='" + application_gid + "'and  a.ccapproval_flag = 'Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getccmember_list = new List<ccmembers_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    lsemployee_name = dt["ccmember_name"].ToString();
                    lsemployee_gid = dt["ccmember_gid"].ToString();
                    lsccadmin_gid = dt["ccadmin_gid"].ToString();

                    string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                    sToken = "";
                    int Length = 100;
                    for (int j = 0; j < Length; j++)
                    {
                        string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                        sToken += sTempChars;
                    }

                    msGetGid = objcmnfunctions.GetMasterGID("CCAP");

                    msSQL = "Insert into agr_trn_tccapproval( " +
                           " ccapproval_gid, " +
                           " application_gid, " +
                           " ccmeeting2members_gid," +
                           " approval_gid," +
                           " approval_name," +
                           " approval_token," +
                           //" requestapproval_remarks," +
                           " ccapprovalrequest_flag," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid + "'," +
                           "'" + dt["application_gid"].ToString() + "'," +
                           "'" + dt["ccmeeting2members_gid"].ToString() + "'," +
                           "'" + lsemployee_gid + "'," +
                           "'" + lsemployee_name + "'," +
                           "'" + sToken + "'," +
                           //"'" + values.approval_remarks.Replace("'", "\\'") + "'," +
                           "'Y'," +
                           "'" + user_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }
            }
            // CC Approval


            msSQL = " update agr_trn_tccapproval set approval_status='Approved',  " +
                 " approval_remarks='Auto Approval'," +
                 " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where application_gid='" + application_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update agr_mst_tccmeeting2members set approval_status='Approved'," +
                    " approval_remarks='Auto Approval'," +
                    " updated_by='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where application_gid='" + application_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update agr_mst_tapplication set approval_status='CC Approved', process_type = null, cccompleted_flag='Y'," +
                        " cc_remarks='Auto Approval'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        " cccompleted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where  application_gid='" + application_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        }

        public void DaEditAppReProceed(string employee_gid, string user_gid, MdlMstApplicationAdd values)
        {

            msSQL = " update agr_mst_tapplication set approval_status='Submitted to Approval',resubmitted_by ='" + employee_gid + "'," +
                    " resubmitted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where application_gid='" + values.application_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "select applicant_type from agr_mst_tapplication where application_gid='" + values.application_gid + "'";
                string lsapplicant_type = objdbconn.GetExecuteScalar(msSQL);

                if (lsapplicant_type == "Individual")
                {

                    msSQL = "select concat(first_name,middle_name,last_name) as customer_name,mobile_no,email_address,contact_gid from agr_mst_tcontact where" +
                        " application_gid='" + values.application_gid + "' and stakeholder_type in ('Applicant','Borrower')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lscustomer_name = objODBCDatareader["customer_name"].ToString();
                        lsmobile_no = objODBCDatareader["mobile_no"].ToString();
                        lsemail_address = objODBCDatareader["email_address"].ToString();
                        //Region
                        msSQL = "select state from agr_mst_tcontact2address where primary_status='Yes' and contact_gid='" + objODBCDatareader["contact_gid"].ToString() + "'";
                        lsregion = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = " update agr_mst_tapplication set customer_name='" + lscustomer_name + "'," +
                       " mobile_no='" + lsmobile_no + "'," +
                       " email_address='" + lsemail_address + "'," +
                       " region='" + lsregion + "'," +
                       " updated_by='" + employee_gid + "'," +
                       " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                       " where application_gid='" + values.application_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    objODBCDatareader.Close();

                }
                else
                {


                    msSQL = "select company_name,mobile_no,email_address,institution_gid from agr_mst_tinstitution where " +
                        " application_gid='" + values.application_gid + "' and stakeholder_type in ('Applicant','Borrower')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lscustomer_name = objODBCDatareader["company_name"].ToString();
                        lsmobile_no = objODBCDatareader["mobile_no"].ToString();
                        lsemail_address = objODBCDatareader["email_address"].ToString();
                        //Region
                        msSQL = "select state from agr_mst_tinstitution2address where primary_status='Yes' and institution_gid='" + objODBCDatareader["institution_gid"].ToString() + "'";
                        lsregion = objdbconn.GetExecuteScalar(msSQL);

                        //Main Table 
                        msSQL = " update agr_mst_tapplication set customer_name='" + lscustomer_name + "'," +
                       " mobile_no='" + lsmobile_no + "'," +
                       " email_address='" + lsemail_address + "'," +
                       " region='" + lsregion + "'," +
                       " updated_by='" + employee_gid + "'," +
                       " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                       " where application_gid='" + values.application_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    objODBCDatareader.Close();
                }

                if (mnResult != 0)
                {

                    values.status = true;
                    values.message = "Customer / Supplier Creation Submitted successfully";

                }
                else
                {

                    values.status = false;
                    values.message = "Error Occured";

                }
            }

        }

        public bool DaSaveInstitutionEditDtl(MdlMstInstitutionAdd values, string employee_gid)
        {

            msSQL = "select application_gid from agr_mst_tinstitution where institution_gid='" + values.institution_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

            //msSQL = "select user_type from ocs_mst_tusertype where usertype_gid ='" + values.stakeholdertype_gid + "'";
            // lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);
            if (lsstakeholder_type == "Borrower" || lsstakeholder_type == "Applicant")
            {
                msSQL = "select stakeholder_type from agr_mst_tcontact where application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Borrower','Applicant')";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Already Added Applicant/Borrower Information";
                    return false;
                }
                msSQL = "select stakeholder_type from agr_mst_tinstitution where application_gid='" + lsapplication_gid + "' and " +
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
            msSQL = " update agr_mst_tinstitution set " +
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
                     " stakeholder_type='" + lsstakeholder_type + "'," +
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
                     " institution_status='Incomplete'," +
                     " tan_number = '" + values.tan_number + "'," +
                     " incometax_returnsstatus ='" + values.incometax_returnsstatus + "',";
            if (values.revenue == null || values.revenue == "")
            {
                msSQL += "revenue='0.00',";
            }
            else
            {
                msSQL += "revenue='" + values.revenue.Replace(",", "") + "',";
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

            msSQL +=
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where institution_gid='" + values.institution_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {

                // Updates for Multiple Add
                msSQL = "update agr_mst_tinstitution2branch set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tinstitution2mobileno set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tinstitution2email set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tinstitution2address set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tinstitution2licensedtl set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tinstitution2bankdtl set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tinstitution2documentupload set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tinstitution2form60documentupload set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tinstitution2ratingdetail set institution_gid='" + values.institution_gid + "', application_gid ='" + lsapplication_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycgstsbpan set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpanauthentication set function_gid='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_trn_ttandtl set function_gid='" + values.institution_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycifscauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycbankaccverification set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
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

        public bool DaSaveInstitutionDtlAdd(MdlMstInstitutionAdd values, string employee_gid)
        {


            msSQL = "select stakeholder_type from agr_mst_tcontact where application_gid='" + values.application_gid + "' and stakeholder_type in ('Borrower','Applicant')";
             lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholder_type)
            {

                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return false;
            }

            msSQL = "select stakeholder_type from agr_mst_tinstitution where application_gid='" + values.application_gid + "' and stakeholder_type in ('Borrower','Applicant')";
            lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholder_type)
            {

                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return false;
            }
            msGetGid = objcmnfunctions.GetMasterGID("APIN");
            msSQL = " insert into agr_mst_tinstitution(" +
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
                msSQL += "'" + values.sundrydebt_adv.Replace(",", "") + "',";
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
                msSQL = "update agr_mst_tinstitution2branch set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tinstitution2mobileno set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tinstitution2email set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tinstitution2address set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tinstitution2licensedtl set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tinstitution2ratingdetail set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tinstitution2bankdtl set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_trn_ttandtl set function_gid='" + msGetGid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycgstsbpan set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpanauthentication set function_gid='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycifscauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycbankaccverification set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                msSQL = "select companydocument_gid, institution2documentupload_gid from agr_mst_tinstitution2documentupload where institution_gid='" + employee_gid + "'";
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

                DaAgrMstScannedDocument objvalues = new DaAgrMstScannedDocument();
                objvalues.DaGroupDocChecklistinfo(values.application_gid, msGetGid, employee_gid);

                msSQL = "update agr_mst_tinstitution2documentupload set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tinstitution2form60documentupload set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
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
        public bool DaSubmitInstitutionDtlAdd(MdlMstInstitutionAdd values, string user_gid, string employee_gid)
        {

            msSQL = " select a.companydocument_gid from ocs_mst_tcompanydocument a " +
                  " left join ocs_mst_tcompanydocumentprogram b on a.companydocument_gid = b.companydocument_gid " +
                  " where a.documenttypes_gid = 'DOCT2022010611'  and " +
                  " status = 'Y' and b.program_gid ='" + values.program_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmasterdocument_list = new List<string>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmasterdocument_list.Add(dr_datarow["companydocument_gid"].ToString());
                }
            }
            dt_datatable.Dispose();

            msSQL = " select distinct(a.companydocument_gid) " +
                    " from agr_mst_tinstitution2documentupload a where  a.documenttype_gid = 'DOCT2022010611' and " +
                    " institution_gid = '" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettransactiondocument_list = new List<string>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    gettransactiondocument_list.Add(dr_datarow["companydocument_gid"].ToString());
                }
            }
            dt_datatable.Dispose();

            var set1 = new HashSet<string>(getmasterdocument_list);
            var set2 = new HashSet<string>(gettransactiondocument_list);

            if (set1.SetEquals(set2) == false)
            {
                values.status = false;
                values.message = "Upload All KYC Documents";
                return false;
            }


            msSQL = "select institution_gid from agr_mst_tinstitution2mobileno where institution_gid='" + employee_gid + "' and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Primary Mobile Number";
                return false;
            }

            msSQL = "select institution_gid from agr_mst_tinstitution2mobileno where institution_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Atleast One Mobile Number";
                return false;
            }

            msSQL = "select institution_gid from agr_mst_tinstitution2email where institution_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Atleast One Email Address";
                return false;
            }

            msSQL = "select institution_gid from agr_mst_tinstitution2address where institution_gid='" + employee_gid + "'";
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
                msSQL = "select institution2branch_gid from agr_mst_tinstitution2branch where institution_gid='" + employee_gid + "' and headoffice_status ='Yes' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == false)
                {
                    values.status = false;
                    values.message = "Atleast Select One GST Number as Head Office";
                    return false;
                }
                objODBCDatareader.Close();
            }

            //msSQL = "select institution_gid from agr_mst_tinstitution2bankdtl where institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "' and primary_status='Yes'";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);

            //if (objODBCDatareader.HasRows == false)
            //{
            //    objODBCDatareader.Close();
            //    values.status = false;
            //    values.message = "Add Primary Bank Account Detail";
            //    return false;
            //}

            //msSQL = "select institution_gid from agr_mst_tinstitution2bankdtl where institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "'";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);

            //if (objODBCDatareader.HasRows == false)
            //{
            //    objODBCDatareader.Close();
            //    values.status = false;
            //    values.message = "Add Atleast One Bank Account Detail";
            //    return false;
            //}


            msSQL = "select stakeholder_type from agr_mst_tcontact where application_gid='" + values.application_gid + "' and stakeholder_type in ('Borrower','Applicant')";
            string lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholder_type)
            {

                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return false;
            }

            msSQL = "select stakeholder_type from agr_mst_tinstitution where application_gid='" + values.application_gid + "' and stakeholder_type in ('Borrower','Applicant')";
            lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholder_type)
            {

                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("APIN");
            msSQL = " insert into agr_mst_tinstitution(" +
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
                msSQL = "update agr_mst_tinstitution2branch set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tinstitution2mobileno set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tinstitution2email set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tinstitution2address set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tinstitution2licensedtl set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tinstitution2ratingdetail set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tinstitution2bankdtl set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycgstsbpan set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpanauthentication set function_gid='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycifscauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycbankaccverification set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_trn_ttandtl set function_gid='" + msGetGid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select companydocument_gid, institution2documentupload_gid from agr_mst_tinstitution2documentupload where institution_gid='" + employee_gid + "'";
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

                DaAgrMstScannedDocument objvalues = new DaAgrMstScannedDocument();
                objvalues.DaGroupDocChecklistinfo(values.application_gid, msGetGid, employee_gid);

                msSQL = "update agr_mst_tinstitution2documentupload set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tinstitution2form60documentupload set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycgstsbpan set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select mobile_no from agr_mst_tinstitution2mobileno where institution_gid='" + msGetGid + "' and primary_status='yes'";
                string lsmobileno = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select email_address from agr_mst_tinstitution2email where institution_gid='" + msGetGid + "' and primary_status='yes'";
                lsemail_address = objdbconn.GetExecuteScalar(msSQL);
                if (values.stakeholder_type == "Borrower" || values.stakeholder_type == "Applicant")
                {
                    msSQL = "update agr_mst_tapplication set applicant_type ='Institution' where application_gid='" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tinstitution set mobile_no='" + lsmobileno + "'," +
                     " email_address='" + lsemail_address + "' where institution_gid='" + msGetGid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (!string.IsNullOrEmpty(values.lspage))
                {

                    msSQL = " select onboarding_status from agr_mst_tapplication where application_gid ='" + values.application_gid + "' ";
                    string lsonboarding_status = objdbconn.GetExecuteScalar(msSQL);

                    DaAgrMstApplicationAdd objMstApplicationAdd = new DaAgrMstApplicationAdd();
                    objMstApplicationAdd.FnProgramBasedDcoument(values.application_gid, employee_gid, user_gid, lsonboarding_status, values.lspage);

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
        public bool DaSubmitInstitutionEditDtl(MdlMstInstitutionAdd values, string employee_gid)
        {


            msSQL = " select a.companydocument_gid from ocs_mst_tcompanydocument a " +
                   " left join ocs_mst_tcompanydocumentprogram b on a.companydocument_gid = b.companydocument_gid " +
                   " where a.documenttypes_gid = 'DOCT2022010611' and " +
                   " status = 'Y' and b.program_gid ='" + values.program_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmasterdocument_list = new List<string>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmasterdocument_list.Add(dr_datarow["companydocument_gid"].ToString());
                }
            }
            dt_datatable.Dispose();

            msSQL = " select distinct(a.companydocument_gid) " +
                    " from agr_mst_tinstitution2documentupload a where a.documenttype_gid = 'DOCT2022010611' and " +
                    " (institution_gid='" + values.institution_gid + "' or institution_gid = '" + employee_gid + "')";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettransactiondocument_list = new List<string>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    gettransactiondocument_list.Add(dr_datarow["companydocument_gid"].ToString());
                }
            }
            dt_datatable.Dispose();

            var set1 = new HashSet<string>(getmasterdocument_list);
            var set2 = new HashSet<string>(gettransactiondocument_list);

            if (set1.SetEquals(set2) == false)
            {
                values.status = false;
                values.message = "Upload All KYC Documents";
                return false;
            }



            msSQL = "select institution_gid from agr_mst_tinstitution2mobileno where institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "' and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Primary Mobile Number";
                return false;
            }

            msSQL = "select institution_gid from agr_mst_tinstitution2mobileno where institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Atleast One Mobile Number";
                return false;
            }
            if (values.Gstflag == "Yes")
            {
                msSQL = "select institution_gid from agr_mst_tinstitution2email where institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == false)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Add Atleast One Email Address";
                    return false;
                }
            }
            if (values.Gstflag == "Yes")
            {
                msSQL = "select institution2branch_gid from agr_mst_tinstitution2branch where (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "') and headoffice_status ='Yes' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == false)
                {
                    values.status = false;
                    values.message = "Atleast Select One GST Number as Head Office";
                    return false;
                }
                objODBCDatareader.Close();

            }

            msSQL = "select institution_gid from agr_mst_tinstitution2address where institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Atleast One Address Detail";
                return false;
            }

            msSQL = "select user_type from ocs_mst_tusertype where usertype_gid='" + values.stakeholdertype_gid + "'";
             lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select application_gid from agr_mst_tinstitution where institution_gid='" + values.institution_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            if (lsstakeholder_type == "Borrower" || lsstakeholder_type == "Applicant")
            {
                msSQL = "select stakeholder_type from agr_mst_tcontact where application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Borrower','Applicant')";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Already Added Applicant/Borrower Information";
                    return false;
                }
                msSQL = "select stakeholder_type from agr_mst_tinstitution where application_gid='" + lsapplication_gid + "' and" +
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
                msSQL = " update agr_mst_tinstitution set " +
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
                         " stakeholder_type='" + lsstakeholder_type + "'," +
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
                    msSQL += "fixed_assets='" + values.sundrydebt_adv.Replace(",", "") + "',";
                }
                if (values.sundrydebt_adv == null || values.sundrydebt_adv == "")
                {
                    msSQL += "sundrydebt_adv='0.00',";
                }
                else
                {
                    msSQL += "sundrydebt_adv='" + values.sundrydebt_adv.Replace(",", "") + "',";
                }

                msSQL +=
                             " institution_status='Completed'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where institution_gid='" + values.institution_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {

                    // Updates for Multiple Add
                    msSQL = "update agr_mst_tinstitution2branch set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tinstitution2mobileno set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tinstitution2email set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tinstitution2address set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tinstitution2licensedtl set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tinstitution2bankdtl set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tinstitution2ratingdetail set institution_gid='" + values.institution_gid + "', application_gid ='" + lsapplication_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tkycgstsbpan set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tkycpanauthentication set function_gid='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_trn_ttandtl set function_gid='" + values.institution_gid + "' where function_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tkycifscauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tkycbankaccverification set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "select companydocument_gid , institution2documentupload_gid from agr_mst_tinstitution2documentupload where institution_gid='" + employee_gid + "'";
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

                    DaAgrMstScannedDocument objvalues = new DaAgrMstScannedDocument();
                    objvalues.DaGroupDocChecklistinfo(lsapplication_gid, values.institution_gid, employee_gid);

                    msSQL = "update agr_mst_tinstitution2documentupload set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tinstitution2form60documentupload set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tkycgstsbpan set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "select mobile_no from agr_mst_tinstitution2mobileno where institution_gid='" + values.institution_gid + "' and primary_status='yes'";
                    string lsmobileno = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select email_address from agr_mst_tinstitution2email where institution_gid='" + values.institution_gid + "' and primary_status='yes'";
                    lsemail_address = objdbconn.GetExecuteScalar(msSQL);

                    if (values.stakeholder_type == "Borrower" || values.stakeholder_type == "Applicant")
                    {
                        msSQL = "update agr_mst_tapplication set applicant_type ='Institution' where application_gid='" + lsapplication_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_mst_tinstitution set mobile_no='" + lsmobile_no + "'," +
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

        public void DaSaveIndividualEditDtl(MdlMstContact values, string employee_gid)
        {

            msSQL = "select application_gid from agr_mst_tcontact where contact_gid='" + values.contact_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            if (values.stakeholder_type == "Borrower" || values.stakeholder_type == "Applicant")
            {
                msSQL = "select stakeholder_type from agr_mst_tcontact where application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Borrower','Applicant')" +
                " and contact_gid<>'" + values.contact_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Already Added Applicant/Borrower Information";
                    return;
                }
                msSQL = "select stakeholder_type from agr_mst_tinstitution where application_gid='" + lsapplication_gid + "' and " +
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

            msSQL = "select pan_status from agr_mst_tcontact where contact_gid='" + values.contact_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (values.pan_status == "Customer Submitting PAN")
            {
                msSQL = "delete from agr_mst_tcontact2panform60 where contact_gid='" + values.contact_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "delete from agr_mst_tcontact2panabsencereason where contact_gid='" + values.contact_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            objODBCDatareader.Close();

            msSQL = " update agr_mst_tcontact set " +
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

                    msSQL = " select panabsencereason from agr_mst_tcontact2panabsencereason" +
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
                            msSQL = " INSERT INTO agr_mst_tcontact2panabsencereason(" +
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
                                msSQL = " INSERT INTO agr_mst_tcontact2panabsencereason(" +
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
                                msSQL = "delete from agr_mst_tcontact2panabsencereason where panabsencereason='" + values.contactpanabsencereason_list[i].panabsencereason + "' and contact_gid = '" + values.contact_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            matchCount2 = 0;
                        }
                    }
                }
                //Updates
                msSQL = "update agr_mst_tcontact2mobileno set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcontact2email set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcontact2address set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcontact2idproof set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcontact2panform60 set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcontact2panabsencereason set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpanauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpanaadhaarlink set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycdlauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycepicauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpassportauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycifscauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycbankaccverification set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select individualdocument_gid , contact2document_gid from agr_mst_tcontact2document where contact_gid='" + employee_gid + "'";
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
                        "'" + lsapplication_gid + "'," +
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
                       "'" + lsapplication_gid + "'," +
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

                DaAgrMstScannedDocument objvalues = new DaAgrMstScannedDocument();
                objvalues.DaGroupDocChecklistinfo(lsapplication_gid, values.contact_gid, employee_gid);

                msSQL = "update agr_mst_tcontact2document set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpanauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpanaadhaarlink set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycdlauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycepicauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpassportauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcontact2panform60 set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcontact2panabsencereason set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
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
        public void DaSaveIndividualDtlAdd(string employee_gid, MdlMstContact values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("CTCT");

            msSQL = "select stakeholder_type from agr_mst_tcontact where application_gid='" + values.application_gid + "' and stakeholder_type in ('Borrower','Applicant')";
             lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholdertype_name)
            {

                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return;
            }

            msSQL = "select stakeholder_type from agr_mst_tinstitution where application_gid='" + values.application_gid + "' and stakeholder_type in ('Borrower','Applicant')";
            lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholdertype_name)
            {

                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return;
            }
            msSQL = " insert into agr_mst_tcontact(" +
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
                // PAN Update
                foreach (string reason in values.panabsencereason_selectedlist)
                {
                    msGetGidpan = objcmnfunctions.GetMasterGID("C2PR");
                    msSQL = " INSERT INTO agr_mst_tcontact2panabsencereason(" +
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

                //Updates

                msSQL = "update agr_mst_tcontact2mobileno set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcontact2email set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcontact2address set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcontact2idproof set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcontact2panform60 set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcontact2panabsencereason set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcontact2bankdtl set contact_gid='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpanauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpanaadhaarlink set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycdlauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycepicauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpassportauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycifscauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycbankaccverification set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                msSQL = "select individualdocument_gid, contact2document_gid from agr_mst_tcontact2document where contact_gid='" + employee_gid + "'";
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
                DaAgrMstScannedDocument objvalues = new DaAgrMstScannedDocument();
                objvalues.DaGroupDocChecklistinfo(values.application_gid, msGetGid, employee_gid);

                msSQL = "update agr_mst_tcontact2document set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
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


                values.status = true;
                values.message = "Individual Details Saved Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }
        public void DaSubmitIndividualDtlAdd(string employee_gid, string user_gid, MdlMstContact values)
        {


            msSQL = " select a.individualdocument_gid from ocs_mst_tindividualdocument a" +
                   " left join ocs_mst_tindividualdocumentprogram b on a.individualdocument_gid = b.individualdocument_gid " +
                   " where  a.documenttypes_gid = 'DOCT2022010611'  and " +
                   " status = 'Y' and b.program_gid ='" + values.program_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmasterdocument_list = new List<string>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmasterdocument_list.Add(dr_datarow["individualdocument_gid"].ToString());
                }
            }
            dt_datatable.Dispose();

            msSQL = " select distinct(a.individualdocument_gid) " +
                    " from agr_mst_tcontact2document a where a.documenttype_gid = 'DOCT2022010611'  and " +
                    " contact_gid = '" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettransactiondocument_list = new List<string>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    gettransactiondocument_list.Add(dr_datarow["individualdocument_gid"].ToString());
                }
            }
            dt_datatable.Dispose();

            var set1 = new HashSet<string>(getmasterdocument_list);
            var set2 = new HashSet<string>(gettransactiondocument_list);

            if (set1.SetEquals(set2) == false)
            {
                values.status = false;
                values.message = "Upload All KYC Documents";
                return;
            }



            msGetGid = objcmnfunctions.GetMasterGID("CTCT");

            msSQL = "select stakeholder_type from agr_mst_tcontact where application_gid='" + values.application_gid + "' and stakeholder_type in ('Borrower','Applicant')";
             lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholdertype_name)
            {
                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return;
            }

            msSQL = "select stakeholder_type from agr_mst_tinstitution where application_gid='" + values.application_gid + "' and stakeholder_type in ('Borrower','Applicant')";
            lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholdertype_name)
            {
                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return;
            }

            msSQL = "select contact_gid from agr_mst_tcontact2mobileno where contact_gid='" + employee_gid + "' and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Mobile Number ";
                return;
            }
            objODBCDatareader.Close();
            msSQL = "select contact_gid from agr_mst_tcontact2email where contact_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add primary Email Address";
                return;
            }
            objODBCDatareader.Close();
            msSQL = "select contact_gid from agr_mst_tcontact2address where contact_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add primary Address";
                return;
            }
            objODBCDatareader.Close();

            //msSQL = "select contact_gid from agr_mst_tcontact2bankdtl where contact_gid='" + employee_gid + "' and primary_status='Yes'";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);

            //if (objODBCDatareader.HasRows == false)
            //{
            //    values.status = false;
            //    values.message = "Add Primary Bank Account Detail";
            //    return;
            //}
            //objODBCDatareader.Close();

            msSQL = " insert into agr_mst_tcontact(" +
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
                     "'Completed'," +
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
                        msSQL = " INSERT INTO agr_mst_tcontact2panabsencereason(" +
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

                msSQL = "update agr_mst_tcontact2mobileno set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcontact2email set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcontact2address set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcontact2idproof set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcontact2bankdtl set contact_gid='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpanauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpanaadhaarlink set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycdlauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycepicauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpassportauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycifscauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycbankaccverification set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                msSQL = "select individualdocument_gid, contact2document_gid from agr_mst_tcontact2document where contact_gid='" + employee_gid + "'";
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

                DaAgrMstScannedDocument objvalues = new DaAgrMstScannedDocument();
                objvalues.DaGroupDocChecklistinfo(values.application_gid, msGetGid, employee_gid);

                msSQL = "update agr_mst_tcontact2document set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
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

                msSQL = "select mobile_no from agr_mst_tcontact2mobileno where contact_gid='" + msGetGid + "' and primary_status='yes'";
                string lsmobileno = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select email_address from agr_mst_tcontact2email where contact_gid='" + msGetGid + "' and primary_status='yes'";
                lsemail_address = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "update agr_mst_tcontact2panform60 set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcontact2panabsencereason set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpanauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpanaadhaarlink set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycdlauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycepicauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpassportauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycifscauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycbankaccverification set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (values.stakeholdertype_name == "Borrower" || values.stakeholdertype_name == "Applicant")
                {
                    msSQL = "update agr_mst_tapplication set applicant_type ='Individual' where application_gid='" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tcontact set mobile_no='" + lsmobileno + "'," +
                        " email_address='" + lsemail_address + "' where contact_gid='" + msGetGid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (!string.IsNullOrEmpty(values.lspage))
                {

                    msSQL = " select onboarding_status from agr_mst_tapplication where application_gid ='" + values.application_gid + "' ";
                    string lsonboarding_status = objdbconn.GetExecuteScalar(msSQL);

                    DaAgrMstApplicationAdd objMstApplicationAdd = new DaAgrMstApplicationAdd();
                    objMstApplicationAdd.FnProgramBasedDcoument(values.application_gid, employee_gid, user_gid, lsonboarding_status, values.lspage);

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

        public void DaSocialAndTradeCapitalsubmit(string employee_gid, MdlMstApplicationAdd values)
        {

            msSQL = " update agr_mst_tapplication set ";
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
                      " where application_gid='" + values.application_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Social/Trade Capital details Submitted successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while submit Social/Trade Capital details";
            }
        }
        public void DaSubmitIndividualEditDtl(string employee_gid, MdlMstContact values)
        {


            msSQL = " select a.individualdocument_gid from ocs_mst_tindividualdocument a" +
                   " left join ocs_mst_tindividualdocumentprogram b on a.individualdocument_gid = b.individualdocument_gid " +
                  " where  a.documenttypes_gid = 'DOCT2022010611'  and " +
                  " status = 'Y' and b.program_gid ='" + values.program_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmasterdocument_list = new List<string>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmasterdocument_list.Add(dr_datarow["individualdocument_gid"].ToString());
                }
            }
            dt_datatable.Dispose();

            msSQL = " select distinct(a.individualdocument_gid) " +
                    " from agr_mst_tcontact2document a where a.documenttype_gid = 'DOCT2022010611' and " +
                    " ( contact_gid='" + values.contact_gid + "' or contact_gid = '" + employee_gid + "' )";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettransactiondocument_list = new List<string>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    gettransactiondocument_list.Add(dr_datarow["individualdocument_gid"].ToString());
                }
            }
            dt_datatable.Dispose();

            var set1 = new HashSet<string>(getmasterdocument_list);
            var set2 = new HashSet<string>(gettransactiondocument_list);

            if (set1.SetEquals(set2) == false)
            {
                values.status = false;
                values.message = "Upload All KYC Documents";
                return;
            }



            msSQL = "select application_gid from agr_mst_tcontact where contact_gid='" + values.contact_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            if (values.stakeholder_type == "Borrower" || values.stakeholder_type == "Applicant")
            {
                msSQL = "select stakeholder_type from agr_mst_tcontact where application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Borrower','Applicant')" +
                    " and contact_gid<>'" + values.contact_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Already Added Applicant/Borrower Information";
                    return;
                }
                msSQL = "select stakeholder_type from agr_mst_tinstitution where application_gid='" + lsapplication_gid + "' and " +
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
            msSQL = "select contact_gid from agr_mst_tcontact2mobileno where (contact_gid='" + employee_gid + "' or  contact_gid='" + values.contact_gid + "') and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Mobile Number ";
                return;
            }
            objODBCDatareader.Close();

            msSQL = "select contact_gid from agr_mst_tcontact2email where contact_gid='" + employee_gid + "' or  contact_gid='" + values.contact_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add primary Email Address";
                return;
            }
            objODBCDatareader.Close();
            msSQL = "select contact_gid from agr_mst_tcontact2address where contact_gid='" + employee_gid + "' or  contact_gid='" + values.contact_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add primary Address";
                return;
            }
            objODBCDatareader.Close();

            //msSQL = "select contact_gid from agr_mst_tcontact2bankdtl where contact_gid='" + employee_gid + "' or contact_gid='" + values.contact_gid + "' and primary_status='Yes'";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);

            //if (objODBCDatareader.HasRows == false)
            //{
            //    objODBCDatareader.Close();
            //    values.status = false;
            //    values.message = "Add Primary Bank Account Detail";
            //    return;
            //}
            //objODBCDatareader.Close();

            msSQL = "select pan_status from agr_mst_tcontact where contact_gid='" + values.contact_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (values.pan_status == "Customer Submitting PAN")
            {
                msSQL = "delete from agr_mst_tcontact2panform60 where contact_gid='" + values.contact_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "delete from agr_mst_tcontact2panabsencereason where contact_gid='" + values.contact_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }
            objODBCDatareader.Close();

            msSQL = " update agr_mst_tcontact set " +
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

                    msSQL = " select panabsencereason from agr_mst_tcontact2panabsencereason" +
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
                            msSQL = " INSERT INTO agr_mst_tcontact2panabsencereason(" +
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
                                msSQL = " INSERT INTO agr_mst_tcontact2panabsencereason(" +
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
                                msSQL = "delete from agr_mst_tcontact2panabsencereason where panabsencereason='" + values.contactpanabsencereason_list[i].panabsencereason + "' and contact_gid = '" + values.contact_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            matchCount2 = 0;
                        }
                    }
                }
                //Updates
                msSQL = "update agr_mst_tcontact2mobileno set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcontact2email set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcontact2address set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcontact2idproof set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcontact2panform60 set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcontact2panabsencereason set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcontact2bankdtl set contact_gid='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpanauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpanaadhaarlink set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycdlauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycepicauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpassportauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycifscauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycbankaccverification set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                msSQL = "select individualdocument_gid ,  contact2document_gid from agr_mst_tcontact2document where contact_gid='" + employee_gid + "'";
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
                        "'" + lsapplication_gid + "'," +
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
                       "'" + lsapplication_gid + "'," +
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

                DaAgrMstScannedDocument objvalues = new DaAgrMstScannedDocument();
                objvalues.DaGroupDocChecklistinfo(lsapplication_gid, values.contact_gid, employee_gid);

                msSQL = "update agr_mst_tcontact2document set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpanauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpanaadhaarlink set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycdlauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycepicauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycpassportauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcontact2panform60 set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcontact2panabsencereason set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select mobile_no from agr_mst_tcontact2mobileno where contact_gid='" + values.contact_gid + "' and primary_status='yes'";
                string lsmobileno = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select email_address from agr_mst_tcontact2email where contact_gid='" + values.contact_gid + "' and primary_status='yes'";
                lsemail_address = objdbconn.GetExecuteScalar(msSQL);

                if (values.stakeholder_type == "Borrower" || values.stakeholder_type == "Applicant")
                {
                    msSQL = "update agr_mst_tapplication set applicant_type ='Individual' where application_gid='" + lsapplication_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tcontact set mobile_no='" + lsmobileno + "'," +
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

        public void DaCICUploadIndividualDocList(string contact2bureau_gid, string employee_gid, MdlCICIndividual values)
        {
            msSQL = " select individual2cicdocumentupload_gid,contact2bureau_gid,cicdocument_name,cicdocument_path,document_content from agr_mst_tindividual2cicdocumentupload " +
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
                        document_path = objcmnstorage.EncryptData((dt["cicdocument_path"].ToString())),
                        contact2bureau_gid = dt["contact2bureau_gid"].ToString(),
                        tmpcicdocument_gid = dt["individual2cicdocumentupload_gid"].ToString(),
                        document_content = dt["document_content"].ToString(),
                    });
                    values.cicuploaddoc_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaCICUploadInstitutionDocList(string institution2bureau_gid, string employee_gid, MdlCICInstitution values)
        {
            msSQL = " select institution2cicdocumentupload_gid, institution2bureau_gid,cicdocument_name,cicdocument_path,document_content from agr_mst_tinstitution2cicdocumentupload " +
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
                        document_path = objcmnstorage.EncryptData((dt["cicdocument_path"].ToString())),
                        institution2bureau_gid = dt["institution2bureau_gid"].ToString(),
                        tmpcicdocument_gid = dt["institution2cicdocumentupload_gid"].ToString(),
                        document_content = dt["document_content"].ToString(),
                    });
                    values.cicuploaddoc_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();

        }

        public void DaCICUploadIndividualDocDelete(string tmpcicdocument_gid, MdlCICIndividual values)
        {
            msSQL = " delete from agr_mst_tindividual2cicdocumentupload where individual2cicdocumentupload_gid='" + tmpcicdocument_gid + "'";
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

        public void DaCICUploadInstitutionDocDelete(string tmpcicdocument_gid, MdlCICIndividual values)
        {
            msSQL = " delete from agr_mst_tinstitution2cicdocumentupload where institution2cicdocumentupload_gid='" + tmpcicdocument_gid + "'";
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
        public void DaGetEditProductcharges(string application_gid, MdlMstApplicationAdd values, string employee_gid)
        {
            try
            {

                msSQL = "select application_gid,overalllimit_amount,processing_fee,doc_charges, applicant_type, productcharge_flag, economical_flag, program_gid," +
                        " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as created_date,applicant_type,status,sa_status,productcharges_status,hypothecation_flag, " +
                        " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, onboarding_status " +
                        " from agr_mst_tapplication a left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid where application_gid='" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.overalllimit_amount = objODBCDatareader["overalllimit_amount"].ToString();
                    values.processing_fee = objODBCDatareader["processing_fee"].ToString();
                    values.doc_charges = objODBCDatareader["doc_charges"].ToString();
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.applicant_type = objODBCDatareader["applicant_type"].ToString();
                    values.economical_flag = objODBCDatareader["economical_flag"].ToString();
                    values.productcharge_flag = objODBCDatareader["productcharge_flag"].ToString();
                    values.created_date = objODBCDatareader["created_date"].ToString();
                    values.created_by = objODBCDatareader["created_by"].ToString();
                    values.applicant_type = objODBCDatareader["applicant_type"].ToString();
                    values.application_status = objODBCDatareader["status"].ToString();
                    values.productcharges_status = objODBCDatareader["productcharges_status"].ToString();
                    values.hypothecation_flag = objODBCDatareader["hypothecation_flag"].ToString();
                    values.sa_status = objODBCDatareader["sa_status"].ToString();
                    values.onboarding_status = objODBCDatareader["onboarding_status"].ToString();
                    values.program_gid = objODBCDatareader["program_gid"].ToString();

                }
                objODBCDatareader.Close();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaPostEditLoanDtl(string employee_gid, MdlMstLoanDtl values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("AP2L");
            msSQL = " insert into agr_mst_tapplication2loan(" +
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
                   " facility_type," +
                   " facility_mode," +
                       " principalfrequency_name," +
                       " principalfrequency_gid," +
                       " interestfrequency_name," +
                       " interestfrequency_gid," +
                       " interest_status," +
                       " moratorium_status," +
                       " moratorium_type," +
                       " moratorium_startdate," +
                       " moratorium_enddate," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + employee_gid + "'," +
                   "'" + Convert.ToDateTime(values.facilityrequested_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   "'" + values.product_type + "'," +
                   "'" + values.producttype_gid + "'," +
                   "'" + values.productsub_type + "'," +
                   "'" + values.productsubtype_gid + "'," +
                   "'" + values.loantype_gid + "'," +
                   "'" + values.loan_type + "'," +
                   "'" + values.facilityloan_amount + "'," +
                   "'" + values.rate_interest + "'," +
                   "'" + values.penal_interest + "'," +
                   "'" + values.facilityvalidity_year + "'," +
                   "'" + values.facilityvalidity_month + "'," +
                   "'" + values.facilityvalidity_days + "'," +
                   "'" + values.facilityoverall_limit + "'," +
                   "'" + values.tenureproduct_year + "'," +
                   "'" + values.tenureproduct_month + "'," +
                   "'" + values.tenureproduct_days + "'," +
                   "'" + values.tenureoverall_limit + "'," +
                   "'" + values.facility_type + "'," +
                   "'" + values.facility_mode + "'," +
                    "'" + values.principalfrequency_name + "'," +
                   "'" + values.principalfrequency_gid + "'," +
                   "'" + values.interestfrequency_name + "'," +
                   "'" + values.interestfrequency_gid + "'," +
                   "'" + values.interest_status + "'," +
                   "'" + values.moratorium_status + "'," +
                   "'" + values.moratorium_type + "',";
            if (values.moratorium_startdate == null || values.moratorium_startdate == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.moratorium_startdate).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if (values.moratorium_enddate == null || values.moratorium_enddate == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.moratorium_enddate).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Loan details Added successfully";



            }
            else
            {
                values.status = false;
                values.message = "Error Occured while Adding Loan";
            }
        }

        public void DaSaveEditProductCharges(string employee_gid, MdlProductCharges values)
        {


            msSQL = " update agr_mst_tapplication set " +
                  " overalllimit_amount='" + values.overalllimit_amount + "'," +
                  " validityoveralllimit_year='" + values.validityoveralllimit_year + "'," +
                  " validityoveralllimit_month='" + values.validityoveralllimit_month + "'," +
                  " validityoveralllimit_days='" + values.validityoveralllimit_days + "'," +
                  " calculationoveralllimit_validity='" + values.calculationoveralllimit_validity + "',";
            if (values.enduse_purpose == null || values.enduse_purpose == "")
            {
                msSQL += " enduse_purpose='',";
            }
            else
            {
                msSQL += " enduse_purpose='" + values.enduse_purpose.Replace("'", "") + "',";
            }
            msSQL += " processing_fee='" + values.processing_fee + "'," +
             " processing_collectiontype='" + values.processing_collectiontype + "'," +
             " doc_charges='" + values.doc_charges + "'," +
             " doccharge_collectiontype='" + values.doccharge_collectiontype + "'," +
             " fieldvisit_charge='" + values.fieldvisit_charge + "'," +
             " fieldvisit_collectiontype='" + values.fieldvisit_collectiontype + "'," +
             " adhoc_fee='" + values.adhoc_fee + "'," +
             " adhoc_collectiontype='" + values.adhoc_collectiontype + "'," +
             " life_insurance='" + values.life_insurance + "'," +
             " lifeinsurance_collectiontype='" + values.lifeinsurance_collectiontype + "'," +
             " acct_insurance='" + values.acct_insurance + "'," +
             " total_collect='" + values.total_collect + "'," +
             " total_deduct='" + values.total_deduct + "'," +
             " productcharge_flag='Y'," +
             " productcharges_status='Incomplete'," +
             " updated_by='" + employee_gid + "'," +
             " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
             " where application_gid='" + values.application_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = " update agr_mst_tapplication2loan set application_gid='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //msSQL = " update agr_mst_tapplication2buyer set application_gid='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update agr_mst_tapplication2collateral set application_gid='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update agr_mst_tapplication2hypothecation set application_gid='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                values.status = true;
                values.message = "Product&Charges Details Saved Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }

        }

        public void DaSubmitEditProductCharges(string employee_gid, MdlProductCharges values)
        {



            msSQL = "select application_gid from agr_mst_tapplication2loan where (application_gid='" + employee_gid + "' or application_gid='" + values.application_gid + "' ) ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Kindly add Loan Details";
                return;
            }
            objODBCDatareader.Close();

            msSQL = "select application_gid from agr_mst_tapplication2loan where loan_type='Secured' and " +
            "(application_gid='" + employee_gid + "' or application_gid='" + values.application_gid + "' ) ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                msSQL = "select application_gid from agr_mst_tapplication2collateral where (application_gid='" + employee_gid + "' or application_gid='" + values.application_gid + "' ) ";
                objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader1.HasRows == false)
                {
                    values.status = false;
                    values.message = "Kindly add Collateral Details";
                    return;
                }
                objODBCDatareader1.Close();
            }
            objODBCDatareader.Close();

            msSQL = " update agr_mst_tapplication set " +
                  " overalllimit_amount='" + values.overalllimit_amount + "'," +
                  " validityoveralllimit_year='" + values.validityoveralllimit_year + "'," +
                  " validityoveralllimit_month='" + values.validityoveralllimit_month + "'," +
                  " validityoveralllimit_days='" + values.validityoveralllimit_days + "'," +
                  " calculationoveralllimit_validity='" + values.calculationoveralllimit_validity + "',";
            if (values.enduse_purpose == null || values.enduse_purpose == "")
            {
                msSQL += " enduse_purpose='',";
            }
            else
            {
                msSQL += " enduse_purpose='" + values.enduse_purpose.Replace("'", "") + "',";
            }
            msSQL += " processing_fee='" + values.processing_fee + "'," +
                   " processing_collectiontype='" + values.processing_collectiontype + "'," +
                   " doc_charges='" + values.doc_charges + "'," +
                   " doccharge_collectiontype='" + values.doccharge_collectiontype + "'," +
                   " fieldvisit_charge='" + values.fieldvisit_charge + "'," +
                   " fieldvisit_collectiontype='" + values.fieldvisit_collectiontype + "'," +
                   " adhoc_fee='" + values.adhoc_fee + "'," +
                   " adhoc_collectiontype='" + values.adhoc_collectiontype + "'," +
                   " life_insurance='" + values.life_insurance + "'," +
                   " lifeinsurance_collectiontype='" + values.lifeinsurance_collectiontype + "'," +
                   " acct_insurance='" + values.acct_insurance + "'," +
                   " total_collect='" + values.total_collect + "'," +
                   " total_deduct='" + values.total_deduct + "'," +
                   " productcharge_flag='Y'," +
                   " productcharges_status='Completed'," +
                   " updated_by='" + employee_gid + "'," +
                   " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                   " where application_gid='" + values.application_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = " update agr_mst_tapplication2loan set application_gid='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //msSQL = " update agr_mst_tapplication2buyer set application_gid='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update agr_mst_tapplication2collateral set application_gid='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update agr_mst_tapplication2hypothecation set application_gid='" + lsapplication_gid + "' where application_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                values.status = true;
                values.message = "Product&Charges Details Submitted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }

        }

        public void DaPostEditHypothecation(string employee_gid, MdlMstHypothecation values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("AP2C");
            msSQL = " insert into agr_mst_tapplication2hypothecation(" +
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



                msSQL = "update agr_mst_tuploadhypothecationocument set application2hypothecation_gid='" + msGetGid + "' where application2hypothecation_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



                msSQL = "update agr_mst_tapplication set hypothecation_flag='Y' where application_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



                msSQL = "select application2hypothecation_gid,securitytype_gid,security_type,security_description,security_value," +
                    " date_format(securityassessed_date,'%d-%m-%Y') as securityassessed_date,asset_id,roc_fillingid,CERSAI_fillingid," +
                    " hypoobservation_summary,primary_security " +
                    " from agr_mst_tapplication2hypothecation where application_gid='" + values.application_gid + "'";
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
                msSQL = " select hypothecationdocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                       " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by,a.document_title " +
                       " from agr_mst_tuploadhypothecationocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
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
                            document_gid = (dr_datarow["hypothecationdocument_gid"].ToString()),
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
                values.message = "Error Occured While Adding Hypothecation";
            }
        }

        public void DaPostEditCollateral(string employee_gid, MdlMstCollatertal values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("AP2C");
            msSQL = " insert into agr_mst_tapplication2collateral(" +
                   " application2collateral_gid ," +
                   " application_gid," +
                   " source_type," +
                   " guideline_value," +
                   " guideline_date," +
                   " marketvalue_date ," +
                   " market_value," +
                   " forcedsource_value," +
                   " collateralSSV_value," +
                   " forcedvalueassessed_on," +
                   " collateralobservation_summary," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.application_gid + "'," +
                   "'" + values.source_type + "',";
            if (values.guideline_value == null || values.guideline_value == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.guideline_value.Replace(",", "") + "',";
            }
            if (values.guideline_date == null || values.guideline_date == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.guideline_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if (values.marketvalue_date == null || values.marketvalue_date == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.marketvalue_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if (values.market_value == null || values.market_value == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.market_value.Replace(",", "") + "',";
            }
            if (values.forcedsource_value == null || values.forcedsource_value == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.forcedsource_value.Replace(",", "") + "',";
            }
            if (values.collateralSSV_value == null || values.collateralSSV_value == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.collateralSSV_value.Replace(",", "") + "',";
            }
            if (values.forcedvalueassessed_on == null || values.forcedvalueassessed_on == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.forcedvalueassessed_on).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if (values.collateralobservation_summary == null || values.collateralobservation_summary == "")
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.collateralobservation_summary.Replace("'", "") + "',";
            }
            msSQL += "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Collateral Details Added Successfully";

                msSQL = "update agr_mst_tuploadcollateraldocument set application2collateral_gid='" + msGetGid + "' where application2collateral_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select application2collateral_gid,source_type,guideline_value,market_value,forcedsource_value,collateralSSV_value," +
                    " date_format(guideline_date,'%d-%m-%Y') as guideline_date,date_format(forcedvalueassessed_on,'%d-%m-%Y') as forcedvalueassessed_on," +
                    " date_format(marketvalue_date,'%d-%m-%Y') as marketvalue_date,collateralobservation_summary " +
                    " from agr_mst_tapplication2collateral where application_gid='" + values.application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcollatertal_list = new List<collatertal_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcollatertal_list.Add(new collatertal_list
                        {
                            application2collateral_gid = (dr_datarow["application2collateral_gid"].ToString()),
                            source_type = (dr_datarow["source_type"].ToString()),
                            guideline_value = (dr_datarow["guideline_value"].ToString()),
                            market_value = (dr_datarow["market_value"].ToString()),
                            forcedsource_value = (dr_datarow["forcedsource_value"].ToString()),
                            collateralSSV_value = (dr_datarow["collateralSSV_value"].ToString()),
                            collateralobservation_summary = (dr_datarow["collateralobservation_summary"].ToString()),
                            guideline_date = (dr_datarow["guideline_date"].ToString()),
                            forcedvalueassessed_on = (dr_datarow["forcedvalueassessed_on"].ToString()),
                            marketvalue_date = (dr_datarow["marketvalue_date"].ToString()),
                        });
                    }
                    values.collatertal_list = getcollatertal_list;
                }
                dt_datatable.Dispose();
                msSQL = " select collateraldocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                      " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by,a.document_title " +
                      " from agr_mst_tuploadcollateraldocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                      " and b.user_gid = c.user_gid and application2collateral_gid='" + employee_gid + "'";

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
                            document_gid = (dr_datarow["collateraldocument_gid"].ToString()),
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
                values.message = "Error Occured While Adding";
            }
        }

        public void DaGroupAddressList(string group_gid, MdlMstAddressDetails values)
        {
            msSQL = "  select group2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, latitude, longitude, landmark," +
                    " postal_code from agr_mst_tgroup2address where group_gid='" + group_gid + "'";
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
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString()),
                        landmark = (dr_datarow["landmark"].ToString())
                    });
                }
                values.mstaddress_list = getmstaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGroupAddressTmpList(string group_gid, string employee_gid, MdlMstAddressDetails values)
        {
            msSQL = "  select group2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, latitude, longitude, landmark," +
                    " postal_code from agr_mst_tgroup2address where group_gid='" + group_gid + "' or group_gid='" + employee_gid + "'";
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
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
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
                    " from agr_mst_tgroup2bank where group_gid='" + group_gid + "'";
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

        public void DaGroupBankTmpList(string group_gid, string employee_gid, MdlMstBankDetails values)
        {
            msSQL = "  select group2bank_gid,ifsc_code,bank_accountno, accountholder_name, bank_name, bank_branch" +
                    " from agr_mst_tgroup2bank where group_gid='" + group_gid + "' or group_gid='" + employee_gid + "'";
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
            msSQL = " select group2document_gid,document_name,document_title,document_path from agr_mst_tgroup2document " +
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
            msSQL = " select group2document_gid,document_name,document_title,document_path from agr_mst_tgroup2document " +
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
                msSQL = " select group_name,date_of_formation,group_type,groupmember_count,groupurn_status,group_urn,group_status" +
                        " from agr_mst_tgroup where group_gid='" + group_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.group_name = objODBCDatareader["group_name"].ToString();
                    if (objODBCDatareader["date_of_formation"].ToString() != "")
                        values.date_of_formation = Convert.ToDateTime(objODBCDatareader["date_of_formation"]).ToString("dd-MM-yyyy");
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


        public void DaSaveGroupDtlAdd(string employee_gid, MdlMstGroup values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("GRUP");

            msSQL = " insert into agr_mst_tgroup(" +
                   " group_gid," +
                   " application_gid," +
                   " group_name," +
                   " date_of_formation," +
                   " group_type," +
                   " groupmember_count," +
                   " groupurn_status," +
                   " group_urn," +
                   " group_status," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.application_gid + "',";

            if (values.group_name == "" || values.group_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.group_name.Replace("'", "") + "',";
            }
            if ((values.date_of_formation == null) || (values.date_of_formation == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.date_of_formation).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }

            msSQL += "'" + values.group_type + "'," +
                     "'" + values.groupmember_count + "'," +
                     "'" + values.groupurn_status + "'," +
                     "'" + values.group_urn + "'," +
                     "'Incomplete'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //}

            //objODBCDatareader.Close();

            if (mnResult != 0)
            {

                //Updates

                msSQL = "update agr_mst_tgroup2address set group_gid ='" + msGetGid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tgroup2bank set group_gid ='" + msGetGid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tgroup2document set group_gid ='" + msGetGid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycifscauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycbankaccverification set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                values.status = true;
                values.message = "Group Details Saved Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while saving Group Details..!";
            }
        }

        public void DaSaveGroupDtlEdit(MdlMstGroup values, string employee_gid)
        {
            msSQL = "select application_gid from agr_mst_tgroup where group_gid='" + values.group_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " update agr_mst_tgroup set ";

            if (values.group_name == "" || values.group_name == null)
            {

            }
            else
            {
                msSQL += " group_name='" + values.group_name.Replace("'", "") + "',";
            }


            if (Convert.ToDateTime(values.date_of_formation).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += " date_of_formation='" + Convert.ToDateTime(values.date_of_formation).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += " group_type='" + values.group_type + "'," +
                     " groupmember_count='" + values.groupmember_count + "'," +
                   " groupurn_status='" + values.groupurn_status + "'," +
                   " group_urn='" + values.group_urn + "'," +
                   " group_status='Incomplete'," +
                   " updated_by='" + employee_gid + "'," +
                   " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                   " where group_gid='" + values.group_gid + "' ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                //Updates
                msSQL = "update agr_mst_tgroup2address set group_gid ='" + values.group_gid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tgroup2bank set group_gid ='" + values.group_gid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select groupdocument_gid, group2document_gid from agr_mst_tgroup2document where group_gid='" + employee_gid + "'";
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
                    msSQL = " insert into agr_trn_tdocumentchecktls(" +
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
                        "'" + lsapplication_gid + "'," +
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
                        msSQL = " insert into agr_trn_tcovanantdocumentcheckdtls(" +
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
                       "'" + lsapplication_gid + "'," +
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

                DaAgrMstScannedDocument objvalues = new DaAgrMstScannedDocument();
                objvalues.DaGroupDocChecklistinfo(lsapplication_gid, values.group_gid, employee_gid);

                msSQL = "update agr_mst_tgroup2document set group_gid ='" + values.group_gid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



                values.status = true;
                values.message = "Group Details Saved Successfully";
            }

            else
            {
                values.status = false;
                values.message = "Error Occured while saving Group Details..!";
            }
        }


        public void DaSubmitGroupDtlAdd(string employee_gid, MdlMstGroup values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("GRUP");

            msSQL = "select group_gid from agr_mst_tgroup2address where group_gid='" + employee_gid + "' and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Primary Address ";
                return;
            }

            msSQL = "select group_gid from agr_mst_tgroup2bank where group_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Atleast one Bank detail ";
                return;
            }

            msSQL = "select group_gid from agr_mst_tgroup2document where group_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Atleast one Document detail ";
                return;
            }

            msSQL = " insert into agr_mst_tgroup(" +
                   " group_gid," +
                   " application_gid," +
                   " group_name," +
                   " date_of_formation," +
                   " group_type," +
                   " groupmember_count," +
                   " groupurn_status," +
                   " group_urn," +
                   " group_status," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.application_gid + "',";

            if (values.group_name == "" || values.group_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.group_name.Replace("'", "") + "',";
            }
            if ((values.date_of_formation == null) || (values.date_of_formation == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.date_of_formation).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }

            msSQL += "'" + values.group_type + "'," +
                     "'" + values.groupmember_count + "'," +
                     "'" + values.groupurn_status + "'," +
                     "'" + values.group_urn + "'," +
                     "'Completed'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);




            if (mnResult != 0)
            {

                //Updates

                msSQL = "update agr_mst_tgroup2address set group_gid ='" + msGetGid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tgroup2bank set group_gid ='" + msGetGid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select group2document_gid, groupdocument_gid from agr_mst_tgroup2document where group_gid='" + employee_gid + "'";
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
                    msSQL = " insert into agr_trn_tdocumentchecktls(" +
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
                        "'" + msGetGid + "'," +
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
                        msSQL = " insert into agr_trn_tcovanantdocumentcheckdtls(" +
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
                       "'" + msGetGid + "'," +
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

                DaAgrMstScannedDocument objvalues = new DaAgrMstScannedDocument();
                objvalues.DaGroupDocChecklistinfo(values.application_gid, msGetGid, employee_gid);

                msSQL = "update agr_mst_tgroup2document set group_gid ='" + msGetGid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycifscauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycbankaccverification set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Group Details Submitted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while submitting Group Details..!";
            }
        }

        public void DaSubmitGroupDtlEdit(string employee_gid, MdlMstGroup values)
        {
            msSQL = "select application_gid from agr_mst_tgroup where group_gid='" + values.group_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select group_gid from agr_mst_tgroup2address where (group_gid='" + employee_gid + "' or  group_gid='" + values.group_gid + "')" + " and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Primary Address ";
                return;
            }

            msSQL = "select group_gid from agr_mst_tgroup2bank where group_gid='" + employee_gid + "' or  group_gid='" + values.group_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Atleast one Bank detail ";
                return;
            }

            msSQL = "select group_gid from agr_mst_tgroup2document where group_gid='" + employee_gid + "' or  group_gid='" + values.group_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Atleast one Document detail ";
                return;
            }

            msSQL = " update agr_mst_tgroup set ";

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
                msSQL += " date_of_formation='" + Convert.ToDateTime(values.dateofformation).AddDays(1).ToString("yyyy-MM-dd HH:mm:ss") + "',";
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
                msSQL = "update agr_mst_tgroup2address set group_gid ='" + values.group_gid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tgroup2bank set group_gid ='" + values.group_gid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select groupdocument_gid, group2document_gid from agr_mst_tgroup2document where group_gid='" + employee_gid + "'";
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
                    msSQL = " insert into agr_trn_tdocumentchecktls(" +
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
                        "'" + lsapplication_gid + "'," +
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
                        msSQL = " insert into agr_trn_tcovanantdocumentcheckdtls(" +
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
                       "'" + lsapplication_gid + "'," +
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

                DaAgrMstScannedDocument objvalues = new DaAgrMstScannedDocument();
                objvalues.DaGroupDocChecklistinfo(lsapplication_gid, values.group_gid, employee_gid);

                msSQL = "update agr_mst_tgroup2document set group_gid ='" + values.group_gid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycifscauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tkycbankaccverification set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Group Details Submitted Successfully";
            }

            else
            {
                values.status = false;
                values.message = "Error Occured while submitting Group Details..!";
            }
        }

        public void DaGetGroupSummary(string application_gid, MdlMstGroup values)
        {
            msSQL = " select a.group_gid,a.group_name,date_format(a.date_of_formation,'%d-%m-%Y') as date_of_formation,a.group_status, a.group_type," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,credit_status," +
                    " (select count(*) from agr_trn_tgroupdocumentchecklist where credit_gid =a.group_gid " +
                    " and(untagged_type is null or untagged_type = 'N')) as OverallDeferralcount, " +
                    " (select count(*) from agr_trn_tgroupcovenantdocumentchecklist where credit_gid = a.group_gid " +
                    " and(untagged_type is null or untagged_type = 'N')) as overallCovenantCount, " +
                    " (select count(*) from agr_trn_tgroupdocumentchecklist where credit_gid = a.group_gid " +
                    " and(untagged_type is null or untagged_type = 'N') and overall_docstatus in ('Waived','Document Verified')) as verifieddeferraldoc, " +
                    " (select count(*) from agr_trn_tgroupcovenantdocumentchecklist where credit_gid = a.group_gid " +
                    " and(untagged_type is null or untagged_type = 'N') and overall_docstatus in ('Waived','Document Verified'))  as verifiedcovenantdoc " +
                    " from agr_mst_tgroup a " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " where application_gid='" + application_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getgroup_list = new List<group_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getgroup_list.Add(new group_list
                    {
                        group_gid = (dr_datarow["group_gid"].ToString()),
                        group_name = (dr_datarow["group_name"].ToString()),
                        date_of_formation = (dr_datarow["date_of_formation"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        group_status = (dr_datarow["group_status"].ToString()),
                        group_type = (dr_datarow["group_type"].ToString()),
                        credit_status = (dr_datarow["credit_status"].ToString()),
                        overallCovenantCount = (dr_datarow["overallCovenantCount"].ToString()),
                        OverallDeferralcount = (dr_datarow["OverallDeferralcount"].ToString()),
                        verifieddeferraldoc = (dr_datarow["verifieddeferraldoc"].ToString()),
                        verifiedcovenantdoc = (dr_datarow["verifiedcovenantdoc"].ToString()),
                    });
                }
            }
            values.group_list = getgroup_list;
            dt_datatable.Dispose();
        }

        public void DaUpdateGroupDtl(string employee_gid, MdlMstGroup values)
        {


            msSQL = "select group_gid from agr_mst_tgroup2address where (group_gid='" + employee_gid + "' or  group_gid='" + values.group_gid + "')" + " and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Primary Address ";
                return;
            }

            msSQL = "select group_gid from agr_mst_tgroup2bank where group_gid='" + employee_gid + "' or  group_gid='" + values.group_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Atleast one Bank detail ";
                return;
            }

            msSQL = "select group_gid from agr_mst_tgroup2document where group_gid='" + employee_gid + "' or  group_gid='" + values.group_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Atleast one Document detail ";
                return;
            }

            msSQL = " update agr_mst_tgroup set ";

            if (values.group_name == "" || values.group_name == null)
            {

            }
            else
            {
                msSQL += " group_name='" + values.group_name.Replace("'", "") + "',";
            }


            if (Convert.ToDateTime(values.date_of_formation).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += " date_of_formation='" + Convert.ToDateTime(values.date_of_formation).ToString("yyyy-MM-dd HH:mm:ss") + "',";
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
                msSQL = "update agr_mst_tgroup2address set group_gid ='" + values.group_gid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tgroup2bank set group_gid ='" + values.group_gid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select application_gid from agr_mst_tgroup where group_gid = '" + values.group_gid + "' ";
                string lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select groupdocument_gid, group2document_gid from agr_mst_tgroup2document where group_gid='" + employee_gid + "'";
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
                    msSQL = " insert into agr_trn_tdocumentchecktls(" +
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
                        "'" + lsapplication_gid + "'," +
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
                        msSQL = " insert into agr_trn_tcovanantdocumentcheckdtls(" +
                       " covenantdocumentcheckdtl_gid," +
                       " application_gid," +
                       " credit_gid," +
                       " groupdocument_gid," +
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
                       "'" + values.group_gid + "'," +
                       "'" + dt["groupdocument_gid"].ToString() + "'," +
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
                DaAgrMstScannedDocument objvalues = new DaAgrMstScannedDocument();
                objvalues.DaGroupDocChecklistinfo(lsapplication_gid, values.group_gid, employee_gid);

                msSQL = "update agr_mst_tgroup2document set group_gid ='" + values.group_gid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Group Details Updated Successfully";
            }

            else
            {
                values.status = false;
                values.message = "Error Occured while updating Group Details..!";
            }
        }

        public void DaGetPANForm60TempList(string contact_gid, string employee_gid, MdlContactPANForm60 values)
        {
            msSQL = "select contact2panform60_gid,document_name, document_path from agr_mst_tcontact2panform60 where " +
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
            msSQL = "select contact2panform60_gid,document_name, document_path from agr_mst_tcontact2panform60 where " +
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
                   " from agr_mst_tcontact2panabsencereason where contact_gid = '" + contact_gid + "' or contact_gid = '" + employee_gid + "'";

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

                msSQL = " select panabsencereason from agr_mst_tcontact2panabsencereason" +
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
                        msSQL = " INSERT INTO agr_mst_tcontact2panabsencereason(" +
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
                        msSQL = "delete from agr_mst_tcontact2panabsencereason where panabsencereason='" + values.contactpanabsencereason_list[i].panabsencereason + "' and contact_gid = '" + values.contact_gid + "'";
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
                   " from agr_mst_tcontact2panabsencereason where contact_gid = '" + contact_gid + "'";

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


        public void DaGetAppProductList(string application_gid, string employee_gid, MdlMstProductDetailList values)
        {
            msSQL = " select application2product_gid,product_gid,product_name,variety_gid,variety_name,sector_name,category_name,hsn_code, " +
                    " botanical_name,alternative_name from agr_mst_tapplication2product where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstproduct_list = new List<mstproduct_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstproduct_list.Add(new mstproduct_list
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
                values.mstproduct_list = getmstproduct_list;
            }
            dt_datatable.Dispose();
        }

        public void DaDeleteAppProductDtl(string application2product_gid, MdlMstMobileNo values)
        {
            msSQL = "delete from agr_mst_tapploan2paymenttypecustomer where application2product_gid='" + application2product_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tapplication2product where application2product_gid='" + application2product_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {

                values.message = "Product Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }
        public void DaGetAppProductTempList(string application_gid, string employee_gid, MdlMstProductDetailList values)
        {
            msSQL = " select application2product_gid,product_gid,product_name,variety_gid,variety_name,sector_name,category_name,hsn_code, " +
                    " botanical_name,alternative_name from agr_mst_tapplication2product " +
                    " where application_gid = '" + employee_gid + "' or application_gid = '" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstproduct_list = new List<mstproduct_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstproduct_list.Add(new mstproduct_list
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
                values.mstproduct_list = getmstproduct_list;
            }
            dt_datatable.Dispose();
        }


        public void DaGetWarehouseAddressDropdown(string warehouse_gid, MdlWarehouseAddressList values)
        {
            msSQL = " select warehouse2address_gid, REPLACE(concat(addresstype_name, ' - ', addressline1, " +
                    " addressline2, ',', taluka, ',', district, ',', state, ',',country, ',', landmark, ',', postal_code), ',,' , ',') as warehouseaddress " +
                    " from agr_mst_twarehouse2address where warehouse_gid='" + warehouse_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstaddress_list = new List<MdlWarehouseAddressdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstaddress_list.Add(new MdlWarehouseAddressdtl
                    {
                        warehouse2address_gid = (dr_datarow["warehouse2address_gid"].ToString()),
                        warehouseaddress_name = (dr_datarow["warehouseaddress"].ToString()),
                    });
                }
                values.MdlWarehouseAddressdtl = getmstaddress_list;
            }
            dt_datatable.Dispose();
        }


        public void DaGetWarehouseDetail(MdlWarehousedtlList values)
        {
            msSQL = " select warehouse_gid,warehouse_name,typeofwarehouse_gid, typeofwarehouse_name, warehouse_area,warehousearea_uom,warehousearea_uomgid, " +
                    " totalcapacity_area, totalcapacityarea_uomgid, area_uom as totalcapacityarea_uom, totalcapacity_volume,volume_uomgid,volume_uom " +
                    " from agr_mst_twarehouse order by warehouse_name asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstaddress_list = new List<MdlWarehousedtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstaddress_list.Add(new MdlWarehousedtl
                    {
                        warehouse_gid = (dr_datarow["warehouse_gid"].ToString()),
                        warehouse_name = (dr_datarow["warehouse_name"].ToString()),
                        warehousearea_uom = (dr_datarow["warehousearea_uom"].ToString()),
                        warehousearea_uomgid = (dr_datarow["warehousearea_uomgid"].ToString()),
                        totalcapacity_area = (dr_datarow["totalcapacity_area"].ToString()),
                        totalcapacityarea_uomgid = (dr_datarow["totalcapacityarea_uomgid"].ToString()),
                        totalcapacityarea_uom = (dr_datarow["totalcapacityarea_uom"].ToString()),
                        totalcapacity_volume = (dr_datarow["totalcapacity_volume"].ToString()),
                        volume_uomgid = (dr_datarow["volume_uomgid"].ToString()),
                        volume_uom = (dr_datarow["volume_uom"].ToString()),
                        typeofwarehouse_gid = (dr_datarow["typeofwarehouse_gid"].ToString()),
                        typeofwarehouse_name = (dr_datarow["typeofwarehouse_name"].ToString()),
                    });
                }
                values.MdlWarehousedtl = getmstaddress_list;
            }
            dt_datatable.Dispose();
        }

        // Supplier Details

        public void DaPostLoan2Supplierdtl(string employee_gid, MdlSupplierdtl values)
        {



            msGetGid = objcmnfunctions.GetMasterGID("ALSD");

            if (values.tmpadd_status == true)
                values.application2loan_gid = employee_gid;

            msSQL = "insert into agr_mst_tapploan2supplierdtl(" +
                    " apploan2supplierdtl_gid," +
                    " application_gid, " +
                    " application2loan_gid," +
                    " supplier_gid," +
                    " supplier_name," +
                    " supplier_address," +
                    " supplier_emailid," +
                    " supplier_phoneno," +
                    " supplier_gstno," +
                    " supplier_pandtl," +
                    " milestone_applicable," +
                    " milestonepaymenttype_gid," +
                    " milestonepaymenttype_name," +
                    " supplier_vintage," +
                    " supplier_margin," +
                    " created_by," +
                    " created_date) values(" +
                     "'" + msGetGid + "'," +
                     "'" + values.application_gid + "'," +
                           "'" + values.application2loan_gid + "'," +
                           "'" + values.supplier_gid + "'," +
                           "'" + values.supplier_name + "'," +
                           "'" + values.supplier_address + "'," +
                           "'" + values.supplier_emailid + "'," +
                           "'" + values.supplier_phoneno + "'," +
                           "'" + values.supplier_gstno + "'," +
                           "'" + values.supplier_pandtl + "'," +
                           "'" + values.milestone_applicable + "'," +
                           "'" + values.milestonepaymenttype_gid + "'," +
                           "'" + values.milestonepaymenttype_name + "'," +
                           "'" + values.supplier_vintage.Replace("'", "") + "',";
                           //"'" + values.supplier_margin.Replace("'", "") + "',";
            if (values.supplier_margin == null || values.supplier_margin == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.supplier_margin.Replace(",", "") + "',";
            }
            msSQL += "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (values.lspage == "CADAcceptanceCustomers")
            {
                //msSQL = " select contract_ref,loanproduct_name from agr_mst_tloansubproduct " +
                //       " where loanproduct_gid in (select DISTINCT producttype_gid from agr_mst_tapplication2loan where application_gid='" + values.application_gid + "') and " +
                //       " loansubproduct_gid in (select DISTINCT productsubtype_gid from agr_mst_tapplication2loan where application_gid='" + values.application_gid + "') ";
                //objODBCDatareader = objdbconn.GetDataReader(msSQL);
                //if (objODBCDatareader.HasRows == true)
                //{
                //    lscontractref_Name = objODBCDatareader["contract_ref"].ToString();
                //    lsloanproduct_name = objODBCDatareader["loanproduct_name"].ToString();
                //}
                //if (lscontractref_Name == "BTRE")
                //{
                //    string lssuppliercode = "SASUP";
                //    String lsrefbtst_name = "BTST";
                //    string lsyear_refno = DateTime.Now.ToString("ddMMyyyy");
                //    string lsusername = lsyear_refno.Substring(6, 2);
                //    String lscontract_code = lscontractref_Name.Substring(2, 2);
                //    string msGETRef = objcmnfunctions.GetMasterGID("SUGU");
                //    msGETRef = msGETRef.Replace("SUGU", "");
                //    lssupplier_refno = lssuppliercode + lsrefbtst_name + lsusername + lscontract_code + msGETRef;

                //}
                //else if (lscontractref_Name == "BTFT")
                //{
                //    string lssuppliercode = "SASUP";
                //    String lsrefbtst_name = "BTST";
                //    string lsyear_refno = DateTime.Now.ToString("ddMMyyyy");
                //    string lsusername = lsyear_refno.Substring(6, 2);
                //    String lscontract_code = lscontractref_Name.Substring(2, 2);
                //    string msGETRef = objcmnfunctions.GetMasterGID("SUST");
                //    msGETRef = msGETRef.Replace("SUST", "");
                //    lssupplier_refno = lssuppliercode + lsrefbtst_name + lsusername + lscontract_code + msGETRef;

                //}
                //else if (lscontractref_Name == "BTMR")
                //{
                //    string lssuppliercode = "SASUP";
                //    String lsrefbtst_name = "BTST";
                //    string lsyear_refno = DateTime.Now.ToString("ddMMyyyy");
                //    string lsusername = lsyear_refno.Substring(6, 2);
                //    String lscontract_code = lscontractref_Name.Substring(2, 2);
                //    string msGETRef = objcmnfunctions.GetMasterGID("SUDE");
                //    msGETRef = msGETRef.Replace("SUDE", "");
                //    lssupplier_refno = lssuppliercode + lsrefbtst_name + lsusername + lscontract_code + msGETRef;

                //}
                //else if (lscontractref_Name == "BTML")
                //{
                //    string lssuppliercode = "SASUP";
                //    String lsrefbtst_name = "MLIN";
                //    string lsyear_refno = DateTime.Now.ToString("ddMMyyyy");
                //    string lsusername = lsyear_refno.Substring(6, 2);
                //    String lscontract_code = "WB";
                //    string msGETRef = objcmnfunctions.GetMasterGID("SUWA");
                //    msGETRef = msGETRef.Replace("SUWA", "");
                //    lssupplier_refno = lssuppliercode + lsrefbtst_name + lsusername + lscontract_code + msGETRef;

                //}
                //else if (lscontractref_Name == "MLEB")
                //{
                //    string lssuppliercode = "SASUP";
                //    String lsrefbtst_name = "MLIN";
                //    string lsyear_refno = DateTime.Now.ToString("ddMMyyyy");
                //    string lsusername = lsyear_refno.Substring(6, 2);
                //    String lscontract_code = "WB";
                //    string msGETRef = objcmnfunctions.GetMasterGID("SUWA");
                //    msGETRef = msGETRef.Replace("SUWA", "");
                //    lssupplier_refno = lssuppliercode + lsrefbtst_name + lsusername + lscontract_code + msGETRef;

                //}
                //else if (lscontractref_Name == "MLAD")
                //{
                //    string lssuppliercode = "SASUP";
                //    String lsrefbtst_name = "MLIN";
                //    string lsyear_refno = DateTime.Now.ToString("ddMMyyyy");
                //    string lsusername = lsyear_refno.Substring(6, 2);
                //    String lscontract_code = "WB";
                //    string msGETRef = objcmnfunctions.GetMasterGID("SUWA");
                //    msGETRef = msGETRef.Replace("SUWA", "");
                //    lssupplier_refno = lssuppliercode + lsrefbtst_name + lsusername + lscontract_code + msGETRef;

                //}
                //else if (lscontractref_Name == "MLWB")
                //{
                //    string lssuppliercode = "SASUP";
                //    String lsrefbtst_name = "MLIN";
                //    string lsyear_refno = DateTime.Now.ToString("ddMMyyyy");
                //    string lsusername = lsyear_refno.Substring(6, 2);
                //    String lscontract_code = "WB";
                //    string msGETRef = objcmnfunctions.GetMasterGID("SUWA");
                //    msGETRef = msGETRef.Replace("SUWA", "");
                //    lssupplier_refno = lssuppliercode + lsrefbtst_name + lsusername + lscontract_code + msGETRef;

                //}
                //else if (lscontractref_Name == "MLCR")
                //{
                //    string lssuppliercode = "SASUP";
                //    String lsrefbtst_name = "MLIN";
                //    string lsyear_refno = DateTime.Now.ToString("ddMMyyyy");
                //    string lsusername = lsyear_refno.Substring(6, 2);
                //    String lscontract_code = "WB";
                //    string msGETRef = objcmnfunctions.GetMasterGID("SUWA");
                //    msGETRef = msGETRef.Replace("SUWA", "");
                //    lssupplier_refno = lssuppliercode + lsrefbtst_name + lsusername + lscontract_code + msGETRef;
                //}
                //else if (lscontractref_Name == "STML")
                //{
                //    string lssuppliercode = "SASUP";
                //    String lsrefbtst_name = "STFY";
                //    string lsyear_refno = DateTime.Now.ToString("ddMMyyyy");
                //    string lsusername = lsyear_refno.Substring(6, 2);
                //    String lscontract_code = lscontractref_Name.Substring(2, 2);
                //    string msGETRef = objcmnfunctions.GetMasterGID("SUFM");
                //    msGETRef = msGETRef.Replace("SUFM", "");
                //    lssupplier_refno = lssuppliercode + lsrefbtst_name + lsusername + lscontract_code + msGETRef;

                //}
                //else if (lscontractref_Name == "STRE")
                //{
                //    string lssuppliercode = "SASUP";
                //    String lsrefbtst_name = "STFY";
                //    string lsyear_refno = DateTime.Now.ToString("ddMMyyyy");
                //    string lsusername = lsyear_refno.Substring(6, 2);
                //    String lscontract_code = lscontractref_Name.Substring(2, 2);
                //    string msGETRef = objcmnfunctions.GetMasterGID("SUFR");
                //    msGETRef = msGETRef.Replace("SUFR", "");
                //    lssupplier_refno = lssuppliercode + lsrefbtst_name + lsusername + lscontract_code + msGETRef;

                //}


                //else if (lsloanproduct_name == "Export")
                //{
                //    string lssuppliercode = "SASUP";
                //    String lsrefbtst_name = "EXIM";
                //    string lsyear_refno = DateTime.Now.ToString("ddMMyyyy");
                //    string lsusername = lsyear_refno.Substring(6, 2);
                //    String lscontract_code = "EX";
                //    string msGETRef = objcmnfunctions.GetMasterGID("SUPO");
                //    msGETRef = msGETRef.Replace("SUPO", "");
                //    lssupplier_refno = lssuppliercode + lsrefbtst_name + lsusername + lscontract_code + msGETRef;

                //}
                //else if (lsloanproduct_name == "Export-STF")
                //{
                //    string lssuppliercode = "SASUP";
                //    String lsrefbtst_name = "EXIM";
                //    string lsyear_refno = DateTime.Now.ToString("ddMMyyyy");
                //    string lsusername = lsyear_refno.Substring(6, 2);
                //    String lscontract_code = "EX";
                //    string msGETRef = objcmnfunctions.GetMasterGID("SUPO");
                //    msGETRef = msGETRef.Replace("SUPO", "");
                //    lssupplier_refno = lssuppliercode + lsrefbtst_name + lsusername + lscontract_code + msGETRef;
                //}
                //else if (lsloanproduct_name == "Import")
                //{
                //    string lssuppliercode = "SASUP";
                //    String lsrefbtst_name = "EXIM";
                //    string lsyear_refno = DateTime.Now.ToString("ddMMyyyy");
                //    string lsusername = lsyear_refno.Substring(6, 2);
                //    String lscontract_code = "IM";
                //    string msGETRef = objcmnfunctions.GetMasterGID("SUIM");
                //    msGETRef = msGETRef.Replace("SUIM", "");
                //    lssupplier_refno = lssuppliercode + lsrefbtst_name + lsusername + lscontract_code + msGETRef;

                //}
                //else if (lsloanproduct_name == "Import-STF")
                //{
                //    string lssuppliercode = "SASUP";
                //    String lsrefbtst_name = "EXIM";
                //    string lsyear_refno = DateTime.Now.ToString("ddMMyyyy");
                //    string lsusername = lsyear_refno.Substring(6, 2);
                //    String lscontract_code = "IM";
                //    string msGETRef = objcmnfunctions.GetMasterGID("SUIM");
                //    msGETRef = msGETRef.Replace("SUIM", "");
                //    lssupplier_refno = lssuppliercode + lsrefbtst_name + lsusername + lscontract_code + msGETRef;

                //}

                msSQL = " select  buyeragreement_id from agr_mst_tapplication  where application_gid = '" + values.application_gid + "' ";
                string buyeragreement_id = objdbconn.GetExecuteScalar(msSQL);

                string byragree_id = buyeragreement_id.Substring(0, 12);

                msSQL = " select count(a.apploan2supplierdtl_gid) from agr_mst_tapploan2supplierdtl a " +
                       " left join  agr_mst_tapplication2loan b on b.application2loan_gid = a.application2loan_gid " +
                       " where a.application_gid='" + values.application_gid + "' ";

                int suprcount = Convert.ToInt16(objdbconn.GetExecuteScalar(msSQL));


                            string msGETsyr = "S" + $"{suprcount:000000}"; 

                            string lssupplier_refno = byragree_id + msGETsyr;
                       

                        msSQL = " update agr_mst_tapploan2supplierdtl set " +
                       " supplieragreement_id ='" + lssupplier_refno + "' " +
                       " where apploan2supplierdtl_gid ='" + msGetGid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    


                }


            if (mnResult != 0)
            {

                msSQL = "update agr_mst_tapploan2supplierpayment set apploan2supplierdtl_gid ='" + msGetGid + "' where apploan2supplierdtl_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tapploan2supplierpayment set application2loan_gid ='" + values.application2loan_gid + "' where application2loan_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (values.MdlSupplierGSTdtl != null)
                {
                    foreach (var i in values.MdlSupplierGSTdtl)
                    {
                        string msGetgstGid = objcmnfunctions.GetMasterGID("A2SG");

                        msSQL = "insert into agr_mst_tapp2suppliergstdtl(" +
                                " app2suppliergstdtl_gid," +
                                " institution2branch_gid, " +
                                " apploan2supplierdtl_gid," +
                                " gst_state," +
                                " gst_no," +
                                " created_by," +
                                " created_date) values(" +
                                 "'" + msGetgstGid + "'," +
                                 "'" + i.institution2branch_gid + "'," +
                                 "'" + msGetGid + "'," +
                                 "'" + i.gst_state + "'," +
                                 "'" + i.gst_no + "'," +
                                 "'" + employee_gid + "'," +
                                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                values.status = true;
                values.message = "Supplier Details are added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        public void DaGetLoan2Supplierdtl(string application_gid, string application2loan_gid, string employee_gid, MdlSupplierdtlList values, string tmp_status)
        {
            msSQL = " select apploan2supplierdtl_gid,application2loan_gid,application_gid, supplier_gid, supplier_name,supplier_address,supplier_emailid, " +
                    " supplier_phoneno, supplier_gstno, supplier_pandtl, milestone_applicable,milestonepaymenttype_gid,milestonepaymenttype_name,supplier_vintage, supplier_margin,supplieragreement_id," +
                    " case when a.suppliername_status ='N' then 'Inactive' else 'Active' end as suppliername_status " +
                    " from agr_mst_tapploan2supplierdtl a ";
            if (tmp_status == "true")
                msSQL += " where a.application_gid='" + application_gid + "' and  a.application2loan_gid = '" + employee_gid + "'  order by apploan2supplierdtl_gid asc";
            else if (tmp_status == "both")
                msSQL += " where a.application_gid='" + application_gid + "' and (a.application2loan_gid = '" + employee_gid + "' or a.application2loan_gid = '" + application2loan_gid + "') order by apploan2supplierdtl_gid asc";
            else
                msSQL += " where a.application2loan_gid = '" + application2loan_gid + "'  order by apploan2supplierdtl_gid asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlist = new List<MdlSupplierdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlist.Add(new MdlSupplierdtl
                    {
                        apploan2supplierdtl_gid = (dr_datarow["apploan2supplierdtl_gid"].ToString()),
                        supplier_gid = (dr_datarow["supplier_gid"].ToString()),
                        supplier_name = (dr_datarow["supplier_name"].ToString()),
                        supplier_address = (dr_datarow["supplier_address"].ToString()),
                        supplier_emailid = (dr_datarow["supplier_emailid"].ToString()),
                        supplier_phoneno = (dr_datarow["supplier_phoneno"].ToString()),
                        supplier_gstno = (dr_datarow["supplier_gstno"].ToString()),
                        supplier_pandtl = (dr_datarow["supplier_pandtl"].ToString()),
                        milestone_applicable = (dr_datarow["milestone_applicable"].ToString()),
                        milestonepaymenttype_gid = (dr_datarow["milestonepaymenttype_gid"].ToString()),
                        milestonepaymenttype_name = (dr_datarow["milestonepaymenttype_name"].ToString()),
                        supplier_vintage = (dr_datarow["supplier_vintage"].ToString()),
                        supplier_margin = (dr_datarow["supplier_margin"].ToString()),
                        supplieragreement_id = (dr_datarow["supplieragreement_id"].ToString()),
                        suppliername_status = (dr_datarow["suppliername_status"].ToString()),

                    });
                }
                values.MdlSupplierdtl = getlist;
            }
            values.status = true;
            dt_datatable.Dispose();
        }

        public void DaGetLoan2SupplierGSTdtl(string apploan2supplierdtl_gid, MdlSupplierGSTdtlList values)
        {
            msSQL = " select app2suppliergstdtl_gid,gst_state,gst_no from agr_mst_tapp2suppliergstdtl where apploan2supplierdtl_gid='" + apploan2supplierdtl_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlist = new List<MdlSupplierGSTdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlist.Add(new MdlSupplierGSTdtl
                    {
                        institution2branch_gid = (dr_datarow["app2suppliergstdtl_gid"].ToString()),
                        gst_state = (dr_datarow["gst_state"].ToString()),
                        gst_no = (dr_datarow["gst_no"].ToString()),
                    });
                }
                values.MdlSupplierGSTdtl = getlist;
            }
            values.status = true;
            dt_datatable.Dispose();
        }

        public void DaDeleteSupplierDtl(string apploan2supplierdtl_gid, result values)
        {
            msSQL = "delete from agr_mst_tapploan2supplierpayment where apploan2supplierdtl_gid='" + apploan2supplierdtl_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tapp2suppliergstdtl  where apploan2supplierdtl_gid='" + apploan2supplierdtl_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tapploan2supplierdtl where apploan2supplierdtl_gid='" + apploan2supplierdtl_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            
            if (mnResult != 0)
            {

                values.message = "Supplier Details are Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public void DaPostLoan2SupplierPaymentdtl(string employee_gid, MdlSupplierPaymentdtl values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("ALSD");

            if (values.tmpadd_status == true)
                values.application2loan_gid = employee_gid;

            msSQL = "insert into agr_mst_tapploan2supplierpayment(" +
                    " apploan2supplierpayment_gid," +
                    " application_gid, " +
                    " application2loan_gid," +
                    " commodity_gid," +
                    " commodity_name," +
                    " supplierpayment_type," +
                    " supplierpayment_typegid," +
                    " maxpercent_paymentterm," +
                    " apploan2supplierdtl_gid," +
                    " created_by," +
                    " created_date) values(" +
                     "'" + msGetGid + "'," +
                     "'" + values.application_gid + "'," +
                           "'" + values.application2loan_gid + "'," +
                           "'" + values.commodity_gid + "'," +
                           "'" + values.commodity_name + "'," +
                           "'" + values.supplierpayment_type + "'," +
                           "'" + values.supplierpayment_typegid + "'," +
                           "'" + values.maxpercent_paymentterm + "'," +
                           "'" + employee_gid + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Supplier Payment Details are added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        public void DaGetLoan2SupplierPaymentdtl(string application_gid, string application2loan_gid, string employee_gid, MdlSupplierPaymentdtlList values, string tmp_status)
        {
            msSQL = " select apploan2supplierpayment_gid,application_gid,application2loan_gid, commodity_gid, commodity_name, " +
                    " supplierpayment_type,supplierpayment_typegid, maxpercent_paymentterm " +
                    " from agr_mst_tapploan2supplierpayment a ";
            if (tmp_status == "true")
                msSQL += " where a.application_gid='" + application_gid + "' and a.application2loan_gid = '" + employee_gid + "' order by apploan2supplierpayment_gid asc";
            else if (tmp_status == "both")
                msSQL += " where  a.application_gid='" + application_gid + "' and (a.application2loan_gid = '" + employee_gid + "' or a.application2loan_gid = '" + application2loan_gid + "')   order by apploan2supplierpayment_gid asc";
            else if (tmp_status == "add")
                msSQL += " where  a.application_gid='" + application_gid + "' and  apploan2supplierdtl_gid = '" + employee_gid + "'  order by apploan2supplierpayment_gid asc";

            else
                msSQL += " where a.application2loan_gid = '" + application2loan_gid + "' order by apploan2supplierpayment_gid asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlist = new List<MdlSupplierPaymentdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlist.Add(new MdlSupplierPaymentdtl
                    {
                        apploan2supplierpayment_gid = (dr_datarow["apploan2supplierpayment_gid"].ToString()),
                        commodity_gid = (dr_datarow["commodity_gid"].ToString()),
                        commodity_name = (dr_datarow["commodity_name"].ToString()),
                        supplierpayment_type = (dr_datarow["supplierpayment_type"].ToString()),
                        supplierpayment_typegid = (dr_datarow["supplierpayment_typegid"].ToString()),
                        maxpercent_paymentterm = (dr_datarow["maxpercent_paymentterm"].ToString()),
                        application2loan_gid = (dr_datarow["application2loan_gid"].ToString())
                    });
                }
                values.MdlSupplierPaymentdtl = getlist;
            }
            values.status = true;
            dt_datatable.Dispose();
        }

        public void DaDeleteSupplierPaymentDtl(string apploan2supplierpayment_gid, result values)
        {
            msSQL = "delete from agr_mst_tapploan2supplierpayment where apploan2supplierpayment_gid='" + apploan2supplierpayment_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Supplier Payment Details are Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public void DaGetSupplierNameDropdown(MdlSupplierDropdowndtlList values)
        {
            msSQL = "  select a.application_gid,a.application_no,a.customer_name " +
                    "    from agr_mst_tsuprapplication a " +
                    " where a.process_type = 'Accept' order by a.customer_name asc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlist = new List<MdlSupplierDropdowndtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlist.Add(new MdlSupplierDropdowndtl
                    {
                        supplier_gid = (dr_datarow["application_gid"].ToString()),
                        supplier_name = (dr_datarow["customer_name"].ToString()),
                    });
                }
                values.MdlSupplierDropdowndtl = getlist;
            }
            dt_datatable.Dispose();
        }

        public void DaGetSupplierNameDtlDropdown(string application_gid, MdlSupplierdtl values)
        {
            try
            {
                msSQL = "select applicant_type from agr_mst_tsuprapplication where application_gid='" + application_gid + "'";
                string lsapplicant_type = objdbconn.GetExecuteScalar(msSQL);
                if (lsapplicant_type == "Individual")
                {

                    msSQL = " select contact_gid, pan_no, (select mobile_no from agr_mst_tsuprcontact2mobileno where contact_gid = a.contact_gid " +
                             " and primary_status = 'Yes') as phone_no, " +
                             " (select email_address from agr_mst_tsuprcontact2email where contact_gid = a.contact_gid and primary_status = 'Yes') as email_id, " +
                             " (select REPLACE(concat(addressline1, ',', addressline2, ',', landmark, ',', city, ',', taluka, ',', district, ',', state, ',', country, '-', " +
                             " postal_code), ',,', ',') from agr_mst_tsuprcontact2address where contact_gid = a.contact_gid and primary_status = 'Yes') as address " +
                             " from agr_mst_tsuprcontact a " +
                             " where application_gid = '" + application_gid + "' and stakeholder_type in ('Applicant','Borrower')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lscontact_gid = objODBCDatareader["contact_gid"].ToString();
                        values.supplier_pandtl = objODBCDatareader["pan_no"].ToString();
                        values.supplier_phoneno = objODBCDatareader["phone_no"].ToString();
                        values.supplier_emailid = objODBCDatareader["email_id"].ToString();
                        values.supplier_address = objODBCDatareader["address"].ToString();
                    }
                    objODBCDatareader.Close();
                }
                else
                {
                    msSQL = " select institution_gid, companypan_no, (select mobile_no from agr_mst_tsuprinstitution2mobileno where institution_gid = a.institution_gid " +
                             " and primary_status = 'Yes') as phone_no, " +
                             " (select email_address from agr_mst_tsuprinstitution2email where institution_gid = a.institution_gid and primary_status = 'Yes') as email_id, " +
                             " (select REPLACE(concat(addressline1, ',', addressline2, ',', landmark, ',', city, ',', taluka, ',', district, ',', state, ',', country, '-', " +
                             " postal_code), ',,', ',') from agr_mst_tsuprinstitution2address where institution_gid = a.institution_gid and primary_status = 'Yes') as address " +
                             " from agr_mst_tsuprinstitution a " +
                             " where application_gid = '" + application_gid + "' and stakeholder_type in ('Applicant','Borrower')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsinstitution_gid = objODBCDatareader["institution_gid"].ToString();
                        values.supplier_pandtl = objODBCDatareader["companypan_no"].ToString();
                        values.supplier_phoneno = objODBCDatareader["phone_no"].ToString();
                        values.supplier_emailid = objODBCDatareader["email_id"].ToString();
                        values.supplier_address = objODBCDatareader["address"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = " select institution2branch_gid,gst_state,gst_no from agr_mst_tinstitution2branch where institution_gid='" + lsinstitution_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getlist = new List<MdlSupplierGSTdtl>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getlist.Add(new MdlSupplierGSTdtl
                            {
                                institution2branch_gid = (dr_datarow["institution2branch_gid"].ToString()),
                                gst_state = (dr_datarow["gst_state"].ToString()),
                                gst_no = (dr_datarow["gst_no"].ToString()),
                            });
                        }
                        values.MdlSupplierGSTdtl = getlist;
                    }
                    dt_datatable.Dispose();
                }
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }


        }

        public void DaGetSupplierOnboardDtlDropdown(string application_gid, MdlSupplierdtl values)
        {
            try
            {
                msSQL = "select applicant_type from agr_mst_tsupronboard where application_gid='" + application_gid + "'";
                string lsapplicant_type = objdbconn.GetExecuteScalar(msSQL);
                if (lsapplicant_type == "Individual")
                {

                    msSQL = " select contact_gid, pan_no, (select mobile_no from agr_mst_tsupronboardcontact2mobileno where contact_gid = a.contact_gid " +
                             " and primary_status = 'Yes') as phone_no, " +
                             " (select email_address from agr_mst_tsupronboardcontact2email where contact_gid = a.contact_gid and primary_status = 'Yes') as email_id, " +
                             " (select REPLACE(concat(addressline1, ',', addressline2, ',', landmark, ',', city, ',', taluka, ',', district, ',', state, ',', country, '-', " +
                             " postal_code), ',,', ',') from agr_mst_tsupronboardcontact2address where contact_gid = a.contact_gid and primary_status = 'Yes') as address " +
                             " from agr_mst_tsupronboardcontact a " +
                             " where application_gid = '" + application_gid + "' and stakeholder_type in ('Applicant','Borrower')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lscontact_gid = objODBCDatareader["contact_gid"].ToString();
                        values.supplier_pandtl = objODBCDatareader["pan_no"].ToString();
                        values.supplier_phoneno = objODBCDatareader["phone_no"].ToString();
                        values.supplier_emailid = objODBCDatareader["email_id"].ToString();
                        values.supplier_address = objODBCDatareader["address"].ToString();
                    }
                    objODBCDatareader.Close();
                }
                else
                {
                    msSQL = " select institution_gid, companypan_no, (select mobile_no from agr_mst_tsupronboardinstitution2mobileno where institution_gid = a.institution_gid " +
                             " and primary_status = 'Yes') as phone_no, " +
                             " (select email_address from agr_mst_tsupronboardinstitution2email where institution_gid = a.institution_gid and primary_status = 'Yes') as email_id, " +
                             " (select REPLACE(concat(addressline1, ',', addressline2, ',', landmark, ',', city, ',', taluka, ',', district, ',', state, ',', country, '-', " +
                             " postal_code), ',,', ',') from agr_mst_tsupronboardinstitution2address where institution_gid = a.institution_gid and primary_status = 'Yes') as address " +
                             " from agr_mst_tsupronboard2institution a " +
                             " where application_gid = '" + application_gid + "' and stakeholder_type in ('Applicant','Borrower')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsinstitution_gid = objODBCDatareader["institution_gid"].ToString();
                        values.supplier_pandtl = objODBCDatareader["companypan_no"].ToString();
                        values.supplier_phoneno = objODBCDatareader["phone_no"].ToString();
                        values.supplier_emailid = objODBCDatareader["email_id"].ToString();
                        values.supplier_address = objODBCDatareader["address"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = " select institution2branch_gid,gst_state,gst_no from agr_mst_tsupronboardinstitution2branch where institution_gid='" + lsinstitution_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getlist = new List<MdlSupplierGSTdtl>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getlist.Add(new MdlSupplierGSTdtl
                            {
                                institution2branch_gid = (dr_datarow["institution2branch_gid"].ToString()),
                                gst_state = (dr_datarow["gst_state"].ToString()),
                                gst_no = (dr_datarow["gst_no"].ToString()),
                            });
                        }
                        values.MdlSupplierGSTdtl = getlist;
                    }
                    dt_datatable.Dispose();
                }
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }


        }


        //    Payment Type - Customer(Repayment- Receipt)

        public void DaPostLoan2Repaymentdtl(string employee_gid, MdlPaymentdtl values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("PTC");

            if (values.tmpadd_status == true)
                values.application2loan_gid = employee_gid;

            msSQL = " insert into agr_mst_tapploan2paymenttypecustomer(" +
                    " paymenttypecustomer_gid," +
                    " application_gid, " +
                    " application2loan_gid," +
                    " application2product_gid, " +
                    " customerpaymenttype_gid," +
                    " customerpaymenttype_name," +
                    " maximumpercent_paymentterm," +
                    " created_by," +
                    " created_date) " +
                    " values(" +
                     "'" + msGetGid + "'," +
                     "'" + values.application_gid + "'," +
                     "'" + values.application2loan_gid + "'," +
                     "'" + employee_gid + "'," +
                     "'" + values.customerpaymenttype_gid + "'," +
                     "'" + values.customerpaymenttype_name + "'," +
                     "'" + values.maximumpercent_paymentterm + "'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                    values.status = true;
                values.message = "Payment Type-Customer(Repayment-Receipt) Details are added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        public void DaGetLoan2Repaymentdtl(string application_gid, string application2loan_gid, string employee_gid, MdlPaymentdtlList values, string tmp_status)
        {
            msSQL = " select paymenttypecustomer_gid,application2loan_gid,application_gid, customerpaymenttype_gid, customerpaymenttype_name, " +
                    " maximumpercent_paymentterm  " +
                    " from agr_mst_tapploan2paymenttypecustomer a ";
            if (tmp_status == "true")
                msSQL += " where application_gid='" + application_gid + "' and a.application2loan_gid = '" + employee_gid + "' order by paymenttypecustomer_gid asc";
            else if (tmp_status == "both")
                msSQL += " where application_gid='" + application_gid + "' and (a.application2loan_gid = '" + employee_gid + "' or a.application2loan_gid = '" + application2loan_gid + "') order by paymenttypecustomer_gid asc";
            else if (tmp_status == "false")
                msSQL += " where application_gid='" + application_gid + "' and (a.application2product_gid = '" + employee_gid + "') order by paymenttypecustomer_gid asc";

            else
                msSQL += " where application_gid='" + application_gid + "' and  a.application2loan_gid = '" + application2loan_gid + "' order by paymenttypecustomer_gid asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlist = new List<MdlPaymentdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlist.Add(new MdlPaymentdtl
                    {
                        paymenttypecustomer_gid = (dr_datarow["paymenttypecustomer_gid"].ToString()),
                        customerpaymenttype_gid = (dr_datarow["customerpaymenttype_gid"].ToString()),
                        customerpaymenttype_name = (dr_datarow["customerpaymenttype_name"].ToString()),
                        maximumpercent_paymentterm = (dr_datarow["maximumpercent_paymentterm"].ToString()),
                    });
                }
                values.MdlPaymentdtl = getlist;

            }
            values.status = true;
            dt_datatable.Dispose();
        }

        public void DaDeleteLoan2Repaymentdtl(string paymenttypecustomer_gid, result values)
        {
            msSQL = "delete from agr_mst_tapploan2paymenttypecustomer where paymenttypecustomer_gid='" + paymenttypecustomer_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Payment Type-Customer(Repayment-Receipt) Details are Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public bool DaPostBankFundingUpload(HttpRequest httpRequest, Agrupload_list objfilename, string employee_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            String path = lspath;
            string lsapplication_gid = httpRequest.Form["application_gid"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/STFBankFundingDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/STFBankFundingDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/STFBankFundingDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        msSQL = " insert into agr_tmp_tbankfundingdataupload( " +
                                    " application_gid, " +
                                    " application2loan_gid, " +
                                    " file_name ," +
                                    " file_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + lsapplication_gid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            msSQL = " select bankfundingdataupload_gid from agr_tmp_tbankfundingdataupload where application_gid='" + lsapplication_gid + "' " +
                                    " and application2loan_gid='" + employee_gid + "'";
                            objfilename.tmp_documentGid = objdbconn.GetExecuteScalar(msSQL);
                            objfilename.document_name = httpPostedFile.FileName;
                            objfilename.document_path = objcmnstorage.EncryptData(lspath + msdocument_gid + FileExtension);

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

        public void DaDeleteBankFundingUpload(string bankfundingdataupload_gid, result values)
        {
            msSQL = "delete from agr_tmp_tbankfundingdataupload where bankfundingdataupload_gid='" + bankfundingdataupload_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Bank Funding Data Document Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public void DaGetAppCommodityDtls(string application2product_gid, MdlCommodityDtls values)
        {
            msSQL = " select  milestone_applicability,insurance_applicability,milestonepayment_name,sa_payout,insurance_availability,  " +
                    " insurance_percent, insurance_cost,net_yield,markto_marketvalue,pricereference_source,creditperiod_years,  " +
                    " creditperiod_months, creditperiod_days , overallcreditperiod_limit, commodity_margin, commoditynet_yield,graceperiod_days " +
                    " from agr_mst_tapplication2product " +
                    " where application2product_gid ='" + application2product_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.milestone_applicability = objODBCDatareader["milestone_applicability"].ToString();
                values.insurance_applicability = objODBCDatareader["insurance_applicability"].ToString();
                values.milestonepayment_name = objODBCDatareader["milestonepayment_name"].ToString();
                values.sa_payout = objODBCDatareader["sa_payout"].ToString();
                values.insurance_availability = objODBCDatareader["insurance_availability"].ToString();
                values.insurance_percent = objODBCDatareader["insurance_percent"].ToString();
                values.insurance_cost = objODBCDatareader["insurance_cost"].ToString();
                values.net_yield = objODBCDatareader["net_yield"].ToString();
                values.markto_marketvalue = objODBCDatareader["markto_marketvalue"].ToString();
                values.pricereference_source = objODBCDatareader["pricereference_source"].ToString();
                values.creditperiod_years = objODBCDatareader["creditperiod_years"].ToString();
                values.creditperiod_months = objODBCDatareader["creditperiod_months"].ToString();
                values.creditperiod_days = objODBCDatareader["creditperiod_days"].ToString();
                values.overallcreditperiod_limit = objODBCDatareader["overallcreditperiod_limit"].ToString();
                values.commodity_margin = objODBCDatareader["commodity_margin"].ToString();
                values.commoditynet_yield = objODBCDatareader["commoditynet_yield"].ToString();
                values.graceperiod_days = objODBCDatareader["graceperiod_days"].ToString();
                
            }
            values.status = true;
            objODBCDatareader.Close();
        }

        public void DaGetAppCommodityGstList(string application2product_gid, commoditygststatuslist values)
        {
            msSQL = " select commoditygststatus_gid ,product_gid,IGST_percent,SGST_percent, CGST_percent,CESS_percent, " +
                    " date_format(wef_date,'%d-%m-%Y') as wef_date from agr_mst_tappproduct2commoditygststatus " +
                    " where application2product_gid ='" + application2product_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getvariety_list = new List<commoditygststatus>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getvariety_list.Add(new commoditygststatus
                    {
                        commoditygststatus_gid = dt["commoditygststatus_gid"].ToString(),
                        product_gid = dt["product_gid"].ToString(),
                        IGST_percent = dt["IGST_percent"].ToString(),
                        SGST_percent = dt["SGST_percent"].ToString(),
                        CGST_percent = dt["CGST_percent"].ToString(),
                        CESS_percent = dt["CESS_percent"].ToString(),
                        wef_date = dt["wef_date"].ToString(),
                    });
                    values.commoditygststatus = getvariety_list;
                }
            }
            values.status = true;
            dt_datatable.Dispose();
        }

        public void DaGetAppCommodityTradeProdctList(string application2product_gid, commodityTradeProdctlist values)
        {
            msSQL = " select commoditytradeproductdtl_gid,product_gid,product_name,subproduct_gid, subproduct_name, " +
                    " insurancecompany_gid, insurancecompany_name, insurancepolicy_name from agr_mst_tappproduct2commoditytradedtl " +
                    " where application2product_gid ='" + application2product_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getvariety_list = new List<commodityTradeProdct>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getvariety_list.Add(new commodityTradeProdct
                    {
                        commoditytradeproductdtl_gid = dt["commoditytradeproductdtl_gid"].ToString(),
                        product_gid = dt["product_gid"].ToString(),
                        product_name = dt["product_name"].ToString(),
                        subproduct_gid = dt["subproduct_gid"].ToString(),
                        subproduct_name = dt["subproduct_name"].ToString(),
                        insurancecompany_gid = dt["insurancecompany_gid"].ToString(),
                        insurancecompany_name = dt["insurancecompany_name"].ToString(),
                        insurancepolicy_name = dt["insurancepolicy_name"].ToString(),
                    });
                    values.commodityTradeProdct = getvariety_list;
                }
            }
            values.status = true;
            dt_datatable.Dispose();
        }

        public void DaGetAppCommodityCustomerpaymentList(string application2product_gid, commodityTradeProdctlist values)
        {
            msSQL = " select customerpaymenttype_name,maximumpercent_paymentterm , paymenttypecustomer_gid from agr_mst_tapploan2paymenttypecustomer where application2product_gid ='" + application2product_gid + "' ";
                   
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getvariety_list = new List<commoditycustomerpayment>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getvariety_list.Add(new commoditycustomerpayment
                    {
                        customerpaymenttype_name = dt["customerpaymenttype_name"].ToString(),
                        maximumpercent_paymentterm = dt["maximumpercent_paymentterm"].ToString(),
                        paymenttypecustomer_gid = dt["paymenttypecustomer_gid"].ToString(),

                    });
                    values.commoditycustomerpayment = getvariety_list;
                }
            }
            values.status = true;
            dt_datatable.Dispose();
        }

        public void DaGetAppCommodityDocumentUploadList(string application2product_gid, commodityDocumentUploadlist values)
        {
            msSQL = " select commoditydocument_gid,date_format(ason_date,'%d-%m-%Y') as ason_date, commodityreport_filename,commodityreport_filepath, " +
                    " riskanalysisreport_filename,riskanalysisreport_filepath, concat(c.user_firstname, c.user_lastname, '/',c.user_code) as created_by " +
                    " from agr_mst_tappproduct2commoditydocument a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                    " where a.application2product_gid ='" + application2product_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getvariety_list = new List<commodityDocumentUpload>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getvariety_list.Add(new commodityDocumentUpload
                    {
                        commoditydocument_gid = dt["commoditydocument_gid"].ToString(),
                        ason_date = dt["ason_date"].ToString(),
                        commodityreport_filepath = objcmnstorage.EncryptData(dt["commodityreport_filepath"].ToString()),
                        commodityreport_filename = dt["commodityreport_filename"].ToString(),
                        riskanalysisreport_filename = dt["riskanalysisreport_filename"].ToString(),
                        riskanalysisreport_filepath = objcmnstorage.EncryptData(dt["riskanalysisreport_filepath"].ToString()),
                        created_by = dt["created_by"].ToString()
                    });
                    values.commodityDocumentUpload = getvariety_list;
                }
            }
            values.status = true;
            dt_datatable.Dispose();
        }


        public void DaGetApprovedSupplierOnboardDropdown(MdlSupplierDropdowndtlList values)
        {
            msSQL = "  select a.application_gid,a.application_no,a.customer_name, a.customerref_name " +
                    "    from agr_mst_tsupronboard a " +
                    " where a.onboard_approvalstatus = 'Y' order by a.customerref_name asc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlist = new List<MdlSupplierDropdowndtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlist.Add(new MdlSupplierDropdowndtl
                    {
                        supplier_gid = (dr_datarow["application_gid"].ToString()),
                        supplier_name = (dr_datarow["customerref_name"].ToString()),
                    });
                }
                values.MdlSupplierDropdowndtl = getlist;
            }
            dt_datatable.Dispose();
        }

        public void DaGetLoan2Repaymentvalue(string application_gid,  string employee_gid, MdlPaymentdtlList values)
        {

            msSQL = " select count(maximumpercent_paymentterm) from agr_mst_tapploan2paymenttypecustomer where application2loan_gid = '" + employee_gid + "' and application_gid = '" + application_gid + "' and application2product_gid = '" + employee_gid + "'";

            lspayment_count = Convert.ToInt32(objdbconn.GetExecuteScalar(msSQL));

            if (lspayment_count == 1)
            {

                msSQL = " select maximumpercent_paymentterm from agr_mst_tapploan2paymenttypecustomer where application2loan_gid = '" + employee_gid + "' and application_gid = '" + application_gid + "' and application2product_gid = '" + employee_gid + "' ";
                values.payment_value = objdbconn.GetExecuteScalar(msSQL);
                values.status = true;
            }

            else if (lspayment_count >= 2)
            {

                msSQL = " select sum(maximumpercent_paymentterm) from agr_mst_tapploan2paymenttypecustomer where application2loan_gid = '" + employee_gid + "' and application_gid = '" + application_gid + "' and application2product_gid = '" + employee_gid + "'";
                values.exceeded_value = objdbconn.GetExecuteScalar(msSQL);

                if (Convert.ToInt32(values.exceeded_value) > 100)
                {

                    values.message = "Payment term of 100 has been added already";
                    values.status = false;

                }

                else
                {
                    values.payment_value = values.exceeded_value;
                    values.status = true;

                }
            }

        }


        public void DaGetSupplier2paymentvalue(string application_gid,  string employee_gid, MdlPaymentdtlList values)
        {

            msSQL = " select count(maxpercent_paymentterm) from agr_mst_tapploan2supplierpayment where application2loan_gid = '" + employee_gid + "' and application_gid = '" + application_gid + "' and apploan2supplierdtl_gid = '" + employee_gid + "'";

            lspayment_count = Convert.ToInt32(objdbconn.GetExecuteScalar(msSQL));

            if (lspayment_count == 1)
            {

                msSQL = " select maxpercent_paymentterm from agr_mst_tapploan2supplierpayment where application2loan_gid = '" + employee_gid + "' and application_gid = '" + application_gid + "' and apploan2supplierdtl_gid = '" + employee_gid + "'";
                values.payment_value = objdbconn.GetExecuteScalar(msSQL);
                values.status = true;
            }

            else if (lspayment_count >= 2)
            {

                msSQL = " select sum(maxpercent_paymentterm) from agr_mst_tapploan2supplierpayment where application2loan_gid = '" + employee_gid + "' and application_gid = '" + application_gid + "' and apploan2supplierdtl_gid = '" + employee_gid + "'";
                values.exceeded_value = objdbconn.GetExecuteScalar(msSQL);

                if (Convert.ToInt32(values.exceeded_value) > 100) {

                    values.message = "Payment term of 100 has been added already";
                    values.status = false;

                }

                else
                {
                    values.payment_value = values.exceeded_value;
                    values.status = true;
                }

            }

        }

        public void DaGetEditLoan2Repaymentvalue(string application_gid, string application2loan_gid, string employee_gid, MdlPaymentdtlList values)
        {

            msSQL = " select count(maximumpercent_paymentterm) from agr_mst_tapploan2paymenttypecustomer where (application2loan_gid = '" + employee_gid + "' or application2loan_gid = '" + application2loan_gid + "' ) and application_gid = '" + application_gid + "' and application2product_gid = '" + employee_gid + "'";

            lspayment_count = Convert.ToInt32(objdbconn.GetExecuteScalar(msSQL));

            if (lspayment_count == 1)
            {

                msSQL = " select maximumpercent_paymentterm from agr_mst_tapploan2paymenttypecustomer where (application2loan_gid = '" + employee_gid + "' or application2loan_gid = '" + application2loan_gid + "' ) and application_gid = '" + application_gid + "'and application2product_gid = '" + employee_gid + "'";
                values.payment_value = objdbconn.GetExecuteScalar(msSQL);
                values.status = true;
            }

            else if (lspayment_count >= 2)
            {

                msSQL = " select sum(maximumpercent_paymentterm) from agr_mst_tapploan2paymenttypecustomer where (application2loan_gid = '" + employee_gid + "' or application2loan_gid = '" + application2loan_gid + "' ) and application_gid = '" + application_gid + "' and application2product_gid = '" + employee_gid + "'";
                values.exceeded_value = objdbconn.GetExecuteScalar(msSQL);

                if (Convert.ToInt32(values.exceeded_value) > 100)
                {

                    values.message = "Payment term of 100 has been added already";
                    values.status = false;

                }

                else
                {
                    values.payment_value = values.exceeded_value;
                    values.status = true;
                }
            }

        }


        public void DaGetEditSupplier2paymentvalue(string application_gid, string application2loan_gid, string employee_gid, MdlPaymentdtlList values)
        {

            msSQL = " select count(maxpercent_paymentterm) from agr_mst_tapploan2supplierpayment where (application2loan_gid = '" + employee_gid + "' or application2loan_gid = '" + application2loan_gid + "' ) and application_gid = '" + application_gid + "' and apploan2supplierdtl_gid = '" + employee_gid + "'";

            lspayment_count = Convert.ToInt32(objdbconn.GetExecuteScalar(msSQL));

            if (lspayment_count == 1)
            {

                msSQL = " select maxpercent_paymentterm from agr_mst_tapploan2supplierpayment where (application2loan_gid = '" + employee_gid + "' or application2loan_gid = '" + application2loan_gid + "' ) and application_gid = '" + application_gid + "'  and apploan2supplierdtl_gid = '" + employee_gid + "'";
                values.payment_value = objdbconn.GetExecuteScalar(msSQL);
                values.status = true;
            }

            else if (lspayment_count >= 2)
            {

                msSQL = " select sum(maxpercent_paymentterm) from agr_mst_tapploan2supplierpayment where (application2loan_gid = '" + employee_gid + "'  or application2loan_gid = '" + application2loan_gid + "' ) and application_gid = '" + application_gid + "' and apploan2supplierdtl_gid = '" + employee_gid + "'";
                values.exceeded_value = objdbconn.GetExecuteScalar(msSQL);

                if (Convert.ToInt32(values.exceeded_value) > 100)
                {

                    values.message = "Payment term of 100 has been added already";
                    values.status = false;

                }

                else
                {
                    values.payment_value = values.exceeded_value;
                    values.status = true;
                }

            }

        }



        public void DaGetSupplierpaymentdtls (string apploan2supplierdtl_gid, MdlSupplierPaymentdtlList values)

        {
        
        msSQL = "   select apploan2supplierpayment_gid,application_gid,application2loan_gid, commodity_gid, commodity_name, " +
                " supplierpayment_type,supplierpayment_typegid, maxpercent_paymentterm " +
                " from agr_mst_tapploan2supplierpayment a where apploan2supplierdtl_gid = '" + apploan2supplierdtl_gid + "'";


            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlist = new List<MdlSupplierPaymentdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlist.Add(new MdlSupplierPaymentdtl
                    {
                        apploan2supplierpayment_gid = (dr_datarow["apploan2supplierpayment_gid"].ToString()),
                        commodity_gid = (dr_datarow["commodity_gid"].ToString()),
                        commodity_name = (dr_datarow["commodity_name"].ToString()),
                        supplierpayment_type = (dr_datarow["supplierpayment_type"].ToString()),
                        supplierpayment_typegid = (dr_datarow["supplierpayment_typegid"].ToString()),
                        maxpercent_paymentterm = (dr_datarow["maxpercent_paymentterm"].ToString()),
                        application2loan_gid = (dr_datarow["application2loan_gid"].ToString())
                    });
                }
                values.MdlSupplierPaymentdtl = getlist;
            }
            values.status = true;
            dt_datatable.Dispose();
        }


        public void DaUpdateGSTHeadOffice(string employee_gid, MdlGSTHeadOffice values)
        {
            msSQL = " update agr_mst_tinstitution2branch set headoffice_status = 'Yes' " +
                    " where institution2branch_gid = '" + values.institution2branch_gid + "' " +
                    " and (institution_gid = '" + employee_gid + "' or institution_gid = '" + values.institution_gid + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = " update agr_mst_tinstitution2branch set headoffice_status='No' " +
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
        public void DaEditSupplierDetailsActive(string apploan2supplierdtl_gid, MdlSupplierdtl values)
        {
            try
            {
                msSQL = " SELECT apploan2supplierdtl_gid,supplier_name,suppliername_status,remarks FROM agr_mst_tapploan2supplierdtl where apploan2supplierdtl_gid='" + apploan2supplierdtl_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.apploan2supplierdtl_gid = objODBCDatareader["apploan2supplierdtl_gid"].ToString();
                    values.supplier_name = objODBCDatareader["supplier_name"].ToString();
                    values.suppliername_status = objODBCDatareader["suppliername_status"].ToString();
                    values.remarks = objODBCDatareader["remarks"].ToString();
                  
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaInactiveSupplierDetails(MdlSupplierdtl values, string employee_gid)
        {
            msSQL = " update agr_mst_tapploan2supplierdtl set suppliername_status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where apploan2supplierdtl_gid='" + values.apploan2supplierdtl_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("SUIA");

                msSQL = " insert into agr_mst_tsupplierdetailsinactivelog (" +
                      " supplierdetailsinactivelog_gid, " +
                      " apploan2supplierdtl_gid," +
                      " supplier_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.apploan2supplierdtl_gid + "'," +
                      " '" + values.supplier_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Supplier Name Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Supplier Name Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }
        public void DaSupplierDetailsInactiveLogview(string apploan2supplierdtl_gid, MdlSupplierdtl values)
        {
            try
            {
                msSQL = " SELECT a.apploan2supplierdtl_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM agr_mst_tsupplierdetailsinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.apploan2supplierdtl_gid ='" + apploan2supplierdtl_gid + "' order by a.supplierdetailsinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMdlSupplierDetailsActive = new List<MdlSupplierDetailsActive>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMdlSupplierDetailsActive.Add(new MdlSupplierDetailsActive
                        {
                            apploan2supplierdtl_gid = (dr_datarow["apploan2supplierdtl_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.MdlSupplierDetailsActive = getMdlSupplierDetailsActive;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }


        public void DaEditAppBasic(string application_gid, MdlMstApplicationAdd values)
        {
            try
            {
                msSQL = " select a.approval_status,a.creditgroup_status from agr_mst_tapplication a" +
                      " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                      " left join adm_mst_tuser c on c.user_gid=b.user_gid where a.application_gid='" + application_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.approval_status = objODBCDatareader["approval_status"].ToString();
                    values.creditgroup_status = objODBCDatareader["creditgroup_status"].ToString();
                }
                objODBCDatareader.Close();
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "failure";
            }
        }


    }
}