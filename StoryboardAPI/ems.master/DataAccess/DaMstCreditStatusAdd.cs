using ems.master.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Web;
using System.Net.Mail;
using System.Net;
using ems.storage.Functions;


namespace ems.master.DataAccess
{
    public class DaMstCreditStatusAdd
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGetDocumentGid;
        string lspath;
        int mnResult;
        HttpPostedFile httpPostedFile;
        string lsbuyer_code, lsbuyer_name, lscoi_date, lsbusinessstart_date, lsyear_business, lsmonth_business, lsconstitution_gid, lsconstitution_name, lsbuyer2gst_gid;
        string lscin_no, lspan_no, lsgst_no, lscontactperson_firstname, lscontactperson_middlename, lscontactperson_lastname, lsgststate_gid, lsgststate_name;
        string lsmobile_no, lsprimary_mobileno, lswhatsapp_mobileno, lsbuyer2mobileno_gid, lsbuyer_gid, lsemail_address, lsprimary_emailaddress, lsbuyer2emailaddress_gid;
        string lsbank_name, lsbranch_name, lsifsc_code, lsbankaccount_name, lsbankaccountlevel_gid, lsbankaccountlevel_name, lsbankaccount_number, lsbuyer2bank_gid;
        string lsaddress_typegid, lsaddress_type, lsaddressline1, lsaddressline2, lslandmark, lstaluka, lsprimary_address, lspostal_code, lscity, lsdistrict;
        string lsstate, lscountry, lslatitude, lslongitude, lsbuyer2address_gid, lsgstregister_status, lsapproval_remarks, lsconfirmbankaccountnumber, lsmicr_code, lsbank_address, lsbankaccounttype_gid, lsbankaccounttype_name;
        string frommail_id, sub, tomail_id, lsstate_name, contactperson, customer_name, body, message, ls_server, ls_username, ls_password, lscontent = string.Empty;
        string lscreatedby, lscreateddate;
        string lsupdatedby, lsupdateddate;
        public string lssource;

        string[] lsToReceipients;
        string[] lsCCReceipients;
        int ls_port;
        string to, commonto;
        string cc, commoncc;



