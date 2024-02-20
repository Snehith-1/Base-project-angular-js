using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.idas.Models;
using System.Configuration;


namespace ems.idas.DataAccess
{
    public class DaIdasTrnPhysicalDoc
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        DataTable dt_child;
        string msSQL;
        OdbcDataReader objODBCDataReader;
        int mnResult;
        string msGetGID, msGETGIDDoc;
        string lsuser_name;
        string lsecms_status = string.Empty;

        #region GetPhysicalDocumentSummary
        public void DaGetPhyDocPendingSummary(MdlPhyDocSummaryList values)
        {
            msSQL = " SELECT DISTINCT a.customer2sanction_gid, b.customer_urn,b.customername,a.sanction_refno," +
                    " DATE_FORMAT(a.sanction_date, '%d-%m-%Y') as sanction_date," +
                    " FORMAT(a.sanction_amount, 2) as sanction_amount," +
                    " (SELECT if(COUNT(*) IS NULL,0,COUNT(*)) FROM ids_trn_tsanctiondocumentdtls b" +
                    " WHERE a.customer2sanction_gid = b.sanction_gid GROUP BY a.customer2sanction_gid) AS tagged_document," +
                    " (SELECT if(COUNT(*) IS NULL,0,COUNT(*)) FROM ids_trn_tsanctiondocumentdtls b" +
                    "  WHERE a.customer2sanction_gid = b.sanction_gid AND b.phydoc_status='Verified'" +
                    " GROUP BY a.customer2sanction_gid) AS phydocverified_count," +
                    " (SELECT COUNT(*) FROM ids_trn_tsanctiondocumentdtls b" +
                    "  WHERE a.customer2sanction_gid = b.sanction_gid AND B.maker_status = 'Maker Confirmed'" +
                    " GROUP BY a.customer2sanction_gid) AS makerconfirmed_count," +
                    " (select group_concat(distinct maker_name) from ids_trn_tsanctiondocumentdtls x where x.sanction_gid=a.customer2sanction_gid) as maker_name," +
                    " a.batch_status," +
                    " IF(a.fileref_no is null, '-', a.fileref_no) as fileref_no" +
                    " FROM ocs_mst_tcustomer2sanction a" +
                    " INNER JOIN ocs_mst_tcustomer b on a.customer_gid=b.customer_gid" +
                    " LEFT JOIN ids_trn_tbatch c on a.customer2sanction_gid=c.sanction_gid" +
                    " INNER JOIN ids_trn_tsanctiondocumentdtls d ON a.customer2sanction_gid=d.sanction_gid " +
                    " LEFT JOIN ids_trn_tlsa e on a.customer2sanction_gid = e.customer2sanction_gid" +
                    " WHERE a.batch_status='Pending' AND d.maker_status<> 'Maker Pending' AND d.checker_status<>'Checker Pending'" +
                    " AND ((((e.approval_status is null or e.approval_status in ('Pending','-')) AND a.entity_gid='CENT1905210002') OR e.approval_status = 'Approved')" +
                    " OR a.lsa_status='No') AND a.customer2sanction_gid not in" +
                    " (SELECT DISTINCT sanction_gid FROM ids_trn_tsanctiondocumentdtls WHERE maker_status='Maker Pending' and checker_status='Checker Pending') " +
                    " ORDER BY a.batch_status DESC, c.created_date DESC  ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var DocSummary = new List<MdlPhyDocSummary>();
            if (dt_datatable.Rows.Count != 0)
            {

                foreach (DataRow docSummary in dt_datatable.Rows)
                {
                    msSQL = " SELECT a.record_id ,b.approval_status" +
                          " FROM ocs_trn_tdeferral a" +
                          " INNER join ocs_trn_tdeferralapproval b on a.deferral_gid = b.deferral_gid" +
                          " LEFT JOIN ocs_trn_tloan c on a.loan_gid = c.loan_gid" +
                          " WHERE c.sanction_gid = '" + docSummary["customer2sanction_gid"].ToString() + "' and a.tracking_type = 'Deferral' and a.deferral_name = 'Original Documents'";
                    dt_child = objdbconn.GetDataTable(msSQL);
                    var Get_Summary = new List<MdlEcmsStatus>();
                    if (dt_child.Rows.Count != 0)
                    {
                        Get_Summary = dt_child.AsEnumerable().Select(row => new MdlEcmsStatus
                        {
                            record_id = row["record_id"].ToString(),
                            approval_status = row["approval_status"].ToString(),

                        }).ToList();
                        lsecms_status = "Y";
                    }
                    else
                    {
                        lsecms_status = "N";
                    }
                    dt_child.Dispose();
                    DocSummary.Add(new MdlPhyDocSummary
                    {
                        sanction_gid = docSummary["customer2sanction_gid"].ToString(),
                        customer_urn = docSummary["customer_urn"].ToString(),
                        customer_name = docSummary["customername"].ToString(),
                        sanction_refno = docSummary["sanction_refno"].ToString(),

                        sanction_date = docSummary["sanction_date"].ToString(),
                        sanction_amount = docSummary["sanction_amount"].ToString(),
                        tagged_document = docSummary["tagged_document"].ToString(),
                        phydocverified_count = docSummary["phydocverified_count"].ToString(),
                        batch_status = docSummary["batch_status"].ToString(),
                        fileref_no = docSummary["fileref_no"].ToString(),
                        scandocverified_count = docSummary["makerconfirmed_count"].ToString(),
                        maker_name = docSummary["maker_name"].ToString(),
                        ecms_status = lsecms_status,
                        MdlEcmsStatusList = Get_Summary

                    });


                }
                values.MdlPhyDocSummary = DocSummary;



                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Records Found";
            }
            dt_datatable.Dispose();
        }
        public void DaGetPhyDocCreatedSummary(MdlPhyDocSummaryList values)
        {
            msSQL = " SELECT a.customer2sanction_gid, b.customer_urn,b.customername,a.sanction_refno," +
                    " DATE_FORMAT(a.sanction_date, '%d-%m-%Y') as sanction_date," +
                    " FORMAT(a.sanction_amount, 2) as sanction_amount," +
                    " (SELECT if(COUNT(*) IS NULL,0,COUNT(*)) FROM ids_trn_tsanctiondocumentdtls b" +
                    " WHERE a.customer2sanction_gid = b.sanction_gid GROUP BY a.customer2sanction_gid) AS tagged_document," +
                    " (SELECT if(COUNT(*) IS NULL,0,COUNT(*)) FROM ids_trn_tsanctiondocumentdtls b" +
                    "  WHERE a.customer2sanction_gid = b.sanction_gid AND b.phydoc_status='Verified'" +
                    " GROUP BY a.customer2sanction_gid) AS phydocverified_count," +
                    " (select group_concat(distinct maker_name) from ids_trn_tsanctiondocumentdtls x where x.sanction_gid=a.customer2sanction_gid) as maker_name," +
                    " a.batch_status," +
                    " IF(a.fileref_no is null, '-', a.fileref_no) as fileref_no" +
                    " FROM ocs_mst_tcustomer2sanction a" +
                    " INNER JOIN ocs_mst_tcustomer b on a.customer_gid=b.customer_gid" +
                    " LEFT JOIN ids_trn_tbatch c on a.customer2sanction_gid=c.sanction_gid" +
                    " WHERE a.batch_status='Batch Created' AND a.customer2sanction_gid not in (SELECT DISTINCT sanction_gid FROM ids_trn_tsanctiondocumentdtls WHERE maker_status='Maker Pending' and checker_status='Checker Pending') " +
                    " ORDER BY a.batch_status DESC, c.created_date DESC  ";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            if (dt_datatable.Rows.Count != 0)
            {
                var DocSummary = new List<MdlPhyDocSummary>();
                foreach (DataRow docSummary in dt_datatable.Rows)
                {
                    msSQL = " SELECT a.record_id ,b.approval_status" +
                         " FROM ocs_trn_tdeferral a" +
                         " INNER join ocs_trn_tdeferralapproval b on a.deferral_gid = b.deferral_gid" +
                         " LEFT JOIN ocs_trn_tloan c on a.loan_gid = c.loan_gid" +
                         " WHERE c.sanction_gid = '" + docSummary["customer2sanction_gid"].ToString() + "' and a.tracking_type = 'Deferral' and a.deferral_name = 'Original Documents'";
                    dt_child = objdbconn.GetDataTable(msSQL);
                    var Get_Summary = new List<MdlEcmsStatus>();
                    if (dt_child.Rows.Count != 0)
                    {
                        Get_Summary = dt_child.AsEnumerable().Select(row => new MdlEcmsStatus
                        {
                            record_id = row["record_id"].ToString(),
                            approval_status = row["approval_status"].ToString(),

                        }).ToList();
                        lsecms_status = "Y";
                    }
                    else
                    {
                        lsecms_status = "N";
                    }
                    dt_child.Dispose();

                    DocSummary.Add(new MdlPhyDocSummary
                    {
                        sanction_gid = docSummary["customer2sanction_gid"].ToString(),
                        customer_urn = docSummary["customer_urn"].ToString(),
                        customer_name = docSummary["customername"].ToString(),
                        sanction_refno = docSummary["sanction_refno"].ToString(),

                        sanction_date = docSummary["sanction_date"].ToString(),
                        sanction_amount = docSummary["sanction_amount"].ToString(),
                        tagged_document = docSummary["tagged_document"].ToString(),
                        phydocverified_count = docSummary["phydocverified_count"].ToString(),
                        batch_status = docSummary["batch_status"].ToString(),
                        fileref_no = docSummary["fileref_no"].ToString(),
                        maker_name = docSummary["maker_name"].ToString(),
                        ecms_status = lsecms_status,
                        MdlEcmsStatusList = Get_Summary

                    });
                    values.status = true;
                    values.message = "Success";
                }
                values.MdlPhyDocSummary = DocSummary;
            }
            else
            {
                values.status = false;
                values.message = "No Records Found";
            }
            dt_datatable.Dispose();
        }


