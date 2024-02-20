using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.idas.Models
{
    public class MdlIdsMstDigitalSignature : result
    {
        public List<employeelist> employeelist { get; set; }
        public List<digitalsignaturelist> digitalsignaturelist { get; set; }
    }

    public class employeelist
    {
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
    }
    
    public class uploadSignature : result
    {
        public List<upload_list> upload_list { get; set; }
    }
    public class upload_list
    {
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }
    public class digitalsignaturelist
    {
        public string digitalsignature_gid { get; set; }
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }
}