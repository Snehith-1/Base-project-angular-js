using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.rsk.Models
{
    public class result
    {
        public bool status { get; set; }
        public string message { get; set; }
    }

    public class resultsample
    {
        public bool status { get; set; }
        public string message { get; set; }

    }

    public class sanctiondetailsList : result
    {
        public List<sanctiondetails> sanctiondetails { get; set; }
    }


    public class sanctiondetails : result
    {
        public string customer2sanction_gid { get; set; }
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string sanction_refno { get; set; }
        public string sanction_date { get; set; }
        public DateTime sanctionDate { get; set; }
        public string sanction_amount { get; set; }
        public string sanction_limit { get; set; }
        public string sanction_validity { get; set; }
        public string expiry_date { get; set; }
        public string review_date { get; set; }
        public string approval_authority { get; set; }
        public string natureof_proposal { get; set; }
        public string constitution { get; set; }
        public string authorized_signatory { get; set; }
        public string revisied_limit { get; set; }
        public string existing_limit { get; set; }
        public string escrow_account { get; set; }
        public string facility_type { get; set; }
        public string specific_conditions { get; set; }
        public string authorizedsignatoryname { get; set; }
        public string tenure_months { get; set; }
        public string customercode { get; set; }
        public string customername { get; set; }
        public string contactperson { get; set; }
        public string mobileno { get; set; }
        public string contactnumber { get; set; }
        public string email { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string region { get; set; }
        public string state { get; set; }
        public string postalcode { get; set; }
        public string country { get; set; }
        public string zonalGid { get; set; }
        public string businessHeadGid { get; set; }
        public string relationshipMgmtGid { get; set; }
        public string clustermanagerGid { get; set; }
        public string creditmanagerGid { get; set; }
        public string tomail { get; set; }
        public string ccmail { get; set; }
        public string zonal_name { get; set; }
        public string businesshead_name { get; set; }
        public string cluster_manager_name { get; set; }
        public string relationshipmgmt_name { get; set; }
        public string creditmanager_name { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_code { get; set; }
        public string state_gid { get; set; }
        public string customer_urn { get; set; }
        public List<documentationname> documentationname { get; set; }
    }

    

    public class documentationname
    {
        public string customer2document_gid { get; set; }
        public string documentation_refno { get; set; }
        public string documentation_name { get; set; }
        public string documentation_type { get; set; }
        public string documentrefname { get; set; }
    }

    public class idassanctiondocumentList : result
    {
        public List<idassanctiondocument> idassanctiondocument { get; set; }
    }

    public class idassanctiondocument : result
    {
        public string customer2sanction_gid { get; set; }
        public string sanctiondocument_gid { get; set; }
        public string document_gid { get; set; }
        public string document_code { get; set; }
        public string document_name { get; set; }
        public string document_flag { get; set; }
        public string file_name { get; set; }
        public string file_path { get; set; }
    }

    public class sanctionviewdtl:result
    {
        public  string customer2sanction_gid { get; set; }
        public string sanction_refno { get; set; }
        public string sanction_date { get; set; }
        public string facility_type { get; set; }
        public string customer_gid { get; set; }
        public string customername { get; set; }
        public string customer_urn { get; set; }
        public string zonal_name { get; set; }
        public string vertical_code { get; set; }
        public string businesshead_name { get; set; }
        public string relationshipmgmt_name { get; set; }
        public string cluster_manager_name { get; set; }
        public string creditmanager_name { get; set; }
        public string collateral_security { get; set; }
        public string mobileno { get; set; }
        public string sanction_amount { get; set; }
        public string riskmanager { get; set; }
    }

    public class rsksanctiondocumentList : result
    {
        public List<rsksanctiondocument> rsksanctiondocument { get; set; }
    }

    public class rsksanctiondocument : result
    {
        public string customer2sanction_gid { get; set; }
        public string rsksanction_documentgid { get; set; }
        public string tmpdocumentation_gid { get; set; }
        public string documentation_gid { get; set; }
        public string documentaion_code { get; set; }
        public string documentation_name { get; set; }
        public string document_name { get; set; }
        public string document_flag { get; set; }
        public string file_name { get; set; }
        public string file_path { get; set; }
        public string document_remarks { get; set; }
    }

    public class sanctiondcoument : result
    {
        public string document_remarks { get; set; }
        public string customer2sanction_gid { get; set; }
        public string customer_gid { get; set; }
        public string rsksanction_documentgid { get; set; }
        public string customer2document_gid { get; set; }
        public string tmpsanction_documentgid { get; set; }
        public string file_name { get; set; }
        public string file_path { get; set; }
               

    }

    public class sanctiondocumentlist
    {
        public string customer2document_gid { get; set; }        
        public string documentationname { get; set; }
    }

    public class uploadidassanctiondocument : result
    {
        public string customer2sanction_gid { get; set; }
        public string sanctiondocument_gid { get; set; }
        public string file_name { get; set; }
        public List<Sanctiondoc_upload> Sanctiondoc_upload { get; set; }

    }

    public class uploadrsksanctiondocument : result
    {
        public string customer2sanction_gid { get; set; }
        public string documentation_gid { get; set; }
        
    }

    public class Sanctiondoc_upload : result
    {
        public string tmpsanction_documentgid { get; set; }
        public string customer2sanction_gid { get; set; }
        public string file_path { get; set; }
        public string file_name { get; set; }
          
       
    }
}