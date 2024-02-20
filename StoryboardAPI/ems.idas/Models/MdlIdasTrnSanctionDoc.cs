using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.idas.Models
{
    public class MdlsanctionDocDtls:result 
    {
        public string[] documentlist_gid { get; set; }
        public string sanction_gid { get; set; }
        public string customer_gid { get; set; }
        public string doc_comments { get; set; }
    }
    public class MdlTaggedDocumentList:result 
    {
        public List<MdlTaggedDocument> MdlTaggedDocument { get; set; }
    }
    public class MdlTaggedDocument
    {
        public string sanctiondocument_gid { get; set; }
        public string document_gid { get; set; }
        public string document_code { get; set; }
        public string document_name { get; set; }
        public string documentrecord_id { get; set; }
        public string document_count { get; set; }
        public string comments { get; set; }
    }
    public class MdlSanctionDocSummaryList : result
    {
        public List<MdlSanctionDocSummary> MdlSanctionDocSummary { get; set; }
        public string totalsummary_count { get; set; }
    }
    public class MdlSanctionDocSummary
    {
        public string sanction_gid { get; set; }
        public string customer_urn { get; set; }
        public string customer_name { get; set; }
        public string sanction_refno { get; set; }
        public string sanction_date { get; set; }
        public string tagged_document { get; set; }
     
        public string batch_status { get; set; }
        public string fileref_no { get; set; }
    }
    public class MdlSummaryList:result
    {
        public List<MdlMakercheckerSummary> MdlMakercheckerSummary { get; set; }
       
    }
    public class MdlMakercheckerSummary:result 
    {
        public string sanction_gid { get; set; }
        public string customer_urn { get; set; }
        public string customer_name { get; set; }
        public string sanction_refno { get; set; }
        public string sanction_date { get; set; }
        public string tagged_document { get; set; }
        public string makerconfirmed_count { get; set; }
        public string checkerconfirmed_count { get; set; }
        public string batch_status { get; set; }
        public string fileref_no { get; set; }

    }
    public class MdlScannDocList
    {
        public List<MdlScannDocSummary> MdlScannDocSummary { get; set; }
    }
    public class MdlScannDocSummary:result 
    {
        public string sanction_gid { get; set; }
        public string sanctiondocument_gid { get; set; }
        public string document_gid { get; set; }
        public string documentlist_gid { get; set; }
        public string documentrecord_id { get; set; }
        public string document_code { get; set; }
        public string scandocument_date { get; set; }
        public string phydocument_date { get; set; }
        public string document_name { get; set; }
        public string document_date { get; set; }
        public string maker_status { get; set; }
        public string maker_name { get; set; }
        public string maker_gid { get; set; }
        public string maked_on { get; set; }
        public string checker_status { get; set; }
        public string checker_name { get; set; }
        public string checker_gid { get; set; }
        public string checked_on { get; set; }
        public string phydoc_status { get; set; }
        public string Phydocverified_on { get; set; }
        public string phydocverifier_name { get; set; }
        public string scanfinal_remarks { get; set; }
        public string phyfinal_remarks { get; set; }
        public string scanconversation_count { get; set; }
        public string scanrmconversation_count { get; set; }
        public string phyconversation_count { get; set; }
        public string externalconversation_count { get; set; }
        public string externalquery_count { get; set; }
        public string externalresponse_count { get; set;}
        public string internalquery_count { get; set; }
        public string internalresponse_count { get; set; }
        public string comments { get; set; }
        public string types_of_copy { get; set; }
        public string phydocument_type { get; set; }
        public string finalremarks { get; set; }
    }

  public class MdlExportConversation:result 
    {
        public string sanction_gid { get; set; }
        public string type_of_copy { get; set; }
        public string attachment_name { get; set; }
        public string attachment_path { get; set; }
        public string attachment_cloudpath { get; set; }
        public string lsacreate_gid { get; set; }
    }
    public class docconlist:result
    {
        public List<MdlDocConversation> MdlDocConversation { get; set; }
    }
    public class MdlDocConversation:result 
    {
        public string noquery_flag { get; set; }
        public string ref_count { get; set; }
        public string docconversation_gid { get; set; }
        public string sanctiondocument_gid { get; set; }
        public string sanction_gid { get; set; }
        public string document_gid { get; set; }
        public string document_code { get; set; }
        public string documentrecord_id { get; set; }
        public string document_name { get; set; }
        public string docconversationref_no { get; set; }
        public string type_of_conversation { get; set; }
        public string type_of_doc { get; set; }
        public string cad_query { get; set; }
        public string rm_response { get; set; }
        public string cad_name { get; set; }
        public string cad_gid { get; set; }
        public string cadquery_on { get; set; }
        public string relationshipmgr_gid { get; set; }
        public string relationshipmgr_name { get; set; }
        public string relationshipmgrquery_on { get; set; }
        public string query_no { get; set; }
        public string flag { get; set; }
        public string forwarded_flag { get; set; }
        public string forwarded_by_name { get; set; }
        public string forwarded_on { get; set; }
        public string attachment_name { get; set; }
        public string attachement_path { get; set;  }
        public string attachement_cloudpath { get; set; }
        public string uploaddocument_count { get; set; }
        public string reference_query { get; set; }
      public string lsa_status { get; set; }
    }

    
    public class uploaddocumentlist:result
    {
        public List<uploaddocument> uploaddocument { get; set; }

    }

    public class uploaddocument : result
    {
        public string conversationdocument_gid { get; set; }
        public string uploaddocument_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string document_title { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class types_of_copy:result
    {
        public string type_copy { get; set; }
        public string sanctiondocument_gid { get; set; }
    }

    public class MdlSanctionDtlsView
    {
        public string sanctionrefno { get; set; }
        public string SanctionDate { get; set; }
        public string SanctionAmount { get; set; }
        public string FacilityType { get; set; }
        public string customerName { get; set; }
        public string Customerurn { get; set; }
        public string collateral_security { get; set; }
        public string zonalHeadName { get; set; }
        public string businessHeadName { get; set; }
        public string clusterManager { get; set; }
        public string creditManager { get; set; }
        public string relationshipmgmt { get; set; }
        public string customercode { get; set; }
        public string verticalCode { get; set; }
        public string contactperson { get; set; }
        public string mobileno { get; set; }
        public string addressline1 { get; set; }
        public string addressline2 { get; set; }
        public string customer_gid { get; set; }
        public string batch_status { get; set; }
        public string status_ofBAL { get; set; }
        public string lsa_status { get; set; }
        public string maker_status { get; set; }
        public string checker_status { get; set; }
    }
    public class MdlBulkverification : result
    {
        public string phydocument_type { get; set; }
        public string type_copy { get; set; }
        public string document_date { get; set; }
        public string phyfinal_remarks { get; set; }
        public string[] sanctiondocument_gid { get; set; }
        public string scanfinal_remarks { get; set; }
        public string finalremarks { get; set; }
        public string confirmation_type { get; set; }
    }
}