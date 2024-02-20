using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.hrloan.Models
{
    public class MdlMstHRLoanHRDocument : result
    {
        public List<hrloanhrdocument_list> hrloanhrdocument_list { get; set; }
        public List<document_list> document_list { get; set; }
        public List<hrdocumentseverity_list> hrdocumentseverity_list { get; set; }
        public List<hrdocument_list> hrdocument_list { get; set; }
        public List<variety_list> variety_list { get; set; }
        public List<checklist_list> checklist_list { get; set; }
        public string variety_gid { get; set; }

    }
        
        public class hrloanhrdocument_list
        {
        public string hrdocument_gid { get; set; }
        public string hrdocument_name { get; set; }
        public string hrloantypeoffinancialassistance_gid { get; set; }
        public string hrloantypeoffinancialassistance_name { get; set; }
        public string hrloanseverity_gid { get; set; }
        public string hrloanseverity_name { get; set; }         
        public string hrdocumentchecklist_gid { get; set; }
        public string hrdocumentchecklist_name { get; set; }
        public string lms_code { get; set; }        
        public string bureau_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }       
        public List<checklist_list> checklist_list { get; set; }

        public string variety_name { get; set; }        
        public string variety_gid { get; set; }
        }
        public class hrloanhrdocument  : result
        {
        public string hrdocument_gid { get; set; }
        public string hrdocument_name { get; set; }
        public string lms_code { get; set; }        
        public string bureau_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public string Status { get; set; }
        public string remarks { get; set; }
        public char rbo_status { get; set; }
        public string variety_gid { get; set; }
        
    }
    public class document_list
    {
        public string hrloantypeoffinancialassistance_gid { get; set; }
        public string hrloantypeoffinancialassistance_name { get; set; }        
    }  
        public class hrdocumentseverity_list
        {
        public string hrloanseverity_gid { get; set; }
        public string hrloanseverity_name { get; set; }
        }
    public class hrdocument_list
    {
        public string hrdocument_gid { get; set; }
        public string hrdocument_name { get; set; }
    }
    public class hrdocument : result
    {
        public string hrdocument_gid { get; set; }
        public string hrdocument_name { get; set; }        
        public string hrloantypeoffinancialassistance_gid { get; set; }
        public string hrloantypeoffinancialassistance_name { get; set; }
        public string hrloanseverity_gid { get; set; }
        public string hrloanseverity_name { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string Status { get; set; }
    }

    public class HRLoanHRDocumentInactiveHistory : result
        {
            public List<hrdocumentinactivehistory_list> hrdocumentinactivehistory_list { get; set; }
        }   

        public class hrdocumentinactivehistory_list
        {
            public string status { get; set; }
            public string remarks { get; set; }
            public string updated_by { get; set; }
            public string updated_date { get; set; }
        }

    public class variety : result
    {
        public string hrdocument_gid { get; set; }
        public string hrdocumentchecklist_gid { get; set; }
        public string variety_gid { get; set; }        
        public string variety_name { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public List<variety_list> variety_list { get; set; }
       
    }
    public class variety_list
    {
        public string hrdocument_gid { get; set; }
        public string hrdocumentchecklist_gid { get; set; }
        public string variety_gid { get; set; }
        public string variety_name { get; set; }       
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string deleted_date { get; set; }
        public string deleted_by { get; set; }
    }

        public class documenttype_list
        {
            public string hrloantypeoffinancialassistance_gid { get; set; }
            public string hrloantypeoffinancialassistance_name { get; set; }
        }
    
        //check list
        public class checklist : result
        {
            public string individualdocument_gid { get; set; }
            public string individualchecklist_gid { get; set; }
            public string hrdocumentchecklist_name { get; set; }
            public string created_date { get; set; }
            public string created_by { get; set; }
            public string employee_gid { get; set; }
            public string employee_name { get; set; }
            public string updated_date { get; set; }
            public string updated_by { get; set; }

            public List<checklist_list> checklist_list { get; set; }           

            public string hrdocument_gid { get; set; }
            public string hrdocumentchecklist_gid { get; set; }

        }

        public class checklist_list
        {
            public string individualdocument_gid { get; set; }
            public string individualchecklist_gid { get; set; }
            public string hrdocumentchecklist_name { get; set; }
            public string created_date { get; set; }
            public string created_by { get; set; }
            public string employee_gid { get; set; }
            public string employee_name { get; set; }
            public string updated_date { get; set; }
            public string updated_by { get; set; }
            public string deleted_date { get; set; }
            public string deleted_by { get; set; }
            
            public string hrdocument_gid { get; set; }
            public string hrdocumentchecklist_gid { get; set; }
        }      
      
        public class documenttype : result
        {
            public string hrloantypeoffinancialassistance_gid { get; set; }
            public string hrdocumenttype_code { get; set; }
            public string hrloantypeoffinancialassistance_name { get; set; }
            public string description { get; set; }
            public string remarks { get; set; }
            public string created_date { get; set; }
            public string created_by { get; set; }
            public string Status { get; set; }
            public char rbo_status { get; set; }
        }
}