using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.ecms.Models
{
    /// <summary>
    /// customerAlertGenerate Controller Class containing API methods for accessing the  Model class MdlCustomerAlert
    /// To get mail History View, Generate mail content, Mail mangament, Get customer mail details, Send mail , History of send mails
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    public class MdlCustomerAlert : result
    {
        public string customer_gid { get; set; }
        public string template_content { get; set; }
        public string[] deferral_gid { get; set; }
        public List<customermail_list> customermail_list { get; set; }
    }

    public class customermail_list : result
    {
        public string customer_gid { get; set; }
        public string customeralert_gid { get; set; }
        public string customercode { get; set; }
        public string customername { get; set; }
        public string contactperson { get; set; }
        public string vertical_code { get; set; }
        public string zonalGid { get; set; }
        public string businessHeadGid { get; set; }
        public string relationshipMgmtGid { get; set; }
        public string clustermanagerGid { get; set; }
        public string creditmanagerName { get; set; }
        public string generated_date { get; set; }
        public string mail_status { get; set; }
        public string mail_count { get; set; }
        public string mailsent_date { get; set; }
        public string startpenality_flag { get; set; }
    }
    public class mailalert : result
    {
        public string customer_gid { get; set; }
        public string customeralert_gid { get; set; }
        public string customercode { get; set; }
        public string customername { get; set; }
        public string contactperson { get; set; }

        public string addressline1edit { get; set; }
        public string addressline2edit { get; set; }
        public string mobileNoedit { get; set; }


        public string vertical_code { get; set; }
        public string zonalGid { get; set; }
        public string businessHeadGid { get; set; }
        public string relationshipMgmtGid { get; set; }
        public string clustermanagerGid { get; set; }
        public string creditmanagerName { get; set; }
        public string content { get; set; }
        public string content_below { get; set; }
        public string from_mail { get; set; }
        public string to_mail { get; set; }
        public string cc_mail { get; set; }
        public string customer_urn { get; set; }
        public string penality_alertdate { get; set; }
        public List<mailalert_list> mailalert_list { get; set; }
        public List<penalityalert_list> penalityalert_list { get; set; }
        public List<mailhistorydeferral_list> mailhistorydeferral_list { get; set; }
    }

    public class mailalert_list : result
    {
        public string deferral_gid { get; set; }
        public string record_id { get; set; }
        public string tracking_type { get; set; }
        public string deferral_name { get; set; }
        public string deferral_category { get; set; }
        public string due_date { get; set; }
        public string deferral_status { get; set; }
        public string approval_status { get; set; }
        public string aging { get; set; }
        public string remarks { get; set; }
        public string customer_remarks { get; set; }
        public string customeralert_sentdate { get; set; }
        public string extend_flag { get; set; }
        public string extend_date { get; set; }
    }

    public class penalityalert_list
    {
        public string penalityalert_start { get; set; }
        public string penalityalert_end { get; set; }
        public string created_by { get; set; }
        public string penalitymessage { get; set; }
    }

    public class mdlmailHistory : result
    {
        public string customer_gid { get; set; }
        public string customercode { get; set; }
        public string customername { get; set; }
        public string content { get; set; }
        public string contactperson { get; set; }

        public string addressline1edit { get; set; }
        public string addressline2edit { get; set; }
        public string mobileNoedit { get; set; }
        public string vertical_code { get; set; }
        public string zonalGid { get; set; }
        public string businessHeadGid { get; set; }
        public string relationshipMgmtGid { get; set; }
        public string clustermanagerGid { get; set; }
        public string creditmanagerName { get; set; }
        public List<mailhistory_list> mailhistory_list { get; set; }
        public List<mailhistorydeferral_list> mailhistorydeferral_list { get; set; }
    }

    public class mailhistory_list : result
    {
        public string customermail_gid { get; set; }
        public string customeralert_gid { get; set; }
        public string customer_gid { get; set; }
        public string customercode { get; set; }
        public string customername { get; set; }
        public string sent_by { get; set; }
        public string sent_date { get; set; }
    }
    public class mailhistorydeferral_list : result
    {
        public string deferral_gid { get; set; }
        public string customeralert_gid { get; set; }
        public string customer_gid { get; set; }
        public string record_id { get; set; }
        public string tracking_type { get; set; }
        public string deferral_type { get; set; }
        public string deferral_category { get; set; }
        public string due_date { get; set; }
        public string deferral_status { get; set; }
        public string approval_status { get; set; }
        public string aging { get; set; }
        public string remarks { get; set; }
        public string customeralert_sentdate { get; set; }
        public string deferral_name { get; set; }
    }
  
}