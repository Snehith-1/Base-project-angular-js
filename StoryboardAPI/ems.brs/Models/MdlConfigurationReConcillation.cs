using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.brs.Models
{
    /// <summary>
    /// To add,edit the each bank template row - column dynamically fetch master
    /// </summary>
    /// <remarks>Written by Motches </remarks>
    public class MdlConfigurationReConcillation : result
    {
        public string brsbank_gid { get; set; }
        public string transc_id { get; set; }
        public string datastart_row { get; set; }
        public string acc_no { get; set; }
        public string trn_date { get; set; }
        public string value_date { get; set; }
        public string payment_date { get; set; }
        public string transact_particulars { get; set; }
        public string debit_amt { get; set; }
        public string credit_amt { get; set; }
        public string transact_val { get; set; }
        public string remarks { get; set; }
        public string custref_no { get; set; }
        public string branch_name { get; set; }
        public string balance_amt { get; set; }
        public string chq_no { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string cr_dr { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public string knockoffby_finance { get; set; }
        public List<configuration_list> configuration_summary { get; set; }



        public class configuration_list : result

        {
            public string bank_gid { get; set; }
            public string bank_name { get; set; }
            public string acc_no { get; set; }
            public string custref_no { get; set; }
            public string branch_name { get; set; }
            public string created_by { get; set; }
            public string created_date { get; set; }
            public string bankconfig_gid { get; set; }
            public string transc_id { get; set; }
            public string datastart_row { get; set; }
            public string trn_date { get; set; }
            public string value_date { get; set; }
            public string payment_date { get; set; }
            public string transact_particulars { get; set; }
            public string debit_amt { get; set; }
            public string credit_amt { get; set; }
            public string transact_val { get; set; }
            public string balance_amt { get; set; }
            public string remarks { get; set; }
            public string bank_id { get; set; }
            public string chq_no { get; set; }
            public string updated_by { get; set; }
            public string cr_dr { get; set; }
            public string updated_date { get; set; }
            public string knockoffby_finance { get; set; }


        }
        public string acc_norow { get; set; }
        public string acc_nocol { get; set; }
        public string custref_norow { get; set; }
        public string custref_nocol { get; set; }
        public string transc_idrow { get; set; }
        public string transc_idcol { get; set; }
        public string trn_daterow { get; set; }
        public string trn_datecol { get; set; }
        public string value_daterow { get; set; }
        public string value_datecol { get; set; }
        public string payment_daterow { get; set; }
        public string payment_datecol { get; set; }
        public string transact_particularsrow { get; set; }
        public string transact_particularscol { get; set; }
        public string debit_amtrow { get; set; }
        public string debit_amtcol { get; set; }
        public string credit_amtrow { get; set; }
        public string credit_amtcol { get; set; }
        public string transact_valrow { get; set; }
        public string transact_valcol { get; set; }
        public string branch_namerow { get; set; }
        public string branch_namecol { get; set; }
        public string balance_amtrow { get; set; }
        public string balance_amtcol { get; set; }
        public string chq_norow { get; set; }
        public string chq_nocol { get; set; }
        public string cr_drrow { get; set; }
        public string cr_drcol { get; set; }
        public string bank_name { get; set; }
        public string bankconfig_gid { get; set; }
        public string knockoffby_financerow { get; set; }
        public string knockoffby_financecol { get; set; }


    }
}