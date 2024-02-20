using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ems.utilities.Models;
namespace ems.lgl.Models
{
    public class mdllglDashboard : result
    {
        public string year_date { get; set; }
        public string customername { get; set; }
        public string month_date { get; set; }
        public List<lawyer_empanelment> lawyer_empanelment { get; set; }
        public List<legal_services> legal_services { get; set; }
        public List<legal_compliance> legal_compliance { get; set; }
        public List<legalSR_list> legalSR_list { get; set; }
        public List<customers_list> customers_list { get; set; }
        //public List<month_list> month_list { get; set; }
        //public List<year_list> year_list { get; set; }
        public List<Legalreportsummary> Legalreportsummary { get; set; }


    


    }
    public class lawyer_empanelment
    {
        public string privilege_gid { get; set; }
    }
    public class legal_services
    {
        public string legalservices_gid { get; set; }
    }
    public class legal_compliance
    {
        public string legalcompliance_gid { get; set; }
    }
    public class legalSR_list:result
    {
       
            public string lspath { get; set; }
            public string lsname { get; set; }
        public string legalsr_gid { get; set; }
        public string auth_date { get; set; }
        public string srref_no { get; set; }
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string customer_urn { get; set; }
        public string constitution { get; set; }
        public string financed_by { get; set; }
        public string raised_by { get; set; }
        public string raised_date { get; set; }
        public string raised_by_department { get; set; }
        public string auth_status { get; set; }
        public string auth_remarks { get; set; }
        public string approval_status { get; set; }
        public string created_date { get; set; }
        

    }
    public class customers_list
    {
        public string customer_gid { get; set; }
        public string legalsr_gid { get; set; }
        public string auth_date { get; set; }
        public string srref_no { get; set; }
        public string customer_name { get; set; }
        public string customer_urn { get; set; }
        public string constitution { get; set; }
        public string financed_by { get; set; }
        public string raised_by { get; set; }
        public string created_date { get; set; }
        public string raised_by_department { get; set; }
        public string auth_status { get; set; }
        public string auth_remarks { get; set; }
        public string approval_status { get; set; }

    }
    //public class month_list
    //{
    //    public string customer_gid { get; set; }
    //    public string legalsr_gid { get; set; }
    //    public string auth_date { get; set; }
    //    public string srref_no { get; set; }
    //    public string customer_name { get; set; }
    //    public string customer_urn { get; set; }
    //    public string constitution { get; set; }
    //    public string financed_by { get; set; }
    //    public string raised_by { get; set; }
    //    public string raised_date { get; set; }
    //    public string raised_by_department { get; set; }
    //    public string auth_status { get; set; }
    //    public string auth_remarks { get; set; }
    //    public string approval_status { get; set; }
    //    public string created_date { get; set; }

    //}
    //public class year_list
    //{
    //    public string customer_gid { get; set; }
    //    public string legalsr_gid { get; set; }
    //    public string auth_date { get; set; }
    //    public string srref_no { get; set; }
    //    public string customer_name { get; set; }
    //    public string customer_urn { get; set; }
    //    public string constitution { get; set; }
    //    public string financed_by { get; set; }
    //    public string raised_by { get; set; }
    //    public string raised_date { get; set; }
    //    public string raised_by_department { get; set; }
    //    public string auth_status { get; set; }
    //    public string auth_remarks { get; set; }
    //    public string approval_status { get; set; }
    //    public string created_date { get; set; }

    //}

    public class Legalreportsummary
    {

        public string customer_gid { get; set; }
        public string legalsr_gid { get; set; }
        public string auth_date { get; set; }
        public string srref_no { get; set; }
        public string customername { get; set; }
        public string customer_urn { get; set; }
        public string constitution { get; set; }
        public string financed_by { get; set; }
        public string raised_by { get; set; }
        public string created_date { get; set; }
        public string raised_by_department { get; set; }
        public string auth_status { get; set; }
        public string auth_remarks { get; set; }
        public string approval_status { get; set; }




    }
}