using ems.masterng.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using static ems.masterng.Models.MdlMstNgApplicationEdit;

namespace ems.masterng.DataAccess
{
    public class DaMstNgApplicationEdit
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader, objODBCDatareader1, objODBCDataReader;
        DataTable dt_datatable;
        DataTable dt_datatable1;
        DataTable dt_datatable2;
        DataTable dt_datatable3;
        int mnResult;
        string msSQL, msGetGid;
        private string lsemployee_name;
        private string lsapplication_gid;
        private string lsdrm_gid;
        private string lsdrm_name;
        string lsapplication_no, lscompany_name, lsdate_incorporation, lscompanypan_no, lsyear_business, lsmonth_business, lscin_no;
        string lsofficial_telephoneno, lsofficial_mailid, lscompanytype_gid, lscompanytype_name, lsstakeholder_type, lsstakeholdertype_gid, lsassessmentagency_gid;
        string lsassessmentagency_name, lsassessmentagencyrating_gid, lsassessmentagencyrating_name, lsratingas_on, lsamlcategory_gid, lsamlcategory_name;
        string lsbusinesscategory_gid, lsbusinesscategory_name, lscontactperson_firstname, lscontactperson_middlename, lscontactperson_lastname, lsdesignation_gid, lsdesignation_name;
        string lsdesignation, lslastyear_turnover, lsescrow, lsstart_date, lsend_date, lsbusinessstart_date;
        string lsapplication2loan_gid, lsfacilityrequested_date, lsproduct_type, lsproducttype_gid, lsproductsub_type, lsfacilityvalidity_month, lsfacilityvalidity_days;
        string lsproductsubtype_gid, lsloantype_gid, lsloan_type, lsfacilityloan_amount, lsrate_interest, lsratemargin, lspenal_interest, lsfacilityvalidity_year;
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
        string lscontact_gid, lscontact2address_gid;
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

        int matchCount1, matchCount2;
        string lspan_status, msGetGidpan;
        string ls_relationshipmanager_name, ls_customerref_name, ls_product_name, ls_institution_gid, tomail_id1, ls_relationshipmanager_gid, ls_clustermanager_gid;
        string lsemployee_mobileno, lsemployee_emailid, lscluster_name, ls_overalllimit_amount;
        private string lsemail_toaddress;
        string lsapplprogram_gid;
        string msGetGidinstitution2documentlog, msGetGidcontact2documentlog;
        int mnResultinstitution2documentlog, mnResultcontact2documentlog;

