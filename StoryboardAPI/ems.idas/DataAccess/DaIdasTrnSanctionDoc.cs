using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using ems.idas.Models;
using System.Configuration;
using System.Drawing;
using ems.storage.Functions;


namespace ems.idas.DataAccess
{
    public class DaIdasTrnSanctionDoc
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        string msSQL;
        OdbcDataReader objODBCDataReader;
        int mnResult;
        string msGetGID, msGETGIDDoc;
        string lsdocument_name = string.Empty;
        string lsdocument_code = string.Empty;
        string lsuser_name = string.Empty;
        string lspath = string.Empty;
        string lscompany_code = string.Empty;
        HttpPostedFile httpPostedFile;

        // Sanction Details

        public void DaGetSanctionDtlsView(string lssanction_gid, MdlSanctionDtlsView values)
        {
            msSQL = " SELECT a.sanction_refno,date_format(a.sanction_date,'%d-%m-%Y') as sanction_date," +
                    " a.sanction_amount,a.facility_type,b.customername,b.customer_urn, a.lsa_status, a.batch_status, a.status_ofBAL," +
                    " a.collateral_security,b.businesshead_name,b.relationshipmgmt_name,b.zonal_name,b.creditmgmt_name," +
                    " b.cluster_manager_name,b.customer_code,b.vertical_code,b.contactperson,b.mobileno,b.address,b.address2,b.customer_gid," +
                    " group_concat(distinct c.maker_status) as maker_status, group_concat(distinct c.checker_status) as checker_status" +
                    " FROM ocs_mst_tcustomer2sanction a" +
                    " INNER JOIN ocs_mst_tcustomer b on a.customer_gid = b.customer_gid" +
                    " INNER JOIN ids_trn_tsanctiondocumentdtls c ON a.customer2sanction_gid=c.sanction_gid " +
                    " WHERE a.customer2sanction_gid = '" + lssanction_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows)
            {
                objODBCDataReader.Read();
                values.sanctionrefno = objODBCDataReader["sanction_refno"].ToString();
                values.SanctionDate = objODBCDataReader["sanction_date"].ToString();
                values.SanctionAmount = objODBCDataReader["sanction_amount"].ToString();
                values.FacilityType = objODBCDataReader["facility_type"].ToString();
                values.customerName = objODBCDataReader["customername"].ToString();
                values.Customerurn = objODBCDataReader["customer_urn"].ToString();
                values.collateral_security = objODBCDataReader["collateral_security"].ToString();
                values.businessHeadName = objODBCDataReader["businesshead_name"].ToString();
                values.relationshipmgmt = objODBCDataReader["relationshipmgmt_name"].ToString();
                values.zonalHeadName = objODBCDataReader["zonal_name"].ToString();
                values.creditManager = objODBCDataReader["creditmgmt_name"].ToString();
                values.clusterManager = objODBCDataReader["cluster_manager_name"].ToString();
                values.customercode = objODBCDataReader["customer_code"].ToString();
                values.verticalCode = objODBCDataReader["vertical_code"].ToString();
                values.contactperson = objODBCDataReader["contactperson"].ToString();
                values.mobileno = objODBCDataReader["mobileno"].ToString();
                values.addressline1 = objODBCDataReader["address"].ToString();
                values.addressline2 = objODBCDataReader["address2"].ToString();
                values.customer_gid = objODBCDataReader["customer_gid"].ToString();
                values.batch_status = objODBCDataReader["batch_status"].ToString();
                values.status_ofBAL = objODBCDataReader["status_ofBAL"].ToString();
                values.lsa_status = objODBCDataReader["lsa_status"].ToString();
                values.maker_status = objODBCDataReader["maker_status"].ToString();
                values.checker_status = objODBCDataReader["checker_status"].ToString();
            }
            objODBCDataReader.Close();
        }

        // sanction document - insert
        public void DaPostSanctionDoc(MdlsanctionDocDtls values, string user_gid)
        {
            foreach (string i in values.documentlist_gid)
            {
                msSQL = "SELECT sanctiondocument_gid FROM ids_trn_tsanctiondocumentdtls WHERE document_gid='" + i + "' AND sanction_gid='" + values.sanction_gid + "'";

                msGetGID = objcmnfunctions.GetMasterGID("SADC");
                msGETGIDDoc = objcmnfunctions.GetMasterGID("IDAS");


                lsdocument_code = objdbconn.GetExecuteScalar("select document_code from ids_mst_tdocumentlist where documentlist_gid='" + i + "'");
                lsdocument_name = objdbconn.GetExecuteScalar("select document_name from ids_mst_tdocumentlist where documentlist_gid='" + i + "'");

                msSQL = " INSERT INTO ids_trn_tsanctiondocumentdtls(" +
                        " sanctiondocument_gid," +
                        " sanction_gid," +
                        " document_gid," +
                        " document_code," +
                        " document_name," +
                        " documentrecord_id," +
                        " created_date," +
                        " created_by)" +
                        " VALUES(" +
                        "'" + msGetGID + "'," +
                        "'" + values.sanction_gid + "'," +
                        "'" + i + "'," +
                        "'" + lsdocument_code + "'," +
                        "'" + lsdocument_name.Replace("'", "") + "'," +
                        "'" + msGETGIDDoc + "'," +
                        "current_timestamp," +
                        "'" + user_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " INSERT INTO rsk_mst_tsanctiondocumentdtl" +
                         " (" +
                         " rsksanction_documentgid," +
                         " customer2sanction_gid," +
                         " customer_gid," +
                         " document_gid," +
                         " document_code," +
                         " document_name," +
                         " created_by," +
                         " created_date," +
                         " document_from" +
                         " )" +
                         " values(" +
                         "'" + msGetGID + "'," +
                         "'" + values.sanction_gid + "'," +
                         "'" + values.customer_gid + "'," +
                         "'" + i + "'," +
                         "'" + lsdocument_code + "'," +
                         "'" + lsdocument_name.Replace("'", "") + "'," +
                         "'" + user_gid + "'," +
                         "current_timestamp," +
                         "'IDAS')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }

            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Sanction Document Tagged Successfully...";

            }
            else
            {
                values.status = true;
                values.message = "Error Occured";

            }
        }

        // sanction document - delete
        public void DaPostSanctionDocDelete(string sanctiondocument_gid, result objResult)
        {
            msSQL = " SELECT phydoc_status FROM ids_trn_tsanctiondocumentdtls WHERE sanctiondocument_gid='" + sanctiondocument_gid + "'";
            var lsstatus = objdbconn.GetExecuteScalar(msSQL);

            if (lsstatus != "Pending")
            {
                objResult.status = false;
                objResult.message = "You cannot delete the document, Physical Document Verified";
                return;
            }
            else
            {

                msSQL = " INSERT INTO ids_trn_tsanctiondocumentdtlshistory" +
                        " SELECT * FROM ids_trn_tsanctiondocumentdtls WHERE sanctiondocument_gid='" + sanctiondocument_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "DELETE FROM ids_trn_tsanctiondocumentdtls WHERE sanctiondocument_gid='" + sanctiondocument_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "DELETE FROM rsk_mst_tsanctiondocumentdtl WHERE rsksanction_documentgid='" + sanctiondocument_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " INSERT INTO ids_trn_tdocconversationhistory" +
                        " SELECT * FROM ids_trn_tdocconversation WHERE sanctiondocument_gid='" + sanctiondocument_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "DELETE FROM ids_trn_tdocconversation WHERE sanctiondocument_gid='" + sanctiondocument_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    objResult.status = true;
                    objResult.message = "Sanction Document Deleted Successfully..!";

                }
                else
                {
                    objResult.status = true;
                    objResult.message = "Error Occured";

                }

            }


        }

        // sanction un tagged doccument list
        public bool DaGetTaggedDocList(MdlTaggedDocumentList objResult, string sanction_gid)
        {

            msSQL = " SELECT a.sanctiondocument_gid,a.document_gid,a.document_code,a.document_name,a.documentrecord_id," +
                    " b.comments," +
                    " (SELECT COUNT(*) FROM ids_trn_tsanctiondocumentdtls x" +
                    " WHERE x.document_gid = a.document_gid AND x.sanction_gid = a.sanction_gid GROUP BY x.document_gid) as document_count" +
                    " FROM ids_trn_tsanctiondocumentdtls a" +
                    " LEFT JOIN ids_mst_tdocumentlist b ON a.document_gid=b.documentlist_gid" +
                    " WHERE a.sanction_gid = '" + sanction_gid + "'" +
                    " ORDER BY b.display_order ASC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                objResult.MdlTaggedDocument = dt_datatable.AsEnumerable().Select(row => new MdlTaggedDocument
                {
                    sanctiondocument_gid = row["sanctiondocument_gid"].ToString(),
                    document_gid = row["document_gid"].ToString(),
                    document_code = row["document_code"].ToString(),
                    document_name = row["document_name"].ToString(),
                    documentrecord_id = row["documentrecord_id"].ToString(),
                    document_count = row["document_count"].ToString(),
                    comments = row["comments"].ToString(),
                }).ToList();
                objResult.status = true;
                objResult.message = "Success";
            }
            else
            {
                objResult.status = false;
                objResult.message = "No Record Found";
            }
            return true;
        }

        // sanction summary
        public void DaGetSanctionSummary(MdlSanctionDocSummaryList values)
        {
            msSQL = " SELECT a.customer2sanction_gid, b.customer_urn,b.customername,a.sanction_refno," +
                    " DATE_FORMAT(a.sanction_date, '%d-%m-%Y') as sanction_date," +
                    " FORMAT(a.sanction_amount, 2) as sanction_amount," +
                    " (SELECT COUNT(*) FROM ids_trn_tsanctiondocumentdtls b" +
                    " WHERE a.customer2sanction_gid = b.sanction_gid GROUP BY a.customer2sanction_gid) AS tagged_document," +
                    " a.batch_status," +
                    " IF(a.fileref_no is null, '-', a.fileref_no) as fileref_no" +
                    " FROM ocs_mst_tcustomer2sanction a" +
                    " INNER JOIN ocs_mst_tcustomer b ON a.customer_gid=b.customer_gid" +
                    " WHERE 1=1" +
                    " ORDER BY a.created_date DESC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<MdlSanctionDocSummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getDocList.Add(new MdlSanctionDocSummary
                    {
                        sanction_gid = dt["customer2sanction_gid"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        customer_name = dt["customername"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),

                        sanction_date = dt["sanction_date"].ToString(),

                        tagged_document = dt["tagged_document"].ToString(),

                        batch_status = dt["batch_status"].ToString(),
                        fileref_no = dt["fileref_no"].ToString(),


                    });
                }

                values.totalsummary_count = objdbconn.GetExecuteScalar("SELECT count(*) FROM ocs_mst_tcustomer2sanction WHERE 1=1");

                values.MdlSanctionDocSummary = getDocList;
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


        // maker checker summary
        public void DaGetMakerCheckerSummary(MdlSummaryList values)
        {
            msSQL = " SELECT /*+ MAX_EXECUTION_TIME(300000) */ DISTINCT a.customer2sanction_gid, b.customer_urn,b.customername,a.sanction_refno," +
                    " DATE_FORMAT(a.sanction_date, '%d-%m-%Y') as sanction_date," +
                    " FORMAT(a.sanction_amount, 2) as sanction_amount," +
                    " (SELECT COUNT(*) FROM ids_trn_tsanctiondocumentdtls b" +
                    " WHERE a.customer2sanction_gid = b.sanction_gid GROUP BY a.customer2sanction_gid) AS tagged_document," +
                    " (SELECT COUNT(*) FROM ids_trn_tsanctiondocumentdtls b" +
                    "  WHERE a.customer2sanction_gid = b.sanction_gid AND B.maker_status = 'Maker Confirmed'" +
                    " GROUP BY a.customer2sanction_gid) AS makerconfirmed_count," +
                    " (SELECT COUNT(*) FROM ids_trn_tsanctiondocumentdtls b" +
                    " WHERE a.customer2sanction_gid = b.sanction_gid AND b.checker_status = 'Checker Confirmed'" +
                    " GROUP BY a.customer2sanction_gid) AS checkerconfirmed_count," +
                    " a.batch_status," +
                    " IF(a.fileref_no is null, '-', a.fileref_no) as fileref_no" +
                    " FROM ocs_mst_tcustomer2sanction a" +
                    " INNER JOIN ocs_mst_tcustomer b on a.customer_gid=b.customer_gid" +
                    " INNER JOIN ids_trn_tsanctiondocumentdtls c ON a.customer2sanction_gid=c.sanction_gid" +
                    " WHERE a.customer2sanction_gid in (SELECT DISTINCT sanction_gid FROM ids_trn_tsanctiondocumentdtls) " +
                    " ORDER BY c.created_date DESC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<MdlMakercheckerSummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getDocList.Add(new MdlMakercheckerSummary
                    {
                        sanction_gid = dt["customer2sanction_gid"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        customer_name = dt["customername"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),

                        sanction_date = dt["sanction_date"].ToString(),

                        tagged_document = dt["tagged_document"].ToString(),
                        makerconfirmed_count = dt["makerconfirmed_count"].ToString(),
                        checkerconfirmed_count = dt["checkerconfirmed_count"].ToString(),
                        batch_status = dt["batch_status"].ToString(),
                        fileref_no = dt["fileref_no"].ToString(),


                    });
                }
                values.MdlMakercheckerSummary = getDocList;
            }
            dt_datatable.Dispose();
        }

        // rm summary
        public void DaGetRmSummary(MdlSummaryList values, string employee_gid)
        {
            msSQL = " SELECT /*+ MAX_EXECUTION_TIME(300000) */ DISTINCT a.customer2sanction_gid, b.customer_urn,b.customername,a.sanction_refno," +
                  " DATE_FORMAT(a.sanction_date, '%d-%m-%Y') as sanction_date," +
                  " FORMAT(a.sanction_amount, 2) as sanction_amount," +
                  " (SELECT COUNT(*) FROM ids_trn_tsanctiondocumentdtls b" +
                  " WHERE a.customer2sanction_gid = b.sanction_gid GROUP BY a.customer2sanction_gid) AS tagged_document," +
                  " a.batch_status," +
                  " IF(a.fileref_no is null, '-', a.fileref_no) as fileref_no" +
                  " FROM ocs_mst_tcustomer2sanction a" +
                  " INNER JOIN ocs_mst_tcustomer b on a.customer_gid=b.customer_gid" +
                  " INNER JOIN ids_trn_tsanctiondocumentdtls c ON a.customer2sanction_gid=c.sanction_gid" +
                  " WHERE a.customer2sanction_gid in (SELECT DISTINCT sanction_gid FROM ids_trn_tsanctiondocumentdtls) " +
                  " AND b.relationship_manager='" + employee_gid + "'" +
                  " ORDER BY c.created_date DESC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<MdlMakercheckerSummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getDocList.Add(new MdlMakercheckerSummary
                    {
                        sanction_gid = dt["customer2sanction_gid"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        customer_name = dt["customername"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),

                        sanction_date = dt["sanction_date"].ToString(),

                        tagged_document = dt["tagged_document"].ToString(),

                        batch_status = dt["batch_status"].ToString(),
                        fileref_no = dt["fileref_no"].ToString(),


                    });
                }
                values.MdlMakercheckerSummary = getDocList;


                dt_datatable.Dispose();
            }
        }
        // document confirm

        public void DaPostDocumentConfirmation(string user_gid, string sanctiondocument_gid, string confirmation_type, MdlDocConversation values)
        {
            msSQL = " SELECT finalremarks FROM ids_trn_tsanctiondocumentdtls " +
                 " WHERE sanctiondocument_gid='" + sanctiondocument_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                if (objODBCDataReader["finalremarks"].ToString() == null || objODBCDataReader["finalremarks"].ToString() == "")
                {
                    values.status = false;
                    values.message = "Final Remarks Need to Document Confirmation";
                    return;
                }
            }
            objODBCDataReader.Close();

            lsuser_name = objdbconn.GetExecuteScalar("SELECT concat(user_firstname,' ',user_lastname)as user  FROM adm_mst_tuser WHERE user_gid='" + user_gid + "'");
            if (confirmation_type == "Maker")
            {
                msSQL = " select relationshipmgr_name,relationshipmgr_gid, relationshipmgrquery_on from ids_trn_tdocconversation " +
                        " where sanctiondocument_gid='" + sanctiondocument_gid + "' and type_of_conversation='Internal' and type_of_doc='Scan Copy' group by sanctiondocument_gid";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if(objODBCDataReader.HasRows == true)
                {
                    msSQL = " select relationshipmgr_name from ids_trn_tdocconversation where sanctiondocument_gid='" + sanctiondocument_gid + "' "+
                            " and type_of_conversation='Internal' and type_of_doc='Scan Copy' group by sanctiondocument_gid";
                    string lsrelationshipmgr_name = objdbconn.GetExecuteScalar(msSQL);
                    if (lsrelationshipmgr_name == "" || lsrelationshipmgr_name == null)
                    {
                        values.status = false;
                        values.message = "Kindly Update Checker Response";
                        return;
                    }

                    msSQL = " UPDATE ids_trn_tsanctiondocumentdtls SET" +
                  " maker_status='Maker Confirmed'," +
                  " maker_gid='" + user_gid + "'," +
                  " maker_name='" + lsuser_name + "'," +
                  " maked_on=current_timestamp," +
                  " checker_status='Checker Confirmed'," +
                  " checker_gid='" + objODBCDataReader["relationshipmgr_gid"].ToString() +"'," +
                  " checker_name='" + objODBCDataReader["relationshipmgr_name"].ToString() + "'," +
                  " checked_on='" + Convert.ToDateTime(objODBCDataReader["relationshipmgrquery_on"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                  " WHERE sanctiondocument_gid='" + sanctiondocument_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    values.status = false;
                    values.message = "Kindly Update Maker Response";
                    return;
                }
                objODBCDataReader.Close();
            }
            else
            {
                msSQL = " select cad_name,cad_gid, cadquery_on from ids_trn_tdocconversation "+
                        " where sanctiondocument_gid='" + sanctiondocument_gid + "' and type_of_conversation='Internal' and type_of_doc='Scan Copy' group by sanctiondocument_gid";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    msSQL = " UPDATE ids_trn_tsanctiondocumentdtls SET" +
                  " maker_status='Maker Confirmed'," +
                  " maker_gid='" + objODBCDataReader["cad_gid"].ToString() + "'," +
                  " maker_name='" + objODBCDataReader["cad_name"].ToString() + "'," +
                  " maked_on='" + Convert.ToDateTime(objODBCDataReader["cadquery_on"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                  " checker_status='Checker Confirmed'," +
                  " checker_gid='" + user_gid + "'," +
                  " checker_name='" + lsuser_name + "'," +
                  " checked_on=current_timestamp" +
                  " WHERE sanctiondocument_gid='" + sanctiondocument_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    msSQL = " UPDATE ids_trn_tsanctiondocumentdtls SET" +
                 " maker_status='Maker Confirmed'," +
                 " maker_gid='" + user_gid + "'," +
                 " maker_name='" + lsuser_name + "'," +
                 " maked_on=current_timestamp," +
                 " checker_status='Checker Confirmed'," +
                 " checker_gid='" + user_gid + "'," +
                 " checker_name='" + lsuser_name + "'," +
                 " checked_on=current_timestamp" +
                 " WHERE sanctiondocument_gid='" + sanctiondocument_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                objODBCDataReader.Close();
            }
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Document Confirmed Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }

        }
        //maker document verify
        public void DaPostMakerDocVerify(string user_gid, string sanctiondocument_gid, result objResult)
        {
            msSQL = " SELECT scanfinal_remarks FROM ids_trn_tsanctiondocumentdtls " +
                   " WHERE sanctiondocument_gid='" + sanctiondocument_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                if (objODBCDataReader["scanfinal_remarks"].ToString() == null || objODBCDataReader["scanfinal_remarks"].ToString() == "")
                {
                    objResult.status = false;
                    objResult.message = "Final Remarks Need to Document Confirmation";
                    return;
                }
            }
            objODBCDataReader.Close();

            lsuser_name = objdbconn.GetExecuteScalar("SELECT concat(user_firstname,' ',user_lastname)as user  FROM adm_mst_tuser WHERE user_gid='" + user_gid + "'");

            msSQL = " UPDATE ids_trn_tsanctiondocumentdtls SET" +
                    " maker_status='Maker Confirmed'," +
                    " maker_gid='" + user_gid + "'," +
                    " maker_name='" + lsuser_name + "'," +
                    " maked_on=current_timestamp" +
                    " WHERE sanctiondocument_gid='" + sanctiondocument_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                objResult.status = true;
                objResult.message = "Document Confirmed Successfully";
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";
            }
        }

        //checker document verify
        public void DaPostCheckerDocVerify(string user_gid, string sanctiondocument_gid, result objResult)
        {
            msSQL = " SELECT scanfinal_remarks FROM ids_trn_tsanctiondocumentdtls " +
                   " WHERE sanctiondocument_gid='" + sanctiondocument_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                if (objODBCDataReader["scanfinal_remarks"].ToString() == null || objODBCDataReader["scanfinal_remarks"].ToString() == "")
                {
                    objResult.status = false;
                    objResult.message = "Final Remarks Need to Document Confirm";
                    return;
                }
            }
            objODBCDataReader.Close();

            lsuser_name = objdbconn.GetExecuteScalar("SELECT concat(user_firstname,' ',user_lastname)as user  FROM adm_mst_tuser WHERE user_gid='" + user_gid + "'");

            msSQL = " UPDATE ids_trn_tsanctiondocumentdtls SET" +
                    " checker_status='Checker Confirmed'," +
                    " checker_gid='" + user_gid + "'," +
                    " checker_name='" + lsuser_name + "'," +
                    " checked_on=current_timestamp" +
                    " WHERE sanctiondocument_gid='" + sanctiondocument_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                objResult.status = true;
                objResult.message = "Document Confirmed Sucessfully";
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";
            }
        }

        // scan document summary
        public void DaGetScanDocSummary(string sanction_gid, MdlScannDocList values, string user_gid)
        {
            msSQL = " SELECT sanctiondocument_gid,a.document_gid,documentrecord_id,a.document_code,a.document_name,if(b.comments='','No Comments',b.comments) as comments," +
                    " if(maker_name is null,'-',maker_name) as  maker_name," +
                    " if(maked_on is null,'-', date_format(maked_on,'%d-%m-%Y %h:%i %p')) as maked_on," +
                    " maker_status," +
                    " if(checker_name is null,'-',checker_name) as checker_name," +
                    " checker_status," +
                    " if(checked_on is null,'-', date_format(checked_on,'%d-%m-%Y %h:%i %p')) as checked_on,maker_gid,checker_gid," +
                    " phydoc_status,if(phydocverifier_name is null,'-',phydocverifier_name) as phydocverifier_name," +
                    " date_format(Phydocverified_on,'%d-%m-%Y %h:%i %p') as Phydocverified_on," +
                    " if((SELECT COUNT(*) FROM ids_trn_tdocconversation x WHERE a.document_gid=x.document_gid AND x.sanctiondocument_gid=a.sanctiondocument_gid AND x.sanction_gid=a.sanction_gid AND x.type_of_doc='Scan Copy' GROUP BY a.document_gid) is null,0," +
                    " (SELECT COUNT(*) FROM ids_trn_tdocconversation x WHERE a.document_gid = x.document_gid AND x.sanctiondocument_gid=a.sanctiondocument_gid AND x.sanction_gid=a.sanction_gid AND x.type_of_doc='Scan Copy' GROUP BY a.document_gid)) AS scanconversation_count," +
                     " if((SELECT COUNT(*) FROM ids_trn_tdocconversation x WHERE a.document_gid=x.document_gid AND x.sanctiondocument_gid=a.sanctiondocument_gid AND x.sanction_gid=a.sanction_gid AND x.type_of_doc='Scan Copy' AND x.type_of_conversation='External' GROUP BY a.document_gid) is null,0," +
                    " (SELECT COUNT(*) FROM ids_trn_tdocconversation x WHERE a.document_gid = x.document_gid AND x.sanctiondocument_gid=a.sanctiondocument_gid AND x.sanction_gid=a.sanction_gid AND x.type_of_doc='Scan Copy' AND x.type_of_conversation='External' GROUP BY a.document_gid)) AS scanrmconversation_count," +
                    " if((SELECT COUNT(*) FROM ids_trn_tdocconversation x WHERE a.document_gid=x.document_gid AND x.sanctiondocument_gid=a.sanctiondocument_gid AND x.sanction_gid=a.sanction_gid AND x.type_of_doc='Physical Copy' GROUP BY a.document_gid) is null,0," +
                    " (SELECT COUNT(*) FROM ids_trn_tdocconversation x WHERE a.document_gid = x.document_gid AND x.sanctiondocument_gid=a.sanctiondocument_gid AND x.sanction_gid=a.sanction_gid AND x.type_of_doc='Physical Copy' GROUP BY a.document_gid)) AS phyconversation_count," +
                     " if((SELECT COUNT(*) FROM ids_trn_tdocconversation x WHERE a.document_gid=x.document_gid AND x.sanctiondocument_gid=a.sanctiondocument_gid AND x.sanction_gid=a.sanction_gid GROUP BY a.document_gid) is null,0," +
                    " (SELECT COUNT(*) FROM ids_trn_tdocconversation x WHERE a.document_gid = x.document_gid AND x.sanctiondocument_gid=a.sanctiondocument_gid AND x.sanction_gid=a.sanction_gid AND x.type_of_conversation='External' GROUP BY a.document_gid)) AS externalconversation_count," +
                    "  if((SELECT COUNT(*) FROM ids_trn_tdocconversation x WHERE a.document_gid=x.document_gid AND x.sanctiondocument_gid=a.sanctiondocument_gid AND x.sanction_gid=a.sanction_gid AND x.view_status='CQ' AND x.type_of_conversation='External' GROUP BY a.document_gid) is null,0," +
                    " (SELECT COUNT(*) FROM ids_trn_tdocconversation x WHERE a.document_gid = x.document_gid AND x.sanctiondocument_gid=a.sanctiondocument_gid AND x.sanction_gid=a.sanction_gid AND x.view_status = 'CQ' AND x.type_of_conversation='External' GROUP BY a.document_gid)) AS externalquery_count," +
                    " if ((SELECT COUNT(*) FROM ids_trn_tdocconversation x WHERE a.document_gid = x.document_gid AND x.sanctiondocument_gid=a.sanctiondocument_gid AND x.sanction_gid=a.sanction_gid AND x.view_status = 'Res'  AND x.type_of_conversation='External' AND relationshipmgr_gid<>'" + user_gid + "' GROUP BY a.document_gid) is null,0," +
                    " (SELECT COUNT(*) FROM ids_trn_tdocconversation x WHERE a.document_gid = x.document_gid AND x.sanctiondocument_gid=a.sanctiondocument_gid AND x.sanction_gid=a.sanction_gid AND x.view_status = 'Res'  AND x.type_of_conversation='External' AND relationshipmgr_gid<>'" + user_gid + "' GROUP BY a.document_gid)) AS externalresponse_count," +
                    "  if((SELECT COUNT(*) FROM ids_trn_tdocconversation x WHERE a.document_gid=x.document_gid AND x.sanctiondocument_gid=a.sanctiondocument_gid AND x.sanction_gid=a.sanction_gid AND x.view_status='CQ' AND x.type_of_conversation='Internal' GROUP BY a.document_gid) is null,0," +
                    " (SELECT COUNT(*) FROM ids_trn_tdocconversation x WHERE a.document_gid = x.document_gid AND x.sanctiondocument_gid=a.sanctiondocument_gid AND x.sanction_gid=a.sanction_gid AND x.view_status = 'CQ' AND x.type_of_conversation='Internal' GROUP BY a.document_gid)) AS internalquery_count," +
                    " if ((SELECT COUNT(*) FROM ids_trn_tdocconversation x WHERE a.document_gid = x.document_gid AND x.sanctiondocument_gid=a.sanctiondocument_gid AND x.sanction_gid=a.sanction_gid AND x.view_status = 'Res'  AND x.type_of_conversation='Internal' AND relationshipmgr_gid<>'" + user_gid + "' GROUP BY a.document_gid) is null,0," +
                    " (SELECT COUNT(*) FROM ids_trn_tdocconversation x WHERE a.document_gid = x.document_gid AND x.sanctiondocument_gid=a.sanctiondocument_gid AND x.sanction_gid=a.sanction_gid AND x.view_status = 'Res'  AND x.type_of_conversation='Internal' AND relationshipmgr_gid<>'" + user_gid + "' GROUP BY a.document_gid)) AS internalresponse_count" +
                    " FROM  ids_trn_tsanctiondocumentdtls a" +
                    " LEFT JOIN ids_mst_tdocumentlist b on a.document_gid=b.documentlist_gid" +
                    " WHERE sanction_gid = '" + sanction_gid + "' ORDER BY b.display_order ASC";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<MdlScannDocSummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getDocList.Add(new MdlScannDocSummary
                    {
                        sanctiondocument_gid = dt["sanctiondocument_gid"].ToString(),
                        documentrecord_id = dt["documentrecord_id"].ToString(),
                        document_code = dt["document_code"].ToString(),

                        document_name = dt["document_name"].ToString(),
                        maked_on = dt["maked_on"].ToString(),
                        maker_gid = dt["maker_gid"].ToString(),

                        maker_name = dt["maker_name"].ToString(),
                        maker_status = dt["maker_status"].ToString(),

                        checked_on = dt["checked_on"].ToString(),
                        checker_status = dt["checker_status"].ToString(),

                        checker_gid = dt["checker_gid"].ToString(),
                        checker_name = dt["checker_name"].ToString(),

                        documentlist_gid = dt["document_gid"].ToString(),
                        scanconversation_count = dt["scanconversation_count"].ToString(),
                        phyconversation_count = dt["phyconversation_count"].ToString(),
                        Phydocverified_on = dt["Phydocverified_on"].ToString(),
                        phydocverifier_name = dt["phydocverifier_name"].ToString(),
                        phydoc_status = dt["phydoc_status"].ToString(),
                        externalquery_count = dt["externalquery_count"].ToString(),
                        externalresponse_count = dt["externalresponse_count"].ToString(),
                        externalconversation_count = dt["externalconversation_count"].ToString(),
                        internalresponse_count = dt["internalresponse_count"].ToString(),
                        internalquery_count = dt["internalquery_count"].ToString(),
                        scanrmconversation_count = dt["scanrmconversation_count"].ToString(),
                        comments = dt["comments"].ToString(),

                    });
                }
                values.MdlScannDocSummary = getDocList;
            }
            dt_datatable.Dispose();
        }

        // attachement load when raise query
        public void DaPostConverseUpload(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid, string user_gid)
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
            //path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "IDAS/ConversationDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocuments" + "/" + lscompany_code + "/" + "IDAS/ConversationDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;


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
                            objfilename.status = false;
                            objfilename.message = "File format is not supported";
                            return;
                        }
                        //lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/ConversationDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");

                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);

                        //lspath = "../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/ConversationDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "IDAS/ConversationDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "IDAS/ConversationDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        msGetGID = objcmnfunctions.GetMasterGID("IDCD");
                        msSQL = " insert into ids_tmp_tconversationdocument( " +
                                    " conversationdocument_gid ," +
                                    " document_name ," +
                                    " document_path," +
                                    " document_title," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGID + "'," +
                                    "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + lsdocument_title.Replace("'", "") + "'," +
                                    "'" + user_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            objfilename.status = true;
                            objfilename.message = "Document Uploaded Successfully";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured";
                        }
                    }
                }
            }
            catch
            {
            }
        }


        public void DaPostCommonDocumentUpload(HttpRequest httpRequest, result objResult, string employee_gid, string user_gid)
        {

            HttpFileCollection httpFileCollection;
            MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string lscompany_code = string.Empty;
            Stream ls_readStream;
            String path;
            string sanction_gid = httpRequest.Form["sanction_gid"];
            string lsdocument_title = httpRequest.Form["document_title"];
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            //path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "IDAS/SanctionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocuments" + "/" + lscompany_code + "/" + "IDAS/SanctionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;


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
                        //lspath = HttpContext.Current.Server.MapPath("../../../erp_documents/" + lscompany_code + "/" + "IDAS/SanctionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");

                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);

                        //lspath = "../../../erp_documents/" + lscompany_code + "/" + "IDAS/SanctionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "IDAS/SanctionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "IDAS/SanctionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        msSQL = " insert into ids_trn_tcommonuploaddocument( " +
                                     " sanction_gid ," +
                                     " document_name ," +
                                     " document_path," +
                                     " document_title, " +
                                     " created_by," +
                                     " created_date" +
                                     " )values(" +
                                     "'" + sanction_gid + "'," +
                                     "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                     "'" + lspath + msdocument_gid + FileExtension + "'," +
                                     "'" + lsdocument_title.Replace("'", "") + "'," +
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
            catch (Exception ex)
            {
            }
        }

        public void DaPostDeleteCommonDocument(string commondocument_gid, result objResult)
        {
            msSQL = " DELETE FROM ids_trn_tcommonuploaddocument" +
                    " WHERE uploaddocument_gid='" + commondocument_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                objResult.status = true;
                objResult.message = "Upload Document Deleted Successfully";
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";
            }
        }

        public void DaGetCommonUploadedDocument(string sanction_gid, uploaddocumentlist objResult)
        {
            msSQL = " SELECT a.uploaddocument_gid, a.document_name,a.document_path,a.document_title,date_format(a.created_date,'%d-%m-%Y') as created_date," +
                    " CONCAT(b.user_code,' / ',b.user_firstname,b.user_lastname) as user_name" +
                    " FROM ids_trn_tcommonuploaddocument a" +
                    " LEFT JOIN adm_mst_tuser b ON a.created_by=b.user_gid" +
                    " WHERE sanction_gid='" + sanction_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<uploaddocument>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getDocList.Add(new uploaddocument
                    {

                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        document_title = dt["document_title"].ToString(),
                        created_by = dt["user_name"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        uploaddocument_gid = dt["uploaddocument_gid"].ToString(),

                    });
                }
                objResult.uploaddocument = getDocList;
            }
            dt_datatable.Dispose();

        }

        //get document from temporary
        public void DaGetconversedoc(uploaddocumentlist objfilename, string user_gid)
        {

            msSQL = " SELECT conversationdocument_gid, document_name,document_path, document_title FROM ids_tmp_tconversationdocument WHERE created_by='" + user_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<uploaddocument>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getDocList.Add(new uploaddocument
                    {
                        conversationdocument_gid = dt["conversationdocument_gid"].ToString(),
                        document_name = dt["document_name"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),

                    });
                }
                objfilename.uploaddocument = getDocList;
            }
            dt_datatable.Dispose();
        }

        // delete the upload document
        public void DaDeleteConverseDoc(string conversationdocument_gid, string user_gid, result objResult)
        {
            if (conversationdocument_gid == "undefine")
            {
                msSQL = " DELETE FROM ids_tmp_tconversationdocument WHERE created_by='" + user_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = " DELETE FROM ids_tmp_tconversationdocument WHERE conversationdocument_gid='" + conversationdocument_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }


            if (mnResult != 0)
            {
                objResult.status = true;
                objResult.message = "Conversation Deleted Successfully";
            }
            else
            {
                objResult.status = false;
            }
        }
        // No Query

        //    public void DaPostScannDocNoQuery(MdlNoQuery values)
        //{
        //    msSQL = " SELECT types_of_copy FROM ids_trn_tsanctiondocumentdtls" +
        //       " WHERE sanctiondocument_gid='" + values.sanctiondocument_gid + "' AND types_of_copy is not null";
        //    objODBCDataReader = objdbconn.GetDataReader(msSQL);
        //    if (objODBCDataReader.HasRows == false)
        //    {
        //        values.message = "Kindly Update the Types of Document Copy";
        //        values.status = false;
        //        return;
        //    }

        //    msSQL = " SELECT scandocument_date FROM ids_trn_tsanctiondocumentdtls" +
        //           " WHERE sanctiondocument_gid='" + values.sanctiondocument_gid + "' AND scandocument_date is not null";
        //    objODBCDataReader = objdbconn.GetDataReader(msSQL);
        //    if (objODBCDataReader.HasRows == false)
        //    {
        //        values.message = "Kindly Update the Document Date";
        //        values.status = false;
        //        return;
        //    }


        //    int query_no = 0;
        //    lsuser_name = objdbconn.GetExecuteScalar("SELECT concat(user_firstname,' ',user_lastname)as user  FROM adm_mst_tuser WHERE user_gid='" + user_gid + "'");

        //    var lsquery_no = objdbconn.GetExecuteScalar("SELECT COUNT(*) FROM ids_trn_tdocconversation WHERE sanctiondocument_gid='" + values.sanctiondocument_gid + "' AND type_of_conversation='" + values.type_of_conversation + "' AND type_of_doc='Scan Copy'");
        //    if (lsquery_no == "")
        //    {
        //        query_no = 1;
        //    }
        //    else
        //    {
        //        query_no = Convert.ToInt16(lsquery_no) + 1;
        //    }

        //    msGetGID = objcmnfunctions.GetMasterGID("DOCV");
        //    if (values.type_of_conversation == "Internal")
        //    {
        //        msGETGIDDoc = objcmnfunctions.GetMasterGID("DOCI");
        //    }
        //    else
        //    {
        //        msGETGIDDoc = objcmnfunctions.GetMasterGID("DOCC");
        //    }


        //}
        // raise conversation
        public void DaPostScannDocQuery(MdlDocConversation values, string user_gid)
        {

            msSQL = " SELECT types_of_copy FROM ids_trn_tsanctiondocumentdtls" +
               " WHERE sanctiondocument_gid='" + values.sanctiondocument_gid + "' AND types_of_copy is not null";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == false)
            {
                values.message = "Kindly Update the Types of Document";
                values.status = false;
                return;
            }
            objODBCDataReader.Close();

            msSQL = " SELECT scandocument_date FROM ids_trn_tsanctiondocumentdtls" +
                   " WHERE sanctiondocument_gid='" + values.sanctiondocument_gid + "' AND scandocument_date is not null";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == false)
            {
                values.message = "Kindly Update the Document Date";
                values.status = false;
                return;
            }
            objODBCDataReader.Close();
            int query_no = 0;
            lsuser_name = objdbconn.GetExecuteScalar("SELECT concat(user_firstname,' ',user_lastname)as user  FROM adm_mst_tuser WHERE user_gid='" + user_gid + "'");

            var lsquery_no = objdbconn.GetExecuteScalar("SELECT COUNT(*) FROM ids_trn_tdocconversation WHERE sanctiondocument_gid='" + values.sanctiondocument_gid + "' AND type_of_conversation='" + values.type_of_conversation + "' AND type_of_doc='Scan Copy'");
            if (lsquery_no == "")
            {
                query_no = 1;
            }
            else
            {
                query_no = Convert.ToInt16(lsquery_no) + 1;
            }

            msGetGID = objcmnfunctions.GetMasterGID("DOCV");
            if (values.type_of_conversation == "Internal")
            {
                msGETGIDDoc = objcmnfunctions.GetMasterGID("DOCI");
            }
            else
            {
                msGETGIDDoc = objcmnfunctions.GetMasterGID("DOCC");
            }


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
                    " noquery_flag," +
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
                    "'Scan Copy'," +
                    "'" + values.cad_query.Replace("'", "") + "'," +
                    "'" + lsuser_name + "'," +
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "'" + query_no + "',";
                if (values.noquery_flag == null)
                {
                    msSQL += "'N',";
                }
                else
                {
                    msSQL += "'Y',";
                }
                msSQL += "'" + user_gid + "'," +
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

        // conversation - response
        public void DaPostScanDocResponse(MdlDocConversation values, string user_gid)
        {
            lsuser_name = objdbconn.GetExecuteScalar("SELECT CONCAT(user_firstname,' ',user_lastname) as user_name FROM adm_mst_tuser WHERE user_gid='" + user_gid + "'");

            msSQL = " UPDATE ids_trn_tdocconversation SET" +
                    " flag='Y'," +
                    " forwarded_flag='N'," +
                    " rm_response='" + values.rm_response.Replace("'", "") + "'," +
                    " view_status='Res'," +
                    " relationshipmgr_gid='" + user_gid + "'," +
                    " relationshipmgr_name='" + lsuser_name + "'," +
                    " relationshipmgrquery_on=current_timestamp" +
                    " WHERE docconversation_gid='" + values.docconversation_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " INSERT INTO ids_trn_tconversationdocument(conversationdocument_gid, docconversation_gid, document_path, document_name, document_title, created_by, created_date)" +
                    " SELECT conversationdocument_gid,'" + values.docconversation_gid + "' ,document_path, document_name, document_title, created_by, created_date" +
                    " FROM ids_tmp_tconversationdocument" +
                    " WHERE created_by='" + user_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            msSQL = " DELETE FROM ids_tmp_tconversationdocument WHERE created_by='" + user_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Responsed Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        // No Query Response

        public void DaPostNoQueryRmResponse(string user_gid, string docconversation_gid, result objResult)
        {
            lsuser_name = objdbconn.GetExecuteScalar("SELECT CONCAT(user_firstname,' ',user_lastname) as user_name FROM adm_mst_tuser WHERE user_gid='" + user_gid + "'");

            var sanctiondocument_gid = objdbconn.GetExecuteScalar("SELECT sanctiondocument_gid FROM ids_trn_tdocconversation WHERE docconversation_gid='" + docconversation_gid + "'");

            msSQL = " UPDATE ids_trn_tdocconversation SET" +
                    " flag='Y'," +
                    " forwarded_flag='N'," +
                    " rm_response='Document Confirmed'," +
                    " view_status='Res'," +
                    " relationshipmgr_gid='" + user_gid + "'," +
                    " relationshipmgr_name='" + lsuser_name + "'," +
                    " relationshipmgrquery_on=current_timestamp" +
                    " WHERE docconversation_gid='" + docconversation_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                msSQL = " select cad_name,cad_gid, cadquery_on from ids_trn_tdocconversation " +
                        " where sanctiondocument_gid='" + sanctiondocument_gid + "' and type_of_conversation='Internal' and type_of_doc='Scan Copy' group by sanctiondocument_gid";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if(objODBCDataReader.HasRows == true)
                {
                    msSQL = " UPDATE ids_trn_tsanctiondocumentdtls SET" +
                                " finalremarks='Others'," +
                                " scanfinal_remarks='Document Confirmed'," +
                                " scanfinalremarks_by='" + lsuser_name + "'," +
                                " scanfinalremarks_on=current_timestamp," +
                                " scanfinalremarksuser_gid='" + user_gid + "'," +
                                " maker_status='Maker Confirmed'," +
                                " maker_gid='" + objODBCDataReader["cad_gid"].ToString() + "'," +
                                " maker_name='" + objODBCDataReader["cad_name"].ToString() + "'," +
                                " maked_on='"+ Convert.ToDateTime(objODBCDataReader["cadquery_on"].ToString()).ToString("yyyy-MM-dd HH:mm:ss")  + "'," +
                                " checker_status='Checker Confirmed'," +
                                " checker_gid='" + user_gid + "'," +
                                " checker_name='" + lsuser_name + "'," +
                                " checked_on=current_timestamp" +
                                " WHERE sanctiondocument_gid='" + sanctiondocument_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                objODBCDataReader.Close();
                if (mnResult == 1)
                {
                    objResult.status = true;
                    objResult.message = "Document Confirmed Successfully";
                }
                else
                {
                    objResult.status = false;
                    objResult.message = "Error Occured in Secondary Table";

                }

            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured in Primary Table";

            }



        }

        //Scan document final remarks
        public void DaPostMkrFinalRemarks(MdlScannDocSummary values, string user_gid)
        {
            msSQL = " SELECT types_of_copy FROM ids_trn_tsanctiondocumentdtls" +
                    " WHERE sanctiondocument_gid='" + values.sanctiondocument_gid + "' AND types_of_copy is not null";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == false)
            {
                values.message = "Kindly Update the Types of Document Copy";
                values.status = false;
                return;
            }
            objODBCDataReader.Close();

            msSQL = " SELECT scandocument_date FROM ids_trn_tsanctiondocumentdtls" +
                   " WHERE sanctiondocument_gid='" + values.sanctiondocument_gid + "' AND scandocument_date is not null";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == false)
            {
                values.message = "Kindly Update the Document Date";
                values.status = false;
                return;
            }
            objODBCDataReader.Close();

            var lsuser_name = objdbconn.GetExecuteScalar("SELECT CONCAT(user_firstname,' ',user_lastname) as user_name FROM adm_mst_tuser WHERE user_gid='" + user_gid + "'");

            msSQL = " UPDATE ids_trn_tsanctiondocumentdtls SET" +
                " finalremarks='" + values.finalremarks + "',";
            if (values.scanfinal_remarks == null || values.scanfinal_remarks == "")
            {
                msSQL += " scanfinal_remarks='',";
            }
            else
            {
                msSQL += " scanfinal_remarks='" + values.scanfinal_remarks.Replace("'", "") + "',";
            }
            msSQL += " scanfinalremarks_by='" + lsuser_name + "'," +
                " scanfinalremarks_on=current_timestamp," +
                " scanfinalremarksuser_gid='" + user_gid + "'" +
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



        // conversation - internal
        public void DaGetScanDocConInternal(docconlist values, string sanctiondocument_gid)
        {
            msSQL = " SELECT docconversation_gid, sanctiondocument_gid, sanction_gid," +
                    " document_gid, document_name, type_of_doc,docconversationref_no," +
                    " cad_query, rm_response, cad_name, cad_gid,date_format(cadquery_on,'%d-%m-%Y %h:%i %p') as cadquery_on," +
                    " relationshipmgr_gid, relationshipmgr_name,date_format(relationshipmgrquery_on,'%d-%m-%Y %h:%i %p') as relationshipmgrquery_on," +
                    " query_no,flag,forwarded_flag,noquery_flag," +
                     " if((SELECT COUNT(*) FROM ids_trn_tdocconversation x" +
                    " WHERE  a.docconversationref_no=x.reference_query GROUP BY a.reference_query) is null,0," +
                    " (SELECT COUNT(*) FROM ids_trn_tdocconversation x" +
                    " WHERE a.docconversationref_no=x.reference_query GROUP BY a.reference_query)) AS reference_count," +
                    " if((SELECT COUNT(*) FROM ids_trn_tconversationdocument x" +
                    " WHERE a.docconversation_gid = x.docconversation_gid GROUP BY a.docconversation_gid) is null,0," +
                    " (SELECT COUNT(*) FROM ids_trn_tconversationdocument x" +
                    " WHERE a.docconversation_gid = x.docconversation_gid GROUP BY a.docconversation_gid)) AS uploaddocument_count" +
                    " FROM ids_trn_tdocconversation a" +
                    " WHERE sanctiondocument_gid = '" + sanctiondocument_gid + "' AND type_of_conversation='Internal' AND a.type_of_doc='Scan Copy'" +
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
                        noquery_flag = dt["noquery_flag"].ToString(),

                        uploaddocument_count = dt["uploaddocument_count"].ToString(),
                        ref_count = dt["reference_count"].ToString(),


                    });
                }
                values.MdlDocConversation = getDocList;
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

        // conversation - external
        public void DaGetScanDocConExternal(docconlist values, string sanctiondocument_gid)
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
                    " WHERE sanctiondocument_gid = '" + sanctiondocument_gid + "' AND type_of_conversation='External' AND a.type_of_doc='Scan Copy'" +
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

        // export with out header details
        public void DaGetScanDocConExport(string sanction_gid, MdlDocConversation values)
        {
            var query_no = objdbconn.GetExecuteScalar("SELECT max(query_no) from ids_trn_tdocconversation where sanction_gid='" + sanction_gid + "'");
            msSQL = " SELECT a.document_code as 'Document Code',a.document_name as 'Document Name'," +
                    " if(a.scandocument_date is null,'-',date_format(a.scandocument_date, '%d-%m-%Y')) as 'Document Date'," +
                    " 'Scan Copy' as 'Types of Copy'," +
                    " a.documentrecord_id as 'Document Record ID',";
            if (query_no != "")
            {

                for (int i = 1; i <= Convert.ToInt16(query_no); i++)
                {
                    msSQL += " (" +
                     " SELECT b.cad_query FROM ids_trn_tdocconversation b" +
                     " WHERE a.sanctiondocument_gid = b.sanctiondocument_gid and" +
                     " query_no = " + i + " AND type_of_doc = 'Scan Copy' AND b.sanction_gid='" + sanction_gid + "' AND type_of_conversation='External'" +
                     " ) AS 'Cad Query " + i + "'," +
                     " (" +
                     " SELECT b.rm_response FROM ids_trn_tdocconversation b" +
                     " WHERE a.sanctiondocument_gid = b.sanctiondocument_gid and" +
                     " query_no = " + i + " AND type_of_doc = 'Scan Copy' AND b.sanction_gid='" + sanction_gid + "' AND type_of_conversation='External'" +
                     " ) as 'RM Response " + i + "',";
                }
            }
            msSQL = msSQL.TrimEnd(',');
            msSQL += " from ids_trn_tsanctiondocumentdtls a" +
                     " where sanction_gid='" + sanction_gid + "'" +
                     " AND a.sanctiondocument_gid in ( SELECT sanctiondocument_gid FROM ids_trn_tdocconversation WHERE sanction_gid='" + sanction_gid + "' AND type_of_conversation='External')" +
                     " order by document_code ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                string lscompany_code = string.Empty;


                MemoryStream ms = new MemoryStream();
                ExcelPackage excel = new ExcelPackage(ms);
                var workSheet = excel.Workbook.Worksheets.Add("IDAS Document Check List");
                try
                {
                    msSQL = " select company_code from adm_mst_tcompany";
                    lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                    values.attachment_name = "IDAS Document Check List.xls";
                    var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "IDAS/CheckList/" + sanction_gid + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                    values.attachement_cloudpath = "IDAS/CheckList/" + sanction_gid + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.attachment_name;
                    values.attachement_path = path + values.attachment_name;
                    bool exists = System.IO.Directory.Exists(path);
                    if (!exists)
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }
                    workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                    dt_datatable.Dispose();

                    FileInfo file = new FileInfo(values.attachement_path);
                    using (var range = workSheet.Cells[1, 1, 1, 20])  //Address "A1:A18"
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.Gray);
                        range.Style.Font.Color.SetColor(Color.Black);

                    }
                    excel.SaveAs(ms);
                    bool status;
                    status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/ApplicationReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/ ApplicationReport.xlsx", ms);
                    ms.Close();
                }
                catch (Exception ex)
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }
            }
            else
            {
                values.status = false;
                values.message = "No records to export!";
                return;
            }

            values.status = true;
            values.message = "Success";
        }

        // export with header details
        public void DaGetConReportExport(MdlExportConversation values)
        {
            msSQL = "delete from ids_tmp_tdownloadcount where created_date <>'" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select customername from ids_trn_tsanctiondocumentdtls a" +
                   " left join ocs_mst_tcustomer2sanction b on a.sanction_gid = b.customer2sanction_gid" +
                   " left join ocs_mst_tcustomer c on b.customer_gid = c.customer_gid where sanction_gid='" + values.sanction_gid + "' group by sanction_gid";
            string lscustomer_name = objdbconn.GetExecuteScalar(msSQL);


            msSQL = "select customer_name from ids_tmp_tdownloadcount where customer_name='" + lscustomer_name.Replace("'", "") + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == false)
            {
                objODBCDataReader.Close();
                string lscount = "0";

                int days_count = Convert.ToInt32(lscount) + 1;

                msSQL = "insert into ids_tmp_tdownloadcount (" +
                " customer_name," +
                " day_count," +
                " created_date ) values(" +
                "'" + lscustomer_name + "'," +
                "'" + days_count + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                objODBCDataReader.Close();
                msSQL = "select day_count from ids_tmp_tdownloadcount where customer_name='" + lscustomer_name.Replace("'", "") + "'";
                string lscount = objdbconn.GetExecuteScalar(msSQL);

                int days_count = Convert.ToInt32(lscount) + 1;

                msSQL = "update ids_tmp_tdownloadcount set day_count= '" + days_count + "' where  customer_name='" + lscustomer_name.Replace("'", "") + "'" +
                        " and created_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            msSQL = " select day_count from ids_tmp_tdownloadcount where customer_name='" + lscustomer_name.Replace("'", "") + "'";
            string customerwise_count = objdbconn.GetExecuteScalar(msSQL);


            string lsfilename = lscustomer_name.Replace(".", " ").Replace("'", " ").Replace("/", " ") + " " + DateTime.Now.ToString("yyyyMMdd") + customerwise_count;

            var query_no = objdbconn.GetExecuteScalar("SELECT max(query_no) from ids_trn_tdocconversation where sanction_gid='" + values.sanction_gid + "' AND type_of_conversation='External' AND type_of_doc = '" + values.type_of_copy + "'");
            msSQL = " SELECT a.document_code as 'Document Code',a.document_name as 'Document Name'," +
                    " if(a.scandocument_date is null,'-',date_format(a.scandocument_date, '%d-%m-%Y')) as 'Document Date'," +
                    " a.types_of_copy as 'Types of Copy'," +
                    " a.documentrecord_id as 'Document Record ID',";
            if (query_no != "")
            {

                for (int i = 1; i <= Convert.ToInt16(query_no); i++)
                {
                    msSQL += " (" +
                     " SELECT b.cad_query FROM ids_trn_tdocconversation b" +
                     " WHERE a.sanctiondocument_gid = b.sanctiondocument_gid and" +
                     " query_no = " + i + " AND type_of_doc = '" + values.type_of_copy + "' AND b.sanction_gid='" + values.sanction_gid + "' AND type_of_conversation='External'" +
                     " ) AS 'Cad Query " + i + "'," +
                     " (" +
                     " SELECT b.rm_response FROM ids_trn_tdocconversation b" +
                     " WHERE a.sanctiondocument_gid = b.sanctiondocument_gid and" +
                     " query_no = " + i + " AND type_of_doc = '" + values.type_of_copy + "' AND b.sanction_gid='" + values.sanction_gid + "' AND type_of_conversation='External'" +
                     " ) as 'RM Response " + i + "',";
                }
            }
            if (values.type_of_copy == "Scan Copy")
            {
                msSQL += " if(a.finalremarks='Others',a.scanfinal_remarks, a.finalremarks ) as 'Final Remarks'";
            }
            else
            {
                msSQL += " a.phyfinal_remarks as 'Final Remarks'";
            }
            msSQL += " FROM ids_trn_tsanctiondocumentdtls a" +
                     " LEFT JOIN ids_mst_tdocumentlist x on a.document_gid=x.documentlist_gid" +
                     " WHERE sanction_gid='" + values.sanction_gid + "'" +
                     " ORDER BY x.display_order ASC ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                string lscompany_code = string.Empty;
                MemoryStream ms = new MemoryStream();
                ExcelPackage excel = new ExcelPackage(ms);
                var workSheet = excel.Workbook.Worksheets.Add("" + lsfilename + "");
                try
                {
                    msSQL = " select company_code from adm_mst_tcompany";
                    lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                    values.attachment_name = "" + lsfilename + "" + ".xlsx";
                    var path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "IDAS/CheckList/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                    values.attachment_path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "IDAS/CheckList/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.attachment_name);
                    values.attachment_cloudpath = lscompany_code + "/" + "IDAS/CheckList/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.attachment_name;
                    bool exists = System.IO.Directory.Exists(path);
                    if (!exists)
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }
                    workSheet.Cells["A11"].LoadFromDataTable(dt_datatable, true);
                    dt_datatable.Dispose();

                    workSheet.Cells["B1"].Value = "IDAS Document Check List -";
                    workSheet.View.FreezePanes(12, 6);
                    using (var range = workSheet.Cells[1, 2, 1, 5])
                    {
                        range.Merge = true;
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        range.Style.Font.Color.SetColor(Color.Black);
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }
                    workSheet.Cells.Style.Locked = false;
                    workSheet.Column(1).Style.Locked = true;
                    workSheet.Column(2).Style.Locked = true;

                    workSheet.Column(3).Style.Locked = true;
                    workSheet.Column(4).Style.Locked = true;
                    workSheet.Column(5).Style.Locked = true;
                    workSheet.Column(6).Style.Locked = true;

                    workSheet.Column(1).AutoFit();
                    workSheet.Column(2).AutoFit();
                    workSheet.Column(3).AutoFit();
                    workSheet.Column(4).AutoFit();
                    workSheet.Column(5).AutoFit();
                    workSheet.Column(6).AutoFit();

                    workSheet.Protection.AllowEditObject = true;
                    workSheet.Protection.AllowEditScenarios = true;
                    workSheet.Protection.AllowFormatCells = true;
                    workSheet.Protection.AllowFormatColumns = true;
                    workSheet.Protection.AllowFormatRows = true;
                    workSheet.Protection.IsProtected = true;
                    workSheet.Protection.SetPassword("Welcome@123");

                    workSheet.Cells["B3"].Value = "Name of the Borrower";
                    using (var range = workSheet.Cells["B3"])
                    {

                        range.Style.Font.Bold = true;

                    }
                    workSheet.Cells["B4"].Value = "Name of the Facility";
                    using (var range = workSheet.Cells["B4"])
                    {
                        range.Style.Font.Bold = true;
                    }
                    workSheet.Cells["B5"].Value = "Facility Sanctioned";
                    using (var range = workSheet.Cells["B5"])
                    {
                        range.Style.Font.Bold = true;
                    }
                    //workSheet.Cells["B8"].Value = "Security Details";
                    //using (var range = workSheet.Cells["B8"])
                    //{
                    //    range.Style.Font.Bold = true;
                    //}
                    workSheet.Cells["B6"].Value = "Sanction Ref & Date";
                    using (var range = workSheet.Cells["B6"])
                    {
                        range.Style.Font.Bold = true;
                    }
                    //workSheet.Cells["B7"].Value = "Facility";
                    //using (var range = workSheet.Cells["B7"])
                    //{
                    //    range.Style.Font.Bold = true;
                    //}
                    workSheet.Cells["B7"].Value = "Segment";
                    using (var range = workSheet.Cells["B7"])
                    {
                        range.Style.Font.Bold = true;
                    }
                    workSheet.Cells["D7"].Value = "Name of the RM";
                    using (var range = workSheet.Cells["D7"])
                    {
                        range.Style.Font.Bold = true;
                        // range.Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                    }
                    workSheet.Cells["B8"].Value = "Name of the Cluster Head";
                    using (var range = workSheet.Cells["B8"])
                    {
                        range.Style.Font.Bold = true;
                    }
                    workSheet.Cells["B9"].Value = "Name of the Business Head";
                    using (var range = workSheet.Cells["B9"])
                    {
                        range.Style.Font.Bold = true;
                    }
                    workSheet.Cells["D8"].Value = "Name of the Zonal Head";
                    using (var range = workSheet.Cells["D8"])
                    {
                        range.Style.Font.Bold = true;
                    }
                    workSheet.Cells["D9"].Value = "Name of the Credit Manager";

                    using (var range = workSheet.Cells["D9"])
                    {
                        range.Style.Font.Bold = true;


                    }


                    msSQL = " SELECT b.customer_urn,b.customername,a.facility_type,a.sanction_amount," +
                            "  CONCAT(a.sanction_refno,' / ',CAST(date_format(a.sanction_date, '%d-%m-%Y')AS CHAR)) as sanction_date," +
                            " b.vertical_code,a.collateral_security,b.zonal_name,b.businesshead_name,b.cluster_manager_name,b.creditmgmt_name,b.relationshipmgmt_name" +
                            " FROM ocs_mst_tcustomer2sanction a" +
                            " INNER JOIN ocs_mst_tcustomer b on a.customer_gid = b.customer_gid" +
                            " WHERE a.customer2sanction_gid='" + values.sanction_gid + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows)
                    {
                        workSheet.Cells["B1"].Value = "IDAS Document Check List -" + objODBCDataReader["customername"].ToString();
                        workSheet.Cells["C3"].Value = objODBCDataReader["customername"].ToString();
                        workSheet.Cells["C4"].Value = objODBCDataReader["facility_type"].ToString();
                        workSheet.Cells["C5"].Value = objODBCDataReader["sanction_amount"].ToString();
                        workSheet.Cells["C6"].Value = objODBCDataReader["sanction_date"].ToString();
                        workSheet.Cells["C7"].Value = objODBCDataReader["vertical_code"].ToString();

                        workSheet.Cells["E7"].Value = objODBCDataReader["relationshipmgmt_name"].ToString();
                        workSheet.Cells["C8"].Value = objODBCDataReader["cluster_manager_name"].ToString();
                        workSheet.Cells["C9"].Value = objODBCDataReader["businesshead_name"].ToString(); ;
                        workSheet.Cells["E8"].Value = objODBCDataReader["zonal_name"].ToString();
                        workSheet.Cells["E9"].Value = objODBCDataReader["creditmgmt_name"].ToString();
                    }
                    objODBCDataReader.Close();
                    FileInfo file = new FileInfo(values.attachment_path);
                    using (var range = workSheet.Cells[11, 1, 11, 40])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        range.Style.Font.Color.SetColor(Color.Black);

                    }



                    excel.SaveAs(ms);
                    bool status;
                    status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "IDAS/CheckList/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.attachment_name, ms);
                    ms.Close();

                }

                catch (Exception ex)
                {
                    values.status = false;
                    values.message = "Failure";
                }
                values.status = true;
                values.message = "Success";
                values.attachment_cloudpath = objcmnstorage.EncryptData(values.attachment_cloudpath);
                values.attachment_path = objcmnstorage.EncryptData(values.attachment_path);

            }
            else
            {
                values.status = false;
                values.message = "No records to export!";
                return;
            }


        }

        // tagged document details
        public void DaGetDocDetailsView(MdlScannDocSummary values, string sanctiondocument_gid)
        {
            msSQL = " SELECT sanctiondocument_gid, sanction_gid, document_gid, document_code, document_name," +
                    " date_format(scandocument_date,'%d-%m-%Y') as document_date, doument_type,date_format(phydocument_date,'%d-%m-%Y') as phydocument_date," +
                    " documentrecord_id, maker_status, maker_gid, maker_name, maked_on, checker_status, checker_gid, checker_name, checked_on," +
                    " phydoc_status, phydocverified_gid, phydocverifier_name, Phydocverified_on," +
                    " checked_on, scanfinal_remarks, scanfinalremarks_by, scanfinalremarks_on,scanfinalremarksuser_gid," +
                    " phyfinal_remarks, phyfinalremarks_by, phyfinalremarks_on, phyfinalremarksuser_gid,types_of_copy,phydocument_type, finalremarks" +
                    " FROM ids_trn_tsanctiondocumentdtls" +
                    " WHERE sanctiondocument_gid = '" + sanctiondocument_gid + "'; ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows)
            {
                values.sanctiondocument_gid = objODBCDataReader["sanctiondocument_gid"].ToString();
                values.sanction_gid = objODBCDataReader["sanction_gid"].ToString();
                values.document_gid = objODBCDataReader["document_gid"].ToString();
                values.document_code = objODBCDataReader["document_code"].ToString();
                values.document_name = objODBCDataReader["document_name"].ToString();
                values.scandocument_date = objODBCDataReader["document_date"].ToString();
                values.documentrecord_id = objODBCDataReader["documentrecord_id"].ToString();
                values.scanfinal_remarks = objODBCDataReader["scanfinal_remarks"].ToString();
                values.phyfinal_remarks = objODBCDataReader["phyfinal_remarks"].ToString();
                values.maker_status = objODBCDataReader["maker_status"].ToString();
                values.checker_status = objODBCDataReader["checker_status"].ToString();
                values.types_of_copy = objODBCDataReader["types_of_copy"].ToString();
                values.phydoc_status = objODBCDataReader["phydoc_status"].ToString();
                values.phydocument_date = objODBCDataReader["phydocument_date"].ToString();
                values.phydocument_type = objODBCDataReader["phydocument_type"].ToString();
                values.finalremarks = objODBCDataReader["finalremarks"].ToString();
            }
            objODBCDataReader.Close();
        }

        // document date - update
        public void DaPostScanDocumentDate(MdlScannDocSummary values)
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
                    " scandocument_date='" + Convert.ToDateTime(values.document_date).ToString("yyyy-MM-dd") + "'" +
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


        // RM Response upload
        public void DaPostUpload(HttpRequest httpRequest, string user_gid, result objResult)
        {
            try
            {
                HttpFileCollection httpFileCollection;
                DataTable dt = null;
                string lspath, lsfilePath, path, lsfile_name;
                string sanction_gid = httpRequest.Form["sanction_gid"];
                string type_of_doc = httpRequest.Form["types_of_doc"];
                string project_flag = httpRequest.Form["project_flag"].ToString();
                Stream ls_readStream;
                MemoryStream ms = new MemoryStream();

                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                // Create Directory
                lsfilePath = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/IDAS/Upload/" + sanction_gid + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
                //path = ConfigurationManager.AppSettings["file_path"] + "/erpdocuments" + "/" + lscompany_code + "/" + "IDAS/Upload/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
                if ((!System.IO.Directory.Exists(lsfilePath)))
                    System.IO.Directory.CreateDirectory(lsfilePath);


                httpFileCollection = httpRequest.Files;
                for (int i = 0; i < httpFileCollection.Count; i++)
                {
                    httpPostedFile = httpFileCollection[i];
                }
                string FileExtension = httpPostedFile.FileName;
                string file_name = httpPostedFile.FileName;
                ls_readStream = httpPostedFile.InputStream;
                ls_readStream.CopyTo(ms);
                byte[] bytes = ms.ToArray();
                if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                {
                    objResult.status = false;
                    objResult.message = "File format is not supported";
                    return;
                }
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/IDAS/Upload/" + sanction_gid + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + file_name + FileExtension, ms);
                ms.Close();
                path = lsfilePath + "/" + file_name;
                //path creation        
                lspath = lsfilePath + "/";
                objcmnfunctions.uploadFile(lspath, file_name);
                //Excel To DataTable
                lsfile_name = file_name;
                lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_name + "";
                lsfilePath = lsfilePath.Replace("\\", "\\\\");
                dt = objcmnfunctions.ExcelToDataTable(lsfilePath, "A11:BZ");
                var query_no = objdbconn.GetExecuteScalar("SELECT max(query_no) from ids_trn_tdocconversation where sanction_gid='" + sanction_gid + "' AND type_of_conversation='External' AND type_of_doc='" + type_of_doc + "'");
                lsuser_name = objdbconn.GetExecuteScalar("SELECT concat(user_firstname,' ',user_lastname)as user  FROM adm_mst_tuser WHERE user_gid='" + user_gid + "'");
                foreach (DataRow row in dt.Rows)
                {
                    int count = 1;
                    for (count = 1; count <= Convert.ToInt16(query_no); count++)
                    {
                        if (row["RM Response " + count].ToString() != "")
                        {
                            var document_gid = objdbconn.GetExecuteScalar("SELECT documentlist_gid FROM ids_mst_tdocumentlist WHERE document_code='" + row["Document Code"].ToString() + "'");

                            var sanctiondocument_gid = objdbconn.GetExecuteScalar("SELECT sanctiondocument_gid FROM ids_trn_tsanctiondocumentdtls WHERE documentrecord_id='" + row["Document Record ID"].ToString() + "'");

                            msSQL = " UPDATE ids_trn_tdocconversation SET" +
                                    "  flag='Y'," +
                                    " relationshipmgr_gid='" + user_gid + "'," +
                                    " relationshipmgr_name='" + lsuser_name + "'," +
                                    " relationshipmgrquery_on=current_timestamp," +
                                    " query_from='Upload Document'," +
                                    " rm_response='" + row["RM Response " + count].ToString().Replace("'", "") + "'," +
                                    " view_status='Res'" +
                                    " WHERE document_gid='" + document_gid + "' AND sanctiondocument_gid='" + sanctiondocument_gid + "' AND " +
                                    " sanction_gid='" + sanction_gid + "' AND query_no='" + count + "' AND type_of_conversation='External' AND type_of_doc = '" + type_of_doc + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }

                }
                dt.Dispose();

                objResult.status = true;
                objResult.message = "Uploaded Successfully";

            }
            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = ex.ToString();
            }
        }

        // Document Comments
        public void DaGetDocComments(MdlsanctionDocDtls values, string sanctiondocument_gid)
        {
            msSQL = " SELECT if(b.comments='','No Comments',b.comments) as comments FROM ids_trn_tsanctiondocumentdtls a " +
                    " LEFT JOIN ids_mst_tdocumentlist b on a.document_gid = b.documentlist_gid" +
                    " WHERE sanctiondocument_gid = '" + sanctiondocument_gid + "'; ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows)
            {

                values.doc_comments = objODBCDataReader["comments"].ToString();

            }
            objODBCDataReader.Close();
        }

        public void DaMkrChrBulkDocVerification(MdlBulkverification values, string user_gid)
        {
            foreach (string i in values.sanctiondocument_gid)
            {
                msSQL = " select * from ids_trn_tsanctiondocumentdtls where sanctiondocument_gid='" + i + "' and maker_status = 'Maker Confirmed' and checker_status = 'Checker Confirmed'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
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

                    if (values.confirmation_type == "Maker")
                    {
                        msSQL = " select relationshipmgr_name,relationshipmgr_gid, relationshipmgrquery_on from ids_trn_tdocconversation " +
                                " where sanctiondocument_gid='" + i + "' and type_of_conversation='Internal' and type_of_doc='Scan Copy' group by sanctiondocument_gid";
                        objODBCDataReader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDataReader.HasRows == true)
                        {
                            msSQL = " select relationshipmgr_name from ids_trn_tdocconversation where sanctiondocument_gid='" + i + "' " +
                           " and type_of_conversation='Internal' and type_of_doc='Scan Copy' group by sanctiondocument_gid";
                            string lsrelationshipmgr_name = objdbconn.GetExecuteScalar(msSQL);
                            if (lsrelationshipmgr_name == "" || lsrelationshipmgr_name == null)
                            {
                                values.status = false;
                                values.message = "Kindly Update Checker Response";
                                return;
                            }

                            msSQL = " UPDATE ids_trn_tsanctiondocumentdtls SET" +
                           " scandocument_date='" + Convert.ToDateTime(values.document_date).ToString("yyyy-MM-dd 00:00:00") + "'," +
                           " types_of_copy='" + values.type_copy + "'," +
                           " finalremarks='" + values.finalremarks + "',";
                            if (values.scanfinal_remarks == null || values.scanfinal_remarks == "")
                            {
                                msSQL += " scanfinal_remarks='',";
                            }
                            else
                            {
                                msSQL += " scanfinal_remarks='" + values.scanfinal_remarks.Replace("'", "") + "',";
                            }
                            msSQL += " scanfinalremarks_by='" + lsuser_name + "'," +
                                    " scanfinalremarks_on=current_timestamp," +
                                    " maker_status='Maker Confirmed'," +
                                    " maker_gid='" + user_gid + "'," +
                                    " maker_name='" + lsuser_name + "'," +
                                    " maked_on=current_timestamp," +
                                    " checker_status='Checker Confirmed'," +
                                    " checker_gid='" + objODBCDataReader["relationshipmgr_gid"].ToString() + "'," +
                                    " checker_name='" + objODBCDataReader["relationshipmgr_name"].ToString() + "'," +
                                    " checked_on='" + Convert.ToDateTime(objODBCDataReader["relationshipmgrquery_on"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                    " WHERE sanctiondocument_gid='" + i + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        else
                        {
                            values.status = false;
                            values.message = "Kindly Update Maker Response";
                            return;
                        }
                        objODBCDataReader.Close();
                    }
                    else
                    {
                        msSQL = " select cad_name,cad_gid, cadquery_on from ids_trn_tdocconversation " +
                                " where sanctiondocument_gid='" + i + "' and type_of_conversation='Internal' and type_of_doc='Scan Copy' group by sanctiondocument_gid";
                        objODBCDataReader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDataReader.HasRows == true)
                        {
                            msSQL = " UPDATE ids_trn_tsanctiondocumentdtls SET" +
                          " scandocument_date='" + Convert.ToDateTime(values.document_date).ToString("yyyy-MM-dd 00:00:00") + "'," +
                          " types_of_copy='" + values.type_copy + "'," +
                          " finalremarks='" + values.finalremarks + "',";
                            if (values.scanfinal_remarks == null || values.scanfinal_remarks == "")
                            {
                                msSQL += " scanfinal_remarks='',";
                            }
                            else
                            {
                                msSQL += " scanfinal_remarks='" + values.scanfinal_remarks.Replace("'", "") + "',";
                            }
                            msSQL += " scanfinalremarks_by='" + lsuser_name + "'," +
                                    " scanfinalremarks_on=current_timestamp," +
                                    " maker_status='Maker Confirmed'," +
                                    " maker_gid='" + objODBCDataReader["cad_gid"].ToString() + "'," +
                                    " maker_name='" + objODBCDataReader["cad_name"].ToString() + "'," +
                                    " maked_on='" + Convert.ToDateTime(objODBCDataReader["cadquery_on"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                    " checker_status='Checker Confirmed'," +
                                    " checker_gid='" + user_gid + "'," +
                                    " checker_name='" + lsuser_name + "'," +
                                    " checked_on=current_timestamp" +
                                    " WHERE sanctiondocument_gid='" + i + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        else
                        {
                            msSQL = " UPDATE ids_trn_tsanctiondocumentdtls SET" +
                           " scandocument_date='" + Convert.ToDateTime(values.document_date).ToString("yyyy-MM-dd 00:00:00") + "'," +
                           " types_of_copy='" + values.type_copy + "'," +
                           " finalremarks='" + values.finalremarks + "',";
                            if (values.scanfinal_remarks == null || values.scanfinal_remarks == "")
                            {
                                msSQL += " scanfinal_remarks='',";
                            }
                            else
                            {
                                msSQL += " scanfinal_remarks='" + values.scanfinal_remarks.Replace("'", "") + "',";
                            }
                            msSQL += " scanfinalremarks_by='" + lsuser_name + "'," +
                                    " scanfinalremarks_on=current_timestamp," +
                                    " maker_status='Maker Confirmed'," +
                                    " maker_gid='" + user_gid + "'," +
                                    " maker_name='" + lsuser_name + "'," +
                                    " maked_on=current_timestamp," +
                                    " checker_status='Checker Confirmed'," +
                                    " checker_gid='" + user_gid + "'," +
                                    " checker_name='" + lsuser_name + "'," +
                                    " checked_on=current_timestamp" +
                                    " WHERE sanctiondocument_gid='" + i + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        objODBCDataReader.Close();
                    }
                }
                objODBCDataReader.Close();
            }
            if (mnResult == 1)
            {
                values.status = true;
                values.message = " Documents Confirmed Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }
    }
}