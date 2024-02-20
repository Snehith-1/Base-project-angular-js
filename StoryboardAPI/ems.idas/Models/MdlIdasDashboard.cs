using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.idas.Models
{
    public class idasUserPrivilege:result 
    {
        public List<idasUserPrivilege_List> idasUserPrivilege_List { get; set; }
    }
    public class idasUserPrivilege_List
    {
        public string idasUserPrivilege { get; set; }

    }
    public class MdlCadDashboard : result
    {
        public List<caddashboard_list> caddashboard_list { get; set; }
        public List<customerupdation_list> customerupdation_list { get; set; }
        public List<sanction_list> sanction_list { get; set; }
        public List<sanctionupdation_list> sanctionupdation_list { get; set; }
        public List<lsamaker_list> lsamaker_list { get; set; }
        public List<lsachecker_list> lsachecker_list { get; set; }
        public List<collateralUpdation_list> collateralUpdation_list { get; set; }
        public List<deferralstage_list> deferralstage_list { get; set; }
        public List<deferralapproval_list> deferralapproval_list { get; set; }
        public List<cadcompliance_list> cadcompliance_list { get; set; }
        public List<doctagged_list> doctagged_list { get; set; }
        public List<deferralcreate_list> deferralcreate_list { get; set; }
        public List<docvettingmaker_list> docvettingmaker_list { get; set; }
        public List<docvettingchecker_list> docvettingchecker_list { get; set; }
    }
    public class caddashboard_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string count { get; set; }
    }
    public class customerupdation_list
    {
        public string customer_count { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class sanction_list
    {
        public string sanctioncreation_count { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class sanctionupdation_list
    {
        public string sanctionupdation_count { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class lsamaker_list
    {
        public string lsamaker_count { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class lsachecker_list
    {
        public string lsachecker_count { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class collateralUpdation_list
    {
        public string collateralupdation_count { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class deferralstage_list
    {
        public string deferralstage_count { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class deferralapproval_list
    {
        public string deferralapproval_count { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class cadcompliance_list
    {
        public string cadcompliance_count { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class doctagged_list
    {
        public string doctagged_count { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class deferralcreate_list
    {
        public string deferralcreate_count { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class docvettingmaker_list
    {
        public string docvettingmaker_count { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class docvettingchecker_list
    {
        public string docvettingchecker_count { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
   
}