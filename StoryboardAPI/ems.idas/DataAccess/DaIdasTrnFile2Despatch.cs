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
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Globalization;
using ems.storage.Functions;

namespace ems.idas.DataAccess
{
    public class DaIdasTrnFile2Despatch
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDataReader;
        DataTable dt_datatable;
        string msSQL;
        int mnResult, rowCount;
        string msGetGIDRef, msGeTGID, lscompany_code, excelRange, lscustomer_urn, lscustomer_name, lstagged_document, lsfileref_no, lsbatched_name, lsbatched_on,
               lsstampref_no, lsbarcode_no, lscartonboxref_no, lsboxed_date, lsremarks;
        HttpPostedFile httpPostedFile;

        public void DaGetBatch(batchlist values)
        {
            msSQL = " SELECT a.batch_gid,a.batchref_no,a.sanction_gid,a.batchcreated_name,a.stampref_no," +
                    " DATE_FORMAT(a.created_date,'%d-%m-%Y %h:%i %p') as batched_on, a.barcoderef_no," +
                    " c.customer_urn, c.customername," +
                    " (SELECT COUNT(*) FROM ids_trn_tsanctiondocumentdtls b" +
                    " WHERE a.sanction_gid = b.sanction_gid GROUP BY a.sanction_gid) AS tagged_document,a.redespatch_flag" +
                    " FROM ids_trn_tbatch a" +
                    " LEFT JOIN ocs_mst_tcustomer2sanction b ON a.sanction_gid = b.customer2sanction_gid" +
                    " LEFT JOIN ocs_mst_tcustomer c ON b.customer_gid=c.customer_gid" +
                    " WHERE a.batch_gid NOT IN (SELECT batch_gid FROM ids_trn_tbox2file WHERE 1=1) and (stampref_no<>'' or stampref_no<>null)" +
                    " ORDER BY a.created_date DESC,stampref_no DESC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<MdlbatchSummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getDocList.Add(new MdlbatchSummary
                    {
                        sanction_gid = dt["sanction_gid"].ToString(),
                        stampref_no = dt["stampref_no"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        customer_name = dt["customername"].ToString(),

                        tagged_document = dt["tagged_document"].ToString(),
                        batch_gid = dt["batch_gid"].ToString(),
                        batch_by = dt["batchcreated_name"].ToString(),
                        fileref_no = dt["batchref_no"].ToString(),
                        batched_on = dt["batched_on"].ToString(),
                        redespatch_flag = dt["redespatch_flag"].ToString(),
                        barcoderef_no = dt["barcoderef_no"].ToString(),
                    });
                }
                values.MdlbatchSummary = getDocList;
            }
            dt_datatable.Dispose();
        }

        public void DaGetTaggedBatchDtls(string cartonbox_gid, batchlist values)
        {
            msSQL = " SELECT a.batch_gid,a.batchref_no,a.sanction_gid,d.batchcreated_name," +
                    " DATE_FORMAT(d.created_date,'%d-%m-%Y %h:%i %p') as batched_on," +
                    " d.customer_urn, d.customer_name,d.stampref_no, d.barcoderef_no, " +
                    " (SELECT COUNT(*) FROM ids_trn_tsanctiondocumentdtls b" +
                    " WHERE a.sanction_gid = b.sanction_gid GROUP BY a.sanction_gid) AS tagged_document,d.redespatch_flag" +
                    " FROM ids_trn_tbox2file a" +
                    " LEFT JOIN ocs_mst_tcustomer2sanction b ON a.sanction_gid = b.customer2sanction_gid" +
                    " LEFT JOIN ids_trn_tbatch d ON a.batch_gid=d.batch_gid" +
                    " WHERE a.cartonbox_gid='" + cartonbox_gid + "'" +
                    " ORDER BY a.created_date DESC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<MdlbatchSummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getDocList.Add(new MdlbatchSummary
                    {
                        sanction_gid = dt["sanction_gid"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        stampref_no = dt["stampref_no"].ToString(),
                        tagged_document = dt["tagged_document"].ToString(),
                        batch_gid = dt["batch_gid"].ToString(),
                        batch_by = dt["batchcreated_name"].ToString(),
                        fileref_no = dt["batchref_no"].ToString(),
                        batched_on = dt["batched_on"].ToString(),
                        redespatch_flag = dt["redespatch_flag"].ToString(),
                        barcoderef_no = dt["barcoderef_no"].ToString(),
                    });
                }
                values.MdlbatchSummary = getDocList;
            }
            dt_datatable.Dispose();
        }

        public void DaPostCartonBoxCreate(MdlCartonBox values, string user_gid)
        {
            msGetGIDRef = objcmnfunctions.GetMasterGID("BOX");
            msGeTGID = objcmnfunctions.GetMasterGID("BXT");
            msSQL = "INSERT INTO ids_trn_tcartonbox(" +
                " cartonbox_gid," +
                " boxref_no," +
                " stampref_no," +
                " cartonbox_date," +
                " boxbarcoderef_no," +
                " remarks," +
                " created_by," +
                " created_date)" +
                " VALUES(" +
                "'" + msGeTGID + "'," +
                "'" + msGetGIDRef + "'," +
                "'" + values.stampref_no.Replace("'", "") + "'," +
                "'" + Convert.ToDateTime(values.cartonbox_date).ToString("yyyy-MM-dd") + "',";
            if (values.boxbarcoderef_no == null || values.boxbarcoderef_no == "" || values.boxbarcoderef_no == "undefined")
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.boxbarcoderef_no.Replace(",", "") + "',";
            }
            msSQL += "'" + values.remarks.Replace("'", "") + "'," +
                "'" + user_gid + "'," +
                "current_timestamp)";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            foreach (string i in values.batch_gid)
            {
                msGetGIDRef = objcmnfunctions.GetMasterGID("BO2F");
                var lsbatchref_no = objdbconn.GetExecuteScalar("SELECT batchref_no FROM ids_trn_tbatch WHERE batch_gid='" + i + "'");
                var lssanction_gid = objdbconn.GetExecuteScalar("SELECT sanction_gid FROM ids_trn_tbatch WHERE batch_gid='" + i + "'");
                var lsbarcoderef_no = objdbconn.GetExecuteScalar("SELECT barcoderef_no FROM ids_trn_tbatch WHERE batch_gid='" + i + "'");
                msSQL = " INSERT INTO ids_trn_tbox2file(" +
                    " box2file_gid," +
                    " cartonbox_gid," +
                    " batchref_no," +
                    " batch_gid," +
                    " sanction_gid," +
                    " boxbarcoderef_no," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGIDRef + "'," +
                    "'" + msGeTGID + "'," +
                    "'" + lsbatchref_no + "'," +
                    "'" + i + "'," +
                    "'" + lssanction_gid + "'," +
                    "'" + lsbarcoderef_no + "'," +
                    "'" + user_gid + "'," +
                    "current_timestamp)";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Box Created Successfully...";
            }
        }

        public void DaGetCartonBoxSummary(CartonBoxlist objResult)
        {
            msSQL = " SELECT a.cartonbox_gid,a.boxref_no,DATE_FORMAT(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " DATE_FORMAT(a.cartonbox_date,'%d-%m-%Y') as cartonbox_date,a.remarks,a.stampref_no," +
                    " CONCAT(b.user_code, ' / ', b.user_firstname) as created_by, a.boxbarcoderef_no," +
                    " (SELECT COUNT(*) FROM ids_trn_tbox2file x WHERE x.cartonbox_gid = a.cartonbox_gid  GROUP BY cartonbox_gid) AS tagged_count" +
                    " FROM ids_trn_tcartonbox a" +
                    " LEFT JOIN adm_mst_tuser b on a.created_by = b.user_gid" +
                    " WHERE a.cartonbox_gid NOT IN(SELECT cartonbox_gid FROM ids_trn_tbox2despatch WHERE 1 = 1)" +
                    " ORDER BY a.created_date DESC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                objResult.MdlCartonBoxSummary = dt_datatable.AsEnumerable().Select(row => new MdlCartonBoxSummary
                {
                    cartonbox_gid = row["cartonbox_gid"].ToString(),
                    cartonboxref_no = row["boxref_no"].ToString(),
                    created_by = row["created_by"].ToString(),
                    tagged_count = row["tagged_count"].ToString(),
                    created_date = row["created_date"].ToString(),
                    remarks = row["remarks"].ToString(),
                    stampref_no = row["stampref_no"].ToString(),
                    cartonboxed_date = row["cartonbox_date"].ToString(),
                    boxbarcoderef_no = row["boxbarcoderef_no"].ToString(),
                }).ToList();
                objResult.status = true;
                objResult.message = "Success";

            }
            else
            {
                objResult.status = false;
                objResult.message = "No Records Found";
            }
            dt_datatable.Dispose();

        }

        public void DaGetTaggedBoxDtls(CartonBoxlist objResult, string despatch_gid)
        {
            msSQL = " SELECT a.cartonbox_gid,c.boxref_no,DATE_FORMAT(c.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " DATE_FORMAT(c.cartonbox_date,'%d-%m-%Y') as cartonbox_date,c.remarks,c.stampref_no," +
                    " CONCAT(b.user_code, ' / ', b.user_firstname) as created_by, c.boxbarcoderef_no, " +
                    " (SELECT COUNT(*) FROM ids_trn_tbox2file x WHERE x.cartonbox_gid = a.cartonbox_gid  GROUP BY cartonbox_gid) AS tagged_count" +
                    " FROM ids_trn_tbox2despatch a" +
                    " LEFT JOIN ids_trn_tcartonbox c on a.cartonbox_gid=c.cartonbox_gid" +
                    " LEFT JOIN adm_mst_tuser b on c.created_by = b.user_gid" +
                    " WHERE a.despatch_gid='" + despatch_gid + "'" +
                    " ORDER BY a.created_date DESC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                objResult.MdlCartonBoxSummary = dt_datatable.AsEnumerable().Select(row => new MdlCartonBoxSummary
                {
                    cartonbox_gid = row["cartonbox_gid"].ToString(),
                    cartonboxref_no = row["boxref_no"].ToString(),
                    created_by = row["created_by"].ToString(),
                    tagged_count = row["tagged_count"].ToString(),
                    created_date = row["created_date"].ToString(),
                    remarks = row["remarks"].ToString(),
                    stampref_no = row["stampref_no"].ToString(),
                    cartonboxed_date = row["cartonbox_date"].ToString(),
                    boxbarcoderef_no = row["boxbarcoderef_no"].ToString(),
                }).ToList();
                objResult.status = true;
                objResult.message = "Success";

            }
            else
            {
                objResult.status = false;
                objResult.message = "No Records Found";
            }
            dt_datatable.Dispose();

        }

        public void DaPostCreateDespatch(MdlDespatch objResult, string user_gid)
        {
            msGeTGID = objcmnfunctions.GetMasterGID("DSPT");
            msGetGIDRef = objcmnfunctions.GetMasterGID("DESP");

            msSQL = " INSERT INTO ids_trn_tdespatch (" +
                    " despatch_gid," +
                    " despatchref_no," +
                    " despatch_date," +
                    " vendor_name," +
                    " contact_person," +
                    " mobile_no," +
                    " stampref_no," +
                    " despatched_by," +
                    " remarks," +
                    " created_by," +
                    " created_date)" +
                    " VALUES(" +
                    "'" + msGeTGID + "'," +
                    "'" + msGetGIDRef + "'," +
                    "'" + Convert.ToDateTime(objResult.despatch_date).ToString("yyyy-MM-dd") + "'," +
                    "'" + objResult.vendor_name + "'," +
                    "'" + objResult.contact_person + "'," +
                    "'" + objResult.mobile_no.Replace("'", "") + "'," +
                    "'" + objResult.stampref_no.Replace("'", "") + "'," +
                    "'" + objResult.desptached_by + "'," +
                    "'" + objResult.remarks.Replace("'", "") + "'," +
                    " ( SELECT CONCAT(user_code,' / ',user_firstname,user_lastname) as user_name FROM adm_mst_tuser WHERE user_gid='" + user_gid + "')," +
                    " current_timestamp)";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult == 1)
            {
                msSQL = "INSERT INTO ids_trn_tdespatchdocument( despatchdocument_gid, despatch_gid, document_path, document_name,document_title, created_by, created_date)" +
                  " SELECT conversationdocument_gid, '" + msGeTGID + "' ,document_path, document_name, document_title, created_by, created_date" +
                  " FROM ids_tmp_tconversationdocument" +
                  " WHERE created_by='" + user_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                msSQL = "DELETE FROM ids_tmp_tconversationdocument WHERE created_by='" + user_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                foreach (string i in objResult.cartonbox_gid)
                {
                    msGetGIDRef = objcmnfunctions.GetMasterGID("BX2D");

                    msSQL = " INSERT INTO ids_trn_tbox2despatch(" +
                            " box2despatch_gid," +
                            " cartonbox_gid," +
                            " despatch_gid," +
                            " created_date)" +
                            " VALUES(" +
                            "'" + msGetGIDRef + "'," +
                            "'" + i + "'," +
                            "'" + msGeTGID + "'," +
                            " current_timestamp)";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }
                objResult.status = true;
                objResult.message = "Despatch Details Updated Successfully";

            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured in Primary Table";
            }




        }

        public void DaGetDespatchDtls(MdlDespatch objResult, string despatch_gid)
        {
            try
            {
                msSQL = " SELECT " +
                                  " despatchref_no,DATE_FORMAT(despatch_date,'%d-%m-%Y') as despatch_date, vendor_name, contact_person, mobile_no," +
                                  " despatched_by, remarks,stampref_no" +
                                  " FROM ids_trn_tdespatch" +
                                  " WHERE despatch_gid='" + despatch_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    objODBCDataReader.Read();
                    objResult.despatchref_no = objODBCDataReader["despatchref_no"].ToString();
                    objResult.despatch_date = objODBCDataReader["despatch_date"].ToString();
                    objResult.vendor_name = objODBCDataReader["vendor_name"].ToString();
                    objResult.contact_person = objODBCDataReader["contact_person"].ToString();
                    objResult.mobile_no = objODBCDataReader["mobile_no"].ToString();
                    objResult.desptached_by = objODBCDataReader["despatched_by"].ToString();
                    objResult.remarks = objODBCDataReader["remarks"].ToString();
                    objResult.stampref_no = objODBCDataReader["stampref_no"].ToString();
                }
                objODBCDataReader.Close();
                objResult.status = true;
                objResult.message = "Success";

            }
            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = ex.Message;
            }
        }

        public void DaGetDespatchDocument(string despatch_gid, uploaddocumentlist objResult)
        {
            msSQL = " SELECT a.despatchdocument_gid, a.document_name,a.document_path,date_format(a.created_date,'%d-%m-%Y') as created_date," +
                    " CONCAT(b.user_code,' / ',b.user_firstname,b.user_lastname) as user_name" +
                    " FROM ids_trn_tdespatchdocument a" +
                    " LEFT JOIN adm_mst_tuser b ON a.created_by=b.user_gid" +
                    " WHERE despatch_gid='" + despatch_gid + "'";
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
                        created_by = dt["user_name"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        uploaddocument_gid = dt["despatchdocument_gid"].ToString(),

                    });
                }
                objResult.uploaddocument = getDocList;
            }
            dt_datatable.Dispose();

        }

        public void DaGetBoxDtls(MdlCartonBox objResult, string cartonbox_gid)
        {
            try
            {
                msSQL = " SELECT " +
                 " boxref_no, stampref_no,DATE_FORMAT( cartonbox_date,'%d-%m-%Y') as cartonbox_date, remarks, boxbarcoderef_no" +
                 " FROM ids_trn_tcartonbox" +
                 " WHERE cartonbox_gid='" + cartonbox_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    objODBCDataReader.Read();
                    objResult.boxref_no = objODBCDataReader["boxref_no"].ToString();
                    objResult.stampref_no = objODBCDataReader["stampref_no"].ToString();
                    objResult.cartonbox_date = objODBCDataReader["cartonbox_date"].ToString();
                    objResult.remarks = objODBCDataReader["remarks"].ToString();
                    objResult.boxbarcoderef_no = objODBCDataReader["boxbarcoderef_no"].ToString();
                }
                objODBCDataReader.Close();
                objResult.status = true;
                objResult.message = "Success";
            }
            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = ex.Message;
            }

        }

        public void DaGetDespatchSummary(DespatchList objResult)
        {
            msSQL = " SELECT a.despatch_gid,a.despatchref_no,a.stampref_no," +
                    " DATE_FORMAT(a.created_date, '%d-%m-%Y %h:%i %p') as created_date," +
                    " DATE_FORMAT(a.despatch_date, '%d-%m-%Y') as despatched_date," +
                    " a.despatched_by,a.vendor_name,a.contact_person," +
                    " (SELECT COUNT(*) FROM ids_trn_tbox2despatch x WHERE x.despatch_gid = a.despatch_gid  GROUP BY despatch_gid) AS tagged_count" +
                    " FROM ids_trn_tdespatch a" +
                    " WHERE 1 = 1" +
                    " ORDER BY a.created_date DESC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                objResult.MdlDespatch = dt_datatable.AsEnumerable().Select(row => new MdlDespatch
                {
                    despatch_gid = row["despatch_gid"].ToString(),
                    despatchref_no = row["despatchref_no"].ToString(),
                    created_date = row["created_date"].ToString(),
                    despatch_date = row["despatched_date"].ToString(),
                    desptached_by = row["despatched_by"].ToString(),
                    vendor_name = row["vendor_name"].ToString(),
                    contact_person = row["contact_person"].ToString(),
                    tagged_count = row["tagged_count"].ToString(),
                    stampref_no = row["stampref_no"].ToString(),

                }).ToList();
                objResult.status = true;
                objResult.message = "Success";

            }
            else
            {
                objResult.status = false;
                objResult.message = "No Records Found";
            }
            dt_datatable.Dispose();


        }

        public void DaGetFileCount(MdlFilecount values)
        {
            try
            {
                msSQL = " SELECT A.totalfile_count,B.taggedfile_count,C.untaggedfile_count," +
                  " D.totalbox_count,E.taggedbox_count,F.untaggedbox_count,G.despatch_count" +
                  " FROM" +
                  " (SELECT IF(count(*) IS  NULL, 0, count(*)) AS totalfile_count FROM ids_trn_tbatch WHERE 1 = 1) AS A," +
                  " (SELECT IF(count(*) IS NULL, 0, count(*)) AS taggedfile_count FROM ids_trn_tbox2file WHERE 1 = 1) AS B," +
                  " (SELECT IF(count(*) IS NULL, 0, count(*)) AS untaggedfile_count FROM ids_trn_tbatch" +
                  " WHERE batch_gid NOT IN(SELECT batch_gid FROM ids_trn_tbox2file WHERE 1 = 1)) AS C," +
                  " (SELECT IF(count(*) IS NULL, 0, count(*)) AS totalbox_count FROM ids_trn_tcartonbox WHERE 1 = 1) AS D," +
                  " (SELECT IF(count(*) is null, 0, count(*)) AS taggedbox_count FROM ids_trn_tbox2despatch WHERE 1 = 1) AS E," +
                  " (SELECT if (count(*) is null,0,count(*)) AS untaggedbox_count FROM ids_trn_tcartonbox" +
                  " WHERE cartonbox_gid NOT IN (SELECT cartonbox_gid FROM ids_trn_tbox2despatch where 1 = 1 )) AS F ," +
                  " (SELECT if (count(*) is null,0,count(*)) AS despatch_count FROM ids_trn_tdespatch WHERE 1=1) AS G; ";

                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    objODBCDataReader.Read();
                    values.totalfile_count = objODBCDataReader["totalfile_count"].ToString();
                    values.taggedfile_count = objODBCDataReader["taggedfile_count"].ToString();
                    values.untaggedfile_count = objODBCDataReader["untaggedfile_count"].ToString();

                    values.totalbox_count = objODBCDataReader["totalbox_count"].ToString();
                    values.taggedbox_count = objODBCDataReader["taggedbox_count"].ToString();
                    values.untaggedbox_count = objODBCDataReader["untaggedbox_count"].ToString();

                    values.despatch_count = objODBCDataReader["despatch_count"].ToString();

                }
                objODBCDataReader.Close();

                values.status = true;
                values.message = "Success";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.Message;
            }


        }

        public void DaPostUpdateBatchStamp(MdlBatchStamp values, result objResult, string user_gid)
        {
            msSQL = " SELECT batch_gid FROM ids_trn_tbatch WHERE stampref_no='" + values.stampref_no.Replace("'", "") + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows)
            {
                objODBCDataReader.Close();
                objResult.status = false;
                objResult.message = "File Stamp Ref No. Already Exist.";
                return;
            }
            objODBCDataReader.Close();

            msSQL = " UPDATE ids_trn_tbatch SET" +
                    " stampref_no='" + values.stampref_no.Replace("'", "") + "'," +
                    " updated_by='" + user_gid + "'," +
                    " updated_date=current_timestamp" +
                    " WHERE sanction_gid='" + values.sanction_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                objResult.status = true;
                objResult.message = "Stamp Reference Number Updated Successfully";
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";
            }
        }

        public void DaEditBarCode(string sanction_gid, MdlbatchSummary values)
        {
            msSQL = " SELECT a.sanction_gid, a.barcoderef_no, c.customer_urn, c.customername" +
                    " FROM ids_trn_tbatch a" +
                    " LEFT JOIN ocs_mst_tcustomer2sanction b ON a.sanction_gid = b.customer2sanction_gid" +
                    " LEFT JOIN ocs_mst_tcustomer c ON b.customer_gid=c.customer_gid" +
                    " WHERE sanction_gid='" + sanction_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows)
            {
                values.sanction_gid = objODBCDataReader["sanction_gid"].ToString();
                values.customer_urn = objODBCDataReader["customer_urn"].ToString();
                values.customer_name = objODBCDataReader["customername"].ToString();
                values.barcoderef_no = objODBCDataReader["barcoderef_no"].ToString();
            }
            objODBCDataReader.Close();
        }

        public void DaUpdateBarCode(MdlBatchStamp values, result objResult, string user_gid)
        {
            msSQL = " SELECT batch_gid FROM ids_trn_tbatch WHERE barcoderef_no='" + values.barcoderef_no.Replace("'", "") + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows)
            {
                objODBCDataReader.Close();
                objResult.status = false;
                objResult.message = "File BarCode Ref No Already Exist.";
                return;
            }
            objODBCDataReader.Close();

            msSQL = " UPDATE ids_trn_tbatch SET" +
                    " barcoderef_no='" + values.barcoderef_no.Replace("'", "") + "'," +
                    " updated_by='" + user_gid + "'," +
                    " updated_date=current_timestamp" +
                    " WHERE sanction_gid='" + values.sanction_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                objResult.status = true;
                objResult.message = "BarCode Reference Number Updated Successfully";
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured While Updating BarCode";
            }
        }

        public void DaBatchPendingSummary(batchlist values)
        {
            msSQL = " SELECT a.batch_gid,a.batchref_no,a.sanction_gid,a.batchcreated_name,a.stampref_no," +
                    " DATE_FORMAT(a.created_date,'%d-%m-%Y %h:%i %p') as batched_on, a.barcoderef_no," +
                    " c.customer_urn, c.customername," +
                    " (SELECT COUNT(*) FROM ids_trn_tsanctiondocumentdtls b" +
                    " WHERE a.sanction_gid = b.sanction_gid GROUP BY a.sanction_gid) AS tagged_document,a.redespatch_flag" +
                    " FROM ids_trn_tbatch a" +
                    " LEFT JOIN ocs_mst_tcustomer2sanction b ON a.sanction_gid = b.customer2sanction_gid" +
                    " LEFT JOIN ocs_mst_tcustomer c ON b.customer_gid=c.customer_gid" +
                    " WHERE a.batch_gid NOT IN (SELECT batch_gid FROM ids_trn_tbox2file WHERE 1=1) and stampref_no is null" +
                    " ORDER BY a.created_date DESC,stampref_no DESC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<MdlbatchSummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getDocList.Add(new MdlbatchSummary
                    {
                        sanction_gid = dt["sanction_gid"].ToString(),
                        stampref_no = dt["stampref_no"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        customer_name = dt["customername"].ToString(),

                        tagged_document = dt["tagged_document"].ToString(),
                        batch_gid = dt["batch_gid"].ToString(),
                        batch_by = dt["batchcreated_name"].ToString(),
                        fileref_no = dt["batchref_no"].ToString(),
                        batched_on = dt["batched_on"].ToString(),
                        redespatch_flag = dt["redespatch_flag"].ToString(),
                        barcoderef_no = dt["barcoderef_no"].ToString(),
                    });
                }
                values.MdlbatchSummary = getDocList;
            }
            dt_datatable.Dispose();
        }

        public void DaBatchExportExcel(MdlbatchSummary values)
        {
            msSQL = " select c.customer_urn as Customer_URN, c.customername as Customer_Name," +
                    " (SELECT COUNT(*) FROM ids_trn_tsanctiondocumentdtls b" +
                    " WHERE a.sanction_gid = b.sanction_gid GROUP BY a.sanction_gid) AS Tagged_Document, a.batchref_no as Sanction_File_Ref_No, " +
                    " a.stampref_no as File_StampRef_No, barcoderef_no as BarCode_No, '-' as CartonBoxRef_No, '-' as Boxed_Date, '-' as Remarks" +
                    " FROM ids_trn_tbatch a" +
                    " LEFT JOIN ocs_mst_tcustomer2sanction b ON a.sanction_gid = b.customer2sanction_gid" +
                    " LEFT JOIN ocs_mst_tcustomer c ON b.customer_gid=c.customer_gid" +
                    " WHERE a.batch_gid NOT IN (SELECT batch_gid FROM ids_trn_tbox2file WHERE 1=1) and (stampref_no<>'' or stampref_no<>null)" +
                    " ORDER BY a.created_date DESC,stampref_no DESC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Batch_List");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "IDAS/BatchReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                {
                    if ((!System.IO.Directory.Exists(values.lspath)))
                        System.IO.Directory.CreateDirectory(values.lspath);
                }
                values.lsname = "Batch_List" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".xlsx";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "IDAS/BatchReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                values.lscloudpath = lscompany_code + "/" + "IDAS/BatchReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 9])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.Green);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "IDAS/BatchReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname, ms);
                ms.Close();

                dt_datatable.Dispose();
                values.lscloudpath = objcmnstorage.EncryptData(values.lscloudpath);
                values.lspath = objcmnstorage.EncryptData(values.lspath);
                values.status = true;
                values.message = "Success";
            }
            catch (Exception ex)
            {
                dt_datatable.Dispose();
                values.message = ex.ToString();
                values.status = false;
            }
        }

        public string dateFormatStandardizer(string sentDate)
        {
            string[] dateArr = sentDate.Split(' ');
            DateTime ldreturnDate = DateTime.ParseExact(dateArr[0], "d/M/yyyy", CultureInfo.InvariantCulture);
            string returnDate = ldreturnDate.ToString("yyyy-MM-dd");
            return returnDate;
        }

        public void DaBatchImportExcel(HttpRequest httpRequest, string employee_gid, string user_gid, MdlbatchSummary values)
        {
            try
            {
                HttpFileCollection httpFileCollection;
                DataTable dt = null;
                string lspath, lsfilePath;
                string project_flag = httpRequest.Form["project_flag"].ToString();

                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                // Create Directory
                lsfilePath = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/IDAS/ImportBatchReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

                if ((!System.IO.Directory.Exists(lsfilePath)))
                    System.IO.Directory.CreateDirectory(lsfilePath);

                httpFileCollection = httpRequest.Files;
                for (int i = 0; i < httpFileCollection.Count; i++)
                {
                    httpPostedFile = httpFileCollection[i];
                }
                string FileExtension = httpPostedFile.FileName;

                string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                string lsfile_gid = msdocument_gid;
                FileExtension = Path.GetExtension(FileExtension).ToLower();
                lsfile_gid = lsfile_gid + FileExtension;

                Stream ls_readStream;
                ls_readStream = httpPostedFile.InputStream;
                MemoryStream ms = new MemoryStream();
                ls_readStream.CopyTo(ms);

                byte[] bytes = ms.ToArray();
                if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                {
                    values.status = false;
                    values.message = "File format is not supported";
                    return;
                }
                //path creation        
                lspath = lsfilePath + "/";
                FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);

                using (ExcelPackage xlPackage = new ExcelPackage(ms))
                {
                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];
                    rowCount = worksheet.Dimension.End.Row;
                }
                file.Close();
                ms.Close();

                objcmnfunctions.uploadFile(lspath, lsfile_gid);

                //Excel To DataTable

                lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";

                excelRange = "A1:K1" + rowCount.ToString();

                dt = objcmnfunctions.ExcelToDataTable(lsfilePath, excelRange);

                //Nullable<DateTime> lsboxed_date;

                foreach (DataRow row in dt.Rows)
                {
                    lscustomer_urn = row["Customer_URN"].ToString();
                    lscustomer_name = row["Customer_Name"].ToString();
                    lstagged_document = row["Tagged_Document"].ToString();
                    lsfileref_no = row["Sanction_File_Ref_No"].ToString();
                    lsstampref_no = row["File_StampRef_No"].ToString();
                    lsbarcode_no = row["BarCode_No"].ToString();
                    lscartonboxref_no = row["CartonBoxRef_No"].ToString();
                    lsboxed_date = row["Boxed_Date"].ToString();
                    lsremarks = row["Remarks"].ToString();

                    if (lsboxed_date.Length > 10)
                    {
                        lsboxed_date = dateFormatStandardizer(lsboxed_date);
                    }
                    else
                    {
                        DateTime lsboxeddate = DateTime.ParseExact(lsboxed_date, "d-M-yyyy", CultureInfo.InvariantCulture);
                        lsboxed_date = lsboxeddate.ToString("yyyy-MM-dd");
                    }

                    msSQL = " SELECT batch_gid FROM ids_trn_tbatch WHERE barcoderef_no='" + lsbarcode_no.Replace("'", "") + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows)
                    {
                        values.status = false;
                        values.message = "File BarCode Ref No Already Exist.";
                        return;
                    }
                    objODBCDataReader.Close();

                    msSQL = " SELECT batch_gid, sanction_gid FROM ids_trn_tbatch WHERE stampref_no='" + lsstampref_no + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows)
                    {
                        values.batch_gid = objODBCDataReader["batch_gid"].ToString();
                        values.sanction_gid = objODBCDataReader["sanction_gid"].ToString();
                    }
                    objODBCDataReader.Close();

                    msSQL = " UPDATE ids_trn_tbatch SET" +
                            " barcoderef_no='" + lsbarcode_no.Replace("'", "") + "'," +
                            " updated_by='" + user_gid + "'," +
                            " updated_date=current_timestamp" +
                            " WHERE sanction_gid='" + values.sanction_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " SELECT cartonbox_gid FROM ids_trn_tcartonbox WHERE stampref_no='" + lscartonboxref_no.Replace("'", "") + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows == false)
                    {
                        msGetGIDRef = objcmnfunctions.GetMasterGID("BOX");
                        msGeTGID = objcmnfunctions.GetMasterGID("BXT");
                        msSQL = " INSERT INTO ids_trn_tcartonbox(" +
                                " cartonbox_gid," +
                                " boxref_no," +
                                " stampref_no," +
                                " cartonbox_date," +
                                " boxbarcoderef_no," +
                                " remarks," +
                                " created_by," +
                                " created_date)" +
                                " VALUES(" +
                                "'" + msGeTGID + "'," +
                                "'" + msGetGIDRef + "'," +
                                "'" + lscartonboxref_no.Replace("'", "") + "'," +
                                "'" + Convert.ToDateTime(lsboxed_date).ToString("yyyy-MM-dd") + "'," +
                                "''," +
                                "'" + lsremarks.Replace("'", "") + "'," +
                                "'" + user_gid + "'," +
                                "current_timestamp)";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    objODBCDataReader.Close();
                                    
                    if(mnResult == 1)
                    {
                        msGetGIDRef = objcmnfunctions.GetMasterGID("BO2F");
                        var lsbatchref_no = objdbconn.GetExecuteScalar("SELECT batchref_no FROM ids_trn_tbatch WHERE batch_gid='" + values.batch_gid + "'");
                        var lssanction_gid = objdbconn.GetExecuteScalar("SELECT sanction_gid FROM ids_trn_tbatch WHERE batch_gid='" + values.batch_gid + "'");
                        var lsbarcoderef_no = objdbconn.GetExecuteScalar("SELECT barcoderef_no FROM ids_trn_tbatch WHERE batch_gid='" + values.batch_gid + "'");
                        msSQL = " INSERT INTO ids_trn_tbox2file(" +
                            " box2file_gid," +
                            " cartonbox_gid," +
                            " batchref_no," +
                            " batch_gid," +
                            " sanction_gid," +
                            " boxbarcoderef_no," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGIDRef + "'," +
                            "'" + msGeTGID + "'," +
                            "'" + lsbatchref_no + "'," +
                            "'" + values.batch_gid + "'," +
                            "'" + lssanction_gid + "'," +
                            "'" + lsbarcoderef_no + "'," +
                            "'" + user_gid + "'," +
                            "current_timestamp)";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt.Dispose();
                if (mnResult == 1)
                {
                    values.status = true;
                    values.message = " Records Uploaded Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured in Uploading Excel Sheet Details";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.ToString();
            }
        }
    }
}