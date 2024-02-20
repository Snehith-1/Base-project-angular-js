using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ems.its.Models
{
    public class viewserviceticket : result
    {
        public List<viewservice_list> viewservice_list { get; set; }
    }

    public class viewservice_list
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
        public string response_status { get; set; }
        public string response_new { get; set; }
        public string raised_by { get; set; }
    }
    public class viewdocument : result
    {
        public List<viewdocument_list> viewdocument_list { get; set; }
    }
    public class viewdocument_list : result
    {
        public string complaint_gid { get; set; }
        public string document_id { get; set; }
        public string document_name { get; set; }
    }

    public class closeticket : result
    {
        public string complaint_gid { get; set; }
    }

    public class incompleteticket : result
    {
        public string complaint_gid { get; set; }
    }

    // View Uploaded Documents
    public class viewDocument : result
    {
        public List<viewDocumentList> viewDocumentList { get; set; }
    }
    public class viewDocumentList
    {
        public string ticketdocument_gid { get; set; }
        public string complaint_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
    }
}
