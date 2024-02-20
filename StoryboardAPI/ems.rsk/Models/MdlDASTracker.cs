using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.rsk.Models
{

    public class listacknowledgedbuyers : result
    {
        public List<acknowledgedbuyers> acknowledgedbuyers { get; set; }
    }

    public class acknowledgedbuyers : result
    {
        public string acknowledgedbuyers_gid { get; set; }
        public string customer_gid { get; set; }
        public string acknowledged_buyers { get; set; }
    }

    public class listremitterbuyers : result
    {
        public List<remitterbuyers> remitterbuyers { get; set; }
    }

    public class remitterbuyers : result
    {
        public string remitterbuyers_gid { get; set; }
        public string customer_gid { get; set; }
        public string remitter_status { get; set; }
        public string remitter_ackbuyersgid { get; set; }
        public string remitter_ackbuyers { get; set; }
        public string remitter_unackbuyers { get; set; }
        public string remitter_self { get; set; }
        public string remitter_buyer { get; set; }
    }


}