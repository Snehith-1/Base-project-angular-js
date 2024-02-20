using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.master.Models
{
    public class MdlCourierAckDtl : result
    {
        public string courierMgmt_gid { get; set; }
        public string courierref_no { get; set; }
        public string date_of_courier { get; set; }
        public string sanction_gid { get; set; }
        public string sanctionref_no { get; set; }
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string document_type { get; set; }
        public string pod_no { get; set; }
        public string couriercompany_name { get; set; }
        public string sender_name { get; set; }
        public string courierhandover_to { get; set; }
        public string address { get; set; }
        public string ack_status { get; set; }
        public string ack_date { get; set; }
        public string ackby_name { get; set; }
        public string courier_type { get; set; }
        public string remarks { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string dateof_courier { get; set; }       
    }
    public class MdlMstCourierAck
    {
        public string CourierMgmt_gid { get; set; }
        public string employee_gid { get; set; }
    }
}