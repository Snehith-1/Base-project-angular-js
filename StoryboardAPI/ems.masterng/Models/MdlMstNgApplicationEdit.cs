using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ems.masterng.Models
{
    public class MdlMstNgApplicationEdit : result
    {
        public string application_gid { get; set; }
        public string creditgroup_gid { get; set; }
        public string vertical_gid { get; set; }
        public string institution_gid { get; set; }
        public string program_gid { get; set; }
        public string program_name { get; set; }
        public List<edit_mstinst_list> edit_mstinstlist { get; set; }
        public List<edit_primaryvaluechain_list> edit_primaryvaluechainlist { get; set; }
        public List<edit_vernacularlanguage_list> edit_vernacularlanguagelist { get; set; }
        public List<edit_valuechainlist> edit_valuechain_list { get; set; }
        public List<edit_vernacularlang_list> edit_vernacularlanglist { get; set; }
        public List<mstngeditproduct_list> mstngeditproductlist { get; set; }
        public editvariety_list[] edit_variety_list { get; set; }
        public string product_gid { get; set; }
        public string product_name { get; set; }
        public string variety_gid { get; set; }
        public string variety_name { get; set; }
        public string sector_name { get; set; }
        public string category_name { get; set; }
        public string botanical_name { get; set; }
        public string alternative_name { get; set; }
        public string application2loan_gid { get; set; }
        public string searchrecord_flag { get; set; }
        public string customer_urn { get; set; }
        public string customer_name { get; set; }
        
        public string vertical_name { get; set; }
        public string verticaltaggs_gid { get; set; }
        public string verticaltaggs_name { get; set; }
        public string constitution_gid { get; set; }
        public string constitution_name { get; set; }
        public string businessunit_gid { get; set; }
        public string businessunit_name { get; set; }
        public string sa_status { get; set; }
        public string saname_gid { get; set; }
        public string sa_id { get; set; }
        public string sa_name { get; set; }
        public string business_activities { get; set; }
        public string relationshipmanager_name { get; set; }
        public string relationshipmanager_gid { get; set; }
        public string social_capital { get; set; }
        public string trade_capital { get; set; }
        public string vernacular_language { get; set; }
        public string vernacularlanguage_gid { get; set; }
        public string contactpersonfirst_name { get; set; }
        public string contactpersonmiddle_name { get; set; }
        public string contactpersonlast_name { get; set; }
        public string designation_gid { get; set; }
        public string designation_type { get; set; }
        public string landline_no { get; set; }
        public string overalllimit_amount { get; set; }
        public string processing_fee { get; set; }
        public string doc_charges { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string productcharge_flag { get; set; }
        public string economical_flag { get; set; }
        public string application_status { get; set; }
        public string applicant_type { get; set; }
        public string opsapplication_gid { get; set; }
        public string updated_date { get; set; }
        public string approval_status { get; set; }
        public string submitted_date { get; set; }
        public string ccsubmitted_date { get; set; }
        public string submitted_by { get; set; }
        public string ccsubmitted_by { get; set; }
        public string region { get; set; }
        public string ccgroup_name { get; set; }
        public string ccmeeting_date { get; set; }
        public string ccmeting_time { get; set; }
        public string scheduled_date { get; set; }
        public string now_date { get; set; }
        public string cccompleted_date { get; set; }
        public string updated_by { get; set; }
        public string createdby { get; set; }
        public string applicationapproval_gid { get; set; }
        public string headapproval_status { get; set; }
        public string initiate_flag { get; set; }
        public string headapproval_date { get; set; }
       
        public string creditheadapproval_status { get; set; }
        public string creditgroup_name { get; set; }
        public string creditassigned_date { get; set; }
        public string creditassigned_by { get; set; }
        public string creditassigned_to { get; set; }
        public string rmquery_flag { get; set; }
        public string momupdated_by { get; set; }
        public string momupdated_date { get; set; }
        public string renewal_flag { get; set; }
        public string history_flag { get; set; }
        public string sentback_flag { get; set; }
        public string enhancement_flag { get; set; }
        public string ccmeetingskip_flag { get; set; }
        public string ccmeetingskipcolor_flag { get; set; }
        public class edit_valuechainlist
        {
            public string valuechain_gid { get; set; }
            public string valuechain_code { get; set; }
            public string valuechain_name { get; set; }
            public string bureau_code { get; set; }
        }
        public class result
        {
            public bool status;
            public string message { get; set; }
        }
        
             public class edit_mstinst_list
        {
            public string institution_name { get; set; }
            public string institution_gid { get; set; }
        }
        public class edit_primaryvaluechain_list
        {
            public string valuechain_name { get; set; }
            public string valuechain_gid { get; set; }
        }
        public class edit_vernacularlang_list
        {
            public string vernacularlanguage_gid { get; set; }
            public string vernacular_language { get; set; }
        }
        public class editvariety_list
        {
            public string variety_gid { get; set; }
            public string variety_name { get; set; }
        }
        public class edit_vernacularlanguage_list
        {
            public string vernacularlanguage_gid { get; set; }
            public string vernacular_language { get; set; }
        }
        public class mstngeditproduct_list
        {
        
            public string application2product_gid { get; set; }
            public string product_gid { get; set; }
            public string product_name { get; set; }
            public string variety_gid { get; set; }
            public string variety_name { get; set; }
            public string sector_name { get; set; }
            public string category_name { get; set; }
            public string botanical_name { get; set; }
            public string alternative_name { get; set; }
            public string application2loan_gid { get; set; }
            public string csacommodity_average { get; set; }
            public string csapercentageoftotal_limit { get; set; }
        }
    }
}