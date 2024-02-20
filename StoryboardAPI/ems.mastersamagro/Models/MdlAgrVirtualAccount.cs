using System.Collections.Generic;
using Newtonsoft.Json;

namespace ems.mastersamagro.Models
{

    /// <summary>
    /// This models will store values for virtual account for the onboarded customers
    /// </summary>
    /// <remarks>Written by Premchandar.K </remarks>

    public class MdlVirtualAccount
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string virtualaccount_number { get; set; }
    }

    public class MdlVACreationRequest
    {
        public string buyer_id { get; set; }
        public string virtualaccount_no { get; set; }
        public string created_by { get; set; }
    }

    public class MdlVACreationResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public string request_id { get; set; }

    }

    public class MdlVirtualAccountResponse
    {
        public bool status { get; set; }
        public string message { get; set; }

    }

    public static class VACreationStatus
    {
        public const string
              Failure = "N",
              Success = "Y"; 
    }

    public class VACreationMailCustomer
    {
        public string customer_name { get; set; }
        public string relationshipmanager_name { get; set; }
        public string customer_email { get; set; }
        public string relationshipmanager_email { get; set; }

    }

    public class VACreationConfirmationMailResponse
    {
        public bool result { get; set; }
        public string message { get; set; }
    }

    public class SendMailResponse
    {
        public bool result { get; set; }
        public string message { get; set; }
    }

}
