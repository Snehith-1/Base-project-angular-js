using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.idas.Models
{
    public class batchlist 
    {
        public List<MdlbatchSummary> MdlbatchSummary { get; set; }
    }
    public class MdlFilecount:result 
    {
        public string totalfile_count { get; set; }
        public string taggedfile_count { get; set; }
        public string untaggedfile_count { get; set; }
        public string totalbox_count { get; set; }
        public string taggedbox_count { get; set; }
        public string untaggedbox_count { get; set; }
        public string despatch_count { get; set; }
    }

    public class CartonBoxlist:result 
    {
        public List<MdlCartonBoxSummary> MdlCartonBoxSummary { get; set; }
    }
    public class DespatchList : result
    {
        public List<MdlDespatch> MdlDespatch { get; set; }
    }
    public class MdlbatchSummary : result
    {
        public string batch_gid { get; set; }
        public string fileref_no { get; set; }
        public string sanction_gid { get; set; }
        public string batch_by { get; set; }
        public string batched_on { get; set; }
        public string customer_urn { get; set; }
        public string customer_name { get; set; }
        public string stampref_no { get; set; }
        public string tagged_document { get; set; }
        public string redespatch_flag { get; set; }
        public string barcoderef_no { get; set; }
        public string lspath { get; set; }
        public string lscloudpath { get; set; }
        public string lsname { get; set; }
    }
    public class MdlCartonBox:result
    {
       
        public string[] batch_gid { get; set; }
        public string boxref_no { get; set; }
        public string stampref_no { get; set; }
        public string cartonbox_date { get; set; }
        public string remarks { get; set; }
       public string boxbarcoderef_no { get; set; }
    }

    public class MdlCartonBoxSummary
    {
        public string cartonbox_gid { get; set; }
        public string cartonboxref_no { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string tagged_count { get; set; }
        public string remarks { get; set; }
        public string cartonboxed_date { get; set; }
        public string stampref_no { get; set; }
        public string boxbarcoderef_no { get; set; }
    }

    public class MdlDespatch:result
    {
        public string despatch_date { get; set; }
        public string[] cartonbox_gid { get; set; }
        public string vendor_name { get; set; }
        public string vendor_gid { get; set; }
        public string contact_person { get; set; }
        public string mobile_no { get; set; }
        public string stampref_no { get; set; }
        public string desptached_by { get; set; }
        public string despatchedby_gid { get; set; }
        public string remarks { get; set; }
        public string created_date { get; set; }
        public string despatchref_no { get; set; }
        public string despatch_gid { get; set; }
        public string tagged_count { get; set; }
    }

    public class MdlBatchStamp
    {
        public string sanction_gid { get; set; }
        public string stampref_no { get; set; }
        public string barcoderef_no { get; set; }
    }

  
}