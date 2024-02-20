using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///(It's used for ApplicationVisitReport in Samfin)ApplicationVisitReport Model Class accessed by API methods from related DataAccess class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash</remarks>

namespace ems.master.Models
{
    public class MdlMstVisitPerson : result
    {
        public string applicationvisit_gid { get; set; }
        public string application_gid { get; set; }
        public string applicationvisit_date { get; set; }
        public DateTime applicationvisitdate { get; set; }
        public string clientkmp_activities{get;set;}
        public string promoter_background{get;set;}
        public string overall_observations{get;set;}
        public string inspectingofficial_recommenation{get;set;}
        public string trading_relationship{get;set;}
        public string summary{get;set;}
        public string draft_flag { get; set; }
        public string statusupdated_by { get; set; }
        public string visitreport_id { get; set; }
        public List <mdlvisitdone> mdlvisitdone { get; set; }
        public List<visitdone_list> visitdone_list { get; set; }
        public List <mstinspectingofficials> mstinspectingofficials { get; set; }
        public List <inspectemployee_list> employeelist { get; set; }
        public List<mstVisitpersondtl_list> mstVisitpersondtl_list { get; set; }
        public List<mstVisitpersonaddress_list> mstVisitpersonaddress_list { get; set; }
        public List<VisitReportList> VisitReportList { get; set; }
        public List<UploadDocumentList> UploadDocumentList { get; set; }
        public List<UploadphotoList> UploadphotoList { get; set; }
    

    }
    public class inspectemployee_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class VisitReportList : result
    {
        public string draft_flag { get; set; }
        public string visitreport_gid { get; set; }
        public string visitreport_date { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string visitreport_id { get; set; }
    }
    public class mstinspectingofficials : result
    {
        public string applicationvisit2inspectingofficial_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class mdlvisitdone : result
    {
        public string applicationvisit2visitdone_gid { get; set; }
        public string visitdone_gid { get; set; }
        public string visitdone_name { get; set; }
    }

    public class visitdone_list : result
    {
        public string applicationvisit2visitdone_gid { get; set; }
        public string visitdone_gid { get; set; }
        public string visitdone_name { get; set; }
    }
    public class mstVisitpersondtl_list :result
    {
        public string applicationvisit_gid { get; set; }
        public string applicationvisit2person_gid { get; set; }
        public string clientrepresentative_name { get; set; }
        public string clientrepresentative_designationgid { get; set; }
        public string clientrepresentative_designationname { get; set; }
        public string clientrepresentative_personalmail { get; set; }      
        public string clientrepresentative_officemail { get; set; }     
        public List<mstVisitpersoncontact_list> mstVisitpersoncontact_list { get; set; }
    }
    public class mstVisitpersoncontact_list : result
    {
        public string applicationvisitperson2contact_gid { get; set; }
        public string applicationvisit_gid { get; set; }
        public string mobile_no { get; set; }        
        public string whatsapp_mobileno { get; set; }
        public string primary_status { get; set; }       
    }
    public class mstVisitpersonaddress_list : result
    {
        public string applicationvisit2address_gid { get; set; }
        public string applicationvisit_gid { get; set; }
        public string addresstype_gid { get; set; }
        public string addresstype_name { get; set; }
        public string primary_status { get; set; }
        public string address_line1 { get; set; }
        public string address_line2 { get; set; }
        public string landmark { get; set; }
        public string postal_code { get; set; }
        public string city { get; set; }
        public string taluk { get; set; }
        public string district { get; set; }
        public string state_gid { get; set; }
        public string state_name { get; set; }
        public string country { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
    }
    public class UploadDocumentList : result
    {
        public string applicationvisit2document_gid { get; set; }
        public string applicationvisit_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string photo_gid { get; set; }
        public string filename { get; set; }
        public string path { get; set; }
        public string created_date { get; set; }
        public string uploaded_by { get; set; }
        public string upload_by { get; set; }
        public string document_type { get; set; }
        public string updated_date { get; set; }
    }
    public class UploadphotoList :result
    {
        public string applicationvisit2photo_gid { get; set; }
        public string applicationvisit_gid { get; set; }
        public string photo_name { get; set; }
        public string document_path { get; set; }
        public string filename { get; set; }
        public string created_date { get; set; }
        public string uploaded_by { get; set; }
        public string upload_by { get; set; }
        public string document_type { get; set; }
        public string updated_date { get; set; }
    }

}