        public void DaGetbuyerToCreditSummary(string employee_gid, MdlMstCreditStatusAdd values)
        {
            msSQL = " select buyer_gid, buyer_code, buyer_name,f.department_name, a.credit_status, case when a.creditActive_status='N' then 'Inactive' else 'Active' end as creditActive_status, " +
                    " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as updated_by,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date" +
                    " from ocs_mst_tbuyer a " +
                    " left join hrm_mst_temployee c on c.employee_gid=a.created_by " +
                    " left join adm_mst_tuser b on c.user_gid=b.user_gid" +
                    " left join hrm_mst_temployee d on d.employee_gid=a.updated_by " +
                    " left join adm_mst_tuser e on d.user_gid=e.user_gid" +
                    " left join hrm_mst_tdepartment f on f.department_gid = c.department_gid " +
                    " where buyertocredit_flag='Y' and creditstatus_Approval='NA' order by buyer_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditstatuslist = new List<creditstatuslist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcreditstatuslist.Add(new creditstatuslist
                    {
                        buyer_gid = dt["buyer_gid"].ToString(),
                        buyer_code = dt["buyer_code"].ToString(),
                        buyer_name = dt["buyer_name"].ToString(),
                        credit_status = dt["credit_status"].ToString(),
                        department_name = dt["department_name"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        updated_by = dt["updated_by"].ToString(),
                        updated_date = dt["updated_date"].ToString(),
                        creditActive_status = dt["creditActive_status"].ToString(),
                    });
                    values.creditstatuslist = getcreditstatuslist;
                }
            }
            dt_datatable.Dispose();
        }

        public bool DaBureauDocumentUpload(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();
            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/BureauScoreDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;
                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {
                        string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
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
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/BureauScoreDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/BureauScoreDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msSQL = " insert into ocs_tmp_tbureauscoredocument( " +
                                    " document_name ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + httpPostedFile.FileName.Replace("'", " ") + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension.Replace("'", " ") + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            objfilename.status = true;
                            objfilename.message = "Document Uploaded Successfully..!";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured..!";
                        }

                        msSQL = " select tmpbureauscoredocument_gid,document_name,document_path from ocs_tmp_tbureauscoredocument " +
                                " where created_by='" + employee_gid + "'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        var getdocumentdtlList = new List<upload_list>();
                        if (dt_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                getdocumentdtlList.Add(new upload_list
                                {
                                    document_name = dt["document_name"].ToString(),
                                    document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                                    tmp_documentGid = dt["tmpbureauscoredocument_gid"].ToString(),
                                });
                                objfilename.upload_list = getdocumentdtlList;
                            }
                        }
                        dt_datatable.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }
            return true;
        }

        public void DaTmpDocumentDelete(string tmp_documentGid, uploaddocument objfilename, string user_gid)
        {
            msSQL = " delete from ocs_tmp_tbureauscoredocument where tmpbureauscoredocument_gid='" + tmp_documentGid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            msSQL = " select tmpbureauscoredocument_gid,document_name,document_path from ocs_tmp_tbureauscoredocument " +
                    " where created_by='" + user_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<upload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new upload_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        tmp_documentGid = dt["tmpbureauscoredocument_gid"].ToString(),
                    });
                    objfilename.upload_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();

            if (mnResult != 0)
            {
                objfilename.status = true;
                objfilename.message = "Document Deleted Successfully..!";
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "Error Occured..!";
            }
        }

        public void DaBureauScoreAdd(string employee_gid, string user_gid, MdlMstCreditStatusAdd values)
        {
            msSQL = " select tmpbureauscoredocument_gid,document_name,document_path from ocs_tmp_tbureauscoredocument " +
                               " where created_by='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<upload_list>();
            if (dt_datatable.Rows.Count == 0)
            {
                values.status = false;
                values.message = "Kindly Upload Document";
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("BSAG");
                msSQL = "Insert into ocs_mst_tbureauscoreadd(" +
                        " bureauscoreadd_GID," +
                        " buyer_gid," +
                        " bureauname_gid," +
                        " bureauname_name," +
                        " bureau_score," +
                        " bureaugenerated_date," +
                        " lastyear_turnover," +
                        " assessmentagency_gid," +
                        " assessmentagency_name," +
                        " creditrating_gid," +
                        " creditrating_name," +
                        " creditrating_date," +
                        " creditratingexpiry_date," +
                        " created_by," +
                        " created_date)" +
                        " values (" +
                        "'" + msGetGid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + values.bureauname_gid + "'," +
                        "'" + values.bureauname_name + "'," +
                        "'" + values.bureau_score + "',";
                if ((values.bureaugenerated_date == null) || (values.bureaugenerated_date == ""))
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.bureaugenerated_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                msSQL += "'" + values.lastyear_turnover + "'," +
                         "'" + values.assessmentagency_gid + "'," +
                         "'" + values.assessmentagency_name + "'," +
                         "'" + values.creditrating_gid + "'," +
                         "'" + values.creditrating_name + "',";
                if ((values.creditrating_date == null) || (values.creditrating_date == ""))
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.creditrating_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                if ((values.creditratingexpiry_date == null) || (values.creditratingexpiry_date == ""))
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.creditratingexpiry_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                msSQL += "'" + employee_gid + "'," +
                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    // Document Attachments
                    msSQL = "select document_name,document_path from ocs_tmp_tbureauscoredocument where created_by='" + employee_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            msGetDocumentGid = objcmnfunctions.GetMasterGID("BSDA");
                            msSQL = " INSERT into ocs_mst_tbureauscoredocumentadd (" +
                                    " bureauscoredocumentadd_gid," +
                                    " bureauscoreadd_GID," +
                                    " buyer_gid," +
                                    " document_name," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " ) VALUES (" +
                                    " '" + msGetDocumentGid + "'," +
                                    " '" + msGetGid + "'," +
                                    " '" + employee_gid + "'," +
                                    " '" + dt["document_name"].ToString() + "'," +
                                    " '" + dt["document_path"].ToString() + "'," +
                                    "'" + user_gid + "'," +
                                    " current_timestamp)";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }

                    dt_datatable.Dispose();

                    msSQL = "delete from ocs_tmp_tbureauscoredocument where created_by='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " select bureauname_name, bureauname_gid, bureau_score, date_format(a.bureaugenerated_date,'%d-%m-%Y') as bureaugenerated_date, " +
                        " lastyear_turnover, assessmentagency_name, assessmentagency_gid, creditrating_name, date_format(a.creditrating_date,'%d-%m-%Y') as creditrating_date, " +
                        " date_format(a.creditratingexpiry_date,'%d-%m-%Y') as creditratingexpiry_date, bureauscoreadd_GID, " +
                        " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                        " from ocs_mst_tbureauscoreadd a " +
                        " LEFT JOIN hrm_mst_temployee c ON a.created_by=c.employee_gid" +
                        " LEFT JOIN adm_mst_tuser b ON c.user_gid=b.user_gid" +
                        "  where a.buyer_gid = '" + values.buyer_gid + "' or a.buyer_gid = '" + employee_gid + "' order by bureauscoreadd_GID desc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var get_bureauscore_list = new List<bureauscore_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            get_bureauscore_list.Add(new bureauscore_list
                            {
                                bureauscoreadd_GID = dt["bureauscoreadd_GID"].ToString(),
                                bureauname_name = dt["bureauname_name"].ToString(),
                                bureauname_gid = dt["bureauname_gid"].ToString(),
                                bureau_score = dt["bureau_score"].ToString(),
                                bureaugenerated_date = dt["bureaugenerated_date"].ToString(),
                                lastyear_turnover = dt["lastyear_turnover"].ToString(),
                                assessmentagency_name = dt["assessmentagency_name"].ToString(),
                                assessmentagency_gid = dt["assessmentagency_gid"].ToString(),
                                creditrating_name = dt["creditrating_name"].ToString(),
                                creditrating_date = dt["creditrating_date"].ToString(),
                                creditratingexpiry_date = dt["creditratingexpiry_date"].ToString(),
                                created_by = dt["created_by"].ToString(),
                                created_date = dt["created_date"].ToString(),
                            });
                            values.bureauscore_list = get_bureauscore_list;
                        }
                    }
                    dt_datatable.Dispose();

                    values.status = true;
                    values.message = "Bureau Score Added Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred";
                }
            }
        }

        public void DaBureauScoreView(string buyer_gid, creditstatuslist values)
        {
            msSQL = " select bureauname_name, bureauname_gid, bureau_score, date_format(a.bureaugenerated_date,'%d-%m-%Y') as bureaugenerated_date, " +
                    " lastyear_turnover, assessmentagency_name, assessmentagency_gid, creditrating_name, date_format(a.creditrating_date,'%d-%m-%Y') as creditrating_date, " +
                    " date_format(a.creditratingexpiry_date,'%d-%m-%Y') as creditratingexpiry_date, bureauscoreadd_GID, " +
                    " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                    " from ocs_mst_tbureauscoreadd a " +
                    " LEFT JOIN hrm_mst_temployee c ON a.created_by=c.employee_gid" +
                    " LEFT JOIN adm_mst_tuser b ON c.user_gid=b.user_gid" +
                    "  where a.buyer_gid = '"+ buyer_gid + "' order by bureauscoreadd_GID desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_bureauscore_list = new List<bureauscore_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_bureauscore_list.Add(new bureauscore_list
                    {
                        bureauscoreadd_GID = dt["bureauscoreadd_GID"].ToString(),
                        bureauname_name = dt["bureauname_name"].ToString(),
                        bureauname_gid = dt["bureauname_gid"].ToString(),
                        bureau_score = dt["bureau_score"].ToString(),
                        bureaugenerated_date = dt["bureaugenerated_date"].ToString(),
                        lastyear_turnover = dt["lastyear_turnover"].ToString(),
                        assessmentagency_name = dt["assessmentagency_name"].ToString(),
                        assessmentagency_gid = dt["assessmentagency_gid"].ToString(),
                        creditrating_name = dt["creditrating_name"].ToString(),
                        creditrating_date = dt["creditrating_date"].ToString(),
                        creditratingexpiry_date = dt["creditratingexpiry_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });
                    values.bureauscore_list = get_bureauscore_list;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaBureauScoreDelete(string bureauscoreadd_GID, creditstatuslist values, string user_gid)
        {
            msSQL = " delete from ocs_mst_tbureauscoreadd where bureauscoreadd_GID='" + bureauscoreadd_GID + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = " delete from ocs_mst_tbureauscoredocumentadd where bureauscoreadd_GID='" + bureauscoreadd_GID + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Bureau Score Deleted Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaGetTempClear(string employee_gid, result values)
        {
            msSQL = "delete from ocs_tmp_tbureauscoredocument where created_by='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_mst_tbureauscoreadd where buyer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_mst_tbuyer2mobileno where buyer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_mst_tbuyer2emailaddress where buyer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_mst_tbuyer2address where buyer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_mst_tbuyer2bank where buyer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from ocs_mst_tbuyer2gst where buyer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }

        public void DabuyerDetailsEdit(string buyer_gid, buyeredit values)
        {
            try
            {
                msSQL = " select buyer_gid, buyer_code, buyer_name, coi_date, businessstart_date, year_business, month_business, constitution_gid, constitution_name, cin_no, pan_no," +
                   " contactperson_fn, contactperson_mn, contactperson_ln, cap_limit, overall_limit, buyer_limit, guarantor_limit,borrower_limit, credit_status, " +
                   " creditActive_status from ocs_mst_tbuyer where buyer_gid='" + buyer_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.buyer_gid = objODBCDatareader["buyer_gid"].ToString();
                    values.buyer_code = objODBCDatareader["buyer_code"].ToString();
                    values.buyer_name = objODBCDatareader["buyer_name"].ToString();
                    if (objODBCDatareader["coi_date"].ToString() == "")
                    {
                    }
                    else
                    {
                        values.editcoi_date = Convert.ToDateTime(objODBCDatareader["coi_date"]).ToString("dd-MM-yyyy");
                    }
                    if (objODBCDatareader["businessstart_date"].ToString() == "")
                    {
                    }
                    else
                    {
                        values.editbusinessstart_date = Convert.ToDateTime(objODBCDatareader["businessstart_date"]).ToString("dd-MM-yyyy");
                    }
                    values.year_business = objODBCDatareader["year_business"].ToString();
                    values.month_business = objODBCDatareader["month_business"].ToString();
                    values.constitution_gid = objODBCDatareader["constitution_gid"].ToString();
                    values.constitution_name = objODBCDatareader["constitution_name"].ToString();
                    values.cin_no = objODBCDatareader["cin_no"].ToString();
                    values.pan_no = objODBCDatareader["pan_no"].ToString();
                    values.contactperson_firstname = objODBCDatareader["contactperson_fn"].ToString();
                    values.contactperson_middlename = objODBCDatareader["contactperson_mn"].ToString();
                    values.contactperson_lastname = objODBCDatareader["contactperson_ln"].ToString();
                    values.cap_limit = objODBCDatareader["cap_limit"].ToString();
                    values.overall_limit = objODBCDatareader["overall_limit"].ToString();
                    values.buyer_limit = objODBCDatareader["buyer_limit"].ToString();
                    values.guarantor_limit = objODBCDatareader["guarantor_limit"].ToString();
                    values.borrower_limit = objODBCDatareader["borrower_limit"].ToString();
                    values.credit_status = objODBCDatareader["credit_status"].ToString();
                    values.creditActive_status = objODBCDatareader["creditActive_status"].ToString();
                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();
            }
           catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DabuyerDetailsUpdate(string employee_gid, buyeredit values)
        {
            msSQL = " select buyer_gid, buyer_code, buyer_name, coi_date, businessstart_date, year_business, month_business, constitution_gid," +
                  " constitution_name, cin_no, pan_no, contactperson_fn, contactperson_mn, contactperson_ln " +
                  " from ocs_mst_tbuyer where buyer_gid='" + values.buyer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsbuyer_code = objODBCDatareader["buyer_code"].ToString();
                lsbuyer_name = objODBCDatareader["buyer_name"].ToString();
                if (objODBCDatareader["coi_date"].ToString() == "")
                {
                }
                else
                {
                    lscoi_date = Convert.ToDateTime(objODBCDatareader["coi_date"]).ToString("dd-MM-yyyy");
                }
                if (objODBCDatareader["businessstart_date"].ToString() == "")
                {
                }
                else
                {
                    lsbusinessstart_date = Convert.ToDateTime(objODBCDatareader["businessstart_date"]).ToString("dd-MM-yyyy");
                }
                lsyear_business = objODBCDatareader["year_business"].ToString();
                lsmonth_business = objODBCDatareader["month_business"].ToString();
                lsconstitution_gid = objODBCDatareader["constitution_gid"].ToString();
                lsconstitution_name = objODBCDatareader["constitution_name"].ToString();
                lscin_no = objODBCDatareader["cin_no"].ToString();
                lspan_no = objODBCDatareader["pan_no"].ToString();
                lscontactperson_firstname = objODBCDatareader["contactperson_fn"].ToString();
                lscontactperson_middlename = objODBCDatareader["contactperson_mn"].ToString();
                lscontactperson_lastname = objODBCDatareader["contactperson_ln"].ToString();
            } 
            objODBCDatareader.Close();
            try
            {
                msSQL = " update ocs_mst_tbuyer set " +
                        " buyer_code='" + values.buyer_code + "'," +
                        " buyer_name='" + values.buyer_name + "',";
                if (Convert.ToDateTime(values.coi_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " coi_date='" + Convert.ToDateTime(values.coi_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                if (Convert.ToDateTime(values.businessstart_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " businessstart_date='" + Convert.ToDateTime(values.businessstart_date).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                msSQL += " year_business='" + values.year_business + "'," +
                         " month_business='" + values.month_business + "'," +
                         " constitution_gid='" + values.constitution_gid + "'," +
                         " constitution_name='" + values.constitution_name + "'," +
                         " cin_no='" + values.cin_no + "'," +
                         " pan_no='" + values.pan_no + "'," +
                         " contactperson_fn='" + values.contactperson_firstname + "'," +
                         " contactperson_mn='" + values.contactperson_middlename + "'," +
                         " contactperson_ln='" + values.contactperson_lastname + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where buyer_gid='" + values.buyer_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if(mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("BULG");

                    msSQL = "Insert into ocs_mst_tbuyerupdatelog(" +
                   " buyerupdatelog_gid, " +
                   " buyer_gid, " +
                   " buyer_code," +
                   " buyer_name," +
                   " coi_date," +
                   " businessstart_date," +
                   " year_business," +
                   " month_business," +
                   " constitution_gid," +
                   " constitution_name," +
                   " cin_no," +
                   " pan_no," +
                   " contactperson_fn," +
                   " contactperson_mn," +
                   " contactperson_ln," +
                   " updated_by," +
                   " updated_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + values.buyer_gid + "'," +
                   "'" + lsbuyer_code + "'," +
                   "'" + lsbuyer_name + "'," +
                   "'" + lscoi_date + "'," +
                   "'" + lsbusinessstart_date + "'," +
                   "'" + lsyear_business + "'," +
                             "'" + lsmonth_business + "'," +
                             "'" + lsconstitution_gid + "'," +
                             "'" + lsconstitution_name + "'," +
                             "'" + lscin_no + "'," +
                             "'" + lspan_no + "'," +
                             "'" + lscontactperson_firstname + "'," +
                             "'" + lscontactperson_middlename + "'," +
                             "'" + lscontactperson_lastname + "'," +
                             "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    msSQL = "update ocs_mst_tbuyer2gst set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Buyer Basic Details Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }
        
 // Mobile Number 

        public bool DaMobileNumberAdd(string employee_gid, string user_gid, Mdlmobile_no values)
        {
            msSQL = "select mobile_no from ocs_mst_tbuyer2mobileno where primary_mobileno='Yes' and buyer_gid='" + employee_gid + "'";
            string lsprimary_mobileno = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_mobileno == (values.primary_mobileno))
            {
                values.status = false;
                values.message = "Already Primary Mobile Number added";
                return false;
            }
            msSQL = "select buyer2mobileno_gid from ocs_mst_tbuyer2mobileno where mobile_no='" + values.mobile_no + "' and buyer_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already This Mobile Number added";
                return false;
            }
            msGetGid = objcmnfunctions.GetMasterGID("B2MN");
            msSQL = " insert into ocs_mst_tbuyer2mobileno(" +
                    " buyer2mobileno_gid," +
                    " buyer_gid," +
                    " mobile_no," +
                    " primary_mobileno," +
                    " whatsapp_mobileno," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.mobile_no + "'," +
                    "'" + values.primary_mobileno + "'," +
                    "'" + values.whatsapp_mobileno + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Mobile Number Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Adding Mobile Number";
                return false;
            }
        }

        public void DaGetMobileNoList(string buyer_gid, string employee_gid, Mdlmobile_no values)
        {
            msSQL = "select mobile_no,primary_mobileno,whatsapp_mobileno, buyer2mobileno_gid from ocs_mst_tbuyer2mobileno " +
                " where buyer_gid='" + employee_gid + "' or buyer_gid='" + buyer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmobileno_list = new List<mobileno_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmobileno_list.Add(new mobileno_list
                    {
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_mobileno = (dr_datarow["primary_mobileno"].ToString()),
                        whatsapp_mobileno = (dr_datarow["whatsapp_mobileno"].ToString()),
                        buyer2mobileno_gid = (dr_datarow["buyer2mobileno_gid"].ToString()),
                    });
                }
                values.mobileno_list = getmobileno_list;
            }
            dt_datatable.Dispose();
        }

        public void DaMobileNoEdit(string buyer2mobileno_gid, Mdlmobile_no values)
        {
            try
            {
                msSQL = "select mobile_no,primary_mobileno,whatsapp_mobileno, buyer2mobileno_gid from ocs_mst_tbuyer2mobileno " +
                " where buyer2mobileno_gid='" + buyer2mobileno_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.mobile_no = objODBCDatareader["mobile_no"].ToString();
                    values.primary_mobileno = objODBCDatareader["primary_mobileno"].ToString();
                    values.whatsapp_mobileno = objODBCDatareader["whatsapp_mobileno"].ToString();
                    values.buyer2mobileno_gid = objODBCDatareader["buyer2mobileno_gid"].ToString();
                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaMobileNoUpdate(string employee_gid, Mdlmobile_no values)
        {
            msSQL = "select mobile_no,primary_mobileno,whatsapp_mobileno, buyer2mobileno_gid, buyer_gid from ocs_mst_tbuyer2mobileno " +
                 " where buyer2mobileno_gid='" + values.buyer2mobileno_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsmobile_no = objODBCDatareader["mobile_no"].ToString();
                lsprimary_mobileno = objODBCDatareader["primary_mobileno"].ToString();
                lswhatsapp_mobileno = objODBCDatareader["whatsapp_mobileno"].ToString();
                lsbuyer2mobileno_gid = objODBCDatareader["buyer2mobileno_gid"].ToString();
                lsbuyer_gid = objODBCDatareader["buyer_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update ocs_mst_tbuyer2mobileno set " +
                         " mobile_no='" + values.mobile_no + "'," +
                         " primary_mobileno='" + values.primary_mobileno + "'," +
                         " whatsapp_mobileno='" + values.whatsapp_mobileno + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where buyer2mobileno_gid='" + values.buyer2mobileno_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    if(lsbuyer_gid == values.buyer_gid)
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("BMNU");

                        msSQL = "Insert into ocs_mst_tbuyer2mobilenoupdatelog(" +
                       " buyer2mobilenoupdatelog_gid, " +
                       " buyer2mobileno_gid, " +
                       " buyer_gid, " +
                       " mobile_no," +
                       " primary_mobileno," +
                       " whatsapp_mobileno," +
                       " created_by," +
                       " created_date)" +
                       " values (" +
                       "'" + msGetGid + "'," +
                       "'" + values.buyer2mobileno_gid + "'," +
                       "'" + values.buyer_gid + "'," +
                       "'" + lsmobile_no + "'," +
                       "'" + lsprimary_mobileno + "'," +
                       "'" + lswhatsapp_mobileno + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    values.status = true;
                    values.message = "Mobile Number Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaDeleteMobileNo(string buyer2mobileno_gid, string employee_gid, Mdlmobile_no values)
        {
            msSQL = "delete from ocs_mst_tbuyer2mobileno where buyer2mobileno_gid='" + buyer2mobileno_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from ocs_mst_tbuyer2mobilenoupdatelog where buyer2mobileno_gid='" + buyer2mobileno_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Mobile Number Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting Mobile Number";
                values.status = false;

            }
        }

// Email Address

        public bool DaEmailAddressAdd(string employee_gid, string user_gid, MdlEmail_address values)
        {
            msSQL = "select primary_emailaddress from ocs_mst_tbuyer2emailaddress where primary_emailaddress='Yes' and buyer_gid='" + employee_gid + "'";
            string lsprimary_emailaddress = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_emailaddress == (values.primary_emailaddress))
            {
                values.status = false;
                values.message = "Already Primary Email Address Added";
                return false;
            }
            msSQL = "select buyer2emailaddress_gid from ocs_mst_tbuyer2emailaddress where email_address='" + values.email_address + "' and buyer_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already This Email Address Added";
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("B2EA");
            msSQL = " insert into ocs_mst_tbuyer2emailaddress(" +
                    " buyer2emailaddress_gid," +
                    " buyer_gid," +
                    " email_address," +
                    " primary_emailaddress," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.email_address + "'," +
                    "'" + values.primary_emailaddress + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Email Address Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured while Adding Email Address";
                return false;
            }
        }

        public void DaGetEmailAddressList(string buyer_gid, string employee_gid, MdlEmail_address values)
        {
            msSQL = "select email_address, primary_emailaddress, buyer2emailaddress_gid, buyer_gid from ocs_mst_tbuyer2emailaddress" +
                  " where buyer_gid='" + employee_gid + "' or buyer_gid='" + buyer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getemail_list = new List<email_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getemail_list.Add(new email_list
                    {
                        email_address = (dr_datarow["email_address"].ToString()),
                        primary_emailaddress = (dr_datarow["primary_emailaddress"].ToString()),
                        buyer2emailaddress_gid = (dr_datarow["buyer2emailaddress_gid"].ToString()),
                        buyer_gid = (dr_datarow["buyer_gid"].ToString()),
                    });
                }
                values.email_list = getemail_list;
            }
            dt_datatable.Dispose();
        }

        public void DaEmailAddressEdit(string buyer2emailaddress_gid, MdlEmail_address values)
        {
            try
            {
                msSQL = "select email_address, primary_emailaddress, buyer2emailaddress_gid, buyer_gid from ocs_mst_tbuyer2emailaddress " +
                " where buyer2emailaddress_gid='" + buyer2emailaddress_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.email_address = objODBCDatareader["email_address"].ToString();
                    values.primary_emailaddress = objODBCDatareader["primary_emailaddress"].ToString();
                    values.buyer2emailaddress_gid = objODBCDatareader["buyer2emailaddress_gid"].ToString();
                    values.buyer_gid = objODBCDatareader["buyer_gid"].ToString();
                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaEmailAddressUpdate(string employee_gid, MdlEmail_address values)
        {
            msSQL = "select email_address, primary_emailaddress, buyer2emailaddress_gid, buyer_gid from ocs_mst_tbuyer2emailaddress" +
                 " where buyer2emailaddress_gid='" + values.buyer2emailaddress_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsemail_address = objODBCDatareader["email_address"].ToString();
                lsprimary_emailaddress = objODBCDatareader["primary_emailaddress"].ToString();
                lsbuyer2emailaddress_gid = objODBCDatareader["buyer2emailaddress_gid"].ToString();
                lsbuyer_gid = objODBCDatareader["buyer_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update ocs_mst_tbuyer2emailaddress set " +
                         " email_address='" + values.email_address + "'," +
                         " primary_emailaddress='" + values.primary_emailaddress + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where buyer2emailaddress_gid='" + values.buyer2emailaddress_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    if (lsbuyer_gid == values.buyer_gid)
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("BEAU");

                        msSQL = "Insert into ocs_mst_tbuyer2emailaddressupdatelog(" +
                       " buyer2emailaddressupdatelog_gid, " +
                       " buyer2emailaddress_gid, " +
                       " buyer_gid, " +
                       " email_address," +
                       " primary_emailaddress," +
                       " created_by," +
                       " created_date)" +
                       " values (" +
                       "'" + msGetGid + "'," +
                       "'" + values.buyer2emailaddress_gid + "'," +
                       "'" + values.buyer_gid + "'," +
                       "'" + lsemail_address + "'," +
                       "'" + lsprimary_emailaddress + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    values.status = true;
                    values.message = "Email Address Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaDeleteEmailAddress(string buyer2emailaddress_gid, string employee_gid, MdlEmail_address values)
        {
            msSQL = "delete from ocs_mst_tbuyer2emailaddress where buyer2emailaddress_gid='" + buyer2emailaddress_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from ocs_mst_tbuyer2emailaddressupdatelog where buyer2emailaddress_gid='" + buyer2emailaddress_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Email Address Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting Email Address";
                values.status = false;

            }
        }
        

 // Bank Detail

        public bool DaBankDetailsAdd(string employee_gid, string user_gid, MdlBank_Details values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("B2BK");
            msSQL = " insert into ocs_mst_tbuyer2bank(" +
                    " buyer2bank_gid," +
                    " buyer_gid," +
                    " bank_name," +
                    " branch_name," +
                    " ifsc_code," +
                    " bankaccount_name," +
                    " bankaccountlevel_gid," +
                    " bankaccountlevel_name," +
                    " bankaccount_number," +
                    " confirmbankaccountnumber," +
                    " micr_code," +
                    " bank_address," +
                    " bankaccounttype_gid," +
                    " bankaccounttype_name," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.bank_name + "'," +
                    "'" + values.branch_name + "'," +
                    "'" + values.ifsc_code + "'," +
                    "'" + values.bankaccount_name + "'," +
                    "'" + values.bankaccountlevel_gid + "'," +
                    "'" + values.bankaccountlevel_name + "'," +
                    "'" + values.bankaccount_number + "'," +
                    "'" + values.confirmbankaccountnumber + "'," +
                    "'" + values.micr_code + "'," +
                    "'" + values.bank_address + "'," +
                    "'" + values.bankaccounttype_gid + "'," +
                    "'" + values.bankaccounttype_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Bank Details Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Adding Bank Details";
                return false;
            }
        }

        public void DaGetBankList(string buyer_gid, string employee_gid, MdlBank_Details values)
        {
            msSQL = "select bank_name, branch_name, ifsc_code, bankaccount_name, bankaccountlevel_gid, bankaccountlevel_name, bankaccount_number, buyer_gid, " +
                " buyer2bank_gid, micr_code, bank_address, bankaccounttype_gid, bankaccounttype_name " +
                " from ocs_mst_tbuyer2bank where buyer_gid='" + employee_gid + "' or buyer_gid='" + buyer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbank_list = new List<bank_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbank_list.Add(new bank_list
                    {
                        bank_name = (dr_datarow["bank_name"].ToString()),
                        branch_name = (dr_datarow["branch_name"].ToString()),
                        ifsc_code = (dr_datarow["ifsc_code"].ToString()),
                        bankaccount_name = (dr_datarow["bankaccount_name"].ToString()),
                        bankaccountlevel_gid = (dr_datarow["bankaccountlevel_gid"].ToString()),
                        bankaccountlevel_name = (dr_datarow["bankaccountlevel_name"].ToString()),
                        bankaccount_number = (dr_datarow["bankaccount_number"].ToString()),
                        buyer2bank_gid = (dr_datarow["buyer2bank_gid"].ToString()),
                        buyer_gid = (dr_datarow["buyer_gid"].ToString()),
                        micr_code = (dr_datarow["micr_code"].ToString()),
                        bank_address = (dr_datarow["bank_address"].ToString()),
                        bankaccounttype_gid = (dr_datarow["bankaccounttype_gid"].ToString()),
                        bankaccounttype_name = (dr_datarow["bankaccounttype_name"].ToString()),
                    });
                }
                values.bank_list = getbank_list;
            }
            dt_datatable.Dispose();
        }
        
        public void DaBankDetailsEdit(string buyer2bank_gid, MdlBank_Details values)
        {
            try
            {
                msSQL = "select bank_name, branch_name, ifsc_code, bankaccount_name, bankaccountlevel_gid, bankaccountlevel_name, bankaccount_number, buyer_gid, confirmbankaccountnumber," +
                    " buyer2bank_gid, micr_code, bank_address, bankaccounttype_gid, bankaccounttype_name " +
                    " from ocs_mst_tbuyer2bank where buyer2bank_gid='" + buyer2bank_gid + "'";
                
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.bank_name = objODBCDatareader["bank_name"].ToString();
                    values.branch_name = objODBCDatareader["branch_name"].ToString();
                    values.ifsc_code = objODBCDatareader["ifsc_code"].ToString();
                    values.bankaccount_name = objODBCDatareader["bankaccount_name"].ToString();
                    values.bankaccountlevel_gid = objODBCDatareader["bankaccountlevel_gid"].ToString();
                    values.bankaccountlevel_name = objODBCDatareader["bankaccountlevel_name"].ToString();
                    values.bankaccount_number = objODBCDatareader["bankaccount_number"].ToString();
                    values.buyer_gid = objODBCDatareader["buyer_gid"].ToString();
                    values.buyer2bank_gid = objODBCDatareader["buyer2bank_gid"].ToString();
                    values.micr_code = objODBCDatareader["micr_code"].ToString();
                    values.bank_address = objODBCDatareader["bank_address"].ToString();
                    values.bankaccounttype_gid = objODBCDatareader["bankaccounttype_gid"].ToString();
                    values.bankaccounttype_name = objODBCDatareader["bankaccounttype_name"].ToString();
                    values.confirmbankaccountnumber = objODBCDatareader["confirmbankaccountnumber"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;
                values.message = "success";
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaBankDetailUpdate(string employee_gid, MdlBank_Details values)
        {
            msSQL = "select bank_name, branch_name, ifsc_code, bankaccount_name, bankaccountlevel_gid, bankaccountlevel_name, bankaccount_number, buyer_gid, buyer2bank_gid, confirmbankaccountnumber," +
                " micr_code, bank_address, bankaccounttype_gid, bankaccounttype_name " +
                " from ocs_mst_tbuyer2bank where buyer2bank_gid='" + values.buyer2bank_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsbank_name = objODBCDatareader["bank_name"].ToString();
                lsbranch_name = objODBCDatareader["branch_name"].ToString();
                lsifsc_code = objODBCDatareader["ifsc_code"].ToString();
                lsbankaccount_name = objODBCDatareader["bankaccount_name"].ToString();
                lsbankaccountlevel_gid = objODBCDatareader["bankaccountlevel_gid"].ToString();
                lsbankaccountlevel_name = objODBCDatareader["bankaccountlevel_name"].ToString();
                lsbankaccount_number = objODBCDatareader["bankaccount_number"].ToString();
                lsbuyer_gid = objODBCDatareader["buyer_gid"].ToString();
                lsbuyer2bank_gid = objODBCDatareader["buyer2bank_gid"].ToString();
                lsconfirmbankaccountnumber = objODBCDatareader["confirmbankaccountnumber"].ToString();
                lsmicr_code = objODBCDatareader["micr_code"].ToString();
                lsbank_address = objODBCDatareader["bank_address"].ToString();
                lsbankaccounttype_gid = objODBCDatareader["bankaccounttype_gid"].ToString();
                lsbankaccounttype_name = objODBCDatareader["bankaccounttype_name"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update ocs_mst_tbuyer2bank set " +
                         " bank_name='" + values.bank_name + "'," +
                         " branch_name='" + values.branch_name + "'," +
                         " ifsc_code='" + values.ifsc_code + "'," +
                         " bankaccount_name='" + values.bankaccount_name + "'," +
                         " bankaccountlevel_gid='" + values.bankaccountlevel_gid + "'," +
                         " bankaccountlevel_name='" + values.bankaccountlevel_name + "'," +
                         " bankaccount_number='" + values.bankaccount_number + "'," +
                         " confirmbankaccountnumber='" + values.confirmbankaccountnumber + "'," +
                         " micr_code='" + values.micr_code + "'," +
                         " bank_address='" + values.bank_address + "'," +
                         " bankaccounttype_gid='" + values.bankaccounttype_gid + "'," +
                         " bankaccounttype_name='" + values.bankaccounttype_name + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where buyer2bank_gid='" + values.buyer2bank_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    if (lsbuyer_gid == values.buyer_gid)
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("BDUL");

                        msSQL = " insert into ocs_mst_tbuyer2bankupdatelog(" +
                    " buyer2bankupdatelog_gid," +
                    " buyer2bank_gid," +
                    " buyer_gid," +
                    " bank_name," +
                    " branch_name," +
                    " ifsc_code," +
                    " bankaccount_name," +
                    " bankaccountlevel_gid," +
                    " bankaccountlevel_name," +
                    " bankaccount_number," +
                    " confirmbankaccountnumber," +
                    " micr_code," +
                    " bank_address," +
                    " bankaccounttype_gid," +
                    " bankaccounttype_name," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.buyer2bank_gid + "'," +
                    "'" + values.buyer_gid + "'," +
                    "'" + values.bank_name + "'," +
                    "'" + values.branch_name + "'," +
                    "'" + values.ifsc_code + "'," +
                    "'" + values.bankaccount_name + "'," +
                    "'" + values.bankaccountlevel_gid + "'," +
                    "'" + values.bankaccountlevel_name + "'," +
                    "'" + values.bankaccount_number + "'," +
                    "'" + values.confirmbankaccountnumber + "'," +
                    "'" + values.micr_code + "'," +
                    "'" + values.bank_address + "'," +
                    "'" + values.bankaccounttype_gid + "'," +
                    "'" + values.bankaccounttype_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    values.status = true;
                    values.message = "Bank Details Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaDeleteBankDetail(string buyer2bank_gid, string employee_gid, MdlBank_Details values)
        {
            msSQL = "delete from ocs_mst_tbuyer2bank where buyer2bank_gid='" + buyer2bank_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from ocs_mst_tbuyer2bankupdatelog where buyer2bank_gid='" + buyer2bank_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Bank Deatils Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting Bank Details";
                values.status = false;

            }
        }

// Address Detail
        
        public bool DaAddressDetailAdd(string employee_gid, string user_gid, MdlMstaddresstype values)
        {
            msSQL = "select primary_address from ocs_mst_tbuyer2address where primary_address='Yes' and buyer_gid='" + employee_gid + "'";
            string lsprimary_address = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_address == (values.primary_address))
            {
                values.status = false;
                values.message = "Already Primary Address added";
                return false;
            }
            msSQL = "select buyer2address_gid from ocs_mst_tbuyer2address where addresstype_name='" + values.address_type + "' and buyer_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already Address Type added";
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("B2AD");
            msSQL = " insert into ocs_mst_tbuyer2address(" +
                    " buyer2address_gid," +
                    " buyer_gid," +
                    " addresstype_gid," +
                    " addresstype_name," +
                    " addressline1," +
                    " addressline2," +
                    " primary_address," +
                    " landmark," +
                    " postal_code," +
                    " city," +
                    " taluka," +
                    " district," +
                    " state_name," +
                    " country," +
                    " latitude," +
                    " longitude," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.address_typegid + "'," +
                    "'" + values.address_type + "'," +
                    "'" + values.addressline1 + "'," +
                    "'" + values.addressline2 + "'," +
                    "'" + values.primary_address + "'," +
                    "'" + values.landmark + "'," +
                    "'" + values.postal_code + "'," +
                    "'" + values.city + "'," +
                    "'" + values.taluka + "'," +
                    "'" + values.district + "'," +
                    "'" + values.state + "'," +
                    "'" + values.country + "'," +
                    "'" + values.latitude + "'," +
                    "'" + values.longitude + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Address Details Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Adding Address";
                return false;
            }

        }

        public void DaGetAddressList(string buyer_gid, string employee_gid, MdlbuyerAddress values)
        {
            msSQL = "  select buyer2address_gid,addresstype_name,primary_address, addressline1, addressline2, taluka, district, state_name, country, latitude, longitude, landmark," +
                    " postal_code from ocs_mst_tbuyer2address where buyer_gid='" + employee_gid + "' or buyer_gid='" + buyer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbuyeraddress_list = new List<buyeraddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbuyeraddress_list.Add(new buyeraddress_list
                    {
                        buyer2address_gid = (dr_datarow["buyer2address_gid"].ToString()),
                        addresstype_name = (dr_datarow["addresstype_name"].ToString()),
                        primary_address = (dr_datarow["primary_address"].ToString()),
                        addressline1 = (dr_datarow["addressline1"].ToString()),
                        addressline2 = (dr_datarow["addressline2"].ToString()),
                        taluka = (dr_datarow["taluka"].ToString()),
                        district = (dr_datarow["district"].ToString()),
                        state_name = (dr_datarow["state_name"].ToString()),
                        country = (dr_datarow["country"].ToString()),
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString()),
                        landmark = (dr_datarow["landmark"].ToString())
                    });
                }
                values.buyeraddress_list = getbuyeraddress_list;
            }
            dt_datatable.Dispose();
        }
        
        public void DaAddressDetailEdit(string buyer2address_gid, MdlMstaddresstype values)
        {
            try
            {
                msSQL = "select addresstype_gid, addresstype_name, addressline1, addressline2, landmark, taluka, primary_address, postal_code, city," +
                    " district, state_name, country, latitude, longitude, buyer_gid, buyer2address_gid " +
                    " from ocs_mst_tbuyer2address where buyer2address_gid='" + buyer2address_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.address_typegid = objODBCDatareader["addresstype_gid"].ToString();
                    values.address_type = objODBCDatareader["addresstype_name"].ToString();
                    values.addressline1 = objODBCDatareader["addressline1"].ToString();
                    values.addressline2 = objODBCDatareader["addressline2"].ToString();
                    values.landmark = objODBCDatareader["landmark"].ToString();
                    values.taluka = objODBCDatareader["taluka"].ToString();
                    values.primary_address = objODBCDatareader["primary_address"].ToString();
                    values.postal_code = objODBCDatareader["postal_code"].ToString();
                    values.city = objODBCDatareader["city"].ToString();
                    values.district = objODBCDatareader["district"].ToString();
                    values.state = objODBCDatareader["state_name"].ToString();
                    values.country = objODBCDatareader["country"].ToString();
                    values.latitude = objODBCDatareader["latitude"].ToString();
                    values.longitude = objODBCDatareader["longitude"].ToString();
                    values.buyer_gid = objODBCDatareader["buyer_gid"].ToString();
                    values.buyer2address_gid = objODBCDatareader["buyer2address_gid"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;
                values.message = "success";
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaAddressDetailUpdate(string employee_gid, MdlMstaddresstype values)
        {
            msSQL = "select addresstype_gid, addresstype_name, addressline1, addressline2, landmark, taluka, primary_address, postal_code, city," +
                    " district, state_name, country, latitude, longitude, buyer_gid, buyer2address_gid " +
                    " from ocs_mst_tbuyer2address where buyer2address_gid='" + values.buyer2address_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsaddress_typegid = objODBCDatareader["addresstype_gid"].ToString();
                lsaddress_type = objODBCDatareader["addresstype_name"].ToString();
                lsaddressline1 = objODBCDatareader["addressline1"].ToString();
                lsaddressline2 = objODBCDatareader["addressline2"].ToString();
                lslandmark = objODBCDatareader["landmark"].ToString();
                lstaluka = objODBCDatareader["taluka"].ToString();
                lsprimary_address = objODBCDatareader["primary_address"].ToString();
                lspostal_code = objODBCDatareader["postal_code"].ToString();
                lscity = objODBCDatareader["city"].ToString();
                lsdistrict = objODBCDatareader["district"].ToString();
                lsstate = objODBCDatareader["state_name"].ToString();
                lscountry = objODBCDatareader["country"].ToString();
                lslatitude = objODBCDatareader["latitude"].ToString();
                lslongitude = objODBCDatareader["longitude"].ToString();
                lsbuyer_gid = objODBCDatareader["buyer_gid"].ToString();
                lsbuyer2address_gid = objODBCDatareader["buyer2address_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update ocs_mst_tbuyer2address set " +
                         " addresstype_gid='" + values.address_typegid + "'," +
                         " addresstype_name='" + values.address_type + "'," +
                         " addressline1='" + values.addressline1 + "'," +
                         " addressline2='" + values.addressline2 + "'," +
                         " landmark='" + values.landmark + "'," +
                         " taluka='" + values.taluka + "'," +
                         " primary_address='" + values.primary_address + "'," +
                         " postal_code='" + values.postal_code + "'," +
                         " city='" + values.city + "'," +
                         " district='" + values.district + "'," +
                         " state_name='" + values.state + "'," +
                         " country='" + values.country + "'," +
                         " latitude='" + values.latitude + "'," +
                         " longitude='" + values.longitude + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where buyer2address_gid='" + values.buyer2address_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    if (lsbuyer_gid == values.buyer_gid)
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("ADUL");

                        msSQL = " insert into ocs_mst_tbuyer2addressupdatelog(" +
                      " buyer2addressupdatelog_gid," +
                      " buyer2address_gid," +
                      " buyer_gid," +
                      " addresstype_gid," +
                      " addresstype_name," +
                      " addressline1," +
                      " addressline2," +
                      " primary_address," +
                      " landmark," +
                      " postal_code," +
                      " city," +
                      " taluka," +
                      " district," +
                      " state_name," +
                      " country," +
                      " latitude," +
                      " longitude," +
                      " created_by," +
                      " created_date)" +
                      " values(" +
                      "'" + msGetGid + "'," +
                      "'" + values.buyer2address_gid + "'," +
                      "'" + values.buyer_gid + "'," +
                      "'" + values.address_typegid + "'," +
                      "'" + values.address_type + "'," +
                      "'" + values.addressline1 + "'," +
                      "'" + values.addressline2 + "'," +
                      "'" + values.primary_address + "'," +
                      "'" + values.landmark + "'," +
                      "'" + values.postal_code + "'," +
                      "'" + values.city + "'," +
                      "'" + values.taluka + "'," +
                      "'" + values.district + "'," +
                      "'" + values.state + "'," +
                      "'" + values.country + "'," +
                      "'" + values.latitude + "'," +
                      "'" + values.longitude + "'," +
                      "'" + employee_gid + "'," +
                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    values.status = true;
                    values.message = "Address Details Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaDeleteAddressDetail(string buyer2address_gid, string employee_gid, MdlMstaddresstype values)
        {
            msSQL = "delete from ocs_mst_tbuyer2address where buyer2address_gid='" + buyer2address_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from ocs_mst_tbuyer2addressupdatelog where buyer2address_gid='" + buyer2address_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Address Deatils Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While deleting Address Details";
                values.status = false;

            }
        }

        public bool DaSaveAsDraftbuyer(string employee_gid, string user_gid, buyeredit values)
        {
            msSQL = "update ocs_mst_tbureauscoreadd set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update ocs_mst_tbureauscoredocumentadd set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update ocs_mst_tbuyer2mobileno set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update ocs_mst_tbuyer2emailaddress set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            
            msSQL = "update ocs_mst_tbuyer2address set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update ocs_mst_tbuyer2bank set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update ocs_mst_tbuyer2gst set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ocs_mst_tbuyer set " +
                        " cap_limit='" + values.cap_limit + "'," +
                        " overall_limit='" + values.overall_limit + "'," +
                        " buyer_limit='" + values.buyer_limit + "'," +
                        " guarantor_limit='" + values.guarantor_limit + "'," +
                        " borrower_limit='" + values.borrower_limit + "'," +
                        " credit_status='Pending'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where buyer_gid='" + values.buyer_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Credit Status Saved Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }

        }

        public bool DaSubmitCreditStatus(string employee_gid, string user_gid, buyeredit values)
        {
            msSQL = "select buyer2mobileno_gid from ocs_mst_tbuyer2mobileno where buyer_gid='" + employee_gid + "' or buyer_gid ='" + values.buyer_gid + "' and primary_mobileno='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Mobile Number";
                return false;
            }
            objODBCDatareader.Close();
            msSQL = "select buyer2mobileno_gid from ocs_mst_tbuyer2mobileno where buyer_gid='" + employee_gid + "' or buyer_gid ='" + values.buyer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Mobile Number";
                return false;
            }
            objODBCDatareader.Close();
            //msSQL = "select buyer_gid from ocs_mst_tbuyer2gst where buyer_gid='" + employee_gid + "' or buyer_gid ='" + values.buyer_gid + "'";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);

            //if (objODBCDatareader.HasRows == false)
            //{
            //    values.status = false;
            //    values.message = "Add Atleast One GST detail";
            //    return false;
            //}

            msSQL = "select buyer2address_gid from ocs_mst_tbuyer2address where buyer_gid='" + employee_gid + "' or buyer_gid ='" + values.buyer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Address";
                return false;
            }
            objODBCDatareader.Close();
            //msSQL = "select buyer_gid from ocs_mst_tbuyer2emailaddress where buyer_gid='" + employee_gid + "' or buyer_gid ='" + values.buyer_gid + "'";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);

            //if (objODBCDatareader.HasRows == false)
            //{
            //    values.status = false;
            //    values.message = "Add Atleast One Email Address";
            //    return false;
            //}
            //objODBCDatareader.Close();
            //msSQL = "select buyer_gid from ocs_mst_tbuyer2bank where buyer_gid='" + employee_gid + "' or buyer_gid ='" + values.buyer_gid + "'";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);

            //if (objODBCDatareader.HasRows == false)
            //{
            //    values.status = false;
            //    values.message = "Add Atleast One Bank Detail";
            //    return false;
            //}

            msSQL = "update ocs_mst_tbureauscoreadd set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update ocs_mst_tbureauscoredocumentadd set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update ocs_mst_tbuyer2mobileno set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update ocs_mst_tbuyer2emailaddress set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update ocs_mst_tbuyer2address set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update ocs_mst_tbuyer2bank set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update ocs_mst_tbuyer2gst set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ocs_mst_tbuyer set " +
                        " cap_limit='" + values.cap_limit + "'," +
                        " overall_limit='" + values.overall_limit + "'," +
                        " buyer_limit='" + values.buyer_limit + "'," +
                        " guarantor_limit='" + values.guarantor_limit + "'," +
                        " borrower_limit='" + values.borrower_limit + "'," +
                        " credit_status='Completed'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where buyer_gid='" + values.buyer_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Credit Status Submitted Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }

        }

        public bool DaUpdateCreditStatus(string employee_gid, string user_gid, buyeredit values)
        {
            msSQL = "select buyer2mobileno_gid from ocs_mst_tbuyer2mobileno where buyer_gid='" + employee_gid + "' or buyer_gid ='" + values.buyer_gid + "' and primary_mobileno='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Mobile Number";
                return false;
            }

            msSQL = "select buyer2mobileno_gid from ocs_mst_tbuyer2mobileno where buyer_gid='" + employee_gid + "' or buyer_gid ='" + values.buyer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Mobile Number";
                return false;
            }

            msSQL = "select buyer2gst_gid from ocs_mst_tbuyer2gst where buyer_gid='" + employee_gid + "' or buyer_gid ='" + values.buyer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One GST detail";
                return false;
            }

            msSQL = "select buyer2address_gid from ocs_mst_tbuyer2address where buyer_gid='" + employee_gid + "' or buyer_gid ='" + values.buyer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Address";
                return false;
            }

            msSQL = "select buyer2emailaddress_gid from ocs_mst_tbuyer2emailaddress where buyer_gid='" + employee_gid + "' or buyer_gid ='" + values.buyer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Email Address";
                return false;
            }

            msSQL = "select buyer2bank_gid from ocs_mst_tbuyer2bank where buyer_gid='" + employee_gid + "' or buyer_gid ='" + values.buyer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Bank Detail";
                return false;
            }

            msSQL = "update ocs_mst_tbureauscoreadd set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update ocs_mst_tbureauscoredocumentadd set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update ocs_mst_tbuyer2mobileno set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update ocs_mst_tbuyer2emailaddress set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update ocs_mst_tbuyer2address set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update ocs_mst_tbuyer2bank set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update ocs_mst_tbuyer2gst set buyer_gid ='" + values.buyer_gid + "' where buyer_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ocs_mst_tbuyer set " +
                        " cap_limit='" + values.cap_limit + "'," +
                        " overall_limit='" + values.overall_limit + "'," +
                        " buyer_limit='" + values.buyer_limit + "'," +
                        " guarantor_limit='" + values.guarantor_limit + "'," +
                        " borrower_limit='" + values.borrower_limit + "'," +
                        " credit_status='Completed'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where buyer_gid='" + values.buyer_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Credit Status Updated Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }

        }

        // GST Details

        public void DaGetGSTList(string buyer_gid, string employee_gid, MdlbuyerGST values)
        {
            msSQL = "select buyer2gst_gid,gststate_name,gst_no, gstregister_status from ocs_mst_tbuyer2gst where buyer_gid='" + employee_gid + "' or buyer_gid='" + buyer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbuyergst_list = new List<buyergst_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbuyergst_list.Add(new buyergst_list
                    {
                        buyer2gst_gid = (dr_datarow["buyer2gst_gid"].ToString()),
                        gststate_name = (dr_datarow["gststate_name"].ToString()),
                        gst_no = (dr_datarow["gst_no"].ToString()),
                        gstregister_status = (dr_datarow["gstregister_status"].ToString())
                    });
                }
                values.buyergst_list = getbuyergst_list;
            }
            dt_datatable.Dispose();
        }

        public bool DaPostGST(string employee_gid, MdlbuyerGST values)
        {

            msGetGid = objcmnfunctions.GetMasterGID("B2GS");
            msSQL = " insert into ocs_mst_tbuyer2gst(" +
                    " buyer2gst_gid," +
                    " buyer_gid," +
                    " gststate_name," +
                    " gst_no," +
                    " gstregister_status," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.gststate_name + "'," +
                    "'" + values.gst_no + "'," +
                    "'" + values.gstregister_status + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "GST details Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured while Adding GST Details";
                return false;
            }

        }
        
        public void DaDeleteGST(string buyer2gst_gid, MdlbuyerGST values)
        {
            msSQL = "delete from ocs_mst_tbuyer2gst where buyer2gst_gid='" + buyer2gst_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from ocs_mst_tbuyer2gstupdatelog where buyer2gst_gid='" + buyer2gst_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "GST Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting GST Details";
                values.status = false;

            }
        }

        public void DaGSTEdit(string buyer2gst_gid, MdlbuyerGST values)
        {
            try
            {
                msSQL = "select gststate_name, gst_no, buyer_gid, buyer2gst_gid, gstregister_status from ocs_mst_tbuyer2gst where buyer2gst_gid='" + buyer2gst_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.gststate_name = objODBCDatareader["gststate_name"].ToString();
                    values.gst_no = objODBCDatareader["gst_no"].ToString();
                    values.buyer2gst_gid = objODBCDatareader["buyer2gst_gid"].ToString();
                    values.buyer_gid = objODBCDatareader["buyer_gid"].ToString();
                    values.gstregister_status = objODBCDatareader["gstregister_status"].ToString();
                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaGSTUpdate(string employee_gid, MdlbuyerGST values)
        {
            msSQL = "select gststate_name, gst_no, gstregister_status, buyer_gid, buyer2gst_gid from ocs_mst_tbuyer2gst where buyer2gst_gid='" + values.buyer2gst_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsgststate_name = objODBCDatareader["gststate_name"].ToString();
                lsgst_no = objODBCDatareader["gst_no"].ToString();
                lsbuyer2gst_gid = objODBCDatareader["buyer2gst_gid"].ToString();
                lsbuyer_gid = objODBCDatareader["buyer_gid"].ToString();
                lsgstregister_status = objODBCDatareader["gstregister_status"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update ocs_mst_tbuyer2gst set " +
                         " gststate_name='" + values.gststate_name + "'," +
                         " gst_no='" + values.gst_no + "'," +
                         " gstregister_status='" + values.gstregister_status + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where buyer2gst_gid='" + values.buyer2gst_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    if (lsbuyer_gid == values.buyer_gid)
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("BGST");

                        msSQL = "Insert into ocs_mst_tbuyer2gstupdatelog(" +
                       " buyer2gstupdatelog_gid, " +
                       " buyer2gst_gid, " +
                       " buyer_gid, " +
                       " gststate_name," +
                       " gst_no," +
                       " gstregister_status," +
                       " created_by," +
                       " created_date)" +
                       " values (" +
                       "'" + msGetGid + "'," +
                       "'" + values.buyer2gst_gid + "'," +
                       "'" + values.buyer_gid + "'," +
                       "'" + lsgststate_name + "'," +
                       "'" + lsgst_no + "'," +
                       "'" + lsgstregister_status + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    values.status = true;
                    values.message = "buyer GST Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        // Inactive Credit Status

        public void DaInactiveCreditStatusbuyer(buyeredit values, string employee_gid)
        {
            msSQL = " update ocs_mst_tbuyer set creditActive_status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where buyer_gid='" + values.buyer_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("BCIL");

                msSQL = " insert into ocs_mst_tbuyerinactivelog (" +
                      " buyerinactivelog_gid, " +
                      " buyer_gid," +
                      " buyer_name," +
                      " creditActive_status," +
                      " remarks," +
                      " created_by," +
                      " created_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.buyer_gid + "'," +
                      " '" + values.buyer_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "buyer Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "buyer Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaCreditStatusbuyerInactiveLogview(string buyer_gid, buyeredit values)
        {
            try
            {
                msSQL = " SELECT buyer_gid,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.creditActive_status='N' then 'Inactive' else 'Active' end as creditActive_status, a.remarks" +
                        " FROM ocs_mst_tbuyerinactivelog a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where buyer_gid ='" + buyer_gid + "' order by a.buyerinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getbuyerInactive_List = new List<buyerInactive_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getbuyerInactive_List.Add(new buyerInactive_List
                        {
                            buyer_gid = (dr_datarow["buyer_gid"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            creditActive_status = (dr_datarow["creditActive_status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.buyerInactive_List = getbuyerInactive_List;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DabuyerMobileNoList(string buyer_gid, string employee_gid, Mdlmobile_no values)
        {
            msSQL = "select mobile_no,primary_mobileno,whatsapp_mobileno, buyer2mobileno_gid from ocs_mst_tbuyer2mobileno " +
                " where buyer_gid='" + buyer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmobileno_list = new List<mobileno_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmobileno_list.Add(new mobileno_list
                    {
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_mobileno = (dr_datarow["primary_mobileno"].ToString()),
                        whatsapp_mobileno = (dr_datarow["whatsapp_mobileno"].ToString()),
                        buyer2mobileno_gid = (dr_datarow["buyer2mobileno_gid"].ToString()),
                    });
                }
                values.mobileno_list = getmobileno_list;
            }
            dt_datatable.Dispose();
        }

        public void DabuyerEmailAddressList(string buyer_gid, string employee_gid, MdlEmail_address values)
        {
            msSQL = "select email_address, primary_emailaddress, buyer2emailaddress_gid, buyer_gid from ocs_mst_tbuyer2emailaddress" +
                  " where buyer_gid='" + buyer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getemail_list = new List<email_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getemail_list.Add(new email_list
                    {
                        email_address = (dr_datarow["email_address"].ToString()),
                        primary_emailaddress = (dr_datarow["primary_emailaddress"].ToString()),
                        buyer2emailaddress_gid = (dr_datarow["buyer2emailaddress_gid"].ToString()),
                        buyer_gid = (dr_datarow["buyer_gid"].ToString()),
                    });
                }
                values.email_list = getemail_list;
            }
            dt_datatable.Dispose();
        }

        public void DabuyerBankList(string buyer_gid, string employee_gid, MdlBank_Details values)
        {
            msSQL = "select bank_name, branch_name, ifsc_code, bankaccount_name, bankaccountlevel_gid, bankaccountlevel_name, bankaccount_number, buyer_gid, buyer2bank_gid," +
                " micr_code, bank_address, bankaccounttype_gid, bankaccounttype_name " +
                " from ocs_mst_tbuyer2bank where buyer_gid='" + buyer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbank_list = new List<bank_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbank_list.Add(new bank_list
                    {
                        bank_name = (dr_datarow["bank_name"].ToString()),
                        branch_name = (dr_datarow["branch_name"].ToString()),
                        ifsc_code = (dr_datarow["ifsc_code"].ToString()),
                        bankaccount_name = (dr_datarow["bankaccount_name"].ToString()),
                        bankaccountlevel_gid = (dr_datarow["bankaccountlevel_gid"].ToString()),
                        bankaccountlevel_name = (dr_datarow["bankaccountlevel_name"].ToString()),
                        bankaccount_number = (dr_datarow["bankaccount_number"].ToString()),
                        buyer2bank_gid = (dr_datarow["buyer2bank_gid"].ToString()),
                        buyer_gid = (dr_datarow["buyer_gid"].ToString()),
                        micr_code = (dr_datarow["micr_code"].ToString()),
                        bank_address = (dr_datarow["bank_address"].ToString()),
                        bankaccounttype_gid = (dr_datarow["bankaccounttype_gid"].ToString()),
                        bankaccounttype_name = (dr_datarow["bankaccounttype_name"].ToString()),
                    });
                }
                values.bank_list = getbank_list;
            }
            dt_datatable.Dispose();
        }

        public void DabuyerAddressList(string buyer_gid, string employee_gid, MdlbuyerAddress values)
        {
            msSQL = "  select buyer2address_gid,addresstype_name,primary_address, addressline1, addressline2, taluka, district, state_name, country, latitude, longitude, city,landmark," +
                    " postal_code from ocs_mst_tbuyer2address where buyer_gid='" + buyer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbuyeraddress_list = new List<buyeraddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbuyeraddress_list.Add(new buyeraddress_list
                    {
                        buyer2address_gid = (dr_datarow["buyer2address_gid"].ToString()),
                        addresstype_name = (dr_datarow["addresstype_name"].ToString()),
                        primary_address = (dr_datarow["primary_address"].ToString()),
                        addressline1 = (dr_datarow["addressline1"].ToString()),
                        addressline2 = (dr_datarow["addressline2"].ToString()),
                        taluka = (dr_datarow["taluka"].ToString()),
                        district = (dr_datarow["district"].ToString()),
                        state_name = (dr_datarow["state_name"].ToString()),
                        country = (dr_datarow["country"].ToString()),
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString()),
                        city = (dr_datarow["city"].ToString()),
                        landmark = (dr_datarow["landmark"].ToString())
                    });
                }
                values.buyeraddress_list = getbuyeraddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DabuyerGSTList(string buyer_gid, string employee_gid, MdlbuyerGST values)
        {
            msSQL = "select buyer2gst_gid,gststate_name,gst_no, gstregister_status from ocs_mst_tbuyer2gst where buyer_gid='" + buyer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbuyergst_list = new List<buyergst_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbuyergst_list.Add(new buyergst_list
                    {
                        buyer2gst_gid = (dr_datarow["buyer2gst_gid"].ToString()),
                        gststate_name = (dr_datarow["gststate_name"].ToString()),
                        gst_no = (dr_datarow["gst_no"].ToString()),
                        gstregister_status = (dr_datarow["gstregister_status"].ToString())
                    });
                }
                values.buyergst_list = getbuyergst_list;
            }
            dt_datatable.Dispose();
        }

        public void DaBureauDocList(string bureauscoreadd_GID, string employee_gid, MdlMstCreditStatusAdd values)
        {
            msSQL = " select bureauscoredocumentadd_gid,document_name,document_path, " +
                    " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date " +
                    " from ocs_mst_tbureauscoredocumentadd a " +
                    " left join adm_mst_tuser b on a.created_by = b.user_gid where bureauscoreadd_GID='" + bureauscoreadd_GID + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<upload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new upload_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });
                    values.upload_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public bool DaCreditStatusApproval(buyeredit values, string employee_gid)
        {
            if (values.approval_remarks == null || values.approval_remarks == "")
            {
                lsapproval_remarks = "";
            }
            else
            {
                lsapproval_remarks = values.approval_remarks.Replace("'", "");
            }

            msSQL = " update ocs_mst_tbuyer set creditstatus_Approval='" + values.rbo_status + "'," +
                    " approval_remarks='" + lsapproval_remarks + "'" +
                    " where buyer_gid='" + values.buyer_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("BALG");

                msSQL = " insert into ocs_mst_tbuyerapprovallog (" +
                      " buyerapprovallog_gid, " +
                      " buyer_gid," +
                      " buyer_name," +
                      " creditstatus_Approval," +
                      " approval_remarks," +
                      " created_by," +
                      " created_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.buyer_gid + "'," +
                      " '" + values.buyer_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + lsapproval_remarks + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Buyer Approved Successfully";

                try
                {
                    msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        ls_server = objODBCDatareader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                        ls_username = objODBCDatareader["pop_username"].ToString();
                        ls_password = objODBCDatareader["pop_password"].ToString();

                    }
                    objODBCDatareader.Close();

                    msSQL = " select employee_emailid from ocs_mst_tbuyer a "+
                            " left join hrm_mst_temployee b on a.created_by=b.employee_gid where buyer_gid='" + values.buyer_gid + "' ";
                    tomail_id = objdbconn.GetExecuteScalar(msSQL);
                    
                    msSQL = "select state_name from ocs_mst_tbuyer2address where buyer_gid='" + values.buyer_gid + "' ";
                    lsstate_name = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select  buyer_name,  date_format(a.created_date, '%d-%m-%Y %h:%i:%s %p') as 'updated_date'," +
                     " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as updated_by" +
                     " from ocs_mst_tbuyer a  left join hrm_mst_temployee b on b.employee_gid = a.updated_by" +
                     " left join adm_mst_tuser c on b.user_gid = c.user_gid  " +
                     " where buyer_gid='" + values.buyer_gid + "' ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsupdatedby = objODBCDatareader["updated_by"].ToString();
                        lsupdateddate = objODBCDatareader["updated_date"].ToString();
                        lsbuyer_name = objODBCDatareader["buyer_name"].ToString();
                    }
                    lssource = ConfigurationManager.AppSettings["img_path"];
                    objODBCDatareader.Close();
                    sub = "New Buyer Approved ";
                    body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
                    body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;align:center;'>";
                    body = body + "<br />";
                    body = body + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <img style='height:150px; width:380px;' src='" + lssource + "'><br />";
                    body = body + "<br />";
                    body = body + " &nbsp &nbsp Dear Sir / Madam, <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp Greetings! <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp The below buyer has been approved. Please proceed with the application in Sam-Custopedia. <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Buyer Name : </b> " + HttpUtility.HtmlEncode(lsbuyer_name) + "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Buyer State : </b> " + HttpUtility.HtmlEncode(lsstate_name) + "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Approved by : </b> " + HttpUtility.HtmlEncode(lsupdatedby) + "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp <b> Approved Time : </b> " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  <br />";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp Regards";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp Sam-Custopedia <br /> ";
                    body = body + "<br />";
                    body = body + "&nbsp &nbsp<hr>&nbsp&nbsp";
                    body = body + "&nbsp &nbsp Reach out to us at samcustopedia@samunnati.com <br /> ";
                    body = body + "<br />";
                    body = body + "</td><td style='margin-left:20px; border-left-color:white;'>&nbsp&nbsp</td></tr></table>";
                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(ls_username);
                    message.To.Add(tomail_id);
                    
                    message.Subject = sub;
                    message.IsBodyHtml = true; //to make message body as html  
                    message.Body = body;
                    smtp.Port = ls_port;
                    smtp.Host = ls_server; //for gmail host  
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                    values.status = true;
                    return true;
                }
                catch (Exception ex)
                {
                    values.message = ex.ToString();
                    values.status = false;
                    return false;
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
                return false;
            }
        }

        public void DaGetCreditStatusApprovedBuyer(string employee_gid, MdlMstCreditStatusAdd values)
        {
            msSQL = " select buyer_gid, buyer_code, buyer_name,f.department_name, a.credit_status, case when a.creditActive_status='N' then 'Inactive' else 'Active' end as creditActive_status, " +
                    " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as updated_by,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date" +
                    " from ocs_mst_tbuyer a " +
                    " left join hrm_mst_temployee c on c.employee_gid=a.created_by " +
                    " left join adm_mst_tuser b on c.user_gid=b.user_gid" +
                    " left join hrm_mst_temployee d on d.employee_gid=a.updated_by " +
                    " left join adm_mst_tuser e on d.user_gid=e.user_gid" +
                    " left join hrm_mst_tdepartment f on f.department_gid = c.department_gid " +
                    " where a.creditstatus_Approval='Y' order by buyer_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditstatuslist = new List<creditstatuslist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcreditstatuslist.Add(new creditstatuslist
                    {
                        buyer_gid = dt["buyer_gid"].ToString(),
                        buyer_code = dt["buyer_code"].ToString(),
                        buyer_name = dt["buyer_name"].ToString(),
                        credit_status = dt["credit_status"].ToString(),
                        department_name = dt["department_name"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        updated_by = dt["updated_by"].ToString(),
                        updated_date = dt["updated_date"].ToString(),
                        creditActive_status = dt["creditActive_status"].ToString(),
                    });
                    values.creditstatuslist = getcreditstatuslist;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetCreditStatusNonApprovedBuyer(string employee_gid, MdlMstCreditStatusAdd values)
        {
            msSQL = " select buyer_gid, buyer_code, buyer_name,f.department_name, a.credit_status, case when a.creditActive_status='N' then 'Inactive' else 'Active' end as creditActive_status, " +
                    " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as updated_by,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date" +
                    " from ocs_mst_tbuyer a " +
                    " left join hrm_mst_temployee c on c.employee_gid=a.created_by " +
                    " left join adm_mst_tuser b on c.user_gid=b.user_gid" +
                    " left join hrm_mst_temployee d on d.employee_gid=a.updated_by " +
                    " left join adm_mst_tuser e on d.user_gid=e.user_gid" +
                    " left join hrm_mst_tdepartment f on f.department_gid = c.department_gid " +
                    " where a.creditstatus_Approval='N' order by buyer_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditstatuslist = new List<creditstatuslist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcreditstatuslist.Add(new creditstatuslist
                    {
                        buyer_gid = dt["buyer_gid"].ToString(),
                        buyer_code = dt["buyer_code"].ToString(),
                        buyer_name = dt["buyer_name"].ToString(),
                        credit_status = dt["credit_status"].ToString(),
                        department_name = dt["department_name"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        updated_by = dt["updated_by"].ToString(),
                        updated_date = dt["updated_date"].ToString(),
                        creditActive_status = dt["creditActive_status"].ToString(),
                    });
                    values.creditstatuslist = getcreditstatuslist;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetCreditStatusApprovalLogview(string buyer_gid, buyeredit values)
        {
            try
            {
                msSQL = " SELECT buyer_gid,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.creditstatus_Approval='N' then 'Non Approved' else 'Approved' end as creditstatus_Approval, a.approval_remarks" +
                        " FROM ocs_mst_tbuyerapprovallog a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where buyer_gid ='" + buyer_gid + "' order by a.buyerapprovallog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getbuyerApproval_List = new List<buyerApproval_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getbuyerApproval_List.Add(new buyerApproval_List
                        {
                            buyer_gid = (dr_datarow["buyer_gid"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            creditstatus_Approval = (dr_datarow["creditstatus_Approval"].ToString()),
                            approval_remarks = (dr_datarow["approval_remarks"].ToString()),
                        });
                    }
                    values.buyerApproval_List = getbuyerApproval_List;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetCreditStatusCount(string employee_gid, string user_gid, buyer_count values)
        {
            msSQL = "SELECT COUNT(buyer_gid) as count_pending FROM ocs_mst_tbuyer WHERE buyertocredit_flag='Y' and creditstatus_Approval='NA'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Read();
                values.count_pending = objODBCDatareader["count_pending"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = "SELECT COUNT(buyer_gid) as count_approved FROM ocs_mst_tbuyer WHERE creditstatus_Approval='Y'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Read();
                values.count_approved = objODBCDatareader["count_approved"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = "SELECT COUNT(buyer_gid) as count_nonapproved FROM ocs_mst_tbuyer WHERE creditstatus_Approval='N'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Read();
                values.count_nonapproved = objODBCDatareader["count_nonapproved"].ToString();
            }
            objODBCDatareader.Close();
        }
    }
}