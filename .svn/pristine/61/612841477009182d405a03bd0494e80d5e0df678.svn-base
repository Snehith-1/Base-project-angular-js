using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.idas.Models
{
    public class MdlIdasCourierManagement : result
    {
        public List<CourierMgmt> CourierMgmt { get; set; }
        public List<CourierAckPending> CourierAckPending { get; set; }
    }
    public class CourierAckPending : result
    {
        public string courierMgmt_gid { get; set; }
        public string courierref_no { get; set; }
        public string date_of_courier { get; set; }
        public string ack_status { get; set; }
        public string ack_date { get; set; }
        public string ackby_name { get; set; }
        public string courier_type { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string dateof_courier { get; set; }
    }
    public class CourierMgmt : result
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
        public List<uploadcourierdocument> uploadcourierdocument { get; set; }
        public List<MdlCourierByList> MdlCourierByList { get; set; }
        public List<MdlCourierToList> MdlCourierToList { get; set; }
        public List<MdlEmployeeList> MdlEmployee { get; set; }
    }
    public class uploadcourierdocumentlist : result
    {
        public List<uploadcourierdocument> uploadcourierdocument { get; set; }

    }

    public class MdlCourierByList
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }

    }
    public class MdlCourierToList
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }

    }

    public class MdlEmployeeList
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }

    }

    public class uploadcourierdocument : result
    {
        public string courierdocument_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string document_title { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class courier_count
    {
        public string courier_inward { get; set; }
        public string courier_outward { get; set; }
        public string physical_inward { get; set; }
        public string physical_outward { get; set; }
    }

    public class MdlCourierAck
    {
        public string CourierMgmt_gid { get; set; }
        public string employee_gid { get; set; }
    }

   
}