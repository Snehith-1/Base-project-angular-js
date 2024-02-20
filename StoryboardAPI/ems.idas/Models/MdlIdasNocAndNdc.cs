using System;
using System.Collections.Generic;


namespace ems.idas.Models
{
    public class MdlIdasNocAndNdc : result
    {
        public string file_name { get; set; }
        public List<employee_list> employee_list { get; set; }
        public List<UploadNocDocumentList> UploadNocDocumentList { get; set; }
        public List<nocandndc_list> nocandndc_list { get; set; }
        public string nocandndc_gid { get; set; }
        public string maker_gid { get; set; }
        public string maker_name { get; set; }
        public string maker_name_edit { get; set; }
        public string checker_gid { get; set; }
        public string checker_name { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
        public string customer_name { get; set; }
        public string sanction_ref_no { get; set; }
        public string sanction_date { get; set; }
        public string loan_closure_date { get; set; }
        public DateTime sanction_Date { get; set; }
        public DateTime noc_issuance_Date { get; set; }
        public string loan_account_no { get; set; }
        public string noc_issuance_date { get; set; }
        public string checker_name_edit { get; set; }
        public string nocandndc_date { get; set; }
        public DateTime nocandndc_Date { get; set; }
        public string nocandndc_date_edit { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string content { get; set; }
        public string lspath { get; set; }
        public string lsname { get; set; }
        public int Years;
        public int Months;
        public int Days;
       

    }

    public class MdlDropDown : result
    {
        public List<vertical_list> vertical_list { get; set; }
    }

        public class nocandndc_list
    {
        public string remarks { get; set; }
        public string nocandndc_gid { get; set; }
        public string maker_gid { get; set; }
        public string maker_name { get; set; }
        public string checker_gid { get; set; }
        public string checker_name { get; set; }
        public string file_name { get; set; }
        public string created_date { get; set; }
        public string nocandndc_date { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
        public string customer_name { get; set; }
        public string sanction_ref_no { get; set; }
        public string sanction_date { get; set; }
        public string loan_account_no { get; set; }
        public string noc_issuance_date { get; set; }
        public string loan_closure_date { get; set; }
        public string created_by { get; set; }
        public string status { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string deleted_date { get; set; }
        public string deleted_by { get; set; }
        public List<employee_list> employee_list { get; set; }
    }
    public class nocandndc : result
    {
        public string remarks { get; set; }
        public string nocandndc_gid { get; set; }
        public string maker_gid { get; set; }
        public string maker_name { get; set; }
        public string checker_gid { get; set; }
        public string checker_name { get; set; }
        public string file_name { get; set; }
        public string created_date { get; set; }
        public string nocandndc_date { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
        public string customer_name { get; set; }
        public string sanction_ref_no { get; set; }
        public string sanction_date { get; set; }
        public string loan_account_no { get; set; }
        public string noc_issuance_date { get; set; }
        public string loan_closure_date { get; set; }
        public string created_by { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public char delete_flag { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string deleted_date { get; set; }
        public string deleted_by { get; set; }
        public List<nocandndc_list> nocandndc_list { get; set; }
        public List<employee_list> employee_list { get; set; }
    }

    public class UploadNocDocumentList
    {
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string nocandndcdocument_gid { get; set; }
        public string nocandndc_gid { get; set; }
        public string tmpnocandndc_gid { get; set; }
        public string file_name { get; set; }
        public string path { get; set; }
        public string created_date { get; set; }
        public string uploaded_by { get; set; }
        public string upload_by { get; set; }
        public string document_type { get; set; }
        public string updated_date { get; set; }
    }

    public class vertical_list
    {
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
        public string vertical_code { get; set; }
    }
}