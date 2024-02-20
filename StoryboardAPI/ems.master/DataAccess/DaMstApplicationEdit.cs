using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using System.Net;
using System.IO;
using ems.utilities.Functions;
using ems.master.Models;
using System.Configuration;
using System.Drawing;
using System.Net.Mail;
using ems.storage.Functions;
using ems.hbapiconn.Functions;
using ems.hbapiconn.Models;
using Newtonsoft.Json;

/// <summary>
/// (It's used for Application Creation Edit in Samfin)ApplicationEdit  DataAccess Class accessed by API methods from related Controller class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash</remarks>


namespace ems.master.DataAccess
{
    public class DaMstApplicationEdit
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DaCustomerMailTrigger objCusotmerMail = new DaCustomerMailTrigger();
        OdbcDataReader objODBCDatareader, objODBCDatareader1, objODBCDatareader2;
        HttpPostedFile httpPostedFile;
        DataTable dt_datatable, dt_tloan, dt_tcontact, dt_tinstitution, dt_thypothecation, dt_datatable1, dt_datatable2, dt_datatable3, dt_datatable4, dt_datatable5;
        string msSQL, msGetGid, lspath, msGetGid1, lsdocument_name, lsdocument_path, msGetDocumentGid, msinstitution2fpocacity_gid, msGetGID;
        int mnResult;
        string sToken = string.Empty;
        Random rand = new Random();
        string lsapplication_gid, lsapplication_no, lscompany_name, lsdate_incorporation, lscompanypan_no, lsyear_business, lsmonth_business, lscin_no;
        string lsofficial_telephoneno, lsofficial_mailid, lscompanytype_gid, lscompanytype_name, lsstakeholder_type, lsstakeholdertype_gid, lsassessmentagency_gid;
        string lsassessmentagency_name, lsassessmentagencyrating_gid, lsassessmentagencyrating_name, lsratingas_on, lsamlcategory_gid, lsamlcategory_name;
        string lsbusinesscategory_gid, lsbusinesscategory_name, lscontactperson_firstname, lscontactperson_middlename, lscontactperson_lastname, lsdesignation_gid, lsdesignation_name;
        string lsdesignation, lslastyear_turnover, lsescrow, lsstart_date, lsend_date, lsbusinessstart_date;
        string lsapplication2loan_gid, lsfacilityrequested_date, lsproduct_type, lsproducttype_gid, lsproductsub_type, lsfacilityvalidity_month, lsfacilityvalidity_days;
        string lsproductsubtype_gid, lsloantype_gid, lsloan_type, lsfacilityloan_amount, lsrate_interest,lsratemargin, lspenal_interest, lsfacilityvalidity_year;
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
        string lscontact2mobileno_gid, lscontact2email_gid, application2servicecharge_count, application2loan_count;
        string lscontact_gid, lscontact2address_gid, taggedby;
        string lsgeneticcode_gid, lsgeneticcode_name, lsgenetic_status, lsgenetic_remarks;
        string lspan_no, lsaadhar_no, lsfirst_name, lsmiddle_name, lslast_name, lsindividual_dob, lsage, lsgender_gid, lsgender_name, lseducationalqualification_gid,
               lseducationalqualification_name, lsmain_occupation, lsannual_income, lsmonthly_income, lspep_status, lspepverified_date, lsmaritalstatus_gid,
               lsmaritalstatus_name, lsfather_firstname, lsfather_middlename, lsfather_lastname, lsfather_dob, lsfather_age,
               lsmother_firstname, lsmother_middlename, lsmother_lastname, lsmother_dob, lsmother_age, lsspouse_firstname, lsspouse_middlename, lsspouse_lastname,
               lsspouse_dob, lsspouse_age, lsownershiptype_gid, lsownershiptype_name, lsresidencetype_gid, lsresidencetype_name, lscurrentresidence_years, lsbranch_distance;

        string lscustomer_urn, lscustomer_name, lsvertical_gid, lsvertical_name, lsverticaltaggs_gid, lsverticaltaggs_name,
                         lsconstitution_gid, lsconstitution_name, lsbusinessunit_gid, lsbusinessunit_name, lssa_status, lssa_id, lssa_name, lsvernacularlanguage_gid,
                         lsvernacular_language, lscontactpersonfirst_name, lscontactpersonmiddle_name, lscontactpersonlast_name, lsdesignation_type, lslandline_no;
        string lspropertyholder_gid, lspropertyholder_name, lsincometype_gid, lsincometype_name, lspreviouscrop, lsprposedcrop, lsinstitution_name;
        string lsgroup_gid, lsgroup_name, lsprofile, lsurn_status, lsurn, lsfathernominee_status, lsmothernominee_status, lsspousenominee_status, lsothernominee_status,
        lsrelationshiptype, lsnomineefirst_name, lsnominee_middlename, lsnominee_lastname, lsnominee_dob, lsnominee_age, lstotallandinacres, lscultivatedland, lsregion;
        string lsprogram, lsprogram_gid, lsprimaryvaluechain_gid, lsprimaryvaluechain_name, lssecondaryvaluechain_gid, lssecondaryvaluechain_name, lscreditgroup_gid, lscreditgroup_name, lsprogram_name;
        string lsproduct_gid, lsproduct_name, lsvariety_gid, lsvariety_name, lssector_name, lscategory_name, lsbotanical_name, lsalternative_name;
        string lsnearsamunnatiabranch_gid, lsnearsamunnatiabranch_name, lsudhayam_registration, lstan_number, lsbusiness_description, lstanstate_gid, lstanstate_name, lsinternalrating_gid, lsinternalrating_name;
        string lsphysicalstatus_gid, lsphysicalstatus_name, lscalamities_prone;
        string lssales, lspurchase, lscredit_summation, lscheque_bounce, lsnumberof_boardmeetings, lsfarmer_count, lscrop_cycle;
        private string cc_mailid;
        private IEnumerable<string> lsCCReceipients, lsBCCReceipients;
        private string body, body1;
        private string sub;
        private string sub1;
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
        int matchCount1, matchCount2;
        string lspan_status, msGetGidpan;
        string ls_relationshipmanager_name, ls_customerref_name, ls_product_name, ls_institution_gid, tomail_id1, ls_relationshipmanager_gid, ls_clustermanager_gid;
        string lsemployee_mobileno, lsemployee_emailid, lscluster_name, ls_overalllimit_amount;
        private string lsemail_toaddress;
        string lsapplprogram_gid;
        string msGetGidinstitution2documentlog, msGetGidcontact2documentlog;
        int mnResultinstitution2documentlog, mnResultcontact2documentlog;

