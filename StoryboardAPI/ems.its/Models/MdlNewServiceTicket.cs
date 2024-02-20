using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ems.its.Models
{
    public class result
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    // New Service Ticket....//

    public class category : result
    {
        public List<category_list> category_list { get; set; }
    }
    public class category_list
    {
        public string category_gid { get; set; }
        public string category_name { get; set; }
    }

    public class subcategory : result
    {
        public string department_approval { get; set; }
        public string management_approval { get; set; }
        public string service_approval { get; set; }
        public string approval_flag { get; set; }     
        public List<subcategory_list> subcategory_list { get; set; }
    }
    public class subcategory_list
    {
        public string subcategory_gid { get; set; }
        public string subcategory_name { get; set; }
    }
    public class type : result
    {
        public List<type_list> type_list { get; set; }
    }
    public class type_list
    {
        public string type_gid { get; set; }
        public string type_name { get; set; }
    }
    public class employee : result
    {
        public List<employee_list> employee_list { get; set; }
    }
    public class employee_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string employee_id { get; set; }
    }
    public class UploadDocumentname : result
    {
        public List<UploadDocumentModel> filename_list { get; set; }
    }

    public class UploadDocumentModel
    {
        public string filename { get; set; }
        public string id { get; set; }
    }

    public class documentcancel : result
    {

    }
    public class raisesubmit : result
    {
        public string category_gid { get; set; }
        public string subcategory_gid { get; set; }
        public string type_gid { get; set; }
        public string raisedfor { get; set; }
        public string raisedemployee { get; set; }
        public string txtcontact_number { get; set; }
        public string txtemail_address { get; set; }
        public string txt_title { get; set; }
        public string txt_remarks { get; set; }
        public string file_name { get; set; }
        public string othersemployee_gid { get; set; }
    }


    public class employeecontactlist : result
    {
        public string employee_emailid { get; set; }
        public string employee_mobileno { get; set; }
        public string department_approval { get; set; }
        public string management_approval { get; set; }
        public string service_approval { get; set; }
        public string approval_flag { get; set; }
        public string reporting_to { get; set; }
    }

    public class employeecontact_getgid : result
    {
        public string employee_gid { get; set; }
        public string employee { get; set; }
        public string employee_emailid { get; set; }
        public string employee_mobileno { get; set; }
        public string department_approval { get; set; }
        public string management_approval { get; set; }
        public string service_approval { get; set; }
        public string approval_flag { get; set; }
        public string reporting_to { get; set; }

    }

    // Temp Clear...//
    public class cleartmpdocument : result
    {

    }

    public class viewpop_ticket : result
    {
        public List<viewticket_details> viewticket_details { get; set; }
    }
    public class viewticket_details : result
    {
        public string complaint_gid { get; set; }
        public string complaint_refno { get; set; }
        public string complaint_date { get; set; }
        public string complaint_title { get; set; }
        public string complaint_remarks { get; set; }
        public string user_firstname { get; set; }
        public string category_name { get; set; }
        public string subcategory_name { get; set; }
        public string type_name { get; set; }
        public string raisedfor_employee { get; set; }
        public string status_view { get; set; }
        public string leadstage_name { get; set; }
        public string department_manager { get; set; }
        public string manager { get; set; }
        public string service_manager { get; set; }
        public string approval_status { get; set; }
        public string remarks { get; set; }
        public string approvalstatus { get; set; }
        public string department_approval { get; set; }
        public string department_approveddate { get; set; }
        public string department_approvedstatus { get; set; }
        public string management_approval { get; set; }
        public string management_approveddate { get; set; }
        public string management_approvedstatus { get; set; }
        public string service_approval { get; set; }
        public string service_approveddate { get; set; }
        public string service_approvedstatus { get; set; }
        public string departmentrejected_remarks { get; set; }
        public string servicerejected_remarks { get; set; }
        public string managementrejected_remarks { get; set; }
    }

    // Submit Response Log...//
    public class responselog : result
    {
        public string complaint_gid { get; set; }
        public string response_description { get; set; }
    }

    // Response Log Details...//
    public class responselogdetails : result
    {
        public List<responselog_detailslist> responselog_detailslist { get; set; }
    }
    public class responselog_detailslist : result
    {
        public string complaint_gid { get; set; }
        public string responselog_gid { get; set; }
        public string complaint_raisedby { get; set; }
        public string complaint_assigned { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string response_description { get; set; }
        public string session_user { get; set; }
    }

}
