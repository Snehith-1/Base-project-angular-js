using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.rsk.Models;
using System.Configuration;
using ems.storage.Functions;



namespace ems.rsk.DataAccess
{
    public class DaSanction
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();  
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGet_documentGid;
        int mnResult;
        DateTime lssanctiondate;
        double lssanction_amount, lssanction_limit;
        HttpPostedFile httpPostedFile;
        string lscustomer_gid, msGetGidREF, lscompany_code, lscustomer_urn;
        string lspath, lsfilename, lsfilepath;
        string lscustomer2sanction_gid, lsdocument_gid, lsdocument_code, lsdocument_name, lsdocument_flag;


        public bool DaPostSanctionDetails(string employee_gid, sanctiondetails values)

        {

            msSQL = "select customer_urn from ocs_mst_tcustomer where customer_gid='" + values.customer_gid + "'";
            lscustomer_urn = objdbconn.GetExecuteScalar(msSQL);

            msGetGid = objcmnfunctions.GetMasterGID("CU2S");

            msSQL = " Insert into ocs_mst_tcustomer2sanction( " +
                           " customer2sanction_gid," +
                           " sanction_refno," +
                           " sanction_date," +
                           " sanction_amount," +
                           " sanction_limit," +
                           " customer_gid ," +
                           " customer_urn," +
                           " approval_authority," +
                           " sanction_validity," +
                           " sanction_expirydate," +
                           " sanction_reviewdate," +
                           " nature_ofproposal," +
                           " constitution," +
                           " specific_condition," +
                           " authorized_signatory," +
                           " existing_limit," +
                           " revisied_limit," +
                           " escrow_account," +
                           " facility_type," +
                           " tenure_months," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid + "', " +
                           "'" + values.sanction_refno + "'," +
                           "'" + Convert.ToDateTime(values.sanction_date).ToString("yyyy-MM-dd") + "'," +
                           "'" + values.sanction_amount.Replace("'", "\\'") + "'," +
                           "'" + values.sanction_limit.Replace("'", "\\'") + "'," +
                           "'" + values.customer_gid + "'," +
                           "'" + lscustomer_urn + "'," +
                           "'" + values.approval_authority + "'," +
                           "'" + values.sanction_validity + "'," +
                           "'" + Convert.ToDateTime(values.expiry_date).ToString("yyyy-MM-dd") + "'," +
                           "'" + Convert.ToDateTime(values.review_date).ToString("yyyy-MM-dd") + "'," +
                           "'" + values.natureof_proposal + "'," +
                           "'" + values.constitution + "'," +
                           "'" + values.specific_conditions.Replace("'", "\\'") + "'," +
                           "'" + values.authorized_signatory + "', " +
                           "'" + values.revisied_limit.Replace("'", "\\'") + "'," +
                           "'" + values.existing_limit.Replace("'", "\\'") + "'," +
                           "'" + values.escrow_account + "'," +
                           "'" + values.facility_type + "'," +
                           "'" + values.tenure_months + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            for (var i = 0; i < values.documentationname.Count; i++)
            {
                msGet_documentGid = objcmnfunctions.GetMasterGID("CU2DT");

                msSQL = "Insert into ocs_trn_tcustomer2documentation( " +
                       " trndocumentation_gid, " +
                       " sanction_gid," +
                       " mstdocumentation_gid," +
                       " documentation_name," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGet_documentGid + "'," +
                       "'" + msGetGid + "'," +
                       "'" + values.documentationname[i].customer2document_gid + "'," +
                       "'" + values.documentationname[i].documentation_name.Replace("'", "\\'") + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Sanction Details are Added Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public bool DaGetSanctionDtlList(sanctiondetailsList values)
        {

            msSQL = " select a.customer2sanction_gid,b.customer_urn,a.sanction_refno,date_format(a.sanction_date,'%d-%m-%Y') as sanction_date, " +
                   " format((sanction_amount),2) as sanction_amount,a.sanction_limit,b.customername from ocs_mst_tcustomer2sanction a " +
                   " left join ocs_mst_tcustomer b on a.customer_gid = b.customer_gid order by customer2sanction_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_sanctiondtl = new List<sanctiondetails>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    //if (dt["sanction_amount"].ToString() != "")
                    //{
                    //    lssanction_amount = dt["sanction_amount"].ToString();
                    //}
                    //if (dt["sanction_limit"].ToString() != "")
                    //{
                    //    lssanction_limit = Convert.ToDouble(dt["sanction_limit"].ToString();
                    //}
                    get_sanctiondtl.Add(new sanctiondetails
                    {
                        customer2sanction_gid = dt["customer2sanction_gid"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        sanction_date = dt["sanction_date"].ToString(),
                        sanction_amount = dt["sanction_amount"].ToString(),
                        sanction_limit = dt["sanction_limit"].ToString(),
                        customer_name = dt["customername"].ToString(),
                        customer_urn = dt["customer_urn"].ToString()
                    });
                }
                values.sanctiondetails = get_sanctiondtl;
            }
            dt_datatable.Dispose();

            return true;
        }



        public bool DaGetIdasSanctionDocumentList(string customer2sanction_gid, idassanctiondocumentList values)
        {

            msSQL = " SELECT rsksanction_documentgid, customer2sanction_gid,document_flag,file_name,file_path, document_gid, document_code, document_name" +
                    " FROM rsk_mst_tsanctiondocumentdtl" +
                    " WHERE customer2sanction_gid= '" + customer2sanction_gid + "' AND document_from='IDAS'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_sanctiondocument = new List<idassanctiondocument>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    get_sanctiondocument.Add(new idassanctiondocument
                    {
                        customer2sanction_gid = dt["customer2sanction_gid"].ToString(),
                        sanctiondocument_gid = dt["rsksanction_documentgid"].ToString(),
                        document_gid = dt["document_gid"].ToString(),
                        document_code = dt["document_code"].ToString(),
                        document_flag = dt["document_flag"].ToString(),
                        document_name = dt["document_name"].ToString(),
                        file_name = dt["file_name"].ToString(),
                        file_path = objcmnstorage.EncryptData((dt["file_path"].ToString())),
                    });
                }
                values.idassanctiondocument = get_sanctiondocument;
                values.status = true;
                values.message = "Success..!";
            }
            else
            {
                values.status = false;
                values.message = "No Record..!";
            }
            dt_datatable.Dispose();

            return true;
        }
        public bool DaGetRskSanctionDocumentList(string customer2sanction_gid, rsksanctiondocumentList values)
        {

            msSQL = " SELECT rsksanction_documentgid, customer2sanction_gid,file_name,file_path,document_flag, document_gid, document_code, document_name," +
                     " document_remarks FROM rsk_mst_tsanctiondocumentdtl" +
                     " WHERE customer2sanction_gid= '" + customer2sanction_gid + "' AND document_from='RSK'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_sanctiondocument = new List<rsksanctiondocument>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_sanctiondocument.Add(new rsksanctiondocument
                    {
                        customer2sanction_gid = dt["customer2sanction_gid"].ToString(),
                        rsksanction_documentgid = dt["rsksanction_documentgid"].ToString(),
                        documentation_gid = dt["document_gid"].ToString(),
                        documentaion_code = dt["document_code"].ToString(),
                        document_flag = dt["document_flag"].ToString(),
                        documentation_name = dt["document_name"].ToString(),
                        file_name = dt["file_name"].ToString(),
                        file_path= objcmnstorage.EncryptData((dt["file_path"].ToString())),
                        document_remarks = dt["document_remarks"].ToString(),
                    });
                }
                values.rsksanctiondocument = get_sanctiondocument;
                values.status = true;
                values.message = "Success..!";
            }
            else
            {
                values.status = false;
                values.message = "No Record..!";
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetSanctionDelete(string customer2sanction_gid, resultsample values)
        {

            msSQL = "delete from ocs_mst_tcustomer2sanction where customer2sanction_gid = '" + customer2sanction_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Sanction Deleted Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }


        public bool DaPostrsksanctiondocument(HttpRequest httpRequest, uploadrsksanctiondocument values, string employee_gid)
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
            string documentation_gid = string.Empty;
            documentation_gid = HttpContext.Current.Request.Params["documentation_gid"];
            string project_flag = httpRequest.Form["project_flag"].ToString();

            String path = lspath;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "RSK/SanctionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
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
                            values.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "RSK/SanctionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "RSK/SanctionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;


                        msSQL = " select sanction_gid,documentation_gid,documentation_name from ocs_tmp_tcustomer2documentation " +
                                " where documentation_gid = '" + documentation_gid + "' ";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lscustomer2sanction_gid = objODBCDatareader["sanction_gid"].ToString();
                            lsdocument_gid = objODBCDatareader["documentation_gid"].ToString();
                            lsdocument_name = objODBCDatareader["documentation_name"].ToString();


                            msGet_documentGid = objcmnfunctions.GetMasterGID("TMRI");

                            msSQL = "Insert into rsk_tmp_tsanctiondocumentdtl( " +
                                   " tmpsanction_documentgid, " +
                                   " sanction_gid," +
                                   " document_gid," +
                                   " document_name," +
                                   " file_name," +
                                   " file_path," +
                                   " document_flag," +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + msGet_documentGid + "'," +
                                   "'" + lscustomer2sanction_gid + "'," +
                                   "'" + lsdocument_gid + "'," +
                                   "'" + lsdocument_name + "' ," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + "'," +
                                   "'Y'," +
                                   "'" + employee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = "update ocs_tmp_tcustomer2documentation set upload_flag='Y'  where documentation_gid = '" + values.documentation_gid + "' ";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                        }
                        objODBCDatareader.Close();
                    }
                }
            }
            catch
            {

            }
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Document Uploaded Successfully..!";
                return true;

            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;

            }
        }
        public bool DapostUploadidasSanctionDocument(HttpRequest httpRequest, uploadidassanctiondocument values, string employee_gid)
        {
            HttpFileCollection httpFileCollection;

            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            //MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string lscompany_code = string.Empty;

            Stream ls_readStream;

            lsdocument_gid = HttpContext.Current.Request.Params["document_gid"];
            string project_flag = httpRequest.Form["project_flag"].ToString();
            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "RSK/SanctionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(lspath)))
                    System.IO.Directory.CreateDirectory(lspath);
            }

            try

            {
                string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                if (httpRequest.Files.Count > 0)
                {

                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {

                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        //string lsfile_gid = msdocument_gid + FileExtension;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        ls_readStream = httpPostedFile.InputStream;
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            values.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "RSK/SanctionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "RSK/SanctionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;

                        msSQL = " UPDATE rsk_mst_tsanctiondocumentdtl SET" +
                                " file_path='" + lspath + "'," +
                                " file_name='" + httpPostedFile.FileName + "'," +
                                " document_flag='Y'," +
                                " updated_by='" + employee_gid + "'," +
                                " updated_date=current_timestamp" +
                                " WHERE rsksanction_documentgid='" + lsdocument_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
            }
            catch(Exception e)
            {

            }
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Document Uploaded Successfully..!";
                return true;

            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;

            }
        }

        public bool DaPostUploadSanctionDocument(HttpRequest httpRequest, uploadidassanctiondocument values, string employee_gid)
        {
            HttpFileCollection httpFileCollection;

            string lsfilepath = string.Empty;
            string customer2sanction_gid = string.Empty;
            //MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string lscompany_code = string.Empty;


            customer2sanction_gid = HttpContext.Current.Request.Params["customer2sanction_gid"];
            string project_flag = httpRequest.Form["project_flag"].ToString();
            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "RSK/SanctionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(lspath)))
                    System.IO.Directory.CreateDirectory(lspath);
            }
            string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
            try

            {

                if (httpRequest.Files.Count > 0)
                {

                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {

                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        //string lsfile_gid = msdocument_gid + FileExtension;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        //ls_readStream = httpPostedFile.InputStream;
                        //ls_readStream.CopyTo(ms);
                        Stream ls_readStream;
                        ls_readStream = httpPostedFile.InputStream;
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);


                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            values.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "RSK/SanctionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "RSK/SanctionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;

                        msGet_documentGid = objcmnfunctions.GetMasterGID("TMRI");

                        msSQL = "Insert into rsk_tmp_tsanctiondocumentdtl( " +
                                   " tmpsanction_documentgid, " +
                                   " customer2sanction_gid," +
                                   " file_name," +
                                   " file_path," +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + msGet_documentGid + "'," +
                                   "'" + customer2sanction_gid + "'," +
                                   "'" + httpPostedFile.FileName + "'," +
                                   "'" + lspath + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        values.file_name = httpPostedFile.FileName;
                    }

                    msSQL = " select tmpsanction_documentgid,customer2sanction_gid,file_name,file_path from " +
                            " rsk_tmp_tsanctiondocumentdtl where customer2sanction_gid='" + customer2sanction_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getcamdocument_list = new List<Sanctiondoc_upload>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getcamdocument_list.Add(new Sanctiondoc_upload
                            {
                                tmpsanction_documentgid = dt["tmpsanction_documentgid"].ToString(),
                                customer2sanction_gid = dt["customer2sanction_gid"].ToString(),
                                file_path = objcmnstorage.EncryptData(dt["file_path"].ToString()),
                                file_name = dt["file_name"].ToString(),

                            });
                            values.Sanctiondoc_upload = getcamdocument_list;
                        }
                    }
                    dt_datatable.Dispose();
                }
            }
            catch(Exception ex)
            {

            }
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Document Uploaded Successfully..!";
                return true;

            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;

            }
        }
        public bool DaPostUploadDocDelete(string document_gid, string user_gid, result objResult)
        {
            msGetGidREF = objcmnfunctions.GetMasterGID("RSKH");
            msSQL = " INSERT INTO rsk_mst_tdocumentdtlhistory(" +
                  " documentdtlhistory_gid,rsksanction_documentgid,customer2sanction_gid," +
                  " document_gid,file_name,file_path,created_by,created_date)" +
                  " SELECT '" + msGetGidREF + "','" + document_gid + "',customer2sanction_gid,document_gid,file_name,file_path," +
                  " '" + user_gid + "',current_timestamp" +
                  " FROM rsk_mst_tsanctiondocumentdtl" +
                  " WHERE rsksanction_documentgid='" + document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from rsk_mst_tsanctiondocumentdtl WHERE rsksanction_documentgid='" + document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                objResult.status = true;
                objResult.message = "Document Deleted Successfully..!";
                return true;
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured..!";
                return false;
            }
        }
        public bool DaPostidasrsksanctiondocument(string employee_gid, uploadidassanctiondocument values)
        {
            msSQL = " select a.sanction_gid,a.document_gid,a.document_name,a.document_flag,a.file_name,a.file_path, " +
                    " b.customer_gid" +
                    " from rsk_tmp_tsanctiondocumentdtl a " +
                    " left join ocs_mst_tcustomer2sanction b on a.sanction_gid= b.customer2sanction_gid " +
                    " where a.sanction_gid = '" + values.customer2sanction_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    lscustomer2sanction_gid = dr_datarow["sanction_gid"].ToString();
                    lsdocument_gid = dr_datarow["document_gid"].ToString();
                    lsdocument_name = dr_datarow["document_name"].ToString();
                    lsdocument_flag = dr_datarow["document_flag"].ToString();
                    lscustomer_gid = dr_datarow["customer_gid"].ToString();
                    lsfilename = dr_datarow["file_name"].ToString();
                    lsfilepath = objcmnstorage.EncryptData(dr_datarow["file_path"].ToString());

                    msGet_documentGid = objcmnfunctions.GetMasterGID("RS2D");
                    if (lsdocument_flag == "N")
                    {
                        msSQL = "select document_code from ids_trn_tsanctiondocumentdtls where document_gid='" + lsdocument_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsdocument_code = objODBCDatareader["document_code"].ToString();


                            msSQL = "Insert into rsk_mst_tsanctiondocumentdtl( " +
                                   " rsksanction_documentgid, " +
                                   " customer2sanction_gid," +
                                   " document_gid," +
                                   " document_code," +
                                   " document_name," +
                                   " file_name, " +
                                   " file_path, " +
                                   " customer_gid," +
                                   " document_flag," +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + msGet_documentGid + "'," +
                                   "'" + lscustomer2sanction_gid + "'," +
                                   "'" + lsdocument_gid + "'," +
                                   "'" + lsdocument_code + "' , " +
                                   "'" + lsdocument_name + "' ," +
                                   "'" + lsfilename + "' , " +
                                   "'" + lsfilepath + "' ," +
                                   "'" + lscustomer_gid + "' ," +
                                   "'" + lsdocument_flag + "' ," +
                                   "'" + employee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                            msSQL = " update ids_trn_tsanctiondocumentdtls set upload_flag='Z' where document_gid = '" + lsdocument_gid + "'  " +
                                     " and sanction_gid='" + values.customer2sanction_gid + "' ";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        objODBCDatareader.Close();
                    }
                    if (lsdocument_flag == "Y")
                    {
                        msSQL = "select documentation_refno from ocs_mst_tcustomer2documentation where customer2document_gid='" + lsdocument_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsdocument_code = objODBCDatareader["documentation_refno"].ToString();

                            msSQL = "Insert into rsk_mst_tsanctiondocumentdtl( " +
                                   " rsksanction_documentgid, " +
                                   " customer2sanction_gid," +
                                   " document_gid," +
                                   " document_code," +
                                   " document_name," +
                                   " file_name, " +
                                   " file_path, " +
                                   " customer_gid," +
                                   " document_flag," +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + msGet_documentGid + "'," +
                                   "'" + lscustomer2sanction_gid + "'," +
                                   "'" + lsdocument_gid + "'," +
                                   "'" + lsdocument_code + "' , " +
                                   "'" + lsdocument_name + "' ," +
                                   "'" + lsfilename + "' , " +
                                   "'" + lsfilepath + "' ," +
                                   "'" + lscustomer_gid + "' ," +
                                   "'" + lsdocument_flag + "' ," +
                                   "'" + employee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " update ocs_tmp_tcustomer2documentation set upload_flag='Z'  " +
                                    " where documentation_gid = '" + lsdocument_gid + "' and " +
                                    " sanction_gid='" + values.customer2sanction_gid + "'  ";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        objODBCDatareader.Close();
                    }

                }
                dt_datatable.Dispose();
                if (mnResult == 1)
                {
                    msSQL = "delete from rsk_tmp_tsanctiondocumentdtl where sanction_gid = '" + values.customer2sanction_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Sanction documents are Added Successfully..!";
                    return true;
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                    return false;
                }
            }
            else
            {
                values.status = false;
                values.message = "No Documents Selected..!";
                return false;
            }

        }


        public void DaPostsanctiondocument(string user_gid, sanctiondcoument values)
        {
            try
            {
                string tmpsanction_documentgid, file_name, file_path;

                msSQL = " select tmpsanction_documentgid,customer2sanction_gid,file_name,file_path " + //document_flag, document_gid, document_name
                    " from rsk_tmp_tsanctiondocumentdtl " +
                    " where customer2sanction_gid = '" + values.customer2sanction_gid + "' ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {

                        tmpsanction_documentgid = dr_datarow["tmpsanction_documentgid"].ToString();
                        file_name = dr_datarow["file_name"].ToString();
                        file_path = dr_datarow["file_path"].ToString();


                        msSQL = "SELECT documentation_name FROM ocs_mst_tcustomer2documentation WHERE customer2document_gid = '" + values.customer2document_gid + "' ";
                        lsdocument_name = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "SELECT documentation_refno FROM ocs_mst_tcustomer2documentation WHERE customer2document_gid = '" + values.customer2document_gid + "' ";
                        lsdocument_code = objdbconn.GetExecuteScalar(msSQL);

                        msGet_documentGid = objcmnfunctions.GetMasterGID("RS2D");

                        msSQL = " INSERT INTO rsk_mst_tsanctiondocumentdtl" +
                                   " (" +
                                   " rsksanction_documentgid," +
                                   " customer2sanction_gid," +
                                   " customer_gid," +
                                   " document_gid," +
                                   " document_code," +
                                   " document_name," +
                                   " document_remarks," +
                                   " created_by," +
                                   " created_date," +
                                   " document_from" +
                                   " )" +
                                   " values(" +
                                   "'" + msGet_documentGid + "'," +
                                   "'" + values.customer2sanction_gid + "'," +
                                   "'" + values.customer_gid + "'," +
                                   "'" + values.customer2document_gid + "'," +
                                   "'" + lsdocument_code + "'," +
                                   "'" + lsdocument_name.Replace("'", "\\'") + "'," +
                                   "'" + values.document_remarks.Replace("'", "\\'") + "'," +
                                   "'" + user_gid + "'," +
                                   "current_timestamp," +
                                   "'RSK')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        //msSQL = " select tmpsanction_documentgid,file_path,file_name from rsk_tmp_tsanctiondocumentdtl " +
                        //        " where customer2sanction_gid='" + values.customer2sanction_gid + "' and created_by='" + user_gid + "'";
                        //objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        //if (objODBCDatareader.HasRows == true)
                        //
                        msSQL = " UPDATE rsk_mst_tsanctiondocumentdtl SET" +
                                " file_path='" + file_path + "'," +
                                " file_name='" + file_name + "'," +
                                " document_flag='Y'," +
                                " updated_by='" + user_gid + "'," +
                                " updated_date=current_timestamp" +
                                " WHERE rsksanction_documentgid='" + msGet_documentGid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            msSQL = "delete from rsk_tmp_tsanctiondocumentdtl where tmpsanction_documentgid='" + tmpsanction_documentgid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                        //objODBCDatareader.Close();

                    }
                    dt_datatable.Dispose();

                }// End the Statement
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Sanction Documents are Added Successfully..!";
                    //return true;
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                    //return false;
                }

            }
            catch (Exception ex)
            {

            }

        }

        public bool DaGetSanctionDetails(string customer2sanction_gid, sanctiondetails values)
        {

            msSQL = " select a.customer2sanction_gid,b.customer_urn,a.sanction_refno,date_format(a.sanction_date,'%Y-%m-%d') as sanction_date, " +
                    " a.approval_authority,a.sanction_validity,a.sanction_expirydate,a.sanction_reviewdate,a.nature_ofproposal,a.facility_type, " +
                    " a.constitution,a.specific_condition,concat(d.user_firstname,' ',d.user_lastname,' / ',d.user_code) as authorizedsignatoryname,a.authorized_signatory, " +
                    " a.existing_limit,a.revisied_limit,a.escrow_account,a.tenure_months," +
                   " format(a.sanction_amount,2) as sanction_amount,a.sanction_limit,b.customername,b.customer_gid from ocs_mst_tcustomer2sanction a " +
                   " left join ocs_mst_tcustomer b on a.customer_gid = b.customer_gid " +
                   " left join adm_mst_tuser d on d.user_gid=a.authorized_signatory " +
                   " where customer2sanction_gid='" + customer2sanction_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.sanction_refno = objODBCDatareader["sanction_refno"].ToString();
                values.sanctionDate = Convert.ToDateTime(objODBCDatareader["sanction_date"].ToString());
                values.sanction_date = objODBCDatareader["sanction_date"].ToString();
                if (objODBCDatareader["sanction_amount"].ToString() != "")
                {
                    values.sanction_amount = objODBCDatareader["sanction_amount"].ToString();
                }
                if (objODBCDatareader["sanction_limit"].ToString() != "")
                {
                    values.sanction_limit = objODBCDatareader["sanction_limit"].ToString();
                }
                if (objODBCDatareader["existing_limit"].ToString() != "")
                {
                    values.existing_limit = objODBCDatareader["existing_limit"].ToString();
                }
                if (objODBCDatareader["revisied_limit"].ToString() != "")
                {
                    values.revisied_limit = objODBCDatareader["revisied_limit"].ToString();
                }
                values.customer_gid = objODBCDatareader["customer_gid"].ToString();
                values.approval_authority = objODBCDatareader["approval_authority"].ToString();
                values.sanction_validity = objODBCDatareader["sanction_validity"].ToString();
                values.expiry_date = objODBCDatareader["sanction_expirydate"].ToString();
                values.review_date = objODBCDatareader["sanction_reviewdate"].ToString();
                values.natureof_proposal = objODBCDatareader["nature_ofproposal"].ToString();
                values.constitution = objODBCDatareader["constitution"].ToString();
                values.specific_conditions = objODBCDatareader["specific_condition"].ToString();
                values.authorized_signatory = objODBCDatareader["authorized_signatory"].ToString();
                values.escrow_account = objODBCDatareader["escrow_account"].ToString();
                values.facility_type = objODBCDatareader["facility_type"].ToString();
                values.authorizedsignatoryname = objODBCDatareader["authorizedsignatoryname"].ToString();
                values.customer_name = objODBCDatareader["customername"].ToString();
                values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                values.tenure_months = objODBCDatareader["tenure_months"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select a.documentation_name,a.mstdocumentation_gid,b.documentation_refno,b.documentation_type, " +
                    " concat(b.documentation_refno,' / ',b.documentation_type) as documentrefname from ocs_trn_tcustomer2documentation a " +
                    " left join ocs_mst_tcustomer2documentation b on a.mstdocumentation_gid = b.customer2document_gid " +
                    " where sanction_gid ='" + customer2sanction_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocument_name = new List<documentationname>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocument_name.Add(new documentationname
                    {
                        customer2document_gid = dt["mstdocumentation_gid"].ToString(),
                        documentation_refno = dt["documentation_refno"].ToString(),
                        documentation_name = dt["documentation_name"].ToString(),
                        documentation_type = dt["documentation_type"].ToString(),
                        documentrefname = dt["documentrefname"].ToString()
                    });
                }
                values.documentationname = getdocument_name;
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaPostSanctionUpdate(string employee_gid, sanctiondetails values)
        {

            msSQL = " update ocs_mst_tcustomer2sanction set " +
                    " sanction_refno='" + values.sanction_refno + "'," +
                    " sanction_date='" + Convert.ToDateTime(values.sanction_date).ToString("yyyy-MM-dd") + "'," +
                    " sanction_amount ='" + values.sanction_amount.Replace("'", "\\'") + "'," +
                    " sanction_limit ='" + values.sanction_limit.Replace("'", "\\'") + "'," +
                    " customer_gid='" + values.customer_gid + "'," +
                    " sanction_validity= '" + values.sanction_validity + "'," +
                    " sanction_expirydate= '" + Convert.ToDateTime(values.expiry_date).ToString("yyyy-MM-dd") + "'," +
                    " sanction_reviewdate= '" + Convert.ToDateTime(values.review_date).ToString("yyyy-MM-dd") + "'," +
                    " approval_authority= '" + values.approval_authority + "'," +
                    " nature_ofproposal= '" + values.natureof_proposal + "'," +
                    " constitution= '" + values.constitution + "'," +
                    " authorized_signatory= '" + values.authorized_signatory + "'," +
                    " revisied_limit= '" + values.revisied_limit.Replace("'", "\\'") + "'," +
                    " existing_limit= '" + values.existing_limit.Replace("'", "\\'") + "'," +
                    " escrow_account= '" + values.escrow_account + "'," +
                    " facility_type= '" + values.facility_type + "'," +
                    " tenure_months='" + values.tenure_months.Replace("'", "\\'") + "'," +
                    " specific_condition= '" + values.specific_conditions.Replace("'", "\\'") + "'," +
                    " updated_by ='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where customer2sanction_gid='" + values.customer2sanction_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_trn_tcustomer2documentation where sanction_gid='" + values.customer2sanction_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            for (var i = 0; i < values.documentationname.Count; i++)
            {
                msGet_documentGid = objcmnfunctions.GetMasterGID("CU2DT");

                msSQL = "Insert into ocs_trn_tcustomer2documentation( " +
                       " trndocumentation_gid, " +
                       " sanction_gid," +
                       " mstdocumentation_gid," +
                       " documentation_name," +
                       " updated_by," +
                       " updated_date)" +
                       " values(" +
                       "'" + msGet_documentGid + "'," +
                       "'" + values.customer2sanction_gid + "'," +
                       "'" + values.documentationname[i].customer2document_gid + "'," +
                       "'" + values.documentationname[i].documentation_name + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Sanction Details are Updated Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }

        }

        public bool DaPostExcelUpload(HttpRequest httpRequest, string employee_gid, resultsample values)
        {

            HttpFileCollection httpFileCollection;

            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            //MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;
            string document_name = httpRequest.Form["document_name"];
            string project_flag = httpRequest.Form["project_flag"].ToString();
            String path = lspath;

            msSQL = "SELECT company_code from adm_mst_tcompany where 1=1";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Read();
                lscompany_code = objODBCDatareader["company_code"].ToString();
            }
            objODBCDatareader.Close();
            path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "Master/SanctionExcelImport/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
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
                        string lsfile_gid = msdocument_gid + FileExtension;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        if ((FileExtension == ".csv"))
                        {
                            ls_readStream = httpPostedFile.InputStream;
                            MemoryStream ms = new MemoryStream();
                            ls_readStream.CopyTo(ms);

                            byte[] bytes = ms.ToArray();

                            if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                            {
                                values.message = "File format is not supported";
                                values.status = false;
                                return false;
                            }

                            //CopyStream(ms, ls_readStream);
                            lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/SanctionExcelImport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + lsfile_gid;
                            FileStream file = new FileStream(lspath, FileMode.Create, FileAccess.Write);
                            ms.WriteTo(file);
                            file.Close();
                            ms.Close();
                            msGetGid = objcmnfunctions.GetMasterGID("SAIM");
                            msSQL = " insert into ocs_trn_tsancimportfiledtl( " +
                                    " sancimport_gid," +
                                    " file_path," +
                                    " file_name," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + lspath + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            string lsreppath = lspath.Replace('\\', '/') + "/" + lsfile_gid;

                            msSQL = " LOAD DATA INFILE '" + lsreppath + "'" +
                                    " INTO TABLE ocs_tmp_tsanctionimport" +
                                    " FIELDS TERMINATED BY ','" +
                                    " ENCLOSED BY '\"' " +
                                    " LINES TERMINATED BY '\r\n'" +
                                    " IGNORE 1 LINES" +
                                    " (@CustomerURN, @SanctionRefNo, @ApprovalAuthority, @Vertical, @LoanType, @CCApprovalDate, @SanctionDate, @NatureofProposal, @ClassificationofMSME," +
                                    " @ValidityinMonths, @ExpiryDate, @ReviewDate, @CustomerName, @EalierSanctionReference, @Constitution, @Address, @State, @Pincode, @ContactNumber," +
                                    " @EmailID, @ContactPerson, @RMName, @RMPhoneNo, @RMEmailID, @SanctionLetterAuthorizedSignatory, @CreditManager, @TypeofFacility," +
                                    " @ExistingLimit, @AdditionalProposedLimit, @OverallLimit, @Purpose, @TenureMonths, @RepaymentPrincipal," +
                                    " @RepaymentInterest, @PrimarySecurity, @CollateralSecurity, @PersonalGuarantee, @SecurityChequeBankName," +
                                    " @SecurityChequeAccountNumber, @Margin, @RateofInterest, @PenalInterest, @ProcessingFee, @BankandChequeNo," +
                                    " @ChequeRealizationDate, @DocumentationChargeClientVisitCharge," +
                                    " @GSTNumber,@DocumentationList, @ModeofOperations, @SpecificConditions, @DateofReceiptofDocsforVetting, @NACHForm," +
                                    " @EscrowAccount, @VirtualAccountNo, @Nameofthebuyers, @StatusofBAL, @ROCApplicable, @ROCStatus, @CERSAIStatus, @NeSLStatus," +
                                    " @PredisbursementDeferal, @DateofDeviation, @StatusPredisbursementDeferal, @Postdisbursementcovanent, @StatusPostdisbursementcovanent," +
                                    " @DateofReleaseOrder, @ROissuingTotalamount, @CasesvettedbytheCAD, @OriginalDocsreceivedatHO, @ScannedanduploadedinDrive," +
                                    " @Monitoringvisit, @BankStatement, @AuditedFinancials, @StockStatement, @PurchaseStatement, @SalesStatement, @DebtorsStatement," +
                                    " @ProvisionalFinancialwithGST, @ROC30daysfromSLonetime, @NoLiabilityCertificatefromFIBankOnetime, @BuyerConfirmationLetter," +
                                    " @CopyofWarehouseReceipt30daysfromSLonetime, @Insurance30daysfromSLonetime, @LoandisbursementdetailstoFarmermember30daysfromSLonetime," +
                                    " @Others)" +
                                    " set" +
                                    " customer_urn = @CustomerURN, sanction_refno = @SanctionRefNo, approval_authority = @ApprovalAuthority, vertical = @Vertical, loan_type = @LoanType," +
                                    " CCapproval_date = @CCApprovalDate, sanction_date = @SanctionDate, nature_ofproposal = @NatureofProposal, classification_MSME = @ClassificationofMSME," +
                                    " sanction_validity = @ValidityinMonths, sanction_expirydate = @ExpiryDate, sanction_reviewdate = @ReviewDate, customer_name = @CustomerName, earlier_sanctionrefno = @EalierSanctionReference," +
                                    " constitution = @Constitution, address = @Address, state = @State, pincode = @Pincode, contact_number = @ContactNumber," +
                                    " email_id = @EmailID, contact_person = @ContactPerson, rm_name = @RMName, rm_phoneno = @RMPhoneNo, rm_emailid = @RMEmailID," +
                                    " authorized_signatory = @SanctionLetterAuthorizedSignatory, credit_manager = @CreditManager, facility_type = @TypeofFacility," +
                                    " existing_limit = @ExistingLimit, additional_proposedlimit = @AdditionalProposedLimit, overall_limit = @OverallLimit," +
                                    " purpose = @Purpose, tenure_months = @TenureMonths, repayment_principal = @RepaymentPrincipal," +
                                    " repayment_interest = @RepaymentInterest, primary_security = @PrimarySecurity, collateral_security = @CollateralSecurity," +
                                    " personal_guarantee = @PersonalGuarantee, securitycheque_bankname = @SecurityChequeBankName," +
                                    " securitycheque_accountnumber = @SecurityChequeAccountNumber, margin = @Margin, rateof_interest = @RateofInterest," +
                                    " penal_interest = @PenalInterest, processing_fee = @ProcessingFee, bankand_chequeno = @BankandChequeNo," +
                                    " cheque_realizationdate = @ChequeRealizationDate, documentation_clientvisitcharge = @DocumentationChargeClientVisitCharge," +
                                    " GST_number = @GSTNumber,documentation_list=@DocumentationList ,modeof_operations = @ModeofOperations, specific_condition = @SpecificConditions," +
                                    " dateof_receiptDocsVetting = @DateofReceiptofDocsforVetting, NACH_form = @NACHForm," +
                                    " escrow_account = @EscrowAccount, virtual_accountno = @VirtualAccountNo, nameofthe_buyers = @Nameofthebuyers, status_ofBAL = @StatusofBAL," +
                                    " roc_applicable = @ROCApplicable, roc_status = @ROCStatus, cersai_status = @CERSAIStatus," +
                                    " nesl_status = @NeSLStatus, predisbursement_deferal = @PredisbursementDeferal, dateof_deviation = @DateofDeviation," +
                                    " statuspre_disbursementdeferal = @StatusPredisbursementDeferal, postdisbursement_covanent = @Postdisbursementcovanent," +
                                    " statuspost_disbursementcovanent = @StatusPostdisbursementcovanent," +
                                    " dateof_releaseorder = @DateofReleaseOrder, roissuing_totalamount = @ROissuingTotalamount, casesvetted_bycad = @CasesvettedbytheCAD," +
                                    " originaldocs_receivedHO = @OriginalDocsreceivedatHO," +
                                    " scanneduploaded_Drive = @ScannedanduploadedinDrive, monitoring_visit = @Monitoringvisit, bank_statement = @BankStatement," +
                                    " audited_financials = @AuditedFinancials, stock_statement = @StockStatement, purchase_statement = @PurchaseStatement," +
                                    " sales_statement = @SalesStatement, debtors_statement = @DebtorsStatement," +
                                    " provisionalfinancial_gst = @ProvisionalFinancialwithGST," +
                                    " roc_30daysfromSLonetime = @ROC30daysfromSLonetime, noliability_certificate = @NoLiabilityCertificatefromFIBankOnetime," +
                                    " buyerconfirmation_letter = @BuyerConfirmationLetter, copyof_warehousereceipt = @CopyofWarehouseReceipt30daysfromSLonetime," +
                                    " insurance_30daysfromSLonetime = @Insurance30daysfromSLonetime, loandisbursement_dtlfarmermember = @LoandisbursementdetailstoFarmermember30daysfromSLonetime," +
                                    " others = @Others";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (mnResult == 1)
                            {
                                msSQL = " insert into ocs_mst_tcustomer2sanction(customer2sanction_gid, customer_urn, sanction_refno, approval_authority, vertical, " +
                                        " loan_type, sanction_validity, sanction_expirydate, sanction_reviewdate, customer_name, earlier_sanctionrefno, constitution, " +
                                        " address, state, pincode, contact_number, email_id, contact_person, rm_name, rm_phoneno, rm_emailid, " +
                                        " authorized_signatory, credit_manager, facility_type, existing_limit, additional_proposedlimit, overall_limit, purpose, " +
                                        " tenure_months, repayment_principal, repayment_interest, primary_security, collateral_security, personal_guarantee, " +
                                        " securitycheque_bankname, securitycheque_accountnumber, margin, rateof_interest, penal_interest, " +
                                        " processing_fee, bankand_chequeno, cheque_realizationdate, documentation_clientvisitcharge, GST_number,documentation_list, " +
                                        " modeof_operations, specific_condition, dateof_receiptDocsVetting, NACH_form, escrow_account, " +
                                        " virtual_accountno, nameofthe_buyers, status_ofBAL, roc_applicable, roc_status, cersai_status, " +
                                        " nesl_status, predisbursement_deferal, dateof_deviation, statuspre_disbursementdeferal, postdisbursement_covanent, " +
                                        " statuspost_disbursementcovanent, dateof_releaseorder, roissuing_totalamount, casesvetted_bycad, originaldocs_receivedHO, " +
                                        " scanneduploaded_Drive, monitoring_visit, bank_statement, audited_financials, stock_statement, purchase_statement, " +
                                        " sales_statement, debtors_statement, provisionalfinancial_gst, roc_30daysfromSLonetime, noliability_certificate, " +
                                        " buyerconfirmation_letter, copyof_warehousereceipt, insurance_30daysfromSLonetime, loandisbursement_dtlfarmermember, " +
                                        " others, created_by, created_date) " +
                                        " (select customer2sanction_gid, customer_urn, sanction_refno, approval_authority, vertical, " +
                                        " loan_type, sanction_validity, sanction_expirydate, sanction_reviewdate, customer_name, earlier_sanctionrefno, constitution, " +
                                        " address, state, pincode, contact_number, email_id, contact_person, rm_name, rm_phoneno, rm_emailid, " +
                                        " authorized_signatory, credit_manager, facility_type, existing_limit, additional_proposedlimit, overall_limit, purpose, " +
                                        " tenure_months, repayment_principal, repayment_interest, primary_security, collateral_security, personal_guarantee, " +
                                        " securitycheque_bankname, securitycheque_accountnumber, margin, rateof_interest, penal_interest, " +
                                        " processing_fee, bankand_chequeno, cheque_realizationdate, documentation_clientvisitcharge, GST_number,documentation_list, " +
                                        " modeof_operations, specific_condition, dateof_receiptDocsVetting, NACH_form, escrow_account, " +
                                        " virtual_accountno, nameofthe_buyers, status_ofBAL, roc_applicable, roc_status, cersai_status," +
                                        " nesl_status, predisbursement_deferal, dateof_deviation, statuspre_disbursementdeferal, postdisbursement_covanent, " +
                                        " statuspost_disbursementcovanent, dateof_releaseorder, roissuing_totalamount, casesvetted_bycad, originaldocs_receivedHO, " +
                                        " scanneduploaded_Drive, monitoring_visit, bank_statement, audited_financials, stock_statement, purchase_statement, " +
                                        " sales_statement, debtors_statement, provisionalfinancial_gst, roc_30daysfromSLonetime, noliability_certificate, " +
                                        " buyerconfirmation_letter, copyof_warehousereceipt, insurance_30daysfromSLonetime, loandisbursement_dtlfarmermember, " +
                                        " others, created_by, created_date from ocs_tmp_tsanctionimport where customer_urn<>'')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                if (mnResult == 1)
                                {
                                    msSQL = "delete from ocs_tmp_tsanctionimport";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    values.message = "Sanction Details are Inserted Successfully..!";
                                    values.status = true;
                                    return true;
                                }
                                else
                                {
                                    values.message = "Check the Details..!";
                                    values.status = false;
                                    return false;
                                }
                            }
                            else
                            {
                                values.message = "Check the Details..!";
                                values.status = false;
                                return false;
                            }

                        }
                        else
                        {
                            values.message = "File Format is not Supported..!";
                            values.status = false;
                            return false;
                        }

                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.ToString();
                return false;
            }



            //for (var i = 0; i < values.Count; i++)
            //{
            //    msSQL = "select customer_gid from ocs_mst_tcustomer where customername='" + values[i].customer_name + "'";
            //    objODBCDatareader = objdbconn.GetDataReader(msSQL);
            //    if (objODBCDatareader.HasRows)
            //    {
            //        lscustomer_gid = objODBCDatareader["customer_gid"].ToString();
            //        objODBCDatareader.Close();
            //    }
            //    else
            //    {

            //        msGetGid = objcmnfunctions.GetMasterGID("CRMS");
            //        msGetGidREF = objcmnfunctions.GetMasterGID("CC");
            //        msSQL = " insert into ocs_mst_tcustomer(" +
            //                " customer_gid," +
            //                " customer_code," +
            //                " customername," +
            //                " contactperson," +
            //                " customer_urn," +
            //                " mobileno," +
            //                " contact_no," +
            //                " email," +
            //                " address," +
            //                " address2," +
            //                " region," +
            //                " vertical_gid," +
            //                " vertical_code," +
            //                " state," +
            //                " state_gid," +
            //                " postalcode," +
            //                " country," +
            //                " tomail_text," +
            //                " ccmail_text," +
            //                " zonal_head," +
            //                " business_head," +
            //                " relationship_manager," +
            //                " cluster_manager_gid," +
            //                " creditmanager_gid," +
            //                " zonal_name," +
            //                " businesshead_name," +
            //                " relationshipmgmt_name," +
            //                " creditmgmt_name," +
            //                " cluster_manager_name," +
            //                " created_by," +
            //                " created_date)" +
            //                " values(" +
            //                "'" + msGetGid + "'," +
            //                "'" + msGetGidREF + "'," +
            //                "'" + values[i].customername + "'," +
            //                "'" + values[i].contactperson + "'," +
            //                "'" + values[i].customer_urn + "'," +
            //                "'" + values[i].mobileno + "'," +
            //                "'" + values[i].contactnumber + "'," +
            //                "'" + values[i].email + "',";
            //        if (values[i].address1 == null)
            //        {
            //            msSQL += "'',";

            //        }
            //        else
            //        {
            //            msSQL += "'" + values[i].address1.Replace("'", "") + "',";
            //        }
            //        if (values[i].address2 == null)
            //        {
            //            msSQL += "'',";

            //        }
            //        else
            //        {
            //            msSQL += "'" + values[i].address2.Replace("'", "") + "',";
            //        }

            //        msSQL += "'" + values[i].region + "'," +
            //                 "'" + values[i].vertical_gid + "'," +
            //                 "'" + values[i].vertical_code.Replace("'", "").Replace("\n", "") + "'," +
            //                 "'" + values[i].state.Replace("    ", "").Replace("\n", "") + "'," +
            //                 "'" + values[i].state_gid + "'," +
            //                 "'" + values[i].postalcode + "'," +
            //                 "'" + values[i].country + "'," +
            //                 "'" + values[i].tomail + "'," +
            //                 "'" + values[i].ccmail + "'," +
            //                 "'" + values[i].zonalGid + "'," +
            //                 "'" + values[i].businessHeadGid + "'," +
            //                 "'" + values[i].relationshipMgmtGid + "'," +
            //                 "'" + values[i].clustermanagerGid + "'," +
            //                 "'" + values[i].creditmanagerGid + "'," +
            //                 "'" + values[i].zonal_name.Replace("    ", "").Replace("\n", "") + "'," +
            //                 "'" + values[i].businesshead_name.Replace("    ", "").Replace("\n", "") + "'," +
            //                 "'" + values[i].relationshipmgmt_name.Replace("    ", "").Replace("\n", "") + "'," +
            //                 "'" + values[i].creditmanager_name.Replace("    ", "").Replace("\n", "") + "'," +
            //                 "'" + values[i].cluster_manager_name.Replace("    ", "").Replace("\n", "") + "'," +
            //                 "'" + employee_gid + "'," +
            //                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //    }

            //    msGetGid = objcmnfunctions.GetMasterGID("CU2S");

            //    msSQL = " Insert into ocs_mst_tcustomer2sanction( " +
            //                 " customer2sanction_gid," +
            //                 " sanction_refno," +
            //                 " sanction_date," +
            //                 " sanction_amount," +
            //                 " sanction_limit," +
            //                 " customer_gid, " +
            //                 " customer_name ," +
            //                 " approval_authority," +
            //                 " sanction_validity," +
            //                 " sanction_expirydate," +
            //                 " sanction_reviewdate," +
            //                 " nature_ofproposal," +
            //                 " constitution," +
            //                 " specific_condition," +
            //                 " authorized_signatory," +
            //                 " existing_limit," +
            //                 " revisied_limit," +
            //                 " escrow_account," +
            //                 " facility_type," +
            //                 " created_by," +
            //                 " created_date)" +
            //                 " values(" +
            //                 "'" + msGetGid + "', " +
            //                 "'" + values[i].sanction_refno + "'," +
            //                 "'" + Convert.ToDateTime(values[i].sanction_date).ToString("yyyy-MM-dd") + "'," +
            //                 "'" + values[i].sanction_amount.Replace("'", "\\'") + "'," +
            //                 "'" + values[i].sanction_limit.Replace("'", "\\'") + "'," +
            //                 "'" + values[i].customer_gid + "'," +
            //                 "'" + values[i].customer_name + "'," +
            //                 "'" + values[i].approval_authority + "'," +
            //                 "'" + values[i].sanction_validity + "'," +
            //                 "'" + Convert.ToDateTime(values[i].expiry_date).ToString("yyyy-MM-dd") + "'," +
            //                 "'" + Convert.ToDateTime(values[i].review_date).ToString("yyyy-MM-dd") + "'," +
            //                 "'" + values[i].natureof_proposal + "'," +
            //                 "'" + values[i].constitution + "'," +
            //                 "'" + values[i].specific_conditions.Replace("'", "\\'") + "'," +
            //                 "'" + values[i].authorized_signatory + "', " +
            //                 "'" + values[i].revisied_limit.Replace("'", "\\'") + "'," +
            //                 "'" + values[i].existing_limit.Replace("'", "\\'") + "'," +
            //                 "'" + values[i].escrow_account + "'," +
            //                 "'" + values[i].facility_type + "'," +
            //                 "'" + employee_gid + "'," +
            //                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //}

            //();
            //if (mnResult != 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}

        }


        public void DaGetSanctionDtls(string sanction_gid, sanctionviewdtl values)
        {
            try
            {
                msSQL = " SELECT concat(d.user_firstname, ' ', d.user_lastname, '/', d.user_code) as riskmanager, " +
                        " a.customer2sanction_gid,b.customer_gid,b.customer_urn,a.sanction_refno, " +
                        " format((sanction_amount), 2) as sanction_amount,b.customername,a.collateral_security, " +
                        " date_format(sanction_date, '%d-%m-%Y') as sanctionDate, " +
                        " b.zonal_head,b.zonal_name,b.business_head,b.businesshead_name,b.relationship_manager, " +
                        " b.relationshipmgmt_name, b.cluster_manager_gid,b.cluster_manager_name,b.creditmanager_gid, " +
                        " b.creditmgmt_name,a.facility_type,a.facilitytype_gid,b.mobileno,b.vertical_code,  credit_manager " +
                        " FROM ocs_mst_tcustomer2sanction a " +
                        " LEFT JOIN ocs_mst_tcustomer b ON a.customer_gid = b.customer_gid " +
                        " left join hrm_mst_temployee c on b.assigned_RM = c.employee_gid " +
                        " left join adm_mst_tuser d on d.user_gid = c.user_gid " +
                        " WHERE customer2sanction_gid = '" + sanction_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.customer2sanction_gid = objODBCDatareader["customer2sanction_gid"].ToString();
                    values.sanction_refno = objODBCDatareader["sanction_refno"].ToString();
                    values.sanction_date = objODBCDatareader["sanctionDate"].ToString();
                    values.facility_type = objODBCDatareader["facility_type"].ToString();
                    values.customer_gid = objODBCDatareader["customer_gid"].ToString();
                    values.customername = objODBCDatareader["customername"].ToString();
                    values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                    values.zonal_name = objODBCDatareader["zonal_name"].ToString();
                    values.vertical_code = objODBCDatareader["vertical_code"].ToString();
                    values.businesshead_name = objODBCDatareader["businesshead_name"].ToString();
                    values.relationshipmgmt_name = objODBCDatareader["relationshipmgmt_name"].ToString();
                    values.cluster_manager_name = objODBCDatareader["cluster_manager_name"].ToString();
                    values.creditmanager_name = objODBCDatareader["creditmgmt_name"].ToString();
                    values.collateral_security = objODBCDatareader["collateral_security"].ToString();
                    values.mobileno = objODBCDatareader["mobileno"].ToString();
                    values.sanction_amount = objODBCDatareader["sanction_amount"].ToString();
                    values.riskmanager = objODBCDatareader["riskmanager"].ToString();
                }
                objODBCDatareader.Close();
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }

        }
    }
}