        public void DaSocialAndTradeEdit(string application_gid, MdlMstApplicationEdit values)
        {
            try
            {
                msSQL = " select application_gid, social_capital, trade_capital from ocs_mst_tapplication where application_gid='" + application_gid + "'";
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
            msSQL = " update ocs_mst_tapplication set ";
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
                    " from ocs_mst_tcontact2bureau where contact2bureau_gid='" + contact2bureau_gid + "'";
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
            msSQL = "select cicdocument_name from ocs_mst_tcontact where contact_gid='" + contact_gid + "'";
            lsdocument_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select cicdocument_path from ocs_mst_tcontact where contact_gid='" + contact_gid + "'";
            lsdocument_path = objdbconn.GetExecuteScalar(msSQL);
            if (lsdocument_name != null && lsdocument_name != "")
            {

                msSQL = "delete from ocs_tmp_tcicdocument where created_by='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " insert into ocs_tmp_tcicdocument( " +
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
                        " from ocs_mst_tinstitution2bureau where institution2bureau_gid='" + institution2bureau_gid + "'";
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
            msSQL = "select cicdocument_name from ocs_mst_tinstitution where institution_gid='" + institution_gid + "'";
            lsdocument_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select cicdocument_path from ocs_mst_tinstitution where institution_gid='" + institution_gid + "'";
            lsdocument_path = objdbconn.GetExecuteScalar(msSQL);
            if (lsdocument_path != null && lsdocument_path != "")
            {

                msSQL = "delete from ocs_tmp_tcicdocument where created_by='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " insert into ocs_tmp_tcicdocument( " +
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
            msSQL = "select document_name from ocs_tmp_tcicdocument where created_by='" + employee_gid + "'";
            lsdocument_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select document_path from ocs_tmp_tcicdocument where created_by='" + employee_gid + "'";
            lsdocument_path = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " update ocs_mst_tcontact set " +
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
            msSQL = "select document_name from ocs_tmp_tcicdocument where created_by='" + employee_gid + "'";
            lsdocument_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select document_path from ocs_tmp_tcicdocument where created_by='" + employee_gid + "'";
            lsdocument_path = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " update ocs_mst_tinstitution set " +
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

            msSQL = " SELECT count(tmpcicdocument_gid) FROM ocs_tmp_tcicdocument where created_by = '" + employee_gid + "' ";
            string count = objdbconn.GetExecuteScalar(msSQL);

            int counts = Convert.ToInt32(count);

            if (counts < 1)
            {

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

                            bool status;
                            status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/CICUploadIndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                            ms.Close();
                            lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/CICUploadIndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                            msSQL = " insert into ocs_tmp_tcicdocument( " +
                                        " document_name ," +
                                        " document_path," +
                                        " created_by," +
                                        " created_date" +
                                        " )values(" +
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

            }
            else
            {
                objfilename.message = "Only One file should Upload..!";
            }
            return true;
        }

        public void DaTempCICUploadDocDelete(string employee_gid, MdlCICIndividual values)
        {
            msSQL = " delete from ocs_tmp_tcicdocument where created_by='" + employee_gid + "'";
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
                        " FROM ocs_mst_tapplication a left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
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
                    " from ocs_mst_tcontact a " +
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
                    " from ocs_mst_tinstitution a " +
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
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/InstitutionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/InstitutionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/InstitutionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msSQL = "select covenant_type from ocs_mst_tcompanydocument where companydocument_gid='" + lscompanydocument_gid + "'";
                        string lscovenant_type = objdbconn.GetExecuteScalar(msSQL);

                        msGetGid = objcmnfunctions.GetMasterGID("INDO");
                        msSQL = " insert into ocs_mst_tinstitution2documentupload( " +
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
                                    "'" + lsdocument_title.Replace("'", "") + "'," +
                                    "'" + lsdocument_id.Replace("'", "") + "'," +
                                    "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                    "'" + lscompanydocument_gid + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension.Replace("'", "") + "'," +
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
            msSQL = " select institution2documentupload_gid,institution_gid,document_name,document_path,document_title, " +
                    " document_id,migration_flag,documenttype_name from ocs_mst_tinstitution2documentupload " +
                    " where institution_gid='" + employee_gid + "' or institution_gid='" + institution_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<institutionupload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new institutionupload_list
                    {
                        migration_flag = dt["migration_flag"].ToString(),
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
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
            msSQL = "delete from ocs_mst_tinstitution2documentupload where institution2documentupload_gid='" + institution2documentupload_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                msSQL = " select groupdocumentchecklist_gid from ocs_trn_tdocumentchecktls where documentuploaded_gid='" + institution2documentupload_gid + "'";
                string lsgroupdocumentchecklist_gid = objdbconn.GetExecuteScalar(msSQL);

                if (lsgroupdocumentchecklist_gid != "")
                {
                    msSQL = " select count(documentcheckdtl_gid) as documentcount from ocs_trn_tdocumentchecktls " +
                            " where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                    string lsdocumentcount = objdbconn.GetExecuteScalar(msSQL);
                    if (lsdocumentcount == "1")
                    {
                        msSQL = "delete from ocs_trn_tgroupdocumentchecklist where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                msSQL = " select groupcovdocumentchecklist_gid from ocs_trn_tcovanantdocumentcheckdtls where documentuploaded_gid='" + institution2documentupload_gid + "'";
                string lschecklist_gid = objdbconn.GetExecuteScalar(msSQL);

                if (lschecklist_gid != "")
                {
                    msSQL = " select count(covenantdocumentcheckdtl_gid) as documentcount from ocs_trn_tcovanantdocumentcheckdtls " +
                      " where groupcovdocumentchecklist_gid='" + lschecklist_gid + "'";
                    string lsdocumentcount = objdbconn.GetExecuteScalar(msSQL);
                    if (lsdocumentcount == "1")
                    {
                        msSQL = "delete from ocs_trn_tgroupcovenantdocumentchecklist where groupcovdocumentchecklist_gid='" + lschecklist_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                msSQL = "delete from ocs_trn_tcovanantdocumentcheckdtls where documentuploaded_gid='" + institution2documentupload_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "delete from ocs_trn_tdocumentchecktls where documentuploaded_gid='" + institution2documentupload_gid + "'";
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

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/IntitutionForm_60Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/IntitutionForm_60Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/IntitutionForm_60Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("IF6D");
                        msSQL = " insert into ocs_mst_tinstitution2form60documentupload( " +
                                    " institution2form60documentupload_gid, " +
                                    " institution_gid," +
                                    " form60document_name ," +
                                    " form60document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + httpPostedFile.FileName.Replace("'", " ") + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension.Replace("'", " ") + "'," +
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
            msSQL = " select institution2form60documentupload_gid,form60document_name,form60document_path from ocs_mst_tinstitution2form60documentupload " +
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
                        document_path = objcmnstorage.EncryptData(dt["form60document_path"].ToString()),
                        institution2form60documentupload_gid = dt["institution2form60documentupload_gid"].ToString()
                    });
                    values.institutionupload_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaInstitutionEditForm_60DocumentDelete(string institution2form60documentupload_gid, institutionuploaddocument objfilename, string employee_gid)
        {
            msSQL = "delete from ocs_mst_tinstitution2form60documentupload where institution2form60documentupload_gid='" + institution2form60documentupload_gid + "'";
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
            msSQL = "select institution2branch_gid,gst_state,gst_no, gst_registered,headoffice_status " +
                    " from ocs_mst_tinstitution2branch where institution_gid='" + institution_gid + "'";
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
            msSQL = "select mobile_no,institution2mobileno_gid,primary_status,whatsapp_no from ocs_mst_tinstitution2mobileno where " +
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
            msSQL = "select email_address,institution2email_gid,primary_status from ocs_mst_tinstitution2email where institution_gid='" + institution_gid + "'";
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
                    " postal_code from ocs_mst_tinstitution2address where institution_gid='" + institution_gid + "'";
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
                    " date_format(expiry_date,'%d-%m-%Y') as expiry_date from ocs_mst_tinstitution2licensedtl" +
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
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as uploaded_date, migration_flag,documenttype_name" +
                    " from ocs_mst_tinstitution2documentupload a" +
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
                        migration_flag = dt["migration_flag"].ToString(),
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
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
                    " from ocs_mst_tinstitution2form60documentupload a" +
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
                        document_path = objcmnstorage.EncryptData(dt["form60document_path"].ToString()),
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
                   " official_telephoneno, officialemail_address,msme_regi_no,kin_no,lei_no,renewaldue_date, companytype_gid, companytype_name, stakeholder_type, stakeholdertype_gid, assessmentagency_gid, " +
                   " assessmentagency_name, assessmentagencyrating_gid, assessmentagencyrating_name, ratingas_on, amlcategory_gid, amlcategory_name,businesscategory_gid, " +
                   " businesscategory_name, contactperson_firstname, contactperson_middlename, contactperson_lastname, designation_gid, designation, " +
                   " lastyear_turnover, escrow, start_date, end_date, institution_status, urn, urn_status, " +
                   " nearsamunnatiabranch_gid,nearsamunnatiabranch_name,udhayam_registration,tan_number,business_description, " +
                   " tanstate_gid,tanstate_name,internalrating_gid,internalrating_name,sales, purchase, credit_summation, " +
                   " cheque_bounce, numberof_boardmeetings, farmer_count, crop_cycle, calamities_prone " +
                   " from ocs_mst_tinstitution where institution_gid='" + institution_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.application_no = objODBCDatareader["application_no"].ToString();
                    values.company_name = objODBCDatareader["company_name"].ToString();

                    values.msme_regi_no = objODBCDatareader["msme_regi_no"].ToString();
                    values.kin_no = objODBCDatareader["kin_no"].ToString();
                    values.lei_no = objODBCDatareader["lei_no"].ToString();
                    if (objODBCDatareader["renewaldue_date"].ToString() == "")
                    {
                    }
                    else
                    {
                        values.renewaldue_date = Convert.ToDateTime(objODBCDatareader["renewaldue_date"]).ToString("dd-MM-yyyy");
                    }
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
                    values.nearsamunnatiabranch_gid = objODBCDatareader["nearsamunnatiabranch_gid"].ToString();
                    values.nearsamunnatiabranch_name = objODBCDatareader["nearsamunnatiabranch_name"].ToString();
                    values.udhayam_registration = objODBCDatareader["udhayam_registration"].ToString();
                    values.tan_number = objODBCDatareader["tan_number"].ToString();
                    values.business_description = objODBCDatareader["business_description"].ToString();
                    values.tanstate_gid = objODBCDatareader["tanstate_gid"].ToString();
                    values.tanstate_name = objODBCDatareader["tanstate_name"].ToString();
                    values.internalrating_gid = objODBCDatareader["internalrating_gid"].ToString();
                    values.internalrating_name = objODBCDatareader["internalrating_name"].ToString();
                    values.sales = objODBCDatareader["sales"].ToString();
                    values.purchase = objODBCDatareader["purchase"].ToString();
                    values.credit_summation = objODBCDatareader["credit_summation"].ToString();
                    values.cheque_bounce = objODBCDatareader["cheque_bounce"].ToString();
                    values.numberof_boardmeetings = objODBCDatareader["numberof_boardmeetings"].ToString();
                    values.farmer_count = objODBCDatareader["farmer_count"].ToString();
                    values.crop_cycle = objODBCDatareader["crop_cycle"].ToString();
                    values.calamities_prone = objODBCDatareader["calamities_prone"].ToString();
                }

                msSQL = "select city_gid,city_name from ocs_mst_tinstitution2fpocacity where institution_gid='" + institution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                var get_fpocity_list = new List<fpocity_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    values.fpocity_list = dt_datatable.AsEnumerable().Select(row =>
                      new fpocity_list
                      {
                          city_gid = row["city_gid"].ToString(),
                          city_name = row["city_name"].ToString()
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();

                msSQL = " Select city_gid,city_name FROM ocs_mst_tcity ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcityedit_list = new List<cityedit_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcityedit_list.Add(new cityedit_list
                        {
                            city_gid = (dr_datarow["city_gid"].ToString()),
                            city_name = (dr_datarow["city_name"].ToString())
                        });
                    }
                    values.cityedit_list = getcityedit_list;
                }
                dt_datatable.Dispose();

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
                    " from ocs_mst_tinstitution2documentupload a where a.documenttype_gid = 'DOCT2022010611' and " +
                    " ( institution_gid='" + values.institution_gid + "' or institution_gid = '" + employee_gid + "') ";
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

            msSQL = "select institution2mobileno_gid from ocs_mst_tinstitution2mobileno where (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "') and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Mobile Number";
                return false;
            }

            msSQL = "select institution2mobileno_gid from ocs_mst_tinstitution2mobileno where institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Mobile Number";
                return false;
            }

            msSQL = "select institution2email_gid from ocs_mst_tinstitution2email where institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Email Address";
                return false;
            }

            msSQL = "select institution2address_gid from ocs_mst_tinstitution2address where institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Address Detail";
                return false;
            }
            if (values.Gstflag == "Yes")
            {
                msSQL = "select institution2branch_gid from ocs_mst_tinstitution2branch where (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "') and headoffice_status ='Yes' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == false)
                {
                    values.status = false;
                    values.message = "Atleast Select One GST Number as Head Office";
                    return false;
                }
                objODBCDatareader.Close();
            }

            msSQL = "select application_gid from ocs_mst_tinstitution where institution_gid='" + values.institution_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            if (values.stakeholder_type == "Borrower" || values.stakeholder_type == "Applicant")
            {
                msSQL = "select stakeholder_type from ocs_mst_tcontact where application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Borrower','Applicant')";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Already Added Applicant/Borrower Information";
                    return false;
                }
                msSQL = "select stakeholder_type from ocs_mst_tinstitution where application_gid='" + lsapplication_gid + "' and" +
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
                     " lastyear_turnover, escrow, start_date, end_date, urn_status, urn,nearsamunnatiabranch_gid,nearsamunnatiabranch_name,udhayam_registration, " +
                     " tan_number,business_description,tanstate_gid,tanstate_name,internalrating_gid,internalrating_name, " +
                     " sales, purchase, credit_summation, cheque_bounce, numberof_boardmeetings, farmer_count, crop_cycle,calamities_prone " +
                     " from ocs_mst_tinstitution where institution_gid='" + values.institution_gid + "'";
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
                lsnearsamunnatiabranch_gid = objODBCDatareader["nearsamunnatiabranch_gid"].ToString();
                lsnearsamunnatiabranch_name = objODBCDatareader["nearsamunnatiabranch_name"].ToString();
                lsudhayam_registration = objODBCDatareader["udhayam_registration"].ToString();
                lstan_number = objODBCDatareader["tan_number"].ToString();
                lsbusiness_description = objODBCDatareader["business_description"].ToString();
                lstanstate_gid = objODBCDatareader["tanstate_gid"].ToString();
                lstanstate_name = objODBCDatareader["tanstate_name"].ToString();
                lsinternalrating_gid = objODBCDatareader["internalrating_gid"].ToString();
                lsinternalrating_name = objODBCDatareader["internalrating_name"].ToString();
                lssales = objODBCDatareader["sales"].ToString();
                lspurchase = objODBCDatareader["purchase"].ToString();
                lscredit_summation = objODBCDatareader["credit_summation"].ToString();
                lscheque_bounce = objODBCDatareader["cheque_bounce"].ToString();
                lsnumberof_boardmeetings = objODBCDatareader["numberof_boardmeetings"].ToString();
                lsfarmer_count = objODBCDatareader["farmer_count"].ToString();
                lscrop_cycle = objODBCDatareader["crop_cycle"].ToString();
                lscalamities_prone = objODBCDatareader["calamities_prone"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update ocs_mst_tinstitution set " +
                        " company_name='" + values.company_name.Replace("'", "\\'") + "',";
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
                if (values.business_description == null || values.business_description == "")
                {
                    lsbusiness_description = "";
                }
                else
                {
                    lsbusiness_description = values.business_description.Replace("'", "");
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
                if (Convert.ToDateTime(values.Renewaldue_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " renewaldue_date='" + Convert.ToDateTime(values.Renewaldue_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                msSQL += " lastyear_turnover='" + values.lastyear_turnover + "'," +
                         " escrow='" + values.escrow + "'," +
                         " urn_status='" + values.urn_status + "'," +
                         " urn='" + values.urn + "'," +
                         " nearsamunnatiabranch_gid ='" + values.nearsamunnatiabranch_gid + "'," +
                         " nearsamunnatiabranch_name ='" + values.nearsamunnatiabranch_name + "'," +
                         " udhayam_registration ='" + values.udhayam_registration + "'," +
                         " tan_number ='" + values.tan_number + "'," +                       
                         " business_description='" + lsbusiness_description + "'," +
                         " tanstate_gid ='" + values.tanstate_gid + "'," +
                         " tanstate_name ='" + values.tanstate_name + "'," +
                         " internalrating_gid ='" + values.internalrating_gid + "'," +
                         " internalrating_name ='" + values.internalrating_name + "'," +
                         " sales ='" + values.sales + "'," +
                         " purchase ='" + values.purchase + "'," +
                         " credit_summation ='" + values.credit_summation + "'," +
                         " cheque_bounce ='" + values.cheque_bounce + "'," +
                         " numberof_boardmeetings ='" + values.numberof_boardmeetings + "'," +
                         " farmer_count ='" + values.farmer_count + "'," +
                         " crop_cycle ='" + values.crop_cycle + "'," +
                         " calamities_prone ='" + values.calamities_prone + "'," +
                         " msme_regi_no ='" + values.msme_regi_no + "'," +
                         " lei_no ='" + values.lei_no + "'," +
                         " kin_no ='" + values.kin_no + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where institution_gid='" + values.institution_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msSQL = " delete from ocs_mst_tinstitution2fpocacity where institution_gid = '" + values.institution_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        for (var i = 0; i < values.fpocity_list.Count; i++)
                        {
                            msinstitution2fpocacity_gid = objcmnfunctions.GetMasterGID("I2FC");
                            msSQL = " Insert into ocs_mst_tinstitution2fpocacity( " +
                               " institution2fpocacity_gid, " +
                               " institution_gid, " +
                               " city_gid," +
                               " city_name," +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msinstitution2fpocacity_gid + "'," +
                               "'" + values.institution_gid + "'," +
                               "'" + values.fpocity_list[i].city_gid + "'," +
                               "'" + values.fpocity_list[i].city_name + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }

                    msGetGid = objcmnfunctions.GetMasterGID("IULG");

                    msSQL = " insert into ocs_mst_tinstitutionupdateLOG(" +
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
                            " calamities_prone, " +
                            " created_by," +
                            " created_date) " +
                            " values (" +
                               "'" + msGetGid + "'," +
                               "'" + values.institution_gid + "'," +
                               "'" + employee_gid + "'," +
                               "'" + employee_gid + "'," +
                               "'" + lscompany_name.Replace("'", "\\'") + "'," +
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
                               "'" + lsnearsamunnatiabranch_gid + "'," +
                               "'" + lsnearsamunnatiabranch_name + "'," +
                               "'" + lsudhayam_registration + "'," +
                               "'" + lstan_number + "'," +
                               "'" + lsbusiness_description.Replace("'", "") + "'," +
                               "'" + lstanstate_gid + "'," +
                               "'" + lstanstate_name + "'," +
                               "'" + lsinternalrating_gid + "'," +
                               "'" + lsinternalrating_name + "'," +
                               "'" + lssales + "'," +
                               "'" + lspurchase + "'," +
                               "'" + lscredit_summation + "'," +
                               "'" + lscheque_bounce + "'," +
                               "'" + lsnumberof_boardmeetings + "'," +
                               "'" + lsfarmer_count + "'," +
                               "'" + lscrop_cycle + "'," +
                               "'" + lscalamities_prone + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    // Updates for Multiple Add
                    msSQL = "update ocs_mst_tinstitution2branch set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update ocs_mst_tinstitution2mobileno set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update ocs_mst_tinstitution2email set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update ocs_mst_tinstitution2address set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update ocs_mst_tinstitution2licensedtl set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update ocs_mst_tinstitution2equipment set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update ocs_mst_tinstitution2livestock set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update ocs_mst_tinstitution2receivable set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "select institution2documentupload_gid,companydocument_gid from ocs_mst_tinstitution2documentupload where institution_gid='" + employee_gid + "'";
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
                        msSQL = " insert into ocs_trn_tdocumentchecktls(" +
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
                            msSQL = " insert into ocs_trn_tcovanantdocumentcheckdtls(" +
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

                    DaMstScannedDocument objvalues = new DaMstScannedDocument();
                    objvalues.DaGroupDocChecklistinfoCredit(lsapplication_gid, values.institution_gid, employee_gid);

                    msSQL = "update ocs_mst_tinstitution2documentupload set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update ocs_mst_tinstitution2form60documentupload set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        msSQL = "select stakeholder_type from ocs_mst_tinstitution where institution_gid='" + values.institution_gid + "' ";
                        string lsstakeholders_type = objdbconn.GetExecuteScalar(msSQL);

                        if (lsstakeholders_type == "Applicant" || lsstakeholders_type == "Borrower")
                        {
                            msSQL = "select mobile_no from ocs_mst_tinstitution2mobileno where institution_gid='" + values.institution_gid + "' and primary_status='yes'";
                            string lsmobileno = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = "select email_address from ocs_mst_tinstitution2email where institution_gid='" + values.institution_gid + "' and primary_status='yes'";
                            lsemail_address = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = "select company_name,institution_gid,urn,stakeholder_type from ocs_mst_tinstitution where " +
                                    " application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Applicant','Borrower')";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lscustomer_name = objODBCDatareader["company_name"].ToString();
                                lsurn = objODBCDatareader["urn"].ToString();
                                lsstakeholder_type = objODBCDatareader["stakeholder_type"].ToString();

                                //Region
                                msSQL = "select state from ocs_mst_tinstitution2address where primary_status='Yes' and institution_gid='" + objODBCDatareader["institution_gid"].ToString() + "'";
                                lsregion = objdbconn.GetExecuteScalar(msSQL);

                                //Main Table 
                                msSQL = " update ocs_mst_tapplication set customer_name='" + lscustomer_name.Replace("'", "\\'") + "'," +
                                        " mobile_no='" + lsmobile_no + "'," +
                                        " email_address='" + lsemail_address + "'," +
                                        " region='" + lsregion + "'," +
                                        " customer_urn='" + lsurn + "'," +
                                        " applicant_type='Institution'," +
                                        " updated_by='" + employee_gid + "'," +
                                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                        " where application_gid='" + lsapplication_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                msSQL = " update ocs_mst_tinstitution set mobile_no='" + lsmobileno + "'," +
                                        " email_address='" + lsemail_address + "' where institution_gid='" + values.institution_gid + "' ";
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
                values.message = "Error Occured..";
                return false;
            }
        }

        public void DaInstitutionGSTTmpList(string institution_gid, string employee_gid, MdlMstGST values)
        {
            msSQL = "select institution2branch_gid,gst_state,gst_no, gst_registered,headoffice_status from ocs_mst_tinstitution2branch " +
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
            msSQL = "select mobile_no,institution2mobileno_gid,primary_status,whatsapp_no from ocs_mst_tinstitution2mobileno where " +
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
            msSQL = "select email_address,institution2email_gid,primary_status from ocs_mst_tinstitution2email " +
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
                    " postal_code from ocs_mst_tinstitution2address where institution_gid='" + institution_gid + "' or institution_gid='" + employee_gid + "'";
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
                    " date_format(expiry_date,'%d-%m-%Y') as expiry_date from ocs_mst_tinstitution2licensedtl" +
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
                msSQL = " select application_gid, overalllimit_amount, validityoveralllimit_year, validityoveralllimit_month, validityoveralllimit_days, calculationoveralllimit_validity," +
                        " enduse_purpose, processing_fee, processing_collectiontype, doc_charges, doccharge_collectiontype, fieldvisit_charge, fieldvisit_collectiontype, " +
                        " adhoc_fee, adhoc_collectiontype, life_insurance, lifeinsurance_collectiontype, acct_insurance, total_collect, total_deduct, productcharges_status, " +
                        " csa_applicability,csaactivity_gid,csaactivity_name,percentageoftotal_limit " +
                        " from ocs_mst_tapplication where application_gid='" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.overalllimit_amount = objODBCDatareader["overalllimit_amount"].ToString();
                    values.validityoveralllimit_year = objODBCDatareader["validityoveralllimit_year"].ToString();
                    values.validityoveralllimit_month = objODBCDatareader["validityoveralllimit_month"].ToString();
                    values.validityoveralllimit_days = objODBCDatareader["validityoveralllimit_days"].ToString();
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
                    values.csa_applicability = objODBCDatareader["csa_applicability"].ToString();
                    values.csaactivity_gid = objODBCDatareader["csaactivity_gid"].ToString();
                    values.csaactivity_name = objODBCDatareader["csaactivity_name"].ToString();
                    values.percentageoftotal_limit = objODBCDatareader["percentageoftotal_limit"].ToString();
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
                               " productsub_type, loanfacility_amount, loan_type, rate_interest, penal_interest, facilityoverall_limit, " +
                               " tenureoverall_limit, facility_type, facility_mode, principalfrequency_name, interestfrequency_name, " +
                               " interest_status, moratorium_type, moratorium_status, date_format(moratorium_startdate, '%d-%m-%Y') as moratorium_startdate,application2loan_gid, " +
                               " date_format(moratorium_enddate, '%d-%m-%Y') as moratorium_enddate from ocs_mst_tapplication2loan " +
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
            msSQL = "select application2loan_gid from ocs_mst_tapplication2loan where product_type='Agri Receivable Finance (ARF)' and " +
            " (application_gid='" + application_gid + "' or application_gid='" + employee_gid + "')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.buyer_status = "Y";
            }
            objODBCDatareader.Close();

            msSQL = "select application2loan_gid from ocs_mst_tapplication2loan where loan_type='Secured' and" +
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
                               " productsub_type, format(loanfacility_amount,0,'en_IN') as loanfacility_amount, loan_type, rate_interest, penal_interest, facilityoverall_limit, " +
                               " tenureoverall_limit, facility_type, facility_mode, principalfrequency_name, interestfrequency_name, " +
                               " interest_status, moratorium_type, moratorium_status, date_format(moratorium_startdate, '%d-%m-%Y') as moratorium_startdate,application2loan_gid, " +
                               " date_format(moratorium_enddate, '%d-%m-%Y') as moratorium_enddate" +
                               " from ocs_mst_tapplication2loan where application_gid='" + application_gid + "' or application_gid='" + employee_gid + "'";
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
                msSQL = " select application2loan_gid, application_gid, date_format(facilityrequested_date,'%Y-%m-%d') as facilityrequested_dateedit, product_type, producttype_gid, productsub_type, productsubtype_gid," +
                      " loantype_gid, loan_type, loanfacility_amount, rate_interest,margin, penal_interest, facilityvalidity_year, facilityvalidity_month, facilityvalidity_days," +
                      " facilityoverall_limit, tenureproduct_year, tenureproduct_month, tenureproduct_days, tenureoverall_limit, facility_type,facility_mode, " +
                      " scheme_type, principalfrequency_name, principalfrequency_gid, interestfrequency_name, interestfrequency_gid, program, program_gid, primaryvaluechain_gid, primaryvaluechain_name, secondaryvaluechain_gid, secondaryvaluechain_name, interest_status, moratorium_status, enduse_purpose, " +
                      " moratorium_type, date_format(moratorium_startdate,'%Y-%m-%d') as moratorium_startdateedit, date_format(moratorium_enddate,'%Y-%m-%d') as moratorium_enddateedit, " +
                      " source_type, guideline_value, date_format(guideline_date,'%Y-%m-%d') as guideline_dateedit, date_format(marketvalue_date,'%Y-%m-%d') as marketvalue_dateedit," +
                      " market_value, forcedsource_value, collateralSSV_value, date_format(forcedvalueassessed_on,'%Y-%m-%d') as forcedvalueassessed_onedit, collateralobservation_summary," +
                      " product_gid,product_name,variety_gid,variety_name,sector_name,category_name,botanical_name,alternative_name " +
                      " from ocs_mst_tapplication2loan where application2loan_gid='" + application2loan_gid + "'";
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
                    values.roi_margin = objODBCDatareader["margin"].ToString();
                    values.penal_interest = objODBCDatareader["penal_interest"].ToString();
                    values.facilityvalidity_year = objODBCDatareader["facilityvalidity_year"].ToString();
                    values.facilityvalidity_month = objODBCDatareader["facilityvalidity_month"].ToString();
                    values.facilityvalidity_days = objODBCDatareader["facilityvalidity_days"].ToString();
                    values.facilityoverall_limit = objODBCDatareader["facilityoverall_limit"].ToString();
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

                    //String[] privalchaingid_list = objODBCDatareader["primaryvaluechain_gid"].ToString().Split(',');
                    //String[] privalchainname_list = objODBCDatareader["primaryvaluechain_name"].ToString().Split(',');

                    //var getprimaryvaluechainList = new List<primaryvaluechain_list>();

                    //for (var i = 0; i < privalchaingid_list.Length; i++)
                    //{
                    //    getprimaryvaluechainList.Add(new primaryvaluechain_list
                    //    {
                    //        valuechain_gid = privalchaingid_list[i],
                    //        valuechain_name = privalchainname_list[i],
                    //    });

                    //}
                    //values.primaryvaluechain_list = getprimaryvaluechainList;

                    //String[] secvalchaingid_list = objODBCDatareader["secondaryvaluechain_gid"].ToString().Split(',');
                    //String[] secvalchainname_list = objODBCDatareader["secondaryvaluechain_name"].ToString().Split(',');

                    //var getsecondaryvaluechainList = new List<secondaryvaluechain_list>();

                    //for (var i = 0; i < secvalchaingid_list.Length; i++)
                    //{
                    //    getsecondaryvaluechainList.Add(new secondaryvaluechain_list
                    //    {
                    //        valuechain_gid = secvalchaingid_list[i],
                    //        valuechain_name = secvalchainname_list[i],
                    //    });

                    //}
                    //values.secondaryvaluechain_list = getsecondaryvaluechainList;


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
               " loantype_gid, loan_type, loanfacility_amount, rate_interest,margin, penal_interest, facilityvalidity_year, facilityvalidity_month, facilityvalidity_days," +
               " facilityoverall_limit, tenureproduct_year, tenureproduct_month, tenureproduct_days, tenureoverall_limit, facility_type,facility_mode, " +
               " scheme_type, principalfrequency_name, principalfrequency_gid, interestfrequency_name, interestfrequency_gid, program, program_gid, primaryvaluechain_gid, primaryvaluechain_name, secondaryvaluechain_gid, secondaryvaluechain_name, interest_status, moratorium_status, " +
               " moratorium_type, moratorium_startdate, moratorium_enddate, enduse_purpose,product_gid,product_name," +
               " variety_gid,variety_name,sector_name,category_name,botanical_name,alternative_name " +
               " from ocs_mst_tapplication2loan where application2loan_gid='" + values.application2loan_gid + "'";
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
                lsratemargin = objODBCDatareader["margin"].ToString();
                lspenal_interest = objODBCDatareader["penal_interest"].ToString();
                lsfacilityvalidity_year = objODBCDatareader["facilityvalidity_year"].ToString();
                lsfacilityvalidity_month = objODBCDatareader["facilityvalidity_month"].ToString();
                lsfacilityvalidity_days = objODBCDatareader["facilityvalidity_days"].ToString();
                lsfacilityoverall_limit = objODBCDatareader["facilityoverall_limit"].ToString();
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
            }
            try
            {
                // msSQL = "select application2loan_gid from ocs_mst_tapplication2loan where producttype_gid='" + values.producttype_gid + "' and " +
                //" productsubtype_gid='" + values.productsubtype_gid + "' and application_gid='" + values.application_gid + "' and  " +
                //" application2loan_gid<>'" + values.application2loan_gid + "'"; ;
                // objODBCDatareader = objdbconn.GetDataReader(msSQL);
                // if (objODBCDatareader.HasRows == false)
                // {
                //     objODBCDatareader.Close();

                msSQL = "select producttype_gid from ocs_mst_tapplication2loan where  application2loan_gid='" + values.application2loan_gid + "' ";
                string application2loanlist_count1 = objdbconn.GetExecuteScalar(msSQL);
                if (application2loanlist_count1 != values.producttype_gid)
                {
                    msSQL = "select COUNT(application2servicecharge_gid) from ocs_mst_tapplicationservicecharge where producttype_gid like '%" + application2loanlist_count1 + "%' and  application_gid='" + values.application_gid + "'";
                string application2servicechargelist_count1 = objdbconn.GetExecuteScalar(msSQL);

                if (application2servicechargelist_count1 != "0")
                {
                    values.status = false;
                    values.message = "Product Type Already Added in Service Charges";
                    return;
                }
            }
                if (values.product_type == "Agri Receivable Finance (ARF)")
                {
                    msSQL = "select application2buyer_gid from ocs_mst_tapplication2buyer  where application2loan_gid='" + employee_gid + "' or " +
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

                        // Product Loan Details With Encore Master Validation - Started
                        if (values.loandetailsvalidation_flag == "Yes")
                        {
                            FnSamFinEncoreLoanAccount objFnSamFinEncoreLoanAccount = new FnSamFinEncoreLoanAccount();

                            MdlProductLoanDetails objMdlProductLoanDetails = new MdlProductLoanDetails();

                            objMdlProductLoanDetails.product = values.producttype_gid;
                            objMdlProductLoanDetails.sub_product = values.productsubtype_gid;
                            objMdlProductLoanDetails.principal_frequency = values.principalfrequency_gid;
                            objMdlProductLoanDetails.interest_frequency = values.interestfrequency_gid;
                            objMdlProductLoanDetails.interestdeduction_upfront = values.interest_status;
                            objMdlProductLoanDetails.moratorium_status = values.moratorium_status;
                            objMdlProductLoanDetails.moratorium_type = values.moratorium_type;

                            objMdlProductLoanDetails.facilityvalidity_days = values.facilityvalidity_days;
                            objMdlProductLoanDetails.facilityvalidity_month = values.facilityvalidity_month;
                            objMdlProductLoanDetails.facilityvalidity_year = values.facilityvalidity_year;

                            string ProductLoanDetails = JsonConvert.SerializeObject(objMdlProductLoanDetails);

                            objFnSamFinEncoreLoanAccount.LogForAuditProductLoanDetails("DaMstApplicationEdit - Function : DaLoanDetailsUpdate - MstApplicationLoanEdit or MstCreditLoanDtlEdit . Log Time - " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + " " + ProductLoanDetails);

                            ProductLoanDetailsWithEncoreMasterValidationResponse objProductLoanDetailsWithEncoreMasterValidationResponse = new ProductLoanDetailsWithEncoreMasterValidationResponse();

                            objProductLoanDetailsWithEncoreMasterValidationResponse = objFnSamFinEncoreLoanAccount.ProductLoanDetailsWithEncoreMasterValidation(objMdlProductLoanDetails);

                            if (objProductLoanDetailsWithEncoreMasterValidationResponse.status == false && objProductLoanDetailsWithEncoreMasterValidationResponse.message == "InValid")
                            {
                                values.status = false;
                                values.message = "Invalid Tenure Units";
                                return;
                            }
                            if (objProductLoanDetailsWithEncoreMasterValidationResponse.status == false)
                            {
                                values.status = false;
                                values.message = "Product Terms doesn't match with those available in Encore Master";
                                return;
                            }
                        }                        
                        // Product Loan Details With Encore Master Validation - Ended

                        msSQL = " update ocs_mst_tapplication2loan set " +
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
                                 " loanfacility_amount='" + values.facilityloan_amount.Replace(",", "") + "'," +
                                 " rate_interest='" + values.rate_interest + "'," +
                                  " margin='" + values.margin + "'," +
                                 " penal_interest='" + values.penal_interest + "'," +
                                 " facilityvalidity_year='" + values.facilityvalidity_year + "'," +
                                 " facilityvalidity_month='" + values.facilityvalidity_month + "'," +
                                 " facilityvalidity_days='" + values.facilityvalidity_days + "'," +
                                 " facilityoverall_limit='" + values.facilityoverall_limit + "'," +
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


                        msSQL += " enduse_purpose='" + values.enduse_purpose.Replace("'", "") + "'," +

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
                                 " updated_by='" + employee_gid + "'," +
                                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                 " where application2loan_gid='" + values.application2loan_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult == 1)
                        {

                            msSQL = "update ocs_mst_tapplication2product set application2loan_gid='" + values.application2loan_gid + "' where application2loan_gid='" + employee_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (values.product_type == "Agri Receivable Finance (ARF)")
                            {
                                msSQL = "update ocs_mst_tapplication2buyer set application2loan_gid='" + values.application2loan_gid + "' where application2loan_gid='" + employee_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            else
                            {
                                msSQL = "delete from  ocs_mst_tapplication2buyer  where application2loan_gid='" + employee_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            }
                            if (values.loan_type == "Secured")
                            {
                                msSQL = "update ocs_mst_tuploadcollateraldocument set application2loan_gid='" + values.application2loan_gid + "' where application2loan_gid='" + employee_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            else
                            {
                                msSQL = "delete from ocs_mst_tuploadcollateraldocument where application2loan_gid='" + employee_gid + "' or application2loan_gid='" + values.application2loan_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            msGetGid = objcmnfunctions.GetMasterGID("A2LU");
                            msSQL = " insert into ocs_mst_tapplication2loanupdateLOG(" +
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
                                    " margin," +
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
                                   "'" + lsfacilityloan_amount + "'," +
                                   "'" + lsrate_interest + "'," +
                                   "'" + lsratemargin + "',";                                
                            if (lspenal_interest == null || lspenal_interest == "")
                            {
                                msSQL += "'0.00',";
                            }
                            else
                            {
                                msSQL += "'" + lspenal_interest.Replace(",", "") + "',";
                            }
                          msSQL += "'" + lsfacilityvalidity_year + "'," +
                                   "'" + lsfacilityvalidity_month + "'," +
                                   "'" + lsfacilityvalidity_days + "'," +
                                   "'" + lsfacilityoverall_limit + "'," +
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
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            values.status = true;
                            values.message = "Loan Details Updated Successfully";


                        }
                    }
                }
                else
                {
                    // Product Loan Details With Encore Master Validation - Started
                    if (values.loandetailsvalidation_flag == "Yes")
                    {
                        FnSamFinEncoreLoanAccount objFnSamFinEncoreLoanAccount = new FnSamFinEncoreLoanAccount();

                        MdlProductLoanDetails objMdlProductLoanDetails = new MdlProductLoanDetails();

                        objMdlProductLoanDetails.product = values.producttype_gid;
                        objMdlProductLoanDetails.sub_product = values.productsubtype_gid;
                        objMdlProductLoanDetails.principal_frequency = values.principalfrequency_gid;
                        objMdlProductLoanDetails.interest_frequency = values.interestfrequency_gid;
                        objMdlProductLoanDetails.interestdeduction_upfront = values.interest_status;
                        objMdlProductLoanDetails.moratorium_status = values.moratorium_status;
                        objMdlProductLoanDetails.moratorium_type = values.moratorium_type;

                        objMdlProductLoanDetails.facilityvalidity_days = values.facilityvalidity_days;
                        objMdlProductLoanDetails.facilityvalidity_month = values.facilityvalidity_month;
                        objMdlProductLoanDetails.facilityvalidity_year = values.facilityvalidity_year;

                        string ProductLoanDetails = JsonConvert.SerializeObject(objMdlProductLoanDetails);

                        objFnSamFinEncoreLoanAccount.LogForAuditProductLoanDetails("DaMstApplicationEdit - Function : DaLoanDetailsUpdate - MstApplicationLoanEdit or MstCreditLoanDtlEdit . Log Time - " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + " " + ProductLoanDetails);

                        ProductLoanDetailsWithEncoreMasterValidationResponse objProductLoanDetailsWithEncoreMasterValidationResponse = new ProductLoanDetailsWithEncoreMasterValidationResponse();

                        objProductLoanDetailsWithEncoreMasterValidationResponse = objFnSamFinEncoreLoanAccount.ProductLoanDetailsWithEncoreMasterValidation(objMdlProductLoanDetails);

                        if (objProductLoanDetailsWithEncoreMasterValidationResponse.status == false && objProductLoanDetailsWithEncoreMasterValidationResponse.message == "InValid")
                        {
                            values.status = false;
                            values.message = "Invalid Tenure Units";
                            return;
                        }
                        if (objProductLoanDetailsWithEncoreMasterValidationResponse.status == false)
                        {
                            values.status = false;
                            values.message = "Product Terms doesn't match with those available in Encore Master";
                            return;
                        }
                    }
                    // Product Loan Details With Encore Master Validation - Ended

                    msSQL = " update ocs_mst_tapplication2loan set " +
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
                             " loanfacility_amount='" + values.facilityloan_amount.Replace(",", "") + "'," +
                             " rate_interest='" + values.rate_interest + "'," +
                             " margin='" + values.margin + "'," +
                             " penal_interest='" + values.penal_interest + "'," +
                             " facilityvalidity_year='" + values.facilityvalidity_year + "'," +
                             " facilityvalidity_month='" + values.facilityvalidity_month + "'," +
                             " facilityvalidity_days='" + values.facilityvalidity_days + "'," +
                             " facilityoverall_limit='" + values.facilityoverall_limit + "'," +
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

                    msSQL += " enduse_purpose='" + values.enduse_purpose.Replace("'", "") + "'," +
                           " product_gid= '" + values.product_gid + "'," +
                         " product_name='" + values.product_name + "'," +
                         " variety_gid= '" + values.variety_gid + "'," +
                         " variety_name='" + values.variety_name + "'," +
                         " sector_name= '" + values.sector_name + "'," +
                         " category_name='" + values.category_name + "'," +
                         " botanical_name= '" + values.botanical_name + "'," +
                         " alternative_name='" + values.alternative_name + "'," +
                           " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where application2loan_gid='" + values.application2loan_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult == 1)
                    {

                        msSQL = "update ocs_mst_tapplication2product set application2loan_gid='" + values.application2loan_gid + "' where application2loan_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (values.product_type == "Agri Receivable Finance (ARF)")
                        {
                            msSQL = "update ocs_mst_tapplication2buyer set application2loan_gid='" + values.application2loan_gid + "' where application2loan_gid='" + employee_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        else
                        {
                            msSQL = "delete from  ocs_mst_tapplication2buyer  where application2loan_gid='" + employee_gid + "' or application2loan_gid='" + values.application2loan_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        if (values.loan_type == "Secured")
                        {
                            msSQL = "update ocs_mst_tuploadcollateraldocument set application2loan_gid='" + values.application2loan_gid + "' where application2loan_gid='" + employee_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        else
                        {
                            msSQL = "delete from ocs_mst_tuploadcollateraldocument where application2loan_gid='" + employee_gid + "' or application2loan_gid='" + values.application2loan_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        msGetGid = objcmnfunctions.GetMasterGID("A2LU");
                        msSQL = " insert into ocs_mst_tapplication2loanupdateLOG(" +
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
                               " margin," +
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
                               "'" + lsfacilityloan_amount + "'," +
                               "'" + lsrate_interest + "'," +
                               "'" + lsratemargin + "',";
                             
                        if (lspenal_interest == null || lspenal_interest == "")
                        {
                            msSQL += "'0.00',";
                        }
                        else
                        {
                            msSQL += "'" + lspenal_interest.Replace(",", "") + "',";
                        }
                      msSQL += "'" + lsfacilityvalidity_year + "'," +
                               "'" + lsfacilityvalidity_month + "'," +
                               "'" + lsfacilityvalidity_days + "'," +
                               "'" + lsfacilityoverall_limit + "'," +
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
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        values.status = true;
                        values.message = "Loan Details Updated Successfully";
                    }
                }
                //    }
                //    else
                //    {
                //    objODBCDatareader.Close();
                //    values.status = false;
                //    values.message = "Already this Program Name added.";
                //}
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaDeleteLoanDetail(string application2loan_gid, MdlMstLoanDtl values)
        {
            msSQL = "delete from ocs_mst_tapplication2loan where application2loan_gid='" + application2loan_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from ocs_mst_tapplication2loanupdateLOG where application2loan_gid='" + application2loan_gid + "'";
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
                    " from ocs_mst_tapplication2buyer where application2loan_gid='" + application2loan_gid + "'";
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
                    " from ocs_mst_tapplication2buyer where application2loan_gid='" + application2loan_gid + "' or application2loan_gid='" + employee_gid + "'";
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
              " from ocs_mst_tapplication2buyer where application2buyer_gid='" + application2buyer_gid + "'";
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
             " from ocs_mst_tapplication2buyer where application2buyer_gid='" + values.application2buyer_gid + "'";
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
            }
            try
            {
                msSQL = " update ocs_mst_tapplication2buyer set " +
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
                    msSQL = " insert into ocs_mst_tapplication2buyerUpdateLog(" +
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
            msSQL = "delete from ocs_mst_tapplication2buyer where application2buyer_gid='" + application2buyer_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from ocs_mst_tapplication2buyerUpdateLog where application2buyer_gid='" + application2buyer_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select application2buyer_gid,buyer_name,buyer_gid,buyer_limit,availed_limit,balance_limit,margin,bill_tenure " +
                    " from ocs_mst_tapplication2buyer where application2loan_gid='" + employee_gid + "'";
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
            string lsproducttypecheck_gid = string.Empty;
            if (values.producttypelist != null)
            {
                var productTypeListCounter = values.producttypelist.GroupBy(item => item.producttype_gid);
                for (var i = 0; i < values.producttypelist.Count; i++)
                {
                    foreach (var item in productTypeListCounter)
                    {
                        string producttypelist_gid = item.Key;
                        int count_gid = item.Count();
                        msSQL = "select producttype_gid from ocs_mst_tapplicationservicecharge where application_gid='" + values.application_gid + "'";
                        //lsproducttypecheck_gid = objdbconn.GetExecuteScalar(msSQL);
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        if (dt_datatable.Rows.Count != 0)
                        {
                            int servicecount = 0;

                            foreach (DataRow dr_datarow in dt_datatable.Rows)
                            {
                                lsproducttypecheck_gid = dr_datarow["producttype_gid"].ToString();
                                if (lsproducttypecheck_gid.Contains(",") == false)
                                {
                                    msSQL = "select COUNT(application2servicecharge_gid) from ocs_mst_tapplicationservicecharge where producttype_gid ='" + producttypelist_gid + "' and  application_gid='" + values.application_gid + "'";
                                    string application2servicechargelist_count1 = objdbconn.GetExecuteScalar(msSQL);
                                    int lsapplication2servicechargelist_count1 = Convert.ToInt16(application2servicechargelist_count1);
                                    msSQL = "select COUNT(application2loan_gid) from ocs_mst_tapplication2loan where producttype_gid ='" + producttypelist_gid + "' and application_gid='" + values.application_gid + "'";
                                    application2loan_count = objdbconn.GetExecuteScalar(msSQL);
                                    int total_count1 = count_gid + lsapplication2servicechargelist_count1;
                                    if (total_count1 > int.Parse(application2loan_count))
                                    {
                                        values.status = false;
                                        values.message = "Product Type Already Added in Service Charges";
                                        return;
                                    }
                                }
                                else
                                {
                                    string[] producttypegid_array = lsproducttypecheck_gid.Split(',');

                                    foreach (var item1 in producttypegid_array)
                                    {
                                        if (item1 == values.producttypelist[i].producttype_gid)
                                        {
                                            servicecount += 1;
                                        }
                                    }

                                    msSQL = "select COUNT(application2loan_gid) from ocs_mst_tapplication2loan where producttype_gid ='" + values.producttypelist[i].producttype_gid + "' and application_gid='" + values.application_gid + "'";
                                    application2loan_count = objdbconn.GetExecuteScalar(msSQL);
                                    int total_count1 = count_gid + servicecount;
                                    if (total_count1 > int.Parse(application2loan_count))
                                    {
                                        values.status = false;
                                        values.message = "Product Type " + values.producttypelist[i].product_type + " Already Added in Service Charges";
                                        return;
                                    }


                                }
                            }
                        }
                        
                    }

                    msSQL = "select producttype_gid from ocs_mst_tapplicationservicecharge where application_gid='" + values.application_gid + "'";
                    //lsproducttypecheck_gid = objdbconn.GetExecuteScalar(msSQL);
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        int servicecount = 0;

                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            lsproducttypecheck_gid = dr_datarow["producttype_gid"].ToString();
                            if (lsproducttypecheck_gid.Contains(",") == false)
                            {

                                msSQL = "select COUNT(application2servicecharge_gid) from ocs_mst_tapplicationservicecharge where producttype_gid ='" + values.producttypelist[i].producttype_gid + "' and  application_gid='" + values.application_gid + "'";
                                application2servicecharge_count = objdbconn.GetExecuteScalar(msSQL);
                                msSQL = "select COUNT(application2loan_gid) from ocs_mst_tapplication2loan where producttype_gid ='" + values.producttypelist[i].producttype_gid + "' and application_gid='" + values.application_gid + "'";
                                application2loan_count = objdbconn.GetExecuteScalar(msSQL);

                                if (int.Parse(application2servicecharge_count) >= int.Parse(application2loan_count))
                                {
                                    values.status = false;
                                    values.message = "Product Type " + values.producttypelist[i].product_type + " Already Added in Service Charges";
                                    return;
                                }
                            }
                            else
                            {
                                string[] producttypegid_array = lsproducttypecheck_gid.Split(',');
                                
                                foreach (var item in producttypegid_array)
                                {
                                    if (item == values.producttypelist[i].producttype_gid)
                                    {
                                        servicecount += 1;
                                    }
                                }

                                msSQL = "select COUNT(application2loan_gid) from ocs_mst_tapplication2loan where producttype_gid ='" + values.producttypelist[i].producttype_gid + "' and application_gid='" + values.application_gid + "'";
                                application2loan_count = objdbconn.GetExecuteScalar(msSQL);

                                if (servicecount >= int.Parse(application2loan_count))
                                {
                                    values.status = false;
                                    values.message = "Product Type " + values.producttypelist[i].product_type + " Already Added in Service Charges";
                                    return;
                                }


                            }

                        }
                    }


                    producttypegid += values.producttypelist[i].producttype_gid + ",";
                    producttype += values.producttypelist[i].product_type + ",";

                }
                producttypegid = producttypegid.TrimEnd(',');
                producttype = producttype.TrimEnd(',');
            }

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



            msSQL = " select application_gid,application2servicecharge_gid,format(processing_fee,0,'en_IN') as processing_fee,processing_collectiontype,format(doc_charges,0,'en_IN') as doc_charges," +
                        " doccharge_collectiontype,format(fieldvisit_charges,0,'en_IN') as fieldvisit_charge,fieldvisit_charges_collectiontype,format(adhoc_fee,0,'en_IN') as adhoc_fee,adhoc_collectiontype," +
                        " format(life_insurance,0,'en_IN') as life_insurance,lifeinsurance_collectiontype,format(acct_insurance,0,'en_IN') as acct_insurance, " +
                        " format(total_collect,0,'en_IN') as total_collect,format(total_deduct,0,'en_IN') as total_deduct," +
                        " product_type,acctinsurance_collectiontype, " +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                        " from ocs_mst_tapplicationservicecharge a " +
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
                        fieldvisit_charge = (dr_datarow["fieldvisit_charge"].ToString()),
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


            //msSQL = " select generatelsa_gid from ocs_trn_tgeneratelsa a " +
            //        " left join ocs_trn_tprocesstype_assign b on b.application_gid = a.application_gid " +
            //        " where a.application_gid = '" + values.application_gid + "' and b.menu_gid = '" + getMenuClass.LSA + "' and maker_approvalflag = 'N'";
            //string generatelsa_gid = objdbconn.GetExecuteScalar(msSQL);
            //if (generatelsa_gid != "")
            //{
            //    string msgetfeechargeGid = objcmnfunctions.GetMasterGID("LFCG");

            //    msSQL = " insert into ocs_trn_tlsafeescharge(" +
            //            " lsafeescharge_gid, " +
            //            " application2servicecharge_gid," +
            //            " application_gid," +
            //            " generatelsa_gid, " +
            //            " processing_fee," +
            //            " processing_collectiontype," +
            //            " doc_charges," +
            //            " doccharge_collectiontype," +
            //            " fieldvisit_charges," +
            //            " fieldvisit_charges_collectiontype," +
            //            " adhoc_fee," +
            //            " adhoc_collectiontype," +
            //            " life_insurance," +
            //            " lifeinsurance_collectiontype," +
            //            " acct_insurance," +
            //            " acctinsurance_collectiontype," +
            //            " total_collect," +
            //            " total_deduct," +
            //            " product_type," +
            //            " created_by," +
            //            " created_date) values(" +
            //            "'" + msgetfeechargeGid + "'," +
            //            "'" + msGetGid + "'," +
            //            "'" + values.application_gid + "'," +
            //            "'" + generatelsa_gid + "'," +
            //            "'" + values.processing_fee + "'," +
            //            "'" + values.processing_collectiontype + "'," +
            //            "'" + values.doc_charges + "'," +
            //            "'" + values.doccharge_collectiontype + "'," +
            //            "'" + values.fieldvisit_charge + "'," +
            //            "'" + values.fieldvisit_collectiontype + "'," +
            //            "'" + values.adhoc_fee + "'," +
            //            "'" + values.adhoc_collectiontype + "'," +
            //            "'" + values.life_insurance + "'," +
            //            "'" + values.lifeinsurance_collectiontype + "'," +
            //            "'" + values.acct_insurance + "'," +
            //            "'" + values.acctinsurance_collectiontype + "'," +
            //            "'" + values.total_collect + "'," +
            //            "'" + values.total_deduct + "'," +
            //            "'" + producttype + "'," +
            //            "'" + employee_gid + "'," +
            //            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //}
            if (mnResult != 0)
            {
                msSQL = "update ocs_mst_tapplication set productcharges_status='Completed' where application_gid = '" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.status = true;
                values.message = "Service Details added Successfully";
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
                    " from ocs_mst_tapplication2collateral where application_gid='" + application_gid + "'";
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
                    " from ocs_mst_tuploadcollateraldocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                    " and b.user_gid = c.user_gid and application2collateral_gid='" + employee_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<DocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new DocumentList
                    {
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_gid = (dr_datarow["collateraldocument_gid"].ToString()),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString(),
                        document_title = dr_datarow["document_title"].ToString()
                    });
                }
                values.DocumentList = get_filename;
            }
        }

        public void DaCollateralTempDetailsList(string employee_gid, string application_gid, MdlMstCollatertal values)
        {
            msSQL = "select application2collateral_gid,source_type,guideline_value,market_value,forcedsource_value,collateralSSV_value," +
                    " date_format(guideline_date,'%d-%m-%Y') as guideline_date,date_format(forcedvalueassessed_on,'%d-%m-%Y') as forcedvalueassessed_on," +
                    " date_format(marketvalue_date,'%d-%m-%Y') as marketvalue_date,collateralobservation_summary " +
                    " from ocs_mst_tapplication2collateral where application_gid='" + employee_gid + "' or application_gid='" + application_gid + "'";
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
                    " from ocs_mst_tapplication2collateral where application2collateral_gid='" + application2collateral_gid + "'";
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
                    " from ocs_mst_tapplication2collateral where application2collateral_gid='" + values.application2collateral_gid + "'";
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
                msSQL = " update ocs_mst_tapplication2collateral set " +
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
                    msSQL = "update ocs_mst_tuploadcollateraldocument set application2collateral_gid='" + values.application2collateral_gid + "'" +
                        " where application2collateral_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msGetGid = objcmnfunctions.GetMasterGID("A2CU");
                    msSQL = " insert into ocs_mst_tapplication2collateralUpdate_LOG(" +
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
            msSQL = "delete from ocs_mst_tapplication2collateral where application2collateral_gid='" + application2collateral_gid + "'";
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
                    " from ocs_mst_tapplication2hypothecation where application_gid='" + application_gid + "'";
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
                    " from ocs_mst_tapplication2hypothecation where application_gid='" + employee_gid + "' or application_gid='" + application_gid + "'";
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
                      " from ocs_mst_tuploadhypothecationocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                      " and b.user_gid = c.user_gid and application2hypothecation_gid='" + employee_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<DocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new DocumentList
                    {
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
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
                    " from ocs_mst_tapplication2hypothecation where application_gid='" + application_gid + "'";
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
                msSQL = " select application2hypothecation_gid,hypothecationdocument_gid,document_name,document_path,document_title from ocs_mst_tuploadhypothecationocument " +
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
                            document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
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


            msSQL = " update ocs_mst_tapplication set " +
                  " overalllimit_amount='" + values.overalllimit_amount + "'," +
                  " validityoveralllimit_year='" + values.validityoveralllimit_year + "'," +
                  " validityoveralllimit_month='" + values.validityoveralllimit_month + "'," +
                  " validityoveralllimit_days='" + values.validityoveralllimit_days + "'," +
                  " calculationoveralllimit_validity='" + values.calculationoveralllimit_validity + "'," +
                  " productcharge_flag='Y'," +
                  " productcharges_status='Incomplete'," +
                  " csa_applicability='" + values.csa_applicability + "'," +
                  " csaactivity_gid='" + values.csaactivity_gid + "'," +
                  " csaactivity_name='" + values.csaactivity_name + "'," +
                  " percentageoftotal_limit='" + values.percentageoftotal_limit + "'," +
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


            msSQL = "select application_gid,date_format(facilityrequested_date, '%d-%m-%Y') as facilityrequested_date, product_type,facilityrequested_date,scheme_type, " +
                            " productsub_type, loanfacility_amount, loan_type, rate_interest,margin, penal_interest, facilityoverall_limit, " +
                            " tenureoverall_limit, facility_type, facility_mode, principalfrequency_name, interestfrequency_name,producttype_gid, " +
                            " interest_status, moratorium_type, moratorium_status, date_format(moratorium_startdate, '%d-%m-%Y') as moratorium_startdate,application2loan_gid, " +
                            " date_format(moratorium_enddate, '%d-%m-%Y') as moratorium_enddate,product_gid,variety_gid from ocs_mst_tapplication2loan " +
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
                        roi_margin = (dr_datarow["margin"].ToString()),
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
                        product_gid = (dr_datarow["product_gid"].ToString()),
                        variety_gid = (dr_datarow["variety_gid"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),

                    });
                }
                values.mstloan_list = getmstloan_list;
            }
            dt_datatable.Dispose();
            msSQL = " select application_gid,application2servicecharge_gid,processing_fee,processing_collectiontype,doc_charges," +
                    " doccharge_collectiontype,fieldvisit_charges,fieldvisit_charges_collectiontype,adhoc_fee,adhoc_collectiontype," +
                   "  life_insurance,lifeinsurance_collectiontype, acct_insurance, " +
                    " format(total_collect,0,'en_IN') as total_collect,format(total_deduct,0,'en_IN') as total_deduct," +
                    " product_type,acctinsurance_collectiontype, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                    " from ocs_mst_tapplicationservicecharge a " +
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
            //msSQL = "select application2loan_gid from ocs_mst_tapplication2loan where producttype_gid='" + values.producttype_gid + "' and " +
            //   " productsubtype_gid='" + values.productsubtype_gid + "' and application_gid='" + values.application_gid + "'";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);
            //if (objODBCDatareader.HasRows == false)
            //{
            //    objODBCDatareader.Close();
            if (values.product_type == "Agri Receivable Finance (ARF)")
            {
                msSQL = "select application2buyer_gid from ocs_mst_tapplication2buyer  where application2loan_gid='" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    values.message = "Kindly add atleast one Buyer";
                    values.status = false;
                    return;
                }
                else
                {
                    // Product Loan Details With Encore Master Validation - Started
                    if (values.loandetailsvalidation_flag == "Yes")
                    {
                        FnSamFinEncoreLoanAccount objFnSamFinEncoreLoanAccount = new FnSamFinEncoreLoanAccount();

                        MdlProductLoanDetails objMdlProductLoanDetails = new MdlProductLoanDetails();

                        objMdlProductLoanDetails.product = values.producttype_gid;
                        objMdlProductLoanDetails.sub_product = values.productsubtype_gid;
                        objMdlProductLoanDetails.principal_frequency = values.principalfrequency_gid;
                        objMdlProductLoanDetails.interest_frequency = values.interestfrequency_gid;
                        objMdlProductLoanDetails.interestdeduction_upfront = values.interest_status;
                        objMdlProductLoanDetails.moratorium_status = values.moratorium_status;
                        objMdlProductLoanDetails.moratorium_type = values.moratorium_type;

                        objMdlProductLoanDetails.facilityvalidity_days = values.facilityvalidity_days;
                        objMdlProductLoanDetails.facilityvalidity_month = values.facilityvalidity_month;
                        objMdlProductLoanDetails.facilityvalidity_year = values.facilityvalidity_year;

                        string ProductLoanDetails = JsonConvert.SerializeObject(objMdlProductLoanDetails);

                        objFnSamFinEncoreLoanAccount.LogForAuditProductLoanDetails("DaMstApplicationEdit - Function : DaPostLoanEditDtl - MstCreditProductChargesDtlEdit or MstApplcreationProductchargesEdit . Log Time - " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + " " + ProductLoanDetails);

                        ProductLoanDetailsWithEncoreMasterValidationResponse objProductLoanDetailsWithEncoreMasterValidationResponse = new ProductLoanDetailsWithEncoreMasterValidationResponse();

                        objProductLoanDetailsWithEncoreMasterValidationResponse = objFnSamFinEncoreLoanAccount.ProductLoanDetailsWithEncoreMasterValidation(objMdlProductLoanDetails);

                        if (objProductLoanDetailsWithEncoreMasterValidationResponse.status == false && objProductLoanDetailsWithEncoreMasterValidationResponse.message == "InValid")
                        {
                            values.status = false;
                            values.message = "Invalid Tenure Units";
                            return;
                        }

                        if (objProductLoanDetailsWithEncoreMasterValidationResponse.status == false)
                        {
                            values.status = false;
                            values.message = "Product Terms doesn't match with those available in Encore Master";
                            return;
                        }
                    }
                    // Product Loan Details With Encore Master Validation - Ended


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
                             " margin," +
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
                            "'" + values.facilityloan_amount.Replace(",", "") + "'," +
                            "'" + values.rate_interest + "'," +
                            "'" + values.margin + "'," +
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
                             "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        msSQL = "update ocs_mst_tapplication2buyer set application2loan_gid='" + msGetGid + "' where application2loan_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update ocs_mst_tapplication2product set application2loan_gid='" + msGetGid + "' where application2loan_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update ocs_mst_tapplication set productcharges_status='Completed',productcharge_flag='Y' where application_gid = '" + values.application_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        values.status = true;


                        values.message = "Loan details Added successfully";
                        msSQL = " select date_format(facilityrequested_date, '%d-%m-%Y') as facilityrequested_date, product_type,facilityrequested_date,scheme_type, " +
                                " productsub_type, format(loanfacility_amount,0,'en_IN') as loanfacility_amount, loan_type, rate_interest,margin, penal_interest, facilityoverall_limit, " +
                                " tenureoverall_limit, facility_type, facility_mode, principalfrequency_name, interestfrequency_name,producttype_gid, " +
                                " interest_status, moratorium_type, moratorium_status, date_format(moratorium_startdate, '%d-%m-%Y') as moratorium_startdate,application2loan_gid, " +
                                " date_format(moratorium_enddate, '%d-%m-%Y') as moratorium_enddate,product_gid,product_name,variety_gid,variety_name,  " +
                                " sector_name, category_name, botanical_name, alternative_name from ocs_mst_tapplication2loan " +
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
                                    roi_margin = (dr_datarow["margin"].ToString()),
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

                        msSQL = "update ocs_mst_tuploadcollateraldocument set application2loan_gid='" + msGetGid + "' where application2loan_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " select collateraldocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                                            " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by,a.document_title " +
                                            " from ocs_mst_tuploadcollateraldocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                                            " and b.user_gid = c.user_gid and application2loan_gid='" + msGetGid + "'";

                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        var get_filename = new List<DocumentList>();
                        if (dt_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow dr_datarow in dt_datatable.Rows)
                            {
                                get_filename.Add(new DocumentList
                                {
                                    document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
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
                        msSQL = "select application2loan_gid from ocs_mst_tapplication2loan where product_type='Agri Receivable Finance (ARF)' and " +
                             " (application_gid='" + employee_gid + "' or  application_gid='" + values.application_gid + "' )";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            values.buyer_status = "Y";
                        }
                        objODBCDatareader.Close();

                        msSQL = "select application2loan_gid from ocs_mst_tapplication2loan where loan_type='Secured' and " +
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
                // Product Loan Details With Encore Master Validation - Started
                if (values.loandetailsvalidation_flag == "Yes")
                {
                    FnSamFinEncoreLoanAccount objFnSamFinEncoreLoanAccount = new FnSamFinEncoreLoanAccount();

                    MdlProductLoanDetails objMdlProductLoanDetails = new MdlProductLoanDetails();

                    objMdlProductLoanDetails.product = values.producttype_gid;
                    objMdlProductLoanDetails.sub_product = values.productsubtype_gid;
                    objMdlProductLoanDetails.principal_frequency = values.principalfrequency_gid;
                    objMdlProductLoanDetails.interest_frequency = values.interestfrequency_gid;
                    objMdlProductLoanDetails.interestdeduction_upfront = values.interest_status;
                    objMdlProductLoanDetails.moratorium_status = values.moratorium_status;
                    objMdlProductLoanDetails.moratorium_type = values.moratorium_type;

                    objMdlProductLoanDetails.facilityvalidity_days = values.facilityvalidity_days;
                    objMdlProductLoanDetails.facilityvalidity_month = values.facilityvalidity_month;
                    objMdlProductLoanDetails.facilityvalidity_year = values.facilityvalidity_year;

                    string ProductLoanDetails = JsonConvert.SerializeObject(objMdlProductLoanDetails);

                    objFnSamFinEncoreLoanAccount.LogForAuditProductLoanDetails("DaMstApplicationEdit - Function : DaPostLoanEditDtl - MstCreditProductChargesDtlEdit or MstApplcreationProductchargesEdit . Log Time - " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + " " + ProductLoanDetails);

                    ProductLoanDetailsWithEncoreMasterValidationResponse objProductLoanDetailsWithEncoreMasterValidationResponse = new ProductLoanDetailsWithEncoreMasterValidationResponse();

                    objProductLoanDetailsWithEncoreMasterValidationResponse = objFnSamFinEncoreLoanAccount.ProductLoanDetailsWithEncoreMasterValidation(objMdlProductLoanDetails);

                    if (objProductLoanDetailsWithEncoreMasterValidationResponse.status == false && objProductLoanDetailsWithEncoreMasterValidationResponse.message == "InValid")
                    {
                        values.status = false;
                        values.message = "Invalid Tenure Units";
                        return;
                    }

                    if (objProductLoanDetailsWithEncoreMasterValidationResponse.status == false)
                    {
                        values.status = false;
                        values.message = "Product Terms doesn't match with those available in Encore Master";
                        return;
                    }
                }
                // Product Loan Details With Encore Master Validation - Ended

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
                         " margin," +
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
                        "'" + values.facilityloan_amount.Replace(",", "") + "'," +
                        "'" + values.rate_interest + "'," +
                        "'" + values.margin + "'," +
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
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = "update ocs_mst_tapplication2buyer set application2loan_gid='" + msGetGid + "' where application2loan_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update ocs_mst_tapplication2product set application2loan_gid='" + msGetGid + "' where application2loan_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update ocs_mst_tapplication set productcharges_status='Completed',productcharge_flag='Y' where application_gid = '" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;


                    values.message = "Loan details Added successfully";
                    msSQL = "select date_format(facilityrequested_date, '%d-%m-%Y') as facilityrequested_date, product_type,facilityrequested_date,scheme_type, " +
                              " productsub_type, loanfacility_amount, loan_type, rate_interest,margin, penal_interest, facilityoverall_limit, " +
                              " tenureoverall_limit, facility_type, facility_mode, principalfrequency_name, interestfrequency_name,producttype_gid, " +
                              " interest_status, moratorium_type, moratorium_status, date_format(moratorium_startdate, '%d-%m-%Y') as moratorium_startdate,application2loan_gid, " +
                              " date_format(moratorium_enddate, '%d-%m-%Y') as moratorium_enddate,product_gid,product_name, " +
                              " variety_gid,variety_name,sector_name,category_name,botanical_name,alternative_name from ocs_mst_tapplication2loan " +
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
                                roi_margin = (dr_datarow["margin"].ToString()),
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

                    msSQL = "update ocs_mst_tuploadcollateraldocument set application2loan_gid='" + msGetGid + "' where application2loan_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " select collateraldocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by,a.document_title " +
                                        " from ocs_mst_tuploadcollateraldocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                                        " and b.user_gid = c.user_gid and application2loan_gid='" + msGetGid + "'";

                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var get_filename = new List<DocumentList>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            get_filename.Add(new DocumentList
                            {
                                document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
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
                    msSQL = "select application2loan_gid from ocs_mst_tapplication2loan where product_type='Agri Receivable Finance (ARF)' and " +
                        "(application_gid = '" + employee_gid + "' or  application_gid = '" + values.application_gid + "')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.buyer_status = "Y";
                    }
                    objODBCDatareader.Close();

                    msSQL = "select application2loan_gid from ocs_mst_tapplication2loan where loan_type='Secured' and " +
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
            //}
            //else
            //{
            //    objODBCDatareader.Close();
            //    values.status = false;
            //    values.message = "Already this Program Name added.";
            //}
        }
        public void DaGetEditLimit(string application_gid, MdlMstApplicationAdd values, string employee_gid)
        {

            msSQL = "select  format(overalllimit_amount,2,'en_IN') from ocs_mst_tapplication where application_gid='" + application_gid + "'";
            values.overalllimit_amount = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select sum(loanfacility_amount) from ocs_mst_tapplication2loan where application_gid='" + employee_gid + "'" +
                " or application_gid='" + application_gid + "'";
            values.loanfacility_amount = objdbconn.GetExecuteScalar(msSQL);

        }
        public void GetEditLoanLimit(MdlMstLoanDtl values, string employee_gid)
        {

            msSQL = "select format(overalllimit_amount,2,'en_IN') from ocs_mst_tapplication where application_gid='" + values.application_gid + "'";
            values.overalllimit_amount = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select sum(loanfacility_amount) from ocs_mst_tapplication2loan where ( application_gid='" + employee_gid + "'" +
                " or application_gid='" + values.application_gid + "') and application2loan_gid <>'" + values.application2loan_gid + "'";
            values.loanfacility_amount = objdbconn.GetExecuteScalar(msSQL);
            values.status = true;
        }
        public void DaGetEditproduct(string application_gid, MdlList values, string employee_gid)
        {

            msSQL = " select a.producttype_gid,a.product_type from ocs_mst_tapplication2loan a" +
                    " where a.application_gid='" + application_gid + "'";
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
                    " from ocs_mst_tapplication2hypothecation where application_gid='" + values.application_gid + "'";
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
                msSQL = " update ocs_mst_tapplication2hypothecation set " +
                        " securitytype_gid='" + values.securitytype_gid + "'," +
                         " security_type='" + values.security_type + "',";
                if (values.security_description == null || values.security_description == "")
                {

                }
                else
                { 
                    msSQL += " security_description='" + values.security_description.Replace("'", "") + "',";
                }
                //msSQL += " security_value='" + values.security_value + "',";
                if (values.security_value == null || values.security_value == "")
                {
                    msSQL += " security_value= '0.00',";
                }
                else
                {
                    msSQL += " security_value='" + values.security_value.Replace("'", "") + "',";
                }
                
                if (lssecurityassessed_date == values.securityassessed_date)
                {
                }
                else
                {
                    msSQL += " securityassessed_date='" + Convert.ToDateTime(values.securityassessed_date).ToString("yyyy-MM-dd") + "',";
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
                    msSQL += " hypoobservation_summary='" + values.hypoobservation_summary.Replace("'", "") + "',";
                }
                if (values.primary_security == null || values.primary_security == "")
                {

                }
                else
                {
                    msSQL += " primary_security='" + values.primary_security.Replace("'", "") + "',";
                }
                msSQL += " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where application2hypothecation_gid='" + lsapplication2hypothecation_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msSQL = "update ocs_mst_tuploadhypothecationocument set application2hypothecation_gid='" + lsapplication2hypothecation_gid + "'" +
                       " where application2hypothecation_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msGetGid = objcmnfunctions.GetMasterGID("A2HU");
                    msSQL = " insert into ocs_mst_tapplication2hypothecationUpdateLOG(" +
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
                   "'" + lsapplication2hypothecation_gid + "'," +
                   "'" + values.application_gid + "'," +
                   "'" + lssecuritytype_gid + "'," +
                   "'" + lssecurity_type.Replace("'", "") + "',";
                    if (lssecurity_description == null || lssecurity_description == "")
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + lssecurity_description.Replace("'", "") + "',";
                    }
                    if (lssecurity_value == null || lssecurity_value == "")
                    {
                        msSQL += "'0.00',";
                    }
                    else
                    {
                        msSQL += "'" + lssecurity_value.Replace("'", "") + "',";
                    }                  
                    if (lssecurityassessed_date == values.securityassessed_date)
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += " securityassessed_date='" + Convert.ToDateTime(values.securityassessed_date).ToString("yyyy-MM-dd") + "',";
                    }                                
                    msSQL += "'" + lsasset_id + "'," +
                             "'" + lsroc_fillingid + "'," +
                             "'" + lsCERSAI_fillingid + "'," +
                             "'" + lshypoobservation_summary.Replace("'", "") + "'," +
                             "'" + lsprimary_security.Replace("'", "") + "'," +
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
                    " from ocs_mst_tapplication2hypothecation a " +
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
                      " from ocs_mst_tuploadhypothecationocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
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
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
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
            msSQL = "delete from ocs_mst_tapplication2hypothecation where application2hypothecation_gid='" + application2hypothecation_gid + "'";
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
                   " from ocs_mst_tapplication where application_gid='" + values.application_gid + "'";
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
                msSQL = " update ocs_mst_tapplication set " +
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
                    msSQL = " update ocs_mst_tapplication2loan set application_gid='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    //msSQL = " update ocs_mst_tapplication2buyer set application_gid='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                    //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_mst_tapplication2collateral set application_gid='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_mst_tapplication2hypothecation set application_gid='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msGetGid = objcmnfunctions.GetMasterGID("APUL");
                    msSQL = " insert into ocs_mst_tapplicationUpdateLOG(" +
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
            msSQL = "delete from ocs_mst_tapplication2buyer where application2loan_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_mst_tapplication2loan where application_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_mst_tapplication2collateral where application_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_mst_tapplication2hypothecation where application_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_mst_tuploadcollateraldocument where application2loan_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_mst_tuploadhypothecationocument where application2hypothecation_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_mst_tapplication2product where application2loan_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }

        public void DaCollateralDocumentTempList(string employee_gid, string application2loan_gid, Documentname objfilename)
        {
            msSQL = " select application2loan_gid,collateraldocument_gid,document_name,document_path,document_title,migration_flag from ocs_mst_tuploadcollateraldocument " +
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
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        document_gid = dt["collateraldocument_gid"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        application2loan_gid = dt["application2loan_gid"].ToString(),
                        migration_flag = dt["migration_flag"].ToString(),                 
                    });
                    objfilename.DocumentList = get_filename;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaCollateralDocumentList(string application2loan_gid, Documentname objfilename)
        {
            msSQL = " select application2loan_gid,collateraldocument_gid,document_name,document_path,document_title,migration_flag from ocs_mst_tuploadcollateraldocument " +
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
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        document_gid = dt["collateraldocument_gid"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        application2loan_gid = dt["application2loan_gid"].ToString(),
                        migration_flag = dt["migration_flag"].ToString(),
                    });
                    objfilename.DocumentList = get_filename;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaHypothecationDocumentTempList(string employee_gid, string application2hypothecation_gid, Documentname objfilename)
        {
            msSQL = " select application2hypothecation_gid,hypothecationdocument_gid,document_name,document_path,document_title from ocs_mst_tuploadhypothecationocument " +
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
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
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
            // MemoryStream ms = new MemoryStream();
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
            path = HttpContext.Current.Server.MapPath("../../erpdocument" + "/" + lscompany_code + "/" + "Master/CollateralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
            //  string lsfirstdocument_filepath = string.Empty;
            string document_title = httpRequest.Form["document_title"].ToString();
            string lsapplication2collateral_gid = httpRequest.Form["application2collateral_gid"].ToString();
            httpFileCollection = httpRequest.Files;


            if (httpRequest.Files.Count > 0)
            {
                string lsfirstdocument_filepath = string.Empty;
                httpFileCollection = httpRequest.Files;
                for (int i = 0; i < httpFileCollection.Count; i++)
                {
                    MemoryStream ms = new MemoryStream();
                    httpPostedFile = httpFileCollection[0];
                    string FileExtension = httpPostedFile.FileName;
                    //string lsfile_gid = msdocument_gid + FileExtension;
                    string lsfile_gid = msdocument_gid;
                    FileExtension = Path.GetExtension(FileExtension).ToLower();
                    lsfile_gid = lsfile_gid + FileExtension;
                    //if ((FileExtension == ".xls") || (FileExtension == ".xlsx") || (FileExtension == ".doc") || (FileExtension == ".docx") || (FileExtension == ".pdf") || (FileExtension == ".zip") || (FileExtension == ".msg") || (FileExtension == ".oft") || (FileExtension == ".txt") || (FileExtension == ".txtx"))
                    //{

                        
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);

                    // Check Document validation;

                    byte[] bytes = ms.ToArray();
                    if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                    {
                        objfilename.message = "File format is not supported";
                        return false;
                    }
                    bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/CollateralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/CollateralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        msGetGid = objcmnfunctions.GetMasterGID("CAMD");
                        msSQL = " insert into ocs_mst_tuploadcollateraldocument( " +
                                     " collateraldocument_gid," +
                                     " document_name, " +
                                     " document_title," +
                                     " document_path, " +
                                     " application2collateral_gid," +
                                     " created_by ," +
                                     " created_date " +
                                     " )values(" +
                                     "'" + msGetGid + "'," +
                                     "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                     "'" + document_title.Replace("'", "") + "'," +
                                     "'" + lspath + msdocument_gid + FileExtension.Replace("'", "") + "'," +
                                     "'" + employee_gid + "'," +
                                     "'" + employee_gid + "'," +
                                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult != 0)
                        {
                            msSQL = " select collateraldocument_gid,document_name,document_path,document_title from ocs_mst_tuploadcollateraldocument " +
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
                                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                                        document_gid = dt["collateraldocument_gid"].ToString(),
                                        document_title = dt["document_title"].ToString(),
                                    });
                                    objfilename.DocumentList = get_filename;
                                }
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
                    //}
                    //else
                    //{
                    //    objfilename.status = false;
                    //    objfilename.message = "File format is not supported";
                      
                    //}

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
            //MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;
            String path = lspath;


            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = HttpContext.Current.Server.MapPath("../../erp_documents" + "/" + lscompany_code + "/" + "Master/HypothecationDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
            string lsfirstdocument_filepath = string.Empty;
            string document_title = httpRequest.Form["document_title"].ToString();
            string lsapplication2hypothecation_gid = httpRequest.Form["application2hypothecation_gid"].ToString();
            httpFileCollection = httpRequest.Files;
            MemoryStream ms = new MemoryStream();
            httpPostedFile = httpFileCollection[0];
            string FileExtension = httpPostedFile.FileName;
            //string lsfile_gid = msdocument_gid + FileExtension;
            string lsfile_gid = msdocument_gid;
            FileExtension = Path.GetExtension(FileExtension).ToLower();
            lsfile_gid = lsfile_gid + FileExtension;
            //if ((FileExtension == ".xls") || (FileExtension == ".xlsx") || (FileExtension == ".doc") || (FileExtension == ".docx") || (FileExtension == ".pdf") || (FileExtension == ".zip") || (FileExtension == ".msg") || (FileExtension == ".oft") || (FileExtension == ".txt") || (FileExtension == ".txtx"))
            //{

                ls_readStream = httpPostedFile.InputStream;
               
                ls_readStream.CopyTo(ms);
                lspath = HttpContext.Current.Server.MapPath("../../erp_documents" + "/" + lscompany_code + "/" + "Master/HypothecationDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                objcmnfunctions.uploadFile(lspath, lsfile_gid);
                lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "Master/HypothecationDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                msGetGid = objcmnfunctions.GetMasterGID("HYPD");
                msSQL = " insert into ocs_mst_tuploadhypothecationocument( " +
                             " hypothecationdocument_gid," +
                             " document_name, " +
                             " document_title," +
                             " document_path, " +
                             " application2hypothecation_gid," +
                             " created_by ," +
                             " created_date " +
                             " )values(" +
                             "'" + msGetGid + "'," +
                             "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                             "'" + document_title.Replace("'", "") + "'," +
                             "'" + lspath + msdocument_gid + FileExtension.Replace("'", "") + "'," +
                             "'" + employee_gid + "'," +
                             "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = " select hypothecationdocument_gid,document_name,document_path,document_title from ocs_mst_tuploadhypothecationocument " +
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
                                document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
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
            //}
            //else
            //{
            //    objfilename.status = false;
            //    objfilename.message = "File format is not supported";
            //    return false;
            //}
        }

        public bool DaPostIndividualMobileNumber(string employee_gid, MdlContactMobileNo values)
        {
            msSQL = "select primary_status from ocs_mst_tcontact2mobileno where primary_status='Yes' and (contact_gid='" + employee_gid + "' or contact_gid='" + values.contact_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Mobile Number Added";
                objdbconn.CloseConn();
                return false;
            }

            msSQL = "select mobile_no from ocs_mst_tcontact2mobileno where mobile_no='" + values.mobile_no + "' and (contact_gid='" + employee_gid + "' or contact_gid='" + values.contact_gid + "') ";
            string lsmobile_no = objdbconn.GetExecuteScalar(msSQL);
            if (lsmobile_no == (values.mobile_no))
            {

                values.status = false;
                values.message = "Already This Mobile Number Added";
                objdbconn.CloseConn();
                return false;
            }

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
                values.status = false;
                values.message = "Error Occured";
                return false;
            }
        }

        public void DaGetIndividualMobileNoTempList(string contact_gid, string employee_gid, MdlContactMobileNo values)
        {
            msSQL = "select mobile_no,contact2mobileno_gid,primary_status,whatsapp_no from ocs_mst_tcontact2mobileno where " +
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
            msSQL = "select mobile_no,contact2mobileno_gid,primary_status,whatsapp_no from ocs_mst_tcontact2mobileno where " +
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
                msSQL = " select mobile_no,contact2mobileno_gid,primary_status,whatsapp_no from ocs_mst_tcontact2mobileno where " +
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
            msSQL = " select mobile_no,contact2mobileno_gid,primary_status,whatsapp_no from ocs_mst_tcontact2mobileno where " +
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
                msSQL = " update ocs_mst_tcontact2mobileno set " +
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

                    msSQL = "Insert into ocs_mst_tcontact2mobilenoupdatelog(" +
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
            msSQL = "delete from ocs_mst_tcontact2mobileno where contact2mobileno_gid='" + contact2mobileno_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from ocs_mst_tcontact2mobilenoupdatelog where contact2mobileno_gid='" + contact2mobileno_gid + "'";
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
            msSQL = "select primary_status from ocs_mst_tcontact2email where primary_status='Yes' and (contact_gid='" + employee_gid + "' or contact_gid='" + values.contact_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Email Address Added";
                objdbconn.CloseConn();
                return false;
            }

            msSQL = "select email_address from ocs_mst_tcontact2email where email_address='" + values.email_address + "' and (contact_gid='" + employee_gid + "' or contact_gid='" + values.contact_gid + "')";
            string lsemail_address = objdbconn.GetExecuteScalar(msSQL);
            if (lsemail_address == (values.email_address))
            {
                values.status = false;
                values.message = "Already This Email Address Added";
                objdbconn.CloseConn();
                return false;
            }

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
                values.status = false;
                values.message = "Error Occured";
                return false;
            }
        }

        public void DaGetIndividualEmailAddressTempList(string contact_gid, string employee_gid, MdlContactEmail values)
        {
            msSQL = "select email_address,contact2email_gid,primary_status from ocs_mst_tcontact2email where " +
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
            msSQL = "select email_address,contact2email_gid,primary_status from ocs_mst_tcontact2email where " +
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
                msSQL = " select email_address,contact2email_gid,primary_status from ocs_mst_tcontact2email where " +
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
            msSQL = " select email_address,contact2email_gid,primary_status from ocs_mst_tcontact2email where " +
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
                msSQL = " update ocs_mst_tcontact2email set " +
                         " email_address='" + values.email_address + "'," +
                         " primary_status='" + values.primary_status + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where contact2email_gid='" + values.contact2email_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("IEUL");

                    msSQL = "Insert into ocs_mst_tcontact2emailupdatelog(" +
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
            msSQL = "delete from ocs_mst_tcontact2email where contact2email_gid='" + contact2email_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from ocs_mst_tcontact2emailupdatelog where contact2email_gid='" + contact2email_gid + "'";
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
            msSQL = "select primary_status from ocs_mst_tcontact2address where primary_status='Yes' and (contact_gid='" + employee_gid + "' or contact_gid='" + values.contact_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Address Added";
                return false;
            }

            msSQL = "select contact2address_gid from ocs_mst_tcontact2address where addresstype_name='" + values.addresstype_name + "' and " +
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
            msSQL = " select contact2address_gid,addresstype_name,primary_status, addressline1, addressline2,landmark, taluka, district, state, country, latitude, longitude," +
                    " postal_code from ocs_mst_tcontact2address where contact_gid='" + employee_gid + "' or contact_gid = '" + contact_gid + "'";
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
                        landmark = (dr_datarow["landmark"].ToString()),
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
            msSQL = " select contact2address_gid,addresstype_name,primary_status, addressline1, addressline2,landmark, taluka, district, state, country, latitude, longitude," +
                    " postal_code from ocs_mst_tcontact2address where contact_gid='" + contact_gid + "'";
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
                        landmark = (dr_datarow["landmark"].ToString()),
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
                    " from ocs_mst_tcontact2address where contact2address_gid='" + contact2address_gid + "'";
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
                    " from ocs_mst_tcontact2address where contact2address_gid='" + values.contact2address_gid + "'";
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
                msSQL = " update ocs_mst_tcontact2address set " +
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

                    msSQL = " insert into ocs_mst_tcontact2addressupdatelog(" +
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
            msSQL = "delete from ocs_mst_tcontact2address where contact2address_gid='" + contact2address_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from ocs_mst_tcontact2addressupdatelog where contact2address_gid='" + contact2address_gid + "'";
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
            msSQL = "select contact2idproof_gid,idproof_name,idproof_no,idproof_dob,file_no,document_name, document_path from ocs_mst_tcontact2idproof where " +
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
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                    });

                    values.contactidproof_list = getcontactidproof_list;
                }
                dt_datatable.Dispose();
            }

        }

        public void DaGetIndividualProofList(string contact_gid, string employee_gid, MdlContactIdProof values)
        {
            msSQL = "select contact2idproof_gid,idproof_name,idproof_no,idproof_dob,file_no,document_name, document_path from ocs_mst_tcontact2idproof where " +
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
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
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
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/IndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/IndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/IndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msSQL = "select covenant_type from ocs_mst_tindividualdocument where individualdocument_gid='" + lsindividualdocument_gid + "'";
                        string lscovenant_type = objdbconn.GetExecuteScalar(msSQL);

                        msGetGid = objcmnfunctions.GetMasterGID("C2DO");
                        msGetDocumentGid = objcmnfunctions.GetMasterGID("BSDA");

                        msSQL = " insert into ocs_mst_tcontact2document( " +
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
                                    "'" + lsdocument_title.Replace("'", "") + "'," +
                                    "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension.Replace("'", "") + "'," +
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
            msSQL = " select contact2document_gid,document_name,document_title,document_path,migration_flag," +
                    " documenttype_name from ocs_mst_tcontact2document " +
                    " where contact_gid='" + employee_gid + "' or contact_gid = '" + contact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<uploadindividualdoc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new uploadindividualdoc_list
                    {
                        migration_flag = dt["migration_flag"].ToString(),
                        document_name = dt["document_name"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        contact2document_gid = dt["contact2document_gid"].ToString(),
                        documenttype_name = dt["documenttype_name"].ToString()
                    });
                    values.uploadindividualdoc_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetIndividualDocList(string contact_gid, MdlContactDocument values)
        {
            msSQL = " select contact2document_gid,document_name,document_title,document_path,migration_flag, " +
                    " documenttype_name from ocs_mst_tcontact2document " +
                    " where contact_gid='" + contact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<uploadindividualdoc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new uploadindividualdoc_list
                    {
                        migration_flag = dt["migration_flag"].ToString(),
                        document_name = dt["document_name"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
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
            msSQL = "delete from ocs_mst_tcontact2document where contact2document_gid='" + contact2document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                msSQL = " select groupdocumentchecklist_gid from ocs_trn_tdocumentchecktls where documentuploaded_gid='" + contact2document_gid + "'";
                string lsgroupdocumentchecklist_gid = objdbconn.GetExecuteScalar(msSQL);

                if (lsgroupdocumentchecklist_gid != "")
                {
                    msSQL = " select count(documentcheckdtl_gid) as documentcount from ocs_trn_tdocumentchecktls " +
                            " where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                    string lsdocumentcount = objdbconn.GetExecuteScalar(msSQL);
                    if (lsdocumentcount == "1")
                    {
                        msSQL = "delete from ocs_trn_tgroupdocumentchecklist where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                msSQL = " select groupcovdocumentchecklist_gid from ocs_trn_tcovanantdocumentcheckdtls where documentuploaded_gid='" + contact2document_gid + "'";
                string lschecklist_gid = objdbconn.GetExecuteScalar(msSQL);
                if (lschecklist_gid != "")
                {
                    msSQL = " select count(covenantdocumentcheckdtl_gid) as documentcount from ocs_trn_tcovanantdocumentcheckdtls " +
                      " where groupcovdocumentchecklist_gid='" + lschecklist_gid + "'";
                    string lsdocumentcount = objdbconn.GetExecuteScalar(msSQL);
                    if (lsdocumentcount == "1")
                    {
                        msSQL = "delete from ocs_trn_tgroupcovenantdocumentchecklist where groupcovdocumentchecklist_gid='" + lschecklist_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                msSQL = "delete from ocs_trn_tcovanantdocumentcheckdtls where documentuploaded_gid='" + contact2document_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "delete from ocs_trn_tdocumentchecktls where documentuploaded_gid='" + contact2document_gid + "'";
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
                        " relationshiptype, nomineefirst_name, nominee_middlename, nominee_lastname, nominee_dob, nominee_age, totallandinacres, cultivatedland," +
                        " nearsamunnatiabranch_gid,nearsamunnatiabranch_name,physicalstatus_gid,physicalstatus_name,internalrating_gid,internalrating_name" +
                        " from ocs_mst_tcontact where contact_gid='" + contact_gid + "'";


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
                    values.nearsamunnatiabranch_gid = objODBCDatareader["nearsamunnatiabranch_gid"].ToString();
                    values.nearsamunnatiabranch_name = objODBCDatareader["nearsamunnatiabranch_name"].ToString();
                    values.physicalstatus_gid = objODBCDatareader["physicalstatus_gid"].ToString();
                    values.physicalstatus_name = objODBCDatareader["physicalstatus_name"].ToString();
                    values.internalrating_gid = objODBCDatareader["internalrating_gid"].ToString();
                    values.internalrating_name = objODBCDatareader["internalrating_name"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select contact2equipment_gid,contact_gid,equipment_gid,equipment_name,availablerenthire, " +
                        " quantity,description,insurance_status,insurance_details from ocs_mst_tcontact2equipment where " +
                        " contact_gid='" + contact_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstequipmentholding_list = new List<mstequipmentholding_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstequipmentholding_list.Add(new mstequipmentholding_list
                        {
                            contact2equipment_gid = (dr_datarow["contact2equipment_gid"].ToString()),
                            contact_gid = (dr_datarow["contact_gid"].ToString()),
                            equipment_gid = (dr_datarow["equipment_gid"].ToString()),
                            equipment_name = (dr_datarow["equipment_name"].ToString()),
                            availablerenthire = (dr_datarow["availablerenthire"].ToString()),
                            quantity = (dr_datarow["quantity"].ToString()),
                            description = (dr_datarow["description"].ToString()),
                            insurance_status = (dr_datarow["insurance_status"].ToString()),
                            insurance_details = (dr_datarow["insurance_details"].ToString()),
                        });
                    }
                    values.mstequipmentholding_list = getmstequipmentholding_list;
                }
                dt_datatable.Dispose();

                msSQL = " select contact2livestock_gid,contact_gid,livestock_gid,livestock_name,count,Breed, " +
                   " insurance_status,insurance_details from ocs_mst_tcontact2livestock where " +
                   " contact_gid='" + contact_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstlivestockholding_list = new List<mstlivestockholding_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstlivestockholding_list.Add(new mstlivestockholding_list
                        {
                            contact2livestock_gid = (dr_datarow["contact2livestock_gid"].ToString()),
                            contact_gid = (dr_datarow["contact_gid"].ToString()),
                            livestock_gid = (dr_datarow["livestock_gid"].ToString()),
                            livestock_name = (dr_datarow["livestock_name"].ToString()),
                            count = (dr_datarow["count"].ToString()),
                            Breed = (dr_datarow["Breed"].ToString()),
                            insurance_status = (dr_datarow["insurance_status"].ToString()),
                            insurance_details = (dr_datarow["insurance_details"].ToString()),
                        });
                    }
                    values.mstlivestockholding_list = getmstlivestockholding_list;
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
                    " from ocs_mst_tcontact2document a where a.documenttype_gid = 'DOCT2022010611' and " +
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

            msSQL = "select application_gid from ocs_mst_tcontact where contact_gid='" + values.contact_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            if (values.stakeholder_type == "Borrower" || values.stakeholder_type == "Applicant")
            {
                msSQL = "select stakeholder_type from ocs_mst_tcontact where application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Borrower','Applicant')" +
                    " and contact_gid<>'" + values.contact_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Already Added Applicant/Borrower Information";
                    return;
                }
                msSQL = "select stakeholder_type from ocs_mst_tinstitution where application_gid='" + lsapplication_gid + "' and " +
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

            msSQL = "select pan_status from ocs_mst_tcontact where contact_gid='" + values.contact_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (values.pan_status == "Customer Submitting PAN")
            {
                msSQL = "delete from ocs_mst_tcontact2panform60 where contact_gid='" + values.contact_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "delete from ocs_mst_tcontact2panabsencereason where contact_gid='" + values.contact_gid + "'";
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
                        " relationshiptype, nomineefirst_name, nominee_middlename, nominee_lastname, nominee_dob, nominee_age, totallandinacres, cultivatedland," +
                        " nearsamunnatiabranch_gid,nearsamunnatiabranch_name,physicalstatus_gid,physicalstatus_name,internalrating_gid,internalrating_name " +
                        " from ocs_mst_tcontact where contact_gid='" + values.contact_gid + "'";
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
                lsnearsamunnatiabranch_gid = objODBCDatareader["nearsamunnatiabranch_gid"].ToString();
                lsnearsamunnatiabranch_name = objODBCDatareader["nearsamunnatiabranch_name"].ToString();
                lsphysicalstatus_gid = objODBCDatareader["physicalstatus_gid"].ToString();
                lsphysicalstatus_name = objODBCDatareader["physicalstatus_name"].ToString();
                lsinternalrating_gid = objODBCDatareader["internalrating_gid"].ToString();
                lsinternalrating_name = objODBCDatareader["institution_name"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " update ocs_mst_tcontact set " +
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
                   " institution_name='" + values.institution_name.Replace("'", "\\'") + "'," +
                   " nearsamunnatiabranch_gid ='" + values.nearsamunnatiabranch_gid + "'," +
                   " nearsamunnatiabranch_name ='" + values.nearsamunnatiabranch_name + "'," +
                   " physicalstatus_gid ='" + values.physicalstatus_gid + "'," +
                   " physicalstatus_name ='" + values.physicalstatus_name + "'," +
                   " internalrating_gid ='" + values.internalrating_gid + "'," +
                   " internalrating_name ='" + values.internalrating_name + "'," +
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

                    msSQL = " select panabsencereason from ocs_mst_tcontact2panabsencereason" +
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
                            msSQL = " INSERT INTO ocs_mst_tcontact2panabsencereason(" +
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
                                msSQL = " INSERT INTO ocs_mst_tcontact2panabsencereason(" +
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
                                msSQL = "delete from ocs_mst_tcontact2panabsencereason where panabsencereason='" + values.contactpanabsencereason_list[i].panabsencereason + "' and contact_gid = '" + values.contact_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            matchCount2 = 0;
                        }
                    }

                }
                msGetGid = objcmnfunctions.GetMasterGID("CTUL");

                msSQL = " insert into ocs_mst_tcontactupdatelog(" +
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
                   " nearsamunnatiabranch_gid," +
                   " nearsamunnatiabranch_name," +
                   " physicalstatus_gid," +
                   " physicalstatus_name," +
                   " internalrating_gid," +
                   " internalrating_name," +
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
                     "'" + lsinstitution_gid + "',";                   
                 if (lsinstitution_name == null || lsinstitution_name == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + lsinstitution_name.Replace("'", "\\'") + "',";
                }
                msSQL += "'" + lsnearsamunnatiabranch_gid + "'," +
                      "'" + lsnearsamunnatiabranch_name + "'," +
                      "'" + lsphysicalstatus_gid + "'," +
                      "'" + lsphysicalstatus_name + "'," +
                      "'" + lsinternalrating_gid + "'," +
                      "'" + lsinternalrating_name + "'," +
                     "'" + employee_gid + "'," +
                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //Updates
                msSQL = "update ocs_mst_tcontact2mobileno set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2email set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2address set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2idproof set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2panform60 set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2panabsencereason set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2equipment set contact_gid='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2livestock set contact_gid='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select individualdocument_gid,contact2document_gid from ocs_mst_tcontact2document where contact_gid='" + employee_gid + "'";
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
                    msSQL = " insert into ocs_trn_tdocumentchecktls(" +
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
                        msSQL = " insert into ocs_trn_tcovanantdocumentcheckdtls(" +
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

                DaMstScannedDocument objvalues = new DaMstScannedDocument();
                objvalues.DaGroupDocChecklistinfoCredit(lsapplication_gid, values.contact_gid, employee_gid);

                msSQL = "update ocs_mst_tcontact2document set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = "select stakeholder_type from ocs_mst_tcontact where contact_gid='" + values.contact_gid + "'";
                    string lsstakeholders_type = objdbconn.GetExecuteScalar(msSQL);

                    if (lsstakeholders_type == "Applicant" || lsstakeholders_type == "Borrower")
                    {
                        msSQL = "select mobile_no from ocs_mst_tcontact2mobileno where contact_gid='" + values.contact_gid + "' and primary_status='yes'";
                        string lsmobileno = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select email_address from ocs_mst_tcontact2email where contact_gid='" + values.contact_gid + "' and primary_status='yes'";
                        lsemail_address = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select concat(first_name,middle_name,last_name) as customer_name,contact_gid,urn,stakeholder_type from ocs_mst_tcontact where" +
                                " application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Applicant','Borrower')";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lscustomer_name = objODBCDatareader["customer_name"].ToString();                            
                            lsurn = objODBCDatareader["urn"].ToString();
                            lsstakeholder_type = objODBCDatareader["stakeholder_type"].ToString();

                            //Region
                            msSQL = "select state from ocs_mst_tcontact2address where primary_status='Yes' and contact_gid='" + objODBCDatareader["contact_gid"].ToString() + "'";
                            lsregion = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = " update ocs_mst_tapplication set customer_name='" + lscustomer_name.Replace("'", "\\'") + "'," +
                                    " mobile_no='" + lsmobile_no + "'," +
                                    " email_address='" + lsemail_address + "'," +
                                    " region='" + lsregion + "'," +
                                    " customer_urn='" + lsurn + "'," +
                                    " applicant_type='Individual'," +
                                    " updated_by='" + employee_gid + "'," +
                                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                    " where application_gid='" + lsapplication_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " update ocs_mst_tcontact set mobile_no='" + lsmobileno + "'," +
                                    " email_address='" + lsemail_address + "' where contact_gid='" + values.contact_gid + "' ";
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

            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        public void DaGetIndividualSummary(string application_gid, MdlMstContact values)
        {
            msSQL = "select a.contact_gid,a.application_gid,a.pan_no,a.aadhar_no,concat(first_name, ' ',middle_name,' ',last_name) as individual_name,stakeholder_type,contact_status," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " institution_name,group_name," +
                    "  case when (d.renewal_flag ='Y' OR d.enhancement_flag ='Y') AND d.approval_status ='Incomplete' and stakeholder_type !='Applicant'   then 'Y' else 'N' end as guarantordelete_flag " +
                    " from ocs_mst_tcontact a " +
                    " left join ocs_mst_tapplication d on a.application_gid=d.application_gid " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " where a.application_gid='" + application_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcontact_list = new List<contact_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcontact_list.Add(new contact_list
                    {
                        contact_gid = (dr_datarow["contact_gid"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        individual_name = (dr_datarow["individual_name"].ToString()),
                        pan_no = (dr_datarow["pan_no"].ToString()),
                        aadhar_no = (dr_datarow["aadhar_no"].ToString()),
                        stakeholder_type = (dr_datarow["stakeholder_type"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        contact_status = (dr_datarow["contact_status"].ToString()),
                        institution_name = dr_datarow["institution_name"].ToString(),
                        group_name = dr_datarow["group_name"].ToString(),
                        guarantordelete_flag = (dr_datarow["guarantordelete_flag"].ToString()),
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
                    " product_gid, variety_gid from ocs_mst_tapplication a " +
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
                        variety_gid = (dr_datarow["variety_gid"].ToString())
                    });
                }
            }
            values.basicdetails_list = getbasicdetails_list;
            dt_datatable.Dispose();
        }

        public bool DaPostAppMobileNo(string employee_gid, MdlMstMobileNo values)
        {
            msSQL = "select primary_mobileno from ocs_mst_tapplication2contactno where primary_mobileno='Yes' and (application_gid='" + employee_gid + "' or" +
               " application_gid='" + values.application_gid + "') ";
            string lsprimary_mobileno = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_mobileno == (values.primary_mobileno))
            {

                values.status = false;
                values.message = "Already Primary Mobile Number Added";
                return false;
            }
            msSQL = "select application2contact_gid from ocs_mst_tapplication2contactno where mobile_no='" + values.mobile_no + "' " +
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
                values.status = false;
                values.message = "Error Occured";
                return false;
            }
        }

        public void DaGetAppMobileNoTempList(string application_gid, string employee_gid, MdlMstMobileNo values)
        {
            msSQL = "select mobile_no,application2contact_gid,primary_mobileno,whatsapp_mobileno from ocs_mst_tapplication2contactno where " +
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
            msSQL = "select mobile_no,application2contact_gid,primary_mobileno,whatsapp_mobileno from ocs_mst_tapplication2contactno where " +
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
                msSQL = " select mobile_no,application2contact_gid,primary_mobileno,whatsapp_mobileno from ocs_mst_tapplication2contactno where " +
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
            msSQL = " select mobile_no,application2contact_gid,primary_mobileno,whatsapp_mobileno from ocs_mst_tapplication2contactno where " +
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
                msSQL = " update ocs_mst_tapplication2contactno set " +
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

                    msSQL = "Insert into ocs_mst_tapplication2contactnoupdatelog(" +
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
            msSQL = "delete from ocs_mst_tapplication2contactno where application2contact_gid='" + application2contact_gid + "'";
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
            msSQL = "select primary_emailaddress from ocs_mst_tapplication2email where primary_emailaddress='Yes' and (application_gid='" + employee_gid + "' or application_gid='" + values.application_gid + "')";
            string lsprimary_emailaddress = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_emailaddress == (values.primary_emailaddress))
            {

                values.status = false;
                values.message = "Already Primary Email Address Added";
                return false;
            }
            msSQL = "select application2email_gid from ocs_mst_tapplication2email where email_address='" + values.email_address + "' and (application_gid='" + employee_gid + "' or application_gid='" + values.application_gid + "')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already This Email Address Added";
                return false;
            }
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
                values.status = false;
                values.message = "Error Occured";
                return false;
            }
        }

        public void DaGetAppEmailAddressTempList(string application_gid, string employee_gid, MdlMstEmailAddress values)
        {
            msSQL = "select email_address,application2email_gid,primary_emailaddress from ocs_mst_tapplication2email where " +
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
            msSQL = "select email_address,application2email_gid,primary_emailaddress from ocs_mst_tapplication2email where " +
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
                msSQL = " select email_address,application2email_gid,primary_emailaddress from ocs_mst_tapplication2email where " +
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
            msSQL = " select email_address,application2email_gid,primary_emailaddress from ocs_mst_tapplication2email where " +
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
                msSQL = " update ocs_mst_tapplication2email set " +
                         " email_address='" + values.email_address + "'," +
                         " primary_emailaddress='" + values.primary_emailaddress + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where application2email_gid='" + values.application2email_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("AEUL");

                    msSQL = "Insert into ocs_mst_tapplication2emailupdatelog(" +
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
            msSQL = "delete from ocs_mst_tapplication2email where application2email_gid='" + application2email_gid + "'";
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
            msSQL = "select geneticcode_gid from ocs_mst_tapplication2geneticcode where (application_gid='" + employee_gid + "' or " +
                " application_gid='" + lsapplication_gid + "') and geneticcode_gid='" + values.geneticcode_gid + "'";
            string lsgenetic_code = objdbconn.GetExecuteScalar(msSQL);
            if (lsgenetic_code == (values.geneticcode_gid))
            {

                values.status = false;
                values.message = "Already Genetic Code Added";
                return;

            }
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
                      " from ocs_mst_tapplication2geneticcode where " +
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
                      " from ocs_mst_tapplication2geneticcode where " +
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
                msSQL = " select application2geneticcode_gid,geneticcode_gid,geneticcode_name,genetic_status,genetic_remarks from ocs_mst_tapplication2geneticcode where " +
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
            msSQL = " select geneticcode_gid,geneticcode_name,genetic_status,genetic_remarks from ocs_mst_tapplication2geneticcode where application2geneticcode_gid='" + values.application2geneticcode_gid + "'";         
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {    
                lsgeneticcode_gid = objODBCDatareader["geneticcode_gid"].ToString();
                lsgeneticcode_name = objODBCDatareader["geneticcode_name"].ToString();
                lsgenetic_status = objODBCDatareader["genetic_status"].ToString();
                lsgenetic_remarks = objODBCDatareader["genetic_remarks"].ToString();
            }

            msSQL = " update ocs_mst_tapplication2geneticcode set " +
                     " genetic_status='" + values.genetic_status + "'," +
                     " genetic_remarks='" + values.genetic_remarks.Replace("'", " ") + "'," +
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where application2geneticcode_gid='" + values.application2geneticcode_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                msGetGid = objcmnfunctions.GetMasterGID("AGUL");

                msSQL = "Insert into ocs_mst_tapplication2geneticcodeupdatelog(" +
               " application2geneticcodeupdatelog_gid, " +
               " application2geneticcode_gid, " +
               " application_gid, " +
               " geneticcode_gid, " +
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
               "'" + lsgenetic_remarks.Replace("'", "") + "'," +
               "'" + employee_gid + "'," +
               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Genetic Code Details Updated Successfully";
            }

        }


        public void DaDeleteAppGeneticCode(string application2geneticcode_gid, MdlMstGeneticCode values, string employee_gid)
        {
            msSQL = "delete from ocs_mst_tapplication2geneticcode where application2geneticcode_gid='" + application2geneticcode_gid + "'";
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
        public void DaEditAppBasic(string application_gid, MdlMstApplicationAdd values)
        {
            try
            {
                msSQL = " select a.approval_status,a.creditgroup_status from ocs_mst_tapplication a" +
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
        public void DaEditAppBasicDetail(string application_gid, MdlMstApplicationAdd values)
        {
            try
            {
                msSQL = " select application_gid,customer_urn,customerref_name as customer_name,vertical_gid,vertical_name,verticaltaggs_gid,verticaltaggs_name," +
                        " constitution_gid,constitution_name,businessunit_gid,businessunit_name,sa_status,sa_id,sa_name,saname_gid,vernacularlanguage_gid," +
                        " vernacular_language,contactpersonfirst_name,contactpersonmiddle_name,contactpersonlast_name,designation_gid,designation_type,landline_no," +
                        " creditgroup_gid,creditgroup_name,program_gid,program_name, product_gid,product_name,variety_gid,variety_name,sector_name,category_name, " +
                        " botanical_name,alternative_name from ocs_mst_tapplication where application_gid='" + application_gid + "'";
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

                msSQL = "select primaryvaluechain_gid,primaryvaluechain_name from ocs_mst_tapplication2primaryvaluechain where application_gid='" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                values.primaryvaluechain_list = dt_datatable.AsEnumerable().Select(row =>
           new primaryvaluechain_list
           {
               valuechain_gid = row["primaryvaluechain_gid"].ToString(),
               valuechain_name = row["primaryvaluechain_name"].ToString()
           }).ToList();
                dt_datatable.Dispose();

                msSQL = "select secondaryvaluechain_gid,secondaryvaluechain_name from ocs_mst_tapplication2secondaryvaluechain where application_gid='" + application_gid + "'";
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
            lsapplication_gid = objdbconn.GetExecuteScalar("select application_gid from ocs_mst_tapplication where application_gid='" + values.application_gid + "' and " +
                " headapproval_status='Comment Raised'");
            if (lsapplication_gid == "" || lsapplication_gid == null)
            {
            }
            else
            {
                string lsverticalgid = objdbconn.GetExecuteScalar("select vertical_gid from ocs_mst_tapplication where application_gid='" + values.application_gid + "'");

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
                string lscreditgroup_gid = objdbconn.GetExecuteScalar("select creditgroup_gid from ocs_mst_tapplication where application_gid='" + values.application_gid + "'");

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
            string lsapp_refno;
            msSQL = "select count(geneticcode_gid) from ocs_mst_tgeneticcode where status='Y'";
            string lsmastercount = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(application2geneticcode_gid) from ocs_mst_tapplication2geneticcode where application_gid='" + employee_gid + "' or application_gid='" + values.application_gid + "'";
            string lsgeneticcount = objdbconn.GetExecuteScalar(msSQL);
            if (lsmastercount == lsgeneticcount)
            {
                msSQL = "select application2contact_gid from ocs_mst_tapplication2contactno where (application_gid='" + employee_gid + "' or application_gid='" + values.application_gid + "')" +
                  " and primary_mobileno='Yes'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Kindly Add Primary Mobile Number ";
                    return;
                }
                objODBCDatareader.Close();

                msSQL = "select application2email_gid from ocs_mst_tapplication2email where (application_gid='" + employee_gid + "' or application_gid='" + values.application_gid + "')" +
                     " and primary_emailaddress='Yes'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Kindly Add Primary Email Adddress";
                    return;
                }

                msSQL = "select application2product_gid from ocs_mst_tapplication2product  where (application_gid='" + employee_gid + "' or application_gid='" + values.application_gid + "') ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Kindly Add Atleast One Product Details";
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
                             " category_name, botanical_name, alternative_name from ocs_mst_tapplication " +
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
                    msSQL = "select status from ocs_mst_tapplication where application_gid='" + values.application_gid + "' ";
                    string lsstatus = objdbconn.GetExecuteScalar(msSQL);
                    if (lsstatus == "" || lsstatus == null)
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

                        msSQL = "select application_no status from ocs_mst_tapplication where application_gid='" + values.application_gid + "' ";
                        lsapp_refno = objdbconn.GetExecuteScalar(msSQL);
                    }
                    msSQL = " select concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as level_one ,a.employeereporting_to from adm_mst_tmodule2employee a " +
                            " left join hrm_mst_temployee f on f.employee_gid = a.employeereporting_to " +
                            " left join adm_mst_tuser g on g.user_gid = f.user_gid " +                     
                            " where  a.module_gid in  (select module_gid_parent from adm_mst_tmodule where module_gid in  " +
                            "  (select modulereportingto_gid from adm_mst_tcompany)) and g.user_status = 'Y' and a.employee_gid ='" + employee_gid + "' " +
                            "  group by a.employee_gid ";
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
                    string lsbaselocationname, lsclustername, lsregionname, lszonalname;

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
                            " g.vertical_gid = '" + values.vertical_gid + "' and h.vertical_gid = '" + values.vertical_gid + "' " +
                            " and c.program_gid = '" + values.program_gid + "'" +
                            " and e.program_gid = '" + values.program_gid + "' and " +
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
                        msSQL = " update ocs_mst_tapplication set " +
                            " application_no='" + lsapp_refno + "'," +
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
                    }
                    objODBCDatareader1.Close();
                    if (mnResult == 1)
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("ABUL");

                        msSQL = "Insert into ocs_mst_tapplicationbasicdetailsupdatelog(" +
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

                        //msSQL = "select primaryvaluechain_gid,primaryvaluechain_name from ocs_mst_tapplication2primaryvaluechain where application_gid='" + values.application_gid + "'";
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
                        //        msSQL = " insert into ocs_mst_tapplication2primaryvaluechain(" +
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
                        //        msSQL = "select application2primaryvaluechain_gid from ocs_mst_tapplication2primaryvaluechain where primaryvaluechain_gid='" + existingprimaryvaluechain_list[i].valuechain_gid + "' and application_gid = '" + values.application_gid + "'";
                        //        string lsapplication2primaryvaluechain_gid = objdbconn.GetExecuteScalar(msSQL);

                        //        msSQL = "delete from ocs_mst_tapplication2primaryvaluechain where application2primaryvaluechain_gid='" + lsapplication2primaryvaluechain_gid + "'";
                        //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        //    }
                        //}

                        //Secondary Value Chain

                        //msSQL = "select secondaryvaluechain_gid,secondaryvaluechain_name from ocs_mst_tapplication2secondaryvaluechain where application_gid='" + values.application_gid + "'";
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
                        //            msSQL = " insert into ocs_mst_tapplication2secondaryvaluechain(" +
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
                        //        msSQL = "select application2secondaryvaluechain_gid from ocs_mst_tapplication2secondaryvaluechain where secondaryvaluechain_gid='" + existingsecondaryvaluechain_list[i].valuechain_gid + "' and application_gid = '" + values.application_gid + "'";
                        //        string lsapplication2secondaryvaluechain_gid = objdbconn.GetExecuteScalar(msSQL);

                        //        msSQL = "delete from ocs_mst_tapplication2secondaryvaluechain where application2secondaryvaluechain_gid='" + lsapplication2secondaryvaluechain_gid + "'";
                        //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        //    }
                        //}

                        //Updates

                        msSQL = "update ocs_mst_tapplication2contactno set application_gid ='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update ocs_mst_tapplication2email set application_gid ='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update ocs_mst_tapplication2geneticcode set application_gid ='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update ocs_mst_tapplication2product set application_gid ='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " select institution_gid from ocs_mst_tinstitution where application_gid = '" + values.application_gid + "' ";

                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        if (dt_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                if (lsprogram_gid != values.program_gid)
                                {
                                    msSQL = "select institution2documentupload_gid from ocs_mst_tinstitution2documentupload where institution_gid='" + dt["institution_gid"] + "'";
                                    dt_datatable = objdbconn.GetDataTable(msSQL);
                                    if (dt_datatable1.Rows.Count != 0)
                                    {
                                        foreach (DataRow dt1 in dt_datatable1.Rows)
                                        {
                                            msGetGidinstitution2documentlog = objcmnfunctions.GetMasterGID("INDO");
                                            msSQL = " insert into ocs_mst_tinstitution2documentuploadlog " +
                                                    " (institution2documentuploadlog_gid,institution2documentupload_gid,institution_gid,document_name,document_path," +
                                                    " created_by,created_date,updated_date,updated_by,companydocument_gid,document_title,document_id,covenant_type," +
                                                    " untagged_type,untagged_by,untagged_date,covenant_periods,covenantperiod_updatedby,covenantperiod_updateddate," +
                                                    " migration_flag,documenttype_gid,documenttype_name) " +
                                                    " select @institution2documentuploadlog_gid := '" + msGetGidinstitution2documentlog + "', " +
                                                    " institution2documentupload_gid,institution_gid,document_name,document_path, " +
                                                    " institution2documentuploadlog_gid,institution2documentupload_gid,institution_gid,document_name,document_path," +
                                                    " created_by,created_date,updated_date,updated_by,companydocument_gid,document_title,document_id,covenant_type," +
                                                    " untagged_type,untagged_by,untagged_date,covenant_periods,covenantperiod_updatedby,covenantperiod_updateddate," +
                                                    " migration_flag,documenttype_gid,documenttype_name " +
                                                    " from ocs_mst_tinstitution2documentupload " +
                                                    " where application_gid='" + values.application_gid + "'" +
                                                    " and institution2documentupload_gid='" + dt1["institution2documentupload_gid"].ToString() + "'";
                                            mnResultinstitution2documentlog = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        }
                                    }
                                    else
                                    {

                                    }
                                    if (mnResultinstitution2documentlog == 1)
                                    {
                                        msSQL = " delete from ocs_mst_tinstitution2documentupload where where institution_gid='" + dt["institution_gid"] + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }
                                }
                            }

                        }

                        msSQL = " select contact_gid from ocs_mst_tcontact where application_gid = '" + values.application_gid + "' ";

                        dt_datatable2 = objdbconn.GetDataTable(msSQL);
                        if (dt_datatable2.Rows.Count != 0)
                        {
                            foreach (DataRow dt2 in dt_datatable2.Rows)
                            {
                                if (lsprogram_gid != values.program_gid)
                                {
                                    msSQL = "select contact2document_gid from ocs_mst_tcontact2document where contact_gid='" + dt2["contact_gid"] + "'";
                                    dt_datatable3 = objdbconn.GetDataTable(msSQL);
                                    if (dt_datatable3.Rows.Count != 0)
                                    {
                                        foreach (DataRow dt3 in dt_datatable3.Rows)
                                        {
                                            msGetGidcontact2documentlog = objcmnfunctions.GetMasterGID("CDUL");
                                            msSQL = " insert into ocs_mst_tcontact2documentlog " +
                                                    " (contact2documentlog_gid,contact2document_gid,contact_gid,individualdocument_gid,document_gid," +
                                                    " document_title,document_name,document_path,created_by,created_date,updated_date,updated_by,covenant_type," +
                                                    " untagged_type,untagged_by,untagged_date,covenant_periods,covenantperiod_updatedby,covenantperiod_updateddate," +
                                                    " migration_flag,documenttype_gid,documenttype_name) " +
                                                    " select @contact2documentlog_gid := '" + msGetGidcontact2documentlog + "', " +
                                                    " contact2document_gid,contact_gid,individualdocument_gid,document_gid," +
                                                    " document_title,document_name,document_path,created_by,created_date,updated_date,updated_by,covenant_type," +
                                                    " untagged_type,untagged_by,untagged_date,covenant_periods,covenantperiod_updatedby,covenantperiod_updateddate," +
                                                    " migration_flag,documenttype_gid,documenttype_name " +
                                                    " from ocs_mst_tcontact2document " +
                                                    " where application_gid='" + values.application_gid + "'" +
                                                    " and contact2document_gid='" + dt3["contact2document_gid"].ToString() + "'";
                                            mnResultcontact2documentlog = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        }
                                    }
                                    else
                                    {

                                    }
                                    if (mnResultcontact2documentlog == 1)
                                    {
                                        msSQL = " delete from ocs_mst_tcontact2document where where contact_gid='" + dt2["contact_gid"] + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }
                                }
                            }
                        }

                        values.status = true;
                        values.message = "Basic Details Updated Successfully";
                    }
                    else
                    {
                        values.message = "Location / Vertical not Assigned for Business Approval";
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
            msSQL = "delete from ocs_mst_tapplication2contactno where application_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_mst_tapplication2email where application_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_mst_tapplication2geneticcode where application_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_mst_tapplication2product where application_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }

        public void DaEditProceed(string application_gid, MdlMstApplicationAdd values, string employee_gid)
        {
            msSQL = "select a.application_gid from ocs_mst_tapplication a" +
               " left join ocs_mst_tcontact b on a.application_gid = b.application_gid " +
               " left join ocs_mst_tinstitution c on a.application_gid = c.application_gid" +
               " where a.application_gid ='" + application_gid + "'" +
               " and(b.stakeholder_type in ('Applicant','Borrower') or c.stakeholder_type in ('Applicant','Borrower'))";
            string lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            if (lsapplication_gid == "" || lsapplication_gid == null)
            {
                values.proceed_flag = "N";
            }
            else
            {

                msSQL = "select applicant_type from ocs_mst_tapplication where application_gid='" + application_gid + "'";
                string lsapplicant_type = objdbconn.GetExecuteScalar(msSQL);

                if (lsapplicant_type == "" || lsapplicant_type == null)
                {
                    values.proceed_flag = "N";
                }
                else
                {
                    msSQL = "select productcharge_flag from  ocs_mst_tapplication where application_gid='" + application_gid + "'";
                    string lsproductcharge_flag = objdbconn.GetExecuteScalar(msSQL);
                    if (lsproductcharge_flag == "N" || lsproductcharge_flag == null || lsproductcharge_flag == "")
                    {
                        values.proceed_flag = "N";
                    }
                    else
                    {
                        msSQL = "select application2loan_gid from ocs_mst_tapplication2loan where application_gid='" + application_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            msSQL = "select group_gid from ocs_mst_tgroup where application_gid = '" + application_gid + "' and group_status='Incomplete'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == false)
                            {
                                msSQL = "select contact_gid from ocs_mst_tcontact where application_gid = '" + application_gid + "' and contact_status='Incomplete'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == false)
                                {
                                    msSQL = "select institution_gid from ocs_mst_tinstitution where application_gid = '" + application_gid + "' and institution_status='Incomplete'";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == false)
                                    {
                                        objODBCDatareader.Close();
                                        msSQL = " select clustermanager_name,zonalhead_name,regionalhead_name,businesshead_name,relationshipmanager_name as level_zero,drm_name as level_one from ocs_mst_tapplication where application_gid = '" + lsapplication_gid + "'";
                                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader1.HasRows == true)
                                        {
                                            values.cluster_head = objODBCDatareader1["clustermanager_name"].ToString();
                                            values.zonal_head = objODBCDatareader1["zonalhead_name"].ToString();
                                            values.regional_head = objODBCDatareader1["regionalhead_name"].ToString();
                                            values.business_head = objODBCDatareader1["businesshead_name"].ToString();
                                            values.level_zero = objODBCDatareader1["level_zero"].ToString();
                                            values.level_one = objODBCDatareader1["level_one"].ToString();
                                        }


                                        objODBCDatareader1.Close();
                                     
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

            msSQL = "select applicationapproval_gid from ocs_trn_tapplicationapproval where application_gid='" + application_gid + "'";
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
 
            msSQL = " select drm_gid, drm_name from ocs_mst_tapplication where application_gid = '" + values.application_gid + "' ";
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
                msSQL = " select approval_gid, approval_name from ocs_trn_tapplicationapproval where application_gid = '" + values.application_gid + "' and hierary_level='1' ";
                objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader1.HasRows == false)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("APAP");
                    msSQL = "Insert into ocs_trn_tapplicationapproval( " +
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

            msSQL = " select clustermanager_name,zonalhead_name,regionalhead_name,businesshead_name,clustermanager_gid,zonalhead_gid,regionalhead_gid,businesshead_gid from ocs_mst_tapplication where application_gid = '" + values.application_gid + "'";
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

                    if (level == '\u0002')
                    {
                        msSQL = " select approval_gid, approval_name from ocs_trn_tapplicationapproval where application_gid = '" + values.application_gid + "' and hierary_level='2' ";
                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader1.HasRows == false)
                        {
                            msGetGid = objcmnfunctions.GetMasterGID("APAP");
                            msSQL = "Insert into ocs_trn_tapplicationapproval( " +
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
                        msSQL = " select approval_gid, approval_name from ocs_trn_tapplicationapproval where application_gid = '" + values.application_gid + "' and hierary_level='3' ";
                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader1.HasRows == false)
                        {
                            
                                msGetGid = objcmnfunctions.GetMasterGID("APAP");
                                msSQL = "Insert into ocs_trn_tapplicationapproval( " +
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
                        msSQL = " select approval_gid, approval_name from ocs_trn_tapplicationapproval where application_gid = '" + values.application_gid + "' and hierary_level='4' ";
                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader1.HasRows == false)
                        {

                            msGetGid = objcmnfunctions.GetMasterGID("APAP");
                            msSQL = "Insert into ocs_trn_tapplicationapproval( " +
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
                        msSQL = " select approval_gid, approval_name from ocs_trn_tapplicationapproval where application_gid = '" + values.application_gid + "' and hierary_level='5' ";
                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader1.HasRows == false)
                        {

                            msGetGid = objcmnfunctions.GetMasterGID("APAP");
                            msSQL = "Insert into ocs_trn_tapplicationapproval( " +
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
            //msSQL = "update ocs_mst_tapplication set approval_flag='Y', approval_status='Submitted to Underwriting',submitted_by='"+employee_gid+"',"+
            //    " submitted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where application_gid='" + values.application_gid + "'";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update ocs_mst_tapplication set approval_status='Submitted to Approval',submitted_by='" + employee_gid + "'," +
            " submitted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
            " where application_gid='" + values.application_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "select applicant_type from ocs_mst_tapplication where application_gid='" + values.application_gid + "'";
                string lsapplicant_type = objdbconn.GetExecuteScalar(msSQL);

                if (lsapplicant_type == "Individual")
                {

                    msSQL = "select concat(first_name,middle_name,last_name) as customer_name,mobile_no,email_address,contact_gid,urn as customer_urn from ocs_mst_tcontact where" +
                            " application_gid='" + values.application_gid + "' and stakeholder_type in ('Applicant','Borrower')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lscustomer_name = objODBCDatareader["customer_name"].ToString();
                        lsmobile_no = objODBCDatareader["mobile_no"].ToString();
                        lsemail_address = objODBCDatareader["email_address"].ToString();
                        lscustomer_urn = objODBCDatareader["customer_urn"].ToString();
                        //Region
                        msSQL = "select state from ocs_mst_tcontact2address where primary_status='Yes' and contact_gid='" + objODBCDatareader["contact_gid"].ToString() + "'";
                        lsregion = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = " update ocs_mst_tapplication set customer_name='" + lscustomer_name + "'," +
                       " mobile_no='" + lsmobile_no + "'," +
                       " email_address='" + lsemail_address + "'," +
                       " region='" + lsregion + "'," +
                       " customer_urn='" + lscustomer_urn + "'," +
                       " updated_by='" + employee_gid + "'," +
                       " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                       " where application_gid='" + values.application_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    objODBCDatareader.Close();

                }
                else
                {


                    msSQL = "select company_name,mobile_no,email_address,institution_gid,urn as customer_urn from ocs_mst_tinstitution where " +
                        " application_gid='" + values.application_gid + "' and stakeholder_type in ('Applicant','Borrower')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lscustomer_name = objODBCDatareader["company_name"].ToString();
                        lsmobile_no = objODBCDatareader["mobile_no"].ToString();
                        lsemail_address = objODBCDatareader["email_address"].ToString();
                        lscustomer_urn = objODBCDatareader["customer_urn"].ToString();
                        //Region
                        msSQL = "select state from ocs_mst_tinstitution2address where primary_status='Yes' and institution_gid='" + objODBCDatareader["institution_gid"].ToString() + "'";
                        lsregion = objdbconn.GetExecuteScalar(msSQL);

                        //Main Table 
                        msSQL = " update ocs_mst_tapplication set customer_name='" + lscustomer_name + "'," +
                       " mobile_no='" + lsmobile_no + "'," +
                       " email_address='" + lsemail_address + "'," +
                       " region='" + lsregion + "'," +
                       " customer_urn='" + lscustomer_urn + "'," +
                       " updated_by='" + employee_gid + "'," +
                       " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                       " where application_gid='" + values.application_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    objODBCDatareader.Close();
                }
                ////Application 
                //msSQL = "insert into ocs_mst_topsapplication (select * from ocs_mst_tapplication where application_gid='" + values.application_gid + "') ";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //msSQL = "insert into ocs_mst_topsapplication2contactno (select * from ocs_mst_tapplication2contactno where application_gid='" + values.application_gid + "') ";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //msSQL = "insert into ocs_mst_topsapplication2email (select * from ocs_mst_tapplication2email where application_gid='" + values.application_gid + "') ";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //msSQL = "insert into ocs_mst_topsapplication2geneticcode (select * from ocs_mst_tapplication2geneticcode where application_gid='" + values.application_gid + "') ";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //// Primary Valuechain
                //msSQL = "insert into ocs_mst_topsapplication2primaryvaluechain (select * from ocs_mst_tapplication2primaryvaluechain where application_gid='" + values.application_gid + "') ";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //// Seccondary Valuechain
                //msSQL = "insert into ocs_mst_topsapplication2secondaryvaluechain (select * from ocs_mst_tapplication2secondaryvaluechain where application_gid='" + values.application_gid + "') ";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                ////Loan
                //msSQL = "insert into ocs_mst_topsapplication2loan (select * from ocs_mst_tapplication2loan where application_gid='" + values.application_gid + "') ";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //msSQL = " select application2loan_gid from ocs_mst_tapplication2loan where application_gid='" + values.application_gid + "' ";
                //dt_tloan = objdbconn.GetDataTable(msSQL);
                //if (dt_tloan.Rows.Count != 0)
                //{
                //    foreach (DataRow dt in dt_tloan.Rows)
                //    {
                //        msSQL = " insert into ocs_mst_topsapplication2buyer (select * from ocs_mst_tapplication2buyer where" +
                //            " application2loan_gid='" + dt["application2loan_gid"].ToString() + "')";
                //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //        msSQL = " insert into ocs_mst_topsuploadcollateraldocument (select * from ocs_mst_tuploadcollateraldocument where" +
                //          " application2loan_gid='" + dt["application2loan_gid"].ToString() + "')";
                //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //    }
                //}
                //dt_tloan.Dispose();
                ////Hypothecation
                //msSQL = "insert into ocs_mst_topsapplication2hypothecation (select * from ocs_mst_tapplication2hypothecation where application_gid='" + values.application_gid + "') ";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //msSQL = " select application2hypothecation_gid from ocs_mst_tapplication2hypothecation where application_gid='" + values.application_gid + "' ";
                //dt_thypothecation = objdbconn.GetDataTable(msSQL);
                //if (dt_thypothecation.Rows.Count != 0)
                //{
                //    foreach (DataRow dt in dt_thypothecation.Rows)
                //    {
                //        msSQL = " insert into ocs_mst_topsuploadhypothecationdocument (select * from ocs_mst_tuploadhypothecationocument where" +
                //            " application2hypothecation_gid='" + dt["application2hypothecation_gid"].ToString() + "')";
                //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //    }
                //}
                //dt_thypothecation.Dispose();
                ////Contact
                //msSQL = "insert into ocs_mst_topscontact (select * from ocs_mst_tcontact where application_gid='" + values.application_gid + "') ";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //msSQL = "select * from ocs_mst_tcontact where application_gid = '" + values.application_gid + "'";
                //dt_tcontact = objdbconn.GetDataTable(msSQL);
                //if (dt_tcontact.Rows.Count != 0)
                //{
                //    foreach (DataRow dt in dt_tcontact.Rows)
                //    {
                //        //Mobileno
                //        msSQL = " insert into ocs_mst_topscontact2mobileno (select * from ocs_mst_tcontact2mobileno where" +
                //          " contact_gid='" + dt["contact_gid"].ToString() + "')";
                //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //        //EMAIL
                //        msSQL = " insert into ocs_mst_topscontact2email (select * from ocs_mst_tcontact2email where" +
                //          " contact_gid='" + dt["contact_gid"].ToString() + "')";
                //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //        //Address
                //        msSQL = " insert into ocs_mst_topscontact2address (select * from ocs_mst_tcontact2address where" +
                //          " contact_gid='" + dt["contact_gid"].ToString() + "')";
                //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //        //ID-Proof
                //        msSQL = " insert into ocs_mst_topscontact2idproof (select * from ocs_mst_tcontact2idproof where" +
                //          " contact_gid='" + dt["contact_gid"].ToString() + "')";
                //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //        //Document Upload
                //        msSQL = " insert into ocs_mst_topscontact2document (select * from ocs_mst_tcontact2document where" +
                //          " contact_gid='" + dt["contact_gid"].ToString() + "')";
                //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //        //CIC - Document Upload
                //        msSQL = " insert into ocs_mst_topsindividual2cicdocumentupload (select * from ocs_mst_tindividual2cicdocumentupload where" +
                //          " contact_gid='" + dt["contact_gid"].ToString() + "')";
                //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //    }
                //}
                //dt_tcontact.Dispose();
                ////Institution
                //msSQL = "insert into ocs_mst_topsinstitution (select * from ocs_mst_tinstitution where application_gid='" + values.application_gid + "') ";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //msSQL = "select institution_gid from ocs_mst_tinstitution where application_gid = '" + values.application_gid + "'";
                //dt_tinstitution = objdbconn.GetDataTable(msSQL);
                //if (dt_tinstitution.Rows.Count != 0)
                //{
                //    foreach (DataRow dt in dt_tinstitution.Rows)
                //    {
                //        //GST
                //        msSQL = " insert into ocs_mst_topsinstitution2branch (select * from ocs_mst_tinstitution2branch where" +
                //            " institution_gid='" + dt["institution_gid"].ToString() + "')";
                //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //        //Mobileno
                //        msSQL = " insert into ocs_mst_topsinstitution2mobileno (select * from ocs_mst_tinstitution2mobileno where" +
                //          " institution_gid='" + dt["institution_gid"].ToString() + "')";
                //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //        //Address
                //        msSQL = " insert into ocs_mst_topsinstitution2address (select * from ocs_mst_tinstitution2address where" +
                //          " institution_gid='" + dt["institution_gid"].ToString() + "')";
                //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //        //E-Mail
                //        msSQL = " insert into ocs_mst_topsinstitution2email (select * from ocs_mst_tinstitution2email where" +
                //          " institution_gid='" + dt["institution_gid"].ToString() + "')";
                //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //        //License
                //        msSQL = " insert into ocs_mst_topsinstitution2licensedtl (select * from ocs_mst_tinstitution2licensedtl where" +
                //          " institution_gid='" + dt["institution_gid"].ToString() + "')";
                //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //        //Document Upload
                //        msSQL = " insert into ocs_mst_topsinstitution2documentupload (select * from ocs_mst_tinstitution2documentupload where" +
                //          " institution_gid='" + dt["institution_gid"].ToString() + "')";
                //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //        //FORM -60
                //        msSQL = " insert into ocs_mst_topsinstitution2form60documentupload (select * from ocs_mst_tinstitution2form60documentupload where" +
                //          " institution_gid='" + dt["institution_gid"].ToString() + "')";
                //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //        //CIC - Document Upload
                //        msSQL = " insert into ocs_mst_topsinstitution2cicdocumentupload (select * from ocs_mst_tinstitution2cicdocumentupload where" +
                //          " institution_gid='" + dt["institution_gid"].ToString() + "')";
                //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //    }
                //}
                //dt_tinstitution.Dispose();
                ////Service Charges
                //msSQL = "insert into ocs_mst_topsapplicationservicecharge (select * from ocs_mst_tapplicationservicecharge where application_gid='" + values.application_gid + "') ";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                ////Group
                //msSQL = "insert into ocs_mst_topsgroup (select * from ocs_mst_tgroup where application_gid='" + values.application_gid + "') ";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //msSQL = "select group_gid from ocs_mst_tgroup where application_gid = '" + values.application_gid + "'";
                //dt_tcontact = objdbconn.GetDataTable(msSQL);
                //if (dt_tcontact.Rows.Count != 0)
                //{
                //    foreach (DataRow dt in dt_tcontact.Rows)
                //    {
                //        //Address
                //        msSQL = " insert into ocs_mst_topsgroup2address (select * from ocs_mst_tgroup2address where" +
                //          " group_gid='" + dt["group_gid"].ToString() + "')";
                //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //        //Bank
                //        msSQL = " insert into ocs_mst_topsgroup2bank (select * from ocs_mst_tgroup2bank where" +
                //          " group_gid='" + dt["group_gid"].ToString() + "')";
                //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //        //Document
                //        msSQL = " insert into ocs_mst_topsgroup2document (select * from ocs_mst_tgroup2document where" +
                //          " group_gid='" + dt["group_gid"].ToString() + "')";
                //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //    }
                //}
                //dt_tcontact.Dispose();
                if (mnResult != 0)
                {
                    DaMstApplicationAdd objMstApplicationAdd = new DaMstApplicationAdd();
                    objMstApplicationAdd.FnProgramBasedDcoument(values.application_gid, employee_gid, user_gid);

                    try
                    {
                        msSQL = " select clustermanager_gid,zonalhead_gid,regionalhead_gid,businesshead_gid from ocs_mst_tapplication where application_gid = '" + values.application_gid + "'";
                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader1.HasRows == true)
                        {
                            cluster_head_gid = objODBCDatareader1["clustermanager_gid"].ToString();
                            zonal_head_gid = objODBCDatareader1["zonalhead_gid"].ToString();
                            regional_head_gid = objODBCDatareader1["regionalhead_gid"].ToString();
                            business_head_gid = objODBCDatareader1["businesshead_gid"].ToString();
                        }

                        objODBCDatareader1.Close();
                        msSQL = " select approval_gid,approval_name from ocs_trn_tapplicationapproval where application_gid = '" + values.application_gid + "' and hierary_level ='1'";
                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader1.HasRows == true)
                        {
                            reportingto_gid = objODBCDatareader1["approval_gid"].ToString();
                            reportingto_name = objODBCDatareader1["approval_name"].ToString();
                        }
                        objODBCDatareader1.Close();
                        msSQL = " SELECT pop_server, pop_port  FROM adm_mst_tcompany";
                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader1.HasRows == true)
                        {
                            ls_server = objODBCDatareader1["pop_server"].ToString();
                            ls_port = Convert.ToInt32(objODBCDatareader1["pop_port"]);
                            ls_username = ConfigurationManager.AppSettings["SamunnatiApprovalEmail"];
                            ls_password = ConfigurationManager.AppSettings["SamunnatiApprovalEmailPassword"];
                        }
                        objODBCDatareader1.Close();

                        msSQL = "select application_no from ocs_mst_tapplication where application_gid='" + values.application_gid + "'";
                        application_no = objdbconn.GetExecuteScalar(msSQL);
                        msSQL = "select customerref_name from ocs_mst_tapplication where application_gid='" + values.application_gid + "'";
                        customer_name = objdbconn.GetExecuteScalar(msSQL);
                        msSQL = "select b.employee_emailid from ocs_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.clustermanager_gid where application_gid='" + values.application_gid + "'";
                        cluster_head_mailid = objdbconn.GetExecuteScalar(msSQL);
                        msSQL = " select b.employee_emailid from ocs_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.zonalhead_gid where a.application_gid='" + values.application_gid + "'";
                        zonalhead_mailid = objdbconn.GetExecuteScalar(msSQL);
                        msSQL = " select b.employee_emailid from ocs_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.regionalhead_gid where a.application_gid='" + values.application_gid + "'";
                        regional_head_mailid = objdbconn.GetExecuteScalar(msSQL);
                        msSQL = " select b.employee_emailid from ocs_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.businesshead_gid where a.application_gid='" + values.application_gid + "'";
                        business_head_mailid = objdbconn.GetExecuteScalar(msSQL);
                        msSQL = " select b.employee_emailid from ocs_trn_tapplicationapproval a left join hrm_mst_temployee b on b.employee_gid = a.approval_gid where a.application_gid='" + values.application_gid + "'  and hierary_level ='1'";
                        reportingto_mailid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select  concat( c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code)  from ocs_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.clustermanager_gid join adm_mst_tuser c on c.user_gid = b.user_gid where application_gid='" + values.application_gid + "'";
                        string cluster_head = objdbconn.GetExecuteScalar(msSQL);
                        msSQL = " select  concat( c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code)  from ocs_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.zonalhead_gid left join adm_mst_tuser c on c.user_gid = b.user_gid where a.application_gid='" + values.application_gid + "'";
                        string zonal_head = objdbconn.GetExecuteScalar(msSQL);
                        msSQL = " select  concat( c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code)  from ocs_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.relationshipmanager_gid left join adm_mst_tuser c on c.user_gid = b.user_gid where a.application_gid='" + values.application_gid + "'";
                        string rm_name = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select b.employee_emailid from ocs_mst_tapplication a left join hrm_mst_temployee b on b.employee_gid = a.created_by where a.application_gid='" + values.application_gid + "'";
                        creater_mailid = objdbconn.GetExecuteScalar(msSQL);

                        tomail_id = reportingto_mailid;
                        lssource = ConfigurationManager.AppSettings["img_path"];

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
                        body = body + "&nbsp&nbsp <b>Cluster Head Name:</b> " + HttpUtility.HtmlEncode(cluster_head) + "<br /><br />";
                        body = body + "&nbsp&nbsp <b>Zonal Head Name:</b> " + HttpUtility.HtmlEncode(zonal_head) + "<br /><br />";
                        body = body + "&nbsp&nbsp <b>Action Time:</b> " + DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") + "<br /><br />";
                        body = body + "<br />";
                        body = body + "&nbsp&nbsp Regards,";
                        body = body + "<br />";
                        body = body + "&nbsp&nbsp Sam-Custopedia <br /> ";
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
                        lsBccmail_id = ConfigurationManager.AppSettings["ApprovalBccMail"].ToString();

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
                        //cc_mailid = "" + cluster_head_mailid + "," + regional_head_mailid + "," + zonalhead_mailid + "";

                        //if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                        //{
                        //    lsCCReceipients = cc_mailid.Split(',');
                        //    if (cc_mailid.Length == 0)
                        //    {
                        //        message.CC.Add(new MailAddress(cc_mailid));
                        //    }
                        //    else
                        //    {
                        //        foreach (string CCEmail in lsCCReceipients)
                        //        {
                        //            message.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                        //        }
                        //    }
                        //}



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

                        //Customer mail


                        //msSQL = " select relationshipmanager_gid,overalllimit_amount,clustermanager_gid,relationshipmanager_name,customerref_name,product_name from ocs_mst_tapplication where application_gid = '" + values.application_gid + "'";
                        //objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        //if (objODBCDatareader1.HasRows == true)
                        //{
                        //    ls_overalllimit_amount = objODBCDatareader1["overalllimit_amount"].ToString();
                        //    ls_clustermanager_gid = objODBCDatareader1["clustermanager_gid"].ToString();
                        //    ls_relationshipmanager_gid = objODBCDatareader1["relationshipmanager_gid"].ToString();
                        //    ls_relationshipmanager_name = objODBCDatareader1["relationshipmanager_name"].ToString();
                        //    ls_customerref_name = objODBCDatareader1["customerref_name"].ToString();
                        //    ls_product_name = objODBCDatareader1["product_name"].ToString();

                        //}
                        //objODBCDatareader1.Close();
                        //msSQL = " select employee_mobileno, employee_emailid, concat(c.user_firstname,' ',c.user_lastname,' || ',c.user_code) as cluster_name from hrm_mst_temployee b " +
                        // " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        // "  where employee_gid = '" + ls_clustermanager_gid + "'";
                        //objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        //if (objODBCDatareader1.HasRows == true)
                        //{
                        //    lsemployee_mobileno = objODBCDatareader1["employee_mobileno"].ToString();
                        //    lsemployee_emailid = objODBCDatareader1["employee_emailid"].ToString();
                        //    lscluster_name = objODBCDatareader1["cluster_name"].ToString();


                        //}
                        //objODBCDatareader1.Close();

                        //msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + ls_relationshipmanager_gid + "'";
                        //string cc_employee_emailid = objdbconn.GetExecuteScalar(msSQL);

                        //msSQL = " select institution_gid from  ocs_mst_tinstitution where stakeholder_type = 'Applicant' and application_gid = '" + values.application_gid + "'";
                        //objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        //if (objODBCDatareader1.HasRows == true)
                        //{
                        //    ls_institution_gid = objODBCDatareader1["institution_gid"].ToString();

                        //    msSQL = "select email_address from ocs_mst_tinstitution2email where primary_status = 'Yes' and institution_gid ='" + ls_institution_gid + "'";
                        //   lsemail_toaddress = objdbconn.GetExecuteScalar(msSQL);

                        //}
                        //else
                        //{

                        //    msSQL = " select contact_gid from  ocs_mst_tcontact where stakeholder_type = 'Applicant' and application_gid = '" + values.application_gid + "'";
                        //    string lscontact_gid = objdbconn.GetExecuteScalar(msSQL);

                        //    msSQL = "select email_address from ocs_mst_tcontact2email where primary_status = 'Yes' and contact_gid ='" + lscontact_gid + "'";
                        //   lsemail_toaddress = objdbconn.GetExecuteScalar(msSQL);

                        //}
                        //tomail_id1 = lsemail_toaddress;


                        //sub1 = "  Application Status ";
                        ////body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
                        ////body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;align:center;'>";
                        //body1 = body1 + "<br />";
                        ////body = body + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <img style='height:150px; width:380px;' src='" + lssource + "'><br />";
                        ////body = body + "<br />";
                        //body1 = body1 + " &nbsp&nbsp Dear Customer,<br />";
                        //body1 = body1 + "<br />";

                        //body1 = body1 + "&nbsp&nbsp This is to inform you that the Loan application requested by you has been submitted by the Relationship Manager (" + ls_relationshipmanager_name + ").<br />";
                        //body1 = body1 + "<br />";
                        //body1 = body1 + "&nbsp&nbsp <b>Applicant Name:</b> " + ls_customerref_name + "<br /><br />";
                        //body1 = body1 + "&nbsp&nbsp <b>Proposed Limit:</b> " + ls_overalllimit_amount + "<br /><br />";
                        //body1 = body1 + "&nbsp&nbsp <b>Loan Product: </b>" + ls_product_name + "<br /><br />";
                        //body1 = body1 + "<br />";
                        //body1 = body1 + "&nbsp&nbsp Regards,";
                        //body1 = body1 + "<br />";
                        //body1 = body1 + "&nbsp&nbsp Samunnati<br /> ";
                        //body1 = body1 + "<br />";
                        //body1 = body1 + "&nbsp&nbsp Note: If you are not able to reach out to the RM, please contact (" + lscluster_name + "/" + lsemployee_emailid + "/ " + lsemployee_mobileno + ")<br /> ";
                        //body1 = body1 + "<br />";
                        ////body = body + "</td><td>&nbsp&nbsp</td></tr></table>";
                        //MailMessage message1 = new MailMessage();
                        //SmtpClient smtp1 = new SmtpClient();
                        //message1.From = new MailAddress(ls_username);
                        //message1.To.Add(new MailAddress(tomail_id1));
                        //lsBccmail_id = ConfigurationManager.AppSettings["ApprovalBccMail"].ToString();
                        //if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                        //{
                        //    lsBCCReceipients = lsBccmail_id.Split(',');
                        //    if (lsBccmail_id.Length == 0)
                        //    {
                        //        message1.Bcc.Add(new MailAddress(lsBccmail_id));
                        //    }
                        //    else
                        //    {
                        //        foreach (string BCCEmail in lsBCCReceipients)
                        //        {
                        //            message1.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                        //        }
                        //    }
                        //}
                        //cc_mailid = "" + cc_employee_emailid + "";

                        //if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                        //{
                        //    lsCCReceipients = cc_mailid.Split(',');
                        //    if (cc_mailid.Length == 0)
                        //    {
                        //        message1.CC.Add(new MailAddress(cc_mailid));
                        //    }
                        //    else
                        //    {
                        //        foreach (string CCEmail in lsCCReceipients)
                        //        {
                        //            message1.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                        //        }
                        //    }
                        //}

                        //message1.Subject = sub1;
                        //message1.IsBodyHtml = true; //to make message body as html  
                        //message1.Body = body1;
                        //smtp1.Port = ls_port;
                        //smtp1.Host = ls_server; //for gmail host  
                        //smtp1.EnableSsl = true;
                        //smtp1.UseDefaultCredentials = false;
                        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        //smtp1.Credentials = new NetworkCredential(ls_username, ls_password);
                        //smtp1.DeliveryMethod = SmtpDeliveryMethod.Network;
                        //smtp1.Send(message1);


                        values.status = true;
                        values.message = "Application Creation Submitted Successfully";
                    }
                    catch (Exception ex)
                    {
                        values.message = ex.ToString();
                        values.status = false;

                    }
                    bool customer_mail_status = objCusotmerMail.DaApplicationCreationMail(values.application_gid);
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured";
                }
            }
        }

        public void DaEditAppReProceed(string employee_gid, string user_gid, MdlMstApplicationAdd values)
        {
            msSQL = "update ocs_mst_tapplication set resubmitted_by='" + employee_gid + "'," +
                   " resubmitted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                   " where application_gid='" + values.application_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "select applicant_type from ocs_mst_tapplication where application_gid='" + values.application_gid + "'";
                string lsapplicant_type = objdbconn.GetExecuteScalar(msSQL);

                if (lsapplicant_type == "Individual")
                {
                    msSQL = "select concat(first_name,middle_name,last_name) as customer_name,mobile_no,email_address,contact_gid,urn as customer_urn from ocs_mst_tcontact where" +
                        " application_gid='" + values.application_gid + "' and stakeholder_type in ('Applicant','Borrower')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lscustomer_name = objODBCDatareader["customer_name"].ToString();
                        lsmobile_no = objODBCDatareader["mobile_no"].ToString();
                        lsemail_address = objODBCDatareader["email_address"].ToString();
                        lscustomer_urn = objODBCDatareader["customer_urn"].ToString();
                        //Region
                        msSQL = "select state from ocs_mst_tcontact2address where primary_status='Yes' and contact_gid='" + objODBCDatareader["contact_gid"].ToString() + "'";
                        lsregion = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = " update ocs_mst_tapplication set customer_name='" + lscustomer_name + "'," +
                                " mobile_no='" + lsmobile_no + "'," +
                                " email_address='" + lsemail_address + "'," +
                                " region='" + lsregion + "'," +
                                " customer_urn='" + lscustomer_urn + "'," +
                                " updated_by='" + employee_gid + "'," +
                                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                " where application_gid='" + values.application_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    objODBCDatareader.Close();
                }
                else
                {
                    msSQL = "select company_name,mobile_no,email_address,institution_gid,urn as customer_urn from ocs_mst_tinstitution where " +
                        " application_gid='" + values.application_gid + "' and stakeholder_type in ('Applicant','Borrower')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lscustomer_name = objODBCDatareader["company_name"].ToString();
                        lsmobile_no = objODBCDatareader["mobile_no"].ToString();
                        lsemail_address = objODBCDatareader["email_address"].ToString();
                        lscustomer_urn = objODBCDatareader["customer_urn"].ToString();
                        //Region
                        msSQL = "select state from ocs_mst_tinstitution2address where primary_status='Yes' and institution_gid='" + objODBCDatareader["institution_gid"].ToString() + "'";
                        lsregion = objdbconn.GetExecuteScalar(msSQL);

                        //Main Table 
                        msSQL = " update ocs_mst_tapplication set customer_name='" + lscustomer_name + "'," +
                                " mobile_no='" + lsmobile_no + "'," +
                                " email_address='" + lsemail_address + "'," +
                                " region='" + lsregion + "'," +
                                " customer_urn='" + lscustomer_urn + "'," +
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
                    values.message = "Application Submited successfully";
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

            msSQL = "select application_gid from ocs_mst_tinstitution where institution_gid='" + values.institution_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            if (values.stakeholder_type == "Borrower" || values.stakeholder_type == "Applicant")
            {
                msSQL = "select stakeholder_type from ocs_mst_tcontact where application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Borrower','Applicant')";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Already Added Applicant/Borrower Information";
                    return false;
                }
                msSQL = "select stakeholder_type from ocs_mst_tinstitution where application_gid='" + lsapplication_gid + "' and " +
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
            msSQL = " update ocs_mst_tinstitution set " +
                        " company_name='" + values.company_name.Replace("'", "\\'") + "',";
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
            if (Convert.ToDateTime(values.Renewaldue_date).AddDays(1).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += " renewaldue_date='" + Convert.ToDateTime(values.Renewaldue_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
            }
            msSQL += " lastyear_turnover='" + values.lastyear_turnover + "'," +
                     " escrow='" + values.escrow + "'," +
                     " urn_status='" + values.urn_status + "'," +
                     " urn='" + values.urn + "'," +
                     " institution_status='Incomplete'," +
                     " nearsamunnatiabranch_gid ='" + values.nearsamunnatiabranch_gid + "'," +
                     " nearsamunnatiabranch_name ='" + values.nearsamunnatiabranch_name + "'," +
                     " udhayam_registration ='" + values.udhayam_registration + "'," +
                     " tan_number ='" + values.tan_number + "'," +
                     " business_description ='" + values.business_description + "'," +
                     " tanstate_gid ='" + values.tanstate_gid + "'," +
                     " tanstate_name ='" + values.tanstate_name + "'," +
                     " internalrating_gid ='" + values.internalrating_gid + "'," +
                     " internalrating_name ='" + values.internalrating_name + "'," +
                     " sales ='" + values.sales + "'," +
                     " purchase ='" + values.purchase + "'," +
                     " credit_summation ='" + values.credit_summation + "'," +
                     " cheque_bounce ='" + values.cheque_bounce + "'," +
                     " numberof_boardmeetings ='" + values.numberof_boardmeetings + "'," +
                     " farmer_count ='" + values.farmer_count + "'," +
                     " crop_cycle ='" + values.crop_cycle + "'," +
                     " calamities_prone ='" + values.calamities_prone + "'," +

                     " msme_regi_no ='" + values.msme_regi_no + "'," +
                     " lei_no ='" + values.lei_no + "'," +
                     " kin_no ='" + values.kin_no + "'," +
                    

                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where institution_gid='" + values.institution_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                msSQL = " delete from ocs_mst_tinstitution2fpocacity where institution_gid = '" + values.institution_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    if (values.fpocity_list == null)
                    {
                    }
                    else
                    {
                        for (var i = 0; i < values.fpocity_list.Count; i++)
                        {
                            msinstitution2fpocacity_gid = objcmnfunctions.GetMasterGID("I2FC");
                            msSQL = " Insert into ocs_mst_tinstitution2fpocacity( " +
                                   " institution2fpocacity_gid, " +
                                   " institution_gid, " +
                                   " city_gid," +
                                   " city_name," +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + msinstitution2fpocacity_gid + "'," +
                                   "'" + values.institution_gid + "'," +
                                   "'" + values.fpocity_list[i].city_gid + "'," +
                                   "'" + values.fpocity_list[i].city_name + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                    }
                }

                // Updates for Multiple Add
                msSQL = "update ocs_mst_tinstitution2branch set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tinstitution2mobileno set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tinstitution2email set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tinstitution2address set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tinstitution2licensedtl set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tinstitution2documentupload set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tinstitution2form60documentupload set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycgstsbpan set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycudyamauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tinstitution2equipment set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tinstitution2livestock set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tinstitution2receivable set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
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


            msSQL = "select stakeholder_type from ocs_mst_tcontact where application_gid='" + values.application_gid + "' and stakeholder_type in ('Borrower','Applicant')";
            string lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholder_type)
            {

                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return false;
            }

            msSQL = "select stakeholder_type from ocs_mst_tinstitution where application_gid='" + values.application_gid + "' and stakeholder_type in ('Borrower','Applicant')";
            lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholder_type)
            {

                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return false;
            }
            if (values.business_description == null || values.business_description == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.business_description.Replace("'", "") + "',";
            }
            msGetGid = objcmnfunctions.GetMasterGID("APIN");
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
                    " numberof_boardmeetings, " +
                    " farmer_count, " +
                    " crop_cycle, " +
                    " calamities_prone, " +
                     " msme_regi_no," +
                    " lei_no," +
                    " kin_no," +
                    " renewaldue_date," +
                    " created_by," +
                    " created_date) values(" +
                  "'" + msGetGid + "'," +
                  "'" + values.application_gid + "'," +
                  "'" + values.company_name.Replace("'", "\\'")  + "',";
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
                    "'" + values.nearsamunnatiabranch_gid + "'," +
                    "'" + values.nearsamunnatiabranch_name + "'," +
                    "'" + values.udhayam_registration + "'," +
                    "'" + values.tan_number + "'," +
                    "'" + values.business_description + "'," +
                    "'" + values.tanstate_gid + "'," +
                    "'" + values.tanstate_name + "'," +
                    "'" + values.internalrating_gid + "'," +
                    "'" + values.internalrating_name + "'," +
                    "'" + values.sales + "'," +
                    "'" + values.purchase + "'," +
                    "'" + values.credit_summation + "'," +
                    "'" + values.cheque_bounce + "'," +
                    "'" + values.numberof_boardmeetings + "'," +
                    "'" + values.farmer_count + "'," +
                    "'" + values.crop_cycle + "'," +
                    "'" + values.calamities_prone + "'," +
                     "'" + values.msme_regi_no + "'," +
                    "'" + values.lei_no + "'," +
                    "'" + values.kin_no + "',";
            if ((values.renewaldue_date == null) || (values.renewaldue_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.renewaldue_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "update ocs_mst_tinstitution2branch set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tinstitution2mobileno set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tinstitution2email set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tinstitution2address set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tinstitution2licensedtl set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tinstitution2equipment set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tinstitution2livestock set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tinstitution2receivable set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select companydocument_gid,institution2documentupload_gid from ocs_mst_tinstitution2documentupload where institution_gid='" + employee_gid + "'";
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
                    msSQL = " insert into ocs_trn_tdocumentchecktls(" +
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
                        msSQL = " insert into ocs_trn_tcovanantdocumentcheckdtls(" +
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

                DaMstScannedDocument objvalues = new DaMstScannedDocument();
                objvalues.DaGroupDocChecklistinfoCredit(values.application_gid, msGetGid, employee_gid);

                if (values.fpocity_list == null)
                {
                }
                else
                {
                    for (var i = 0; i < values.fpocity_list.Count; i++)
                    {
                        msGetGid1 = objcmnfunctions.GetMasterGID("I2FC");
                        msSQL = " insert into ocs_mst_tinstitution2fpocacity(" +
                                " institution2fpocacity_gid," +
                                " institution_gid," +
                                " city_gid," +
                                " city_name," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid1 + "'," +
                                "'" + msGetGid + "'," +
                                "'" + values.fpocity_list[i].city_gid + "'," +
                                "'" + values.fpocity_list[i].city_name + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                msSQL = "update ocs_mst_tinstitution2documentupload set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tinstitution2form60documentupload set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycgstsbpan set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycudyamauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
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
        public bool DaSubmitInstitutionDtlAdd(MdlMstInstitutionAdd values, string employee_gid, string user_gid)
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
                    " from ocs_mst_tinstitution2documentupload a where  a.documenttype_gid = 'DOCT2022010611' and " +
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

            msSQL = "select institution2mobileno_gid from ocs_mst_tinstitution2mobileno where institution_gid='" + employee_gid + "' and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Mobile Number";
                return false;
            }

            msSQL = "select institution2mobileno_gid from ocs_mst_tinstitution2mobileno where institution_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Mobile Number";
                return false;
            }

            msSQL = "select institution2email_gid from ocs_mst_tinstitution2email where institution_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Email Address";
                return false;
            }

            msSQL = "select institution2address_gid from ocs_mst_tinstitution2address where institution_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Address Detail";
                return false;
            }
            if (values.Gstflag == "Yes")
            {
                msSQL = "select institution2branch_gid from ocs_mst_tinstitution2branch where institution_gid='" + employee_gid + "' and headoffice_status ='Yes' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == false)
                {
                    values.status = false;
                    values.message = "Atleast Select One GST Number as Head Office";
                    return false;
                }
                objODBCDatareader.Close();
            }
            msSQL = "select stakeholder_type from ocs_mst_tcontact where application_gid='" + values.application_gid + "' and stakeholder_type in ('Borrower','Applicant')";
            string lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholder_type)
            {

                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return false;
            }

            msSQL = "select stakeholder_type from ocs_mst_tinstitution where application_gid='" + values.application_gid + "' and stakeholder_type in ('Borrower','Applicant')";
            lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholder_type)
            {

                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return false;
            }
            if (values.business_description == null || values.business_description == "")
            {
                lsbusiness_description = "";
            }
            else
            {
                lsbusiness_description = values.business_description.Replace("'", "");
            }
            msGetGid = objcmnfunctions.GetMasterGID("APIN");
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
                    " numberof_boardmeetings, " +
                    " farmer_count, " +
                    " crop_cycle, " +
                    " calamities_prone, " +
                     " msme_regi_no," +
                    " lei_no," +
                    " kin_no," +
                    " renewaldue_date," +
                    " created_by," +
                    " created_date) values(" +
                  "'" + msGetGid + "'," +
                  "'" + values.application_gid + "'," +
                  "'" + values.company_name.Replace("'", "\\'") + "',";
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
                    "'" + values.nearsamunnatiabranch_gid + "'," +
                    "'" + values.nearsamunnatiabranch_name + "'," +
                    "'" + values.udhayam_registration + "'," +
                    "'" + values.tan_number + "'," +
                    "'" + lsbusiness_description + "'," +
                    "'" + values.tanstate_gid + "'," +
                    "'" + values.tanstate_name + "'," +
                    "'" + values.internalrating_gid + "'," +
                    "'" + values.internalrating_name + "'," +
                    "'" + values.sales + "'," +
                    "'" + values.purchase + "'," +
                    "'" + values.credit_summation + "'," +
                    "'" + values.cheque_bounce + "'," +
                    "'" + values.numberof_boardmeetings + "'," +
                    "'" + values.farmer_count + "'," +
                    "'" + values.crop_cycle + "'," +
                    "'" + values.calamities_prone + "'," +
                     "'" + values.msme_regi_no + "'," +
                    "'" + values.lei_no + "'," +
                    "'" + values.kin_no + "',";
            if ((values.renewaldue_date == null) || (values.renewaldue_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.renewaldue_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "update ocs_mst_tinstitution2branch set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tinstitution2mobileno set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tinstitution2email set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tinstitution2address set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tinstitution2licensedtl set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tinstitution2equipment set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tinstitution2livestock set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tinstitution2receivable set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tapplication set updated_by='" + employee_gid + "',updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'  where application_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select companydocument_gid,institution2documentupload_gid from ocs_mst_tinstitution2documentupload where institution_gid='" + employee_gid + "'";
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
                    msSQL = " insert into ocs_trn_tdocumentchecktls(" +
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
                        msSQL = " insert into ocs_trn_tcovanantdocumentcheckdtls(" +
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

                DaMstScannedDocument objvalues = new DaMstScannedDocument();
                objvalues.DaGroupDocChecklistinfoCredit(values.application_gid, msGetGid, employee_gid);

                if (values.fpocity_list == null)
                {
                }
                else
                {
                    for (var i = 0; i < values.fpocity_list.Count; i++)
                    {
                        msGetGid1 = objcmnfunctions.GetMasterGID("I2FC");
                        msSQL = " insert into ocs_mst_tinstitution2fpocacity(" +
                                " institution2fpocacity_gid," +
                                " institution_gid," +
                                " city_gid," +
                                " city_name," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid1 + "'," +
                                "'" + msGetGid + "'," +
                                "'" + values.fpocity_list[i].city_gid + "'," +
                                "'" + values.fpocity_list[i].city_name + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                msSQL = "update ocs_mst_tinstitution2documentupload set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tinstitution2form60documentupload set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycgstsbpan set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycudyamauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = "select stakeholder_type from ocs_mst_tinstitution where institution_gid='" + msGetGid + "' ";
                    string lsstakeholders_type = objdbconn.GetExecuteScalar(msSQL);

                    if (lsstakeholders_type == "Applicant" || lsstakeholders_type == "Borrower")
                    {
                        msSQL = "select mobile_no from ocs_mst_tinstitution2mobileno where institution_gid='" + msGetGid + "' and primary_status='yes'";
                        string lsmobileno = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select email_address from ocs_mst_tinstitution2email where institution_gid='" + msGetGid + "' and primary_status='yes'";
                        lsemail_address = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select company_name,institution_gid,urn,stakeholder_type from ocs_mst_tinstitution where " +
                                " application_gid='" + values.application_gid + "' and stakeholder_type in ('Applicant','Borrower')";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lscustomer_name = objODBCDatareader["company_name"].ToString();
                            lsurn = objODBCDatareader["urn"].ToString();
                            lsstakeholder_type = objODBCDatareader["stakeholder_type"].ToString();

                            //Region
                            msSQL = "select state from ocs_mst_tinstitution2address where primary_status='Yes' and institution_gid='" + objODBCDatareader["institution_gid"].ToString() + "'";
                            lsregion = objdbconn.GetExecuteScalar(msSQL);

                            //Main Table 
                            msSQL = " update ocs_mst_tapplication set customer_name='" + lscustomer_name.Replace("'", "") + "'," +
                                    " mobile_no='" + lsmobile_no + "'," +
                                    " email_address='" + lsemail_address + "'," +
                                    " region='" + lsregion + "'," +
                                    " customer_urn='" + lsurn + "'," +
                                    " applicant_type='Institution'," +
                                    " updated_by='" + employee_gid + "'," +
                                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                    " where application_gid='" + values.application_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " update ocs_mst_tinstitution set mobile_no='" + lsmobileno + "'," +
                                    " email_address='" + lsemail_address + "' where institution_gid='" + msGetGid + "' ";
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

                if (values.lspage == "PendingCADReview")
                {

                    DaMstApplicationEdit objMstApplicationEdit = new DaMstApplicationEdit();
                    objMstApplicationEdit.FnProgramBasedDcoument4otherflows(values.application_gid, employee_gid, user_gid, values.lspage);

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
                    " from ocs_mst_tinstitution2documentupload a where  a.documenttype_gid = 'DOCT2022010611' and " +
            " ( institution_gid='" + values.institution_gid + "' or institution_gid = '" + employee_gid + "' )";

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

            msSQL = "select institution2mobileno_gid from ocs_mst_tinstitution2mobileno where institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "' and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Mobile Number";
                return false;
            }

            msSQL = "select institution2mobileno_gid from ocs_mst_tinstitution2mobileno where institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Mobile Number";
                return false;
            }

            msSQL = "select institution2email_gid from ocs_mst_tinstitution2email where institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Email Address";
                return false;
            }

            msSQL = "select institution2address_gid from ocs_mst_tinstitution2address where institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Address Detail";
                return false;
            }
            if (values.Gstflag == "Yes")
            {
                msSQL = "select institution2branch_gid from ocs_mst_tinstitution2branch where (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "') and headoffice_status ='Yes' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == false)
                {
                    values.status = false;
                    values.message = "Atleast Select One GST Number as Head Office";
                    return false;
                }
                objODBCDatareader.Close();
            }

            msSQL = "select application_gid from ocs_mst_tinstitution where institution_gid='" + values.institution_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            if (values.stakeholder_type == "Borrower" || values.stakeholder_type == "Applicant")
            {
                msSQL = "select stakeholder_type from ocs_mst_tcontact where application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Borrower','Applicant')";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Already Added Applicant/Borrower Information";
                    return false;
                }
                msSQL = "select stakeholder_type from ocs_mst_tinstitution where application_gid='" + lsapplication_gid + "' and" +
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
                msSQL = " update ocs_mst_tinstitution set " +
                        " company_name='" + values.company_name.Replace("'", "\\'") + "',";
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
                if (Convert.ToDateTime(values.Renewaldue_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " renewaldue_date='" + Convert.ToDateTime(values.Renewaldue_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
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
                         " institution_status='Completed'," +
                         " nearsamunnatiabranch_gid ='" + values.nearsamunnatiabranch_gid + "'," +
                         " nearsamunnatiabranch_name ='" + values.nearsamunnatiabranch_name + "'," +
                         " udhayam_registration ='" + values.udhayam_registration + "'," +
                         " tan_number ='" + values.tan_number + "'," +
                         " business_description ='" + values.business_description.Replace("'", "") + "'," +
                         " tanstate_gid ='" + values.tanstate_gid + "'," +
                         " tanstate_name ='" + values.tanstate_name + "'," +
                         " internalrating_gid ='" + values.internalrating_gid + "'," +
                         " internalrating_name ='" + values.internalrating_name + "'," +
                         " sales ='" + values.sales + "'," +
                         " purchase ='" + values.purchase + "'," +
                         " credit_summation ='" + values.credit_summation + "'," +
                         " cheque_bounce ='" + values.cheque_bounce + "'," +
                         " numberof_boardmeetings ='" + values.numberof_boardmeetings + "'," +
                         " farmer_count ='" + values.farmer_count + "'," +
                         " crop_cycle ='" + values.crop_cycle + "'," +
                         " calamities_prone ='" + values.calamities_prone + "'," +
                         " msme_regi_no ='" + values.msme_regi_no + "'," +
                         " lei_no ='" + values.lei_no + "'," +
                         " kin_no ='" + values.kin_no + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where institution_gid='" + values.institution_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msSQL = " delete from ocs_mst_tinstitution2fpocacity where institution_gid = '" + values.institution_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        for (var i = 0; i < values.fpocity_list.Count; i++)
                        {
                            msinstitution2fpocacity_gid = objcmnfunctions.GetMasterGID("I2FC");
                            msSQL = " Insert into ocs_mst_tinstitution2fpocacity( " +
                                   " institution2fpocacity_gid, " +
                                   " institution_gid, " +
                                   " city_gid," +
                                   " city_name," +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + msinstitution2fpocacity_gid + "'," +
                                   "'" + values.institution_gid + "'," +
                                   "'" + values.fpocity_list[i].city_gid + "'," +
                                   "'" + values.fpocity_list[i].city_name + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }

                    // Updates for Multiple Add
                    msSQL = "update ocs_mst_tinstitution2branch set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update ocs_mst_tinstitution2mobileno set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update ocs_mst_tinstitution2email set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update ocs_mst_tinstitution2address set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update ocs_mst_tinstitution2licensedtl set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update ocs_mst_tinstitution2equipment set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update ocs_mst_tinstitution2livestock set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update ocs_mst_tinstitution2receivable set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "select companydocument_gid,institution2documentupload_gid from ocs_mst_tinstitution2documentupload where institution_gid='" + employee_gid + "'";
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
                        msSQL = " insert into ocs_trn_tdocumentchecktls(" +
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
                            msSQL = " insert into ocs_trn_tcovanantdocumentcheckdtls(" +
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

                    DaMstScannedDocument objvalues = new DaMstScannedDocument();
                    objvalues.DaGroupDocChecklistinfoCredit(lsapplication_gid, values.institution_gid, employee_gid);

                    msSQL = "update ocs_mst_tinstitution2documentupload set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update ocs_mst_tinstitution2form60documentupload set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update ocs_mst_tkycgstsbpan set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update ocs_mst_tkycudyamauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        msSQL = "select stakeholder_type from ocs_mst_tinstitution where institution_gid='" + values.institution_gid + "' ";
                        string lsstakeholders_type = objdbconn.GetExecuteScalar(msSQL);

                        if (lsstakeholders_type == "Applicant" || lsstakeholders_type == "Borrower")
                        {
                            msSQL = "select mobile_no from ocs_mst_tinstitution2mobileno where institution_gid='" + values.institution_gid + "' and primary_status='yes'";
                            string lsmobileno = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = "select email_address from ocs_mst_tinstitution2email where institution_gid='" + values.institution_gid + "' and primary_status='yes'";
                            lsemail_address = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = "select company_name,institution_gid,urn,stakeholder_type from ocs_mst_tinstitution where " +
                                    " application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Applicant','Borrower')";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lscustomer_name = objODBCDatareader["company_name"].ToString();
                                lsurn = objODBCDatareader["urn"].ToString();
                                lsstakeholder_type = objODBCDatareader["stakeholder_type"].ToString();

                                //Region
                                msSQL = "select state from ocs_mst_tinstitution2address where primary_status='Yes' and institution_gid='" + objODBCDatareader["institution_gid"].ToString() + "'";
                                lsregion = objdbconn.GetExecuteScalar(msSQL);

                                //Main Table 
                                msSQL = " update ocs_mst_tapplication set customer_name='" + lscustomer_name.Replace("'", "") + "'," +
                                        " mobile_no='" + lsmobile_no + "'," +
                                        " email_address='" + lsemail_address + "'," +
                                        " region='" + lsregion + "'," +
                                        " customer_urn='" + lsurn + "'," +
                                        " applicant_type='Institution'," +
                                        " updated_by='" + employee_gid + "'," +
                                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                        " where application_gid='" + lsapplication_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                msSQL = " update ocs_mst_tinstitution set mobile_no='" + lsmobileno + "'," +
                                        " email_address='" + lsemail_address + "' where institution_gid='" + values.institution_gid + "' ";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            objODBCDatareader.Close();
                            values.status = true;
                            values.message = "Institution Details Submitted Successfully";
                            return true;
                        }
                        else
                        {

                        }

                        values.status = true;
                        values.message = "Institution Details Submitted Successfully";
                    }                   
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

            msSQL = "select application_gid from ocs_mst_tcontact where contact_gid='" + values.contact_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            if (values.stakeholder_type == "Borrower" || values.stakeholder_type == "Applicant")
            {
                msSQL = "select stakeholder_type from ocs_mst_tcontact where application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Borrower','Applicant')" +
                " and contact_gid<>'" + values.contact_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Already Added Applicant/Borrower Information";
                    return;
                }
                msSQL = "select stakeholder_type from ocs_mst_tinstitution where application_gid='" + lsapplication_gid + "' and " +
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

            msSQL = "select pan_status from ocs_mst_tcontact where contact_gid='" + values.contact_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (values.pan_status == "Customer Submitting PAN")
            {
                msSQL = "delete from ocs_mst_tcontact2panform60 where contact_gid='" + values.contact_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "delete from ocs_mst_tcontact2panabsencereason where contact_gid='" + values.contact_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            objODBCDatareader.Close();

            msSQL = " update ocs_mst_tcontact set " +
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
                   " nearsamunnatiabranch_gid ='" + values.nearsamunnatiabranch_gid + "'," +
                   " nearsamunnatiabranch_name ='" + values.nearsamunnatiabranch_name + "'," +
                   " physicalstatus_gid ='" + values.physicalstatus_gid + "'," +
                   " physicalstatus_name ='" + values.physicalstatus_name + "'," +
                   " internalrating_gid ='" + values.internalrating_gid + "'," +
                   " internalrating_name ='" + values.internalrating_name + "'," +
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

                    msSQL = " select panabsencereason from ocs_mst_tcontact2panabsencereason" +
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
                            msSQL = " INSERT INTO ocs_mst_tcontact2panabsencereason(" +
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
                                msSQL = " INSERT INTO ocs_mst_tcontact2panabsencereason(" +
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
                                msSQL = "delete from ocs_mst_tcontact2panabsencereason where panabsencereason='" + values.contactpanabsencereason_list[i].panabsencereason + "' and contact_gid = '" + values.contact_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            matchCount2 = 0;
                        }
                    }
                }
                //Updates
                msSQL = "update ocs_mst_tcontact2mobileno set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2email set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2address set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2idproof set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2panform60 set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2panabsencereason set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2equipment set contact_gid='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2livestock set contact_gid='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select individualdocument_gid,contact2document_gid from ocs_mst_tcontact2document where contact_gid='" + employee_gid + "'";
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
                    msSQL = " insert into ocs_trn_tdocumentchecktls(" +
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
                        msSQL = " insert into ocs_trn_tcovanantdocumentcheckdtls(" +
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

                DaMstScannedDocument objvalues = new DaMstScannedDocument();
                objvalues.DaGroupDocChecklistinfoCredit(lsapplication_gid, values.contact_gid, employee_gid);

                msSQL = "update ocs_mst_tcontact2document set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycpanauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycpanaadhaarlink set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycdlauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycepicauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycpassportauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2panform60 set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2panabsencereason set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
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

            msSQL = "select stakeholder_type from ocs_mst_tcontact where application_gid='" + values.application_gid + "' and stakeholder_type in ('Borrower','Applicant')";
            string lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholdertype_name)
            {

                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return;
            }

            msSQL = "select stakeholder_type from ocs_mst_tinstitution where application_gid='" + values.application_gid + "' and stakeholder_type in ('Borrower','Applicant')";
            lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholdertype_name)
            {

                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return;
            }
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
                   " physicalstatus_gid," +
                   " physicalstatus_name," +
                   " internalrating_gid," +
                   " internalrating_name," +
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
                      "'" + values.nearsamunnatiabranch_gid + "'," +
                      "'" + values.nearsamunnatiabranch_name + "'," +
                      "'" + values.physicalstatus_gid + "'," +
                      "'" + values.physicalstatus_name + "'," +
                      "'" + values.internalrating_gid + "'," +
                      "'" + values.internalrating_name + "'," +
                      "'" + employee_gid + "'," +
                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                // PAN Update
                foreach (string reason in values.panabsencereason_selectedlist)
                {
                    msGetGidpan = objcmnfunctions.GetMasterGID("C2PR");
                    msSQL = " INSERT INTO ocs_mst_tcontact2panabsencereason(" +
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

                msSQL = "update ocs_mst_tcontact2mobileno set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2email set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2address set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2idproof set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2panform60 set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2panabsencereason set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2equipment set contact_gid='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2livestock set contact_gid='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select individualdocument_gid,contact2document_gid from ocs_mst_tcontact2document where contact_gid='" + employee_gid + "'";
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
                    msSQL = " insert into ocs_trn_tdocumentchecktls(" +
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
                        msSQL = " insert into ocs_trn_tcovanantdocumentcheckdtls(" +
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
                DaMstScannedDocument objvalues = new DaMstScannedDocument();
                objvalues.DaGroupDocChecklistinfoCredit(values.application_gid, msGetGid, employee_gid);

                msSQL = "update ocs_mst_tcontact2document set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycpanauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycpanaadhaarlink set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycdlauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycepicauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycpassportauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                values.status = true;
                values.message = "Individual Details Saved Sucessfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }
        public void DaSubmitIndividualDtlAdd(string employee_gid, string user_gid, MdlMstContact values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("CTCT");

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
                    " from ocs_mst_tcontact2document a where a.documenttype_gid = 'DOCT2022010611'  and " +
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

            msSQL = "select stakeholder_type from ocs_mst_tcontact where application_gid='" + values.application_gid + "' and stakeholder_type in ('Borrower','Applicant')";
            string lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholdertype_name)
            {
                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return;
            }

            msSQL = "select stakeholder_type from ocs_mst_tinstitution where application_gid='" + values.application_gid + "' and stakeholder_type in ('Borrower','Applicant')";
            lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholdertype_name)
            {
                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return;
            }

            msSQL = "select contact2mobileno_gid from ocs_mst_tcontact2mobileno where contact_gid='" + employee_gid + "' and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Mobile Number ";
                return;
            }
            objODBCDatareader.Close();
            msSQL = "select contact2email_gid from ocs_mst_tcontact2email where contact_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Email Address";
                return;
            }
            objODBCDatareader.Close();
            msSQL = "select contact2address_gid from ocs_mst_tcontact2address where contact_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Address";
                return;
            }
            objODBCDatareader.Close();

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
                   " physicalstatus_gid," +
                   " physicalstatus_name," +
                   " internalrating_gid," +
                   " internalrating_name," +
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
                     "'" + values.institution_name.Replace("'", "\\'") + "'," +
                     "'Completed'," +
                     "'" + values.nearsamunnatiabranch_gid + "'," +
                     "'" + values.nearsamunnatiabranch_name + "'," +
                     "'" + values.physicalstatus_gid + "'," +
                     "'" + values.physicalstatus_name + "'," +
                     "'" + values.internalrating_gid + "'," +
                     "'" + values.internalrating_name + "'," +
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
                        msSQL = " INSERT INTO ocs_mst_tcontact2panabsencereason(" +
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

                msSQL = "update ocs_mst_tcontact2mobileno set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2email set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2address set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2idproof set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2equipment set contact_gid='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2livestock set contact_gid='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select contact2document_gid,individualdocument_gid from ocs_mst_tcontact2document where contact_gid='" + employee_gid + "'";
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
                    msSQL = " insert into ocs_trn_tdocumentchecktls(" +
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
                        msSQL = " insert into ocs_trn_tcovanantdocumentcheckdtls(" +
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

                DaMstScannedDocument objvalues = new DaMstScannedDocument();
                objvalues.DaGroupDocChecklistinfoCredit(values.application_gid, msGetGid, employee_gid);

                msSQL = "update ocs_mst_tcontact2document set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycpanauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycpanaadhaarlink set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycdlauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycepicauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycpassportauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select mobile_no from ocs_mst_tcontact2mobileno where contact_gid='" + msGetGid + "' and primary_status='yes'";
                string lsmobileno = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select email_address from ocs_mst_tcontact2email where contact_gid='" + msGetGid + "' and primary_status='yes'";
                lsemail_address = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "update ocs_mst_tcontact2panform60 set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2panabsencereason set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tapplication set updated_by='" + employee_gid + "',updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'  where application_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = "select stakeholder_type from ocs_mst_tcontact where contact_gid='" + msGetGid + "'";
                    string lsstakeholders_type = objdbconn.GetExecuteScalar(msSQL);

                    if (lsstakeholders_type == "Applicant" || lsstakeholders_type == "Borrower")
                    {
                        msSQL = "select mobile_no from ocs_mst_tcontact2mobileno where contact_gid='" + msGetGid + "' and primary_status='yes'";
                        lsmobileno = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select email_address from ocs_mst_tcontact2email where contact_gid='" + msGetGid + "' and primary_status='yes'";
                        lsemail_address = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select concat(first_name,middle_name,last_name) as customer_name,contact_gid,urn,stakeholder_type from ocs_mst_tcontact where" +
                                " application_gid='" + values.application_gid + "' and stakeholder_type in ('Applicant','Borrower')";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lscustomer_name = objODBCDatareader["customer_name"].ToString();
                            lsurn = objODBCDatareader["urn"].ToString();
                            lsstakeholder_type = objODBCDatareader["stakeholder_type"].ToString();

                            //Region
                            msSQL = "select state from ocs_mst_tcontact2address where primary_status='Yes' and contact_gid='" + objODBCDatareader["contact_gid"].ToString() + "'";
                            lsregion = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = " update ocs_mst_tapplication set customer_name='" + lscustomer_name.Replace("'", "") + "'," +
                                    " mobile_no='" + lsmobile_no + "'," +
                                    " email_address='" + lsemail_address + "'," +
                                    " region='" + lsregion + "'," +
                                    " customer_urn='" + lsurn + "'," +
                                    " applicant_type='Individual'," +
                                    " updated_by='" + employee_gid + "'," +
                                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                    " where application_gid='" + values.application_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " update ocs_mst_tcontact set mobile_no='" + lsmobileno + "'," +
                                    " email_address='" + lsemail_address + "' where contact_gid='" + msGetGid + "' ";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }
                        objODBCDatareader.Close();
                        values.status = true;
                        values.message = "Individual Details Submitted Successfully";
                    }
                    else
                    {
                    }
                }

