using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using ems.idas.Models;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Text;
using ems.storage.Functions;

namespace ems.idas.DataAccess
{
    public class DaIdasTrnRecordRetrieval
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        OdbcDataReader objODBCDataReader;
        string msSQL;
        int mnResult;
        string msGetGID, msGETGIDDoc;
        string lspath = string.Empty;
        string lscompany_code = string.Empty;
        HttpPostedFile httpPostedFile;
        string reference_gid;
        StringBuilder sb1 = new StringBuilder();

        public result DaPostRetrievalRequest(string user_gid,MdlIdasRecordRequest values)
        {
            result objResult = new Models.result();
          
            msGetGID = objcmnfunctions.GetMasterGID("REQU");

            msSQL = " INSERT INTO ids_trn_tretrievalrequest(" +
                    " retrievalrequest_gid," +
                    " requested_date," +
                    " requestedby_name," +
                    " approved_date," +
                    " approvalby_name," +
                    " req_remarks," +
                    " retrieval_type,"+
                    " requested_for,"+
                    " documentretrieved_mode,"+
                    " created_date," +
                    " created_by)" +
                    " VALUES(" +
                    "'" + msGetGID + "'," +
                    "'" + Convert.ToDateTime(values.requested_date).ToString("yyyy-MM-dd") + "'," +
                    "'" + values.requestedby_name + "'," +                
                    "'" + Convert.ToDateTime(values.approved_date).ToString("yyyy-MM-dd") + "'," +
                    "'" + values.approvalby_name + "'," +
                    "'" + values.req_remarks.Replace("'", "") + "'," +
                    "'" + values.retrieval_type +"',"+
                    "'" + values.requested_for +"',"+
                    "'"+ values.documentretrieved_mode+"',"+
                    "current_timestamp," +
                    "'" + user_gid + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if(mnResult ==1)
            {
                for (int i = 0; i < values.reteival_record.GetLength(0); i++)
                {
                    msGETGIDDoc = objcmnfunctions.GetMasterGID("REQD");

                    msSQL = " INSERT INTO ids_trn_tretrievalrequestdtls(" +
                                "retrievalrequestdtls_gid," +
                                " retrievalrequest_gid,"+
                                " customer_gid," +
                                " customer_name," +
                                " despatch_gid," +
                                " despatchref_no," +
                                " batch_gid," +
                                " filestampref_no," +
                                " fileref_no," +
                                " box_gid," +
                                " boxstampref_no)" +
                                " VALUES(" +
                                "'" + msGETGIDDoc + "'," +
                                "'"+ msGetGID + "',"+
                                "'" + values.reteival_record[i, 0] + "'," +
                                "'" + values.reteival_record[i, 1] + "'," +
                                "'" + values.reteival_record[i, 2] + "'," +
                                "'" + values.reteival_record[i, 3] + "'," +
                                "'" + values.reteival_record[i, 6] + "'," +
                                "'" + values.reteival_record[i, 7] + "'," +
                                "'" + values.reteival_record[i, 8] + "'," +
                                "'" + values.reteival_record[i, 4] + "'," +
                                "'" + values.reteival_record[i, 5] + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " UPDATE ids_trn_tbatch SET" +
                            " redespatch_flag='Y'"+
                            " WHERE batch_gid='"+ values.reteival_record[i, 6] + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = " INSERT INTO ids_trn_tretrievalapprovadoc(retrievalapprovadoc_gid,retrievalrequest_gid,document_path,document_name,document_title,created_by,created_date)" +
                          " SELECT tmpretrievalapprovadoc_gid,'" + msGetGID + "',document_path,document_name,document_title,created_by,created_date" +
                          " FROM ids_tmp_tretrievalapprovadoc" +
                          " WHERE created_by='" + user_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult ==1)
                    {
                        msSQL = " DELETE FROM ids_tmp_tretrievalapprovadoc WHERE created_by='"+user_gid+"'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                       
                        DaIdasTrnSentMail objDaSent = new DaIdasTrnSentMail();
                        objResult=objDaSent.DaPostRetreivalReqMail(msGetGID,user_gid );

                        if (objResult.status ==true)
                        {
                            objResult.status = true;
                            objResult.message = "Retrieval Request Created Successfully";

                        }
                        else
                        {
                            objResult.status = false;
                            objResult.message = "Erro Occured While Mail Sending";
                        }
                        
                        
                    }
                    else
                    {
                        objResult.status = false;
                        objResult.message = "Error in Upload Document";
                    }
                   
                }
            
               
            else
            {
                objResult.status = false;
                objResult.message = "Error in Primary Table";
            }
            return objResult;
        }
        public void DaDeleteRequestDtls(string tmpretrievalrequestdtls_gid,string user_gid, result objResult)
        {
            if(tmpretrievalrequestdtls_gid !=null)
            {
                msSQL = " DELETE FROM ids_tmp_tretrievalrequestdtls WHERE tmpretrievalrequestdtls_gid='" + tmpretrievalrequestdtls_gid + "'";
            }
            else
            {
                msSQL = " DELETE FROM ids_tmp_tretrievalrequestdtls WHERE created_by='" + user_gid + "'";
            }
         
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult ==1)
            {
                objResult.status = true;
                objResult.message = "Record Deleted Successfully";
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";

            }
        }
        public void DaTmpDocumentDelete(string uploaddocument_gid, string user_gid, result objResult)
        {
            if (uploaddocument_gid != null)
            {
                msSQL = " DELETE FROM ids_tmp_tretrievalapprovadoc WHERE tmpretrievalapprovadoc_gid='" + uploaddocument_gid + "'";
            }
            else
            {
                msSQL = " DELETE FROM ids_tmp_tretrievalapprovadoc WHERE created_by='" + user_gid + "'";
            }

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                objResult.status = true;
                objResult.message = "Uploaded Document Deleted Successfully";
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";

            }
        }

        public void DaGetRetrievalReqSummary(MdlIdasRecordReqSummarylist objResult)
        {
            msSQL = " SELECT a.retrievalrequest_gid,DATE_FORMAT(a.requested_date,'%d-%m-%Y') as requested_date,a.requestedby_name,a.documentretrieved_status, " +
                    " DATE_FORMAT(a.created_date,'%d-%m-%Y') as created_date,DATE_FORMAT(a.approved_date,'%d-%m-%Y') as approved_date,a.approvalby_name," +
                    " CONCAT(b.user_code,' / ',b.user_firstname,b.user_lastname) as user_name,retrieval_type,requested_for," +
                    " (SELECT COUNT(*) FROM ids_trn_tretrievalrequestdtls x WHERE x.retrievalrequest_gid=a.retrievalrequest_gid GROUP BY retrievalrequest_gid) as total_count"+
                    " FROM ids_trn_tretrievalrequest a" +
                    " LEFT JOIN adm_mst_tuser b on a.created_by=b.user_gid" +
                    " WHERE 1=1  ORDER BY a.created_date DESC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                objResult.MdlIdasRecordReqSummary = dt_datatable.AsEnumerable().Select(row => new MdlIdasRecordReqSummary
                {
                    retrievalrequest_gid = row["retrievalrequest_gid"].ToString(),
                    requested_date = row["requested_date"].ToString(),
                  
                    created_date = row["created_date"].ToString(),
                    user_name = row["user_name"].ToString(),
                    total_count = row["total_count"].ToString(),
                    retrieval_type = row["retrieval_type"].ToString (),
                    requested_for = row["requested_for"].ToString (),
                    documentretrieved_status=row["documentretrieved_status"].ToString (),
                    approved_date =row["approved_date"].ToString (),
                   
                }).ToList();
                dt_datatable.Dispose();
                objResult.status = true;
                objResult.message = "Success";
            }
            else
            {
                objResult.status = false;
                objResult.message = "No Record Found";
            }
          


        }
        //public void DaGetRetrievalTemporarySummary(MdlIdasRecordReceivedSummaryList objResult)
        //{
        //    msSQL = " SELECT a.retrievalrequest_gid,DATE_FORMAT(a.requested_date,'%d-%m-%Y') as requested_date,a.requestedby_name,a.documentretrieved_status, " +
        //            " DATE_FORMAT(c.created_date,'%d-%m-%Y') as created_date,DATE_FORMAT(a.approved_date,'%d-%m-%Y') as approved_date,a.approvalby_name," +
        //            " CONCAT(c.user_code,' / ',c.user_firstname,c.user_lastname) as user_name,retrieval_type,requested_for," +
        //            " (SELECT COUNT(*) FROM ids_trn_tretrievalrequestdtls x WHERE x.retrievalrequest_gid=a.retrievalrequest_gid GROUP BY retrievalrequest_gid) as total_count," +
        //            " b.documentreceived_gid, DATE_FORMAT(b.documentretrieved_date,'%d-%m-%Y') as documentretrieved_date,b.documentreceivedto_name," +
        //            " b.contactperson_name" +
        //            " FROM ids_trn_tretrievalrequest a" +
        //            " LEFT JOIN ids_trn_documentreceiveddtls b ON a.retrievalrequest_gid=b.retrievalrequest_gid" +
        //            " LEFT JOIN adm_mst_tuser c on c.user_gid=b.created_by" +
        //            " WHERE 1=1 AND a.documentretrieved_status='Received' AND retrieval_type='Temporary' ORDER BY b.documentretrieved_date DESC";
        //    dt_datatable = objdbconn.GetDataTable(msSQL);
        //    if (dt_datatable.Rows.Count != 0)
        //    {
        //        objResult.MdlIdasRecordReceivedSummary = dt_datatable.AsEnumerable().Select(row => new MdlIdasRecordReceivedSummary
        //        {
        //            documentreceived_gid=row["documentreceived_gid"].ToString (),
        //            retrievalrequest_gid = row["retrievalrequest_gid"].ToString(),
        //            requested_date = row["requested_date"].ToString(),
        //            created_date = row["created_date"].ToString(),
        //            user_name = row["user_name"].ToString(),
        //            total_count = row["total_count"].ToString(),
        //            retrieval_type = row["retrieval_type"].ToString(),
        //            requested_for = row["requested_for"].ToString(),
        //            documentretrieved_status=row["documentretrieved_status"].ToString (),
        //            approved_date = row["approved_date"].ToString(),
        //            documentretrieved_date=row["documentretrieved_date"].ToString (),
        //            documentretrievedto_name=row["documentreceivedto_name"].ToString (),
        //            contactperson_name=row["contactperson_name"].ToString (),

        //        }).ToList();
        //        dt_datatable.Dispose();
        //        objResult.status = true;
        //        objResult.message = "Success";
        //    }
        //    else
        //    {
        //        objResult.status = false;
        //        objResult.message = "No Record Found";
        //    }



        //}

        //public void DaGetRetrievalPermanentSummary(MdlIdasRecordReceivedSummaryList objResult)
        //{
        //    msSQL = " SELECT a.retrievalrequest_gid,DATE_FORMAT(a.requested_date,'%d-%m-%Y') as requested_date,a.requestedby_name,a.documentretrieved_status, " +
        //            " DATE_FORMAT(c.created_date,'%d-%m-%Y') as created_date,DATE_FORMAT(a.approved_date,'%d-%m-%Y') as approved_date,a.approvalby_name," +
        //            " CONCAT(c.user_code,' / ',c.user_firstname,c.user_lastname) as user_name,retrieval_type,requested_for," +
        //            " (SELECT COUNT(*) FROM ids_trn_tretrievalrequestdtls x WHERE x.retrievalrequest_gid=a.retrievalrequest_gid GROUP BY retrievalrequest_gid) as total_count," +
        //            " b.documentreceived_gid, DATE_FORMAT(b.documentretrieved_date,'%d-%m-%Y') as documentretrieved_date,b.documentreceivedto_name,b.contactperson_name" +
        //            " FROM ids_trn_tretrievalrequest a" +
        //            " LEFT JOIN ids_trn_documentreceiveddtls b ON a.retrievalrequest_gid=b.retrievalrequest_gid" +
        //            " LEFT JOIN adm_mst_tuser c ON c.user_gid=b.created_by" +
        //            " WHERE 1=1 AND a.documentretrieved_status='Received' AND a.retrieval_type='Permanent' ORDER BY b.documentretrieved_date DESC";
        //    dt_datatable = objdbconn.GetDataTable(msSQL);
        //    if (dt_datatable.Rows.Count != 0)
        //    {
        //        objResult.MdlIdasRecordReceivedSummary = dt_datatable.AsEnumerable().Select(row => new MdlIdasRecordReceivedSummary
        //        {
        //            documentreceived_gid = row["documentreceived_gid"].ToString(),
        //            retrievalrequest_gid = row["retrievalrequest_gid"].ToString(),
        //            requested_date = row["requested_date"].ToString(),
        //            created_date = row["created_date"].ToString(),
        //            user_name = row["user_name"].ToString(),
        //            total_count = row["total_count"].ToString(),
        //            retrieval_type = row["retrieval_type"].ToString(),
        //            requested_for = row["requested_for"].ToString(),
        //            documentretrieved_status = row["documentretrieved_status"].ToString(),
        //            approved_date = row["approved_date"].ToString(),
        //            documentretrieved_date = row["documentretrieved_date"].ToString(),
        //            documentretrievedto_name = row["documentreceivedto_name"].ToString(),
        //            contactperson_name = row["contactperson_name"].ToString(),

        //        }).ToList();
        //        dt_datatable.Dispose();
        //        objResult.status = true;
        //        objResult.message = "Success";
        //    }
        //    else
        //    {
        //        objResult.status = false;
        //        objResult.message = "No Record Found";
        //    }



        //}

        public void DaGetRetrievalPermanentSummary(MdlIdasRecordReceivedSummaryList values)
        {
            msSQL = " SELECT a.retrievalrequest_gid, a.retrievalrequestdtls_gid,DATE_FORMAT(b.requested_date,'%d-%m-%Y') as requested_date,b.requestedby_name ," +
                    " c.documentreceivedto_name,date_format(c.documentretrieved_date, '%d-%m-%Y') as documentreceived_date,a.customer_name," +
                    " c.contactperson_name,a.filestampref_no,a.boxstampref_no,b.documentretrieved_status" +
                    " FROM ids_trn_tretrievalrequestdtls a" +
                    " LEFT JOIN ids_trn_tretrievalrequest b ON a.retrievalrequest_gid = b.retrievalrequest_gid" +
                    " LEFT JOIN ids_trn_documentreceiveddtls c ON a.retrievalrequest_gid = c.retrievalrequest_gid" +
                    " WHERE 1 = 1 AND a.file_status = 'Received' AND retrieval_type = 'Permanent' ORDER BY c.documentretrieved_date DESC";
            dt_datatable = objdbconn.GetDataTable(msSQL );
            if (dt_datatable .Rows .Count !=0)
            {
                values.MdlIdasRecordReceivedSummary = dt_datatable.AsEnumerable().Select(row => new MdlIdasRecordReceivedSummary
                  {
                    retrievalrequest_gid = row["retrievalrequest_gid"].ToString(),
                    retrievalrequestdtls_gid =row["retrievalrequestdtls_gid"].ToString (),
                    requested_date =row["requested_date"].ToString (),
                    requestedby_name =row["requestedby_name"].ToString (),
                    documentreceivedto_name =row["documentreceivedto_name"].ToString (),
                    documentreceived_date =row ["documentreceived_date"].ToString (),
                    contactperson_name =row["contactperson_name"].ToString (),
                    filestampref_no =row["filestampref_no"].ToString (),
                    boxstampref_no =row["boxstampref_no"].ToString (),
                    documentretrieved_status =row["documentretrieved_status"].ToString(),
                    customer_name = row["customer_name"].ToString(),
                }).ToList();
                values.status = true;
                values.message = "Records Found";
            }
            else
            {
                values.status = false;
                values.message = "No Records";
            }
        }
        public void DaGetRetrievalTemporarySummary(MdlIdasRecordReceivedSummaryList values)
        {
            msSQL = " SELECT a.retrievalrequest_gid, a.retrievalrequestdtls_gid,DATE_FORMAT(b.requested_date,'%d-%m-%Y') as requested_date,b.requestedby_name ," +
                    " c.documentreceivedto_name,date_format(c.documentretrieved_date, '%d-%m-%Y') as documentreceived_date,a.customer_name," +
                    " c.contactperson_name,a.filestampref_no,a.boxstampref_no,b.documentretrieved_status,a.ensure_flag" +
                    " FROM ids_trn_tretrievalrequestdtls a" +
                    " LEFT JOIN ids_trn_tretrievalrequest b ON a.retrievalrequest_gid = b.retrievalrequest_gid" +
                    " LEFT JOIN ids_trn_documentreceiveddtls c ON a.retrievalrequest_gid = c.retrievalrequest_gid" +
                    " WHERE 1 = 1 AND a.file_status = 'Received' AND retrieval_type = 'Temporary' ORDER BY c.documentretrieved_date DESC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlIdasRecordReceivedSummary = dt_datatable.AsEnumerable().Select(row => new MdlIdasRecordReceivedSummary
                {
                    retrievalrequest_gid = row["retrievalrequest_gid"].ToString(),
                    retrievalrequestdtls_gid = row["retrievalrequestdtls_gid"].ToString(),
                    requested_date = row["requested_date"].ToString(),
                    requestedby_name = row["requestedby_name"].ToString(),
                    documentreceivedto_name = row["documentreceivedto_name"].ToString(),
                    documentreceived_date = row["documentreceived_date"].ToString(),
                    contactperson_name = row["contactperson_name"].ToString(),
                    filestampref_no = row["filestampref_no"].ToString(),
                    boxstampref_no = row["boxstampref_no"].ToString(),
                    documentretrieved_status = row["documentretrieved_status"].ToString(),
                    customer_name =row["customer_name"].ToString (),
                    ensure_flag=row["ensure_flag"].ToString (),
                }).ToList();
                values.status = true;
                values.message = "Records Found";
            }
            else
            {
                values.status = false;
                values.message = "No Records";
            }
        }
        public void DaGetRetrievalReqView(MdlRecordReqView values,string retrievalrequest_gid)
        {
            msSQL =" SELECT "+
                   " retrievalrequest_gid,DATE_FORMAT(requested_date,'%d-%m-%Y') as requested_date," +
                   " requestedby_name,DATE_FORMAT(approved_date,'%d-%m-%Y') as approved_date,approvalby_name," +
                   " documentretrieved_status,retrieval_type,requested_for,req_remarks ,documentretrieved_mode" +
                   " FROM ids_trn_tretrievalrequest" +
                   " WHERE retrievalrequest_gid='" + retrievalrequest_gid + "'";
            objODBCDataReader  = objdbconn.GetDataReader (msSQL );
            if (objODBCDataReader .HasRows )
            {
                objODBCDataReader.Read();
                values.retrievalrequest_gid = objODBCDataReader["retrievalrequest_gid"].ToString();
                values.requested_date = objODBCDataReader["requested_date"].ToString();
                values.requestedby_name = objODBCDataReader["requestedby_name"].ToString();
                values.approved_date = objODBCDataReader["approved_date"].ToString();
                values.approvalby_name = objODBCDataReader["approvalby_name"].ToString();
                values.documentretrieved_status = objODBCDataReader["documentretrieved_status"].ToString();
                values.retrieval_type = objODBCDataReader["retrieval_type"].ToString();
                values.requested_for = objODBCDataReader["requested_for"].ToString();
                values.req_remarks = objODBCDataReader["req_remarks"].ToString();
                values.documentretrieved_mode = objODBCDataReader["documentretrieved_mode"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = " SELECT retrievalapprovadoc_gid,document_path,document_name,document_title" +
                   " FROM ids_trn_tretrievalapprovadoc" +
                   " WHERE retrievalrequest_gid='" + retrievalrequest_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlIdasUploadDocument = dt_datatable.AsEnumerable().Select(row => new MdlIdasUploadDocument
                {
                    uploaddocument_gid = row["retrievalapprovadoc_gid"].ToString(),
                    document_name = row["document_name"].ToString(),
                    document_title = row["document_title"].ToString(),
                    document_path = objcmnstorage.EncryptData(row["document_path"].ToString())

                }).ToList();

            }
            dt_datatable.Dispose();
            if (values.documentretrieved_mode == "Customer")
            {
                msSQL = " SELECT DISTINCT  a.customer_gid,b.customer_urn,b.customername,b.vertical_code,"+
                        " b.businesshead_name,b.zonal_name,b.cluster_manager_name," +
                        " b.creditmgmt_name,b.relationshipmgmt_name" +
                        " FROM ids_trn_tretrievalrequestdtls a" +
                        " INNER JOIN ocs_mst_tcustomer b on a.customer_gid = b.customer_gid" +
                        " WHERE a.retrievalrequest_gid = '" + retrievalrequest_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    values.MdlCustomerDtlsList = dt_datatable.AsEnumerable().Select(row => new MdlCustomerDtls
                    {
                        customer_gid=row["customer_gid"].ToString (),
                        customer_urn = row["customer_urn"].ToString(),
                        customername = row["customername"].ToString(),
                        vertical_code = row["vertical_code"].ToString(),
                        businesshead_name = row["businesshead_name"].ToString(),
                        zonal_name = row["zonal_name"].ToString(),
                        cluster_manager_name = row["cluster_manager_name"].ToString(),
                        creditmgmt_name = row["creditmgmt_name"].ToString(),
                        relationshipmgmt_name = row["relationshipmgmt_name"].ToString()

                    }).ToList();
                }
                dt_datatable.Dispose();
            }

            if (values.documentretrieved_mode == "Box")
            {
                msSQL = " SELECT DISTINCT a.box_gid, b.boxref_no,b.stampref_no,date_format(b.cartonbox_date,'%d-%m-%Y') as cartonbox_date,b.remarks"+
                        " FROM ids_trn_tretrievalrequestdtls a"+
                        " INNER JOIN ids_trn_tcartonbox b on a.box_gid = b.cartonbox_gid"+
                        " WHERE a.retrievalrequest_gid = '"+ retrievalrequest_gid +"'";
                dt_datatable  = objdbconn.GetDataTable (msSQL);
                    if(dt_datatable .Rows .Count !=0 )
                {
                        values.MdlBoxDtlsList = dt_datatable.AsEnumerable().Select(row => new MdlBoxDtls
                        {
                            box_gid=row["box_gid"].ToString (),
                            boxref_no = row["boxref_no"].ToString(),
                            stampref_no = row["stampref_no"].ToString(),
                            cartonbox_date = row["cartonbox_date"].ToString(),
                            remarks = row["remarks"].ToString(),
                    }).ToList();
                }
                dt_datatable .Dispose ();
            }
          

        }
        public void DaGetReteivalReqBatchDtls(MdlIdasRecordDtls values, MdlTrnRequiredlist objResult)
        {

            msSQL = " SELECT a.retrievalrequestdtls_gid,a.batch_gid,a.despatch_gid,a.box_gid," +
                  " CONCAT(c.customer_urn,IF(c.customer_urn='','','/'),c.customer_name) as customer_name," +
                  " CONCAT(d.boxref_no, ' || ', IF(d.stampref_no IS NULL, '', d.stampref_no)) as boxref_no," +
                  " CONCAT(e.despatchref_no,' || ',IF(e.stampref_no IS NULL,'',e.stampref_no)) as despatchref_no," +
                  " CONCAT(c.batchref_no,' || ' ,IF(c.stampref_no IS NULL,'',c.stampref_no)) AS batchref_no,a.file_status," +
                  " date_format(e.despatch_date, '%d-%m-%Y') as despatch_date," +
                  " e.contact_person,e.despatched_by,date_format(d.cartonbox_date, '%d-%m-%Y') AS cartonbox_date" +
                  " FROM ids_trn_tretrievalrequestdtls a" +
                  " INNER JOIN ids_trn_tbatch c on a.batch_gid=c.batch_gid" +
                  " INNER JOIN ids_trn_tcartonbox d on a.box_gid=d.cartonbox_gid" +
                  " INNER JOIN ids_trn_tdespatch e on a.despatch_gid=e.despatch_gid" +
                  " WHERE a.retrievalrequest_gid='" + values.retrievalrequest_gid + "' ";
            if (values.documentretrieved_mode=="Customer")
            {
                msSQL += " AND a.customer_gid='"+ values.customer_gid  +"'";
            }
            else if(values .documentretrieved_mode =="Box")
            {
                msSQL += " AND a.box_gid='" + values.box_gid + "'";
            }
                 msSQL +=" ORDER BY a.created_date";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    objResult .MdlTrnRequired = dt_datatable.AsEnumerable().Select(row => new MdlTrnRequired
                    {
                        trn_gid = row["retrievalrequestdtls_gid"].ToString(),
                        batch_gid = row["batch_gid"].ToString(),
                        despatch_gid = row["despatch_gid"].ToString(),
                        box_gid = row["box_gid"].ToString(),
                        customer_name = row["customer_name"].ToString(),
                        boxref_no = row["boxref_no"].ToString(),
                        despatchref_no = row["despatchref_no"].ToString(),
                        batchref_no = row["batchref_no"].ToString(),
                        file_status = row["file_status"].ToString(),
                        despatch_date = row["despatch_date"].ToString(),
                        contact_person = row["contact_person"].ToString(),
                        despatched_by = row["despatched_by"].ToString(),
                        cartonbox_date = row["cartonbox_date"].ToString(),
                    }).ToList();

                }
                dt_datatable.Dispose();

        }
        public void DaPostUploadDocument(HttpRequest httpRequest,result objResult,string user_gid)
        {
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;
            String path = lspath;
            string lsdocument_title = httpRequest.Form["document_title"];
            string project_flag = httpRequest.Form["project_flag"].ToString();


            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "IDAS/RecordRetrieval/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

            if ((!System.IO.Directory.Exists(path)))
                System.IO.Directory.CreateDirectory(path);

            string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;
                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {
                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);
                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objResult.status = false;
                            objResult.message = "File format is not supported";
                            return;
                        }
                        //lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/RecordRetrieval/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");

                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);

                        //lspath = "../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/RecordRetrieval/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "IDAS/RecordRetrieval/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "IDAS/RecordRetrieval/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGID = objcmnfunctions.GetMasterGID("DOCU");
                        msSQL = " insert into ids_tmp_tretrievalapprovadoc( " +
                                    " tmpretrievalapprovadoc_gid ," +
                                    " document_name ," +
                                    " document_path," +
                                    " document_title," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGID + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + lsdocument_title + "'," +
                                    "'" + user_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            objResult.status = true;
                            objResult.message = "Document Uploaded Successfully";
                        }
                        else
                        {
                            objResult.status = false;
                            objResult.message = "Error Occured";
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public void DaGetTmpUploadDocument(MdlIdasUploadDocumentList  objResult,string user_gid)
        {
            msSQL = " SELECT tmpretrievalapprovadoc_gid,document_path,document_name,document_title" +
                    " FROM ids_tmp_tretrievalapprovadoc"+
                    " WHERE created_by='"+ user_gid+"'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
                if(dt_datatable.Rows.Count !=0)
            {
                objResult.MdlIdasUploadDocument = dt_datatable.AsEnumerable().Select(row => new MdlIdasUploadDocument
                {
                    uploaddocument_gid =row["tmpretrievalapprovadoc_gid"].ToString (),
                    document_name =row["document_name"].ToString (),
                    document_title = row["document_title"].ToString(),
                    document_path = objcmnstorage.EncryptData(row["document_path"].ToString ())

                }).ToList();
                   
            }
            dt_datatable.Dispose();
        }

        public void DaGetUploadDocument(MdlIdasUploadDocumentList objResult, string retrievalrequest_gid)
        {
            msSQL = " SELECT retrievalapprovadoc_gid,document_path,document_name,document_title" +
                    " FROM ids_trn_tretrievalapprovadoc" +
                    " WHERE retrievalrequest_gid='" + retrievalrequest_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                objResult.MdlIdasUploadDocument = dt_datatable.AsEnumerable().Select(row => new MdlIdasUploadDocument
                {
                    uploaddocument_gid = row["retrievalapprovadoc_gid"].ToString(),
                    document_name = row["document_name"].ToString(),
                    document_title = row["document_title"].ToString(),
                    document_path = (row["document_path"].ToString())

                }).ToList();

            }
            dt_datatable.Dispose();
        }

        public void DaPostDocReceivedDtls(MdlPostDocDtlReceived values,string user_gid,result objResult)
        {
            msSQL = " UPDATE ids_trn_tretrievalrequestdtls SET" +
                    " file_status='Received'," +
                    " received_date=current_timestamp," +
                    " receivedentry_by='" + user_gid + "'" +
                    " WHERE retrievalrequestdtls_gid='" + values.retrievalrequestdtls_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult ==1)
            {
                if (values.received_type=="Permanent")
                {
                    msSQL = " UPDATE ids_trn_tbatch SET" +
                            " status='Closed'"+
                            " WHERE batch_gid=(SELECT batch_gid FROM ids_trn_tretrievalrequestdtls WHERE retrievalrequestdtls_gid='"+ values.retrievalrequestdtls_gid  + "' )";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    msSQL = " UPDATE ids_trn_tbatch SET" +
                            " status='Re-Despatch'" +
                            " WHERE batch_gid=(SELECT batch_gid FROM ids_trn_tretrievalrequestdtls WHERE retrievalrequestdtls_gid='" + values.retrievalrequestdtls_gid + "' )";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }


                objResult.status = true;
                objResult.message = "Received";

            }
            else
            {
                objResult.status = false;
                objResult.message = "Error";
            }
        }

        public result DaPostDocReDespatched( string user_gid, MdlRedespatch values)
        {
            result objResult = new result();
            msGetGID = objcmnfunctions.GetMasterGID("REDS");

            msSQL = " INSERT INTO ids_trn_tredespatch(" +
                    " redespatch_gid," +
                    " redespatched_date," +
                    " redespatchedby_name," +
                    " vendor_name," +
                    " contact_person," +
                    " remarks," +
                    " created_by)"+
                    " VALUES("+
                    "'"+ msGetGID +"',"+
                    "'"+ Convert .ToDateTime (values.redespatched_date ).ToString ("yyyy-MM-dd")+"',"+                 
                    "'"+values.redespatchedby_name +"',"+
                    "'Crown',"+
                    "'"+values.contact_person.Replace ("'","") +"',"+
                    "'"+values.remarks.Replace ("'","") +"',"+
                    "'"+user_gid +"')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            foreach (string i in values.retrievalrequestdtls_gid)
            {
                msSQL = " UPDATE ids_trn_tretrievalrequestdtls SET" +
                        " file_status='Re-Despatched'," +
                        " redespatched_date=current_timestamp," +
                        " redespatch_gid='"+ msGetGID  + "',"+
                        " redespatchedentry_by='" + user_gid + "'" +
                        " WHERE retrievalrequestdtls_gid='" + i + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " UPDATE ids_trn_tbatch SET" +
                        " status='Re-Despatched'" +
                        " WHERE batch_gid in (SELECT batch_gid FROM ids_trn_tretrievalrequestdtls WHERE retrievalrequestdtls_gid='" + i + "' )";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult ==1)
            {
                objResult.status = true;
                objResult.message = "Redespatched Successfully";
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";
            }
            return objResult;
         
        }

        public void DaGetReDespatchSummary(MdlReDespatchSummaryList values)
        {
            msSQL = " SELECT a.redespatch_gid ,date_format( a.redespatched_date,'%d-%m-%Y') as redespatched_date," +
                    " a.redespatchedby_name, a.contact_person, a.remarks,date_format(a.created_date,'%d-%m-%Y') as created_date," +
                    " CONCAT(b.user_code,'/',b.user_firstname,b.user_lastname) as created_by"+
                    " FROM ids_trn_tredespatch a" +
                    " INNER JOIN adm_mst_tuser b on a.created_by=b.user_gid" +
                    " WHERE 1=1";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable .Rows.Count!=0)
            {

                values.MdlReDespatchSummary = dt_datatable.AsEnumerable().Select(row => new MdlReDespatchSummary
                {
                    redespatch_gid = row["redespatch_gid"].ToString(),
                    redespatched_date = row["redespatched_date"].ToString(),
                    created_date = row["created_date"].ToString(),
                    redespatchedby_name = row["redespatchedby_name"].ToString(),
                    contact_person = row["contact_person"].ToString(),
                    remarks = row["remarks"].ToString(),
                    created_by = row["created_by"].ToString(),

                }).ToList();
                values.status = true;
                values.message = "Record Found";
            }
            else
            {
                values.status = false;
                values.message = "No Record";
            }

        }

        public void DaGetReDespatchView(string redespatch_gid, MdlRedespatch360 values)
        {
            msSQL = " SELECT a.redespatch_gid ,DATE_FORMAT(a.redespatched_date,'%d-%m-%Y') as redespatched_date," +
                   " a.redespatchedby_name, a.contact_person, a.remarks,date_format(a.created_date,'%d-%m-%Y') as created_date," +
                   " CONCAT(b.user_code,'/',b.user_firstname,b.user_lastname) as created_by" +
                   " FROM ids_trn_tredespatch a" +
                   " INNER JOIN adm_mst_tuser b on a.created_by=b.user_gid" +
                   " WHERE a.redespatch_gid='"+ redespatch_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader .HasRows )
            {
                objODBCDataReader.Read();
                values.redespatch_gid = objODBCDataReader["redespatch_gid"].ToString();
                values.redespatched_date = objODBCDataReader["redespatched_date"].ToString();
                values.redespatchedby_name = objODBCDataReader["redespatchedby_name"].ToString();
                values.contact_person =objODBCDataReader ["contact_person"].ToString ();
                values.remarks = objODBCDataReader["remarks"].ToString();
                values.created_date = objODBCDataReader["created_date"].ToString();
                values.created_by = objODBCDataReader["created_by"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = " SELECT DISTINCT retrievalrequest_gid FROM ids_trn_tretrievalrequestdtls WHERE redespatch_gid='"+ redespatch_gid +"'";
           var retrievalrequest_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " SELECT " +
                 " retrievalrequest_gid,DATE_FORMAT(requested_date,'%d-%m-%Y') as requested_date," +
                 " requestedby_name,DATE_FORMAT(approved_date,'%d-%m-%Y') as approved_date,approvalby_name," +
                 " documentretrieved_status,retrieval_type,requested_for,req_remarks ,documentretrieved_mode" +
                 " FROM ids_trn_tretrievalrequest" +
                 " WHERE retrievalrequest_gid='" + retrievalrequest_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows)
            {
                objODBCDataReader.Read();
                values.retrievalrequest_gid = objODBCDataReader["retrievalrequest_gid"].ToString();
                values.requested_date = objODBCDataReader["requested_date"].ToString();
                values.requestedby_name = objODBCDataReader["requestedby_name"].ToString();
                values.approved_date = objODBCDataReader["approved_date"].ToString();
                values.approvalby_name = objODBCDataReader["approvalby_name"].ToString();
                values.documentretrieved_status = objODBCDataReader["documentretrieved_status"].ToString();
                values.retrieval_type = objODBCDataReader["retrieval_type"].ToString();
                values.requested_for = objODBCDataReader["requested_for"].ToString();
                values.req_remarks = objODBCDataReader["req_remarks"].ToString();
                values.documentretrieved_mode = objODBCDataReader["documentretrieved_mode"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = " SELECT retrievalapprovadoc_gid,document_path,document_name,document_title" +
                   " FROM ids_trn_tretrievalapprovadoc" +
                   " WHERE retrievalrequest_gid='" + retrievalrequest_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlIdasUploadDocument = dt_datatable.AsEnumerable().Select(row => new MdlIdasUploadDocument
                {
                    uploaddocument_gid = row["retrievalapprovadoc_gid"].ToString(),
                    document_name = row["document_name"].ToString(),
                    document_title = row["document_title"].ToString(),
                    document_path = (row["document_path"].ToString())

                }).ToList();

            }
            dt_datatable.Dispose();
            msSQL = " SELECT a.retrievalrequestdtls_gid,a.batch_gid,a.despatch_gid,a.box_gid," +
                 " CONCAT(b.customer_urn,IF(b.customer_urn='','','/'),b.customername) as customer_name," +
                 " CONCAT(d.boxref_no, ' || ', IF(d.stampref_no IS NULL, '', d.stampref_no)) as boxref_no," +
                 " CONCAT(e.despatchref_no,' || ',IF(e.stampref_no IS NULL,'',e.stampref_no)) as despatchref_no," +
                 " CONCAT(c.batchref_no,' || ' ,IF(c.stampref_no IS NULL,'',c.stampref_no)) AS batchref_no,a.file_status," +
                 " date_format(e.despatch_date, '%d-%m-%Y') as despatch_date," +
                 " e.contact_person,e.despatched_by,date_format(d.cartonbox_date, '%d-%m-%Y') AS cartonbox_date" +
                 " FROM ids_trn_tretrievalrequestdtls a" +
                 " INNER JOIN ocs_mst_tcustomer b on a.customer_gid=b.customer_gid" +
                 " INNER JOIN ids_trn_tbatch c on a.batch_gid=c.batch_gid" +
                 " INNER JOIN ids_trn_tcartonbox d on a.box_gid=d.cartonbox_gid" +
                 " INNER JOIN ids_trn_tdespatch e on a.despatch_gid=e.despatch_gid" +
                 " WHERE a.redespatch_gid='" + redespatch_gid  + "' ";
            msSQL += " ORDER BY a.created_date";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlTrnRequired = dt_datatable.AsEnumerable().Select(row => new MdlTrnRequired
                {
                    trn_gid = row["retrievalrequestdtls_gid"].ToString(),
                    batch_gid = row["batch_gid"].ToString(),
                    despatch_gid = row["despatch_gid"].ToString(),
                    box_gid = row["box_gid"].ToString(),
                    customer_name = row["customer_name"].ToString(),
                    boxref_no = row["boxref_no"].ToString(),
                    despatchref_no = row["despatchref_no"].ToString(),
                    batchref_no = row["batchref_no"].ToString(),
                    file_status = row["file_status"].ToString(),
                    despatch_date = row["despatch_date"].ToString(),
                    contact_person = row["contact_person"].ToString(),
                    despatched_by = row["despatched_by"].ToString(),
                    cartonbox_date = row["cartonbox_date"].ToString(),
                }).ToList();

            }
            dt_datatable.Dispose();


        }
        public void DaGetBatchList(MdlBatchList values, MdlReferene objReference)
        {
            try
            {
               
                if (objReference. reference_type == "Customer")
                {
                    if (objReference.MdlCustomer.Count!=0)
                    {
                        for (int i = 0; i < objReference.MdlCustomer.Count; i++)
                        {
                            sb1.Append("'" + objReference.MdlCustomer[i].customer_gid + "',");

                        }
                        reference_gid = sb1.ToString();
                        sb1.Clear();
                        reference_gid = reference_gid.Trim(",".ToCharArray());
                    }
                    else
                    {
                        reference_gid = "null";
                    }
                   
                }
                else if (objReference.reference_type == "Box")
                {
                    if (objReference.MdlGetBox .Count != 0)
                    {
                        for (int i = 0; i < objReference.MdlGetBox .Count; i++)
                        {
                            sb1.Append("'" + objReference.MdlGetBox [i].box_gid + "',");

                        }
                        reference_gid = sb1.ToString();
                        sb1.Clear();
                        reference_gid = reference_gid.Trim(",".ToCharArray());
                    }
                    else
                    {
                        reference_gid = "null";
                    }

                }
                else
                {
                    reference_gid = "null";
                }
               
              
                msSQL = " SELECT DISTINCT a.batch_gid,a.customer_gid,b.cartonbox_gid,c.despatch_gid," +
                  " CONCAT(a.customer_urn, IF(a.customer_urn = '', '', ' / '), a.customer_name) as customername," +
                  " a.batchref_no as fileref_no,IF(a.stampref_no IS NULL, '', a.stampref_no) AS filestampref_no," +
                  " IF(e.stampref_no IS NULL, '', e.stampref_no) AS boxstampref_no,date_format(e.cartonbox_date, '%d-%m-%Y') AS cartonbox_date," +
                  " f.despatchref_no,date_format(f.despatch_date, '%d-%m-%Y') as despatch_date," +
                  " f.contact_person,f.despatched_by" +
                  " FROM ids_trn_tbatch a" +
                  " INNER JOIN ids_trn_tbox2file b ON a.batch_gid = b.batch_gid" +
                  " INNER JOIN ids_trn_tbox2despatch c ON b.cartonbox_gid = c.cartonbox_gid" +
                  " INNER JOIN ids_trn_tcartonbox e ON b.cartonbox_gid = e.cartonbox_gid" +
                  " INNER JOIN ids_trn_tdespatch f ON c.despatch_gid = f.despatch_gid" +
                  " WHERE a.batch_gid not  in (SELECT batch_gid FROM ids_trn_tretrievalrequestdtls WHERE file_status in( 'Pending','Received')) AND a.status <> 'Closed'";
                if (objReference.reference_type == "Customer" && reference_gid != "null")
                {
                    msSQL += " AND a.customer_gid IN (" + reference_gid + ")";
                }
                if (objReference . reference_type == "Box" && reference_gid != "null")
                {
                    msSQL += " AND b.cartonbox_gid IN (" + reference_gid + ")";
                }
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    values.MdlGetBatch = dt_datatable.AsEnumerable().Select(row => new MdlGetBatch
                    {
                        batch_gid = row["batch_gid"].ToString(),
                        customer_gid = row["customer_gid"].ToString(),
                        cartonbox_gid = row["cartonbox_gid"].ToString(),
                        despatch_gid = row["despatch_gid"].ToString(),
                        customername = row["customername"].ToString(),
                        fileref_no = row["fileref_no"].ToString(),
                        filestampref_no = row["filestampref_no"].ToString(),
                        boxstampref_no = row["boxstampref_no"].ToString(),
                        cartonbox_date = row["cartonbox_date"].ToString(),
                        despatchref_no = row["despatchref_no"].ToString(),
                        despatch_date = row["despatch_date"].ToString(),
                        contact_person = row["contact_person"].ToString(),
                        despatched_by = row["despatched_by"].ToString()
                    }).ToList();
                    values.status = true;
                    values.message = "Success";
                }
                else
                {
                    values.status = false;
                    values.message = "No Record";
                }
                dt_datatable.Dispose();

            }
            catch (Exception ex)
            {
                values.status = false;
                values.message =ex.Message ;
            }
                  }

        public void DaGetReconciliationCount(MdlReconciliationCount values,MdlReferene objReference)
        {
            if (objReference.reference_type == "Customer")
            {
                if (objReference.MdlCustomer.Count != 0)
                {
                    for (int i = 0; i < objReference.MdlCustomer.Count; i++)
                    {
                        sb1.Append("'" + objReference.MdlCustomer[i].customer_gid + "',");

                    }
                    reference_gid = sb1.ToString();
                    sb1.Clear();
                    reference_gid = reference_gid.Trim(",".ToCharArray());
                }
                else
                {
                    reference_gid = "null";
                }

            }
            else if (objReference.reference_type == "Box")
            {
                if (objReference.MdlGetBox.Count != 0)
                {
                    for (int i = 0; i < objReference.MdlGetBox.Count; i++)
                    {
                        sb1.Append("'" + objReference.MdlGetBox[i].box_gid + "',");

                    }
                    reference_gid = sb1.ToString();
                    sb1.Clear();
                    reference_gid = reference_gid.Trim(",".ToCharArray());
                }
                else
                {
                    reference_gid = "null";
                }

            }
            else
            {
                reference_gid = "null";
            }


            if (objReference.reference_type  == "Customer")
            {
                msSQL = " select * from " +
                 " (SELECT count(*) as file_count FROM ids_trn_tbatch WHERE customer_gid IN ("+ reference_gid + ")) as a," +
                 " (SELECT count(*) as despatch_count" +
                 " FROM ids_trn_tbatch a" +
                 " INNER JOIN ids_trn_tbox2file b on a.batch_gid = b.batch_gid" +
                 " INNER JOIN ids_trn_tbox2despatch c on b.cartonbox_gid = c.cartonbox_gid" +
                 " WHERE customer_gid IN ("+ reference_gid + ")) as b," +
                 " (SELECT COUNT(*) as permanent_count" +
                 " FROM ids_trn_tretrievalrequestdtls a" +
                 " INNER JOIN ids_trn_tretrievalrequest b ON a.retrievalrequest_gid = b.retrievalrequest_gid" +
                 " WHERE a.customer_gid IN( "+ reference_gid + ") AND b.retrieval_type = 'Permanent') as c ," +
                 " (SELECT COUNT(*) as temproary_count" +
                 " FROM ids_trn_tretrievalrequestdtls a" +
                 " INNER JOIN ids_trn_tretrievalrequest b ON a.retrievalrequest_gid = b.retrievalrequest_gid" +
                 " WHERE a.customer_gid IN( "+ reference_gid + ") AND b.retrieval_type = 'Temporary' and a.file_status not in ('Pending','Received')) as d ";
            }
            if(objReference.reference_type  == "Box")
            {
                msSQL = " SELECT * from " +
                        " (SELECT count(*) as file_count" +
                        " FROM ids_trn_tbox2file b" +
                        " WHERE b.cartonbox_gid IN ("+ reference_gid + ")) as a," +
                        " (SELECT count(*) as despatch_count" +
                        " FROM ids_trn_tbox2file b" +
                        " INNER JOIN ids_trn_tbox2despatch c on b.cartonbox_gid = c.cartonbox_gid" +
                        " WHERE b.cartonbox_gid IN ("+ reference_gid + ")) as b," +
                        " (SELECT COUNT(*) as permanent_count" +
                        " FROM ids_trn_tretrievalrequestdtls a" +
                        " INNER JOIN ids_trn_tretrievalrequest b ON a.retrievalrequest_gid = b.retrievalrequest_gid" +
                        " WHERE a.box_gid IN ( "+ reference_gid + ") AND b.retrieval_type = 'Permanent') as c ," +
                        " (SELECT COUNT(*) as temproary_count" +
                        " FROM ids_trn_tretrievalrequestdtls a" +
                        " INNER JOIN ids_trn_tretrievalrequest b ON a.retrievalrequest_gid = b.retrievalrequest_gid" +
                        " WHERE a.box_gid IN ("+ reference_gid + ") AND b.retrieval_type = 'Temporary' and a.file_status not in ('Pending','Received')) as d ";
            }
            if (objReference .reference_type == "File")
            {
                msSQL = " select * from " +
                " (SELECT count(*) as file_count FROM ids_trn_tbatch WHERE 1=1) as a," +
                " (SELECT count(*) as despatch_count" +
                " FROM ids_trn_tbatch a" +
                " INNER JOIN ids_trn_tbox2file b on a.batch_gid = b.batch_gid" +
                " INNER JOIN ids_trn_tbox2despatch c on b.cartonbox_gid = c.cartonbox_gid" +
                " WHERE 1=1) as b," +
                " (SELECT COUNT(*) as permanent_count" +
                " FROM ids_trn_tretrievalrequestdtls a" +
                " INNER JOIN ids_trn_tretrievalrequest b ON a.retrievalrequest_gid = b.retrievalrequest_gid" +
                " WHERE 1=1 AND b.retrieval_type = 'Permanent') as c ," +
                " (SELECT COUNT(*) as temproary_count" +
                " FROM ids_trn_tretrievalrequestdtls a" +
                " INNER JOIN ids_trn_tretrievalrequest b ON a.retrievalrequest_gid = b.retrievalrequest_gid" +
                " WHERE 1=1 AND b.retrieval_type = 'Temporary' and a.file_status not in ('Pending','Received')) as d ";
            }
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows)
            {
                objODBCDataReader.Read();
                values.file_count = objODBCDataReader["file_count"].ToString();
                values.despatched_count = objODBCDataReader["despatch_count"].ToString();
                values.permanet_count = objODBCDataReader["permanent_count"].ToString();
                values.temporary_count = objODBCDataReader["temproary_count"].ToString();
                values.status = true;
                values.message = "Success";
          
            }
            else
            {
                values.file_count = "0";
                values.despatched_count = "0";
                values.permanet_count = "0";
                values.temporary_count ="0";
                values.status = true;
                values.message = "Failure";
            }
            objODBCDataReader.Close();


        }

        public void DaGetBoxList(MdlBoxList values)
        {
            try
            {
                msSQL = " SELECT a.cartonbox_gid,concat(boxref_no ,' || ', if( stampref_no is null,'',stampref_no)) as boxref_no" +
                        " FROM ids_trn_tcartonbox a" +
                        " INNER JOIN ids_trn_tbox2despatch b on a.cartonbox_gid = b.cartonbox_gid" +
                        " WHERE 1 = 1";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    values.MdlGetBox = dt_datatable.AsEnumerable().Select(row => new MdlGetBox
                    {
                        box_gid =row["cartonbox_gid"].ToString (),
                        boxref_no =row["boxref_no"].ToString (),
                    }).ToList();
                    values.status = true;
                    values.message = "Success";
                }
                else
                {
                    values.status = false;
                    values.message = "No Record";
                }
                }
            catch(Exception ex)
            {
                values.status = false;
                values.message = ex.Message;
            }
          


        }

        public void DaPostDocReceived(MdlDocumentReceived values,result objResult,string user_gid)
        {
            msGetGID = objcmnfunctions.GetMasterGID("DRCV");

            msSQL = " INSERT INTO ids_trn_documentreceiveddtls (" +
                    " documentreceived_gid," +
                    " retrievalrequest_gid," +
                    " documentretrieved_mode," +
                    " documentretrieved_date," +
                    " documentreceivedto_gid," +
                    " documentreceivedto_name," +
                    " contactperson_name," +
                    " mobile_no," +
                    " created_by)" +
                    " values (" +
                    "'" + msGetGID + "'," +
                    "'" + values.retrievalrequest_gid + "'," +
                    "'" + values.documentretrieved_mode + "'," +
                    "'" + Convert.ToDateTime(values.documentretrieved_date).ToString("yyyy-MM-dd") + "'," +
                    "'" + values.documentreceivedto_gid + "'," +
                    "'"+ values.documentreceivedto_name +"',"+
                    "'" + values.contactperson_name.Replace("'", "") + "'," +
                    "'" + values.mobile_no + "',"+
                    "'" + user_gid + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult ==1)
            { 
                msSQL = " UPDATE ids_trn_tretrievalrequest SET documentretrieved_status='Received' WHERE retrievalrequest_gid='" + values.retrievalrequest_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " UPDATE ids_trn_tretrievalrequestdtls SET" +
                 " file_status='Received'," +
                 " received_date=current_timestamp," +
                 " receivedentry_by='" + user_gid + "'" +
                 " WHERE retrievalrequest_gid='" + values.retrievalrequest_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (values.received_mode == "Permanent")
                {
                    msSQL = " UPDATE ids_trn_tbatch SET" +
                            " status='Closed'" +
                            " WHERE batch_gid in (SELECT batch_gid FROM ids_trn_tretrievalrequestdtls WHERE retrievalrequest_gid='" + values.retrievalrequest_gid + "' )";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    msSQL = " UPDATE ids_trn_tbatch SET" +
                            " status='Received'" +
                            " WHERE batch_gid in (SELECT batch_gid FROM ids_trn_tretrievalrequestdtls WHERE retrievalrequest_gid='" + values.retrievalrequest_gid + "' )";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }

                if (mnResult ==1)
                {
                    objResult.status = true;
                    objResult.message = "Record Received Creation Successfully";
                }
              
            }
        }

        public void DaGetDocReceived(string retrievalrequest_gid, MdlDocumentReceived values)
        {
            msSQL =" SELECT "+
                   " documentreceived_gid, retrievalrequest_gid, documentretrieved_mode,DATE_FORMAT( documentretrieved_date,'%d-%m-%Y') as documentretrieved_date ," +
                   " documentreceivedto_gid, documentreceivedto_name, contactperson_name, mobile_no"+
                   " FROM ids_trn_documentreceiveddtls" +
                   " where retrievalrequest_gid='" + retrievalrequest_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader .HasRows )
            {
                objODBCDataReader.Read();
                values.documentretrieved_mode = objODBCDataReader["documentretrieved_mode"].ToString();
                values.documentretrieved_date = objODBCDataReader["documentretrieved_date"].ToString();
                values.documentreceivedto_gid = objODBCDataReader["documentreceivedto_gid"].ToString();

                values.documentreceivedto_name = objODBCDataReader["documentreceivedto_name"].ToString();
                values.contactperson_name = objODBCDataReader["contactperson_name"].ToString();
                values.mobile_no = objODBCDataReader["mobile_no"].ToString();
            }
            objODBCDataReader.Close();
        }

        public void DaGetDespatchedCusomer(MdlCustomerList values)
        {
            msSQL = " SELECT DISTINCT CONCAT(a.customername,' / ',a.customer_urn) AS customer_name,a.customer_gid" +
                    " FROM ocs_mst_tcustomer a" +
                    " INNER JOIN ids_trn_tbatch b on a.customer_gid = b.customer_gid" +
                    " INNER JOIN ids_trn_tbox2file c on b.batch_gid = c.batch_gid" +
                    " INNER JOIN ids_trn_tbox2despatch d on c.cartonbox_gid = d.cartonbox_gid" +
                    " WHERE 1 = 1";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable .Rows.Count!=0)
            {
                if (dt_datatable.Rows.Count != 0)
                {
                    values.MdlCustomer  = dt_datatable.AsEnumerable().Select(row => new MdlCustomer 
                    {
                        customer_gid = row["customer_gid"].ToString(),
                        customername = row["customer_name"].ToString(),

                    }).ToList();
                }
                dt_datatable.Dispose();

            }
        }

        public result  DaPostEnsure(String retrievalrequestdtls_gid)
        {
            result objResult = new Models.result();

            msSQL = " UPDATE ids_trn_tretrievalrequestdtls SET" +
                    " ensure_flag='Y'"+
                    " WHERE retrievalrequestdtls_gid='"+ retrievalrequestdtls_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult ==1)
            {
                objResult.message  = "Ensured";
                objResult.status = true;
                return objResult;
            }
            else
            {
                objResult.message = "Error";
                objResult.status = false;
                return objResult;
            }

        }
    }
}