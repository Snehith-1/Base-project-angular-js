using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.master.Models
{
    public class MdlBranchname : result
    {
        public List<branchname_list> branchname_list { get; set; }
    }
    public class branchname_list
    {
        public string branch_gid { get; set; }
        public string branch_name { get; set; }
    }
    public class MdlVisitor : result
    {
        public List<VisitorMobileNo> visitormobileno_list { get; set; }
        public List<VisitorEmailAddress> visitoremailaddress_list { get; set; }
        public List<VisitorName> visitorname_list { get; set; }
        public List<Visitor> visitor_list { get; set; }
        public List<VisitorUploadphotoList> VisitorUploadphotoList { get; set; }
        public string visitor_gid { get; set; }
        public string branch_gid { get; set; }
    }
    public class VisitorMobileNo : result
    {
        public List<VisitorMobileNo> visitormobileno_list;
        public string visitor2contact_gid { get; set; }
        public string visitor2mobileno_gid { get; set; }
        public string visitor_gid { get; set; }
        public string mobile_no { get; set; }
        public string primary_mobileno { get; set; }
        public string whatsapp_mobileno { get; set; }
        public string primary_status { get; set; }
        public string statusupdated_by { get; set; }
    }
    public class VisitorEmailAddress : result
    {
        public List<VisitorEmailAddress> visitoremailaddress_list;
        public string visitor2email_gid { get; set; }
        public string visitorname_gid { get; set; }
        public string email_address { get; set; }
        public string primary_emailaddress { get; set; }
        public string primary_status { get; set; }
        public string statusupdated_by { get; set; }
    }
    public class VisitorName : result
    {
        public List<VisitorName> visitorname_list;
        public string visitorname_gid { get; set; }
        public string visitor_gid { get; set; }
        public string visitor_name { get; set; }
        public string visitoridproof_no { get; set; }
        public string visitoridproof { get; set; }
        public string vaccination_status { get; set; }
        public string tag_id { get; set; }
        public string tag_validity_from { get; set; }
        public string tag_validity_to { get; set; }
        public string temperature { get; set; }
        public string statusupdated_by { get; set; }
        public string generate_status { get; set; }
        public string visitorcompany_name { get; set; }
        public string spo2 { get; set; }
        public string visitor_email { get; set; }
        public string visitor_mobileno { get; set; }
    }
    public class visitortag : result
    {
        public string visitorname_gid { get; set; }
        public string tag_id { get; set; }
        public string tag_validity_from { get; set; }
        public string tag_validity_to { get; set; }
    }
    public class Visitor : result
    {
        public List<Visitor> visitor_list;
        public DateTime Tin_time { get; set; }
        public DateTime Tactual_out_time { get; set; }
        public DateTime Ttentative_out_time { get; set; }
        public string generate_status { get; set; }
        public string visitor_gid { get; set; }
        public string visit_id { get; set; }
        public string branch_gid { get; set; }
        public string branch_name { get; set; }
        public string visiting_type { get; set; }
        public string in_time { get; set; }
        public string tentative_out_time { get; set; }
        public string visit_date { get; set; }        
        public string actual_out_time { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string purpose_of_visit { get; set; }
        public string visitor_name { get; set; }
        public string visitingofficial_name { get; set; }
        public string now_date { get; set; }
        public List<visitingofficer_name> visitingofficer_name { get; set; }
        public List<visitingofficerem_list> visitingofficerem_list { get; set; }  
        public string visitofficer_name { get; set; }
    }
    public class visitingofficer_name
    {
        public string visitingofficer_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class visitingofficerem_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class VisitorUploadphotoList : result
    {
        public List<VisitorUploadphotoList> VisitorUploadphoto_List;
        public string visitor2photo_gid { get; set; }
        public string visitor_gid { get; set; }
        public string photo_name { get; set; }
        public string document_path { get; set; }
        public string filename { get; set; }
        public string created_date { get; set; }
        public string uploaded_by { get; set; }
        public string upload_by { get; set; }
        public string document_type { get; set; }
        public string updated_date { get; set; }
    }
    public class VisitorCount
    {
        public string todayvisitor_count { get; set; }
        public string taggedvisitor_count { get; set; }
        public string visitorhistory_count { get; set; }
        public string totalvisitor_count { get; set; }
        public Int16 lstotalcount { get; set; }
    }
    public class VisitorNameView : result
    {
        public List<VisitorNameView> visitornameview_list;
        public string visitorname_gid { get; set; }
        public string visitor_gid { get; set; }
        public string visitor_name { get; set; }
        public string visitor_idproofno { get; set; }
        public string visitor_idproof { get; set; }
        public string vaccination_status { get; set; }
        public string tag_id { get; set; }
        public string tag_validity_from { get; set; }
        public string tag_validity_to { get; set; }
        public string temperature { get; set; }
        public string statusupdated_by { get; set; }
        public string generate_status { get; set; }
        public string visitorcompany_name { get; set; }
        public string spo2 { get; set; }
        public string visitor_email { get; set; }
        public string visitor_mobileno { get; set; }
    }
    public class MdlMstvisitordocview : result
    {       
        public List<UploadDocument_List> UploadDocument_List { get; set; }
    }
    public class UploadDocument_List
    {
        public string file_name { get; set; }
        public string visitphoto_path { get; set; }
        public string created_date { get; set; }
        public string uploaded_by { get; set; }
        public string upload_by { get; set; }
        public string updated_date { get; set; }
        public string visitphoto_name { get; set; }
        public string visitor2photo_gid { get; set; }
    }
    public class VisitorExport : result
    {
        public string lsname { get; set; }
        public string lspath { get; set; }
        public string lscloudpath { get; set; }
    }
}