        public void DaGetEditProductDetailList(string application_gid, MdlMstNgApplicationEdit values)
        {
            msSQL = "select application_gid from ocs_mst_tapplication where application_gid='" + application_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            if (lsapplication_gid == "" || lsapplication_gid == null)
            {
                lsapplication_gid = values.application_gid;
            }

            msSQL = " select application2product_gid,product_gid,product_name,variety_gid,variety_name,sector_name,category_name," +
                    " botanical_name,alternative_name from ocs_mst_tapplication2product where application_gid='" + lsapplication_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstproduct_list = new List<mstngeditproduct_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstproduct_list.Add(new mstngeditproduct_list
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
                values.mstngeditproductlist = getmstproduct_list;
            }
            dt_datatable.Dispose();
        }
        public bool DaPostEditProductDetailAdd(string employee_gid, MdlMstNgApplicationEdit values)
        {
            string variety_gid = string.Empty;
            string variety_name = string.Empty;

            if (values.edit_variety_list != null)
            {
                for (var i = 0; i < values.edit_variety_list.Length; i++)
                {
                    variety_gid += values.edit_variety_list[i].variety_gid + ",";
                    variety_name += values.edit_variety_list[i].variety_name + ",";

                }

                variety_gid = variety_gid.TrimEnd(',');
                variety_name = variety_name.TrimEnd(',');
            }
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
                    "'" + values.product_gid + "'," +
                    "'" + values.product_name + "'," +
                    "'" + variety_gid + "'," +
                    "'" + variety_name + "'," +
                    "'" + values.sector_name + "'," +
                    "'" + values.category_name + "'," +
                    "'" + values.botanical_name + "'," +
                    "'" + values.alternative_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Product Details Added Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }
        }

        public void DaUpdateAppBasicDetail(string employee_gid, MdlMstNgApplicationEdit values)
        {
            lsapplication_gid = objdbconn.GetExecuteScalar("select application_gid from ocs_mst_tapplication where application_gid='" + values.application_gid + "' and " +
                " headapproval_status='Comment Raised'");
            if (lsapplication_gid == "" || lsapplication_gid == null)
            {
            }
            else
            {

                //string lsverticalgid = objdbconn.GetExecuteScalar("select vertical_gid from ocs_mst_tapplication where application_gid='" + values.application_gid + "'");

                //if (lsverticalgid != values.vertical_gid)
                //{
                //    values.status = false;
                //    values.message = "Already Approval has been Initiated.. You Can't Change the Vertical";
                //    return;

                //}

            }
            string lsapp_refno;
            //msSQL = " select application_gid from ocs_trn_tAppcreditapproval where application_gid='" + values.application_gid + "' and hierary_level<>'0'";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);
            //if (objODBCDatareader.HasRows == true)
            //{
            //    objODBCDatareader.Close();
            //    string lscreditgroup_gid = objdbconn.GetExecuteScalar("select creditgroup_gid from ocs_mst_tapplication where application_gid='" + values.application_gid + "'");

            //    if (lscreditgroup_gid != values.creditgroup_gid)
            //    {
            //        values.status = false;
            //        values.message = "Already Approval has been Initiated.. You Can't Change the Credit Group";
            //        return;

            //    }
            //}
            //else
            //{
            //    objODBCDatareader.Close();
            //}

           

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


            for (var i = 0; i < values.edit_vernacularlanguagelist.Count; i++)
            {
                gsvernacularlanguage_gid += values.edit_vernacularlanguagelist[i].vernacularlanguage_gid + ",";
                gsvernacular_language += values.edit_vernacularlanguagelist[i].vernacular_language + ",";

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

                //string lsclusterhead, lsregionalhead, lszonalhead, lsbusinesshead;
                //string lsclusterheadgid, lsregionalheadgid, lszonalheadgid, lsbusinessheadgid;
                //string lsbaselocationgid, lsclustergid, lsregiongid, lszonalgid;
                //string lsbaselocationname, lsclustername, lsregionname, lszonalname;

                //msSQL = " select distinct b.baselocation_gid,b.baselocation_name,c.cluster_gid,c.cluster_name, " +
                //        " c.employee_name as clusterhead,c.vertical_name as clustervertical," +
                //        " e.region_gid,e.region_name,e.employee_name as regionhead,g.zonal_gid,g.zonal_name,g.employee_name as zonalhead ," +
                //        " c.employee_gid as clusterhead_gid,e.employee_gid as regionhead_gid, " +
                //        " g.employee_gid as zonalhead_gid,h.employee_gid as businesshead_gid, " +
                //        " h.employee_name as businesshead from hrm_mst_temployee a" +
                //        " left join sys_mst_tcluster2baselocation b on b.baselocation_gid = a.baselocation_gid" +
                //        " left  join sys_mst_tclusterhead c on b.cluster_gid = c.cluster_gid" +
                //        " left join sys_mst_tregion2cluster d on c.cluster_gid = d.cluster_gid" +
                //        " left join sys_mst_tregionhead e on d.region_gid = e.region_gid" +
                //        " left join sys_mst_tzone2region f on f.region_gid = d.region_gid" +
                //        " left join sys_mst_tzonalhead g on f.zone_gid = g.zonal_gid" +
                //        " left join sys_mst_tbusinesshead h on h.zone_gid = g.zonal_gid where a.employee_gid = '" + employee_gid + "' and" +
                //        " c.vertical_gid = '" + values.vertical_gid + "'" +
                //        " and e.vertical_gid = '" + values.vertical_gid + "' and " +
                //        " g.vertical_gid = '" + values.vertical_gid + "' and h.vertical_gid = '" + values.vertical_gid + "' " +
                //        " and c.program_gid = '" + values.program_gid + "'" +
                //        " and e.program_gid = '" + values.program_gid + "' and " +
                //        " g.program_gid = '" + values.program_gid + "' and h.program_gid = '" + values.program_gid + "' " +
                //        " and e.status='Y' and g.status='Y' and h.status='Y' and c.status='Y'";
                //objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                //if ()
                ////{
                //    lsclusterhead = objODBCDatareader1["clusterhead"].ToString();
                //    lsregionalhead = objODBCDatareader1["regionhead"].ToString();
                //    lszonalhead = objODBCDatareader1["zonalhead"].ToString();
                //    lsbusinesshead = objODBCDatareader1["businesshead"].ToString();
                //    lsclusterheadgid = objODBCDatareader1["clusterhead_gid"].ToString();
                //    lsregionalheadgid = objODBCDatareader1["regionhead_gid"].ToString();
                //    lszonalheadgid = objODBCDatareader1["zonalhead_gid"].ToString();
                //    lsbusinessheadgid = objODBCDatareader1["businesshead_gid"].ToString();
                //    lsbaselocationgid = objODBCDatareader1["baselocation_gid"].ToString();
                //    lsbaselocationname = objODBCDatareader1["baselocation_name"].ToString();
                //    lsclustergid = objODBCDatareader1["cluster_gid"].ToString();
                //    lsclustername = objODBCDatareader1["cluster_name"].ToString();
                //    lsregiongid = objODBCDatareader1["region_gid"].ToString();
                //    lsregionname = objODBCDatareader1["region_name"].ToString();
                //    lszonalgid = objODBCDatareader1["zonal_gid"].ToString();
                //    lszonalname = objODBCDatareader1["zonal_name"].ToString();
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
                         //" cluster_gid='" + lsclustergid + "'," +
                         //" cluster_name='" + lsclustername + "'," +
                         //" region_gid='" + lsregiongid + "'," +
                         //" region_name='" + lsregionname + "'," +
                         //" zone_gid='" + lszonalgid + "'," +
                         //" zone_name='" + lszonalname + "'," +
                         " drm_gid='" + lsdrm_gid + "'," +
                         " drm_name='" + lsdrm_name + "'," +
                         //" clustermanager_gid='" + lsclusterheadgid + "'," +
                         //" clustermanager_name='" + lsclusterhead + "'," +
                         //" zonalhead_name='" + lszonalhead + "'," +
                         //" zonalhead_gid='" + lszonalheadgid + "'," +
                         //" regionalhead_name='" + lsregionalhead + "'," +
                         //" regionalhead_gid='" + lsregionalheadgid + "'," +
                         //" businesshead_name='" + lsbusinesshead + "'," +
                         //" businesshead_gid='" + lsbusinessheadgid + "'," +
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
                          " business_activities='" + values.business_activities.Replace("'","\\'") + "', " +
                         " status = 'Completed'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where application_gid='" + values.application_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
               // }
              //  objODBCDatareader1.Close();
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

                 
                    msSQL = "update ocs_mst_tapplication2product set application_gid ='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                   
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


        public void DaEditAppBasicDetail(string application_gid, MdlMstNgApplicationEdit values)
        {
            try
            {
                msSQL = " select application_gid,customer_urn,customerref_name as customer_name,vertical_gid,vertical_name,verticaltaggs_gid,verticaltaggs_name," +
                        " constitution_gid,constitution_name,businessunit_gid,businessunit_name,sa_status,sa_id,sa_name,saname_gid,vernacularlanguage_gid," +
                        " vernacular_language,contactpersonfirst_name,contactpersonmiddle_name,contactpersonlast_name,designation_gid,designation_type,landline_no," +
                        " creditgroup_gid,creditgroup_name,program_gid,program_name, product_gid,product_name,variety_gid,variety_name,sector_name,category_name, " +
                        " botanical_name,alternative_name,business_activities from ocs_mst_tapplication where application_gid='" + application_gid + "'";
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
                    values.business_activities = objODBCDatareader["business_activities"].ToString();


                    //values.vernacularlanguage_gid = objODBCDatareader["vernacularlanguage_gid"].ToString();
                    //values.vernacular_language = objODBCDatareader["vernacular_language"].ToString();


                    String[] verlanggid_list = objODBCDatareader["vernacularlanguage_gid"].ToString().Split(',');
                    String[] verlangname_list = objODBCDatareader["vernacular_language"].ToString().Split(',');
                    // String[] verlangname_list1234 = objODBCDatareader["customer_name"].ToString().Split(',');


                    var getvernacularLanguageList = new List<edit_vernacularlanguage_list>();

                    for (var i = 0; i < verlanggid_list.Length; i++)
                    {
                        getvernacularLanguageList.Add(new edit_vernacularlanguage_list
                        {
                            vernacularlanguage_gid = verlanggid_list[i],
                            vernacular_language = verlangname_list[i],
                        });

                    }
                    values.edit_vernacularlanguagelist = getvernacularLanguageList;                 

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
                var getvaluechain = new List<edit_valuechainlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getvaluechain.Add(new edit_valuechainlist
                        {
                            valuechain_gid = (dr_datarow["valuechain_gid"].ToString()),
                            valuechain_name = (dr_datarow["valuechain_name"].ToString()),
                        });
                    }
                    values.edit_valuechain_list = getvaluechain;
                }
                dt_datatable.Dispose();

                //Vernacular Language
                msSQL = " SELECT vernacularlanguage_gid,vernacular_language FROM ocs_mst_tvernacularlanguage a" +
                          " where status='Y' order by a.vernacularlanguage_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getvernacularlang_list = new List<edit_vernacularlang_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getvernacularlang_list.Add(new edit_vernacularlang_list
                        {
                            vernacularlanguage_gid = (dr_datarow["vernacularlanguage_gid"].ToString()),
                            vernacular_language = (dr_datarow["vernacular_language"].ToString()),
                        });
                    }
                    values.edit_vernacularlanglist = getvernacularlang_list;
                }
                dt_datatable.Dispose();

                msSQL = "select primaryvaluechain_gid,primaryvaluechain_name from ocs_mst_tapplication2primaryvaluechain where application_gid='" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                values.edit_primaryvaluechainlist = dt_datatable.AsEnumerable().Select(row =>
           new edit_primaryvaluechain_list
           {
               valuechain_gid = row["primaryvaluechain_gid"].ToString(),
               valuechain_name = row["primaryvaluechain_name"].ToString()
           }).ToList();
                dt_datatable.Dispose();

                msSQL = "select secondaryvaluechain_gid,secondaryvaluechain_name from ocs_mst_tapplication2secondaryvaluechain where application_gid='" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                values.edit_primaryvaluechainlist = dt_datatable.AsEnumerable().Select(row =>
             new edit_primaryvaluechain_list
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

        public void DaGetInstitution(string application_gid, MdlMstNgApplicationEdit values)
        {
            msSQL = " select institution_gid" +
                    " from ocs_mst_tinstitution where application_gid='" + application_gid + "'";
            // dt_datatable = objdbconn.GetDataTable(msSQL);
            values.institution_gid = objdbconn.GetExecuteScalar(msSQL);
           
            //var getmstgst_list = new List<edit_mstinst_list>();
            //if (dt_datatable.Rows.Count != 0)
            //{
            //    foreach (DataRow dr_datarow in dt_datatable.Rows)
            //    {
            //        getmstgst_list.Add(new edit_mstinst_list
            //        {
            //            institution_gid = (dr_datarow["institution_gid"].ToString()),

            //        });
            //    }
            //    values.edit_mstinstlist = getmstgst_list;
            //}
           // dt_datatable.Dispose();
        }
    }
}