        public void DaGetPhyDocUnVerifiedCount(string sanction_gid, MdlPhyDocUnverifiedCount values)
        {
            msSQL = " SELECT if(COUNT(*) IS NULL,0,COUNT(*)) AS phydocunverified_count FROM ids_trn_tsanctiondocumentdtls b" +
                    "  WHERE b.sanction_gid ='" + sanction_gid + "' AND b.phydoc_status='Pending'" +
                    " GROUP BY b.sanction_gid";
            values.phydocunverified_count = objdbconn.GetExecuteScalar(msSQL);

        }

        #endregion

        // physical document remarks
        public void DaPostPhyFinalRemarks(MdlScannDocSummary values, string user_gid)
        {
            var lsuser_name = objdbconn.GetExecuteScalar("SELECT CONCAT(user_firstname,' ',user_lastname) as user_name FROM adm_mst_tuser WHERE user_gid='" + user_gid + "'");

            msSQL = " UPDATE ids_trn_tsanctiondocumentdtls SET" +
                " phyfinal_remarks='" + values.phyfinal_remarks + "'," +
                " phyfinalremarks_by='" + lsuser_name + "'," +
                " phyfinalremarks_on=current_timestamp," +
                " phyfinalremarksuser_gid='" + user_gid + "'" +
                " WHERE sanctiondocument_gid='" + values.sanctiondocument_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Final Remarks Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }



