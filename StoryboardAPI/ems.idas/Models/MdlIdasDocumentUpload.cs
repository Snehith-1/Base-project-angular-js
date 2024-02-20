using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.idas.Models
{
    public class MdlIdasDocumentUpload:result 
    {
        public string folder_name { get; set; }
        public string directory_type { get; set; }
        public string parent_directorygid { get; set; }
        public string customer_gid { get; set; }
        public string customer2sanction_gid { get; set; }
        public string remarks { get; set; }
    }
    public class DirectoryDtlsList:result 
    {
        public List <DirectoryDtls> DirectoryDtls { get; set; }
    }

    public class DirectoryDtls
    {
        public string fileupload_gid { get; set; }
        public string customerfileupload_gid { get; set; }
        public string creditfileupload_gid { get; set; }
        public string creditoperationsfileupload_gid { get; set; }
        public string folder_name { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string directory_type { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string documentlabel_name { get; set; }
        public string remarks { get; set; }
    }

    public class FolderDtls : result
    {
        public string customerfileupload_gid { get; set; }
        public string fileupload_gid { get; set; }
        public string creditfileupload_gid { get; set; }
        public string creditoperationsfileupload_gid { get; set; }
        public string folder_name { get; set; }
        public string serial_no { get; set; }
        public string type { get; set; }
    }

    public class breadCrumbList 
    {
       public List<FolderDtls> FolderDtls { get; set; }
    }

    public class Mdlsanction2customer : result
    {
        public List<sanction2customer_list> sanction2customer_list { get; set; }
        public List<MOM_DocumentList> MOM_DocumentList { get; set; }
      
    }
    public class sanction2customer_list
    {
        public string sanctionref_no { get; set; }
        public string customer2sanction_gid { get; set; }
        public string sanction_date { get; set; }
        public string sanction_amount { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }
    public class MOM_DocumentList
    {
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string document_gid { get; set; }
        public string filename { get; set; }
        public string path { get; set; }
        public string created_date { get; set; }
        public string uploaded_by { get; set; }
        public string upload_by { get; set; }
        public string document_type { get; set; }
        public string updated_date { get; set; }
        public string buyer_gid { get; set; }
        public string buyer_name { get; set; }
        public string buyer_exposure { get; set; }
        public string baldocument_gid { get; set; }
    }
    public class MdlDocumentLabel : result
    {
        public string documentlabel_name { get; set; }
        public string documentlabel_desc { get; set; }
        public string documentlabel_gid { get; set; }
        public string department_gid { get; set; }
        public string department_name { get; set; }
        public List<DocumentLabelList> DocumentLabelList { get; set; }
        public List<CreditAdminDocumentLabelList> CreditAdminDocumentLabelList { get; set; }
        public List<CreditUnderwritingDocumentLabelList> CreditUnderwritingDocumentLabelList { get; set; }
        public List<CreditOperationsDocumentLabelList> CreditOperationsDocumentLabelList { get; set; }
    }

    public class DocumentLabelList
    {
        public string documentlabel_name { get; set; }
        public string documentlabel_desc { get; set; }
        public string documentlabel_gid { get; set; }
        public string department_name { get; set; }
    }

    public class MdlCadTeamFlag : result
    {
        public string cadteam_flag { get; set; }
    }
    public class MdlTaggedCustomer : result
    {
        public List<customer_list> customer_list { get; set; }
        public string tagged_count { get; set; }
        public string untagged_count { get; set; }

    }
    public class customer_list
    {
        public string customer_gid { get; set; }
        public string customercode { get; set; }
        public string customername { get; set; }
        public string contactperson { get; set; }
        public string vertical_code { get; set; }
        public string zonalGid { get; set; }
        public string businessHeadGid { get; set; }
        public string relationshipMgmtGid { get; set; }
        public string clustermanagerGid { get; set; }
        public string creditmanagerName { get; set; }
        public string mail_count { get; set; }
        public string customer_urn { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }
    public class MdlArchivalCondition
    {
        public string customer_gid { get; set; }
        public string archival_type { get; set; }
        public string customer2sanction_gid { get; set; }
    }
    public class WorkItemList : result
    {
        public List<MdlWorkItem> MdlWorkItem { get; set; }
    }
    public class MdlWorkItem
    {
        public string email_gid { get; set; }
        public string workitemref_no { get; set; }
        public string email_from { get; set; }
        public string email_date { get; set; }
        public string email_subject { get; set; }
        public string[] emailSubject { get; set; }
        public string email_content { get; set; }
        public string created_date { get; set; }
        public string status_attachment { get; set; }
        public string cc { get; set; }
        public string bcc { get; set; }
        public string message_id { get; set; }
        public string email_to { get; set; }
        public string rmemployee_gid { get; set; }
        public string archival_type { get; set; }
        public string zone_name { get; set; }
        public string zone_gid { get; set; }
        public string rmemployee_name { get; set; }
        public string rmemployee_mailid { get; set; }
        public string employee_gid { get; set; }
        public string checkeremployee_name { get; set; }
        public string status { get; set; }
        public string reference_id { get; set; }
        public string aging { get; set; }
        public string seen_flag { get; set; }
        public string email_address { get; set; }
        public string allottedby_on { get; set; }
        public string updatedby_on { get; set; }
        public string customer_name { get; set; }
        public string remarks { get; set; }
        public string archival_by { get; set; }
        public string archival_date { get; set; }
        public List<MdlAttachmentList> MdlAttachmentList { get; set; }

    }

    public class MdlAttachmentList
    {
        public string mailattachment_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string document_extension { get; set; }
    }
    public class MdlCreditTeamFlag : result
    {
        public string creditteam_flag { get; set; }
        public string creditoperationteam_flag { get; set; }
    }
    public class Departmentname : result
    {
        public List<department_list> department_list { get; set; }
    }
    public class department_list
    {
        public string department_name { get; set; }
        public string department_gid { get; set; }
    }
    public class CreditAdminDocumentLabelList
    {
        public string documentlabel_name { get; set; }
        public string documentlabel_desc { get; set; }
        public string documentlabel_gid { get; set; }
        public string department_name { get; set; }
    }
    public class CreditUnderwritingDocumentLabelList
    {
        public string documentlabel_name { get; set; }
        public string documentlabel_desc { get; set; }
        public string documentlabel_gid { get; set; }
        public string department_name { get; set; }
    }
    public class CreditOperationsDocumentLabelList
    {
        public string documentlabel_name { get; set; }
        public string documentlabel_desc { get; set; }
        public string documentlabel_gid { get; set; }
        public string department_name { get; set; }
    }
}