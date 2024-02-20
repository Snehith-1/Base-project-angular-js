using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.hbapiconn.Models
{
    public class MdlSamFinEncoreDisbursement
    {
    }

    public class MdlEncoreDisbursement : result1234
    {
        public string application_gid { get; set; }
        public string application2sanction_gid { get; set; }
        public string application2loan_gid { get; set; }
        public string customer_urn { get; set; }
        public string rmdisbursementrequest_gid { get; set; }
        public string farmercontact_gid { get; set; }
        public string disbursementsupplier_gid { get; set; }
        public string instrument_gid { get; set; }
        public string instrument { get; set; }

    }

    public class MdlEncoreDisbursementRequest
    {
        public transaction transaction { get; set; }

        public MdlEncoreDisbursementRequest()
        {
            transaction = new transaction();
        }
        public string safeMode { get; set; }
        public string runReversedTransactions { get; set; }
    }

    public class transaction
    {
        public string accountId { get; set; }
        public string customerId { get; set; }
        public string amount1 { get; set; }
        public string transactionName { get; set; }
        public string param2Str { get; set; }
        public string userId { get; set; }
        public string valueDateStr { get; set; }
        public string instrument { get; set; }
        public string remarks { get; set; }
        public string description { get; set; }
        public string transactionDateStr { get; set; }
        public string part1 { get; set; }
        public string part2 { get; set; }
        public string part3 { get; set; }
        public string part4 { get; set; }            
        public string part5 { get; set; }
        public string part6 { get; set; }
        public string part7 { get; set; }
        public string part8 { get; set; }
        public string payeeAccountId { get; set; }
        public string param5 { get; set; }
        public string reference { get; set; }
        public string param4 { get; set; }
    }

    public class MdlEncoreDisbursementResponse : result1234
    {

    }

}