        public void DaPostTypesOfCopy(types_of_copy objResult)
        {
            msSQL = " UPDATE ids_trn_tsanctiondocumentdtls SET" +
                    " phydocument_type='" + objResult.type_copy + "'" +
                    " WHERE sanctiondocument_gid='" + objResult.sanctiondocument_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                objResult.status = true;
                objResult.message = "Types of Document Updated Successfully..!";
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";

            }
        }

        #region StatusVerification
        public void DaPostPhyDocVerify(string user_gid, string sanctiondocument_gid, result objResult)
        {
            msSQL = " SELECT phyfinal_remarks FROM ids_trn_tsanctiondocumentdtls " +
                   " WHERE sanctiondocument_gid='" + sanctiondocument_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                if (objODBCDataReader["phyfinal_remarks"].ToString() == null || objODBCDataReader["phyfinal_remarks"].ToString() == "")
                {
                    objODBCDataReader.Close();
                    objResult.status = false;
                    objResult.message = "Final Remarks Need to Document Verified";
                    return;
                }


            }
            objODBCDataReader.Close();

            msSQL = " UPDATE ids_trn_tsanctiondocumentdtls SET" +
                    " phydoc_status='Verified'," +
                    " phydocverified_gid='" + user_gid + "'," +
                    " phydocverifier_name=(SELECT concat(user_firstname,' ',user_lastname)as user  FROM adm_mst_tuser WHERE user_gid='" + user_gid + "')," +
                    " Phydocverified_on=current_timestamp" +
                    " WHERE sanctiondocument_gid='" + sanctiondocument_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                objResult.status = true;
                objResult.message = "Verified";
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";
            }
        }

        #endregion

        //batch create
        public void DaPostBatch(MdlBatch values, string user_gid)
        {
            msGetGID = objcmnfunctions.GetMasterGID("FILE");
            msGETGIDDoc = objcmnfunctions.GetMasterGID("BTCH");

            msSQL = " UPDATE ocs_mst_tcustomer2sanction SET" +
                    " batch_status='Batch Created'," +
                    " fileref_no='" + values.sanctionref_no + "/" + msGetGID + "'" +
                    " where customer2sanction_gid='" + values.sanction_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            var lsuser_name = objdbconn.GetExecuteScalar("SELECT CONCAT(user_firstname,' ',user_lastname) as user_name FROM adm_mst_tuser WHERE user_gid='" + user_gid + "'");

            msSQL = " INSERT INTO ids_trn_tbatch(" +
                    " batch_gid ," +
                    " batchref_no ," +
                    " customer_gid," +
                    " customer_urn," +
                    " customer_name," +
                    " sanction_gid ," +
                    " sanctionref_no," +
                    " batchcreated_name ," +
                    " created_by ," +
                    " created_date )" +
                    " VALUES(" +
                    "'" + msGETGIDDoc + "'," +
                    "'" + values.sanctionref_no + "/" + msGetGID + "'," +
                    "'" + values.customer_gid + "'," +
                    "'" + values.customer_urn + "'," +
                    "'" + values.customer_name + "'," +
                    "'" + values.sanction_gid + "'," +
                    "'" + values.sanctionref_no + "'," +
                    "'" + lsuser_name + "'," +
                    "'" + user_gid + "'," +
                    "current_timestamp)";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Batch Created Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        //physical document date update
        public void DaPostPhyDocumentDate(MdlScannDocSummary values)
        {
            DateTime documentdate = DateTime.Parse(Convert.ToDateTime(values.document_date).ToShortDateString());
            DateTime nowdate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

            if (documentdate > nowdate)
            {
                values.status = false;
                values.message = "Future Date is Not Allowed...";
                return;
            }

            msSQL = " UPDATE ids_trn_tsanctiondocumentdtls SET" +
                    " phydocument_date='" + Convert.ToDateTime(values.document_date).ToString("yyyy-MM-dd") + "'" +
                    " WHERE sanctiondocument_gid='" + values.sanctiondocument_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Updated...!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        public void DaPostPhyDocQuery(MdlDocConversation values, string user_gid)
        {
            msSQL = " SELECT phydocument_type FROM ids_trn_tsanctiondocumentdtls" +
            " WHERE sanctiondocument_gid='" + values.sanctiondocument_gid + "' AND phydocument_type is not null";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == false)
            {
                objODBCDataReader.Close();
                values.message = "Kindly Update the Types of Document";
                values.status = false;
                return;
            }
            objODBCDataReader.Close();

            msSQL = " SELECT phydocument_date FROM ids_trn_tsanctiondocumentdtls" +
                   " WHERE sanctiondocument_gid='" + values.sanctiondocument_gid + "' AND phydocument_date is not null";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == false)
            {
                objODBCDataReader.Close();
                values.message = "Kindly Update the Document Date";
                values.status = false;
                return;
            }
            objODBCDataReader.Close();

            int query_no = 0;
            lsuser_name = objdbconn.GetExecuteScalar("SELECT concat(user_firstname,' ',user_lastname)as user  FROM adm_mst_tuser WHERE user_gid='" + user_gid + "'");

            var lsquery_no = objdbconn.GetExecuteScalar("SELECT COUNT(*) FROM ids_trn_tdocconversation WHERE sanctiondocument_gid='" + values.sanctiondocument_gid + "' AND type_of_conversation='" + values.type_of_conversation + "' AND type_of_doc='Physical Copy'");
            if (lsquery_no == "")
            {
                query_no = 1;
            }
            else
            {
                query_no = Convert.ToInt16(lsquery_no) + 1;
            }

            msGetGID = objcmnfunctions.GetMasterGID("DOCV");

            msGETGIDDoc = objcmnfunctions.GetMasterGID("DOCC");


            msSQL = " SELECT docconversation_gid  from ids_trn_tdocconversation where reference_query='" + values.reference_query + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == false)
            {
                objODBCDataReader.Close();
                msSQL = " INSERT INTO ids_trn_tdocconversation(" +
                        " docconversation_gid ," +
                        " sanctiondocument_gid," +
                        " sanction_gid," +
                        " document_gid," +
                        " document_name," +
                        " docconversationref_no," +
                        " reference_query," +
                        " type_of_conversation," +
                        " type_of_doc," +
                        " cad_query," +
                        " cad_name," +
                        " cad_gid," +
                        " cadquery_on," +
                        " query_no," +
                        " created_by," +
                        " created_date)" +
                    " values(" +
                    "'" + msGetGID + "'," +
                    "'" + values.sanctiondocument_gid + "'," +
                    "'" + values.sanction_gid + "'," +
                    "'" + values.document_gid + "'," +
                    "'" + values.document_name + "'," +
                    "'" + msGETGIDDoc + "',";
                if (values.reference_query == null)
                {
                    msSQL += "Null,";
                }
                else
                {
                    msSQL += "'" + values.reference_query + "',";
                }

                msSQL += "'" + values.type_of_conversation + "'," +
                    "'Physical Copy'," +
                    "'" + values.cad_query.Replace("'", "") + "'," +
                    "'" + lsuser_name + "'," +
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "'" + query_no + "'," +
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "INSERT INTO ids_trn_tconversationdocument(conversationdocument_gid, docconversation_gid, document_path, document_name,document_title, created_by, created_date)" +
                    " SELECT conversationdocument_gid,'" + msGetGID + "' ,document_path, document_name,document_title, created_by, created_date" +
                    " FROM ids_tmp_tconversationdocument" +
                    " WHERE created_by='" + user_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                msSQL = "DELETE FROM ids_tmp_tconversationdocument WHERE created_by='" + user_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                objODBCDataReader.Close();
                values.status = false;
                values.message = "Query Already Sent";
                return;
            }



            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Conversation Added Successfully";
            }
            else
            {
                values.status = false;
            }



        }

        public void DaGetPhyDocConExternal(docconlist values, string sanctiondocument_gid)
        {
            msSQL = " SELECT docconversation_gid, sanctiondocument_gid, sanction_gid," +
                    " document_gid, document_name, type_of_doc,docconversationref_no," +
                    " cad_query, rm_response, cad_name, cad_gid,date_format(cadquery_on,'%d-%m-%Y %h:%i %p') as cadquery_on," +
                    " relationshipmgr_gid, relationshipmgr_name,date_format(relationshipmgrquery_on,'%d-%m-%Y %h:%i %p') as relationshipmgrquery_on," +
                    " query_no,flag," +
                    " if((SELECT COUNT(*) FROM ids_trn_tconversationdocument x" +
                    " WHERE a.docconversation_gid = x.docconversation_gid GROUP BY a.docconversation_gid) is null,0," +
                    " (SELECT COUNT(*) FROM ids_trn_tconversationdocument x" +
                    " WHERE a.docconversation_gid = x.docconversation_gid GROUP BY a.docconversation_gid)) AS uploaddocument_count," +
                    " date_format(a.forwarded_on,'%d-%m-%Y %h:%i %p') as forwarded_on,forwarded_by_name,forwarded_flag" +
                    " FROM ids_trn_tdocconversation a" +
                    " WHERE sanctiondocument_gid = '" + sanctiondocument_gid + "' AND type_of_conversation='External' AND a.type_of_doc='Physical Copy'" +
                    " ORDER BY query_no ASC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<MdlDocConversation>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getDocList.Add(new MdlDocConversation
                    {
                        docconversation_gid = dt["docconversation_gid"].ToString(),
                        sanctiondocument_gid = dt["sanctiondocument_gid"].ToString(),
                        sanction_gid = dt["sanction_gid"].ToString(),
                        docconversationref_no = dt["docconversationref_no"].ToString(),
                        document_name = dt["document_name"].ToString(),
                        document_gid = dt["document_gid"].ToString(),

                        type_of_doc = dt["type_of_doc"].ToString(),

                        cad_query = dt["cad_query"].ToString(),
                        rm_response = dt["rm_response"].ToString(),

                        cad_name = dt["cad_name"].ToString(),
                        cad_gid = dt["cad_gid"].ToString(),

                        cadquery_on = dt["cadquery_on"].ToString(),
                        relationshipmgr_name = dt["relationshipmgr_name"].ToString(),

                        relationshipmgrquery_on = dt["relationshipmgrquery_on"].ToString(),

                        query_no = dt["query_no"].ToString(),

                        flag = dt["flag"].ToString(),
                        forwarded_flag = dt["forwarded_flag"].ToString(),
                        forwarded_by_name = dt["forwarded_by_name"].ToString(),
                        forwarded_on = dt["forwarded_on"].ToString(),
                        uploaddocument_count = dt["uploaddocument_count"].ToString(),
                    });
                }
                values.MdlDocConversation = getDocList;
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No record to display";
            }
            dt_datatable.Dispose();
        }

        public void DaPostNoQuery(string sanctiondocument_gid, string user_gid, result objResult)
        {

            msSQL = " SELECT phydocument_type FROM ids_trn_tsanctiondocumentdtls" +
          " WHERE sanctiondocument_gid='" + sanctiondocument_gid + "' AND phydocument_type is not null";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == false)
            {
                objODBCDataReader.Close();
                objResult.message = "Kindly Update the Types of Document";
                objResult.status = false;
                return;
            }
            objODBCDataReader.Close();
            msSQL = " SELECT phydocument_date FROM ids_trn_tsanctiondocumentdtls" +
                   " WHERE sanctiondocument_gid='" + sanctiondocument_gid + "' AND phydocument_date is not null";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == false)
            {
                objODBCDataReader.Close();
                objResult.message = "Kindly Update the Document Date";
                objResult.status = false;
                return;
            }
            objODBCDataReader.Close();

            msSQL = " UPDATE ids_trn_tsanctiondocumentdtls SET" +
                    " phyfinal_remarks='Document Verified'," +
                    " phyfinalremarks_by='" + lsuser_name + "'," +
                    " phyfinalremarks_on=current_timestamp," +
                    " phyfinalremarksuser_gid='" + user_gid + "'," +
                    " phydoc_status='Verified'," +
                    " phydocverified_gid='" + user_gid + "'," +
                    " phydocverifier_name=(SELECT concat(user_firstname,' ',user_lastname)as user  FROM adm_mst_tuser WHERE user_gid='" + user_gid + "')," +
                    " Phydocverified_on=current_timestamp" +
                    " WHERE sanctiondocument_gid='" + sanctiondocument_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                objResult.status = true;
                objResult.message = "Document Verified Successfully";
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";

            }

        }

        public void DaPostBulkDocVerification(MdlBulkverification values, string user_gid)
        {
            foreach (string i in values.sanctiondocument_gid)
            {
                msSQL = " select phydoc_status from ids_trn_tsanctiondocumentdtls where sanctiondocument_gid='" + i + "'";
                string lsphydoc_status = objdbconn.GetExecuteScalar(msSQL);
                if (lsphydoc_status == "Verified")
                {

                }
                else
                {
                    DateTime documentdate = DateTime.Parse(Convert.ToDateTime(values.document_date).ToShortDateString());
                    DateTime nowdate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

                    if (documentdate > nowdate)
                    {
                        values.status = false;
                        values.message = "Future Date is Not Allowed...";
                        return;
                    }

                    var lsuser_name = objdbconn.GetExecuteScalar("SELECT CONCAT(user_firstname,' ',user_lastname) as user_name FROM adm_mst_tuser WHERE user_gid='" + user_gid + "'");

                    msSQL = " UPDATE ids_trn_tsanctiondocumentdtls SET" +
                            " phydocument_date='" + Convert.ToDateTime(values.document_date).ToString("yyyy-MM-dd 00:00:00") + "'," +
                            " phydocument_type='" + values.type_copy + "'," +
                            " phyfinal_remarks='" + values.phyfinal_remarks + "'," +
                            " phyfinalremarks_by='" + lsuser_name + "'," +
                            " phyfinalremarks_on=current_timestamp," +
                            " phyfinalremarksuser_gid='" + user_gid + "'," +
                            " phydoc_status='Verified'," +
                            " phydocverified_gid='" + user_gid + "'," +
                            " phydocverifier_name=(SELECT concat(user_firstname,' ',user_lastname)as user  FROM adm_mst_tuser WHERE user_gid='" + user_gid + "')," +
                            " Phydocverified_on=current_timestamp" +
                            " WHERE sanctiondocument_gid='" + i + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            if (mnResult == 1)
            {
                values.status = true;
                values.message = " Documents Verified Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        public void DaFutureDateCheck(string date, result values)
        {
            DateTime documentdate = DateTime.Parse(Convert.ToDateTime(date).ToShortDateString());
            DateTime nowdate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

            if (documentdate > nowdate)
            {
                values.status = false;
                values.message = "Future Date is Not Allowed...";
            }
            else
            {
                values.status = true;
            }
        }

        public void DaPostLSAStatus(MdlDocConversation values)
        {
            msSQL = " UPDATE ocs_mst_tcustomer2sanction SET" +
                    " lsa_status='" + values.lsa_status + "'" +
                    " WHERE customer2sanction_gid='" + values.sanction_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "LSA Status Updated Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";

            }
        }
    }
}