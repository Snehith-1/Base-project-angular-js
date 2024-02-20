using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.idas.Models;
using System.Configuration;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Web;
using Spire.Doc;
using Spire.Doc.Documents;
using ems.storage.Functions;

namespace ems.idas.DataAccess
{
    public class DaDocList
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        string msSQL;
        OdbcDataReader objODBCDataReader, objODBCDataReader1;
        int mnResult;
        string lstemplate_content, lscontent, msGetGid, lscompany_code, lspath;
        DaSanctionMst objDaSanction = new DaSanctionMst();

        public bool DaGetDocList(DocumentList values)
        {

            msSQL = " SELECT documentlist_gid,document_code,document_name,display_order,comments " +
                   " FROM ids_mst_tdocumentlist ORDER  BY display_order ASC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<IDASDocument>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                   
                    getDocList.Add(new IDASDocument
                    {
                        documentlist_gid = dt["documentlist_gid"].ToString(),
                        document_code=dt["document_code"].ToString (),
                        document_name=dt["document_name"].ToString (),
                        display_order=Convert.ToInt16 (dt["display_order"].ToString ()),
                        comments=dt["comments"].ToString (),

                    });
                }
                values.IDASDocument = getDocList;
                values.status = true;
                values.message = "Data Fetched";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();

            return true;
        }

        public void DaPostCreationDocList(IDASDocument values,string user_gid,result objResult)
        {
            msSQL = " SELECT documentlist_gid FROM" +
                    " ids_mst_tdocumentlist" +
                    " WHERE document_name='" + values.document_name.Replace("'", "") + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows)
            {
                objODBCDataReader.Close();
                objResult.message = "Document Name Already Exists";
                objResult.status = false;
                return;
               }

            msSQL = " SELECT documentlist_gid "+
                    " FROM ids_mst_tdocumentlist"+
                    " WHERE display_order='"+ values.display_order +"'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if(objODBCDataReader .HasRows)
            {
                objODBCDataReader.Close();
                objResult.message = "Display Order Already Exists";
                objResult.status = false;
                return;
            }

            objODBCDataReader.Close();
            values.documentlist_gid = objcmnfunctions.GetMasterGID("DOCL");
            values.document_code = objcmnfunctions.GetMasterGID("D");

            msSQL = " INSERT INTO ids_mst_tdocumentlist(" +
                    " documentlist_gid," +
                    " document_code," +
                    " document_name," +
                    " comments,"+
                    " display_order,"+
                    " created_by," +
                    " created_date)" +
                    " VALUES("+
                    "'" + values.documentlist_gid +"'," +
                    "'" + values.document_code  +"'," +
                    "'" + values.document_name.Replace ("'","") + "'," +
                    "'" + values.comments.Replace("'","") + "',"+
                    "'" + values.display_order +"',"+
                    "'" + user_gid + "'," +
                    "current_timestamp)";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if ( mnResult ==1)
            {
                objResult.message = "Document List Added Successfully..!";
                objResult.status = true;
                return;
            }
            else
            {
                objResult.message = "Error Occured";
                objResult.status = false;
                return;
            }


        }

        public void DaPostDelete(string doclist_gid,result values)
        {
            msSQL = "SELECT document_gid FROM ids_trn_tsanctiondocumentdtls "+
                " WHERE document_gid='"+ doclist_gid +"'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader .HasRows )
            {
                objODBCDataReader.Close();
                values.message = "The Document Tagged In Sanction,you Cannot Delete";
                values.status = false;
            }
            else
            {
                objODBCDataReader.Close();
                msSQL = " DELETE FROM ids_mst_tdocumentlist" +
                              " WHERE documentlist_gid='" + doclist_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    values.message = "Document List Deleted Successfully....!";
                    values.status = true;
                    return;
                }
                else
                {
                    values.message = "Error Occured";
                    values.status = false;
                    return;
                }
            }

          
        }

        public void DaGetEditDocList(IDASDocument values,string doclist_gid)
        {
            try
            {
                msSQL = "SELECT documentlist_gid, document_code, document_name,display_order,comments, template_content" +
                                " FROM ids_mst_tdocumentlist" +
                                " WHERE documentlist_gid='" + doclist_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    values.documentlist_gid = objODBCDataReader["documentlist_gid"].ToString();
                    values.document_code = objODBCDataReader["document_code"].ToString();
                    values.document_name = objODBCDataReader["document_name"].ToString();
                    values.display_order = Convert.ToInt16(objODBCDataReader["display_order"].ToString());
                    values.comments = objODBCDataReader["comments"].ToString();
                    values.template_content = objODBCDataReader["template_content"].ToString();
                }
                objODBCDataReader.Close();
                values.status = true;
                values.message = "Data Fetched";
            }
            catch(Exception ex)
            {
                values.status = false;
                values.message = ex.Message;
            }

         
        }

        public void DaPostUpdateDoc(IDASDocument values,string user_gid, result objResult)
        {
            msSQL = " SELECT documentlist_gid FROM" +
                    " ids_mst_tdocumentlist" +
                    " WHERE document_name='" + values.document_name.Replace("'","") + "'";
            var DocList = objdbconn.GetExecuteScalar(msSQL);
            if(DocList !="")
            {
                if (DocList != values.documentlist_gid )
                {
                    objResult.message = "Document Name Already Exits";
                    objResult.status = false;
                    return;
                }
            }

            msSQL = " SELECT documentlist_gid FROM" +
                   " ids_mst_tdocumentlist" +
                   " WHERE display_order='" + values.display_order + "'";
            var DocListGid = objdbconn.GetExecuteScalar(msSQL);
            if (DocListGid != "")
            {
                if (DocListGid != values.documentlist_gid)
                {
                    objResult.message = "Document Display Order Already Exits";
                    objResult.status = false;
                    return;
                }
            }


            msSQL = " UPDATE ids_mst_tdocumentlist SET"+
                " document_name='"+ values .document_name.Replace ("'","") +"',"+
                " display_order='"+values.display_order+"',"+
                " comments='"+values.comments.Replace("'","")+"',"+
                " updated_by='"+ user_gid +"',"+
                " updated_date=current_timestamp"+
                " WHERE documentlist_gid='"+ values .documentlist_gid +"'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if(mnResult ==1)
            {
                objResult.message = "Document List Updated Successfully..!";
                objResult.status = true;

            }
            else
            {
                objResult.message = "Error Occured";
                objResult.status = false;
            }
        }

        public void DaGetDocumentData(documentSummary objdocumentsummary)
        {
            msSQL = "select a.document_code as 'Document Code',a.document_name as 'Document Name',a.comments as 'Comments',"+
                    " date_format(a.created_date, '%d-%m-%Y %h:%i:%s %p') as 'Created Date', concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as "+
                    " 'Created By'  from adm_mst_tuser b"+
                    " left join ids_mst_tdocumentlist a on a.created_by = b.user_gid where a.created_by = b.user_gid";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();

            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Document List");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objdocumentsummary.lsname = "Document List Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "IDAS/Document/DocumentReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objdocumentsummary.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "IDAS/Document/DocumentReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objdocumentsummary.lsname;
                objdocumentsummary.lscloudpath = lscompany_code + "/" + "IDAS/Document/DocumentReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objdocumentsummary.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objdocumentsummary.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 5])  //Address "A1:A5"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "IDAS/Document/DocumentReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objdocumentsummary.lsname, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                objdocumentsummary.status = false;
                objdocumentsummary.message = "Failure";
            }
            objdocumentsummary.lscloudpath = objcmnstorage.EncryptData(objdocumentsummary.lscloudpath);
            objdocumentsummary.lspath = objcmnstorage.EncryptData(objdocumentsummary.lspath);
            objdocumentsummary.status = true;
            objdocumentsummary.message = "Success";
        }

        public bool DaPostDocTemplate(string user_gid, IDASDocument values)
        {
            try
            {
                msSQL = " update ids_mst_tdocumentlist set " +
                        " template_content='" + values.template_content.Replace("'", "") + "'," +
                        " updated_by='" + user_gid + "'," +
                        " updated_date=current_timestamp" +
                        " where documentlist_gid='" + values.documentlist_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    values.message = "Template Details Updated Successfully..!";
                    values.status = true;
                }
                else
                {
                    values.message = "Error Occured";
                    values.status = false;
                }
            }
            catch
            {
                values.status = false;
            }
            return true;
        }

        public bool DaGetDocTemplateContent(IDASDocument values, string doclist_gid)
        {
            //Get Template Content
            msSQL = " select template_content from ids_mst_tdocumentlist where documentlist_gid='" + doclist_gid + "'";
            lstemplate_content = objdbconn.GetExecuteScalar(msSQL);

            lscontent = lstemplate_content;

            msSQL = " select sanction_gid from ids_trn_tsanctiondocumentdtls where document_gid='"+ doclist_gid + "'";
            string lssanction_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select a.sanction_refno, a.customer_name, date_format(a.ccapproved_date,'%d-%m-%Y') as ccapproved_date, a.validity_months, b.contactperson," +
                    " b.mobileno, b.mobileno, b.email, b.address, a.purpose_lending, sanction_amount, date_format(a.sanction_date, '%d-%m-%Y') as sanction_date " +
                    " from ocs_mst_tcustomer2sanction a " +
                    " LEFT JOIN ocs_mst_tcustomer b on a.customer_gid = b.customer_gid " +
                    " where customer2sanction_gid='" + lssanction_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.sanction_refno = objODBCDataReader["sanction_refno"].ToString();
                values.customer_name = objODBCDataReader["customer_name"].ToString();
                values.ccapproved_date = objODBCDataReader["ccapproved_date"].ToString();
                values.address = objODBCDataReader["address"].ToString();
                values.mobileno = objODBCDataReader["mobileno"].ToString();
                values.email = objODBCDataReader["email"].ToString();
                values.contactperson = objODBCDataReader["contactperson"].ToString();
                values.purpose_lending = objODBCDataReader["purpose_lending"].ToString();
                values.validity_months = objODBCDataReader["validity_months"].ToString();
                values.sanction_amount = objODBCDataReader["sanction_amount"].ToString();
                values.sanction_date = objODBCDataReader["sanction_date"].ToString();
            }
            objODBCDataReader.Close();

            int sanctionamount = Convert.ToInt32(values.sanction_amount);
            string sanctionamount_inwords = objcmnfunctions.NumberToWords(sanctionamount);
            
            lscontent = lscontent.Replace("sanction_ref_no", values.sanction_refno);
            lscontent = lscontent.Replace("sanction_amount", values.sanction_amount);
            lscontent = lscontent.Replace("sanctionamount_inwords", sanctionamount_inwords);
            lscontent = lscontent.Replace("sanction_date", values.sanction_date);

            msSQL = "select SUBSTRING_INDEX(proposed_roi, '%', 1) proposed_roi, loanfacility_type, expiry_date, revolving_type, tenure" +
              " from ocs_mst_tsanction2loanfacilitytype  where customer2sanction_gid='" + lssanction_gid + "'";
            objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader1.HasRows == true)
            {
                values.proposed_roi = objODBCDataReader1["proposed_roi"].ToString();
                values.loanfacility_type = objODBCDataReader1["loanfacility_type"].ToString();
                values.expiry_date = objODBCDataReader1["expiry_date"].ToString();
                values.revolving_type = objODBCDataReader1["revolving_type"].ToString();
                values.tenure = objODBCDataReader1["tenure"].ToString();
            }
            objODBCDataReader1.Close();
            
            lscontent = lscontent.Replace("proposed_roi", values.proposed_roi);
            lscontent = lscontent.Replace("loanfacility_type", values.loanfacility_type);
            lscontent = lscontent.Replace("expiry_date", values.expiry_date);
            lscontent = lscontent.Replace("revolving_type", values.revolving_type);
            lscontent = lscontent.Replace("tenure_months", values.tenure);

            values.template_content = lscontent;

            values.status = true;
            return true;
        }

        public bool DaGetDocument2SanctionList(string sanction_gid, DocumentList values)
        {
            msSQL = " select a.sanction_gid, a.document_gid,a.document_code,a.document_name,a.documentrecord_id, a.doctemplate_status, a.doctemplate_flag, b.template_content " +
                   " from ids_trn_tsanctiondocumentdtls a "+
                   " inner join ids_mst_tdocumentlist b on a.document_gid = b.documentlist_gid "+
                   " where sanction_gid='" + sanction_gid + "' and doctemplate_flag='Y' ORDER BY document_gid ASC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<IDASDocument>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getDocList.Add(new IDASDocument
                    {
                        documentlist_gid = dt["document_gid"].ToString(),
                        document_code = dt["document_code"].ToString(),
                        document_name = dt["document_name"].ToString(),
                        documentrecord_id = dt["documentrecord_id"].ToString(),
                        doctemplate_status = dt["doctemplate_status"].ToString(),
                        sanction_gid = dt["sanction_gid"].ToString(),
                        doctemplate_flag = dt["doctemplate_flag"].ToString(),
                        template_content = dt["template_content"].ToString(),
                    });
                }
                values.IDASDocument = getDocList;
                values.status = true;
                values.message = "Data Fetched";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaPostDocTemplateGenerate(IDASDocument values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("DTGG");
            msSQL = "insert into ids_trn_tdoctemplategenerate(" +
                " doctemplategenerate_gid, " +
                " sanction_gid," +
                " documentlist_gid, " +
                " document_code, " +
                " template_content, " +
                " created_by," +
                " created_date)" +
                " values (" +
                "'" + msGetGid + "'," +
                "'" + values.sanction_gid + "'," +
                "'" + values.documentlist_gid + "'," +
                "'" + values.document_code + "'," +
                "'" + values.template_content.Replace("'", "") + "'," +
                "'" + employee_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if(mnResult == 1)
            {
                msSQL = " update ids_trn_tsanctiondocumentdtls set doctemplate_status='Generated', doctemplate_flag='Y', template_content='" + values.template_content.Replace("'", "") + "'" +
                        " where document_gid='" + values.documentlist_gid + "' and sanction_gid='"+ values.sanction_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Document Generated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
                return false;
            }
        }

        public void DaGetEditDoc2sanction(IDASDocument values, string document_gid, string sanction_gid)
        {
            try
            {
                msSQL = " SELECT document_code, document_name,template_content " +
                        " FROM ids_trn_tsanctiondocumentdtls WHERE document_gid='" + document_gid + "' and sanction_gid='" + sanction_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    values.document_code = objODBCDataReader["document_code"].ToString();
                    values.document_name = objODBCDataReader["document_name"].ToString();
                    values.template_content = objODBCDataReader["template_content"].ToString();
                }
                objODBCDataReader.Close();
                values.status = true;
                values.message = "Data Fetched";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.Message;
            }


        }

        public void DaGetDocWordGenerate(string documentlist_gid, string sanction_gid, template_list values)
        {
            msSQL = " SELECT template_content FROM ids_trn_tsanctiondocumentdtls where document_gid='" + documentlist_gid + "' and sanction_gid='" + sanction_gid + "'";
            string lstemplatecontent = objdbconn.GetExecuteScalar(msSQL);
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "IDAS/PreFilDocumentGeneration/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                {
                    if ((!System.IO.Directory.Exists(values.lspath)))
                        System.IO.Directory.CreateDirectory(values.lspath);
                }
                string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                string lsfile_gid = msdocument_gid;

                values.lsname = "Sanction_Document" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".docx";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "IDAS/PreFilDocumentGeneration/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;

                // Save the HTML string as HTML File.
                string htmlFilePath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "IDAS/PreFilHTML/prefildoc.html";
                File.WriteAllText(htmlFilePath, lstemplatecontent);

                Spire.Doc.Document document = new Spire.Doc.Document();

                document.LoadFromFile(htmlFilePath, FileFormat.Html, XHTMLValidationType.None);

                //Read Header and Footer File

                Document doc1 = new Document();
                doc1.LoadFromFile(ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "TmpFile/Logo/headerfile.docx");
                HeaderFooter header = doc1.Sections[0].HeadersFooters.Header;

                Document doc2 = new Document(htmlFilePath);

                foreach (Section section in doc2.Sections)
                {
                    foreach (DocumentObject obj in header.ChildObjects)
                    {
                        section.HeadersFooters.Header.ChildObjects.Add(obj.Clone());
                    }
                }

                // Document
                string lsprefilfile = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "TmpFile/Footer/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + lsfile_gid + "/");
                {
                    if ((!System.IO.Directory.Exists(lsprefilfile)))
                        System.IO.Directory.CreateDirectory(lsprefilfile);
                }
                lsprefilfile = lsprefilfile + values.lsname;
                doc2.SaveToFile(lsprefilfile, FileFormat.Docx);

                Document doc3 = new Document();
                doc3.LoadFromFile(lsprefilfile);
                HeaderFooter footer = doc3.Sections[0].HeadersFooters.Footer;
                Paragraph footerParagraph = footer.AddParagraph();
                
                HeaderFooter footer1 = doc3.Sections[0].HeadersFooters.Footer;
                Paragraph footerParagraph1 = footer1.AddParagraph();
                footerParagraph1.AppendField("page number", FieldType.FieldPage);
                footerParagraph1.AppendText(" of ");
                footerParagraph1.AppendField("number of pages", FieldType.FieldNumPages);
                footerParagraph1.Format.HorizontalAlignment = HorizontalAlignment.Center;

                TextWatermark txtWatermark = new TextWatermark();
                //set the text watermark with text string, font, color and layout.
                txtWatermark.Text = "Samunnati";
                txtWatermark.FontSize = 45;
                txtWatermark.Color = Color.Gray;
                txtWatermark.Layout = WatermarkLayout.Diagonal;
                //add the text watermark
                doc3.Watermark = txtWatermark;
                //Protect Word
                doc3.Protect(ProtectionType.AllowOnlyReading, "Welcome@123");

                doc3.SaveToFile(values.lspath, FileFormat.Docx);

                File.Delete(htmlFilePath);

                values.status = true;
                values.message = "Success";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
        }

        public bool DaGetGeneratedDocList(DocumentList values)
        {
            msSQL = " select a.sanction_gid, c.sanction_refno, d.customername, d.customer_urn, date_format(c.sanction_date,'%d-%m-%Y') as sanction_date " +
                    " from ids_trn_tsanctiondocumentdtls a " +
                    " inner join ids_mst_tdocumentlist b on a.document_gid = b.documentlist_gid" +
                    " inner join ocs_mst_tcustomer2sanction c on c.customer2sanction_gid = a.sanction_gid" +
                    " inner join ocs_mst_tcustomer d on d.customer_gid = c.customer_gid "+
                    " where a.doctemplate_flag = 'Y' GROUP BY a.sanction_gid ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<IDASDocument>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getDocList.Add(new IDASDocument
                    {
                        sanction_gid = dt["sanction_gid"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        customer_name = dt["customername"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        sanction_date = dt["sanction_date"].ToString(),
                    });
                }
                values.IDASDocument = getDocList;
                values.status = true;
                values.message = "Data Fetched";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();
            return true;
        }
    }
}