                if (values.lspage == "PendingCADReview")
                {

                    DaMstApplicationEdit objMstApplicationEdit = new DaMstApplicationEdit();
                    objMstApplicationEdit.FnProgramBasedDcoument4otherflows(values.application_gid, employee_gid, user_gid, values.lspage);

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

            msSQL = " update ocs_mst_tapplication set ";
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
                    " from ocs_mst_tcontact2document a where a.documenttype_gid = 'DOCT2022010611' and " +
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

            msSQL = "select application_gid from ocs_mst_tcontact where contact_gid='" + values.contact_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            if (values.stakeholder_type == "Borrower" || values.stakeholder_type == "Applicant")
            {
                msSQL = "select stakeholder_type from ocs_mst_tcontact where application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Borrower','Applicant')" +
                    " and contact_gid<>'" + values.contact_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Already Added Applicant/Borrower Information";
                    return;
                }
                msSQL = "select stakeholder_type from ocs_mst_tinstitution where application_gid='" + lsapplication_gid + "' and " +
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
            msSQL = "select contact2mobileno_gid from ocs_mst_tcontact2mobileno where (contact_gid='" + employee_gid + "' or  contact_gid='" + values.contact_gid + "') and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Mobile Number ";
                return;
            }
            objODBCDatareader.Close();

