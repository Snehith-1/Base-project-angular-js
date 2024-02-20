using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.master.Models
{
    public class MdlCreditGroup : result
    {
        public string creditmapping_gid { get; set; }
        public string creditgroup_gid { get; set; }
        public string creditgroup_name { get; set; }
        public string creditgroup_status { get; set; }
        public string creditgroup_id { get; set; }
        public List<CreditGroup> CreditGroup { get; set; }
        public List<Credithead> Credithead { get; set; }
        public List<Creditnationalmanager> Creditnationalmanager { get; set; }
        public List<Creditregionalmanager> Creditregionalmanager { get; set; }
        public List<CreditManager> CreditManager { get; set; }
        public List<Creditlog> Creditlog { get; set; }
        public List<Creditheadem_list> Creditheadem_list { get; set; }
        public List<Creditnationalmanagerem_list> Creditnationalmanagerem_list { get; set; }
        public List<Creditregionalmanagerem_list> Creditregionalmanagerem_list { get; set; }
        public List<CreditManagerem_list> CreditManagerem_list { get; set; }
        public List<creditgoupname> creditgoupname { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string credit2creditmanager_gid { get; set; }
        public string creditr2regionalmanager_gid { get; set; }
        public string credit2nationalmanager_gid { get; set; }
        public string credit2credithead_gid { get; set; }
    }
    public class Creditheadem_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class Creditnationalmanagerem_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class Creditregionalmanagerem_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class CreditManagerem_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }

    public class CreditGroup : result
    {
        public string creditmapping_gid { get; set; }
        public string creditgroup_id { get; set; }
        public string creditgroup_name { get; set; }
        public string creditgroup_status { get; set; }
        public string remarks { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public char rbo_status { get; set; }
        public string api_code { get; set; }
    }
    public class Credithead
    {
        public string credit2credithead_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class Creditnationalmanager
    {
        public string credit2nationalmanager_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class Creditregionalmanager
    {
        public string creditr2regionalmanager_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class CreditManager
    {
        public string credit2creditmanager_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class Creditlog
    {
        public string creditmapping_gid { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string remarks { get; set; }
        public string status { get; set; }
    }
    public class MdlCreditheadassign : result
    {
        public string creditgroup_gid { get; set; }
        public string creditmanager_gid { get; set; }
        public string regionalcredit_gid { get; set; }
        public string nationalcredit_gid { get; set; }
        public string credithead_gid { get; set; }
        public string creditgroup_name { get; set; }
        public string creditmanager_name { get; set; }
        public string regionalcredit_name { get; set; }
        public string nationalcredit_name { get; set; }
        public string credithead_name { get; set; }
        public string creditgroup_status { get; set; }
        public string application_gid { get; set; }
        public string remarks { get; set; }
    }
    public class creditgoupname : result
    {
        public string creditgroup_gid { get; set; }
        public string creditgroup_name { get; set; }
    }
    public class creditheads : result
    {
        public string credithead { get; set; }
        public string creditmanager { get; set; }
        public string creditregional_manager { get; set; }
        public string creditnational_manager { get; set; }
    }

    public class MdlreassignedlogInfo : result
    {
        public List<reassignedloglist> reassignedloglist { get; set; }
    }
    public class reassignedloglist
    {
        public string application_gid { get; set; }
        public string creditmanger_gid { get; set; }
        public string creditmanger_name { get; set; }
        public string reassignto_creditmanger_gid { get; set; }
        public string reassignto_creditmanger_name { get; set; }

        public string creditregionalmanager_gid { get; set; }
        public string creditregionalmanager_name { get; set; }
        public string reassignto_creditregionalmanager_gid { get; set; }
        public string reassignto_creditregionalmanager_name { get; set; }

        public string creditnationalmanager_gid { get; set; }
        public string creditnationalmanager_name { get; set; }
        public string reassignto_creditnationalmanager_gid { get; set; }
        public string reassignto_creditnationalmanager_name { get; set; }

        public string credithead_gid { get; set; }
        public string credithead_name { get; set; }
        public string reassignto_credithead_gid { get; set; }
        public string reassignto_credithead_name { get; set; }

        public string remarks { get; set; }
        public string reassign_remarks { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string reassignto_creditgroup_name { get; set; }
        public string reassignto_creditgroup_gid { get; set; }
        public string creditgroup_name { get; set; }
        public string creditgroup_gid { get; set; }
    }

    public class MdlCreditMappingLogInfo : result
    {
        public List<creditmappingloglist> creditmappingloglist { get; set; }
    }
    public class creditmappingloglist
    {

        public string creditmanager_gid { get; set; }
        public string creditmanager_name { get; set; }
        public string regionalmanager_gid { get; set; }
        public string regionalmanager_name { get; set; }
        public string nationalmanager_gid { get; set; }
        public string nationalmanager_name { get; set; }
        public string credithead_gid { get; set; }
        public string credithead_name { get; set; }

        public string new_creditmanager_gid { get; set; }
        public string new_creditmanager_name { get; set; }
        public string new_regionalmanager_gid { get; set; }
        public string new_regionalmanager_name { get; set; }
        public string new_nationalmanager_gid { get; set; }
        public string new_nationalmanager_name { get; set; }
        public string new_credithead_gid { get; set; }
        public string new_credithead_name { get; set; }


        public string created_by { get; set; }
        public string created_date { get; set; }

        public string creditgroup_name { get; set; }
        public string old_creditgroup_name { get; set; }
    }
    public class CreditGroupTitle_list
    {
        public string creditgroup_name { get; set; }
        public string creditgroup_id { get; set; }
        public List<GroupTitle_dtl> GroupTitle_dtl { get; set; }
    }
    public class GroupTitle_dtl
    {
        public string grouptitle_gid { get; set; }
        public string grouptitle_name { get; set; }
        public string footer_flag { get; set; }
        public string group_order { get; set; }
        public string final_scoredisplay { get; set; }
    }

    public class MdlGroupTitleQuestion_list : result
    {
        public List<MdlGroupTitleQuestion> MdlGroupTitleQuestion { get; set; }
    }

    public class MdlGroupTitleQuestion : result
    {
        public string creditquestionrule_gid { get; set; }
        public string creditmapping_gid { get; set; }
        public string grouptitle_gid { get; set; }
        public string grouptitle_name { get; set; }
        public string question { get; set; }
        public string answer_type { get; set; }
        public string number_score { get; set; }
        public string calculation_formula { get; set; }
        public string simplify_formula { get; set; }
        public string addfinal_score { get; set; }
        public string hidden_question { get; set; }
        public string group_order { get; set; }
        public string question_order { get; set; }
        public List<listarray> listarray { get; set; }
        public List<calculation_dtl> calculation_dtl { get; set; }
    }

    public class listarray
    {
        public string creditquestionrule_gid { get; set; }
        public string questionlistoption_gid { get; set; }
        public string list_name { get; set; }
        public string Score { get; set; }
    }

    public class calculation_dtl
    {
        public string question { get; set; }
        public string question_gid { get; set; }
        public string field_type { get; set; }
        public string constantvalue { get; set; }
        public string operations { get; set; }
        public string grouptitle_gid { get; set; }
        public string grouptitle_name { get; set; }
        public string simplify_key { get; set; }
    }

    public class MdlCreditGroupQuestiondtl : result
    {
        public List<GroupTitle_dtl> GroupTitle_dtl { get; set; }
        public List<listarray> listarray { get; set; }
        public List<MdlCreditGroupTitleQuestion> MdlCreditGroupTitleQuestion { get; set; }
    }

    public class MdlCreditGroupTitleQuestion : result
    {
        public string creditquestionrule_gid { get; set; }
        public string creditmapping_gid { get; set; }
        public string grouptitle_gid { get; set; }
        public string question { get; set; }
        public string answer_type { get; set; }
        public string number_score { get; set; }
        public string calculation_formula { get; set; }
        public string addfinal_score { get; set; }
        public string hidden_question { get; set; }
        public string actual_score { get; set; }
        public string actual_value { get; set; }
        public string actual_number { get; set; }
        public string final_scoredisplay { get; set; }
        public string actualvalue_gid { get; set; }
    }

    public class MdlCreditGroupScore : result
    {
        public object question_score { get; set; }
        public string creditquestionrule_gid { get; set; }
    }

    public class MdlCreditGroupScoredtl : result
    {
        public string creditquestionrule_gid { get; set; }
        public string grouptitle_gid { get; set; }
        public string application_gid { get; set; }
        public List<GroupTitle_list> GroupTitle_list { get; set; }
    }

    public class GroupTitle_list
    {
        public string grouptitle_gid { get; set; }
        public string grouptitle_name { get; set; }
        public string final_scoredisplay { get; set; }
        public List<GroupQuestion_list> GroupQuestion_list { get; set; }
    }

    public class GroupQuestion_list : result
    {
        public string creditquestionrule_gid { get; set; }
        public string creditmapping_gid { get; set; }
        public string grouptitle_gid { get; set; }
        public string question { get; set; }
        public string answer_type { get; set; }
        public string Score { get; set; }
        public string number_score { get; set; }
        public string calculation_formula { get; set; }
        public string field_number { get; set; }
        public string final_score { get; set; }
        public string final_scoredisplay { get; set; }
        public List<DropdownListArray> DropdownListArray { get; set; }
        public string actual_number { get; set; }
    }
    public class DropdownListArray
    {
        public string creditquestionrule_gid { get; set; }
        public string questionlistoption_gid { get; set; }
        public string list_name { get; set; }
        public string Score { get; set; }
    }


    public class arraycalculation
    {
        public string key { get; set; }
        public decimal value { get; set; }
    }

    public class MdlCreditGroupdtl : result
    {
        public List<GroupTitle_dtl> GroupTitle_dtl { get; set; }
    }

    public class calculationinfo
    {
        public string grouptitle_gid { get; set; }
        public string creditquestionrule_gid { get; set; }
        public string question_gid { get; set; }
        public string final_score { get; set; }
    }

    public class VerticalTitle_list
    {
        public string vertical_name { get; set; }
        public string vertical_code { get; set; }
        public string applicant_type { get; set; }
        public List<GroupVerticalTitle_dtl> GroupTitle_dtl { get; set; }
    }
    public class GroupVerticalTitle_dtl
    {
        public string grouptitle_gid { get; set; }
        public string grouptitle_name { get; set; }
        public string footer_flag { get; set; }
        public string group_order { get; set; }
        public string final_scoredisplay { get; set; }
        public string applicant_type { get; set; }
        
    }

    public class MdlVerticalGroupTitleQuestion_list : result
    {
        public List<MdlVerticalGroupTitleQuestion> MdlVerticalGroupTitleQuestion { get; set; }
    }

    public class MdlVerticalGroupTitleQuestion : result
    {
        public string verticalquestionrule_gid { get; set; }
        public string vertical_gid { get; set; }
        public string grouptitle_gid { get; set; }
        public string grouptitle_name { get; set; }
        public string question { get; set; }
        public string answer_type { get; set; }
        public string number_score { get; set; }
        public string calculation_formula { get; set; }
        public string simplify_formula { get; set; }
        public string addfinal_score { get; set; }
        public string hidden_question { get; set; }
        public string group_order { get; set; }
        public string question_order { get; set; }

        public string applicant_type { get; set; }
        public string verticalapplicanttyperule_gid { get; set; }
        public string applicant_typegid { get; set; }
        
        public List<Verticallistarray> listarray { get; set; }
        public List<Verticalcalculation_dtl> calculation_dtl { get; set; }
    }

    public class Verticallistarray
    {
        public string creditquestionrule_gid { get; set; }
        public string questionlistoption_gid { get; set; }
        public string list_name { get; set; }
        public string Score { get; set; }
    }

    public class Verticalcalculation_dtl
    {
        public string question { get; set; }
        public string question_gid { get; set; }
        public string field_type { get; set; }
        public string constantvalue { get; set; }
        public string operations { get; set; }
        public string grouptitle_gid { get; set; }
        public string grouptitle_name { get; set; }
        public string simplify_key { get; set; }
    }

    public class MdlVerticalQuestiondtl : result
    {
        public List<GroupTitle_dtl> GroupTitle_dtl { get; set; }
        public List<listarray> listarray { get; set; }
        public List<MdlVerticalTitleQuestion> MdlVerticalTitleQuestion { get; set; }
    }

    public class MdlVerticalTitleQuestion : result
    {
        public string creditquestionrule_gid { get; set; }
        public string vertical_gid { get; set; }
        public string grouptitle_gid { get; set; }
        public string grouptitle_name { get; set; }
        public string question { get; set; }
        public string answer_type { get; set; }
        public string number_score { get; set; }
        public string calculation_formula { get; set; }
        public string addfinal_score { get; set; }
        public string hidden_question { get; set; }
        public string actual_score { get; set; }
        public string actual_value { get; set; }
        public string actual_number { get; set; }
        public string final_scoredisplay { get; set; }
        public string actualvalue_gid { get; set; } 
        public string simplify_formula { get; set; }
        public string applicant_typegid { get; set; }
        public string applicant_type { get; set; }
        public string vertical_name { get; set; }
        public string vertical_code { get; set; }
        public string verticalapplicanttyperule_gid { get; set; }
                
        public List<listarray> listarray { get; set; }
        public List<calculation_dtl> calculation_dtl { get; set; }
    }

    public class MdlVerticalScore : result
    {
        public object question_score { get; set; }
        public string creditquestionrule_gid { get; set; }
    }

    public class MdlVerticalScoredtl : result
    {
        public string creditquestionrule_gid { get; set; }
        public string grouptitle_gid { get; set; }
        public string application_gid { get; set; }
        public List<VerticalGroupTitle_list> GroupTitle_list { get; set; }
    }

    public class VerticalGroupTitle_list
    {
        public string grouptitle_gid { get; set; }
        public string grouptitle_name { get; set; }
        public string final_scoredisplay { get; set; }
        public List<VerticalGroupQuestion_list> GroupQuestion_list { get; set; }
    }

    public class VerticalGroupQuestion_list : result
    {
        public string creditquestionrule_gid { get; set; }
        public string creditmapping_gid { get; set; }
        public string grouptitle_gid { get; set; }
        public string question { get; set; }
        public string answer_type { get; set; }
        public string Score { get; set; }
        public string number_score { get; set; }
        public string calculation_formula { get; set; }
        public string field_number { get; set; }
        public string final_score { get; set; }
        public string final_scoredisplay { get; set; }
        public List<VerticalDropdownListArray> DropdownListArray { get; set; }
        public string actual_number { get; set; }
    }
    public class VerticalDropdownListArray
    {
        public string creditquestionrule_gid { get; set; }
        public string questionlistoption_gid { get; set; }
        public string list_name { get; set; }
        public string Score { get; set; }
    }


    public class Verticalarraycalculation
    {
        public string key { get; set; }
        public decimal value { get; set; }
    }

    public class MdlVerticaldtl : result
    {
        public List<GroupVerticalTitle_dtl> GroupTitle_dtl { get; set; }
    }

    public class Verticalcalculationinfo
    {
        public string grouptitle_gid { get; set; }
        public string creditquestionrule_gid { get; set; }
        public string question_gid { get; set; }
        public string final_score { get; set; }
    }


    // Vertical Transaction
    public class MdlVerticalGroupQuestiondtl : result
    {
        public string application_gid { get; set; }
        public string editruletype_gid { get; set; }
        public List<Vertical_GroupTitle_dtl> Vertical_GroupTitle_dtl { get; set; }
        public List<MdlTrnVerticalGroupTitleQuestion> MdlTrnVerticalGroupTitleQuestion { get; set; }
        public List<Vertical_listarray> Vertical_listarray { get; set; }
    }
    public class Vertical_GroupTitle_dtl : result
    {
        public string grouptitle_gid { get; set; }
        public string grouptitle_name { get; set; }
        public string footer_flag { get; set; }
        public string group_order { get; set; }
        public string final_scoredisplay { get; set; }

    }


    public class MdlTrnVerticalGroupTitleQuestion : result
    {
        public string verticalquestionrule_gid { get; set; }
        public string vertical_gid { get; set; }
        public string grouptitle_gid { get; set; }
        public string question { get; set; }
        public string answer_type { get; set; }
        public string number_score { get; set; }
        public string calculation_formula { get; set; }
        public string addfinal_score { get; set; }
        public string hidden_question { get; set; }
        public string actual_score { get; set; }
        public string actual_value { get; set; }
        public string actual_number { get; set; }
        public string final_scoredisplay { get; set; }
        public string actualvalue_gid { get; set; }
    }
    public class Vertical_listarray
    {
        public string verticalquestionrule_gid { get; set; }
        public string verticalquestionlistoption_gid { get; set; }
        public string list_name { get; set; }
        public string Score { get; set; }
    }
    public class MdlVerticalGroupScore : result
    {
        public object question_score { get; set; }
        public string verticalquestionrule_gid { get; set; }
    }

    public class MdlVerticalGroupScoredtl : result
    {
        public string verticalquestionrule_gid { get; set; }
        public string grouptitle_gid { get; set; }
        public string vertical_gid { get; set; }
        public string applicanttype { get; set; }
        public string application_gid { get; set; }
        public string editruletype_gid { get; set; }

        public List<GroupTitle_list1> GroupTitle_list1 { get; set; }
    }
    public class MdlVerticalGroupTitleQn : result
    {
        public string verticalquestionrule_gid { get; set; }
        public string vertical_gid { get; set; }
        public string grouptitle_gid { get; set; }
        public string grouptitle_name { get; set; }
        public string question { get; set; }
        public string answer_type { get; set; }
        public string number_score { get; set; }
        public string calculation_formula { get; set; }
        public string simplify_formula { get; set; }
        public string addfinal_score { get; set; }
        public string hidden_question { get; set; }
        public string group_order { get; set; }
        public string question_order { get; set; }
        public List<Verticallistarray1> Verticallistarray1 { get; set; }
        public List<Vertical_calculation_dtl> Vertical_calculation_dtl { get; set; }
    }

    public class Verticallistarray1
    {
        public string creditquestionrule_gid { get; set; }
        public string questionlistoption_gid { get; set; }
        public string list_name { get; set; }
        public string Score { get; set; }
    }

    public class Vertical_calculation_dtl
    {
        public string question { get; set; }
        public string question_gid { get; set; }
        public string field_type { get; set; }
        public string constantvalue { get; set; }
        public string operations { get; set; }
        public string grouptitle_gid { get; set; }
        public string grouptitle_name { get; set; }
        public string simplify_key { get; set; }
    }
    public class Arraycalculation1
    {
        public string key { get; set; }
        public decimal value { get; set; }
    }
    public class Calculationinfo1
    {
        public string grouptitle_gid { get; set; }
        public string verticalquestionrule_gid { get; set; }
        public string question_gid { get; set; }
        public string final_score { get; set; }
    }
    public class GroupTitle_list1
    {
        public string grouptitle_gid { get; set; }
        public string grouptitle_name { get; set; }
        public string final_scoredisplay { get; set; }
        public string applicanttype { get; set; }
        public List<GroupQuestion_list1> GroupQuestion_list1 { get; set; }
    }

    public class GroupQuestion_list1 : result
    {
        public string verticalquestionrule_gid { get; set; }
        public string vertical_gid { get; set; }
        public string grouptitle_gid { get; set; }
        public string question { get; set; }
        public string answer_type { get; set; }
        public string Score { get; set; }
        public string number_score { get; set; }
        public string calculation_formula { get; set; }
        public string field_number { get; set; }
        public string final_score { get; set; }
        public string final_scoredisplay { get; set; }
        public string actual_number { get; set; }
        public List<DropdownListArray1> DropdownListArray1 { get; set; }
    }
    public class DropdownListArray1
    {
        public string verticalquestionrule_gid { get; set; }
        public string verticalquestionlistoption_gid { get; set; }
        public string list_name { get; set; }
        public string Score { get; set; }
    }

    public class MdlTrnVertical : result
    {
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
        public string scorecard_submit { get; set; }
        public string application_gid { get; set; }
        public string editruletype_gid { get; set; }

    }
}