            msSQL = "select contact2email_gid from ocs_mst_tcontact2email where contact_gid='" + employee_gid + "' or  contact_gid='" + values.contact_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add primary Email Address";
                return;
            }
            objODBCDatareader.Close();
            msSQL = "select contact2address_gid from ocs_mst_tcontact2address where contact_gid='" + employee_gid + "' or  contact_gid='" + values.contact_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add primary Address";
                return;
            }
            objODBCDatareader.Close();

            msSQL = "select pan_status from ocs_mst_tcontact where contact_gid='" + values.contact_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (values.pan_status == "Customer Submitting PAN")
            {
                msSQL = "delete from ocs_mst_tcontact2panform60 where contact_gid='" + values.contact_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "delete from ocs_mst_tcontact2panabsencereason where contact_gid='" + values.contact_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }
            objODBCDatareader.Close();

            msSQL = " update ocs_mst_tcontact set " +
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
                   " institution_name='" + values.institution_name.Replace("'", "\\'") + "'," +
                   " contact_status='Completed'," +
                   " nearsamunnatiabranch_gid='" + values.nearsamunnatiabranch_gid + "'," +
                   " nearsamunnatiabranch_name='" + values.nearsamunnatiabranch_name + "'," +
                   " physicalstatus_gid='" + values.physicalstatus_gid + "'," +
                   " physicalstatus_name='" + values.physicalstatus_name + "'," +
                   " internalrating_gid='" + values.internalrating_gid + "'," +
                   " internalrating_name='" + values.internalrating_name + "'," +
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

                    msSQL = " select panabsencereason from ocs_mst_tcontact2panabsencereason" +
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
                            msSQL = " INSERT INTO ocs_mst_tcontact2panabsencereason(" +
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
                                msSQL = " INSERT INTO ocs_mst_tcontact2panabsencereason(" +
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
                                msSQL = "delete from ocs_mst_tcontact2panabsencereason where panabsencereason='" + values.contactpanabsencereason_list[i].panabsencereason + "' and contact_gid = '" + values.contact_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            matchCount2 = 0;
                        }
                    }
                }
                //Updates
                msSQL = "update ocs_mst_tcontact2mobileno set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2email set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2address set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2idproof set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2panform60 set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2panabsencereason set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2equipment set contact_gid='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2livestock set contact_gid='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select individualdocument_gid,contact2document_gid from ocs_mst_tcontact2document where contact_gid='" + employee_gid + "'";
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
                    msSQL = " insert into ocs_trn_tdocumentchecktls(" +
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
                        msSQL = " insert into ocs_trn_tcovanantdocumentcheckdtls(" +
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

                DaMstScannedDocument objvalues = new DaMstScannedDocument();
                objvalues.DaGroupDocChecklistinfoCredit(lsapplication_gid, values.contact_gid, employee_gid);

                msSQL = "update ocs_mst_tcontact2document set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycpanauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycpanaadhaarlink set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycdlauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycepicauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycpassportauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2panform60 set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tcontact2panabsencereason set contact_gid ='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = "select stakeholder_type from ocs_mst_tcontact where contact_gid='" + values.contact_gid + "'";
                    string lsstakeholders_type = objdbconn.GetExecuteScalar(msSQL);

                    if (lsstakeholders_type == "Applicant" || lsstakeholders_type == "Borrower")
                    {
                        msSQL = "select mobile_no from ocs_mst_tcontact2mobileno where contact_gid='" + values.contact_gid + "' and primary_status='yes'";
                        string lsmobileno = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select email_address from ocs_mst_tcontact2email where contact_gid='" + values.contact_gid + "' and primary_status='yes'";
                        lsemail_address = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select concat(first_name,middle_name,last_name) as customer_name,contact_gid,urn,stakeholder_type from ocs_mst_tcontact where" +
                                " application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Applicant','Borrower')";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lscustomer_name = objODBCDatareader["customer_name"].ToString();
                            lsurn = objODBCDatareader["urn"].ToString();
                            lsstakeholder_type = objODBCDatareader["stakeholder_type"].ToString();

                            //Region
                            msSQL = "select state from ocs_mst_tcontact2address where primary_status='Yes' and contact_gid='" + objODBCDatareader["contact_gid"].ToString() + "'";
                            lsregion = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = " update ocs_mst_tapplication set customer_name='" + lscustomer_name.Replace("'", "\\'") + "'," +
                                    " mobile_no='" + lsmobile_no + "'," +
                                    " email_address='" + lsemail_address + "'," +
                                    " region='" + lsregion + "'," +
                                    " customer_urn='" + lsurn + "'," +
                                    " applicant_type='Individual'," +
                                    " updated_by='" + employee_gid + "'," +
                                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                    " where application_gid='" + lsapplication_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " update ocs_mst_tcontact set mobile_no='" + lsmobileno + "'," +
                                    " email_address='" + lsemail_address + "' where contact_gid='" + values.contact_gid + "' ";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }
                        objODBCDatareader.Close();
                        values.status = true;
                        values.message = "Individual Details Submitted Successfully";
                    }
                    else
                    {
                    }
                }
                values.status = true;
                values.message = "Individual Details Submitted Successfully";
            }

            else
            {
                values.status = false;
                values.message = "Error Occured while submit Individual";
            }
        }

        public void DaCICUploadIndividualDocList(string contact2bureau_gid, string employee_gid, MdlCICIndividual values)
        {
            msSQL = " select individual2cicdocumentupload_gid,contact2bureau_gid,cicdocument_name,cicdocument_path,document_content,migration_flag from ocs_mst_tindividual2cicdocumentupload " +
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
                        migration_flag = dt["migration_flag"].ToString(),
                    });
                    values.cicuploaddoc_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaCICUploadInstitutionDocList(string institution2bureau_gid, string employee_gid, MdlCICInstitution values)
        {
            msSQL = " select institution2cicdocumentupload_gid, institution2bureau_gid,cicdocument_name,cicdocument_path,document_content,migration_flag from ocs_mst_tinstitution2cicdocumentupload " +
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

        public void DaCICUploadIndividualDocDelete(string tmpcicdocument_gid, MdlCICIndividual values)
        {
            msSQL = " delete from ocs_mst_tindividual2cicdocumentupload where individual2cicdocumentupload_gid='" + tmpcicdocument_gid + "'";
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
            msSQL = " delete from ocs_mst_tinstitution2cicdocumentupload where institution2cicdocumentupload_gid='" + tmpcicdocument_gid + "'";
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

                msSQL = "select application_gid,overalllimit_amount,processing_fee,doc_charges, applicant_type, productcharge_flag, economical_flag," +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date,applicant_type,status,productcharges_status,hypothecation_flag, " +
                        " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                        " program_gid,program_name from ocs_mst_tapplication a " +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
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
                    values.program_gid = objODBCDatareader["program_gid"].ToString();
                    values.program_name = objODBCDatareader["program_name"].ToString();
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
                    " margin," +
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
                   "'" + values.facilityloan_amount.Replace(",", "") + "'," +
                   "'" + values.rate_interest + "'," +
                    "'" + values.margin + "'," +
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


            msSQL = " update ocs_mst_tapplication set " +
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
                msSQL = " update ocs_mst_tapplication2loan set application_gid='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //msSQL = " update ocs_mst_tapplication2buyer set application_gid='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_mst_tapplication2collateral set application_gid='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_mst_tapplication2hypothecation set application_gid='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
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



            msSQL = "select application2loan_gid from ocs_mst_tapplication2loan where (application_gid='" + employee_gid + "' or application_gid='" + values.application_gid + "' ) ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Kindly add Loan Details";
                return;
            }
            objODBCDatareader.Close();

            msSQL = "select application2loan_gid from ocs_mst_tapplication2loan where loan_type='Secured' and " +
            "(application_gid='" + employee_gid + "' or application_gid='" + values.application_gid + "' ) ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                msSQL = "select application2collateral_gid from ocs_mst_tapplication2collateral where (application_gid='" + employee_gid + "' or application_gid='" + values.application_gid + "' ) ";
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

            msSQL = " update ocs_mst_tapplication set " +
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
                msSQL = " update ocs_mst_tapplication2loan set application_gid='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //msSQL = " update ocs_mst_tapplication2buyer set application_gid='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_mst_tapplication2collateral set application_gid='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_mst_tapplication2hypothecation set application_gid='" + lsapplication_gid + "' where application_gid='" + employee_gid + "'";
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
            msSQL = " insert into ocs_mst_tapplication2hypothecation(" +
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
                   "'" + values.security_type.Replace("'", "") + "'," +
                   "'" + values.security_description.Replace("'", "") + "',";
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



                msSQL = "update ocs_mst_tuploadhypothecationocument set application2hypothecation_gid='" + msGetGid + "' where application2hypothecation_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



                msSQL = "update ocs_mst_tapplication set hypothecation_flag='Y' where application_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



                msSQL = "select application2hypothecation_gid,securitytype_gid,security_type,security_description,security_value," +
                    " date_format(securityassessed_date,'%d-%m-%Y') as securityassessed_date,asset_id,roc_fillingid,CERSAI_fillingid," +
                    " hypoobservation_summary,primary_security " +
                    " from ocs_mst_tapplication2hypothecation where application_gid='" + values.application_gid + "'";
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
                       " from ocs_mst_tuploadhypothecationocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                       " and b.user_gid = c.user_gid and application2hypothecation_gid='" + employee_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<DocumentList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new DocumentList
                        {
                            document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
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
            msSQL = " insert into ocs_mst_tapplication2collateral(" +
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

                msSQL = "update ocs_mst_tuploadcollateraldocument set application2collateral_gid='" + msGetGid + "' where application2collateral_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select application2collateral_gid,source_type,guideline_value,market_value,forcedsource_value,collateralSSV_value," +
                    " date_format(guideline_date,'%d-%m-%Y') as guideline_date,date_format(forcedvalueassessed_on,'%d-%m-%Y') as forcedvalueassessed_on," +
                    " date_format(marketvalue_date,'%d-%m-%Y') as marketvalue_date,collateralobservation_summary " +
                    " from ocs_mst_tapplication2collateral where application_gid='" + values.application_gid + "'";
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
                      " from ocs_mst_tuploadcollateraldocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                      " and b.user_gid = c.user_gid and application2collateral_gid='" + employee_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<DocumentList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new DocumentList
                        {
                            document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
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
                    " postal_code from ocs_mst_tgroup2address where group_gid='" + group_gid + "'";
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
                    " postal_code from ocs_mst_tgroup2address where group_gid='" + group_gid + "' or group_gid='" + employee_gid + "'";
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
                    " from ocs_mst_tgroup2bank where group_gid='" + group_gid + "'";
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
                    " from ocs_mst_tgroup2bank where group_gid='" + group_gid + "' or group_gid='" + employee_gid + "'";
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
            msSQL = " select group2document_gid,document_name,document_title,document_path from ocs_mst_tgroup2document " +
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
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        group2document_gid = dt["group2document_gid"].ToString(),
                    });
                    values.groupdocument_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGroupDocumentTmpList(string group_gid, string employee_gid, MdlGroupDocument values)
        {
            msSQL = " select group2document_gid,document_name,document_title,document_path from ocs_mst_tgroup2document " +
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
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
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
                msSQL = " select group_name,date_of_formation,group_type,groupmember_count,groupurn_status,group_urn,group_status, " +
                        " male_count,female_count,internalrating_gid,internalrating_name " +
                        " from ocs_mst_tgroup where group_gid='" + group_gid + "'";

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
                    values.male_count = objODBCDatareader["male_count"].ToString();
                    values.female_count = objODBCDatareader["female_count"].ToString();
                    values.internalrating_gid = objODBCDatareader["internalrating_gid"].ToString();
                    values.internalrating_name = objODBCDatareader["internalrating_name"].ToString();
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

            msSQL = " insert into ocs_mst_tgroup(" +
                   " group_gid," +
                   " application_gid," +
                   " group_name," +
                   " date_of_formation," +
                   " group_type," +
                   " groupmember_count," +
                   " groupurn_status," +
                   " group_urn," +
                   " group_status," +
                   " male_count," +
                   " female_count," +
                   " internalrating_gid," +
                   " internalrating_name," +
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
                     "'" + values.male_count + "'," +
                     "'" + values.female_count + "'," +
                     "'" + values.internalrating_gid + "'," +
                     "'" + values.internalrating_name + "'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //}

            //objODBCDatareader.Close();

            if (mnResult != 0)
            {

                //Updates

                msSQL = "update ocs_mst_tgroup2address set group_gid ='" + msGetGid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tgroup2bank set group_gid ='" + msGetGid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tgroup2document set group_gid ='" + msGetGid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tgroup2livestock set group_gid ='" + msGetGid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tgroup2equipment set group_gid ='" + msGetGid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycifscauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycbankaccverification set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                values.status = true;
                values.message = "Group Details Saved Sucessfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while saving Group Details..!";
            }
        }

        public void DaSaveGroupDtlEdit(MdlMstGroup values, string employee_gid)
        {
            msSQL = "select application_gid from ocs_mst_tgroup where group_gid='" + values.group_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " update ocs_mst_tgroup set ";

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
                   " internalrating_gid='" + values.internalrating_gid + "'," +
                   " internalrating_name='" + values.internalrating_name + "'," +
                   " male_count='" + values.male_count + "'," +
                   " female_count='" + values.female_count + "'," +
                   " updated_by='" + employee_gid + "'," +
                   " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                   " where group_gid='" + values.group_gid + "' ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                //Updates
                msSQL = "update ocs_mst_tgroup2address set group_gid ='" + values.group_gid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tgroup2bank set group_gid ='" + values.group_gid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tgroup2livestock set group_gid ='" + values.group_gid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tgroup2equipment set group_gid ='" + values.group_gid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select groupdocument_gid,group2document_gid from ocs_mst_tgroup2document where group_gid='" + employee_gid + "'";
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
                    msSQL = " insert into ocs_trn_tdocumentchecktls(" +
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
                        msSQL = " insert into ocs_trn_tcovanantdocumentcheckdtls(" +
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

                DaMstScannedDocument objvalues = new DaMstScannedDocument();
                objvalues.DaGroupDocChecklistinfoCredit(lsapplication_gid, values.group_gid, employee_gid);

                msSQL = "update ocs_mst_tgroup2document set group_gid ='" + values.group_gid + "' where group_gid='" + employee_gid + "'";
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

            msSQL = "select group2address_gid from ocs_mst_tgroup2address where group_gid='" + employee_gid + "' and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Primary Address ";
                return;
            }

            msSQL = "select group2bank_gid from ocs_mst_tgroup2bank where group_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Atleast one Bank detail ";
                return;
            }

            msSQL = "select group2document_gid from ocs_mst_tgroup2document where group_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Atleast one Document detail ";
                return;
            }

            msSQL = " insert into ocs_mst_tgroup(" +
                   " group_gid," +
                   " application_gid," +
                   " group_name," +
                   " date_of_formation," +
                   " group_type," +
                   " groupmember_count," +
                   " groupurn_status," +
                   " group_urn," +
                   " group_status," +
                   " male_count," +
                   " female_count," +
                   " internalrating_gid," +
                   " internalrating_name," +
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
                     "'" + values.male_count + "'," +
                     "'" + values.female_count + "'," +
                     "'" + values.internalrating_gid + "'," +
                     "'" + values.internalrating_name + "'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                //Updates
                msSQL = "update ocs_mst_tapplication set updated_by='" + employee_gid + "',updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where application_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tgroup2address set group_gid ='" + msGetGid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tgroup2bank set group_gid ='" + msGetGid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tgroup2equipment set group_gid ='" + msGetGid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tgroup2livestock set group_gid ='" + msGetGid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select groupdocument_gid,group2document_gid from ocs_mst_tgroup2document where group_gid='" + employee_gid + "'";
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
                    msSQL = " insert into ocs_trn_tdocumentchecktls(" +
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
                        msSQL = " insert into ocs_trn_tcovanantdocumentcheckdtls(" +
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

                DaMstScannedDocument objvalues = new DaMstScannedDocument();
                objvalues.DaGroupDocChecklistinfoCredit(values.application_gid, msGetGid, employee_gid);

                msSQL = "update ocs_mst_tgroup2document set group_gid ='" + msGetGid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycifscauthentication set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycbankaccverification set function_gid ='" + values.application_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Group Details Submitted Sucessfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while submitting Group Details..!";
            }
        }

        public void DaSubmitGroupDtlEdit(string employee_gid, MdlMstGroup values)
        {
            msSQL = "select application_gid from ocs_mst_tgroup where group_gid='" + values.group_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select group2address_gid from ocs_mst_tgroup2address where (group_gid='" + employee_gid + "' or  group_gid='" + values.group_gid + "')" + " and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Primary Address ";
                return;
            }

            msSQL = "select group2bank_gid from ocs_mst_tgroup2bank where group_gid='" + employee_gid + "' or  group_gid='" + values.group_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Atleast one Bank detail ";
                return;
            }

            msSQL = "select group2document_gid from ocs_mst_tgroup2document where group_gid='" + employee_gid + "' or  group_gid='" + values.group_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Atleast one Document detail ";
                return;
            }

            msSQL = " update ocs_mst_tgroup set ";

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
                   " internalrating_gid='" + values.internalrating_gid + "'," +
                   " internalrating_name='" + values.internalrating_name + "'," +
                   " male_count='" + values.male_count + "'," +
                   " female_count='" + values.female_count + "'," +
                   " updated_by='" + employee_gid + "'," +
                   " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                   " where group_gid='" + values.group_gid + "' ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                //Updates
                msSQL = "update ocs_mst_tgroup2address set group_gid ='" + values.group_gid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tgroup2bank set group_gid ='" + values.group_gid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tgroup2livestock set group_gid ='" + values.group_gid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tgroup2equipment set group_gid ='" + values.group_gid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select groupdocument_gid,group2document_gid from ocs_mst_tgroup2document where group_gid='" + employee_gid + "'";
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
                    msSQL = " insert into ocs_trn_tdocumentchecktls(" +
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
                        msSQL = " insert into ocs_trn_tcovanantdocumentcheckdtls(" +
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

                DaMstScannedDocument objvalues = new DaMstScannedDocument();
                objvalues.DaGroupDocChecklistinfoCredit(lsapplication_gid, values.group_gid, employee_gid);

                msSQL = "update ocs_mst_tgroup2document set group_gid ='" + values.group_gid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycifscauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tkycbankaccverification set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
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
            msSQL = " select a.group_gid,a.application_gid,a.group_name,date_format(a.date_of_formation,'%d-%m-%Y') as date_of_formation,a.group_status, a.group_type," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,credit_status," +
                    " (select count(groupdocumentchecklist_gid) from ocs_trn_tgroupdocumentchecklist where credit_gid =a.group_gid " +
                    " and(untagged_type is null or untagged_type = 'N')) as OverallDeferralcount, " +
                    " (select count(groupcovdocumentchecklist_gid) from ocs_trn_tgroupcovenantdocumentchecklist where credit_gid = a.group_gid " +
                    " and(untagged_type is null or untagged_type = 'N')) as overallCovenantCount, " +
                    " (select count(groupdocumentchecklist_gid) from ocs_trn_tgroupdocumentchecklist where credit_gid = a.group_gid " +
                    " and(untagged_type is null or untagged_type = 'N') and overall_docstatus in ('Waived','Document Verified')) as verifieddeferraldoc, " +
                    " (select count(groupcovdocumentchecklist_gid) from ocs_trn_tgroupcovenantdocumentchecklist where credit_gid = a.group_gid " +
                    " and(untagged_type is null or untagged_type = 'N') and overall_docstatus in ('Waived','Document Verified'))  as verifiedcovenantdoc, d.vertical_gid, d.vertical_name, e.bre_status " +
                    " from ocs_mst_tgroup a " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " left join ocs_mst_tapplication d on d.application_gid = a.application_gid" +
                    " left join ocs_trn_tverticalscorecardgrouptitle e on e.editruletype_gid = a.group_gid" +
                    " where a.application_gid='" + application_gid + "' group by a.group_gid";
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
                        vertical_gid = (dr_datarow["vertical_gid"].ToString()),
                        vertical_name = (dr_datarow["vertical_name"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        bre_status = (dr_datarow["bre_status"].ToString()),

                    });
                }
            }
            values.group_list = getgroup_list;
            dt_datatable.Dispose();
        }

        public void DaUpdateGroupDtl(string employee_gid, MdlMstGroup values)
        {


            msSQL = "select group2address_gid from ocs_mst_tgroup2address where (group_gid='" + employee_gid + "' or  group_gid='" + values.group_gid + "')" + " and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Primary Address ";
                return;
            }

            msSQL = "select group2bank_gid from ocs_mst_tgroup2bank where group_gid='" + employee_gid + "' or  group_gid='" + values.group_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Atleast one Bank detail ";
                return;
            }

            msSQL = "select group2document_gid from ocs_mst_tgroup2document where group_gid='" + employee_gid + "' or  group_gid='" + values.group_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Atleast one Document detail ";
                return;
            }

            msSQL = " update ocs_mst_tgroup set ";

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
                   " internalrating_gid='" + values.internalrating_gid + "'," +
                   " internalrating_name='" + values.internalrating_name + "'," +
                   " male_count='" + values.male_count + "'," +
                   " female_count='" + values.female_count + "'," +
                   " updated_by='" + employee_gid + "'," +
                   " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                   " where group_gid='" + values.group_gid + "' ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                //Updates
                msSQL = "update ocs_mst_tgroup2address set group_gid ='" + values.group_gid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tgroup2bank set group_gid ='" + values.group_gid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select application_gid from ocs_mst_tgroup where group_gid = '" + values.group_gid + "' ";
                string lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "update ocs_mst_tgroup2livestock set group_gid ='" + values.group_gid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tgroup2equipment set group_gid ='" + values.group_gid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select groupdocument_gid,group2document_gid from ocs_mst_tgroup2document where group_gid='" + employee_gid + "'";
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
                    msSQL = " insert into ocs_trn_tdocumentchecktls(" +
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
                        msSQL = " insert into ocs_trn_tcovanantdocumentcheckdtls(" +
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
                DaMstScannedDocument objvalues = new DaMstScannedDocument();
                objvalues.DaGroupDocChecklistinfoCredit(lsapplication_gid, values.group_gid, employee_gid);

                msSQL = "update ocs_mst_tgroup2document set group_gid ='" + values.group_gid + "' where group_gid='" + employee_gid + "'";
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
            msSQL = "select contact2panform60_gid,document_name, document_path from ocs_mst_tcontact2panform60 where " +
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
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                    });

                    values.contactpanform60_list = getcontactpanform60_list;
                }
                dt_datatable.Dispose();
            }
        }

        public void DaGetPANForm60List(string contact_gid, string employee_gid, MdlContactPANForm60 values)
        {
            msSQL = "select contact2panform60_gid,document_name, document_path from ocs_mst_tcontact2panform60 where " +
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
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
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
                   " from ocs_mst_tcontact2panabsencereason where contact_gid = '" + contact_gid + "' or contact_gid = '" + employee_gid + "'";

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

                msSQL = " select panabsencereason from ocs_mst_tcontact2panabsencereason" +
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
                        msSQL = " INSERT INTO ocs_mst_tcontact2panabsencereason(" +
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
                        msSQL = "delete from ocs_mst_tcontact2panabsencereason where panabsencereason='" + values.contactpanabsencereason_list[i].panabsencereason + "' and contact_gid = '" + values.contact_gid + "'";
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
                   " from ocs_mst_tpanabsencereason";

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
                   " from ocs_mst_tcontact2panabsencereason where contact_gid = '" + contact_gid + "'";

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
                        if (values.panabsencereason_list[i].panabsencereason == panabsencereason_contactlist[i].panabsencereason)
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
            msSQL = " select application2product_gid,product_gid,product_name,variety_gid,variety_name,sector_name,category_name," +
                    " botanical_name,alternative_name from ocs_mst_tapplication2product where application_gid='" + application_gid + "'";
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
                        alternative_name = (dr_datarow["alternative_name"].ToString())
                    });
                }
                values.mstproduct_list = getmstproduct_list;
            }
            dt_datatable.Dispose();
        }

        public void DaDeleteAppProductDtl(string application2product_gid, MdlMstProductDetailAdd values)
        {
            msSQL = "delete from ocs_mst_tapplication2product where application2product_gid='" + application2product_gid + "'";
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
            msSQL = " select application2product_gid,product_gid,product_name,variety_gid,variety_name,sector_name,category_name," +
                    " botanical_name,alternative_name from ocs_mst_tapplication2product " +
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
                        alternative_name = (dr_datarow["alternative_name"].ToString())
                    });
                }
                values.mstproduct_list = getmstproduct_list;
            }
            dt_datatable.Dispose();
        }

        public void DaUpdateGSTHeadOffice(string employee_gid, MdlGSTHeadOffice values)
        {
            msSQL = " update ocs_mst_tinstitution2branch set headoffice_status = 'Yes' " +
                    " where institution2branch_gid = '" + values.institution2branch_gid + "' " +
                    " and (institution_gid = '" + employee_gid + "' or institution_gid = '" + values.institution_gid + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = " update ocs_mst_tinstitution2branch set headoffice_status='No' " +
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

        // Get Institution Equipment Holding
        public void DaGetInstitutionEquipmentHoldingList(string employee_gid, string institution_gid, MdlMstEquipmentHolding values)
        {
            msSQL = " select institution2equipment_gid,institution_gid,equipment_gid,equipment_name,availablerenthire, " +
                    " quantity,description,insurance_status,insurance_details from ocs_mst_tinstitution2equipment where " +
                    " institution_gid='" + institution_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstequipmentholding_list = new List<mstequipmentholding_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstequipmentholding_list.Add(new mstequipmentholding_list
                    {
                        institution2equipment_gid = (dr_datarow["institution2equipment_gid"].ToString()),
                        institution_gid = (dr_datarow["institution_gid"].ToString()),
                        equipment_gid = (dr_datarow["equipment_gid"].ToString()),
                        equipment_name = (dr_datarow["equipment_name"].ToString()),
                        availablerenthire = (dr_datarow["availablerenthire"].ToString()),
                        quantity = (dr_datarow["quantity"].ToString()),
                        description = (dr_datarow["description"].ToString()),
                        insurance_status = (dr_datarow["insurance_status"].ToString()),
                        insurance_details = (dr_datarow["insurance_details"].ToString()),
                    });
                }
                values.mstequipmentholding_list = getmstequipmentholding_list;
            }
            dt_datatable.Dispose();
        }

        // Get Institution Livestock Holding
        public void DaGetInstitutionLivestockList(string employee_gid, string institution_gid, MdlMstLivestock values)
        {
            msSQL = " select institution2livestock_gid,institution_gid,livestock_gid,livestock_name,count,Breed, " +
                    " insurance_status,insurance_details from ocs_mst_tinstitution2livestock where " +
                    " institution_gid='" + institution_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstlivestockholding_list = new List<mstlivestockholding_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstlivestockholding_list.Add(new mstlivestockholding_list
                    {
                        institution2livestock_gid = (dr_datarow["institution2livestock_gid"].ToString()),
                        institution_gid = (dr_datarow["institution_gid"].ToString()),
                        livestock_gid = (dr_datarow["livestock_gid"].ToString()),
                        livestock_name = (dr_datarow["livestock_name"].ToString()),
                        count = (dr_datarow["count"].ToString()),
                        Breed = (dr_datarow["Breed"].ToString()),
                        insurance_status = (dr_datarow["insurance_status"].ToString()),
                        insurance_details = (dr_datarow["insurance_details"].ToString()),
                    });
                }
                values.mstlivestockholding_list = getmstlivestockholding_list;
            }
            dt_datatable.Dispose();
        }

        // Get Institution Equipment Holding
        public void DaGetEditInstitutionEquipmentHoldingList(string employee_gid, string institution_gid, MdlMstEquipmentHolding values)
        {
            msSQL = " select institution2equipment_gid,institution_gid,equipment_gid,equipment_name,availablerenthire, " +
                    " quantity,description,insurance_status,insurance_details from ocs_mst_tinstitution2equipment where " +
                    " institution_gid='" + institution_gid + "' or institution_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstequipmentholding_list = new List<mstequipmentholding_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstequipmentholding_list.Add(new mstequipmentholding_list
                    {
                        institution2equipment_gid = (dr_datarow["institution2equipment_gid"].ToString()),
                        institution_gid = (dr_datarow["institution_gid"].ToString()),
                        equipment_gid = (dr_datarow["equipment_gid"].ToString()),
                        equipment_name = (dr_datarow["equipment_name"].ToString()),
                        availablerenthire = (dr_datarow["availablerenthire"].ToString()),
                        quantity = (dr_datarow["quantity"].ToString()),
                        description = (dr_datarow["description"].ToString()),
                        insurance_status = (dr_datarow["insurance_status"].ToString()),
                        insurance_details = (dr_datarow["insurance_details"].ToString()),
                    });
                }
                values.mstequipmentholding_list = getmstequipmentholding_list;
            }
            dt_datatable.Dispose();
        }

        // Get Edit Institution Livestock Holding
        public void DaGetEditInstitutionLivestockList(string employee_gid, string institution_gid, MdlMstLivestock values)
        {
            msSQL = " select institution2livestock_gid,institution_gid,livestock_gid,livestock_name,count,Breed, " +
                    " insurance_status,insurance_details from ocs_mst_tinstitution2livestock where " +
                    " institution_gid='" + institution_gid + "' or institution_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstlivestockholding_list = new List<mstlivestockholding_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstlivestockholding_list.Add(new mstlivestockholding_list
                    {
                        institution2livestock_gid = (dr_datarow["institution2livestock_gid"].ToString()),
                        institution_gid = (dr_datarow["institution_gid"].ToString()),
                        livestock_gid = (dr_datarow["livestock_gid"].ToString()),
                        livestock_name = (dr_datarow["livestock_name"].ToString()),
                        count = (dr_datarow["count"].ToString()),
                        Breed = (dr_datarow["Breed"].ToString()),
                        insurance_status = (dr_datarow["insurance_status"].ToString()),
                        insurance_details = (dr_datarow["insurance_details"].ToString()),
                    });
                }
                values.mstlivestockholding_list = getmstlivestockholding_list;
            }
            dt_datatable.Dispose();
        }

        // Get Contact Equipment Holding
        public void DaGetContactEquipmentHoldingList(string employee_gid, string contact_gid, MdlMstEquipmentHolding values)
        {
            msSQL = " select contact2equipment_gid,contact_gid,equipment_gid,equipment_name,availablerenthire, " +
                    " quantity,description,insurance_status,insurance_details from ocs_mst_tcontact2equipment where " +
                    " contact_gid='" + contact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstequipmentholding_list = new List<mstequipmentholding_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstequipmentholding_list.Add(new mstequipmentholding_list
                    {
                        contact2equipment_gid = (dr_datarow["contact2equipment_gid"].ToString()),
                        contact_gid = (dr_datarow["contact_gid"].ToString()),
                        equipment_gid = (dr_datarow["equipment_gid"].ToString()),
                        equipment_name = (dr_datarow["equipment_name"].ToString()),
                        availablerenthire = (dr_datarow["availablerenthire"].ToString()),
                        quantity = (dr_datarow["quantity"].ToString()),
                        description = (dr_datarow["description"].ToString()),
                        insurance_status = (dr_datarow["insurance_status"].ToString()),
                        insurance_details = (dr_datarow["insurance_details"].ToString()),
                    });
                }
                values.mstequipmentholding_list = getmstequipmentholding_list;
            }
            dt_datatable.Dispose();
        }

        // Get Contact Equipment Holding
        public void DaGetEditContactEquipmentHoldingList(string employee_gid, string contact_gid, MdlMstEquipmentHolding values)
        {
            msSQL = " select contact2equipment_gid,contact_gid,equipment_gid,equipment_name,availablerenthire, " +
                    " quantity,description,insurance_status,insurance_details from ocs_mst_tcontact2equipment where " +
                    " contact_gid='" + contact_gid + "' or contact_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstequipmentholding_list = new List<mstequipmentholding_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstequipmentholding_list.Add(new mstequipmentholding_list
                    {
                        contact2equipment_gid = (dr_datarow["contact2equipment_gid"].ToString()),
                        contact_gid = (dr_datarow["contact_gid"].ToString()),
                        equipment_gid = (dr_datarow["equipment_gid"].ToString()),
                        equipment_name = (dr_datarow["equipment_name"].ToString()),
                        availablerenthire = (dr_datarow["availablerenthire"].ToString()),
                        quantity = (dr_datarow["quantity"].ToString()),
                        description = (dr_datarow["description"].ToString()),
                        insurance_status = (dr_datarow["insurance_status"].ToString()),
                        insurance_details = (dr_datarow["insurance_details"].ToString()),
                    });
                }
                values.mstequipmentholding_list = getmstequipmentholding_list;
            }
            dt_datatable.Dispose();
        }

        // Get Contact Livestock Holding
        public void DaGetContactLivestockList(string employee_gid, string contact_gid, MdlMstLivestock values)
        {
            msSQL = " select contact2livestock_gid,contact_gid,livestock_gid,livestock_name,count,Breed, " +
                    " insurance_status,insurance_details from ocs_mst_tcontact2livestock where " +
                    " contact_gid='" + contact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstlivestockholding_list = new List<mstlivestockholding_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstlivestockholding_list.Add(new mstlivestockholding_list
                    {
                        contact2livestock_gid = (dr_datarow["contact2livestock_gid"].ToString()),
                        contact_gid = (dr_datarow["contact_gid"].ToString()),
                        livestock_gid = (dr_datarow["livestock_gid"].ToString()),
                        livestock_name = (dr_datarow["livestock_name"].ToString()),
                        count = (dr_datarow["count"].ToString()),
                        Breed = (dr_datarow["Breed"].ToString()),
                        insurance_status = (dr_datarow["insurance_status"].ToString()),
                        insurance_details = (dr_datarow["insurance_details"].ToString()),
                    });
                }
                values.mstlivestockholding_list = getmstlivestockholding_list;
            }
            dt_datatable.Dispose();
        }

        // Get Edit Contact Livestock Holding
        public void DaGetEditContactLivestockList(string employee_gid, string contact_gid, MdlMstLivestock values)
        {
            msSQL = " select contact2livestock_gid,contact_gid,livestock_gid,livestock_name,count,Breed, " +
                    " insurance_status,insurance_details from ocs_mst_tcontact2livestock where " +
                    " contact_gid='" + contact_gid + "' or contact_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstlivestockholding_list = new List<mstlivestockholding_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstlivestockholding_list.Add(new mstlivestockholding_list
                    {
                        contact2livestock_gid = (dr_datarow["contact2livestock_gid"].ToString()),
                        contact_gid = (dr_datarow["contact_gid"].ToString()),
                        livestock_gid = (dr_datarow["livestock_gid"].ToString()),
                        livestock_name = (dr_datarow["livestock_name"].ToString()),
                        count = (dr_datarow["count"].ToString()),
                        Breed = (dr_datarow["Breed"].ToString()),
                        insurance_status = (dr_datarow["insurance_status"].ToString()),
                        insurance_details = (dr_datarow["insurance_details"].ToString()),
                    });
                }
                values.mstlivestockholding_list = getmstlivestockholding_list;
            }
            dt_datatable.Dispose();
        }

        // Get Group Equipment Holding
        public void DaGetGroupEquipmentHoldingList(string employee_gid, string group_gid, MdlMstEquipmentHolding values)
        {
            msSQL = " select group2equipment_gid,group_gid,equipment_gid,equipment_name,availablerenthire, " +
                    " quantity,description,insurance_status,insurance_details from ocs_mst_tgroup2equipment where " +
                    " group_gid='" + group_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstequipmentholding_list = new List<mstequipmentholding_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstequipmentholding_list.Add(new mstequipmentholding_list
                    {
                        group2equipment_gid = (dr_datarow["group2equipment_gid"].ToString()),
                        group_gid = (dr_datarow["group_gid"].ToString()),
                        equipment_gid = (dr_datarow["equipment_gid"].ToString()),
                        equipment_name = (dr_datarow["equipment_name"].ToString()),
                        availablerenthire = (dr_datarow["availablerenthire"].ToString()),
                        quantity = (dr_datarow["quantity"].ToString()),
                        description = (dr_datarow["description"].ToString()),
                        insurance_status = (dr_datarow["insurance_status"].ToString()),
                        insurance_details = (dr_datarow["insurance_details"].ToString()),
                    });
                }
                values.mstequipmentholding_list = getmstequipmentholding_list;
            }
            dt_datatable.Dispose();
        }

        // GetEdit Group Equipment Holding
        public void DaGetEditGroupEquipmentHoldingList(string employee_gid, string group_gid, MdlMstEquipmentHolding values)
        {
            msSQL = " select group2equipment_gid,group_gid,equipment_gid,equipment_name,availablerenthire, " +
                    " quantity,description,insurance_status,insurance_details from ocs_mst_tgroup2equipment where " +
                    " group_gid='" + group_gid + "' or group_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstequipmentholding_list = new List<mstequipmentholding_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstequipmentholding_list.Add(new mstequipmentholding_list
                    {
                        group2equipment_gid = (dr_datarow["group2equipment_gid"].ToString()),
                        group_gid = (dr_datarow["group_gid"].ToString()),
                        equipment_gid = (dr_datarow["equipment_gid"].ToString()),
                        equipment_name = (dr_datarow["equipment_name"].ToString()),
                        availablerenthire = (dr_datarow["availablerenthire"].ToString()),
                        quantity = (dr_datarow["quantity"].ToString()),
                        description = (dr_datarow["description"].ToString()),
                        insurance_status = (dr_datarow["insurance_status"].ToString()),
                        insurance_details = (dr_datarow["insurance_details"].ToString()),
                    });
                }
                values.mstequipmentholding_list = getmstequipmentholding_list;
            }
            dt_datatable.Dispose();
        }

        // Get Group Livestock Holding
        public void DaGetGroupLivestockList(string employee_gid, string group_gid, MdlMstLivestock values)
        {
            msSQL = " select group2livestock_gid,group_gid,livestock_gid,livestock_name,count,Breed, " +
                    " insurance_status,insurance_details from ocs_mst_tgroup2livestock where " +
                    " group_gid='" + group_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstlivestockholding_list = new List<mstlivestockholding_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstlivestockholding_list.Add(new mstlivestockholding_list
                    {
                        group2livestock_gid = (dr_datarow["group2livestock_gid"].ToString()),
                        group_gid = (dr_datarow["group_gid"].ToString()),
                        livestock_gid = (dr_datarow["livestock_gid"].ToString()),
                        livestock_name = (dr_datarow["livestock_name"].ToString()),
                        count = (dr_datarow["count"].ToString()),
                        Breed = (dr_datarow["Breed"].ToString()),
                        insurance_status = (dr_datarow["insurance_status"].ToString()),
                        insurance_details = (dr_datarow["insurance_details"].ToString()),
                    });
                }
                values.mstlivestockholding_list = getmstlivestockholding_list;
            }
            dt_datatable.Dispose();
        }

        // Get Edit Group Livestock Holding
        public void DaGetEditGroupLivestockList(string employee_gid, string group_gid, MdlMstLivestock values)
        {
            msSQL = " select group2livestock_gid,group_gid,livestock_gid,livestock_name,count,Breed, " +
                    " insurance_status,insurance_details from ocs_mst_tgroup2livestock where " +
                    " group_gid='" + group_gid + "' or group_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstlivestockholding_list = new List<mstlivestockholding_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstlivestockholding_list.Add(new mstlivestockholding_list
                    {
                        group2livestock_gid = (dr_datarow["group2livestock_gid"].ToString()),
                        group_gid = (dr_datarow["group_gid"].ToString()),
                        livestock_gid = (dr_datarow["livestock_gid"].ToString()),
                        livestock_name = (dr_datarow["livestock_name"].ToString()),
                        count = (dr_datarow["count"].ToString()),
                        Breed = (dr_datarow["Breed"].ToString()),
                        insurance_status = (dr_datarow["insurance_status"].ToString()),
                        insurance_details = (dr_datarow["insurance_details"].ToString()),
                    });
                }
                values.mstlivestockholding_list = getmstlivestockholding_list;
            }
            dt_datatable.Dispose();
        }

        // Get Edit Institution Receivable List
        public void DaGetEditInstitutionReceivableList(string employee_gid, string institution_gid, MdlMstReceivable values)
        {
            msSQL = " select institution2receivable_gid,institution_gid,date_format(receivable_date, '%d-%m-%Y') as receivable_date, " +
                    " onetothirty_days,thirtyonetosixty_days,sixtyonetoninety_days,ninety_days from ocs_mst_tinstitution2receivable " +
                    " where institution_gid='" + institution_gid + "' or institution_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstreceivable_list = new List<mstreceivable_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstreceivable_list.Add(new mstreceivable_list
                    {
                        institution2receivable_gid = (dr_datarow["institution2receivable_gid"].ToString()),
                        institution_gid = (dr_datarow["institution_gid"].ToString()),
                        receivable_date = (dr_datarow["receivable_date"].ToString()),
                        onetothirty_days = (dr_datarow["onetothirty_days"].ToString()),
                        thirtyonetosixty_days = (dr_datarow["thirtyonetosixty_days"].ToString()),
                        sixtyonetoninety_days = (dr_datarow["sixtyonetoninety_days"].ToString()),
                        ninety_days = (dr_datarow["ninety_days"].ToString())
                    });
                }
                values.mstreceivable_list = getmstreceivable_list;
            }
            dt_datatable.Dispose();
        }

        // Get Institution Receivable List
        public void DaGetInstitutionReceivableList(string employee_gid, string institution_gid, MdlMstReceivable values)
        {
            msSQL = " select institution2receivable_gid,institution_gid,date_format(receivable_date, '%d-%m-%Y') as receivable_date, " +
                    " onetothirty_days,thirtyonetosixty_days,sixtyonetoninety_days,ninety_days from ocs_mst_tinstitution2receivable " +
                    " where institution_gid='" + institution_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstreceivable_list = new List<mstreceivable_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstreceivable_list.Add(new mstreceivable_list
                    {
                        institution2receivable_gid = (dr_datarow["institution2receivable_gid"].ToString()),
                        institution_gid = (dr_datarow["institution_gid"].ToString()),
                        receivable_date = (dr_datarow["receivable_date"].ToString()),
                        onetothirty_days = (dr_datarow["onetothirty_days"].ToString()),
                        thirtyonetosixty_days = (dr_datarow["thirtyonetosixty_days"].ToString()),
                        sixtyonetoninety_days = (dr_datarow["sixtyonetoninety_days"].ToString()),
                        ninety_days = (dr_datarow["ninety_days"].ToString())
                    });
                }
                values.mstreceivable_list = getmstreceivable_list;
            }
            dt_datatable.Dispose();
        }
        public void DaDeleteinstitution(string institution_gid, string application_gid, string employee_gid, MdlCICInstitution values)
        {

            msSQL = "Insert into ocs_mst_tinstitutionlog" +
                    " (deleted_by,deleted_date,institution_gid, application_gid, application_no, company_name, date_incorporation, " +
                    " form60document_name, form60document_path, companypan_no, year_business, month_business, " +
                    " cin_no, official_telephoneno, officialemail_address, companytype_gid, companytype_name, " +
                    " stakeholder_type, stakeholdertype_gid, assessmentagency_gid, assessmentagency_name, assessmentagencyrating_gid,  " +
                    " assessmentagencyrating_name, ratingas_on, amlcategory_gid, amlcategory_name, businesscategory_gid, businesscategory_name, " +
                    " contactperson_firstname, contactperson_middlename, contactperson_lastname, contactperson_contactno, designation_gid, " +
                    " designation, lastyear_turnover, escrow, start_date, end_date, created_by, created_date, updated_date, updated_by, urn, " +
                    " urn_status, observations, bureau_response, institution_status, businessstart_date, bureauname_name, bureau_score, bureauname_gid, " +
                    " bureauscore_date, cicdocument_name, cicdocument_path, mobile_no, email_address, economical_status, social_capital, trade_capital, " +
                    " official_mailid, economical_flag, startupasofloansanction_date, occupation_gid, occupation, lineofactivity_gid, lineofactivity, " +
                    " bsrcode_gid, bsrcode, pslcategory_gid, pslcategory, weakersection_gid, weakersection, pslpurpose_gid, pslpurpose, " +
                    " totalsanction_financialinstitution, pslsanction_limit, natureofentity_gid, natureofentity, indulgeinmarketing_activity, " +
                    " plantandmachineryinvestment_gid, plantandmachineryinvestment, turnover_gid, turnover, msmeclassification_gid, msmeclassification, " +
                    " loansanction_date, entityincorporation_date, hq_metropolitancity, clientdtl_gid, client_dtl, psltagging_flag, credit_status, " +
                    " nearsamunnatiabranch_gid, nearsamunnatiabranch_name, udhayam_registration, tan_number, business_description, tanstate_gid, " +
                    " tanstate_name, internalrating_gid, internalrating_name, sales, purchase, credit_summation, cheque_bounce, numberof_boardmeetings, " +
                    " farmer_count, crop_cycle, calamities_prone, lei_no, renewaldue_date, cin_date, kin_no,  msme_regi_no) " +
                " select " +
                " @deleted_by := '" + employee_gid + "',  @deleted_date := current_timestamp,institution_gid, application_gid, application_no, company_name, date_incorporation, " +
                " form60document_name, form60document_path, companypan_no, year_business, month_business, " +
                " cin_no, official_telephoneno, officialemail_address, companytype_gid, companytype_name, " +
                " stakeholder_type, stakeholdertype_gid, assessmentagency_gid, assessmentagency_name, assessmentagencyrating_gid,  " +
                " assessmentagencyrating_name, ratingas_on, amlcategory_gid, amlcategory_name, businesscategory_gid, businesscategory_name, " +
                " contactperson_firstname, contactperson_middlename, contactperson_lastname, contactperson_contactno, designation_gid, " +
                " designation, lastyear_turnover, escrow, start_date, end_date, created_by, created_date, updated_date, updated_by, urn, " +
                " urn_status, observations, bureau_response, institution_status, businessstart_date, bureauname_name, bureau_score, bureauname_gid, " +
                " bureauscore_date, cicdocument_name, cicdocument_path, mobile_no, email_address, economical_status, social_capital, trade_capital, " +
                " official_mailid, economical_flag, startupasofloansanction_date, occupation_gid, occupation, lineofactivity_gid, lineofactivity, " +
                " bsrcode_gid, bsrcode, pslcategory_gid, pslcategory, weakersection_gid, weakersection, pslpurpose_gid, pslpurpose, " +
                " totalsanction_financialinstitution, pslsanction_limit, natureofentity_gid, natureofentity, indulgeinmarketing_activity, " +
                " plantandmachineryinvestment_gid, plantandmachineryinvestment, turnover_gid, turnover, msmeclassification_gid, msmeclassification, " +
                " loansanction_date, entityincorporation_date, hq_metropolitancity, clientdtl_gid, client_dtl, psltagging_flag, credit_status, " +
                " nearsamunnatiabranch_gid, nearsamunnatiabranch_name, udhayam_registration, tan_number, business_description, tanstate_gid, " +
                " tanstate_name, internalrating_gid, internalrating_name, sales, purchase, credit_summation, cheque_bounce, numberof_boardmeetings, " +
                " farmer_count, crop_cycle, calamities_prone, lei_no, renewaldue_date, cin_date, kin_no,  msme_regi_no " +
                "from ocs_mst_tinstitution where institution_gid='" + institution_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "Delete from ocs_mst_tinstitution where institution_gid='" + institution_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {

                msSQL = " select institution_gid,company_name,date_incorporation,stakeholder_type,institution_status," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                        " from ocs_mst_tinstitution a " +
                        " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                        " where a.application_gid='" + application_gid + "' order by institution_gid desc ";
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
                            date_incorporation = dt["date_incorporation"].ToString(),
                            stakeholder_type = dt["stakeholder_type"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            institution_status = dt["institution_status"].ToString(),
                        });

                    }
                }
                values.cicinstitution_list = getcicinstitutionList;
                dt_datatable.Dispose();
                values.message = "Company Information Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while Deleting";
                values.status = false;
            }

        }

        public void FnProgramBasedDcoument4otherflows(string application_gid, string employee_gid, string user_gid, string toBeSearched)
        {

            msSQL = " select institution_gid from ocs_mst_tinstitution where application_gid = '" + application_gid + "' ";

            if (toBeSearched == "PendingCADReview")
            {
                 taggedby = "CAD";

            }

            else { 
                 taggedby = "Credit"; 
            }

            msSQL = "select concat(user_firstname,' ', user_lastname, ' / ', user_code) as username from adm_mst_tuser where user_gid='" + user_gid + "'";
            string tagged_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select institution_gid from ocs_mst_tinstitution where application_gid = '" + application_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {

                foreach (DataRow dt in dt_datatable.Rows)
                {

                    string program_gid = ""; string lscompanydocument_gid = "";

                    msSQL = " select program_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "' ";

                    program_gid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select GROUP_CONCAT('\\\'',companydocument_gid,'\\\'')  from ocs_trn_tdocumentchecktls where  credit_gid = '" + dt["institution_gid"] + "' and application_gid = '" + application_gid + "' ";
                    lscompanydocument_gid = objdbconn.GetExecuteScalar(msSQL);

                    if (!string.IsNullOrEmpty(lscompanydocument_gid))
                    {

                        msSQL = " select a.companydocument_gid,a.documenttypes_gid,a.documenttype_name,a.companydocument_name,a.covenant_type " +
                     " from ocs_mst_tcompanydocument a " +
                     " left join ocs_mst_tcompanydocumentprogram b on a.companydocument_gid = b.companydocument_gid " +
                     " where a.status='Y' and b.program_gid = '" + program_gid + "' and a.companydocument_gid not in (" + lscompanydocument_gid + ") ";

                    }

                    else
                    {
                        msSQL = " select a.companydocument_gid,a.documenttypes_gid,a.documenttype_name,a.companydocument_name,a.covenant_type " +
                     " from ocs_mst_tcompanydocument a " +
                     " left join ocs_mst_tcompanydocumentprogram b on a.companydocument_gid = b.companydocument_gid " +
                     " where a.status='Y' and b.program_gid = '" + program_gid + "'  ";
                    }

                    dt_datatable1 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable1.Rows.Count != 0)
                    {
                        foreach (DataRow dt1 in dt_datatable1.Rows)
                        {
                            msGetGid = objcmnfunctions.GetMasterGID("DOCG");
                            msSQL = " insert into ocs_trn_tdocumentchecktls(" +
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
                                " created_by)" +
                                " VALUES(" +
                                "'" + msGetGid + "'," +
                                "'" + application_gid + "'," +
                                "'" + dt["institution_gid"] + "'," +
                                "'" + dt1["companydocument_gid"] + "'," +
                             "'" + dt1["documenttypes_gid"] + "'," +
                                "'" + dt1["documenttype_name"] + "'," +
                                "'" + dt1["companydocument_name"] + "'," +
                                "'" + dt1["covenant_type"] + "'," +
                                "'" + taggedby + "'," +
                                "'" + tagged_name + "'," +
                                "current_timestamp," +
                                "'" + user_gid + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                            string msGetgroupDocGID = objcmnfunctions.GetMasterGID("GDCG");
                            msSQL = " insert into ocs_trn_tgroupdocumentchecklist(" +
                                      " groupdocumentchecklist_gid," +
                                      " application_gid," +
                                      " credit_gid, " +
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
                                  "'" + dt["institution_gid"] + "'," +
                                  "'" + dt1["companydocument_gid"] + "'," +
                                  "'" + dt1["companydocument_name"].ToString() + "'," +
                                  "'" + dt1["covenant_type"].ToString() + "'," +
                                  "'" + dt1["documenttypes_gid"].ToString() + "'," +
                                  "'" + dt1["documenttype_name"].ToString() + "'," +
                                  "'" + taggedby + "'," +
                                  "current_timestamp," +
                                  "'" + employee_gid + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                            msSQL = " update ocs_trn_tdocumentchecktls set groupdocumentchecklist_gid ='" + msGetgroupDocGID + "'" +
                                   " where documentcheckdtl_gid ='" + msGetGid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }



                    }

                    string lscovenantcompanydocument_gid = "";

                    msSQL = " select GROUP_CONCAT('\\\'',companydocument_gid,'\\\'')  from ocs_trn_tcovanantdocumentcheckdtls where  covenant_type = 'Y' and credit_gid = '" + dt["institution_gid"] + "' and application_gid = '" + application_gid + "' ";
                    lscovenantcompanydocument_gid = objdbconn.GetExecuteScalar(msSQL);

                    if (!string.IsNullOrEmpty(lscovenantcompanydocument_gid))
                    {

                        msSQL = " select a.companydocument_gid,a.documenttypes_gid,a.documenttype_name,a.companydocument_name,a.covenant_type " +
                     " from ocs_mst_tcompanydocument a " +
                     " left join ocs_mst_tcompanydocumentprogram b on a.companydocument_gid = b.companydocument_gid " +
                     " where a.status='Y' and b.program_gid = '" + program_gid + "' and a.covenant_type = 'Y' and a.companydocument_gid not in (" + lscovenantcompanydocument_gid + ") ";

                    }

                    else
                    {

                        msSQL = " select a.companydocument_gid,a.documenttypes_gid,a.documenttype_name,a.companydocument_name,a.covenant_type " +
                    " from ocs_mst_tcompanydocument a " +
                    " left join ocs_mst_tcompanydocumentprogram b on a.companydocument_gid = b.companydocument_gid " +
                    " where a.status='Y' and b.program_gid = '" + program_gid + "' and a.covenant_type = 'Y'  ";


                    }

                    dt_datatable2 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable2.Rows.Count != 0)
                    {

                        foreach (DataRow dt2 in dt_datatable2.Rows)
                        {

                            msGetGID = objcmnfunctions.GetMasterGID("CDCL");
                            msSQL = " insert into ocs_trn_tcovanantdocumentcheckdtls(" +
                            " covenantdocumentcheckdtl_gid," +
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
                                " created_by)" +
                                " VALUES(" +
                                "'" + msGetGID + "'," +
                                "'" + application_gid + "'," +
                                 "'" + dt["institution_gid"] + "'," +
                                "'" + dt2["companydocument_gid"] + "'," +
                             "'" + dt2["documenttypes_gid"] + "'," +
                                "'" + dt2["documenttype_name"] + "'," +
                                "'" + dt2["companydocument_name"] + "'," +
                                "'" + dt2["covenant_type"] + "'," +
                                "'" + taggedby + "'," +
                                "'" + tagged_name + "'," +
                                "current_timestamp," +
                                "'" + user_gid + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



                            string msGetgroupCovDocGID = objcmnfunctions.GetMasterGID("GCDG");
                            msSQL = " insert into ocs_trn_tgroupcovenantdocumentchecklist(" +
                                      " groupcovdocumentchecklist_gid," +
                                      " application_gid," +
                                      " credit_gid, " +
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
                                  "'" + dt["institution_gid"] + "'," +
                                  "'" + dt2["companydocument_gid"] + "'," +
                                  "'" + dt2["companydocument_name"].ToString() + "'," +
                                  "'" + dt2["covenant_type"].ToString() + "'," +
                                  "'" + dt2["documenttypes_gid"].ToString() + "'," +
                                  "'" + dt2["documenttype_name"].ToString() + "'," +
                                   "'" + taggedby + "'," +
                                  "current_timestamp," +
                                  "'" + employee_gid + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " update ocs_trn_tcovanantdocumentcheckdtls set groupcovdocumentchecklist_gid ='" + msGetgroupCovDocGID + "'" +
                                   " where covenantdocumentcheckdtl_gid ='" + msGetGID + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                    }
                }


            }


            msSQL = " select contact_gid from ocs_mst_tcontact where application_gid = '" + application_gid + "' ";

            dt_datatable3 = objdbconn.GetDataTable(msSQL);

            if (dt_datatable3.Rows.Count != 0)
            {

                foreach (DataRow dt3 in dt_datatable3.Rows)
                {

                    string program_gid = ""; string lsindividualdocument_gid = "";

                    msSQL = " select program_gid from ocs_mst_tapplication where application_gid = '" + application_gid + "' ";

                    program_gid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select GROUP_CONCAT('\\\'',individualdocument_gid,'\\\'')  from ocs_trn_tdocumentchecktls where  credit_gid = '" + dt3["contact_gid"] + "' and  application_gid = '" + application_gid + "'";
                    lsindividualdocument_gid = objdbconn.GetExecuteScalar(msSQL);

                    if (!string.IsNullOrEmpty(lsindividualdocument_gid))
                    {

                        msSQL = " select a.individualdocument_gid, a.documenttypes_gid, a.documenttype_name, a.individualdocument_name, a.covenant_type " +
                     " from ocs_mst_tindividualdocument a " +
                     " left join ocs_mst_tindividualdocumentprogram b on a.individualdocument_gid = b.individualdocument_gid " +
                     " where  a.status='Y' and b.program_gid = '" + program_gid + "' and a.individualdocument_gid not in (" + lsindividualdocument_gid + ") ";

                    }

                    else
                    {
                        msSQL = " select a.individualdocument_gid, a.documenttypes_gid, a.documenttype_name, a.individualdocument_name, a.covenant_type " +
                     " from ocs_mst_tindividualdocument a " +
                     " left join ocs_mst_tindividualdocumentprogram b on a.individualdocument_gid = b.individualdocument_gid " +
                     " where  a.status='Y' and b.program_gid = '" + program_gid + "'  ";
                    }

                    dt_datatable4 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable4.Rows.Count != 0)
                    {
                        foreach (DataRow dt4 in dt_datatable4.Rows)
                        {
                            msGetGid = objcmnfunctions.GetMasterGID("DOCG");
                            msSQL = " insert into ocs_trn_tdocumentchecktls(" +
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
                                " created_by)" +
                                " VALUES(" +
                                "'" + msGetGid + "'," +
                                "'" + application_gid + "'," +
                                "'" + dt3["contact_gid"] + "'," +
                                "'" + dt4["individualdocument_gid"] + "'," +
                             "'" + dt4["documenttypes_gid"] + "'," +
                                "'" + dt4["documenttype_name"] + "'," +
                                "'" + dt4["individualdocument_name"] + "'," +
                                "'" + dt4["covenant_type"] + "'," +
                                "'" + taggedby + "'," +
                                "'" + tagged_name + "'," +
                                "current_timestamp," +
                                "'" + user_gid + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                            string msGetgroupDocGID = objcmnfunctions.GetMasterGID("GDCG");
                            msSQL = " insert into ocs_trn_tgroupdocumentchecklist(" +
                                      " groupdocumentchecklist_gid," +
                                      " application_gid," +
                                      " credit_gid, " +
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
                                  "'" + dt3["contact_gid"] + "'," +
                                  "'" + dt4["individualdocument_gid"] + "'," +
                                  "'" + dt4["individualdocument_name"].ToString() + "'," +
                                  "'" + dt4["covenant_type"].ToString() + "'," +
                                  "'" + dt4["documenttypes_gid"].ToString() + "'," +
                                  "'" + dt4["documenttype_name"].ToString() + "'," +
                                  "'" + taggedby + "'," +
                                  "current_timestamp," +
                                  "'" + employee_gid + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                            msSQL = " update ocs_trn_tdocumentchecktls set groupdocumentchecklist_gid ='" + msGetgroupDocGID + "'" +
                                   " where documentcheckdtl_gid ='" + msGetGid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }



                    }

                    string lscovenantindividualdocument_gid = "";

                    msSQL = " select GROUP_CONCAT('\\\'',individualdocument_gid,'\\\'')  from ocs_trn_tcovanantdocumentcheckdtls where  covenant_type = 'Y' and credit_gid = '" + dt3["contact_gid"] + "' and  application_gid = '" + application_gid + "' ";
                    lscovenantindividualdocument_gid = objdbconn.GetExecuteScalar(msSQL);

                    if (!string.IsNullOrEmpty(lscovenantindividualdocument_gid))
                    {

                        msSQL = " select a.individualdocument_gid, a.documenttypes_gid, a.documenttype_name, a.individualdocument_name, a.covenant_type " +
                   " from ocs_mst_tindividualdocument a " +
                   " left join ocs_mst_tindividualdocumentprogram b on a.individualdocument_gid = b.individualdocument_gid " +
                   " where a.status='Y' and b.program_gid = '" + program_gid + "'  and a.covenant_type = 'Y' and a.individualdocument_gid not in (" + lsindividualdocument_gid + ") ";

                    }

                    else
                    {

                        msSQL = " select a.individualdocument_gid, a.documenttypes_gid, a.documenttype_name, a.individualdocument_name, a.covenant_type " +
                   " from ocs_mst_tindividualdocument a " +
                   " left join ocs_mst_tindividualdocumentprogram b on a.individualdocument_gid = b.individualdocument_gid " +
                   " where a.status='Y' and b.program_gid = '" + program_gid + "'  and a.covenant_type = 'Y'  ";

                    }


                    dt_datatable5 = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable5.Rows.Count != 0)
                    {

                        foreach (DataRow dt5 in dt_datatable5.Rows)
                        {

                            msGetGID = objcmnfunctions.GetMasterGID("CDCL");
                            msSQL = " insert into ocs_trn_tcovanantdocumentcheckdtls(" +
                            " covenantdocumentcheckdtl_gid," +
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
                                " created_by)" +
                                " VALUES(" +
                                "'" + msGetGID + "'," +
                                "'" + application_gid + "'," +
                                 "'" + dt3["contact_gid"] + "'," +
                                "'" + dt5["individualdocument_gid"] + "'," +
                             "'" + dt5["documenttypes_gid"] + "'," +
                                "'" + dt5["documenttype_name"] + "'," +
                                "'" + dt5["individualdocument_name"] + "'," +
                                "'" + dt5["covenant_type"] + "'," +
                                "'" + taggedby + "'," +
                                "'" + tagged_name + "'," +
                                "current_timestamp," +
                                "'" + user_gid + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



                            string msGetgroupCovDocGID = objcmnfunctions.GetMasterGID("GCDG");
                            msSQL = " insert into ocs_trn_tgroupcovenantdocumentchecklist(" +
                                      " groupcovdocumentchecklist_gid," +
                                      " application_gid," +
                                      " credit_gid, " +
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
                                  "'" + dt3["contact_gid"] + "'," +
                                  "'" + dt5["individualdocument_gid"] + "'," +
                                  "'" + dt5["individualdocument_name"].ToString() + "'," +
                                  "'" + dt5["covenant_type"].ToString() + "'," +
                                  "'" + dt5["documenttypes_gid"].ToString() + "'," +
                                  "'" + dt5["documenttype_name"].ToString() + "'," +
                                   "'" + taggedby + "'," +
                                  "current_timestamp," +
                                  "'" + employee_gid + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " update ocs_trn_tcovanantdocumentcheckdtls set groupcovdocumentchecklist_gid ='" + msGetgroupCovDocGID + "'" +
                                   " where covenantdocumentcheckdtl_gid ='" + msGetGID + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                    }
                }


            }

        }

    }
}